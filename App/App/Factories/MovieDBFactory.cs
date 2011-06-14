// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieDBFactory.cs" company="The YANFOE Project">
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using DevExpress.Utils;
    using DevExpress.XtraBars.Ribbon;

    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.UI.Dialogs.General;

    /// <summary>
    /// Data access layer for all movie related data.
    /// </summary>
    public static class MovieDBFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The gallery group.
        /// </summary>
        private static readonly GalleryItemGroup galleryGroup;

        /// <summary>
        /// The in gallery.
        /// </summary>
        private static readonly BindingList<string> inGallery;

        /// <summary>
        /// The current movie present in the main movie window.
        /// </summary>
        private static MovieModel currentMovie;

        /// <summary>
        /// The is multi selected.
        /// </summary>
        private static bool isMultiSelected;

        /// <summary>
        /// The multi select.
        /// </summary>
        private static MovieModel multiSelect;

        /// <summary>
        /// The multi selected movies.
        /// </summary>
        private static BindingList<MovieModel> multiSelectedMovies;

        public static string TempScraperGroup { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MovieDBFactory"/> class. 
        /// </summary>
        static MovieDBFactory()
        {
            MovieDatabase = new BindingList<MovieModel>();
            currentMovie = new MovieModel();
            galleryGroup = new GalleryItemGroup();
            multiSelectedMovies = new BindingList<MovieModel>();
            TempScraperGroup = string.Empty;

            inGallery = new BindingList<string>();

            multiSelectedMovies.ListChanged += MultiSelectedMovies_ListChanged;
            MovieDatabase.ListChanged += MovieDB_ListChanged;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [current movie changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentMovieChanged = delegate { };

        /// <summary>
        /// Occurs when [current movie value changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentMovieValueChanged = delegate { };

        /// <summary>
        /// Occurs when [movie database changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler DatabaseChanged = delegate { };

        /// <summary>
        /// Occurs when [displayed database values require refresh].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler DatabaseValuesRefreshRequired = delegate { };

        /// <summary>
        /// Occurs when [fanart loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler FanartLoaded = delegate { };

        /// <summary>
        /// Occurs when [fanart loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler FanartLoading = delegate { };

        /// <summary>
        /// Occurs when [movie gallery changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler GalleryChanged = delegate { };

        /// <summary>
        /// Occurs when [multi selected values changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler MultiSelectedValuesChanged = delegate { };

        /// <summary>
        /// Occurs when [poster loaded].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler PosterLoaded = delegate { };

        /// <summary>
        /// Occurs when [poster loading].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler PosterLoading = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ImportProgressCurrent.
        /// </summary>
        public static int ImportProgressCurrent { get; set; }

        /// <summary>
        /// Gets or sets ImportProgressMaximum.
        /// </summary>
        public static int ImportProgressMaximum { get; set; }

        /// <summary>
        /// Gets or sets ImportProgressStatus.
        /// </summary>
        public static string ImportProgressStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsMultiSelected.
        /// </summary>
        public static bool IsMultiSelected
        {
            get
            {
                multiSelect = new MovieModel { Title = "Multiple Movies Selected" };

                return isMultiSelected;
            }

            set
            {
                isMultiSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets the movie database.
        /// </summary>
        /// <value>
        /// The movie database.
        /// </value>
        public static BindingList<MovieModel> MovieDatabase { get; set; }

        /// <summary>
        /// Gets or sets MultiSelectedMovies.
        /// </summary>
        public static BindingList<MovieModel> MultiSelectedMovies
        {
            get
            {
                return multiSelectedMovies;
            }

            set
            {
                multiSelectedMovies = value;
                MultiSelectedValuesChanged(null, new EventArgs());
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check if Fanart has downloaded.
        /// </summary>
        /// <returns>
        /// Downloaded status
        /// </returns>
        public static bool FanartDownloaded()
        {
            string url = currentMovie.CurrentFanartImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// Gets the current movie.
        /// </summary>
        /// <returns>
        /// The current movie
        /// </returns>
        public static MovieModel GetCurrentMovie()
        {
            return currentMovie;
        }

        /// <summary>
        /// Download fanart
        /// </summary>
        public static void GetFanart()
        {
            if (FanartDownloaded())
            {
                InvokeFanartLoaded(new EventArgs());
                return;
            }

            InvokeFanartLoading(new EventArgs());

            var bgwFanart = new BackgroundWorker();

            bgwFanart.DoWork += BgwFanart_DoWork;
            bgwFanart.RunWorkerCompleted += BgwFanart_RunWorkerCompleted;
            bgwFanart.RunWorkerAsync(currentMovie.CurrentFanartImageUrl);
        }

        /// <summary>
        /// Get gallery group.
        /// </summary>
        /// <returns>
        /// The gallery group.
        /// </returns>
        public static GalleryItemGroup GetGalleryGroup()
        {
            return galleryGroup;
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
        public static MovieModel GetMovie(string id)
        {
            try
            {
                return (from m in MovieDatabase where m.MovieUniqueId == id select m).SingleOrDefault();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// The get poster.
        /// </summary>
        public static void GetPoster()
        {
            if (PosterDownloaded())
            {
                InvokePosterLoaded(new EventArgs());
                return;
            }

            InvokePosterLoading(new EventArgs());

            var bgwPoster = new BackgroundWorker();

            bgwPoster.DoWork += BgwPoster_DoWork;
            bgwPoster.RunWorkerCompleted += BgwPoster_RunWorkerCompleted;
            bgwPoster.RunWorkerAsync(currentMovie.CurrentPosterImageUrl);
        }

        /// <summary>
        /// Invokes the current movie changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        public static void InvokeCurrentMovieChanged(EventArgs e)
        {
            EventHandler handler = CurrentMovieChanged;
            if (handler != null)
            {
                handler(null, e);
            }

            currentMovie.PropertyChanged += MultiSelect_PropertyChanged;
        }

        /// <summary>
        /// Invokes the current movie value changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        public static void InvokeCurrentMovieValueChanged(EventArgs e)
        {
            EventHandler handler = CurrentMovieValueChanged;
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
        public static void InvokeFanartLoaded(EventArgs e)
        {
            EventHandler handler = FanartLoaded;
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
        public static void InvokeFanartLoading(EventArgs e)
        {
            EventHandler handler = FanartLoading;
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
        public static void InvokePosterLoaded(EventArgs e)
        {
            EventHandler handler = PosterLoaded;
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
        public static void InvokePosterLoading(EventArgs e)
        {
            EventHandler handler = PosterLoading;
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
        public static bool IsSameAsCurrentMovie(MovieModel movieModel)
        {
            return movieModel.MovieUniqueId == currentMovie.MovieUniqueId;
        }

        /// <summary>
        /// The load fanart.
        /// </summary>
        /// <returns>
        /// Image object of fanart
        /// </returns>
        public static Image LoadFanart()
        {
            if (!string.IsNullOrEmpty(currentMovie.FanartPathOnDisk) && File.Exists(currentMovie.FanartPathOnDisk))
            {
                return ImageHandler.LoadImage(currentMovie.FanartPathOnDisk);
            }

            string url = currentMovie.CurrentFanartImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            if (!File.Exists(urlCache) || Downloader.Downloading.Contains(url))
            {
                return null;
            }

            return ImageHandler.LoadImage(urlCache);
        }

        /// <summary>
        /// The load poster.
        /// </summary>
        /// <returns>
        /// Image object of poster
        /// </returns>
        public static Image LoadPoster()
        {
            if (!string.IsNullOrEmpty(currentMovie.PosterPathOnDisk) && File.Exists(currentMovie.PosterPathOnDisk))
            {
                return ImageHandler.LoadImage(currentMovie.PosterPathOnDisk);
            }

            string urlCache = WebCache.GetPathFromUrl(currentMovie.CurrentPosterImageUrl, Section.Movies);

            if (!File.Exists(urlCache) || Downloader.Downloading.Contains(currentMovie.CurrentPosterImageUrl))
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
        public static void MergeWithDatabase(BindingList<MovieModel> importDatabase)
        {
            foreach (MovieModel movie in importDatabase)
            {
                if (movie.SmallPoster != null)
                {
                    movie.SmallPoster = ImageHandler.ResizeImage(movie.SmallPoster, 100, 150);
                }

                MovieDatabase.Add(movie);
            }

            MediaPathDBFactory.GetMediaPathMoviesUnsorted().Clear();
            GeneratePictureGallery();
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
        public static void MultiSelect_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (isMultiSelected)
            {
                foreach (MovieModel movie in multiSelectedMovies)
                {
                    if (e.PropertyName == "Marked")
                    {
                        if (currentMovie.Marked != movie.Marked)
                        {
                            movie.Marked = currentMovie.Marked;
                        }
                    }

                    if (e.PropertyName == "Locked")
                    {
                        if (currentMovie.Locked != movie.Locked)
                        {
                            movie.Locked = currentMovie.Locked;
                        }
                    }

                    if (e.PropertyName == "Year")
                    {
                        if (currentMovie.Year != movie.Year)
                        {
                            movie.Year = currentMovie.Year;
                        }
                    }

                    if (e.PropertyName == "Plot")
                    {
                        if (currentMovie.Plot != movie.Plot)
                        {
                            movie.Plot = currentMovie.Plot;
                        }
                    }
                }

                DatabaseValuesRefreshRequired(null, null);
            }
        }

        /// <summary>
        /// The overwrite database.
        /// </summary>
        /// <param name="database">
        /// The database.
        /// </param>
        public static void OverwriteDatabase(BindingList<MovieModel> database)
        {
            MovieDatabase = database;
            DatabaseChanged(null, null);
        }

        /// <summary>
        /// Check if Poster has downloaded.
        /// </summary>
        /// <returns>
        /// Downloaded status
        /// </returns>
        public static bool PosterDownloaded()
        {
            string url = currentMovie.CurrentPosterImageUrl;
            string urlCache = WebCache.GetPathFromUrl(url, Section.Movies);

            return File.Exists(urlCache);
        }

        /// <summary>
        /// The replace movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        public static void ReplaceMovie(MovieModel movieModel)
        {
            lock (MovieDatabase)
            {
                var getMovieInDatabase = MovieDatabase.ToList().FindIndex(w => w.MovieUniqueId == movieModel.MovieUniqueId);

                if (getMovieInDatabase == -1)
                {
                    return;
                }

                movieModel.IsBusy = false;

                movieModel.IsBusy = false;
                MovieDatabase[getMovieInDatabase] = movieModel;

                if (movieModel.MovieUniqueId == currentMovie.MovieUniqueId)
                {
                    SetCurrentMovie(movieModel);
                    InvokeCurrentMovieChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// The set current movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        public static void SetCurrentMovie(MovieModel movieModel)
        {
            if (movieModel.MultiSelectModel)
            {
                PopulateMultiSelectMovieModel(movieModel);
            }

            currentMovie = movieModel;
            InvokeCurrentMovieChanged(new EventArgs());
        }

        /// <summary>
        /// The set current movie.
        /// </summary>
        /// <param name="id">
        /// The movie ID
        /// </param>
        public static void SetCurrentMovie(string id)
        {
            if (IsMultiSelected)
            {
                currentMovie = multiSelect;
                currentMovie.PropertyChanged += MultiSelectMovie_PropertyChanged;
            }
            else
            {
                MovieModel movie = (from m in MovieDatabase where m.MovieUniqueId == id select m).SingleOrDefault();

                if (movie != null)
                {
                    SetCurrentMovie(movie);
                }
            }
        }

        #endregion

        #region Methods

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
            var url = e.Argument as string;
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
        /// Handles the DoWork event of the bgwPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwPoster_DoWork(object sender, DoWorkEventArgs e)
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
        /// Handles the RunWorkerCompleted event of the bgwPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwPoster_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InvokePosterLoaded(new EventArgs());
        }

        /// <summary>
        /// Check if a value matches multiselected movies
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
        /// Datetime value
        /// </returns>
        private static DateTime? CheckMultiSelectDateValue(
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
        /// Check if a value matches multiselected movies
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
        private static double? CheckMultiSelectDoubleValue(
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
        /// Secondary method for PopulateMultiSelectMovieModel for compairing int collections
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
        /// Returned Int value
        /// </returns>
        private static int? CheckMultiSelectIntValue(
            int? scraperGroupBaseValue, int? value, out bool scraperGroupIsSame)
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
        /// Secondary method for PopulateMultiSelectMovieModel for compairing string collections
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
        private static string CheckMultiSelectStringValue(
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
        /// The generate picture gallery.
        /// </summary>
        private static void GeneratePictureGallery()
        {
            bool changed = false;

            for (int index = 0; index < MovieDatabase.Count; index++)
            {
                MovieModel movie = MovieDatabase[index];
                if (!inGallery.Contains(movie.MovieUniqueId))
                {
                    if (movie.SmallPoster != null)
                    {
                        var superTip = new SuperToolTip { AllowHtmlText = DefaultBoolean.True };

                        superTip.Items.AddTitle(string.Format("{0} ({1})", movie.Title, movie.Year));

                        var galleryItem = new GalleryItem(movie.SmallPoster, movie.Title, string.Empty)
                            { 
                                Tag = movie.MovieUniqueId,
                                SuperTip = superTip
                            };

                        if (!galleryGroup.Items.Contains(galleryItem))
                        {
                            galleryGroup.Items.Add(galleryItem);
                            inGallery.Add(movie.MovieUniqueId);

                            changed = true;
                        }
                    }
                }
            }

            if (changed)
            {
                GalleryChanged(null, null);
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
        private static void MovieDB_ListChanged(object sender, ListChangedEventArgs e)
        {
            GeneratePictureGallery();
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
        private static void MultiSelectMovie_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MultiSelectedValuesChanged(null, new EventArgs());
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
        private static void MultiSelectedMovies_ListChanged(object sender, ListChangedEventArgs e)
        {
            MultiSelectedValuesChanged(null, new EventArgs());
        }

        /// <summary>
        /// Fills a multi select moviemodel object with items that are the same from SelectedMovies collection
        /// </summary>
        /// <param name="movieModel">
        /// The movie Model.
        /// </param>
        private static void PopulateMultiSelectMovieModel(MovieModel movieModel)
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

            bool OriginalTitleGroupIsSame;
            string OriginalTitleGroupBaseValue = string.Empty;

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

            foreach (MovieModel movie in MultiSelectedMovies)
            {
                scraperGroupBaseValue = CheckMultiSelectStringValue(
                    scraperGroupBaseValue, movie.ScraperGroup, out scraperGroupIsSame);
                yearGroupBaseValue = CheckMultiSelectIntValue(yearGroupBaseValue, movie.Year, out yearGroupIsSame);
                plotGroupBaseValue = CheckMultiSelectStringValue(plotGroupBaseValue, movie.Plot, out plotGroupIsSame);
                outlineGroupBaseValue = CheckMultiSelectStringValue(
                    outlineGroupBaseValue, movie.Outline, out outlineGroupIsSame);
                taglineGroupBaseValue = CheckMultiSelectStringValue(
                    taglineGroupBaseValue, movie.Tagline, out taglineGroupIsSame);
                OriginalTitleGroupBaseValue = CheckMultiSelectStringValue(
                    OriginalTitleGroupBaseValue, movie.OriginalTitle, out OriginalTitleGroupIsSame);
                studioGroupBaseValue = CheckMultiSelectStringValue(
                    studioGroupBaseValue, movie.SetStudio, out studioGroupIsSame);
                releasedGroupBaseValue = CheckMultiSelectDateValue(
                    releasedGroupBaseValue, movie.ReleaseDate, out releasedGroupIsSame);
                directorsGroupBaseValue = CheckMultiSelectStringValue(
                    directorsGroupBaseValue, movie.DirectorAsString, out directorsGroupIsSame);
                writersGroupBaseValue = CheckMultiSelectStringValue(
                    writersGroupBaseValue, movie.WritersAsString, out writersGroupIsSame);
                genreGroupBaseValue = CheckMultiSelectStringValue(
                    genreGroupBaseValue, movie.GenreAsString, out genreGroupIsSame);
                languagesGroupBaseValue = CheckMultiSelectStringValue(
                    languagesGroupBaseValue, movie.LanguageAsString, out languagesGroupIsSame);
                countryGroupBaseValue = CheckMultiSelectStringValue(
                    countryGroupBaseValue, movie.CountryAsString, out countryGroupIsSame);
                runtimeGroupBaseValue = CheckMultiSelectStringValue(
                    runtimeGroupBaseValue, movie.RuntimeInHourMin, out runtimeGroupIsSame);
                ratingGroupBaseValue = CheckMultiSelectDoubleValue(
                    ratingGroupBaseValue, movie.Rating, out ratingGroupIsSame);
                mpaaGroupBaseValue = CheckMultiSelectStringValue(mpaaGroupBaseValue, movie.Mpaa, out mpaaGroupIsSame);
                sourceGroupBaseValue = CheckMultiSelectStringValue(
                    sourceGroupBaseValue, movie.VideoSource, out sourceGroupIsSame);
                certGroupBaseValue = CheckMultiSelectStringValue(
                    sourceGroupBaseValue, movie.Certification, out certGroupIsSame);
                top250GroupBaseValue = CheckMultiSelectIntValue(
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

        public static void RemoveMissingMovies()
        {
            var toDelete = new List<MovieModel>();

            for (int index = 0; index < MovieDatabase.Count; index++)
            {
                var movie = MovieDatabase[index];

                foreach (var file in movie.AssociatedFiles.Media)
                {
                    if (!File.Exists(file.FileNameAndPath))
                    {
                        toDelete.Add(movie);
                        break;
                    }
                }
            }

            if (toDelete.Count > 0)
            {
                var frmMissingFilesMovies = new FrmMissingFilesMovies(toDelete);
                frmMissingFilesMovies.ShowDialog();
            }
        }
    }
}