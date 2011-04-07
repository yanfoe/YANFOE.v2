// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RestructureAction.cs" company="The YANFOE Project">
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
    /// The restructure type.
    /// </summary>
    public enum RestructureType
    {
        /// <summary>
        /// Create a New Folder
        /// </summary>
        CreateFolder, 

        /// <summary>
        /// Copy a File
        /// </summary>
        Copy, 

        /// <summary>
        /// Move a File
        /// </summary>
        Move, 

        /// <summary>
        /// Delete a File.
        /// </summary>
        DeleteFile, 
    }

    /// <summary>
    /// The restructure action.
    /// </summary>
    [Serializable]
    public class RestructureAction
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RestructureAction"/> class.
        /// </summary>
        public RestructureAction()
        {
            this.From = string.Empty;

            this.To = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ActionType.
        /// </summary>
        public RestructureType ActionType { get; set; }

        /// <summary>
        /// Gets or sets From.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets To.
        /// </summary>
        public string To { get; set; }

        #endregion
    }
}