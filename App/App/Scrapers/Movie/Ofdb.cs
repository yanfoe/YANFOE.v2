// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Ofdb.cs" company="The YANFOE Project">
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
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;

    public class Ofdb : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ofdb"/> class.
        /// </summary>
        public Ofdb()
        {
            this.ScraperName = ScraperList.OFDB;

            this.Urls = new Dictionary<string, string>
                {
                    { "main", "http://http://www.ofdb.de/film/{0}" }
                };

            this.UrlHtmlCache = new Dictionary<string, string>();

            this.AvailableSearchMethod = new BindingList<ScrapeSearchMethod>();

            this.AvailableSearchMethod.AddRange(new[]
                                                    {
                                                        ScrapeSearchMethod.Bing,
                                                    });

            this.AvailableScrapeMethods = new BindingList<ScrapeFields>();

            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Title,
                                                   ScrapeFields.Year,
                                                   ScrapeFields.OrigionalTitle,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Genre
                                               });

            this.HtmlEncoding = Encoding.UTF8;
            this.HtmlBaseUrl = "ofdb.de";

            this.BingRegexMatchTitle = @"(?<title>.*?)\s\((?<year>\d{4}) - IMDb";
            this.BingRegexMatchYear = @"(?<title>.*?)\s\((?<year>\d{4}) - IMDb";
            this.BingRegexMatchID = "http://www.ofdb.de/film/(?<id>.*)";
        }

        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            query.Results = new BindingList<QueryResult>();

            try
            {
                query.Results = Bing.SearchBing(
                    string.Format(CultureInfo.CurrentCulture, "{0} {1} site:http://www.ofdb.de", query.Title, query.Year),
                    string.Empty,
                    threadID,
                    BingRegexMatchTitle,
                    BingRegexMatchYear,
                    BingRegexMatchID,
                    ScraperList.Imdb);

                return query.Results.Count > 0;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Title value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Title value.</param>
        /// <param name="alternatives">Alternative namings found for a title.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory)
        {
            output = string.Empty;
            alternatives = new BindingList<string>();

            try
            {
                var html = this.GetHtml("main", threadID, id);

                output = YRegex.Match(@"<title>OFDb\s-\s(?<title>.*?)\s\((?<year>\d{4})\)</title>", html, "title", true)
                    .Trim();

                output = Tools.Clean.Text.ValidizeResult(output);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Year value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeYear(string id, int threadID, out int output, string logCatagory)
        {
            output = 0;

            try
            {
                var html = this.GetHtml("main", threadID, id);

                output = YRegex.Match(@"<title>OFDb\s-\s(?<title>.*?)\s\((?<year>\d{4})\)</title>", html, "title", true)
                    .ToInt();

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }
    }
}
