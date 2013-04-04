// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TheMovieDbTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for TheMovieDbTest and is intended
//   to contain all TheMovieDbTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using System.ComponentModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.UI;

    /// <summary>
    /// This is a test class for TheMovieDbTest and is intended
    /// to contain all TheMovieDbTest Unit Tests
    /// </summary>
    [TestClass]
    public class TheMovieDbTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// The MovieUniqueId for theses tests.
        /// </summary>
        private const string ID = "187";

        /// <summary>
        /// A test for SearchSite
        /// </summary>
        [TestMethod]
        public void SearchSiteTest()
        {
            var target = new TheMovieDb();
            var query = new Query { Results = new ThreadedBindingList<QueryResult>(), Title = "Transformers", Year = "2007" };
            const int ThreadID = 0;
            var actual = target.SearchSite(query, ThreadID, string.Empty);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// A test for SearchViaBing
        /// </summary>
        [TestMethod]
        public void SearchViaBingTest()
        {
            var target = new TheMovieDb();
            var query = new Query { Results = new ThreadedBindingList<QueryResult>(), Title = "Transformers", Year = "2007" };
            const int ThreadID = 0;
            var actual = target.SearchViaBing(query, ThreadID, string.Empty);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// A test for SearchYANFOE
        /// </summary>
        [TestMethod]
        public void SearchYANFOETest()
        {
            var target = new TheMovieDb();
            var query = new Query { Results = new ThreadedBindingList<QueryResult>(), Title = "Transformers", Year = "2007" };
            const int ThreadID = 0;
            var actual = target.SearchYANFOE(query, ThreadID, string.Empty);
            Assert.IsTrue(actual);
        }

        /// <summary>
        /// A test for ScrapeTitle
        /// </summary>
        [TestMethod]
        public void ScrapeTitleTest()
        {
            var target = new TheMovieDb();
            string output;

            ThreadedBindingList<string> alternatives;

            var result = target.ScrapeTitle(ID, 0, out output, out alternatives, string.Empty);
            Assert.IsTrue(result);
            Assert.IsTrue(output == "Sin City");
        }

        /// <summary>
        /// A test for ScrapeYear
        /// </summary>
        [TestMethod]
        public void ScrapeYearTest()
        {
            var target = new TheMovieDb();

            int output;

            var result = target.ScrapeYear(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(output == 2005);
        }

        /// <summary>
        /// A test for ScrapeRating
        /// </summary>
        [TestMethod]
        public void ScrapeRatingTest()
        {
            var target = new TheMovieDb();

            double output;

            var result = target.ScrapeRating(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for ScrapeDirector
        /// </summary>
        [TestMethod]
        public void ScrapeDirectorTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<PersonModel> output;

            var result = target.ScrapeDirector(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for ScrapeOutline
        /// </summary>
        [TestMethod]
        public void ScrapeOutlineTest()
        {
            var target = new TheMovieDb();

            string output;

            var result = target.ScrapeOutline(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for ScrapeCertification
        /// </summary>
        [TestMethod]
        public void ScrapeCertificationTest()
        {
            var target = new TheMovieDb();

            string output;

            var result = target.ScrapeCertification(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for ScrapeCountry
        /// </summary>
        [TestMethod]
        public void ScrapeCountryTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<string> output;

            var result = target.ScrapeCountry(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(output.Count > 0);
        }

        /// <summary>
        /// A test for ScrapeLanguage
        /// </summary>
        [TestMethod]
        public void ScrapeLanguageTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<string> output;

            var result = target.ScrapeLanguage(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(output.Count > 0);
        }

        /// <summary>
        /// A test for ScrapeCast
        /// </summary>
        [TestMethod]
        public void ScrapeCastTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<PersonModel> output;

            var result = target.ScrapeCast(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(output.Count > 0);
        }

        /// <summary>
        /// A test for ScrapeTagline
        /// </summary>
        [TestMethod]
        public void ScrapeTaglineTest()
        {
            var target = new TheMovieDb();

            string output;

            var result = target.ScrapeTagline(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsFalse(string.IsNullOrEmpty(output));
        }

        /// <summary>
        /// A test for ScrapeStudio
        /// </summary>
        [TestMethod]
        public void ScrapeStudioTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<string> output;

            var result = target.ScrapeStudio(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(output.Count > 0);
        }

        /// <summary>
        /// A test for ScrapeWriters
        /// </summary>
        [TestMethod]
        public void ScrapeWritersTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<PersonModel> output;

            var result = target.ScrapeWriters(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for ScrapePosters
        /// </summary>
        [TestMethod]
        public void ScrapePostersTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<ImageDetailsModel> output;

            var result = target.ScrapePoster(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
            Assert.IsTrue(!string.IsNullOrEmpty(output[0].UriThumb.ToString()));
        }

        /// <summary>
        /// A test for ScrapeFanart
        /// </summary>
        [TestMethod]
        public void ScrapeFanartTest()
        {
            var target = new TheMovieDb();

            ThreadedBindingList<ImageDetailsModel> output;

            var result = target.ScrapeFanart(ID, 0, out output, string.Empty);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// A test for GetPeople
        /// </summary>
        [TestMethod]
        [DeploymentItem("YANFOE.exe")]
        public void GetPeopleTest()
        {
            var target = new TheMovieDb(); 

            const int ThreadID = 0;

            var actual = target.GetPeople(TheMovieDb.PersonType.Actor, ThreadID, ID);
            Assert.IsTrue(actual.Count > 10);

            actual = target.GetPeople(TheMovieDb.PersonType.Author, ThreadID, ID);
            Assert.IsTrue(actual.Count == 1);

            actual = target.GetPeople(TheMovieDb.PersonType.Director, ThreadID, ID);
            Assert.IsTrue(actual.Count == 3);
        }

        /// <summary>
        /// A test for GetImages
        /// </summary>
        [TestMethod]
        [DeploymentItem("YANFOE.exe")]
        public void GetImagesTest()
        {
            var target = new TheMovieDb();

            const int ThreadID = 0;

            var actual = target.GetImages(ImageUsageType.Poster, ThreadID, ID);
            Assert.IsTrue(actual.Count > 2);
        }


    }
}
