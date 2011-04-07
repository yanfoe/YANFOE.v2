namespace YANFOE.UI.UserControls.MediaManagerControls
{
    partial class MediaManagerUc
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
            this.tabMediaPaths = new DevExpress.XtraTab.XtraTabPage();
            this.tabMovies = new DevExpress.XtraTab.XtraTabPage();
            this.importMoviesUc1 = new YANFOE.UI.UserControls.MediaManagerControls.ImportMoviesUc();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.mediaPathManager1 = new YANFOE.UI.UserControls.MediaManagerControls.MediaPathManager();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabMediaPaths.SuspendLayout();
            this.tabMovies.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabMediaPaths;
            this.xtraTabControl1.Size = new System.Drawing.Size(828, 668);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMediaPaths,
            this.tabMovies,
            this.xtraTabPage1});
            // 
            // tabMediaPaths
            // 
            this.tabMediaPaths.Controls.Add(this.mediaPathManager1);
            this.tabMediaPaths.Name = "tabMediaPaths";
            this.tabMediaPaths.Size = new System.Drawing.Size(824, 643);
            this.tabMediaPaths.Text = "Media Paths";
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.importMoviesUc1);
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.Size = new System.Drawing.Size(824, 643);
            this.tabMovies.Text = "Movies";
            // 
            // importMoviesUc1
            // 
            this.importMoviesUc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importMoviesUc1.Location = new System.Drawing.Point(0, 0);
            this.importMoviesUc1.Name = "importMoviesUc1";
            this.importMoviesUc1.Size = new System.Drawing.Size(824, 643);
            this.importMoviesUc1.TabIndex = 0;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(824, 643);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // mediaPathManager1
            // 
            this.mediaPathManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaPathManager1.Location = new System.Drawing.Point(0, 0);
            this.mediaPathManager1.Name = "mediaPathManager1";
            this.mediaPathManager1.Size = new System.Drawing.Size(824, 643);
            this.mediaPathManager1.TabIndex = 0;
            // 
            // MediaManagerUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "MediaManagerUc";
            this.Size = new System.Drawing.Size(828, 668);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabMediaPaths.ResumeLayout(false);
            this.tabMovies.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabMediaPaths;
        private DevExpress.XtraTab.XtraTabPage tabMovies;
        private ImportMoviesUc importMoviesUc1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private MediaPathManager mediaPathManager1;
    }
}
