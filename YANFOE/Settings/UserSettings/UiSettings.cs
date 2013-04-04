// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="UiSettings.cs">
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
//   UI settings
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Drawing;

    #endregion

    /// <summary>
    ///   UI settings
    /// </summary>
    [Serializable]
    public class UiSettings
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="UiSettings" /> class.
        /// </summary>
        public UiSettings()
        {
            this.ShowWelcomeMessage = true;
            this.Skin = "Foggy";
            this.EnableTVPathColumn = false;
            this.HideSeasonZero = false;

            this.PictureThumbnailFanart = new Size(100, 60);

            this.PictureThumbnailBanner = new Size(100, 30);

            this.PictureThumbnailPoster = new Size(100, 160);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets EnableTVPathColumn.
        /// </summary>
        public bool EnableTVPathColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether hide season zero.
        /// </summary>
        public bool HideSeasonZero { get; set; }

        /// <summary>
        /// Gets or sets the picture thumbnail banner.
        /// </summary>
        public Size PictureThumbnailBanner { get; set; }

        /// <summary>
        /// Gets or sets the picture thumbnail fanart.
        /// </summary>
        public Size PictureThumbnailFanart { get; set; }

        /// <summary>
        /// Gets or sets the picture thumbnail poster.
        /// </summary>
        public Size PictureThumbnailPoster { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show welcome message.
        /// </summary>
        public bool ShowWelcomeMessage { get; set; }

        /// <summary>
        ///   Gets or sets Skin.
        /// </summary>
        public string Skin { get; set; }

        #endregion
    }
}