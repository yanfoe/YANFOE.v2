namespace YANFOE.UI.Dialogs.General
{
    partial class frmWelcomePage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelcomePage));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.butOk = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnDonate = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.chkNeverShowAgain = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeverShowAgain.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureEdit1.EditValue = global::YANFOE.Properties.Resources.Untitled_3;
            this.pictureEdit1.Location = new System.Drawing.Point(8, 3);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Size = new System.Drawing.Size(151, 132);
            this.pictureEdit1.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(3, 132);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(295, 123);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Welcome To YANFOE v2";
            // 
            // labelControl1
            // 
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControl1.Location = new System.Drawing.Point(2, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Padding = new System.Windows.Forms.Padding(10);
            this.labelControl1.Size = new System.Drawing.Size(284, 111);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = resources.GetString("labelControl1.Text");
            // 
            // butOk
            // 
            this.butOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butOk.Image = global::YANFOE.Properties.Resources.accept32;
            this.butOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.butOk.Location = new System.Drawing.Point(402, 261);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(88, 36);
            this.butOk.TabIndex = 2;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnDonate);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Location = new System.Drawing.Point(304, 132);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(186, 123);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Support YANFOE";
            // 
            // btnDonate
            // 
            this.btnDonate.Image = global::YANFOE.Properties.Resources.btn_donate_SM;
            this.btnDonate.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnDonate.Location = new System.Drawing.Point(15, 71);
            this.btnDonate.Name = "btnDonate";
            this.btnDonate.Size = new System.Drawing.Size(160, 35);
            this.btnDonate.TabIndex = 1;
            this.btnDonate.Click += new System.EventHandler(this.btnDonate_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(6, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(169, 52);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "If you find YANFOE useful please\r\nmake a donation to support further\r\ndevelopemen" +
    "t.\r\n\r\n";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.memoEdit1);
            this.groupControl3.Location = new System.Drawing.Point(304, 6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(186, 120);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "License";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit1.EditValue = "YANFOE 2 is licensed under the Creative Commons Attribution-NonCommercial-ShareAl" +
    "ike 3.0 Unported License. ";
            this.memoEdit1.Location = new System.Drawing.Point(2, 22);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(182, 96);
            this.memoEdit1.TabIndex = 0;
            // 
            // chkNeverShowAgain
            // 
            this.chkNeverShowAgain.Location = new System.Drawing.Point(3, 278);
            this.chkNeverShowAgain.Name = "chkNeverShowAgain";
            this.chkNeverShowAgain.Properties.Caption = "Never show this again.";
            this.chkNeverShowAgain.Size = new System.Drawing.Size(156, 19);
            this.chkNeverShowAgain.TabIndex = 5;
            // 
            // frmWelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butOk;
            this.ClientSize = new System.Drawing.Size(496, 303);
            this.Controls.Add(this.chkNeverShowAgain);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.pictureEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWelcomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Welcome to YANFOE v2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkNeverShowAgain.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton butOk;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnDonate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.CheckEdit chkNeverShowAgain;
    }
}