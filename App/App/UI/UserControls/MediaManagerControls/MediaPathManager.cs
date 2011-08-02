// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaPathManager.cs" company="The YANFOE Project">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Media;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.UI.Dialogs.General;
    using YANFOE.UI.Dialogs.TV;

    /// <summary>
    /// Raw media manager
    /// </summary>
    public partial class MediaPathManager : XtraUserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The background worker.
        /// </summary>
        private BackgroundWorker backgroundWorker;

        /// <summary>
        /// The current progress.
        /// </summary>
        private Progress currentProgress;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPathManager"/> class.
        /// </summary>
        public MediaPathManager()
        {
            this.InitializeComponent();

            this.SetupUi();
            this.SetupBindings();
        }

        #endregion

        #region Events

        /// <summary>
        /// The import movies clicked.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler ImportMoviesClicked = delegate { };

        /// <summary>
        /// The import tv clicked.
        /// </summary>
        [field: NonSerialized]
        public event EventHandler ImportTvClicked = delegate { };

        #endregion

        #region Public Methods

        /// <summary>
        /// The invoke import movies clicked.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void InvokeImportMoviesClicked(EventArgs e)
        {
            EventHandler handler = this.ImportMoviesClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// The invoke import tv clicked.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public void InvokeImportTvClicked(EventArgs e)
        {
            EventHandler handler = this.ImportTvClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// The setup bindings.
        /// </summary>
        public void SetupBindings()
        {
            this.grdMediaPathList.DataSource = MediaPathDBFactory.MediaPathDB;
            this.grdMediaPathUnsortedMovies.DataSource = MediaPathDBFactory.GetMediaPathMoviesUnsorted();
            this.grdMediaPathUnsortedTv.DataSource = MediaPathDBFactory.GetMediaPathTvUnsorted();
        }

        /// <summary>
        /// The setup ui.
        /// </summary>
        public void SetupUi()
        {
            this.currentProgress = new Progress { Message = "Idle." };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MediaPathDBFactory.RefreshFiles(this.currentProgress);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // this.DisplayMediaPathMovieCount();

            // this.lvlTotalTVFiles.Text = MediaPathDBFactory.GetMediaPathTvUnsorted().Count.ToString();
            this.SetupBindings();

            this.EnableForm(true);
        }

        /// <summary>
        /// Handles the Click event of the BtnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var frmEditMediaPath = new FrmEditMediaPath(FrmEditMediaPath.MediaPathActionType.Add, new MediaPathModel());
            frmEditMediaPath.ShowDialog(this);

            grdViewMain.RefreshData();
            // ButRefresh_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the BtnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            this.RemoteMediaPaths();
        }

        private void RemoteMediaPaths()
        {
            DialogResult result = XtraMessageBox.Show(
                this,
                string.Format(
                    "Are you sure you wish to delete the selected {0} media path(s)?",
                    this.grdViewMain.SelectedRowsCount), 
                "Are you sure?", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var removeModels = new List<MediaPathModel>();

                foreach (int v in this.grdViewMain.GetSelectedRows())
                {
                    var row = this.grdViewMain.GetRow(v) as MediaPathModel;
                    removeModels.Add(row);
                }

                foreach (var model in removeModels)
                {
                    MediaPathDBFactory.RemoveFromDatabase(model);
                }

                this.grdViewMain.RefreshData();
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnProcessMovies control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnProcessMovies_Click(object sender, EventArgs e)
        {
            var importMoviesUc = new ImportMoviesUc();
            this.InvokeImportMoviesClicked(new EventArgs());
            importMoviesUc.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the BtnProcessTv control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnProcessTv_Click(object sender, EventArgs e)
        {
            var frmImportTv = new FrmImportTv();
            frmImportTv.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the ButRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButRefresh_Click(object sender, EventArgs e)
        {
            MediaPathDBFactory.GetMediaPathMoviesUnsorted().Clear();
            MediaPathDBFactory.GetMediaPathTvUnsorted().Clear();

            this.backgroundWorker = new BackgroundWorker();

            this.backgroundWorker.DoWork += this.BackgroundWorker_DoWork;
            this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompleted;

            this.backgroundWorker.RunWorkerAsync();

            this.EnableForm(false);
        }

        /// <summary>
        /// The edit current row.
        /// </summary>
        private void EditCurrentRow()
        {
            var selectedRows = this.grdViewMain.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                return;
            }

            var frmEditMediaPath = new FrmEditMediaPath(
                FrmEditMediaPath.MediaPathActionType.Edit, 
                this.grdViewMain.GetRow(selectedRows[0]) as MediaPathModel);

            frmEditMediaPath.ShowDialog(this);
        }

        /// <summary>
        /// The enable form.
        /// </summary>
        /// <param name="enable">
        /// The enable.
        /// </param>
        private void EnableForm(bool enable)
        {
            this.btnRefresh.Enabled = enable;
            this.btnAdd.Enabled = enable;
            this.btnEdit.Enabled = enable;
            this.btnDelete.Enabled = enable;
            this.btnProcessMovies.Enabled = enable;
            this.btnProcessTv.Enabled = enable;
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.lblCurrentStatus.Text = this.currentProgress.Message;
            this.progressBarControl.EditValue = this.currentProgress.Percent;
            this.btnProcessTv.Enabled = this.grdViewUnsortedTv.RowCount > 0 && !this.backgroundWorker.IsBusy;
            this.btnProcessMovies.Enabled = this.grdViewUnsortedMovies.RowCount > 0 && !this.backgroundWorker.IsBusy;
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.EditCurrentRow();
        }

        /// <summary>
        /// Handles the DoubleClick event of the grdViewMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void grdViewMain_DoubleClick(object sender, EventArgs e)
        {
            this.EditCurrentRow();
        }

        #endregion

        private void grdMediaPathList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && grdMediaPathList.Focused && grdViewMain.SelectedRowsCount > 0)
            {
                RemoteMediaPaths();
            }
        }
    }
}