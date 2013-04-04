// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Genres.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.UserSettings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using YANFOE.Tools.Enums;

    /// <summary>
    /// The genres.
    /// </summary>
    [Serializable]
    public class Genres
    {
        #region Constants and Fields

        /// <summary>
        /// The imdb collection
        /// </summary>
        public BindingList<string> CustomGenres;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Genres"/> class.
        /// </summary>
        public Genres()
        {
            this.PopulateGenresFromScraper(ScraperList.Imdb);
            CustomGenres.AllowEdit = true;
            CustomGenres.AllowNew = true;
            CustomGenres.AllowRemove = true;
        }

        private void PopulateGenresFromScraper(ScraperList scraper)
        {
            var scrapers = Scrapers.Movie.MovieScraperHandler.ReturnAllScrapers();

            var imdbScraper = (from s in scrapers where s.ScraperName == scraper select s).Single();

            this.CustomGenres = imdbScraper.DefaultGenres;
        }

        #endregion

    }
}