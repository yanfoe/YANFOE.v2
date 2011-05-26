// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportMoviesUc.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.MediaManagerControls
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.Data;
    using DevExpress.XtraLayout.Utils;

    using YANFOE.Factories;
    using YANFOE.Factories.Import;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie.Models.Search;
    using YANFOE.Tools.Extentions;

    public partial class ImportMoviesUc : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private BackgroundWorker bgw = new BackgroundWorker();

        /// <summary>
        /// Used to display the current status to the UI
        /// </summary>
        private string currentStatus;

        /// <summary>
        /// Is the form being initialized. Stops events from being fired.
        /// </summary>
        private bool initializing;

        /// <summary>
        /// A collection of background workers
        /// </summary>
        private BindingList<BackgroundWorker> bgwCollection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportMoviesUc"/> class.
        /// </summary>
        public ImportMoviesUc()
        {
            InitializeComponent();

            this.bgwCollection = new BindingList<BackgroundWorker>();
            this.SetupDataBindings();
        }

        #endregion

        #region Initializations

        public void Reset()
        {
            this.SetupDataBindings();
            this.ResetStatus();
        }

        /// <summary>
        /// Setup Data Bindings
        /// </summary>
        private void SetupDataBindings()
        {
            this.initializing = true;

            this.grdMovies.DataSource = ImportMoviesFactory.ImportDatabase;
            grdViewMoviesList.BeginSort();
            grdViewMoviesList.Columns["Title"].SortOrder = ColumnSortOrder.Ascending;
            grdViewMoviesList.EndSort();

            btnOK.Enabled = false;
            this.layoutControlItemScrape.Visibility = LayoutVisibility.Never;
            this.layoutControlItemProgress.Visibility = LayoutVisibility.Always;
            grpMain.Enabled = false;

            Factories.UI.Windows7UIFactory.ProgressChanged += (sender, e) =>
                {
                    if (this.InvokeRequired)
                    {
                        BeginInvoke(
                            new Progress(UpdateProgress),
                            new object[]
                                {
                                    Factories.UI.Windows7UIFactory.ProgressValue,
                                    Factories.UI.Windows7UIFactory.ProgressMax
                                });
                    }
                };

            this.bgw = new BackgroundWorker();
            this.bgw.DoWork += this.bgw_DoWork;
            this.bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            this.bgw.RunWorkerAsync();

            this.ClearBindings();
            this.SetBindings();
        }

        delegate void Progress(int value, int max);

        public void UpdateProgress(int value, int max)
        {
            progressBarControl1.EditValue = value;
            progressBarControl1.Properties.Maximum = max;
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportMoviesFactory.ConvertMediaPathImportToDB();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.initializing = false;
            this.btnOK.Enabled = true;
            this.layoutControlItemScrape.Visibility = LayoutVisibility.Always;
            this.layoutControlItemProgress.Visibility = LayoutVisibility.Never;
            grpMain.Enabled = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the movie model.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        /// <param name="result">The result to gather values from</param>
        private void UpdateMovieModel(MovieModel movieModel, QueryResult result)
        {
            movieModel.YanfoeID = result.YanfoeId;
            movieModel.Title = result.Title;
            movieModel.Year = result.Year;
            movieModel.ImdbId = result.ImdbID;
            movieModel.TmdbId = result.TmdbID;
            movieModel.SmallPoster = result.Poster;
        }

        /// <summary>
        /// Clears the data bindings.
        /// </summary>
        private void ClearBindings()
        {
            this.txtTitle.DataBindings.Clear();
            this.txtYear.DataBindings.Clear();
            this.txtImdbID.DataBindings.Clear();
            this.txtTmdbId.DataBindings.Clear();
            this.picBox.DataBindings.Clear();

            this.txtNfo.DataBindings.Clear();
            this.txtPoster.DataBindings.Clear();
            this.txtFanart.DataBindings.Clear();
        }

        /// <summary>
        /// Sets the bindings.
        /// </summary>
        private void SetBindings()
        {
            try
            {
                this.txtTitle.Text = ImportMoviesFactory.CurrentRecord.Title;
                this.txtYear.Text = ImportMoviesFactory.CurrentRecord.Year.ToString();
                this.txtImdbID.Text = ImportMoviesFactory.CurrentRecord.ImdbId;
                this.txtTmdbId.Text = ImportMoviesFactory.CurrentRecord.TmdbId;

                dxErrorProvider1.DataSource = ImportMoviesFactory.CurrentRecord;

                if (ImportMoviesFactory.CurrentRecord.SmallPoster != null)
                {
                    this.picBox.DataBindings.Add("Image", ImportMoviesFactory.CurrentRecord, "SmallPoster");
                }
                else
                {
                    this.picBox.Image = null;
                }

                this.txtNfo.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "NfoPathOnDisk");
                this.txtPoster.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "PosterPathOnDisk");
                this.txtFanart.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "FanartPathOnDisk");
            }
            catch (Exception ex)
            {
                InternalApps.Logs.Log.WriteToLog(LogSeverity.Error, 0, ex.Message, "ImportMoviesUC > SetBindings");
            }
        }

        /// <summary>
        /// Starts the search process.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <param name="year">The year value.</param>
        /// <param name="imdbid"></param>
        /// <returns>A query object.</returns>
        private Query DoSearch(string title, string year, string imdbid, string tmdbid)
        {
            var results = new BindingList<QueryResult>();
            var query = new Query
                {
                    Results = results, 
                    Title = title, 
                    Year = year,
                    ImdbId = imdbid,
                    TmdbId = tmdbid
                };

            Factories.Scraper.MovieScrapeFactory.QuickSearchTmdb(query);
            return query;
        }

        /// <summary>
        /// Enable disable clickable form elemtns.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        private void FormElementsEnabled(bool b)
        {
            this.grpMain.Enabled = b;
            this.btnBulkScan.Enabled = b;
            this.btnChangeFanart.Enabled = b;
            this.btnChangeNfo.Enabled = b;
            this.btnChangePoster.Enabled = b;
            this.btnOK.Enabled = b;
            this.btnCancel.Enabled = b;
        }

        /// <summary>
        /// Resets the status displayed on the UI.
        /// </summary>
        private void ResetStatus()
        {
            this.currentStatus = "Idle";
        }

        #endregion

        #region Events

        [field: NonSerialized]
        public event EventHandler OkClicked = delegate { };

        public void InvokeOkClicked(EventArgs e)
        {
            EventHandler handler = this.OkClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        [field: NonSerialized]
        public event EventHandler CancelClicked = delegate { };

        public void InvokeCancelClicked(EventArgs e)
        {
            EventHandler handler = this.CancelClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #region UI Initiations

        /// <summary>
        /// Event handler for the BtnCancel Click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancelClick(object sender, EventArgs e)
        {
            ImportMoviesFactory.CancelMovieImport();
            this.Close();
        }

        /// <summary>
        /// Event handler for the BtnOk Click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            MovieDBFactory.RemoveMissingMovies();
            ImportMoviesFactory.MergeImportDatabaseWithMain();
            this.Close();
        }

        /// <summary>
        /// Event handler for the GrdViewMoviesList FocusedRowChanged event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void GrdViewMoviesListFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (this.initializing)
                {
                    return;
                }

                var row = grdViewMoviesList.GetRow(e.FocusedRowHandle) as MovieModel;

                ImportMoviesFactory.CurrentRecord = row;

                this.ClearBindings();
                this.SetBindings();

                this.grdAssociatedFiles.DataSource = ImportMoviesFactory.CurrentRecord.AssociatedFiles.Media;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
        }

        #endregion

        #region Code Initiations

        /// <summary>
        /// Background worker is complete
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwMultiElementRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.bgwCollection.Remove(sender as BackgroundWorker);
            this.FormElementsEnabled(true);
            this.ResetStatus();
            dxErrorProvider1.UpdateBinding();
            this.ClearBindings();

            this.SetBindings();
        }

        /// <summary>
        /// Background worker is initialized.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BgwMultiElementDoWork(object sender, DoWorkEventArgs e)
        {
            var movieIndex = (int)e.Argument;

            var movie = grdViewMoviesList.GetRow(movieIndex) as MovieModel;

            var query = this.DoSearch(movie.Title, movie.Year.ToString(), movie.ImdbId, movie.TmdbId);

            e.Result = movieIndex;

            if (query.Results.Count > 0)
            {
                this.UpdateMovieModel(movie, query.Results[0]);
            }
        }

        /// <summary>
        /// UI Update Timer
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TimerTick(object sender, EventArgs e)
        {
            lblCurrentActivity.Text = this.currentStatus;
        }

        #endregion

        private BackgroundWorker bgw1 = new BackgroundWorker();
        private BackgroundWorker bgw2 = new BackgroundWorker();
        private BackgroundWorker bgw3 = new BackgroundWorker();

        /// <summary>
        /// Handles the Click event of the btnBulkScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBulkScan_Click(object sender, EventArgs e)
        {
            this.bgw1.DoWork += this.BgwMultiElementDoWork;
            this.bgw1.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            this.bgw2.DoWork += this.BgwMultiElementDoWork;
            this.bgw2.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            this.bgw3.DoWork += this.BgwMultiElementDoWork;
            this.bgw3.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            for (int index = 0; index < this.grdViewMoviesList.GetSelectedRows().Length; index++)
            {
                var movieIndex = this.grdViewMoviesList.GetSelectedRows()[index];

                this.lblCurrentActivity.Text = string.Format("Looking up {0}", (this.grdViewMoviesList.GetRow(movieIndex) as MovieModel).Title);

                if (!this.bgw1.IsBusy)
                {
                    this.bgw1.RunWorkerAsync(movieIndex);
                }
                else if (!this.bgw2.IsBusy)
                {
                    this.bgw2.RunWorkerAsync(movieIndex);
                }
                else if (!this.bgw3.IsBusy)
                {
                    this.bgw3.RunWorkerAsync(movieIndex);
                }
                else
                {
                    index--;
                    Thread.Sleep(50);
                    Application.DoEvents();
                }
            }

            var row = this.grdViewMoviesList.GetSelectedRows()[0];
            grdViewMoviesList.ClearSelection();
            grdViewMoviesList.SelectRow(row);
        }

        #endregion

        /// <summary>
        /// Handles the EditValueChanged event of the txtTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtTitle_EditValueChanged(object sender, EventArgs e)
        {
            ImportMoviesFactory.CurrentRecord.Title = this.txtTitle.Text;
        }

        /// <summary>
        /// Handles the EditValueChanged event of the txtYear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtYear_EditValueChanged(object sender, EventArgs e)
        {
            ImportMoviesFactory.CurrentRecord.Year = this.txtYear.Text.ToInt();
        }

        /// <summary>
        /// Handles the EditValueChanged event of the txtTmdbId control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtTmdbId_EditValueChanged(object sender, EventArgs e)
        {
            ImportMoviesFactory.CurrentRecord.TmdbId = this.txtTmdbId.Text;
        }

        /// <summary>
        /// Handles the EditValueChanged event of the txtImdbID control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtImdbID_EditValueChanged(object sender, EventArgs e)
        {
            ImportMoviesFactory.CurrentRecord.ImdbId = this.txtImdbID.Text;
        }
    }
}
