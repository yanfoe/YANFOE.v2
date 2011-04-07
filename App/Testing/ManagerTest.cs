using YANFOE.InternalApps.DownloadManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YANFOE.InternalApps.DownloadManager.Model;

namespace Testing
{
    using System.IO;

    using YANFOE.Tools.Enums;

    /// <summary>
    ///This is a test class for ManagerTest and is intended
    ///to contain all ManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ManagerTest
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
        ///A test for Download
        ///</summary>
        [TestMethod]
        public void DownloadTest()
        {
            var actual = Downloader.ProcessDownload("http://www.google.com", DownloadType.Html, Section.Tv, false);
            Assert.IsFalse(string.IsNullOrEmpty(actual));

            actual = Downloader.ProcessDownload("http://yanfoef.com/images/logo.gif", DownloadType.Binary, Section.Movies, false);
            Assert.IsFalse(string.IsNullOrEmpty(actual));
            Assert.IsTrue(File.Exists(actual));
        }
    }
}
