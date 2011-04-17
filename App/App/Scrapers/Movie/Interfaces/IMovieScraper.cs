// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMovieScraper.cs" company="The YANFOE Project">
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

namespace YANFOE.Scrapers.Movie.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;

    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// Interface for all movie scrapers.
    /// </summary>
    public interface IMovieScraper
    {
        /// <summary>
        /// Gets or sets the html encoding.
        /// </summary>
        /// <value>
        /// The scrape encoding.
        /// </value>
        Encoding HtmlEncoding { get; set; }

        /// <summary>
        /// Gets or sets the HTML base.
        /// </summary>
        /// <value>
        /// The HTML base.
        /// </value>
        string HtmlBaseUrl { get; set; }

        /// <summary>
        /// Gets the name of the scraper.
        /// </summary>
        /// <value>The name of the scraper.</value>
        ScraperList ScraperName { get; }

        /// <summary>
        /// Gets or sets the available methods.
        /// </summary>
        /// <value>The available methods.</value>
        BindingList<ScrapeFields> AvailableScrapeMethods { get; set; }

        /// <summary>
        /// Gets or sets the scrape search method.
        /// </summary>
        /// <value>The scrape search method.</value>
        BindingList<ScrapeSearchMethod> AvailableSearchMethod { get; set; }

        /// <summary>
        /// Gets or sets the urls storage object.
        /// </summary>
        /// <value>The urls storage object.</value>
        Dictionary<string, string> Urls { get; set; }

        /// <summary>
        /// Gets or sets the an item in the HTML cache.
        /// </summary>
        /// <value>The URL HTML cache string.</value>
        Dictionary<string, string> UrlHtmlCache { get; set; }

        #region Searching Methods

        /// <summary>
        /// Searches bing for the scraper MovieUniqueId.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        bool SearchViaBing(Query query, int threadID, string logCatagory);

        /// <summary>
        /// Searches the sites search engine for movies.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        bool SearchSite(Query query, int threadID, string logCatagory);

        /// <summary>
        /// Searches the YANFOE database for MovieUniqueId's
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        bool SearchYANFOE(Query query, int threadID, string logCatagory);

        #endregion
        #region Scraping Methods

        /// <summary>
        /// Scrapes the Title value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Title value.</param>
        /// <param name="alternatives">Alternative namings found for a title.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory);

        /// <summary>
        /// Scrapes the Original Title value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Original Title value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeOriginalTitle(string id, int threadID, out string output, string logCatagory);

        /// <summary>
        /// Scrapes the Year value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeYear(string id, int threadID, out int output, string logCatagory);

        /// <summary>
        /// Scrapes the Rating value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Reting value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeRating(string id, int threadID, out double output, string logCatagory);

        /// <summary>
        /// Scrapes the Director value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Director value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeDirector(string id, int threadID, out BindingList<PersonModel> output, string logCatagory);

        /// <summary>
        /// Scrapes the Plot value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Plot value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <param name="returnShort">if set to <c>true</c> short plot is returned if available.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapePlot(string id, int threadID, out string output, string logCatagory, bool returnShort = false);

        /// <summary>
        /// Scrapes the Outline value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Outline value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeOutline(string id, int threadID, out string output, string logCatagory);

        /// <summary>
        /// Scrapes the Certification value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Certification value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeCertification(string id, int threadID, out string output, string logCatagory);

        /// <summary>
        /// Scrapes the Mpaa descrion
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped mpaa value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeMpaa(string id, int threadID, out string output, string logCatagory);

        /// <summary>
        /// Scrapes the Country copllection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Country collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeCountry(string id, int threadID, out BindingList<string> output, string logCatagory);

        /// <summary>
        /// Scrapes the Language collection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Language collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeLanguage(string id, int threadID, out BindingList<string> output, string logCatagory);

        /// <summary>
        /// Scrapes the Genre collection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeGenre(string id, int threadID, out BindingList<string> output, string logCatagory);

        /// <summary>
        /// Scrapes the Cast collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Cast value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeCast(string id, int threadID, out BindingList<PersonModel> output, string logCatagory);

        /// <summary>
        /// Scrapes the tagline value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped tagline value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeTagline(string id, int threadID, out string output, string logCatagory);

        /// <summary>
        /// Scrapes the Top250 value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Top250 value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeTop250(string id, int threadID, out int output, string logCatagory);

        /// <summary>
        /// Scrapes the studio value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped studio value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeStudio(string id, int threadID, out BindingList<string> output, string logCatagory);

        /// <summary>
        /// Scrapes the votes value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped votes value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeVotes(string id, int threadID, out int output, string logCatagory);

        /// <summary>
        /// Scrapes the release date value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped release date value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeReleaseDate(string id, int threadID, out DateTime output, string logCatagory);

        /// <summary>
        /// Scrapes the runtime value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeRuntime(string id, int threadID, out int output, string logCatagory);

        /// <summary>
        /// Scrapes the writers value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeWriters(string id, int threadID, out BindingList<PersonModel> output, string logCatagory);

        /// <summary>
        /// Scrapes the poster image collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped poster image collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapePoster(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory);

        /// <summary>
        /// Scrapes the fanart image collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped fanart image collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeFanart(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory);

        /// <summary>
        /// Scrapes the trailer collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped trailer collection.</param>
        /// <param name="logCatagory">The catagory sent to the log if an error occurs.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        bool ScrapeTrailer(string id, int threadID, out BindingList<TrailerDetailsModel> output, string logCatagory);

        #endregion

        /// <summary>
        /// Returns HTML related to URL.
        /// </summary>
        /// <param name="url">The URL key name</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="id">The to replace on the url</param>
        /// <returns>Html code contained within value.</returns>
        string GetHtml(string url, int threadID, string id);
    }
}
