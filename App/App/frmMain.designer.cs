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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
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
            this.mnuHelpReportIssues = new DevExpress.XtraBars.BarButtonItem();
            this.mnuHelpSourceCode = new DevExpress.XtraBars.BarButtonItem();
            this.mnuHelpWiki = new DevExpress.XtraBars.BarButtonItem();
            this.mnuHelpHomepage = new DevExpress.XtraBars.BarButtonItem();
            this.mnuDonate = new DevExpress.XtraBars.BarButtonItem();
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
            this.mediaPathManager1 = new YANFOE.UI.UserControls.MediaManagerControls.MediaPathManager();
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
            this.hyperLinkEdit5 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.memoEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.textEdit5 = new DevExpress.XtraEditors.TextEdit();
            this.hyperLinkEdit4 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyperLinkEdit3 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyperLinkEdit2 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.txtBuild = new DevExpress.XtraEditors.TextEdit();
            this.txtVersion = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.picUpdateStatus = new DevExpress.XtraEditors.PictureEdit();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.picThread8 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread7 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread6 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread5 = new DevExpress.XtraEditors.PictureEdit();
            this.lblDownloadStatus = new DevExpress.XtraEditors.LabelControl();
            this.picThread4 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread3 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread2 = new DevExpress.XtraEditors.PictureEdit();
            this.picThread1 = new DevExpress.XtraEditors.PictureEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread8.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread6.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread1.Properties)).BeginInit();
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
            this.mnuFileExit,
            this.mnuToolsMovieScraperGroupManager,
            this.mnuFileSaveDatabase,
            this.mnuEditSettings,
            this.mnuHelpReportIssues,
            this.mnuHelpHomepage,
            this.mnuHelpSourceCode,
            this.mnuHelpWiki,
            this.mnuDonate});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 35;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem9),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuDonate)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DisableClose = true;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.DrawDragBorder = false;
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
            this.mnuFileSaveDatabase.Glyph = global::YANFOE.Properties.Resources.save;
            this.mnuFileSaveDatabase.Id = 28;
            this.mnuFileSaveDatabase.Name = "mnuFileSaveDatabase";
            this.mnuFileSaveDatabase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuFileSaveDatabase_ItemClick);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Caption = global::YANFOE.Language.FrmMain_Menu_File_Exit;
            this.mnuFileExit.Glyph = global::YANFOE.Properties.Resources.shut_down;
            this.mnuFileExit.Id = 22;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuFileExit_ItemClick);
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
            this.mnuEditSettings.Glyph = global::YANFOE.Properties.Resources.tools2;
            this.mnuEditSettings.Id = 29;
            this.mnuEditSettings.Name = "mnuEditSettings";
            this.mnuEditSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuEditSettings_ItemClick);
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
            this.mnuToolsMovieScraperGroupManager.Glyph = global::YANFOE.Properties.Resources.scrapergroupmanager;
            this.mnuToolsMovieScraperGroupManager.Id = 25;
            this.mnuToolsMovieScraperGroupManager.LargeGlyph = global::YANFOE.Properties.Resources.scrapergroupmanager;
            this.mnuToolsMovieScraperGroupManager.Name = "mnuToolsMovieScraperGroupManager";
            this.mnuToolsMovieScraperGroupManager.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuToolsMovieScraperGroupManager_ItemClick);
            // 
            // barSubItem9
            // 
            this.barSubItem9.Caption = "Help";
            this.barSubItem9.Id = 19;
            this.barSubItem9.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuHelpReportIssues, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuHelpSourceCode),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuHelpWiki),
            new DevExpress.XtraBars.LinkPersistInfo(this.mnuHelpHomepage, true)});
            this.barSubItem9.Name = "barSubItem9";
            // 
            // mnuHelpReportIssues
            // 
            this.mnuHelpReportIssues.Caption = "Report Issues";
            this.mnuHelpReportIssues.Glyph = global::YANFOE.Properties.Resources.cloud_comment;
            this.mnuHelpReportIssues.Id = 30;
            this.mnuHelpReportIssues.Name = "mnuHelpReportIssues";
            this.mnuHelpReportIssues.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuHelpReportIssues_ItemClick);
            // 
            // mnuHelpSourceCode
            // 
            this.mnuHelpSourceCode.Caption = "Source Code";
            this.mnuHelpSourceCode.Glyph = global::YANFOE.Properties.Resources.keyboard;
            this.mnuHelpSourceCode.Id = 32;
            this.mnuHelpSourceCode.Name = "mnuHelpSourceCode";
            this.mnuHelpSourceCode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuHelpSourceCode_ItemClick);
            // 
            // mnuHelpWiki
            // 
            this.mnuHelpWiki.Caption = "Wiki";
            this.mnuHelpWiki.Glyph = global::YANFOE.Properties.Resources.school_board1;
            this.mnuHelpWiki.Id = 33;
            this.mnuHelpWiki.Name = "mnuHelpWiki";
            this.mnuHelpWiki.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuHelpWiki_ItemClick);
            // 
            // mnuHelpHomepage
            // 
            this.mnuHelpHomepage.Caption = "Homepage";
            this.mnuHelpHomepage.Glyph = global::YANFOE.Properties.Resources.home;
            this.mnuHelpHomepage.Id = 31;
            this.mnuHelpHomepage.Name = "mnuHelpHomepage";
            this.mnuHelpHomepage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuHelpHomepage_ItemClick);
            // 
            // mnuDonate
            // 
            this.mnuDonate.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.mnuDonate.Caption = "Donate";
            this.mnuDonate.Glyph = global::YANFOE.Properties.Resources.btn_donate_SM;
            this.mnuDonate.Id = 34;
            this.mnuDonate.Name = "mnuDonate";
            toolTipTitleItem1.Text = "Donate to the YANFOE project.";
            toolTipItem1.Appearance.Image = global::YANFOE.Properties.Resources.dollar_currency_sign;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = global::YANFOE.Properties.Resources.dollar_currency_sign;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Show your support for YANFOE and\r\nhelp fund future development.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.mnuDonate.SuperTip = superToolTip1;
            this.mnuDonate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnuDonate_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(992, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 547);
            this.barDockControlBottom.Size = new System.Drawing.Size(992, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 518);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(992, 29);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 518);
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
            this.tabControlMain.Location = new System.Drawing.Point(0, 29);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.Padding = new System.Windows.Forms.Padding(5);
            this.tabControlMain.SelectedTabPage = this.tabMediaManager;
            this.tabControlMain.Size = new System.Drawing.Size(992, 491);
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
            this.tabMediaManager.Controls.Add(this.mediaPathManager1);
            this.tabMediaManager.Image = global::YANFOE.Properties.Resources.database24;
            this.tabMediaManager.Name = "tabMediaManager";
            this.tabMediaManager.Size = new System.Drawing.Size(988, 456);
            superToolTip2.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem2.Appearance.Image = global::YANFOE.Properties.Resources.database24;
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = global::YANFOE.Properties.Resources.database24;
            toolTipTitleItem2.Text = "Media Manager";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Manage and import TV and Movie media.\r\n\r\n(#) - Total Media Paths";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.tabMediaManager.SuperTip = superToolTip2;
            this.tabMediaManager.Text = "Media Manager";
            // 
            // mediaPathManager1
            // 
            this.mediaPathManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mediaPathManager1.Location = new System.Drawing.Point(0, 0);
            this.mediaPathManager1.Name = "mediaPathManager1";
            this.mediaPathManager1.Size = new System.Drawing.Size(988, 456);
            this.mediaPathManager1.TabIndex = 0;
            // 
            // tabMovies
            // 
            this.tabMovies.Controls.Add(this.moviesUserControl1);
            this.tabMovies.Image = global::YANFOE.Properties.Resources.video24;
            this.tabMovies.Name = "tabMovies";
            this.tabMovies.Size = new System.Drawing.Size(988, 456);
            toolTipTitleItem3.Text = "Movies";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "(#) - Total Movies";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.tabMovies.SuperTip = superToolTip3;
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
            this.moviesUserControl1.Size = new System.Drawing.Size(986, 454);
            this.moviesUserControl1.TabIndex = 0;
            // 
            // tabTv
            // 
            this.tabTv.Controls.Add(this.tvUserControl1);
            this.tabTv.Image = global::YANFOE.Properties.Resources.television24;
            this.tabTv.Name = "tabTv";
            this.tabTv.Size = new System.Drawing.Size(988, 456);
            toolTipTitleItem4.Text = "TV";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "(#) - Total Series";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.tabTv.SuperTip = superToolTip4;
            this.tabTv.Text = "TV";
            // 
            // tvUserControl1
            // 
            this.tvUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUserControl1.Location = new System.Drawing.Point(0, 0);
            this.tvUserControl1.Name = "tvUserControl1";
            this.tvUserControl1.Size = new System.Drawing.Size(986, 454);
            this.tvUserControl1.TabIndex = 0;
            // 
            // tabDownloads
            // 
            this.tabDownloads.Controls.Add(this.downloadsUserControl1);
            this.tabDownloads.Image = global::YANFOE.Properties.Resources.download24;
            this.tabDownloads.Name = "tabDownloads";
            this.tabDownloads.Size = new System.Drawing.Size(988, 456);
            toolTipTitleItem5.Text = "Downloads Manager\r\n";
            toolTipItem5.LeftIndent = 6;
            toolTipItem5.Text = "Downloads (Urgent Queue : Background Queue)";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem5);
            this.tabDownloads.SuperTip = superToolTip5;
            this.tabDownloads.Text = "Downloads";
            // 
            // downloadsUserControl1
            // 
            this.downloadsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.downloadsUserControl1.Name = "downloadsUserControl1";
            this.downloadsUserControl1.Size = new System.Drawing.Size(986, 454);
            this.downloadsUserControl1.TabIndex = 0;
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.logsUserControl1);
            this.tabLogs.Image = global::YANFOE.Properties.Resources.books24;
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Size = new System.Drawing.Size(988, 456);
            this.tabLogs.Text = "Logs";
            // 
            // logsUserControl1
            // 
            this.logsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.logsUserControl1.Name = "logsUserControl1";
            this.logsUserControl1.Size = new System.Drawing.Size(986, 454);
            this.logsUserControl1.TabIndex = 0;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.layoutControl1);
            this.tabAbout.Image = global::YANFOE.Properties.Resources.info1;
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Size = new System.Drawing.Size(988, 456);
            this.tabAbout.Text = "About";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.hyperLinkEdit5);
            this.layoutControl1.Controls.Add(this.memoEdit2);
            this.layoutControl1.Controls.Add(this.textEdit5);
            this.layoutControl1.Controls.Add(this.hyperLinkEdit4);
            this.layoutControl1.Controls.Add(this.hyperLinkEdit3);
            this.layoutControl1.Controls.Add(this.hyperLinkEdit2);
            this.layoutControl1.Controls.Add(this.hyperLinkEdit1);
            this.layoutControl1.Controls.Add(this.memoEdit1);
            this.layoutControl1.Controls.Add(this.txtBuild);
            this.layoutControl1.Controls.Add(this.txtVersion);
            this.layoutControl1.Controls.Add(this.textEdit2);
            this.layoutControl1.Controls.Add(this.textEdit1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1338, 361, 492, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(986, 454);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // hyperLinkEdit5
            // 
            this.hyperLinkEdit5.EditValue = "http://dryicons.com";
            this.hyperLinkEdit5.Location = new System.Drawing.Point(117, 208);
            this.hyperLinkEdit5.MenuManager = this.barManager1;
            this.hyperLinkEdit5.Name = "hyperLinkEdit5";
            this.hyperLinkEdit5.Size = new System.Drawing.Size(378, 20);
            this.hyperLinkEdit5.StyleController = this.layoutControl1;
            this.hyperLinkEdit5.TabIndex = 15;
            // 
            // memoEdit2
            // 
            this.memoEdit2.EditValue = resources.GetString("memoEdit2.EditValue");
            this.memoEdit2.Location = new System.Drawing.Point(523, 332);
            this.memoEdit2.MenuManager = this.barManager1;
            this.memoEdit2.Name = "memoEdit2";
            this.memoEdit2.Properties.ReadOnly = true;
            this.memoEdit2.Size = new System.Drawing.Size(439, 98);
            this.memoEdit2.StyleController = this.layoutControl1;
            this.memoEdit2.TabIndex = 14;
            // 
            // textEdit5
            // 
            this.textEdit5.EditValue = "Nicolas Katsidis";
            this.textEdit5.Location = new System.Drawing.Point(117, 184);
            this.textEdit5.MenuManager = this.barManager1;
            this.textEdit5.Name = "textEdit5";
            this.textEdit5.Properties.ReadOnly = true;
            this.textEdit5.Size = new System.Drawing.Size(378, 20);
            this.textEdit5.StyleController = this.layoutControl1;
            this.textEdit5.TabIndex = 13;
            // 
            // hyperLinkEdit4
            // 
            this.hyperLinkEdit4.EditValue = "http://www.yanfoe.com/forum/";
            this.hyperLinkEdit4.Location = new System.Drawing.Point(117, 324);
            this.hyperLinkEdit4.MenuManager = this.barManager1;
            this.hyperLinkEdit4.Name = "hyperLinkEdit4";
            this.hyperLinkEdit4.Size = new System.Drawing.Size(378, 20);
            this.hyperLinkEdit4.StyleController = this.layoutControl1;
            this.hyperLinkEdit4.TabIndex = 12;
            // 
            // hyperLinkEdit3
            // 
            this.hyperLinkEdit3.EditValue = "https://github.com/yanfoe/YANFOE.v2";
            this.hyperLinkEdit3.Location = new System.Drawing.Point(117, 348);
            this.hyperLinkEdit3.MenuManager = this.barManager1;
            this.hyperLinkEdit3.Name = "hyperLinkEdit3";
            this.hyperLinkEdit3.Size = new System.Drawing.Size(378, 20);
            this.hyperLinkEdit3.StyleController = this.layoutControl1;
            this.hyperLinkEdit3.TabIndex = 11;
            // 
            // hyperLinkEdit2
            // 
            this.hyperLinkEdit2.EditValue = "https://github.com/yanfoe/YANFOE.v2/issues";
            this.hyperLinkEdit2.Location = new System.Drawing.Point(117, 300);
            this.hyperLinkEdit2.MenuManager = this.barManager1;
            this.hyperLinkEdit2.Name = "hyperLinkEdit2";
            this.hyperLinkEdit2.Size = new System.Drawing.Size(378, 20);
            this.hyperLinkEdit2.StyleController = this.layoutControl1;
            this.hyperLinkEdit2.TabIndex = 10;
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.EditValue = "http://www.yanfoe.com";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(117, 276);
            this.hyperLinkEdit1.MenuManager = this.barManager1;
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Size = new System.Drawing.Size(378, 20);
            this.hyperLinkEdit1.StyleController = this.layoutControl1;
            this.hyperLinkEdit1.TabIndex = 9;
            // 
            // memoEdit1
            // 
            this.memoEdit1.EditValue = resources.GetString("memoEdit1.EditValue");
            this.memoEdit1.Location = new System.Drawing.Point(523, 136);
            this.memoEdit1.MenuManager = this.barManager1;
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Properties.ReadOnly = true;
            this.memoEdit1.Size = new System.Drawing.Size(439, 148);
            this.memoEdit1.StyleController = this.layoutControl1;
            this.memoEdit1.TabIndex = 8;
            // 
            // txtBuild
            // 
            this.txtBuild.Location = new System.Drawing.Point(117, 68);
            this.txtBuild.MenuManager = this.barManager1;
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.Properties.ReadOnly = true;
            this.txtBuild.Size = new System.Drawing.Size(845, 20);
            this.txtBuild.StyleController = this.layoutControl1;
            this.txtBuild.TabIndex = 7;
            // 
            // txtVersion
            // 
            this.txtVersion.EditValue = "Alpha 2";
            this.txtVersion.Location = new System.Drawing.Point(117, 44);
            this.txtVersion.MenuManager = this.barManager1;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Properties.ReadOnly = true;
            this.txtVersion.Size = new System.Drawing.Size(845, 20);
            this.txtVersion.StyleController = this.layoutControl1;
            this.txtVersion.TabIndex = 6;
            // 
            // textEdit2
            // 
            this.textEdit2.EditValue = "Kamil Kluziak, Steven Tarcza";
            this.textEdit2.Location = new System.Drawing.Point(117, 160);
            this.textEdit2.MenuManager = this.barManager1;
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.ReadOnly = true;
            this.textEdit2.Size = new System.Drawing.Size(378, 20);
            this.textEdit2.StyleController = this.layoutControl1;
            this.textEdit2.TabIndex = 5;
            // 
            // textEdit1
            // 
            this.textEdit1.EditValue = "Russell Lewis";
            this.textEdit1.Location = new System.Drawing.Point(117, 136);
            this.textEdit1.MenuManager = this.barManager1;
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.ReadOnly = true;
            this.textEdit1.Size = new System.Drawing.Size(378, 20);
            this.textEdit1.StyleController = this.layoutControl1;
            this.textEdit1.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlGroup3,
            this.layoutControlGroup4,
            this.layoutControlGroup5,
            this.layoutControlGroup6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(986, 454);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Credits";
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem10,
            this.layoutControlItem12});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 92);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(499, 140);
            this.layoutControlGroup2.Text = "Credits";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit1;
            this.layoutControlItem1.CustomizationFormText = "Programming Lead";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem1.Text = "Development Lead";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem2.Control = this.textEdit2;
            this.layoutControlItem2.CustomizationFormText = "Developer";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem2.Text = "Contributers";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem10.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem10.Control = this.textEdit5;
            this.layoutControlItem10.CustomizationFormText = "QA Team";
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem10.Text = "QA Team";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem12.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem12.Control = this.hyperLinkEdit5;
            this.layoutControlItem12.CustomizationFormText = "Icons";
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem12.Text = "Icons";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.CustomizationFormText = "Version Info";
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(966, 92);
            this.layoutControlGroup3.Text = "Version Info";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem3.Control = this.txtVersion;
            this.layoutControlItem3.CustomizationFormText = "Current Version";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(942, 24);
            this.layoutControlItem3.Text = "Current Version";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem4.Control = this.txtBuild;
            this.layoutControlItem4.CustomizationFormText = "Build";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(942, 24);
            this.layoutControlItem4.Text = "Build";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.CustomizationFormText = "License";
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(499, 92);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(467, 196);
            this.layoutControlGroup4.Text = "License";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.memoEdit1;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(443, 152);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.CustomizationFormText = "Links";
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup5.Location = new System.Drawing.Point(0, 232);
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Size = new System.Drawing.Size(499, 202);
            this.layoutControlGroup5.Text = "Links";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem6.Control = this.hyperLinkEdit1;
            this.layoutControlItem6.CustomizationFormText = "Homepage";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem6.Text = "Homepage";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem7.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem7.Control = this.hyperLinkEdit2;
            this.layoutControlItem7.CustomizationFormText = "Issues";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem7.Text = "Issues";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem8.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem8.Control = this.hyperLinkEdit3;
            this.layoutControlItem8.CustomizationFormText = "Source Code";
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(475, 86);
            this.layoutControlItem8.Text = "Source Code";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem9.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem9.Control = this.hyperLinkEdit4;
            this.layoutControlItem9.CustomizationFormText = "Forum";
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(475, 24);
            this.layoutControlItem9.Text = "Forum";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(89, 13);
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.CustomizationFormText = "Thanks";
            this.layoutControlGroup6.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem11});
            this.layoutControlGroup6.Location = new System.Drawing.Point(499, 288);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(467, 146);
            this.layoutControlGroup6.Text = "Thanks";
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.memoEdit2;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(443, 102);
            this.layoutControlItem11.Text = "layoutControlItem11";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
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
            this.panelControl1.Controls.Add(this.picUpdateStatus);
            this.panelControl1.Controls.Add(this.picThread8);
            this.panelControl1.Controls.Add(this.picThread7);
            this.panelControl1.Controls.Add(this.picThread6);
            this.panelControl1.Controls.Add(this.picThread5);
            this.panelControl1.Controls.Add(this.lblDownloadStatus);
            this.panelControl1.Controls.Add(this.picThread4);
            this.panelControl1.Controls.Add(this.picThread3);
            this.panelControl1.Controls.Add(this.picThread2);
            this.panelControl1.Controls.Add(this.picThread1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 520);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panelControl1.Size = new System.Drawing.Size(992, 27);
            this.panelControl1.TabIndex = 9;
            // 
            // picUpdateStatus
            // 
            this.picUpdateStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.picUpdateStatus.Location = new System.Drawing.Point(969, 3);
            this.picUpdateStatus.MenuManager = this.barManager1;
            this.picUpdateStatus.Name = "picUpdateStatus";
            this.picUpdateStatus.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picUpdateStatus.Properties.Appearance.Options.UseBackColor = true;
            this.picUpdateStatus.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picUpdateStatus.Properties.ShowMenu = false;
            this.picUpdateStatus.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picUpdateStatus.Size = new System.Drawing.Size(23, 24);
            this.picUpdateStatus.TabIndex = 12;
            this.picUpdateStatus.ToolTipController = this.toolTipController1;
            this.picUpdateStatus.DoubleClick += new System.EventHandler(this.picUpdateStatus_DoubleClick);
            // 
            // toolTipController1
            // 
            this.toolTipController1.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController1_GetActiveObjectInfo);
            // 
            // picThread8
            // 
            this.picThread8.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread8.Location = new System.Drawing.Point(168, 3);
            this.picThread8.MenuManager = this.barManager1;
            this.picThread8.Name = "picThread8";
            this.picThread8.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread8.Properties.Appearance.Options.UseBackColor = true;
            this.picThread8.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread8.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread8.Size = new System.Drawing.Size(24, 24);
            this.picThread8.TabIndex = 11;
            // 
            // picThread7
            // 
            this.picThread7.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread7.Location = new System.Drawing.Point(144, 3);
            this.picThread7.MenuManager = this.barManager1;
            this.picThread7.Name = "picThread7";
            this.picThread7.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread7.Properties.Appearance.Options.UseBackColor = true;
            this.picThread7.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread7.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread7.Size = new System.Drawing.Size(24, 24);
            this.picThread7.TabIndex = 10;
            // 
            // picThread6
            // 
            this.picThread6.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread6.Location = new System.Drawing.Point(120, 3);
            this.picThread6.MenuManager = this.barManager1;
            this.picThread6.Name = "picThread6";
            this.picThread6.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread6.Properties.Appearance.Options.UseBackColor = true;
            this.picThread6.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread6.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread6.Size = new System.Drawing.Size(24, 24);
            this.picThread6.TabIndex = 9;
            // 
            // picThread5
            // 
            this.picThread5.Dock = System.Windows.Forms.DockStyle.Left;
            this.picThread5.Location = new System.Drawing.Point(96, 3);
            this.picThread5.MenuManager = this.barManager1;
            this.picThread5.Name = "picThread5";
            this.picThread5.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picThread5.Properties.Appearance.Options.UseBackColor = true;
            this.picThread5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picThread5.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picThread5.Size = new System.Drawing.Size(24, 24);
            this.picThread5.TabIndex = 8;
            // 
            // lblDownloadStatus
            // 
            this.lblDownloadStatus.Location = new System.Drawing.Point(196, 6);
            this.lblDownloadStatus.Name = "lblDownloadStatus";
            this.lblDownloadStatus.Size = new System.Drawing.Size(63, 13);
            this.lblDownloadStatus.TabIndex = 7;
            this.lblDownloadStatus.Text = "labelControl1";
            this.lblDownloadStatus.Visible = false;
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
            // uiTimer
            // 
            this.uiTimer.Enabled = true;
            this.uiTimer.Interval = 50;
            this.uiTimer.Tick += new System.EventHandler(this.uiTimer_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(992, 547);
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
            this.Text = "YANFOE 2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
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
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBuild.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpdateStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread8.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread6.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picThread1.Properties)).EndInit();
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
        private DevExpress.XtraTab.XtraTabPage tabAbout;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarButtonItem mnuToolsMovieScraperGroupManager;
        private DevExpress.XtraBars.BarButtonItem mnuFileSaveDatabase;
        private System.Windows.Forms.Timer uiTimer;
        private DevExpress.XtraEditors.PictureEdit picThread4;
        private DevExpress.XtraEditors.PictureEdit picThread3;
        private DevExpress.XtraEditors.PictureEdit picThread2;
        private DevExpress.XtraEditors.PictureEdit picThread1;
        private DevExpress.XtraBars.BarButtonItem mnuEditSettings;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.TextEdit txtBuild;
        private DevExpress.XtraEditors.TextEdit txtVersion;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit3;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit2;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private MediaPathManager mediaPathManager1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.TextEdit textEdit5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.MemoEdit memoEdit2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.PictureEdit picThread8;
        private DevExpress.XtraEditors.PictureEdit picThread7;
        private DevExpress.XtraEditors.PictureEdit picThread6;
        private DevExpress.XtraEditors.PictureEdit picThread5;
        private DevExpress.XtraEditors.PictureEdit picUpdateStatus;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraBars.BarButtonItem mnuHelpReportIssues;
        private DevExpress.XtraBars.BarButtonItem mnuHelpHomepage;
        private DevExpress.XtraBars.BarButtonItem mnuHelpSourceCode;
        private DevExpress.XtraBars.BarButtonItem mnuHelpWiki;
        private DevExpress.XtraBars.BarButtonItem mnuDonate;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.LabelControl lblDownloadStatus;
    }
}