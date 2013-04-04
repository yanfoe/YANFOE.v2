using YANFOE.Factories.Apps.MediaInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YANFOE.Factories.Apps.MediaInfo.Models;

namespace Testing
{
    using System.Diagnostics;

    /// <summary>
    ///This is a test class for MediaInfoFactoryTest and is intended
    ///to contain all MediaInfoFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MediaInfoFactoryTest
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
        ///A test for DoMediaInfoScan
        ///</summary>
        [TestMethod()]
        public void DoMediaInfoScanTest()
        {
            string filePath = @"E:\HD\Sex and the City (2008)\Sex and the City (2008).mkv";
            MiResponseModel actual = MediaInfoFactory.DoMediaInfoScan(filePath);


        }

        /// <summary>
        ///A test for GetMediaInfoXml
        ///</summary>
        [TestMethod()]
        public void GetMediaInfoXmlTest()
        {
            string filePath = @"E:\HD\Sex and the City (2008)\Sex and the City (2008).mkv";
            var actual = MediaInfoFactory.GetMediaInfoXml(filePath);
            Debug.Write(actual);
        }
    }
}
