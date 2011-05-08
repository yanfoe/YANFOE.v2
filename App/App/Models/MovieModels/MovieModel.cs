// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.MovieModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using DevExpress.Utils;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors.DXErrorProvider;

    using Newtonsoft.Json;

    using YANFOE.Factories.Sets;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.IO;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.NFOModels;
    using YANFOE.Properties;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Images;
    using YANFOE.Tools.Models;

    using ErrorInfo = DevExpress.XtraEditors.DXErrorProvider.ErrorInfo;

    /// <summary>
    /// The model used for the main movie object.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    [Serializable]
    public class MovieModel : ModelBase, IDXDataErrorInfo
    {
        #region Constants and Fields

        private Dictionary<ScraperList, string> ScraperIds { get; set; }

        /// <summary>
        /// The change list.
        /// </summary>
        private readonly BindingList<string> changeList;

        /// <summary>
        /// The allocine id.
        /// </summary>
        private string allocineId;

        /// <summary>
        /// Busy backing field
        /// </summary>
        private bool busy;

        /// <summary>
        /// Certification backing field
        /// </summary>
        private string certification;

        /// <summary>
        /// Changed Poster back field
        /// </summary>
        private bool changedFanart;

        /// <summary>
        /// Changed Poster back field
        /// </summary>
        private bool changedPoster;

        /// <summary>
        /// Changed Text backing field
        /// </summary>
        private bool changedText;

        /// <summary>
        /// Changed Trailer back field
        /// </summary>
        private bool changedTrailer;

        /// <summary>
        /// The current fanart image url.
        /// </summary>
        private string currentFanartImageUrl;

        /// <summary>
        /// The current poster image url.
        /// </summary>
        private string currentPosterImageUrl;

        /// <summary>
        /// The current trailer url.
        /// </summary>
        private string currentTrailerUrl;

        /// <summary>
        /// The fanart path on disk.
        /// </summary>
        private string fanartPathOnDisk;

        /// <summary>
        /// The film affinity id.
        /// </summary>
        private string filmAffinityId;

        /// <summary>
        /// The film delta id.
        /// </summary>
        private string filmDeltaId;

        /// <summary>
        /// The film up id.
        /// </summary>
        private string filmUpId;

        /// <summary>
        /// The film web id.
        /// </summary>
        private string filmWebId;

        /// <summary>
        /// The imdb id.
        /// </summary>
        private string imdbId;

        /// <summary>
        /// The impawards id.
        /// </summary>
        private string impawardsId;

        /// <summary>
        /// The is new.
        /// </summary>
        private bool isNew;

        /// <summary>
        /// The kinopoisk id.
        /// </summary>
        private string kinopoiskId;

        /// <summary>
        /// The locked.
        /// </summary>
        private bool locked;

        /// <summary>
        /// The marked.
        /// </summary>
        private bool marked;

        /// <summary>
        /// The mpaa backing field
        /// </summary>
        private string mpaa;

        /// <summary>
        /// The multi select model.
        /// </summary>
        private bool multiSelectModel;

        /// <summary>
        /// The nfo path.
        /// </summary>
        private string nfoPathOnDisk;

        /// <summary>
        /// Original Title backing field
        /// </summary>
        private string originalTitle;

        /// <summary>
        /// Outline backing field
        /// </summary>
        private string outline;

        /// <summary>
        /// Plot backing field
        /// </summary>
        private string plot;

        /// <summary>
        /// The poster path on disk.
        /// </summary>
        private string posterPathOnDisk;

        /// <summary>
        /// Rating backing field
        /// </summary>
        private double? rating;

        /// <summary>
        /// Release Date backing field
        /// </summary>
        private DateTime? releaseDate;

        /// <summary>
        /// Runtime backing field
        /// </summary>
        private int? runtime;

        /// <summary>
        /// The scraper group.
        /// </summary>
        private string scraperGroup;

        /// <summary>
        /// The set studio.
        /// </summary>
        private string setStudio;

        /// <summary>
        /// SmallPoster backing field
        /// </summary>
        private Image smallFanart;

        /// <summary>
        /// SmallPoster backing field
        /// </summary>
        private Image smallPoster;

        /// <summary>
        /// Tagline backing field
        /// </summary>
        private string tagline;

        /// <summary>
        /// Title backing field
        /// </summary>
        private string title;

        /// <summary>
        /// The tmdb id.
        /// </summary>
        private string tmdbId;

        /// <summary>
        /// Top250 backing field
        /// </summary>
        private int? top250;

        /// <summary>
        /// The trailer path on disk.
        /// </summary>
        private string trailerPathOnDisk;

        /// <summary>
        /// The video source.
        /// </summary>
        private string videoSource;

        /// <summary>
        /// Votes backing field
        /// </summary>
        private long? votes;

        /// <summary>
        /// The watched.
        /// </summary>
        private bool watched;

        /// <summary>
        /// The yanfoe id.
        /// </summary>
        private string yanfoeID;

        /// <summary>
        /// Year backing field
        /// </summary>
        private int? year;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieModel"/> class.
        /// </summary>
        public MovieModel()
        {
            this.MovieUniqueId = Guid.NewGuid().ToString();

            this.IsBusy = false;

            this.ScraperGroup = string.Empty;

            this.ChangedText = false;
            this.ChangedPoster = false;
            this.ChangedTrailer = false;
            this.changeList = new BindingList<string>();

            this.Title = string.Empty;
            this.OriginalTitle = string.Empty;
            this.Year = null;
            this.Rating = null;
            this.Director = new BindingList<PersonModel>();
            this.Plot = string.Empty;
            this.Outline = string.Empty;
            this.Certification = string.Empty;
            this.Country = new BindingList<string>();
            this.Language = new BindingList<string>();
            this.Genre = new BindingList<string>();
            this.Cast = new BindingList<PersonModel>();
            this.Tagline = string.Empty;
            this.Top250 = null;
            this.Studios = new BindingList<string>();
            this.Votes = null;
            this.ReleaseDate = new DateTime();
            this.Mpaa = string.Empty;
            this.Runtime = null;
            this.Writers = new BindingList<PersonModel>();
            this.AlternativePosters = new BindingList<ImageDetailsModel>();
            this.AlternativeFanart = new BindingList<ImageDetailsModel>();
            this.AlternativeTrailers = new BindingList<TrailerDetailsModel>();
            this.FileInfo = new FileInfoModel();
            this.AssociatedFiles = new AssociatedFilesModel();

            this.AllocineId = string.Empty;
            this.FilmAffinityId = string.Empty;
            this.FilmDeltaId = string.Empty;
            this.FilmUpId = string.Empty;
            this.FilmWebId = string.Empty;
            this.ImdbId = string.Empty;
            this.ImpawardsId = string.Empty;
            this.KinopoiskId = string.Empty;
            this.TmdbId = string.Empty;
            this.OfdbId = string.Empty;
            this.SratimId = string.Empty;
            this.RottenTomatoId = string.Empty;

            this.NfoPathOnDisk = string.Empty;
            this.PosterPathOnDisk = string.Empty;
            this.FanartPathOnDisk = string.Empty;
            this.TrailerPathOnDisk = string.Empty;

            this.PropertyChanged += this.MovieModel_PropertyChanged;
        }

        #endregion

        #region Events

        /// <summary>
        /// The locked status changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler LockedStatusChanged;

        /// <summary>
        /// The marked status changed.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler MarkedStatusChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether ActorsEnabled.
        /// </summary>
        public bool ActorsEnabled
        {
            get
            {
                return !this.multiSelectModel;
            }
        }

        /// <summary>
        /// Gets or sets AllocineId.
        /// </summary>
        public string AllocineId
        {
            get
            {
                return this.allocineId;
            }

            set
            {
                this.allocineId = value;
                this.OnPropertyChanged("AllocineId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets the fanart.
        /// </summary>
        /// <value>The fanart.</value>
        public BindingList<ImageDetailsModel> AlternativeFanart { get; set; }

        /// <summary>
        /// Gets or sets the poster.
        /// </summary>
        /// <value>The poster.</value>
        public BindingList<ImageDetailsModel> AlternativePosters { get; set; }

        /// <summary>
        /// Gets or sets the trailer.
        /// </summary>
        /// <value>The poster.</value>
        public BindingList<TrailerDetailsModel> AlternativeTrailers { get; set; }

        /// <summary>
        /// Gets or sets AssociatedFiles.
        /// </summary>
        public AssociatedFilesModel AssociatedFiles { get; set; }

        /// <summary>
        /// Gets Busy.
        /// </summary>
        [JsonIgnore]
        public Image Busy
        {
            get
            {
                if (this.busy == false)
                {
                    return Resources.blank;
                }

                return Resources.smallanim;
            }
        }

        /// <summary>
        /// Gets or sets the cast.
        /// </summary>
        /// <value>The cast binding list.</value>
        public BindingList<PersonModel> Cast { get; set; }

        /// <summary>
        /// Gets or sets the certification.
        /// </summary>
        /// <value>The certification.</value>
        public string Certification
        {
            get
            {
                return this.certification;
            }

            set
            {
                if (this.certification != value)
                {
                    this.certification = value;
                    this.OnPropertyChanged("Certification");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ChangedFanart.
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
                    this.OnPropertyChanged("ChangedFanart");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [changed image].
        /// </summary>
        /// <value><c>true</c> if [changed image]; otherwise, <c>false</c>.</value>
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
        /// Gets or sets a value indicating whether [changed text].
        /// </summary>
        /// <value><c>true</c> if [changed text]; otherwise, <c>false</c>.</value>
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
        /// Gets or sets a value indicating whether [changed image].
        /// </summary>
        /// <value><c>true</c> if [changed image]; otherwise, <c>false</c>.</value>
        public bool ChangedTrailer
        {
            get
            {
                return this.changedTrailer;
            }

            set
            {
                if (this.changedTrailer != value)
                {
                    this.DatabaseSaved = false;
                    this.changedTrailer = value;
                    this.OnPropertyChanged("ChangedTrailer");
                }
            }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country binding list.</value>
        public BindingList<string> Country { get; set; }

        /// <summary>
        /// Gets or sets CountryAsString.
        /// </summary>
        public string CountryAsString
        {
            get
            {
                return this.Country.ToCommaList();
            }

            set
            {
                if (this.Country != value.ToBindingStringList())
                {
                    this.Country = value.ToBindingStringList();
                    this.OnPropertyChanged("CountryAsString");
                }
            }
        }

        /// <summary>
        /// Gets CurrentFanartImage.
        /// </summary>
        [JsonIgnore]
        public Image CurrentFanartImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.fanartPathOnDisk))
                {
                    url = Downloader.ProcessDownload(this.CurrentFanartImageUrl, DownloadType.Binary, Section.Movies);
                    this.FanartPathOnDisk = url;
                }
                else
                {
                    url = this.fanartPathOnDisk;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        /// Gets or sets CurrentFanartImageUrl.
        /// </summary>
        public string CurrentFanartImageUrl
        {
            get
            {
                return this.currentFanartImageUrl;
            }

            set
            {
                if (this.currentFanartImageUrl != value)
                {
                    this.currentFanartImageUrl = value;

                    this.GenerateSmallFanart();

                    this.OnPropertyChanged("CurrentFanartImageUrl");
                    this.OnPropertyChanged("CurrentFanartImage");
                    this.ChangedFanart = true;
                }
            }
        }

        /// <summary>
        /// Gets the current nfo body (loaded from disk)
        /// </summary>
        [JsonIgnore]
        public string CurrentNFO
        {
            get
            {
                if (!File.Exists(this.nfoPathOnDisk))
                {
                    return string.Empty;
                }

                return File.ReadAllText(this.nfoPathOnDisk);
            }
        }

        /// <summary>
        /// Gets CurrentPosterImage.
        /// </summary>
        [JsonIgnore]
        public Image CurrentPosterImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.posterPathOnDisk))
                {
                    url = Downloader.ProcessDownload(this.CurrentPosterImageUrl, DownloadType.Binary, Section.Movies);
                    this.posterPathOnDisk = url;
                }
                else
                {
                    url = this.posterPathOnDisk;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        /// Gets or sets CurrentPosterImageUrl.
        /// </summary>
        public string CurrentPosterImageUrl
        {
            get
            {
                return this.currentPosterImageUrl;
            }

            set
            {
                if (this.currentPosterImageUrl != value)
                {
                    this.currentPosterImageUrl = value;

                    this.GenerateSmallPoster();

                    this.OnPropertyChanged("CurrentPosterImageUrl");
                    this.OnPropertyChanged("CurrentPosterImage");
                    this.ChangedPoster = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets CurrentTrailerUrl.
        /// </summary>
        public string CurrentTrailerUrl
        {
            get
            {
                return this.currentTrailerUrl;
            }

            set
            {
                if (this.currentTrailerUrl != value)
                {
                    this.currentTrailerUrl = value;

                    this.OnPropertyChanged("CurrentTrailerUrl");
                    this.ChangedTrailer = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [database saved].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [database saved]; otherwise, <c>false</c>.
        /// </value>
        public bool DatabaseSaved { get; set; }

        /// <summary>
        /// Gets or sets the director.
        /// </summary>
        /// <value>The director.</value>
        public BindingList<PersonModel> Director { get; set; }

        /// <summary>
        /// Gets or sets DirectorAsString.
        /// </summary>
        public string DirectorAsString
        {
            get
            {
                return this.Director.ToString(',');
            }

            set
            {
                if (this.Director != value.ToPersonList())
                {
                    this.Director = value.ToPersonList();
                    this.OnPropertyChanged("DirectorAsString");
                }
            }
        }

        /// <summary>
        /// Gets FanartAltGallery.
        /// </summary>
        public GalleryItemGroup FanartAltGallery
        {
            get
            {
                var gallery = new GalleryItemGroup();

                foreach (ImageDetailsModel image in this.AlternativeFanart)
                {
                    string path = Downloader.ProcessDownload(
                        image.UriThumb.ToString(), DownloadType.Binary, Section.Movies);

                    if (File.Exists(path) && !Downloader.Downloading.Contains(path))
                    {
                        Image resizedimage = ImageHandler.LoadImage(path, 100, 60);

                        var galleryItem = new GalleryItem(resizedimage, string.Empty, image.Width + "x" + image.Height)
                            {
                               Tag = "movieFanart|" + image.UriFull 
                            };

                        gallery.Items.Add(galleryItem);
                    }
                }

                return gallery;
            }
        }

        /// <summary>
        /// Gets or sets FanartPathOnDisk.
        /// </summary>
        public string FanartPathOnDisk
        {
            get
            {
                return this.fanartPathOnDisk;
            }

            set
            {
                if (this.fanartPathOnDisk != value)
                {
                    this.fanartPathOnDisk = value;
                    this.OnPropertyChanged("FanartPathOnDisk");
                }
            }
        }

        /// <summary>
        /// Gets or sets the file info.
        /// </summary>
        /// <value>The file info.</value>
        public FileInfoModel FileInfo { get; set; }

        /// <summary>
        /// Gets or sets FilmAffinityId.
        /// </summary>
        public string FilmAffinityId
        {
            get
            {
                return this.filmAffinityId;
            }

            set
            {
                this.filmAffinityId = value;
                this.OnPropertyChanged("FilmAffinityId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets FilmDeltaId.
        /// </summary>
        public string FilmDeltaId
        {
            get
            {
                return this.filmDeltaId;
            }

            set
            {
                this.filmDeltaId = value;
                this.OnPropertyChanged("FilmDeltaId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets FilmUpId.
        /// </summary>
        public string FilmUpId
        {
            get
            {
                return this.filmUpId;
            }

            set
            {
                this.filmUpId = value;
                this.OnPropertyChanged("FilmUpId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets FilmWebId.
        /// </summary>
        public string FilmWebId
        {
            get
            {
                return this.filmWebId;
            }

            set
            {
                this.filmWebId = value;
                this.OnPropertyChanged("FilmWebId");
                this.OnPropertyChanged("Status");
            }
        }

        private string movieMeterId;

        /// <summary>
        /// Gets or sets the movie meter id.
        /// </summary>
        /// <value>
        /// The movie meter id.
        /// </value>
        public string MovieMeterId
        {
            get
            {
                return this.movieMeterId;
            }

            set
            {
                this.movieMeterId = value;
                this.OnPropertyChanged("MovieMeterId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>The genre binding list.</value>
        public BindingList<string> Genre { get; set; }

        /// <summary>
        /// Gets or sets GenreAsString.
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
                    this.OnPropertyChanged("GenreAsString");
                }
            }
        }

        /// <summary>
        /// Gets GetBaseFilePath.
        /// </summary>
        [JsonIgnore]
        public string GetBaseFilePath
        {
            get
            {
                return this.AssociatedFiles.GetMediaCollection()[0].FilePathFolder;
            }
        }

        /// <summary>
        /// Gets or sets the imdb id.
        /// </summary>
        /// <value>The imdb id.</value>
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
                    this.OnPropertyChanged("ImdbId");
                    this.OnPropertyChanged("Status");
                }
            }
        }

        /// <summary>
        /// Gets or sets ImpawardsId.
        /// </summary>
        public string ImpawardsId
        {
            get
            {
                return this.impawardsId;
            }

            set
            {
                this.impawardsId = value;
                this.OnPropertyChanged("ImpawardsId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.busy;
            }

            set
            {
                if (this.busy != value)
                {
                    this.busy = value;
                    this.OnPropertyChanged("IsBusy");
                    this.OnPropertyChanged("Busy");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsNew.
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.isNew;
            }

            set
            {
                if (this.isNew != value)
                {
                    this.isNew = value;
                    this.OnPropertyChanged("IsNew");
                }
            }
        }

        /// <summary>
        /// Gets or sets KinopoiskId.
        /// </summary>
        public string KinopoiskId
        {
            get
            {
                return this.kinopoiskId;
            }

            set
            {
                this.kinopoiskId = value;
                this.OnPropertyChanged("KinopoiskId");
                this.OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language binding list.</value>
        public BindingList<string> Language { get; set; }

        /// <summary>
        /// Gets or sets LanguageAsString.
        /// </summary>
        public string LanguageAsString
        {
            get
            {
                return this.Language.ToCommaList();
            }

            set
            {
                if (this.Language != value.ToBindingStringList())
                {
                    this.Language = value.ToBindingStringList();
                    this.OnPropertyChanged("LanguageAsString");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                if (this.locked != value)
                {
                    this.locked = value;
                    this.OnPropertyChanged("Locked");
                    this.OnPropertyChanged("LockedImage");
                }
            }
        }

        /// <summary>
        /// Gets LockedImage.
        /// </summary>
        [JsonIgnore]
        public Image LockedImage
        {
            get
            {
                if (this.Locked)
                {
                    return Resources.locked32;
                }

                return Resources.unlock32;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Marked.
        /// </summary>
        public bool Marked
        {
            get
            {
                return this.marked;
            }

            set
            {
                if (this.marked != value)
                {
                    this.marked = value;
                    this.OnPropertyChanged("Marked");
                    this.OnPropertyChanged("MarkedImage");
                }
            }
        }

        /// <summary>
        /// Gets MarkedImage.
        /// </summary>
        [JsonIgnore]
        public Image MarkedImage
        {
            get
            {
                if (this.Marked)
                {
                    return Resources.star_full32;
                }

                return Resources.star_empty32;
            }
        }

        /// <summary>
        /// Gets unique id for the movie object.
        /// </summary>
        public string MovieUniqueId { get; private set; }

        /// <summary>
        /// Gets or sets Mpaa.
        /// </summary>
        public string Mpaa
        {
            get
            {
                return this.mpaa;
            }

            set
            {
                if (this.mpaa != value)
                {
                    this.mpaa = value;
                    this.OnPropertyChanged("Runtime");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether MultiSelectModel.
        /// </summary>
        public bool MultiSelectModel
        {
            get
            {
                return this.multiSelectModel;
            }

            set
            {
                if (this.multiSelectModel != value)
                {
                    this.multiSelectModel = value;
                    this.OnPropertyChanged("MultiSelectModel");
                }
            }
        }

        /// <summary>
        /// Gets or sets Nfo Path.
        /// </summary>
        public string NfoPathOnDisk
        {
            get
            {
                return this.nfoPathOnDisk;
            }

            set
            {
                if (this.nfoPathOnDisk != value)
                {
                    this.nfoPathOnDisk = value;
                    this.OnPropertyChanged("NfoPathOnDisk");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Original title.
        /// </summary>
        /// <value>The Original title.</value>
        public string OriginalTitle
        {
            get
            {
                return this.originalTitle;
            }

            set
            {
                if (this.originalTitle != value)
                {
                    this.originalTitle = value;
                    this.OnPropertyChanged("OriginalTitle");
                }
            }
        }

        /// <summary>
        /// Gets or sets the outline.
        /// </summary>
        /// <value>The outline.</value>
        public string Outline
        {
            get
            {
                return this.outline;
            }

            set
            {
                if (this.outline != value)
                {
                    this.outline = value;
                    this.OnPropertyChanged("Outline");
                }
            }
        }

        /// <summary>
        /// Gets or sets the plot.
        /// </summary>
        /// <value>The plot value.</value>
        public string Plot
        {
            get
            {
                return this.plot;
            }

            set
            {
                if (this.plot != value)
                {
                    this.plot = value;
                    this.OnPropertyChanged("Plot");
                }
            }
        }

        /// <summary>
        /// Gets PosterAltGallery.
        /// </summary>
        public GalleryItemGroup PosterAltGallery
        {
            get
            {
                var gallery = new GalleryItemGroup();

                foreach (ImageDetailsModel image in this.AlternativePosters)
                {
                    string path = Downloader.ProcessDownload(
                        image.UriThumb.ToString(), DownloadType.Binary, Section.Movies);

                    if (File.Exists(path) && !Downloader.Downloading.Contains(path))
                    {
                        Image resizedimage = ImageHandler.LoadImage(path, 100, 160);

                        var galleryItem = new GalleryItem(resizedimage, string.Empty, image.Width + "x" + image.Height)
                            {
                               Tag = "moviePoster|" + image.UriFull 
                            };

                        gallery.Items.Add(galleryItem);
                    }
                }

                return gallery;
            }
        }

        /// <summary>
        /// Gets or sets PosterPathOnDisk.
        /// </summary>
        public string PosterPathOnDisk
        {
            get
            {
                return this.posterPathOnDisk;
            }

            set
            {
                this.posterPathOnDisk = value;
                this.OnPropertyChanged("PosterPathOnDisk");
            }
        }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating.</value>
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
                    this.OnPropertyChanged("Rating");
                }
            }
        }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>The release date.</value>
        public DateTime? ReleaseDate
        {
            get
            {
                return this.releaseDate;
            }

            set
            {
                if (this.releaseDate != value)
                {
                    this.releaseDate = value;
                    this.OnPropertyChanged("ReleaseDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the runtime.
        /// </summary>
        /// <value>The runtime.</value>
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
                    this.OnPropertyChanged("Runtime");
                    this.OnPropertyChanged("RuntimeInHourMin");
                }
            }
        }

        /// <summary>
        /// Gets or sets RuntimeInHourMin.
        /// </summary>
        public string RuntimeInHourMin
        {
            get
            {
                if (this.Runtime == null)
                {
                    return null;
                }

                TimeSpan timeSpan = TimeSpan.FromMinutes((double)this.Runtime);

                return string.Format("{0}h {1:d2}m", timeSpan.Hours, timeSpan.Minutes);
            }

            set
            {
                if (value == null)
                {
                    this.Runtime = null;
                    return;
                }

                Match match = Regex.Match(value, @"(?<hour>\d)h\s(?<minute>\d{2})m", RegexOptions.None);

                if (match.Success)
                {
                    int hourOut;
                    int minOut;

                    int.TryParse(match.Groups["hour"].Value, out hourOut);
                    int.TryParse(match.Groups["minute"].Value, out minOut);

                    var timeSpan = new TimeSpan(hourOut, minOut, 0);

                    var totalMinutes = (int)timeSpan.TotalMinutes;

                    this.runtime = totalMinutes;
                }
            }
        }

        /// <summary>
        /// Gets or sets ScraperGroup.
        /// </summary>
        public string ScraperGroup
        {
            get
            {
                return this.scraperGroup;
            }

            set
            {
                if (this.scraperGroup != value)
                {
                    this.scraperGroup = value;
                    this.OnPropertyChanged("SmallPoster");
                }
            }
        }

        /// <summary>
        /// Gets or sets SetStudio.
        /// </summary>
        public string SetStudio
        {
            get
            {
                return this.setStudio;
            }

            set
            {
                if (this.setStudio != value)
                {
                    this.setStudio = value;
                    this.OnPropertyChanged("SetStudio");
                }
            }
        }

        /// <summary>
        /// Gets or sets SmallFanart.
        /// </summary>
        [JsonIgnore]
        public Image SmallFanart
        {
            get
            {
                return this.smallFanart;
            }

            set
            {
                if (this.smallFanart != value)
                {
                    this.smallFanart = value;
                    this.OnPropertyChanged("SmallFanart");
                }
            }
        }

        /// <summary>
        /// Gets or sets SmallPoster.
        /// </summary>
        [JsonIgnore]
        public Image SmallPoster
        {
            get
            {
                return this.smallPoster;
            }

            set
            {
                if (this.smallPoster != value)
                {
                    this.smallPoster = value;
                    this.OnPropertyChanged("SmallPoster");
                }
            }
        }

        /// <summary>
        /// Gets Status.
        /// </summary>
        [JsonIgnore]
        public Image Status
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImdbId) || string.IsNullOrEmpty(this.TmdbId))
                {
                    return Resources.promo_red16;
                }

                return Resources.promo_green16;
            }
        }

        /// <summary>
        /// Gets or sets the studio.
        /// </summary>
        /// <value>The studio.</value>
        public BindingList<string> Studios { get; set; }

        /// <summary>
        /// Gets or sets the tagline.
        /// </summary>
        /// <value>The tagline.</value>
        public string Tagline
        {
            get
            {
                return this.tagline;
            }

            set
            {
                if (this.tagline != value)
                {
                    this.tagline = value;
                    this.OnPropertyChanged("Tagline");
                }
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.ChangedText = true;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether TitleEnabled.
        /// </summary>
        public bool TitleEnabled
        {
            get
            {
                return !this.multiSelectModel;
            }
        }

        /// <summary>
        /// Gets or sets TmdbId.
        /// </summary>
        public string TmdbId
        {
            get
            {
                return this.tmdbId;
            }

            set
            {
                if (this.tmdbId != value)
                {
                    this.tmdbId = value;
                    this.OnPropertyChanged("TmdbId");
                    this.OnPropertyChanged("Status");
                }
            }
        }

        /// <summary>
        /// Gets or sets the top250.
        /// </summary>
        /// <value>The top250.</value>
        public int? Top250
        {
            get
            {
                return this.top250;
            }

            set
            {
                if (this.top250 != value)
                {
                    this.top250 = value;
                    this.OnPropertyChanged("Top250");
                }
            }
        }

        /// <summary>
        /// Gets or sets TrailerPathOnDisk.
        /// </summary>
        public string TrailerPathOnDisk
        {
            get
            {
                return this.trailerPathOnDisk;
            }

            set
            {
                if (this.trailerPathOnDisk != value)
                {
                    this.trailerPathOnDisk = value;
                    this.OnPropertyChanged("TrailerPathOnDisk");
                }
            }
        }

        /// <summary>
        /// Gets or sets VideoSource.
        /// </summary>
        public string VideoSource
        {
            get
            {
                return this.videoSource;
            }

            set
            {
                if (this.videoSource != value)
                {
                    this.videoSource = value;
                    this.OnPropertyChanged("VideoSource");
                }
            }
        }

        /// <summary>
        /// Gets or sets the votes.
        /// </summary>
        /// <value>The votes.</value>
        public long? Votes
        {
            get
            {
                return this.votes;
            }

            set
            {
                if (this.votes != value)
                {
                    this.votes = value;
                    this.OnPropertyChanged("Votes");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Watched.
        /// </summary>
        public bool Watched
        {
            get
            {
                return this.watched;
            }

            set
            {
                this.watched = value;
            }
        }

        /// <summary>
        /// Gets or sets the writers.
        /// </summary>
        /// <value>The writers.</value>
        public BindingList<PersonModel> Writers { get; set; }

        /// <summary>
        /// Gets or sets WritersAsString.
        /// </summary>
        public string WritersAsString
        {
            get
            {
                return this.Writers.ToString(',');
            }

            set
            {
                if (this.Writers != value.ToPersonList())
                {
                    this.Writers = value.ToPersonList();
                    this.OnPropertyChanged("WritersAsString");
                }
            }
        }

        /// <summary>
        /// Gets YamjXml.
        /// </summary>
        public string YamjXml
        {
            get
            {
                var yamj = new YAMJ();
                return yamj.GenerateMovieOutput(this);
            }
        }

        /// <summary>
        /// Gets or sets YanfoeID.
        /// </summary>
        public string YanfoeID
        {
            get
            {
                return this.yanfoeID;
            }

            set
            {
                if (this.yanfoeID != value)
                {
                    this.yanfoeID = value;
                    this.OnPropertyChanged("SmallPoster");
                }
            }
        }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year value</value>
        public int? Year
        {
            get
            {
                return this.year;
            }

            set
            {
                if (this.year != value)
                {
                    this.year = value;
                    this.OnPropertyChanged("Year");
                }
            }
        }

        private string ofdbbId;

        public string OfdbId
        {
            get
            {
                return this.ofdbbId;
            }

            set
            {
                this.ofdbbId = value;
                this.OnPropertyChanged("OfdbId");
                this.OnPropertyChanged("Status");
            }
        }

        private string sratimId;

        public string SratimId
        {
            get
            {
                return this.sratimId;
            }

            set
            {
                this.sratimId = value;
                this.OnPropertyChanged("SratimId");
                this.OnPropertyChanged("Status");
            }
        }

        private string rottenTomatoId;

        public string RottenTomatoId
        {
            get
            {
                return this.rottenTomatoId;
            }

            set
            {
                this.rottenTomatoId = value;
                this.OnPropertyChanged("RottenTomatoId");
                this.OnPropertyChanged("Status");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The generate small fanart.
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        public void GenerateSmallFanart(string path = null)
        {
            this.SmallFanart = string.IsNullOrEmpty(path)
                                   ? Tools.ResizeImage(this.CurrentFanartImage, 100, 60)
                                   : ImageHandler.LoadImage(path, 100, 60);
        }

        /// <summary>
        /// The generate small poster.
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        public void GenerateSmallPoster(string path = null)
        {
            this.SmallPoster = string.IsNullOrEmpty(path)
                                   ? Tools.ResizeImage(this.CurrentPosterImage, 100, 150)
                                   : ImageHandler.LoadImage(path, 100, 150);
        }

        /// <summary>
        /// Gets the super tip for the movie
        /// </summary>
        /// <returns>
        /// A supertip object
        /// </returns>
        public SuperToolTip GetMovieSuperTip()
        {
            var superTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };

            string yearValue;

            if (string.IsNullOrEmpty(this.Year.ToString()))
            {
                yearValue = " (No year found)";
            }
            else
            {
                yearValue = " (" + this.year + ")";
            }

            superTip.Items.AddTitle(this.Title + yearValue);

            var item = new ToolTipTitleItem { Image = this.SmallPoster };

            var sb = new StringBuilder();

            if (item.Image == null)
            {
                sb.Append("<b>No Poster Found</b>" + Environment.NewLine);
            }

            sb.Append(Environment.NewLine);

            if (!string.IsNullOrEmpty(this.ImdbId))
            {
                sb.Append("<b>Imdb ID:</b> " + this.ImdbId + Environment.NewLine);
            }

            if (!string.IsNullOrEmpty(this.TmdbId))
            {
                sb.Append("<b>Tmdb ID:</b> " + this.TmdbId + Environment.NewLine);
            }

            sb.Append(Environment.NewLine);

            if (this.ChangedText)
            {
                sb.Append("<b>Text</b> not saved." + Environment.NewLine);
            }

            if (this.ChangedPoster)
            {
                sb.Append("<b>Poster</b> not saved." + Environment.NewLine);
            }

            if (this.ChangedPoster)
            {
                sb.Append("<b>Fanart</b> not saved." + Environment.NewLine);
            }

            List<string> sets = MovieSetManager.GetSetsContainingMovie(this);

            if (sets.Count > 0)
            {
                sb.Append(Environment.NewLine + "<b>In the following sets:</b>" + Environment.NewLine);

                foreach (string set in sets)
                {
                    sb.Append(set + Environment.NewLine);
                }
            }

            item.Text = sb.ToString();

            superTip.Items.Add(item);

            if (this.smallFanart != null)
            {
                superTip.Items.Add(new ToolTipTitleItem { Image = this.smallFanart });
            }
            else
            {
                superTip.Items.Add("<b>No Fanart Found</b>");
            }

            return superTip;
        }

        #endregion

        #region Implemented Interfaces

        #region IDXDataErrorInfo

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">
        /// An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.
        /// </param>
        public void GetError(ErrorInfo info)
        {
        }

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a specific business object's property.
        /// </summary>
        /// <param name="propertyName">
        /// A string that identifies the name of the property for which information on an error is to be returned.
        /// </param>
        /// <param name="info">
        /// An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.
        /// </param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "Title":
                    if (this.Title == string.Empty)
                    {
                        info.ErrorText = "Title must not be empty";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;

                case "Year":
                    if (this.Year.ToString().Length != 4)
                    {
                        info.ErrorText = string.Format(
                            "This year is only {0} numbers long. It needs to be 4", this.Year.ToString().Length);

                        info.ErrorType = ErrorType.Critical;
                    }

                    break;

                case "YanfoeID":
                    if (string.IsNullOrEmpty(this.YanfoeID))
                    {
                        info.ErrorText =
                            "No YANFOE MovieUniqueId. It is recommended to use the quick search functionality to retrieve this value.";
                        info.ErrorType = ErrorType.Information;
                    }

                    break;

                case "ImdbId":
                    if (string.IsNullOrEmpty(this.YanfoeID))
                    {
                        info.ErrorText =
                            "No IMDB MovieUniqueId. It is recommended to use the quick search functionality to retrieve this value.";
                        info.ErrorType = ErrorType.Information;
                    }

                    break;
            }
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The on locked status changed.
        /// </summary>
        protected void OnLockedStatusChanged()
        {
            EventHandler handler = this.LockedStatusChanged;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// The on marked status changed.
        /// </summary>
        protected void OnMarkedStatusChanged()
        {
            EventHandler handler = this.MarkedStatusChanged;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the MovieModel control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void MovieModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!this.changeList.Contains(e.PropertyName))
            {
                this.changeList.Add(e.PropertyName);
            }
        }

        #endregion
    }
}