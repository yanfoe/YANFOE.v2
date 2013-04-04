// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scraper.cs" company="The YANFOE Project">
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
    using System.Text.RegularExpressions;

    using YANFOE.Settings.UserSettings.ScraperSettings;

    /// <summary>
    /// The scraper.
    /// </summary>
    [Serializable]
    public class Scraper
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scraper"/> class.
        /// </summary>
        public Scraper()
        {
            this.TvDbTime = string.Empty;
            this.Generic = new Generic();

            this.TmDBDownloadPosterSize = 1;
            this.TmDBDownloadFanartSize = 0;
            this.TvDBLanguage = "English (en)";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the generic.
        /// </summary>
        /// <value>
        /// The generic.
        /// </value>
        public Generic Generic { get; set; }

        /// <summary>
        /// Gets or sets TvDbTime.
        /// </summary>
        /// <value>
        /// The tv db time.
        /// </value>
        public string TvDbTime { get; set; }

        #endregion

        #region TheMovieDB

        public int TmDBDownloadPosterSize { get; set; }

        public int TmDBDownloadFanartSize { get; set; }

        #endregion

        #region TvDB

        public string TvDBLanguage { get; set; }

        public string TvDBLanguageAbbr
        {
            get
            {
                return Regex.Match(this.TvDBLanguage, @"\((?<abbr>.{2})\)").Groups["abbr"].Value;
            }
        }

        #endregion
    }
}