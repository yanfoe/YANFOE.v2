// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieMeter.cs" company="The YANFOE Project">
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

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.Xml;

    public class MovieMeter : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieMeter"/> class.
        /// </summary>
        public MovieMeter()
        {
            this.ScraperName = ScraperList.MovieMeter;

            this.Urls = new Dictionary<string, string>
                            {
                                { "main", "$$Internal_movieMeterHandler?moviemeterid={0}" },
                            };

            this.AvailableSearchMethod.AddRange(new[]
                                                {
                                                    ScrapeSearchMethod.Bing,
                                                    ScrapeSearchMethod.Site
                                                });

            this.AvailableScrapeMethods.AddRange(new[]
                                                {
                                                    ScrapeFields.Title,
                                                    ScrapeFields.Year,
                                                    ScrapeFields.Cast,
                                                    ScrapeFields.Genre,
                                                    ScrapeFields.Plot,
                                                    ScrapeFields.Director,
                                                    ScrapeFields.ReleaseDate,
                                                    ScrapeFields.Country,
                                                    ScrapeFields.Rating
                                                });

            this.HtmlEncoding = Encoding.GetEncoding("iso-8859-1");
            this.HtmlBaseUrl = "moviemeter";

            this.BingMatchString = "http://www.moviemeter.nl/film";
            this.BingSearchQuery = "{0} {1} site:www.moviemeter.nl/film";
            this.BingRegexMatchTitle = @"(?<title>.*?)\s\((?<year>\d{4})\)\s-\sMovieMeter\.nl";
            this.BingRegexMatchYear = @"(?<title>.*?)\s\((?<year>\d{4})\)\s-\sMovieMeter\.nl";
            this.BingRegexMatchID = @"http://www\.moviemeter\.nl/film/(?<id>\d*?)$";
        }

        /// <summary>
        /// Scrapes the Title value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Title value.</param>
        /// <param name="alternatives">The alternatives.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory)
        {
            output = string.Empty;
            alternatives = new BindingList<string>();

            try
            {
                var xml = this.GetHtml("main", threadID, id);
                output = XRead.GetString(XRead.OpenXml(xml), "title");

                return !string.IsNullOrEmpty(output);
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
                var xml = this.GetHtml("main", threadID, id);
                output = XRead.GetInt(XRead.OpenXml(xml), "year");

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
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeCast(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var xml = this.GetHtml("main", threadID, id);
                var xmlDoc = XRead.OpenXml(xml);

                foreach (var actor in XRead.GetStrings(xmlDoc, "actor"))
                {
                    output.Add(new PersonModel(actor));
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
                var xmlDoc = XRead.OpenXml(this.GetHtml("main", threadID, id));
                output = XRead.GetStrings(xmlDoc, "actor").ToBindingList();

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
                var xmlDoc = XRead.OpenXml(this.GetHtml("main", threadID, id));
                output = XRead.GetString(xmlDoc, "plot");

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
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeDirector(string id, int threadID, out BindingList<PersonModel> output, string logCatagory)
        {
            output = new BindingList<PersonModel>();

            try
            {
                var xml = this.GetHtml("main", threadID, id);
                var xmlDoc = XRead.OpenXml(xml);

                foreach (var director in XRead.GetStrings(xmlDoc, "director"))
                {
                    output.Add(new PersonModel(director));
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
        /// Scrapes the release date value
        /// </summary>
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped release date value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeReleaseDate(string id, int threadID, out DateTime output, string logCatagory)
        {
            output = new DateTime(1700, 1, 1);

            try
            {
                var xmlDoc = XRead.OpenXml(this.GetHtml("main", threadID, id));
                output = XRead.GetDateTime(xmlDoc, "releasedate", "yyyy-MM-dd");
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
        /// <param name="id">The Id for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Country collection.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeCountry(string id, int threadID, out BindingList<string> output, string logCatagory)
        {
            output = new BindingList<string>();

            try
            {
                var xmlDoc = XRead.OpenXml(this.GetHtml("main", threadID, id));
                output = XRead.GetStrings(xmlDoc, "country").ToBindingList();

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
                var xmlDoc = XRead.OpenXml(this.GetHtml("main", threadID, id));
                output = XRead.GetDouble(xmlDoc, "rating");

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
