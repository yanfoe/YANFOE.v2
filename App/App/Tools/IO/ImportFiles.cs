// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportFiles.cs" company="The YANFOE Project">
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
//   The import files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools.IO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.ThirdParty;

    /// <summary>
    /// The import files.
    /// </summary>
    public class ImportFiles
    {
        #region Constants and Fields

        /// <summary>
        /// The import in progress.
        /// </summary>
        private static bool importInProgress;

        #endregion

        #region Public Methods

        /// <summary>
        /// Scans the media path.
        /// </summary>
        /// <param name="mediaPathModel">
        /// The media path model.
        /// </param>
        public static void ScanMediaPath(MediaPathModel mediaPathModel)
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += BgwDoWork;
            bgw.RunWorkerCompleted += BgwRunWorkerCompleted;

            importInProgress = true;

            bgw.RunWorkerAsync(mediaPathModel);

            do
            {
                Thread.Sleep(50);
            }
            while (importInProgress);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the new entries.
        /// </summary>
        /// <param name="mediaPathModel">
        /// The media path model.
        /// </param>
        /// <param name="files">
        /// The files.
        /// </param>
        private static void AddNewEntries(MediaPathModel mediaPathModel, string[] files)
        {
            foreach (string f in files)
            {
                MediaPathFileModel.MediaPathFileType type = DetectType.FindType(
                    f, mediaPathModel.ContainsTv, mediaPathModel.ContainsMovies);

                AddFolderType importType;

                if (type == MediaPathFileModel.MediaPathFileType.Movie)
                {
                    importType = mediaPathModel.NameFileBy;
                }
                else
                {
                    importType = AddFolderType.NotApplicable;
                }

                MediaPathFileModel obj = MediaPathFileModel.Add(
                    f, type, importType, mediaPathModel.ScraperGroup, mediaPathModel.DefaultSource);

                string f1 = f;

                var check = false;

                for (int index = 0; index < mediaPathModel.FileCollection.Count; index++)
                {
                    var file = mediaPathModel.FileCollection[index];
                    if (file.PathAndFileName == f1)
                    {
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    mediaPathModel.FileCollection.Add(obj);
                }
            }
        }

        /// <summary>
        /// BGWs the do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwDoWork(object sender, DoWorkEventArgs e)
        {
            var mediaPathModel = e.Argument as MediaPathModel;

            string[] files = FastDirectoryEnumerator.EnumarateFilesPathList(
                mediaPathModel.MediaPath, "*.*", SearchOption.AllDirectories);

            var returnCollection = new List<object>(2) { mediaPathModel, files };

            e.Result = returnCollection;

            importInProgress = false;
        }

        /// <summary>
        /// BGWs the run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BgwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var returnCollection = e.Result as List<object>;

            var mediaPathModel = returnCollection[0] as MediaPathModel;
            var files = returnCollection[1] as string[];

            mediaPathModel.FileCollection = new BindingList<MediaPathFileModel>();

            AddNewEntries(mediaPathModel, files);

            mediaPathModel.LastScannedTime = DateTime.Now;
        }

        #endregion

        ///// <summary>
        ///// Removes the missing entries.
        ///// </summary>
        ///// <param name="mediaPathModel">The media path model.</param>
        // private static void RemoveMissingEntries(MediaPathModel mediaPathModel)
        // {
        // var removeCollection = new List<MediaPathFileModel>();

        // foreach (var file in mediaPathModel.FileCollection)
        // {
        // if (!File.Exists(file.Path))
        // {
        // removeCollection.Add(file);
        // }
        // }

        // foreach (var file in removeCollection)
        // {
        // mediaPathModel.FileCollection.Remove(file);
        // }
        // }
    }
}