// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileNaming.cs" company="The YANFOE Project">
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
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The file naming.
    /// </summary>
    public static class FileNaming
    {
        #region Public Methods

        /// <summary>
        /// Remove illegal chars.
        /// </summary>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// Processed string
        /// </returns>
        public static string RemoveIllegalChars(string filename)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            return invalid.Aggregate(filename, (current, c) => current.Replace(c.ToString(), String.Empty));
        }

        #endregion
    }
}