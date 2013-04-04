// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ScanSeason.cs">
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
//   The scan season.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.Scan
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///   The scan season.
    /// </summary>
    [Serializable]
    public class ScanSeason
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="ScanSeason" /> class.
        /// </summary>
        public ScanSeason()
        {
            this.Episodes = new SortedDictionary<int, ScanEpisode>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Episodes.
        /// </summary>
        public SortedDictionary<int, ScanEpisode> Episodes { get; set; }

        #endregion
    }
}