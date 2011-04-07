// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmSavingDialog.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.General
{
    using System;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.InOut;

    /// <summary>
    /// The frm saving dialog.
    /// </summary>
    public partial class FrmSavingDialog : XtraForm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmSavingDialog"/> class.
        /// </summary>
        public FrmSavingDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets EpisodeMax.
        /// </summary>
        public int EpisodeMax
        {
            get
            {
                return this.progressBarEpisode.Properties.Maximum;
            }

            set
            {
                this.progressBarEpisode.Properties.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the season max.
        /// </summary>
        /// <value>
        /// The season max.
        /// </value>
        public int SeasonMax
        {
            get
            {
                return this.progressBarSeason.Properties.Maximum;
            }

            set
            {
                this.progressBarSeason.Properties.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the series max.
        /// </summary>
        /// <value>
        /// The series max.
        /// </value>
        public int SeriesMax
        {
            get
            {
                return this.progressBarSeries.Properties.Maximum;
            }

            set
            {
                this.progressBarSeries.Properties.Maximum = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Click event of the BtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            OutFactory.Cancel = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.progressBarSeries.EditValue = OutFactory.ProgressModel.SeriesCurrent;
            this.progressBarSeason.EditValue = OutFactory.ProgressModel.SeasonCurrent;
            this.progressBarEpisode.EditValue = OutFactory.ProgressModel.EpisodeCurrent;

            this.lblSeries.Text = OutFactory.ProgressModel.SeriesText;
            this.lblSeason.Text = OutFactory.ProgressModel.SeasonText;
            this.lblEpisode.Text = OutFactory.ProgressModel.EpisodeText;
        }

        #endregion
    }
}