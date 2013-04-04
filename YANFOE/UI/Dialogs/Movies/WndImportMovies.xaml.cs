using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BitFactory.Logging;
using YANFOE.Factories;
using YANFOE.Factories.Import;
using YANFOE.Models.MovieModels;
using YANFOE.Scrapers.Movie.Models.Search;
using YANFOE.Tools.UI;
using YANFOE.UI.Popups;

namespace YANFOE.UI.UserControls.MediaManagerControls
{
    /// <summary>
    /// Interaction logic for WndImportMovies.xaml
    /// </summary>
    public partial class WndImportMovies : Window
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
        public WndImportMovies()
        {
            InitializeComponent();

            this.bgwCollection = new BindingList<BackgroundWorker>();
            this.SetupDataBindings();
        }

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
            
            btnOK.IsEnabled = false;
            //this.layoutControlItemScrape.Visibility = LayoutVisibility.Never;
            //this.layoutControlItemProgress.Visibility = LayoutVisibility.Always;
            grpMain.IsEnabled = false;

            //Factories.UI.Windows7UIFactory.ProgressChanged += (sender, e) =>
            //{
            //    if (this.InvokeRequired)
            //    {
            //        BeginInvoke(
            //            new Progress(UpdateProgress),
            //            new object[]
            //                    {
            //                        Factories.UI.Windows7UIFactory.ProgressValue,
            //                        Factories.UI.Windows7UIFactory.ProgressMax
            //                    });
            //    }
            //};

            this.bgw = new BackgroundWorker();
            this.bgw.DoWork += this.bgw_DoWork;
            this.bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            this.bgw.RunWorkerAsync();

