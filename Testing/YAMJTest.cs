using YANFOE.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YANFOE.Models.MovieModels;
using YANFOE.Models.IOModels;

namespace Testing
{
    using System.ComponentModel;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.Models;

    /// <summary>
    ///This is a test class for YAMJTest and is intended
    ///to contain all YAMJTest Unit Tests
    ///</summary>
    [TestClass()]
    public class YAMJTest
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

        private MovieModel CreateTestMovie()
        {
            var movieModel = new MovieModel();

            movieModel.Title = "this is a title";
            movieModel.Year = 2008;
            movieModel.Top250 = 2;
            movieModel.ReleaseDate = new DateTime(2008, 02, 03);
            movieModel.Rating = 6.5;
            movieModel.Votes = 199999;
            movieModel.Plot = "This is a plot";
            movieModel.Tagline = "This is a tagline";
            movieModel.Runtime = 199;
            movieModel.Mpaa = "Rated G";
            movieModel.Certification = "USA:G";
            movieModel.SetStudio = "Fox Studios";
            movieModel.Country = new BindingList<string> { "USA", "UK" };
            movieModel.Genre = new BindingList<string> { "Comedy", "Drama" };
            var person1 = new PersonModel("Russell Lewis", role: "Actors person");
            movieModel.Writers = new BindingList<PersonModel> { person1, person1 };
            movieModel.Director = new BindingList<PersonModel> { person1 };
            movieModel.Cast = new BindingList<PersonModel> { person1, person1 };
            movieModel.CurrentPosterImageUrl = "http://cf1.themoviedb.org/posters/011/4cdb8b335e73d605e6000011/toy-story-3-cover.jpg";
            movieModel.CurrentFanartImageUrl = "http://cf1.themoviedb.org/backdrops/0bf/4bc92cb5017a3c57fe0120bf/toy-story-3-thumb.jpg";

            movieModel.AssociatedFiles.AddToMediaCollection(this.CreateTextMediaPathFileModel());

            return movieModel;
        }

        private MediaPathFileModel CreateTextMediaPathFileModel()
        {
            var mediaPathFileModel = new MediaPathFileModel();
            mediaPathFileModel.PathAndFileName = @"D:\Movies\Toy Story 3\Toy.Story.3.TC.XviD-FLAWL3SS.avi";

            return mediaPathFileModel;
        }

        /// <summary>
        ///A test for GenerateMovieOutput
        ///</summary>
        [TestMethod()]
        public void varGenerateMovieOutputTest()
        {
            var target = new YAMJ();
            var movieModel = this.CreateTestMovie();

            var output = target.GenerateMovieOutput(movieModel);

            Assert.IsFalse(string.IsNullOrEmpty(output));
        }

        /// <summary>
        ///A test for SaveMovie
        ///</summary>
        [TestMethod()]
        public void SaveMovieTest()
        {
            var target = new YAMJ();
            MovieModel movieModel = this.CreateTestMovie();
            MovieSaveSettings movieSaveSettings = new MovieSaveSettings
                {
                    NormalFanartNameTemplate = "<fileName>-fanart",
                    NormalPosterNameTemplate = "<fileName>",
                    NormalNfoNameTemplate = "<fileName>.nfo"
                };
            
            target.SaveMovie(movieModel);
        }
    }
}
