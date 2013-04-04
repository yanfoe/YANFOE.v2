// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileSystemCharChange.cs">
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
//   The file system char change.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Restructure
{
    #region Required Namespaces

    using System;

    using YANFOE.Settings;

    #endregion

    /// <summary>
    ///   The file system char change.
    /// </summary>
    public static class FileSystemCharChange
    {
        #region Static Fields

        /// <summary>
        /// The replace values.
        /// </summary>
        private static readonly string[,] replaceValues;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="FileSystemCharChange"/> class.
        /// </summary>
        static FileSystemCharChange()
        {
            replaceValues = new[,]
                {
                    {
                       ":", "$3A" 
                    }, {
                          "\"", "$22" 
                       }, {
                             "\\", "$5C" 
                          }, {
                                "<", "$3C" 
                             }, {
                                   "/", "$2F" 
                                }, {
                                      ">", "$3E" 
                                   }, 
                    {
                       "|", "$7C" 
                    }, {
                          "*", "$2A" 
                       }, {
                             "?", "$3F" 
                          }, 
                };
        }

        #endregion

        #region Enums

        /// <summary>
        /// The convert area.
        /// </summary>
        public enum ConvertArea
        {
            /// <summary>
            /// The none.
            /// </summary>
            None, 

            /// <summary>
            /// The movie.
            /// </summary>
            Movie, 

            /// <summary>
            /// The tv.
            /// </summary>
            Tv
        }

        /// <summary>
        /// The convert type.
        /// </summary>
        public enum ConvertType
        {
            /// <summary>
            /// The none.
            /// </summary>
            None, 

            /// <summary>
            /// The hex.
            /// </summary>
            Hex, 

            /// <summary>
            /// The char.
            /// </summary>
            Char
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// File System Char Change - Convert From
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <returns>
        /// Processed string 
        /// </returns>
        public static string From(string value, ConvertArea area)
        {
            return ReplaceByColumn(value, area, 1, 0);
        }

        /// <summary>
        /// The replace by column.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <param name="column1">
        /// The column 1.
        /// </param>
        /// <param name="column2">
        /// The column 2.
        /// </param>
        /// <param name="convertType">
        /// The convert type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ReplaceByColumn(
            string value, ConvertArea area, int column1, int column2, ConvertType convertType = ConvertType.None)
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

        /// <summary>
        /// File System Char Change - Convert Too
        /// </summary>
        /// <param name="value">
        /// The value. 
        /// </param>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// Processed string 
        /// </returns>
        public static string To(string value, ConvertArea area, ConvertType type = ConvertType.None)
        {
            return ReplaceByColumn(value, area, 0, 1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get convert type.
        /// </summary>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <returns>
        /// The <see cref="ConvertType"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private static ConvertType GetConvertType(ConvertArea area)
        {
            if (area == ConvertArea.Movie)
            {
                if (Get.InOutCollection.MovieIOReplaceWithChar)
                {
                    return ConvertType.Char;
                }

                if (Get.InOutCollection.MovieIOReplaceWithHex)
                {
                    return ConvertType.Hex;
                }

                throw new Exception();
            }

            if (area == ConvertArea.Tv)
            {
                if (Get.InOutCollection.TvIOReplaceWithChar)
                {
                    return ConvertType.Char;
                }

                if (Get.InOutCollection.TvIOReplaceWithHex)
                {
                    return ConvertType.Hex;
                }

                throw new Exception();
            }

            throw new Exception();
        }

        /// <summary>
        /// The get convert value.
        /// </summary>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetConvertValue(ConvertArea area)
        {
            if (area == ConvertArea.Movie)
            {
                return Get.InOutCollection.MovieIOReplaceChar;
            }

            return Get.InOutCollection.TvIOReplaceChar;
        }

        #endregion
    }
}