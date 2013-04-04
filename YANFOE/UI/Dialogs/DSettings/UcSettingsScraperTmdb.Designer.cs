namespace YANFOE.UI.Dialogs.DSettings
{
    partial class UcSettingsScraperTmdb
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.radioFanartSize = new DevExpress.XtraEditors.RadioGroup();
            this.radioPosterSize = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioFanartSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioPosterSize.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.radioPosterSize);
            this.groupControl1.Controls.Add(this.radioFanartSize);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(14, 14);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(348, 159);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Image Size Download";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(6, 26);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Fanart";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(180, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Poster";
            // 
            // radioFanartSize
            // 
            this.radioFanartSize.Location = new System.Drawing.Point(5, 45);
            this.radioFanartSize.Name = "radioFanartSize";
            this.radioFanartSize.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Original"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Poster (780x439)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "720 (1280x720)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thumbnail (300x169)")});
            this.radioFanartSize.Size = new System.Drawing.Size(160, 100);
            this.radioFanartSize.TabIndex = 2;
            this.radioFanartSize.SelectedIndexChanged += new System.EventHandler(this.radioFanartSize_SelectedIndexChanged);
            // 
            // radioPosterSize
            // 
            this.radioPosterSize.Location = new System.Drawing.Point(180, 45);
            this.radioPosterSize.Name = "radioPosterSize";
            this.radioPosterSize.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Original"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mid (500x750)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Cover (185x278)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Thumb (92x138)")});
            this.radioPosterSize.Size = new System.Drawing.Size(160, 100);
            this.radioPosterSize.TabIndex = 3;
            this.radioPosterSize.SelectedIndexChanged += new System.EventHandler(this.radioPosterSize_SelectedIndexChanged);
            // 
            // UcSettingsScraperTmdb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "UcSettingsScraperTmdb";
            this.Size = new System.Drawing.Size(731, 553);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioFanartSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioPosterSize.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.RadioGroup radioPosterSize;
        private DevExpress.XtraEditors.RadioGroup radioFanartSize;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}
