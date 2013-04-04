namespace YANFOE.UI.Dialogs.General
{
    partial class FrmEditMediaPath
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.chkFolderContainsMovies = new DevExpress.XtraEditors.CheckEdit();
            this.chkFolderContainsTvShows = new DevExpress.XtraEditors.CheckEdit();
            this.groupMovieDefaults = new DevExpress.XtraEditors.GroupControl();
            this.cmbSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbScraperGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupMovieNaming = new DevExpress.XtraEditors.GroupControl();
            this.chkImportUsingFileName = new DevExpress.XtraEditors.CheckEdit();
            this.chkImportUsingParentFolderName = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnMediaPathBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtMediaPath = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsMovies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsTvShows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMovieDefaults)).BeginInit();
            this.groupMovieDefaults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScraperGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMovieNaming)).BeginInit();
            this.groupMovieNaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingParentFolderName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnCancel);
            this.groupControl1.Controls.Add(this.btnOK);
            this.groupControl1.Controls.Add(this.groupControl4);
            this.groupControl1.Controls.Add(this.groupMovieDefaults);
            this.groupControl1.Controls.Add(this.groupMovieNaming);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnMediaPathBrowse);
            this.groupControl1.Controls.Add(this.txtMediaPath);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(496, 236);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Import Details";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::YANFOE.Properties.Resources.delete32;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(420, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 50);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnOK
            // 
            this.btnOK.Image = global::YANFOE.Properties.Resources.accept32;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(421, 121);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 49);
            this.btnOK.TabIndex = 8;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.chkFolderContainsMovies);
            this.groupControl4.Controls.Add(this.chkFolderContainsTvShows);
            this.groupControl4.Location = new System.Drawing.Point(13, 73);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(204, 70);
            this.groupControl4.TabIndex = 7;
            this.groupControl4.Text = "Import Type";
            // 
            // chkFolderContainsMovies
            // 
            this.chkFolderContainsMovies.Location = new System.Drawing.Point(5, 46);
            this.chkFolderContainsMovies.Name = "chkFolderContainsMovies";
            this.chkFolderContainsMovies.Properties.Caption = "Folder Contains Movies";
            this.chkFolderContainsMovies.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkFolderContainsMovies.Properties.RadioGroupIndex = 1;
            this.chkFolderContainsMovies.Size = new System.Drawing.Size(157, 19);
            this.chkFolderContainsMovies.TabIndex = 2;
            this.chkFolderContainsMovies.TabStop = false;
            this.chkFolderContainsMovies.CheckedChanged += new System.EventHandler(this.chkFolderContainsMovies_CheckedChanged);
            this.chkFolderContainsMovies.Click += new System.EventHandler(this.chkFolderContainsMovies_Click);
            // 
            // chkFolderContainsTvShows
            // 
            this.chkFolderContainsTvShows.Location = new System.Drawing.Point(5, 25);
            this.chkFolderContainsTvShows.Name = "chkFolderContainsTvShows";
            this.chkFolderContainsTvShows.Properties.Caption = "Folder Contains TV Shows";
            this.chkFolderContainsTvShows.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkFolderContainsTvShows.Properties.RadioGroupIndex = 1;
            this.chkFolderContainsTvShows.Size = new System.Drawing.Size(157, 19);
            this.chkFolderContainsTvShows.TabIndex = 1;
            this.chkFolderContainsTvShows.TabStop = false;
            this.chkFolderContainsTvShows.Click += new System.EventHandler(this.chkFolderContainsTvShows_Click);
            // 
            // groupMovieDefaults
            // 
            this.groupMovieDefaults.Controls.Add(this.cmbSource);
            this.groupMovieDefaults.Controls.Add(this.labelControl3);
            this.groupMovieDefaults.Controls.Add(this.cmbScraperGroup);
            this.groupMovieDefaults.Controls.Add(this.labelControl2);
            this.groupMovieDefaults.Enabled = false;
            this.groupMovieDefaults.Location = new System.Drawing.Point(225, 73);
            this.groupMovieDefaults.Name = "groupMovieDefaults";
            this.groupMovieDefaults.Size = new System.Drawing.Size(190, 130);
            this.groupMovieDefaults.TabIndex = 6;
            this.groupMovieDefaults.Text = "Movie Defaults";
            // 
            // cmbSource
            // 
            this.cmbSource.Location = new System.Drawing.Point(6, 95);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSource.Size = new System.Drawing.Size(175, 20);
            this.cmbSource.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(6, 76);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(37, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Source:";
            // 
            // cmbScraperGroup
            // 
            this.cmbScraperGroup.Location = new System.Drawing.Point(6, 46);
            this.cmbScraperGroup.Name = "cmbScraperGroup";
            this.cmbScraperGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbScraperGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbScraperGroup.Size = new System.Drawing.Size(175, 20);
            this.cmbScraperGroup.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(104, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Movie Scraper Group:";
            // 
            // groupMovieNaming
            // 
            this.groupMovieNaming.Controls.Add(this.chkImportUsingFileName);
            this.groupMovieNaming.Controls.Add(this.chkImportUsingParentFolderName);
            this.groupMovieNaming.Enabled = false;
            this.groupMovieNaming.Location = new System.Drawing.Point(12, 149);
            this.groupMovieNaming.Name = "groupMovieNaming";
            this.groupMovieNaming.Size = new System.Drawing.Size(205, 78);
            this.groupMovieNaming.TabIndex = 5;
            this.groupMovieNaming.Text = "Movie Naming";
            // 
            // chkImportUsingFileName
            // 
            this.chkImportUsingFileName.Location = new System.Drawing.Point(5, 25);
            this.chkImportUsingFileName.Name = "chkImportUsingFileName";
            this.chkImportUsingFileName.Properties.Caption = "Import using file name.";
            this.chkImportUsingFileName.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkImportUsingFileName.Properties.RadioGroupIndex = 1;
            this.chkImportUsingFileName.Size = new System.Drawing.Size(141, 19);
            this.chkImportUsingFileName.TabIndex = 3;
            this.chkImportUsingFileName.TabStop = false;
            // 
            // chkImportUsingParentFolderName
            // 
            this.chkImportUsingParentFolderName.Location = new System.Drawing.Point(5, 50);
            this.chkImportUsingParentFolderName.Name = "chkImportUsingParentFolderName";
            this.chkImportUsingParentFolderName.Properties.Caption = "Import using parent folder name";
            this.chkImportUsingParentFolderName.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkImportUsingParentFolderName.Properties.RadioGroupIndex = 1;
            this.chkImportUsingParentFolderName.Size = new System.Drawing.Size(195, 19);
            this.chkImportUsingParentFolderName.TabIndex = 4;
            this.chkImportUsingParentFolderName.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(13, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(113, 16);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Select a Media Path";
            // 
            // btnMediaPathBrowse
            // 
            this.btnMediaPathBrowse.Image = global::YANFOE.Properties.Resources.folder32;
            this.btnMediaPathBrowse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMediaPathBrowse.Location = new System.Drawing.Point(421, 26);
            this.btnMediaPathBrowse.Name = "btnMediaPathBrowse";
            this.btnMediaPathBrowse.Size = new System.Drawing.Size(67, 40);
            this.btnMediaPathBrowse.TabIndex = 1;
            this.btnMediaPathBrowse.Click += new System.EventHandler(this.BtnMediaPathBrowseClick);
            // 
            // txtMediaPath
            // 
            this.txtMediaPath.AllowDrop = true;
            this.txtMediaPath.Location = new System.Drawing.Point(13, 46);
            this.txtMediaPath.Name = "txtMediaPath";
            this.txtMediaPath.Size = new System.Drawing.Size(402, 20);
            this.txtMediaPath.TabIndex = 0;
            this.txtMediaPath.TextChanged += new System.EventHandler(this.txtMediaPath_TextChanged);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // FrmEditMediaPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(496, 235);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmEditMediaPath";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "YANFOE: Import Details";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsMovies.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsTvShows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMovieDefaults)).EndInit();
            this.groupMovieDefaults.ResumeLayout(false);
            this.groupMovieDefaults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScraperGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupMovieNaming)).EndInit();
            this.groupMovieNaming.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingParentFolderName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnMediaPathBrowse;
        private DevExpress.XtraEditors.TextEdit txtMediaPath;
        private DevExpress.XtraEditors.GroupControl groupMovieDefaults;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSource;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbScraperGroup;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupMovieNaming;
        private DevExpress.XtraEditors.CheckEdit chkImportUsingFileName;
        private DevExpress.XtraEditors.CheckEdit chkImportUsingParentFolderName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.CheckEdit chkFolderContainsMovies;
        private DevExpress.XtraEditors.CheckEdit chkFolderContainsTvShows;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}