// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Bing.cs">
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
//   Contains all methods used for bing searches
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Scrapers.Movie
{
    #region Required Namespaces

    using System;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.net.bing.api;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   Contains all methods used for bing searches
    /// </summary>
    public static class Bing
    {
        #region Public Methods and Operators

        /// <summary>
        /// Searches Bing.com API
        /// </summary>
        /// <param name="query">
        /// The QueryString to search against 
        /// </param>
        /// <param name="urlmatch">
        /// Only return URLs containing the following match 
        /// </param>
        /// <param name="threadID">
        /// The thread MovieUniqueId. 
        /// </param>
        /// <param name="regexTitle">
        /// The regex Title.
        /// </param>
        /// <param name="regexYear">
        /// The regex Year.
        /// </param>
        /// <param name="regexID">
        /// The regex ID.
        /// </param>
        /// <param name="scraperList">
        /// The scraper List.
        /// </param>
        /// <returns>
        /// First successful match. 
        /// </returns>
        public static ThreadedBindingList<QueryResult> SearchBing(
            string query, 
            string urlmatch, 
            int threadID, 
            string regexTitle, 
            string regexYear, 
            string regexID, 
            ScraperList scraperList)
        {
            var logCatagory = "Scrape > Bing Search > " + query;

            try
            {
                var queryResults = new ThreadedBindingList<QueryResult>();

                query = query.Replace("%20", " ");

                // http://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor
                using (BingService service = new BingService())
                {
                    var searchRequest = new SearchRequest
                        {
                            Query = query, 
                            Sources = new[] { SourceType.Web }, 
                            AppId = "9A2F2F47CF77629DA4E35E912F4B696217DCFC3C"
                        };

                    var webRequest = new WebRequest { Count = 10, Offset = 0, OffsetSpecified = true };
                    searchRequest.Web = webRequest;

                    var response = service.Search(searchRequest);

                    if (response.Web.Results != null)
                    {
                        foreach (var result in response.Web.Results)
                        {
                            if (string.IsNullOrEmpty(result.Url) || result.Url.Contains(urlmatch))
                            {
                                var queryResult = new QueryResult();

                                if (Regex.IsMatch(result.Title, regexTitle))
                                {
                                    if (Regex.IsMatch(result.Url, regexID))
                                    {
                                        switch (scraperList)
                                        {
                                            case ScraperList.Imdb:
                                                queryResult.ImdbID = Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.TheMovieDB:
                                                queryResult.TmdbID = Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.Allocine:
                                                queryResult.AllocineId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.FilmAffinity:
                                                queryResult.FilmAffinityId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.FilmDelta:
                                                queryResult.FilmDeltaId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.FilmUp:
                                                queryResult.FilmUpId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.FilmWeb:
                                                queryResult.FilmWebId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.Impawards:
                                                queryResult.ImpawardsId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.Kinopoisk:
                                                queryResult.KinopoiskId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.OFDB:
                                                queryResult.OfdbId = Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.MovieMeter:
                                                queryResult.MovieMeterId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                            case ScraperList.Sratim:
                                                queryResult.SratimId =
                                                    Regex.Match(result.Url, regexID).Groups["id"].Value;
                                                break;
                                        }
                                    }

                                    queryResult.Title = Regex.Match(result.Title, regexTitle).Groups["title"].Value;
                                    queryResult.Year = Regex.Match(result.Title, regexYear).Groups["year"].Value.ToInt();
                                }
                                else
                                {
                                    queryResult.Title = result.Title;
                                }

                                queryResult.AdditionalInfo = result.Description;
                                queryResult.URL = result.Url;

                                queryResults.Add(queryResult);
                            }
                        }
                    }

                    Log.WriteToLog(
                        LogSeverity.Info, 
                        0, 
                        string.Format("Bing search complete ({0} results)", queryResults.Count), 
                        query);

                    return queryResults;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
                return null;
            }
        }

        #endregion
    }
}