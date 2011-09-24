namespace YANFOE.UI.Dialogs.DSettings
{
    partial class UcSettingsMoviesLists
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabGenres = new DevExpress.XtraTab.XtraTabPage();
            this.listEditGenre = new YANFOE.UI.UserControls.CommonControls.ListEditUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabGenres.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabGenres;
            this.xtraTabControl1.Size = new System.Drawing.Size(609, 527);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGenres});
            // 
            // tabGenres
            // 
            this.tabGenres.Controls.Add(this.listEditGenre);
            this.tabGenres.Name = "tabGenres";
            this.tabGenres.Size = new System.Drawing.Size(605, 503);
            this.tabGenres.Text = "Genres";
            // 
            // listEditGenre
            // 
            this.listEditGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEditGenre.Location = new System.Drawing.Point(0, 0);
            this.listEditGenre.Name = "listEditGenre";
            this.listEditGenre.Size = new System.Drawing.Size(605, 503);
            this.listEditGenre.TabIndex = 0;
            // 
            // UcSettingsMoviesLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "UcSettingsMoviesLists";
            this.Size = new System.Drawing.Size(609, 527);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabGenres.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabGenres;
        private UserControls.CommonControls.ListEditUserControl listEditGenre;

    }
}
