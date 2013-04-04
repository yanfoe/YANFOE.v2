// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportMoviesFactoryTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for ImportMoviesFactoryTest and is intended
//   to contain all ImportMoviesFactoryTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Factories.Import;

    /// <summary>
    /// This is a test class for ImportMoviesFactoryTest and is intended
    /// to contain all ImportMoviesFactoryTest Unit Tests
    /// </summary>
    [TestClass]
    public class ImportMoviesFactoryTest
    {
        /// <summary>
        /// A test for FindNFO
        /// </summary>
        [TestMethod]
        public void FindNFOTest()
        {
            const string NfoFullPath = @"C:\tests\movies\this is a movie\movie.avi";
            const string NfoPath = @"C:\tests\movies\this is a movie\";

            string actual = ImportMoviesFactory.Instance.FindNFO("this is a movie file.avi", NfoPath);
            Assert.IsTrue(actual == NfoFullPath);
        }

        /// <summary>
        /// A test for FindPoster
        /// </summary>
        [TestMethod]
        public void FindPosterTest()
        {
            const string NfoFullPath = @"C:\tests\movies\this is a movie\this is a movie file.jpg";
            const string NfoPath = @"C:\tests\movies\this is a movie\";

            string actual = ImportMoviesFactory.Instance.FindPoster("this is a movie file.avi", NfoPath);
            Assert.IsTrue(actual == NfoFullPath);
        }

        /// <summary>
        /// A test for FindFanart
        /// </summary>
        [TestMethod]
        public void FindFanartTest()
        {
            const string NfoFullPath = @"C:\tests\movies\this is a movie\this is a movie file-fanart.jpg";
            const string NfoPath = @"C:\tests\movies\this is a movie\";

            string actual = ImportMoviesFactory.Instance.FindFanart("this is a movie file.avi", NfoPath);
            Assert.IsTrue(actual == NfoFullPath);
        }
    }
}
