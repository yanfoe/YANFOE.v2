// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieScrapeFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Scraper
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Extentions;

    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// The movie scrape factory.
    /// </summary>
    public static class MovieScrapeFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The post process.
        /// </summary>
        private static readonly BindingList<MovieModel> postProcess;

        /// <summary>
        /// The timer.
        /// </summary>
        private static readonly Timer timer;

        /// <summary>
        /// The scrape count.
        /// </summary>
        private static int scrapeCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MovieScrapeFactory"/> class. 
        /// </summary>
        static MovieScrapeFactory()
        {
            postProcess = new BindingList<MovieModel>();

            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 100;
            timer.Start();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Do a quick lookup on TmDB
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        /// The quick search tmdb.
        /// </returns>
        public static bool QuickSearchTmdb(Query query)
        {
            if (query == null)
            {
                query = new Query();
            }

            var tmdb = new TheMovieDb();
            tmdb.SearchSite(query, 0, "Tmdb");

            return query.Results.Count > 0;
        }

        /// <summary>
        /// The run multi scrape.
        /// </summary>
        /// <param name="movieModelList">The movie model list.</param>
        public static void RunMultiScrape(BindingList<MovieModel> movieModelList)
        {
            foreach (var movieModel in movieModelList)
            {
                if (!movieModel.Locked)
                {
                    movieModel.IsBusy = true;
                }
            }

            var bgwMulti = new BackgroundWorker();
            bgwMulti.DoWork += BgwMulti_DoWork;
            bgwMulti.RunWorkerCompleted += BgwMulti_RunWorkerCompleted;

            bgwMulti.RunWorkerAsync(movieModelList);
        }

        /// <summary>
        /// The run single scrape.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        public static void RunSingleScrape(MovieModel movieModel)
        {
            var movieModelList = new BindingList<MovieModel>
                {
                    movieModel
                };

            if (!movieModel.Locked)
            {
                movieModel.IsBusy = true;
            }

            RunMultiScrape(movieModelList);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private static void BgwSingle_DoWork(object sender, DoWorkEventArgs e)
        {
            var obj = e.Argument as MovieModel;
            obj.IsBusy = true;
            MovieScraperHandler.RunSingleScrape(obj);

            e.Result = obj;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private static void BgwSingle_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeCount--;

            postProcess.Add(e.Result as MovieModel);
        }

        /// <summary>
        /// The scrape movie.
        /// </summary>
        /// <param name="movieModel">The movie Model.</param>
        private static void ScrapeMovie(MovieModel movieModel)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += BgwSingle_DoWork;
            bgw.RunWorkerCompleted += BgwSingle_RunWorkerCompleted;

            if (movieModel.Locked)
            {
                Log.WriteToLog(LogSeverity.Info, 0, "Scraping -> Skipping Locked Movie", movieModel.Title);
                movieModel.IsBusy = false;
            }
            else
            {
                Log.WriteToLog(LogSeverity.Info, 0, "Scraping -> Scraping Movie", movieModel.Title);
                movieModel.IsBusy = true;
                var scrape = movieModel.Clone();
                bgw.RunWorkerAsync(scrape);
            }
        }

        /// <summary>
        /// Handles the DoWork event of the bgwMulti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private static void BgwMulti_DoWork(object sender, DoWorkEventArgs e)
        {
            BindingList<MovieModel> movieModelList = (e.Argument as BindingList<MovieModel>).Clone();

            scrapeCount = 0;

            foreach (MovieModel movie in movieModelList)
            {
                do
                {
                    Thread.Sleep(50);
                }
                while (scrapeCount > 4);

                scrapeCount++;
                ScrapeMovie(movie);
            }

            do
            {
                Thread.Sleep(100);
            }
            while (scrapeCount > 0);

            scrapeCount = 0;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwMulti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private static void BgwMulti_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            for (int index = 0; index < postProcess.Count; index++)
            {
                var movie = postProcess[index];
                movie.IsBusy = false;
                MovieDBFactory.ReplaceMovie(movie);
            }

            postProcess.Clear();
            MovieDBFactory.TempScraperGroup = string.Empty;
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (postProcess.Count > 0)
            {
                var movie = postProcess[0];

                postProcess.Remove(movie);

                MovieDBFactory.ReplaceMovie(movie);
            }
        }

        #endregion
    }
}