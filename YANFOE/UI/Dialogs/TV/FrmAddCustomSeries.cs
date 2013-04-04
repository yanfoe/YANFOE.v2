// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmAddCustomSeries.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the FrmAddCustomSeries type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.TV
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    using DevExpress.XtraGrid.Views.Grid.ViewInfo;

    using YANFOE.Factories;
    using YANFOE.Factories.AddCustomSeries;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;

    public partial class FrmAddCustomSeries : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmAddCustomSeries"/> class.
        /// </summary>
        public FrmAddCustomSeries()
        {
            InitializeComponent();

            AddCustomSeriesFactory.Reset();

            this.SetupBindings();
        }

        /// <summary>
        /// Setups the databindings.
        /// </summary>
        private void SetupBindings()
        {
            AddCustomSeriesFactory.UpdateSeriesList += this.AddCustomSeriesFactory_UpdateSeriesList;
            AddCustomSeriesFactory.SeriesChanged += this.AddCustomSeriesFactory_SeriesChanged;

            dxErrorProvider1.DataSource = AddCustomSeriesFactory.NewSeriesModel;

            txtNewSeriesName.DataBindings.Add("Text", AddCustomSeriesFactory.NewSeriesModel, "NewSeriesName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSeasons.DataBindings.Add("Value", AddCustomSeriesFactory.NewSeriesModel, "Series", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAddSeason0.DataBindings.Add("Checked", AddCustomSeriesFactory.NewSeriesModel, "AddSeriesZero", true, DataSourceUpdateMode.OnPropertyChanged);

            grdSeasons.DataSource = AddCustomSeriesFactory.NewSeriesModel.SeriesList;
        }

        /// <summary>
        /// Handles the SeriesChanged event of the AddCustomSeriesFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void AddCustomSeriesFactory_SeriesChanged(object sender, EventArgs e)
        {
            grdFiles.DataSource = AddCustomSeriesFactory.GetSeasonFiles();
        }

        /// <summary>
        /// Handles the UpdateSeriesList event of the AddCustomSeriesFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddCustomSeriesFactory_UpdateSeriesList(object sender, EventArgs e)
        {
            grdViewSeasons.RefreshData();
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            var series = AddCustomSeriesFactory.GenerateSeries();

            TvDBFactory.AddCustomSeries(series);

            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the DragDrop event of the grdFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdFiles_DragDrop(object sender, DragEventArgs e)
        {
            var fileNameW = (string[])e.Data.GetData(DataFormats.FileDrop);

            var filteredFiles = new List<string>();

            if (fileNameW != null)
            {
                var fileNames = (string[])fileNameW;

                foreach (var file in fileNames)
                {
                    if (
                        Get.InOutCollection.VideoExtentions.Contains(
                            Path.GetExtension(file.ToLower()).Replace(".", string.Empty)))
                    {
                        filteredFiles.Add(file);
                    }
                }

                AddCustomSeriesFactory.AddFiles(filteredFiles);
            }
        }

        /// <summary>
        /// Handles the DragOver event of the grdFiles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DragEventArgs"/> instance containing the event data.</param>
        private void grdFiles_DragOver(object sender, DragEventArgs e)
        {
            object fileNameW = e.Data.GetData("FileNameW");
            if (fileNameW != null)
            {
                var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (fileNames.Length > 0)
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
        /// Handles the FocusedRowChanged event of the grdViewSeasons control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs"/> instance containing the event data.</param>
        private void grdViewSeasons_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var selectedRows = grdViewSeasons.GetSelectedRows();

            if (selectedRows.Length == 0)
            {
                return;
            }

            var row = grdViewSeasons.GetRow(selectedRows[0]) as string;
            AddCustomSeriesFactory.ChangeSelectedSeries(row);
            AddCustomSeriesFactory.InvokeSeriesChanged(new EventArgs());
        }

        private void grdViewFiles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.btnRemoveFiles.Enabled = this.grdViewFiles.GetSelectedRows().Length > 0;
        }

        private void btnAddBlankShow_Click(object sender, EventArgs e)
        {
            AddCustomSeriesFactory.AddBlankToCurrentSeries();
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
        }
    }
}