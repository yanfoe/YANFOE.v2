// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ErrorInfo.cs">
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
//   The error type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Error
{
    /// <summary>
    /// The error type.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// The critical.
        /// </summary>
        Critical, 

        /// <summary>
        /// The default.
        /// </summary>
        Default, 

        /// <summary>
        /// The information.
        /// </summary>
        Information, 

        /// <summary>
        /// The none.
        /// </summary>
        None, 

        /// <summary>
        /// The warning.
        /// </summary>
        Warning, 

        /// <summary>
        /// The user 1.
        /// </summary>
        User1, 

        /// <summary>
        /// The user 2.
        /// </summary>
        User2, 

        /// <summary>
        /// The user 3.
        /// </summary>
        User3, 

        /// <summary>
        /// The user 4.
        /// </summary>
        User4, 

        /// <summary>
        /// The user 5.
        /// </summary>
        User5, 

        /// <summary>
        /// The user 6.
        /// </summary>
        User6, 

        /// <summary>
        /// The user 7.
        /// </summary>
        User7, 

        /// <summary>
        /// The user 8.
        /// </summary>
        User8, 

        /// <summary>
        /// The user 9.
        /// </summary>
        User9
    }

    /// <summary>
    /// The error info.
    /// </summary>
    public class ErrorInfo
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the error text.
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// Gets or sets the error type.
        /// </summary>
        public ErrorType ErrorType { get; set; }

        #endregion
    }
}