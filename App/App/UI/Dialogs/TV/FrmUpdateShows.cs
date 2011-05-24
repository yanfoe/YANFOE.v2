// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmUpdateShows.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.TV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Scrapers.TV;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The UpdateShows Form
    /// </summary>
    public partial class FrmUpdateShows : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        /// The bgw update.
        /// </summary>
        private readonly BackgroundWorker bgwUpdate = new BackgroundWorker();

        /// <summary>
        /// The bgw scan.
        /// </summary>
        private BackgroundWorker bgwScan = new BackgroundWorker();

        /// <summary>
        /// The update database.
        /// </summary>
        private List<UpdateTvRecords> updateDatabase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmUpdateShows"/> class.
        /// </summary>
        /// <param name="autoStart">
        /// if set to <c>true</c> [auto start].
        /// </param>
        public FrmUpdateShows(bool autoStart = false)
        {
            this.InitializeComponent();

            this.GenerateUpdateDatabase();
            this.SetBindings();

            if (autoStart)
            {
                this.DoFullScan();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Does the full scan.
        /// </summary>
        private void DoFullScan()
        {
            this.btnOk.Enabled = false;

            this.bgwScan = new BackgroundWorker
                {
                    WorkerReportsProgress = true, 
                    WorkerSupportsCancellation = true
                };

            this.bgwScan.DoWork += this.BgwScan_DoWork;
            this.bgwScan.ProgressChanged += this.BgwScan_ProgressChanged;
            this.bgwScan.RunWorkerCompleted += this.BgwScan_RunWorkerCompleted;

            this.bgwScan.RunWorkerAsync();
        }

        /// <summary>
        /// Generates the UpdateDatabase
        /// </summary>
        private void GenerateUpdateDatabase()
        {
            if (this.updateDatabase == null)
            {
                this.updateDatabase = new List<UpdateTvRecords>();
            }

            this.updateDatabase.Clear();
            this.updateDatabase = (from s in TvDBFactory.TvDatabase
                                   orderby s.Value.SeriesName
                                   select
                                       new UpdateTvRecords
                                           {
                                               SeriesName = s.Key, 
                                               SeriesId = s.Value.SeriesID, 
                                               PreviousTime = s.Value.Lastupdated
                                           }).ToList();
        }

        /// <summary>
        /// Setup the form bindings.
        /// </summary>
        private void SetBindings()
        {
            this.grdSeriesList.DataSource = this.updateDatabase;
        }

        /// <summary>
        /// Handles the DoWork event of the bgwScan control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private void BgwScan_DoWork(object sender, DoWorkEventArgs e)
        {
            var tvdb = new TheTvdb();
            int count = 0;

            foreach (UpdateTvRecords series in this.updateDatabase)
            {
                this.bgwScan.ReportProgress(0, series.SeriesName);

                Series seriesObj = TvDBFactory.GetSeriesFromName(series.SeriesName);

                Series newSeries = tvdb.CheckForUpdate(seriesObj.SeriesID, seriesObj.Language, seriesObj.Lastupdated);

                if (newSeries != null)
                {
                    series.NewTime = newSeries.Lastupdated;
                    count++;
                }
            }

            e.Result = count;
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgwScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void BgwScan_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.txtStatus.Text = string.Format("Updating: {0}", e.UserState);

            UpdateTvRecords row =
                (from r in this.updateDatabase where r.SeriesName == e.UserState.ToString() select r).SingleOrDefault();

            this.gridSeriesListView.RefreshData();

            int index = this.updateDatabase.IndexOf(row);

            this.gridSeriesListView.FocusedRowHandle = index;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.gridSeriesListView.RefreshData();
            this.btnOk.Enabled = true;
            this.txtStatus.Text = string.Format("Updating Complete. {0} updated series available.", (int)e.Result);
        }

        /// <summary>
        /// Handles the DoWork event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (UpdateTvRecords series in this.updateDatabase)
            {
                if (series.UpdateSeries)
                {
                    this.bgwUpdate.ReportProgress(0, series.SeriesName);
                    TvDBFactory.UpdateSeries(series.SeriesId);
                }
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.txtStatus.Text = string.Format("Updating {0}", e.UserState);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.bgwUpdate.DoWork += this.BgwUpdate_DoWork;
            this.bgwUpdate.ProgressChanged += this.BgwUpdate_ProgressChanged;
            this.bgwUpdate.RunWorkerCompleted += this.BgwUpdate_RunWorkerCompleted;

            this.bgwUpdate.WorkerReportsProgress = true;

            this.bgwUpdate.RunWorkerAsync();

            this.btnOk.Enabled = false;
            this.btnCancel.Enabled = false;
        }

        #endregion

        /// <summary>
        /// The update tv records.
        /// </summary>
        private class UpdateTvRecords : ModelBase
        {
            #region Constants and Fields

            /// <summary>
            /// The new time.
            /// </summary>
            private string newTime;

            #endregion

            #region Properties

            /// <summary>
            /// Gets or sets NewTime.
            /// </summary>
            public string NewTime
            {
                set
                {
                    this.newTime = value;

                    this.UpdatedOnServer = this.newTime != this.PreviousTime;
                    this.UpdateSeries = this.newTime != this.PreviousTime;
                }
            }

            /// <summary>
            /// Gets or sets PreviousTime.
            /// </summary>
            public string PreviousTime { private get; set; }

            /// <summary>
            /// Gets or sets SeriesId.
            /// </summary>
            public uint? SeriesId { get; set; }

            /// <summary>
            /// Gets or sets SeriesName.
            /// </summary>
            public string SeriesName { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether [update series].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [update series]; otherwise, <c>false</c>.
            /// </value>
            public bool UpdateSeries { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether UpdatedOnServer.
            /// </summary>
            public bool UpdatedOnServer { get; set; }

            #endregion
        }
    }
}