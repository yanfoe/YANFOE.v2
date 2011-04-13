namespace YANFOE.UI.UserControls.MovieControls
{
    using YANFOE.UI.UserControls.CommonControls;

    partial class MoviesUserControl
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip11 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem13 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip12 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem14 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem9 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem3 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem15 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoviesUserControl));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.tabByTitle = new DevExpress.XtraTab.XtraTabPage();
            this.grdMoviesList = new DevExpress.XtraGrid.GridControl();
            this.grdViewByTitle = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LoadingStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BusyPicture = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.clmTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmMarked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkMarked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.clmLocked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.clmReleased = new DevExpress.XtraGrid.Columns.GridColumn();
            this.RepositoryImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.tabByPoster = new DevExpress.XtraTab.XtraTabPage();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.picFanart = new YANFOE.UI.UserControls.CommonControls.DisplayPictureUserControl();
            this.tabEditTabsControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabMainDetails = new DevExpress.XtraTab.XtraTabPage();
            this.movieMainDetailsUserControl1 = new YANFOE.UI.UserControls.MovieControls.MovieMainDetailsUserControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.btnMarked = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnLoadFromWeb = new DevExpress.XtraEditors.SimpleButton();
            this.btnLock = new DevExpress.XtraEditors.SimpleButton();
            this.tabFileInfo = new DevExpress.XtraTab.XtraTabPage();
            this.movieFileInfoUserControl1 = new YANFOE.UI.UserControls.MovieControls.MovieFileInfoUserControl();
            this.tabIdentify = new DevExpress.XtraTab.XtraTabPage();
            this.movieIdentierUserControl11 = new YANFOE.UI.UserControls.MovieControls.MovieIdentierUserControl1();
            this.tabSets = new DevExpress.XtraTab.XtraTabPage();
            this.setManagerUserControl1 = new YANFOE.UI.UserControls.MovieControls.SetManagerUserControl();
            this.tabTrailers = new DevExpress.XtraTab.XtraTabPage();
            this.movieTrailerUserControl1 = new YANFOE.UI.UserControls.MovieControls.MovieTrailerUserControl();
            this.tabPreview = new DevExpress.XtraTab.XtraTabPage();
            this.nfoPreviewUserControl1 = new YANFOE.UI.UserControls.CommonControls.NFOPreviewUserControl();
            this.picPoster = new YANFOE.UI.UserControls.CommonControls.DisplayPictureUserControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlImages = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem3 = new DevExpress.XtraLayout.SplitterItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem2 = new DevExpress.XtraLayout.SplitterItem();
            this.galleryControlGallery1 = new DevExpress.XtraBars.Ribbon.Gallery.GalleryControlGallery();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.popupSave = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnSaveNfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveAllImages = new DevExpress.XtraBars.BarButtonItem();
            this.btnSavePoster = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveFanart = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.popupLoadFromWeb = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem1 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.popupMovieList = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.tabByTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMoviesList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewByTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BusyPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMarked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryImageComboBox)).BeginInit();
            this.tabByPoster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabEditTabsControl)).BeginInit();
            this.tabEditTabsControl.SuspendLayout();
            this.tabMainDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.tabFileInfo.SuspendLayout();
            this.tabIdentify.SuspendLayout();
            this.tabSets.SuspendLayout();
            this.tabTrailers.SuspendLayout();
            this.tabPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupLoadFromWeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMovieList)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.xtraTabControl2);
            this.layoutControl1.Controls.Add(this.picFanart);
            this.layoutControl1.Controls.Add(this.tabEditTabsControl);
            this.layoutControl1.Controls.Add(this.picPoster);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1200, 238, 354, 436);
            this.layoutControl1.Padding = new System.Windows.Forms.Padding(10);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1049, 714);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Location = new System.Drawing.Point(2, 2);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.tabByTitle;
            this.xtraTabControl2.Size = new System.Drawing.Size(259, 710);
            this.xtraTabControl2.TabIndex = 1;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabByTitle,
            this.tabByPoster});
            // 
            // tabByTitle
            // 
            this.tabByTitle.Controls.Add(this.grdMoviesList);
            this.tabByTitle.Name = "tabByTitle";
            this.tabByTitle.Size = new System.Drawing.Size(253, 684);
            this.tabByTitle.Text = "Title (0)";
            // 
            // grdMoviesList
            // 
            this.grdMoviesList.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode2.RelationName = "Level1";
            this.grdMoviesList.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.grdMoviesList.Location = new System.Drawing.Point(0, 0);
            this.grdMoviesList.MainView = this.grdViewByTitle;
            this.grdMoviesList.Name = "grdMoviesList";
            this.grdMoviesList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.RepositoryImageComboBox,
            this.BusyPicture,
            this.chkMarked});
            this.grdMoviesList.ShowOnlyPredefinedDetails = true;
            this.grdMoviesList.Size = new System.Drawing.Size(253, 684);
            this.grdMoviesList.TabIndex = 1;
            this.grdMoviesList.ToolTipController = this.toolTipController1;
            this.grdMoviesList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.grdViewByTitle});
            // 
            // grdViewByTitle
            // 
            this.grdViewByTitle.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LoadingStatus,
            this.clmTitle,
            this.clmYear,
            this.clmMarked,
            this.clmLocked,
            this.clmReleased});
            this.grdViewByTitle.GridControl = this.grdMoviesList;
            this.grdViewByTitle.Name = "grdViewByTitle";
            this.grdViewByTitle.OptionsSelection.MultiSelect = true;
            this.grdViewByTitle.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            this.grdViewByTitle.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GrdViewByTitle_RowCellStyle);
            this.grdViewByTitle.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.grdViewByTitle_PopupMenuShowing);
            this.grdViewByTitle.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.GrdViewByTitle_SelectionChanged);
            this.grdViewByTitle.DoubleClick += new System.EventHandler(this.GrdViewByTitle_DoubleClick);
            // 
            // LoadingStatus
            // 
            this.LoadingStatus.Caption = " ";
            this.LoadingStatus.ColumnEdit = this.BusyPicture;
            this.LoadingStatus.FieldName = "Busy";
            this.LoadingStatus.MaxWidth = 20;
            this.LoadingStatus.Name = "LoadingStatus";
            this.LoadingStatus.Visible = true;
            this.LoadingStatus.VisibleIndex = 0;
            this.LoadingStatus.Width = 20;
            // 
            // BusyPicture
            // 
            this.BusyPicture.Name = "BusyPicture";
            // 
            // clmTitle
            // 
            this.clmTitle.Caption = "Title";
            this.clmTitle.FieldName = "Title";
            this.clmTitle.Name = "clmTitle";
            this.clmTitle.OptionsColumn.AllowEdit = false;
            this.clmTitle.ToolTip = "Test tool tip";
            this.clmTitle.Visible = true;
            this.clmTitle.VisibleIndex = 1;
            this.clmTitle.Width = 104;
            // 
            // clmYear
            // 
            this.clmYear.Caption = "Year";
            this.clmYear.FieldName = "Year";
            this.clmYear.MaxWidth = 60;
            this.clmYear.Name = "clmYear";
            this.clmYear.OptionsColumn.AllowEdit = false;
            this.clmYear.Visible = true;
            this.clmYear.VisibleIndex = 2;
            this.clmYear.Width = 40;
            // 
            // clmMarked
            // 
            this.clmMarked.AppearanceHeader.Image = global::YANFOE.Properties.Resources.star_full;
            this.clmMarked.AppearanceHeader.Options.UseImage = true;
            this.clmMarked.ColumnEdit = this.chkMarked;
            this.clmMarked.FieldName = "Marked";
            this.clmMarked.MaxWidth = 22;
            this.clmMarked.Name = "clmMarked";
            this.clmMarked.Visible = true;
            this.clmMarked.VisibleIndex = 3;
            this.clmMarked.Width = 20;
            // 
            // chkMarked
            // 
            this.chkMarked.AutoHeight = false;
            this.chkMarked.Name = "chkMarked";
            this.chkMarked.PictureChecked = global::YANFOE.Properties.Resources.star_full;
            this.chkMarked.PictureUnchecked = global::YANFOE.Properties.Resources.star_empty;
            // 
            // clmLocked
            // 
            this.clmLocked.Caption = "Locked";
            this.clmLocked.FieldName = "Locked";
            this.clmLocked.MaxWidth = 20;
            this.clmLocked.Name = "clmLocked";
            this.clmLocked.Visible = true;
            this.clmLocked.VisibleIndex = 4;
            this.clmLocked.Width = 20;
            // 
            // clmReleased
            // 
            this.clmReleased.Caption = "Released";
            this.clmReleased.Name = "clmReleased";
            // 
            // RepositoryImageComboBox
            // 
            this.RepositoryImageComboBox.AutoHeight = false;
            this.RepositoryImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RepositoryImageComboBox.Name = "RepositoryImageComboBox";
            // 
            // toolTipController1
            // 
            this.toolTipController1.AllowHtmlText = true;
            this.toolTipController1.InitialDelay = 1;
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.ToolTipController1_GetActiveObjectInfo);
            // 
            // tabByPoster
            // 
            this.tabByPoster.Controls.Add(this.galleryControl1);
            this.tabByPoster.Name = "tabByPoster";
            this.tabByPoster.Size = new System.Drawing.Size(253, 684);
            this.tabByPoster.Text = "Poster (0)";
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            this.galleryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // galleryControlGallery2
            // 
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(100, 150);
            this.galleryControl1.Gallery.ItemClick += new DevExpress.XtraBars.Ribbon.GalleryItemClickEventHandler(this.GalleryItem_Click);
            this.galleryControl1.Location = new System.Drawing.Point(0, 0);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(253, 684);
            this.galleryControl1.TabIndex = 0;
            this.galleryControl1.Text = "Movies";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(232, 680);
            // 
            // picFanart
            // 
            this.picFanart.HeaderDetails = "No Image";
            this.picFanart.HeaderTitle = "Fanart";
            this.picFanart.Location = new System.Drawing.Point(692, 356);
            this.picFanart.Margin = new System.Windows.Forms.Padding(0);
            this.picFanart.Name = "picFanart";
            this.picFanart.Size = new System.Drawing.Size(352, 333);
            this.picFanart.TabIndex = 7;
            this.picFanart.Type = YANFOE.Tools.Enums.GalleryType.MovieFanart;
            // 
            // tabEditTabsControl
            // 
            this.tabEditTabsControl.Location = new System.Drawing.Point(270, 2);
            this.tabEditTabsControl.Name = "tabEditTabsControl";
            this.tabEditTabsControl.SelectedTabPage = this.tabMainDetails;
            this.tabEditTabsControl.Size = new System.Drawing.Size(777, 342);
            this.tabEditTabsControl.TabIndex = 5;
            this.tabEditTabsControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMainDetails,
            this.tabFileInfo,
            this.tabIdentify,
            this.tabSets,
            this.tabTrailers,
            this.tabPreview});
            // 
            // tabMainDetails
            // 
            this.tabMainDetails.Controls.Add(this.movieMainDetailsUserControl1);
            this.tabMainDetails.Controls.Add(this.panelControl1);
            this.tabMainDetails.Name = "tabMainDetails";
            this.tabMainDetails.Size = new System.Drawing.Size(771, 316);
            this.tabMainDetails.Text = "Main Details";
            // 
            // movieMainDetailsUserControl1
            // 
            this.movieMainDetailsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieMainDetailsUserControl1.Location = new System.Drawing.Point(0, 44);
            this.movieMainDetailsUserControl1.Name = "movieMainDetailsUserControl1";
            this.movieMainDetailsUserControl1.Size = new System.Drawing.Size(771, 272);
            this.movieMainDetailsUserControl1.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton4);
            this.panelControl1.Controls.Add(this.simpleButton3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.btnNew);
            this.panelControl1.Controls.Add(this.btnMarked);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.btnLoadFromWeb);
            this.panelControl1.Controls.Add(this.btnLock);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(771, 44);
            this.panelControl1.TabIndex = 1;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton4.Enabled = false;
            this.simpleButton4.Image = global::YANFOE.Properties.Resources.folder32;
            this.simpleButton4.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton4.Location = new System.Drawing.Point(156, 2);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(48, 40);
            superToolTip8.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem10.Text = "Open Movie";
            toolTipItem6.LeftIndent = 6;
            toolTipItem6.Text = "Clicking this link will open the movie files <fileName>\r\n";
            superToolTip8.Items.Add(toolTipTitleItem10);
            superToolTip8.Items.Add(toolTipItem6);
            this.simpleButton4.SuperTip = superToolTip8;
            this.simpleButton4.TabIndex = 11;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButton3.Enabled = false;
            this.simpleButton3.Image = global::YANFOE.Properties.Resources.monitor;
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(108, 2);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(48, 40);
            superToolTip9.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem11.Text = "Open Movie";
            toolTipItem7.LeftIndent = 6;
            toolTipItem7.Text = "Clicking this link will open the movie files <fileName>\r\n";
            superToolTip9.Items.Add(toolTipTitleItem11);
            superToolTip9.Items.Add(toolTipItem7);
            this.simpleButton3.SuperTip = superToolTip9;
            this.simpleButton3.TabIndex = 10;
            // 
            // panelControl2
            // 
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl2.Location = new System.Drawing.Point(98, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(10, 40);
            this.panelControl2.TabIndex = 9;
            // 
            // btnNew
            // 
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNew.Image = global::YANFOE.Properties.Resources.new32;
            this.btnNew.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNew.Location = new System.Drawing.Point(625, 2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(48, 40);
            superToolTip10.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem12.Appearance.Image = global::YANFOE.Properties.Resources.promo_red16;
            toolTipTitleItem12.Appearance.Options.UseImage = true;
            toolTipTitleItem12.Image = global::YANFOE.Properties.Resources.promo_red16;
            toolTipTitleItem12.Text = "Movie is marked as New";
            toolTipItem8.LeftIndent = 6;
            toolTipItem8.Text = "The new status will be removed next time YANFOE is started.<br>\r\n\r\n\r\n";
            superToolTip10.Items.Add(toolTipTitleItem12);
            superToolTip10.Items.Add(toolTipItem8);
            this.btnNew.SuperTip = superToolTip10;
            this.btnNew.TabIndex = 7;
            this.btnNew.Visible = false;
            this.btnNew.DoubleClick += new System.EventHandler(this.BtnNew_DoubleClick);
            // 
            // btnMarked
            // 
            this.btnMarked.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMarked.Image = global::YANFOE.Properties.Resources.star_empty32;
            this.btnMarked.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMarked.Location = new System.Drawing.Point(673, 2);
            this.btnMarked.Name = "btnMarked";
            this.btnMarked.Size = new System.Drawing.Size(48, 40);
            superToolTip11.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem13.Text = "Marked / Unmarked";
            superToolTip11.Items.Add(toolTipTitleItem13);
            this.btnMarked.SuperTip = superToolTip11;
            this.btnMarked.TabIndex = 6;
            this.btnMarked.Tag = "unmarked";
            this.btnMarked.Click += new System.EventHandler(this.BtnMarked_Click);
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.Image = global::YANFOE.Properties.Resources.save32;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(50, 2);
            this.btnSave.Name = "btnSave";
            this.barManager1.SetPopupContextMenu(this.btnSave, this.popupSave);
            this.btnSave.Size = new System.Drawing.Size(48, 40);
            superToolTip12.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem14.Text = "Save";
            toolTipItem9.LeftIndent = 6;
            toolTipItem9.Text = "Will Save Both NFO and Images To Disk";
            toolTipTitleItem15.LeftIndent = 6;
            toolTipTitleItem15.Text = "Right click for more options...\r\n";
            superToolTip12.Items.Add(toolTipTitleItem14);
            superToolTip12.Items.Add(toolTipItem9);
            superToolTip12.Items.Add(toolTipSeparatorItem3);
            superToolTip12.Items.Add(toolTipTitleItem15);
            this.btnSave.SuperTip = superToolTip12;
            this.btnSave.TabIndex = 4;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnLoadFromWeb
            // 
            this.btnLoadFromWeb.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLoadFromWeb.Image = global::YANFOE.Properties.Resources.globe32;
            this.btnLoadFromWeb.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLoadFromWeb.Location = new System.Drawing.Point(2, 2);
            this.btnLoadFromWeb.Name = "btnLoadFromWeb";
            this.barManager1.SetPopupContextMenu(this.btnLoadFromWeb, this.popupLoadFromWeb);
            this.btnLoadFromWeb.Size = new System.Drawing.Size(48, 40);
            superToolTip5.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem5.Text = "Load From Web";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Will download details using the scraper set for this movie.";
            toolTipTitleItem6.LeftIndent = 6;
            toolTipTitleItem6.Text = "Right click for more options...\r\n";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem4);
            superToolTip5.Items.Add(toolTipSeparatorItem1);
            superToolTip5.Items.Add(toolTipTitleItem6);
            this.btnLoadFromWeb.SuperTip = superToolTip5;
            this.btnLoadFromWeb.TabIndex = 0;
            this.btnLoadFromWeb.Click += new System.EventHandler(this.BtnLoadFromWeb_Click);
            // 
            // btnLock
            // 
            this.btnLock.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLock.Image = global::YANFOE.Properties.Resources.unlock32;
            this.btnLock.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnLock.Location = new System.Drawing.Point(721, 2);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(48, 40);
            superToolTip6.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem7.Text = "Locked / Unlock";
            superToolTip6.Items.Add(toolTipTitleItem7);
            this.btnLock.SuperTip = superToolTip6;
            this.btnLock.TabIndex = 8;
            this.btnLock.Tag = "unlocked";
            this.btnLock.Click += new System.EventHandler(this.BtnLock_Click);
            // 
            // tabFileInfo
            // 
            this.tabFileInfo.Controls.Add(this.movieFileInfoUserControl1);
            this.tabFileInfo.Name = "tabFileInfo";
            this.tabFileInfo.Size = new System.Drawing.Size(771, 316);
            this.tabFileInfo.Text = "File Info";
            // 
            // movieFileInfoUserControl1
            // 
            this.movieFileInfoUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieFileInfoUserControl1.Location = new System.Drawing.Point(0, 0);
            this.movieFileInfoUserControl1.Name = "movieFileInfoUserControl1";
            this.movieFileInfoUserControl1.Size = new System.Drawing.Size(771, 316);
            this.movieFileInfoUserControl1.TabIndex = 0;
            // 
            // tabIdentify
            // 
            this.tabIdentify.Controls.Add(this.movieIdentierUserControl11);
            this.tabIdentify.Name = "tabIdentify";
            this.tabIdentify.Size = new System.Drawing.Size(771, 316);
            this.tabIdentify.Text = "Identifier";
            // 
            // movieIdentierUserControl11
            // 
            this.movieIdentierUserControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieIdentierUserControl11.Location = new System.Drawing.Point(0, 0);
            this.movieIdentierUserControl11.Name = "movieIdentierUserControl11";
            this.movieIdentierUserControl11.Size = new System.Drawing.Size(771, 316);
            this.movieIdentierUserControl11.TabIndex = 0;
            // 
            // tabSets
            // 
            this.tabSets.Controls.Add(this.setManagerUserControl1);
            this.tabSets.Name = "tabSets";
            this.tabSets.Size = new System.Drawing.Size(771, 316);
            this.tabSets.Text = "Set";
            // 
            // setManagerUserControl1
            // 
            this.setManagerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setManagerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.setManagerUserControl1.Name = "setManagerUserControl1";
            this.setManagerUserControl1.Size = new System.Drawing.Size(771, 316);
            this.setManagerUserControl1.TabIndex = 0;
            // 
            // tabTrailers
            // 
            this.tabTrailers.Controls.Add(this.movieTrailerUserControl1);
            this.tabTrailers.Name = "tabTrailers";
            this.tabTrailers.Size = new System.Drawing.Size(771, 316);
            this.tabTrailers.Text = "Trailers";
            // 
            // movieTrailerUserControl1
            // 
            this.movieTrailerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movieTrailerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.movieTrailerUserControl1.Name = "movieTrailerUserControl1";
            this.movieTrailerUserControl1.Size = new System.Drawing.Size(771, 316);
            this.movieTrailerUserControl1.TabIndex = 0;
            // 
            // tabPreview
            // 
            this.tabPreview.Controls.Add(this.nfoPreviewUserControl1);
            this.tabPreview.Name = "tabPreview";
            this.tabPreview.Size = new System.Drawing.Size(771, 316);
            this.tabPreview.Text = "Previews";
            // 
            // nfoPreviewUserControl1
            // 
            this.nfoPreviewUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nfoPreviewUserControl1.Location = new System.Drawing.Point(0, 0);
            this.nfoPreviewUserControl1.Name = "nfoPreviewUserControl1";
            this.nfoPreviewUserControl1.PreviewArea = YANFOE.UI.UserControls.CommonControls.NFOPreviewUserControl.TvOrMovies.Movies;
            this.nfoPreviewUserControl1.Size = new System.Drawing.Size(771, 316);
            this.nfoPreviewUserControl1.TabIndex = 0;
            // 
            // picPoster
            // 
            this.picPoster.HeaderDetails = "No Image";
            this.picPoster.HeaderTitle = "Poster";
            this.picPoster.Location = new System.Drawing.Point(273, 356);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(410, 333);
            this.picPoster.TabIndex = 6;
            this.picPoster.Type = YANFOE.Tools.Enums.GalleryType.MoviePoster;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlImages,
            this.splitterItem3,
            this.layoutControlItem1,
            this.splitterItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1049, 714);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.tabEditTabsControl;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(268, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(781, 346);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlImages
            // 
            this.layoutControlImages.CustomizationFormText = "Images";
            this.layoutControlImages.ExpandButtonMode = DevExpress.Utils.Controls.ExpandButtonMode.Inverted;
            this.layoutControlImages.ExpandButtonVisible = true;
            this.layoutControlImages.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.splitterItem1,
            this.layoutControlItem3});
            this.layoutControlImages.Location = new System.Drawing.Point(268, 351);
            this.layoutControlImages.Name = "layoutControlImages";
            this.layoutControlImages.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlImages.Size = new System.Drawing.Size(781, 363);
            this.layoutControlImages.Text = "Show / Hide Images";
            this.layoutControlImages.TextLocation = DevExpress.Utils.Locations.Bottom;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.picFanart;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(419, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(356, 337);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(414, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(5, 337);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.picPoster;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(414, 337);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // splitterItem3
            // 
            this.splitterItem3.AllowHotTrack = true;
            this.splitterItem3.CustomizationFormText = "splitterItem3";
            this.splitterItem3.Location = new System.Drawing.Point(268, 346);
            this.splitterItem3.Name = "splitterItem3";
            this.splitterItem3.Size = new System.Drawing.Size(781, 5);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.xtraTabControl2;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(263, 714);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // splitterItem2
            // 
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.CustomizationFormText = "splitterItem2";
            this.splitterItem2.Location = new System.Drawing.Point(263, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new System.Drawing.Size(5, 714);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barStaticItem1,
            this.barSubItem1,
            this.barButtonItem2,
            this.barStaticItem2,
            this.btnSaveNfo,
            this.btnSaveAllImages,
            this.btnSavePoster,
            this.btnSaveFanart,
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barCheckItem1,
            this.barCheckItem2});
            this.barManager1.MaxItemId = 15;
            // 
            // popupSave
            // 
            this.popupSave.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveNfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveAllImages),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSavePoster, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSaveFanart),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8, true)});
            this.popupSave.Manager = this.barManager1;
            this.popupSave.Name = "popupSave";
            // 
            // btnSaveNfo
            // 
            this.btnSaveNfo.Caption = "Save NFO";
            this.btnSaveNfo.Glyph = global::YANFOE.Properties.Resources.school_board;
            this.btnSaveNfo.Id = 5;
            this.btnSaveNfo.Name = "btnSaveNfo";
            this.btnSaveNfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSaveNfo_ItemClick);
            // 
            // btnSaveAllImages
            // 
            this.btnSaveAllImages.Caption = "Save All Images";
            this.btnSaveAllImages.Glyph = global::YANFOE.Properties.Resources.picture;
            this.btnSaveAllImages.Id = 6;
            this.btnSaveAllImages.Name = "btnSaveAllImages";
            this.btnSaveAllImages.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSaveAllImages_ItemClick);
            // 
            // btnSavePoster
            // 
            this.btnSavePoster.Caption = "Save Poster";
            this.btnSavePoster.Glyph = global::YANFOE.Properties.Resources.picture_poster;
            this.btnSavePoster.Id = 7;
            this.btnSavePoster.Name = "btnSavePoster";
            this.btnSavePoster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSavePoster_ItemClick);
            // 
            // btnSaveFanart
            // 
            this.btnSaveFanart.Caption = "Save Fanart";
            this.btnSaveFanart.Glyph = global::YANFOE.Properties.Resources.picture;
            this.btnSaveFanart.Id = 8;
            this.btnSaveFanart.Name = "btnSaveFanart";
            this.btnSaveFanart.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSaveFanart_ItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "Save All";
            this.barButtonItem8.Glyph = global::YANFOE.Properties.Resources.save;
            this.barButtonItem8.Id = 10;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSave_Click);
            // 
            // popupLoadFromWeb
            // 
            this.popupLoadFromWeb.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7, true)});
            this.popupLoadFromWeb.Manager = this.barManager1;
            this.popupLoadFromWeb.Name = "popupLoadFromWeb";
            this.popupLoadFromWeb.ShowCaption = true;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "<Current Scraper Group>";
            this.barStaticItem2.Id = 4;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Scrape Using <alternative 1>";
            this.barButtonItem7.Id = 9;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1049, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 714);
            this.barDockControlBottom.Size = new System.Drawing.Size(1049, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 714);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1049, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 714);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Load From Web";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Load From Web";
            this.barStaticItem1.Id = 1;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 2;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Scrape Text From Scraper Group";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Lock";
            this.barButtonItem3.Id = 11;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Unlock";
            this.barButtonItem4.Id = 12;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barCheckItem1
            // 
            this.barCheckItem1.Caption = "Lock";
            this.barCheckItem1.Glyph = global::YANFOE.Properties.Resources.unlock32;
            this.barCheckItem1.Id = 13;
            this.barCheckItem1.Name = "barCheckItem1";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.Caption = "Mark";
            this.barCheckItem2.Glyph = global::YANFOE.Properties.Resources.star_empty32;
            this.barCheckItem2.Id = 14;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "lock.png");
            this.imageCollection1.Images.SetKeyName(1, "star_full.png");
            // 
            // popupMovieList
            // 
            this.popupMovieList.Manager = this.barManager1;
            this.popupMovieList.Name = "popupMovieList";
            this.popupMovieList.BeforePopup += new System.ComponentModel.CancelEventHandler(this.popupMovieList_BeforePopup);
            // 
            // MoviesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "MoviesUserControl";
            this.Size = new System.Drawing.Size(1049, 714);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.tabByTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMoviesList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdViewByTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BusyPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMarked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RepositoryImageComboBox)).EndInit();
            this.tabByPoster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabEditTabsControl)).EndInit();
            this.tabEditTabsControl.ResumeLayout(false);
            this.tabMainDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.tabFileInfo.ResumeLayout(false);
            this.tabIdentify.ResumeLayout(false);
            this.tabSets.ResumeLayout(false);
            this.tabTrailers.ResumeLayout(false);
            this.tabPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupLoadFromWeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMovieList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTab.XtraTabControl tabEditTabsControl;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraTab.XtraTabPage tabMainDetails;
        private DevExpress.XtraTab.XtraTabPage tabFileInfo;
        private DevExpress.XtraTab.XtraTabPage tabTrailers;
        private DevExpress.XtraTab.XtraTabPage tabPreview;
        private DisplayPictureUserControl picFanart;
        private DisplayPictureUserControl picPoster;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private MovieMainDetailsUserControl movieMainDetailsUserControl1;
        private DevExpress.XtraTab.XtraTabPage tabSets;
        private SetManagerUserControl setManagerUserControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLoadFromWeb;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnMarked;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.SimpleButton btnLock;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.PopupMenu popupLoadFromWeb;
        private DevExpress.XtraBars.BarButtonItem btnSaveNfo;
        private DevExpress.XtraBars.BarButtonItem btnSaveAllImages;
        private DevExpress.XtraBars.BarButtonItem btnSavePoster;
        private DevExpress.XtraBars.BarButtonItem btnSaveFanart;
        private DevExpress.XtraBars.PopupMenu popupSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraTab.XtraTabPage tabIdentify;
        private MovieIdentierUserControl1 movieIdentierUserControl11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlImages;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private NFOPreviewUserControl nfoPreviewUserControl1;
        private DevExpress.XtraLayout.SplitterItem splitterItem3;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl2;
        private DevExpress.XtraTab.XtraTabPage tabByTitle;
        private DevExpress.XtraGrid.GridControl grdMoviesList;
        private DevExpress.XtraGrid.Views.Grid.GridView grdViewByTitle;
        private DevExpress.XtraGrid.Columns.GridColumn LoadingStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit BusyPicture;
        private DevExpress.XtraGrid.Columns.GridColumn clmTitle;
        private DevExpress.XtraGrid.Columns.GridColumn clmYear;
        private DevExpress.XtraGrid.Columns.GridColumn clmMarked;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkMarked;
        private DevExpress.XtraGrid.Columns.GridColumn clmLocked;
        private DevExpress.XtraGrid.Columns.GridColumn clmReleased;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox RepositoryImageComboBox;
        private DevExpress.XtraTab.XtraTabPage tabByPoster;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Ribbon.Gallery.GalleryControlGallery galleryControlGallery1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SplitterItem splitterItem2;
        private MovieFileInfoUserControl movieFileInfoUserControl1;
        private MovieTrailerUserControl movieTrailerUserControl1;
        private DevExpress.XtraBars.PopupMenu popupMovieList;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarCheckItem barCheckItem1;
        private DevExpress.XtraBars.BarCheckItem barCheckItem2;

    }
}
