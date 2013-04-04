// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Database.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.TvModels
{
    using System;
    using System.Collections.Generic;

    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;

    /// <summary>
    /// The database.
    /// </summary>
    [Serializable]
    public class Database
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database()
        {
            this.Shows = new SortedList<string, Series>();
            this.Scan = new SortedDictionary<string, ScanSeries>();
            this.NotCatagorized = new List<ScanNotCatagorized>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the not catagorized collection.
        /// </summary>
        /// <value>
        /// The not catagorized collection.
        /// </value>
        public List<ScanNotCatagorized> NotCatagorized { get; set; }

        /// <summary>
        /// Gets or sets the scan collection.
        /// </summary>
        /// <value>
        /// The scan collection.
        /// </value>
        public SortedDictionary<string, ScanSeries> Scan { get; set; }

        /// <summary>
        /// Gets or sets the shows collection.
        /// </summary>
        /// <value>
        /// The shows collection.
        /// </value>
        public SortedList<string, Series> Shows { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The add scan result.
        /// </summary>
        /// <param name="seriesName">The series name.</param>
        /// <param name="seasonNumber">The season number.</param>
        /// <param name="episodeNumber">The episode number.</param>
        /// <param name="filePath">The file path.</param>
        public void AddScanResult(string seriesName, int seasonNumber, int episodeNumber, string filePath)
        {
            // Process Series
            if (!this.Scan.ContainsKey(seriesName))
            {
                this.Scan.Add(seriesName, new ScanSeries());
            }

            // Process Series
            if (!this.Scan[seriesName].Seasons.ContainsKey(seasonNumber))
            {
                this.Scan[seriesName].Seasons.Add(seasonNumber, new ScanSeason());
            }

            if (!this.Scan[seriesName].Seasons[seasonNumber].Episodes.ContainsKey(episodeNumber))
            {
                this.Scan[seriesName].Seasons[seasonNumber].Episodes.Add(episodeNumber, new ScanEpisode());
            }

            this.Scan[seriesName].Seasons[seasonNumber].Episodes[episodeNumber].FilePath = filePath;
        }

        #endregion
    }
}