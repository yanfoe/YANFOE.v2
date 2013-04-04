// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemCharChange.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Restructure
{
    using System;

    /// <summary>
    /// The file system char change.
    /// </summary>
    public static class FileSystemCharChange
    {
        #region Public Methods

        public enum ConvertArea
        {
           None,
           Movie,
           Tv
        }
        
        public enum ConvertType
        {
            None,
            Hex,
            Char
        }

        private static string[,] replaceValues;

        static FileSystemCharChange()
        {
            replaceValues = new[,]
               {
                   { ":", "$3A" }, 
                   { "\"", "$22" }, 
                   { "\\", "$5C" }, 
                   { "<", "$3C" }, 
                   { "/", "$2F" }, 
                   { ">", "$3E" },
                   { "|", "$7C" }, 
                   { "*", "$2A" }, 
                   { "?", "$3F" },
               };
        }

        /// <summary>
        /// File System Char Change - Convert From
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Processed string
        /// </returns>
        public static string From(string value, ConvertArea area)
        {
            return ReplaceByColumn(value, area, 1, 0);
        }

        /// <summary>
        /// File System Char Change - Convert Too
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Processed string
        /// </returns>
        public static string To(string value, ConvertArea area, ConvertType type = ConvertType.None)
        {
            return ReplaceByColumn(value, area, 0, 1);
        }

        public static string ReplaceByColumn(string value, ConvertArea area, int column1, int column2, ConvertType convertType = ConvertType.None)
        {
            if (convertType == ConvertType.None)
            {
                convertType = GetConvertType(area);
            }

            for (var i = 0; i < (replaceValues.Length / 2) - 1; i++)
            {
                if (convertType == ConvertType.Hex)
                {
                    value = value.Replace(replaceValues[i, column1], replaceValues[i, column2]);
                }
                else
                {
                    var convertValue = GetConvertValue(area);

                    value = value.Replace(replaceValues[i, column1], convertValue);
                }
            }

            return value;
        }

        private static string GetConvertValue(ConvertArea area)
        {
            if (area == ConvertArea.Movie)
            {
                return Settings.Get.InOutCollection.MovieIOReplaceChar;
            }

            return Settings.Get.InOutCollection.TvIOReplaceChar;
        }

        private static ConvertType GetConvertType(ConvertArea area)
        {
            if (area == ConvertArea.Movie)
            {
                if (Settings.Get.InOutCollection.MovieIOReplaceWithChar)
                {
                    return ConvertType.Char;
                }

                if (Settings.Get.InOutCollection.MovieIOReplaceWithHex)
                {
                    return ConvertType.Hex;
                }

                throw new Exception();
            }

            if (area == ConvertArea.Tv)
            {
                if (Settings.Get.InOutCollection.TvIOReplaceWithChar)
                {
                    return ConvertType.Char;
                }

                if (Settings.Get.InOutCollection.TvIOReplaceWithHex)
                {
                    return ConvertType.Hex;
                }

                throw new Exception();
            }

            throw new Exception();
        }

        #endregion
    }
}