// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Folders.cs" company="The YANFOE Project">
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

    using YANFOE.Tools.ThirdParty;

    /// <summary>
    /// The folders.
    /// </summary>
    public static class Folders
    {
        #region Public Methods

        /// <summary>
        /// The directory check exists.
        /// </summary>
        /// <param name="path">
        /// The file path.
        /// </param>
        /// <param name="create">
        /// Create path if not found
        /// </param>
        public static void CheckExists(string path, bool create)
        {
            if (path == null)
            {
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }

        /// <summary>
        /// Removes all files in folder.
        /// </summary>
        /// <param name="path">
        /// The path of which to remove all files
        /// </param>
        public static void RemoveAllFilesInFolder(string path)
        {
            string[] files = FileHelper.GetFilesRecursive(path).ToArray();

            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        #endregion
    }
}