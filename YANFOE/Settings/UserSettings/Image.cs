// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.UserSettings
{
    using System;

    /// <summary>
    /// Image related settings.
    /// </summary>
    [Serializable]
    public class Image
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.ConstructDefaultValues();
        }

        /// <summary>
        /// Gets or sets a value indicating whether UseDefaultImagePathForTV.
        /// </summary>
        public bool UseDefaultImagePathForTV { get; set; }

        /// <summary>
        /// Gets or sets DefaultImagePathForTV.
        /// </summary>
        public string DefaultImagePathForTV { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether UseDefaultImagePathForMovies.
        /// </summary>
        public bool UseDefaultImagePathForMovies { get; set; }

        /// <summary>
        /// Gets or sets DefaultImagePathForMovies.
        /// </summary>
        public string DefaultImagePathForMovies { get; set; }

        /// <summary>
        /// The construct default values.
        /// </summary>
        private void ConstructDefaultValues()
        {
            this.DefaultImagePathForTV = string.Empty;
            this.DefaultImagePathForMovies = string.Empty;
        }
    }
}