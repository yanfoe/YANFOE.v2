// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageDetailsModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Models
{
    using System;

    [Serializable]
    public class ImageDetailsModel : ModelBase
    {
        private Uri uriFull;

        private Uri uriThumb;

        private int width;

        private int height;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The image URL.</value>
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
    }
}