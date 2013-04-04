// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanSeriesPick.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.TvModels.Scan
{
    using System;

    /// <summary>
    /// The scan series pick.
    /// </summary>
    [Serializable]
    public class ScanSeriesPick
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanSeriesPick"/> class.
        /// </summary>
        public ScanSeriesPick()
        {
            this.SearchString = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets SearchString.
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        /// Gets or sets SeriesID.
        /// </summary>
        public string SeriesID { get; set; }

        /// <summary>
        /// Gets or sets SeriesName.
        /// </summary>
        public string SeriesName { get; set; }

        #endregion
    }
}