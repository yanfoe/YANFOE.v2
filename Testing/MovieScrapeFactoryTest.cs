using YANFOE.Factories.Scraper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YANFOE.Scrapers.Movie.Models.Search;

namespace Testing
{
    
    
    /// <summary>
    ///This is a test class for MovieScrapeFactoryTest and is intended
    ///to contain all MovieScrapeFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MovieScrapeFactoryTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for QuickSearchTmdb
        ///</summary>
        [TestMethod()]
        public void QuickSearchTmdbTest()
        {
            Query query = new Query();

            query.Title = "Sin City";

            var actual = MovieScrapeFactory.QuickSearchTmdb(query);

            Assert.IsTrue(actual);
        }
    }
}
