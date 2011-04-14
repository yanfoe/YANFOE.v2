// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UiSettings.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.UserSettings
{
    using System;

    /// <summary>
    /// UI settings
    /// </summary>
    [Serializable]
    public class UiSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UiSettings"/> class.
        /// </summary>
        public UiSettings()
        {
            this.Skin = "Foggy";
            this.EnableTVPathColumn = true;
        }

        /// <summary>
        /// Gets or sets Skin.
        /// </summary>
        public string Skin { get; set; }

        /// <summary>
        /// Gets or sets EnableTVPathColumn.
        /// </summary>
        public bool EnableTVPathColumn { get; set; }

        public bool HideSeasonZero { get; set; }
    }
}
