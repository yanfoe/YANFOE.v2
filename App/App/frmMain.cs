// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmMain.cs" company="The YANFOE Project">
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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Forms;

    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Versioning;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Properties;
    using YANFOE.Tools.Enums;
    using YANFOE.UI.Dialogs.DSettings;
    using YANFOE.UI.Dialogs.Movies;

    using Skin = YANFOE.Tools.UI.Skin;

    /// <summary>
    /// The frm main.
    /// </summary>
    public partial class FrmMain : XtraForm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMain"/> class.
        /// </summary>
        public FrmMain()
        {
            this.InitializeComponent();

            this.Text = string.Format(
                "{0} {1}", 
                Settings.ConstSettings.Application.ApplicationName,
                Settings.ConstSettings.Application.ApplicationVersion);

            txtBuild.Text = Settings.ConstSettings.Application.ApplicationBuild;
            txtVersion.Text = Settings.ConstSettings.Application.ApplicationVersion;

            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = Skin.SetCurrentSkin();

            Settings.Get.InOutCollection.SetCurrentSettings(NFOType.YAMJ);

            MovieDBFactory.MovieDatabase.ListChanged += this.FrmMain_ListChanged;
            VersionUpdateFactory.VersionUpdateChanged += this.VersionUpdateFactory_VersionUpdateChanged;
            VersionUpdateFactory.CheckForUpdate();

        }

        private void VersionUpdateFactory_VersionUpdateChanged(object sender, EventArgs e)
        {
            picUpdateStatus.Image = VersionUpdateFactory.ImageStatus;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Text.
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
        /// Handles the ListChanged event of the FrmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ListChangedEventArgs"/> instance containing the event data.</param>
        private void FrmMain_ListChanged(object sender, ListChangedEventArgs e)
        {
            this.tabMovies.Text = string.Format("Movies ({0})", MovieDBFactory.MovieDatabase.Count);
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuEditSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void MnuEditSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            var settings = new FrmSettingsMain();
            settings.ShowDialog();
            settings.Dispose();
        }

        /// <summary>
        /// Handles the ItemClick event of the MnuFileExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void MnuFileExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Handles the ItemClick event of the MnuFileSaveDatabase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void MnuFileSaveDatabase_ItemClick(object sender, ItemClickEventArgs e)
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuToolsMovieScraperGroupManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void MnuToolsMovieScraperGroupManager_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new FrmScraperGroupManager();
            frm.ShowDialog();
        }

        /// <summary>
        /// Handles the Tick event of the uiTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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

        #endregion

        /// <summary>
        /// Handles the FormClosed event of the FrmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosedEventArgs"/> instance containing the event data.</param>
        private void FrmMain_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);
            Application.Exit();
        }

        /// <summary>
        /// Handles the GetActiveObjectInfo event of the toolTipController1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs"/> instance containing the event data.</param>
        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl == picUpdateStatus)
            {
                e.Info = new ToolTipControlInfo();
                e.Info.Object = picUpdateStatus;
                e.Info.ToolTipType = ToolTipType.SuperTip;
                e.Info.SuperTip = VersionUpdateFactory.UpdateTip;

            }
        }

        /// <summary>
        /// Handles the DoubleClick event of the picUpdateStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void picUpdateStatus_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(VersionUpdateFactory.UpdateLink))
            {
                Process.Start(VersionUpdateFactory.UpdateLink);
            }
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpReportIssues control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void mnuHelpReportIssues_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/issues/");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpWiki control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void mnuHelpWiki_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/wiki");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpSourceCode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void mnuHelpSourceCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuHelpHomepage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void mnuHelpHomepage_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.yanfoe.com");
        }

        /// <summary>
        /// Handles the ItemClick event of the mnuDonate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private void mnuDonate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=3HFMBVFJE8XGA");
        }
    }
}