// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScrapeSettings.cs" company="The YANFOE Project">
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

namespace YANFOE.Scrapers.Movie.Models.Scrape
{
    using System.Collections.Generic;

    using YANFOE.Tools.Enums;

    /// <summary>
    /// The Scrape Settings model.
    /// </summary>
    public class ScrapeSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrapeSettings"/> class.
        /// </summary>
        public ScrapeSettings()
        {
            this.Scrapers = new Dictionary<ScrapeFields, ScraperList>();
            this.ScraperIDs = new Dictionary<ScraperList, string>();
        }

        /// <summary>
        /// Gets or sets the scrapers.
        /// </summary>
        /// <value>The scrapers.</value>
        public Dictionary<ScrapeFields, ScraperList> Scrapers { get; set; }

        /// <summary>
        /// Gets or sets the scraper ids.
        /// </summary>
        /// <value>The scraper I ds.</value>
        public Dictionary<ScraperList, string> ScraperIDs { get; set; }

        /// <summary>
        /// Gets or sets the thread MovieUniqueId.
        /// </summary>
        /// <value>The thread MovieUniqueId.</value>
        public int ThreadID { get; set; }
    }
}
