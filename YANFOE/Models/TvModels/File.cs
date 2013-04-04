// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="File.cs">
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
//   The file path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels
{
    #region Required Namespaces

    using System;
    using System.IO;

    using Newtonsoft.Json;

    #endregion

    /// <summary>
    ///   The file path.
    /// </summary>
    [Serializable]
    public class FilePath
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="FilePath" /> class.
        /// </summary>
        public FilePath()
        {
            this.FileNameAndPath = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets Current FileName.
        /// </summary>
        [JsonIgnore]
        public string CurrentFileName
        {
            get
            {
                return Path.GetFileName(this.FileNameAndPath);
            }
        }

        /// <summary>
        ///   Gets CurrentFileNameWithoutExt.
        /// </summary>
        [JsonIgnore]
        public string CurrentFileNameWithoutExt
        {
            get
            {
                return Path.GetFileNameWithoutExtension(this.FileNameAndPath);
            }
        }

        /// <summary>
        ///   Gets CurrentPath.
        /// </summary>
        [JsonIgnore]
        public string CurrentPath
        {
            get
            {
                return string.IsNullOrEmpty(this.FileNameAndPath)
                           ? string.Empty
                           : this.FileNameAndPath.Replace(Path.GetFileName(this.FileNameAndPath), string.Empty);
            }
        }

        /// <summary>
        ///   Gets or sets FileNameAndPath.
        /// </summary>
        public string FileNameAndPath { get; set; }

        #endregion
    }
}