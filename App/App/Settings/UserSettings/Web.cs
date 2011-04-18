// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Web.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YANFOE.Scrapers.Movie;
    using YANFOE.Tools.Models;

    /// <summary>
    /// Contains all web related settings.
    /// </summary>
    [Serializable]
    public class Web : ModelBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Web"/> class.
        /// </summary>
        public Web()
        {
            this.ContructDefaultValues();
            this.WebEncodings = new Dictionary<string, Encoding>();

            this.BuildWebEncodings();
        }

        private void BuildWebEncodings()
        {
            var scrapers = MovieScraperHandler.ReturnAllScrapers();

            foreach (var scraper in scrapers)
            {
                if (scraper.HtmlBaseUrl != null && scraper.HtmlEncoding != null)
                {
                    this.WebEncodings.Add(scraper.HtmlBaseUrl, scraper.HtmlEncoding);
                }
            }
        }

        #endregion

        #region Properties

        public Dictionary<string, Encoding> WebEncodings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EnableAddToBackgroundQue.
        /// </summary>
        public bool EnableAddToBackgroundQue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EnableBackgroundQueProcessing.
        /// </summary>
        public bool EnableBackgroundQueProcessing { get; set; }

        /// <summary>
        /// Gets or sets the proxy ip.
        /// </summary>
        /// <value>The proxy ip.</value>
        public string ProxyIP { get; set; }

        /// <summary>
        /// Gets or sets the proxy password.
        /// </summary>
        /// <value>The proxy password.</value>
        public string ProxyPassword { get; set; }

        /// <summary>
        /// Gets or sets the proxy port.
        /// </summary>
        /// <value>The proxy port.</value>
        public int? ProxyPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a proxy should be used.
        /// </summary>
        /// <value><c>true</c> if [proxy use]; otherwise, <c>false</c>.</value>
        public bool ProxyUse { get; set; }

        /// <summary>
        /// Gets or sets the name of the proxy user.
        /// </summary>
        /// <value>The name of the proxy user.</value>
        public string ProxyUserName { get; set; }

        /// <summary>
        /// Gets or sets the user agent header value.
        /// </summary>
        /// <value>The user agent.</value>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the QuickTime user agent header value.
        /// </summary>
        /// <value>The user agent.</value>
        public string QTUserAgent { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Populate the values with default values.
        /// </summary>
        private void ContructDefaultValues()
        {
            this.UserAgent = "Mozilla/5.0 (Windows; U; MSIE 9.0; Windows NT 9.0; en-US)";
            this.QTUserAgent = "QuickTime/7.6.9 (qtver=7.6.9;os=Windows+NT+6.1)";
            this.ProxyIP = string.Empty;
            this.ProxyPort = null;
            this.ProxyUserName = string.Empty;
            this.ProxyPassword = string.Empty;
            this.EnableAddToBackgroundQue = true;
            this.EnableBackgroundQueProcessing = true;
        }

        #endregion
    }
}