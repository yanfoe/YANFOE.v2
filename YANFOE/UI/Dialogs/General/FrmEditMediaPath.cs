// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmEditMediaPath.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.General
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.IO;

    /// <summary>
    /// The frm edit media path.
    /// </summary>
    public partial class FrmEditMediaPath : XtraForm
    {
        public bool Confirmed;
        private MediaPathModel editingMediaPathModel = new MediaPathModel();

        private MediaPathActionType actionType;

        #region Constructors and Destructors

        public enum MediaPathActionType
        {
            Add,
            Edit
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmEditMediaPath"/> class.
        /// </summary>
        public FrmEditMediaPath(MediaPathActionType type, MediaPathModel mediaPathModel)
        {
            this.InitializeComponent();

            this.actionType = type;
            this.editingMediaPathModel = mediaPathModel;

            this.SetupBindings();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The setup bindings.
        /// </summary>
        public void SetupBindings()
        {
            dxErrorProvider1.DataSource = this.editingMediaPathModel;

            this.txtMediaPath.DataBindings.Add("Text", this.editingMediaPathModel, "MediaPath", true);
            this.chkImportUsingFileName.DataBindings.Add("Checked", this.editingMediaPathModel, "ImportUsingFileName", true);
            this.chkImportUsingParentFolderName.DataBindings.Add("Checked", this.editingMediaPathModel, "ImportUsingParentFolderName", true);
            this.chkFolderContainsMovies.DataBindings.Add("Checked", this.editingMediaPathModel, "ContainsMovies", true, DataSourceUpdateMode.OnPropertyChanged);
            this.chkFolderContainsTvShows.DataBindings.Add("Checked", this.editingMediaPathModel, "ContainsTv", true, DataSourceUpdateMode.OnPropertyChanged);

            cmbScraperGroup.DataBindings.Add("Text", this.editingMediaPathModel, "ScraperGroup", true);
            cmbSource.DataBindings.Add("Text", this.editingMediaPathModel, "DefaultSource", true);

            Factories.Scraper.MovieScraperGroupFactory.GetScraperGroupsOnDisk(cmbScraperGroup);

            this.FillSource();
        }

        #endregion

        #region Methods

        public void FillSource()
        {
            foreach (var source in Settings.Get.Keywords.Sources)
            {
                cmbSource.Properties.Items.Add(source.Key);
            }
        }

        /// <summary>
        /// BTNs the cancel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// BTNs the media path browse click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnMediaPathBrowseClick(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            dialog.SelectedPath = txtMediaPath.Text;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.editingMediaPathModel.MediaPath = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// BTNs the ok click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            if (chkFolderContainsMovies.Checked == false && chkFolderContainsTvShows.Checked == false)
            {
                XtraMessageBox.Show("Please select an import type");
                return;
            }

            switch (actionType)
            {
                case MediaPathActionType.Add:
                    // This can probably be prettier
                    foreach (var model in MediaPathDBFactory.MediaPathDB)
                    {
                        if (model.MediaPath == this.editingMediaPathModel.MediaPath)
                        {
                            XtraMessageBox.Show("You can't add the same source twice!");
                            return;
                        }
                    }
                    MediaPathDBFactory.MediaPathDB.Add(this.editingMediaPathModel);
                    break;
            }

            MediaPathDBFactory.CurrentMediaPath = this.editingMediaPathModel;

            this.Close();

            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.MediaPathDb);

            ImportFiles.ScanMediaPath(MediaPathDBFactory.CurrentMediaPath);

        }

        #endregion

        /// <summary>
        /// Handles the TextChanged event of the txtMediaPath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtMediaPath_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = Directory.Exists(txtMediaPath.Text);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkFolderContainsMovies control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkFolderContainsMovies_CheckedChanged(object sender, EventArgs e)
        {
            groupMovieNaming.Enabled = chkFolderContainsMovies.Checked;
            groupMovieDefaults.Enabled = chkFolderContainsMovies.Checked;
        }

        /// <summary>
        /// Handles the Click event of the chkFolderContainsMovies control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkFolderContainsMovies_Click(object sender, EventArgs e)
        {
            chkFolderContainsMovies.Checked = true;
        }

        /// <summary>
        /// Handles the Click event of the chkFolderContainsTvShows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkFolderContainsTvShows_Click(object sender, EventArgs e)
        {
            chkFolderContainsTvShows.Checked = true;

            chkImportUsingFileName.Checked = false;
            chkImportUsingParentFolderName.Checked = false;

            cmbSource.Text = string.Empty;
            cmbScraperGroup.Text = string.Empty;
        }
    }
}