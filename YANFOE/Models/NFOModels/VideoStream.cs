// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="VideoStream.cs">
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
//   Contains VideoStream information from an NFO.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.NFOModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   Contains VideoStream information from an NFO.
    /// </summary>
    [Serializable]
    public class VideoStream : ModelBase
    {
        #region Fields

        /// <summary>
        ///   Aspect Ratio Backing Field
        /// </summary>
        private string aspect;

        /// <summary>
        ///   Codec Backing Field.
        /// </summary>
        private string codec;

        /// <summary>
        ///   Height Backing Field
        /// </summary>
        private int? height;

        /// <summary>
        ///   Width Backing Field
        /// </summary>
        private int? width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="VideoStream" /> class.
        /// </summary>
        public VideoStream()
        {
            this.Codec = string.Empty;
            this.Aspect = string.Empty;
            this.Width = null;
            this.Height = null;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the aspect.
        /// </summary>
        /// <value> The aspect. </value>
        public string Aspect
        {
            get
            {
                return this.aspect;
            }

            set
            {
                this.aspect = value;
                this.OnPropertyChanged("Aspect");
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
        ///   Gets or sets the height.
        /// </summary>
        /// <value> The height. </value>
        public int? Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
                this.OnPropertyChanged("Height");
            }
        }

        /// <summary>
        ///   Gets or sets Width.
        /// </summary>
        public int? Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
                this.OnPropertyChanged("Width");
            }
        }

        #endregion
    }
}