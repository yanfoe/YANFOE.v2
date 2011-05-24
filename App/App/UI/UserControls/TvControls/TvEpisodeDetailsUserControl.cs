// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvEpisodeDetailsUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.TvControls
{
    using System;

    using YANFOE.Factories;

    /// <summary>
    /// TvEpisodeDetailsUserControl user control
    /// </summary>
    public partial class TvEpisodeDetailsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvEpisodeDetailsUserControl"/> class.
        /// </summary>
        public TvEpisodeDetailsUserControl()
        {
            InitializeComponent();

            tvTopMenuUserControl1.Type = SaveType.SaveEpisode;

            this.RefreshBindings();

            TvDBFactory.CurrentEpisodeChanged += this.TvDBFactory_CurrentEpisodeChanged;
        }

        /// <summary>
        /// Handles the CurrentEpisodeChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_CurrentEpisodeChanged(object sender, EventArgs e)
        {
            this.RefreshBindings();
        }

        /// <summary>
        /// Refreshes the form bindings.
        /// </summary>
        private void RefreshBindings()
        {
            layoutControlGroupDetails.DataBindings.Clear();
            layoutControlGroupDetails.DataBindings.Add("Enabled", TvDBFactory.CurrentEpisode, "NotLocked");

            layoutControlGroupGuestStars.DataBindings.Clear();
            layoutControlGroupGuestStars.DataBindings.Add("Enabled", TvDBFactory.CurrentEpisode, "NotLocked");

            txtTitle.DataBindings.Clear();
            txtTitle.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "EpisodeName");

            txtEpisodeNumber.DataBindings.Clear();
            txtEpisodeNumber.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "EpisodeNumber");

            txtAired.DataBindings.Clear();
            txtAired.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "FirstAired");

            txtPlot.DataBindings.Clear();
            txtPlot.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "Overview");

            txtDirector.DataBindings.Clear();
            txtDirector.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "DirectorAsString");

            txtWriter.DataBindings.Clear();
            txtWriter.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "WritersAsString");

            txtImdb.DataBindings.Clear();
            txtImdb.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "IMDBID");

            txtRating.DataBindings.Clear();
            txtRating.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "Rating");

            txtProductionCode.DataBindings.Clear();
            txtProductionCode.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "ProductionCode");

            txtLanguage.DataBindings.Clear();
            txtLanguage.DataBindings.Add("Text", TvDBFactory.CurrentEpisode, "Language");

            layoutControl1.DataBindings.Clear();
            layoutControl1.DataBindings.Add("Enabled", TvDBFactory.CurrentEpisode, "NotSecondary");

            grdActors.DataSource = TvDBFactory.CurrentEpisode.GuestStars;
        }
    }
}
