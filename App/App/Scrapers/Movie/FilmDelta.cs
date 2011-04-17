// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilmDelta.cs" company="The YANFOE Project">
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
    using System.Text;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The Movie scraper for FilmDelta
    /// </summary>
    public class FilmDelta : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmDelta"/> class.
        /// </summary>
        public FilmDelta()
        {
            this.ScraperName = ScraperList.FilmDelta;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "http://www.filmdelta.se/titles.php?movieId={0}" }
                            };

            this.AvailableSearchMethod.AddRange(new[]
                                        {
                                            ScrapeSearchMethod.Site
                                        });

            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Title,
                                                   ScrapeFields.OriginalTitle,
                                                   ScrapeFields.Year,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Runtime,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Poster
                                               });

            this.HtmlEncoding = Encoding.GetEncoding("iso-8859-1");
            this.HtmlBaseUrl = "filmdelta";
        }

        /// <summary>
        /// Searches the sites search engine for movies.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public new bool SearchSite(Query query, int threadID, string logCatagory)
        {
            try
            {
                var url = string.Format(
                    "http://www.filmdelta.se/search.php?string={0}&type=movie", query.Title.Replace(' ', '+'));

                var searchHtml =
                    Downloader.ProcessDownload(url, DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                var matches = Regex.Matches(
                    searchHtml,
                    @"filmer/(?<id>.*?)/.*?"">(?<title>.*?)</a>.*?(?<year>\d{4})");

                foreach (Match m in matches)
                {
                    var queryResult = new QueryResult
                                          {
                                              Title = m.Groups["title"].Value,
                                              URL = string.Format("http://www.filmdelta.se/titles.php?movieId={0}", m.Groups["id"].Value),
                                              Year = m.Groups["title"].Value.ToInt()
                                          };

                    query.Results.Add(queryResult);
                }

                return true;
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
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Title value.</param>
        /// <param name="alternatives">Alternative namings found for a title.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory)
        {
            output = string.Empty; 
            alternatives = new BindingList<string>();

            try
            {
                output = YRegex.Match(
                                @"<meta\sname=""description""\scontent=""(?<title>.*?) \((?<year>\d{4})\)", 
                                this.GetHtml("html", threadID, id),
                                "title", 
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
        /// Scrapes the Original Title value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Original Title value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeOriginalTitle(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                output = YRegex.Match(
                                @"<h4>Originaltitel</h4>\s*<h5>(?<Originaltitle>.*?)</h5>",
                                this.GetHtml("html", threadID, id),
                                "Originaltitle",
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
        /// Scrapes the Year value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeYear(string id, int threadID, out int output, string logCatagory)
        {
            output = -1;

            try
            {
                output = YRegex.Match(
                        this.GetHtml("main", threadID, id),
                        @"<meta\sname=""description""\scontent=""(?<title>.*?) \((?<year>\d{4})\)",
                        "year",
                        true)
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
        /// Scrapes the Rating value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Reting value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeRating(string id, int threadID, out double output, string logCatagory)
        {
            output = -1;

            try
            {
                var rating = YRegex.Match(
                    this.GetHtml("main", threadID, id),
                    @"Snitt: (?<rating>\d.\d.*?)",
                    "rating",
                    true);

                output = Maths.ProcessDouble(rating, 2.0).ToDouble(); 

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
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Director value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeDirector(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var directorBlock = YRegex.Match(
                    @"<h4>Regiss&ouml;r</h4>(?<directorblock>.*?)</div>",
                    this.GetHtml("main", threadID, id), 
                    "directorblock");

                output = YRegex.MatchesToPersonList(
                    @"<.*?><h5><a\shref='.*?'>(?<director>.*?)</a></h5><.*?>",
                    directorBlock, 
                    "director",
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
        /// Scrapes the Plot value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Plot value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <param name="returnShort">if set to <c>true</c> short plot is returned if available.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapePlot(string id, int threadID, out string output, string logCatagory, bool returnShort)
        {
            output = string.Empty;

            try
            {
                output = YRegex.Match(
                    @"<div class=""text"">\s+<p>(?<plot>.*?)</p>.*</div>",
                    this.GetHtml("main", threadID, id),
                    "plot",
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
        /// Scrapes the Genre collection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeGenre(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();

            try
            {
                output = YRegex.MatchesToList(
                    @"<h5><a\shref='/search\.php\?string=.*?&type=category'>(?<genre>.*?)\s</a></h5>",
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
        /// Scrapes the Cast collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Cast value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeCast(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var htmlBlock = YRegex.Match(
                    @"<h4>Sk&aring;despelare</h4>(?<castblock>.*?)</div>",
                    this.GetHtml("main", threadID, id),
                    "castblock",
                    true);

                output = YRegex.MatchesToPersonList(
                    @"<a href='.*?'>(?<name>.*?)</a>\s-\s(?<role>.*?)</h5>",
                    htmlBlock,
                    "name",
                    "role");

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the runtime value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeRuntime(string id, int threadID, out int output, string logCatagory)
        {
            output = -1;

            try
            {
                output = YRegex.Match(
                        @"</a>,\s(?<runtime>\d[0-9]*)\smin",
                        this.GetHtml("main", threadID, id),
                        "runtime",
                        true)
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
        /// Scrapes the writers value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped runtime value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeWriters(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var htmlBlock = YRegex.Match(
                    @"<h4>Manus</h4>(?<writerblock>.*?)</div>",
                    this.GetHtml("main", threadID, id),
                    "writerblock");

                output = YRegex.MatchesToPersonList(
                    @"<.*?><h5><a\shref='.*?'>(?<writer>.*?)</a></h5><.*?>",
                    htmlBlock,
                    "writer",
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
        /// Scrapes the poster image collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped poster image collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapePoster(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory)
        {
            output = new BindingList<ImageDetailsModel>();

            try
            {
                var images = YRegex.MatchesToList(
                    @"<a\sonclick=""loadBigImage\('(?<url>/functions/download\.php\?id=\d[0-9].*?)'",
                    this.GetHtml("main", threadID, id),
                    "url");

                foreach (var s in images)
                {
                    output.Add(new ImageDetailsModel
                        {
                            UriFull = new Uri("http://www.filmdelta.se" + s)
                        });
                }

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
