// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Info.cs">
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
//   Info tools
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.IO
{
    #region Required Namespaces

    using System;
    using System.IO;
    using System.Linq;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Tools.ThirdParty;

    #endregion

    /// <summary>
    ///   Info tools
    /// </summary>
    public static class Info
    {
        #region Public Methods and Operators

        /// <summary>
        /// Returns the total file size of a folder.
        /// </summary>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="pattern">
        /// The pattern. 
        /// </param>
        /// <returns>
        /// The get directory size. 
        /// </returns>
        public static long GetDirectorySize(string path, string pattern = "*.*")
        {
            const string LogCatagory = "IO > Info > GetDirectorySize";

            try
            {
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    return 0;
                }

                path = path.Replace(Path.GetFileName(path), string.Empty);
                string[] a = FileHelper.GetFilesRecursive(path, pattern).ToArray();
                return a.Select(name => new FileInfo(name)).Select(info => info.Length).Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, LoggerName.GeneralLog, LogCatagory, ex.Message);
            }

            return 0;
        }

        #endregion
    }
}