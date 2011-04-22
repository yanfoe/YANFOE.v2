// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EpisodeDetails.cs" company="The YANFOE Project">
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
    using System.Collections.Generic;

    /// <summary>
    /// The episode details.
    /// </summary>
    public class EpisodeDetails
    {
        #region Constants and Fields

        /// <summary>
        /// The series name.
        /// </summary>
        private string seriesName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EpisodeDetails"/> class.
        /// </summary>
        public EpisodeDetails()
        {
            this.SecondaryNumbers = new List<int>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets EpisodeNumber.
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets SeasonNumber.
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Gets or sets SecondaryNumbers.
        /// </summary>
        public List<int> SecondaryNumbers { get; set; }

        /// <summary>
        /// Gets or sets SeriesName.
        /// </summary>
        public string SeriesName
        {
            get
            {
                if (this.seriesName == null)
                {
                    this.seriesName = "";
                    return this.seriesName;
                }
                return this.seriesName.Replace(".", " ");
            }

            set
            {
                this.seriesName = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether TvMatchSuccess.
        /// </summary>
        public bool TvMatchSuccess { get; set; }

        #endregion
    }
}