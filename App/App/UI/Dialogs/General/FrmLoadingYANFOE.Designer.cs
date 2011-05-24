namespace YANFOE.UI.Dialogs.General
{
    partial class FrmLoadingYANFOE
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
            this.lblYANFOETitle = new DevExpress.XtraEditors.LabelControl();
            this.lblProgress1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.progress = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblVersion = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblYANFOETitle
            // 
            this.lblYANFOETitle.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYANFOETitle.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblYANFOETitle.Location = new System.Drawing.Point(41, 375);
            this.lblYANFOETitle.Name = "lblYANFOETitle";
            this.lblYANFOETitle.Size = new System.Drawing.Size(145, 18);
            this.lblYANFOETitle.TabIndex = 0;
            this.lblYANFOETitle.Text = "YANFOE 2.0 - Alpha 3";
            // 
            // lblProgress1
            // 
            this.lblProgress1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblProgress1.Location = new System.Drawing.Point(41, 428);
            this.lblProgress1.Name = "lblProgress1";
            this.lblProgress1.Size = new System.Drawing.Size(0, 18);
            this.lblProgress1.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl2.Location = new System.Drawing.Point(41, 393);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(147, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "The Media Meta Data Manager";
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(41, 453);
            this.progress.Name = "progress";
            this.progress.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progress.Size = new System.Drawing.Size(418, 16);
            this.progress.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl3.Location = new System.Drawing.Point(361, 375);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(98, 18);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Copyright 2011";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(361, 393);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(98, 13);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "The YANFOE Project";
            // 
            // lblVersion
            // 
            this.lblVersion.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(41, 407);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(147, 13);
            this.lblVersion.TabIndex = 6;
            this.lblVersion.Text = "The Media Meta Data Manager";
            // 
            // FrmLoadingYANFOE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayoutStore = System.Windows.Forms.ImageLayout.Tile;
            this.BackgroundImageStore = global::YANFOE.Properties.Resources.yanfoe;
            this.ClientSize = new System.Drawing.Size(501, 501);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblProgress1);
            this.Controls.Add(this.lblYANFOETitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLoadingYANFOE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLoadingYANFOE";
            ((System.ComponentModel.ISupportInitialize)(this.progress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblYANFOETitle;
        private DevExpress.XtraEditors.LabelControl lblProgress1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ProgressBarControl progress;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblVersion;


    }
}