// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="IResult.cs">
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
//   Contains information regarding a download
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.DownloadManager.Interfaces
{
    /// <summary>
    ///   Contains information regarding a download
    /// </summary>
    public interface IResult
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the resulting download, be it a string path or a html string.
        /// </summary>
        /// <value> The result. </value>
        string Result { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether the download was a success.
        /// </summary>
        bool Success { get; set; }

        #endregion
    }
}