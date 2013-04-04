// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Mirrors.cs">
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
//   The mirrors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.TVDB
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The mirrors.
    /// </summary>
    [Serializable]
    public class Mirrors
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Mirrors" /> class.
        /// </summary>
        public Mirrors()
        {
            this.MirrorPath = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        ///   Gets or sets MirrorPath.
        /// </summary>
        public string MirrorPath { get; set; }

        /// <summary>
        ///   Gets or sets TypeMark.
        /// </summary>
        public int TypeMark { get; set; }

        #endregion
    }
}