// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Kinopoisk.cs" company="The YANFOE Project">
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
    /// The scraper for Kinopoisk
    /// </summary>
    public class Kinopoisk : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Kinopoisk"/> class.
        /// </summary>
        public Kinopoisk()
        {
            this.ScraperName = ScraperList.Kinopoisk;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "http://www.kinopoisk.ru/level/1/film/{0}" },
                                { "cast", "http://www.kinopoisk.ru/level/19/film/{0}" }
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
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Studio,
                                                   ScrapeFields.Runtime,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Poster
                                               });

            this.HtmlEncoding = Encoding.GetEncoding("windows-1251");
            this.HtmlBaseUrl = "kinopoisk";
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
                    string.Format("{0} site:www.kinopoisk.ru/level/1/", query.Title),
                    string.Empty,
                    threadID,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    ScraperList.Kinopoisk);

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
        /// <param name="threadID">The thread ID.</param>
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
                    "<title>(?<title>.*?)</title>", 
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
                    @"<span\sstyle=""color:\s#666;\sfont-size:\s13px"">(?<origionaltitle>.*?)</span>",
                    this.GetHtml("main", threadID, id),
                    "origionaltitle");

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
                        @""">(?<year>\d{4})</a></td></tr>",
                        this.GetHtml("main", threadID, id),
                        "year")
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
                output = YRegex.Match(
                        @"text-decoration:\snone"">(?<rating>.*?)<span\sstyle=""font:100",
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
                    "режиссер</td><td>(?<director>.*?)</td>",
                    this.GetHtml("main", threadID, id),
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
                    @"<span\sclass=""_reachbanner_"">(?<plot>.*?)</span>",
                    this.GetHtml("main", threadID, id),
                    "plot");

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
                    @"страна</td><td\sclass=""""><a\shref="".*?"">(?<country>.*?)</a>",
                    this.GetHtml("main", threadID, id),
                    "country");

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
                output = YRegex.MatchDelimitedToList(
                    "жанр(?<genre>.*?)</td></tr>",
                    this.GetHtml("main", threadID, id),
                    "genre",
                    ',',
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
                var matches = Regex.Matches(this.GetHtml("cast", threadID, id), @"<img\ssrc=""/images/sm_actor/(?<thumb>.*?)""\salt="".*?"" /></a>\s\s\s<p><a\shref="".*?"">(?<name>.*?)</a><b>.*?</b>\.\.\.\s(?<role>.*?)</p>");

                foreach (Match m in matches)
                {
                    var name = m.Groups["name"].Value;
                    var role = m.Groups["role"].Value;
                    var image = string.Empty;

                    if (!image.Contains("no-poster.gif"))
                    {
                        image = string.Format("http://www.kinopoisk.ru/images/sm_actor/{0}", m.Groups["image"].Value);
                    }

                    output.Add(new PersonModel(name, image, role));
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
        /// Scrapes the tagline value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped tagline value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeTagline(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                output = YRegex.Match(
                    "слоган</td><.*?>(?<tagline>.*?)</td></tr>", 
                    this.GetHtml("main", threadID, id), 
                    "tagline");

                output = output.Replace("&laquo;", "«");
                output = output.Replace("&raquo;", "»");

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

            var monthConvert = new Dictionary<string, int>
                                   {
                                       { "январь", 1 }, 
                                       { "февраль", 2 }, 
                                       { "март", 3 }, 
                                       { "апрель", 4 }, 
                                       { "май", 5 }, 
                                       { "июня", 6 }, 
                                       { "июль", 7 }, 
                                       { "август", 8 }, 
                                       { "сентябрь", 9 }, 
                                       { "октябрь", 10 }, 
                                       { "ноябрь", 11 }, 
                                       { "декабрь", 12 }
                                   };

            try
            {
                output = YRegex.MatchToDateTime(
                    @"(?<releasedate>(?<day>\d{2})\s(?<month>январь|февраль|март|апрель|май|июня|июль|август|сентябрь|октябрь|ноябрь|декабрь)\s(?<year>\d{4}))",
                    this.GetHtml("main", threadID, id),
                    "day",
                    "month",
                    "year",
                    monthConvert);

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
                        @"время</td><td\sclass=""time""\sid=""runtime"">(?<runtime>\d*?)\sмин",
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
                    "сценарий(?<writers>.*?)</td></tr>",
                    this.GetHtml("main", threadID, id),
                    "writers",
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
                var posterDownloadHtml =
                    Downloader.ProcessDownload(string.Format("http://www.kinopoisk.ru/level/17/film/{0}/page/1/", id), DownloadType.Html, Section.Movies).
                        RemoveCharacterReturn();

                var posterDownloadHtml2 =
                    Downloader.ProcessDownload(string.Format("http://www.kinopoisk.ru/level/17/film/{0}/page/2/", id), DownloadType.Html, Section.Movies).
                        RemoveCharacterReturn();

                if (Regex.Match(posterDownloadHtml2, "#ff6600\"><b style=\"color:#ffffff\">2</b>").Success)
                {
                    posterDownloadHtml += posterDownloadHtml2;
                }

                var matches =
                    Regex.Matches(
                        @"<a href=""/picture/(?<number>\d*)/"" target=""_blank"" title="".*?""></a>",
                        posterDownloadHtml);

                foreach (string m in matches)
                {
                    var posterDownloadHtml3 =
                        Downloader.ProcessDownload(string.Format("http://www.kinopoisk.ru/picture/{0}/", m), DownloadType.Html, Section.Movies).
                            RemoveCharacterReturn();

                    var match = Regex.Match(posterDownloadHtml3, @"<tr><td width=\d*><a href="".*?""><img alt="".*?"" src='(?<imageurl>.*?)' width='\d*' height='\d*' style="".*?"" onload=''></a></td></tr>");

                    if (match.Success)
                    {
                        output.Add(new ImageDetailsModel { UriFull = new Uri(match.Groups["imageurl"].Value) });
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

        /// <summary>
        /// Scrapes the fanart image collection.
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped fanart image collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeFanart(string id, int threadID, out BindingList<ImageDetailsModel> output, string logCatagory)
        {
            output = new BindingList<ImageDetailsModel>();

            try
            {
                var fanartDownloadHtml =
                    Downloader.ProcessDownload(string.Format("http://www.kinopoisk.ru/level/12/film/{0}/", id), DownloadType.Html, Section.Movies).
                        RemoveCharacterReturn();

                var fanartMatches = YRegex.Matches(
                    @"/picture/(?<fanart>\d*?)/w_size/1024/", 
                    fanartDownloadHtml,
                    "fanart");

                foreach (var s in fanartMatches)
                {
                    var downloadFanartHtml =
                        Downloader.ProcessDownload(string.Format("http://www.kinopoisk.ru/picture/{0}/w_size/1024/", s), DownloadType.Html, Section.Movies).
                            RemoveCharacterReturn();

                    var match = Regex.Match(
                        downloadFanartHtml,
                        " src='(?<url>http://st.*?)' width='1024'");

                    if (match.Success)
                    {
                        output.Add(new ImageDetailsModel { UriFull = new Uri(match.Groups["url"].Value) });
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
