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

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;

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
                    { "main", "http://www.ofdb.de/film/{0}" },
                    { "cast", "http://www.ofdb.de/view.php?page=film_detail&fid={0}"}
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
                                                   ScrapeFields.OriginalTitle,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Genre
                                               });

            this.HtmlEncoding = Encoding.UTF8;
            this.HtmlBaseUrl = "ofdb";

            this.BingRegexMatchTitle = @"OFDb - (?<title>.*) \((?<year>\d{4})\)";
            this.BingRegexMatchYear = @"OFDb - (?<title>.*) \((?<year>\d{4})\)";
            this.BingRegexMatchID = "http://www.ofdb.de/film/(?<id>.*)";
        }

        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            query.Results = new BindingList<QueryResult>();

            try
            {
                var year = query.Year;

                if (year == "0")
                {
                    year = string.Empty;
                }

                query.Results = Bing.SearchBing(
                    string.Format(CultureInfo.CurrentCulture, @"{0} {1} site:http://www.ofdb.de/film", query.Title, year),
                    HtmlBaseUrl,
                    threadID,
                    BingRegexMatchTitle,
                    BingRegexMatchYear,
                    BingRegexMatchID,
                    ScraperName);

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

                output = YRegex.Match(@"<title>OFDb\s-\s(?<title>.*?)\s\((?<year>\d{4})\)</title>", html, "year", true)
                    .ToInt();

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Original Title value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Original Title value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeOriginalTitle(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

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
        /// Scrapes the Cast collection.
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Cast value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeCast(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var html = this.GetHtml("cast", threadID, id);

                var castBlockHtml = YRegex.Match(@"Darsteller(?<castblock>.*?)Drehbuchautor", html, "castblock");

                output =
                    YRegex.MatchesToPersonList(
                        @"src=""thumbnail\.php\?cover=(?<image>.*?)&size=6"".*?<b>(?<name>.*?)</b>.*?\.\.\.(?<role>.*?)</font></td>",
                        castBlockHtml,
                        "name",
                        "role",
                        "image");

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Country copllection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Country collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeCountry(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();

            try
            {
                var html = this.GetHtml("main", threadID, id);

                output = YRegex.MatchesToList("Kat=Land&Text=(?<country>.*?)\"", html, "country", true);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Plot value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Plot value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <param name="returnShort">if set to <c>true</c> short plot is returned if available.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapePlot(string id, int threadID, out string output, string logCatagory, bool returnShort)
        {
            output = string.Empty;

            try
            {
                var main = this.GetHtml("main", threadID, id);
                var plotUrl = "http://www.ofdb.de/plot/" + YRegex.Match("plot/(?<ploturl>.*?)\"", main, "ploturl");
                var plotHtml = InternalApps.DownloadManager.Downloader.ProcessDownload(plotUrl, DownloadType.Html, Section.Movies)
                    .RemoveCharacterReturn()
                    .RemoveExtraWhiteSpace();

                output = YRegex.Match(@"</b><br><br>(?<plot>.*?)\.\.\.", plotHtml, "plot", true);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Director value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Director value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeDirector(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var html = this.GetHtml("cast", threadID, id);

                var directorBlockHtml = YRegex.Match(@"Produzent\(in\)(?<directorblock>.*?)Komponist\(in\)", html, "castblock");

                output = YRegex.MatchesToPersonList(@"<a href=""view\.php\?page=person&id=\d*?""><b>(?<name>.*?)</b>", directorBlockHtml, "name", string.Empty);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the writers value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeWriters(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var html = this.GetHtml("cast", threadID, id);

                var directorBlockHtml = YRegex.Match(@"Drehbuchautor\(in\)(?<directorblock>.*?)Produzent\(in\)", html, "castblock");

                output = YRegex.MatchesToPersonList(@"<a href=""view\.php\?page=person&id=\d*?""><b>(?<name>.*?)</b>", directorBlockHtml, "name", string.Empty);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Genre collection
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeGenre(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();

            try
            {
                output = YRegex.MatchesToList(
                    @"view\.php\?page=genre&Genre=.*?"">(?<genre>.*?)</a",
                    this.GetHtml("main", threadID, id),
                    "genre",
                    true);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Rating value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Reting value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeRating(string id, int threadID, out double output, string logCatagory)
        {
            output = -1;

            try
            {
                output = YRegex.Match(
                        @"Note: (?<rating>\d.\d{1,2})",
                        this.GetHtml("main", threadID, id),
                        "rating",
                        true)
                    .ToDouble();

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
