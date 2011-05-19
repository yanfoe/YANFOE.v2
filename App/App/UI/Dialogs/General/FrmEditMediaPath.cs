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

    using YANFOE.Factories.Media;
    using YANFOE.Tools.IO;

    /// <summary>
    /// The frm edit media path.
    /// </summary>
    public partial class FrmEditMediaPath : XtraForm
    {
        public bool Confirmed;
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
        public FrmEditMediaPath(MediaPathActionType type)
        {
            this.InitializeComponent();

            this.actionType = type;

            this.SetupBindings();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The setup bindings.
        /// </summary>
        public void SetupBindings()
        {
            dxErrorProvider1.DataSource = MediaPathDBFactory.CurrentMediaPathEdit;

            this.txtMediaPath.DataBindings.Add("Text", MediaPathDBFactory.CurrentMediaPathEdit, "MediaPath", true);

            this.chkImportUsingFileName.DataBindings.Add("Checked", MediaPathDBFactory.CurrentMediaPathEdit, "ImportUsingFileName");

            this.chkImportUsingParentFolderName.DataBindings.Add("Checked", MediaPathDBFactory.CurrentMediaPathEdit, "ImportUsingParentFolderName");

            this.chkFolderContainsMovies.DataBindings.Add("Checked", MediaPathDBFactory.CurrentMediaPathEdit, "ContainsMovies");

            this.chkFolderContainsTvShows.DataBindings.Add("Checked", MediaPathDBFactory.CurrentMediaPathEdit, "ContainsTv");

            cmbScraperGroup.DataBindings.Add("Text", MediaPathDBFactory.CurrentMediaPathEdit, "ScraperGroup");

            cmbSource.DataBindings.Add("Text", MediaPathDBFactory.CurrentMediaPathEdit, "DefaultSource");

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
                MediaPathDBFactory.CurrentMediaPathEdit.MediaPath = dialog.SelectedPath;
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

            this.Close();

            if (actionType == MediaPathActionType.Add)
            {
                MediaPathDBFactory.CommitNewRecord();
            }
            else if (actionType == MediaPathActionType.Edit)
            {
                MediaPathDBFactory.CommitChangedRecord();    
            }

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
    }
}