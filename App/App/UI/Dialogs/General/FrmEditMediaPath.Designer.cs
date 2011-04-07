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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEditMediaPath));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.chkFolderContainsMovies = new DevExpress.XtraEditors.CheckEdit();
            this.chkFolderContainsTvShows = new DevExpress.XtraEditors.CheckEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.cmbSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbScraperGroup = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chkImportUsingFileName = new DevExpress.XtraEditors.CheckEdit();
            this.chkImportUsingParentFolderName = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnMediaPathBrowse = new DevExpress.XtraEditors.SimpleButton();
            this.txtMediaPath = new DevExpress.XtraEditors.TextEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsMovies.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFolderContainsTvShows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScraperGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingFileName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingParentFolderName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.groupControl4);
            this.groupControl1.Controls.Add(this.groupControl3);
            this.groupControl1.Controls.Add(this.groupControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.btnMediaPathBrowse);
            this.groupControl1.Controls.Add(this.txtMediaPath);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(500, 272);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Import Details";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.chkFolderContainsMovies);
            this.groupControl4.Controls.Add(this.chkFolderContainsTvShows);
            this.groupControl4.Controls.Add(this.memoEdit1);
            this.groupControl4.Location = new System.Drawing.Point(13, 73);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(475, 118);
            this.groupControl4.TabIndex = 7;
            this.groupControl4.Text = "Import Type";
            // 
            // chkFolderContainsMovies
            // 
            this.chkFolderContainsMovies.Location = new System.Drawing.Point(245, 83);
            this.chkFolderContainsMovies.Name = "chkFolderContainsMovies";
            this.chkFolderContainsMovies.Properties.Caption = "Folder Contains Movies";
            this.chkFolderContainsMovies.Size = new System.Drawing.Size(157, 19);
            this.chkFolderContainsMovies.TabIndex = 2;
            // 
            // chkFolderContainsTvShows
            // 
            this.chkFolderContainsTvShows.Location = new System.Drawing.Point(64, 83);
            this.chkFolderContainsTvShows.Name = "chkFolderContainsTvShows";
            this.chkFolderContainsTvShows.Properties.Caption = "Folder Contains TV Shows";
            this.chkFolderContainsTvShows.Size = new System.Drawing.Size(157, 19);
            this.chkFolderContainsTvShows.TabIndex = 1;
            // 
            // memoEdit1
            // 
            this.memoEdit1.EditValue = resources.GetString("memoEdit1.EditValue");
            this.memoEdit1.Location = new System.Drawing.Point(7, 26);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(463, 50);
            this.memoEdit1.TabIndex = 0;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.cmbSource);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.cmbScraperGroup);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Location = new System.Drawing.Point(225, 197);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(263, 78);
            this.groupControl3.TabIndex = 6;
            this.groupControl3.Text = "Defaults";
            // 
            // cmbSource
            // 
            this.cmbSource.Location = new System.Drawing.Point(141, 46);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSource.Size = new System.Drawing.Size(117, 20);
            this.cmbSource.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(141, 25);
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
            this.cmbScraperGroup.Size = new System.Drawing.Size(117, 20);
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
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkImportUsingFileName);
            this.groupControl2.Controls.Add(this.chkImportUsingParentFolderName);
            this.groupControl2.Location = new System.Drawing.Point(13, 197);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(205, 78);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "Movie Naming";
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
            this.chkImportUsingFileName.CheckedChanged += new System.EventHandler(this.chkImportUsingFileName_CheckedChanged);
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
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOK.Image = global::YANFOE.Properties.Resources.accept32;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(351, 272);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 52);
            this.btnOK.TabIndex = 1;
            this.btnOK.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Image = global::YANFOE.Properties.Resources.delete32;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(425, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 52);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // FrmEditMediaPath
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 324);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
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
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScraperGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingFileName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportUsingParentFolderName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMediaPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnMediaPathBrowse;
        private DevExpress.XtraEditors.TextEdit txtMediaPath;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSource;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbScraperGroup;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit chkImportUsingFileName;
        private DevExpress.XtraEditors.CheckEdit chkImportUsingParentFolderName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.CheckEdit chkFolderContainsMovies;
        private DevExpress.XtraEditors.CheckEdit chkFolderContainsTvShows;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}