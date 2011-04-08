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

    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.Factories.Internal;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Properties;
    using YANFOE.Settings.ConstSettings;
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
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MediaPathDb);

            this.InitializeComponent();

            this.Text = string.Format(
                "YANFOE 2 - Alpha 2", 
                Application.ApplicationBuild);

            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SkinName = Skin.SetCurrentSkin();

            Settings.Get.InOutCollection.SetCurrentSettings(NFOType.YAMJ);

            MovieDBFactory.MovieDatabase.ListChanged += this.FrmMain_ListChanged;

            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.All);
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
        /// Handles the Load event of the FrmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            string r = Downloader.ProcessDownload(
                "http://www.yanfoe.com/YNoCache-revision.txt", DownloadType.Html, Section.Movies);

            if (r != Application.ApplicationBuild)
            {
                XtraMessageBox.Show("This Alpha Has Expired. Please Update.");
                System.Windows.Forms.Application.Exit();
            }
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
            if (Downloader.Progress1.Message.Contains("Idle."))
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

                this.picThread1.ToolTip = Downloader.Progress1.Message;
            }

            if (Downloader.Progress2.Message.Contains("Idle."))
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

                this.picThread2.ToolTip = Downloader.Progress2.Message;
            }

            if (Downloader.Progress3.Message.Contains("Idle."))
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
                this.picThread3.ToolTip = Downloader.Progress3.Message;
            }

            if (Downloader.Progress4.Message.Contains("Idle."))
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
                this.picThread4.ToolTip = Downloader.Progress4.Message;
            }

            this.lblDownloadStatus.Text = string.Format(
                "Queue: {0} / Background Que {1}", Downloader.CurrentQue, Downloader.BackgroundDownloadQue.Count);
        }

        #endregion
    }
}