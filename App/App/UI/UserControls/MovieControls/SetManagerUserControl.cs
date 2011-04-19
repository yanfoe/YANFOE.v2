// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetManagerUserControl.cs" company="The YANFOE Project">
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
    using System.Windows.Forms;

    using DevExpress.Data;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Base;

    using YANFOE.Factories;
    using YANFOE.Factories.Sets;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.SetsModels;
    using YANFOE.Properties;
    using YANFOE.Tools;
    using YANFOE.UI.Dialogs.General;

    /// <summary>
    /// The set manager user control.
    /// </summary>
    public partial class SetManagerUserControl : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SetManagerUserControl"/> class.
        /// </summary>
        public SetManagerUserControl()
        {
            this.InitializeComponent();

            MovieDBFactory.CurrentMovieChanged += this.movieDBFactory_CurrentMovieChanged;
            MovieSetManager.SetListChanged += this.movieSetManager_SetListChanged;

            this.SetupBindings();

            this.PopulateSetList();
            this.EnableForm(false);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the movie to set.
        /// </summary>
        /// <param name="movie">
        /// The movie.
        /// </param>
        public void AddMovieToSet(MovieModel movie)
        {
            if (string.IsNullOrEmpty(MovieSetManager.GetCurrentSet.SetName))
            {
                XtraMessageBox.Show("No set selected to add movie too");
                return;
            }

            MovieSetManager.AddMovieToCurrentSet(movie);
        }

        /// <summary>
        /// The switch up down buttons.
        /// </summary>
        public void SwitchUpDownButtons()
        {
            MovieSetObjectModel currentRow = this.GetCurrentRow();

            this.btnMoveUp.Enabled = true;
            this.btnMoveDown.Enabled = true;
            this.btnTrash.Enabled = true;

            if (currentRow == null)
            {
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
                this.btnTrash.Enabled = false;
                return;
            }

            if (currentRow.Order == 1)
            {
                this.btnMoveUp.Enabled = false;
            }

            if (currentRow.Order == MovieSetManager.GetCurrentSet.Movies.Count)
            {
                this.btnMoveDown.Enabled = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Binds to movies in set list.
        /// </summary>
        private void BindToMoviesInSetList()
        {
            this.gridControl.DataSource = MovieSetManager.GetCurrentSet.Movies;
        }

        /// <summary>
        /// Enables/disables the form.
        /// </summary>
        /// <param name="enable">
        /// if set to <c>true</c> [enable].
        /// </param>
        private void EnableForm(bool enable)
        {
            this.grpPoster.Enabled = enable;
            this.grpFanart.Enabled = enable;
            this.btnDeleteSet.Enabled = enable;
            this.pnlMovieListButtons.Enabled = enable;
        }

        /// <summary>
        /// The generate movies in set.
        /// </summary>
        private void GenerateMoviesInSet()
        {
            var setList = MovieSetManager.GetSetsContainingMovie(MovieDBFactory.GetCurrentMovie());

            this.txtMovieIsSets.Text = string.Join(Environment.NewLine, setList);
        }

        /// <summary>
        /// The get current row.
        /// </summary>
        /// <returns>
        /// </returns>
        private MovieSetObjectModel GetCurrentRow()
        {
            int[] currentRows = this.gridView.GetSelectedRows();

            if (currentRows.Length == 0)
            {
                return null;
            }

            return this.gridView.GetRow(currentRows[0]) as MovieSetObjectModel;
        }

        /// <summary>
        /// Populates the set list.
        /// </summary>
        private void PopulateSetList()
        {
            this.cmbSetsList.Properties.Items.Clear();

            foreach (MovieSetModel value in MovieSetManager.CurrentDatabase)
            {
                this.cmbSetsList.Properties.Items.Add(value.SetName);
            }
        }

        /// <summary>
        /// The refresh and reorder.
        /// </summary>
        private void RefreshAndReorder()
        {
            this.gridView.RefreshData();
            this.gridView.Columns["Order"].SortOrder = ColumnSortOrder.Ascending;
        }

        /// <summary>
        /// Removes the movie from set.
        /// </summary>
        private void RemoveMovieFromSet()
        {
            foreach (int movieIndex in this.gridView.GetSelectedRows())
            {
                var movie = this.gridView.GetRow(movieIndex) as MovieSetObjectModel;
                MovieSetManager.RemoveFromSet(movie.MovieUniqueId);
                MovieDBFactory.GetMovie(movie.MovieUniqueId).ChangedText = true;
            }
        }

        /// <summary>
        /// The set fanart data binding.
        /// </summary>
        private void SetFanartDataBinding()
        {
            this.picFanart.DataBindings.Clear();
            this.picFanart.DataBindings.Add("Image", MovieSetManager.GetCurrentSet, "Fanart");
        }

        /// <summary>
        /// Sets the poster data binding.
        /// </summary>
        private void SetPosterDataBinding()
        {
            this.picPoster.DataBindings.Clear();
            this.picPoster.DataBindings.Add("Image", MovieSetManager.GetCurrentSet, "Poster");
        }

        /// <summary>
        /// The setup bindings.
        /// </summary>
        private void SetupBindings()
        {
            this.BindToMoviesInSetList();

            MovieSetManager.CurrentSetChanged -= this.movieSetManager_CurrentSetChanged;
            MovieSetManager.CurrentSetChanged += this.movieSetManager_CurrentSetChanged;

            MovieSetManager.CurrentSetMoviesChanged -= this.movieSetManager_CurrentSetMoviesChanged;
            MovieSetManager.CurrentSetMoviesChanged += this.movieSetManager_CurrentSetMoviesChanged;

            this.SetPosterDataBinding();
            this.SetFanartDataBinding();
        }

        /// <summary>
        /// Handles the Click event of the BtnAddNewSet control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnAddNewSet_Click(object sender, EventArgs e)
        {
            var enterAValue = new FrmEnterAValue("Enter a new set name");
            enterAValue.ShowDialog();

            if (!string.IsNullOrEmpty(enterAValue.Response))
            {
                if (!MovieSetManager.HasSetWithName(enterAValue.Response))
                {
                    MovieSetManager.AddNewSet(enterAValue.Response);
                }
                else
                {
                    var notificationPanel = new FrmShowNotification("There is already a set with this name");
                    notificationPanel.ShowDialog();
                }

            }
        }

        /// <summary>
        /// Handles the Click event of the btnDeleteSet control.c
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnDeleteSet_Click(object sender, EventArgs e)
        {
            if (MovieSetManager.GetCurrentSet.Movies.Count > 0)
            {
                DialogResult result =
                    XtraMessageBox.Show(
                        string.Format("Are you sure you wish to delete '{0}'?", MovieSetManager.GetCurrentSet.SetName), 
                        "Are you sure?", 
                        MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            MovieSetManager.RemoveSet(this.cmbSetsList.Text);
        }

        /// <summary>
        /// The btn fanart clear_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnFanartClear_Click(object sender, EventArgs e)
        {
            MovieSetManager.ClearCurrentSetFanart();
            this.picFanart.Image = Resources.picturefaded128;
        }

        /// <summary>
        /// Handles the Click event of the btnMoveDown control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MovieSetObjectModel currentRow = this.GetCurrentRow();

            if (currentRow == null)
            {
                return;
            }

            MovieSetManager.GetCurrentSet.MoveMovieDown(currentRow.MovieUniqueId);

            this.RefreshAndReorder();
            this.SwitchUpDownButtons();
            MovieSetManager.SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// Handles the Click event of the btnMoveUp control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MovieSetObjectModel currentRow = this.GetCurrentRow();

            if (currentRow == null)
            {
                return;
            }

            MovieSetManager.GetCurrentSet.MoveMovieUp(currentRow.MovieUniqueId);

            this.RefreshAndReorder();
            this.SwitchUpDownButtons();
            MovieSetManager.SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// The btn poster clear_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnPosterClear_Click(object sender, EventArgs e)
        {
            MovieSetManager.ClearCurrentSetPoster();
            this.picPoster.Image = Resources.picturefaded128;
        }

        /// <summary>
        /// Handles the Click event of the btnTrash control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void btnTrash_Click(object sender, EventArgs e)
        {
            this.RemoveMovieFromSet();
            MovieSetManager.SetAllMoviesChangedInCurrentSet();
        }

        /// <summary>
        /// The cmb sets list_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void cmbSetsList_TextChanged(object sender, EventArgs e)
        {
            MovieSetManager.SetCurrentSet(this.cmbSetsList.Text);
        }

        /// <summary>
        /// The grid view_ focused row changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            this.SwitchUpDownButtons();
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
        private void movieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            this.GenerateMoviesInSet();
        }

        /// <summary>
        /// Handles the CurrentSetChanged event of the MovieSetManager control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void movieSetManager_CurrentSetChanged(object sender, EventArgs e)
        {
            this.SetupBindings();
            this.PopulateSetList();

            this.cmbSetsList.Text = MovieSetManager.GetCurrentSet == null
                                        ? string.Empty
                                        : MovieSetManager.GetCurrentSet.SetName;

            this.GenerateMoviesInSet();

            this.gridView.Columns["Order"].SortOrder = ColumnSortOrder.Ascending;

            this.EnableForm(MovieSetManager.GetCurrentSet != null);
        }

        /// <summary>
        /// Handles the CurrentSetMoviesChanged event of the MovieSetManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void movieSetManager_CurrentSetMoviesChanged(object sender, EventArgs e)
        {
            this.GenerateMoviesInSet();
        }

        /// <summary>
        /// Handles the SetListChanged event of the MovieSetManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void movieSetManager_SetListChanged(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                return;
            }

            this.PopulateSetList();
            this.GenerateMoviesInSet();
        }

        /// <summary>
        /// The popup fanart_ before popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void popupFanart_BeforePopup(object sender, CancelEventArgs e)
        {
            var selectFanartUsingUrl = new BarButtonItem(this.barManager1, "Get From Url") { Glyph = Resources.globe32 };

            this.popupFanart.ClearLinks();
            this.popupFanart.AddItem(selectFanartUsingUrl);

            List<MovieModel> movieCollection = MovieSetManager.GetMoviesInSets(MovieSetManager.GetCurrentSet);

            foreach (MovieModel movie in movieCollection)
            {
                if (!string.IsNullOrEmpty(movie.CurrentFanartImageUrl) || !string.IsNullOrEmpty(movie.FanartPathOnDisk))
                {
                    string path = movie.FanartPathOnDisk;

                    if (string.IsNullOrEmpty(path))
                    {
                        path = movie.CurrentFanartImageUrl;
                    }

                    var selectFanartFromMovie = new BarButtonItem(
                        this.barManager1, string.Format("Use fanart from {0}", movie.Title))
                        {
                           Tag = path, Glyph = ImageHandler.ResizeImage(movie.SmallFanart, 32, 24) 
                        };

                    selectFanartFromMovie.ItemClick += this.selectFanartFromMovie_ItemClick;
                    this.popupFanart.AddItem(selectFanartFromMovie);
                }
            }
        }

        /// <summary>
        /// Handles the BeforePopup event of the popupPoster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void popupPoster_BeforePopup(object sender, CancelEventArgs e)
        {
            var selectPosterUsingUrl = new BarButtonItem(this.barManager1, "Get From Url") { Glyph = Resources.globe32 };

            this.popupPoster.ClearLinks();
            this.popupPoster.AddItem(selectPosterUsingUrl);

            List<MovieModel> movieCollection = MovieSetManager.GetMoviesInSets(MovieSetManager.GetCurrentSet);

            foreach (MovieModel movie in movieCollection)
            {
                if (!string.IsNullOrEmpty(movie.CurrentPosterImageUrl) || !string.IsNullOrEmpty(movie.PosterPathOnDisk))
                {
                    string path = movie.PosterPathOnDisk;

                    if (string.IsNullOrEmpty(path))
                    {
                        path = movie.CurrentPosterImageUrl;
                    }

                    var selectPosterFromMovie = new BarButtonItem(
                        this.barManager1, string.Format("Use poster from {0}", movie.Title))
                        {
                           Tag = path, Glyph = ImageHandler.ResizeImage(movie.SmallPoster, 32, 46) 
                        };

                    selectPosterFromMovie.ItemClick += this.selectPosterFromMovie_ItemClick;
                    this.popupPoster.AddItem(selectPosterFromMovie);
                }
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the selectFanartFromMovie control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void selectFanartFromMovie_ItemClick(object sender, ItemClickEventArgs e)
        {
            MovieSetManager.ChangeCurrentSetFanart(e.Item.Tag.ToString());
            this.SetFanartDataBinding();
        }

        /// <summary>
        /// Handles the ItemClick event of the byMoviePoster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void selectPosterFromMovie_ItemClick(object sender, ItemClickEventArgs e)
        {
            MovieSetManager.ChangeCurrentSetPoster(e.Item.Tag.ToString());
            this.SetPosterDataBinding();
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnPosterSelectNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnPosterSelectNew_Click(object sender, EventArgs e)
        {
            this.PickFileDialog(FileType.Poster);
        }

        /// <summary>
        /// Handles the Click event of the btnFanartSelectNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnFanartSelectNew_Click(object sender, EventArgs e)
        {
            this.PickFileDialog(FileType.Fanart);
        }

        private void PickFileDialog(FileType type)
        {
            var openFileDialog = new OpenFileDialog
                {
                    Title = string.Format("Select a new set {0}", type),
                    Filter = Language.SetManagerUserControl_PickFileDialog_Jpeg____jpg____jpg_All_files__________
                };

            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (type == FileType.Poster)
                {
                    MovieSetManager.ChangeCurrentSetPoster(openFileDialog.FileName);
                    this.SetPosterDataBinding();
                }
                else
                {
                    MovieSetManager.ChangeCurrentSetFanart(openFileDialog.FileName);
                    this.SetFanartDataBinding();
                }
            }
        }

        private enum FileType
        {
            Poster,
            Fanart
        }
    }
}