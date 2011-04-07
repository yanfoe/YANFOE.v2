// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadItem.cs" company="The YANFOE Project">
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
    using System;
    using System.Net;

    using YANFOE.InternalApps.DownloadManager.Interfaces;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// The download type.
    /// </summary>
    public enum DownloadType
    {
        /// <summary>
        /// Type of html.
        /// </summary>
        Html, 

        /// <summary>
        /// Type of binary.
        /// </summary>
        Binary,

        /// <summary>
        /// Type of Applebinary.
        /// </summary>
        AppleBinary
    }

    /// <summary>
    /// The download item.
    /// </summary>
    [Serializable]
    public class DownloadItem
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadItem"/> class.
        /// </summary>
        public DownloadItem()
        {
            this.Type = new DownloadType();

            this.Url = string.Empty;

            this.Progress = new Progress();

            this.CookieContainer = new CookieContainer();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets CookieContainer.
        /// </summary>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IgnoreCache.
        /// </summary>
        public bool IgnoreCache { get; set; }

        /// <summary>
        /// Gets or sets Progress.
        /// </summary>
        public Progress Progress { get; set; }

        /// <summary>
        /// Gets or sets Result.
        /// </summary>
        public IResult Result { get; set; }

        /// <summary>
        /// Gets or sets Section.
        /// </summary>
        public Section Section { get; set; }

        /// <summary>
        /// Gets or sets ThreadID.
        /// </summary>
        public int ThreadID { get; set; }

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public DownloadType Type { get; set; }

        /// <summary>
        /// Gets or sets Url.
        /// </summary>
        public string Url { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The add new url.
        /// </summary>
        /// <param name="url">The download url.</param>
        /// <param name="type">The download type.</param>
        /// <param name="section">The download section.</param>
        /// <param name="threadId">The thread id.</param>
        /// <param name="ignoreCache">Ignore cache.</param>
        /// <param name="cookieContainer">The cookie container.</param>
        /// <returns>Return DownloadItem</returns>
        public static DownloadItem AddNewUrl(
            string url, 
            DownloadType type, 
            Section section, 
            int threadId, 
            bool ignoreCache, 
            CookieContainer cookieContainer = null)
        {
            return new DownloadItem
                {
                    Url = url,
                    Type = type,
                    Section = section,
                    CookieContainer = cookieContainer,
                    IgnoreCache = ignoreCache
                };
        }

        #endregion
    }
}