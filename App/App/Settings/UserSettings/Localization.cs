// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Localization.cs" company="The YANFOE Project">
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
    /// Localization related settings.
    /// </summary>
    [Serializable]
    public class Localization
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Localization"/> class.
        /// </summary>
        public Localization()
        {
            this.ContructDefaultValues();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current application language.
        /// </summary>
        /// <value>The app lang.</value>
        public string AppLang { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Contructs the default values.
        /// </summary>
        private void ContructDefaultValues()
        {
            this.AppLang = "en";
        }

        #endregion
    }
}