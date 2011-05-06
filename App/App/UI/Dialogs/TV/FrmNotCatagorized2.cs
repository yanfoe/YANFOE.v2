// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmProcessingMoviesStatus.cs" company="The YANFOE Project">
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
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;

    using YANFOE.Factories;
    using YANFOE.Factories.Import;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Scrapers.TV;

    /// <summary>
    /// The frm not catagorized 2.
    /// </summary>
    public partial class FrmNotCatagorized2 : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        /// The down hit info.
        /// </summary>
        private GridHitInfo downHitInfo;

        private Timer tmr;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmNotCatagorized2"/> class.
        /// </summary>
        public FrmNotCatagorized2()
        {
            this.InitializeComponent();

            this.grdUnsorted.DataSource = ImportTvFactory.NotCatagorized;

            this.UpdateDataBindings();

            this.tmr = new Timer();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The replace show.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="notCatagorized">The not catagorized.</param>
        private void ReplaceShow(Episode episode, ScanNotCatagorized notCatagorized)
        {
            episode.FilePath.FileNameAndPath = notCatagorized.FilePath;
            ImportTvFactory.NotCatagorized.Remove(notCatagorized);
            this.grdViewEpisodes.RefreshData();
            this.lblStatus.Text = string.Format("{0} set to {1}", notCatagorized.FilePath, episode.EpisodeName);
        }

        /// <summary>
        /// The update data bindings.
        /// </summary>
        private void UpdateDataBindings()
        {
            this.grdSeries.DataSource = (from s in TvDBFactory.TvDatabase orderby s.Key select s.Value).ToList();
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += this.bgw_DoWork;
            bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;


            this.tmr.Tick += this.tmr_Tick;
            this.tmr.Interval = 100;

            this.tmr.Start();

            this.btnOk.Enabled = false;
            bgw.RunWorkerAsync();

        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = TvDBFactory.UpdateStatus;
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            TvDBFactory.ProcessDatabaseUpdate();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.tmr.Stop();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
            this.txtSearch.Enabled = false;

            var theTvdb = new TheTvdb();
            theTvdb.DoFullSearch(this.txtSearch.Text);
            this.UpdateDataBindings();
            
            this.btnSearch.Enabled = true;
            this.txtSearch.Enabled = true;
            this.txtSearch.Text = string.Empty;
        }

        /// <summary>
        /// Handles the DragDrop event of the grdEpisode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdEpisode_DragDrop(object sender, DragEventArgs e)
        {
            GridHitInfo hitTest = this.grdViewEpisodes.CalcHitInfo(this.grdEpisode.PointToClient(new Point(e.X, e.Y)));

            if (hitTest.InRow)
            {
                var notCatagorized = e.Data.GetData(typeof(ScanNotCatagorized)) as ScanNotCatagorized;
                var episode = this.grdViewEpisodes.GetRow(hitTest.RowHandle) as Episode;
                var fileNameW = e.Data.GetData("FileNameW");

                if (episode != null && notCatagorized != null || fileNameW != null)
                {
                    if (fileNameW != null)
                    {
                        string[] fileNames = (string[])fileNameW;
                        if (fileNames.Length == 1)
                        {
                            string fileName = fileNames[0];

                            if (Settings.Get.InOutCollection.VideoExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                            {
                                notCatagorized = new ScanNotCatagorized();
                                notCatagorized.FilePath = fileName;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(episode.FilePath.FileNameAndPath))
                    {
                        this.ReplaceShow(episode, notCatagorized);
                    }
                    else
                    {
                        var result =
                            XtraMessageBox.Show(
                                string.Format(
                                    "Do you want to replace\n{0}\nwith\n{1}", 
                                    episode.FilePath.FileNameAndPath, 
                                    notCatagorized.FilePath), 
                                "Are you sure to want to replace?", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            this.ReplaceShow(episode, notCatagorized);
                        }
                    }
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the DragOver event of the grdEpisode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdEpisode_DragOver(object sender, DragEventArgs e)
        {
            object fileNameW = e.Data.GetData("FileNameW");
            if (fileNameW != null)
            {
                string[] fileNames = (string[])fileNameW;
                if (fileNames.Length == 1)
                {
                    string fileName = fileNames[0];

                    if (Settings.Get.InOutCollection.VideoExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }

            }
            else
            {

                e.Effect = e.Data.GetDataPresent(typeof(ScanNotCatagorized))
                               ? DragDropEffects.Move
                               : DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the grdViewSeasons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void grdViewSeasons_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var season = this.grdViewSeasons.GetRow(e.FocusedRowHandle) as Season;

            if (season != null)
            {
                this.grdEpisode.DataSource = season.Episodes;
            }
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the grdViewSeries control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void grdViewSeries_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var series = this.grdViewSeries.GetRow(e.FocusedRowHandle) as Series;

            if (series != null)
            {
                this.grdSeasons.DataSource =
                    (from s in series.Seasons orderby s.Value.SeasonNumber select s.Value).ToList();
            }
        }

        /// <summary>
        /// Handles the MouseDown event of the grdViewUnsorted control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.
        /// </param>
        private void grdViewUnsorted_MouseDown(object sender, MouseEventArgs e)
        {
            var view = sender as GridView;

            this.downHitInfo = null;

            var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (ModifierKeys != Keys.None)
            {
                return;
            }

            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                this.downHitInfo = hitInfo;
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the grdViewUnsorted control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.
        /// </param>
        private void grdViewUnsorted_MouseMove(object sender, MouseEventArgs e)
        {
            var view = sender as GridView;

            if (e.Button == MouseButtons.Left && this.downHitInfo != null)
            {
                var dragSize = SystemInformation.DragSize;
                var dragRect =
                    new Rectangle(
                        new Point(
                            this.downHitInfo.HitPoint.X - dragSize.Width / 2, 
                            this.downHitInfo.HitPoint.Y - dragSize.Height / 2), 
                        dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    var notCatagorized = view.GetRow(this.downHitInfo.RowHandle) as ScanNotCatagorized;

                    this.lblStatus.Text = string.Format("{0} selected.", notCatagorized.FilePath);
                    view.GridControl.DoDragDrop(notCatagorized, DragDropEffects.Move);
                    this.downHitInfo = null;
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        /// <summary>
        /// The grd view unsorted_ popup menu showing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void grdViewUnsorted_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            var view = sender as GridView;

            e.Allow = false;

            this.popupUnsortedList.ShowPopup(this.barManager1, view.GridControl.PointToScreen(e.Point));
        }

        /// <summary>
        /// The item_ item click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            var rows = this.grdViewUnsorted.GetSelectedRows();
            var seriesName = (e.Item as BarButtonItem).Tag.ToString();
            var series = TvDBFactory.GetSeriesFromName(seriesName);
            var failedList = new List<string>();
            var successCount = 0;

            var sb = new StringBuilder();
            var failed = new StringBuilder();

            if (series != null)
            {
                var assignList = new List<ScanNotCatagorized>();

                foreach (int row in rows)
                {
                    var fileObj = this.grdViewUnsorted.GetRow(row) as ScanNotCatagorized;

                    assignList.Add(fileObj);
                }

                foreach (var fileObj in assignList)
                {

                    var episodeDetails = ImportTvFactory.GetEpisodeDetails(fileObj.FilePath);

                    if (episodeDetails.TvMatchSuccess)
                    {

                        try
                        {
                            this.ReplaceShow(series.Seasons[episodeDetails.SeasonNumber].Episodes[episodeDetails.EpisodeNumber - 1], fileObj);
                        }
                        catch (Exception)
                        {
                            failed.AppendLine(fileObj.FilePath);
                        }


                        if (successCount < 10)
                        {
                            sb.AppendLine(
                         string.Format(
                            "{0} -> {3} s{1}e{2}", 
                            Path.GetFileName(fileObj.FilePath), 
                            episodeDetails.SeasonNumber, 
                            episodeDetails.EpisodeNumber, 
                            seriesName));
                        }

                        successCount++;
                    }
                    else
                    {
                        failedList.Add(episodeDetails.FilePath);
                    }
                }

                XtraMessageBox.Show(
                    string.Format("{0} files successfully assigned.{1}{2}{1}{1}Failed{1}{3}", successCount, Environment.NewLine, sb.ToString(), failedList.Count));
            }
        }

        /// <summary>
        /// The popup unsorted list_ before popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void popupUnsortedList_BeforePopup(object sender, CancelEventArgs e)
        {
            this.mnuAssignTo.ClearLinks();

            foreach (var series in TvDBFactory.TvDatabase)
            {
                var item = new BarButtonItem(this.barManager1, series.Key);
                item.Tag = series.Key;
                item.ItemClick += this.item_ItemClick;
                this.mnuAssignTo.AddItem(item);
            }
        }

        /// <summary>
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.</param>
        private void toolTipController1_GetActiveObjectInfo(
            object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl == this.grdEpisode)
            {
                var hi = this.grdViewEpisodes.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    var episode = this.grdViewEpisodes.GetRow(hi.RowHandle) as Episode;

                    var superTip = new SuperToolTip();
                    superTip.Items.AddTitle(episode.CurrentFilenameAndPath);

                    e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty)
                        {
                           SuperTip = superTip 
                        };
                }
            }
        }

        #endregion

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (ModifierKeys == Keys.Return)
            {
                this.btnSearch_Click(null, null);
            }
        }
    }
}