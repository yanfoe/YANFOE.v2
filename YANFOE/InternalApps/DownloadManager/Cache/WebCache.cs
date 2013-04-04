// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebCache.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.DownloadManager.Cache
{
    using System;
    using System.IO;

    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// Deals will all Web Cache
    /// </summary>
    public static class WebCache
    {
        /// <summary>
        /// Gets the path from URL.
        /// </summary>
        /// <param name="url">The URL to download from</param>
        /// <param name="section">The section (Movies/TV)</param>
        /// <returns>A full path</returns>
        public static string GetPathFromUrl(string url, Section section)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            foreach (char c in invalid)
            {
                url = url.Replace(c.ToString(), "");
            }
            return GetSectionPath(section) + Path.DirectorySeparatorChar + UrlToFileName(url);
        }

        /// <summary>
        /// Checks if URL is too long.
        /// </summary>
        /// <param name="downloadUrl">The download URL.</param>
        /// <returns>A bool value</returns>
        public static bool CheckIfUrlIsTooLong(string downloadUrl)
        {
            return downloadUrl.Length <= 200;
        }

        /// <summary>
        /// Checks if cache path exists.
        /// </summary>
        /// <param name="cachePath">
        /// The cache path.
        /// </param>
        /// <returns>
        /// Check if cache path exists.
        /// </returns>
        public static bool CheckIfCachePathExists(string cachePath)
        {
            return File.Exists(cachePath);
        }

        public static bool CheckIfDownloadItemExistsInCache(DownloadItem downloadItem, bool populateReturn)
        {
            var path = GetPathFromUrl(downloadItem.Url, downloadItem.Section);
            var checkExists = File.Exists(path);

            if (checkExists)
            {
                switch (downloadItem.Type)
                {
                    case DownloadType.Html:
                        downloadItem.Result = new HtmlResult { Success = true, Result = File.ReadAllText(path) };
                        break;
                    case DownloadType.Binary:
                        downloadItem.Result = new BinaryResult { Success = true, Result = path };
                        break;
                }
            }

            return checkExists;
        }

        /// <summary>
        /// Gets the section path.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// The get section path.
        /// </returns>
        private static string GetSectionPath(Section section)
        {
            if (section == Section.Tv)
            {
                return Get.FileSystemPaths.PathDirCacheTV;
            }

            return section == Section.Movies ? Get.FileSystemPaths.PathDirCacheMovies : string.Empty;
        }

        /// <summary>
        /// Returns an "Known value replacement encoded" version of a URL  
        /// </summary>
        /// <param name="url">The url to encode.</param>
        /// <returns>The processed filename</returns>
        private static string UrlToFileName(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            // Generic
            url = url.Replace(@"http://", "[hp]");
            url = url.Replace(@"/", "[f]");
            url = url.Replace(@":", "[b]");
            url = url.Replace(@"?", "[q]");
            url = url.Replace(@"http", "[h]");
            url = url.Replace(@"search.yanfoe.com", "[yqs]");
            url = url.Replace(@"fanart", "[fa]");

            // TMDB
            url = url.Replace(@"images.themoviedb.org", "[ti]");
            url = url.Replace(@"api.themoviedb.org", "[ta]");
            url = url.Replace(@"Movie.getInfo", "[mg]");
            url = url.Replace(@"Movie.imdbLookup", "[mi]");
            url = url.Replace(@"backdrops", "[bk]");
            url = url.Replace(@"posters", "[ps]");
            url = url.Replace(@"_thumb", "[t]");

            // IMDB
            url = url.Replace(@"www.imdb.com", "[imdb]");
            url = url.Replace(@"plotsummary", "[ps]");
            url = url.Replace(@"synopsis", "[sy]");

            // Allocine
            url = url.Replace(@"allocine.fr", "[al]");
            url = url.Replace(@"galerievignette_gen_cfilm=", "[gv]");
            url = url.Replace(@"a69.g.akamai.net[f]n[f]69[f]10688[f]v1[f]img5.[al][f]acmedia[f]rsz[f]434[f]x[f]x[f]x[f]medias[f]nmedia", "[ai]");

            // Ofdb
            url = url.Replace(@"www.ofdb.com", "[ofdb]");

            // Movie Meter
            url = url.Replace(@"$$Internal_movieMeterHandler", "[mm]");

            // tvdb
            url = url.Replace(@"www.thetvdb.com", "[tv]");
            url = url.Replace(@"www.thetvdb.com", "[ctv]");
            url = url.Replace(@"banners", "[bn]");
            url = url.Replace(@"cache", "[_c]");
            url = url.Replace(@"original", "[or]");
            url = url.Replace(@"original", "[or]");
            url = url.Replace(@"GetSeries.php", "[gs]");

            return url;
        }
    }
}
