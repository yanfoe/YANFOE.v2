// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaPathFileModelTest.cs" company="Russell Lewis">
//   Copyright 2011 Russell Lewis
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Testing
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using YANFOE.Models.GeneralModels.AssociatedFiles;

    /// <summary>
    /// This is a test class for MediaPathFileModelTest and is intended
    /// to contain all MediaPathFileModelTest Unit Tests
    /// </summary>
    [TestClass]
    public class MediaPathFileModelTest
    {
        /// <summary>
        /// A test for Path
        /// </summary>
        [TestMethod]
        public void PathTest()
        {
            var target = new MediaPathFileModel();

            target.PathAndFileName = @"c:\stuff\folder\this is a filename.avi";

            Assert.IsTrue(target.FilenameExt == ".avi");
            Assert.IsTrue(target.FilenameWithOutExt == "this is a filename");
            Assert.IsTrue(target.Path == @"c:\stuff\folder\");
        }
    }
}
