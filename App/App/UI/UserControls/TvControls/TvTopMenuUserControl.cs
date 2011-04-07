// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvTopMenuUserControl.cs" company="The YANFOE Project">
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
    using System.Windows.Forms;

    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Factories.InOut;
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.UI.Dialogs.TV;

    /// <summary>
    /// The tv top menu user control.
    /// </summary>
    public partial class TvTopMenuUserControl : XtraUserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The type.
        /// </summary>
        private SaveType type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TvTopMenuUserControl"/> class.
        /// </summary>
        public TvTopMenuUserControl()
        {
            this.InitializeComponent();

            TvDBFactory.CurrentEpisodeChanged += this.TvDBFactory_CurrentEpisodeChanged;

            this.SetBinding();

            this.btnSaveSelectedSeries.Visible = false;
            this.btnSaveSelectedSeason.Visible = false;
            this.btnSaveSelectedEpisode.Visible = false;

            switch (this.Type)
            {
                case SaveType.SaveSeries:
                    this.btnSaveSelectedSeries.Visible = true;
                    break;
                case SaveType.SaveSeason:
                    this.btnSaveSelectedSeason.Visible = true;
                    break;
                case SaveType.SaveEpisode:
                    this.btnSaveSelectedEpisode.Visible = true;
                    break;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Type.
        /// </summary>
        public SaveType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
                this.btnSaveSelectedSeries.Visible = false;
                this.btnSaveSelectedSeason.Visible = false;
                this.btnSaveSelectedEpisode.Visible = false;

                switch (this.Type)
                {
                    case SaveType.SaveSeries:
                        this.btnSaveSelectedSeries.Visible = true;
                        break;
                    case SaveType.SaveSeason:
                        this.btnSaveSelectedSeason.Visible = true;
                        break;
                    case SaveType.SaveEpisode:
                        this.btnSaveSelectedEpisode.Visible = true;
                        break;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The set binding.
        /// </summary>
        private void SetBinding()
        {
            this.btnAssignFileToEpisode.DataBindings.Clear();

            this.btnAssignFileToEpisode.DataBindings.Add(
                "Image", TvDBFactory.CurrentEpisode, "CurrentFilePathStatusImage");

            this.btnLock.DataBindings.Clear();

            switch (this.Type)
            {
                case SaveType.SaveSeries:
                    this.btnLock.DataBindings.Add("Image", TvDBFactory.CurrentSeries, "LockedImage");
                    break;
                case SaveType.SaveSeason:
                    this.btnLock.DataBindings.Add("Image", TvDBFactory.CurrentSeason, "LockedImage");
                    break;
                case SaveType.SaveEpisode:
                    this.btnLock.DataBindings.Add("Image", TvDBFactory.CurrentEpisode, "LockedImage");
                    break;
            }
        }

        /// <summary>
        /// Handles the CurrentEpisodeChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void TvDBFactory_CurrentEpisodeChanged(object sender, EventArgs e)
        {
            this.SetBinding();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnAllSaveEpisode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnAllSaveEpisode_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedEpisodeDetails();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnAllSaveSeason control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnAllSaveSeason_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeasonDetails();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnAllSaveSeries control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnAllSaveSeries_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails();
        }

        /// <summary>
        /// Handles the Click event of the btnAssignFileToEpisode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnAssignFileToEpisode_Click(object sender, EventArgs e)
        {
            TvDBFactory.UpdateEpisodeFile(ModifierKeys == Keys.Shift);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnEpisodeSaveNFo control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnEpisodeSaveNFo_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedEpisodeDetails(EpisodeIOType.Nfo);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnEpisodeSaveScreenshot control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnEpisodeSaveScreenshot_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedEpisodeDetails(EpisodeIOType.Screenshot);
        }

        /// <summary>
        /// Handles the Click event of the btnSaveAll control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveAll_Click(object sender, EventArgs e)
        {
            OutFactory.SaveRecursiveSelectedSeriesDetails();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveSeasonBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSeasonBanner_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeasonDetails(SeasonIOType.Banner);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveSeasonFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSeasonFanart_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeasonDetails(SeasonIOType.Fanart);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveSeasonPoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSeasonPoster_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeasonDetails(SeasonIOType.Poster);
        }

        /// <summary>
        /// Handles the Click event of the btnSaveSelectedEpisode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSelectedEpisode_Click(object sender, EventArgs e)
        {
            OutFactory.SaveAllSelectedEpisodeDetails();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveSelectedSeason control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSelectedSeason_Click(object sender, EventArgs e)
        {
            OutFactory.SaveAllSelectedSeasonDetails();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveSelectedSeries control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnSaveSelectedSeries_Click(object sender, EventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSeriesSaveBanner control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSeriesSaveBanner_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails(SeriesIOType.Banner);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSeriesSaveFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSeriesSaveFanart_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails(SeriesIOType.Fanart);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSeriesSaveNfo control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSeriesSaveNfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails(SeriesIOType.Nfo);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSeriesSavePoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void btnSeriesSavePoster_ItemClick(object sender, ItemClickEventArgs e)
        {
            OutFactory.SaveAllSelectedSeriesDetails(SeriesIOType.Poster);
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var update = new FrmUpdateShows(true);
            update.ShowDialog();
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnLock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            switch (this.Type)
            {
                case SaveType.SaveSeries:
                    TvDBFactory.CurrentSeries.IsLocked = !TvDBFactory.CurrentSeries.IsLocked;
                    break;
                case SaveType.SaveSeason:
                    TvDBFactory.CurrentSeason.IsLocked = !TvDBFactory.CurrentSeason.IsLocked;
                    break;
                case SaveType.SaveEpisode:
                    TvDBFactory.CurrentEpisode.IsLocked = !TvDBFactory.CurrentEpisode.IsLocked;
                    break;
            }
        }
    }

    /// <summary>
    /// The save type.
    /// </summary>
    public enum SaveType
    {
        /// <summary>
        /// The all.
        /// </summary>
        All, 

        /// <summary>
        /// The save series.
        /// </summary>
        SaveSeries, 

        /// <summary>
        /// The save season.
        /// </summary>
        SaveSeason, 

        /// <summary>
        /// The save episode.
        /// </summary>
        SaveEpisode
    }
}