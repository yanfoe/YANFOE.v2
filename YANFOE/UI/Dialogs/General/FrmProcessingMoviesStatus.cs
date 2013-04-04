// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmProcessingMoviesStatus.cs" company="The YANFOE Project">
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

    public partial class FrmProcessingMoviesStatus : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmProcessingMoviesStatus"/> class.
        /// </summary>
        public FrmProcessingMoviesStatus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarControl1.Properties.Maximum = Factories.MovieDBFactory.ImportProgressMaximum;
            progressBarControl1.EditValue = Factories.MovieDBFactory.ImportProgressCurrent;
            lblStatus.Text = Factories.MovieDBFactory.ImportProgressStatus;
        }

        /// <summary>
        /// Handles the Shown event of the FrmProcessingMoviesStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrmProcessingMoviesStatus_Shown(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void FrmProcessingMoviesStatus_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.timer1.Stop();
        }
    }
}