// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="SubtitleStreamModel.cs">
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
//   A Subtitle stream model for use attached to a MediaScanOutput
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.GeneralModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   A Subtitle stream model for use attached to a MediaScanOutput
    /// </summary>
    [Serializable]
    public class SubtitleStreamModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   Backing field for CodecID
        /// </summary>
        private string codecID;

        /// <summary>
        ///   Backing field for CodecIDInfo
        /// </summary>
        private string codecIDInfo;

        /// <summary>
        ///   Backing field for Format
        /// </summary>
        private string format;

        /// <summary>
        ///   Backing field for MovieUniqueId
        /// </summary>
        private int id;

        /// <summary>
        ///   Backing field for Language
        /// </summary>
        private string language;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="SubtitleStreamModel" /> class.
        /// </summary>
        public SubtitleStreamModel()
        {
            this.Format = string.Empty;
            this.CodecID = string.Empty;
            this.CodecIDInfo = string.Empty;
            this.Language = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the codec MovieUniqueId.
        /// </summary>
        /// <value> The codec MovieUniqueId. </value>
        public string CodecID
        {
            get
            {
                return this.codecID;
            }

            set
            {
                this.codecID = value;
                this.OnPropertyChanged("CodecID");
            }
        }

        /// <summary>
        ///   Gets or sets the codec MovieUniqueId info.
        /// </summary>
        /// <value> The codec MovieUniqueId info. </value>
        public string CodecIDInfo
        {
            get
            {
                return this.codecIDInfo;
            }

            set
            {
                this.codecIDInfo = value;
                this.OnPropertyChanged("CodecIDInfo");
            }
        }

        /// <summary>
        ///   Gets or sets the format.
        /// </summary>
        /// <value> The format. </value>
        public string Format
        {
            get
            {
                return this.format;
            }

            set
            {
                this.format = value;
                this.OnPropertyChanged("Format");
            }
        }

        /// <summary>
        ///   Gets or sets MovieUniqueId.
        /// </summary>
        public int ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.OnPropertyChanged("MovieUniqueId");
            }
        }

        /// <summary>
        ///   Gets or sets the language.
        /// </summary>
        /// <value> The language. </value>
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                this.language = value;
                this.OnPropertyChanged("Language");
            }
        }

        #endregion
    }
}