// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmImportTv.cs" company="The YANFOE Project">
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

    using BitFactory.Logging;

    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Factories.Import;
    using YANFOE.Factories.Internal;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Models.TvModels.TVDB;
    using YANFOE.Scrapers.TV;

    public partial class FrmImportTv : XtraForm
    {
        private BackgroundWorker bgw;

        private TheTvdb theTvdb;

        private int count;

        private string currentStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmImportTv"/> class.
        /// </summary>
        public FrmImportTv()
        {
            InitializeComponent();

            this.bgw = new BackgroundWorker();
            grdSeriesList.DataSource = ImportTvFactory.SeriesNameList;
            ImportTvFactory.DoImportScan();

            this.ButNextClick(null, null);
        }

        private void ButNextClick(object sender, EventArgs e)
        {
            grdSeriesList.DataSource = ImportTvFactory.SeriesNameList;

            gridView1.BeginSort();
            gridView1.Columns["SeriesName"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            gridView1.EndSort();

            this.count = 0;

            this.bgw = new BackgroundWorker();
            
            this.bgw.DoWork += this.Bgw_DoWork;
            this.bgw.ProgressChanged += this.Bgw_ProgressChanged;
            this.bgw.WorkerReportsProgress = true;
            this.bgw.WorkerSupportsCancellation = true;
            this.bgw.RunWorkerCompleted += this.Bgw_RunWorkerCompleted;

            progressBar.Properties.Minimum = 0;
            progressBar.Properties.Maximum = ImportTvFactory.Scan.Count;
            progressBar.Position = 0;

            this.bgw.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the Bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.theTvdb = new TheTvdb();

            var toAdd = new Dictionary<string, ScanSeries>();
            var toRemove = new List<string>();

            Factories.UI.Windows7UIFactory.StartProgressState(ImportTvFactory.Scan.Count);

            var count = 0;

            foreach (var s in ImportTvFactory.Scan)
            {
                Factories.UI.Windows7UIFactory.SetProgressValue(count);

                if (
                    (from series in ImportTvFactory.SeriesNameList
                     where series.SeriesName == s.Key && series.WaitingForScan
                     select series).SingleOrDefault() != null)
                {
                    if (!string.IsNullOrEmpty(s.Key))
                    {
                        this.currentStatus = "Processing " + s.Key;

                        var searchResults = this.theTvdb.SeriesSearch(s.Key); // open initial object and do search

                        Series series;

                        if (searchResults.Count > 1 || searchResults.Count == 0)
                        {
                           var scan =
                                (from scanSeriesPick in ImportTvFactory.ScanSeriesPicks
                                 where scanSeriesPick.SearchString == s.Key
                                 select scanSeriesPick).SingleOrDefault();

                            if (scan != null)
                            {
                                searchResults = this.theTvdb.SeriesSearch(scan.SeriesName);

                                var result = (from r in searchResults where r.SeriesID == scan.SeriesID select r).Single();
                                series = this.theTvdb.OpenNewSeries(result);
                            }
                            else
                            {
                            
                                var resultCollection = new List<object>(4)
                                    { 
                                        searchResults, 
                                        s.Key, 
                                        s.Value, 
                                        toAdd, 
                                        toRemove 
                                    };

                                e.Result = resultCollection;

                                return;
                            }
                        }
                        else
                        {
                            series = this.theTvdb.OpenNewSeries(searchResults[0]); // download series details
                        }

                        this.Set(series, toRemove, toAdd, s.Key, s.Value);
                    }
                }

                this.count++;
                this.bgw.ReportProgress(this.count);

                if (this.bgw.CancellationPending)
                {
                    return;
                }

                count++;
            }

            foreach (var s in toRemove)
            {
                ImportTvFactory.Scan.Remove(s);
            }

            foreach (var s in toAdd)
            {
                ImportTvFactory.Scan.Add(s.Key, s.Value);
            }

            this.theTvdb.ApplyScan();

            Factories.UI.Windows7UIFactory.StopProgressState();
        }

        private void Set(Series series, List<string> toRemove, Dictionary<string, ScanSeries> toAdd, string key, ScanSeries value)
        {
            try
            {
                if (!TvDBFactory.TvDatabase.ContainsKey(series.SeriesName))
                {
                    TvDBFactory.TvDatabase.Add(series.SeriesName, series); // add series to db
                }

                var m = (from show in ImportTvFactory.SeriesNameList where show.SeriesName == key select show).Single();

                m.WaitingForScan = false;
                m.ScanComplete = true;

                var changedValue = value;
                toRemove.Add(key);

                if (!string.IsNullOrEmpty(series.SeriesName) && !toAdd.ContainsKey(series.SeriesName))
                {
                    toAdd.Add(series.SeriesName, changedValue);
                }
                else if (toAdd.ContainsKey(series.SeriesName))
                {
                    foreach (var season in changedValue.Seasons)
                    {
                        if (!toAdd[series.SeriesName].Seasons.ContainsKey(season.Key))
                        {
                            toAdd[series.SeriesName].Seasons.Add(season.Key, season.Value);
                        }
                        else
                        {
                            foreach (var episode in season.Value.Episodes)
                            {
                                toAdd[series.SeriesName].Seasons[season.Key].Episodes.Add(episode.Key, episode.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Log.WriteToLog(LogSeverity.Error, 0, "FrmImportTv > Set", exception.Message);
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the Bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Position = this.count;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the Bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                var resultObject = e.Result as List<object>;

                var s0 = resultObject[0] as List<SearchDetails>;
                var s1 = resultObject[1] as string;
                var s2 = resultObject[2] as ScanSeries;
                var toAdd = resultObject[3] as Dictionary<string, ScanSeries>;
                var toRemove = resultObject[4] as List<string>;

                var frmSelectSeries = new FrmSelectSeries(s0, s1);
                frmSelectSeries.ShowDialog();

                if (frmSelectSeries.Cancelled)
                {
                    var seriesname =
                        (from s in ImportTvFactory.SeriesNameList where s.SeriesName == s1 select s).SingleOrDefault();

                    seriesname.Skipped = true;
                    seriesname.WaitingForScan = false;
                }
                else
                {

                    if (frmSelectSeries.SelectedSeries != null)
                    {
                        ImportTvFactory.ScanSeriesPicks.Add(
                        new ScanSeriesPick
                            {
                                SearchString = s1, 
                                SeriesID = frmSelectSeries.SelectedSeries.SeriesID, 
                                SeriesName = frmSelectSeries.SelectedSeries.SeriesName
                            });
                    }

                    var series = this.theTvdb.OpenNewSeries(frmSelectSeries.SelectedSeries);

                    this.Set(series, toRemove, toAdd, s1, s2);
                }

                frmSelectSeries.Dispose();

                this.ButNextClick(null, null);
            }
            else
            {
                this.Hide();

                var frmNotCatagorized = new FrmNotCatagorized2();
                frmNotCatagorized.ShowDialog();
            }

            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.ScanSeriesPick);
        }

        /// <summary>
        /// Handles the Tick event of the TmrUi control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TmrUi_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = this.currentStatus;

            this.butNext.Enabled = !this.bgw.IsBusy;
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.bgw.CancelAsync();
            this.Close();
        }
    }
}