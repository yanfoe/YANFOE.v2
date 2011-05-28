// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaPathDBFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Media
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    using BitFactory.Logging;

    using YANFOE.Factories.Internal;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Settings;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.IO;

    /// <summary>
    /// The media path db factory.
    /// </summary>
    public static class MediaPathDBFactory
    {
        #region Constants and Fields

        /// <summary>
        /// MediaPathMoviesUnsorted collection.
        /// </summary>
        private static BindingList<MediaPathFileModel> mediaPathMoviesUnsorted;

        /// <summary>
        /// MediaPathTvUnsorted collection.
        /// </summary>
        private static BindingList<MediaPathFileModel> mediaPathTvUnsorted;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MediaPathDBFactory"/> class.
        /// </summary>
        static MediaPathDBFactory()
        {
            MediaPathDB = new BindingList<MediaPathModel>();
            mediaPathTvUnsorted = new BindingList<MediaPathFileModel>();
            mediaPathMoviesUnsorted = new BindingList<MediaPathFileModel>();

            MediaPathDB.ListChanged += MediaPathDB_ListChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CurrentMediaPath.
        /// </summary>
        public static MediaPathModel CurrentMediaPath { get; set; }

        /// <summary>
        /// Gets or sets CurrentMediaPathEdit.
        /// </summary>
        public static MediaPathModel CurrentMediaPathEdit { get; set; }

        /// <summary>
        /// Gets or sets MediaPathDB.
        /// </summary>
        public static BindingList<MediaPathModel> MediaPathDB { get; set; }

        #endregion

        #region Public Methods


        /// <summary>
        /// Ensure media path database exists.
        /// </summary>
        public static void EnsureMediaPathDatabaseExists()
        {
            if (MediaPathDB == null)
            {
                MediaPathDB = new BindingList<MediaPathModel>();
            }
        }

        /// <summary>
        /// Generatate unsorted list.
        /// </summary>
        /// <param name="progress"></param>
        public static void GeneratateUnsortedList(Progress progress)
        {
            mediaPathTvUnsorted.Clear();
            mediaPathMoviesUnsorted.Clear();

            
            var count = 0;

            try
            {
                foreach (MediaPathModel mediaPath in MediaPathDB)
                {
                    for (int index = 0; index < mediaPath.FileCollection.Count; index++)
                    {
                        progress.Message = string.Format("Processing {0}", mediaPath.FileCollection[index].PathAndFileName);

                        MediaPathFileModel filePath = mediaPath.FileCollection[index];

                        if (filePath.Type == MediaPathFileModel.MediaPathFileType.Movie)
                        {
                            if (new System.IO.FileInfo(filePath.PathAndFileName).Length > Get.InOutCollection.MinimumMovieSize)
                            {
                                if (!MasterMediaDBFactory.MovieDatabaseContains(filePath.PathAndFileName))
                                {
                                    mediaPathMoviesUnsorted.Add(filePath);
                                }
                            }
                        }

                        if (filePath.Type == MediaPathFileModel.MediaPathFileType.TV)
                        {
                            if (!MasterMediaDBFactory.TvDatabaseContains(filePath.PathAndFileName))
                            {
                                mediaPathTvUnsorted.Add(filePath);
                            }
                        }

                        count++;

                        var total = (long)MediaPathDB.Sum(pathColletion => pathColletion.FileCollection.Count);

                        progress.Percent = Convert.ToInt32((long)count * 100 / total);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "Could not Generated Unsorted List", ex.Message);
            }
        }

        /// <summary>
        /// Get mediaPathMoviesUnsorted
        /// </summary>
        /// <returns>
        /// The mediaPathMoviesUnsorted collection.
        /// </returns>
        public static BindingList<MediaPathFileModel> GetMediaPathMoviesUnsorted()
        {
            return mediaPathMoviesUnsorted ?? (mediaPathMoviesUnsorted = new BindingList<MediaPathFileModel>());
        }

        /// <summary>
        /// Get mediaPathTvUnsorted
        /// </summary>
        /// <returns>
        /// The mediaPathTvUnsorted collection
        /// </returns>
        public static BindingList<MediaPathFileModel> GetMediaPathTvUnsorted()
        {
            return mediaPathTvUnsorted ?? (mediaPathTvUnsorted = new BindingList<MediaPathFileModel>());
        }

        /// <summary>
        /// Refresh the files found from the mediapaths.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public static void RefreshFiles(Progress progress)
        {
            foreach (MediaPathModel mediaPath in MediaPathDB)
            {
                progress.Message = string.Format("Scanning: {0}", mediaPath.MediaPath);

                ImportFiles.ScanMediaPath(mediaPath);
            }

            progress.Message = "Scan complete. Processing";

            GeneratateUnsortedList(progress);

            progress.Message = "Idle.";
        }

        /// <summary>
        /// Remove a MediaPathModel from the MediaPathDB
        /// </summary>
        /// <param name="mediaPathModel">The media path model.</param>
        public static void RemoveFromDatabase(MediaPathModel mediaPathModel)
        {
            EnsureMediaPathDatabaseExists();
            MediaPathDB.Remove(mediaPathModel);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the ListChanged event of the MediaPathDB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private static void MediaPathDB_ListChanged(object sender, ListChangedEventArgs e)
        {
            SaveMediaPathDB();
        }

        /// <summary>
        /// The save media path db.
        /// </summary>
        private static void SaveMediaPathDB()
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.MediaPathDb);
        }

        #endregion
    }
}