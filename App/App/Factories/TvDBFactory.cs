// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvDBFactory.cs" company="The YANFOE Project">
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
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.Utils;
    using DevExpress.XtraBars.Ribbon;

    using YANFOE.Factories.InOut;
    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.IO;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Scrapers.TV;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.UI.Dialogs.TV;

    #endregion

    /// <summary>
    /// The tv db factory.
    /// </summary>
    [Serializable]
    public class TvDBFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The current season.
        /// </summary>
        public static Season CurrentSeason;

        /// <summary>
        /// The gallery group.
        /// </summary>
        private static readonly GalleryItemGroup galleryGroup;

        /// <summary>
        /// The current selected episode.
        /// </summary>
        private static List<Episode> currentSelectedEpisode;

        /// <summary>
        /// The current selected season.
        /// </summary>
        private static List<Season> currentSelectedSeason;

        /// <summary>
        /// The current selected series.
        /// </summary>
        private static List<Series> currentSelectedSeries;

        /// <summary>
        /// The master series name list.
        /// </summary>
        private static BindingList<MasterSeriesListModel> masterSeriesNameList;

        /// <summary>
        /// The tv database.
        /// </summary>
        private static SortedList<string, Series> tvDatabase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="TvDBFactory"/> class. 
        /// </summary>
        static TvDBFactory()
        {
            tvDatabase = new SortedList<string, Series>();

            CurrentSeries = new Series();
            CurrentSeason = new Season();
            CurrentEpisode = new Episode();

            galleryGroup = new GalleryItemGroup();

            masterSeriesNameList = new BindingList<MasterSeriesListModel>();

            currentSelectedSeries = new List<Series>();
            currentSelectedSeason = new List<Season>();
            currentSelectedEpisode = new List<Episode>();
        }

        #endregion

        #region Events

        [field: NonSerialized]
        public static event EventHandler MasterSeriesNameListChanged = delegate { };

        /// <summary>
        /// Occurs when [current episode changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentEpisodeChanged = delegate { };

        /// <summary>
        /// Occurs when [current season changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentSeasonChanged = delegate { };

        /// <summary>
        /// Occurs when [current series changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentSeriesChanged = delegate { };

        /// <summary>
        /// Occurs when [episode loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler EpisodeLoaded = delegate { };

        /// <summary>
        /// Occurs when [episode loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler EpisodeLoading = delegate { };

        /// <summary>
        /// Occurs when [gallery changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler GalleryChanged = delegate { };

        /// <summary>
        /// Occurs when [season banner loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonBannerLoaded = delegate { };

        /// <summary>
        /// Occurs when [season banner loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonBannerLoading = delegate { };

        /// <summary>
        /// Occurs when [season fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonFanartLoaded = delegate { };

        /// <summary>
        /// Occurs when [season fanart loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonFanartLoading = delegate { };

        /// <summary>
        /// Occurs when [season poster loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonPosterLoaded = delegate { };

        /// <summary>
        /// Occurs when [season poster loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeasonPosterLoading = delegate { };

        /// <summary>
        /// Occurs when [series banner loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesBannerLoaded = delegate { };

        /// <summary>
        /// Occurs when [series banner loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesBannerLoading = delegate { };

        /// <summary>
        /// Occurs when [series fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesFanartLoaded = delegate { };

        /// <summary>
        /// Occurs when [series fanart loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesFanartLoading = delegate { };

        /// <summary>
        /// Occurs when [series poster loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesPosterLoaded = delegate { };

        /// <summary>
        /// Occurs when [series poster loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesPosterLoading = delegate { };

        /// <summary>
        /// Occurs when [tv db changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler TvDbChanged = delegate { };

        /// <summary>
        /// Occurs when [update progress changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler UpdateProgressChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CurrentSelectedEpisode.
        /// </summary>
        public static List<Episode> CurrentSelectedEpisode
        {
            get
            {
                return currentSelectedEpisode;
            }

            set
            {
                currentSelectedEpisode = value;
            }
        }

        /// <summary>
        /// Gets or sets CurrentSelectedSeason.
        /// </summary>
        public static List<Season> CurrentSelectedSeason
        {
            get
            {
                return currentSelectedSeason;
            }

            set
            {
                currentSelectedSeason = value;
            }
        }

        /// <summary>
        /// Gets or sets CurrentSelectedSeries.
        /// </summary>
        public static List<Series> CurrentSelectedSeries
        {
            get
            {
                return currentSelectedSeries;
            }

            set
            {
                currentSelectedSeries = value;
            }
        }

        /// <summary>
        /// The current series.
        /// </summary>
        public static Series CurrentSeries { get; set; }

        /// <summary>
        /// Gets GetCurrentSeasonsList.
        /// </summary>
        public static List<Season> GetCurrentSeasonsList
        {
            get
            {
                if (!Settings.Get.Ui.HideSeasonZero)
                {
                    return (from s in CurrentSeries.Seasons.AsParallel().AsOrdered() where s.Value.SeasonNumber != 0 select s.Value).ToList();
                }
                else
                {
                    return (from s in CurrentSeries.Seasons.AsParallel().AsOrdered() select s.Value).ToList();
                }

            }
        }

        /// <summary>
        /// Gets or sets the series name list.
        /// </summary>
        /// <value>The series name list.</value>
        public static BindingList<MasterSeriesListModel> MasterSeriesNameList
        {
            get
            {
                return masterSeriesNameList;
            }

            set
            {
                masterSeriesNameList = value;
            }
        }

        /// <summary>
        /// Gets or sets the tv database.
        /// </summary>
        /// <value>The tv database.</value>
        public static SortedList<string, Series> TvDatabase
        {
            get
            {
                return tvDatabase;
            }

            set
            {
                tvDatabase = value;
            }
        }

        /// <summary>
        /// Gets or sets UpdateStatus.
        /// </summary>
        public static string UpdateStatus { get; set; }

        /// <summary>
        /// The current episode.
        /// </summary>
        public static Episode CurrentEpisode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if banner downloaded.
        /// </summary>
        /// <returns>
        /// Downloaded status
        /// </returns>
        public static bool BannerDownloaded()
        {
            string url = GetImageUrl(CurrentSeries.SeriesBannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// The change update status.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void ChangeUpdateStatus(string value)
        {
            UpdateStatus = value;
            InvokeUpdateProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The default current episode.
        /// </summary>
        public static void DefaultCurrentEpisode()
        {
            foreach (var season in CurrentSeries.Seasons)
            {
                foreach (Episode episode in season.Value.Episodes)
                {
                    SetCurrentEpisode(episode.Guid);
                    break;
                }

                break;
            }
        }

        /// <summary>
        /// The default current season and episode.
        /// </summary>
        public static void DefaultCurrentSeasonAndEpisode()
        {
            foreach (var season in CurrentSeries.Seasons)
            {
                SetCurrentSeason(season.Value.Guid);

                foreach (Episode episode in season.Value.Episodes)
                {
                    SetCurrentEpisode(episode.Guid);
                    break;
                }

                break;
            }
        }

        /// <summary>
        /// Check if episode screenshot downloaded.
        /// </summary>
        /// <returns>
        /// Downloaded status
        /// </returns>
        public static bool EpisodeDownloaded()
        {
            string url = GetImageUrl(CurrentEpisode.EpisodeScreenshotUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Check if fanart downloaded.
        /// </summary>
        /// <returns>
        /// Downloaded status
        /// </returns>
        public static bool FanartDownloaded()
        {
            string url = GetImageUrl(CurrentSeries.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Generates the master series list.
        /// </summary>
        public static void GenerateMasterSeriesList()
        {
            BindingList<MasterSeriesListModel> list = (from s in tvDatabase.AsParallel().AsOrdered()
                                                       select
                                                           new MasterSeriesListModel
                                                               {
                                                                   SeriesName = s.Value.SeriesName, 
                                                                   BannerPath = s.Value.SeriesBannerUrl, 
                                                                   SeriesGuid = s.Value.Guid
                                                               }).ToBindingList();

            masterSeriesNameList.Clear();

            foreach (MasterSeriesListModel v in list)
            {
                masterSeriesNameList.Add(v);
            }

            MasterMediaDBFactory.PopulateMasterTvMediaDatabase();
        }

        /// <summary>
        /// The generate picture gallery.
        /// </summary>
        public static void GeneratePictureGallery()
        {
            foreach (var series in tvDatabase)
            {
                if (series.Value.SmallBanner == null && !string.IsNullOrEmpty(series.Value.SeriesBannerPath))
                {
                    if (File.Exists(series.Value.SeriesBannerPath))
                    {
                        Image banner = ImageHandler.LoadImage(series.Value.SeriesBannerPath);
                        series.Value.SmallBanner = ImageHandler.ResizeImage(banner, 300, 55);
                    }
                }

                if (series.Value.SmallBanner != null)
                {
                    var galleryItem = new GalleryItem(series.Value.SmallBanner, series.Value.SeriesName, string.Empty)
                        {
                           Tag = series.Value.Guid 
                        };

                    if (!galleryGroup.Items.Contains(galleryItem))
                    {
                        galleryGroup.Items.Add(galleryItem);
                    }
                }
            }

            InvokeGalleryChanged(new EventArgs());
        }

        /// <summary>
        /// Get collection of episodes in current seasons
        /// </summary>
        /// <returns>
        /// Collection of episodes in current seasons
        /// </returns>
        public static List<Episode> GetCurrentEpisodeList()
        {
            return (from s in CurrentSeason.Episodes.AsParallel().AsOrdered() select s).ToList();
        }

        /// <summary>
        /// The get episode.
        /// </summary>
        public static void GetEpisode()
        {
            if (EpisodeDownloaded())
            {
                InvokeEpisodeLoaded(new EventArgs());
                return;
            }

            InvokeEpisodeLoading(new EventArgs());

            var bgwEpisode = new BackgroundWorker();

            bgwEpisode.DoWork += BgwEpisode_DoWork;
            bgwEpisode.RunWorkerCompleted += BgwEpisode_RunWorkerCompleted;
            bgwEpisode.RunWorkerAsync(CurrentEpisode.EpisodeScreenshotUrl);
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
        public static SuperToolTip GetEpisodeSuperTip(Episode episode)
        {
            if (episode == null)
            {
                return new SuperToolTip();
            }

            var superTip = new SuperToolTip();

            superTip.Items.AddTitle(episode.EpisodeName);

            if (!string.IsNullOrEmpty(episode.EpisodeScreenshotUrl))
            {
                string url = GetImageUrl(episode.EpisodeScreenshotUrl);
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
        /// GalleryGroup galleryitemgroup
        /// </returns>
        public static GalleryItemGroup GetGalleryGroup()
        {
            return galleryGroup;
        }

        /// <summary>
        /// Get TvDB image url.
        /// </summary>
        /// <param name="value">
        /// The image value.
        /// </param>
        /// <returns>
        /// The get image url.
        /// </returns>
        public static string GetImageUrl(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.ToLower().Contains("http://"))
            {
                return value;
            }

            return "http://cache.thetvdb.com/banners/" + value;
        }

        /// <summary>
        /// The get season banner.
        /// </summary>
        public static void GetSeasonBanner()
        {
            if (SeasonBannerDownloaded())
            {
                InvokeSeasonBannerLoaded(new EventArgs());
                return;
            }

            InvokeSeasonBannerLoading(new EventArgs());

            var bgwSeasonBanner = new BackgroundWorker();

            bgwSeasonBanner.DoWork += BgwSeasonBanner_DoWork;

            bgwSeasonBanner.RunWorkerCompleted += BgwSeasonBanner_RunWorkerCompleted;

            bgwSeasonBanner.RunWorkerAsync(CurrentSeason.BannerUrl);
        }

        /// <summary>
        /// The get season fanart.
        /// </summary>
        public static void GetSeasonFanart()
        {
            if (SeasonFanartDownloaded())
            {
                InvokeSeasonFanartLoaded(new EventArgs());
                return;
            }

            InvokeSeasonFanartLoading(new EventArgs());

            var bgwSeasonFanart = new BackgroundWorker();

            bgwSeasonFanart.DoWork += BgwSeasonFanart_DoWork;

            bgwSeasonFanart.RunWorkerCompleted += BgwSeasonFanart_RunWorkerCompleted;

            bgwSeasonFanart.RunWorkerAsync(CurrentSeason.FanartUrl);
        }

        /// <summary>
        /// The get season poster.
        /// </summary>
        public static void GetSeasonPoster()
        {
            if (SeasonPosterDownloaded())
            {
                InvokeSeasonPosterLoaded(new EventArgs());
                return;
            }

            InvokeSeasonPosterLoading(new EventArgs());

            var bgwSeasonPoster = new BackgroundWorker();

            bgwSeasonPoster.DoWork += BgwSeasonPoster_DoWork;

            bgwSeasonPoster.RunWorkerCompleted += BgwSeasonPoster_RunWorkerCompleted;

            bgwSeasonPoster.RunWorkerAsync(CurrentSeason.PosterUrl);
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
        public static SuperToolTip GetSeasonSuperTip(Season season)
        {
            if (season == null)
            {
                return new SuperToolTip();
            }

            var superTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };

            superTip.Items.AddTitle(string.Format("Season {0}", season.SeasonNumber));

            foreach (Episode episode in season.Episodes)
            {
                var tip = new ToolTipItem();

                string found = File.Exists(episode.FilePath.FileNameAndPath)
                                   ? "<color=206,244,208>File Exists</color>"
                                   : "<b><color=244,206,206>File Does Not Exist</color></b>";

                tip.Text = string.Format("<b>{0}</b>:{1} - {2}", episode.EpisodeNumber, episode.EpisodeName, found);

                superTip.Items.Add(tip);
            }

            return superTip;
        }

        /// <summary>
        /// The get series banner.
        /// </summary>
        public static void GetSeriesBanner()
        {
            if (BannerDownloaded())
            {
                InvokeBannerLoaded(new EventArgs());
                return;
            }

            InvokeBannerLoading(new EventArgs());

            var bgwBanner = new BackgroundWorker();

            bgwBanner.DoWork += BgwBanner_DoWork;
            bgwBanner.RunWorkerCompleted += BgwBanner_RunWorkerCompleted;
            bgwBanner.RunWorkerAsync(CurrentSeries.SeriesBannerUrl);
        }

        /// <summary>
        /// The get series fanart.
        /// </summary>
        public static void GetSeriesFanart()
        {
            if (FanartDownloaded())
            {
                InvokeFanartLoaded(new EventArgs());
                return;
            }

            InvokeSeriesFanartLoading(new EventArgs());

            var bgwFanart = new BackgroundWorker();

            bgwFanart.DoWork += BgwFanart_DoWork;
            bgwFanart.RunWorkerCompleted += BgwFanart_RunWorkerCompleted;
            bgwFanart.RunWorkerAsync(CurrentSeries.FanartUrl);
        }

        /// <summary>
        /// Get series using series guid.
        /// </summary>
        /// <param name="guid">
        /// The series guid.
        /// </param>
        /// <returns>
        /// Series object
        /// </returns>
        public static Series GetSeriesFromGuid(string guid)
        {
            return (from e in tvDatabase.AsParallel() where e.Value.Guid == guid select e.Value).SingleOrDefault();
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
        public static Series GetSeriesFromName(string seriesName)
        {
            return (from s in tvDatabase.AsParallel() where s.Key == seriesName select s.Value).SingleOrDefault();
        }

        /// <summary>
        /// Get series poster.
        /// </summary>
        public static void GetSeriesPoster()
        {
            if (SeriesPosterDownloaded())
            {
                InvokeSeriesPosterLoaded(new EventArgs());
                return;
            }

            InvokeSeriesPosterLoading(new EventArgs());

            var bgwSeriesPoster = new BackgroundWorker();
            bgwSeriesPoster.DoWork += BgwSeriesPoster_DoWork;
            bgwSeriesPoster.RunWorkerCompleted += BgwSeriesPoster_RunWorkerCompleted;
            bgwSeriesPoster.RunWorkerAsync(CurrentSeries.PosterUrl);
        }

        /// <summary>
        /// Get series super tip.
        /// </summary>
        /// <param name="seriesguid">
        /// The series guid.
        /// </param>
        /// <returns>
        /// Series super tip.
        /// </returns>
        public static SuperToolTip GetSeriesSuperTip(string seriesguid)
        {
            Series series = GetSeriesFromGuid(seriesguid);

            var superTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };

            superTip.Items.AddTitle(series.SeriesName);
            var smallBanner = new ToolTipTitleItem { Image = series.SmallBanner };

            var smallPoster = new ToolTipTitleItem { Image = series.SmallPoster };
            var smallFanart = new ToolTipTitleItem { Image = series.SmallFanart };

            var sb = new StringBuilder();
            sb.Append("<b>Total Seasons</b>: " + series.Seasons.Count + Environment.NewLine);
            sb.Append("<b>Total Episodes</b>: " + GetTotalEpisodes(series) + Environment.NewLine);
            sb.Append("<b>Total Missing Episodes</b>: " + GetTotalEpisodes(series, true) + Environment.NewLine);

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
        public static int GetTotalEpisodes(Series series, bool missingOnly = false)
        {
            int count = 0;

            foreach (var season in series.Seasons)
            {
                if (missingOnly == false)
                {
                    count += season.Value.Episodes.Count;
                }
                else
                {
                    count +=
                        season.Value.Episodes.Count(episode => string.IsNullOrEmpty(episode.FilePath.FileNameAndPath));
                }
            }

            return count;
        }

        public static void InvokeMasterSeriesNameListChanged(EventArgs e)
        {
            EventHandler handler = MasterSeriesNameListChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the BannerLoaded event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        public static void InvokeBannerLoaded(EventArgs e)
        {
            EventHandler handler = SeriesBannerLoaded;
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
        public static void InvokeBannerLoading(EventArgs e)
        {
            EventHandler handler = SeriesBannerLoading;
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
        public static void InvokeCurrentEpisodeChanged(EventArgs e)
        {
            EventHandler handler = CurrentEpisodeChanged;
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
        public static void InvokeCurrentSeasonChanged(EventArgs e)
        {
            EventHandler handler = CurrentSeasonChanged;
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
        public static void InvokeCurrentSeriesChanged(EventArgs e)
        {
            EventHandler handler = CurrentSeriesChanged;
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
        public static void InvokeEpisodeLoaded(EventArgs e)
        {
            EventHandler handler = EpisodeLoaded;
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
        public static void InvokeEpisodeLoading(EventArgs e)
        {
            EventHandler handler = EpisodeLoading;
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
        public static void InvokeFanartLoaded(EventArgs e)
        {
            EventHandler handler = SeriesFanartLoaded;
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
        public static void InvokeGalleryChanged(EventArgs e)
        {
            EventHandler handler = GalleryChanged;
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
        public static void InvokeGalleryEpisodeChanged(EventArgs e)
        {
            EventHandler handler = CurrentEpisodeChanged;
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
        public static void InvokeSeasonBannerLoaded(EventArgs e)
        {
            EventHandler handler = SeasonBannerLoaded;
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
        public static void InvokeSeasonBannerLoading(EventArgs e)
        {
            EventHandler handler = SeasonBannerLoading;
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
        public static void InvokeSeasonFanartLoaded(EventArgs e)
        {
            EventHandler handler = SeasonFanartLoaded;
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
        public static void InvokeSeasonFanartLoading(EventArgs e)
        {
            EventHandler handler = SeasonFanartLoading;
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
        public static void InvokeSeasonPosterLoaded(EventArgs e)
        {
            EventHandler handler = SeasonPosterLoaded;
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
        public static void InvokeSeasonPosterLoading(EventArgs e)
        {
            EventHandler handler = SeasonPosterLoading;
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
        public static void InvokeSeriesBannerLoaded(EventArgs e)
        {
            EventHandler handler = SeriesBannerLoaded;
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
        public static void InvokeSeriesFanartLoaded(EventArgs e)
        {
            EventHandler handler = SeriesFanartLoaded;
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
        public static void InvokeSeriesFanartLoading(EventArgs e)
        {
            EventHandler handler = SeriesFanartLoading;
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
        public static void InvokeSeriesPosterLoaded(EventArgs e)
        {
            EventHandler handler = SeriesPosterLoaded;
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
        public static void InvokeSeriesPosterLoading(EventArgs e)
        {
            EventHandler handler = SeriesPosterLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the TvDbChanged event
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        public static void InvokeTvDbChanged(EventArgs e)
        {
            EventHandler handler = TvDbChanged;
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
        public static void InvokeUpdateProgressChanged(EventArgs e)
        {
            EventHandler handler = UpdateProgressChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Load episode screenshot
        /// </summary>
        /// <returns>
        /// Episode image
        /// </returns>
        public static Image LoadEpisode()
        {
            if (!string.IsNullOrEmpty(CurrentEpisode.EpisodeScreenshotPath) && File.Exists(CurrentEpisode.EpisodeScreenshotPath))
            {
                return ImageHandler.LoadImage(CurrentEpisode.EpisodeScreenshotPath);
            }

            string url = GetImageUrl(CurrentEpisode.EpisodeScreenshotUrl);
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
        /// Load season banner.
        /// </summary>
        /// <returns>
        /// Season Banner image
        /// </returns>
        public static Image LoadSeasonBanner()
        {
            if (!string.IsNullOrEmpty(CurrentSeason.BannerPath) && File.Exists(CurrentSeason.BannerPath))
            {
                return ImageHandler.LoadImage(CurrentSeason.BannerPath);
            }

            string url = GetImageUrl(CurrentSeason.BannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            Image image = ImageHandler.LoadImage(urlCache);

            if (CurrentSeries.SmallBanner == null)
            {
                CurrentSeries.SmallBanner = ImageHandler.ResizeImage(image, 100, 30);
            }

            return image;
        }

        /// <summary>
        /// Load season fanart.
        /// </summary>
        /// <returns>
        /// Season fanart image
        /// </returns>
        public static Image LoadSeasonFanart()
        {
            if (!string.IsNullOrEmpty(CurrentSeason.FanartPath) && File.Exists(CurrentSeason.FanartPath))
            {
                return ImageHandler.LoadImage(CurrentSeason.FanartPath);
            }

            string url = GetImageUrl(CurrentSeason.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (Downloader.Downloading.Contains(url))
            {
                return null;
            }

            try
            {
                Image image = ImageHandler.LoadImage(urlCache);

                if (CurrentSeries.SmallPoster == null)
                {
                    CurrentSeries.SmallPoster = ImageHandler.ResizeImage(image, 100, 60);
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
        /// The load season poster.
        /// </summary>
        /// <returns>
        /// Season poster image
        /// </returns>
        public static Image LoadSeasonPoster()
        {
            if (!string.IsNullOrEmpty(CurrentSeason.PosterPath) && File.Exists(CurrentSeason.PosterPath))
            {
                return ImageHandler.LoadImage(CurrentSeason.PosterPath);
            }

            string url = GetImageUrl(CurrentSeason.PosterUrl);
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
        /// The load series banner.
        /// </summary>
        /// <returns>
        /// Series banner image
        /// </returns>
        public static Image LoadSeriesBanner()
        {
            if (!string.IsNullOrEmpty(CurrentSeries.SeriesBannerPath) && File.Exists(CurrentSeries.SeriesBannerPath))
            {
                return ImageHandler.LoadImage(CurrentSeries.SeriesBannerPath);
            }

            string url = GetImageUrl(CurrentSeries.SeriesBannerUrl);
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
        /// Load series fanart.
        /// </summary>
        /// <returns>
        /// Series Fanart image
        /// </returns>
        public static Image LoadSeriesFanart()
        {
            if (!string.IsNullOrEmpty(CurrentSeries.FanartPath) && File.Exists(CurrentSeries.FanartPath))
            {
                return ImageHandler.LoadImage(CurrentSeries.FanartPath);
            }

            string url = GetImageUrl(CurrentSeries.FanartUrl);
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

            if (CurrentSeries.SmallFanart == null)
            {
                CurrentSeries.SmallFanart = ImageHandler.ResizeImage(image, 100, 60);
            }

            return image;
        }

        /// <summary>
        /// Load series poster.
        /// </summary>
        /// <returns>
        /// Series Poster image
        /// </returns>
        public static Image LoadSeriesPoster()
        {
            if (!string.IsNullOrEmpty(CurrentSeries.PosterPath) && File.Exists(CurrentSeries.PosterPath))
            {
                return ImageHandler.LoadImage(CurrentSeries.PosterPath);
            }

            string url = GetImageUrl(CurrentSeries.PosterUrl);

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

            if (CurrentSeries.SmallPoster == null)
            {
                CurrentSeries.SmallPoster = ImageHandler.ResizeImage(image, 100, 150);
            }

            return image;
        }

        /// <summary>
        /// The process database update.
        /// </summary>
        public static void ProcessDatabaseUpdate()
        {
            GenerateMasterSeriesList();
            GeneratePictureGallery();
            AddImagesToBackgroundDownload();
            LoadSeriesNFOs();
        }

        /// <summary>
        /// Check if season banner has downloaded
        /// </summary>
        /// <returns>
        /// Return value
        /// </returns>
        public static bool SeasonBannerDownloaded()
        {
            string url = GetImageUrl(CurrentSeason.BannerUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Check if season fanart has downloaded
        /// </summary>
        /// <returns>
        /// Return value
        /// </returns>
        public static bool SeasonFanartDownloaded()
        {
            string url = GetImageUrl(CurrentSeason.FanartUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Check if season poster has downloaded
        /// </summary>
        /// <returns>
        /// Return value
        /// </returns>
        public static bool SeasonPosterDownloaded()
        {
            string url = GetImageUrl(CurrentSeason.PosterUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Check if series poster has downloaded
        /// </summary>
        /// <returns>
        /// Return value
        /// </returns>
        public static bool SeriesPosterDownloaded()
        {
            string url = GetImageUrl(CurrentSeries.PosterUrl);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Set current episode by guid
        /// </summary>
        /// <param name="guid">
        /// The guid value
        /// </param>
        public static void SetCurrentEpisode(string guid)
        {
            Episode episode = (from e in CurrentSeason.Episodes.AsParallel() where e.Guid == guid select e).SingleOrDefault();

            if (episode != null)
            {
                CurrentEpisode = episode;
                InvokeGalleryEpisodeChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Set current season by guid
        /// </summary>
        /// <param name="guid">
        /// The guid value
        /// </param>
        public static void SetCurrentSeason(string guid)
        {
            Season season =
                (from e in CurrentSeries.Seasons.AsParallel() where e.Value.Guid == guid select e.Value).SingleOrDefault();

            if (season != null)
            {
                CurrentSeason = season;
                InvokeCurrentSeasonChanged(new EventArgs());
                InvokeCurrentEpisodeChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Set current series by guid
        /// </summary>
        /// <param name="guid">
        /// The guid value
        /// </param>
        public static void SetCurrentSeries(string guid)
        {
            Series series = GetSeriesFromGuid(guid);

            if (series != null)
            {
                CurrentSeries = series;
                InvokeCurrentSeriesChanged(new EventArgs());
            }
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
        public static void UpdateEpisode(Season season, Episode newEpisode)
        {
            Episode episode =
                (from e in season.Episodes.AsParallel() where e.EpisodeNumber == newEpisode.EpisodeNumber select e).SingleOrDefault();

            if (episode == null)
            {
                season.Episodes.Add(newEpisode);
                return;
            }

            if (!newEpisode.IsLocked && episode.Lastupdated != newEpisode.Lastupdated)
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

                if (string.IsNullOrEmpty(episode.EpisodeScreenshotPath) &&
                    string.IsNullOrEmpty(episode.EpisodeScreenshotUrl))
                {
                    episode.EpisodeScreenshotUrl = newEpisode.EpisodeScreenshotUrl;
                }

                if (string.IsNullOrEmpty(episode.EpisodeScreenshotPath) &&
                    string.IsNullOrEmpty(episode.EpisodeScreenshotUrl))
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
        public static void UpdateEpisodeFile(bool move)
        {
            var fileDialog = new OpenFileDialog();

            string series = CurrentSeries.SeriesName;
            int season = CurrentSeason.SeasonNumber;
            string episode = CurrentEpisode.EpisodeName;

            fileDialog.Title = string.Format("Assign a file to {0} > {1} > {2}", series, season, episode);
            fileDialog.InitialDirectory = CurrentSeason.GetSeasonPath();

            DialogResult dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CurrentEpisode.FilePath.FileNameAndPath = fileDialog.FileName;
                InvokeCurrentEpisodeChanged(new EventArgs());
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
        public static void UpdateSeason(Series series, Season newSeason)
        {
            if (!series.Seasons.ContainsKey(newSeason.SeasonNumber))
            {
                series.Seasons.Add(newSeason.SeasonNumber, newSeason);
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
                    season.BannerUrl = newSeason.FanartUrl;
                }

                if (string.IsNullOrEmpty(season.PosterPath) && string.IsNullOrEmpty(season.PosterUrl))
                {
                    season.BannerUrl = newSeason.PosterUrl;
                }
            }

            foreach (Episode episode in season.Episodes)
            {
                UpdateEpisode(season, episode);
            }
        }

        /// <summary>
        /// Update series from TvDB database.
        /// </summary>
        /// <param name="seriesId">
        /// The series id.
        /// </param>
        public static void UpdateSeries(uint? seriesId)
        {
            Series seriesObj = GetSeriesFromSeriesId(seriesId);

            var tvdb = new TheTvdb();

            Series newSeries = tvdb.CheckForUpdate(seriesObj.SeriesID, seriesObj.Language, seriesObj.Lastupdated);

            if (!seriesObj.IsLocked)
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
                UpdateSeason(seriesObj, season.Value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add images to background download.
        /// </summary>
        private static void AddImagesToBackgroundDownload()
        {
            foreach (var series in tvDatabase)
            {
                UpdateStatus = "Process Images for " + series.Value.SeriesName;

                ProcessSeriesToBackground(series.Value);

                foreach (var season in series.Value.Seasons)
                {
                    ProcessSeasonToBackground(season.Value);

                    foreach (Episode episode in season.Value.Episodes)
                    {
                        ProcessEpisodeToBackground(episode);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the DoWork event of the bgwBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwBanner_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwBanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeBannerLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwEpisode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwEpisode_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwEpisode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwEpisode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeEpisodeLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwFanart_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwFanart_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeFanartLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSeasonBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonBanner_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwSeasonBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonBanner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeSeasonBannerLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSeasonFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonFanart_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwSeasonFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonFanart_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeSeasonFanartLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSeasonPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonPoster_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwSeasonPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeasonPoster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeSeasonPosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSeriesPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeriesPoster_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetImageUrl(e.Argument as string);
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwSeriesPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwSeriesPoster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokeSeriesPosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Gno wet series from series id.
        /// </summary>
        /// <param name="seriesId">
        /// The series id.
        /// </param>
        /// <returns>
        /// Return Series object
        /// </returns>
        private static Series GetSeriesFromSeriesId(uint? seriesId)
        {
            return (from s in tvDatabase.AsParallel() where s.Value.SeriesID == seriesId select s.Value).SingleOrDefault();
        }

        /// <summary>
        /// The load series NFO
        /// </summary>
        private static void LoadSeriesNFOs()
        {
            foreach (var series in tvDatabase)
            {
                UpdateStatus = "Loading Series: " + series.Value.SeriesName;
                OutFactory.LoadSeries(series.Value);
            }
        }

        /// <summary>
        /// The process episode to background.
        /// </summary>
        /// <param name="episode">
        /// The episode.
        /// </param>
        private static void ProcessEpisodeToBackground(Episode episode)
        {
            if (!string.IsNullOrEmpty(episode.CurrentFilenameAndPath))
            {
                var current = GenerateOutput.AccessCurrentIOHandler() as IoInterface;

                if (current == null)
                {
                    return;
                }

                string path = current.GetEpisodeScreenshot(episode);

                if (string.IsNullOrEmpty(path))
                {
                    var downloadItem = new DownloadItem
                        {
                            Url = TvDBFactory.GetImageUrl(episode.EpisodeScreenshotUrl), 
                            Type = DownloadType.Binary, 
                            Section = Section.Tv
                        };

                    Downloader.AddToBackgroundQue(downloadItem);
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
        private static void ProcessSeasonToBackground(Season season)
        {
            if (season.ContainsEpisodesWithFiles())
            {
                var current = GenerateOutput.AccessCurrentIOHandler() as IoInterface;

                if (current == null)
                {
                    return;
                }

                string bannerPath = current.GetSeasonBanner(season);

                if (string.IsNullOrEmpty(bannerPath))
                {
                    var downloadItem = new DownloadItem
                        {
                            Url = TvDBFactory.GetImageUrl(season.BannerUrl), 
                            Type = DownloadType.Binary, 
                            Section = Section.Tv
                        };

                    Downloader.AddToBackgroundQue(downloadItem);
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
                            Url = TvDBFactory.GetImageUrl(season.PosterUrl), 
                            Type = DownloadType.Binary, 
                            Section = Section.Tv
                        };

                    Downloader.AddToBackgroundQue(downloadItem);
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
                            Url = TvDBFactory.GetImageUrl(season.FanartUrl), 
                            Type = DownloadType.Binary, 
                            Section = Section.Tv
                        };

                    Downloader.AddToBackgroundQue(downloadItem);
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
        private static void ProcessSeriesToBackground(Series series)
        {
            var current = GenerateOutput.AccessCurrentIOHandler() as IoInterface;

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

        /// <summary>
        /// Show the "Add Custom Series" dialog.
        /// </summary>
        public static void CreateCustomSeries()
        {
            var addCustomSeries = new FrmAddCustomSeries();
            addCustomSeries.ShowDialog();
        }

        public static void AddCustomSeries(Series series)
        {
            tvDatabase.Add(series.SeriesName, series);
            GenerateMasterSeriesList();
            InvokeMasterSeriesNameListChanged(new EventArgs());
        }
    }
}