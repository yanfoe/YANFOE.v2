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
        private List<string> imdb;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Genres"/> class.
        /// </summary>
        public Genres()
        {
            this.GenreDictionary = new Dictionary<ScraperList, List<string>>();
            this.AddImdb();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the genre dictionary.
        /// </summary>
        /// <value>
        /// The genre dictionary.
        /// </value>
        public Dictionary<ScraperList, List<string>> GenreDictionary { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The add imdb.
        /// </summary>
        private void AddImdb()
        {
            this.imdb = new List<string>
                {
                    "Action",
                    "Adventure",
                    "Animation",
                    "Biography",
                    "Comedy",
                    "Crime",
                    "Documentary",
                    "Drama",
                    "Family",
                    "Fantasy",
                    "Film-Noir",
                    "Game-Show",
                    "History",
                    "Horror",
                    "Music",
                    "Musical",
                    "Mystery",
                    "News",
                    "Reality-TV",
                    "Romance",
                    "Sci-Fi",
                    "Short",
                    "Sport",
                    "Talk-Show",
                    "Thriller",
                    "War",
                    "Western"
                };

            this.GenreDictionary.Add(ScraperList.Imdb, this.imdb);
        }

        #endregion
    }
}