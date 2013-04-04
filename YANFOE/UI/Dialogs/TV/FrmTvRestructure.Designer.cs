namespace YANFOE.UI.Dialogs.TV
{
    partial class FrmTvRestructure
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
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.grdPreview = new DevExpress.XtraGrid.GridControl();
            this.gridPreviewView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtSeriesNameTemplate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtSeasonNameTemplate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtEpisodeNameTemplate = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.txtTvStructure = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviewView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeriesNameTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeasonNameTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEpisodeNameTemplate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTvStructure.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtTvStructure);
            this.layoutControl1.Controls.Add(this.txtEpisodeNameTemplate);
            this.layoutControl1.Controls.Add(this.txtSeasonNameTemplate);
            this.layoutControl1.Controls.Add(this.txtSeriesNameTemplate);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.grdPreview);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1160, 347, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(872, 514);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
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
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(872, 514);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // grdPreview
            // 
            this.grdPreview.Location = new System.Drawing.Point(12, 12);
            this.grdPreview.MainView = this.gridPreviewView;
            this.grdPreview.Name = "grdPreview";
            this.grdPreview.Size = new System.Drawing.Size(848, 408);
            this.grdPreview.TabIndex = 4;
            this.grdPreview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridPreviewView});
            // 
            // gridPreviewView
            // 
            this.gridPreviewView.GridControl = this.grdPreview;
            this.gridPreviewView.Name = "gridPreviewView";
            this.gridPreviewView.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.grdPreview;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(852, 412);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(501, 424);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(359, 78);
            this.panelControl1.TabIndex = 5;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.panelControl1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(489, 412);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(363, 82);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // txtSeriesNameTemplate
            // 
            this.txtSeriesNameTemplate.EditValue = "";
            this.txtSeriesNameTemplate.Location = new System.Drawing.Point(83, 424);
            this.txtSeriesNameTemplate.Name = "txtSeriesNameTemplate";
            this.txtSeriesNameTemplate.Size = new System.Drawing.Size(160, 20);
            this.txtSeriesNameTemplate.StyleController = this.layoutControl1;
            this.txtSeriesNameTemplate.TabIndex = 7;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtSeriesNameTemplate;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 412);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(235, 24);
            this.layoutControlItem4.Text = "Series Layer";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(67, 13);
            // 
            // txtSeasonNameTemplate
            // 
            this.txtSeasonNameTemplate.EditValue = "";
            this.txtSeasonNameTemplate.Location = new System.Drawing.Point(318, 424);
            this.txtSeasonNameTemplate.Name = "txtSeasonNameTemplate";
            this.txtSeasonNameTemplate.Size = new System.Drawing.Size(179, 20);
            this.txtSeasonNameTemplate.StyleController = this.layoutControl1;
            this.txtSeasonNameTemplate.TabIndex = 9;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtSeasonNameTemplate;
            this.layoutControlItem5.CustomizationFormText = "Season Layer";
            this.layoutControlItem5.Location = new System.Drawing.Point(235, 412);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(254, 24);
            this.layoutControlItem5.Text = "Season Layer";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(67, 13);
            // 
            // txtEpisodeNameTemplate
            // 
            this.txtEpisodeNameTemplate.EditValue = "";
            this.txtEpisodeNameTemplate.Location = new System.Drawing.Point(83, 448);
            this.txtEpisodeNameTemplate.Name = "txtEpisodeNameTemplate";
            this.txtEpisodeNameTemplate.Size = new System.Drawing.Size(414, 20);
            this.txtEpisodeNameTemplate.StyleController = this.layoutControl1;
            this.txtEpisodeNameTemplate.TabIndex = 10;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtEpisodeNameTemplate;
            this.layoutControlItem3.CustomizationFormText = "Episode Layer";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 436);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(489, 24);
            this.layoutControlItem3.Text = "Episode Layer";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(67, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 484);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(489, 10);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // txtTvStructure
            // 
            this.txtTvStructure.Location = new System.Drawing.Point(83, 472);
            this.txtTvStructure.Name = "txtTvStructure";
            this.txtTvStructure.Size = new System.Drawing.Size(414, 20);
            this.txtTvStructure.StyleController = this.layoutControl1;
            this.txtTvStructure.TabIndex = 11;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtTvStructure;
            this.layoutControlItem6.CustomizationFormText = "Tv Structure";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 460);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(489, 24);
            this.layoutControlItem6.Text = "Tv Structure";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(67, 13);
            // 
            // FrmTvRestructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 514);
            this.Controls.Add(this.layoutControl1);
            this.Name = "FrmTvRestructure";
            this.Text = "FrmTvRestructure";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPreviewView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeriesNameTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSeasonNameTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEpisodeNameTemplate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTvStructure.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtEpisodeNameTemplate;
        private DevExpress.XtraEditors.TextEdit txtSeasonNameTemplate;
        private DevExpress.XtraEditors.TextEdit txtSeriesNameTemplate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grdPreview;
        private DevExpress.XtraGrid.Views.Grid.GridView gridPreviewView;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtTvStructure;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}