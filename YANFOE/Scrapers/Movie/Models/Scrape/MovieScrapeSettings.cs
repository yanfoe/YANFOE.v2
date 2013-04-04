// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieScrapeSettings.cs">
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
//   Sets up what scrapers are responsible for scraping which item of information.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Scrapers.Movie.Models.Scrape
{
    #region Required Namespaces

    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   Sets up what scrapers are responsible for scraping which item of information.
    /// </summary>
    public class MovieScrapeSettings
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MovieScrapeSettings" /> class.
        /// </summary>
        public MovieScrapeSettings()
        {
            this.Clear();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the budget.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Budget { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the cast.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Cast { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the certification.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Certification { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the country.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Country { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the director.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Director { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the fanart.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Fanart { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the genre.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Genre { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the homepage.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Homepage { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the language.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Language { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the mpaa.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Mpaa { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the Original title.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList OriginalTitle { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the outline.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Outline { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the plot.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Plot { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the poster.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Poster { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the rating.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Rating { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the release date.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList ReleaseDate { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the revenue.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Revenue { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the runtime.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Runtime { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the studio.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Studio { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the tagline.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Tagline { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the title.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Title { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the top250.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Top250 { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the trailer.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Trailer { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the votes.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Votes { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the writers.
        /// </summary>
        /// <value> A ScraperList enum.. </value>
        public ScraperList Writers { get; set; }

        /// <summary>
        ///   Gets or sets what if any scraper should be used to aquire the year.
        /// </summary>
        /// <value> A ScraperList enum. </value>
        public ScraperList Year { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   Clears this instance.
        /// </summary>
        public void Clear()
        {
            this.Title = ScraperList.None;
            this.Year = ScraperList.None;
            this.OriginalTitle = ScraperList.None;
            this.Rating = ScraperList.None;
            this.Director = ScraperList.None;
            this.Tagline = ScraperList.None;
            this.Plot = ScraperList.None;
            this.Outline = ScraperList.None;
            this.Certification = ScraperList.None;
            this.Top250 = ScraperList.None;
            this.Mpaa = ScraperList.None;
            this.Country = ScraperList.None;
            this.Language = ScraperList.None;
            this.Genre = ScraperList.None;
            this.Runtime = ScraperList.None;
            this.Budget = ScraperList.None;
            this.Revenue = ScraperList.None;
            this.Studio = ScraperList.None;
            this.ReleaseDate = ScraperList.None;
            this.Homepage = ScraperList.None;
            this.Votes = ScraperList.None;
            this.Trailer = ScraperList.None;
            this.Cast = ScraperList.None;
            this.Writers = ScraperList.None;
            this.Poster = ScraperList.None;
            this.Fanart = ScraperList.None;
        }

        /// <summary>
        /// Determines whether the specified scraper contains a specifc scraper.
        /// </summary>
        /// <param name="scraper">
        /// The scraper. 
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified scraper contains scraper; otherwise, <c>false</c> . 
        /// </returns>
        public bool ContainsScraper(ScraperList scraper)
        {
            if (this.ContainsScraperText(scraper) && this.ContainsScraperImage(scraper))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [contains scraper image] [the specified scraper].
        /// </summary>
        /// <param name="scraper">
        /// The scraper. 
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains scraper image] [the specified scraper]; otherwise, <c>false</c> . 
        /// </returns>
        public bool ContainsScraperImage(ScraperList scraper)
        {
            if (this.Poster == scraper)
            {
                return true;
            }

            if (this.Fanart == scraper)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [contains scraper text] [the specified scraper].
        /// </summary>
        /// <param name="scraper">
        /// The scraper. 
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains scraper text] [the specified scraper]; otherwise, <c>false</c> . 
        /// </returns>
        public bool ContainsScraperText(ScraperList scraper)
        {
            if (this.Title == scraper)
            {
                return true;
            }

            if (this.Year == scraper)
            {
                return true;
            }

            if (this.OriginalTitle == scraper)
            {
                return true;
            }

            if (this.Rating == scraper)
            {
                return true;
            }

            if (this.Director == scraper)
            {
                return true;
            }

            if (this.Tagline == scraper)
            {
                return true;
            }

            if (this.Plot == scraper)
            {
                return true;
            }

            if (this.Outline == scraper)
            {
                return true;
            }

            if (this.Certification == scraper)
            {
                return true;
            }

            if (this.Top250 == scraper)
            {
                return true;
            }

            if (this.Mpaa == scraper)
            {
                return true;
            }

            if (this.Country == scraper)
            {
                return true;
            }

            if (this.Language == scraper)
            {
                return true;
            }

            if (this.Genre == scraper)
            {
                return true;
            }

            if (this.Runtime == scraper)
            {
                return true;
            }

            if (this.Budget == scraper)
            {
                return true;
            }

            if (this.ReleaseDate == scraper)
            {
                return true;
            }

            if (this.Revenue == scraper)
            {
                return true;
            }

            if (this.Studio == scraper)
            {
                return true;
            }

            if (this.Homepage == scraper)
            {
                return true;
            }

            if (this.Votes == scraper)
            {
                return true;
            }

            if (this.Trailer == scraper)
            {
                return true;
            }

            if (this.Cast == scraper)
            {
                return true;
            }

            if (this.Writers == scraper)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}