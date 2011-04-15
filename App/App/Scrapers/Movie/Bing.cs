// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bing.cs" company="The YANFOE Project">
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
    using System.ComponentModel;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.net.live.search.api;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Extentions;

    /// <summary>
    /// Contains all methods used for bing searches
    /// </summary>
    public static class Bing
    {
        /// <summary>
        /// Searches Bing.com API
        /// </summary>
        /// <param name="query">The QueryString to search against</param>
        /// <param name="urlmatch">Only return URLs containing the following match</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <returns>First successful match.</returns>
        public static BindingList<QueryResult> SearchBing(string query, string urlmatch, int threadID, string regexTitle, string regexYear, string regexID)
        {
            var logCatagory = "Scrape > Bing Search > " + query;

            try
            {
                var queryResults = new BindingList<QueryResult>();

                query = query.Replace("%20", " ");

                using (var service = new BingService())
                {
                    var searchRequest = new SearchRequest
                                            {
                                                Query = query,
                                                Sources = new[] { SourceType.Web },
                                                AppId = "9A2F2F47CF77629DA4E35E912F4B696217DCFC3C"
                                            };

                    var webRequest = new WebRequest
                                         {
                                             Count = 10,
                                             Offset = 0,
                                             OffsetSpecified = true
                                         };
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
                                        queryResult.ImdbID =
                                            Regex.Match(result.Url, regexID).Groups["id"].Value;
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

                    return queryResults;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, logCatagory, ex.Message);
                return null;
            }
        }
    }
}