            this.SetBindings();
        }

        delegate void Progress(int value, int max);

        public void UpdateProgress(int value, int max)
        {
            progressBarControl1.Value = value;
            progressBarControl1.Maximum = max;
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportMoviesFactory.Instance.ConvertMediaPathImportToDB();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.initializing = false;
            this.btnOK.IsEnabled = true;
            //this.layoutControlItemScrape.Visibility = LayoutVisibility.Always;
            //this.layoutControlItemProgress.Visibility = LayoutVisibility.Never;
            grpMain.IsEnabled = true;
            if (ImportMoviesFactory.Instance.ImportDuplicatesDatabase.Count > 0)
            {
                string movies = "";
                BindingList<string> moviesCounted = new BindingList<string>();
                foreach (var movie in ImportMoviesFactory.Instance.ImportDuplicatesDatabase)
                {
                    var result = (from m in ImportMoviesFactory.Instance.ImportDuplicatesDatabase where (m.Title.ToLower().Trim() == movie.Title.ToLower().Trim()) select m).ToList();
                    if (result.Count > 1)
                    {
                        if (!moviesCounted.Contains(movie.Title))
                        {
                            movies += movie.Title + " (multiple), ";
                            moviesCounted.Add(movie.Title);
                        }
                    }
                    else
                    {
                        movies += movie.Title + ", ";
                    }
                }
                MessageBox.Show(string.Format("{0} scanned movies already exists in the database:\n{1}",
                    ImportMoviesFactory.Instance.ImportDuplicatesDatabase.Count, movies.Substring(0, movies.Length - 2)), "Duplicate movies found");

                InternalApps.Logs.Log.WriteToLog(LogSeverity.Info, 0, string.Format("{0} duplicate movies found: {1}",
                    ImportMoviesFactory.Instance.ImportDuplicatesDatabase.Count, movies.Substring(0, movies.Length - 2)), "ImportMoviesUC > RunWorkerCompleted");
            }
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
        /// Sets the bindings.
        /// </summary>
        private void SetBindings()
        {
            try
            {
                //this.txtTitle.Text = ImportMoviesFactory.CurrentRecord.Title;
                //this.txtYear.Text = ImportMoviesFactory.CurrentRecord.Year.ToString();
                //this.txtImdbID.Text = ImportMoviesFactory.CurrentRecord.ImdbId;
                //this.txtTmdbId.Text = ImportMoviesFactory.CurrentRecord.TmdbId;
                
                //if (ImportMoviesFactory.CurrentRecord.SmallPoster != null)
                //{
                //    this.picBox.DataBindings.Add("Image", ImportMoviesFactory.CurrentRecord, "SmallPoster");
                //}
                //else
                //{
                //    this.picBox.Image = null;
                //}

                //this.txtNfo.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "NfoPathOnDisk");
                //this.txtPoster.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "PosterPathOnDisk");
                //this.txtFanart.DataBindings.Add("Text", ImportMoviesFactory.CurrentRecord, "FanartPathOnDisk");
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
            var results = new ThreadedBindingList<QueryResult>();
            var query = new Query
            {
                Results = results,
                Title = title,
                Year = year,
                ImdbId = imdbid,
                TmdbId = tmdbid
            };

            Factories.Scraper.MovieScrapeFactory.QuickSearchTMDB(query);
            return query;
        }

        /// <summary>
        /// Enable disable clickable form elemtns.
        /// </summary>
        /// <param name="b">if set to <c>true</c> [b].</param>
        private void FormElementsEnabled(bool b)
        {
            //this.grpMain.Enabled = b;
            //this.btnBulkScan.Enabled = b;
            //this.btnChangeFanart.Enabled = b;
            //this.btnChangeNfo.Enabled = b;
            //this.btnChangePoster.Enabled = b;
            //this.btnOK.Enabled = b;
            //this.btnCancel.Enabled = b;
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
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            ImportMoviesFactory.Instance.CancelMovieImport();
            this.Close();
        }

        /// <summary>
        /// Event handler for the BtnOk Click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            MovieDBFactory.Instance.RemoveMissingMovies();
            ImportMoviesFactory.Instance.MergeImportDatabaseWithMain();
            this.Close();
        }

        ///// <summary>
        ///// Event handler for the GrdViewMoviesList FocusedRowChanged event.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        //private void GrdViewMoviesListFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (this.initializing)
        //        {
        //            return;
        //        }

        //        var row = grdViewMoviesList.GetRow(e.FocusedRowHandle) as MovieModel;

        //        ImportMoviesFactory.CurrentRecord = row;

        //        this.ClearBindings();
        //        this.SetBindings();

        //        this.grdAssociatedFiles.DataSource = ImportMoviesFactory.CurrentRecord.AssociatedFiles.Media;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Write(ex.Message);
        //    }
        //}

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

            this.SetBindings();
        }

        /// <summary>
        /// Background worker is initialized.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BgwMultiElementDoWork(object sender, DoWorkEventArgs e)
        {
            var movie = (MovieModel) e.Argument;

            var query = this.DoSearch(movie.Title, movie.Year.ToString(), movie.ImdbId, movie.TmdbId);

            e.Result = movie;

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
        //private void TimerTick(object sender, EventArgs e)
        //{
        //    lblCurrentActivity.Text = this.currentStatus;
        //}

        #endregion

        private BackgroundWorker bgw1 = new BackgroundWorker();
        private BackgroundWorker bgw2 = new BackgroundWorker();
        private BackgroundWorker bgw3 = new BackgroundWorker();

        /// <summary>
        /// Handles the Click event of the btnBulkScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnBulkScan_Click(object sender, RoutedEventArgs e)
        {
            this.bgw1.DoWork += this.BgwMultiElementDoWork;
            this.bgw1.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            this.bgw2.DoWork += this.BgwMultiElementDoWork;
            this.bgw2.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            this.bgw3.DoWork += this.BgwMultiElementDoWork;
            this.bgw3.RunWorkerCompleted += this.BgwMultiElementRunWorkerCompleted;

            foreach (MovieModel selectedModel in movieList.SelectedItems)
            {
                this.lblCurrentActivity.Content = string.Format("Looking up {0}", selectedModel.Title);

                if (!this.bgw1.IsBusy)
                {
                    this.bgw1.RunWorkerAsync(selectedModel);
                }
                else if (!this.bgw2.IsBusy)
                {
                    this.bgw2.RunWorkerAsync(selectedModel);
                }
                else if (!this.bgw3.IsBusy)
                {
                    this.bgw3.RunWorkerAsync(selectedModel);
                }
                else
                {
                    Thread.Sleep(50);
                }
            }

            try
            {
                //movieList.SelectedItems.Clear();
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("Please select a row to perform the action", "Nothing selected");
            }
        }

        #endregion

        private void btnChangeNfo_Click(object sender, RoutedEventArgs e)
        {
            var form = new WndSimpleBrowse(WndSimpleBrowse.browseType.File);
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                this.txtNfo.Text = form.getInput();
            }
        }

        private void btnChangePoster_Click(object sender, RoutedEventArgs e)
        {
            var form = new WndSimpleBrowse(WndSimpleBrowse.browseType.File);
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                this.txtPoster.Text = form.getInput();
            }
        }

        private void btnChangeFanart_Click(object sender, RoutedEventArgs e)
        {
            var form = new WndSimpleBrowse(WndSimpleBrowse.browseType.File);
            form.ShowDialog();
            if (form.DialogResult == true)
            {
                this.txtFanart.Text = form.getInput();
            }
        }
    }
}
