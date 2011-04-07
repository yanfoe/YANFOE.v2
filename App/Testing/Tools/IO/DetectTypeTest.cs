// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetectTypeTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for DetectTypeTest and is intended
//   to contain all DetectTypeTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing.Tools.IO
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.IO;

    /// <summary>
    /// This is a test class for DetectTypeTest and is intended
    /// to contain all DetectTypeTest Unit Tests
    /// </summary>
    [TestClass]
    public class DetectTypeTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for FindType
        /// </summary>
        [TestMethod]
        public void FindTypeTest()
        {
            var fileName = "this is a movie.avi"; 
            var expected = MediaPathFileModel.MediaPathFileType.Movie;
            var actual = DetectType.FindType(fileName, false, true);
            Assert.AreEqual(expected, actual);

            fileName = "this is a movie.avi";
            expected = MediaPathFileModel.MediaPathFileType.Movie;
            actual = DetectType.FindType(fileName, true, false);
            Assert.AreNotEqual(expected, actual);

            fileName = "this is a tv show.s01e02.avi";
            expected = MediaPathFileModel.MediaPathFileType.TV;
            actual = DetectType.FindType(fileName, true, false);
            Assert.AreEqual(expected, actual);

            fileName = "this is a tv show.s01e02.avi";
            expected = MediaPathFileModel.MediaPathFileType.TV;
            actual = DetectType.FindType(fileName, false, true);
            Assert.AreNotEqual(expected, actual);
        }
    }
}
