// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="LookAndFeel.cs">
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
//   The look and feel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Drawing;

    #endregion

    /// <summary>
    ///   The look and feel.
    /// </summary>
    [Serializable]
    public class LookAndFeel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="LookAndFeel" /> class.
        /// </summary>
        public LookAndFeel()
        {
            this.TextChanged = new Font("Tahoma", (float)8.25, FontStyle.Bold);
            this.TextNormal = new Font("Tahoma", (float)8.25, FontStyle.Regular);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the text changed.
        /// </summary>
        /// <value> The text changed. </value>
        public Font TextChanged { get; set; }

        /// <summary>
        ///   Gets or sets the text normal.
        /// </summary>
        /// <value> The text normal. </value>
        public Font TextNormal { get; set; }

        #endregion
    }
}