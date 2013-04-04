// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileLookup.cs">
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
//   The file helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.ThirdParty
{
    #region Required Namespaces

    using System.Collections.Generic;
    using System.IO;

    #endregion

    /// <summary>
    /// The file helper.
    /// </summary>
    internal static class FileHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get files recursive.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="pattern">
        /// The pattern.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetFilesRecursive(string b, string pattern = "*.*")
        {
            // 1.
            // Store results in the file results list.
            var result = new List<string>();

            // 2.
            // Store a stack of our directories.
            var stack = new Stack<string>();

            // 3.
            // Add initial directory.
            stack.Push(b);

            // 4.
            // Continue while there are directories to process
            while (stack.Count > 0)
            {
                // A.
                // Get top directory
                string dir = stack.Pop();

                try
                {
                    // B
                    // Add all files at this directory to the result List.
                    result.AddRange(Directory.GetFiles(dir, pattern));

                    // C
                    // Add all directories at this directory.
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                }
                catch
                {
                    // D
                    // Could not open the directory
                }
            }

            return result;
        }

        #endregion
    }
}