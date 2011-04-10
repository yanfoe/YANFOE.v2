// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Apple.cs" company="The YANFOE Project">
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
    using YANFOE.Scrapers.Movie.Models.AppleTrailers;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;

    /// <summary>
    /// Defines the Apple type.
    /// </summary>
    public class Apple : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Apple"/> class.
        /// </summary>
        public Apple()
        {
            this.ScraperName = ScraperList.Apple;

            this.Urls = new Dictionary<string, string>();

            this.UrlHtmlCache = new Dictionary<string, string>();

            this.AvailableSearchMethod = new BindingList<ScrapeSearchMethod>();

            this.AvailableSearchMethod.AddRange(new[]
                                                    {
                                                        ScrapeSearchMethod.Site
                                                    });

            this.AvailableScrapeMethods = new BindingList<ScrapeFields>();

            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Trailer
                                               });
        }

        /// <summary>
        /// Scrapes trailers from source
        /// </summary>
        /// <param name="id">The id value</param>
        /// <param name="threadID">The thread id.</param>
        /// <param name="output">The return output</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>
        /// Scrape succeeded [true/false]
        /// </returns>
        public new bool ScrapeTrailer(string id, int threadID, out BindingList<TrailerDetailsModel> output, string logCatagory)
        {
            output = new BindingList<TrailerDetailsModel>();

            Regex trailerRegex = new Regex("http://(movies|images|trailers).apple.com/([0-9]*/us/media/trailers|trailers|movies)/[^\"]+?-(tlr|trailer)[^\"]+?\\.(mov|m4v)");
            MatchCollection matchCollection;
            int itmsIndex;
            string moviePage;

            try
            {
                var html =
                    Downloader.ProcessDownload(
                        string.Format(
                            "http://trailers.apple.com/trailers/home/scripts/quickfind.php?callback=&q={0}", id),
                        DownloadType.Html,
                        Section.Movies);
                var atsm = Newtonsoft.Json.JsonConvert.DeserializeObject(html, typeof(AppleTrailerSearchModel)) as AppleTrailerSearchModel;

                if (atsm != null && !atsm.error)
                {
                    for (int i = 0; i < atsm.results.Count; i++)
                    {
                        if (atsm.results[i].title.ToLower() == id.ToLower())
                        {
                            if ((itmsIndex = atsm.results[i].location.IndexOf("itms://")) != -1)
                            {
                                moviePage = "http" + atsm.results[i].location.Substring(itmsIndex + 4);
                            }
                            else
                            {
                                moviePage = this.GetAbsoluteURL(
                                    "http://trailers.apple.com/trailers/", atsm.results[i].location);
                            }

                            string movieHDPage = this.GetAbsoluteURL(moviePage, "hd");
                            string moviePlaylist = this.GetAbsoluteURL(moviePage, "includes/playlists/web.inc");
                            string movieHDPlaylist = this.GetAbsoluteURL(moviePage, "hd/includes/playlists/web.inc");

                            html = Downloader.ProcessDownload(moviePage, DownloadType.Html, Section.Movies);
                            matchCollection = trailerRegex.Matches(html);
                            if (matchCollection.Count != 0)
                            {
                                foreach (Match trailerMatch in matchCollection)
                                {
                                    int lastUnderscore = trailerMatch.Value.LastIndexOf('_');
                                    if (lastUnderscore == -1) continue;
                                    if (trailerMatch.Value[lastUnderscore+1] != 'h')
                                    {
                                        output.Add(
                                            new TrailerDetailsModel(
                                                trailerMatch.Value.Insert(lastUnderscore + 1, "h"),
                                                0,
                                                "Unk",
                                                atsm.results[i].title));
                                    }
                                    else
                                    {
                                        if (trailerMatch.Value[lastUnderscore + 2] == '.')
                                            output.Add(
                                                new TrailerDetailsModel(
                                                    trailerMatch.Value.Remove(lastUnderscore + 2, 1),
                                                    0,
                                                    "Unk",
                                                    atsm.results[i].title));
                                        else
                                            output.Add(new TrailerDetailsModel(trailerMatch.Value, 0, "Unk", atsm.results[i].title));
                                    }
                                }

                                continue;
                            }

                            html = Downloader.ProcessDownload(movieHDPage, DownloadType.Html, Section.Movies);
                            matchCollection = trailerRegex.Matches(html);
                            if (matchCollection.Count != 0)
                            {
                                foreach (Match trailerMatch in matchCollection)
                                {
                                    int lastUnderscore = trailerMatch.Value.LastIndexOf('_');
                                    if (lastUnderscore == -1) continue;
                                    if (trailerMatch.Value[lastUnderscore + 1] != 'h')
                                    {
                                        output.Add(
                                            new TrailerDetailsModel(
                                                trailerMatch.Value.Insert(lastUnderscore + 1, "h"),
                                                0,
                                                "Unk",
                                                atsm.results[i].title));
                                    }
                                    else
                                    {
                                        if (trailerMatch.Value[lastUnderscore + 2] == '.')
                                            output.Add(
                                                new TrailerDetailsModel(
                                                    trailerMatch.Value.Remove(lastUnderscore + 2, 1),
                                                    0,
                                                    "Unk",
                                                    atsm.results[i].title));
                                        else
                                            output.Add(new TrailerDetailsModel(trailerMatch.Value, 0, "Unk", atsm.results[i].title));
                                    }
                                }

                                continue;
                            }

                            html = Downloader.ProcessDownload(moviePlaylist, DownloadType.Html, Section.Movies);
                            matchCollection = trailerRegex.Matches(html);
                            if (matchCollection.Count != 0)
                            {
                                foreach (Match trailerMatch in matchCollection)
                                {
                                    int lastUnderscore = trailerMatch.Value.LastIndexOf('_');
                                    if (lastUnderscore == -1) continue;
                                    if (trailerMatch.Value[lastUnderscore + 1] != 'h')
                                    {
                                        output.Add(
                                            new TrailerDetailsModel(
                                                trailerMatch.Value.Insert(lastUnderscore + 1, "h"),
                                                0,
                                                "Unk",
                                                atsm.results[i].title));
                                    }
                                    else
                                    {
                                        if (trailerMatch.Value[lastUnderscore + 2] == '.')
                                            output.Add(
                                                new TrailerDetailsModel(
                                                    trailerMatch.Value.Remove(lastUnderscore + 2, 1),
                                                    0,
                                                    "Unk",
                                                    atsm.results[i].title));
                                        else
                                            output.Add(new TrailerDetailsModel(trailerMatch.Value, 0, "Unk", atsm.results[i].title));
                                    }
                                }

                                continue;
                            }

                            html = Downloader.ProcessDownload(movieHDPlaylist, DownloadType.Html, Section.Movies);
                            matchCollection = trailerRegex.Matches(html);
                            if (matchCollection.Count != 0)
                            {
                                foreach (Match trailerMatch in matchCollection)
                                {
                                    int lastUnderscore = trailerMatch.Value.LastIndexOf('_');
                                    if (lastUnderscore == -1) continue;
                                    if (trailerMatch.Value[lastUnderscore + 1] != 'h')
                                    {
                                        output.Add(
                                            new TrailerDetailsModel(
                                                trailerMatch.Value.Insert(lastUnderscore + 1, "h"),
                                                0,
                                                "Unk",
                                                atsm.results[i].title));
                                    }
                                    else
                                    {
                                        if (trailerMatch.Value[lastUnderscore + 2] == '.')
                                            output.Add(
                                                new TrailerDetailsModel(
                                                    trailerMatch.Value.Remove(lastUnderscore + 2, 1),
                                                    0,
                                                    "Unk",
                                                    atsm.results[i].title));
                                        else
                                            output.Add(new TrailerDetailsModel(trailerMatch.Value, 0, "Unk", atsm.results[i].title));
                                    }
                                }

                                continue;
                            }
                        }
                    }

                    return output.IsFilled();
                }

                return false;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the absolute URL.
        /// </summary>
        /// <param name="baseURL">The base url.</param>
        /// <param name="relativeURL">The relative url.</param>
        /// <returns>The absolute url</returns>
        private string GetAbsoluteURL(string baseURL, string relativeURL)
        {
            return new Uri(new Uri(baseURL), relativeURL).ToString();
        }
    }
}
