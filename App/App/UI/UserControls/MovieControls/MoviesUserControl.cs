// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoviesUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.MovieControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;

    using YANFOE.Factories;
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Models.MovieModels;

    /// <summary>
    /// The main movie user control.
    /// </summary>
    public partial class MoviesUserControl : XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoviesUserControl"/> class.
        /// </summary>
        public MoviesUserControl()
        {
            InitializeComponent();

            this.SetupDatabindings();
            this.SetupEventHandlers();
        }

        #region Setup

        /// <summary>
        /// Setups the form databindings.
        /// </summary>
        private void SetupDatabindings()
        {
            this.grdMoviesList.DataSource = null;
            this.grdMoviesList.DataSource = MovieDBFactory.MovieDatabase;
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
        /// Handles the DatabaseValuesRefreshRequired event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_DatabaseValuesRefreshRequired(object sender, EventArgs e)
        {
            grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the DatabaseChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_DatabaseChanged(object sender, EventArgs e)
        {
            this.SetupDatabindings();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the gallery.
        /// </summary>
        private void UpdateGallery()
        {
            this.galleryControl1.Gallery.Groups.Clear();
            this.galleryControl1.Gallery.Groups.Add(MovieDBFactory.GetGalleryGroup());
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
        /// Handles the ListChanged event of the MoviesUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void MoviesUserControl_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.UpdateTitleCount();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            this.btnLock.DataBindings.Clear();
            this.btnMarked.DataBindings.Clear();

            this.btnLock.DataBindings.Add("Image", MovieDBFactory.GetCurrentMovie(), "LockedImage", true, DataSourceUpdateMode.OnPropertyChanged);
            this.btnMarked.DataBindings.Add("Image", MovieDBFactory.GetCurrentMovie(), "MarkedImage", true, DataSourceUpdateMode.OnPropertyChanged);

            this.btnNew.Visible = MovieDBFactory.GetCurrentMovie().IsNew;
        }

        /// <summary>
        /// Handles the GalleryChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_GalleryChanged(object sender, EventArgs e)
        {
            this.UpdatePosterCount();

            this.UpdateGallery();
        }

        #endregion

        #region UI Events

        /// <summary>
        /// Handles the DoubleClick event of the BtnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnNew_DoubleClick(object sender, EventArgs e)
        {
            var button = sender as SimpleButton;

            MovieDBFactory.GetCurrentMovie().IsNew = false;

            button.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the BtnMarked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnMarked_Click(object sender, EventArgs e)
        {
            MovieDBFactory.GetCurrentMovie().Marked = !MovieDBFactory.GetCurrentMovie().Marked;
            grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the GalleryItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs"/> instance containing the event data.</param>
        private void GalleryItem_Click(object sender, GalleryItemClickEventArgs e)
        {
            MovieDBFactory.SetCurrentMovie(e.Item.Tag.ToString());
        }

        /// <summary>
        /// Handles the Click event of the BtnLock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnLock_Click(object sender, EventArgs e)
        {
            MovieDBFactory.GetCurrentMovie().Locked = !MovieDBFactory.GetCurrentMovie().Locked;
            grdViewByTitle.RefreshData();
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnLoadFromWeb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnLoadFromWeb_Click(object sender, EventArgs e)
        {
            grdViewByTitle.RefreshData();

            var count = grdViewByTitle.SelectedRowsCount;

            if (count == 1)
            {
                Factories.Scraper.MovieScrapeFactory.RunSingleScrape(MovieDBFactory.GetCurrentMovie());
            }
            else if (count > 1)
            {
                Factories.Scraper.MovieScrapeFactory.RunMultiScrape(MovieDBFactory.MultiSelectedMovies);
            }
        }

        /// <summary>
        /// Determines whether [is ready for scrape].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is ready for scrape]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsReadyForScrape()
        {
            if (string.IsNullOrEmpty(MovieDBFactory.GetCurrentMovie().ImdbId))
            {
                InternalApps.Logs.Log.WriteToLog(LogSeverity.Warning, 0, "Scrape fail", "No IMDB ID found for " + MovieDBFactory.GetCurrentMovie().Title);
                XtraMessageBox.Show("No IMDB ID Found for this movie");

                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void GrdViewByTitle_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var rows = grdViewByTitle.GetSelectedRows();

            this.UpdatedSelectedMoviesInFactory(rows);

            if (rows.Length == 1)
            {
                MovieDBFactory.IsMultiSelected = false;

                var selectedRow = grdViewByTitle.GetRow(rows[0]) as MovieModel;

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
        /// Updated the selected movies in factory.
        /// </summary>
        /// <param name="rows">The rows.</param>
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
        /// Handles the RowCellStyle event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void GrdViewByTitle_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var row = grdViewByTitle.GetRow(e.RowHandle) as MovieModel;

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
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.</param>
        private void ToolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            GridHitInfo hi = grdViewByTitle.CalcHitInfo(e.ControlMousePosition);
            if (hi.InRowCell)
            {
                var movieModel = grdViewByTitle.GetRow(hi.RowHandle) as MovieModel;
                e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty)
                    {
                        SuperTip = movieModel.GetMovieSuperTip()
                    };
            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GrdViewByTitle_DoubleClick(object sender, EventArgs e)
        {
            if (this.tabSets.Visible)
            {
                var pt = grdViewByTitle.GridControl.PointToClient(MousePosition);
                var info = grdViewByTitle.CalcHitInfo(pt);

                if (info.InRow || info.InRowCell)
                {
                    var movie = grdViewByTitle.GetRow(info.RowHandle) as MovieModel;

                    setManagerUserControl1.AddMovieToSet(movie);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            this.StartSaveMovie();
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnSave_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.StartSaveMovie();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveFanart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnSaveFanart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Fanart);
        }

        /// <summary>
        /// Starts the save movie process.
        /// </summary>
        /// <param name="type">The type.</param>
        private void StartSaveMovie(MovieIOType type = MovieIOType.All)
        {
            this.UpdatedSelectedMoviesInFactory(this.grdViewByTitle.GetSelectedRows());
            Factories.InOut.OutFactory.SaveMovie(type);
            grdViewByTitle.RefreshData();
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSavePoster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnSavePoster_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Poster);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveAllImages control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnSaveAllImages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Images);
        }

        /// <summary>
        /// Handles the ItemClick event of the btnSaveNfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void BtnSaveNfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.StartSaveMovie(MovieIOType.Nfo);
        }

        /// <summary>
        /// Handles the PopupMenuShowing event of the grdViewByTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs"/> instance containing the event data.</param>
        private void grdViewByTitle_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var view = sender as GridView;

            e.Allow = false;

            this.popupMovieList.ShowPopup(this.barManager1, view.GridControl.PointToScreen(e.Point));
        }

        /// <summary>
        /// Handles the BeforePopup event of the popupMovieList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void popupMovieList_BeforePopup(object sender, CancelEventArgs e)
        {
            var rows = grdViewByTitle.GetSelectedRows();
            var movieList = rows.Select(row => this.grdViewByTitle.GetRow(row) as MovieModel).ToList();

            Popups.MovieListPopup.Generate(this.barManager1, popupMovieList, movieList);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FilePath))
            {
                Process.Start(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FilePath);
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            var argument = string.Empty;
            argument = string.Format(
                @"/select,""{0}""",
                File.Exists(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FilePath)
                    ? MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FilePath
                    : MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].FilePathFolder);

            Process.Start("explorer.exe", argument);
        }
    }
}
