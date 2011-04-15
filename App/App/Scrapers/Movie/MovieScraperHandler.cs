// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieScraperHandler.cs" company="The YANFOE Project">
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
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Scraper;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.ScraperGroup;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The movie scraper handler.
    /// </summary>
    public static class MovieScraperHandler
    {
        #region Constants and Fields

        /// <summary>
        /// The scrapers.
        /// </summary>
        private static List<IMovieScraper> scrapers;
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieScraperHandler"/> class.
        /// </summary>
        static MovieScraperHandler()
        {
            scrapers = ReturnAllScrapers();
        }


        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the scrapers as string list.
        /// </summary>
        /// <param name="scrapeField">
        /// The scrape field.
        /// </param>
        /// <param name="addNoneFirst">
        /// if set to <c>true</c> [add none first].
        /// </param>
        /// <param name="addMediaInfo">
        /// if set to <c>true</c> [add media info].
        /// </param>
        /// <returns>
        /// Scrapers collection
        /// </returns>
        public static BindingList<string> GetScrapersAsStringList(
            ScrapeFields scrapeField, bool addNoneFirst = false, bool addMediaInfo = false)
        {
            var output = new BindingList<string>();

            List<IMovieScraper> scrapers = GetScrapersSupporting(scrapeField);

            var tempSortedList = new SortedList<string, string>();

            foreach (IMovieScraper scraper in scrapers)
            {
                tempSortedList.Add(scraper.ScraperName.ToString(), null);
            }

            if (addNoneFirst)
            {
                output.Add("<None>");
            }

            foreach (var s in tempSortedList)
            {
                output.Add(s.Key);
            }

            if (addMediaInfo)
            {
                output.Add("Use MediaInfo Data");
            }

            return output;
        }

        /// <summary>
        /// Get scrapers supporting.
        /// </summary>
        /// <param name="scrapeFields">
        /// The scrape fields.
        /// </param>
        /// <returns>
        /// Scraper collection
        /// </returns>
        public static List<IMovieScraper> GetScrapersSupporting(ScrapeFields scrapeFields)
        {
            List<IMovieScraper> scrapers = ReturnAllScrapers();

            return
                (from s in scrapers where s.AvailableScrapeMethods.Contains(scrapeFields) orderby s.ScraperName select s)
                    .ToList();
        }

        /// <summary>
        /// Return all scrapers.
        /// </summary>
        /// <returns>
        /// Scraper collection
        /// </returns>
        public static List<IMovieScraper> ReturnAllScrapers()
        {
            List<IMovieScraper> instances = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                             where
                                                 t.GetInterfaces().Contains(typeof(IMovieScraper)) &&
                                                 t.GetConstructor(Type.EmptyTypes) != null
                                             select Activator.CreateInstance(t) as IMovieScraper).ToList();

            return instances;
        }

        /// <summary>
        /// The return all scrapers as string list.
        /// </summary>
        /// <returns>
        /// Scraper collection
        /// </returns>
        public static BindingList<string> ReturnAllScrapersAsStringList()
        {
            List<IMovieScraper> scrapers = ReturnAllScrapers();

            var tempSortedList = new SortedList<string, string>();

            foreach (IMovieScraper scraper in scrapers)
            {
                tempSortedList.Add(scraper.ScraperName.ToString(), null);
            }

            var output = new BindingList<string>();

            foreach (var scraper in tempSortedList)
            {
                output.Add(scraper.Key);
            }

            return output;
        }

        /// <summary>
        /// Runs the single scrape.
        /// </summary>
        /// <param name="movie">
        /// The model.
        /// </param>
        /// <param name="testmode">
        /// if set to <c>true</c> [testmode].
        /// </param>
        /// <returns>
        /// The run single scrape.
        /// </returns>
        public static bool RunSingleScrape(MovieModel movie, bool testmode = false)
        {
            scrapers.Clear();
            scrapers = ReturnAllScrapers();

            if (string.IsNullOrEmpty(movie.ScraperGroup))
            {
                XtraMessageBox.Show(
                    "No Scraper Group Selected", "Select a Scraper Group", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                Log.WriteToLog(
                    LogSeverity.Error, 0, "No Scraper Group Selected", "No scraper group selected for " + movie.Title);

                return false;
            }

            MovieScraperGroupModel scraperGroup;

            if (testmode)
            {
                scraperGroup = new MovieScraperGroupModel
                    {
                        Title = "Imdb", 
                        Year = "Imdb", 
                        Cast = "Imdb", 
                        Certification = "Imdb", 
                        Country = "Imdb", 
                        Director = "Imdb", 
                        Fanart = "TheMovieDB", 
                        Genre = "Imdb", 
                        Language = "Imdb", 
                        Top250 = "Imdb", 
                        Outline = "TheMovieDB", 
                        Plot = "Imdb", 
                        Rating = "Imdb", 
                        ReleaseDate = "Imdb", 
                        Runtime = "Imdb", 
                        Studio = "Imdb", 
                        Tagline = "Imdb", 
                        Votes = "Imdb", 
                        Writers = "Imdb", 
                        Trailer = "Apple"
                    };
            }
            else
            {
                scraperGroup = MovieScraperGroupFactory.GetScaperGroupModel(movie.ScraperGroup);
            }

            bool outResult = true;

            if (!string.IsNullOrEmpty(scraperGroup.Title) && scraperGroup.Title != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Title, ScrapeFields.Title, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Year) && scraperGroup.Year != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Year, ScrapeFields.Year, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Top250) && scraperGroup.Top250 != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Top250, ScrapeFields.Top250, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Cast) && scraperGroup.Cast != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Cast, ScrapeFields.Cast, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Certification) && scraperGroup.Certification != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Certification, ScrapeFields.Certification, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Mpaa) && scraperGroup.Mpaa != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Mpaa, ScrapeFields.Mpaa, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Country) && scraperGroup.Country != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Country, ScrapeFields.Country, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Director) && scraperGroup.Director != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Director, ScrapeFields.Director, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Fanart) && scraperGroup.Fanart != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Fanart, ScrapeFields.Fanart, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Genre) && scraperGroup.Genre != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Genre, ScrapeFields.Genre, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Language) && scraperGroup.Language != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Language, ScrapeFields.Language, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Outline) && scraperGroup.Outline != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Outline, ScrapeFields.Outline, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Plot) && scraperGroup.Plot != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Plot, ScrapeFields.Plot, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Rating) && scraperGroup.Rating != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Rating, ScrapeFields.Rating, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.ReleaseDate) && scraperGroup.ReleaseDate != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.ReleaseDate, ScrapeFields.ReleaseDate, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Runtime) && scraperGroup.Runtime != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Runtime, ScrapeFields.Runtime, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Studio) && scraperGroup.Studio != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Studio, ScrapeFields.Studio, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Tagline) && scraperGroup.Tagline != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Tagline, ScrapeFields.Tagline, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Votes) && scraperGroup.Votes != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Votes, ScrapeFields.Votes, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Writers) && scraperGroup.Writers != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Writers, ScrapeFields.Writers, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Poster) && scraperGroup.Poster != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Poster, ScrapeFields.Poster, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            if (!string.IsNullOrEmpty(scraperGroup.Trailer) && scraperGroup.Trailer != "<None>")
            {
                bool result;
                ScrapeValues(movie, scraperGroup.Trailer, ScrapeFields.Trailer, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            return outResult;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create log catagory.
        /// </summary>
        /// <param name="title">
        /// The log title.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <returns>
        /// Log catagory
        /// </returns>
        private static string CreateLogCatagory(string title, ScrapeFields type)
        {
            return "Scrape > " + type + " > " + title;
        }

        /// <summary>
        /// The get scraper id.
        /// </summary>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// The scraper id
        /// </returns>
        private static string GetScraperID(string scraperName, MovieModel movie)
        {
            var results = new BindingList<QueryResult>();
            var query = new Query
                        { Results = results, Title = movie.Title, Year = movie.Year.ToString(), ImdbId = movie.ImdbId };

            if (scraperName == "Imdb" || scraperName == "TheMovieDB")
            {
                if (string.IsNullOrEmpty(movie.ImdbId) || string.IsNullOrEmpty(movie.TmdbId))
                {
                    MovieScrapeFactory.QuickSearchTmdb(query);

                    if (query.Results.Count > 0)
                    {
                        if (string.IsNullOrEmpty(movie.ImdbId))
                        {
                            movie.ImdbId = query.Results[0].ImdbID;
                        }

                        if (string.IsNullOrEmpty(movie.TmdbId))
                        {
                            movie.TmdbId = query.Results[0].TmdbID;
                        }
                    }
                }
            }

            switch (scraperName)
            {
                case "Imdb":
                    return "tt" + movie.ImdbId;
                case "TheMovieDB":
                    return movie.TmdbId;
                case "Apple":
                    return movie.Title;
                case "Allocine":
                    if (string.IsNullOrEmpty(movie.AllocineId))
                    {
                        var scraper =
                            (from s in scrapers where s.ScraperName == ScraperList.Allocine select s).SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.AllocineId = query.Results[0].AllocineId;
                        }
                    }

                    return movie.AllocineId;
                case "FilmAffinity":
                    return movie.FilmAffinityId;
                case "FilmDelta":
                    return movie.FilmDeltaId;
                case "FilmUp":
                    return movie.FilmUpId;
                case "FilmWeb":
                    return movie.FilmWebId;
                case "Impawards":
                    return movie.ImpawardsId;
                case "Kinopoisk":
                    return movie.KinopoiskId;
            }

            return null;
        }

        /// <summary>
        /// Scrape 250.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool Scrape250(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeTop250(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Top250 = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Scrape cast.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeCast(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeCast(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Cast = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Scrape certification.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeCertification(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeCertification(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Certification = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape country.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeCountry(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeCountry(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Country = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape director.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeDirector(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeDirector(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Director = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape fanart.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeFanart(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<ImageDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapeFanart(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                if (output.Count > 0)
                {
                    movie.CurrentFanartImageUrl = output[0].UriFull.ToString();
                }

                movie.AlternativeFanart = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape genre.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeGenre(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;

            BindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeGenre(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Genre = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape language.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeLanguage(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeLanguage(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Language = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape mpaa.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeMpaa(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeMpaa(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Mpaa = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape outline.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeOutline(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeOutline(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Outline = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape plot.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapePlot(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapePlot(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Plot = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape poster.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapePoster(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<ImageDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapePoster(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                if (output.Count > 0)
                {
                    movie.CurrentPosterImageUrl = output[0].UriFull.ToString();
                }

                movie.AlternativePosters = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape rating.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeRating(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            double output;

            bool scrapeSuccess = scraper.ScrapeRating(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Rating = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape release date.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeReleaseDate(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            DateTime output;

            bool scrapeSuccess = scraper.ScrapeReleaseDate(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.ReleaseDate = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape runtime.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeRuntime(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeRuntime(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Runtime = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape studio.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeStudio(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeStudio(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Studios = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape tagline.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeTagline(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeTagline(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Tagline = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape title.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeTitle(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;
            BindingList<string> altOutput;

            bool scrapeSuccess = scraper.ScrapeTitle(
                GetScraperID(scraperName, movie), 
                0, 
                out output, 
                out altOutput, 
                CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Title = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape trailer.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeTrailer(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<TrailerDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapeTrailer(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.AlternativeTrailers = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape votes.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeVotes(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeVotes(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Votes = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape writers.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeWriters(
            IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            BindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeWriters(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Writers = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape year.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// Scrape successful
        /// </returns>
        private static bool ScrapeYear(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeYear(
                GetScraperID(scraperName, movie), 0, out output, CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.Year = output;
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// The scrape values.
        /// </summary>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The ScrapeFields type.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        private static void ScrapeValues(MovieModel movie, string scraperName, ScrapeFields type, out bool result)
        {
            result = true;

            foreach (IMovieScraper scraper in scrapers)
            {
                if (scraper.ScraperName.ToString() == scraperName)
                {
                    if (type == ScrapeFields.Title)
                    {
                        result = ScrapeTitle(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Year)
                    {
                        result = ScrapeYear(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Top250)
                    {
                        result = Scrape250(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Cast)
                    {
                        result = ScrapeCast(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Certification)
                    {
                        result = ScrapeCertification(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Country)
                    {
                        result = ScrapeCountry(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Director)
                    {
                        result = ScrapeDirector(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Genre)
                    {
                        result = ScrapeGenre(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Language)
                    {
                        result = ScrapeLanguage(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Outline)
                    {
                        result = ScrapeOutline(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Plot)
                    {
                        result = ScrapePlot(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Rating)
                    {
                        result = ScrapeRating(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.ReleaseDate)
                    {
                        result = ScrapeReleaseDate(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Runtime)
                    {
                        result = ScrapeRuntime(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Studio)
                    {
                        result = ScrapeStudio(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Tagline)
                    {
                        result = ScrapeTagline(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Votes)
                    {
                        result = ScrapeVotes(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Writers)
                    {
                        result = ScrapeWriters(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Mpaa)
                    {
                        result = ScrapeMpaa(scraper, scraperName, type, movie);
                    }

                    if (type == ScrapeFields.Poster)
                    {
                        result = ScrapePoster(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Fanart)
                    {
                        result = ScrapeFanart(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Trailer)
                    {
                        result = ScrapeTrailer(scraper, scraperName, type, movie);
                        break;
                    }
                }
            }
        }

        #endregion
    }
}