// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmMain.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE
{
    using System;
    using System.Diagnostics;
    using System.IO;
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
    /// The main application form
    /// </summary>
    public partial class FrmMain : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        ///   The current status text
        /// </summary>
        public ChangeText ChangeTextValue;

        /// <summary>
        ///   The download count 1.
        /// </summary>
        private int downloadCount1;

        /// <summary>
        ///   The download count 2.
        /// </summary>
        private int downloadCount2;

        /// <summary>
        ///   The log count.
        /// </summary>
        private int logCount;

        /// <summary>
        ///   The media path count.
        /// </summary>
        private int mediaPathCount;

        /// <summary>
        ///   The tv count.
        /// </summary>
        private int tvCount;

        /// <summary>
        ///   The tmr.
        /// </summary>
        private readonly Timer tmr = new Timer();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "FrmMain" /> class.
        /// </summary>
        public FrmMain()
        {
            this.InitializeComponent();

            this.SetTitle(false);

            this.ChangeTextValue = new ChangeText(this.SetTitle);

            this.txtBuild.Text = Settings.ConstSettings.Application.ApplicationBuild;
            this.txtVersion.Text = Settings.ConstSettings.Application.ApplicationVersion;

            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = Skin.SetCurrentSkin();

            this.SetupEventBindings();

            VersionUpdateFactory.CheckForUpdate();

            this.tmr.Interval = 500;
            this.tmr.Tick += this.tmr_Tick;
            this.tmr.Start();
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

        /// <summary>
        /// The update tab headers delegate.
        /// </summary>
        private delegate void UpdateTabHeadersDelegate();

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
        /// The setup event bindings.
        /// </summary>
        private void SetupEventBindings()
        {
            MovieDBFactory.MovieDatabase.ListChanged += (sender, e) =>
                {
                    try
                    {
                        this.tabMovies.Text = string.Format("Movies ({0})", MovieDBFactory.MovieDatabase.Count);
                    }
                    catch (Exception)
                    {
                        // Do nothing
                    }
                };

            TvDBFactory.TvDbChanged += (sender, e) => { this.tvCount = TvDBFactory.MasterSeriesNameList.Count; };

            VersionUpdateFactory.VersionUpdateChanged +=
                (sender, e) => { this.picUpdateStatus.Image = VersionUpdateFactory.ImageStatus; };

            DatabaseIOFactory.DatabaseDirtyChanged += (sender, e) =>
                {
                    try
                    {
                        this.Invoke(this.ChangeTextValue, DatabaseIOFactory.DatabaseDirty);
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                };

            MediaPathDBFactory.MediaPathDB.ListChanged +=
                (sender, e) => { this.mediaPathCount = MediaPathDBFactory.MediaPathDB.Count; };

            Downloader.Downloading.ListChanged += (sender, e) => this.UpdateDownloaderTabHeader();

            Downloader.BackgroundDownloadQue.ListChanged += (sender, e) => this.UpdateDownloaderTabHeader();

            InternalApps.Logs.Log.GetInternalLogger(LoggerName.GeneralLog).ListChanged +=
                (sender, e) => { this.logCount = InternalApps.Logs.Log.GetInternalLogger(LoggerName.GeneralLog).Count; };
        }

        /// <summary>
        /// Updates the downloader tab header.
        /// </summary>
        private void UpdateDownloaderTabHeader()
        {
            this.downloadCount1 = Downloader.Downloading.Count;
            this.downloadCount2 = Downloader.BackgroundDownloadQue.Count;
        }

        /// <summary>
        /// Updates the tab headers.
        /// </summary>
        private void UpdateTabHeaders()
        {
            try
            {
                this.tabMediaManager.Text = string.Format("Media Manager ({0})", this.mediaPathCount);
                this.tabTv.Text = string.Format("TV ({0})", this.tvCount);
                this.tabLogs.Text = string.Format("Log ({0})", this.logCount);
                this.tabDownloads.Text = string.Format("Downloads ({0}/{1})", this.downloadCount1, this.downloadCount2);
            }
            catch (Exception)
            {
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
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
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
            Environment.Exit(0);
        }

        /// <summary>
        /// Handles the FormClosing event of the frmMain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.
        /// </param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DatabaseIOFactory.SavingCount > 0)
            {
                e.Cancel = true;
            }

            this.mnuFileSaveDatabase.Enabled = false;
            this.mnuFileExit.Enabled = false;

            foreach (var file in Directory.GetFiles(Settings.Get.FileSystemPaths.PathDirTemp))
            {
                File.Delete(file);
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
        private void frmMain_Shown(object sender, EventArgs e)
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
        /// Handles the ItemClick event of the mnuEditSettings control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuEditSettings_ItemClick(object sender, ItemClickEventArgs e)
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
        private void mnuFileExit_ItemClick(object sender, ItemClickEventArgs e)
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
        private void mnuFileSaveDatabase_ItemClick(object sender, ItemClickEventArgs e)
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);
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
        /// Handles the ItemClick event of the mnuToolsMovieScraperGroupManager control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.
        /// </param>
        private void mnuToolsMovieScraperGroupManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new FrmScraperGroupManager();
            frm.ShowDialog();
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
        /// Handles the Tick event of the tmr control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
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
                e.Info = new ToolTipControlInfo
                    {
                        Object = this.picUpdateStatus,
                        ToolTipType = ToolTipType.SuperTip,
                        SuperTip = VersionUpdateFactory.UpdateTip
                    };
            }
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
        private void uiTimer_Tick(object sender, EventArgs e)
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

        #endregion
    }
}