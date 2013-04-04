// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImageDetailsModel.cs">
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
//   The image details model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Models
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    /// The image details model.
    /// </summary>
    [Serializable]
    public class ImageDetailsModel : ModelBase
    {
        #region Fields

        /// <summary>
        /// The height.
        /// </summary>
        private int height;

        /// <summary>
        /// The uri full.
        /// </summary>
        private Uri uriFull;

        /// <summary>
        /// The uri thumb.
        /// </summary>
        private Uri uriThumb;

        /// <summary>
        /// The width.
        /// </summary>
        private int width;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height
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
        ///   Gets or sets the URL.
        /// </summary>
        /// <value> The image URL. </value>
        public Uri UriFull
        {
            get
            {
                return this.uriFull;
            }

            set
            {
                this.uriFull = value;
                this.OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// Gets or sets the uri thumb.
        /// </summary>
        public Uri UriThumb
        {
            get
            {
                return this.uriThumb;
            }

            set
            {
                this.uriThumb = value;
                this.OnPropertyChanged("Url");
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width
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