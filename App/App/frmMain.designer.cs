using YANFOE.UI.UserControls.MediaManagerControls;

namespace YANFOE
{
    using YANFOE.UI.UserControls.DownloadControls;
    using YANFOE.UI.UserControls.LogControls;
    using YANFOE.UI.UserControls.MovieControls;
    using YANFOE.UI.UserControls.TvControls;

    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem5 = new DevExpress.XtraBars.BarSubItem();
            this.mnuFileSaveDatabase = new DevExpress.XtraBars.BarButtonItem();
            this.mnuFileExit = new DevExpress.XtraBars.BarButtonItem();
            this.mnuEdit = new DevExpress.XtraBars.BarSubItem();
            this.mnuEditSettings = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem7 = new DevExpress.XtraBars.BarSubItem();
            this.mnuToolsMovieScraperGroupManager = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem9 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem4 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem8 = new DevExpress.XtraBars.BarSubItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.tabControlMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabMediaManager = new DevExpress.XtraTab.XtraTabPage();
            this.mediaManagerUc1 = new YANFOE.UI.UserControls.MediaManagerControls.MediaManagerUc();
            this.tabMovies = new DevExpress.XtraTab.XtraTabPage();
            this.moviesUserControl1 = new YANFOE.UI.UserControls.MovieControls.MoviesUserControl();
            this.tabTv = new DevExpress.XtraTab.XtraTabPage();
            this.tvUserControl1 = new YANFOE.UI.UserControls.TvControls.TvUserControl();
            this.tabDownloads = new DevExpress.XtraTab.XtraTabPage();
            this.downloadsUserControl1 = new YANFOE.UI.UserControls.DownloadControls.DownloadsUserControl();
            this.tabLogs = new DevExpress.XtraTab.XtraTabPage();
            this.logsUserControl1 = new YANFOE.UI.UserControls.LogControls.LogsUserControl();
            this.tabAbout = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblDownloadStatus = new DevExpress.XtraEditors.LabelControl();
            this.picThread4 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread3 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread2 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread1 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.uiTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabMediaManager.SuspendLayout();
            this.tabMovies.SuspendLayout();
            this.tabTv.SuspendLayout();
            this.tabDownloads.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThread4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem2,
            this.barSubItem3,
            this.barSubItem4,
            this.barButtonItem1,
            this.barSubItem5,
            this.mnuEdit,
            this.barSubItem7,
            this.barSubItem8,
            this.barStaticItem1,
            this.barStaticItem2,
            this.barStaticItem3,
            this.barSubItem9,
            this.barButtonItem2,
            this.mnuFileExit,
            this.barButtonItem5,
            this.barButtonItem6,
            this.mnuToolsMovieScraperGroupManager,
            this.mnuFileSaveDatabase,
            this.mnuEditSettings});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 30;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 5";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem5, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem9)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 5";
            // 
            // barSubItem5
            // 
            this.barSubItem5.Caption = global::YANFOE.Language.FrmMain_Menu_File;
            this.barSubItem5.Glyph = global::YANFOE.Properties.Resources.save32;
            this.barSubItem5.Id = 5;
            this.barSubItem5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuFileSaveDatabase),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuFileExit)});
            this.barSubItem5.Name = "barSubItem5";
            // 
            // mnuFileSaveDatabase
            // 
            this.mnuFileSaveDatabase.Caption = "Save Database";
            this.mnuFileSaveDatabase.Id = 28;
            this.mnuFileSaveDatabase.Name = "mnuFileSaveDatabase";
            this.mnuFileSaveDatabase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuFileSaveDatabase_ItemClick);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Caption = global::YANFOE.Language.FrmMain_Menu_File_Exit;
            this.mnuFileExit.Glyph = global::YANFOE.Properties.Resources.shut_down;
            this.mnuFileExit.Id = 22;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuFileExit_ItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Caption = global::YANFOE.Language.FrmMain_Menu_Edit;
            this.mnuEdit.Id = 6;
            this.mnuEdit.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuEditSettings)});
            this.mnuEdit.Name = "mnuEdit";
            // 
            // mnuEditSettings
            // 
            this.mnuEditSettings.Caption = "Settings";
            this.mnuEditSettings.Id = 29;
            this.mnuEditSettings.Name = "mnuEditSettings";
            this.mnuEditSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuEditSettings_ItemClick);
            // 
            // barSubItem7
            // 
            this.barSubItem7.Caption = global::YANFOE.Language.FrmMain_Menu_Tools;
            this.barSubItem7.Id = 7;
            this.barSubItem7.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuToolsMovieScraperGroupManager)});
            this.barSubItem7.Name = "barSubItem7";
            // 
            // mnuToolsMovieScraperGroupManager
            // 
            this.mnuToolsMovieScraperGroupManager.Caption = global::YANFOE.Language.FrmMain_Menu_Tools_MovieScraperGroupManager;
            this.mnuToolsMovieScraperGroupManager.Id = 25;
            this.mnuToolsMovieScraperGroupManager.Name = "mnuToolsMovieScraperGroupManager";
            this.mnuToolsMovieScraperGroupManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuToolsMovieScraperGroupManager_ItemClick);
            // 
            // barSubItem9
            // 
            this.barSubItem9.Caption = "Help";
            this.barSubItem9.Id = 19;
            this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2, true)});
            this.barSubItem9.Name = "barSubItem9";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Id = 26;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Check For Updates";
            this.barButtonItem6.Id = 24;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = global::YANFOE.Language.FrmMain_Menu_About;
            this.barButtonItem2.Glyph = global::YANFOE.Properties.Resources.home;
            this.barButtonItem2.Id = 20;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1016, 22);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 646);
            this.barDockControlBottom.Size = new System.Drawing.Size(1016, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 22);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 624);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1016, 22);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 624);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = global::YANFOE.Language.FrmMain_Menu_File;
            this.barSubItem1.Id = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = global::YANFOE.Language.FrmMain_Menu_File_Exit;
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = global::YANFOE.Language.FrmMain_Menu_Edit;
            this.barSubItem2.Id = 1;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = global::YANFOE.Language.FrmMain_Menu_Tools;
            this.barSubItem3.Id = 2;
            this.barSubItem3.Name = "barSubItem3";
            // 
            // barSubItem4
            // 
            this.barSubItem4.Id = 27;
            this.barSubItem4.Name = "barSubItem4";
            // 
            // barSubItem8
            // 
            this.barSubItem8.Caption = "Help";
            this.barSubItem8.Id = 8;
            this.barSubItem8.Name = "barSubItem8";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "Movies: 0";
            this.barStaticItem1.Id = 9;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "Tv Series: 0";
            this.barStaticItem2.Id = 10;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "Tv Episodes: 0";
            this.barStaticItem3.Id = 12;
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 22);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Windows.Forms.Padding(5);
            this.tabControlMain.SelectedTabPage = this.tabMediaManager;
            this.tabControlMain.Size = new System.Drawing.Size(1016, 597);
            this.tabControlMain.TabIndex = 4;
            this.tabControlMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabMediaManager,
            this.tabMovies,
            this.tabTv,
            this.tabDownloads,
            this.tabLogs,
            this.tabAbout});
            // 
            // tabMediaManager
            // 
            this.tabMediaManager.Controls.Add(this.mediaManagerUc1);
            this.tabMediaManager.Image = global::YANFOE.Properties.Resources.database24;
            this.tabMediaManager.Name = "tabMediaManager";
            this.tabMediaManager.Size = new System.Drawing.Size(1010, 560);
            this.tabMediaManager.Text = "Media Manager";
            // 
            // mediaManagerUc1
            // 
            this.mediaManagerUc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaManagerUc1.Location = new System.Drawing.Point(0, 0);
            this.mediaManagerUc1.Name = "mediaManagerUc1";
            this.mediaManagerUc1.Size = new System.Drawing.Size(1010, 560);
            this.mediaManagerUc1.TabIndex = 0;
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.moviesUserControl1);
            this.tabMovies.Image = global::YANFOE.Properties.Resources.video24;
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.Size = new System.Drawing.Size(1010, 560);
            this.tabMovies.Text = "Movies";
            // 
            // moviesUserControl1
            // 
            this.moviesUserControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.moviesUserControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(73)))), ((int)(((byte)(88)))));
            this.moviesUserControl1.Appearance.Options.UseBackColor = true;
            this.moviesUserControl1.Appearance.Options.UseForeColor = true;
            this.moviesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.moviesUserControl1.Location = new System.Drawing.Point(0, 0);
            this.moviesUserControl1.Name = "moviesUserControl1";
            this.moviesUserControl1.Size = new System.Drawing.Size(1012, 562);
            this.moviesUserControl1.TabIndex = 0;
            // 
            // tabTv
            // 
            this.tabTv.Controls.Add(this.tvUserControl1);
            this.tabTv.Image = global::YANFOE.Properties.Resources.television24;
            this.tabTv.Name = "tabTv";
            this.tabTv.Size = new System.Drawing.Size(1010, 560);
            this.tabTv.Text = "TV";
            // 
            // tvUserControl1
            // 
            this.tvUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUserControl1.Location = new System.Drawing.Point(0, 0);
            this.tvUserControl1.Name = "tvUserControl1";
            this.tvUserControl1.Size = new System.Drawing.Size(1012, 562);
            this.tvUserControl1.TabIndex = 0;
            // 
            // tabDownloads
            // 
            this.tabDownloads.Controls.Add(this.downloadsUserControl1);
            this.tabDownloads.Image = global::YANFOE.Properties.Resources.download24;
            this.tabDownloads.Name = "tabDownloads";
            this.tabDownloads.Size = new System.Drawing.Size(1010, 560);
            this.tabDownloads.Text = "Downloads";
            // 
            // downloadsUserControl1
            // 
            this.downloadsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.downloadsUserControl1.Name = "downloadsUserControl1";
            this.downloadsUserControl1.Size = new System.Drawing.Size(1012, 562);
            this.downloadsUserControl1.TabIndex = 0;
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.logsUserControl1);
            this.tabLogs.Image = global::YANFOE.Properties.Resources.books24;
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Size = new System.Drawing.Size(1010, 560);
            this.tabLogs.Text = "Logs";
            // 
            // logsUserControl1
            // 
            this.logsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.logsUserControl1.Name = "logsUserControl1";
            this.logsUserControl1.Size = new System.Drawing.Size(1012, 562);
            this.logsUserControl1.TabIndex = 0;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.layoutControl1);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(1010, 560);
            this.tabAbout.Text = "About";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1012, 562);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(988, 538);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "About YANFOE";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(1012, 562);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.groupControl1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(992, 542);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem3.Id = 14;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem4.Id = 14;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblDownloadStatus);
            this.panelControl1.Controls.Add(this.picThread4);
            this.panelControl1.Controls.Add(this.picThread3);
            this.panelControl1.Controls.Add(this.picThread2);
            this.panelControl1.Controls.Add(this.picThread1);
            this.panelControl1.Controls.Add(this.pictureBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 619);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panelControl1.Size = new System.Drawing.Size(1016, 27);
            this.panelControl1.TabIndex = 9;
            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.Location = new System.Drawing.Point(102, 6);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(63, 13);
            this.lblDownloadStatus.TabIndex = 7;
            this.lblDownloadStatus.Text = "labelControl1";
            // 
            // picThread4
            // 
            this.picThread4.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread4.Location = new System.Drawing.Point(72, 3);
            this.picThread4.MenuManager = this.barManager1;
            this.picThread4.Name = "picThread4";
            this.picThread4.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread4.Properties.Appearance.Options.UseBackColor = true;
            this.picThread4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread4.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread4.Size = new System.Drawing.Size(24, 24);
            this.picThread4.TabIndex = 6;
            // 
            // picThread3
            // 
            this.picThread3.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread3.Location = new System.Drawing.Point(48, 3);
            this.picThread3.MenuManager = this.barManager1;
            this.picThread3.Name = "picThread3";
            this.picThread3.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread3.Properties.Appearance.Options.UseBackColor = true;
            this.picThread3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread3.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread3.Size = new System.Drawing.Size(24, 24);
            this.picThread3.TabIndex = 5;
            // 
            // picThread2
            // 
            this.picThread2.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread2.Location = new System.Drawing.Point(24, 3);
            this.picThread2.MenuManager = this.barManager1;
            this.picThread2.Name = "picThread2";
            this.picThread2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread2.Properties.Appearance.Options.UseBackColor = true;
            this.picThread2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread2.Size = new System.Drawing.Size(24, 24);
            this.picThread2.TabIndex = 4;
            // 
            // picThread1
            // 
            this.picThread1.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread1.Location = new System.Drawing.Point(0, 3);
            this.picThread1.MenuManager = this.barManager1;
            this.picThread1.Name = "picThread1";
            this.picThread1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread1.Properties.Appearance.Options.UseBackColor = true;
            this.picThread1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread1.Size = new System.Drawing.Size(24, 24);
            this.picThread1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::YANFOE.Properties.Resources.accept24;
            this.pictureBox1.Location = new System.Drawing.Point(988, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 24);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Interval = 50;
            this.uiTimer.Tick += new System.EventHandler(this.UiTimer_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 646);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Sharp Plus";
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YANFOE 2 - Early Alpha 1";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabMediaManager.ResumeLayout(false);
            this.tabMovies.ResumeLayout(false);
            this.tabTv.ResumeLayout(false);
            this.tabDownloads.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picThread4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTab.XtraTabControl tabControlMain;
        private DevExpress.XtraTab.XtraTabPage tabMovies;
        private DevExpress.XtraTab.XtraTabPage tabTv;
        private DevExpress.XtraTab.XtraTabPage tabDownloads;
        private DevExpress.XtraTab.XtraTabPage tabMediaManager;
        private DevExpress.XtraTab.XtraTabPage tabLogs;
        private MoviesUserControl moviesUserControl1;
        private TvUserControl tvUserControl1;
        private DownloadsUserControl downloadsUserControl1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarSubItem barSubItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem barSubItem5;
        private DevExpress.XtraBars.BarSubItem mnuEdit;
        private DevExpress.XtraBars.BarSubItem barSubItem7;
        private DevExpress.XtraBars.BarSubItem barSubItem8;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private LogsUserControl logsUserControl1;
        private DevExpress.XtraBars.BarButtonItem mnuFileExit;
        private DevExpress.XtraBars.BarSubItem barSubItem9;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraTab.XtraTabPage tabAbout;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem mnuToolsMovieScraperGroupManager;
        private DevExpress.XtraBars.BarButtonItem mnuFileSaveDatabase;
        private System.Windows.Forms.Timer uiTimer;
        private DevExpress.XtraEditors.PictureEdit picThread4;
        private DevExpress.XtraEditors.PictureEdit picThread3;
        private DevExpress.XtraEditors.PictureEdit picThread2;
        private DevExpress.XtraEditors.PictureEdit picThread1;
        private DevExpress.XtraEditors.LabelControl lblDownloadStatus;
        private MediaManagerUc mediaManagerUc1;
        private DevExpress.XtraBars.BarButtonItem mnuEditSettings;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
    }
}