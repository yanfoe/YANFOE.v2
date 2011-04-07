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
    /// <summary>
    /// The file system char change.
    /// </summary>
    public static class FileSystemCharChange
    {
        #region Public Methods

        /// <summary>
        /// File System Char Change - Convert From
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// Processed string
        /// </returns>
        public static string From(string value)
        {
            value = value.Replace("$3A", ":");
            value = value.Replace("$22", "\"");
            value = value.Replace("$3C", "<");
            value = value.Replace("$3E", ">");
            value = value.Replace("$7C", "|");
            value = value.Replace("$2A", "*");
            value = value.Replace("$3F", "?");

            return value;
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
        public static string To(string value)
        {
            value = value.Replace(":", "$3A");
            value = value.Replace("\"", "$22");
            value = value.Replace("<", "$3C");
            value = value.Replace(">", "$3E");
            value = value.Replace("|", "$7C");
            value = value.Replace("*", "$2A");
            value = value.Replace("?", "$3F");

            return value;
        }

        #endregion
    }
}