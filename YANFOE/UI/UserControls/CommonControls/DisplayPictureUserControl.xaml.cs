// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="DisplayPictureUserControl.xaml.cs">
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
//   Interaction logic for DisplayPictureUserControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.UserControls.CommonControls
{
    #region Required Namespaces

    using System;
    using System.Windows;
    using System.Windows.Controls;

    using YANFOE.Factories;
    using YANFOE.Tools.Enums;

    using Image = System.Drawing.Image;

    #endregion

    /// <summary>
    ///   Interaction logic for DisplayPictureUserControl.xaml
    /// </summary>
    public partial class DisplayPictureUserControl : UserControl
    {
        #region Static Fields

        /// <summary>
        /// The gallery images property.
        /// </summary>
        public static readonly DependencyProperty GalleryImagesProperty = DependencyProperty.Register(
            "GalleryImages", typeof(GalleryItemGroup), typeof(DisplayPictureUserControl));

        /// <summary>
        /// The selected image property.
        /// </summary>
        public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register(
            "SelectedImage", typeof(Image), typeof(DisplayPictureUserControl));

        #endregion

        #region Fields

        /// <summary>
        ///   The gallery type.
        /// </summary>
        private GalleryType galleryType;

        /// <summary>
        ///   The header details.
        /// </summary>
        private string headerDetails;

        /// <summary>
        ///   The header title.
        /// </summary>
        private string headerTitle;

        /// <summary>
        ///   Enable/Disable populateGallery routines.
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

            MovieDBFactory.Instance.CurrentMovieChanged += (sender, e) =>
                {
                    if (this.galleryType == GalleryType.MoviePoster || this.galleryType == GalleryType.MovieFanart)
                    {
                        this.populateGallery = true;
                        this.SetMovieBinding();
                    }
                };

            TVDBFactory.Instance.CurrentSeriesChanged += this.TvDBFactory_CurrentSeriesChanged;
            TVDBFactory.Instance.CurrentSeasonChanged += this.TvDBFactory_CurrentSeasonChanged;
            TVDBFactory.Instance.CurrentEpisodeChanged += this.TvDBFactory_CurrentEpisodeChanged;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the gallery images.
        /// </summary>
        public GalleryItemGroup GalleryImages
        {
            get
            {
                return (GalleryItemGroup)this.GetValue(GalleryImagesProperty);
            }

            set
            {
                this.SetValue(GalleryImagesProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the plain text at the top of the picture control
        /// </summary>
        /// <value> The header details text </value>
        public string HeaderDetails
        {
            get
            {
                return this.headerDetails;
            }

            set
            {
                this.headerDetails = value;
                this.lblPicTitle.Content = value;
            }
        }

        /// <summary>
        ///   Gets or sets the bold text at the top of the picture control
        /// </summary>
        /// <value> The header title text </value>
        public string HeaderTitle
        {
            get
            {
                return this.headerTitle;
            }

            set
            {
                this.headerTitle = value;
                this.lblPicAreaName.Content = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected image.
        /// </summary>
        public Image SelectedImage
        {
            get
            {
                return (Image)this.GetValue(SelectedImageProperty);
            }

            set
            {
                this.SetValue(SelectedImageProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the type of the picture control
        /// </summary>
        /// <value> The GalleryType type. </value>
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
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void ImageLoading(object sender, EventArgs e)
        {
            this.StartLoading();

            // this.imageMain.Image = Resources.LoadingGlobe;
        }

        /// <summary>
        /// Handles the FanartLoaded event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void MovieDBFactory_FanartLoaded(object sender, EventArgs e)
        {
            // var image = MovieDBFactory.Instance.LoadFanart();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // this.StopLoading();
            // //this.imageMain.Image = image;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the PosterLoaded event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void MovieDBFactory_PosterLoaded(object sender, EventArgs e)
        {
            // Image image = MovieDBFactory.LoadPoster();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // this.StopLoading();
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        ///   Processes the episode screenshot download.
        /// </summary>
        private void ProcessEpisodeScreenshotDownload()
        {
            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentEpisode.EpisodeScreenshotPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentEpisode.EpisodeScreenshotPath);

            // return;
            // }

            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentEpisode.EpisodeScreenshotUrl))
            // {
            // return;
            // }

            // TVDBFactory.EpisodeLoading += this.ImageLoading;
            // TVDBFactory.EpisodeLoaded += this.TvDBFactory_EpisodeLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentEpisode.EpisodeScreenshotUrl))
            // {
            // TVDBFactory.GetEpisode();
            // }
            // else
            // {
            // this.ShowNoImage();
            // }
        }

        /// <summary>
        ///   Processes the movie fanart download.
        /// </summary>
        private void ProcessMovieFanartDownload()
        {
            // if (Downloader.Downloading.Contains(MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl))
            // {
            // return;
            // }

            // MovieDBFactory.FanartLoading += this.ImageLoading;
            // MovieDBFactory.FanartLoaded += this.MovieDBFactory_FanartLoaded;

            // if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().CurrentFanartImageUrl))
            // {
            // MovieDBFactory.GetFanart();
            // }
        }

        /// <summary>
        ///   Processes the movie poster download.
        /// </summary>
        private void ProcessMoviePosterDownload()
        {
            // if (Downloader.Downloading.Contains(MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl))
            // {
            // return;
            // }

            // MovieDBFactory.PosterLoading += this.ImageLoading;
            // MovieDBFactory.PosterLoaded += this.MovieDBFactory_PosterLoaded;

            // if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().CurrentPosterImageUrl))
            // {
            // MovieDBFactory.GetPoster();
            // }
        }

        /// <summary>
        ///   Processes the season banner download.
        /// </summary>
        private void ProcessSeasonBannerDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeason.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeasonBannerLoading += this.ImageLoading;
            // TVDBFactory.SeasonBannerLoaded += this.TvDBFactory_SeasonBannerLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.BannerUrl))
            // {
            // TVDBFactory.GetSeasonBanner();
            // }
        }

        /// <summary>
        ///   Processes the season fanart download.
        /// </summary>
        private void ProcessSeasonFanartDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeason.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeasonFanartLoading += this.ImageLoading;
            // TVDBFactory.SeasonFanartLoaded += this.TvDBFactory_SeasonFanartLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.FanartUrl))
            // {
            // TVDBFactory.GetSeasonFanart();
            // }
        }

        /// <summary>
        ///   Processes the season poster download.
        /// </summary>
        private void ProcessSeasonPosterDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeason.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeasonPosterLoading += this.ImageLoading;
            // TVDBFactory.SeasonPosterLoaded += this.TvDBFactory_SeasonPosterLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.PosterUrl))
            // {
            // TVDBFactory.GetSeasonPoster();
            // }
        }

        /// <summary>
        ///   Processes the series banner download.
        /// </summary>
        private void ProcessSeriesBannerDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeries.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeriesBannerLoading += this.ImageLoading;
            // TVDBFactory.SeriesBannerLoaded += this.TvDbFactory_SeriesBannerLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.SeriesBannerUrl))
            // {
            // TVDBFactory.GetSeriesBanner();
            // }
        }

        /// <summary>
        ///   Processes the series fanart download.
        /// </summary>
        private void ProcessSeriesFanartDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeries.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeriesFanartLoading += this.ImageLoading;
            // TVDBFactory.SeriesFanartLoaded += this.TvDbFactory_SeriesFanartLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.FanartUrl))
            // {
            // TVDBFactory.GetSeriesFanart();
            // }
        }

        /// <summary>
        ///   Processes the series poster download.
        /// </summary>
        private void ProcessSeriesPosterDownload()
        {
            // if (Downloader.Downloading.Contains(TVDBFactory.CurrentSeries.PosterUrl))
            // {
            // return;
            // }

            // TVDBFactory.SeriesPosterLoading += this.ImageLoading;
            // TVDBFactory.SeriesPosterLoaded += this.TvDbFactory_SeriesPosterLoaded;

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.PosterUrl))
            // {
            // TVDBFactory.GetSeriesPoster();
            // }
        }

        /// <summary>
        ///   The set movie binding.
        /// </summary>
        private void SetMovieBinding()
        {
            // this.imageMain.DataBindings.Clear();
            // this.imageMain.Image = Resources.picturefaded128;

            // layoutControl2.DataBindings.Clear();
            // layoutControl2.DataBindings.Add("Enabled", MovieDBFactory.GetCurrentMovie(), "Unlocked");

            // hideContainerLeft.DataBindings.Clear();
            // hideContainerLeft.DataBindings.Add("Enabled", MovieDBFactory.GetCurrentMovie(), "Unlocked");

            // switch (this.galleryType)
            // {
            // case GalleryType.MoviePoster:

            // if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().PosterPathOnDisk))
            // {
            // this.MovieDBFactory_PosterLoaded(null, null);
            // }
            // else
            // {
            // this.ProcessMoviePosterDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.ImageSize = new Size(100, 160);
            // this.galleryControl.Gallery.Groups.Add(MovieDBFactory.GetCurrentMovie().PosterAltGallery);
            // this.populateGallery = false;
            // }

            // break;

            // case GalleryType.MovieFanart:

            // if (!string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().FanartPathOnDisk))
            // {
            // this.MovieDBFactory_FanartLoaded(null, null);
            // }
            // else
            // {
            // this.ProcessMovieFanartDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(MovieDBFactory.GetCurrentMovie().FanartAltGallery);
            // this.populateGallery = false;
            // this.galleryControl.Gallery.ImageSize = new Size(100, 60);
            // }

            // break;
            // }
        }

        /// <summary>
        ///   The set tv episode binding.
        /// </summary>
        private void SetTvEpisodeBinding()
        {
            // if (this.galleryType != GalleryType.TvEpisodeScreenshot)
            // {
            // return;
            // }

            // this.imageMain.DataBindings.Clear();
            // this.imageMain.Image = Resources.picturefaded128;

            // layoutControl2.DataBindings.Clear();
            // layoutControl2.DataBindings.Add("Enabled", TVDBFactory.CurrentEpisode, "NotLocked");

            // hideContainerLeft.DataBindings.Clear();
            // hideContainerLeft.DataBindings.Add("Enabled", TVDBFactory.CurrentEpisode, "NotLocked");

            // this.ProcessEpisodeScreenshotDownload();
        }

        /// <summary>
        ///   Sets tv season binding.
        /// </summary>
        private void SetTvSeasonBinding()
        {
            // if (((this.galleryType != GalleryType.TvSeasonBanner) && (this.galleryType != GalleryType.TvSeasonFanart)) &&
            // (this.galleryType != GalleryType.TvSeasonPoster))
            // {
            // return;
            // }

            // this.imageMain.DataBindings.Clear();
            // this.imageMain.Image = Resources.picturefaded128;

            // layoutControl2.DataBindings.Clear();
            // layoutControl2.DataBindings.Add("Enabled", TVDBFactory.CurrentSeason, "NotLocked");

            // hideContainerLeft.DataBindings.Clear();
            // hideContainerLeft.DataBindings.Add("Enabled", TVDBFactory.CurrentSeason, "NotLocked");

            // switch (this.galleryType)
            // {
            // case GalleryType.TvSeasonPoster:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.PosterPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeason.PosterPath);
            // }
            // else
            // {
            // this.ProcessSeasonPosterDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailPoster;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeason.SeasonPosterAltGallery);
            // this.populateGallery = false;
            // }

            // break;

            // case GalleryType.TvSeasonFanart:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.FanartPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeason.FanartPath);
            // }
            // else
            // {
            // this.ProcessSeasonFanartDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailFanart;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeason.SeasonFanartAltGallery);
            // this.populateGallery = false;
            // }

            // break;

            // case GalleryType.TvSeasonBanner:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeason.BannerPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeason.BannerPath);
            // }
            // else
            // {
            // this.ProcessSeasonBannerDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailBanner;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeason.SeasonBannerAltGallery);
            // this.populateGallery = false;
            // }

            // break;
            // }
        }

        /// <summary>
        ///   Sets tv series binding.
        /// </summary>
        private void SetTvSeriesBinding()
        {
            // if (((this.galleryType != GalleryType.TvSeriesBanner) && (this.galleryType != GalleryType.TvSeriesFanart)) &&
            // (this.galleryType != GalleryType.TvSeriesPoster))
            // {
            // return;
            // }

            // this.imageMain.DataBindings.Clear();
            // this.imageMain.Image = Resources.picturefaded128;

            // layoutControl2.DataBindings.Clear();
            // layoutControl2.DataBindings.Add("Enabled", TVDBFactory.CurrentSeries, "NotLocked");

            // hideContainerLeft.DataBindings.Clear();
            // hideContainerLeft.DataBindings.Add("Enabled", TVDBFactory.CurrentSeries, "NotLocked");

            // switch (this.galleryType)
            // {
            // case GalleryType.TvSeriesPoster:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.PosterPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeries.PosterPath);
            // }
            // else
            // {
            // this.ProcessSeriesPosterDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailPoster;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeries.SeriesPosterAltGallery);
            // this.populateGallery = false;
            // }

            // break;

            // case GalleryType.TvSeriesFanart:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.FanartPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeries.FanartPath);
            // }
            // else
            // {
            // this.ProcessSeriesFanartDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailFanart;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeries.SeriesFanartAltGallery);
            // this.populateGallery = false;
            // }

            // break;

            // case GalleryType.TvSeriesBanner:

            // if (!string.IsNullOrEmpty(TVDBFactory.CurrentSeries.SeriesBannerPath))
            // {
            // imageMain.Image = ImageHandler.LoadImage(TVDBFactory.CurrentSeries.SeriesBannerPath);
            // }
            // else
            // {
            // this.ProcessSeriesBannerDownload();
            // }

            // if (this.populateGallery)
            // {
            // this.galleryControl.Gallery.ImageSize = Get.Ui.PictureThumbnailBanner;
            // this.galleryControl.Gallery.Groups.Clear();
            // this.galleryControl.Gallery.Groups.Add(TVDBFactory.CurrentSeries.SeriesBannerAltGallery);
            // this.populateGallery = false;
            // }

            // break;
            // }
        }

        /// <summary>
        ///   Show "no image" image.
        /// </summary>
        private void ShowNoImage()
        {
            // this.imageMain.Image = Resources.picturefaded128;
        }

        /// <summary>
        ///   Starts loading animation.
        /// </summary>
        private void StartLoading()
        {
            // this.imageMain.StartAnimation();
        }

        /// <summary>
        ///   Stops loading animation.
        /// </summary>
        private void StopLoading()
        {
            // this.imageMain.StopAnimation();
        }

        /// <summary>
        /// Handles the CurrentEpisodeChanged event of the TVDBFactory control.
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
        /// Handles the CurrentSeasonChanged event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_CurrentSeasonChanged(object sender, EventArgs e)
        {
            if (this.galleryType == GalleryType.TvSeasonBanner || this.galleryType == GalleryType.TvSeasonFanart
                || this.galleryType == GalleryType.TvSeasonPoster)
            {
                this.SetTvSeasonBinding();
                this.populateGallery = true;
            }
        }

        /// <summary>
        /// Handles the CurrentSeriesChanged event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_CurrentSeriesChanged(object sender, EventArgs e)
        {
            if (this.galleryType == GalleryType.TvSeriesBanner || this.galleryType == GalleryType.TvSeriesFanart
                || this.galleryType == GalleryType.TvSeriesPoster)
            {
                this.SetTvSeriesBinding();
                this.populateGallery = true;
            }
        }

        /// <summary>
        /// Handles the EpisodeLoaded event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_EpisodeLoaded(object sender, EventArgs e)
        {
            // Image image = TVDBFactory.LoadEpisode();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonBannerLoaded event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_SeasonBannerLoaded(object sender, EventArgs e)
        {
            // Image image = TVDBFactory.LoadSeasonBanner();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonFanartLoaded event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_SeasonFanartLoaded(object sender, EventArgs e)
        {
            // Image image = TVDBFactory.LoadSeasonFanart();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeasonPosterLoaded event of the TVDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDBFactory_SeasonPosterLoaded(object sender, EventArgs e)
        {
            // Image image = TVDBFactory.LoadSeasonPoster();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesBannerLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDbFactory_SeriesBannerLoaded(object sender, EventArgs e)
        {
            // Image image = TVDBFactory.LoadSeriesBanner();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesFanartLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDbFactory_SeriesFanartLoaded(object sender, EventArgs e)
        {
            // var image = TVDBFactory.LoadSeriesFanart();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        /// Handles the SeriesPosterLoaded event of the TvDbFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TvDbFactory_SeriesPosterLoaded(object sender, EventArgs e)
        {
            // var image = TVDBFactory.LoadSeriesPoster();

            // if (image == null)
            // {
            // this.populateGallery = false;
            // return;
            // }

            // this.StopLoading();
            // this.imageMain.Image = image;
        }

        /// <summary>
        ///   The update layout.
        /// </summary>
        private void UpdateLayout()
        {
            // switch (this.galleryType)
            // {
            // case GalleryType.MoviePoster:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
            // break;
            // case GalleryType.MovieFanart:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
            // break;
            // case GalleryType.TvSeriesFanart:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
            // break;
            // case GalleryType.TvSeriesPoster:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
            // break;
            // case GalleryType.TvSeriesBanner:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Banner;
            // break;
            // case GalleryType.TvSeasonFanart:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Fanart;
            // break;
            // case GalleryType.TvSeasonPoster:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Posters;
            // break;
            // case GalleryType.TvSeasonBanner:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Banner;
            // break;
            // case GalleryType.TvEpisodeScreenshot:
            // this.dockPanel1.TabText = Language.DisplayPictureUserControl_UpdateLayout_Alternative_Screenshots;
            // break;
            // }
        }

        #endregion

        // <summary>
        /// Handles the DragOver event of the imageMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        // private void imageMain_DragOver(object sender, DragEventArgs e)
        // {
        // var fileNameW = e.Data.GetData("FileNameW");
        // if (fileNameW != null)
        // {
        // var fileNames = (string[])fileNameW;
        // if (fileNames.Length == 1)
        // {
        // string fileName = fileNames[0];

        // if (Get.InOutCollection.ImageExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
        // {
        // e.Effect = DragDropEffects.Copy;
        // }
        // }
        // }
        // }
    }
}