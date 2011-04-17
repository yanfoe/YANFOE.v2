// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScraperMovieBase.cs" company="The YANFOE Project">
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

namespace YANFOE.Scrapers.Movie
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Net;
    using System.Text;

    using BitFactory.Logging;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The movie scraper base model
    /// </summary>
    public abstract class ScraperMovieBase
    {
        /// <summary>
        /// Gets or sets the name of the scraper.
        /// </summary>
        /// <value>The name of the scraper.</value>
        public ScraperList ScraperName { get; set; }

        /// <summary>
        /// Gets or sets the available methods.
        /// </summary>
        /// <value>The available methods.</value>
        public BindingList<ScrapeFields> AvailableScrapeMethods { get; set; }

        /// <summary>
        /// Gets or sets the scrape search method.
        /// </summary>
        /// <value>The scrape search method.</value>
        public BindingList<ScrapeSearchMethod> AvailableSearchMethod { get; set; }

        /// <summary>
        /// Gets or sets the URLS used in the class.
        /// </summary>
        /// <value>The URLs used in the class.</value>
        public Dictionary<string, string> Urls { get; set; }

        /// <summary>
        /// Gets or sets the an item in the HTML cache.
        /// </summary>
        /// <value>The URL HTML cache string.</value>
        public Dictionary<string, string> UrlHtmlCache { get; set; }

        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
        public CookieContainer Cookie { get; set; }

        /// <summary>
        /// Gets or sets the html encoding.
        /// </summary>
        /// <value>
        /// The scrape encoding.
        /// </value>
        public Encoding HtmlEncoding { get; set; }

        /// <summary>
        /// Gets or sets the HTML base.
        /// </summary>
        /// <value>
        /// The HTML base.
        /// </value>
        public string HtmlBaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the bing regex match title.
        /// </summary>
        /// <value>
        /// The bing regex match title.
        /// </value>
        public string BingRegexMatchTitle { get; set; }

        /// <summary>
        /// Gets or sets the bing regex match year.
        /// </summary>
        /// <value>
        /// The bing regex match year.
        /// </value>
        public string BingRegexMatchYear { get; set; }

        /// <summary>
        /// Gets or sets the bing regex match ID.
        /// </summary>
        /// <value>
        /// The bing regex match ID.
        /// </value>
        public string BingRegexMatchID { get; set; }

        /// <summary>
        /// Searches bing for the scraper MovieUniqueId.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the sites search engine for movies.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public bool SearchSite(Query query, int threadID, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Searches the YANFOE database for MovieUniqueId's
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public bool SearchYANFOE(Query query, int threadID, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Title value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Title value.</param>
        /// <param name="alternatives">Alternative namings found for a title.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Original Title value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Original Title value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeOriginalTitle(string id, int threadID, out string output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Year value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeYear(string id, int threadID, out int output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Rating value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Reting value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeRating(string id, int threadID, out double output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Director value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Director value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeDirector(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Plot value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Plot value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <param name="returnShort">if set to <c>true</c> short plot is returned if available.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapePlot(string id, int threadID, out string output, string logCatagory, bool returnShort)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Outline value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Outline value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeOutline(string id, int threadID, out string output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Certification value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Certification value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeCertification(string id, int threadID, out string output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Mpaa descrion
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped mpaa value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeMpaa(string id, int threadID, out string output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Country copllection
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Country collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeCountry(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Language collection
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Language collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeLanguage(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Genre collection
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeGenre(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Cast collection.
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Cast value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeCast(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the tagline value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped tagline value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeTagline(string id, int threadID, out string output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the Top250 value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Top250 value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeTop250(string id, int threadID, out int output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the studio value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped studio value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeStudio(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the votes value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped votes value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeVotes(string id, int threadID, out int output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the release date value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped release date value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeReleaseDate(string id, int threadID, out DateTime output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the runtime value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeRuntime(string id, int threadID, out int output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the writers value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeWriters(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the poster image collection.
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped poster image collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapePoster(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the fanart image collection.
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped fanart image collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeFanart(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scrapes the trailer.
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread ID.</param>
        /// <param name="output">The output.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public bool ScrapeTrailer(string id, int threadID, out BindingList<TrailerDetailsModel> output, string logCatagory)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns HTML related to URL.
        /// </summary>
        /// <param name="url">The URL key name</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="id">The to replace on the url</param>
        /// <returns>Html code contained within value.</returns>
        public string GetHtml(string url, int threadID, string id)
        {
            if (this.UrlHtmlCache.ContainsKey(url))
            {
                return this.UrlHtmlCache[url].RemoveCharacterReturn();
            }

            var html = Downloader.ProcessDownload(string.Format(this.Urls[url], id), DownloadType.Html, Section.Movies).RemoveCharacterReturn();

            this.UrlHtmlCache.Add(url, html);

            return html;
        }

        /// <summary>
        /// Writes an error message to the log
        /// </summary>
        /// <param name="catagory">The catagory.</param>
        /// <param name="exception">The message.</param>
        public void WriteErrorToLog(string catagory, Exception exception)
        {
            Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, catagory, exception.Message);
        }
    }
}
