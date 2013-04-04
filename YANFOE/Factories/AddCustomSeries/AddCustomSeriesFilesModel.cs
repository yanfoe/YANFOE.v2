// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AddCustomSeriesFilesModel.cs">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the
//   license terms of this work.
// </license>
// <summary>
//   The add custom series files model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.AddCustomSeries
{
    /// <summary>
    ///   The add custom series files model.
    /// </summary>
    public class AddCustomSeriesFilesModel
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the episode number.
        /// </summary>
        public int EpisodeNumber { get; set; }

        /// <summary>
        ///   Gets or sets the file path.
        /// </summary>
        public string FilePath { get; set; }

        #endregion
    }
}