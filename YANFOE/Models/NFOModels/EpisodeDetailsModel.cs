// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="EpisodeDetailsModel.cs">
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
//   Mdoel for TVEpisode details
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.NFOModels
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   Mdoel for TVEpisode details
    /// </summary>
    [Serializable]
    public class EpisodeDetailsModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="EpisodeDetailsModel" /> class.
        /// </summary>
        public EpisodeDetailsModel()
        {
            this.Season = -1;
            this.Episode = -1;
            this.Title = string.Empty;
            this.Plot = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Episode.
        /// </summary>
        public int Episode { get; set; }

        /// <summary>
        ///   Gets or sets Plot.
        /// </summary>
        public string Plot { get; set; }

        /// <summary>
        ///   Gets or sets Season.
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        ///   Gets or sets Title.
        /// </summary>
        public string Title { get; set; }

        #endregion
    }
}