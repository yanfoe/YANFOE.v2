// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ScanEpisode.cs">
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
//   The scan episode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.Scan
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The scan episode.
    /// </summary>
    [Serializable]
    public class ScanEpisode
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets FilePath.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether Secondary.
        /// </summary>
        public bool Secondary { get; set; }

        /// <summary>
        ///   Gets or sets SecondaryTo.
        /// </summary>
        public int? SecondaryTo { get; set; }

        #endregion
    }
}