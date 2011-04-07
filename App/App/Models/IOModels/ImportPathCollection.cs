// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportPathCollection.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.IOModels
{
    using System;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The import path movie type.
    /// </summary>
    public enum ImportPathMovieType
    {
        /// <summary>
        /// The import using file names.
        /// </summary>
        ImportUsingFileNames,

        /// <summary>
        /// The import using folder name.
        /// </summary>
        ImportUsingFolderName
    }

    /// <summary>
    /// The import path collection.
    /// </summary>
    public class ImportPathCollection : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The import path movie type.
        /// </summary>
        private ImportPathMovieType importPathMovieType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ImportPath.
        /// </summary>
        public Uri ImportPath { get; set; }

        /// <summary>
        /// Gets or sets ImportPathMovieType.
        /// </summary>
        public ImportPathMovieType ImportPathMovieType
        {
            get
            {
                return this.importPathMovieType;
            }

            set
            {
                this.importPathMovieType = value;
                this.OnPropertyChanged("ImportPathMovieType");
            }
        }

        #endregion
    }
}