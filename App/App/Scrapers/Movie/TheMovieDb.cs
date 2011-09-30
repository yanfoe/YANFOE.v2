// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheMovieDb.cs" company="The YANFOE Project">
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
    #region Usings
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

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
    using YANFOE.Tools.Xml;
    #endregion

    /// <summary>
    /// The Movie DB Scraper For YANFOE
    /// </summary>
    public class TheMovieDb : ScraperMovieBase, IMovieScraper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TheMovieDb"/> class.
        /// </summary>
        public TheMovieDb()
        {
            this.ScraperName = ScraperList.TheMovieDB;

            this.DefaultUrl = "http://www.themoviedb.org/";

            this.Urls = new Dictionary<string, string>
                {
                    {
                        "getInfo", "http://api.themoviedb.org/2.1/Movie.getInfo/en/xml/" + Settings.ConstSettings.Application.TheMovieDBApi + "/{0}"
                    }
                };

            this.AvailableSearchMethod.AddRange(new[]
                                                    {
                                                        ScrapeSearchMethod.Site,
                                                        ScrapeSearchMethod.Bing
                                                    });

            this.AvailableScrapeMethods.AddRange(new[]
                                               {
                                                   ScrapeFields.Title,
                                                   ScrapeFields.Year,
                                                   ScrapeFields.Rating,
                                                   ScrapeFields.Director,
                                                   ScrapeFields.Outline,
                                                   ScrapeFields.Certification,
                                                   ScrapeFields.Country,
                                                   ScrapeFields.Language,
                                                   ScrapeFields.Genre,
                                                   ScrapeFields.Cast,
                                                   ScrapeFields.Tagline,
                                                   ScrapeFields.Studio,
                                                   ScrapeFields.Votes,
                                                   ScrapeFields.ReleaseDate,
                                                   ScrapeFields.Runtime,
                                                   ScrapeFields.Writers,
                                                   ScrapeFields.Poster,
                                                   ScrapeFields.Fanart
                                               });

            this.HtmlEncoding = Encoding.UTF8;
            this.HtmlBaseUrl = "themoviedb";
        }

        /// <summary>
        /// A collection of possible people type on tMDB
        /// </summary>
        public enum PersonType
        {
            /// <summary>
            /// Is an actor.
            /// </summary>
            Actor,

            /// <summary>
            ///  Is a Director
            /// </summary>
            Director,

            /// <summary>
            /// Is a Author/Writer
            /// </summary>
            Author
        }

        /// <summary>
        /// Searches Bing for Applicable MovieUniqueId's
        /// </summary>
        /// <param name="query">A BingQuery object.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>[true/false] if an error occurred.</returns>
        public new bool SearchViaBing(Query query, int threadID, string logCatagory)
        {
            query.Results = new BindingList<QueryResult>();

            try
            {
                query.Results = Bing.SearchBing(
                    string.Format(CultureInfo.CurrentCulture, "{0} {1} site:www.themoviedb.org", query.Title, query.Year),
                    string.Empty,
                    threadID,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    ScraperList.TheMovieDB);

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
                string xml;

                if (!string.IsNullOrEmpty(query.ImdbId))
                {
                    Log.WriteToLog(LogSeverity.Info, 0, "Doing TheMovieDB search (IMDB ID)", query.ImdbId);

                    xml =
                        Downloader.ProcessDownload(
                            string.Format(
                                "http://api.themoviedb.org/2.1/Movie.imdbLookup/en/xml/{0}/{1}",
                                Settings.ConstSettings.Application.TheMovieDBApi,
                                query.ImdbId),
                            DownloadType.Html,
                            Section.Movies);
                }
                else if (!string.IsNullOrEmpty(query.TmdbId))
                {
                    Log.WriteToLog(LogSeverity.Info, 0, "Doing TheMovieDB search (Tmdb ID)", query.TmdbId);

                    xml =
                        Downloader.ProcessDownload(
                            string.Format(
                                "http://api.themoviedb.org/2.1/Movie.getInfo/en/xml/{0}/{1}",
                                Settings.ConstSettings.Application.TheMovieDBApi,
                                query.TmdbId),
                            DownloadType.Html,
                            Section.Movies);
                }
                else
                {
                    Log.WriteToLog(LogSeverity.Info, 0, "Doing TheMovieDB search (Title)", query.Title);

                    xml =
                        Downloader.ProcessDownload(
                            string.Format(
                                "http://api.themoviedb.org/2.1/Movie.search/en/xml/{0}/{1}",
                                Settings.ConstSettings.Application.TheMovieDBApi,
                                query.Title),
                            DownloadType.Html,
                            Section.Movies);
                }

                if (string.IsNullOrEmpty(xml))
                {
                    return false;
                }

                XDocument xmlDoc = XDocument.Parse(xml);

                var movies = from m in xmlDoc.Descendants("movie") select m;

                foreach (var movie in movies)
                {
                    var title = movie.Element("name").Value;
                    var releaseDate = movie.Element("released").Value;
                    var imdbID = movie.Element("imdb_id").Value;
                    var tmdbId = movie.Element("id").Value;
                    var additionalDetails = movie.Element("overview").Value;

                    XDocument xmlDocImages = XDocument.Parse(movie.ToString());

                    var images = (from i in xmlDocImages.Descendants("image")
                                  where 
                                    i.Attribute("type").Value == "poster" && 
                                    i.Attribute("size").Value == "cover"
                                  select i.Attribute("url").Value).ToList();

                    var queryResult = new QueryResult();
                    DateTime releaseParse;
                    var reseaseParseSuccess = DateTime.TryParse(releaseDate, out releaseParse);

                    queryResult.Title = title;

                    if (reseaseParseSuccess)
                    {
                        queryResult.Year = releaseParse.Year;
                        queryResult.ReleaseDate = releaseParse;
                    }

                    queryResult.AdditionalInfo = additionalDetails;
                    queryResult.ImdbID = imdbID.ToLower().Replace("tt", string.Empty);
                    queryResult.TmdbID = tmdbId;

                    if (images.Count > 0)
                    {
                        queryResult.PosterUrl = images[0];
                    }

                    query.Results.Add(queryResult);
                }

                if (query.Results.Count == 0)
                {
                    Log.WriteToLog(LogSeverity.Info, 0, "Doing quick failed", query.Title);
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
        /// <param name="logCatagory">The log category.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeTitle(string id, int threadID, out string output, out BindingList<string> alternatives, string logCatagory)
        {
            output = string.Empty;
            alternatives = new BindingList<string>();

            try
            {
                var xml = this.GetHtml("getInfo", threadID, id);

                var doc = XRead.OpenXml(xml);

                output = XRead.GetString(doc, "name");

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
        /// <param name="logCatagory">The log category.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeYear(string id, int threadID, out int output, string logCatagory)
        {
            output = 0;

            try
            {
                var xml = this.GetHtml("getInfo", threadID, id);

                var releasedDate = XRead.GetDateTime(XRead.OpenXml(xml), "released");

                output = releasedDate.Year;

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
                var xml = this.GetHtml("getInfo", threadID, id);

                output = XRead.GetDouble(XRead.OpenXml(xml), "rating");

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
                output = this.GetPeople(PersonType.Director, threadID, id);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Scrapes the Outline value
        /// </summary>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="output">The scraped Outline value.</param>
        /// <param name="logCatagory">The log catagory.</param>
        /// <returns>Scrape succeeded [true/false]</returns>
        public new bool ScrapeOutline(string id, int threadID, out string output, string logCatagory)
        {
            output = string.Empty;

            try
            {
                output = XRead.GetString(XRead.OpenXml(this.GetHtml("getInfo", threadID, id)), "overview");

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
                output = output = XRead.GetString(XRead.OpenXml(this.GetHtml("getInfo", threadID, id)), "certification");

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
                var xml = this.GetHtml("getInfo", threadID, id);

                output = new BindingList<string>();

                XDocument xmlDoc = XDocument.Parse(xml);

                var counties = from m in xmlDoc.Descendants("country")
                               select new { CountryCode = m.Attribute("code").Value };

                foreach (var country in counties)
                {
                    output.Add(country.CountryCode);
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
                output = new BindingList<string>();

                var languageCode = XRead.GetString(XRead.OpenXml(this.GetHtml("getInfo", threadID, id)), "certification");

                if (!string.IsNullOrEmpty(languageCode))
                {
                    output.Add(languageCode);
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
                output = this.GetPeople(PersonType.Actor, threadID, id);

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
                output = XRead.GetString(XRead.OpenXml(this.GetHtml("getInfo", threadID, id)), "tagline");

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
                var xml = this.GetHtml("getInfo", threadID, id);

                output = new BindingList<string>();

                XDocument xmlDoc = XDocument.Parse(xml);

                var studios = from m in xmlDoc.Descendants("studio")
                               select new { StudioName = m.Attribute("name").Value };

                foreach (var studio in studios)
                {
                    output.Add(studio.StudioName);
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
                output = this.GetPeople(PersonType.Author, threadID, id);

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
                output = this.GetImages(ImageUsageType.Poster, threadID, id);

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
                output = this.GetImages(ImageUsageType.Fanart, threadID, id);

                return output.IsFilled();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, threadID, logCatagory, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the people from the cast block based on type of person
        /// </summary>
        /// <param name="type">The type of person.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="id">The MovieUniqueId for the scraper.</param>
        /// <returns>A BindingList of people objects.</returns>
        public BindingList<PersonModel> GetPeople(PersonType type, int threadID, string id)
        {
            var xml = this.GetHtml("getInfo", threadID, id);

            var output = new BindingList<PersonModel>();

            var xmlDoc = XDocument.Parse(xml);

            var people = from m in xmlDoc.Descendants("person") where m.Attribute("job").Value == type.ToString() select m;

            foreach (var person in people)
            {
                output.Add(new PersonModel(person.Attribute("name").Value, role: person.Attribute("character").Value));
            }

            return output;
        }

        /// <summary>
        /// Gets images from the image block
        /// </summary>
        /// <param name="type">The ImageUsageType type.</param>
        /// <param name="threadID">The thread MovieUniqueId.</param>
        /// <param name="id">The tmDB id</param>
        /// <returns>An image collection</returns>
        public BindingList<ImageDetailsModel> GetImages(ImageUsageType type, int threadID, string id)
        {
            var xml = this.GetHtml("getInfo", threadID, id);

            var output = new BindingList<ImageDetailsModel>();

            var xmlDoc = XDocument.Parse(xml);

            var tmdbType = string.Empty;

            var posterSize = string.Empty;
            var fanartSize = string.Empty;

            switch (type)
            {
                case ImageUsageType.Poster:
                    tmdbType = "poster";

                    switch (Settings.Get.Scraper.TmDBDownloadPosterSize)
                    {
                        case 0:
                            posterSize = "original";
                            break;
                        case 1:
                            posterSize = "mid";
                            break;
                        case 2:
                            posterSize = "cover";
                            break;
                        case 3:
                            posterSize = "thumb";
                            break;
                    }

                    break;
                case ImageUsageType.Fanart:
                    tmdbType = "backdrop";

                    switch (Settings.Get.Scraper.TmDBDownloadFanartSize)
                    {
                        case 0:
                            fanartSize = "original";
                            break;
                        case 1:
                            fanartSize = "poster";
                            break;
                        case 2:
                            fanartSize = "w1280";
                            break;
                        case 3:
                            fanartSize = "thumb";
                            break;
                    }

                    break;
            }

            var images = from m in xmlDoc.Descendants("image") where m.Attribute("type").Value == tmdbType select m;

            foreach (var image in images)
            {
                if (image.Attribute("size").Value == fanartSize || image.Attribute("size").Value == posterSize)
                {
                    var Original = new Uri(image.Attribute("url").Value);
                    var thumb = Original.ToString();

                    if (thumb.Contains("-original"))
                    {
                        thumb = thumb.Replace("-original", "-thumb");
                    }
                    else if (thumb.Contains("-mid.jpg"))
                    {
                        thumb = thumb.Replace("-mid", "-thumb");
                    }
                    else if (thumb.Contains("-poster.jpg"))
                    {
                        thumb = thumb.Replace("-poster", "-thumb");
                    }

                    var width = image.Attribute("width").Value.ToInt();
                    var height = image.Attribute("height").Value.ToInt();

                    var downloadItem = new DownloadItem
                        {
                            Type = DownloadType.Binary, Url = thumb.ToString(), Section = Section.Movies 
                        };

                    Downloader.AddToBackgroundQue(downloadItem);

                    output.Add(
                        new ImageDetailsModel
                            {
                                Height = height, Width = width, UriFull = Original, UriThumb = new Uri(thumb) 
                            });
                }
            }

            return output;
        }
    }
}
