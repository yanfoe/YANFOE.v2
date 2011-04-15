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
    using System.Globalization;
    using System.Linq;
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
    /// Defines the IMDB type.
    /// </summary>
    public class IMDB : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IMDB"/> class.
        /// </summary>
        public IMDB()
        {
            this.ScraperName = ScraperList.Imdb;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "http://www.imdb.com/title/{0}/combined" },
                                { "plot", "http://www.imdb.com/title/{0}/plotsummary" },
                                { "summary", "http://www.imdb.com/title/{0}/synopsis" },
                                { "cast", "http://www.imdb.com/title/{0}/fullcredits" },
                                { "officialsites", "http://www.imdb.com/title/{0}/officialsites" },
                                { "releaseinfo", "http://www.imdb.com/title/{0}/releaseinfo" }
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
                                                   ScrapeFields.Year,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Plot,
                                                   ScrapeFields.Certification,
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Language,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Tagline,
                                                   ScrapeFields.Top250,
                                                   ScrapeFields.Studio,
                                                   ScrapeFields.Votes,
                                                   ScrapeFields.ReleaseDate,
                                                   ScrapeFields.Runtime,
                                                   ScrapeFields.Mpaa,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Poster
                                               });

            this.HtmlEncoding = Encoding.GetEncoding("iso-8859-1");
            this.HtmlBaseUrl = "imdb";

            this.BingRegexMatchTitle = @"(?<title>.*?)\s\((?<year>\d{4}) - IMDb";
            this.BingRegexMatchYear = @"(?<title>.*?)\s\((?<year>\d{4}) - IMDb";
            this.BingRegexMatchID = @"(?<imdbid>tt\d{7})";
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
                var url = string.Format("http://www.imdb.com/find?s=all&q={0}", query.Title);

                var webPage = Downloader.ProcessDownload(url, DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                webPage = webPage
                    .ReplaceWithStringEmpty(new[] { "<b>", "</b>" })
                    .RemoveExtraWhiteSpace()
                    .RemoveCharacterReturn();

                var matches = Regex.Matches(
                    webPage,
                    @"(rc=""(?<imageurl>http://ia.media-imdb.com/images/M/.{46}@@._V1._SY30_SX23_.jpg)?"" width=""23"" height=""32"".*?)?onclick="".{1,100}"">(?<title>.{1,100})</a> \((?<year>\d{4})\) </td></tr>");

                foreach (Match m in matches)
                {
                    var queryResult = new QueryResult
                                          {
                                              URL = string.Format("http://www.imdb.com/title/{0}/combined", m.Groups["id"].Value),
                                              PosterUrl = m.Groups["imageurl"].Value.Replace("SY30_SX23", "SY500_SX500"),
                                              ImdbID = m.Groups["id"].Value,
                                              Title = m.Groups["title"].Value,
                                              Year = m.Groups["year"].Value.ToInt()
                                          };

                    query.Results.Add(queryResult);
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
        /// Searches bing for the scraper MovieUniqueId.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// [true/false] if an error occurred.
        /// </returns>
        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            query.Results = new BindingList<QueryResult>();

            try
            {
                query.Results = Bing.SearchBing(
                    string.Format(CultureInfo.CurrentCulture, "{0} {1} site:www.imdb.com", query.Title, query.Year),
                    string.Empty,
                    threadID,
                    BingRegexMatchTitle,
                    BingRegexMatchYear,
                    BingRegexMatchID);

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
                var html = this.GetHtml("main", threadID, id);
                var releaseInfoHtml = this.GetHtml("releaseinfo", threadID, id);

                output = YRegex.Match("(<title>)(.*)( [(].*</title>)", html, 2, true);

                var titleAltHtml = YRegex.Match(
                    @"\(AKA\)</a></h5><table\sborder=""0""\scellpadding=""2"">(?<html>.*?)</tr></table>",
                    releaseInfoHtml, 
                    "html");

                var altTitles = YRegex.Matches(
                    @"<td>(?<name>.*?)</td><td>(?<details>.*?)</td>",
                    titleAltHtml,
                    "name",
                    "details",
                    true);

                alternatives.AddRange(from s in altTitles where !s.Value.ToLower().Contains(new[] { "imax ", "working ", "fake " }) select s.Key);

                if (html.Contains("title-extra"))
                {
                    var origTitle =
                        YRegex.Match(
                            @"class=""title-extra"">(?<title>.*?) <i>\(original title\)</i>",
                            html,
                            "title");

                    if (origTitle.Trim().Length > 0)
                    {
                        output = origTitle;
                    }
                }

                output = Regex.Replace(output, @"\(\d{4}\)", string.Empty);

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
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Year value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeYear(string id, int threadID, out int output, string logCatagory)
        {
            output = 0;

            try
            {
                output = YRegex.Match(
                        @"\((?<year>\d{4})/.*?\)|\((?<year>\d{4})\)", 
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
                        @"<div\sclass=""starbar-meta"">\s*?<b>(?<rating>.*?)/10</b>",
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
                var directors = YRegex.Match(
                        @"Directed By (?<director>.*?)\.", 
                        this.GetHtml("main", threadID, id), 
                        "director", 
                        true);

                directors = Tools.Clean.Text.ValidizeResult(directors);

                output = directors
                            .Split(',')
                            .ToPersonList();

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
        /// <param name="id">The ID for the scraper.</param>
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
                if (returnShort)
                {
                    output = YRegex.Match(@"<h5>Plot:</h5>(?<plot>.*?)</div>", this.GetHtml("main", threadID, id), "plot", true);
                }
                else
                {
                    var plot = YRegex.Match(
                        @"<p class=""plotpar"">(?<plot>.*?)<i>", 
                        this.GetHtml("plot", threadID, id),
                        "plot", 
                        true);

                    if (!string.IsNullOrEmpty(plot))
                    {
                        output = plot.Trim();
                    }
                    else if (!string.IsNullOrEmpty(plot))
                    {
                        output = YRegex.Match(
                                @"<div id=""swiki.2.1"">(?<synopsis>.*?)</div>",
                                this.GetHtml("summary", threadID, id),
                                "synopsis", 
                                true)
                            .Trim();
                    }
                    else
                    {
                        this.ScrapePlot(id, threadID, out output, logCatagory, true);
                    }
                }

                output = output.Replace(
                                        new[]
                                            {
                                                "Add synopsis &raquo;",
                                                "Full synopsis &raquo;",
                                                "Full summary &raquo;",
                                                "See more &raquo;"
                                            },
                                       string.Empty);

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
        /// Scrapes the Certification value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Certification value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeCertification(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                var cert = YRegex.Match(@"<h5>Certification:</h5>(?<cert>.*?)</div>", this.GetHtml("main", threadID, id), "cert", true);

                var matches = Regex.Matches(cert, @"USA:(?<mpaa>.*?)\s|USA:(?<mpaa>.*?)$", RegexOptions.IgnoreCase);
                foreach (Match match in matches)
                {
                    if (!match.Groups["mpaa"].Value.Contains("TV"))
                    {
                        output = string.Format("{0}", match.Groups["mpaa"].Value);
                        break;
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
        /// Scrapes the Mpaa descrion
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped mpaa value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeMpaa(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                output = YRegex.Match(
                    @"MPAA</a>:</h5><div\sclass=""info-content"">(?<mpaa>.*?)</div>",
                    this.GetHtml("main", threadID, id),
                    "mpaa",
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
                var match = YRegex.MatchesToList(
                    "<a href=\"/country/.{0,30}?\">(?<country>.*?)</a>",
                    this.GetHtml("main", threadID, id), 
                    "country", 
                    true);

                foreach (var country in match)
                {
                    if (Equals(country, "UK"))
                    {
                        output.Add("United Kingdom");      
                    }
                    else if (!country.ToLower(CultureInfo.CurrentCulture).Contains("imdb"))
                    {
                        output.Add(Tools.Clean.Text.ValidizeResult(country));
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
        /// Scrapes the Language collection
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Language collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeLanguage(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();

            try
            {
                output = YRegex.MatchesToList(
                    "<a href=\"/language/.{2,3}\">(?<language>.*?)</a>",
                    this.GetHtml("main", threadID, id),
                    "language",
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
                    "<a href=\"/Sections/Genres/(?<genre>[a-zA-Z-]*)(/\">|\">)", 
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
                    ">(?<name>.{0,40}?)</a></td><td class=\"ddd\"> ... </td><td class=\"char\">(?<role>.*?)</td>",
                    this.GetHtml("main", threadID, id),
                    "name",
                    "role",
                    "imgurl");

                foreach (var p in output)
                {
                    p.Name = p.Name.Replace("...", string.Empty).Trim();

                    if (!p.ImageUrl.Contains("SX400"))
                    {
                        p.ImageUrl = string.Empty;
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
                    @"<h5>Tagline:</h5><div\sclass=""info-content"">(?<tagline>.*?)</div>",
                    this.GetHtml("main", threadID, id),
                    "tagline",
                    true);

                if (output.EndsWith(" more"))
                {
                    output.TrimEnd(" more".ToCharArray());
                }

                output = output.Replace("See more &raquo;", string.Empty);

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
                        @"Top 250: #(?<top250>\d{1,3})</a>", 
                        this.GetHtml("main", threadID, id), 
                        "top250")
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
                    @"href=""/company/co\d{7}/"">(?<studio>.*?)</a>", 
                    this.GetHtml("main", threadID, id), 
                    "studio", 
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
                        @"tn15more"">(?<votes>.*?)\s", 
                        this.GetHtml("main", threadID, id),
                        "votes")
                    .Replace(",", string.Empty)
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
                var match = Regex.Match(
                    this.GetHtml("main", threadID, id),
                    @"Release\sDate:</h5><div\sclass=""info-content"">(?<day>\d*?)\s(?<month>.*?)\s(?<year>\d*?)\s\(",
                    RegexOptions.IgnoreCase);

                if (match.Success)
                {
                    var day = int.Parse(match.Groups["day"].Value);
                    var month = 0;

                    switch (match.Groups["month"].ToString())
                    {
                        case "January":
                            month = 1;
                            break;
                        case "February":
                            month = 2;
                            break;
                        case "March":
                            month = 3;
                            break;
                        case "April":
                            month = 4;
                            break;
                        case "May":
                            month = 5;
                            break;
                        case "June":
                            month = 6;
                            break;
                        case "July":
                            month = 7;
                            break;
                        case "August":
                            month = 8;
                            break;
                        case "September":
                            month = 9;
                            break;
                        case "October":
                            month = 10;
                            break;
                        case "November":
                            month = 11;
                            break;
                        case "December":
                            month = 12;
                            break;
                    }

                    int year;
                    var result = int.TryParse(match.Groups["year"].Value, out year);

                    if (result && (year > 1699 && month <= 12 && day <= 31))
                    {
                        output = new DateTime(year, month, day);
                    }
                    else
                    {
                        return false;
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
                        @"Runtime:</h5><div\sclass=""info-content"">.*?(?<runtime>\d*?)\smin", 
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
                    @"writerlist/(.*?)"">(?<name>.*?)</a>(?<role>.*?)<br",
                    this.GetHtml("main", threadID, id), 
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
                foreach (var type in new[] { "product", "poster" })
                {
                    var individualPosterHtml =
                        Downloader.ProcessDownload(
                            string.Format("http://www.imdb.com/title/{0}/mediaindex?refine={1}", id, type),
                            DownloadType.Html,
                            Section.Movies).RemoveCharacterReturn();

                    var matches = YRegex.MatchesToList(
                        @"(?<url>/rg/mediaindex/unknown-thumbnail/media/rm\d{10}/tt\d{7})", 
                        individualPosterHtml, 
                        "url");

                    foreach (var m in matches)
                    {
                        if (!m.Contains("swf"))
                        {
                            var newHtml =
                                Downloader.ProcessDownload(string.Format("http://www.imdb.com{0}", m), DownloadType.Html, Section.Movies).RemoveCharacterReturn();

                            var match = Regex.Match(
                                newHtml, 
                                @"src=""(?<url>http://ia\.media-imdb\.com/images/M/(?<str>[A-Za-z0-9_]+?)@@\._V1\._SX(?<width>\d{3})_SY(?<height>\d{3})_\.jpg)""\s");

                            if (match.Success)
                            {
                                var matchValue = match.Groups["url"].Value;

                                int width;
                                int height;

                                int.TryParse(match.Groups["width"].Value, out width);
                                int.TryParse(match.Groups["height"].Value, out height);

                                if (height > width)
                                {
                                    if (!string.IsNullOrEmpty(matchValue))
                                    {
                                        output.Add(
                                            new ImageDetailsModel { Height = height, Width = width, UriFull = new Uri(matchValue) });
                                    }
                                }
                            }
                        }
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
