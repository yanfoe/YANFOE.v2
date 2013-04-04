// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="GalleryType.cs">
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
//   Specify the type of gallery.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Enums
{
    /// <summary>
    ///   Specify the type of gallery.
    /// </summary>
    public enum GalleryType
    {
        /// <summary>
        ///   GalleryType: none.
        /// </summary>
        None, 

        /// <summary>
        ///   GalleryType: movie poster.
        /// </summary>
        MoviePoster, 

        /// <summary>
        ///   GalleryType: movie fanart.
        /// </summary>
        MovieFanart, 

        /// <summary>
        ///   GalleryType: tv series poster.
        /// </summary>
        TvSeriesPoster, 

        /// <summary>
        ///   GalleryType: tv series fanart.
        /// </summary>
        TvSeriesFanart, 

        /// <summary>
        ///   GalleryType: tv series banner.
        /// </summary>
        TvSeriesBanner, 

        /// <summary>
        ///   GalleryType: tv season fanart.
        /// </summary>
        TvSeasonFanart, 

        /// <summary>
        ///   GalleryType: tv season poster.
        /// </summary>
        TvSeasonPoster, 

        /// <summary>
        ///   GalleryType: tv season banner.
        /// </summary>
        TvSeasonBanner, 

        /// <summary>
        ///   GalleryType: tv episode screenshot.
        /// </summary>
        TvEpisodeScreenshot, 
    }
}