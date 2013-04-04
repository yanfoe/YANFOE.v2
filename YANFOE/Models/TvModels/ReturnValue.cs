// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ReturnValue.cs">
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
//   The return value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The return value.
    /// </summary>
    [Serializable]
    public class ReturnValue
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets Value.
        /// </summary>
        public string Value { get; set; }

        #endregion
    }
}