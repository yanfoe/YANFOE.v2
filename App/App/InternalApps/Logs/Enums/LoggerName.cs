// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggerName.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.Logs.Enums
{
    /// <summary>
    /// An enumeration of logs.
    /// </summary>
    public enum LoggerName
    {
        /// <summary>
        /// The main YANFOE log.
        /// </summary>
        GeneralLog = 0,

        /// <summary>
        /// Downloader 1 thread log
        /// </summary>
        Downloader1 = 1,

        /// <summary>
        /// Downloader 2 thread log
        /// </summary>
        Downloader2 = 2,

        /// <summary>
        /// Downloader 3 thread log
        /// </summary>
        Downloader3 = 3,

        /// <summary>
        /// Downloader 4 thread log
        /// </summary>
        Downloader4 = 4
    }
}
