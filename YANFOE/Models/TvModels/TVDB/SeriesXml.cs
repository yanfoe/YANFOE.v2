// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="SeriesXml.cs">
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
//   The series xml.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.TVDB
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The series xml.
    /// </summary>
    [Serializable]
    public class SeriesXml
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="SeriesXml" /> class.
        /// </summary>
        public SeriesXml()
        {
            this.En = string.Empty;
            this.Banners = string.Empty;
            this.Actors = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Actors.
        /// </summary>
        public string Actors { get; set; }

        /// <summary>
        ///   Gets or sets Banners.
        /// </summary>
        public string Banners { get; set; }

        /// <summary>
        ///   Gets or sets En.
        /// </summary>
        public string En { get; set; }

        #endregion
    }
}