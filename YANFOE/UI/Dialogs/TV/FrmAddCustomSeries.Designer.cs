namespace YANFOE.UI.Dialogs.TV
{
    partial class FrmAddCustomSeries
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRemoveFiles = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddBlankShow = new DevExpress.XtraEditors.SimpleButton();
            this.grdFiles = new DevExpress.XtraGrid.GridControl();
            this.grdViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtSeasons = new DevExpress.XtraEditors.SpinEdit();
            this.txtNewSeriesName = new DevExpress.XtraEditors.TextEdit();
            this.grdSeasons = new DevExpress.XtraGrid.GridControl();
            this.grdViewSeasons = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.chkAddSeason0 = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeasons.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewSeriesName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSeasons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewSeasons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAddSeason0.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRemoveFiles);
            this.layoutControl1.Controls.Add(this.btnAddBlankShow);
            this.layoutControl1.Controls.Add(this.grdFiles);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.txtSeasons);
            this.layoutControl1.Controls.Add(this.txtNewSeriesName);
            this.layoutControl1.Controls.Add(this.grdSeasons);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.chkAddSeason0);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(990, 128, 452, 547);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(796, 477);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRemoveFiles
            // 
            this.btnRemoveFiles.Enabled = false;
            this.btnRemoveFiles.Image = global::YANFOE.Properties.Resources.trash;
            this.btnRemoveFiles.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnRemoveFiles.Location = new System.Drawing.Point(484, 398);
            this.btnRemoveFiles.Name = "btnRemoveFiles";
            this.btnRemoveFiles.Size = new System.Drawing.Size(305, 30);
            this.btnRemoveFiles.StyleController = this.layoutControl1;
            this.btnRemoveFiles.TabIndex = 12;
            // 
            // btnAddBlankShow
            // 
            this.btnAddBlankShow.Image = global::YANFOE.Properties.Resources.add24;
            this.btnAddBlankShow.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddBlankShow.Location = new System.Drawing.Point(183, 398);
            this.btnAddBlankShow.Name = "btnAddBlankShow";
            this.btnAddBlankShow.Size = new System.Drawing.Size(297, 30);
            this.btnAddBlankShow.StyleController = this.layoutControl1;
            this.btnAddBlankShow.TabIndex = 11;
            this.btnAddBlankShow.Click += new System.EventHandler(this.btnAddBlankShow_Click);
            // 
            // grdFiles
            // 
            this.grdFiles.AllowDrop = true;
            this.grdFiles.Location = new System.Drawing.Point(183, 75);
            this.grdFiles.MainView = this.grdViewFiles;
            this.grdFiles.Name = "grdFiles";
            this.grdFiles.Size = new System.Drawing.Size(606, 319);
            this.grdFiles.TabIndex = 10;
            this.grdFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewFiles});
            this.grdFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.grdFiles_DragDrop);
            this.grdFiles.DragOver += new System.Windows.Forms.DragEventHandler(this.grdFiles_DragOver);
            // 
            // grdViewFiles
            // 
            this.grdViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.grdViewFiles.GridControl = this.grdFiles;
            this.grdViewFiles.Name = "grdViewFiles";
            this.grdViewFiles.OptionsSelection.MultiSelect = true;
            this.grdViewFiles.OptionsView.ShowGroupPanel = false;
            this.grdViewFiles.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.grdViewFiles.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdViewFiles_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Episode Number";
            this.gridColumn1.FieldName = "EpisodeNumber";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "File Path";
            this.gridColumn2.FieldName = "FilePath";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 477;
            // 
            // btnOK
            // 
            this.btnOK.Image = global::YANFOE.Properties.Resources.accept32;
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.Location = new System.Drawing.Point(4, 435);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(358, 38);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 7;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtSeasons
            // 
            this.txtSeasons.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSeasons.Location = new System.Drawing.Point(126, 28);
            this.txtSeasons.Name = "txtSeasons";
            this.txtSeasons.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSeasons.Size = new System.Drawing.Size(239, 20);
            this.txtSeasons.StyleController = this.layoutControl1;
            this.txtSeasons.TabIndex = 5;
            // 
            // txtNewSeriesName
            // 
            this.txtNewSeriesName.Location = new System.Drawing.Point(126, 4);
            this.txtNewSeriesName.Name = "txtNewSeriesName";
            this.txtNewSeriesName.Size = new System.Drawing.Size(666, 20);
            this.txtNewSeriesName.StyleController = this.layoutControl1;
            this.txtNewSeriesName.TabIndex = 4;
            // 
            // grdSeasons
            // 
            this.grdSeasons.Location = new System.Drawing.Point(7, 75);
            this.grdSeasons.MainView = this.grdViewSeasons;
            this.grdSeasons.Name = "grdSeasons";
            this.grdSeasons.Size = new System.Drawing.Size(172, 353);
            this.grdSeasons.TabIndex = 9;
            this.grdSeasons.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewSeasons});
            // 
            // grdViewSeasons
            // 
            this.grdViewSeasons.GridControl = this.grdSeasons;
            this.grdViewSeasons.Name = "grdViewSeasons";
            this.grdViewSeasons.OptionsBehavior.Editable = false;
            this.grdViewSeasons.OptionsView.ShowGroupPanel = false;
            this.grdViewSeasons.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.grdViewSeasons_FocusedRowChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::YANFOE.Properties.Resources.delete32;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(366, 435);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(426, 38);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "simpleButton2";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkAddSeason0
            // 
            this.chkAddSeason0.Location = new System.Drawing.Point(369, 28);
            this.chkAddSeason0.Name = "chkAddSeason0";
            this.chkAddSeason0.Properties.Caption = "Add Season 0";
            this.chkAddSeason0.Size = new System.Drawing.Size(423, 19);
            this.chkAddSeason0.StyleController = this.layoutControl1;
            this.chkAddSeason0.TabIndex = 6;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlGroup2,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(796, 477);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem1.Control = this.txtNewSeriesName;
            this.layoutControlItem1.CustomizationFormText = "Enter a new series name";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(792, 24);
            this.layoutControlItem1.Text = "Enter a new series name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(118, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem2.Control = this.txtSeasons;
            this.layoutControlItem2.CustomizationFormText = "Seasons";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(365, 24);
            this.layoutControlItem2.Text = "Seasons";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(118, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOK;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 431);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(362, 42);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkAddSeason0;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(365, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(427, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Organise Seasons";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 48);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(792, 383);
            this.layoutControlGroup2.Text = "Organise Seasons";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.grdFiles;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(176, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(610, 323);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.grdSeasons;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(176, 357);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnAddBlankShow;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(176, 323);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(301, 34);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnRemoveFiles;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(477, 323);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(309, 34);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(362, 431);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(430, 42);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // FrmAddCustomSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(796, 477);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAddCustomSeries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Custom Series";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeasons.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewSeriesName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSeasons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewSeasons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAddSeason0.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.CheckEdit chkAddSeason0;
        private DevExpress.XtraEditors.SpinEdit txtSeasons;
        private DevExpress.XtraEditors.TextEdit txtNewSeriesName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnRemoveFiles;
        private DevExpress.XtraEditors.SimpleButton btnAddBlankShow;
        private DevExpress.XtraGrid.GridControl grdFiles;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewFiles;
        private DevExpress.XtraGrid.GridControl grdSeasons;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewSeasons;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}