// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MediaPathFileModel.cs">
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
//   The media path file model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    #region Required Namespaces

    using System;

    using Newtonsoft.Json;

    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The media path file model.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class MediaPathFileModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The added.
        /// </summary>
        private DateTime added;

        /// <summary>
        ///   The default video source.
        /// </summary>
        private string defaultVideoSource;

        /// <summary>
        ///   The media path type.
        /// </summary>
        private AddFolderType mediaPathType;

        /// <summary>
        ///   The path and filename
        /// </summary>
        private string pathAndFileName;

        /// <summary>
        ///   The scraper group.
        /// </summary>
        private string scraperGroup;

        /// <summary>
        ///   The MediaPathFileType type.
        /// </summary>
        private MediaPathFileType type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MediaPathFileModel" /> class.
        /// </summary>
        public MediaPathFileModel()
        {
            this.PathAndFileName = string.Empty;
            this.Type = MediaPathFileType.Unknown;
            this.Added = DateTime.Now;
            this.ScraperGroup = string.Empty;
            this.DefaultVideoSource = string.Empty;
        }

        #endregion

        #region Enums

        /// <summary>
        ///   The media path file type.
        /// </summary>
        public enum MediaPathFileType
        {
            /// <summary>
            ///   MediaPathFileType: unknown.
            /// </summary>
            Unknown = 0, 

            /// <summary>
            ///   MediaPathFileType: TV.
            /// </summary>
            TV = 1, 

            /// <summary>
            ///   MediaPathFileType: movie.
            /// </summary>
            Movie = 2, 

            /// <summary>
            ///   MediaPathFileType: image.
            /// </summary>
            Image = 3, 

            /// <summary>
            ///   MediaPathFileType: NFO.
            /// </summary>
            NFO = 4, 

            /// <summary>
            ///   MediaPathFileType: music.
            /// </summary>
            Music = 5, 

            /// <summary>
            ///   MediaPathFileType: video.
            /// </summary>
            Video = 6, 

            /// <summary>
            ///   MediaPathFileType: sample.
            /// </summary>
            Sample = 7
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Added.
        /// </summary>
        public DateTime Added
        {
            get
            {
                return this.added;
            }

            set
            {
                this.added = value;
                this.OnPropertyChanged("Added");
            }
        }

        /// <summary>
        ///   Gets or sets DefaultVideoSource.
        /// </summary>
        public string DefaultVideoSource
        {
            get
            {
                return this.defaultVideoSource;
            }

            set
            {
                this.defaultVideoSource = value;
                this.OnPropertyChanged("ScraperGroup");
            }
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        [JsonIgnore]
        public string Filename
        {
            get
            {
                return System.IO.Path.GetFileName(this.pathAndFileName);
            }
        }

        /// <summary>
        ///   Gets FilenameExt.
        /// </summary>
        [JsonIgnore]
        public string FilenameExt
        {
            get
            {
                return System.IO.Path.GetExtension(this.pathAndFileName);
            }
        }

        /// <summary>
        ///   Gets FilenameWithOutExt.
        /// </summary>
        [JsonIgnore]
        public string FilenameWithOutExt
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(this.pathAndFileName);
            }
        }

        /// <summary>
        ///   Gets or sets MediaPathType.
        /// </summary>
        public AddFolderType MediaPathType
        {
            get
            {
                return this.mediaPathType;
            }

            set
            {
                this.mediaPathType = value;
                this.OnPropertyChanged("MediaPathType");
            }
        }

        /// <summary>
        ///   Gets Path.
        /// </summary>
        [JsonIgnore]
        public string Path
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.pathAndFileName) + System.IO.Path.DirectorySeparatorChar;
            }
        }

        /// <summary>
        ///   Gets or sets Path.
        /// </summary>
        public string PathAndFileName
        {
            get
            {
                return this.pathAndFileName;
            }

            set
            {
                this.pathAndFileName = value;
                this.OnPropertyChanged("Path");
            }
        }

        /// <summary>
        ///   Gets or sets ScraperGroup.
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

        /// <summary>
        ///   Gets or sets Type.
        /// </summary>
        public MediaPathFileType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                this.OnPropertyChanged("Type");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Build a MediaPathFileModel
        /// </summary>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="type">
        /// The MediaPathFileType type. 
        /// </param>
        /// <param name="nameBy">
        /// The AddFolderType name by. 
        /// </param>
        /// <param name="scraperGroup">
        /// The scraper Group. 
        /// </param>
        /// <param name="defaultSource">
        /// The default Source. 
        /// </param>
        /// <returns>
        /// MediaPathFileModel object 
        /// </returns>
        public static MediaPathFileModel Add(
            string path, MediaPathFileType type, AddFolderType nameBy, string scraperGroup, string defaultSource)
        {
            var newMediaPathFileModel = new MediaPathFileModel
                {
                    PathAndFileName = path, 
                    Type = type, 
                    MediaPathType = nameBy, 
                    ScraperGroup = scraperGroup, 
                    DefaultVideoSource = defaultSource
                };

            return newMediaPathFileModel;
        }

        #endregion
    }
}