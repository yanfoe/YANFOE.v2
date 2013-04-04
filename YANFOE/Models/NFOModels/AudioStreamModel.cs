// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AudioStreamModel.cs">
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
//   Audio Stream Model
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.NFOModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   Audio Stream Model
    /// </summary>
    [Serializable]
    public class AudioStreamModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   Backing frield for channels property
        /// </summary>
        private int channels;

        /// <summary>
        ///   Backing frield for codec property
        /// </summary>
        private string codec;

        /// <summary>
        ///   Backing frield for language property
        /// </summary>
        private string language;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="AudioStreamModel" /> class.
        /// </summary>
        public AudioStreamModel()
        {
            this.Codec = string.Empty;
            this.Language = string.Empty;
            this.Channels = -1;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the channels.
        /// </summary>
        /// <value> The channels. </value>
        public int Channels
        {
            get
            {
                return this.channels;
            }

            set
            {
                this.channels = value;
                this.OnPropertyChanged("Channels");
            }
        }

        /// <summary>
        ///   Gets or sets Codec.
        /// </summary>
        public string Codec
        {
            get
            {
                return this.codec;
            }

            set
            {
                this.codec = value;
                this.OnPropertyChanged("Codec");
            }
        }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
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