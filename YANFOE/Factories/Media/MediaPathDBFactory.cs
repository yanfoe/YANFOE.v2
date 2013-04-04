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

using YANFOE.Tools;
using YANFOE.Tools.Models;
using YANFOE.Tools.UI;

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
    using YANFOE.Models.MovieModels;
    using YANFOE.Settings;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.IO;

    /// <summary>
    /// The media path db factory.
    /// </summary>
    public class MediaPathDBFactory : FactoryBase
    {
        public static MediaPathDBFactory Instance = new MediaPathDBFactory();
        #region Constants and Fields

        /// <summary>
        /// MediaPathMoviesUnsorted collection.
        /// </summary>
        private ThreadedBindingList<MediaPathFileModel> mediaPathMoviesUnsorted;

        /// <summary>
        /// MediaPathTvUnsorted collection.
        /// </summary>
        private ThreadedBindingList<MediaPathFileModel> mediaPathTvUnsorted;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MediaPathDBFactory"/> class.
        /// </summary>
        private MediaPathDBFactory()
        {
            MediaPathDB = new ThreadedBindingList<MediaPathModel>();
            MediaPathTvUnsorted = new ThreadedBindingList<MediaPathFileModel>();
            MediaPathMoviesUnsorted = new ThreadedBindingList<MediaPathFileModel>();

            MediaPathDB.ListChanged += MediaPathDB_ListChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CurrentMediaPath.
        /// </summary>
        private MediaPathModel currentMediaPath;
        public MediaPathModel CurrentMediaPath
        {
            get { return currentMediaPath; } 
            set { currentMediaPath = value; OnPropertyChanged("CurrentMediaPath"); }
        }

        /// <summary>
        /// Gets or sets CurrentMediaPathEdit.
        /// </summary>
        private MediaPathModel currentMediaPathEdit;
        public MediaPathModel CurrentMediaPathEdit
        {
            get { return currentMediaPathEdit; } 
            set { currentMediaPathEdit = value; OnPropertyChanged("CurrentMediaPathEdit"); }
        }

        /// <summary>
        /// Gets or sets MediaPathDB.
        /// </summary>
        private ThreadedBindingList<MediaPathModel> mediaPathDB;
        public ThreadedBindingList<MediaPathModel> MediaPathDB
        {
            get { return mediaPathDB; } 
            set { mediaPathDB = value; OnPropertyChanged("MediaPathDB"); }
        }

        /// <summary>
        /// MediaPathMoviesUnsorted collection.
        /// </summary>
        public ThreadedBindingList<MediaPathFileModel> MediaPathMoviesUnsorted
        {
            get { return mediaPathMoviesUnsorted; }
            set { mediaPathMoviesUnsorted = value; OnPropertyChanged("MediaPathMoviesUnsorted"); }
        }

        /// <summary>
        /// MediaPathTvUnsorted collection.
        /// </summary>
        public ThreadedBindingList<MediaPathFileModel> MediaPathTvUnsorted
        {
            get { return mediaPathTvUnsorted; }
            set { mediaPathTvUnsorted = value; OnPropertyChanged("MediaPathTvUnsorted"); }
        }

        #endregion

        #region Public Methods


        /// <summary>
        /// Ensure media path database exists.
        /// </summary>
        public void EnsureMediaPathDatabaseExists()
        {
            if (MediaPathDB == null)
            {
                MediaPathDB = new ThreadedBindingList<MediaPathModel>();
            }
        }

        /// <summary>
        /// Generatate unsorted list.
        /// </summary>
        /// <param name="progress"></param>
        public void GeneratateUnsortedList(Progress progress)
        {
            MediaPathTvUnsorted.Clear();
            MediaPathMoviesUnsorted.Clear();

            
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
                                    MediaPathMoviesUnsorted.Add(filePath);
                                }
                            }
                        }

                        if (filePath.Type == MediaPathFileModel.MediaPathFileType.TV)
                        {
                            if (!MasterMediaDBFactory.TVDatabaseContains(filePath.PathAndFileName))
                            {
                                MediaPathTvUnsorted.Add(filePath);
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
        /// Refresh the files found from the mediapaths.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void RefreshFiles(Progress progress)
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
        public void RemoveFromDatabase(MediaPathModel mediaPathModel)
        {
            EnsureMediaPathDatabaseExists();
            // Remove movies from db
            for (int index = 0; index < mediaPathModel.FileCollection.Count; index++)
            {

                MediaPathFileModel filePath = mediaPathModel.FileCollection[index];

                if (filePath.Type == MediaPathFileModel.MediaPathFileType.Movie)
                {
                    string p = filePath.Path.TrimEnd('\\');
                    if (!
                        MovieDBFactory.Instance.MovieDatabase.Remove(
                        (from m in MovieDBFactory.Instance.MovieDatabase where m.GetBaseFilePath == p select m).
                            SingleOrDefault())
                        )
                    {
                        MovieDBFactory.Instance.HiddenMovieDatabase.Remove(
                        (from m in MovieDBFactory.Instance.HiddenMovieDatabase where m.GetBaseFilePath == p select m).
                            SingleOrDefault());
                    }
                }

                if (filePath.Type == MediaPathFileModel.MediaPathFileType.TV)
                {
                    if (MasterMediaDBFactory.TVDatabaseContains(filePath.PathAndFileName))
                    {
                        MasterMediaDBFactory.MasterTVMediaDatabase.Remove(filePath.PathAndFileName);
                    }
                }
            }

            // Remove media path
            MediaPathDB.Remove(mediaPathModel);

            // Update master
            MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the ListChanged event of the MediaPathDB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void MediaPathDB_ListChanged(object sender, ListChangedEventArgs e)
        {
            SaveMediaPathDB();
        }

        /// <summary>
        /// The save media path db.
        /// </summary>
        private void SaveMediaPathDB()
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.MediaPathDb);
        }

        #endregion
    }
}