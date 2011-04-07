namespace YANFOE.UI.UserControls.MovieControls
{
    partial class MovieTrailerUserControl
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
            this.grdViewTrailerLocs = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdTrailers = new DevExpress.XtraGrid.GridControl();
            this.grdViewTrailers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmTrailerMovieTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmTrailerLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdSelectTrailer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkSelectedTrailer = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewTrailerLocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTrailers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewTrailers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectedTrailer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdViewTrailerLocs
            // 
            this.grdViewTrailerLocs.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmLocation});
            this.grdViewTrailerLocs.GridControl = this.grdTrailers;
            this.grdViewTrailerLocs.Name = "grdViewTrailerLocs";
            // 
            // clmLocation
            // 
            this.clmLocation.Caption = "Trailer Location";
            this.clmLocation.FieldName = "trailerLocation";
            this.clmLocation.Name = "clmLocation";
            this.clmLocation.Visible = true;
            this.clmLocation.VisibleIndex = 0;
            // 
            // grdTrailers
            // 
            this.grdTrailers.Location = new System.Drawing.Point(12, 12);
            this.grdTrailers.MainView = this.grdViewTrailers;
            this.grdTrailers.Name = "grdTrailers";
            this.grdTrailers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkSelectedTrailer});
            this.grdTrailers.Size = new System.Drawing.Size(685, 464);
            this.grdTrailers.TabIndex = 2;
            this.grdTrailers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewTrailers,
            this.grdViewTrailerLocs});
            // 
            // grdViewTrailers
            // 
            this.grdViewTrailers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmTrailerMovieTitle,
            this.clmTrailerLocation,
            this.grdSelectTrailer});
            this.grdViewTrailers.GridControl = this.grdTrailers;
            this.grdViewTrailers.Name = "grdViewTrailers";
            this.grdViewTrailers.OptionsDetail.EnableMasterViewMode = false;
            this.grdViewTrailers.OptionsView.ShowGroupPanel = false;
            // 
            // clmTrailerMovieTitle
            // 
            this.clmTrailerMovieTitle.Caption = "Movie Title";
            this.clmTrailerMovieTitle.FieldName = "TrailerMovieTitle";
            this.clmTrailerMovieTitle.Name = "clmTrailerMovieTitle";
            this.clmTrailerMovieTitle.Visible = true;
            this.clmTrailerMovieTitle.VisibleIndex = 0;
            this.clmTrailerMovieTitle.Width = 121;
            // 
            // clmTrailerLocation
            // 
            this.clmTrailerLocation.Caption = "Trailer Location";
            this.clmTrailerLocation.FieldName = "UriFull";
            this.clmTrailerLocation.Name = "clmTrailerLocation";
            this.clmTrailerLocation.Visible = true;
            this.clmTrailerLocation.VisibleIndex = 1;
            this.clmTrailerLocation.Width = 484;
            // 
            // grdSelectTrailer
            // 
            this.grdSelectTrailer.Caption = "...";
            this.grdSelectTrailer.ColumnEdit = this.chkSelectedTrailer;
            this.grdSelectTrailer.FieldName = "SelectedTrailer";
            this.grdSelectTrailer.Name = "grdSelectTrailer";
            this.grdSelectTrailer.Visible = true;
            this.grdSelectTrailer.VisibleIndex = 2;
            this.grdSelectTrailer.Width = 62;
            // 
            // chkSelectedTrailer
            // 
            this.chkSelectedTrailer.AutoHeight = false;
            this.chkSelectedTrailer.Name = "chkSelectedTrailer";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.grdTrailers);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(709, 488);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(709, 488);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdTrailers;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(689, 468);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // MovieTrailerUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "MovieTrailerUserControl";
            this.Size = new System.Drawing.Size(709, 488);
            ((System.ComponentModel.ISupportInitialize)(this.grdViewTrailerLocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTrailers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewTrailers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSelectedTrailer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdTrailers;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewTrailers;
        private DevExpress.XtraGrid.Columns.GridColumn clmTrailerMovieTitle;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewTrailerLocs;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocation;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn clmTrailerLocation;
        private DevExpress.XtraGrid.Columns.GridColumn grdSelectTrailer;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkSelectedTrailer;

    }
}
