// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Genres.cs">
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
//   The genres.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Linq;

    using YANFOE.Scrapers.Movie;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The genres.
    /// </summary>
    [Serializable]
    public class Genres
    {
        #region Fields

        /// <summary>
        ///   The imdb collection
        /// </summary>
        public ThreadedBindingList<string> CustomGenres;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Genres" /> class.
        /// </summary>
        public Genres()
        {
            this.PopulateGenresFromScraper(ScraperList.Imdb);
            this.CustomGenres.AllowEdit = true;
            this.CustomGenres.AllowNew = true;
            this.CustomGenres.AllowRemove = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The populate genres from scraper.
        /// </summary>
        /// <param name="scraper">
        /// The scraper.
        /// </param>
        private void PopulateGenresFromScraper(ScraperList scraper)
        {
            var scrapers = MovieScraperHandler.Instance.ReturnAllScrapers();

            var imdbScraper = (from s in scrapers where s.ScraperName == scraper select s).Single();

            this.CustomGenres = imdbScraper.DefaultGenres;
        }

        #endregion
    }
}