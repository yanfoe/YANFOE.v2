// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheTvdb.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the TheTvdb type.
// </summary>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//
//   See this page: http://www.yanfoe.com/license
//   
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Scrapers.TV
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using Ionic.Zip;

    using YANFOE.Factories;
    using YANFOE.Factories.Import;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Download;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Models.TvModels.TVDB;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.IO;
    using YANFOE.Tools.Xml;
    using YANFOE.UI.Dialogs.TV;

    /// <summary>
    /// The the tvdb.
    /// </summary>
    public class TheTvdb
    {
        #region Constants and Fields

        /// <summary>
        /// The tvdb api.
        /// </summary>
        public const string TvdbApi = "71F9C54F38B71B4F";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TheTvdb"/> class.
        /// </summary>
        public TheTvdb()
        {
            this.Mirrors = new List<Mirrors>();
            this.ServerTime = string.Empty;

            this.ServerTime = DownloadServerTime();

            Get.Scraper.TvDbTime = this.ServerTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Mirrors.
        /// </summary>
        public List<Mirrors> Mirrors { get; set; }

        /// <summary>
        /// Gets or sets ServerTime.
        /// </summary>
        public string ServerTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the banner download path.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>
        /// The return banner download path.
        /// </returns>
        public static string ReturnBannerDownloadPath(string filename, bool useCache = false)
        {
            if (useCache)
            {
                return string.Format(@"http://www.thetvdb.com/banners/_cache/{0}", filename); 
            }

            return string.Format(@"http://www.thetvdb.com/banners/{0}", filename);
        }

        /// <summary>
        /// Apply the scan to the current tv db.
        /// </summary>
        public void ApplyScan()
        {
            foreach (var series in ImportTvFactory.Scan)
            {
                if (!string.IsNullOrEmpty(series.Key))
                {
                    KeyValuePair<string, ScanSeries> series1 = series;
                    string checkScanSeriesPick =
                        (from s in ImportTvFactory.ScanSeriesPicks
                         where s.SearchString == series1.Key
                         select s.SeriesName).SingleOrDefault();

                    string seriesKey = checkScanSeriesPick ?? series.Key;

                    foreach (var season in ImportTvFactory.Scan[series.Key].Seasons)
                    {
                        foreach (var episode in ImportTvFactory.Scan[series.Key].Seasons[season.Key].Episodes)
                        {
                            try
                            {
                                foreach (Episode s in TvDBFactory.TvDatabase[seriesKey].Seasons[season.Key].Episodes)
                                {
                                    if (s.EpisodeNumber == episode.Key)
                                    {
                                        s.FilePath.FileNameAndPath = episode.Value.FilePath;

                                        s.SecondaryTo = episode.Value.SecondaryTo;
                                    }
                                }
                            }
                            catch
                            {
                                ImportTvFactory.NotCatagorized.Add(
                                    new ScanNotCatagorized { FilePath = episode.Value.FilePath });
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the series needs updating, and if so returns an updated series.
        /// </summary>
        /// <param name="seriesId">The series id.</param>
        /// <param name="seriesLanguage">The series language.</param>
        /// <param name="lastUpdated">The last updated value</param>
        /// <returns>
        /// Updated series object, or NULL value if no update was found.
        /// </returns>
        public Series CheckForUpdate(uint? seriesId, string seriesLanguage, string lastUpdated)
        {
            var seriesXml = this.GetSeriesDetails(seriesId.ToString(), seriesLanguage, true);
            var newSeries = new Series();

            newSeries.PopulateFullDetails(seriesXml);

            return lastUpdated != newSeries.Lastupdated ? newSeries : null;
        }

        /// <summary>
        /// Searches TvDB using plain text string.
        /// </summary>
        /// <param name="name">The show name to search.</param>
        /// <returns>
        /// Found series name.
        /// </returns>
        public string DoFullSearch(string name)
        {
            List<SearchDetails> searchResults = this.SeriesSearch(name); // open initial object and do search
            SearchDetails selectResult = this.ProcessSearchResults(searchResults, name);

            // process results, and allow user to choose alternative options
            if (selectResult == null || selectResult.SeriesID == string.Empty)
            {
                return string.Empty;
            }

            Series series = this.OpenNewSeries(selectResult); // download series details

            if (!TvDBFactory.TvDatabase.ContainsKey(series.SeriesName))
            {
                TvDBFactory.TvDatabase.Add(series.SeriesName, series); // add series to db
            }

            return series.SeriesName;
        }

        /// <summary>
        /// Gets the series details.
        /// </summary>
        /// <param name="seriesId">The series ID.</param>
        /// <param name="language">The language ID.</param>
        /// <param name="skipCache">if set to <c>true</c> [skip cache].</param>
        /// <returns>
        /// Series XML collection
        /// </returns>
        public SeriesXml GetSeriesDetails(string seriesId, string language, bool skipCache = false)
        {
            var output = new SeriesXml();
            var path = DownloadSeriesZip(seriesId, language, skipCache);

            ZipFile zipFile;

            try
            {
                zipFile = ZipFile.Read(path);
            }
            catch
            {
                File.Delete(path);

                path = DownloadSeriesZip(seriesId, language, true);
                zipFile = ZipFile.Read(path);
            }

            string temp = Get.FileSystemPaths.PathDirTemp + Path.DirectorySeparatorChar + seriesId +
                          Path.DirectorySeparatorChar;

            foreach (ZipEntry e in zipFile)
            {
                Directory.CreateDirectory(temp);

                e.Extract(temp, ExtractExistingFileAction.OverwriteSilently);

                if (e.FileName == Get.Scraper.TvDBLanguageAbbr + ".xml")
                {
                    output.En = File.ReadAllText(temp + string.Format("{0}.xml", Get.Scraper.TvDBLanguageAbbr), Encoding.UTF8);
                }
                else
                {
                    switch (e.FileName)
                    {
                        case "banners.xml":
                            output.Banners = File.ReadAllText(temp + "banners.xml", Encoding.UTF8);
                            break;
                        case "actors.xml":
                            output.Actors = File.ReadAllText(temp + "actors.xml", Encoding.UTF8);
                            break;
                    }
                }
            }

            Delete.DeleteDirectory(temp);

            return output;
        }

        /// <summary>
        /// Populate series search details into object.
        /// </summary>
        /// <param name="seriesSearchDetails">The series Search Details.</param>
        /// <returns>
        /// The series object.
        /// </returns>
        public Series OpenNewSeries(SearchDetails seriesSearchDetails)
        {
            SeriesXml rawData = this.GetSeriesDetails(seriesSearchDetails.SeriesID, seriesSearchDetails.Language);

            var details = new Series();
            details.PopulateFullDetails(rawData);

            if (!string.IsNullOrEmpty(details.SeriesBannerUrl))
            {
                string url = "http://www.thetvdb.com/banners/_cache/" + details.SeriesBannerUrl;

                string imagePath = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);

                details.SmallBanner = ImageHandler.LoadImage(imagePath);
            }

            return details;
        }

        /// <summary>
        /// Processes the search results.
        /// </summary>
        /// <param name="seriesResults">The series results.</param>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>
        /// The process search results.
        /// </returns>
        public SearchDetails ProcessSearchResults(List<SearchDetails> seriesResults, string searchTerm)
        {
            if (seriesResults.Count > 1 || seriesResults.Count == 0)
            {
                Factories.UI.Windows7UIFactory.PauseProgressState();

                var frmSelectSeriesName = new FrmSelectSeries(seriesResults, searchTerm);
                frmSelectSeriesName.ShowDialog();

                Factories.UI.Windows7UIFactory.PauseProgressState();

                return frmSelectSeriesName.SelectedSeries;
            }

            if (seriesResults.Count == 1)
            {
                return seriesResults[0];
            }

            return null;
        }

        /// <summary>
        /// Search tMDB using the value
        /// </summary>
        /// <param name="value">The Search results</param>
        /// <returns>
        /// The series search.
        /// </returns>
        public List<SearchDetails> SeriesSearch(string value)
        {
            var seriesResults = new List<SearchDetails>();

            string searchXml =
                Downloader.ProcessDownload(
                    string.Format("http://www.thetvdb.com/api/GetSeries.php?seriesname={0}", value), 
                    DownloadType.Html, 
                    Section.Tv);

            if (string.IsNullOrEmpty(searchXml))
            {
                return seriesResults;
            }

            var document = new XmlDocument();
            document.LoadXml(searchXml.Replace("<br>", string.Empty));

            XmlNodeList results = document.GetElementsByTagName("Series");

            // Iterate through series
            foreach (XmlNode result in results)
            {
                var singleDocument = new XmlDocument();
                singleDocument.LoadXml(result.OuterXml);

                // Populate object with series details
                var modelSeriesSearchDetails = new SearchDetails();
                modelSeriesSearchDetails.PopulateSeriesDetail(singleDocument);
                seriesResults.Add(modelSeriesSearchDetails);
            }

            return seriesResults;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Download Series Zip from Server.
        /// </summary>
        /// <param name="seriesId">The series ID.</param>
        /// <param name="language">The language.</param>
        /// <param name="skipCache">if set to <c>true</c> [skip cache].</param>
        /// <returns>
        /// The path to the downloaded series zip.
        /// </returns>
        private static string DownloadSeriesZip(string seriesId, string language, bool skipCache = false)
        {
            string prePath = string.Format(
                "http://www.thetvdb.com/api/{0}/series/{1}/all/{2}.zip", TvdbApi, seriesId, Get.Scraper.TvDBLanguageAbbr);

            return Downloader.ProcessDownload(prePath, DownloadType.Binary, Section.Tv, skipCache);
        }

        /// <summary>
        /// Download Server Time for TheTVDB.
        /// </summary>
        /// <returns>
        /// The current server time. Useful for updates.
        /// </returns>
        private static string DownloadServerTime()
        {
            var downloadItem = new DownloadItem
                {
                    Url = "http://www.thetvdb.com/api/Updates.php?type=none",
                    Type = DownloadType.Html 
                };

            var html = new Html();

            html.Get(downloadItem);

            string serverTimeXml =
                Downloader.ProcessDownload(
                    string.Format(string.Format("http://www.thetvdb.com/api/Updates.php?type=none")), 
                    DownloadType.Html, 
                    Section.Tv);

            if (string.IsNullOrEmpty(serverTimeXml))
            {
                return string.Empty;
            }

            var doc = new XmlDocument();
            doc.LoadXml(serverTimeXml);
            var timeValue = XRead.GetString(doc, "Time");
            return timeValue;
        }

        #endregion
    }
}