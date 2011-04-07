namespace YANFOE.UI.UserControls.MovieControls
{
    partial class MovieFileInfoUserControl
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
            this.grdFileInfo = new DevExpress.XtraGrid.GridControl();
            this.grdViewFileInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.clmFilePath = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdFileInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFileInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFileInfo
            // 
            this.grdFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFileInfo.Location = new System.Drawing.Point(0, 0);
            this.grdFileInfo.MainView = this.grdViewFileInfo;
            this.grdFileInfo.Name = "grdFileInfo";
            this.grdFileInfo.Size = new System.Drawing.Size(709, 488);
            this.grdFileInfo.TabIndex = 2;
            this.grdFileInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewFileInfo});
            // 
            // grdViewFileInfo
            // 
            this.grdViewFileInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.clmFilePath});
            this.grdViewFileInfo.GridControl = this.grdFileInfo;
            this.grdViewFileInfo.Name = "grdViewFileInfo";
            // 
            // clmFilePath
            // 
            this.clmFilePath.Caption = "File Path";
            this.clmFilePath.FieldName = "FilePath";
            this.clmFilePath.Name = "clmFilePath";
            this.clmFilePath.Visible = true;
            this.clmFilePath.VisibleIndex = 0;
            // 
            // MovieFileInfoUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdFileInfo);
            this.Name = "MovieFileInfoUserControl";
            this.Size = new System.Drawing.Size(709, 488);
            ((System.ComponentModel.ISupportInitialize)(this.grdFileInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFileInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdFileInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewFileInfo;
        private DevExpress.XtraGrid.Columns.GridColumn clmFilePath;

    }
}
