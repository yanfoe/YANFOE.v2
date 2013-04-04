// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Impawards.cs">
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
// <summary>
//   The ImpaAwards Movie Scraper
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Scrapers.Movie
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;

    using BitFactory.Logging;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The ImpaAwards Movie Scraper
    /// </summary>
    public class Impawards : ScraperMovieBase, IMovieScraper
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Impawards" /> class.
        /// </summary>
        public Impawards()
        {
            this.ScraperName = ScraperList.Impawards;

            this.DefaultUrl = "http://www.impawards.com/";

            this.Urls = new Dictionary<string, string> { { "main", "http://http://www.impawards.com{0}" } };

            this.AvailableSearchMethod.AddRange(new[] { ScrapeSearchMethod.Bing });

            this.AvailableScrapeMethods.AddRange(new[] { ScrapeFields.Poster });

            this.HtmlEncoding = Encoding.GetEncoding("iso-8859-1");
            this.HtmlBaseUrl = "impawards";
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Scrapes the poster image collection.
        /// </summary>
        /// <param name="id">
        /// The MovieUniqueId for the scraper. 
        /// </param>
        /// <param name="threadID">
        /// The thread MovieUniqueId. 
        /// </param>
        /// <param name="output">
        /// The scraped poster image collection. 
        /// </param>
        /// <param name="logCatagory">
        /// The log catagory. 
        /// </param>
        /// <returns>
        /// Scrape succeeded [true/false] 
        /// </returns>
        public new bool ScrapePoster(
            string id, int threadID, out ThreadedBindingList<ImageDetailsModel> output, string logCatagory)
        {
            output = new ThreadedBindingList<ImageDetailsModel>();

            try
            {
                var posterFound = true;
                var i = 1;
                do
                {
                    if (i == 1)
                    {
                        var poster = this.ExtractPoster(id);

                        if (poster == string.Empty)
                        {
                            return output.IsFilled();
                        }

                        output.Add(new ImageDetailsModel { UriFull = new Uri(poster) });

                        if (id.IndexOf("_ver") > 0)
                        {
                            for (var a = 1; a < 10; a++)
                            {
                                id = id.Replace(string.Format("_ver{0}.html", a), ".html");
                            }
                        }
                    }
                    else
                    {
                        var path = this.ExtractPoster(id.Replace(".html", string.Format("_ver{0}.html", i)));

                        if (string.IsNullOrEmpty(path))
                        {
                            posterFound = false;
                        }
                        else
                        {
                            output.Add(new ImageDetailsModel { UriFull = new Uri(path) });
                        }
                    }

                    i++;
                }
                while (posterFound);

                return output.IsFilled();
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
        /// <param name="query">
        /// The query. 
        /// </param>
        /// <param name="threadID">
        /// The thread MovieUniqueId. 
        /// </param>
        /// <param name="logCatagory">
        /// The log catagory. 
        /// </param>
        /// <returns>
        /// [true/false] if an error occurred. 
        /// </returns>
        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            try
            {
                query.Results =
                    Bing.SearchBing(
                        string.Format("{0}%20{1}%20site:www.impawards.com%20poster", query.Title, query.Year), 
                        string.Empty, 
                        threadID, 
                        string.Empty, 
                        string.Empty, 
                        string.Empty, 
                        ScraperList.Impawards);

                return query.Results.Count > 0;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Extracts the poster.
        /// </summary>
        /// <param name="id">
        /// The thread MovieUniqueId 
        /// </param>
        /// <returns>
        /// The return URL 
        /// </returns>
        private string ExtractPoster(string id)
        {
            try
            {
                var result =
                    Downloader.ProcessDownload(this.Urls["main"] + id, DownloadType.Html, Section.Movies).
                        RemoveCharacterReturn();

                var ifindPoster = result.IndexOf("posters/");
                if (ifindPoster == -1)
                {
                    return string.Empty;
                }

                var firstPart = result.Substring(ifindPoster);
                var findPosterEnd = firstPart.IndexOf("alt=");
                var outputUrl = firstPart.Substring(0, findPosterEnd - 2);
                var arrFindUrlPath = id.Split('/');

                return string.Format("{0}/{1}/{2}", this.Urls["main"], arrFindUrlPath[1], outputUrl);
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}