// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieSetManager.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Sets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    using DevExpress.XtraEditors;

    using YANFOE.Models.MovieModels;
    using YANFOE.Models.SetsModels;
    using YANFOE.Settings;

    /// <summary>
    /// The movie set manager.
    /// </summary>
    [Serializable]
    public static class MovieSetManager
    {
        #region Constants and Fields

        /// <summary>
        /// The current set.
        /// </summary>
        private static MovieSetModel currentSet;

        /// <summary>
        /// The movie set database.
        /// </summary>
        private static BindingList<MovieSetModel> database;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MovieSetManager"/> class.
        /// </summary>
        static MovieSetManager()
        {
            database = new BindingList<MovieSetModel>();
            currentSet = new MovieSetModel();

            database.ListChanged += Database_ListChanged;
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [current set changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentSetChanged = delegate { };

        /// <summary>
        /// Occurs when [current set movies changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler CurrentSetMoviesChanged = delegate { };

        /// <summary>
        /// Occurs when [set list changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SetListChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CurrentDatabase.
        /// </summary>
        public static BindingList<MovieSetModel> CurrentDatabase
        {
            get
            {
                return database;
            }

            set
            {
                database = value;
            }
        }

        /// <summary>
        /// Gets GetCurrentSet.
        /// </summary>
        public static MovieSetModel GetCurrentSet
        {
            get
            {
                return currentSet;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the movie to current set.
        /// </summary>
        /// <param name="movie">The movie to add to set</param>
        public static void AddMovieToCurrentSet(MovieModel movie)
        {
            MovieSetObjectModel check =
                (from m in currentSet.Movies where m.MovieUniqueId == movie.MovieUniqueId select m).SingleOrDefault();

            if (check != null)
            {
                XtraMessageBox.Show(string.Format("{0} already exists in set {1}", movie.Title, currentSet.SetName));
                return;
            }

            currentSet.AddMovie(movie);
            InvokeCurrentSetMoviesChanged(new EventArgs());
        }

        /// <summary>
        /// The add movie to set.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <param name="setName">The set name.</param>
        /// <param name="order">The order.</param>
        public static void AddMovieToSet(MovieModel movie, string setName, int? order = null)
        {
            MovieSetModel check = (from m in database where m.SetName == setName select m).SingleOrDefault();

            var movieSetObjectModel = new MovieSetObjectModel { MovieUniqueId = movie.MovieUniqueId };

            if (order != null)
            {
                movieSetObjectModel.Order = (int)order;
            }

            if (check == null)
            {
                var movieSetModel = new MovieSetModel();

                movieSetModel.Movies.Add(movieSetObjectModel);
                movieSetModel.SetName = setName;

                database.Add(movieSetModel);
            }
            else
            {
                check.AddMovie(movie, order);
            }

            movie.ChangedText = true;

            SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// Add new set.
        /// </summary>
        /// <param name="response">The response.</param>
        public static void AddNewSet(string response)
        {
            var newSetModel = new MovieSetModel { SetName = response };
            CurrentDatabase.Add(newSetModel);
            SetCurrentSet(newSetModel);
        }

        /// <summary>
        /// Checks existence of a set with a particular name
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static bool HasSetWithName(string response)
        {
            MovieSetModel find = (from s in database where s.SetName.ToLower() == response.ToLower() select s).SingleOrDefault();

            if (find == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The change current set fanart.
        /// </summary>
        /// <param name="path">The path to change fanart</param>
        public static void ChangeCurrentSetFanart(string path)
        {
            var newPath = Get.FileSystemPaths.PathMoviesSets + "fanart." + GetCurrentSet.SetName +
                             Path.GetExtension(path);

            File.Copy(path, newPath, true);

            GetCurrentSet.FanartUrl = newPath;

            SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// The change current set poster.
        /// </summary>
        /// <param name="path">The path to process the poster change</param>
        public static void ChangeCurrentSetPoster(string path)
        {
            var newPath = Get.FileSystemPaths.PathMoviesSets + "poster." + GetCurrentSet.SetName +
                             Path.GetExtension(path);

            File.Copy(path, newPath, true);

            GetCurrentSet.PosterUrl = newPath;

            SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// Clear current set fanart.
        /// </summary>
        public static void ClearCurrentSetFanart()
        {
            if (File.Exists(GetCurrentSet.FanartUrl))
            {
                File.Delete(GetCurrentSet.FanartUrl);
            }

            GetCurrentSet.FanartUrl = string.Empty;
            SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// Clear current set poster.
        /// </summary>
        public static void ClearCurrentSetPoster()
        {
            if (File.Exists(GetCurrentSet.PosterUrl))
            {
                File.Delete(GetCurrentSet.PosterUrl);
            }

            GetCurrentSet.PosterUrl = string.Empty;
            SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// Returns the movies in sets.
        /// </summary>
        /// <param name="movieSetModel">The movie set model.</param>
        /// <returns>Collection of movies in a set</returns>
        public static List<MovieModel> GetMoviesInSets(MovieSetModel movieSetModel)
        {
            return movieSetModel.Movies.Select(movieSetObect => MovieDBFactory.GetMovie(movieSetObect.MovieUniqueId)).Where(movie => movie != null).ToList();
        }

        /// <summary>
        /// Gets the order of movie in set.
        /// </summary>
        /// <param name="setName">The movie set model name.</param>
        /// <param name="movieModel">The movie model.</param>
        /// <returns>
        /// The order the movie falls in the set
        /// </returns>
        public static int? GetOrderOfMovieInSet(string setName, MovieModel movieModel)
        {
            MovieSetModel movieSetModel = (from s in database where s.SetName == setName select s).SingleOrDefault();

            if (movieSetModel == null)
            {
                return null;
            }

            var check = (from m in movieSetModel.Movies where m.MovieUniqueId == movieModel.MovieUniqueId select m.Order).ToList();

            if (check.Count() > 0)
            {
                return check[0];
            }

            return null;
        }

        /// <summary>
        /// The get set using set name
        /// </summary>
        /// <param name="setName">The set name.</param>
        /// <returns>A MovieSetModel object</returns>
        public static MovieSetModel GetSet(string setName)
        {
            return (from s in database where s.SetName == setName select s).SingleOrDefault();
        }

        /// <summary>
        /// Gets the set return list for output handlers.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        /// <returns>SetReturnModel Collection</returns>
        public static List<SetReturnModel> GetSetReturnList(MovieModel movieModel)
        {
            var sets = GetSetsContainingMovie(movieModel);

            return sets.Select(set => new SetReturnModel { SetName = set, Order = GetOrderOfMovieInSet(set, movieModel) }).ToList();
        }

        /// <summary>
        /// Finds the sets containing movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>Collection of sets containing a movie</returns>
        public static List<string> GetSetsContainingMovie(MovieModel movie)
        {
            return GetSetsContainingMovie(movie.MovieUniqueId);
        }

        /// <summary>
        /// Finds the sets containing movie by ID
        /// </summary>
        /// <param name="id">The ID to search</param>
        /// <returns>Collection of set names.</returns>
        public static List<string> GetSetsContainingMovie(string id)
        {
            return (from m in database where m.ContainsMovie(id) select m.SetName).ToList();
        }

        /// <summary>
        /// Invokes the current set changed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void InvokeCurrentSetChanged(EventArgs e)
        {
            EventHandler handler = CurrentSetChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the current set movies changed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void InvokeCurrentSetMoviesChanged(EventArgs e)
        {
            EventHandler handler = CurrentSetMoviesChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the set list changed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void InvokeSetListChanged(EventArgs e)
        {
            EventHandler handler = SetListChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Removes from set.
        /// </summary>
        /// <param name="id">The id of the set to remove</param>
        public static void RemoveFromSet(string id)
        {
            MovieSetObjectModel setObj =
                (from m in currentSet.Movies where m.MovieUniqueId == id select m).SingleOrDefault();

            if (setObj != null)
            {
                currentSet.Movies.Remove(setObj);
            }
        }

        /// <summary>
        /// Removes the set.
        /// </summary>
        /// <param name="setName">The Set Name.</param>
        public static void RemoveSet(string setName)
        {
            MovieSetModel find = (from s in database where s.SetName == setName select s).SingleOrDefault();

            if (find == null)
            {
                return;
            }

            SetAllMoviesChangedInSet(find);

            database.Remove(find);

            InvokeSetListChanged(new EventArgs());

            SetCurrentSet(new MovieSetModel());
        }

        /// <summary>
        /// The scan for set images.
        /// </summary>
        public static void ScanForSetImages()
        {
            foreach (MovieSetModel set in database)
            {
                bool posterFound = false;
                bool fanartFound = false;

                foreach (MovieSetObjectModel movie in set.Movies)
                {
                    if (movie.GetMovieModel() != null)
                    {
                        string posterPath = movie.GetMovieModel().GetBaseFilePath + "Set_" + set.SetName + "_1.jpg";

                        string fanartPath = movie.GetMovieModel().GetBaseFilePath + "Set_" + set.SetName +
                                            "_1.fanart.jpg";

                        if (File.Exists(posterPath))
                        {
                            string newPosterPath = Get.FileSystemPaths.PathMoviesSets + "poster." + set.SetName +
                                                   Path.GetExtension(posterPath);
                            File.Copy(posterPath, newPosterPath, true);
                            set.PosterUrl = newPosterPath;
                            posterFound = true;
                        }

                        if (File.Exists(fanartPath))
                        {
                            string newFanartPath = Get.FileSystemPaths.PathMoviesSets + "fanart." + set.SetName +
                                                   Path.GetExtension(fanartPath);
                            File.Copy(fanartPath, newFanartPath, true);
                            set.FanartUrl = newFanartPath;
                            fanartFound = true;
                        }

                        if (posterFound && fanartFound)
                        {
                            break;
                        }
                    }
                }
            }

            InvokeCurrentSetChanged(new EventArgs());
        }

        /// <summary>
        /// The set all movies changed in current set.
        /// </summary>
        public static void SetAllMoviesChangedInCurrentSet()
        {
            SetAllMoviesChangedInSet(currentSet);
        }

        /// <summary>
        /// Sets the current set.
        /// </summary>
        /// <param name="movieSetModel">The movie set model.</param>
        public static void SetCurrentSet(MovieSetModel movieSetModel)
        {
            currentSet = movieSetModel;
            InvokeCurrentSetChanged(new EventArgs());
        }

        /// <summary>
        /// Sets the current set.
        /// </summary>
        /// <param name="setName">Name of the set.</param>
        public static void SetCurrentSet(string setName)
        {
            var movieSetModel = (from s in database where s.SetName == setName select s).SingleOrDefault();

            if (movieSetModel != null)
            {
                SetCurrentSet(movieSetModel);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the ListChanged event of the Database control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private static void Database_ListChanged(object sender, ListChangedEventArgs e)
        {
            InvokeSetListChanged(new EventArgs());
        }

        /// <summary>
        /// The set all movies changed in set.
        /// </summary>
        /// <param name="set">The set to alter.</param>
        private static void SetAllMoviesChangedInSet(MovieSetModel set)
        {
            var movies = GetMoviesInSets(set);

            foreach (var movie in movies)
            {
                movie.ChangedText = true;
            }
        }

        #endregion
    }
}