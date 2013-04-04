using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YANFOE.Factories.Media;
using YANFOE.InternalApps.DownloadManager.Model;
using YANFOE.Models.GeneralModels.AssociatedFiles;
using YANFOE.UI.Dialogs.General;
using YANFOE.UI.Dialogs.TV;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using UserControl = System.Windows.Controls.UserControl;

namespace YANFOE.UI.UserControls.MediaManagerControls
{
    /// <summary>
    /// Interaction logic for MediaPathManager.xaml
    /// </summary>
    public partial class MediaPathManager : UserControl
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
            MediaPathDBFactory.Instance.RefreshFiles(this.currentProgress);
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

            this.EnableForm(true);
        }

        /// <summary>
        /// Handles the DoWork event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_DoWorkRemoveMediaPath(object sender, DoWorkEventArgs e)
        {
            var removeModels = (List<MediaPathModel>)e.Argument;
            foreach (var model in removeModels)
            {
                MediaPathDBFactory.Instance.RemoveFromDatabase(model);
            }
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void BackgroundWorker_RunWorkerCompletedRemoveMediaPath(object sender, RunWorkerCompletedEventArgs e)
        {
            this.EnableForm(true);
        }

        /// <summary>
        /// Handles the Click event of the BtnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var frmEditMediaPath = new WndEditMediaPath(WndEditMediaPath.MediaPathActionType.Add, new MediaPathModel());
            frmEditMediaPath.ShowDialog();

            // ButRefresh_Click(null, null);
        }

        /// <summary>
        /// Handles the Click event of the BtnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.RemoveMediaPaths();
        }

        private void RemoveMediaPaths()
        {
            MessageBoxResult result = MessageBox.Show(
                string.Format(
                    "Are you sure you wish to delete the selected {0} media path(s)?",
                    this.mediaPathList.SelectedItems.Count),
                "Are you sure?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var removeModels = new List<MediaPathModel>();

                foreach (MediaPathModel v in this.mediaPathList.SelectedItems)
                {
                    removeModels.Add(v);
                }

                this.backgroundWorker = new BackgroundWorker();

                this.backgroundWorker.DoWork += this.BackgroundWorker_DoWorkRemoveMediaPath;
                this.backgroundWorker.RunWorkerCompleted += this.BackgroundWorker_RunWorkerCompletedRemoveMediaPath;

                this.backgroundWorker.RunWorkerAsync(removeModels);

                this.EnableForm(false);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnProcessMovies control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnProcessMovies_Click(object sender, RoutedEventArgs e)
        {
            var importMoviesUc = new WndImportMovies();
            this.InvokeImportMoviesClicked(new EventArgs());
            importMoviesUc.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the BtnProcessTv control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnProcessTv_Click(object sender, RoutedEventArgs e)
        {
            var frmImportTv = new WndImportTv();
            frmImportTv.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the ButRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButRefresh_Click(object sender, RoutedEventArgs e)
        {
            MediaPathDBFactory.Instance.MediaPathMoviesUnsorted.Clear();
            MediaPathDBFactory.Instance.MediaPathTvUnsorted.Clear();

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
            var selectedRows = this.mediaPathList.SelectedItems;

            if (selectedRows.Count == 0)
            {
                return;
            }

            var frmEditMediaPath = new WndEditMediaPath(
                WndEditMediaPath.MediaPathActionType.Edit,
                (MediaPathModel)selectedRows[0]);

            frmEditMediaPath.ShowDialog();
        }

        /// <summary>
        /// The enable form.
        /// </summary>
        /// <param name="enable">
        /// The enable.
        /// </param>
        private void EnableForm(bool enable)
        {
            //this.btnRefresh.Enabled = enable;
            //this.btnAdd.Enabled = enable;
            //this.btnEdit.Enabled = enable;
            //this.btnDelete.Enabled = enable;
            //this.btnProcessMovies.Enabled = enable;
            //this.btnProcessTv.Enabled = enable;
            //this.btnClean.Enabled = enable;
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, RoutedEventArgs e)
        {
            //this.lblCurrentStatus.Text = this.currentProgress.Message;
            //this.progressBarControl.EditValue = this.currentProgress.Percent;
            //this.btnProcessTv.Enabled = this.grdViewUnsortedTv.RowCount > 0 && !this.backgroundWorker.IsBusy;
            //this.btnProcessMovies.Enabled = this.grdViewUnsortedMovies.RowCount > 0 && !this.backgroundWorker.IsBusy;
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.EditCurrentRow();
        }

        /// <summary>
        /// Handles the DoubleClick event of the grdViewMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void grdViewMain_DoubleClick(object sender, RoutedEventArgs e)
        {
            this.EditCurrentRow();
        }

        #endregion

        private void grdMediaPathList_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete && grdMediaPathList.Focused && grdViewMain.SelectedRowsCount > 0)
            //{
            //    RemoveMediaPaths();
            //}
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                string.Format(
                    "Are you sure you wish to clean the selected {0} media path(s)?\nNote that this will delete ALL files except movie and subtitles!",
                    this.mediaPathList.SelectedItems.Count),
                "Are you sure?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var cleanModels = new List<MediaPathModel>();

                foreach (MediaPathModel v in this.mediaPathList.SelectedItems)
                {
                    YANFOE.Tools.Clean.FileCleanUp.CleanFolder(v.MediaPath);
                }
            }
        }
    }
}
