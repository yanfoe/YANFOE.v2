namespace YANFOE.UI.Dialogs.TV
{
    partial class FrmUpdateShows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdateShows));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtStatus = new DevExpress.XtraEditors.LabelControl();
            this.grdSeriesList = new DevExpress.XtraGrid.GridControl();
            this.gridSeriesListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SeriesID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SeriesName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UpdatedOnServer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UpdateSeries = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSeriesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSeriesListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtStatus);
            this.layoutControl1.Controls.Add(this.grdSeriesList);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1209, 342, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(711, 479);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(18, 400);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(34, 13);
            this.txtStatus.StyleController = this.layoutControl1;
            this.txtStatus.TabIndex = 9;
            this.txtStatus.Text = "rewrw\\";
            // 
            // grdSeriesList
            // 
            this.grdSeriesList.Location = new System.Drawing.Point(24, 44);
            this.grdSeriesList.MainView = this.gridSeriesListView;
            this.grdSeriesList.Name = "grdSeriesList";
            this.grdSeriesList.Size = new System.Drawing.Size(663, 314);
            this.grdSeriesList.TabIndex = 5;
            this.grdSeriesList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridSeriesListView});
            // 
            // gridSeriesListView
            // 
            this.gridSeriesListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SeriesID,
            this.SeriesName,
            this.UpdatedOnServer,
            this.UpdateSeries});
            this.gridSeriesListView.GridControl = this.grdSeriesList;
            this.gridSeriesListView.Name = "gridSeriesListView";
            this.gridSeriesListView.OptionsView.ShowGroupPanel = false;
            // 
            // SeriesID
            // 
            this.SeriesID.Caption = "SeriesID";
            this.SeriesID.FieldName = "SeriesId";
            this.SeriesID.Name = "SeriesID";
            this.SeriesID.OptionsColumn.AllowEdit = false;
            this.SeriesID.Visible = true;
            this.SeriesID.VisibleIndex = 0;
            this.SeriesID.Width = 61;
            // 
            // SeriesName
            // 
            this.SeriesName.Caption = "Series Name";
            this.SeriesName.FieldName = "SeriesName";
            this.SeriesName.Name = "SeriesName";
            this.SeriesName.OptionsColumn.AllowEdit = false;
            this.SeriesName.Visible = true;
            this.SeriesName.VisibleIndex = 1;
            this.SeriesName.Width = 386;
            // 
            // UpdatedOnServer
            // 
            this.UpdatedOnServer.Caption = "UpdatedOnServer";
            this.UpdatedOnServer.FieldName = "UpdatedOnServer";
            this.UpdatedOnServer.Name = "UpdatedOnServer";
            this.UpdatedOnServer.OptionsColumn.AllowEdit = false;
            this.UpdatedOnServer.OptionsColumn.AllowFocus = false;
            this.UpdatedOnServer.Visible = true;
            this.UpdatedOnServer.VisibleIndex = 2;
            this.UpdatedOnServer.Width = 104;
            // 
            // UpdateSeries
            // 
            this.UpdateSeries.Caption = "UpdateSeries";
            this.UpdateSeries.FieldName = "UpdateSeries";
            this.UpdateSeries.Name = "UpdateSeries";
            this.UpdateSeries.Visible = true;
            this.UpdateSeries.VisibleIndex = 3;
            this.UpdateSeries.Width = 94;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::YANFOE.Properties.Resources.delete32;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(349, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(347, 38);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Image = global::YANFOE.Properties.Resources.accept32;
            this.btnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOk.Location = new System.Drawing.Point(15, 426);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(330, 38);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 7;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlGroup5,
            this.layoutControlGroup2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(711, 479);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Series List";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(691, 362);
            this.layoutControlGroup3.Text = "Series List";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.grdSeriesList;
            this.layoutControlItem2.CustomizationFormText = "Series List";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(667, 318);
            this.layoutControlItem2.Text = "Series List";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "layoutControlGroup5";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem4});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 411);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup5.Size = new System.Drawing.Size(691, 48);
            this.layoutControlGroup5.Text = "layoutControlGroup5";
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(334, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(351, 42);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOk;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(334, 42);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Status";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 362);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup2.Size = new System.Drawing.Size(691, 49);
            this.layoutControlGroup2.Text = "Status";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtStatus;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(679, 17);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // FrmUpdateShows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 479);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUpdateShows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Shows";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSeriesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSeriesListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl txtStatus;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl grdSeriesList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSeriesListView;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraGrid.Columns.GridColumn SeriesID;
        private DevExpress.XtraGrid.Columns.GridColumn SeriesName;
        private DevExpress.XtraGrid.Columns.GridColumn UpdatedOnServer;
        private DevExpress.XtraGrid.Columns.GridColumn UpdateSeries;
    }
}