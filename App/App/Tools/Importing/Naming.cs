// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Naming.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Importing
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;

    using YANFOE.Settings;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// The movie naming.
    /// </summary>
    public static class MovieNaming
    {
        #region Enums

        /// <summary>
        /// The movie file type.
        /// </summary>
        public enum MovieFileType
        {
            /// <summary>
            /// File is a main movie
            /// </summary>
            Movie, 

            /// <summary>
            /// File is a sample video
            /// </summary>
            Sample, 

            /// <summary>
            /// File is a trailer video
            /// </summary>
            Trailer
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get blu ray name.
        /// </summary>
        /// <param name="path">
        /// The path of the bluray
        /// </param>
        /// <returns>
        /// The BluRay name.
        /// </returns>
        public static string GetBluRayName(string path)
        {
            string[] split = Path.GetDirectoryName(path).Split('\\');
            return split[split.Length - 3];
        }

        /// <summary>
        /// The get blu ray path.
        /// </summary>
        /// <param name="path">
        /// The path of the bluray
        /// </param>
        /// <returns>
        /// The BluRay path.
        /// </returns>
        public static string GetBluRayPath(string path)
        {
            string[] split = Path.GetDirectoryName(path).Split('\\');

            return string.Join(@"\", split, 0, split.Length - 3) + "\\";
        }

        /// <summary>
        /// The get dvd name.
        /// </summary>
        /// <param name="path">
        /// The DVD path.
        /// </param>
        /// <returns>
        /// The dvd name.
        /// </returns>
        public static string GetDvdName(string path)
        {
            string[] split = Path.GetDirectoryName(path).Split('\\');
            return split[split.Length - 2];
        }

        /// <summary>
        /// The get dvd path.
        /// </summary>
        /// <param name="path">
        /// The full DVD path.
        /// </param>
        /// <returns>
        /// The dvd path.
        /// </returns>
        public static string GetDvdPath(string path)
        {
            string[] split = Path.GetDirectoryName(path).Split('\\');

            return string.Join(@"\", split, 0, split.Length - 2) + "\\";
        }

        /// <summary>
        /// The get file type.
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        /// <returns>
        /// MovieFileType object
        /// </returns>
        public static MovieFileType GetFileType(string path)
        {
            if (Regex.IsMatch(path, "\\[([^\\[\\]]*trailer[^\\[]*)\\]"))
            {
                return MovieFileType.Trailer;
            }

            if (Regex.IsMatch(path, "SAMPLE"))
            {
                return MovieFileType.Sample;
            }

            return MovieFileType.Movie;
        }

        /// <summary>
        /// Get the movie name.
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        /// <param name="type">
        /// The AddFolderType type.
        /// </param>
        /// <returns>
        /// The movie name.
        /// </returns>
        public static string GetMovieName(string path, AddFolderType type)
        {
            if (path == string.Empty)
            {
                return string.Empty;
            }

            if (IsDVD(path))
            {
                return GetDvdName(path);
            }

            if (IsBluRay(path))
            {
                return GetBluRayName(path);
            }

            string movieName = string.Empty;

            if (type == AddFolderType.NameByFolder)
            {
                string[] split = Path.GetDirectoryName(path).Split('\\');
                movieName = split[split.Length - 1];
            }

            if (type == AddFolderType.NameByFile)
            {
                movieName = Path.GetFileNameWithoutExtension(path);
            }

            movieName = RemoveBrackets(movieName);

            var ignoreList = new List<string>();

            ignoreList.AddRange(Get.Keywords.Tags);
            ignoreList.AddRange(Get.Keywords.IgnoreNames);
            ignoreList.AddRange(Get.Keywords.Codecs);
            ignoreList.AddRange(Get.Keywords.Resolutions);
            ignoreList.AddRange(Get.Keywords.GetSourcesAsList());

            movieName = movieName.Replace('[', ' ');
            movieName = movieName.Replace(']', ' ');
            movieName = movieName.Replace('_', ' ');
            movieName = movieName.Replace('.', ' ');

            string input = movieName;

            foreach (string keyword in ignoreList)
            {
                if (input.ToLower().Contains(" " + keyword + "-"))
                {
                    Match hmatch = Regex.Match(input, keyword + "-(.*)", RegexOptions.IgnoreCase);
                    input = input.Replace(hmatch.Groups[0].Value, string.Empty);
                }

                input = Regex.Replace(input, " " + keyword + " ", " ", RegexOptions.IgnoreCase);

                if (input.EndsWith(" " + keyword, true, new CultureInfo("en-US")))
                {
                    input = input.Substring(0, input.Length - keyword.Length);
                }
            }

            movieName = input;
            movieName = Regex.Replace(movieName, @"\s{2,}", " ");

            if (movieName.Length >= 4)
            {
                if (movieName.Substring(0, 4) == GetMovieYear(movieName).ToString())
                {
                    string yearStore = movieName.Substring(0, 4);
                    string theRest = movieName.Substring(4);
                    string yearResult = GetMovieYear(theRest).ToString();
                    movieName = Regex.Replace(theRest, yearResult, " ");
                    movieName = yearStore + movieName;
                }
                else
                {
                    movieName = Regex.Replace(movieName, GetMovieYear(movieName).ToString(), string.Empty);
                    movieName = movieName.Replace("()", string.Empty);
                }
            }

            return movieName.Trim();
        }

        /// <summary>
        /// Get the movie year.
        /// </summary>
        /// <param name="text">
        /// The text string
        /// </param>
        /// <returns>
        /// The movie year
        /// </returns>
        public static int? GetMovieYear(string text)
        {
            Match match = Regex.Match(text, @"(18|19|20)\d\d", RegexOptions.IgnoreCase);

            string year = string.Empty;

            if (match.Success)
            {
                string yearExtracted = match.Groups[0].Value;

                if (text.StartsWith(yearExtracted))
                {
                    string fileNameAfter = text.Substring(4);
                    match = Regex.Match(fileNameAfter, @"(18|19|20)\d\d", RegexOptions.IgnoreCase);
                }

                year = match.Groups[0].Value;

                if (string.IsNullOrEmpty(year))
                {
                    return null;
                }

                if ((Convert.ToInt32(year) < 1878) || (Convert.ToInt32(year) > DateTime.Now.Year + 2))
                {
                    return null;
                }
            }

            int intYear;

            int.TryParse(year, out intYear);

            if (intYear == 0)
            {
                return null;
            }

            return intYear;
        }

        /// <summary>
        /// The disk part number.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The part number.
        /// </returns>
        public static int GetPartNumber(string fileName)
        {
            int result = 0;

            Match regexResult = Regex.Match(
                fileName, 
                @"(CD\d{1,10})|(CD\s\d{1,10})|(PART\d{1,10})|(PART\s\d{1,10})|(DISC\d{1,10})|(DISC\s\d{1,10})|(DISK\d{1,10})|(DISK\s\d{1,10})", 
                RegexOptions.IgnoreCase);

            if (regexResult.Success)
            {
                string val = regexResult.Value;

                string stringInt = YRegex.Match(@"(?<value>\d{1,3})", val, "value");
                int.TryParse(stringInt, out result);
            }

            return result;
        }

        /// <summary>
        /// get set name.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The set name.
        /// </returns>
        public static string GetSetName(string fileName)
        {
            Match regexResult = Regex.Match(fileName, @"\[set\s(?<name>.*?)\]", RegexOptions.IgnoreCase);

            string result = string.Empty;

            if (regexResult.Success)
            {
                result = regexResult.Groups["name"].Value;
            }

            return result;
        }

        /// <summary>
        /// Check if path is a bluray
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        /// <returns>
        /// Is bluray.
        /// </returns>
        public static bool IsBluRay(string path)
        {
            if (path == null)
            {
                return false;
            }

            var check = path.ToLower().Contains("bdmv") && path.ToLower().Contains("stream");

            return check;
        }

        /// <summary>
        /// Check if path is a dvd
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        /// <returns>
        /// Path is a dvd.
        /// </returns>
        public static bool IsDVD(string path)
        {
            if (path == null)
            {
                return false;
            }

            return path.ToLower().Contains("video_ts");
        }

        /// <summary>
        /// Remove brackets from a string
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The processed string
        /// </returns>
        public static string RemoveBrackets(string fileName)
        {
            return Regex.Replace(fileName, @"\[.*?\]", string.Empty, RegexOptions.IgnoreCase);
        }

        #endregion
    }
}