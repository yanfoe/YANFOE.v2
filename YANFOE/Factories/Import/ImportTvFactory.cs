// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImportTvFactory.cs">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the
//   license terms of this work.
// </license>
// <summary>
//   The factory for all Import TV functions
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Import
{
    #region Required Namespaces

    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Settings;
    using YANFOE.Settings.ConstSettings;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The factory for all Import TV functions
    /// </summary>
    public class ImportTvFactory
    {
        #region Static Fields

        /// <summary>
        ///   The instance.
        /// </summary>
        private static ImportTvFactory instance = new ImportTvFactory();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Prevents a default instance of the <see cref="ImportTvFactory" /> class from being created. 
        ///   Initializes static members of the <see cref="ImportTvFactory" /> class.
        /// </summary>
        private ImportTvFactory()
        {
            this.Scan = new SortedDictionary<string, ScanSeries>();

            this.NotCategorized = new ThreadedBindingList<ScanNotCategorized>();

            this.ScanSeriesPicks = new ThreadedBindingList<ScanSeriesPick>();

            this.SeriesList = new ThreadedBindingList<SeriesListModel>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the instance.
        /// </summary>
        public static ImportTvFactory Instance
        {
            get
            {
                return instance;
            }

            set
            {
                instance = value;
            }
        }

        /// <summary>
        ///   Gets or sets NotCategorized.
        /// </summary>
        public ThreadedBindingList<ScanNotCategorized> NotCategorized { get; set; }

        /// <summary>
        ///   Gets or sets Scan.
        /// </summary>
        public SortedDictionary<string, ScanSeries> Scan { get; set; }

        /// <summary>
        ///   Gets or sets ScanSeriesPicks.
        /// </summary>
        public ThreadedBindingList<ScanSeriesPick> ScanSeriesPicks { get; set; }

        /// <summary>
        ///   Gets or sets SeriesNameList.
        /// </summary>
        public ThreadedBindingList<SeriesListModel> SeriesList { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Add scan result.
        /// </summary>
        /// <param name="episodeDetails">
        /// The episode details. 
        /// </param>
        public void AddScanResult(EpisodeDetails episodeDetails)
        {
            var seriesName = episodeDetails.SeriesName;

            var name = seriesName;
            var check1 = (from s in this.Scan where s.Key.ToLower() == name.ToLower() select s.Key).SingleOrDefault();

            // Process Series
            if (check1 == null)
            {
                this.Scan.Add(seriesName, new ScanSeries());

                var check2 =
                    (from s in this.SeriesList
                     where s.SeriesName.ToLower() == seriesName.ToLower()
                     select s.SeriesName.ToLower()).ToList();

                if (!check2.Contains(seriesName.ToLower()) && !string.IsNullOrEmpty(seriesName))
                {
                    string dir = Directory.GetParent(episodeDetails.FilePath).Name;
                    string path = string.Empty;
                    if (dir.StartsWith("season", true, CultureInfo.CurrentCulture))
                    {
                        // Looks like it. Qualified series guess
                        var directoryInfo = Directory.GetParent(episodeDetails.FilePath).Parent;
                        if (directoryInfo != null)
                        {
                            path = directoryInfo.FullName;
                        }
                    }
                    else
                    {
                        // Best guess
                        path = Directory.GetParent(episodeDetails.FilePath).FullName;
                    }

                    this.SeriesList.Add(
                        new SeriesListModel { WaitingForScan = true, SeriesName = seriesName, SeriesPath = path });
                }
            }
            else
            {
                seriesName = check1;
            }

            // Process Series
            if (!this.Scan[seriesName].Seasons.ContainsKey(episodeDetails.SeasonNumber))
            {
                this.Scan[seriesName].Seasons.Add(episodeDetails.SeasonNumber, new ScanSeason());
            }

            if (
                !this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.ContainsKey(
                    episodeDetails.EpisodeNumber))
            {
                this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.Add(
                    episodeDetails.EpisodeNumber, new ScanEpisode());
            }

            this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[episodeDetails.EpisodeNumber].FilePath =
                episodeDetails.FilePath;

            if (episodeDetails.SecondaryNumbers.Count > 0)
            {
                foreach (int secondary in episodeDetails.SecondaryNumbers)
                {
                    if (!this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.ContainsKey(secondary))
                    {
                        this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.Add(
                            secondary, new ScanEpisode());
                    }

                    this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].FilePath =
                        episodeDetails.FilePath;
                    this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].Secondary = true;
                    this.Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].SecondaryTo =
                        episodeDetails.EpisodeNumber;
                }
            }

            var findList =
                (from s in MediaPathDBFactory.Instance.MediaPathTvUnsorted
                 where s.PathAndFileName == episodeDetails.FilePath
                 select s).ToList();

            foreach (var path in findList)
            {
                MediaPathDBFactory.Instance.MediaPathTvUnsorted.Remove(path);
            }
        }

        /// <summary>
        ///   The do import scan.
        /// </summary>
        public void DoImportScan()
        {
            this.SeriesList = new ThreadedBindingList<SeriesListModel>();
            this.Scan = new SortedDictionary<string, ScanSeries>();

            // Get file list
            var filters = Get.InOutCollection.VideoExtentions;

            var paths = MediaPathDBFactory.Instance.MediaPathTvUnsorted;

            var filteredFiles = paths.Select(
                file => new { file, pathAndFileName = Path.GetExtension(file.PathAndFileName).ToLower() }).Where(
                    @t => filters.Any(filter => @t.pathAndFileName.ToLower() == "." + filter.ToLower())).Select(
                        @t => @t.file.PathAndFileName).ToList();

            // Process each file and add to ScanDB);
            foreach (var f in filteredFiles)
            {
                var episodeDetails = this.GetEpisodeDetails(f);

                if (episodeDetails.TvMatchSuccess)
                {
                    this.AddScanResult(episodeDetails);
                }
                else
                {
                    this.NotCategorized.Add(new ScanNotCategorized { FilePath = f });
                }
            }

            MasterMediaDBFactory.PopulateMasterTVMediaDatabase();
        }

        /// <summary>
        /// Gets the episode details.
        /// </summary>
        /// <param name="filePath">
        /// The file path. 
        /// </param>
        /// <returns>
        /// The episode details. 
        /// </returns>
        public EpisodeDetails GetEpisodeDetails(string filePath)
        {
            const string LogCategory = "ImportTvFactory > GetEpisodeDetails";

            var episodeDetails = new EpisodeDetails { FilePath = filePath };

            string series;

            if (MovieNaming.IsDVD(filePath))
            {
                series = MovieNaming.GetDvdName(filePath);
            }
            else if (MovieNaming.IsBluRay(filePath))
            {
                series = MovieNaming.GetBluRayName(filePath);
            }
            else
            {
                series = Path.GetFileNameWithoutExtension(filePath);
            }

            Log.WriteToLog(LogSeverity.Debug, 0, string.Format("Getting episode details for: {0}", series), LogCategory);

            // Check if dir structure is <root>/<series>/<season>/<episodes>
            string dir = Directory.GetParent(filePath).Name;
            string seriesGuess = string.Empty;
            if (dir.StartsWith("season", true, CultureInfo.CurrentCulture))
            {
                // Looks like it. Qualified series guess
                var directoryInfo = Directory.GetParent(filePath).Parent;
                if (directoryInfo != null)
                {
                    seriesGuess = directoryInfo.Name;
                }
            }

            Log.WriteToLog(
                LogSeverity.Debug, 
                0, 
                string.Format("Qualified series name guess, based on folder: {0}", seriesGuess), 
                LogCategory);

            var regex = Regex.Match(series, "(?<seriesName>.*?)" + DefaultRegex.Tv, RegexOptions.IgnoreCase);

            if (regex.Success)
            {
                string rawSeriesName = regex.Groups["seriesName"].Value.Trim();
                Log.WriteToLog(
                    LogSeverity.Debug, 0, string.Format("Series name from file: {0}", rawSeriesName), LogCategory);

                var result =
                    (from r in this.ScanSeriesPicks where r.SearchString == rawSeriesName select r).SingleOrDefault();

                string rawSeriesGuess = rawSeriesName.Replace(new[] { ".", "_", "-" }, " ").Trim();

                if (seriesGuess != string.Empty)
                {
                    if (rawSeriesName == string.Empty)
                    {
                        // Anything's better than nothing
                        rawSeriesName = seriesGuess;
                    }
                    else if (rawSeriesName.StartsWith(seriesGuess))
                    {
                        // Regex might've picked up some random crap between series name and s01e01
                        rawSeriesName = seriesGuess;
                    }
                    else if (rawSeriesName.Length > 0
                             &&
                             (rawSeriesName.Substring(0, 1) == seriesGuess.Substring(0, 1)
                              ||
                              rawSeriesGuess.ToLower().Replace("and", "&") == seriesGuess.ToLower().Replace("and", "&")))
                    {
                        // Regex might've picked up an acronym for the series
                        // TGaaG = Two Guys and a Girl
                        if (result == null)
                        {
                            rawSeriesName = seriesGuess;
                        }
                    }
                }
                else
                {
                    rawSeriesName = rawSeriesGuess;
                }

                Log.WriteToLog(
                    LogSeverity.Debug, 0, string.Format("Best series guess: {0}", rawSeriesName), LogCategory);

                episodeDetails.SeriesName = result != null ? result.SeriesName : rawSeriesName;

                episodeDetails.SeriesName = FileSystemCharChange.From(
                    episodeDetails.SeriesName, FileSystemCharChange.ConvertArea.Tv);
            }

            var seasonMatch = Regex.Match(series, DefaultRegex.TvSeason, RegexOptions.IgnoreCase);
            if (seasonMatch.Success)
            {
                episodeDetails.SeasonNumber = seasonMatch.Groups[1].Value.GetNumber();
            }

            Log.WriteToLog(
                LogSeverity.Debug, 
                0, 
                string.Format("Extracted season number: {0}", episodeDetails.SeasonNumber), 
                LogCategory);

            var episodeMatch = Regex.Matches(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
            if (episodeMatch.Count > 0)
            {
                episodeDetails.TvMatchSuccess = true;
                if (episodeMatch.Count > 1)
                {
                    bool first = true;
                    foreach (Match match in episodeMatch)
                    {
                        if (first)
                        {
                            episodeDetails.EpisodeNumber = match.Value.GetNumber();
                            first = false;
                        }
                        else
                        {
                            episodeDetails.SecondaryNumbers.Add(match.Value.GetNumber());
                        }
                    }

                    Log.WriteToLog(
                        LogSeverity.Debug, 
                        0, 
                        string.Format(
                            "Extracted episode numbers ({0}): {1}", 
                            episodeDetails.SecondaryNumbers.Count, 
                            string.Join(", ", episodeDetails.SecondaryNumbers)), 
                        LogCategory);
                }
                else
                {
                    var episodeMatch2 = Regex.Match(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
                    episodeDetails.EpisodeNumber = episodeMatch2.Groups[1].Value.GetNumber();

                    Log.WriteToLog(
                        LogSeverity.Debug, 
                        0, 
                        string.Format("Extracted episode number: {0}", episodeDetails.EpisodeNumber), 
                        LogCategory);
                }
            }

            episodeDetails.SeriesName = episodeDetails.SeriesName.Replace(".", string.Empty).Trim();
            if (episodeDetails.SeriesName.EndsWith("-"))
            {
                episodeDetails.SeriesName = episodeDetails.SeriesName.TrimEnd('-').Trim();
            }

            var check =
                (from s in this.ScanSeriesPicks where s.SearchString == episodeDetails.SeriesName select s).SingleOrDefault();

            if (check != null)
            {
                episodeDetails.SeriesName = check.SeriesName;
            }

            Log.WriteToLog(
                LogSeverity.Debug, 0, string.Format("Final series name: {0}", episodeDetails.SeriesName), LogCategory);

            return episodeDetails;
        }

        #endregion
    }
}