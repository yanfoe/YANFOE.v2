// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilmWeb.cs" company="The YANFOE Project">
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
    /// The scraper for FilmWeb
    /// </summary>
    public class FilmWeb : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmWeb"/> class.
        /// </summary>
        public FilmWeb()
        {
            this.ScraperName = ScraperList.FilmWeb;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "{0}" }
                            };

            this.UrlHtmlCache = new Dictionary<string, string>();

            this.AvailableSearchMethod = new BindingList<ScrapeSearchMethod>();
            this.AvailableSearchMethod.AddRange(new[]
                                                    {
                                                        ScrapeSearchMethod.Site
                                                    });

            this.AvailableScrapeMethods = new BindingList<ScrapeFields>();
            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Title,
                                                   ScrapeFields.Year,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Top250,
                                                   ScrapeFields.Votes,
                                                   ScrapeFields.Studio,
                                                   ScrapeFields.ReleaseDate,
                                                   ScrapeFields.Runtime,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Poster
                                               });
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
                var url =
                    string.Format(
                        "http://www.filmweb.pl/szukaj?q={0}&type=&startYear={1}&endYear={1}&startRate=&endRate=&startCount=&endCount=&sort=TEXT_SCORE&sortAscending=false",
                        query.Title.Replace(' ', '+'),
                        query.Year);

                var downloadHtml = Downloader.ProcessDownload(url, DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                query.Results.Add(
                    new QueryResult
                        {
                            URL = YRegex.Match(@"searchResultTitle""\shref=""(?<url>.*?)"">.*?</a>", downloadHtml, "url")
                        });

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
                    @"<title>(?<title>.*?)\s\((?<year>.*?)\)\s\s-\sFilm",
                    this.GetHtml("main", threadID, id),
                    "title");

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
                        @"<title>(?<title>.*?)\s\((?<year>.*?)\)\s\s-\sFilm",
                        this.GetHtml("main", threadID, id),
                    "title").ToInt();

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
                output = YRegex.Match(
                    "class=\"value\">(?<rating>.*?)</strong>/10",
                    this.GetHtml("main", threadID, id),
                    "rating")
                .ToDouble();

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
                output = YRegex.MatchesToPersonList(
                    @"reżyseria(?<director>.*?)scenariusz",
                    this.GetHtml("main", threadID, id),
                    "rating", 
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
                    @"<span></span></h2>(?<plot>.*?)\.\.\.",
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
                output = YRegex.MatchesToList(
                    @"produkcja:(?<countries>.*?)gatunek",
                    this.GetHtml("main", threadID, id),
                    "countries",
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
                    @"gatunek:.*?<a.*?>\s(?<genre>.*?)</a>",
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
                    @"<th scope=""col"">Bohater</th>(?<actorblock>.*?)zobacz więcej", 
                    this.GetHtml("main", threadID, id), 
                    "actorblock");

                htmlBlock = Regex.Replace(htmlBlock.Replace("\t", string.Empty), @"\s{2,}", " ");

                output = YRegex.MatchesToPersonList(
                    @"<img.*?rc=""(?<thumb>.*?)""\stitle="".*?""\salt=.*?>\s(?<name>.*?)</a></td>\s<td.*?>\s(?<role>.*?)\s<s.*?</tr>",
                    htmlBlock,
                    "name",
                    "role",
                    "thumb");

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Top250 value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Top250 value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeTop250(string id, int threadID, out int output, string logCatagory)
        {
            output = -1;
            try
            {
                output = YRegex.Match(
                        @"top\sświat:\s(?<top250>\d*)",
                        this.GetHtml("main", threadID, id),
                        "genre")
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
        /// Scrapes the studio value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped studio value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeStudio(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();
            try
            {
                output = YRegex.MatchesToList(
                        @"dystrybucja:</abbr>\t*\s\s(?<studio>.*?)\t",
                        this.GetHtml("main", threadID, id),
                        "studio");

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the votes value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped votes value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeVotes(string id, int threadID, out int output, string logCatagory)
        {
            output = -1;
            try
            {
                output = YRegex.Match(
                        @"głosów:.*?>(?<votes>\d.*?)<",
                        this.GetHtml("main", threadID, id),
                        "votes")
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
        /// Scrapes the release date value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped release date value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeReleaseDate(string id, int threadID, out DateTime output, string logCatagory)
        {
            output = new DateTime(1700, 1, 1);
            try
            {
                output = YRegex.MatchToDateTime(
                    @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})",
                    this.GetHtml("main", threadID, id),
                    "day",
                    "month",
                    "year");

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
                        @"trwania:\s(?<runtime>.*?)\t",
                        this.GetHtml("main", threadID, id),
                        "runtime")
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
                output = YRegex.MatchesToPersonList(
                    @"reżyseria(?<director>.*?)scenariusz",
                    this.GetHtml("main", threadID, id),
                    "runtime", 
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
                var poster = YRegex.Match(
                    @"<a\srel=""artshow""\shref=""(?<poster>.*?)"">",
                    this.GetHtml("main", threadID, id),
                    "poster");

                output.Add(new ImageDetailsModel { UriFull = new Uri(poster) });

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Returns HTML related to URL.
        /// </summary>
        /// <param name="url">The URL key name</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="id">The to replace on the url</param>
        /// <returns>Html code contained within value.</returns>
        public new string GetHtml(string url, int threadID, string id)
        {
            if (this.Cookie == null)
            {
                Downloader.ProcessDownload("http://www.filmweb.pl", DownloadType.Html, Section.Movies, false, Cookie).
                    RemoveCharacterReturn();
            }

            if (this.UrlHtmlCache.ContainsKey(url))
            {
                return this.UrlHtmlCache[url].RemoveCharacterReturn();
            }

            var html = Downloader.ProcessDownload(string.Format(this.Urls[url], id), DownloadType.Html, Section.Movies, false, Cookie).
                        RemoveCharacterReturn();

            this.UrlHtmlCache.Add(url, html);

            return html;
        }
    }
}
