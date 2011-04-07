namespace YANFOE.UI.UserControls.CommonControls
{
    partial class DisplayPictureUserControl
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.imageMain = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.pnlTopMenu = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.lblPicTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblPicAreaName = new DevExpress.XtraEditors.LabelControl();
            this.butLeft = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.galleryControl = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnGetImageFromDisk = new DevExpress.XtraBars.BarButtonItem();
            this.btnGetImageFromUrl = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopMenu)).BeginInit();
            this.pnlTopMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl)).BeginInit();
            this.galleryControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerLeft.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(39, 12);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Size = new System.Drawing.Size(419, 22);
            this.pictureEdit1.TabIndex = 4;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(462, 12);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(20, 22);
            this.simpleButton2.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(12, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(23, 22);
            this.simpleButton1.TabIndex = 5;
            // 
            // imageMain
            // 
            this.imageMain.EditValue = global::YANFOE.Properties.Resources.picturefaded128;
            this.imageMain.Location = new System.Drawing.Point(2, 26);
            this.imageMain.Name = "imageMain";
            this.barManager1.SetPopupContextMenu(this.imageMain, this.popupMenu1);
            this.imageMain.Properties.ShowMenu = false;
            this.imageMain.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.imageMain.Size = new System.Drawing.Size(1106, 683);
            this.imageMain.StyleController = this.layoutControl2;
            this.imageMain.TabIndex = 5;
            this.imageMain.ImageChanged += new System.EventHandler(this.ImageMain_ImageChanged);
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.pnlTopMenu);
            this.layoutControl2.Controls.Add(this.imageMain);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(19, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(1110, 711);
            this.layoutControl2.TabIndex = 2;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // pnlTopMenu
            // 
            this.pnlTopMenu.Controls.Add(this.simpleButton3);
            this.pnlTopMenu.Controls.Add(this.lblPicTitle);
            this.pnlTopMenu.Controls.Add(this.lblPicAreaName);
            this.pnlTopMenu.Controls.Add(this.butLeft);
            this.pnlTopMenu.Location = new System.Drawing.Point(2, 2);
            this.pnlTopMenu.MaximumSize = new System.Drawing.Size(0, 20);
            this.pnlTopMenu.Name = "pnlTopMenu";
            this.pnlTopMenu.Size = new System.Drawing.Size(1106, 20);
            this.pnlTopMenu.TabIndex = 4;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButton3.Enabled = false;
            this.simpleButton3.Image = global::YANFOE.Properties.Resources.next24;
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(1066, 2);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(38, 16);
            this.simpleButton3.TabIndex = 4;
            // 
            // lblPicTitle
            // 
            this.lblPicTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPicTitle.Location = new System.Drawing.Point(98, 2);
            this.lblPicTitle.Name = "lblPicTitle";
            this.lblPicTitle.Size = new System.Drawing.Size(62, 13);
            this.lblPicTitle.TabIndex = 2;
            this.lblPicTitle.Text = "<No Image>";
            // 
            // lblPicAreaName
            // 
            this.lblPicAreaName.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPicAreaName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPicAreaName.Location = new System.Drawing.Point(40, 2);
            this.lblPicAreaName.Name = "lblPicAreaName";
            this.lblPicAreaName.Padding = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.lblPicAreaName.Size = new System.Drawing.Size(58, 13);
            this.lblPicAreaName.TabIndex = 3;
            this.lblPicAreaName.Text = "Poster :";
            // 
            // butLeft
            // 
            this.butLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.butLeft.Enabled = false;
            this.butLeft.Image = global::YANFOE.Properties.Resources.back24;
            this.butLeft.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.butLeft.Location = new System.Drawing.Point(2, 2);
            this.butLeft.Name = "butLeft";
            this.butLeft.Size = new System.Drawing.Size(38, 16);
            this.butLeft.TabIndex = 1;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "layoutControlGroup2";
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1110, 711);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Text = "layoutControlGroup2";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pnlTopMenu;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1110, 24);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.imageMain;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1110, 687);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // galleryControl
            // 
            this.galleryControl.Controls.Add(this.galleryControlClient1);
            this.galleryControl.DesignGalleryGroupIndex = 0;
            this.galleryControl.DesignGalleryItemIndex = 0;
            this.galleryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // galleryControlGallery1
            // 
            galleryItemGroup2.Caption = "Group6";
            galleryItemGroup2.CaptionAlignment = DevExpress.XtraBars.Ribbon.GalleryItemGroupCaptionAlignment.Center;
            galleryItemGroup2.CaptionControlSize = new System.Drawing.Size(1, 1);
            this.galleryControl.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup2});
            this.galleryControl.Gallery.ImageSize = new System.Drawing.Size(45, 60);
            this.galleryControl.Gallery.ScrollMode = DevExpress.XtraBars.Ribbon.Gallery.GalleryScrollMode.Smooth;
            this.galleryControl.Gallery.ShowGroupCaption = false;
            this.galleryControl.Gallery.ShowItemText = true;
            this.galleryControl.Gallery.ShowScrollBar = DevExpress.XtraBars.Ribbon.Gallery.ShowScrollBar.Auto;
            this.galleryControl.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.GalleryControlGallery1_ItemClick);
            this.galleryControl.Location = new System.Drawing.Point(0, 0);
            this.galleryControl.Margin = new System.Windows.Forms.Padding(0);
            this.galleryControl.Name = "galleryControl";
            this.galleryControl.Size = new System.Drawing.Size(144, 684);
            this.galleryControl.TabIndex = 0;
            this.galleryControl.Text = "galleryControl";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(140, 680);
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerLeft});
            this.dockManager1.DockingOptions.HideImmediatelyOnAutoHide = true;
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.hideContainerLeft.Controls.Add(this.dockPanel1);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(19, 711);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.DockVertical = DevExpress.Utils.DefaultBoolean.False;
            this.dockPanel1.ID = new System.Guid("c91c77d0-08d7-4881-9143-e4622502dff9");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(152, 711);
            this.dockPanel1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.SavedIndex = 0;
            this.dockPanel1.Size = new System.Drawing.Size(152, 711);
            this.dockPanel1.Text = "Alts";
            this.dockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.galleryControl);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(144, 684);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnGetImageFromDisk,
            this.btnGetImageFromUrl});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1129, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 711);
            this.barDockControlBottom.Size = new System.Drawing.Size(1129, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 711);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1129, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 711);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGetImageFromDisk),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGetImageFromUrl)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // btnGetImageFromDisk
            // 
            this.btnGetImageFromDisk.Caption = "Get Image From Disk";
            this.btnGetImageFromDisk.Glyph = global::YANFOE.Properties.Resources.folder32;
            this.btnGetImageFromDisk.Id = 0;
            this.btnGetImageFromDisk.Name = "btnGetImageFromDisk";
            this.btnGetImageFromDisk.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGetImageFromDisk_ItemClick);
            // 
            // btnGetImageFromUrl
            // 
            this.btnGetImageFromUrl.Caption = "Get Image From URL";
            this.btnGetImageFromUrl.Glyph = global::YANFOE.Properties.Resources.globe32;
            this.btnGetImageFromUrl.Id = 1;
            this.btnGetImageFromUrl.Name = "btnGetImageFromUrl";
            this.btnGetImageFromUrl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnGetImageFromUrl_ItemClick);
            // 
            // DisplayPictureUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl2);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Name = "DisplayPictureUserControl";
            this.Size = new System.Drawing.Size(1129, 711);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopMenu)).EndInit();
            this.pnlTopMenu.ResumeLayout(false);
            this.pnlTopMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl)).EndInit();
            this.galleryControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerLeft.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PictureEdit imageMain;
        private DevExpress.XtraEditors.PanelControl pnlTopMenu;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraEditors.LabelControl lblPicTitle;
        private DevExpress.XtraEditors.SimpleButton butLeft;
        private DevExpress.XtraEditors.LabelControl lblPicAreaName;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnGetImageFromDisk;
        private DevExpress.XtraBars.BarButtonItem btnGetImageFromUrl;
        private DevExpress.XtraBars.PopupMenu popupMenu1;

    }
}
