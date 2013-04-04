// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Series.cs">
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
//   The series.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.Show
{
    #region Required Namespaces

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    using YANFOE.Factories;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.TvModels.TVDB;
    using YANFOE.Properties;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;
    using YANFOE.Tools.Xml;
    using YANFOE.UI.UserControls.CommonControls;

    #endregion

    /// <summary>
    ///   The series.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class Series : ModelBase, IComparer
    {
        #region Fields

        /// <summary>
        ///   The airs day of week.
        /// </summary>
        private readonly string airsDayOfWeek;

        /// <summary>
        ///   The objects guid.
        /// </summary>
        private readonly string guid;

        /// <summary>
        ///   The added.
        /// </summary>
        private string added;

        /// <summary>
        ///   The added by.
        /// </summary>
        private string addedBy;

        /// <summary>
        ///   The airs time.
        /// </summary>
        private string airsTime;

        /// <summary>
        ///   The changed banner.
        /// </summary>
        private bool changedBanner;

        /// <summary>
        ///   The changed fanart.
        /// </summary>
        private bool changedFanart;

        /// <summary>
        ///   The changed poster.
        /// </summary>
        private bool changedPoster;

        /// <summary>
        ///   The changed text.
        /// </summary>
        private bool changedText;

        /// <summary>
        ///   The content rating.
        /// </summary>
        private string contentRating;

        /// <summary>
        ///   The country.
        /// </summary>
        private string country;

        /// <summary>
        ///   The fanart path.
        /// </summary>
        private string fanartPath;

        /// <summary>
        ///   The fanart.
        /// </summary>
        private string fanartUrl;

        /// <summary>
        ///   The first aired.
        /// </summary>
        private DateTime? firstAired;

        /// <summary>
        ///   The id value.
        /// </summary>
        private uint? id;

        /// <summary>
        ///   The imdb id.
        /// </summary>
        private string imdbId;

        /// <summary>
        ///   The is locked.
        /// </summary>
        private bool isLocked;

        /// <summary>
        ///   The language.
        /// </summary>
        private string language;

        /// <summary>
        ///   The lastupdated.
        /// </summary>
        private string lastupdated;

        /// <summary>
        ///   The network.
        /// </summary>
        private string network;

        /// <summary>
        ///   The network id.
        /// </summary>
        private string networkID;

        /// <summary>
        ///   The overview.
        /// </summary>
        private string overview;

        /// <summary>
        ///   The poster path.
        /// </summary>
        private string posterPath;

        /// <summary>
        ///   The poster.
        /// </summary>
        private string posterUrl;

        /// <summary>
        ///   The rating.
        /// </summary>
        private double? rating;

        /// <summary>
        ///   The runtime.
        /// </summary>
        private int? runtime;

        /// <summary>
        ///   Gets or sets the seasons.
        /// </summary>
        /// <value> The seasons object. </value>
        private ThreadedBindingList<Season> seasons;

        /// <summary>
        ///   The series banner path.
        /// </summary>
        private string seriesBannerPath;

        /// <summary>
        ///   The banner 1.
        /// </summary>
        private string seriesBannerUrl;

        /// <summary>
        ///   The series id.
        /// </summary>
        private uint? seriesID;

        /// <summary>
        ///   The series name.
        /// </summary>
        private string seriesName;

        /// <summary>
        ///   The small banner.
        /// </summary>
        private Image smallBanner;

        /// <summary>
        ///   The status.
        /// </summary>
        private string status;

        /// <summary>
        ///   The zap 2 it id.
        /// </summary>
        private string zap2ItID;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Series" /> class.
        /// </summary>
        public Series()
        {
            this.guid = GenerateRandomID.Generate();

            this.ID = null;
            this.AirsDayOfWeek = string.Empty;
            this.ContentRating = string.Empty;
            this.FirstAired = null;
            this.Genre = new ThreadedBindingList<string>();
            this.ImdbId = string.Empty;
            this.Language = string.Empty;
            this.Network = string.Empty;
            this.NetworkID = string.Empty;
            this.Overview = string.Empty;
            this.Rating = null;
            this.Runtime = null;
            this.SeriesID = null;
            this.SeriesName = string.Empty;
            this.Status = string.Empty;
            this.Added = string.Empty;
            this.AddedBy = string.Empty;
            this.SeriesBannerUrl = string.Empty;
            this.FanartUrl = string.Empty;
            this.Lastupdated = string.Empty;
            this.PosterUrl = string.Empty;
            this.Zap2It_Id = string.Empty;
            this.airsDayOfWeek = string.Empty;

            this.Actors = new ThreadedBindingList<PersonModel>();
            this.Banner = new Banner();
            this.Seasons = new ThreadedBindingList<Season>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the actors.
        /// </summary>
        /// <value> The actors object </value>
        public ThreadedBindingList<PersonModel> Actors { get; set; }

        /// <summary>
        ///   Gets or sets Added.
        /// </summary>
        public string Added
        {
            get
            {
                return this.added;
            }

            set
            {
                if (this.added != value)
                {
                    this.added = value;
                    this.OnPropertyChanged("Added", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets AddedBy.
        /// </summary>
        public string AddedBy
        {
            get
            {
                return this.addedBy;
            }

            set
            {
                if (this.addedBy != value)
                {
                    this.addedBy = value;
                    this.OnPropertyChanged("AddedBy", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets AirsDayOfWeek.
        /// </summary>
        public string AirsDayOfWeek
        {
            get
            {
                return this.airsDayOfWeek;
            }

            set
            {
                if (this.addedBy != value)
                {
                    this.addedBy = value;
                    this.OnPropertyChanged("AddedBy", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets AirsTime.
        /// </summary>
        public string AirsTime
        {
            get
            {
                return this.airsTime;
            }

            set
            {
                if (this.airsTime != value)
                {
                    this.airsTime = value;
                    this.OnPropertyChanged("AirsTime", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the banner object
        /// </summary>
        /// <value> The banner object </value>
        public Banner Banner { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedBanner.
        /// </summary>
        public bool ChangedBanner
        {
            get
            {
                return this.changedBanner;
            }

            set
            {
                if (this.changedBanner != value)
                {
                    this.DatabaseSaved = false;
                    this.changedBanner = value;
                    this.OnPropertyChanged("ChangedBanner");
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedFanart.
        /// </summary>
        public bool ChangedFanart
        {
            get
            {
                return this.changedFanart;
            }

            set
            {
                if (this.changedFanart != value)
                {
                    this.DatabaseSaved = false;
                    this.changedFanart = value;
                    this.OnPropertyChanged("ChangedPoster");
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedPoster.
        /// </summary>
        public bool ChangedPoster
        {
            get
            {
                return this.changedPoster;
            }

            set
            {
                if (this.changedPoster != value)
                {
                    this.DatabaseSaved = false;
                    this.changedPoster = value;
                    this.OnPropertyChanged("ChangedPoster");
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedText.
        /// </summary>
        public bool ChangedText
        {
            get
            {
                return this.changedText;
            }

            set
            {
                if (this.changedText != value)
                {
                    this.DatabaseSaved = false;
                    this.changedText = value;
                    this.OnPropertyChanged("ChangedText");
                }
            }
        }

        /// <summary>
        ///   Gets or sets ContentRating.
        /// </summary>
        public string ContentRating
        {
            get
            {
                return this.contentRating;
            }

            set
            {
                if (this.contentRating != value)
                {
                    this.contentRating = value;
                    this.OnPropertyChanged("ContentRating");
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Country.
        /// </summary>
        public string Country
        {
            get
            {
                return this.country;
            }

            set
            {
                if (this.country != value)
                {
                    this.country = value;
                    this.OnPropertyChanged("Country", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether [database saved].
        /// </summary>
        /// <value> <c>true</c> if [database saved]; otherwise, <c>false</c> . </value>
        public bool DatabaseSaved { get; set; }

        /// <summary>
        ///   Gets the fanart image.
        /// </summary>
        [JsonIgnore]
        public Image FanartImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.FanartPath))
                {
                    url = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(this.FanartUrl), DownloadType.Binary, Section.Movies);
                    this.FanartPath = url;
                }
                else
                {
                    url = this.FanartPath;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        ///   Gets or sets FanartPath.
        /// </summary>
        public string FanartPath
        {
            get
            {
                return this.fanartPath;
            }

            set
            {
                if (this.fanartPath != value)
                {
                    this.SmallFanart = null;
                    this.fanartPath = value;
                    this.OnPropertyChanged("FanartPath", true);
                    this.ChangedFanart = true;

                    if (!string.IsNullOrEmpty(this.FanartUrl))
                    {
                        this.fanartUrl = string.Empty;
                        this.SmallFanart = ImageHandler.LoadImage(this.fanartPath, 100, 60);
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets Fanart.
        /// </summary>
        public string FanartUrl
        {
            get
            {
                return this.fanartUrl;
            }

            set
            {
                if (this.fanartUrl != value)
                {
                    this.SmallFanart = null;
                    this.fanartUrl = value;
                    this.OnPropertyChanged("FanartUrl", true);
                    this.ChangedFanart = true;

                    if (!string.IsNullOrEmpty(this.fanartUrl))
                    {
                        string url = WebCache.GetPathFromUrl(
                            TVDBFactory.Instance.GetImageUrl(this.fanartUrl, true), Section.Tv);
                        this.SmallFanart = ImageHandler.LoadImage(url, 100, 60);
                        this.fanartPath = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets FirstAired.
        /// </summary>
        public DateTime? FirstAired
        {
            get
            {
                return this.firstAired;
            }

            set
            {
                if (this.firstAired != value)
                {
                    this.firstAired = value;
                    this.OnPropertyChanged("FirstAired", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Genre.
        /// </summary>
        public ThreadedBindingList<string> Genre { get; set; }

        /// <summary>
        ///   Gets or sets GenreAsString.
        /// </summary>
        public string GenreAsString
        {
            get
            {
                return this.Genre.ToCommaList();
            }

            set
            {
                if (this.Genre != value.ToBindingStringList())
                {
                    this.Genre = value.ToBindingStringList();
                    this.OnPropertyChanged("GenreAsString", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets Guid.
        /// </summary>
        public string Guid
        {
            get
            {
                return this.guid;
            }
        }

        /// <summary>
        ///   Gets or sets MovieUniqueId.
        /// </summary>
        public uint? ID
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("Id", true);
                }
            }
        }

        /// <summary>
        ///   Gets or sets ImdbId.
        /// </summary>
        public string ImdbId
        {
            get
            {
                return this.imdbId;
            }

            set
            {
                if (this.imdbId != value)
                {
                    this.imdbId = value;
                    this.OnPropertyChanged("ImdbId", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether IsLocked.
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return this.isLocked;
            }

            set
            {
                this.isLocked = value;
                this.OnPropertyChanged("IsLocked", true);
                this.OnPropertyChanged("LockedImage");
            }
        }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                if (this.language != value)
                {
                    this.language = value;
                    this.OnPropertyChanged("Language", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Lastupdated.
        /// </summary>
        public string Lastupdated
        {
            get
            {
                return this.lastupdated;
            }

            set
            {
                if (this.lastupdated != value)
                {
                    this.lastupdated = value;
                    this.OnPropertyChanged("Lastupdated", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets the locked image.
        /// </summary>
        [JsonIgnore]
        public Image LockedImage
        {
            get
            {
                return this.IsLocked ? Resources.locked32 : Resources.unlock32;
            }
        }

        /// <summary>
        ///   Gets or sets MediaPaths.
        /// </summary>
        public ThreadedBindingList<string> MediaPaths { get; set; }

        /// <summary>
        ///   Gets or sets Network.
        /// </summary>
        public string Network
        {
            get
            {
                return this.network;
            }

            set
            {
                if (this.network != value)
                {
                    this.network = value;
                    this.OnPropertyChanged("Network", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets NetworkID.
        /// </summary>
        public string NetworkID
        {
            get
            {
                return this.networkID;
            }

            set
            {
                if (this.networkID != value)
                {
                    this.networkID = value;
                    this.OnPropertyChanged("NetworkID", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets a value indicating whether [not locked].
        /// </summary>
        /// <value> <c>true</c> if [not locked]; otherwise, <c>false</c> . </value>
        public bool NotLocked
        {
            get
            {
                return !this.isLocked;
            }
        }

        /// <summary>
        ///   Gets or sets Overview.
        /// </summary>
        public string Overview
        {
            get
            {
                return this.overview;
            }

            set
            {
                if (this.overview != value)
                {
                    this.overview = value;
                    this.OnPropertyChanged("Overview", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets the poster image.
        /// </summary>
        [JsonIgnore]
        public Image PosterImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.PosterPath))
                {
                    url = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(this.PosterUrl), DownloadType.Binary, Section.Movies);
                    this.PosterPath = url;
                }
                else
                {
                    url = this.PosterPath;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        ///   Gets or sets PosterPath.
        /// </summary>
        public string PosterPath
        {
            get
            {
                return this.posterPath;
            }

            set
            {
                if (this.posterPath != value)
                {
                    this.SmallPoster = null;
                    this.posterPath = value;
                    this.OnPropertyChanged("PosterPath", true);
                    this.ChangedPoster = true;

                    if (!string.IsNullOrEmpty(this.posterPath))
                    {
                        this.SmallPoster = ImageHandler.LoadImage(this.posterPath, 100, 150);
                        this.posterUrl = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets Poster.
        /// </summary>
        public string PosterUrl
        {
            get
            {
                return this.posterUrl;
            }

            set
            {
                if (this.posterUrl != value)
                {
                    this.SmallPoster = null;
                    this.posterUrl = value;
                    this.OnPropertyChanged("PosterUrl", true);
                    this.ChangedPoster = true;

                    if (!string.IsNullOrEmpty(this.posterUrl))
                    {
                        string url = WebCache.GetPathFromUrl(
                            TVDBFactory.Instance.GetImageUrl(this.posterUrl, true), Section.Tv);
                        this.SmallPoster = ImageHandler.LoadImage(url, 100, 150);
                        this.posterPath = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets Rating.
        /// </summary>
        public double? Rating
        {
            get
            {
                return this.rating;
            }

            set
            {
                if (this.rating != value)
                {
                    this.rating = value;
                    this.OnPropertyChanged("Rating", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Runtime.
        /// </summary>
        public int? Runtime
        {
            get
            {
                return this.runtime;
            }

            set
            {
                if (this.runtime != value)
                {
                    this.runtime = value;
                    this.OnPropertyChanged("Runtime", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the seasons.
        /// </summary>
        public ThreadedBindingList<Season> Seasons
        {
            get
            {
                return this.seasons;
            }

            set
            {
                this.seasons = value;
                this.OnPropertyChanged("Seasons");
            }
        }

        /// <summary>
        ///   Gets the series banner alt gallery.
        /// </summary>
        [JsonIgnore]
        public GalleryItemGroup SeriesBannerAltGallery
        {
            get
            {
                var gallery = new GalleryItemGroup();

                var images = from i in this.Banner.Series
                             where i.BannerType2 == BannerType2.graphical
                             orderby i.Rating descending
                             select i;

                foreach (var image in images)
                {
                    string path = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(image.BannerPath, true), DownloadType.Binary, Section.Tv);

                    if (File.Exists(path) && !Downloader.Downloading.Contains(path))
                    {
                        Image resizedimage = ImageHandler.LoadImage(path, Get.Ui.PictureThumbnailBanner);

                        var superTip = new SuperToolTip { AllowHtmlText = true };
                        superTip.Items.Add("<b>Rating:</b> " + image.Rating);
                        superTip.Items.Add("<b>Rating count:</b> " + image.RatingCount);

                        var galleryItem = new GalleryItem(resizedimage, string.Empty, image.BannerType2.ToString())
                            {
                               Tag = "tvSeriesBanner|" + image.BannerPath, SuperTip = superTip 
                            };

                        gallery.Items.Add(galleryItem);
                    }
                }

                return gallery;
            }
        }

        /// <summary>
        ///   Gets the series banner image.
        /// </summary>
        [JsonIgnore]
        public Image SeriesBannerImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.SeriesBannerPath))
                {
                    url = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(this.SeriesBannerUrl), DownloadType.Binary, Section.Movies);
                    this.SeriesBannerPath = url;
                }
                else
                {
                    url = this.SeriesBannerPath;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        ///   Gets or sets SeriesBannerPath.
        /// </summary>
        public string SeriesBannerPath
        {
            get
            {
                return this.seriesBannerPath;
            }

            set
            {
                if (this.seriesBannerPath != value)
                {
                    this.SmallBanner = null;
                    this.seriesBannerPath = value;
                    this.OnPropertyChanged("SeriesBannerPath", true);
                    this.ChangedText = true;

                    if (!string.IsNullOrEmpty(this.seriesBannerPath))
                    {
                        this.SmallFanart = ImageHandler.LoadImage(this.seriesBannerPath, 100, 30);
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets banner.
        /// </summary>
        public string SeriesBannerUrl
        {
            get
            {
                return this.seriesBannerUrl;
            }

            set
            {
                if (this.seriesBannerUrl != value)
                {
                    this.SmallBanner = null;
                    this.seriesBannerUrl = value;
                    this.OnPropertyChanged("SeriesBannerUrl", true);
                    this.ChangedText = true;

                    if (!string.IsNullOrEmpty(this.seriesBannerUrl))
                    {
                        string url =
                            WebCache.GetPathFromUrl(
                                TVDBFactory.Instance.GetImageUrl(this.seriesBannerUrl, true), Section.Tv);
                        this.SmallFanart = ImageHandler.LoadImage(url, 100, 30);
                        this.seriesBannerPath = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets the series fanart alt gallery.
        /// </summary>
        [JsonIgnore]
        public GalleryItemGroup SeriesFanartAltGallery
        {
            get
            {
                var gallery = new GalleryItemGroup();
                var images = from i in this.Banner.Fanart orderby i.Rating descending select i;

                foreach (var image in images)
                {
                    string path = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(image.BannerPath, true), DownloadType.Binary, Section.Tv);

                    if (File.Exists(path) && !Downloader.Downloading.Contains(path))
                    {
                        Image resizedimage = ImageHandler.LoadImage(path, Get.Ui.PictureThumbnailFanart);

                        var superTip = new SuperToolTip();
                        superTip.AllowHtmlText = true;
                        superTip.Items.Add("<b>Rating:</b> " + image.Rating);
                        superTip.Items.Add("<b>Rating count:</b> " + image.RatingCount);

                        var galleryItem = new GalleryItem(resizedimage, string.Empty, image.BannerType2.ToString())
                            {
                               Tag = "tvSeriesFanart|" + image.BannerPath, SuperTip = superTip 
                            };

                        gallery.Items.Add(galleryItem);
                    }
                }

                return gallery;
            }
        }

        /// <summary>
        ///   Gets or sets SeriesID.
        /// </summary>
        public uint? SeriesID
        {
            get
            {
                return this.seriesID;
            }

            set
            {
                if (this.seriesID != value)
                {
                    this.seriesID = value;
                    this.OnPropertyChanged("SeriesID", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets SeriesName.
        /// </summary>
        public string SeriesName
        {
            get
            {
                return this.seriesName;
            }

            set
            {
                if (this.seriesName != value)
                {
                    if (this.seriesName != null)
                    {
                        var seriesIndex =
                            (from s in TVDBFactory.Instance.MasterSeriesList where s.SeriesGuid == this.guid select s).
                                SingleOrDefault();

                        if (seriesIndex != null)
                        {
                            seriesIndex.SeriesName = value;
                            TVDBFactory.Instance.InvokeMasterSeriesNameListChanged(new EventArgs());
                        }
                    }

                    this.seriesName = value;
                    this.OnPropertyChanged("SeriesName", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets the series poster alt gallery.
        /// </summary>
        [JsonIgnore]
        public GalleryItemGroup SeriesPosterAltGallery
        {
            get
            {
                var gallery = new GalleryItemGroup();
                var images = from i in this.Banner.Poster orderby i.Rating descending select i;

                foreach (var image in images)
                {
                    string path = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(image.BannerPath, true), DownloadType.Binary, Section.Tv);

                    if (File.Exists(path) && !Downloader.Downloading.Contains(path))
                    {
                        Image resizedimage = ImageHandler.LoadImage(path, Get.Ui.PictureThumbnailPoster);

                        var superTip = new SuperToolTip();
                        superTip.AllowHtmlText = true;
                        superTip.Items.Add("<b>Rating:</b> " + image.Rating);
                        superTip.Items.Add("<b>Rating count:</b> " + image.RatingCount);

                        var galleryItem = new GalleryItem(resizedimage, string.Empty, image.BannerType2.ToString())
                            {
                               Tag = "tvSeriesPoster|" + image.BannerPath, SuperTip = superTip 
                            };

                        gallery.Items.Add(galleryItem);
                    }
                }

                return gallery;
            }
        }

        /// <summary>
        ///   Gets or sets SmallBanner.
        /// </summary>
        [JsonIgnore]
        public Image SmallBanner
        {
            get
            {
                return ImageHandler.LoadThumb(this.Guid, "b");
            }

            set
            {
                if (!ImageHandler.Compare(value, this.Guid, "b"))
                {
                    ImageHandler.SaveThumb(value, this.Guid, "b");
                    this.OnPropertyChanged("SmallBanner");
                }
            }
        }

        /// <summary>
        ///   Gets or sets SmallFanart.
        /// </summary>
        [JsonIgnore]
        public Image SmallFanart
        {
            get
            {
                return ImageHandler.LoadThumb(this.Guid, "f");
            }

            set
            {
                if (!ImageHandler.Compare(value, this.Guid, "f"))
                {
                    ImageHandler.SaveThumb(value, this.Guid, "f");
                    this.OnPropertyChanged("SmallFanart");
                }
            }
        }

        /// <summary>
        ///   Gets or sets SmallPoster.
        /// </summary>
        [JsonIgnore]
        public Image SmallPoster
        {
            get
            {
                return ImageHandler.LoadThumb(this.Guid, "p");
            }

            set
            {
                if (!ImageHandler.Compare(value, this.Guid, "p"))
                {
                    ImageHandler.SaveThumb(value, this.Guid, "p");
                    this.OnPropertyChanged("SmallPoster");
                }
            }
        }

        /// <summary>
        ///   Gets or sets Status.
        /// </summary>
        public string Status
        {
            get
            {
                return this.status;
            }

            set
            {
                if (this.status != value)
                {
                    this.status = value;
                    this.OnPropertyChanged("Status", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets zap2it_id.
        /// </summary>
        public string Zap2It_Id
        {
            get
            {
                return this.zap2ItID;
            }

            set
            {
                if (this.zap2ItID != value)
                {
                    this.zap2ItID = value;
                    this.OnPropertyChanged("Zap2It_Id", true);
                    this.ChangedText = true;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add high priority to background queue.
        /// </summary>
        /// <param name="url">
        /// The url. 
        /// </param>
        public void AddHighPriorityToBackgroundQueue(string url)
        {
            Downloader.AddToBackgroundQueue(
                new DownloadItem
                    {
                        Priority = DownloadPriority.High, 
                        Section = Section.Tv, 
                        Type = DownloadType.Binary, 
                        Url = TVDBFactory.Instance.GetImageUrl(url)
                    });
        }

        /// <summary>
        /// The add high priority to background queue cache only.
        /// </summary>
        /// <param name="url">
        /// The url. 
        /// </param>
        public void AddHighPriorityToBackgroundQueueCacheOnly(string url)
        {
            Downloader.AddToBackgroundQueue(
                new DownloadItem
                    {
                        Priority = DownloadPriority.High, 
                        Section = Section.Tv, 
                        Type = DownloadType.Binary, 
                        Url = TVDBFactory.Instance.GetImageUrl(url, true)
                    });
        }

        /// <summary>
        /// The add high priority to background queue with cache.
        /// </summary>
        /// <param name="url">
        /// The url. 
        /// </param>
        public void AddHighPriorityToBackgroundQueueWithCache(string url)
        {
            this.AddHighPriorityToBackgroundQueue(url);
            this.AddHighPriorityToBackgroundQueueCacheOnly(url);
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">
        /// The first object to compare. 
        /// </param>
        /// <param name="y">
        /// The second object to compare. 
        /// </param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x"/> and <paramref name="y"/> , as shown in the following table.Value Meaning Less than zero <paramref name="x"/> is less than <paramref name="y"/> . Zero <paramref name="x"/> equals <paramref name="y"/> . Greater than zero <paramref name="x"/> is greater than <paramref name="y"/> . 
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// Neither
        ///   <paramref name="x"/>
        ///   nor
        ///   <paramref name="y"/>
        ///   implements the
        ///   <see cref="T:System.IComparable"/>
        ///   interface.-or-
        ///   <paramref name="x"/>
        ///   and
        ///   <paramref name="y"/>
        ///   are of different types and neither one can handle comparisons with the other.
        /// </exception>
        public int Compare(object x, object y)
        {
            var x0 = (Series)x;
            var y0 = (Series)y;
            return x0.SeriesName.CompareTo(y0.SeriesName);
        }

        /// <summary>
        ///   Counts the missing episodes in the series
        /// </summary>
        /// <returns> Total episodes without a filepath </returns>
        public int CountMissingEpisodes()
        {
            return this.Seasons.SelectMany(s => s.Episodes).Count(e => string.IsNullOrEmpty(e.FilePath.PathAndFilename));
        }

        /// <summary>
        ///   Counts the total amount of episodes in the series
        /// </summary>
        /// <returns> Total episodes without a filepath </returns>
        public int CountTotalEpisodes()
        {
            return this.Seasons.SelectMany(s => s.Episodes).Count(
                e => !string.IsNullOrEmpty(e.FilePath.PathAndFilename));
        }

        /// <summary>
        ///   Gets the missing episodes in the series
        /// </summary>
        /// <returns> Total episodes without a filepath </returns>
        public List<Episode> GetMissingEpisodes()
        {
            return
                this.Seasons.SelectMany(s => s.Episodes).Where(e => string.IsNullOrEmpty(e.FilePath.PathAndFilename)).
                    ToList();
        }

        /// <summary>
        ///   Gets the series name on disk.
        /// </summary>
        /// <returns> The series name on disk. </returns>
        public string GetSeriesNameOnDisk()
        {
            foreach (var season in this.Seasons)
            {
                foreach (Episode episode in season.Episodes)
                {
                    if (!string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
                    {
                        if (File.Exists(episode.FilePath.PathAndFilename))
                        {
                            string[] segSplit = episode.FilePath.PathAndFilename.Split(new[] { '\\' });

                            return segSplit[segSplit.Length - 3];
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///   Gets the series path.
        /// </summary>
        /// <returns> The Series path </returns>
        public string GetSeriesPath()
        {
            foreach (var seasons in this.Seasons)
            {
                foreach (Episode episode in seasons.Episodes)
                {
                    if (!string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
                    {
                        if (File.Exists(episode.FilePath.PathAndFilename))
                        {
                            if (MovieNaming.IsDVD(episode.FilePath.PathAndFilename))
                            {
                                string[] segSplit = episode.FilePath.PathAndFilename.Split(new[] { '\\' });

                                return string.Join(@"\", segSplit, 0, segSplit.Length - 2);
                            }

                            if (MovieNaming.IsBluRay(episode.FilePath.PathAndFilename))
                            {
                                string[] segSplit = episode.FilePath.PathAndFilename.Split(new[] { '\\' });

                                return string.Join(@"\", segSplit, 0, segSplit.Length - 3);
                            }
                            else
                            {
                                string[] segSplit = episode.FilePath.PathAndFilename.Split(new[] { '\\' });
                                string path = string.Join(@"\", segSplit, 0, segSplit.Length - 2);

                                return path;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Populates the the series object with details from a series xml object.
        /// </summary>
        /// <param name="xml">
        /// The series xml. 
        /// </param>
        public void PopulateFullDetails(SeriesXml xml)
        {
            var docList = new XmlDocument();
            docList.LoadXml(xml.En);

            XmlNodeList nodes = docList.GetElementsByTagName("Series");

            var doc = new XmlDocument();
            doc.LoadXml(nodes[0].OuterXml);

            this.ID = XRead.GetUInt(doc, "id");
            this.AirsDayOfWeek = XRead.GetString(doc, "Airs_DayOfWeek");
            this.AirsTime = XRead.GetString(doc, "Airs_Time");
            this.ContentRating = XRead.GetString(doc, "ContentRating");
            this.FirstAired = XRead.GetDateTime(doc, "FirstAired", "yyyy-MM-dd");
            this.Genre = XRead.GetString(doc, "Genre").ToBindingStringList('|');
            this.ImdbId = XRead.GetString(doc, "IMDB_ID");
            this.Language = XRead.GetString(doc, "Language");
            this.Network = XRead.GetString(doc, "Network");
            this.NetworkID = XRead.GetString(doc, "NetworkID");
            this.Overview = XRead.GetString(doc, "Overview");
            this.Rating = XRead.GetDouble(doc, "Rating");
            this.Runtime = XRead.GetInt(doc, "Runtime");
            this.SeriesID = XRead.GetUInt(doc, "id");
            this.SeriesName = XRead.GetString(doc, "SeriesName");
            this.Status = XRead.GetString(doc, "Status");
            this.Added = XRead.GetString(doc, "added");
            this.AddedBy = XRead.GetString(doc, "addedby");
            this.SeriesBannerUrl = XRead.GetString(doc, "banner");
            this.FanartUrl = XRead.GetString(doc, "fanart");
            this.Lastupdated = XRead.GetString(doc, "lastupdated");
            this.Zap2It_Id = XRead.GetString(doc, "zap2it_id");
            this.PosterUrl = XRead.GetString(doc, "poster");

            nodes = docList.GetElementsByTagName("Episode");

            int? count = 0;

            // Count Seasons
            foreach (XmlNode node in nodes)
            {
                var episode = new Episode();
                episode.Populate(node.OuterXml);

                if (episode.SeasonNumber > count)
                {
                    count = episode.SeasonNumber;
                }
            }

            // Extract main Actors
            var actorsDoc = new XDocument(XDocument.Parse(xml.Actors));

            IEnumerable<XElement> linqActors = from a in actorsDoc.Descendants("Actor") select a;

            foreach (XElement a in linqActors)
            {
                string image = a.Element("Image").Value;

                if (!string.IsNullOrEmpty(image))
                {
                    image = TVDBFactory.Instance.GetImageUrl(image);
                }

                var m = new PersonModel(a.Element("Name").Value, image, a.Element("Role").Value);
                this.Actors.Add(m);
            }

            this.Banner.Populate(xml.Banners);

            // Create Seasons
            int count2;
            int.TryParse(count.ToString(), out count2);

            for (int i = 0; i < count2 + 1; i++)
            {
                var season = new Season { SeasonNumber = i };

                List<string> seasonBanner = (from p in this.Banner.Season
                                             where
                                                 p.BannerType2 == BannerType2.seasonwide
                                                 && p.Season == season.SeasonNumber.ToString()
                                             select p.BannerPath).ToList();

                if (seasonBanner.Count > 0)
                {
                    season.BannerUrl = seasonBanner[0];

                    // this.AddHighPriorityToBackgroundQueueWithCache(season.BannerUrl);
                }

                List<string> seasonPoster =
                    (from p in this.Banner.Season where p.Season == season.SeasonNumber.ToString() select p.BannerPath).
                        ToList();

                if (this.posterUrl != null && seasonPoster.Count > 0)
                {
                    season.PosterUrl = seasonPoster[0];

                    // this.AddHighPriorityToBackgroundQueueWithCache(season.PosterUrl);
                }

                List<BannerDetails> seasonFanart = (from p in this.Banner.Fanart select p).ToList();

                if (seasonFanart.Count > i)
                {
                    season.FanartUrl = seasonFanart[i].BannerPath;
                }
                else if (seasonFanart.Count > 0)
                {
                    season.FanartUrl = seasonFanart[0].BannerPath;
                }

                if (!string.IsNullOrEmpty(season.FanartUrl))
                {
                    // this.AddHighPriorityToBackgroundQueueWithCache(season.FanartUrl);
                }

                this.Seasons.Add(season);
            }

            foreach (XmlNode node in nodes)
            {
                var episode = new Episode();
                bool result = episode.Populate(node.OuterXml);

                if (result)
                {
                    int episodeNumber;
                    int.TryParse(episode.SeasonNumber.ToString(), out episodeNumber);

                    this.Seasons[episodeNumber].Episodes.Add(episode);
                }
            }

            this.PreCacheSeriesThumbs();
        }

        /// <summary>
        ///   The pre cache series thumbs.
        /// </summary>
        public void PreCacheSeriesThumbs()
        {
            var imagesFanart = from i in this.Banner.Fanart select i;

            foreach (var image in imagesFanart)
            {
                this.AddHighPriorityToBackgroundQueueCacheOnly(image.BannerPath);
            }

            var imagesPoster = from i in this.Banner.Poster orderby i.Rating descending select i;

            foreach (var image in imagesPoster)
            {
                this.AddHighPriorityToBackgroundQueueCacheOnly(image.BannerPath);
            }

            var imagesSeries = from i in this.Banner.Series orderby i.Rating descending select i;

            foreach (var image in imagesSeries)
            {
                this.AddHighPriorityToBackgroundQueueCacheOnly(image.BannerPath);
            }

            var imagesSeason = from i in this.Banner.Season orderby i.Rating descending select i;

            foreach (var image in imagesSeason)
            {
                this.AddHighPriorityToBackgroundQueueCacheOnly(image.BannerPath);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Gets the first episode in the series.
        /// </summary>
        /// <returns> The first episode in the series. </returns>
        internal string GetFirstEpisode()
        {
            foreach (var season in this.Seasons)
            {
                foreach (Episode episode in season.Episodes)
                {
                    if (!string.IsNullOrEmpty(episode.CurrentFilenameAndPath))
                    {
                        return episode.CurrentFilenameAndPath;
                    }
                }
            }

            return string.Empty;
        }

        #endregion
    }
}