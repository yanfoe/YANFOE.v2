// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmMain.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The frm main.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
    using YANFOE.Factories.Versioning;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Properties;
    using YANFOE.Tools.Enums;
    using YANFOE.UI.Dialogs.DSettings;
    using YANFOE.UI.Dialogs.General;
    using YANFOE.UI.Dialogs.Movies;

    using Skin = YANFOE.Tools.UI.Skin;
    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// The frm main.
    /// </summary>
    public partial class FrmMain : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        /// The change text.
        /// </summary>
        public ChangeText changeText;

        private Timer tmr = new Timer();

        private int MediaPathCount = 0;

        private int MovieCount = 0;

        private int TvCount = 0;

        private int DownloadCount1 = 0;

        private int DownloadCount2 = 0;

        private int LogCount = 0;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FrmMain" /> class.
        /// </summary>
        public FrmMain()
        {
            this.InitializeComponent();

            this.SetTitle(false);

            this.changeText = new ChangeText(this.SetTitle);

            this.txtBuild.Text = Settings.ConstSettings.Application.ApplicationBuild;
            this.txtVersion.Text = Settings.ConstSettings.Application.ApplicationVersion;

            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = Skin.SetCurrentSkin();

            Settings.Get.InOutCollection.SetCurrentSettings(NFOType.YAMJ);

            MovieDBFactory.MovieDatabase.ListChanged += this.FrmMain_ListChanged;
            TvDBFactory.TvDbChanged += this.TvDBFactory_TvDbChanged;
            VersionUpdateFactory.VersionUpdateChanged += this.VersionUpdateFactory_VersionUpdateChanged;
            DatabaseIOFactory.DatabaseDirtyChanged += this.DatabaseIOFactory_DatabaseDirtyChanged;
            MediaPathDBFactory.MediaPathDB.ListChanged += this.MediaPathDB_ListChanged;
            Downloader.Downloading.ListChanged += this.Downloading_ListChanged;
            Downloader.BackgroundDownloadQue.ListChanged += this.BackgroundDownloadQue_ListChanged;
            InternalApps.Logs.Log.GetInternalLogger(LoggerName.GeneralLog).ListChanged += this.Log_ListChanged;

            VersionUpdateFactory.CheckForUpdate();

            this.tmr.Interval = 500;
            this.tmr.Tick += new EventHandler(this.tmr_Tick);
            this.tmr.Start();
        }

        private delegate void UpdateTabHeadersDelegate();

        /// <summary>
        /// Handles the Tick event of the tmr control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void tmr_Tick(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UpdateTabHeadersDelegate(this.UpdateTabHeaders));
            }
            else
            {
                this.UpdateTabHeaders();
            }
        }

        /// <summary>
        /// Updates the tab headers.
        /// </summary>
        private void UpdateTabHeaders()
        {
            try
            {
                this.tabMediaManager.Text = string.Format("Media Manager ({0})", this.MediaPathCount);
                this.tabTv.Text = string.Format("TV ({0})", this.TvCount);
                this.tabLogs.Text = string.Format("Log ({0})", this.LogCount);
                this.tabDownloads.Text = string.Format("Downloads ({0}/{1})", this.DownloadCount1, this.DownloadCount2);
            }
            catch (Exception) 
            { 

            }
        }

        /// <summary>
        /// Handles the ListChanged event of the Log control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void Log_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.LogCount = InternalApps.Logs.Log.GetInternalLogger(LoggerName.GeneralLog).Count;
        }

        /// <summary>
        /// Handles the ListChanged event of the BackgroundDownloadQue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void BackgroundDownloadQue_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.UpdateDownloaderTabHeader();
        }

        /// <summary>
        /// Handles the ListChanged event of the Downloading control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void Downloading_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.UpdateDownloaderTabHeader();
        }

        /// <summary>
        /// Updates the downloader tab header.
        /// </summary>
        private void UpdateDownloaderTabHeader()
        {
            this.DownloadCount1 = Downloader.Downloading.Count;
            this.DownloadCount2 = Downloader.BackgroundDownloadQue.Count;
        }

        /// <summary>
        /// Handles the ListChanged event of the MediaPathDB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void MediaPathDB_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.MediaPathCount = MediaPathDBFactory.MediaPathDB.Count;
        }

        /// <summary>
        /// Handles the TvDbChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TvDBFactory_TvDbChanged(object sender, EventArgs e)
        {
            this.TvCount = TvDBFactory.MasterSeriesNameList.Count;
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The change text.
        /// </summary>
        /// <param name="isDirty">
        /// The is dirty.
        /// </param>
        public delegate void ChangeText(bool isDirty);

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets Text.
        /// </summary>
        public override sealed string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The database io factory_ database dirty changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DatabaseIOFactory_DatabaseDirtyChanged(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(this.changeText, DatabaseIOFactory.DatabaseDirty);
            }
            catch
            {
                // ignore
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the FrmMain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.
        /// </param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DatabaseIOFactory.DatabaseDirty)
            {
                DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);

                do
                {
                    Thread.Sleep(50);
                    Application.DoEvents();
                }
                while (DatabaseIOFactory.SavingCount > 0);
            }

            Settings.Get.SaveAll();

            InternalApps.Logs.Log.WriteToLog(LogSeverity.Info, 0, "YANFOE Closed.", string.Empty);
            Application.Exit();
        }

        /// <summary>
        /// Handles the ListChanged event of the FrmMain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void FrmMain_ListChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                this.tabMovies.Text = string.Format("Movies ({0})", MovieDBFactory.MovieDatabase.Count);
            }
            catch (Exception)
            {
                // Do nothing
            }
        }

        /// <summary>
        /// The frm main_ shown.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            DatabaseIOFactory.DatabaseDirty = false;

            if (Settings.Get.Ui.ShowWelcomeMessage)
            {
                var frmWelcomePage = new frmWelcomePage();
                frmWelcomePage.ShowDialog();
            }

            InternalApps.Logs.Log.WriteToLog(LogSeverity.Info, 0, "YANFOE Started.", string.Empty);
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuEditSettings control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void MnuEditSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            var settings = new FrmSettingsMain();
            settings.ShowDialog();
            settings.Dispose();
        }

        /// <summary>
        /// Handles the ItemClick event of the MnuFileExit control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void MnuFileExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Handles the ItemClick event of the MnuFileSaveDatabase control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void MnuFileSaveDatabase_ItemClick(object sender, ItemClickEventArgs e)
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuToolsMovieScraperGroupManager control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void MnuToolsMovieScraperGroupManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new FrmScraperGroupManager();
            frm.ShowDialog();
        }

        /// <summary>
        /// The set title.
        /// </summary>
        /// <param name="databaseDirty">
        /// The database dirty.
        /// </param>
        private void SetTitle(bool databaseDirty)
        {
            var dirtyString = string.Empty;

            if (DatabaseIOFactory.DatabaseDirty)
            {
                dirtyString = "(Database Changed)";
            }

            this.Text = string.Format(
                "{0} {1} Build: {2} {3}", 
                Settings.ConstSettings.Application.ApplicationName, 
                Settings.ConstSettings.Application.ApplicationVersion, 
                Settings.ConstSettings.Application.ApplicationBuild, 
                dirtyString);
        }

        /// <summary>
        /// Handles the Tick event of the uiTimer control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void UiTimer_Tick(object sender, EventArgs e)
        {
            if (Downloader.Progress[0].Message.Contains("Idle."))
            {
                this.picThread1.Image = Resources.globe_faded16;
                this.picThread1.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread1.Image != Resources.globe16)
                {
                    this.picThread1.Image = Resources.globe16;
                }

                this.picThread3.Image = Resources.globe16;

                this.picThread1.ToolTip = Downloader.Progress[0].Message;
            }

            if (Downloader.Progress[1].Message.Contains("Idle."))
            {
                this.picThread2.Image = Resources.globe_faded16;
                this.picThread2.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread2.Image != Resources.globe16)
                {
                    this.picThread2.Image = Resources.globe16;
                }

                this.picThread3.Image = Resources.globe16;

                this.picThread2.ToolTip = Downloader.Progress[1].Message;
            }

            if (Downloader.Progress[2].Message.Contains("Idle."))
            {
                this.picThread3.Image = Resources.globe_faded16;
                this.picThread3.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread3.Image != Resources.globe16)
                {
                    this.picThread3.Image = Resources.globe16;
                }

                this.picThread3.Image = Resources.globe16;
                this.picThread3.ToolTip = Downloader.Progress[2].Message;
            }

            if (Downloader.Progress[3].Message.Contains("Idle."))
            {
                this.picThread4.Image = Resources.globe_faded16;
                this.picThread4.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread4.Image != Resources.globe16)
                {
                    this.picThread4.Image = Resources.globe16;
                }

                this.picThread4.Image = Resources.globe16;
                this.picThread4.ToolTip = Downloader.Progress[3].Message;
            }

            if (Downloader.Progress[4].Message.Contains("Idle."))
            {
                this.picThread5.Image = Resources.globe_faded16;
                this.picThread5.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread5.Image != Resources.globe16)
                {
                    this.picThread5.Image = Resources.globe16;
                }

                this.picThread5.Image = Resources.globe16;
                this.picThread5.ToolTip = Downloader.Progress[4].Message;
            }

            if (Downloader.Progress[5].Message.Contains("Idle."))
            {
                this.picThread6.Image = Resources.globe_faded16;
                this.picThread6.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread6.Image != Resources.globe16)
                {
                    this.picThread6.Image = Resources.globe16;
                }

                this.picThread6.Image = Resources.globe16;
                this.picThread6.ToolTip = Downloader.Progress[5].Message;
            }

            if (Downloader.Progress[6].Message.Contains("Idle."))
            {
                this.picThread7.Image = Resources.globe_faded16;
                this.picThread7.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread7.Image != Resources.globe16)
                {
                    this.picThread7.Image = Resources.globe16;
                }

                this.picThread7.Image = Resources.globe16;
                this.picThread7.ToolTip = Downloader.Progress[6].Message;
            }

            if (Downloader.Progress[7].Message.Contains("Idle."))
            {
                this.picThread8.Image = Resources.globe_faded16;
                this.picThread8.ToolTip = string.Empty;
            }
            else
            {
                if (this.picThread8.Image != Resources.globe16)
                {
                    this.picThread8.Image = Resources.globe16;
                }

                this.picThread8.Image = Resources.globe16;
                this.picThread8.ToolTip = Downloader.Progress[7].Message;
            }

            this.lblDownloadStatus.Text = string.Format(
                "Queue: {0} / Background Que {1}", Downloader.CurrentQue, Downloader.BackgroundDownloadQue.Count);
        }

        /// <summary>
        /// The version update factory_ version update changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void VersionUpdateFactory_VersionUpdateChanged(object sender, EventArgs e)
        {
            this.picUpdateStatus.Image = VersionUpdateFactory.ImageStatus;
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuDonate control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuDonate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start(Settings.ConstSettings.Application.DonateUrl);
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpHomepage control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuHelpHomepage_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.yanfoe.com");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpReportIssues control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuHelpReportIssues_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/issues/");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpSourceCode control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuHelpSourceCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpWiki control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuHelpWiki_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/wiki");
        }

        /// <summary>
        /// Handles the DoubleClick event of the picUpdateStatus control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void picUpdateStatus_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(VersionUpdateFactory.UpdateLink))
            {
                Process.Start(VersionUpdateFactory.UpdateLink);
            }
        }

        /// <summary>
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.
        /// </param>
        private void toolTipController1_GetActiveObjectInfo(
            object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl == this.picUpdateStatus)
            {
                e.Info = new ToolTipControlInfo();
                e.Info.Object = this.picUpdateStatus;
                e.Info.ToolTipType = ToolTipType.SuperTip;
                e.Info.SuperTip = VersionUpdateFactory.UpdateTip;
            }
        }

        #endregion
    }
}