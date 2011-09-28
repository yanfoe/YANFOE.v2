namespace YANFOE.UI.Popups
{
    partial class ExportMissingEpisodes
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
            this.components = new System.ComponentModel.Container();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.SeriesName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.missingEpisodesCount = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.episodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.dropDownExportTo = new DevExpress.XtraEditors.DropDownButton();
            this.popupMenuExportTo = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupExportXML = new DevExpress.XtraBars.BarStaticItem();
            this.popupExportHTML = new DevExpress.XtraBars.BarStaticItem();
            this.popupExportPDF = new DevExpress.XtraBars.BarStaticItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuExportTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.SeriesName,
            this.missingEpisodesCount,
            this.episodeName});
            this.treeList1.KeyFieldName = "id";
            this.treeList1.Location = new System.Drawing.Point(12, 12);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList1.ParentFieldName = "parent";
            this.treeList1.RootValue = "0";
            this.treeList1.Size = new System.Drawing.Size(776, 469);
            this.treeList1.TabIndex = 0;
            // 
            // SeriesName
            // 
            this.SeriesName.Caption = "Series Name";
            this.SeriesName.FieldName = "seriesname";
            this.SeriesName.Name = "SeriesName";
            this.SeriesName.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.SeriesName.Visible = true;
            this.SeriesName.VisibleIndex = 0;
            this.SeriesName.Width = 337;
            // 
            // missingEpisodesCount
            // 
            this.missingEpisodesCount.Caption = "Missing Episodes";
            this.missingEpisodesCount.FieldName = "missingEpisodesCount";
            this.missingEpisodesCount.Name = "missingEpisodesCount";
            this.missingEpisodesCount.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.missingEpisodesCount.Visible = true;
            this.missingEpisodesCount.VisibleIndex = 1;
            this.missingEpisodesCount.Width = 300;
            // 
            // episodeName
            // 
            this.episodeName.Caption = "Episode Name";
            this.episodeName.FieldName = "episodename";
            this.episodeName.Name = "episodeName";
            this.episodeName.Visible = true;
            this.episodeName.VisibleIndex = 2;
            this.episodeName.Width = 617;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(713, 487);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnExportToXml_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(632, 487);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export list";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dropDownExportTo
            // 
            this.dropDownExportTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDownExportTo.DropDownControl = this.popupMenuExportTo;
            this.dropDownExportTo.Location = new System.Drawing.Point(491, 487);
            this.dropDownExportTo.Name = "dropDownExportTo";
            this.dropDownExportTo.Size = new System.Drawing.Size(135, 23);
            this.dropDownExportTo.TabIndex = 3;
            this.dropDownExportTo.Text = "Export to ...";
            // 
            // popupMenuExportTo
            // 
            this.popupMenuExportTo.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.popupExportXML),
            new DevExpress.XtraBars.LinkPersistInfo(this.popupExportHTML),
            new DevExpress.XtraBars.LinkPersistInfo(this.popupExportPDF)});
            this.popupMenuExportTo.Manager = this.barManager1;
            this.popupMenuExportTo.Name = "popupMenuExportTo";
            // 
            // popupExportXML
            // 
            this.popupExportXML.Caption = "XML";
            this.popupExportXML.Id = 2;
            this.popupExportXML.Name = "popupExportXML";
            this.popupExportXML.TextAlignment = System.Drawing.StringAlignment.Near;
            this.popupExportXML.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popupMenuExportTo_ItemClick);
            // 
            // popupExportHTML
            // 
            this.popupExportHTML.Caption = "HTML";
            this.popupExportHTML.Id = 3;
            this.popupExportHTML.Name = "popupExportHTML";
            this.popupExportHTML.TextAlignment = System.Drawing.StringAlignment.Near;
            this.popupExportHTML.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popupMenuExportTo_ItemClick);
            // 
            // popupExportPDF
            // 
            this.popupExportPDF.Caption = "PDF";
            this.popupExportPDF.Id = 4;
            this.popupExportPDF.Name = "popupExportPDF";
            this.popupExportPDF.TextAlignment = System.Drawing.StringAlignment.Near;
            this.popupExportPDF.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popupMenuExportTo_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.popupExportXML,
            this.popupExportHTML,
            this.popupExportPDF});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 5;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            this.bar3.Visible = false;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(800, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 499);
            this.barDockControlBottom.Size = new System.Drawing.Size(800, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 477);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(800, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 477);
            // 
            // ExportMissingEpisodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.dropDownExportTo);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ExportMissingEpisodes";
            this.Text = "ExportMissingEpisodes";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuExportTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn SeriesName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn missingEpisodesCount;
        private DevExpress.XtraTreeList.Columns.TreeListColumn episodeName;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.DropDownButton dropDownExportTo;
        private DevExpress.XtraBars.PopupMenu popupMenuExportTo;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem popupExportXML;
        private DevExpress.XtraBars.BarStaticItem popupExportHTML;
        private DevExpress.XtraBars.BarStaticItem popupExportPDF;

    }
}