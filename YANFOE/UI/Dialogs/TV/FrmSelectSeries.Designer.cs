namespace YANFOE.UI.Dialogs.TV
{
    partial class FrmSelectSeries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectSeries));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbSearchResults = new DevExpress.XtraEditors.ListBoxControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblFirstAired = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.tbOverview = new System.Windows.Forms.TextBox();
            this.lblLanguage = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.lblSeriesName = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearchAgain = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.butSearchAgain = new DevExpress.XtraEditors.SimpleButton();
            this.butUse = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lblSearchTerm = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblSeriesPath = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchAgain.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblSeriesPath);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.cmbSearchResults);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Controls.Add(this.butCancel);
            this.groupControl1.Controls.Add(this.txtSearchAgain);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.butSearchAgain);
            this.groupControl1.Controls.Add(this.butUse);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lblSearchTerm);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(579, 313);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Identify this series";
            // 
            // cmbSearchResults
            // 
            this.cmbSearchResults.Location = new System.Drawing.Point(5, 131);
            this.cmbSearchResults.Name = "cmbSearchResults";
            this.cmbSearchResults.Size = new System.Drawing.Size(246, 84);
            this.cmbSearchResults.TabIndex = 17;
            this.cmbSearchResults.SelectedValueChanged += new System.EventHandler(this.CmbSearchResults_SelectedValueChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.lblFirstAired);
            this.panelControl2.Controls.Add(this.labelControl10);
            this.panelControl2.Controls.Add(this.tbOverview);
            this.panelControl2.Controls.Add(this.lblLanguage);
            this.panelControl2.Controls.Add(this.labelControl8);
            this.panelControl2.Controls.Add(this.lblSeriesName);
            this.panelControl2.Controls.Add(this.labelControl5);
            this.panelControl2.Controls.Add(this.pictureBox1);
            this.panelControl2.Location = new System.Drawing.Point(258, 25);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(310, 238);
            this.panelControl2.TabIndex = 16;
            // 
            // lblFirstAired
            // 
            this.lblFirstAired.Location = new System.Drawing.Point(64, 220);
            this.lblFirstAired.Name = "lblFirstAired";
            this.lblFirstAired.Size = new System.Drawing.Size(73, 13);
            this.lblFirstAired.TabIndex = 7;
            this.lblFirstAired.Text = "<series name>";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(5, 220);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(53, 13);
            this.labelControl10.TabIndex = 6;
            this.labelControl10.Text = "First Aired:";
            // 
            // tbOverview
            // 
            this.tbOverview.BackColor = System.Drawing.Color.White;
            this.tbOverview.Location = new System.Drawing.Point(6, 87);
            this.tbOverview.Multiline = true;
            this.tbOverview.Name = "tbOverview";
            this.tbOverview.ReadOnly = true;
            this.tbOverview.Size = new System.Drawing.Size(299, 126);
            this.tbOverview.TabIndex = 5;
            // 
            // lblLanguage
            // 
            this.lblLanguage.Location = new System.Drawing.Point(293, 67);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(12, 13);
            this.lblLanguage.TabIndex = 4;
            this.lblLanguage.Text = "en";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(236, 67);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(51, 13);
            this.labelControl8.TabIndex = 3;
            this.labelControl8.Text = "Language:";
            // 
            // lblSeriesName
            // 
            this.lblSeriesName.Location = new System.Drawing.Point(75, 67);
            this.lblSeriesName.Name = "lblSeriesName";
            this.lblSeriesName.Size = new System.Drawing.Size(73, 13);
            this.lblSeriesName.TabIndex = 2;
            this.lblSeriesName.Text = "<series name>";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(6, 67);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(63, 13);
            this.labelControl5.TabIndex = 1;
            this.labelControl5.Text = "Series Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 55);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // butCancel
            // 
            this.butCancel.Image = global::YANFOE.Properties.Resources.delete32;
            this.butCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.butCancel.Location = new System.Drawing.Point(412, 269);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(74, 39);
            this.butCancel.TabIndex = 15;
            this.butCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // txtSearchAgain
            // 
            this.txtSearchAgain.Location = new System.Drawing.Point(9, 243);
            this.txtSearchAgain.Name = "txtSearchAgain";
            this.txtSearchAgain.Size = new System.Drawing.Size(242, 20);
            this.txtSearchAgain.TabIndex = 14;
            this.txtSearchAgain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtSearchAgain_KeyPress);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(9, 225);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(67, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Search Again:";
            // 
            // butSearchAgain
            // 
            this.butSearchAgain.Image = global::YANFOE.Properties.Resources.globe32;
            this.butSearchAgain.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.butSearchAgain.Location = new System.Drawing.Point(184, 269);
            this.butSearchAgain.Name = "butSearchAgain";
            this.butSearchAgain.Size = new System.Drawing.Size(67, 39);
            this.butSearchAgain.TabIndex = 12;
            this.butSearchAgain.Click += new System.EventHandler(this.ButSearchAgain_Click);
            // 
            // butUse
            // 
            this.butUse.Enabled = false;
            this.butUse.Image = global::YANFOE.Properties.Resources.accept32;
            this.butUse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.butUse.Location = new System.Drawing.Point(492, 269);
            this.butUse.Name = "butUse";
            this.butUse.Size = new System.Drawing.Size(75, 39);
            this.butUse.TabIndex = 4;
            this.butUse.Click += new System.EventHandler(this.ButUse_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(9, 114);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(75, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Search Results:";
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchTerm.Location = new System.Drawing.Point(6, 43);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(82, 14);
            this.lblSearchTerm.TabIndex = 1;
            this.lblSearchTerm.Text = "<series name>";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(10, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Series Name:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 13);
            this.labelControl2.TabIndex = 18;
            this.labelControl2.Text = "Series Path:";
            // 
            // lblSeriesPath
            // 
            this.lblSeriesPath.Location = new System.Drawing.Point(6, 83);
            this.lblSeriesPath.Name = "lblSeriesPath";
            this.lblSeriesPath.Size = new System.Drawing.Size(69, 13);
            this.lblSeriesPath.TabIndex = 19;
            this.lblSeriesPath.Text = "<series path>";
            // 
            // FrmSelectSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 313);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmSelectSeries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select a series";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSearchResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchAgain.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ListBoxControl cmbSearchResults;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblFirstAired;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.TextBox tbOverview;
        private DevExpress.XtraEditors.LabelControl lblLanguage;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl lblSeriesName;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.SimpleButton butCancel;
        private DevExpress.XtraEditors.TextEdit txtSearchAgain;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton butSearchAgain;
        private DevExpress.XtraEditors.SimpleButton butUse;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl lblSearchTerm;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblSeriesPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}