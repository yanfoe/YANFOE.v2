// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="TvDBFactory.cs">
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
//   The TV DB factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using BitFactory.Logging;

    using Microsoft.Win32;

    using YANFOE.Factories.InOut;
    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.IO;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Models.TvModels.TVDB;
    using YANFOE.Scrapers.TV;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.UI;
    using YANFOE.Tools.Xml;
    using YANFOE.UI.Dialogs.TV;
    using YANFOE.UI.UserControls.CommonControls;

    #endregion

    /// <summary>
    ///   The TV DB factory.
    /// </summary>
    [Serializable]
    public class TVDBFactory : FactoryBase
    {
        #region Static Fields

        /// <summary>
        ///   The instance.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", 
            Justification = "Implements Singleton.")]
        public static TVDBFactory Instance = new TVDBFactory();

        #endregion

        #region Fields

        /// <summary>
        ///   The gallery group.
        /// </summary>
        private readonly GalleryItemGroup galleryGroup;

        /// <summary>
        ///   The current season.
        /// </summary>
        private Season currentSeason;

        /// <summary>
        ///   The current selected episode.
        /// </summary>
        private List<Episode> currentSelectedEpisode;

        /// <summary>
        ///   The current selected season.
        /// </summary>
        private List<Season> currentSelectedSeason;

        /// <summary>
        ///   The current selected series.
        /// </summary>
        private List<Series> currentSelectedSeries;

        /// <summary>
        ///   The master series name list.
        /// </summary>
        private ThreadedBindingList<MasterSeriesListModel> masterSeriesNameList;

        /// <summary>
        ///   The TV database.
        /// </summary>
        private ThreadedBindingList<Series> tvDatabase;

        /// <summary>
        ///   Gets or sets UpdateStatus.
        /// </summary>
        private string updateStatus;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Prevents a default instance of the <see cref="TVDBFactory" /> class from being created. 
        ///   Initializes members of the <see cref="TVDBFactory" /> class.
        /// </summary>
        private TVDBFactory()
        {
            this.TVDatabase = new ThreadedBindingList<Series>();

            this.HiddenTVDB = new ThreadedBindingList<Series>();

            this.CurrentSeries = new Series();
            this.currentSeason = new Season();
            this.CurrentEpisode = new Episode();

            this.galleryGroup = new GalleryItemGroup();

            this.masterSeriesNameList = new ThreadedBindingList<MasterSeriesListModel>();

            this.currentSelectedSeries = new List<Series>();
            this.currentSelectedSeason = new List<Season>();
            this.currentSelectedEpisode = new List<Episode>();

            this.masterSeriesNameList.ListChanged += this.MasterSeriesNameListListChanged;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when [current episode changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler CurrentEpisodeChanged = delegate { };

        /// <summary>
        ///   Occurs when [current season changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler CurrentSeasonChanged = delegate { };

        /// <summary>
        ///   Occurs when [current series changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler CurrentSeriesChanged = delegate { };

        /// <summary>
        ///   Occurs when [episode loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler EpisodeLoaded = delegate { };

        /// <summary>
        ///   Occurs when [episode loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler EpisodeLoading = delegate { };

        /// <summary>
        ///   Occurs when [gallery changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler GalleryChanged = delegate { };

        /// <summary>
        ///   Occurs when [master series name list changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler MasterSeriesNameListChanged = delegate { };

        /// <summary>
        ///   Occurs when [redraw layout].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler RedrawLayout = delegate { };

        /// <summary>
        ///   Occurs when [season banner loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonBannerLoaded = delegate { };

        /// <summary>
        ///   Occurs when [season banner loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonBannerLoading = delegate { };

        /// <summary>
        ///   Occurs when [season fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonFanartLoaded = delegate { };

        /// <summary>
        ///   Occurs when [season fanart loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonFanartLoading = delegate { };

        /// <summary>
        ///   Occurs when [season poster loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonPosterLoaded = delegate { };

        /// <summary>
        ///   Occurs when [season poster loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeasonPosterLoading = delegate { };

        /// <summary>
        ///   Occurs when [series banner loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesBannerLoaded = delegate { };

        /// <summary>
        ///   Occurs when [series banner loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesBannerLoading = delegate { };

        /// <summary>
        ///   Occurs when [series fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesFanartLoaded = delegate { };

        /// <summary>
        ///   Occurs when [series fanart loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesFanartLoading = delegate { };

        /// <summary>
        ///   Occurs when [series poster loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesPosterLoaded = delegate { };

        /// <summary>
        ///   Occurs when [series poster loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler SeriesPosterLoading = delegate { };

        /// <summary>
        ///   Occurs when [TV DB changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler TVDBChanged = delegate { };

        /// <summary>
        ///   Occurs when [update progress changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler UpdateProgressChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the current episode.
        /// </summary>
        public Episode CurrentEpisode { get; set; }

        /// <summary>
        ///   Gets or sets CurrentSelectedEpisode.
        /// </summary>
        public List<Episode> CurrentSelectedEpisode
        {
            get
            {
                return this.currentSelectedEpisode;
            }

            set
            {
                this.currentSelectedEpisode = value;
            }
        }

        /// <summary>
        ///   Gets or sets CurrentSelectedSeason.
        /// </summary>
        public List<Season> CurrentSelectedSeason
        {
            get
            {
                return this.currentSelectedSeason;
            }

            set
            {
                this.currentSelectedSeason = value;
            }
        }

        /// <summary>
        ///   Gets or sets CurrentSelectedSeries.
        /// </summary>
        public List<Series> CurrentSelectedSeries
        {
            get
            {
                return this.currentSelectedSeries;
            }

            set
            {
                this.currentSelectedSeries = value;
            }
        }

        /// <summary>
        ///   Gets or sets The current series.
        /// </summary>
        public Series CurrentSeries { get; set; }

        /// <summary>
        ///   Gets GetCurrentSeasonsList.
        /// </summary>
        public List<Season> GetCurrentSeasonsList
        {
            get
            {
                if (!Get.Ui.HideSeasonZero)
                {
                    return (from s in this.CurrentSeries.Seasons where s.SeasonNumber != 0 select s).ToList();
                }

                return (from s in this.CurrentSeries.Seasons select s).ToList();
            }
        }

        /// <summary>
        ///   Gets or sets the hidden TVDB.
        /// </summary>
        public ThreadedBindingList<Series> HiddenTVDB { get; set; }

        /// <summary>
        ///   Gets or sets the series name list.
        /// </summary>
        /// <value> The series name list. </value>
        public ThreadedBindingList<MasterSeriesListModel> MasterSeriesList
        {
            get
            {
                return this.masterSeriesNameList;
            }

            set
            {
                this.masterSeriesNameList = value;
                this.OnPropertyChanged("MasterSeriesList");
            }
        }

        /// <summary>
        ///   Gets or sets the TV database.
        /// </summary>
        /// <value> The TV database. </value>
        public ThreadedBindingList<Series> TVDatabase
        {
            get
            {
                return this.tvDatabase;
            }

            set
            {
                this.tvDatabase = value;
                this.OnPropertyChanged("TVDatabase");
            }
        }

        /// <summary>
        ///   Gets or sets the update status.
        /// </summary>
        public string UpdateStatus
        {
            get
            {
                return this.updateStatus;
            }

            set
            {
                this.updateStatus = value;
                this.OnPropertyChanged("UpdateStatus");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add custom series.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        public void AddCustomSeries(Series series)
        {
            this.TVDatabase.Add(series);
            this.GenerateMasterSeriesList();
            this.InvokeMasterSeriesNameListChanged(new EventArgs());
        }

        /// <summary>
        ///   Check if banner downloaded.
        /// </summary>
        /// <returns> Downloaded status </returns>
        public bool BannerDownloaded()
        {
            string url = this.GetImageUrl(this.CurrentSeries.SeriesBannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// The change update status.
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        public void ChangeUpdateStatus(string value)
        {
            this.UpdateStatus = value;
            this.InvokeUpdateProgressChanged(new EventArgs());
        }

        /// <summary>
        ///   Show the "Add Custom Series" dialog.
        /// </summary>
        public void CreateCustomSeries()
        {
            var addCustomSeries = new WndAddCustomSeries();
            addCustomSeries.ShowDialog();
        }

        /// <summary>
        ///   The default current episode.
        /// </summary>
        public void DefaultCurrentEpisode()
        {
            foreach (var season in this.CurrentSeries.Seasons)
            {
                foreach (Episode episode in season.Episodes)
                {
                    this.SetCurrentEpisode(episode.Guid);
                    break;
                }

                break;
            }
        }

        /// <summary>
        ///   The default current season and episode.
        /// </summary>
        public void DefaultCurrentSeasonAndEpisode()
        {
            foreach (var season in this.CurrentSeries.Seasons)
            {
                if (Get.Ui.HideSeasonZero && season.SeasonNumber != 0)
                {
                    this.SetCurrentSeason(season.Guid);

                    foreach (Episode episode in season.Episodes)
                    {
                        this.SetCurrentEpisode(episode.Guid);
                        break;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// The delete series.
        /// </summary>
        /// <param name="masterSeriesList">
        /// The master series list. 
        /// </param>
        public void DeleteSeries(MasterSeriesListModel masterSeriesList)
        {
            var series = this.MasterSeriesList.Single(c => c.SeriesName == masterSeriesList.SeriesName);
            this.masterSeriesNameList.Remove(series);

            var s = this.TVDatabase.First(x => x.SeriesName == series.SeriesName);

            foreach (var season in s.Seasons)
            {
                foreach (var episode in season.Episodes)
                {
                    if (!string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
                    {
                        var files =
                            MasterMediaDBFactory.MasterTVMediaDatabase.Where(f => f == episode.FilePath.PathAndFilename)
                                .ToList();

                        foreach (var f in files)
                        {
                            MasterMediaDBFactory.MasterTVMediaDatabase.Remove(f);
                        }
                    }
                }
            }

            this.TVDatabase.Remove(this.TVDatabase.First(x => x.SeriesName == series.SeriesName));

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        ///   Check if episode screenshot downloaded.
        /// </summary>
        /// <returns> Downloaded status </returns>
        public bool EpisodeDownloaded()
        {
            string url = this.GetImageUrl(this.CurrentEpisode.EpisodeScreenshotUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Check if fanart downloaded.
        /// </summary>
        /// <returns> Downloaded status </returns>
        public bool FanartDownloaded()
        {
            string url = this.GetImageUrl(this.CurrentSeries.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Generates the master series list.
        /// </summary>
        public void GenerateMasterSeriesList()
        {
            ThreadedBindingList<MasterSeriesListModel> list = (from s in this.TVDatabase
                                                               select
                                                                   new MasterSeriesListModel
                                                                       {
                                                                           SeriesName = s.SeriesName, 
                                                                           BannerPath = s.SeriesBannerUrl, 
                                                                           SeriesGuid = s.Guid
                                                                       }).ToThreadedBindingList();

            this.masterSeriesNameList.Clear();

            foreach (MasterSeriesListModel v in list)
            {
                if (!string.IsNullOrEmpty(v.SeriesName.Trim()))
                {
                    this.masterSeriesNameList.Add(v);
                }
            }

            MasterMediaDBFactory.PopulateMasterTVMediaDatabase();
        }

        /// <summary>
        ///   The generate picture gallery.
        /// </summary>
        public void GeneratePictureGallery()
        {
            this.galleryGroup.Items.Clear();

            foreach (var series in this.TVDatabase)
            {
                if (series.SmallBanner == null && !string.IsNullOrEmpty(series.SeriesBannerPath))
                {
                    if (File.Exists(series.SeriesBannerPath))
                    {
                        Image banner = ImageHandler.LoadImage(series.SeriesBannerPath);
                        series.SmallBanner = ImageHandler.ResizeImage(banner, 300, 55);
                    }
                }

                if (series.SmallBanner != null)
                {
                    var superTip = new SuperToolTip { AllowHtmlText = true };

                    if (series.FirstAired != null)
                    {
                        superTip.Items.AddTitle(
                            string.Format("{0} ({1})", series.SeriesName, series.FirstAired.Value.Year));
                    }

                    var galleryItem = new GalleryItem(series.SmallBanner, series.SeriesName, string.Empty)
                        {
                           Tag = series.Guid, SuperTip = superTip 
                        };

                    if (!this.galleryGroup.Items.Contains(galleryItem))
                    {
                        this.galleryGroup.Items.Add(galleryItem);
                    }
                }
            }

            this.InvokeGalleryChanged(new EventArgs());
        }

        /// <summary>
        ///   Get collection of episodes in current seasons
        /// </summary>
        /// <returns> Collection of episodes in current seasons </returns>
        public List<Episode> GetCurrentEpisodeList()
        {
            return (from s in this.currentSeason.Episodes select s).ToList();
        }

        /// <summary>
        ///   The get episode.
        /// </summary>
        public void GetEpisode()
        {
            if (this.EpisodeDownloaded())
            {
                this.InvokeEpisodeLoaded(new EventArgs());
                return;
            }

            this.InvokeEpisodeLoading(new EventArgs());

            var bgwEpisode = new BackgroundWorker();

            bgwEpisode.DoWork += this.BgwEpisodeDoWork;
            bgwEpisode.RunWorkerCompleted += this.BgwEpisodeRunWorkerCompleted;
            bgwEpisode.RunWorkerAsync(this.CurrentEpisode.EpisodeScreenshotUrl);
        }

        /// <summary>
        /// Get episode super tip.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <returns>
        /// Return episode super tool tip 
        /// </returns>
        public SuperToolTip GetEpisodeSuperTip(Episode episode)
        {
            if (episode == null)
            {
                return new SuperToolTip();
            }

            var superTip = new SuperToolTip();

            superTip.Items.AddTitle(episode.EpisodeName);

            if (!string.IsNullOrEmpty(episode.EpisodeScreenshotUrl))
            {
                string url = this.GetImageUrl(episode.EpisodeScreenshotUrl);
                string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

                if (File.Exists(urlCache) && !Downloader.Downloading.Contains(url))
                {
                    try
                    {
                        Image episodePathImage = ImageHandler.LoadImage(urlCache);
                        var smallBanner = new ToolTipTitleItem
                            {
                               Image = ImageHandler.ResizeImage(episodePathImage, 150, 90) 
                            };

                        superTip.Items.Add(smallBanner);
                    }
                    catch
                    {
                        Log.WriteToLog(LogSeverity.Error, 0, "Could not load banner", urlCache);
                    }
                }
            }

            return superTip;
        }

        /// <summary>
        /// The get gallery group.
        /// </summary>
        /// <returns>
        /// GalleryGroup gallery item group 
        /// </returns>
        /// <summary>
        /// Get TvDB image url.
        /// </summary>
        /// <param name="value">
        /// The image value. 
        /// </param>
        /// <param name="smallVersion">
        /// The small Version. 
        /// </param>
        /// <returns>
        /// The get image url. 
        /// </returns>
        public string GetImageUrl(string value, bool smallVersion = false)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.ToLower().Contains("http://"))
            {
                return value;
            }

            string startPath = smallVersion
                                   ? "http://www.thetvdb.com/banners/_cache/"
                                   : "http://www.thetvdb.com/banners/";

            return startPath + value;
        }

        /// <summary>
        ///   The get season banner.
        /// </summary>
        public void GetSeasonBanner()
        {
            if (this.SeasonBannerDownloaded())
            {
                this.InvokeSeasonBannerLoaded(new EventArgs());
                return;
            }

            this.InvokeSeasonBannerLoading(new EventArgs());

            var bgwSeasonBanner = new BackgroundWorker();

            bgwSeasonBanner.DoWork += this.BgwSeasonBannerDoWork;

            bgwSeasonBanner.RunWorkerCompleted += this.BgwSeasonBannerRunWorkerCompleted;

            bgwSeasonBanner.RunWorkerAsync(this.currentSeason.BannerUrl);
        }

        /// <summary>
        ///   The get season fanart.
        /// </summary>
        public void GetSeasonFanart()
        {
            if (this.SeasonFanartDownloaded())
            {
                this.InvokeSeasonFanartLoaded(new EventArgs());
                return;
            }

            this.InvokeSeasonFanartLoading(new EventArgs());

            var bgwSeasonFanart = new BackgroundWorker();

            bgwSeasonFanart.DoWork += this.BgwSeasonFanartDoWork;

            bgwSeasonFanart.RunWorkerCompleted += this.BgwSeasonFanartRunWorkerCompleted;

            bgwSeasonFanart.RunWorkerAsync(this.currentSeason.FanartUrl);
        }

        /// <summary>
        ///   The get season poster.
        /// </summary>
        public void GetSeasonPoster()
        {
            if (this.SeasonPosterDownloaded())
            {
                this.InvokeSeasonPosterLoaded(new EventArgs());
                return;
            }

            this.InvokeSeasonPosterLoading(new EventArgs());

            var bgwSeasonPoster = new BackgroundWorker();

            bgwSeasonPoster.DoWork += this.BgwSeasonPosterDoWork;

            bgwSeasonPoster.RunWorkerCompleted += this.BgwSeasonPosterRunWorkerCompleted;

            bgwSeasonPoster.RunWorkerAsync(this.currentSeason.PosterUrl);
        }

        /// <summary>
        /// Get season super tip.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <returns>
        /// The season super tip. 
        /// </returns>
        public SuperToolTip GetSeasonSuperTip(Season season)
        {
            if (season == null)
            {
                return new SuperToolTip();
            }

            var superTip = new SuperToolTip { AllowHtmlText = true };

            superTip.Items.AddTitle(string.Format("Season {0}", season.SeasonNumber));

            foreach (Episode episode in season.Episodes)
            {
                var tip = new ToolTipItem();

                string found = File.Exists(episode.FilePath.PathAndFilename)
                                   ? "<color=206,244,208>File Exists</color>"
                                   : "<b><color=244,206,206>File Does Not Exist</color></b>";

                tip.Text = string.Format("<b>{0}</b>:{1} - {2}", episode.EpisodeNumber, episode.EpisodeName, found);

                superTip.Items.Add(tip);
            }

            return superTip;
        }

        /// <summary>
        ///   The get series banner.
        /// </summary>
        public void GetSeriesBanner()
        {
            if (this.BannerDownloaded())
            {
                this.InvokeBannerLoaded(new EventArgs());
                return;
            }

            this.InvokeBannerLoading(new EventArgs());

            var bgwBanner = new BackgroundWorker();

            bgwBanner.DoWork += this.BgwBannerDoWork;
            bgwBanner.RunWorkerCompleted += this.BgwBannerRunWorkerCompleted;
            bgwBanner.RunWorkerAsync(this.CurrentSeries.SeriesBannerUrl);
        }

        /// <summary>
        ///   The get series fanart.
        /// </summary>
        public void GetSeriesFanart()
        {
            if (this.FanartDownloaded())
            {
                this.InvokeFanartLoaded(new EventArgs());
                return;
            }

            this.InvokeSeriesFanartLoading(new EventArgs());

            var bgwFanart = new BackgroundWorker();

            bgwFanart.DoWork += this.BgwFanartDoWork;
            bgwFanart.RunWorkerCompleted += this.BgwFanartRunWorkerCompleted;
            bgwFanart.RunWorkerAsync(this.CurrentSeries.FanartUrl);
        }

        /// <summary>
        /// Get series using series GUID.
        /// </summary>
        /// <param name="guid">
        /// The series GUID. 
        /// </param>
        /// <returns>
        /// Series object 
        /// </returns>
        public Series GetSeriesFromGUID(string guid)
        {
            return (from e in this.TVDatabase where e.Guid == guid select e).SingleOrDefault();
        }

        /// <summary>
        /// Get series using series name.
        /// </summary>
        /// <param name="seriesName">
        /// The series name. 
        /// </param>
        /// <returns>
        /// Series object 
        /// </returns>
        public Series GetSeriesFromName(string seriesName)
        {
            return (from s in this.TVDatabase where s.SeriesName == seriesName select s).SingleOrDefault();
        }

        /// <summary>
        ///   Get series poster.
        /// </summary>
        public void GetSeriesPoster()
        {
            if (this.SeriesPosterDownloaded())
            {
                this.InvokeSeriesPosterLoaded(new EventArgs());
                return;
            }

            this.InvokeSeriesPosterLoading(new EventArgs());

            var bgwSeriesPoster = new BackgroundWorker();
            bgwSeriesPoster.DoWork += this.BgwSeriesPosterDoWork;
            bgwSeriesPoster.RunWorkerCompleted += this.BgwSeriesPosterRunWorkerCompleted;
            bgwSeriesPoster.RunWorkerAsync(this.CurrentSeries.PosterUrl);
        }

        /// <summary>
        /// Get series super tip.
        /// </summary>
        /// <param name="seriesguid">
        /// The series GUID. 
        /// </param>
        /// <returns>
        /// Series super tip. 
        /// </returns>
        public SuperToolTip GetSeriesSuperTip(string seriesguid)
        {
            Series series = this.GetSeriesFromGUID(seriesguid);

            var superTip = new SuperToolTip { AllowHtmlText = true };

            superTip.Items.AddTitle(series.SeriesName);
            var smallBanner = new ToolTipTitleItem { Image = series.SmallBanner };

            var smallPoster = new ToolTipTitleItem { Image = series.SmallPoster };
            var smallFanart = new ToolTipTitleItem { Image = series.SmallFanart };

            var sb = new StringBuilder();
            sb.Append("<b>Total Seasons</b>: " + series.Seasons.Count + Environment.NewLine);
            sb.Append("<b>Total Episodes</b>: " + this.GetTotalEpisodes(series) + Environment.NewLine);
            sb.Append("<b>Total Missing Episodes</b>: " + this.GetTotalEpisodes(series, true) + Environment.NewLine);

            smallPoster.Text = sb.ToString();

            superTip.Items.Add(smallBanner);
            superTip.Items.Add(smallPoster);
            superTip.Items.Add(smallFanart);

            return superTip;
        }

        /// <summary>
        /// Get total episodes.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <param name="missingOnly">
        /// The missing only. 
        /// </param>
        /// <returns>
        /// Total episodes. 
        /// </returns>
        public int GetTotalEpisodes(Series series, bool missingOnly = false)
        {
            int count = 0;

            foreach (var season in series.Seasons)
            {
                if (missingOnly == false)
                {
                    count += season.Episodes.Count;
                }
                else
                {
                    count += season.Episodes.Count(episode => string.IsNullOrEmpty(episode.FilePath.PathAndFilename));
                }
            }

            return count;
        }

        /// <summary>
        /// The hide episode.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <exception cref="NotImplementedException">
        /// . da
        /// </exception>
        public void HideEpisode(Episode episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The hide season.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        public void HideSeason(Season season)
        {
            // TODO: Hide season
        }

        /// <summary>
        /// Invokes the BannerLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeBannerLoaded(EventArgs e)
        {
            EventHandler handler = this.SeriesBannerLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the BannerLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeBannerLoading(EventArgs e)
        {
            EventHandler handler = this.SeriesBannerLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the CurrentEpisodeChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeCurrentEpisodeChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentEpisodeChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the CurrentSeasonChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeCurrentSeasonChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentSeasonChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the CurrentSeriesChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeCurrentSeriesChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentSeriesChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the EpisodeLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeEpisodeLoaded(EventArgs e)
        {
            EventHandler handler = this.EpisodeLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the EpisodeLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeEpisodeLoading(EventArgs e)
        {
            EventHandler handler = this.EpisodeLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the FanartLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeFanartLoaded(EventArgs e)
        {
            EventHandler handler = this.SeriesFanartLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the GalleryChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeGalleryChanged(EventArgs e)
        {
            EventHandler handler = this.GalleryChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the GalleryEpisodeChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeGalleryEpisodeChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentEpisodeChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the master series name list changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeMasterSeriesNameListChanged(EventArgs e)
        {
            EventHandler handler = this.MasterSeriesNameListChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the redraw layout.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeRedrawLayout(EventArgs e)
        {
            EventHandler handler = this.RedrawLayout;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonBannerLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonBannerLoaded(EventArgs e)
        {
            EventHandler handler = this.SeasonBannerLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonBannerLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonBannerLoading(EventArgs e)
        {
            EventHandler handler = this.SeasonBannerLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonFanartLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonFanartLoaded(EventArgs e)
        {
            EventHandler handler = this.SeasonFanartLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonFanartLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonFanartLoading(EventArgs e)
        {
            EventHandler handler = this.SeasonFanartLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonPosterLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonPosterLoaded(EventArgs e)
        {
            EventHandler handler = this.SeasonPosterLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeasonPosterLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeasonPosterLoading(EventArgs e)
        {
            EventHandler handler = this.SeasonPosterLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeriesBannerLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeriesBannerLoaded(EventArgs e)
        {
            EventHandler handler = this.SeriesBannerLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeriesFanartLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeriesFanartLoaded(EventArgs e)
        {
            EventHandler handler = this.SeriesFanartLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeriesFanartLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeriesFanartLoading(EventArgs e)
        {
            EventHandler handler = this.SeriesFanartLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeriesPosterLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeriesPosterLoaded(EventArgs e)
        {
            EventHandler handler = this.SeriesPosterLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the SeriesPosterLoading event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeSeriesPosterLoading(EventArgs e)
        {
            EventHandler handler = this.SeriesPosterLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the TVDBChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeTVDBChanged(EventArgs e)
        {
            EventHandler handler = this.TVDBChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the UpdateProgressChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeUpdateProgressChanged(EventArgs e)
        {
            EventHandler handler = this.UpdateProgressChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        ///   Load episode screenshot
        /// </summary>
        /// <returns> Episode image </returns>
        public Image LoadEpisode()
        {
            if (!string.IsNullOrEmpty(this.CurrentEpisode.EpisodeScreenshotPath)
                && File.Exists(this.CurrentEpisode.EpisodeScreenshotPath))
            {
                return ImageHandler.LoadImage(this.CurrentEpisode.EpisodeScreenshotPath);
            }

            string url = this.GetImageUrl(this.CurrentEpisode.EpisodeScreenshotUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                return null;
            }

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            return ImageHandler.LoadImage(urlCache);
        }

        /// <summary>
        ///   Load season banner.
        /// </summary>
        /// <returns> Season Banner image </returns>
        public Image LoadSeasonBanner()
        {
            if (!string.IsNullOrEmpty(this.currentSeason.BannerPath) && File.Exists(this.currentSeason.BannerPath))
            {
                return ImageHandler.LoadImage(this.currentSeason.BannerPath);
            }

            string url = this.GetImageUrl(this.currentSeason.BannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = ImageHandler.LoadImage(urlCache);

            if (this.CurrentSeries.SmallBanner == null)
            {
                this.CurrentSeries.SmallBanner = ImageHandler.ResizeImage(image, 100, 30);
            }

            return image;
        }

        /// <summary>
        ///   Load season fanart.
        /// </summary>
        /// <returns> Season fanart image </returns>
        public Image LoadSeasonFanart()
        {
            if (!string.IsNullOrEmpty(this.currentSeason.FanartPath) && File.Exists(this.currentSeason.FanartPath))
            {
                return ImageHandler.LoadImage(this.currentSeason.FanartPath);
            }

            string url = this.GetImageUrl(this.currentSeason.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            try
            {
                Image image = ImageHandler.LoadImage(urlCache);

                if (this.CurrentSeries.SmallPoster == null)
                {
                    this.CurrentSeries.SmallPoster = ImageHandler.ResizeImage(image, 100, 60);
                }

                return image;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Could not load image", urlCache + " - " + ex.Message);
            }

            return null;
        }

        /// <summary>
        ///   The load season poster.
        /// </summary>
        /// <returns> Season poster image </returns>
        public Image LoadSeasonPoster()
        {
            if (!string.IsNullOrEmpty(this.currentSeason.PosterPath) && File.Exists(this.currentSeason.PosterPath))
            {
                return ImageHandler.LoadImage(this.currentSeason.PosterPath);
            }

            string url = this.GetImageUrl(this.currentSeason.PosterUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = null;

            try
            {
                image = ImageHandler.LoadImage(urlCache);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Failed loading image from cache : LoadSeasonPoster", ex.Message);
            }

            return image;
        }

        /// <summary>
        ///   The load series banner.
        /// </summary>
        /// <returns> Series banner image </returns>
        public Image LoadSeriesBanner()
        {
            if (!string.IsNullOrEmpty(this.CurrentSeries.SeriesBannerPath)
                && File.Exists(this.CurrentSeries.SeriesBannerPath))
            {
                return ImageHandler.LoadImage(this.CurrentSeries.SeriesBannerPath);
            }

            string url = this.GetImageUrl(this.CurrentSeries.SeriesBannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = null;

            try
            {
                image = ImageHandler.LoadImage(urlCache);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Failed loading image from cache : LoadSeasonPoster", ex.Message);
            }

            return image;
        }

        /// <summary>
        ///   Load series fanart.
        /// </summary>
        /// <returns> Series Fanart image </returns>
        public Image LoadSeriesFanart()
        {
            if (!string.IsNullOrEmpty(this.CurrentSeries.FanartPath) && File.Exists(this.CurrentSeries.FanartPath))
            {
                return ImageHandler.LoadImage(this.CurrentSeries.FanartPath);
            }

            string url = this.GetImageUrl(this.CurrentSeries.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = null;

            try
            {
                image = ImageHandler.LoadImage(urlCache);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Failed loading image from cache : LoadSeasonPoster", ex.Message);
            }

            if (this.CurrentSeries.SmallFanart == null)
            {
                this.CurrentSeries.SmallFanart = ImageHandler.ResizeImage(image, 100, 60);
            }

            return image;
        }

        /// <summary>
        ///   Load series poster.
        /// </summary>
        /// <returns> Series Poster image </returns>
        public Image LoadSeriesPoster()
        {
            if (!string.IsNullOrEmpty(this.CurrentSeries.PosterPath) && File.Exists(this.CurrentSeries.PosterPath))
            {
                return ImageHandler.LoadImage(this.CurrentSeries.PosterPath);
            }

            string url = this.GetImageUrl(this.CurrentSeries.PosterUrl);

            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = null;

            try
            {
                if (!File.Exists(urlCache))
                {
                    Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                }

                image = ImageHandler.LoadImage(urlCache);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Failed loading image from cache : LoadSeasonPoster", ex.Message);
            }

            if (this.CurrentSeries.SmallPoster == null)
            {
                this.CurrentSeries.SmallPoster = ImageHandler.ResizeImage(image, 100, 150);
            }

            return image;
        }

        /// <summary>
        /// The lock episode.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void LockEpisode(Episode episode)
        {
            episode.IsLocked = true;
        }

        /// <summary>
        /// The lock season.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        public void LockSeason(Season season)
        {
            season.IsLocked = true;
        }

        /// <summary>
        /// The open episode file.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void OpenEpisodeFile(Episode episode)
        {
            if (File.Exists(episode.FilePath.PathAndFilename))
            {
                Process.Start(episode.FilePath.PathAndFilename);
            }
        }

        /// <summary>
        /// The open episode folder.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void OpenEpisodeFolder(Episode episode)
        {
            string argument = string.Format(
                @"/select,""{0}""", 
                File.Exists(episode.FilePath.PathAndFilename)
                    ? episode.FilePath.PathAndFilename
                    : episode.FilePath.FolderPath);

            Process.Start("explorer.exe", argument);
        }

        /// <summary>
        ///   The process database update.
        /// </summary>
        public void ProcessDatabaseUpdate()
        {
            this.GenerateMasterSeriesList();
            this.AddImagesToBackgroundDownload();
            this.LoadSeriesNFO();
        }

        /// <summary>
        /// The restore hidden series.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        public void RestoreHiddenSeries(Series series)
        {
            this.masterSeriesNameList.Add(
                new MasterSeriesListModel
                    {
                        BannerPath = series.SeriesBannerPath, 
                        Locked = false, 
                        SeriesGuid = series.Guid, 
                        SeriesName = series.SeriesName
                    });

            this.TVDatabase.Add(series);

            this.HiddenTVDB.Remove(series);

            this.InvokeTVDBChanged(new EventArgs());

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        /// The search default show database.
        /// </summary>
        /// <param name="showName">
        /// The show name. 
        /// </param>
        /// <returns>
        /// The <see cref="ThreadedBindingList"/> . 
        /// </returns>
        public ThreadedBindingList<SearchDetails> SearchDefaultShowDatabase(string showName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TV", "Defaults", "DefaultShows.xml");

            if (!File.Exists(path))
            {
                return new ThreadedBindingList<SearchDetails>();
            }

            var xml = XRead.OpenPath(path);
            var shows = xml.GetElementsByTagName("show");

            var returnList = new ThreadedBindingList<SearchDetails>();

            foreach (XmlNode show in shows)
            {
                if (show.Attributes != null)
                {
                    if (show.Attributes["name"].Value.Equals(showName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        var searchDetails = new SearchDetails
                            {
                               SeriesName = show.Attributes["name"].Value, SeriesID = show.Attributes["id"].Value 
                            };

                        returnList.Add(searchDetails);

                        return returnList;
                    }
                }
            }

            return new ThreadedBindingList<SearchDetails>();
        }

        /// <summary>
        ///   Check if season banner has downloaded
        /// </summary>
        /// <returns> Return value </returns>
        public bool SeasonBannerDownloaded()
        {
            string url = this.GetImageUrl(this.currentSeason.BannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Check if season fanart has downloaded
        /// </summary>
        /// <returns> Return value </returns>
        public bool SeasonFanartDownloaded()
        {
            string url = this.GetImageUrl(this.currentSeason.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Check if season poster has downloaded
        /// </summary>
        /// <returns> Return value </returns>
        public bool SeasonPosterDownloaded()
        {
            string url = this.GetImageUrl(this.currentSeason.PosterUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Check if series poster has downloaded
        /// </summary>
        /// <returns> Return value </returns>
        public bool SeriesPosterDownloaded()
        {
            string url = this.GetImageUrl(this.CurrentSeries.PosterUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Set current episode by GUID
        /// </summary>
        /// <param name="guid">
        /// The GUID value 
        /// </param>
        public void SetCurrentEpisode(string guid)
        {
            Episode episode = (from e in this.currentSeason.Episodes where e.Guid == guid select e).SingleOrDefault();

            if (episode != null)
            {
                this.CurrentEpisode = episode;
                this.InvokeGalleryEpisodeChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Set current season by GUID
        /// </summary>
        /// <param name="guid">
        /// The GUID value 
        /// </param>
        public void SetCurrentSeason(string guid)
        {
            Season season = (from e in this.CurrentSeries.Seasons where e.Guid == guid select e).SingleOrDefault();

            if (season != null)
            {
                this.currentSeason = season;
                this.InvokeCurrentSeasonChanged(new EventArgs());
                this.InvokeCurrentEpisodeChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Set current series by GUID
        /// </summary>
        /// <param name="guid">
        /// The GUID value 
        /// </param>
        public void SetCurrentSeries(string guid)
        {
            Series series = this.GetSeriesFromGUID(guid);

            if (series != null)
            {
                this.CurrentSeries = series;
                this.InvokeCurrentSeriesChanged(new EventArgs());
            }
        }

        /// <summary>
        /// The set episode unwatched.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void SetEpisodeUnwatched(Episode episode)
        {
            episode.Watched = false;
        }

        /// <summary>
        /// The set episode watched.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void SetEpisodeWatched(Episode episode)
        {
            episode.Watched = true;
        }

        /// <summary>
        /// The set series hide.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        public void SetSeriesHide(MasterSeriesListModel series)
        {
            this.HideSeries(series.SeriesName);

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        /// The unlock episode.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        public void UnlockEpisode(Episode episode)
        {
            episode.IsLocked = false;
        }

        /// <summary>
        /// The unlock season.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        public void UnlockSeason(Season season)
        {
            season.IsLocked = false;
        }

        /// <summary>
        /// Update episode.
        /// </summary>
        /// <param name="season">
        /// The season to update 
        /// </param>
        /// <param name="newEpisode">
        /// The new episode. 
        /// </param>
        /// <param name="force">
        /// The force. 
        /// </param>
        public void UpdateEpisode(Season season, Episode newEpisode, bool force = false)
        {
            Log.WriteToLog(
                LogSeverity.Debug, 
                0, 
                "Factories > TVDBFactory > UpdateEpisode", 
                string.Format(
                    "Updating episode {0} in season {1} ({2}). Forced: {3}", 
                    newEpisode.EpisodeNumber, 
                    newEpisode.SeasonNumber, 
                    newEpisode.GetSeriesName(), 
                    force ? "true" : "false"));

            Episode episode =
                (from e in season.Episodes where e.EpisodeNumber == newEpisode.EpisodeNumber select e).SingleOrDefault();

            if (episode == null)
            {
                season.Episodes.Add(newEpisode);
                return;
            }

            if (!newEpisode.IsLocked && (episode.Lastupdated != newEpisode.Lastupdated || force))
            {
                episode.AbsoluteNumber = newEpisode.AbsoluteNumber;
                episode.CombinedEpisodenumber = newEpisode.CombinedEpisodenumber;
                episode.CombinedSeason = newEpisode.CombinedSeason;
                episode.Director = newEpisode.Director;
                episode.DvdChapter = newEpisode.DvdChapter;
                episode.DvdDiscid = newEpisode.DvdDiscid;
                episode.DvdEpisodenumber = newEpisode.DvdEpisodenumber;
                episode.DvdSeason = newEpisode.DvdSeason;
                episode.EpisodeImgFlag = newEpisode.EpisodeImgFlag;
                episode.EpisodeName = newEpisode.EpisodeName;
                episode.EpisodeNumber = newEpisode.EpisodeNumber;
                episode.FirstAired = newEpisode.FirstAired;
                episode.GuestStars = newEpisode.GuestStars;
                episode.IMDBID = newEpisode.IMDBID;
                episode.Language = newEpisode.Language;
                episode.Overview = newEpisode.Overview;
                episode.ProductionCode = newEpisode.ProductionCode;
                episode.Rating = newEpisode.Rating;
                episode.SeasonNumber = newEpisode.SeasonNumber;
                episode.Writers = newEpisode.Writers;

                if (string.IsNullOrEmpty(episode.EpisodeScreenshotPath)
                    && string.IsNullOrEmpty(episode.EpisodeScreenshotUrl))
                {
                    episode.EpisodeScreenshotUrl = newEpisode.EpisodeScreenshotUrl;
                }
            }
        }

        /// <summary>
        /// The update episode file.
        /// </summary>
        /// <param name="move">
        /// if set to <c>true</c> [move]. 
        /// </param>
        public void UpdateEpisodeFile(bool move)
        {
            var fileDialog = new OpenFileDialog();

            string series = this.CurrentSeries.SeriesName;
            int season = this.currentSeason.SeasonNumber;
            string episode = this.CurrentEpisode.EpisodeName;

            fileDialog.Title = string.Format("Assign a file to {0} > {1} > {2}", series, season, episode);
            fileDialog.InitialDirectory = this.currentSeason.GetSeasonPath();

            bool? dialogResult = fileDialog.ShowDialog();

            if (dialogResult == true)
            {
                this.CurrentEpisode.FilePath.PathAndFilename = fileDialog.FileName;
                this.InvokeCurrentEpisodeChanged(new EventArgs());
            }
        }

        /// <summary>
        /// The update season.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <param name="newSeason">
        /// The new season. 
        /// </param>
        /// <param name="episodeNumber">
        /// The episode Number. 
        /// </param>
        /// <param name="force">
        /// The force. 
        /// </param>
        public void UpdateSeason(Series series, Season newSeason, int? episodeNumber = null, bool force = false)
        {
            Log.WriteToLog(
                LogSeverity.Debug, 
                0, 
                "Factories > TVDBFactory > UpdateSeason", 
                string.Format(
                    "Called with episodeNumber {0} ({1}). Forced: {2}", 
                    episodeNumber.GetValueOrDefault(0), 
                    series.SeriesName, 
                    force ? "true" : "false"));

            if (series.Seasons.All(x => x.SeasonNumber != newSeason.SeasonNumber))
            {
                series.Seasons.Add(newSeason);
                return;
            }

            Season season = series.Seasons[newSeason.SeasonNumber];

            if (!season.IsLocked)
            {
                if (string.IsNullOrEmpty(season.BannerPath) && string.IsNullOrEmpty(season.BannerUrl))
                {
                    season.BannerUrl = newSeason.BannerUrl;
                }

                if (string.IsNullOrEmpty(season.FanartPath) && string.IsNullOrEmpty(season.FanartUrl))
                {
                    season.FanartPath = newSeason.FanartUrl;
                }

                if (string.IsNullOrEmpty(season.PosterPath) && string.IsNullOrEmpty(season.PosterUrl))
                {
                    season.PosterPath = newSeason.PosterUrl;
                }
            }

            foreach (Episode episode in newSeason.Episodes)
            {
                if (episodeNumber == null)
                {
                    this.UpdateEpisode(season, episode, force);
                }
                else if (episode.EpisodeNumber == episodeNumber)
                {
                    this.UpdateEpisode(season, episode, true);
                }
            }
        }

        /// <summary>
        /// Update series from TVDB database.
        /// </summary>
        /// <param name="seriesId">
        /// The series id. 
        /// </param>
        /// <param name="seasonNumber">
        /// The season Number. 
        /// </param>
        /// <param name="episodeNumber">
        /// The episode Number. 
        /// </param>
        public void UpdateSeries(uint? seriesId, int? seasonNumber = null, int? episodeNumber = null)
        {
            Series seriesObj = this.GetSeriesFromSeriesID(seriesId);

            Log.WriteToLog(
                LogSeverity.Debug, 
                0, 
                "Factories > TVDBFactory > UpdateSeries", 
                string.Format(
                    "Called with seriesId {0}({1}), seasonNumber {2}, episodeNumber {3}", 
                    seriesId.GetValueOrDefault(0), 
                    seriesObj.SeriesName, 
                    seasonNumber.GetValueOrDefault(0), 
                    episodeNumber.GetValueOrDefault(0)));

            var tvdb = new TheTvdb();

            Series newSeries = tvdb.CheckForUpdate(seriesObj.SeriesID, seriesObj.Language, seriesObj.Lastupdated, true);

            if (!seriesObj.IsLocked && seasonNumber == null && episodeNumber == null)
            {
                seriesObj.Added = newSeries.Added;
                seriesObj.AddedBy = newSeries.AddedBy;
                seriesObj.AirsDayOfWeek = newSeries.AirsDayOfWeek;
                seriesObj.AirsTime = newSeries.AirsTime;
                seriesObj.ContentRating = newSeries.ContentRating;
                seriesObj.Country = newSeries.Country;
                seriesObj.Genre = newSeries.Genre;
                seriesObj.ImdbId = newSeries.ImdbId;
                seriesObj.Language = newSeries.ImdbId;
                seriesObj.Lastupdated = newSeries.Lastupdated;
                seriesObj.Network = newSeries.Lastupdated;
                seriesObj.NetworkID = newSeries.NetworkID;
                seriesObj.Overview = newSeries.Overview;
                seriesObj.Rating = newSeries.Rating;
                seriesObj.Runtime = newSeries.Runtime;
                seriesObj.SeriesName = newSeries.SeriesName;
                seriesObj.Status = newSeries.Status;
                seriesObj.Zap2It_Id = newSeries.Zap2It_Id;

                if (string.IsNullOrEmpty(seriesObj.SeriesBannerPath) && string.IsNullOrEmpty(seriesObj.SeriesBannerUrl))
                {
                    seriesObj.SeriesBannerUrl = newSeries.SeriesBannerUrl;
                }

                if (string.IsNullOrEmpty(seriesObj.PosterUrl) && string.IsNullOrEmpty(seriesObj.PosterPath))
                {
                    seriesObj.PosterUrl = newSeries.PosterUrl;
                }

                if (string.IsNullOrEmpty(seriesObj.FanartUrl) && string.IsNullOrEmpty(seriesObj.FanartPath))
                {
                    seriesObj.FanartUrl = newSeries.FanartUrl;
                }
            }

            foreach (var season in newSeries.Seasons)
            {
                if (seasonNumber == null)
                {
                    this.UpdateSeason(seriesObj, season, episodeNumber);
                }
                else if (season.SeasonNumber == seasonNumber)
                {
                    this.UpdateSeason(seriesObj, season, episodeNumber, true);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   The add images to background download.
        /// </summary>
        private void AddImagesToBackgroundDownload()
        {
            foreach (var series in this.TVDatabase)
            {
                this.UpdateStatus = "Process Images for " + series.SeriesName;

                this.ProcessSeriesToBackground(series);

                foreach (var season in series.Seasons)
                {
                    this.ProcessSeasonToBackground(season);

                    foreach (Episode episode in season.Episodes)
                    {
                        this.ProcessEpisodeToBackground(episode);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the DoWork event of the Banner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwBannerDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwBannerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeBannerLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwEpisodeDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwEpisodeRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeEpisodeLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwFanartDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwFanartRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeFanartLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonBannerDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonBannerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeSeasonBannerLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonFanartDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonFanartRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeSeasonFanartLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonPosterDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeasonPosterRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeSeasonPosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeriesPosterDoWork(object sender, DoWorkEventArgs e)
        {
            string url = this.GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwSeriesPosterRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokeSeriesPosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Get series from series id.
        /// </summary>
        /// <param name="seriesId">
        /// The series id. 
        /// </param>
        /// <returns>
        /// Return Series object 
        /// </returns>
        private Series GetSeriesFromSeriesID(uint? seriesId)
        {
            return (from s in this.TVDatabase where s.SeriesID == seriesId select s).SingleOrDefault();
        }

        /// <summary>
        /// The hide series.
        /// </summary>
        /// <param name="seriesName">
        /// The series name. 
        /// </param>
        private void HideSeries(string seriesName)
        {
            var series = this.MasterSeriesList.Single(c => c.SeriesName == seriesName);
            this.masterSeriesNameList.Remove(series);

            this.HiddenTVDB.Add(this.TVDatabase.First(x => x.SeriesName == seriesName));
            this.TVDatabase.Remove(this.TVDatabase.First(x => x.SeriesName == seriesName));

            this.InvokeTVDBChanged(new EventArgs());

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        ///   The load series NFO
        /// </summary>
        private void LoadSeriesNFO()
        {
            foreach (var series in this.TVDatabase)
            {
                this.UpdateStatus = "Loading Series: " + series.SeriesName;
                OutFactory.LoadSeries(series);
            }
        }

        /// <summary>
        /// Handles the ListChanged event of the masterSeriesNameList control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data. 
        /// </param>
        private void MasterSeriesNameListListChanged(object sender, ListChangedEventArgs e)
        {
            this.InvokeTVDBChanged(new EventArgs());
        }

        /// <summary>
        /// The process episode to background.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        private void ProcessEpisodeToBackground(Episode episode)
        {
            if (!string.IsNullOrEmpty(episode.CurrentFilenameAndPath))
            {
                var current = GenerateOutput.AccessCurrentIOHandler();

                if (current == null)
                {
                    return;
                }

                string path = current.GetEpisodeScreenshot(episode);

                if (string.IsNullOrEmpty(path))
                {
                    var downloadItem = new DownloadItem
                        {
                            Url = this.GetImageUrl(episode.EpisodeScreenshotUrl), 
                            Type = DownloadType.Binary, 
                            Section = Section.Tv
                        };

                    Downloader.AddToBackgroundQueue(downloadItem);
                }
                else
                {
                    episode.EpisodeScreenshotPath = path;
                }
            }
        }

        /// <summary>
        /// Processes the season to the background queue.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        private void ProcessSeasonToBackground(Season season)
        {
            if (season.ContainsEpisodesWithFiles())
            {
                var current = GenerateOutput.AccessCurrentIOHandler();

                if (current == null)
                {
                    return;
                }

                string bannerPath = current.GetSeasonBanner(season);

                if (string.IsNullOrEmpty(bannerPath))
                {
                    var downloadItem = new DownloadItem
                        {
                           Url = this.GetImageUrl(season.BannerUrl), Type = DownloadType.Binary, Section = Section.Tv 
                        };

                    Downloader.AddToBackgroundQueue(downloadItem);
                }
                else
                {
                    season.BannerPath = bannerPath;
                }

                string posterPath = current.GetSeasonPoster(season);

                if (string.IsNullOrEmpty(posterPath))
                {
                    var downloadItem = new DownloadItem
                        {
                           Url = this.GetImageUrl(season.PosterUrl), Type = DownloadType.Binary, Section = Section.Tv 
                        };

                    Downloader.AddToBackgroundQueue(downloadItem);
                }
                else
                {
                    season.PosterPath = posterPath;
                }

                string fanartPath = current.GetSeasonFanart(season);

                if (string.IsNullOrEmpty(fanartPath))
                {
                    var downloadItem = new DownloadItem
                        {
                           Url = this.GetImageUrl(season.FanartUrl), Type = DownloadType.Binary, Section = Section.Tv 
                        };

                    Downloader.AddToBackgroundQueue(downloadItem);
                }
                else
                {
                    season.FanartPath = fanartPath;
                }
            }
        }

        /// <summary>
        /// The process series to background.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        private void ProcessSeriesToBackground(Series series)
        {
            var current = GenerateOutput.AccessCurrentIOHandler();

            if (current == null)
            {
                return;
            }

            string posterPath = current.GetSeriesPoster(series);
            string fanartPath = current.GetSeriesFanart(series);
            string bannerPath = current.GetSeriesBanner(series);

            if (!string.IsNullOrEmpty(posterPath))
            {
                series.PosterPath = posterPath;
            }

            if (!string.IsNullOrEmpty(fanartPath))
            {
                series.FanartPath = fanartPath;
            }

            if (!string.IsNullOrEmpty(bannerPath))
            {
                series.SeriesBannerPath = bannerPath;
            }
        }

        #endregion
    }
}