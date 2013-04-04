// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieDBFactory.cs">
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
//   Data access layer for all movie related data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.UI;
    using YANFOE.UI.UserControls.CommonControls;

    #endregion

    /// <summary>
    ///   Data access layer for all movie related data.
    /// </summary>
    public class MovieDBFactory : FactoryBase
    {
        #region Static Fields

        /// <summary>
        ///   The instance.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", 
            Justification = "Implements Singleton.")]
        public static MovieDBFactory Instance = new MovieDBFactory();

        #endregion

        #region Fields

        /// <summary>
        ///   The gallery group.
        /// </summary>
        private readonly GalleryItemGroup galleryGroup;

        /// <summary>
        ///   The in gallery.
        /// </summary>
        private readonly ThreadedBindingList<string> inGallery;

        /// <summary>
        ///   The current movie present in the main movie window.
        /// </summary>
        private MovieModel currentMovie;

        /// <summary>
        ///   Gets or sets the database of duplicated movies.
        /// </summary>
        /// <value> The database of duplicated movies. </value>
        private ThreadedBindingList<MovieModel> duplicatedMoviesDatabase;

        /// <summary>
        ///   The hidden movie database.
        /// </summary>
        private ThreadedBindingList<MovieModel> hiddenMovieDatabase;

        /// <summary>
        ///   The ignore multi select.
        /// </summary>
        private bool ignoreMultiSelect;

        /// <summary>
        ///   Gets or sets ImportProgressCurrent.
        /// </summary>
        private int importProgressCurrent;

        /// <summary>
        ///   Gets or sets ImportProgressMaximum.
        /// </summary>
        private int importProgressMaximum;

        /// <summary>
        ///   Gets or sets ImportProgressStatus.
        /// </summary>
        private string importProgressStatus;

        /// <summary>
        ///   The is multi selected.
        /// </summary>
        private bool isMultiSelected;

        /// <summary>
        ///   Gets or sets the movie database.
        /// </summary>
        /// <value> The movie database. </value>
        private ThreadedBindingList<MovieModel> movieDatabase;

        /// <summary>
        ///   The multi select.
        /// </summary>
        private MovieModel multiSelect;

        /// <summary>
        ///   The multi selected movies.
        /// </summary>
        private ThreadedBindingList<MovieModel> multiSelectedMovies;

        /// <summary>
        ///   The temp scraper group.
        /// </summary>
        private string tempScraperGroup;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Prevents a default instance of the <see cref="MovieDBFactory" /> class from being created. 
        ///   Initializes static members of the <see cref="MovieDBFactory" /> class.
        /// </summary>
        private MovieDBFactory()
        {
            this.MovieDatabase = new ThreadedBindingList<MovieModel>();
            this.HiddenMovieDatabase = new ThreadedBindingList<MovieModel>();
            this.DuplicatedMoviesDatabase = new ThreadedBindingList<MovieModel>();
            this.currentMovie = new MovieModel();
            this.galleryGroup = new GalleryItemGroup();
            this.multiSelectedMovies = new ThreadedBindingList<MovieModel>();
            this.TempScraperGroup = string.Empty;

            this.inGallery = new ThreadedBindingList<string>();

            this.MovieDatabase.ListChanged += this.MovieDatabaseListChanged;

            this.multiSelectedMovies.ListChanged += this.MultiSelectedMoviesListChanged;
            this.MovieDatabase.ListChanged += this.MovieDBListChanged;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when [current movie changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler CurrentMovieChanged = delegate { };

        /// <summary>
        ///   Occurs when [current movie value changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler CurrentMovieValueChanged = delegate { };

        /// <summary>
        ///   Occurs when [movie database changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DatabaseChanged = delegate { };

        /// <summary>
        ///   Occurs when [displayed database values require refresh].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler DatabaseValuesRefreshRequired = delegate { };

        /// <summary>
        ///   Occurs when [fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler FanartLoaded = delegate { };

        /// <summary>
        ///   Occurs when [fanart loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler FanartLoading = delegate { };

        /// <summary>
        ///   Occurs when [movie gallery changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler GalleryChanged = delegate { };

        /// <summary>
        ///   Occurs when [multi selected values changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler MultiSelectedValuesChanged = delegate { };

        /// <summary>
        ///   Occurs when [poster loaded].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler PosterLoaded = delegate { };

        /// <summary>
        ///   Occurs when [poster loading].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler PosterLoading = delegate { };

        #endregion

        #region Enums

        /// <summary>
        ///   The movie DB types.
        /// </summary>
        public enum MovieDBTypes
        {
            /// <summary>
            ///   The movies.
            /// </summary>
            Movies = 0, 

            /// <summary>
            ///   The duplicates.
            /// </summary>
            Duplicates
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the duplicated movies database.
        /// </summary>
        public ThreadedBindingList<MovieModel> DuplicatedMoviesDatabase
        {
            get
            {
                return this.duplicatedMoviesDatabase;
            }

            set
            {
                this.duplicatedMoviesDatabase = value;
                this.OnPropertyChanged("DuplicatedMoviesDatabase");
            }
        }

        /// <summary>
        ///   Gets or sets the hidden movie database.
        /// </summary>
        public ThreadedBindingList<MovieModel> HiddenMovieDatabase
        {
            get
            {
                return this.hiddenMovieDatabase;
            }

            set
            {
                this.hiddenMovieDatabase = value;
                this.OnPropertyChanged("HiddenMovieDatabase");
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ignore multi select.
        /// </summary>
        public bool IgnoreMultiSelect
        {
            get
            {
                return this.ignoreMultiSelect;
            }

            set
            {
                this.ignoreMultiSelect = value;
                this.OnPropertyChanged("IgnoreMultiSelect");
            }
        }

        /// <summary>
        ///   Gets or sets the import progress current.
        /// </summary>
        public int ImportProgressCurrent
        {
            get
            {
                return this.importProgressCurrent;
            }

            set
            {
                this.importProgressCurrent = value;
                this.OnPropertyChanged("ImportProgressCurrent");
            }
        }

        /// <summary>
        ///   Gets or sets the import progress maximum.
        /// </summary>
        public int ImportProgressMaximum
        {
            get
            {
                return this.importProgressMaximum;
            }

            set
            {
                this.importProgressMaximum = value;
                this.OnPropertyChanged("ImportProgressMaximum");
            }
        }

        /// <summary>
        ///   Gets or sets the import progress status.
        /// </summary>
        public string ImportProgressStatus
        {
            get
            {
                return this.importProgressStatus;
            }

            set
            {
                this.importProgressStatus = value;
                this.OnPropertyChanged("ImportProgressStatus");
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether IsMultiSelected.
        /// </summary>
        public bool IsMultiSelected
        {
            get
            {
                this.multiSelect = new MovieModel { Title = "Multiple Movies Selected" };
                return this.isMultiSelected;
            }

            set
            {
                this.isMultiSelected = value;
                this.OnPropertyChanged("IsMultiSelected");
            }
        }

        /// <summary>
        ///   Gets or sets the movie database.
        /// </summary>
        public ThreadedBindingList<MovieModel> MovieDatabase
        {
            get
            {
                return this.movieDatabase;
            }

            set
            {
                this.movieDatabase = value;
                this.OnPropertyChanged("MovieDatabase");
            }
        }

        /// <summary>
        ///   Gets or sets MultiSelectedMovies.
        /// </summary>
        public ThreadedBindingList<MovieModel> MultiSelectedMovies
        {
            get
            {
                return this.multiSelectedMovies;
            }

            set
            {
                this.multiSelectedMovies = value;
                this.OnPropertyChanged("MultiSelectedMovies");
            }
        }

        /// <summary>
        ///   Gets or sets the temp scraper group.
        /// </summary>
        public string TempScraperGroup
        {
            get
            {
                return this.tempScraperGroup;
            }

            set
            {
                this.tempScraperGroup = value;
                this.OnPropertyChanged("TempScraperGroup");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Check if Fanart has downloaded.
        /// </summary>
        /// <returns> Downloaded status </returns>
        public bool FanartDownloaded()
        {
            string url = this.currentMovie.CurrentFanartImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   Gets the current movie.
        /// </summary>
        /// <returns> The current movie </returns>
        public MovieModel GetCurrentMovie()
        {
            return this.currentMovie;
        }

        /// <summary>
        ///   Download fanart
        /// </summary>
        public void GetFanart()
        {
            if (this.FanartDownloaded())
            {
                this.InvokeFanartLoaded(new EventArgs());
                return;
            }

            this.InvokeFanartLoading(new EventArgs());

            var bgwFanart = new BackgroundWorker();

            bgwFanart.DoWork += this.BgwFanart_DoWork;
            bgwFanart.RunWorkerCompleted += this.BgwFanartRunWorkerCompleted;
            bgwFanart.RunWorkerAsync(this.currentMovie.CurrentFanartImageUrl);
        }

        /// <summary>
        ///   Get gallery group.
        /// </summary>
        /// <returns> The gallery group. </returns>
        public GalleryItemGroup GetGalleryGroup()
        {
            return this.galleryGroup;
        }

        /// <summary>
        /// Gets the movie based on its Id
        /// </summary>
        /// <param name="id">
        /// The Id value. 
        /// </param>
        /// <returns>
        /// A movie object. 
        /// </returns>
        public MovieModel GetMovie(string id)
        {
            try
            {
                return (from m in this.MovieDatabase where m.MovieUniqueId == id select m).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///   The get poster.
        /// </summary>
        public void GetPoster()
        {
            if (this.PosterDownloaded())
            {
                this.InvokePosterLoaded(new EventArgs());
                return;
            }

            this.InvokePosterLoading(new EventArgs());

            var bgwPoster = new BackgroundWorker();

            bgwPoster.DoWork += this.BgwPoster_DoWork;
            bgwPoster.RunWorkerCompleted += this.BgwPosterRunWorkerCompleted;
            bgwPoster.RunWorkerAsync(this.currentMovie.CurrentPosterImageUrl);
        }

        /// <summary>
        /// The hide movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        public void HideMovie(MovieModel movieModel)
        {
            this.HiddenMovieDatabase.Add(movieModel);
            movieModel.Hidden = true;
            this.MovieDatabase.Remove(movieModel);

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        /// Invokes the current movie changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeCurrentMovieChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentMovieChanged;
            if (handler != null)
            {
                handler(null, e);
            }

            this.currentMovie.PropertyChanged += this.MultiSelectPropertyChanged;
        }

        /// <summary>
        /// Invokes the current movie value changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeCurrentMovieValueChanged(EventArgs e)
        {
            EventHandler handler = this.CurrentMovieValueChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// The invoke database changed.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        public void InvokeDatabaseChanged(EventArgs e)
        {
            EventHandler handler = this.DatabaseChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the fanart loaded.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeFanartLoaded(EventArgs e)
        {
            EventHandler handler = this.FanartLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the fanart loading.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokeFanartLoading(EventArgs e)
        {
            EventHandler handler = this.FanartLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the poster loaded.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokePosterLoaded(EventArgs e)
        {
            EventHandler handler = this.PosterLoaded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the poster loading.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public void InvokePosterLoading(EventArgs e)
        {
            EventHandler handler = this.PosterLoading;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Is same as current movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        /// <returns>
        /// The is same as current movie. 
        /// </returns>
        public bool IsSameAsCurrentMovie(MovieModel movieModel)
        {
            return movieModel.MovieUniqueId == this.currentMovie.MovieUniqueId;
        }

        /// <summary>
        ///   The load fanart.
        /// </summary>
        /// <returns> Image object of fanart </returns>
        public Image LoadFanart()
        {
            if (!string.IsNullOrEmpty(this.currentMovie.FanartPathOnDisk)
                && File.Exists(this.currentMovie.FanartPathOnDisk))
            {
                return ImageHandler.LoadImage(this.currentMovie.FanartPathOnDisk);
            }

            string url = this.currentMovie.CurrentFanartImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            if (!File.Exists(urlCache) || Downloader.Downloading.Contains(url))
            {
                return null;
            }

            return ImageHandler.LoadImage(urlCache);
        }

        /// <summary>
        ///   The load poster.
        /// </summary>
        /// <returns> Image object of poster </returns>
        public Image LoadPoster()
        {
            if (!string.IsNullOrEmpty(this.currentMovie.PosterPathOnDisk)
                && File.Exists(this.currentMovie.PosterPathOnDisk))
            {
                return ImageHandler.LoadImage(this.currentMovie.PosterPathOnDisk);
            }

            string urlCache = WebCache.GetPathFromUrl(this.currentMovie.CurrentPosterImageUrl, Section.Movies);

            if (!File.Exists(urlCache) || Downloader.Downloading.Contains(this.currentMovie.CurrentPosterImageUrl))
            {
                return null;
            }

            return ImageHandler.LoadImage(urlCache);
        }

        /// <summary>
        /// The merge with database.
        /// </summary>
        /// <param name="importDatabase">
        /// The import database. 
        /// </param>
        /// <param name="type">
        /// The type. 
        /// </param>
        public void MergeWithDatabase(
            ThreadedBindingList<MovieModel> importDatabase, MovieDBTypes type = MovieDBTypes.Movies)
        {
            foreach (MovieModel movie in importDatabase)
            {
                if (movie.SmallPoster != null)
                {
                    movie.SmallPoster = ImageHandler.ResizeImage(movie.SmallPoster, 100, 150);
                }

                switch (type)
                {
                    case MovieDBTypes.Movies:
                        this.MovieDatabase.Add(movie);
                        break;
                    case MovieDBTypes.Duplicates:
                        this.DuplicatedMoviesDatabase.Add(movie);
                        break;
                }
            }

            if (type == MovieDBTypes.Movies)
            {
                MediaPathDBFactory.Instance.MediaPathMoviesUnsorted.Clear();
                this.GeneratePictureGallery();
            }
        }

        /// <summary>
        /// Handles the PropertyChanged event of the MultiSelect control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data. 
        /// </param>
        public void MultiSelectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.isMultiSelected && !this.IgnoreMultiSelect)
            {
                foreach (MovieModel movie in this.multiSelectedMovies)
                {
                    if (e.PropertyName == "Marked")
                    {
                        if (this.currentMovie.Marked != movie.Marked)
                        {
                            movie.Marked = this.currentMovie.Marked;
                        }
                    }

                    if (e.PropertyName == "Locked")
                    {
                        if (this.currentMovie.Locked != movie.Locked)
                        {
                            movie.Locked = this.currentMovie.Locked;
                        }
                    }

                    if (e.PropertyName == "Year")
                    {
                        if (this.currentMovie.Year != movie.Year)
                        {
                            movie.Year = this.currentMovie.Year;
                        }
                    }

                    if (e.PropertyName == "Plot")
                    {
                        if (this.currentMovie.Plot != movie.Plot)
                        {
                            movie.Plot = this.currentMovie.Plot;
                        }
                    }
                }

                this.DatabaseValuesRefreshRequired(null, null);
            }
        }

        /// <summary>
        /// The overwrite database.
        /// </summary>
        /// <param name="database">
        /// The database. 
        /// </param>
        public void OverwriteDatabase(ThreadedBindingList<MovieModel> database)
        {
            this.MovieDatabase = database;
            this.DatabaseChanged(null, null);
        }

        /// <summary>
        ///   Check if Poster has downloaded.
        /// </summary>
        /// <returns> Downloaded status </returns>
        public bool PosterDownloaded()
        {
            string url = this.currentMovie.CurrentPosterImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            return File.Exists(urlCache);
        }

        /// <summary>
        ///   The remove missing movies.
        /// </summary>
        public void RemoveMissingMovies()
        {
            var toDelete =
                this.MovieDatabase.Where(
                    movie => movie.AssociatedFiles.Media.Any(file => !File.Exists(file.PathAndFilename))).ToList();

            if (toDelete.Count > 0)
            {
                // var frmMissingFilesMovies = new FrmMissingFilesMovies(toDelete);
                // frmMissingFilesMovies.ShowDialog();
            }
        }

        /// <summary>
        /// The remove movie.
        /// </summary>
        /// <param name="movie">
        /// The movie. 
        /// </param>
        public void RemoveMovie(MovieModel movie)
        {
            foreach (var file in movie.AssociatedFiles.Media)
            {
                var fileEntry =
                    (from f in MasterMediaDBFactory.MasterMovieMediaDatabase
                     where f.PathAndFilename == file.PathAndFilename
                     select f).SingleOrDefault();

                if (fileEntry != null)
                {
                    MasterMediaDBFactory.MasterMovieMediaDatabase.Remove(fileEntry);
                }
            }

            this.MovieDatabase.Remove(movie);

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        /// The replace movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        public void ReplaceMovie(MovieModel movieModel)
        {
            lock (this.MovieDatabase)
            {
                var getMovieInDatabase =
                    this.MovieDatabase.ToList().FindIndex(w => w.MovieUniqueId == movieModel.MovieUniqueId);

                if (getMovieInDatabase == -1)
                {
                    return;
                }

                movieModel.IsBusy = false;

                movieModel.IsBusy = false;

                // TODO: This causes a weird bug where you can't just replace the movie in the database or it doesn't allow you to select the movie in the list anymore.
                this.MovieDatabase.RemoveAt(getMovieInDatabase);
                this.MovieDatabase.Insert(getMovieInDatabase, movieModel);

                // MovieDatabase[getMovieInDatabase] = movieModel
                if (movieModel.MovieUniqueId == this.currentMovie.MovieUniqueId)
                {
                    this.SetCurrentMovie(movieModel);
                    this.InvokeCurrentMovieChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// The restore hidden movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        public void RestoreHiddenMovie(MovieModel movieModel)
        {
            this.HiddenMovieDatabase.Remove(movieModel);
            movieModel.Hidden = false;
            this.MovieDatabase.Add(movieModel);

            DatabaseIOFactory.DatabaseDirty = true;
        }

        /// <summary>
        /// The set current movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        public void SetCurrentMovie(MovieModel movieModel)
        {
            if (movieModel.MultiSelectModel)
            {
                this.PopulateMultiSelectMovieModel(movieModel);
            }

            this.currentMovie = movieModel;
            this.InvokeCurrentMovieChanged(new EventArgs());
        }

        /// <summary>
        /// The set current movie.
        /// </summary>
        /// <param name="id">
        /// The movie ID 
        /// </param>
        public void SetCurrentMovie(string id)
        {
            if (this.IsMultiSelected)
            {
                this.currentMovie = this.multiSelect;
                this.currentMovie.PropertyChanged += this.MultiSelectMoviePropertyChanged;
            }
            else
            {
                MovieModel movie = (from m in this.MovieDatabase where m.MovieUniqueId == id select m).SingleOrDefault();

                if (movie != null)
                {
                    this.SetCurrentMovie(movie);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Fanart control.
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
        /// Handles the DoWork event of the Fanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwFanart_DoWork(object sender, DoWorkEventArgs e)
        {
            var url = e.Argument as string;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Tv);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);
                e.Result = path;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Poster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwPosterRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InvokePosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Handles the DoWork event of the Poster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwPoster_DoWork(object sender, DoWorkEventArgs e)
        {
            var url = e.Argument as string;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            if (!File.Exists(urlCache))
            {
                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Movies);
                e.Result = path;
            }
        }

        /// <summary>
        /// Check if a value matches multi-selected movies
        /// </summary>
        /// <param name="scraperGroupBaseValue">
        /// The scraper group base value. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="scraperGroupIsSame">
        /// The scraper group is same. 
        /// </param>
        /// <returns>
        /// Date time value 
        /// </returns>
        private DateTime? CheckMultiSelectDateValue(
            DateTime? scraperGroupBaseValue, DateTime? value, out bool scraperGroupIsSame)
        {
            scraperGroupIsSame = true;

            if (scraperGroupBaseValue == null)
            {
                scraperGroupBaseValue = value;

                return scraperGroupBaseValue;
            }

            if (scraperGroupBaseValue != value)
            {
                scraperGroupIsSame = false;
            }

            return scraperGroupBaseValue;
        }

        /// <summary>
        /// Check if a value matches multi-selected movies
        /// </summary>
        /// <param name="scraperGroupBaseValue">
        /// The scraper group base value. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="scraperGroupIsSame">
        /// The scraper group is same. 
        /// </param>
        /// <returns>
        /// Double value 
        /// </returns>
        private double? CheckMultiSelectDoubleValue(
            double? scraperGroupBaseValue, double? value, out bool scraperGroupIsSame)
        {
            scraperGroupIsSame = true;

            if (scraperGroupBaseValue == null)
            {
                scraperGroupBaseValue = value;

                return scraperGroupBaseValue;
            }

            if (scraperGroupBaseValue != value)
            {
                scraperGroupIsSame = false;
            }

            return scraperGroupBaseValue;
        }

        /// <summary>
        /// Secondary method for PopulateMultiSelectMovieModel for comparing integer collections
        /// </summary>
        /// <param name="scraperGroupBaseValue">
        /// The scraper Group Base Value. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="scraperGroupIsSame">
        /// The scraper Group Is Same. 
        /// </param>
        /// <returns>
        /// Returned Integer value 
        /// </returns>
        private int? CheckMultiSelectIntValue(int? scraperGroupBaseValue, int? value, out bool scraperGroupIsSame)
        {
            scraperGroupIsSame = true;

            if (scraperGroupBaseValue == null)
            {
                scraperGroupBaseValue = value;

                return scraperGroupBaseValue;
            }

            if (scraperGroupBaseValue != value)
            {
                scraperGroupIsSame = false;
            }

            return scraperGroupBaseValue;
        }

        /// <summary>
        /// Secondary method for PopulateMultiSelectMovieModel for comparing string collections
        /// </summary>
        /// <param name="scraperGroupBaseValue">
        /// The scraper Group Base Value. 
        /// </param>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="scraperGroupIsSame">
        /// The scraper Group Is Same. 
        /// </param>
        /// <returns>
        /// The check multi select string value. 
        /// </returns>
        private string CheckMultiSelectStringValue(
            string scraperGroupBaseValue, string value, out bool scraperGroupIsSame)
        {
            scraperGroupIsSame = true;

            if (scraperGroupBaseValue == string.Empty)
            {
                scraperGroupBaseValue = value;

                return scraperGroupBaseValue;
            }

            if (scraperGroupBaseValue != value)
            {
                scraperGroupIsSame = false;
            }

            return scraperGroupBaseValue;
        }

        /// <summary>
        ///   The generate picture gallery.
        /// </summary>
        private void GeneratePictureGallery()
        {
            bool changed = false;

            foreach (MovieModel movie in this.MovieDatabase)
            {
                if (!this.inGallery.Contains(movie.MovieUniqueId))
                {
                    if (movie.SmallPoster != null)
                    {
                        var superTip = new SuperToolTip { AllowHtmlText = true };

                        superTip.Items.AddTitle(string.Format("{0} ({1})", movie.Title, movie.Year));

                        var galleryItem = new GalleryItem(movie.SmallPoster, movie.Title, string.Empty)
                            {
                               Tag = movie.MovieUniqueId, SuperTip = superTip 
                            };

                        if (!this.galleryGroup.Items.Contains(galleryItem))
                        {
                            this.galleryGroup.Items.Add(galleryItem);
                            this.inGallery.Add(movie.MovieUniqueId);

                            changed = true;
                        }
                    }
                }
            }

            if (changed)
            {
                this.GalleryChanged(null, null);
            }
        }

        /// <summary>
        /// Handles the ListChanged event of the MovieDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data. 
        /// </param>
        private void MovieDBListChanged(object sender, ListChangedEventArgs e)
        {
            if (DatabaseIOFactory.AppLoading)
            {
                return;
            }

            this.GeneratePictureGallery();
        }

        /// <summary>
        /// The movie database_ list changed.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void MovieDatabaseListChanged(object sender, ListChangedEventArgs e)
        {
            this.DatabaseChanged(sender, e);
        }

        /// <summary>
        /// Handles the PropertyChanged event of the MultiSelectMovie control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data. 
        /// </param>
        private void MultiSelectMoviePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.MultiSelectedValuesChanged(null, new EventArgs());
        }

        /// <summary>
        /// Handles the ListChanged event of the MultiSelectedMovies control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data. 
        /// </param>
        private void MultiSelectedMoviesListChanged(object sender, ListChangedEventArgs e)
        {
            this.MultiSelectedValuesChanged(null, new EventArgs());
        }

        /// <summary>
        /// Fills a multi select movie model object with items that are the same from SelectedMovies collection
        /// </summary>
        /// <param name="movieModel">
        /// The movie Model. 
        /// </param>
        private void PopulateMultiSelectMovieModel(MovieModel movieModel)
        {
            bool scraperGroupIsSame = true;
            string scraperGroupBaseValue = string.Empty;

            bool yearGroupIsSame = true;
            int? yearGroupBaseValue = null;

            bool plotGroupIsSame = true;
            string plotGroupBaseValue = string.Empty;

            bool outlineGroupIsSame = true;
            string outlineGroupBaseValue = string.Empty;

            bool taglineGroupIsSame = true;
            string taglineGroupBaseValue = string.Empty;

            bool originalTitleGroupIsSame;
            string originalTitleGroupBaseValue = string.Empty;

            bool studioGroupIsSame = true;
            string studioGroupBaseValue = string.Empty;

            bool releasedGroupIsSame = true;
            var releasedGroupBaseValue = new DateTime?();

            bool directorsGroupIsSame = true;
            string directorsGroupBaseValue = string.Empty;

            bool writersGroupIsSame = true;
            string writersGroupBaseValue = string.Empty;

            bool genreGroupIsSame = true;
            string genreGroupBaseValue = string.Empty;

            bool languagesGroupIsSame = true;
            string languagesGroupBaseValue = string.Empty;

            bool countryGroupIsSame = true;
            string countryGroupBaseValue = string.Empty;

            bool runtimeGroupIsSame = true;
            string runtimeGroupBaseValue = string.Empty;

            bool ratingGroupIsSame = true;
            double? ratingGroupBaseValue = null;

            bool mpaaGroupIsSame = true;
            string mpaaGroupBaseValue = string.Empty;

            bool sourceGroupIsSame = true;
            string sourceGroupBaseValue = string.Empty;

            bool certGroupIsSame = true;
            string certGroupBaseValue = string.Empty;

            bool top250GroupIsSame = true;
            int? top250GroupBaseValue = null;

            foreach (MovieModel movie in this.MultiSelectedMovies)
            {
                scraperGroupBaseValue = this.CheckMultiSelectStringValue(
                    scraperGroupBaseValue, movie.ScraperGroup, out scraperGroupIsSame);
                yearGroupBaseValue = this.CheckMultiSelectIntValue(yearGroupBaseValue, movie.Year, out yearGroupIsSame);
                plotGroupBaseValue = this.CheckMultiSelectStringValue(
                    plotGroupBaseValue, movie.Plot, out plotGroupIsSame);
                outlineGroupBaseValue = this.CheckMultiSelectStringValue(
                    outlineGroupBaseValue, movie.Outline, out outlineGroupIsSame);
                taglineGroupBaseValue = this.CheckMultiSelectStringValue(
                    taglineGroupBaseValue, movie.Tagline, out taglineGroupIsSame);
                originalTitleGroupBaseValue = this.CheckMultiSelectStringValue(
                    originalTitleGroupBaseValue, movie.OriginalTitle, out originalTitleGroupIsSame);
                studioGroupBaseValue = this.CheckMultiSelectStringValue(
                    studioGroupBaseValue, movie.SetStudio, out studioGroupIsSame);
                releasedGroupBaseValue = this.CheckMultiSelectDateValue(
                    releasedGroupBaseValue, movie.ReleaseDate, out releasedGroupIsSame);
                directorsGroupBaseValue = this.CheckMultiSelectStringValue(
                    directorsGroupBaseValue, movie.DirectorAsString, out directorsGroupIsSame);
                writersGroupBaseValue = this.CheckMultiSelectStringValue(
                    writersGroupBaseValue, movie.WritersAsString, out writersGroupIsSame);
                genreGroupBaseValue = this.CheckMultiSelectStringValue(
                    genreGroupBaseValue, movie.GenreAsString, out genreGroupIsSame);
                languagesGroupBaseValue = this.CheckMultiSelectStringValue(
                    languagesGroupBaseValue, movie.LanguageAsString, out languagesGroupIsSame);
                countryGroupBaseValue = this.CheckMultiSelectStringValue(
                    countryGroupBaseValue, movie.CountryAsString, out countryGroupIsSame);
                runtimeGroupBaseValue = this.CheckMultiSelectStringValue(
                    runtimeGroupBaseValue, movie.RuntimeInHourMin, out runtimeGroupIsSame);
                ratingGroupBaseValue = this.CheckMultiSelectDoubleValue(
                    ratingGroupBaseValue, movie.Rating, out ratingGroupIsSame);
                mpaaGroupBaseValue = this.CheckMultiSelectStringValue(
                    mpaaGroupBaseValue, movie.Mpaa, out mpaaGroupIsSame);
                sourceGroupBaseValue = this.CheckMultiSelectStringValue(
                    sourceGroupBaseValue, movie.VideoSource, out sourceGroupIsSame);
                certGroupBaseValue = this.CheckMultiSelectStringValue(
                    sourceGroupBaseValue, movie.Certification, out certGroupIsSame);
                top250GroupBaseValue = this.CheckMultiSelectIntValue(
                    top250GroupBaseValue, movie.Top250, out top250GroupIsSame);
            }

            if (scraperGroupIsSame)
            {
                movieModel.ScraperGroup = scraperGroupBaseValue;
            }

            if (yearGroupIsSame)
            {
                movieModel.Year = yearGroupBaseValue;
            }

            if (plotGroupIsSame)
            {
                movieModel.Plot = plotGroupBaseValue;
            }

            if (outlineGroupIsSame)
            {
                movieModel.Outline = outlineGroupBaseValue;
            }

            if (taglineGroupIsSame)
            {
                movieModel.Tagline = taglineGroupBaseValue;
            }

            if (studioGroupIsSame)
            {
                movieModel.SetStudio = studioGroupBaseValue;
            }

            if (releasedGroupIsSame)
            {
                movieModel.ReleaseDate = releasedGroupBaseValue;
            }

            if (directorsGroupIsSame)
            {
                movieModel.DirectorAsString = directorsGroupBaseValue;
            }

            if (writersGroupIsSame)
            {
                movieModel.WritersAsString = writersGroupBaseValue;
            }

            if (genreGroupIsSame)
            {
                movieModel.GenreAsString = genreGroupBaseValue;
            }

            if (languagesGroupIsSame)
            {
                movieModel.LanguageAsString = languagesGroupBaseValue;
            }

            if (countryGroupIsSame)
            {
                movieModel.CountryAsString = countryGroupBaseValue;
            }

            if (runtimeGroupIsSame)
            {
                movieModel.RuntimeInHourMin = runtimeGroupBaseValue;
            }

            if (ratingGroupIsSame)
            {
                movieModel.Rating = ratingGroupBaseValue;
            }

            if (mpaaGroupIsSame)
            {
                movieModel.Mpaa = mpaaGroupBaseValue;
            }

            if (sourceGroupIsSame)
            {
                movieModel.VideoSource = sourceGroupBaseValue;
            }

            if (certGroupIsSame)
            {
                movieModel.Certification = certGroupBaseValue;
            }

            if (top250GroupIsSame)
            {
                movieModel.Top250 = top250GroupBaseValue;
            }
        }

        #endregion
    }
}