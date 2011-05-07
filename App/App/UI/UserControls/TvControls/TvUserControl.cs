// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.TvControls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraGrid.Views.Grid.ViewInfo;

    using YANFOE.Factories;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Properties;
    using YANFOE.Settings;
    using YANFOE.UI.Dialogs.General;

    public partial class TvUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvUserControl"/> class.
        /// </summary>
        public TvUserControl()
        {
            InitializeComponent();

            this.picSeriesBanner.HeaderTitle = "Banner";

            this.picSeriesFanart.HeaderTitle = "Fanart";

            this.picSeriesPoster.HeaderTitle = "Poster";

            this.picEpisodeFrame.HeaderTitle = "Episode Frame";

            grdTvTitleList.DataSource = Factories.TvDBFactory.MasterSeriesNameList;

            TvDBFactory.GalleryChanged += this.TvDBFactory_GalleryChanged;
            TvDBFactory.GeneratePictureGallery();

            TvDBFactory.CurrentSeriesChanged += this.TvDBFactory_CurrentSeriesChanged;
            TvDBFactory.CurrentSeasonChanged += this.TvDBFactory_CurrentSeasonChanged;
            TvDBFactory.MasterSeriesNameListChanged += new EventHandler(TvDBFactory_MasterSeriesNameListChanged);
        }

        /// <summary>
        /// Handles the MasterSeriesNameListChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TvDBFactory_MasterSeriesNameListChanged(object sender, EventArgs e)
        {
            gridViewTvTitleList.RefreshData();
        }

        /// <summary>
        /// Handles the CurrentSeasonChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TvDBFactory_CurrentSeasonChanged(object sender, EventArgs e)
        {
            this.UpdateEpisodes();
        }

        /// <summary>
        /// Handles the CurrentSeriesChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void TvDBFactory_CurrentSeriesChanged(object sender, EventArgs e)
        {
            this.UpdateSeasons();
            this.UpdateEpisodes();
        }

        private void UpdateSeasons()
        {
            grdSeasons.DataSource = null;
            grdSeasons.DataSource = TvDBFactory.GetCurrentSeasonsList;

            var season = gridViewSeasons.GetRow(gridViewSeasons.GetSelectedRows()[0]) as Season;

            TvDBFactory.SetCurrentSeason(season.Guid);
        }

        private void UpdateEpisodes()
        {
            grdEpisodes.DataSource = null;
            grdEpisodes.DataSource = TvDBFactory.GetCurrentEpisodeList();
        }

        /// <summary>
        /// Handles the GalleryChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_GalleryChanged(object sender, EventArgs e)
        {
            this.galleryBanners.Gallery.Groups.Clear();
            this.galleryBanners.Gallery.Groups.Add(TvDBFactory.GetGalleryGroup());
            this.galleryBanners.Gallery.ItemClick += this.Gallery_ItemClick;
        }

        /// <summary>
        /// Handles the ItemClick event of the Gallery control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs"/> instance containing the event data.</param>
        private void Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            TvDBFactory.SetCurrentSeries(e.Item.Tag.ToString());
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the gridViewTvTitleList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewTvTitleList_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = gridViewTvTitleList.GetRow(e.FocusedRowHandle) as MasterSeriesListModel;

            if (row == null)
            {
                return;
            }

            TvDBFactory.SetCurrentSeries(row.SeriesGuid);

            TvDBFactory.DefaultCurrentSeasonAndEpisode();
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the gridViewSeasons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewSeasons_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var season = gridViewSeasons.GetRow(e.FocusedRowHandle) as Season;

            if (season == null)
            {
                return;
            }


            TvDBFactory.SetCurrentSeason(season.Guid);

            TvDBFactory.DefaultCurrentEpisode();
        }

        /// <summary>
        /// Handles the FocusedRowChanged event of the gridViewEpisodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewEpisodes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var episode = gridViewEpisodes.GetRow(e.FocusedRowHandle) as Episode;

            if (episode == null)
            {
                return;
            }

            TvDBFactory.SetCurrentEpisode(episode.Guid);
        }

        /// <summary>
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.</param>
        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl == grdTvTitleList)
            {
                GridHitInfo hi = gridViewTvTitleList.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    var series = gridViewTvTitleList.GetRow(hi.RowHandle) as MasterSeriesListModel;
                    e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty)
                        { SuperTip = TvDBFactory.GetSeriesSuperTip(series.SeriesGuid) };
                }
            }
            else if (e.SelectedControl == grdSeasons)
            {
                GridHitInfo hi = gridViewSeasons.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    var season = gridViewSeasons.GetRow(hi.RowHandle) as Season;
                    e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty) { SuperTip = TvDBFactory.GetSeasonSuperTip(season) };
                }
            }
            else if (e.SelectedControl == grdEpisodes)
            {
                GridHitInfo hi = gridViewEpisodes.CalcHitInfo(e.ControlMousePosition);
                if (hi.InRowCell)
                {
                    var episode = gridViewEpisodes.GetRow(hi.RowHandle) as Episode;
                    e.Info = new ToolTipControlInfo(hi + " " + hi.Column.Name + " " + hi.RowHandle, string.Empty)
                        {
                            SuperTip = TvDBFactory.GetEpisodeSuperTip(episode)
                        };
                }
            }
        }

        /// <summary>
        /// Handles the RowCellStyle event of the gridViewTvTitleList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void gridViewTvTitleList_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var row = gridViewTvTitleList.GetRow(e.RowHandle) as MasterSeriesListModel;

            if (row == null)
            {
                return;
            }

            var series = TvDBFactory.GetSeriesFromGuid(row.SeriesGuid);

            if (series.ChangedText || series.ChangedPoster || series.ChangedFanart || series.ChangedBanner)
            {
                e.Appearance.Font = Get.LookAndFeel.TextChanged;
            }
            else
            {
                e.Appearance.Font = Get.LookAndFeel.TextNormal;
            }
        }

        /// <summary>
        /// Handles the RowCellStyle event of the gridViewSeasons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void gridViewSeasons_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var season = gridViewSeasons.GetRow(e.RowHandle) as Season;

            if (season == null)
            {
                return;
            }

            if (season.ContainsChangedEpisodes())
            {
                e.Appearance.Font = YANFOE.Settings.Get.LookAndFeel.TextChanged;
            }
            else
            {
                e.Appearance.Font = YANFOE.Settings.Get.LookAndFeel.TextNormal;
            }
        }

        /// <summary>
        /// Handles the RowCellStyle event of the gridViewEpisodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs"/> instance containing the event data.</param>
        private void gridViewEpisodes_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var episode = gridViewEpisodes.GetRow(e.RowHandle) as Episode;

            if (episode == null)
            {
                return;
            }

            if (episode.ChangedText || episode.ChangedScreenshot)
            {
                e.Appearance.Font = YANFOE.Settings.Get.LookAndFeel.TextChanged;
            }
            else
            {
                e.Appearance.Font = Get.LookAndFeel.TextNormal;
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the gridViewTvTitleList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewTvTitleList_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var seriesList = new List<Series>();

            foreach (var row in gridViewTvTitleList.GetSelectedRows())
            {
                var seriesListModel = gridViewTvTitleList.GetRow(row) as MasterSeriesListModel;
                var series = TvDBFactory.GetSeriesFromGuid(seriesListModel.SeriesGuid);
                seriesList.Add(series);
            }

            TvDBFactory.CurrentSelectedSeries = seriesList;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the gridViewSeasons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewSeasons_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var seasonsList = new List<Season>();

            foreach (var row in gridViewSeasons.GetSelectedRows())
            {
                var season = gridViewSeasons.GetRow(row) as Season;
                seasonsList.Add(season);
            }

            TvDBFactory.CurrentSelectedSeason = seasonsList;
        }

        /// <summary>
        /// Handles the SelectionChanged event of the gridViewEpisodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewEpisodes_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            var episodeList = new List<Episode>();

            foreach (var row in gridViewEpisodes.GetSelectedRows())
            {
                var episode = gridViewEpisodes.GetRow(row) as Episode;
                episodeList.Add(episode);
            }

            TvDBFactory.CurrentSelectedEpisode = episodeList;
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Factories.InOut.OutFactory.RefreshGrids)
            {
                gridViewTvTitleList.RefreshData();
                gridViewSeasons.RefreshData();
                gridViewEpisodes.RefreshData();
                Factories.InOut.OutFactory.RefreshGrids = false;
            }
        }

        private void grdEpisodes_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            clmEpisodePath.Visible = Get.Ui.EnableTVPathColumn;
        }

        private void gridViewTvTitleList_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            var view = sender as GridView;
            e.Allow = false;

            this.popupSeries.ShowPopup(this.barManager1, view.GridControl.PointToScreen(e.Point));
        }

        private void popupSeries_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var rows = gridViewTvTitleList.GetSelectedRows();           
            rows.Select(row => this.gridViewTvTitleList.GetRow(row) as MasterSeriesListModel).ToList();

            popupSeries.ClearLinks();

            var addBarItem = new BarSubItem(barManager1, "Add Series");
            addBarItem.Glyph = Resources.add32;
            popupSeries.AddItem(addBarItem);

            addBarItem.AddItem(new BarButtonItem(barManager1, "Add TvDB Series"));
            addBarItem.AddItem(new BarButtonItem(barManager1, "Add Custom Series"));

            var updateBarItem = new BarSubItem(barManager1, "Update Series");
            updateBarItem.Glyph = Resources.globe32;
            popupSeries.AddItem(updateBarItem);

            updateBarItem.AddItem(new BarButtonItem(barManager1, "From TvDB"));
        }

        /// <summary>
        /// Handles the DragOver event of the grdEpisodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdEpisodes_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            object fileNameW = e.Data.GetData("FileNameW");
            if (fileNameW != null)
            {
                string[] fileNames = (string[])fileNameW;
                if (fileNames.Length == 1)
                {
                    string fileName = fileNames[0];

                    if (Get.InOutCollection.VideoExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the DragDrop event of the grdEpisodes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdEpisodes_DragDrop(object sender, DragEventArgs e)
        {
            GridHitInfo hitTest = this.gridViewEpisodes.CalcHitInfo(this.grdEpisodes.PointToClient(new Point(e.X, e.Y)));

            var episode = this.gridViewEpisodes.GetRow(hitTest.RowHandle) as Episode;

            if (hitTest.InRow)
            {
                var fileNameW = e.Data.GetData("FileNameW");

                if (fileNameW != null)
                {
                    string[] fileNames = (string[])fileNameW;
                    if (fileNames.Length == 1)
                    {
                        string fileName = fileNames[0];

                        if (Get.InOutCollection.VideoExtentions.Contains(Path.GetExtension(fileName.ToLower()).Replace(".", string.Empty)))
                        {
                            if (string.IsNullOrEmpty(episode.FilePath.FileNameAndPath))
                            {
                                episode.FilePath.FileNameAndPath = fileName;
                            }
                            else
                            {
                                var result =
                                    XtraMessageBox.Show(
                                        string.Format(
                                            "Do you want to replace\n{0}\nwith\n{1}",
                                            episode.FilePath.FileNameAndPath,
                                            fileName),
                                        "Are you sure to want to replace?",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    episode.FilePath.FileNameAndPath = fileName;
                                }
                            }

                            gridViewEpisodes.RefreshData();
                        }
                    }
                }
            }
        }

        private void btnAddCustomSeries_Click(object sender, EventArgs e)
        {
            TvDBFactory.CreateCustomSeries();
        }
    }
}
