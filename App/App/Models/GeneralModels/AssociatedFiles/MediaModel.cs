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

namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The media model
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    [Serializable]
    public class MediaModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// FilePath backing field
        /// </summary>
        private MediaPathFileModel fileModel;

        /// <summary>
        /// The file path.
        /// </summary>
        private string _fileNameAndPath;

        /// <summary>
        /// Order backing field
        /// </summary>
        private int order;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaModel"/> class.
        /// </summary>
        public MediaModel()
        {
            this.FileModel = new MediaPathFileModel();
            this._fileNameAndPath = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public MediaPathFileModel FileModel
        {
            get
            {
                return this.fileModel;
            }

            set
            {

                this.fileModel = value;
                this.OnPropertyChanged("FileModel");
            }
        }

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public string FileNameAndPath
        {
            get
            {
                return this._fileNameAndPath;
            }

            set
            {
                this._fileNameAndPath = value;
            }
        }

        /// <summary>
        /// Gets FilePathFolder.
        /// </summary>
        [JsonIgnore]
        public string FilePathFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_fileNameAndPath))
                {
                    return string.Empty;
                }

                return this._fileNameAndPath.Replace(Path.GetFileName(this._fileNameAndPath), string.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the media info scan output.
        /// </summary>
        /// <value>The media info scan output.</value>
        [JsonIgnore]
        public MiResponseModel MiResponseModel
        {
            get
            {
                var responseModel = new MiResponseModel();

                if (File.Exists(this.FileNameAndPath + ".mediainfo"))
                {
                    var xml = Tools.Text.IO.ReadTextFromFile(this.FileNameAndPath + ".mediainfo");
                    responseModel.PopulateFromXML(xml);
                    return responseModel;
                }

                return new MiResponseModel();
            }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order
        {
            get
            {
                return this.order;
            }

            set
            {
                this.order = value;
                this.OnPropertyChanged("Order");
            }
        }

        /// <summary>
        /// Gets or sets FileName.
        /// </summary>
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileNameAndPath))
                {
                    return string.Empty;
                }

                return Path.GetFileName(_fileNameAndPath);
            }
        }

        #endregion
    }
}