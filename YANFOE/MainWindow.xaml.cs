// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MainWindow.xaml.cs">
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
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows;

    using BitFactory.Logging;

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Versioning;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Settings;
    using YANFOE.Tools.Exporting;
    using YANFOE.UI.Dialogs.DSettings;
    using YANFOE.UI.Dialogs.General;
    using YANFOE.UI.Dialogs.Movies;

    using Application = YANFOE.Settings.ConstSettings.Application;

    #endregion

    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            VersionUpdateFactory.CheckForUpdate();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The window_ closed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (DatabaseIOFactory.DatabaseDirty)
            {
                DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.All);

                do
                {
                    Thread.Sleep(50);
                }
                while (DatabaseIOFactory.SavingCount > 0);
            }

            Get.SaveAll();

            Log.WriteToLog(LogSeverity.Info, 0, "YANFOE Closed.", string.Empty);
            Environment.Exit(0);
        }

        /// <summary>
        /// The window_ closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.mnuFileSaveDatabase.IsEnabled = false;
            this.mnuFileExit.IsEnabled = false;

            foreach (var file in Directory.GetFiles(Get.FileSystemPaths.PathDirTemp))
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// The window_ loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseIOFactory.DatabaseDirty = false;

            if (Get.Ui.ShowWelcomeMessage)
            {
                var frmWelcomePage = new WndWelcomePage();
                frmWelcomePage.ShowDialog();
            }

            Log.WriteToLog(LogSeverity.Info, 0, "YANFOE Started.", string.Empty);
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
        private void mnuDonate_ItemClick(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.DonateUrl);
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
        private void mnuEditSettings_ItemClick(object sender, RoutedEventArgs e)
        {
            var settings = new WndSettingsMain();
            settings.ShowDialog();
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
        private void mnuFileExit_ItemClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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
        private void mnuFileSaveDatabase_ItemClick(object sender, RoutedEventArgs e)
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
        private void mnuHelpHomepage_ItemClick(object sender, RoutedEventArgs e)
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
        private void mnuHelpReportIssues_ItemClick(object sender, RoutedEventArgs e)
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
        private void mnuHelpSourceCode_ItemClick(object sender, RoutedEventArgs e)
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
        private void mnuHelpWiki_ItemClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/yanfoe/YANFOE.v2/wiki");
        }

        /// <summary>
        /// The mnu tools export missing episodes list_ item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void mnuToolsExportMissingEpisodesList_ItemClick(object sender, RoutedEventArgs e)
        {
            Exporting.ExportMissingTvShowEpisodes();
        }

        /// <summary>
        /// The mnu tools export movie list_ item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void mnuToolsExportMovieList_ItemClick(object sender, RoutedEventArgs e)
        {
            Exporting.ExportMovieList();
        }

        /// <summary>
        /// The mnu tools export tv shows list_ item click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void mnuToolsExportTvShowsList_ItemClick(object sender, RoutedEventArgs e)
        {
            Exporting.ExportTvShowList();
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
        private void mnuToolsMovieScraperGroupManager_ItemClick(object sender, RoutedEventArgs e)
        {
            var frm = new WndScraperGroupManager();
            frm.ShowDialog();
        }

        #endregion
    }
}