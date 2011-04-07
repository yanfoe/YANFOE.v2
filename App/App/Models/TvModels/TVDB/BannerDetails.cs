// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BannerDetails.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.TvModels.TVDB
{
    using System;

    /// <summary>
    /// The banner details.
    /// </summary>
    [Serializable]
    public class BannerDetails
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerDetails"/> class.
        /// </summary>
        public BannerDetails()
        {
            this.ID = null;

            this.BannerPath = string.Empty;

            this.BannerType = BannerType.None;

            this.BannerType2 = BannerType2.blank;

            this.Colors = string.Empty;

            this.Language = string.Empty;

            this.SeriesName = string.Empty;

            this.ThumbnailPath = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets BannerPath.
        /// </summary>
        public string BannerPath { get; set; }

        /// <summary>
        /// Gets or sets BannerType.
        /// </summary>
        public BannerType BannerType { get; set; }

        /// <summary>
        /// Gets or sets BannerType2.
        /// </summary>
        public BannerType2 BannerType2 { get; set; }

        /// <summary>
        /// Gets or sets Colors.
        /// </summary>
        public string Colors { get; set; }

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public uint? ID { get; set; }

        /// <summary>
        /// Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets Season.
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Gets or sets SeriesName.
        /// </summary>
        public string SeriesName { get; set; }

        /// <summary>
        /// Gets or sets ThumbnailPath.
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Gets or sets VignettePath.
        /// </summary>
        public string VignettePath { get; set; }

        #endregion
    }
}