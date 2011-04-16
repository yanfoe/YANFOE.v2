// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResult.cs" company="The YANFOE Project">
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

namespace YANFOE.Scrapers.Movie.Models.Search
{
    using System;
    using System.Drawing;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// A result from a movie search 
    /// </summary>
    public class QueryResult
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryResult"/> class.
        /// </summary>
        public QueryResult()
        {
            this.Title = string.Empty;
            this.OrigionalTitle = string.Empty;
            this.Year = -1;
            this.ReleaseDate = new DateTime();
            this.AdditionalInfo = string.Empty;
            this.Language = string.Empty;
            this.Poster = null;
            this.ScraperName = ScraperList.None;
            this.URL = string.Empty;
            this.ImdbID = string.Empty;
            this.TmdbID = string.Empty;
            this.YanfoeId = string.Empty;
            this.AllocineId = string.Empty;
            this.FilmAffinityId = string.Empty;
            this.FilmDeltaId = string.Empty;
            this.FilmUpId = string.Empty;
            this.FilmWebId = string.Empty;
            this.ImpawardsId = string.Empty;
            this.KinopoiskId = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the movie overview.
        /// </summary>
        /// <value>The movie overview.</value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the imdb Id.
        /// </summary>
        /// <value>The imdb Id.</value>
        public string ImdbID { get; set; }

        /// <summary>
        /// Gets or sets the allocine id.
        /// </summary>
        /// <value>
        /// The allocine id.
        /// </value>
        public string AllocineId { get; set; }

        /// <summary>
        /// Gets or sets the film affinity id.
        /// </summary>
        /// <value>
        /// The film affinity id.
        /// </value>
        public string FilmAffinityId { get; set; }

        /// <summary>
        /// Gets or sets the film delta id.
        /// </summary>
        /// <value>
        /// The film delta id.
        /// </value>
        public string FilmDeltaId { get; set; }

        /// <summary>
        /// Gets or sets the film up id.
        /// </summary>
        /// <value>
        /// The film up id.
        /// </value>
        public string FilmUpId { get; set; }

        /// <summary>
        /// Gets or sets the film web id.
        /// </summary>
        /// <value>
        /// The film web id.
        /// </value>
        public string FilmWebId { get; set; }

        /// <summary>
        /// Gets or sets the impawards id.
        /// </summary>
        /// <value>
        /// The impawards id.
        /// </value>
        public string ImpawardsId { get; set; }

        /// <summary>
        /// Gets or sets the kinopoisk id.
        /// </summary>
        /// <value>
        /// The kinopoisk id.
        /// </value>
        public string KinopoiskId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The movies language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the origional title.
        /// </summary>
        /// <value>The origional title.</value>
        public string OrigionalTitle { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>The image URL of an image associated.</value>
        public Image Poster { get; set; }

        /// <summary>
        /// Sets PosterUrl.
        /// </summary>
        public string PosterUrl
        {
            set
            {
                string path = Downloader.ProcessDownload(value, DownloadType.Binary, Section.Movies);
                this.Poster = ImageHandler.LoadImage(path);
            }
        }

        /// <summary>
        /// Gets or sets ReleaseDate.
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the scraper.
        /// </summary>
        /// <value>The name of the scraper.</value>
        public ScraperList ScraperName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The movie title</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets TmdbID.
        /// </summary>
        public string TmdbID { get; set; }

        /// <summary>
        /// Gets or sets the URL
        /// </summary>
        /// <value>The Result URL.</value>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets YanfoeId.
        /// </summary>
        public string YanfoeId { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The movies year.</value>
        public int Year { get; set; }

        #endregion
    }
}