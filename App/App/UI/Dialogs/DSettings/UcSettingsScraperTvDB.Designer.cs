namespace YANFOE.UI.Dialogs.DSettings
{
    partial class UcSettingsScraperTvDB
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
            this.picLoading = new DevExpress.XtraEditors.PictureEdit();
            this.cmbLanguages = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguages.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.picLoading);
            this.groupControl1.Controls.Add(this.cmbLanguages);
            this.groupControl1.Location = new System.Drawing.Point(4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(275, 124);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Retrieve Language";
            // 
            // picLoading
            // 
            this.picLoading.EditValue = global::YANFOE.Properties.Resources.smallanim;
            this.picLoading.Location = new System.Drawing.Point(245, 24);
            this.picLoading.Name = "picLoading";
            this.picLoading.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Properties.Appearance.Options.UseBackColor = true;
            this.picLoading.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLoading.Size = new System.Drawing.Size(30, 22);
            this.picLoading.TabIndex = 1;
            this.picLoading.Visible = false;
            // 
            // cmbLanguages
            // 
            this.cmbLanguages.Location = new System.Drawing.Point(6, 26);
            this.cmbLanguages.Name = "cmbLanguages";
            this.cmbLanguages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbLanguages.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbLanguages.Size = new System.Drawing.Size(242, 20);
            this.cmbLanguages.TabIndex = 0;
            this.cmbLanguages.SelectedIndexChanged += new System.EventHandler(this.cmbLanguages_SelectedIndexChanged);
            // 
            // UcSettingsScraperTvDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "UcSettingsScraperTvDB";
            this.Size = new System.Drawing.Size(520, 363);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguages.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PictureEdit picLoading;
        private DevExpress.XtraEditors.ComboBoxEdit cmbLanguages;
    }
}
