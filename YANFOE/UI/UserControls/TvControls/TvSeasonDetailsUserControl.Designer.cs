namespace YANFOE.UI.UserControls.TvControls
{
    partial class TvSeasonDetailsUserControl
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.tvTopMenuUserControl1 = new YANFOE.UI.UserControls.TvControls.TvTopMenuUserControl();
            this.displayPictureUserControl3 = new YANFOE.UI.UserControls.CommonControls.DisplayPictureUserControl();
            this.displayPictureUserControl2 = new YANFOE.UI.UserControls.CommonControls.DisplayPictureUserControl();
            this.displayPictureUserControl1 = new YANFOE.UI.UserControls.CommonControls.DisplayPictureUserControl();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.tvTopMenuUserControl1);
            this.layoutControl1.Controls.Add(this.displayPictureUserControl3);
            this.layoutControl1.Controls.Add(this.displayPictureUserControl2);
            this.layoutControl1.Controls.Add(this.displayPictureUserControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(753, 383, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(771, 592);
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
            this.layoutControlItem3,
            this.splitterItem1,
            this.splitterItem2,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(771, 592);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(326, 223);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(7, 369);
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(0, 216);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(771, 7);
            // 
            // tvTopMenuUserControl1
            // 
            this.tvTopMenuUserControl1.Location = new System.Drawing.Point(2, 2);
            this.tvTopMenuUserControl1.MinimumSize = new System.Drawing.Size(0, 48);
            this.tvTopMenuUserControl1.Name = "tvTopMenuUserControl1";
            this.tvTopMenuUserControl1.Size = new System.Drawing.Size(767, 48);
            this.tvTopMenuUserControl1.TabIndex = 7;
            this.tvTopMenuUserControl1.Type = YANFOE.UI.UserControls.TvControls.SaveType.SaveSeason;
            // 
            // displayPictureUserControl3
            // 
            this.displayPictureUserControl3.HeaderDetails = null;
            this.displayPictureUserControl3.HeaderTitle = "Fanart";
            this.displayPictureUserControl3.Location = new System.Drawing.Point(335, 225);
            this.displayPictureUserControl3.Name = "displayPictureUserControl3";
            this.displayPictureUserControl3.Size = new System.Drawing.Size(434, 365);
            this.displayPictureUserControl3.TabIndex = 6;
            this.displayPictureUserControl3.Type = YANFOE.Tools.Enums.GalleryType.TvSeasonFanart;
            // 
            // displayPictureUserControl2
            // 
            this.displayPictureUserControl2.HeaderDetails = null;
            this.displayPictureUserControl2.HeaderTitle = null;
            this.displayPictureUserControl2.Location = new System.Drawing.Point(2, 225);
            this.displayPictureUserControl2.Name = "displayPictureUserControl2";
            this.displayPictureUserControl2.Size = new System.Drawing.Size(322, 365);
            this.displayPictureUserControl2.TabIndex = 5;
            this.displayPictureUserControl2.Type = YANFOE.Tools.Enums.GalleryType.TvSeasonPoster;
            // 
            // displayPictureUserControl1
            // 
            this.displayPictureUserControl1.HeaderDetails = null;
            this.displayPictureUserControl1.HeaderTitle = "Banner";
            this.displayPictureUserControl1.Location = new System.Drawing.Point(2, 54);
            this.displayPictureUserControl1.Name = "displayPictureUserControl1";
            this.displayPictureUserControl1.Size = new System.Drawing.Size(767, 160);
            this.displayPictureUserControl1.TabIndex = 4;
            this.displayPictureUserControl1.Type = YANFOE.Tools.Enums.GalleryType.TvSeasonBanner;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.displayPictureUserControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(771, 164);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.displayPictureUserControl2;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 223);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(326, 369);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.displayPictureUserControl3;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(333, 223);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(438, 369);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.tvTopMenuUserControl1;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(771, 52);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // TvSeasonDetailsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "TvSeasonDetailsUserControl";
            this.Size = new System.Drawing.Size(771, 592);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private CommonControls.DisplayPictureUserControl displayPictureUserControl3;
        private CommonControls.DisplayPictureUserControl displayPictureUserControl2;
        private CommonControls.DisplayPictureUserControl displayPictureUserControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private TvTopMenuUserControl tvTopMenuUserControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}
