// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ScanType.cs">
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
//   The video scan type
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Enums
{
    /// <summary>
    ///   The video scan type
    /// </summary>
    public enum ScanType
    {
        /// <summary>
        ///   Choose no scan type.
        /// </summary>
        None, 

        /// <summary>
        ///   Choose a scan type of progressive.
        /// </summary>
        Progressive, 

        /// <summary>
        ///   Choose a scan type of interlaced.
        /// </summary>
        Interlaced
    }
}