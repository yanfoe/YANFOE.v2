// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieScraperHandlerTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for MovieScraperHandlerTest and is intended
//   to contain all MovieScraperHandlerTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie;

    /// <summary>
    /// This is a test class for MovieScraperHandlerTest and is intended
    /// to contain all MovieScraperHandlerTest Unit Tests
    /// </summary>
    [TestClass()]
    public class MovieScraperHandlerTest
    {

        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// A test for ScrapeMovie
        /// </summary>
        [TestMethod()]
        public void RunSingleScrapeTest()
        {
            MovieScraperHandler target = new MovieScraperHandler();
            MovieModel movie = new MovieModel();

            movie.Title = "Sin City";
            movie.ImdbId = "0401792";

            movie.ScraperGroup = "test";

            var result = target.RunSingleScrape(movie, true);

            // Test Title
            Assert.IsTrue(result);

            // Test Year
            Assert.IsTrue(movie.Year == 2005);

            //test  Cast
            Assert.IsTrue(movie.Cast.Count > 0);

        }

        /// <summary>
        /// A test for ScrapeMovie
        /// </summary>
        [TestMethod()]
        public void TestTitleScrape()
        {
            MovieScraperHandler target = new MovieScraperHandler();
            MovieModel movie = new MovieModel();

            movie.ImdbId = "0401792";

            movie.ScraperGroup = "test";

            var result = target.RunSingleScrape(movie, true);

            Assert.IsTrue(result);
            Assert.IsFalse(string.IsNullOrEmpty(movie.Title));
        }

        /// <summary>
        /// A test for ScrapeMovie
        /// </summary>
        [TestMethod()]
        public void TestYearScrape()
        {
            MovieScraperHandler target = new MovieScraperHandler();
            MovieModel movie = new MovieModel();

            movie.Title = "Sin City";
            movie.ImdbId = "0401792";
            movie.TmdbId = "187";

            movie.ScraperGroup = "test";

            var result = target.RunSingleScrape(movie, true);

            var title = movie.Title;
            var year = movie.Year;
            var certification = movie.Certification;
            var country = movie.Country;
            var director = movie.Director;
            var genre = movie.Genre;
            var language = movie.Language;
            var outline = movie.Outline;
            var plot = movie.Plot;
            var rating = movie.Rating;
            var studio = movie.Studios;
            var tagline = movie.Tagline;
            var top250 = movie.Top250;
            var votes = movie.Votes;
            var writers = movie.Writers;
            var posters = movie.AlternativePosters;
            var fanart = movie.AlternativeFanart;

            //Assert.IsTrue(result);
            Assert.IsTrue(result);
        }
    }
}
