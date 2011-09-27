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
            this.tabMoviesGenres = new DevExpress.XtraTab.XtraTabPage();
            this.listEditGenre = new YANFOE.UI.UserControls.CommonControls.ListEditUserControl();
            this.tabMoviesHiddenMovies = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRestore = new DevExpress.XtraEditors.SimpleButton();
            this.grdHiddenMovies = new DevExpress.XtraGrid.GridControl();
            this.grdViewHiddenMovie = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabMoviesGenres.SuspendLayout();
            this.tabMoviesHiddenMovies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHiddenMovies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewHiddenMovie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabMoviesGenres;
            this.xtraTabControl1.Size = new System.Drawing.Size(609, 527);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMoviesGenres,
            this.tabMoviesHiddenMovies});
            // 
            // tabMoviesGenres
            // 
            this.tabMoviesGenres.Controls.Add(this.listEditGenre);
            this.tabMoviesGenres.Name = "tabMoviesGenres";
            this.tabMoviesGenres.Size = new System.Drawing.Size(605, 503);
            this.tabMoviesGenres.Text = "Genres";
            // 
            // listEditGenre
            // 
            this.listEditGenre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listEditGenre.Location = new System.Drawing.Point(0, 0);
            this.listEditGenre.Name = "listEditGenre";
            this.listEditGenre.Size = new System.Drawing.Size(605, 503);
            this.listEditGenre.TabIndex = 0;
            // 
            // tabMoviesHiddenMovies
            // 
            this.tabMoviesHiddenMovies.Controls.Add(this.layoutControl1);
            this.tabMoviesHiddenMovies.Name = "tabMoviesHiddenMovies";
            this.tabMoviesHiddenMovies.Size = new System.Drawing.Size(605, 503);
            this.tabMoviesHiddenMovies.Text = "Hidden Movies";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRestore);
            this.layoutControl1.Controls.Add(this.grdHiddenMovies);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(605, 503);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRestore
            // 
            this.btnRestore.Image = global::YANFOE.Properties.Resources.back1;
            this.btnRestore.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRestore.Location = new System.Drawing.Point(12, 453);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(581, 38);
            this.btnRestore.StyleController = this.layoutControl1;
            this.btnRestore.TabIndex = 5;
            this.btnRestore.Text = "Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // grdHiddenMovies
            // 
            this.grdHiddenMovies.Location = new System.Drawing.Point(12, 12);
            this.grdHiddenMovies.MainView = this.grdViewHiddenMovie;
            this.grdHiddenMovies.Name = "grdHiddenMovies";
            this.grdHiddenMovies.Size = new System.Drawing.Size(581, 437);
            this.grdHiddenMovies.TabIndex = 4;
            this.grdHiddenMovies.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewHiddenMovie});
            // 
            // grdViewHiddenMovie
            // 
            this.grdViewHiddenMovie.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.grdViewHiddenMovie.GridControl = this.grdHiddenMovies;
            this.grdViewHiddenMovie.Name = "grdViewHiddenMovie";
            this.grdViewHiddenMovie.OptionsDetail.EnableMasterViewMode = false;
            this.grdViewHiddenMovie.OptionsSelection.MultiSelect = true;
            this.grdViewHiddenMovie.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.grdViewHiddenMovie.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Title";
            this.gridColumn1.FieldName = "Title";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Year";
            this.gridColumn2.FieldName = "Year";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(605, 503);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdHiddenMovies;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(585, 441);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnRestore;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 441);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(585, 42);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
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
            this.tabMoviesGenres.ResumeLayout(false);
            this.tabMoviesHiddenMovies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHiddenMovies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewHiddenMovie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabMoviesGenres;
        private UserControls.CommonControls.ListEditUserControl listEditGenre;
        private DevExpress.XtraTab.XtraTabPage tabMoviesHiddenMovies;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnRestore;
        private DevExpress.XtraGrid.GridControl grdHiddenMovies;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewHiddenMovie;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;

    }
}
