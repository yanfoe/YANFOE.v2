// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoviesUserControl.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The main movie user control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.UserControls.MovieControls
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;

    using YANFOE.Factories;
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Factories.Scraper;
    using YANFOE.Factories.UI;
    using YANFOE.Models.MovieModels;

    /// <summary>
    /// The main movie user control.
    /// </summary>
    public partial class MoviesUserControl : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MoviesUserControl" /> class.
        /// </summary>
        public MoviesUserControl()
        {
            this.InitializeComponent();

            this.SetupDatabindings();
            this.SetupEventHandlers();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Click event of the btnLoadFromWeb control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnLoadFromWeb_Click(object sender, EventArgs e)
        {
            this.grdViewByTitle.RefreshData();

            var count = this.grdViewByTitle.SelectedRowsCount;

            if (count == 1)
            {
                MovieScrapeFactory.RunSingleScrape(MovieDBFactory.GetCurrentMovie());
            }
            else if (count > 1)
            {
                MovieScrapeFactory.RunMultiScrape(MovieDBFactory.MultiSelectedMovies);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnLock control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnLock_Click(object sender, EventArgs e)
        {
            MovieDBFactory.GetCurrentMovie().Locked = !MovieDBFactory.GetCurrentMovie().Locked;
            this.grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the btnWatched control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnWatched_Click(object sender, EventArgs e)
        {
            MovieDBFactory.GetCurrentMovie().Watched = !MovieDBFactory.GetCurrentMovie().Watched;
            this.grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the BtnMarked control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnMarked_Click(object sender, EventArgs e)
        {
            MovieDBFactory.GetCurrentMovie().Marked = !MovieDBFactory.GetCurrentMovie().Marked;
            this.grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the DoubleClick event of the BtnNew control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnNew_DoubleClick(object sender, EventArgs e)
        {
            var button = sender as SimpleButton;

            MovieDBFactory.GetCurrentMovie().IsNew = false;

            button.Visible = false;
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveAllImages control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSaveAllImages_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Images);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveFanart control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSaveFanart_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Fanart);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveNfo control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSaveNfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Nfo);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSavePoster control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSavePoster_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Poster);
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.StartSaveMovie();
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSave_Click(object sender, ItemClickEventArgs e)
        {
            this.StartSaveMovie();
        }

        /// <summary>
        /// Handles the Click event of the GalleryItem control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void GalleryItem_Click(object sender, GalleryItemClickEventArgs e)
        {
            this.grdViewByTitle.ClearSelection();

            var selectedMovie = MovieDBFactory.MovieDatabase.IndexOf(MovieDBFactory.GetMovie(e.Item.Tag.ToString()));
            var handle = this.grdViewByTitle.GetRowHandle(selectedMovie);
            this.grdViewByTitle.FocusedRowHandle = handle;
            this.grdViewByTitle.SelectRow(handle);
            this.UpdateMovieFromGrid();

            // MovieDBFactory.SetCurrentMovie(e.Item.Tag.ToString());
        }

        /// <summary>
        /// Handles the DoubleClick event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void GrdViewByTitle_DoubleClick(object sender, EventArgs e)
        {
            if (this.tabSets.Visible)
            {
                var pt = this.grdViewByTitle.GridControl.PointToClient(MousePosition);
                var info = this.grdViewByTitle.CalcHitInfo(pt);

                if (info.InRow || info.InRowCell)
                {
                    var movie = this.grdViewByTitle.GetRow(info.RowHandle) as MovieModel;

                    this.setManagerUserControl1.AddMovieToSet(movie);
                }
            }
        }

        /// <summary>
        /// Handles the RowCellStyle event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs"/> instance containing the event data.
        /// </param>
        private void GrdViewByTitle_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var row = this.grdViewByTitle.GetRow(e.RowHandle) as MovieModel;

            if (row == null)
            {
                return;
            }

            if (row.ChangedText || row.ChangedPoster)
            {
                e.Appearance.Font = Settings.Get.LookAndFeel.TextChanged;
            }
            else
            {
                e.Appearance.Font = Settings.Get.LookAndFeel.TextNormal;
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void GrdViewByTitle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateMovieFromGrid();
        }

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            this.btnLock.DataBindings.Clear();
            this.btnMarked.DataBindings.Clear();
            this.btnWatched.DataBindings.Clear();

            this.btnLoadFromWeb.DataBindings.Clear();
            this.btnLoadFromWeb.DataBindings.Add("Enabled", MovieDBFactory.GetCurrentMovie(), "Unlocked");

            this.btnLock.DataBindings.Add(
                "Image", MovieDBFactory.GetCurrentMovie(), "LockedImage", true, DataSourceUpdateMode.OnPropertyChanged);
            this.btnMarked.DataBindings.Add(
                "Image", MovieDBFactory.GetCurrentMovie(), "MarkedImage", true, DataSourceUpdateMode.OnPropertyChanged);

            this.btnNew.Visible = MovieDBFactory.GetCurrentMovie().IsNew;

            this.btnWatched.DataBindings.Add("Image", MovieDBFactory.GetCurrentMovie(), "WatchedImage");

            this.btnMediaInfo.DataBindings.Clear();
            
            this.btnMediaInfo.DataBindings.Add("Image", MovieDBFactory.GetCurrentMovie(), "MediaInfoImage");
        }

        /// <summary>
        /// Handles the DatabaseChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void MovieDBFactory_DatabaseChanged(object sender, EventArgs e)
        {
            this.SetupDatabindings();
        }

        /// <summary>
        /// Handles the DatabaseValuesRefreshRequired event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void MovieDBFactory_DatabaseValuesRefreshRequired(object sender, EventArgs e)
        {
            this.grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the GalleryChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void MovieDBFactory_GalleryChanged(object sender, EventArgs e)
        {
            this.UpdatePosterCount();

            this.UpdateGallery();
        }

        /// <summary>
        /// Handles the ListChanged event of the MoviesUserControl control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void MoviesUserControl_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.UpdateTitleCount();
        }

        /// <summary>
        /// Setups the form databindings.
        /// </summary>
        private void SetupDatabindings()
        {
            this.grdMoviesList.DataSource = null;
            this.grdMoviesList.DataSource = MovieDBFactory.MovieDatabase;

            grdViewByTitle.BeginSort();
            grdViewByTitle.Columns["Title"].SortOrder = ColumnSortOrder.Ascending;
            grdViewByTitle.EndSort();
        }

        /// <summary>
        /// Setups the event handlers.
        /// </summary>
        private void SetupEventHandlers()
        {
            MovieDBFactory.DatabaseChanged += this.MovieDBFactory_DatabaseChanged;

            MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;
            MovieDBFactory.GalleryChanged += this.MovieDBFactory_GalleryChanged;
            MovieDBFactory.MovieDatabase.ListChanged += this.MoviesUserControl_ListChanged;
            MovieDBFactory.DatabaseValuesRefreshRequired += this.MovieDBFactory_DatabaseValuesRefreshRequired;

            this.UpdateGallery();
        }

        /// <summary>
        /// Starts the save movie process.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        private void StartSaveMovie(MovieIOType type = MovieIOType.All)
        {
            this.UpdatedSelectedMoviesInFactory(this.grdViewByTitle.GetSelectedRows());
            Factories.InOut.OutFactory.SaveMovie(type);
            this.grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.
        /// </param>
        private void ToolTipController1_GetActiveObjectInfo(
            object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            GridHitInfo hi = this.grdViewByTitle.CalcHitInfo(e.ControlMousePosition);
            if (hi.InRowCell)
            {
                var movieModel = this.grdViewByTitle.GetRow(hi.RowHandle) as MovieModel;

                if (movieModel != null)
                {
                    e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty)
                    {
                       SuperTip = movieModel.GetMovieSuperTip() 
                    };
                }
            }
        }

        /// <summary>
        /// Updates the gallery.
        /// </summary>
        private void UpdateGallery()
        {
            this.galleryControl1.Gallery.Groups.Clear();
            this.galleryControl1.Gallery.Groups.Add(MovieDBFactory.GetGalleryGroup());
        }

        /// <summary>
        /// The update movie from grid.
        /// </summary>
        private void UpdateMovieFromGrid()
        {
            var rows = this.grdViewByTitle.GetSelectedRows();

            this.UpdatedSelectedMoviesInFactory(rows);

            btnMutliWatchedFalse.Visible = rows.Length != 1;
            this.btnMultiWatchedTrue.Visible = rows.Length != 1;
            btnWatched.Visible = rows.Length == 1;

            if (rows.Length == 1)
            {
                MovieDBFactory.IsMultiSelected = false;

                var selectedRow = this.grdViewByTitle.GetRow(rows[0]) as MovieModel;

                if (MovieDBFactory.IsSameAsCurrentMovie(selectedRow))
                {
                    return;
                }

                MovieDBFactory.SetCurrentMovie(selectedRow);
            }
            else if (rows.Length > 1)
            {
                MovieDBFactory.IsMultiSelected = true;
                MovieDBFactory.SetCurrentMovie(
                    new MovieModel { Title = "Multiple Movies Selected", MultiSelectModel = true });
            }
        }

        /// <summary>
        /// Updates the poster count.
        /// </summary>
        private void UpdatePosterCount()
        {
            this.tabByPoster.Text = string.Format("Poster ({0})", MovieDBFactory.GetGalleryGroup().Items.Count);
        }

        /// <summary>
        /// Updates the title count.
        /// </summary>
        private void UpdateTitleCount()
        {
            this.tabByTitle.Text = string.Format("Title ({0})", MovieDBFactory.MovieDatabase.Count);
        }

        /// <summary>
        /// Updated the selected movies in factory.
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        private void UpdatedSelectedMoviesInFactory(int[] rows)
        {
            MovieDBFactory.MultiSelectedMovies.Clear();

            foreach (var movieIndex in rows)
            {
                var movieModel = this.grdViewByTitle.GetRow(movieIndex) as MovieModel;
                MovieDBFactory.MultiSelectedMovies.Add(movieModel);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the barItemLink control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void barItemLink_ItemClick(object sender, ItemClickEventArgs e)
        {
            MovieDBFactory.TempScraperGroup = e.Item.Tag.ToString();
            this.BtnLoadFromWeb_Click(this, new EventArgs());
        }

        /// <summary>
        /// Handles the Click event of the btnOpenFile control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].PathAndFilename))
            {
                Process.Start(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].PathAndFilename);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOpenFolder control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string argument = string.Format(
                @"/select,""{0}""",
                File.Exists(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].PathAndFilename)
                    ? MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].PathAndFilename
                    : MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FolderPath);

            Process.Start("explorer.exe", argument);
        }

        /// <summary>
        /// Handles the PopupMenuShowing event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs"/> instance containing the event data.
        /// </param>
        private void grdViewByTitle_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var view = sender as GridView;

            e.Allow = false;

            this.popupMovieList.ShowPopup(this.barManager1, view.GridControl.PointToScreen(e.Point));
        }

        /// <summary>
        /// Handles the BeforePopup event of the popupLoadFromWeb control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.
        /// </param>
        private void popupLoadFromWeb_BeforePopup(object sender, CancelEventArgs e)
        {
            this.popupLoadFromWeb.ClearLinks();

            var barItemLink = new BarButtonItem(
                this.barManager1, "Scrape using " + MovieDBFactory.GetCurrentMovie().ScraperGroup);
            barItemLink.Tag = MovieDBFactory.GetCurrentMovie().ScraperGroup;
            barItemLink.ItemClick += this.barItemLink_ItemClick;
            this.popupLoadFromWeb.AddItem(barItemLink);

            foreach (var scraper in MovieScraperGroupFactory.GetScraperGroupsOnDisk())
            {
                barItemLink = new BarButtonItem(this.barManager1, "Use " + scraper);
                barItemLink.Tag = MovieDBFactory.GetCurrentMovie().ScraperGroup;
                barItemLink.ItemClick += this.barItemLink_ItemClick;
                this.popupLoadFromWeb.AddItem(barItemLink);
            }
        }

        /// <summary>
        /// Handles the BeforePopup event of the popupMovieList control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.
        /// </param>
        private void popupMovieList_BeforePopup(object sender, CancelEventArgs e)
        {
            var rows = this.grdViewByTitle.GetSelectedRows();
            var movieList = rows.Select(row => this.grdViewByTitle.GetRow(row) as MovieModel).ToList();

            if (movieList.Count == 1)
            {
                this.popupLock.Visibility = movieList[0].Locked ? BarItemVisibility.Never : BarItemVisibility.Always;
                this.popupUnlock.Visibility = movieList[0].Locked ? BarItemVisibility.Always : BarItemVisibility.Never;

                this.popupMark.Visibility = movieList[0].Marked ? BarItemVisibility.Never : BarItemVisibility.Always;
                this.popupUnmark.Visibility = movieList[0].Marked ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
            else
            {
                if (movieList.Exists(i => i.Locked))
                {
                    this.popupUnlock.Visibility = BarItemVisibility.Always;
                }

                if (movieList.Exists(i => i.Unlocked))
                {
                    this.popupLock.Visibility = BarItemVisibility.Always;
                }

                if (movieList.Exists(i => i.Marked))
                {
                    this.popupUnmark.Visibility = BarItemVisibility.Always;
                }

                if (movieList.Exists(i => i.Unlocked))
                {
                    this.popupMark.Visibility = BarItemVisibility.Always;
                }
            }
        }

        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnMutliWatchedTrue_Click(object sender, EventArgs e)
        {
            foreach (var movie in MovieDBFactory.MultiSelectedMovies)
            {
                movie.Watched = true;
            }
        }

        private void btnMutliWatchedFalse_Click(object sender, EventArgs e)
        {
            foreach (var movie in MovieDBFactory.MultiSelectedMovies)
            {
                movie.Watched = false;
            }
        }

        private void btnMediaInfo_Click(object sender, EventArgs e)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            btnMediaInfo.Enabled = false;
            bgw.RunWorkerAsync();
        }



        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var rows = grdViewByTitle.GetSelectedRows();
            var countMax = rows.Count();
            var count = 0;

            Windows7UIFactory.StartProgressState(countMax);

            foreach (var movieIndex in rows)
            {
                (grdViewByTitle.GetRow(movieIndex) as MovieModel).DoMediaInfoLookup();
                count++;
                Windows7UIFactory.SetProgressValue(count);
            }

            Windows7UIFactory.StopProgressState();
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnMediaInfo.Enabled = true;
        }

        private void popupLock_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeAllLockOnSelected(true);
        }

        private void popupUnlock_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeAllLockOnSelected(false);
        }

        private void ChangeAllLockOnSelected(bool value)
        {
            MovieDBFactory.IgnoreMultiSelect = true;

            this.grdViewByTitle.GetSelectedRows()
                .Select(row => this.grdViewByTitle.GetRow(row) as MovieModel)
                .Select(c => c.Locked = value).ToList();

            MovieDBFactory.IgnoreMultiSelect = false;
        }

        private void ChangeAllMarkOnSelected(bool value)
        {
            MovieDBFactory.IgnoreMultiSelect = true;

            this.grdViewByTitle.GetSelectedRows()
                .Select(row => this.grdViewByTitle.GetRow(row) as MovieModel)
                .Select(c => c.Marked = value).ToList();

            MovieDBFactory.IgnoreMultiSelect = false;
        }

        private void popupMark_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeAllMarkOnSelected(true);
        }

        private void popupUnmark_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChangeAllMarkOnSelected(false);
        }
    }
}