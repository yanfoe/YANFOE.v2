// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Find.cs">
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
//   Class contain IO functions to find information or files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.IO
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;
    using YANFOE.Tools.ThirdParty;

    #endregion

    /// <summary>
    ///   Class contain IO functions to find information or files.
    /// </summary>
    public static class Find
    {
        #region Public Methods and Operators

        /// <summary>
        /// The find all files in a path
        /// </summary>
        /// <param name="path">
        /// The files path. 
        /// </param>
        /// <param name="type">
        /// The SearchOption type. 
        /// </param>
        /// <returns>
        /// All files in the path 
        /// </returns>
        public static string[] FindAll(string path, SearchOption type)
        {
            return FileHelper.GetFilesRecursive(path).ToArray();
        }

        /// <summary>
        /// Finds the files in path.
        /// </summary>
        /// <param name="path">
        /// The files path. 
        /// </param>
        /// <param name="type">
        /// The SearchOption type. 
        /// </param>
        /// <returns>
        /// A collection of files and FileInfo collections 
        /// </returns>
        public static Dictionary<string, Section> FindFilesInPath(string path, SearchOption type)
        {
            var fileCollection = new List<string>();

            foreach (string ext in Get.InOutCollection.VideoExtentions)
            {
                var files = FileHelper.GetFilesRecursive(path, "*." + ext.ToLower().ToList());

                fileCollection.AddRange((from f in files select f).ToList());
            }

            return fileCollection.ToDictionary(
                file => file, 
                file =>
                Regex.IsMatch(
                    file, "(?<![0-9])s{0,1}([0-9]{1,2})((?:(?:e[0-9]+)+)|(?:(?:x[0-9]+)+))", RegexOptions.IgnoreCase)
                    ? Section.Tv
                    : Section.Movies);
        }

        /// <summary>
        /// Finds a fanart image related to a movie file
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The find fanart. 
        /// </returns>
        public static string FindMovieFanart(string fileName, string path, string[] fileList = null)
        {
            return FindCore(
                fileName, path, Get.InOutCollection.FanartTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Finds the NFO.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The find nfo. 
        /// </returns>
        public static string FindMovieNFO(string fileName, string path, string[] fileList = null)
        {
            string nfo = FindCore(
                fileName, path, Get.InOutCollection.NFOTypes, Get.InOutCollection.NfoExtentions, fileList);

            if (nfo == string.Empty)
            {
                // Attempt to find any nfo file stored in the folder
                string[] files = Directory.GetFiles(path, "*.nfo", SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
                {
                    nfo = files[0];
                }
            }

            return nfo;
        }

        /// <summary>
        /// Finds the poster.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The find poster. 
        /// </returns>
        public static string FindMoviePoster(string fileName, string path, string[] fileList = null)
        {
            return FindCore(
                fileName, path, Get.InOutCollection.PosterTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Finds the season NFO.
        /// </summary>
        /// <param name="seriesName">
        /// Name of the season. 
        /// </param>
        /// <param name="seasonPath">
        /// The season path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The found nfo. 
        /// </returns>
        public static string FindNFO(string seriesName, string seasonPath, string[] fileList = null)
        {
            var find = FindCore(
                seriesName, seasonPath, Get.InOutCollection.NFOTypes, Get.InOutCollection.NfoExtentions, fileList);

            if (!string.IsNullOrEmpty(find))
            {
                return find;
            }

            seriesName = FileSystemCharChange.To(
                seriesName, FileSystemCharChange.ConvertArea.Tv, FileSystemCharChange.ConvertType.Hex);
            find = FindCore(
                seriesName, seasonPath, Get.InOutCollection.NFOTypes, Get.InOutCollection.NfoExtentions, fileList);

            if (!string.IsNullOrEmpty(find))
            {
                return find;
            }

            seriesName = FileSystemCharChange.To(
                seriesName, FileSystemCharChange.ConvertArea.Tv, FileSystemCharChange.ConvertType.Char);
            find = FindCore(
                seriesName, seasonPath, Get.InOutCollection.NFOTypes, Get.InOutCollection.NfoExtentions, fileList);

            if (!string.IsNullOrEmpty(find))
            {
                return find;
            }

            return string.Empty;
        }

        /// <summary>
        /// Finds a season banner in a path
        /// </summary>
        /// <param name="seasonName">
        /// The season name. 
        /// </param>
        /// <param name="seasonPath">
        /// The season path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The season banner. 
        /// </returns>
        public static string FindSeasonBanner(string seasonName, string seasonPath, string[] fileList = null)
        {
            return FindCore(
                seasonName, seasonPath, Get.InOutCollection.BannerTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find season fanart.
        /// </summary>
        /// <param name="seasonName">
        /// The season name. 
        /// </param>
        /// <param name="seasonPath">
        /// The season path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The season fanart. 
        /// </returns>
        public static string FindSeasonFanart(string seasonName, string seasonPath, string[] fileList = null)
        {
            return FindCore(
                seasonName, seasonPath, Get.InOutCollection.FanartTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find season poster.
        /// </summary>
        /// <param name="seasonName">
        /// The season name. 
        /// </param>
        /// <param name="seasonPath">
        /// The season path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The season poster. 
        /// </returns>
        public static string FindSeasonPoster(string seasonName, string seasonPath, string[] fileList = null)
        {
            return FindCore(
                seasonName, seasonPath, Get.InOutCollection.PosterTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find series banner.
        /// </summary>
        /// <param name="name">
        /// The series name. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The series banner. 
        /// </returns>
        public static string FindSeriesBanner(string name, string path, string[] fileList = null)
        {
            return FindCore(name, path, Get.InOutCollection.BannerTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find series fanart.
        /// </summary>
        /// <param name="name">
        /// The series name. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The series fanart. 
        /// </returns>
        public static string FindSeriesFanart(string name, string path, string[] fileList = null)
        {
            return FindCore(name, path, Get.InOutCollection.FanartTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find series poster.
        /// </summary>
        /// <param name="name">
        /// The series name. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The series poster. 
        /// </returns>
        public static string FindSeriesPoster(string name, string path, string[] fileList = null)
        {
            return FindCore(name, path, Get.InOutCollection.PosterTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Find tv series screenshot.
        /// </summary>
        /// <param name="fileName">
        /// The file name. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The tv series screenshot. 
        /// </returns>
        public static string FindTvSeriesScreenshot(string fileName, string path, string[] fileList = null)
        {
            return FindCore(
                fileName, path, Get.InOutCollection.EpisodeTypes, Get.InOutCollection.ImageExtentions, fileList);
        }

        /// <summary>
        /// Return the largest file in a specific path using the pattern specified.
        /// </summary>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="pattern">
        /// The pattern. 
        /// </param>
        /// <returns>
        /// The largest file in path. 
        /// </returns>
        public static string LargestFileInPath(string path, string pattern = "*.*")
        {
            const string LogCatagory = "IO > Find > LargestFileInPath";

            try
            {
                if (Directory.Exists(path))
                {
                    var directoryInfo = new DirectoryInfo(path);

                    FileInfo[] files = directoryInfo.GetFiles(pattern, SearchOption.TopDirectoryOnly);

                    IEnumerable<string> fileOrdered = from file in files
                                                      orderby file.Length descending
                                                      select file.FullName;

                    if (fileOrdered.Count() > 0)
                    {
                        return fileOrdered.First();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return string.Empty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Core find routine
        /// </summary>
        /// <param name="name">
        /// The name to search for. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="types">
        /// The types. 
        /// </param>
        /// <param name="extentions">
        /// The extentions. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// The find core. 
        /// </returns>
        private static string FindCore(
            string name, string path, List<string> types, List<string> extentions, string[] fileList = null)
        {
            if (path.Contains("BDMV\\STREAM\\"))
            {
                name = MovieNaming.GetBluRayName(path + "name");
                path = path.Replace("BDMV\\STREAM\\", string.Empty);
                fileList = null;
            }

            if (path.Contains("VIDEO_TS\\"))
            {
                name = MovieNaming.GetDvdName(path + "name");
                path = path.Replace("VIDEO_TS\\", string.Empty);
                fileList = null;
            }

            if (fileList == null)
            {
                fileList = FindAll(path, SearchOption.TopDirectoryOnly);
            }

            foreach (string f in fileList)
            {
                string fileMatch = Path.GetFileNameWithoutExtension(f);

                string matchVideoExtention = Path.GetExtension(f).ToLower().Replace(".", string.Empty);

                if (extentions.Contains(matchVideoExtention))
                {
                    foreach (string type in types)
                    {
                        string replacedType = type.Replace("<fileName>", name);

                        if (replacedType.ToLower() == fileMatch.ToLower())
                        {
                            return f;
                        }

                        var a = RemovePart(replacedType).ToLower();
                        var b = RemovePart(fileMatch).ToLower();

                        if (a == b)
                        {
                            return f;
                        }
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The remove part.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string RemovePart(string path)
        {
            return Regex.Replace(
                path, "(?:(?:CD[0-9]+)|(?:DISC[0-9]+)|(?:DISK[0-9]+)|(?:PART[0-9]+))", string.Empty, RegexOptions.IgnoreCase);
        }

        #endregion
    }
}