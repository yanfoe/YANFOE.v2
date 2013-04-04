// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaPathModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    using System;
    using System.ComponentModel;
    using System.IO;

    using DevExpress.XtraEditors.DXErrorProvider;

    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The media path model.
    /// </summary>
    [Serializable]
    public class MediaPathModel : ModelBase, IDXDataErrorInfo
    {
        #region Constants and Fields

        /// <summary>
        /// The contains movies.
        /// </summary>
        private bool containsMovies;

        /// <summary>
        /// The contains tv.
        /// </summary>
        private bool containsTv;

        /// <summary>
        /// The default source.
        /// </summary>
        private string defaultSource;

        /// <summary>
        /// The file collection.
        /// </summary>
        private BindingList<MediaPathFileModel> fileCollection;

        /// <summary>
        /// The found files.
        /// </summary>
        private long foundFiles;

        /// <summary>
        /// The import using file name.
        /// </summary>
        private bool importUsingFileName;

        /// <summary>
        /// The import using parent folder name.
        /// </summary>
        private bool importUsingParentFolderName = true;

        /// <summary>
        /// The last scanned time.
        /// </summary>
        private DateTime lastScannedTime;

        /// <summary>
        /// The media path.
        /// </summary>
        private string mediaPath;

        /// <summary>
        /// The recursive scan.
        /// </summary>
        private bool recursiveScan;

        /// <summary>
        /// The scraper group.
        /// </summary>
        private string scraperGroup;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPathModel"/> class.
        /// </summary>
        public MediaPathModel()
        {
            this.mediaPath = string.Empty;

            this.fileCollection = new BindingList<MediaPathFileModel>();
            this.defaultSource = string.Empty;
            this.scraperGroup = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether ContainsMovies.
        /// </summary>
        public bool ContainsMovies
        {
            get
            {
                return this.containsMovies;
            }

            set
            {
                if (this.containsMovies != value)
                {
                    this.containsMovies = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ContainsTv.
        /// </summary>
        public bool ContainsTv
        {
            get
            {
                return this.containsTv;
            }

            set
            {
                if (this.containsTv != value)
                {
                    this.containsTv = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets DefaultSource.
        /// </summary>
        public string DefaultSource
        {
            get
            {
                return this.defaultSource;
            }

            set
            {
                this.defaultSource = value;
                this.OnPropertyChanged("DefaultVideoSource");
            }
        }

        /// <summary>
        /// Gets or sets FileCollection.
        /// </summary>
        public BindingList<MediaPathFileModel> FileCollection
        {
            get
            {
                return this.fileCollection;
            }

            set
            {
                this.fileCollection = value;
                this.OnPropertyChanged("FileCollection");
            }
        }

        /// <summary>
        /// Gets FileCount.
        /// </summary>
        public int FileCount
        {
            get
            {
                if (this.FileCollection == null)
                {
                    this.FileCollection = new BindingList<MediaPathFileModel>();
                }

                return this.FileCollection.Count;
            }
        }

        /// <summary>
        /// Gets or sets FoundFiles.
        /// </summary>
        public long FoundFiles
        {
            get
            {
                return this.foundFiles;
            }

            set
            {
                this.foundFiles = value;
                this.OnPropertyChanged("FoundFiles");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ImportUsingFileName.
        /// </summary>
        public bool ImportUsingFileName
        {
            get
            {
                return this.importUsingFileName;
            }

            set
            {
                if (this.importUsingFileName != value)
                {
                    this.importUsingParentFolderName = !value;
                    this.importUsingFileName = value;
                    this.OnPropertyChanged("ImportUsingFileName");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ImportUsingParentFolderName.
        /// </summary>
        public bool ImportUsingParentFolderName
        {
            get
            {
                return this.importUsingParentFolderName;
            }

            set
            {
                if (this.importUsingParentFolderName != value)
                {
                    this.importUsingParentFolderName = !value;
                    this.importUsingFileName = value;
                    this.OnPropertyChanged("ImportUsingParentFolderName");
                }
            }
        }

        /// <summary>
        /// Gets or sets Contains.
        /// </summary>
        public string Contains
        {
            get
            {
                if (this.ContainsMovies)
                {
                    return "Movies";
                }

                if (this.ContainsTv)
                {
                    return "TV";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets LastScannedTime.
        /// </summary>
        public DateTime LastScannedTime
        {
            get
            {
                return this.lastScannedTime;
            }

            set
            {
                this.lastScannedTime = value;
                this.OnPropertyChanged("LastScannedTime");
            }
        }

        /// <summary>
        /// Gets or sets MediaPath.
        /// </summary>
        public string MediaPath
        {
            get
            {
                return this.mediaPath;
            }

            set
            {
                this.mediaPath = value;
                this.OnPropertyChanged("MediaPath");
                this.OnPropertyChanged("IsMediaPathValid");
            }
        }

        /// <summary>
        /// Gets NameFileBy.
        /// </summary>
        public AddFolderType NameFileBy
        {
            get
            {
                return this.ImportUsingFileName ? AddFolderType.NameByFile : AddFolderType.NameByFolder;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether RecursiveScan.
        /// </summary>
        public bool RecursiveScan
        {
            get
            {
                return this.recursiveScan;
            }

            set
            {
                this.recursiveScan = value;
                this.OnPropertyChanged("MediaPath");
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
                this.scraperGroup = value;
                this.OnPropertyChanged("ScraperGroup");
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IDXDataErrorInfo

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a business object.
        /// </summary>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
        public void GetError(ErrorInfo info)
        {
        }

        /// <summary>
        /// When implemented by a class, this method returns information on an error associated with a specific business object's property.
        /// </summary>
        /// <param name="propertyName">A string that identifies the name of the property for which information on an error is to be returned.</param>
        /// <param name="info">An <see cref="T:DevExpress.XtraEditors.DXErrorProvider.ErrorInfo"/> object that contains information on an error.</param>
        public void GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "MediaPath":
                    if (this.MediaPath == string.Empty)
                    {
                        info.ErrorText += "Media path must not be empty. ";
                        info.ErrorType = ErrorType.Critical;
                    }

                    if (!Directory.Exists(this.MediaPath))
                    {
                        info.ErrorText += "Media path must exist. ";
                        info.ErrorType = ErrorType.Critical;
                    }

                    break;
            }
        }

        #endregion

        #endregion
    }
}