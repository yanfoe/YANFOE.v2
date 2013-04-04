// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="CheckButton.cs">
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
//   The check button.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.UI
{
    #region Required Namespaces

    using System.Windows.Controls;

    #endregion

    /// <summary>
    /// The check button.
    /// </summary>
    public class CheckButton : Button
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether checked.
        /// </summary>
        public bool Checked { get; set; }

        #endregion
    }
}