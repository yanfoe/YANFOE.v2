// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="LogType.cs">
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
//   Log save type
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.Logs.Enums
{
    /// <summary>
    ///   Log save type
    /// </summary>
    public enum LogType
    {
        /// <summary>
        ///   Store log internally
        /// </summary>
        Internal, 

        /// <summary>
        ///   Save log to file
        /// </summary>
        File
    }
}