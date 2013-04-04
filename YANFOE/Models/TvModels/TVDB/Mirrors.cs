// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mirrors.cs" company="The YANFOE Project">
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
    /// The mirrors.
    /// </summary>
    [Serializable]
    public class Mirrors
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Mirrors"/> class.
        /// </summary>
        public Mirrors()
        {
            this.MirrorPath = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets MirrorPath.
        /// </summary>
        public string MirrorPath { get; set; }

        /// <summary>
        /// Gets or sets TypeMark.
        /// </summary>
        public int TypeMark { get; set; }

        #endregion
    }
}