// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MasterMediaDBFactory.cs">
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
//   The master media db factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Media
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The master media DB factory.
    /// </summary>
    public static class MasterMediaDBFactory
    {
        #region Static Fields

        /// <summary>
        ///   The master movie media database.
        /// </summary>
        private static ThreadedBindingList<MediaModel> masterMovieMediaDatabase;

        /// <summary>
        ///   The master TV media database.
        /// </summary>
        private static ThreadedBindingList<string> masterTVMediaDatabase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="MasterMediaDBFactory" /> class.
        /// </summary>
        static MasterMediaDBFactory()
        {
            masterMovieMediaDatabase = new ThreadedBindingList<MediaModel>();
            masterTVMediaDatabase = new ThreadedBindingList<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets MasterMovieMediaDatabase.
        /// </summary>
        public static ThreadedBindingList<MediaModel> MasterMovieMediaDatabase
        {
            get
            {
                return masterMovieMediaDatabase;
            }

            set
            {
                masterMovieMediaDatabase = value;
            }
        }

        /// <summary>
        ///   Gets or sets MasterTVMediaDatabase.
        /// </summary>
        public static ThreadedBindingList<string> MasterTVMediaDatabase
        {
            get
            {
                return masterTVMediaDatabase;
            }

            set
            {
                masterTVMediaDatabase = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether movies loading.
        /// </summary>
        public static bool MoviesLoading { get; private set; }

        /// <summary>
        /// Gets a value indicating whether TV loading.
        /// </summary>
        public static bool TVLoading { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The change TV file name.
        /// </summary>
        /// <param name="oldPath">
        /// The old path.
        /// </param>
        /// <param name="newPath">
        /// The new path.
        /// </param>
        public static void ChangeTVFileName(string oldPath, string newPath)
        {
            try
            {
                masterTVMediaDatabase.Remove(oldPath);
                masterTVMediaDatabase.Add(newPath);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Failed to change path in MasterMediaDB/TV", ex.Message);
            }
        }

        /// <summary>
        /// Movies the database contains.
        /// </summary>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <returns>
        /// The movie database contains. 
        /// </returns>
        public static bool MovieDatabaseContains(string path)
        {
            var result = from m in masterMovieMediaDatabase where m.PathAndFilename == path select m;

            return result.Any();
        }

        /// <summary>
        ///   The populate master movie media database.
        /// </summary>
        public static void PopulateMasterMovieMediaDatabase()
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += BuildMasterMovieMediaDatabase;

            MoviesLoading = true;
            bgw.RunWorkerAsync();
        }

        /// <summary>
        ///   The populate master TV media database.
        /// </summary>
        public static void PopulateMasterTVMediaDatabase()
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += BuildMasterTVMediaDatabase;
            TVLoading = true;
            bgw.RunWorkerAsync();
        }

        /// <summary>
        /// The TV database contains.
        /// </summary>
        /// <param name="pathAndFileName">
        /// The path and file name. 
        /// </param>
        /// <returns>
        /// True/False - Database contains filename and path. 
        /// </returns>
        public static bool TVDatabaseContains(string pathAndFileName)
        {
            return masterTVMediaDatabase.Contains(pathAndFileName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the master movie media database.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BuildMasterMovieMediaDatabase(object sender, DoWorkEventArgs e)
        {
            masterMovieMediaDatabase = new ThreadedBindingList<MediaModel>();

            foreach (var f in MovieDBFactory.Instance.MovieDatabase.Where(m => m.AssociatedFiles.Media != null))
            {
                foreach (var m in f.AssociatedFiles.Media)
                {
                    masterMovieMediaDatabase.Add(m);
                }
            }

            MoviesLoading = false;
        }

        /// <summary>
        /// Builds the master TV media database.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BuildMasterTVMediaDatabase(object sender, DoWorkEventArgs e)
        {
            masterTVMediaDatabase = new ThreadedBindingList<string>();

            for (var i = 0; i < TVDBFactory.Instance.TVDatabase.Count; i++)
            {
                foreach (var season in TVDBFactory.Instance.TVDatabase.ElementAt(i).Seasons)
                {
                    foreach (var episode in season.Episodes)
                    {
                        if (!string.IsNullOrEmpty(episode.FilePath.PathAndFilename)
                            && File.Exists(episode.FilePath.PathAndFilename))
                        {
                            try
                            {
                                masterTVMediaDatabase.Add(episode.FilePath.PathAndFilename);
                            }
                            catch (Exception ex)
                            {
                                Log.WriteToLog(LogSeverity.Error, 0, "BuildMasterTVMediaDatabase", ex.Message);
                            }
                        }
                    }
                }
            }

            TVLoading = false;
        }

        #endregion
    }
}