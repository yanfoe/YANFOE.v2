// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Application.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.ConstSettings
{
    /// <summary>
    /// Application specific settings.
    /// </summary>
    public static class Application
    {
        #region ApplicationNamingConditions
        /// <summary>
        /// The string "YANFOE"
        /// </summary>
        public const string ApplicationName = "YANFOE";

        /// <summary>
        /// The current version of YANFOE.
        /// </summary>
        public const string ApplicationVersion = "2.0 Alpha 2";

        /// <summary>
        /// The current version of YANFOE.
        /// </summary>
        public const string ApplicationBuild = "110412";

        /// <summary>
        /// Type (AKA Donor)
        /// </summary>
        public const string ApplicationMessage = "Early Alpha";
        #endregion

        #region ApiKeys
        /// <summary>
        /// The API key for TheMovieDB.
        /// </summary>
        public const string TheMovieDBApi = "8b86f88ebee1b9257e58357076e1816f";

        /// <summary>
        /// The API Key for TVDb.
        /// </summary>
        public const string TvdbApi = "71F9C54F38B71B4F";
        #endregion
    }
}
