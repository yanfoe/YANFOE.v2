// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlResult.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.DownloadManager.Model
{
    using YANFOE.InternalApps.DownloadManager.Interfaces;

    /// <summary>
    /// Contains a string containing HTML.
    /// </summary>
    public class HtmlResult : IResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlResult"/> class.
        /// </summary>
        public HtmlResult()
        {
            this.Result = string.Empty;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the download was a success.
        /// </summary>
        /// <value></value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the resulting download, be it a string path or a html string.
        /// </summary>
        /// <value>The result.</value>
        public string Result { get; set; }
    }
}
