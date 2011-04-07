// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScraperHandlerTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for ScraperHandlerTest and is intended
//   to contain all ScraperHandlerTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// This is a test class for ScraperHandlerTest and is intended
    /// to contain all ScraperHandlerTest Unit Tests
    /// </summary>
    [TestClass]
    public class ScraperHandlerTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for GetScrapersSupportingScrape
        /// </summary>
        [TestMethod]
        public void GetScrapersSupportingScrapeTest()
        {
            const ScrapeFields ScrapeFields = new ScrapeFields();
            var actual = MovieScraperHandler.GetScrapersSupporting(ScrapeFields);
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        /// A test for ReturnAllScrapers
        /// </summary>
        [TestMethod]
        public void ReturnScrapersTest()
        {
            var actual = MovieScraperHandler.ReturnAllScrapers();
            Assert.IsTrue(actual.Count > 0);
        }

        /// <summary>
        /// A test for ReturnAllScrapers
        /// </summary>
        [TestMethod]
        public void GetScrapersAsStringListTest()
        {
            var actual = MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Title, false);
            Assert.IsTrue(actual.Count > 0);
        }
    }
}
