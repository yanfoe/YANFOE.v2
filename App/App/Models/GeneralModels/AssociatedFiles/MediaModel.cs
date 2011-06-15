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
        private string _pathAndFilename;

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
            this._pathAndFilename = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public string PathAndFilename
        {
            get
            {
                return this._pathAndFilename;
            }

            set
            {
                this._pathAndFilename = value;
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

                if (File.Exists(this.PathAndFilename + ".mediainfo"))
                {
                    var xml = Tools.Text.IO.ReadTextFromFile(this.PathAndFilename + ".mediainfo");
                    responseModel.PopulateFromXML(xml);
                    return responseModel;
                }

                return new MiResponseModel();
            }
        }

        public string ScanXML
        {
            get
            {
                return Tools.Text.IO.ReadTextFromFile(this.PathAndFilename + ".mediainfo");
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
        [JsonIgnore]
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(this._pathAndFilename))
                {
                    return string.Empty;
                }

                return Path.GetFileName(this._pathAndFilename);
            }
        }

        [JsonIgnore]
        public string FolderPath
        {
            get
            {
                return Path.GetDirectoryName(this._pathAndFilename);
            }
        }

        public string FilenameWithOutExt
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this._pathAndFilename);
            }
        }

        #endregion
    }
}