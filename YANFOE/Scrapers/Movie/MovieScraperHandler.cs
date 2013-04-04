// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieScraperHandler.cs">
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
//   The movie scraper handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Scrapers.Movie
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;

    using BitFactory.Logging;

    using YANFOE.Factories;
    using YANFOE.Factories.Scraper;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Scrapers.Movie.Models.ScraperGroup;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The movie scraper handler.
    /// </summary>
    public class MovieScraperHandler
    {
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        public static MovieScraperHandler Instance = new MovieScraperHandler();

        #endregion

        #region Fields

        /// <summary>
        ///   The scrapers.
        /// </summary>
        private List<IMovieScraper> scrapers;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MovieScraperHandler"/> class from being created. 
        ///   Initializes a new instance of the <see cref="MovieScraperHandler"/> class.
        /// </summary>
        private MovieScraperHandler()
        {
            this.scrapers = this.ReturnAllScrapers();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   The return all scrapers as string list.
        /// </summary>
        /// <returns> Scraper collection </returns>
        public ThreadedBindingList<string> AllScrapersAsStringList
        {
            get
            {
                List<IMovieScraper> scrapers = this.ReturnAllScrapers();

                var tempSortedList = new SortedList<string, string>();

                foreach (IMovieScraper scraper in scrapers)
                {
                    tempSortedList.Add(scraper.ScraperName.ToString(), null);
                }

                var output = new ThreadedBindingList<string>();

                foreach (var scraper in tempSortedList)
                {
                    output.Add(scraper.Key);
                }

                return output;
            }
        }

        /// <summary>
        /// Gets the cast scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> CastScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Cast, true);
            }
        }

        /// <summary>
        /// Gets the certification scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> CertificationScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Certification, true);
            }
        }

        /// <summary>
        /// Gets the country scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> CountryScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Country, true);
            }
        }

        /// <summary>
        /// Gets the director scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> DirectorScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Director, true);
            }
        }

        /// <summary>
        /// Gets the fanart scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> FanartScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Fanart, true);
            }
        }

        /// <summary>
        /// Gets the genre scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> GenreScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Genre, true);
            }
        }

        /// <summary>
        /// Gets the language scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> LanguageScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Language, true);
            }
        }

        /// <summary>
        /// Gets the mpaa scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> MpaaScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Mpaa, true);
            }
        }

        /// <summary>
        /// Gets the original title scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> OriginalTitleScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.OriginalTitle, true);
            }
        }

        /// <summary>
        /// Gets the outline scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> OutlineScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Outline, true);
            }
        }

        /// <summary>
        /// Gets the plot scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> PlotScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Plot, true);
            }
        }

        /// <summary>
        /// Gets the poster scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> PosterScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Poster, true);
            }
        }

        /// <summary>
        /// Gets the rating scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> RatingScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Rating, true);
            }
        }

        /// <summary>
        /// Gets the release date scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> ReleaseDateScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.ReleaseDate, true);
            }
        }

        /// <summary>
        /// Gets the runtime scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> RuntimeScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Runtime, true, true);
            }
        }

        /// <summary>
        /// Gets the studio scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> StudioScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Studio, true);
            }
        }

        /// <summary>
        /// Gets the tagline scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> TaglineScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Tagline, true);
            }
        }

        /// <summary>
        /// Gets the title scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> TitleScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Title, true);
            }
        }

        /// <summary>
        /// Gets the top 250 scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> Top250ScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Top250, true);
            }
        }

        /// <summary>
        /// Gets the trailer scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> TrailerScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Trailer, true);
            }
        }

        /// <summary>
        /// Gets the votes scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> VotesScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Votes, true);
            }
        }

        /// <summary>
        /// Gets the writers scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> WritersScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Writers, true);
            }
        }

        /// <summary>
        /// Gets the year scrapers as string list.
        /// </summary>
        public ThreadedBindingList<string> YearScrapersAsStringList
        {
            get
            {
                return this.GetScrapersAsStringList(ScrapeFields.Year, true);
            }
        }

        #endregion

        #region Public Methods and Operators

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
        public ThreadedBindingList<string> GetScrapersAsStringList(
            ScrapeFields scrapeField, bool addNoneFirst = false, bool addMediaInfo = false)
        {
            var output = new ThreadedBindingList<string>();

            List<IMovieScraper> scrapers = this.GetScrapersSupporting(scrapeField);

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
        public List<IMovieScraper> GetScrapersSupporting(ScrapeFields scrapeFields)
        {
            List<IMovieScraper> scrapers = this.ReturnAllScrapers();

            return
                (from s in scrapers where s.AvailableScrapeMethods.Contains(scrapeFields) orderby s.ScraperName select s)
                    .ToList();
        }

        /// <summary>
        ///   Return all scrapers.
        /// </summary>
        /// <returns> Scraper collection </returns>
        public List<IMovieScraper> ReturnAllScrapers()
        {
            List<IMovieScraper> instances = (from t in Assembly.GetExecutingAssembly().GetTypes()
                                             where
                                                 t.GetInterfaces().Contains(typeof(IMovieScraper))
                                                 && t.GetConstructor(Type.EmptyTypes) != null
                                             select Activator.CreateInstance(t) as IMovieScraper).ToList();

            var sortedScrapers = new SortedDictionary<string, IMovieScraper>();

            foreach (var s in instances)
            {
                sortedScrapers.Add(s.ScraperName.ToString(), s);
            }

            instances.Clear();

            instances.AddRange(sortedScrapers.Select(s => s.Value));

            return instances;
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
        public bool RunSingleScrape(MovieModel movie, bool testmode = false)
        {
            this.scrapers.Clear();
            this.scrapers = this.ReturnAllScrapers();

            if (string.IsNullOrEmpty(movie.ScraperGroup)
                && string.IsNullOrEmpty(MovieDBFactory.Instance.TempScraperGroup))
            {
                MessageBox.Show(
                    "No Scraper Group Selected", "Select a Scraper Group", MessageBoxButton.OK, MessageBoxImage.Hand);

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
                if (!string.IsNullOrEmpty(MovieDBFactory.Instance.TempScraperGroup))
                {
                    scraperGroup =
                        MovieScraperGroupFactory.Instance.GetScaperGroupModel(MovieDBFactory.Instance.TempScraperGroup);
                }
                else
                {
                    scraperGroup = MovieScraperGroupFactory.Instance.GetScaperGroupModel(movie.ScraperGroup);
                }
            }

            bool outResult = true;

            var noneValue = "<None>";

            outResult = this.GetOutResult(
                ScrapeFields.Title, scraperGroup.Title, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.OriginalTitle, scraperGroup.OriginalTitle, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Year, scraperGroup.Year, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Top250, scraperGroup.Top250, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Cast, scraperGroup.Cast, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Certification, scraperGroup.Certification, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Mpaa, scraperGroup.Mpaa, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Country, scraperGroup.Country, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Director, scraperGroup.Director, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Fanart, scraperGroup.Fanart, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Genre, scraperGroup.Genre, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Language, scraperGroup.Language, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Outline, scraperGroup.Outline, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Plot, scraperGroup.Plot, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Rating, scraperGroup.Rating, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.ReleaseDate, scraperGroup.ReleaseDate, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Runtime, scraperGroup.Runtime, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Studio, scraperGroup.Studio, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Tagline, scraperGroup.Tagline, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Votes, scraperGroup.Votes, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Writers, scraperGroup.Writers, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Poster, scraperGroup.Poster, movie, noneValue, scraperGroup, outResult);
            outResult = this.GetOutResult(
                ScrapeFields.Trailer, scraperGroup.Trailer, movie, noneValue, scraperGroup, outResult);

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
        private string CreateLogCatagory(string title, ScrapeFields type)
        {
            return "Scrape > " + type + " > " + title;
        }

        /// <summary>
        /// The get out result.
        /// </summary>
        /// <param name="scrapeFields">
        /// The scrape fields.
        /// </param>
        /// <param name="scrapeGroup">
        /// The scrape group.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <param name="noneValue">
        /// The none value.
        /// </param>
        /// <param name="scraperGroup">
        /// The scraper group.
        /// </param>
        /// <param name="outResult">
        /// The out result.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool GetOutResult(
            ScrapeFields scrapeFields, 
            string scrapeGroup, 
            MovieModel movie, 
            string noneValue, 
            MovieScraperGroupModel scraperGroup, 
            bool outResult)
        {
            if (!string.IsNullOrEmpty(scrapeGroup) && scrapeGroup != noneValue)
            {
                bool result;
                this.ScrapeValues(movie, scrapeGroup, scrapeFields, out result);

                if (!result)
                {
                    outResult = false;
                }
            }

            return outResult;
        }

        /// <summary>
        /// The get scraper id.
        /// </summary>
        /// <param name="scraperNameString">
        /// The scraper Name String.
        /// </param>
        /// <param name="movie">
        /// The movie. 
        /// </param>
        /// <returns>
        /// The scraper id 
        /// </returns>
        private string GetScraperID(string scraperNameString, MovieModel movie)
        {
            var results = new ThreadedBindingList<QueryResult>();
            var query = new Query
                {
                   Results = results, Title = movie.Title, Year = movie.Year.ToString(), ImdbId = movie.ImdbId 
                };

            var scraperName = (ScraperList)Enum.Parse(typeof(ScraperList), scraperNameString);

            if (scraperName == ScraperList.Imdb || scraperName == ScraperList.TheMovieDB)
            {
                if (string.IsNullOrEmpty(movie.ImdbId) || string.IsNullOrEmpty(movie.TmdbId))
                {
                    MovieScrapeFactory.QuickSearchTMDB(query);

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
                case ScraperList.Imdb:
                    return "tt" + movie.ImdbId;
                case ScraperList.TheMovieDB:
                    return movie.TmdbId;
                case ScraperList.Apple:
                    return movie.Title;
                case ScraperList.Allocine:

                    if (string.IsNullOrEmpty(movie.AllocineId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.Allocine select s).
                                SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.AllocineId = query.Results[0].AllocineId;
                        }
                    }

                    return movie.AllocineId;

                case ScraperList.FilmAffinity:

                    if (string.IsNullOrEmpty(movie.FilmAffinityId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.FilmAffinity select s).
                                SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.FilmAffinityId = query.Results[0].FilmAffinityId;
                        }
                    }

                    return movie.FilmAffinityId;
                case ScraperList.FilmDelta:

                    if (string.IsNullOrEmpty(movie.FilmDeltaId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.FilmDelta select s).
                                SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.FilmDeltaId = query.Results[0].FilmDeltaId;
                        }
                    }

                    return movie.FilmDeltaId;
                case ScraperList.FilmUp:

                    if (string.IsNullOrEmpty(movie.FilmUpId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.FilmUp select s).SingleOrDefault
                                ();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.FilmUpId = query.Results[0].FilmUpId;
                        }
                    }

                    return movie.FilmUpId;
                case ScraperList.FilmWeb:

                    if (string.IsNullOrEmpty(movie.FilmWebId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.FilmWeb select s).
                                SingleOrDefault();

                        scraper.SearchSite(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.FilmWebId = query.Results[0].FilmWebId;
                        }
                    }

                    return movie.FilmWebId;
                case ScraperList.Impawards:
                    return movie.ImpawardsId;
                case ScraperList.MovieMeter:

                    if (string.IsNullOrEmpty(movie.MovieMeterId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.MovieMeter select s).
                                SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.MovieMeterId = query.Results[0].MovieMeterId;
                        }
                    }

                    return movie.MovieMeterId;
                case ScraperList.OFDB:

                    if (string.IsNullOrEmpty(movie.OfdbId))
                    {
                        var scraper =
                            (from s in this.scrapers where s.ScraperName == ScraperList.OFDB select s).SingleOrDefault();

                        scraper.SearchViaBing(query, 0, string.Empty);

                        if (query.Results.Count > 0)
                        {
                            movie.OfdbId = query.Results[0].OfdbId;
                        }
                    }

                    return movie.OfdbId;

                case ScraperList.Kinopoisk:
                    return movie.KinopoiskId;
                case ScraperList.Sratim:
                    return movie.SratimId;
                case ScraperList.RottenTomato:
                    return movie.RottenTomatoId;
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
        private bool Scrape250(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeTop250(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeCast(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeCast(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeCertification(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeCertification(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeCountry(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeCountry(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeDirector(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeDirector(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeFanart(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<ImageDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapeFanart(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeGenre(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;

            ThreadedBindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeGenre(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeLanguage(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeLanguage(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeMpaa(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeMpaa(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        /// The scrape original title.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        /// <param name="scraperName">
        /// The scraper name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="movie">
        /// The movie.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ScrapeOriginalTitle(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;
            bool scrapeSuccess = scraper.ScrapeOriginalTitle(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

            if (scrapeSuccess)
            {
                movie.OriginalTitle = output;
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
        private bool ScrapeOutline(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeOutline(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapePlot(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapePlot(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapePoster(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<ImageDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapePoster(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeRating(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            double output;

            bool scrapeSuccess = scraper.ScrapeRating(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeReleaseDate(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            DateTime output;

            bool scrapeSuccess = scraper.ScrapeReleaseDate(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeRuntime(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeRuntime(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeStudio(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<string> output;

            bool scrapeSuccess = scraper.ScrapeStudio(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeTagline(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;

            bool scrapeSuccess = scraper.ScrapeTagline(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeTitle(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            string output;
            ThreadedBindingList<string> altOutput;

            bool scrapeSuccess = scraper.ScrapeTitle(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                out altOutput, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeTrailer(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<TrailerDetailsModel> output;

            bool scrapeSuccess = scraper.ScrapeTrailer(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private void ScrapeValues(MovieModel movie, string scraperName, ScrapeFields type, out bool result)
        {
            result = true;

            for (int index = 0; index < this.scrapers.Count; index++)
            {
                IMovieScraper scraper = this.scrapers[index];
                if (scraper.ScraperName.ToString() == scraperName)
                {
                    if (type == ScrapeFields.Title)
                    {
                        result = this.ScrapeTitle(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.OriginalTitle)
                    {
                        result = this.ScrapeOriginalTitle(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Year)
                    {
                        result = this.ScrapeYear(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Top250)
                    {
                        result = this.Scrape250(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Cast)
                    {
                        result = this.ScrapeCast(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Certification)
                    {
                        result = this.ScrapeCertification(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Country)
                    {
                        result = this.ScrapeCountry(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Director)
                    {
                        result = this.ScrapeDirector(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Genre)
                    {
                        result = this.ScrapeGenre(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Language)
                    {
                        result = this.ScrapeLanguage(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Outline)
                    {
                        result = this.ScrapeOutline(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Plot)
                    {
                        result = this.ScrapePlot(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Rating)
                    {
                        result = this.ScrapeRating(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.ReleaseDate)
                    {
                        result = this.ScrapeReleaseDate(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Runtime)
                    {
                        result = this.ScrapeRuntime(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Studio)
                    {
                        result = this.ScrapeStudio(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Tagline)
                    {
                        result = this.ScrapeTagline(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Votes)
                    {
                        result = this.ScrapeVotes(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Writers)
                    {
                        result = this.ScrapeWriters(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Mpaa)
                    {
                        result = this.ScrapeMpaa(scraper, scraperName, type, movie);
                    }

                    if (type == ScrapeFields.Poster)
                    {
                        result = this.ScrapePoster(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Fanart)
                    {
                        result = this.ScrapeFanart(scraper, scraperName, type, movie);
                        break;
                    }

                    if (type == ScrapeFields.Trailer)
                    {
                        result = this.ScrapeTrailer(scraper, scraperName, type, movie);
                        break;
                    }
                }
            }
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
        private bool ScrapeVotes(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeVotes(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeWriters(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            ThreadedBindingList<PersonModel> output;

            bool scrapeSuccess = scraper.ScrapeWriters(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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
        private bool ScrapeYear(IMovieScraper scraper, string scraperName, ScrapeFields type, MovieModel movie)
        {
            bool result = true;
            int output;

            bool scrapeSuccess = scraper.ScrapeYear(
                this.GetScraperID(scraperName, movie), 
                0, 
                out output, 
                this.CreateLogCatagory(scraper.ScraperName.ToString(), type));

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

        #endregion
    }
}