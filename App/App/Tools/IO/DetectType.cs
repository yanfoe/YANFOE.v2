// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetectType.cs" company="The YANFOE Project">
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
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Tools.IO
{
    using System.IO;
    using System.Text.RegularExpressions;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Settings;
    using YANFOE.Settings.ConstSettings;

    /// <summary>
    /// Detect the type of the file.
    /// </summary>
    public static class DetectType
    {
        #region Constants and Fields

        /// <summary>
        /// The movie regex.
        /// </summary>
        public const string MovieRegex =
            @"(CD\d{1,10})|(CD\s\d{1,10})|(PART\d{1,10})|(PART\s\d{1,10})|(DISC\d{1,10})|(DSIC\s\d{1,10})|(DISK\d{1,10})|(DISK\s\d{1,10})";

        /// <summary>
        /// The tv regex.
        /// </summary>
        public const string TvRegex = DefaultRegex.Tv;

        #endregion

        #region Public Methods

        /// <summary>
        /// The find type.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="containsTv">
        /// The contains tv.
        /// </param>
        /// <param name="containMovies">
        /// The contain movies.
        /// </param>
        /// <returns>
        /// MediaPathFileType enum
        /// </returns>
        public static MediaPathFileModel.MediaPathFileType FindType(
            string fileName, bool containsTv, bool containMovies)
        {
            string ext = Path.GetExtension(fileName).ToLower().Replace(".", string.Empty);

            if (Get.InOutCollection.VideoExtentions.Contains(ext))
            {
                if (containsTv && Regex.IsMatch(fileName, TvRegex, RegexOptions.IgnoreCase))
                {
                    return MediaPathFileModel.MediaPathFileType.TV;
                }

                if (containMovies && Regex.IsMatch(fileName, MovieRegex, RegexOptions.IgnoreCase))
                {
                    return MediaPathFileModel.MediaPathFileType.Movie;
                }

                if (fileName.ToLower() == "sample")
                {
                    return MediaPathFileModel.MediaPathFileType.Sample;
                }

                if (containMovies)
                {
                    return MediaPathFileModel.MediaPathFileType.Movie;
                }

                return MediaPathFileModel.MediaPathFileType.Video;
            }

            if (ext == "nfo" || ext == "xml")
            {
                return MediaPathFileModel.MediaPathFileType.NFO;
            }

            if (ext == ".gif" || ext == ".jpg" || ext == ".png")
            {
                return MediaPathFileModel.MediaPathFileType.NFO;
            }

            if (ext == ".mp3")
            {
                return MediaPathFileModel.MediaPathFileType.Music;
            }

            return MediaPathFileModel.MediaPathFileType.Unknown;
        }

        #endregion
    }
}