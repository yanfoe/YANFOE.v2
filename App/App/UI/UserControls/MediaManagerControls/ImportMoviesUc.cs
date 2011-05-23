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

    using YANFOE.Factories;
    using YANFOE.Factories.Import;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie.Models.Search;

    using Timer = System.Windows.Forms.Timer;

    public partial class ImportMoviesUc : DevExpress.XtraEditors.XtraForm
    {
        #region Fields

        private Timer tmr = new Timer();

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
            SetupDataBindings();
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

            this.bgw = new BackgroundWorker();
            this.bgw.DoWork += this.bgw_DoWork;
            this.bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            this.bgw.RunWorkerAsync();

            this.ClearBindings();
            this.SetBindings();
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
            this.txtTitle.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "Title");

            this.txtYear.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "Year");

            this.txtImdbID.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "ImdbId");

            this.txtTmdbId.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "TmdbId", true);

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

        /// <summary>
        /// Starts the search process.
        /// </summary>
        /// <param name="yanfoeid">The yanfoe id.</param>
        /// <param name="title">The title value.</param>
        /// <param name="year">The year value.</param>
        /// <returns>A query object.</returns>
        private Query DoSearch(string yanfoeid, string title, string year)
        {
            var results = new BindingList<QueryResult>();
            var query = new Query
                {
                    Results = results, 
                    Title = title, 
                    Year = year
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
        /// Background worker is initialized.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BgwMultiDoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var movieIndex in grdViewMoviesList.GetSelectedRows())
            {
                do
                {
                    Thread.Sleep(50);
                }
                while (this.bgwCollection.Count > 2);
                
                var bgw = new BackgroundWorker();

                bgw.DoWork += this.BgwMultiElementDoWork;
                bgw.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

                this.bgwCollection.Add(bgw);

                bgw.RunWorkerAsync(movieIndex);

                Thread.Sleep(50);
            }

            do
            {
                Thread.Sleep(50);
            }
            while (this.bgwCollection.Count > 0);
        }

        /// <summary>
        /// Background worker is complete
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwMultiElementRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.bgwCollection.Remove(sender as BackgroundWorker);
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

            var query = this.DoSearch(movie.YanfoeID, movie.Title, movie.Year.ToString());

            if (query.Results.Count > 0)
            {
                this.UpdateMovieModel(movie, query.Results[0]);
            }
        }

        /// <summary>
        /// Background worker is complete
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BgwMultiRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ClearBindings();
            this.SetBindings();
            this.FormElementsEnabled(true);
            this.ResetStatus();
            dxErrorProvider1.UpdateBinding();
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

        #endregion
    }
}
