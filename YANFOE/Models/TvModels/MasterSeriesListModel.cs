// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MasterSeriesListModel.cs">
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
//   The master series list model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The master series list model.
    /// </summary>
    [Serializable]
    public class MasterSeriesListModel : ModelBase
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets BannerPath.
        /// </summary>
        public string BannerPath { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether this <see cref="MasterSeriesListModel" /> is locked.
        /// </summary>
        /// <value> <c>true</c> if locked; otherwise, <c>false</c> . </value>
        public bool Locked { get; set; }

        /// <summary>
        ///   Gets or sets SeriesGuid.
        /// </summary>
        public string SeriesGuid { get; set; }

        /// <summary>
        ///   Gets or sets SeriesName.
        /// </summary>
        public string SeriesName { get; set; }

        #endregion
    }
}