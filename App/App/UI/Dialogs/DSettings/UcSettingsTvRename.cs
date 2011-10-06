// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YRegex.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.DSettings
{
    using System;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Renamer;
    using YANFOE.Settings;

    /// <summary>
    /// The uc settings tv rename.
    /// </summary>
    public partial class UcSettingsTvRename : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsTvRename"/> class.
        /// </summary>
        public UcSettingsTvRename()
        {
            this.InitializeComponent();

            this.txtRenameTemplate.Text = Get.InOutCollection.EpisodeNamingTemplate;

            this.txtSeriesNameTemplate.Text = TvRenamerFactory.SeriesNameTemplate;
            this.txtEpisodeName.Text = TvRenamerFactory.EpisodeNameTemplate;
            this.txtSeasonNumber1Template.Text = TvRenamerFactory.SeasonNumber1Template;
            this.txtSeasonNumber2Template.Text = TvRenamerFactory.SeasonNumber2Template;
            this.txtEpisodeNumber1Template.Text = TvRenamerFactory.EpisodeNumber1Template;
            this.txtEpisodeNumber2Template.Text = TvRenamerFactory.EpisodeNumber2Template;

            this.chkReplaceWithChar.DataBindings.Add("Checked", Get.InOutCollection, "TvIOReplaceWithChar");
            this.chkReplaceWithHex.DataBindings.Add("Checked", Get.InOutCollection, "TvIOReplaceWithHex");
            this.txtReplaceCharWith.DataBindings.Add("Text", Get.InOutCollection, "TvIOReplaceChar");

            this.chkEnableRename.DataBindings.Add("Checked", Get.InOutCollection, "RenameTV");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update preview.
        /// </summary>
        private void UpdatePreview()
        {
            this.txtPreview.Text = TvRenamerFactory.RenameEpisode(null);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtRenameTemplate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TxtRenameTemplate_TextChanged(object sender, EventArgs e)
        {
            Get.InOutCollection.EpisodeNamingTemplate = this.txtRenameTemplate.Text;
            this.UpdatePreview();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkReplaceWithChar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkReplaceWithChar_CheckedChanged(object sender, EventArgs e)
        {
            txtReplaceCharWith.Enabled = chkReplaceWithChar.Checked;
        }

        #endregion


    }
}