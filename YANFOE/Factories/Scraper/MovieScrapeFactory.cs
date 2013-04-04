// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieScrapeFactory.cs">
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
//   The movie scrape factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Scraper
{
    #region Required Namespaces

    using System.ComponentModel;
    using System.Threading;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The movie scrape factory.
    /// </summary>
    public static class MovieScrapeFactory
    {
        #region Static Fields

        /// <summary>
        ///   The scrape count.
        /// </summary>
        private static int scrapeCount;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Do a quick lookup on TMDB
        /// </summary>
        /// <param name="query">
        /// The query. 
        /// </param>
        /// <returns>
        /// The quick search TMDB. 
        /// </returns>
        public static bool QuickSearchTMDB(Query query)
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
        /// <param name="movieModelList">
        /// The movie model list. 
        /// </param>
        public static void RunMultiScrape(ThreadedBindingList<MovieModel> movieModelList)
        {
            foreach (var movieModel in movieModelList)
            {
                if (!movieModel.Locked)
                {
                    movieModel.IsBusy = true;
                }
            }

            var bgwMulti = new BackgroundWorker();
            bgwMulti.DoWork += BgwMultiDoWork;
            bgwMulti.RunWorkerCompleted += BgwMultiRunWorkerCompleted;

            bgwMulti.RunWorkerAsync(movieModelList);
        }

        /// <summary>
        /// The run single scrape.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        public static void RunSingleScrape(MovieModel movieModel)
        {
            var movieModelList = new ThreadedBindingList<MovieModel> { movieModel };

            if (!movieModel.Locked)
            {
                movieModel.IsBusy = true;
            }

            RunMultiScrape(movieModelList);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the Multi control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwMultiDoWork(object sender, DoWorkEventArgs e)
        {
            var movieModelList = e.Argument as ThreadedBindingList<MovieModel>;

            scrapeCount = 0;

            if (movieModelList != null)
            {
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
            }

            do
            {
                Thread.Sleep(100);
            }
            while (scrapeCount > 0);

            scrapeCount = 0;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwMultiRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MovieDBFactory.Instance.TempScraperGroup = string.Empty;
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwSingleDoWork(object sender, DoWorkEventArgs e)
        {
            var obj = e.Argument as MovieModel;
            if (obj != null)
            {
                obj.IsBusy = true;
                MovieScraperHandler.Instance.RunSingleScrape(obj);

                e.Result = obj;
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwSingleRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeCount--;
            ((MovieModel)e.Result).IsBusy = false;
        }

        /// <summary>
        /// The scrape movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie Model. 
        /// </param>
        private static void ScrapeMovie(MovieModel movieModel)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += BgwSingleDoWork;
            bgw.RunWorkerCompleted += BgwSingleRunWorkerCompleted;

            if (movieModel.Locked)
            {
                Log.WriteToLog(LogSeverity.Info, 0, "Scraping -> Skipping Locked Movie", movieModel.Title);
                movieModel.IsBusy = false;
            }
            else
            {
                Log.WriteToLog(LogSeverity.Info, 0, "Scraping -> Scraping Movie", movieModel.Title);
                movieModel.IsBusy = true;
                bgw.RunWorkerAsync(movieModel);
            }
        }

        #endregion
    }
}