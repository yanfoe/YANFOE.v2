// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilmUp.cs" company="The YANFOE Project">
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
    /// Scraper for FilmUp
    /// </summary>
    public class FilmUp : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilmUp"/> class.
        /// </summary>
        public FilmUp()
        {
            this.ScraperName = ScraperList.FilmUp;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "http://filmup.leonardo.it/sc_{0}.htm" }
                            };

            this.UrlHtmlCache = new Dictionary<string, string>();

            this.AvailableSearchMethod = new BindingList<ScrapeSearchMethod>();
            this.AvailableSearchMethod.AddRange(new[]
                                                {
                                                    ScrapeSearchMethod.Bing,
                                                    ScrapeSearchMethod.Site
                                                });

            this.AvailableScrapeMethods = new BindingList<ScrapeFields>();
            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Title,
                                                   ScrapeFields.OrigionalTitle,
                                                   ScrapeFields.Year,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Studio,
                                                   ScrapeFields.ReleaseDate,
                                                   ScrapeFields.Poster
                                               });

            this.HtmlEncoding = Encoding.GetEncoding("iso-8859-1");
            this.HtmlBaseUrl = "filmup";
        }

        /// <summary>
        /// Searches bing for the scraper MovieUniqueId.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            try
            {
                query.Results = Bing.SearchBing(
                    string.Format("{0} {1} site:filmup.leonardo.it/", query.Title, query.Year),
                    "http://filmup.leonardo.it/sc_",
                    threadID,
                    string.Empty,
                    string.Empty,
                    string.Empty);

                return query.Results.Count > 0;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
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
                var url = string.Format("http://filmup.leonardo.it/cgi-bin/search.cgi?q={0}", query.Title);

                var html = Downloader.ProcessDownload(url, DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                var matches = Regex.Matches(
                    html, 
                    @"href=""(?<url>http://filmup.leonardo.it/sc_.{1,20}.htm)?"" TARGET=""_blank"">\s(?<title>.*?)\(.*?(?<year>\d{4})", 
                    RegexOptions.IgnoreCase);

                foreach (Match m in matches)
                {
                    query.Results.Add(new QueryResult
                                          {
                                              Title = m.Groups["title"].Value,
                                              Year = m.Groups["year"].Value.ToInt(),
                                              URL = m.Groups["url"].Value
                                          });
                }

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
                    @"size=""3""><b>(?<title>.*?)</b></font",
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
        /// Scrapes the Origional Title value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Origional Title value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeOrigionalTitle(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                output = YRegex.Match(
                    @">Titolo\so.*?e:.*?=""2"">(?<origionaltitle>.*?)</f",
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
                        @"Anno:.*?=""2"">(?<year>\d{4})",
                        this.GetHtml("main", threadID, id),
                        "title")
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
                var url = YRegex.Match(
                    @"uid=(?<id>\d.*?)""", 
                    this.GetHtml("main", threadID, id), 
                    "id");

                if (string.IsNullOrEmpty(url))
                {
                    return false;
                }

                var html = Downloader.ProcessDownload(string.Format("http://filmup.leonardo.it/opinioni/op.php?uid={0}", url), DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                var ratingstring = YRegex.Match(
                    @"Media Voto.*?<b>(?<rating>.*?)</b>\s-\s<img", 
                    html,
                    "rating");

                output = YRegex.Match(
                        @"Media Voto.*?<b>(?<rating>.*?)</b>\s-\s<img",
                        ratingstring,
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
                    @"Regia:.*?=""2"">(?<director>.*?)</font",
                    this.GetHtml("main", threadID, id),
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
                    @">Trama:<br>(?<plot>.*?)</",
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
                    @"Genere:.*?=""2"">(?<genre>.*?)</font",
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
                output = YRegex.MatchesToPersonList(
                    "Cast.*?=\"2\">(?<cast>.*?)</f",
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
                    "Distribuzione.*?=\"2\">(?<studio>.*?)</f",
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
                var releasedate = Regex.Match(
                    this.GetHtml("main", threadID, id), 
                    @"Data\sdi\suscita:.*?=""2"">(?<day>\d*)\s(?<month>.*?)\s(?<year>\d{4})\s\(");

                if (releasedate.Success)
                {
                    var day = Convert.ToInt32(releasedate.Groups["day"].Value);
                    var monthstring = releasedate.Groups["month"].Value;
                    var month = 0;
                    var year = Convert.ToInt32(releasedate.Groups["year"].Value);

                    switch (monthstring.ToLower())
                    {
                        case "gennaio":
                            month = 1;
                            break;
                        case "febbraio":
                            month = 2;
                            break;
                        case "marzo":
                            month = 3;
                            break;
                        case "aprile":
                            month = 4;
                            break;
                        case "maggio":
                            month = 5;
                            break;
                        case "giugno":
                            month = 6;
                            break;
                        case "luglio":
                            month = 7;
                            break;
                        case "agosto":
                            month = 8;
                            break;
                        case "settembre":
                            month = 9;
                            break;
                        case "ottobre":
                            month = 10;
                            break;
                        case "novembre":
                            month = 11;
                            break;
                        case "dicembre":
                            month = 12;
                            break;
                    }

                    output = new DateTime(year, month, day);
                }

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
                var url = YRegex.Match(
                    @"cards\.cgi"".*?href=""(?<posterHtmlUrl>.*?)""", 
                    this.GetHtml("main", threadID, id),
                    "posterHtmlUrl");

                if (!string.IsNullOrEmpty(url))
                {
                    var posterHtml = Downloader.ProcessDownload("http://filmup.leonardo.it/" + url, DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                    var posterImg = YRegex.Match(
                        @"/loc/500/(?<poster>.*?)\.jpg""", 
                        posterHtml, 
                        "poster");

                    if (!string.IsNullOrEmpty(posterImg))
                    {
                        output.Add(
                            new ImageDetailsModel
                                {
                                    UriFull = new Uri(string.Format("http://filmup.leonardo.it/posters/loc/500/{0}.jpg", posterImg))
                                });
                    }
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
