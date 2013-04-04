// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="IMyDataErrorInfo.cs">
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
//   The MyDataErrorInfo interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Error
{
    /// <summary>
    /// The MyDataErrorInfo interface.
    /// </summary>
    internal interface IMyDataErrorInfo
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get error.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        void GetError(ErrorInfo info);

        /// <summary>
        /// The get property error.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="info">
        /// The info.
        /// </param>
        void GetPropertyError(string propertyName, ErrorInfo info);

        #endregion
    }
}