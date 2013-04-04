// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Delete.cs">
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
//   Delete operations
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.IO
{
    #region Required Namespaces

    using System;
    using System.IO;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Tools.ThirdParty;

    #endregion

    /// <summary>
    ///   Delete operations
    /// </summary>
    public class Delete
    {
        #region Public Methods and Operators

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="target_dir">
        /// The target_dir. 
        /// </param>
        public static void DeleteDirectory(string target_dir)
        {
            try
            {
                DeleteFilesInFolder(target_dir);

                Directory.Delete(target_dir, false);
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Warning, 0, "Could not remove " + target_dir, ex.Message);
            }
        }

        /// <summary>
        /// The delete files in folder.
        /// </summary>
        /// <param name="target_dir">
        /// The target_dir.
        /// </param>
        public static void DeleteFilesInFolder(string target_dir)
        {
            var files = FileHelper.GetFilesRecursive(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }
        }

        #endregion
    }
}