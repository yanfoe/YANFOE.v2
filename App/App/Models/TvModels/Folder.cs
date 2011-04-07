// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Folder.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.TvModels
{
    using System;

    /// <summary>
    /// The add folder type.
    /// </summary>
    public enum AddFolderType
    {
        /// <summary>
        /// Name by folder.
        /// </summary>
        NameByFolder = 0, 

        /// <summary>
        /// Name by filename.
        /// </summary>
        NameByFilename = 1, 

        /// <summary>
        /// Exclude from search.
        /// </summary>
        Exclude = 2
    }

    /// <summary>
    /// The folder.
    /// </summary>
    [Serializable]
    public class Folder
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Folder"/> class.
        /// </summary>
        public Folder()
        {
            this.FolderPath = string.Empty;

            this.Scrapergroup = string.Empty;

            this.DefaultSource = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets AddType.
        /// </summary>
        public AddFolderType AddType { get; set; }

        /// <summary>
        /// Gets or sets DefaultSource.
        /// </summary>
        public string DefaultSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether DetectFirstMovieOnly.
        /// </summary>
        public bool DetectFirstMovieOnly { get; set; }

        /// <summary>
        /// Gets or sets FolderPath.
        /// </summary>
        public string FolderPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MakeChanges.
        /// </summary>
        public bool MakeChanges { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether RecursiveScan.
        /// </summary>
        public bool RecursiveScan { get; set; }

        /// <summary>
        /// Gets or sets Scrapergroup.
        /// </summary>
        public string Scrapergroup { get; set; }

        #endregion
    }
}