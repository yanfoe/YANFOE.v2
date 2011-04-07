// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamingTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// <summary>
//   This is a test class for NamingTest and is intended
//   to contain all NamingTest Unit Tests
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing.Tools.Importing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for NamingTest and is intended
    /// to contain all NamingTest Unit Tests
    /// </summary>
    [TestClass]
    public class NamingTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        ///// <summary>
        ///// A test for GetFileType
        ///// </summary>
        //[TestMethod]
        //[DeploymentItem("YANFOE.exe")]
        //public void GetFileTypeTest()
        //{
        //    string path = "This is a movie [trailer 1].avi";
        //    var expected = Naming_Accessor.MovieFileType.Trailer;
        //    var actual = Naming_Accessor.GetFileType(path);
        //    Assert.AreEqual(expected, actual);

        //    path = "this is a movie.SAMPLE.avi";
        //    expected = Naming_Accessor.MovieFileType.Sample;
        //    actual = Naming_Accessor.GetFileType(path);
        //    Assert.AreEqual(expected, actual);

        //    path = "this is a movie.avi";
        //    expected = Naming_Accessor.MovieFileType.Movie;
        //    actual = Naming_Accessor.GetFileType(path);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        ///// A test for GetMovieName
        ///// </summary>
        //[TestMethod]
        //public void GetMovieNameTest()
        //{
        //    // Test Folder
        //    var path = @"c:\this is a movie folder\this is a movie file(2010).avi"; 
        //    var expected = "this is a movie folder";
        //    var actual = MovieNaming.GetMovieName(path, AddFolderType.NameByFolder);
        //    Assert.AreEqual(expected, actual);

        //    // Test File
        //    path = @"c:\this is a movie folder\this is a movie file(2010).avi"; 
        //    expected = "this is a movie file"; 
        //    actual = MovieNaming.GetMovieName(path, AddFolderType.NameByFile);
        //    Assert.AreEqual(expected, actual);

        //    // Test File
        //    path = @"c:\this is a movie folder\this is a movie file.divx.2010.avi"; 
        //    expected = "this is a movie file"; 
        //    actual = MovieNaming.GetMovieName(path, AddFolderType.NameByFile);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        ///// A test for GetMovieYear
        ///// </summary>
        //[TestMethod]
        //public void GetMovieYearTest()
        //{
        //    const string text = "this is a movie (2010)";
        //    const string expected = "2010";
        //    var actual = MovieNaming.GetMovieYear(text);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        ///// A test for GetPartNumber
        ///// </summary>
        //[TestMethod]
        //[DeploymentItem("YANFOE.exe")]
        //public void GetPartNumberTest()
        //{
        //    var fileName = "filename [cd1]";
        //    var expected = 1;
        //    var actual = Naming_Accessor.GetPartNumber(fileName);
        //    Assert.AreEqual(expected, actual);

        //    fileName = "filename [part 3]";
        //    expected = 3;
        //    actual = Naming_Accessor.GetPartNumber(fileName);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        ///// A test for GetSetName
        ///// </summary>
        //[TestMethod]
        //[DeploymentItem("YANFOE.exe")]
        //public void GetSetNameTest()
        //{
        //    const string fileName = "this is a movie [set setname1]";
        //    const string expected = "setname1";
        //    string actual = Naming_Accessor.GetSetName(fileName);
        //    Assert.AreEqual(expected, actual);
        //}

        ///// <summary>
        ///// A test for RemoveBrackets
        ///// </summary>
        //[TestMethod]
        //[DeploymentItem("YANFOE.exe")]
        //public void RemoveBracketsTest()
        //{
        //    const string fileName = "this is a test[set][trailer]";
        //    const string expected = "this is a test";
        //    string actual = Naming_Accessor.RemoveBrackets(fileName);
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
