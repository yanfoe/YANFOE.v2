// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImportFiles.cs">
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
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.ThirdParty;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The import files.
    /// </summary>
    public class ImportFiles
    {
        #region Static Fields

        /// <summary>
        ///   The import in progress.
        /// </summary>
        private static bool importInProgress;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Scans the media path.
        /// </summary>
        /// <param name="mediaPathModel">
        /// The media path model. 
        /// </param>
        public static void ScanMediaPath(MediaPathModel mediaPathModel)
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += Bgw_DoWork;
            bgw.RunWorkerCompleted += Bgw_RunWorkerCompleted;

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
            if (files == null)
            {
                return;
            }

            foreach (string f in files)
            {
                MediaPathFileModel.MediaPathFileType type = DetectType.FindType(
                    f, mediaPathModel.ContainsTV, mediaPathModel.ContainsMovies);

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

                try
                {
                    var check = mediaPathModel.FileCollection.Any(file => file.PathAndFileName == f1);

                    if (!check)
                    {
                        mediaPathModel.FileCollection.Add(obj);
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Handles the DoWork event of the Bgw control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var mediaPathModel = e.Argument as MediaPathModel;
            var files = FileHelper.GetFilesRecursive(mediaPathModel.MediaPath).ToArray();
            var returnCollection = new List<object>(2) { mediaPathModel, files };

            e.Result = returnCollection;

            importInProgress = false;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Bgw control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private static void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var returnCollection = e.Result as List<object>;

            var mediaPathModel = returnCollection[0] as MediaPathModel;
            var files = returnCollection[1] as string[];

            mediaPathModel.FileCollection = new ThreadedBindingList<MediaPathFileModel>();

            AddNewEntries(mediaPathModel, files);

            mediaPathModel.LastScannedTime = DateTime.Now;
        }

        #endregion
    }
}