// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MediaModel.cs">
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
//   The media model
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels.AssociatedFiles
{
    #region Required Namespaces

    using System;
    using System.IO;

    using Newtonsoft.Json;

    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.Text;

    #endregion

    /// <summary>
    ///   The media model
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    [Serializable]
    public class MediaModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The file path.
        /// </summary>
        private string pathAndFilename;

        /// <summary>
        ///   Order backing field
        /// </summary>
        private int order;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MediaModel" /> class.
        /// </summary>
        public MediaModel()
        {
            this.pathAndFilename = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets FileName.
        /// </summary>
        [JsonIgnore]
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(this.pathAndFilename))
                {
                    return string.Empty;
                }

                return Path.GetFileName(this.pathAndFilename);
            }
        }

        /// <summary>
        /// Gets the filename with out ext.
        /// </summary>
        public string FilenameWithOutExt
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.pathAndFilename);
            }
        }

        /// <summary>
        /// Gets the folder path.
        /// </summary>
        [JsonIgnore]
        public string FolderPath
        {
            get
            {
                return Path.GetDirectoryName(this.pathAndFilename);
            }
        }

        /// <summary>
        ///   Gets the media info scan output.
        /// </summary>
        /// <value> The media info scan output. </value>
        [JsonIgnore]
        public MiResponseModel MiResponseModel
        {
            get
            {
                var responseModel = new MiResponseModel();

                if (File.Exists(this.PathAndFilename + ".mediainfo"))
                {
                    var xml = IO.ReadTextFromFile(this.PathAndFilename + ".mediainfo");
                    responseModel.PopulateFromXml(xml);
                    return responseModel;
                }

                return new MiResponseModel();
            }
        }

        /// <summary>
        ///   Gets or sets the order.
        /// </summary>
        /// <value> The order. </value>
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
        ///   Gets or sets FilePath.
        /// </summary>
        public string PathAndFilename
        {
            get
            {
                return this.pathAndFilename;
            }

            set
            {
                this.pathAndFilename = value;
            }
        }

        /// <summary>
        /// Gets the scan xml.
        /// </summary>
        public string ScanXml
        {
            get
            {
                return IO.ReadTextFromFile(this.PathAndFilename + ".mediainfo");
            }
        }

        #endregion
    }
}