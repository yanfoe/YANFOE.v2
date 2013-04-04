// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.IOModels
{
    using System;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The file status.
    /// </summary>
    public enum FileStatus
    {
        /// <summary>
        /// The added.
        /// </summary>
        Added, 

        /// <summary>
        /// The missing.
        /// </summary>
        Missing, 

        /// <summary>
        /// The skipped.
        /// </summary>
        Skipped
    }

    /// <summary>
    /// The media model.
    /// </summary>
    public class MediaModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The file path.
        /// </summary>
        private Uri filePath;

        /// <summary>
        /// The file status.
        /// </summary>
        private FileStatus fileStatus;

        /// <summary>
        /// The file status reason.
        /// </summary>
        private string fileStatusReason;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public Uri FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                this.filePath = value;
                this.OnPropertyChanged("FilePath");
            }
        }

        /// <summary>
        /// Gets or sets the files current status.
        /// </summary>
        /// <value>The file current status.</value>
        public FileStatus FileStatus
        {
            get
            {
                return this.fileStatus;
            }

            set
            {
                this.fileStatus = value;
                this.OnPropertyChanged("FileStatus");
            }
        }

        /// <summary>
        /// Gets or sets FileStatusReason.
        /// </summary>
        public string FileStatusReason
        {
            get
            {
                return this.fileStatusReason;
            }

            set
            {
                this.fileStatusReason = value;
                this.OnPropertyChanged("FileStatusReason");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The create new media model.
        /// </summary>
        /// <returns>
        /// A MediaModel object
        /// </returns>
        public static MediaModel CreateNewMediaModel()
        {
            return new MediaModel();
        }

        #endregion
    }
}