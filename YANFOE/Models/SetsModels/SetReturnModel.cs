// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetReturnModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.SetsModels
{
    /// <summary>
    /// Used to return data to a IO handler
    /// </summary>
    public class SetReturnModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the set orderorder.
        /// </summary>
        /// <value>
        /// The index of the movie in the set
        /// </value>
        public int? Order { get; set; }

        /// <summary>
        /// Gets or sets the name of the set.
        /// </summary>
        /// <value>
        /// The name of the set.
        /// </value>
        public string SetName { get; set; }

        #endregion
    }
}