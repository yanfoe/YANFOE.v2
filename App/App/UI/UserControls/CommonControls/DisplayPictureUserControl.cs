// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayPictureUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.Properties;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.UI.Dialogs.General;

    /// <summary>
    /// The display picture user control.
    /// </summary>
    public partial class DisplayPictureUserControl : XtraUserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The gallery type.
        /// </summary>
        private GalleryType galleryType;

        /// <summary>
        /// The header details.
        /// </summary>
        private string headerDetails;

        /// <summary>
        /// The header title.
        /// </summary>
        private string headerTitle;

        /// <summary>
        /// Enable/Disable populateGallery routines.
        /// </summary>
        private bool populateGallery;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayPictureUserControl"/> class.
        /// </summary>
        public DisplayPictureUserControl()
        {
            this.InitializeComponent();

            MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;
            TvDBFactory.CurrentSeriesChanged += this.TvDBFactory_CurrentSeriesChanged;
            TvDBFactory.CurrentSeasonChanged += this.TvDBFactory_CurrentSeasonChanged;
            TvDBFactory.CurrentEpisodeChanged += this.TvDBFactory_CurrentEpisodeChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the plain text at the top of the picture control
        /// </summary>
        /// <value>The header details text</value>
        public string HeaderDetails
        {
            get
            {
                return this.headerDetails;
            }

            set
            {
                this.headerDetails = value;
                this.lblPicTitle.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the bold text at the top of the picture control
        /// </summary>
        /// <value>The header title text</value>
        public string HeaderTitle
        {
            get
            {
                return this.headerTitle;
            }

            set
            {
                this.headerTitle = value;
                this.lblPicAreaName.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the picture control
        /// </summary>
        /// <value>The GalleryType type.</value>
        public GalleryType Type
        {
            get
            {
                return this.galleryType;
            }

            set
            {
                this.galleryType = value;

                this.UpdateLayout();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Images the loading.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageLoading(object sender, EventArgs e)
        {
            this.StartLoading();
            this.imageMain.Image = Resources.LoadingGlobe;
        }

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            if (galleryType == GalleryType.MoviePoster || galleryType == GalleryType.MovieFanart)
            {
                this.populateGallery = true;
                this.SetMovieBinding();
            }
        }

        /// <summary>
        /// Handles the FanartLoaded event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_FanartLoaded(object sender, EventArgs e)
        {
            var image = MovieDBFactory.LoadFanart();

            if (image == null)
            {
                this.populateGallery = false;
                this.StopLoading();
                this.imageMain.Image = image;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the PosterLoaded event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_PosterLoaded(object sender, EventArgs e)
        {
            Image image = MovieDBFactory.LoadPoster();

            if (image == null)
            {
                this.populateGallery = false;
                this.StopLoading();
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Processes the episode screenshot download.
        /// </summary>
        private void ProcessEpisodeScreenshotDownload()
        {
            if (!string.IsNullOrEmpty(TvDBFactory.CurrentEpisode.EpisodeScreenshotPath))
            {
                imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentEpisode.EpisodeScreenshotPath);

                return;
            }

            if (Downloader.Downloading.Contains(TvDBFactory.CurrentEpisode.EpisodeScreenshotUrl))
            {
                return;
            }

            TvDBFactory.EpisodeLoading += this.ImageLoading;
            TvDBFactory.EpisodeLoaded += this.TvDBFactory_EpisodeLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentEpisode.EpisodeScreenshotUrl))
            {
                TvDBFactory.GetEpisode();
            }
            else
            {
                this.ShowNoImage();
            }
        }

        /// <summary>
        /// Processes the movie fanart download.
        /// </summary>
        private void ProcessMovieFanartDownload()
        {
            if (Downloader.Downloading.Contains(MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl))
            {
                return;
            }

            MovieDBFactory.FanartLoading += this.ImageLoading;
            MovieDBFactory.FanartLoaded += this.MovieDBFactory_FanartLoaded;

            if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl))
            {
                MovieDBFactory.GetFanart();
            }
        }

        /// <summary>
        /// Processes the movie poster download.
        /// </summary>
        private void ProcessMoviePosterDownload()
        {
            if (Downloader.Downloading.Contains(MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl))
            {
                return;
            }

            MovieDBFactory.PosterLoading += this.ImageLoading;
            MovieDBFactory.PosterLoaded += this.MovieDBFactory_PosterLoaded;

            if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl))
            {
                MovieDBFactory.GetPoster();
            }
        }

        /// <summary>
        /// Processes the season banner download.
        /// </summary>
        private void ProcessSeasonBannerDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeason.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeasonBannerLoading += this.ImageLoading;
            TvDBFactory.SeasonBannerLoaded += this.TvDBFactory_SeasonBannerLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.BannerUrl))
            {
                TvDBFactory.GetSeasonBanner();
            }
        }

        /// <summary>
        /// Processes the season fanart download.
        /// </summary>
        private void ProcessSeasonFanartDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeason.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeasonFanartLoading += this.ImageLoading;
            TvDBFactory.SeasonFanartLoaded += this.TvDBFactory_SeasonFanartLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.FanartUrl))
            {
                TvDBFactory.GetSeasonFanart();
            }
        }

        /// <summary>
        /// Processes the season poster download.
        /// </summary>
        private void ProcessSeasonPosterDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeason.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeasonPosterLoading += this.ImageLoading;
            TvDBFactory.SeasonPosterLoaded += this.TvDBFactory_SeasonPosterLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.PosterUrl))
            {
                TvDBFactory.GetSeasonPoster();
            }
        }

        /// <summary>
        /// Processes the series banner download.
        /// </summary>
        private void ProcessSeriesBannerDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeries.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeriesBannerLoading += this.ImageLoading;
            TvDBFactory.SeriesBannerLoaded += this.TvDbFactory_SeriesBannerLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.SeriesBannerUrl))
            {
                TvDBFactory.GetSeriesBanner();
            }
        }

        /// <summary>
        /// Processes the series fanart download.
        /// </summary>
        private void ProcessSeriesFanartDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeries.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeriesFanartLoading += this.ImageLoading;
            TvDBFactory.SeriesFanartLoaded += this.TvDbFactory_SeriesFanartLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.FanartUrl))
            {
                TvDBFactory.GetSeriesFanart();
            }
        }

        /// <summary>
        /// Processes the series poster download.
        /// </summary>
        private void ProcessSeriesPosterDownload()
        {
            if (Downloader.Downloading.Contains(TvDBFactory.CurrentSeries.PosterUrl))
            {
                return;
            }

            TvDBFactory.SeriesPosterLoading += this.ImageLoading;
            TvDBFactory.SeriesPosterLoaded += this.TvDbFactory_SeriesPosterLoaded;

            if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.PosterUrl))
            {
                TvDBFactory.GetSeriesPoster();
            }
        }

        /// <summary>
        /// The set movie binding.
        /// </summary>
        private void SetMovieBinding()
        {
            this.imageMain.DataBindings.Clear();
            this.imageMain.Image = Resources.picturefaded128;

            layoutControl2.DataBindings.Clear();
            layoutControl2.DataBindings.Add("Enabled", MovieDBFactory.GetCurrentMovie(), "Unlocked");

            hideContainerLeft.DataBindings.Clear();
            hideContainerLeft.DataBindings.Add("Enabled", MovieDBFactory.GetCurrentMovie(), "Unlocked");

            switch (this.galleryType)
            {
                case GalleryType.MoviePoster:

                    if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().PosterPathOnDisk))
                    {
                        this.MovieDBFactory_PosterLoaded(null, null);
                    }
                    else
                    {
                        this.ProcessMoviePosterDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.ImageSize = new Size(100, 160);
                        this.galleryControl.Gallery.Groups.Add(MovieDBFactory.GetCurrentMovie().PosterAltGallery);
                        this.populateGallery = false;
                    }

                    break;

                case GalleryType.MovieFanart:

                    if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().FanartPathOnDisk))
                    {
                        this.MovieDBFactory_FanartLoaded(null, null);
                    }
                    else
                    {
                        this.ProcessMovieFanartDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(MovieDBFactory.GetCurrentMovie().FanartAltGallery);
                        this.populateGallery = false;
                        this.galleryControl.Gallery.ImageSize = new Size(100, 60);
                    }

                    

                    break;
            }
        }

        /// <summary>
        /// The set tv episode binding.
        /// </summary>
        private void SetTvEpisodeBinding()
        {
            if (this.galleryType != GalleryType.TvEpisodeScreenshot)
            {
                return;
            }

            this.imageMain.DataBindings.Clear();
            this.imageMain.Image = Resources.picturefaded128;

            layoutControl2.DataBindings.Clear();
            layoutControl2.DataBindings.Add("Enabled", TvDBFactory.CurrentEpisode, "NotLocked");

            hideContainerLeft.DataBindings.Clear();
            hideContainerLeft.DataBindings.Add("Enabled", TvDBFactory.CurrentEpisode, "NotLocked");

            this.ProcessEpisodeScreenshotDownload();
        }

        /// <summary>
        /// Sets tv season binding.
        /// </summary>
        private void SetTvSeasonBinding()
        {
            if (((this.galleryType != GalleryType.TvSeasonBanner) && (this.galleryType != GalleryType.TvSeasonFanart)) &&
                (this.galleryType != GalleryType.TvSeasonPoster))
            {
                return;
            }

            this.imageMain.DataBindings.Clear();
            this.imageMain.Image = Resources.picturefaded128;

            layoutControl2.DataBindings.Clear();
            layoutControl2.DataBindings.Add("Enabled", TvDBFactory.CurrentSeason, "NotLocked");

            hideContainerLeft.DataBindings.Clear();
            hideContainerLeft.DataBindings.Add("Enabled", TvDBFactory.CurrentSeason, "NotLocked");

            switch (this.galleryType)
            {
                case GalleryType.TvSeasonPoster:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.PosterPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeason.PosterPath);
                    }
                    else
                    {
                        this.ProcessSeasonPosterDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailPoster;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeason.SeasonPosterAltGallery);
                        this.populateGallery = false;
                    }

                    break;

                case GalleryType.TvSeasonFanart:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.FanartPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeason.FanartPath);
                    }
                    else
                    {
                        this.ProcessSeasonFanartDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailFanart;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeason.SeasonFanartAltGallery);
                        this.populateGallery = false;
                    }

                    break;

                case GalleryType.TvSeasonBanner:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeason.BannerPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeason.BannerPath);
                    }
                    else
                    {
                        this.ProcessSeasonBannerDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailBanner;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeason.SeasonBannerAltGallery);
                        this.populateGallery = false;
                    }

                    break;
            }
        }

        /// <summary>
        /// Sets tv series binding.
        /// </summary>
        private void SetTvSeriesBinding()
        {
            if (((this.galleryType != GalleryType.TvSeriesBanner) && (this.galleryType != GalleryType.TvSeriesFanart)) &&
                (this.galleryType != GalleryType.TvSeriesPoster))
            {
                return;
            }

            this.imageMain.DataBindings.Clear();
            this.imageMain.Image = Resources.picturefaded128;

            layoutControl2.DataBindings.Clear();
            layoutControl2.DataBindings.Add("Enabled", TvDBFactory.CurrentSeries, "NotLocked");

            hideContainerLeft.DataBindings.Clear();
            hideContainerLeft.DataBindings.Add("Enabled", TvDBFactory.CurrentSeries, "NotLocked");

            switch (this.galleryType)
            {
                case GalleryType.TvSeriesPoster:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.PosterPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeries.PosterPath);
                    }
                    else
                    {
                        this.ProcessSeriesPosterDownload();
                    }
                    
                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailPoster;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeries.SeriesPosterAltGallery);
                        this.populateGallery = false;
                    }

                    break;

                case GalleryType.TvSeriesFanart:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.FanartPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeries.FanartPath);
                    }
                    else
                    {
                        this.ProcessSeriesFanartDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailFanart;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeries.SeriesFanartAltGallery);
                        this.populateGallery = false;
                    }

                    break;

                case GalleryType.TvSeriesBanner:

                    if (!string.IsNullOrEmpty(TvDBFactory.CurrentSeries.SeriesBannerPath))
                    {
                        imageMain.Image = ImageHandler.LoadImage(TvDBFactory.CurrentSeries.SeriesBannerPath);
                    }
                    else
                    {
                        this.ProcessSeriesBannerDownload();
                    }

                    if (this.populateGallery)
                    {
                        this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailBanner;
                        this.galleryControl.Gallery.Groups.Clear();
                        this.galleryControl.Gallery.Groups.Add(TvDBFactory.CurrentSeries.SeriesBannerAltGallery);
                        this.populateGallery = false;
                    }

                    break;
            }
        }

        /// <summary>
        /// Show "no image" image.
        /// </summary>
        private void ShowNoImage()
        {
            this.imageMain.Image = Resources.picturefaded128;
        }

        /// <summary>
        /// Starts loading animation.
        /// </summary>
        private void StartLoading()
        {
            this.imageMain.StartAnimation();
        }

        /// <summary>
        /// Stops loading animation.
        /// </summary>
        private void StopLoading()
        {
            this.imageMain.StopAnimation();
        }

        /// <summary>
        /// Handles the CurrentEpisodeChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void TvDBFactory_CurrentEpisodeChanged(object sender, EventArgs e)
        {
            
            this.SetTvEpisodeBinding();
            this.populateGallery = true;
        }

        /// <summary>
        /// Handles the CurrentSeasonChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void TvDBFactory_CurrentSeasonChanged(object sender, EventArgs e)
        {
            if (this.galleryType == GalleryType.TvSeasonBanner || this.galleryType == GalleryType.TvSeasonFanart || this.galleryType == GalleryType.TvSeasonPoster)
            {
                this.SetTvSeasonBinding();
                this.populateGallery = true;
            }
        }

        /// <summary>
        /// Handles the CurrentSeriesChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_CurrentSeriesChanged(object sender, EventArgs e)
        {
            if (this.galleryType == GalleryType.TvSeriesBanner || this.galleryType == GalleryType.TvSeriesFanart || this.galleryType == GalleryType.TvSeriesPoster)
            {
                this.SetTvSeriesBinding();
                this.populateGallery = true;
            }
        }

        /// <summary>
        /// Handles the EpisodeLoaded event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_EpisodeLoaded(object sender, EventArgs e)
        {
            Image image = TvDBFactory.LoadEpisode();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonBannerLoaded event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_SeasonBannerLoaded(object sender, EventArgs e)
        {
            Image image = TvDBFactory.LoadSeasonBanner();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonFanartLoaded event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_SeasonFanartLoaded(object sender, EventArgs e)
        {
            Image image = TvDBFactory.LoadSeasonFanart();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonPosterLoaded event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_SeasonPosterLoaded(object sender, EventArgs e)
        {
            Image image = TvDBFactory.LoadSeasonPoster();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesBannerLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDbFactory_SeriesBannerLoaded(object sender, EventArgs e)
        {
            Image image = TvDBFactory.LoadSeriesBanner();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesFanartLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDbFactory_SeriesFanartLoaded(object sender, EventArgs e)
        {
            var image = TvDBFactory.LoadSeriesFanart();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesPosterLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDbFactory_SeriesPosterLoaded(object sender, EventArgs e)
        {
            var image = TvDBFactory.LoadSeriesPoster();

            if (image == null)
            {
                this.populateGallery = false;
                return;
            }

            this.StopLoading();
            this.imageMain.Image = image;
        }

        /// <summary>
        /// The update layout.
        /// </summary>
        private void UpdateLayout()
        {
            switch (this.galleryType)
            {
                case GalleryType.MoviePoster:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
                    break;
                case GalleryType.MovieFanart:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
                    break;
                case GalleryType.TvSeriesFanart:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
                    break;
                case GalleryType.TvSeriesPoster:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
                    break;
                case GalleryType.TvSeriesBanner:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Banner;
                    break;
                case GalleryType.TvSeasonFanart:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
                    break;
                case GalleryType.TvSeasonPoster:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
                    break;
                case GalleryType.TvSeasonBanner:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Banner;
                    break;
                case GalleryType.TvEpisodeScreenshot:
                    this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Screenshots;
                    break;
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnGetImageFromDisk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnGetImageFromDisk_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dialog = new OpenFileDialog { Filter = @"Jpg (*.jpg)|*.jpg|All Files (*.*)|*.*" };

            const string Title = "Select {0} image from disk";

            switch (this.galleryType)
            {
                case GalleryType.MovieFanart:
                    dialog.Title = string.Format(Title, "movie fanart");
                    break;
                case GalleryType.MoviePoster:
                    dialog.Title = string.Format(Title, "movie poster");
                    break;
                case GalleryType.TvSeriesBanner:
                    dialog.Title = string.Format(Title, "tv series banner");
                    break;
                case GalleryType.TvSeriesPoster:
                    dialog.Title = string.Format(Title, "tv series poster");
                    break;
                case GalleryType.TvSeriesFanart:
                    dialog.Title = string.Format(Title, "tv series fanart");
                    break;
                case GalleryType.TvSeasonPoster:
                    dialog.Title = string.Format(Title, "tv season poster");
                    break;
                case GalleryType.TvSeasonFanart:
                    dialog.Title = string.Format(Title, "tv season fanart");
                    break;
                case GalleryType.TvSeasonBanner:
                    dialog.Title = string.Format(Title, "tv series banner");
                    break;
                case GalleryType.TvEpisodeScreenshot:
                    dialog.Title = string.Format(Title, "tv episode screenshot");
                    break;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.UpdateImagePath(dialog.FileName);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the btnGetImageFromUrl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnGetImageFromUrl_ItemClick(object sender, ItemClickEventArgs e)
        {
            var enterUrl = new FrmEnterAValue("Enter a URL");
            enterUrl.ShowDialog();

            if (!string.IsNullOrEmpty(enterUrl.Response))
            {
                switch (this.galleryType)
                {
                    case GalleryType.MovieFanart:
                        MovieDBFactory.GetCurrentMovie().FanartPathOnDisk = string.Empty;
                        MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl = enterUrl.Response;
                        this.ProcessMovieFanartDownload();
                        break;
                    case GalleryType.MoviePoster:
                        MovieDBFactory.GetCurrentMovie().PosterPathOnDisk = string.Empty;
                        MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl = enterUrl.Response;
                        this.ProcessMoviePosterDownload();
                        break;
                    case GalleryType.TvSeriesBanner:
                        TvDBFactory.CurrentSeries.SeriesBannerPath = string.Empty;
                        TvDBFactory.CurrentSeries.SeriesBannerUrl = enterUrl.Response;
                        this.ProcessSeriesBannerDownload();
                        break;
                    case GalleryType.TvSeriesPoster:
                        TvDBFactory.CurrentSeries.PosterPath = string.Empty;
                        TvDBFactory.CurrentSeries.PosterUrl = enterUrl.Response;
                        this.ProcessSeriesPosterDownload();
                        break;
                    case GalleryType.TvSeriesFanart:
                        TvDBFactory.CurrentSeries.FanartPath = string.Empty;
                        TvDBFactory.CurrentSeries.FanartUrl = enterUrl.Response;
                        this.ProcessSeriesFanartDownload();
                        break;
                    case GalleryType.TvSeasonBanner:
                        TvDBFactory.CurrentSeason.BannerPath = string.Empty;
                        TvDBFactory.CurrentSeason.BannerUrl = enterUrl.Response;
                        this.ProcessSeasonBannerDownload();
                        break;
                    case GalleryType.TvSeasonFanart:
                        TvDBFactory.CurrentSeason.FanartPath = string.Empty;
                        TvDBFactory.CurrentSeason.FanartUrl = enterUrl.Response;
                        this.ProcessSeasonFanartDownload();
                        break;
                    case GalleryType.TvSeasonPoster:
                        TvDBFactory.CurrentSeason.PosterPath = string.Empty;
                        TvDBFactory.CurrentSeason.PosterUrl = enterUrl.Response;
                        this.ProcessSeasonPosterDownload();
                        break;
                    case GalleryType.TvEpisodeScreenshot:
                        TvDBFactory.CurrentEpisode.EpisodeScreenshotPath = string.Empty;
                        TvDBFactory.CurrentEpisode.EpisodeScreenshotUrl = enterUrl.Response;
                        this.ProcessEpisodeScreenshotDownload();
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the galleryControlGallery1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs"/> instance containing the event data.</param>
        private void GalleryControlGallery1_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            var type = e.Item.Tag.ToString().Split('|')[0];
            var url = e.Item.Tag.ToString().Split('|')[1];

            switch (type)
            {
                case "moviePoster":
                    MovieDBFactory.GetCurrentMovie().PosterPathOnDisk = string.Empty;
                    MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl = url;
                    this.SetMovieBinding();
                    break;

                case "movieFanart":
                    MovieDBFactory.GetCurrentMovie().FanartPathOnDisk = string.Empty;
                    MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl = url;
                    this.SetMovieBinding();
                    break;

                case "tvSeriesFanart":
                    TvDBFactory.CurrentSeries.FanartPath = string.Empty;
                    TvDBFactory.CurrentSeries.FanartUrl = url;
                    this.SetTvSeriesBinding();
                    break;

                case "tvSeriesPoster":
                    TvDBFactory.CurrentSeries.PosterPath = string.Empty;
                    TvDBFactory.CurrentSeries.PosterUrl = url;
                    this.SetTvSeriesBinding();
                    break;

                case "tvSeriesBanner":
                    TvDBFactory.CurrentSeries.SeriesBannerPath = string.Empty;
                    TvDBFactory.CurrentSeries.SeriesBannerUrl = url;
                    this.SetTvSeriesBinding();
                    break;

                case "tvSeasonFanart":
                    TvDBFactory.CurrentSeason.FanartPath = string.Empty;
                    TvDBFactory.CurrentSeason.FanartUrl = url;
                    this.SetTvSeasonBinding();
                    break;

                case "tvSeasonPoster":
                    TvDBFactory.CurrentSeason.PosterPath = string.Empty;
                    TvDBFactory.CurrentSeason.PosterUrl = url;
                    this.SetTvSeasonBinding();
                    break;

                case "tvSeasonBanner":
                    TvDBFactory.CurrentSeason.BannerPath = string.Empty;
                    TvDBFactory.CurrentSeason.BannerUrl = url;
                    this.SetTvSeasonBinding();
                    break;
            }
        }

        /// <summary>
        /// Handles the ImageChanged event of the imageMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ImageMain_ImageChanged(object sender, EventArgs e)
        {
            if (this.imageMain != null && this.imageMain.Image != null)
            {
                this.lblPicTitle.Text = string.Format(
                    "{0}x{1}", this.imageMain.Image.Width, this.imageMain.Image.Height);
            }
        }

        #endregion

        /// <summary>
        /// Handles the DragDrop event of the imageMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void imageMain_DragDrop(object sender, DragEventArgs e)
        {
            var fileNameW = e.Data.GetData("FileNameW");

            if (fileNameW != null)
            {
                string[] fileNames = (string[])fileNameW;
                if (fileNames.Length == 1)
                {
                    string fileName = fileNames[0];

                    if (Get.InOutCollection.ImageExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                    {
                        this.UpdateImagePath(fileName);
                    }
                }
            }
        }

        private void UpdateImagePath(string fileName)
        {
            switch (this.galleryType)
            {
                case GalleryType.MoviePoster:
                    MovieDBFactory.GetCurrentMovie().PosterPathOnDisk = fileName;
                    MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl = string.Empty;
                    this.MovieDBFactory_PosterLoaded(this, new EventArgs());
                    break;

                case GalleryType.MovieFanart:
                    MovieDBFactory.GetCurrentMovie().FanartPathOnDisk = fileName;
                    MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl = string.Empty;
                    this.MovieDBFactory_FanartLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvSeriesPoster:
                    TvDBFactory.CurrentSeries.PosterPath = fileName;
                    TvDBFactory.CurrentSeries.PosterUrl = string.Empty;
                    this.TvDbFactory_SeriesPosterLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvSeriesFanart:
                    TvDBFactory.CurrentSeries.FanartPath = fileName;
                    TvDBFactory.CurrentSeries.FanartUrl = string.Empty;
                    this.TvDbFactory_SeriesFanartLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvSeriesBanner:
                    TvDBFactory.CurrentSeries.SeriesBannerPath = fileName;
                    TvDBFactory.CurrentSeries.SeriesBannerUrl = string.Empty;
                    this.TvDbFactory_SeriesFanartLoaded(this, new EventArgs());
                    break;
          
                case GalleryType.TvSeasonPoster:
                    TvDBFactory.CurrentSeason.PosterPath = fileName;
                    TvDBFactory.CurrentSeason.PosterUrl = string.Empty;
                    this.TvDBFactory_SeasonPosterLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvSeasonFanart:
                    TvDBFactory.CurrentSeason.FanartPath = fileName;
                    TvDBFactory.CurrentSeason.FanartUrl = string.Empty;
                    this.TvDBFactory_SeasonFanartLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvSeasonBanner:
                    TvDBFactory.CurrentSeason.BannerPath = fileName;
                    TvDBFactory.CurrentSeason.BannerUrl = string.Empty;
                    this.TvDBFactory_SeasonBannerLoaded(this, new EventArgs());
                    break;

                case GalleryType.TvEpisodeScreenshot:
                    TvDBFactory.CurrentEpisode.EpisodeScreenshotPath = fileName;
                    TvDBFactory.CurrentEpisode.EpisodeScreenshotUrl = string.Empty;
                    this.TvDBFactory_SeasonBannerLoaded(this, new EventArgs());
                    break;
            }
        }

        /// <summary>
        /// Handles the DragOver event of the imageMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void imageMain_DragOver(object sender, DragEventArgs e)
        {
            object fileNameW = e.Data.GetData("FileNameW");
            if (fileNameW != null)
            {
                string[] fileNames = (string[])fileNameW;
                if (fileNames.Length == 1)
                {
                    string fileName = fileNames[0];

                    if (Get.InOutCollection.ImageExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }
    }
}