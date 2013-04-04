using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YANFOE.Factories;
using YANFOE.Factories.Internal;
using YANFOE.Factories.Media;
using YANFOE.Factories.Sets;
using YANFOE.Models.GeneralModels.AssociatedFiles;
using YANFOE.Tools.UI;

namespace YANFOE.UI.Dialogs.General
{
    /// <summary>
    /// Interaction logic for WndLoadingYANFOE.xaml
    /// </summary>
    public partial class WndLoadingYANFOE : Window
    {
        private BackgroundWorker bgw = new BackgroundWorker();

        private Window frmMain;

        public WndLoadingYANFOE()
        {
            DatabaseIOFactory.AppLoading = true;

            //lblYANFOETitle.Text = Settings.ConstSettings.Application.ApplicationName + " " + Settings.ConstSettings.Application.ApplicationVersion;

            //lblVersion.Text = Settings.ConstSettings.Application.ApplicationBuild;

            MediaPathDBFactory.Instance.MediaPathDB = new ThreadedBindingList<MediaPathModel>();

            // The MediaPathDB must be created prior to creating the main form.
            // However, the main form must be created prior to loading the rest of the databases.
            this.frmMain = new MainWindow();

            this.bgw.DoWork += this.bgw_DoWork;
            this.bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            this.bgw.ProgressChanged += this.bgw_ProgressChanged;
            this.bgw.WorkerReportsProgress = true;

            this.bgw.RunWorkerAsync();

            //if (Directory.Exists(Settings.Get.FileSystemPaths.PathDirTemp))
            //{
            //    foreach (var file in Directory.GetFiles(Settings.Get.FileSystemPaths.PathDirTemp))
            //    {
            //        File.Delete(file);
            //    }
            //}
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.bgw.ReportProgress(17, "Loading Media Path Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MediaPathDb);

            this.bgw.ReportProgress(34, "Loading Movie Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MovieDb);

            this.bgw.ReportProgress(51, "Loading Media Sets Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MovieSets);

            this.bgw.ReportProgress(68, "Loading Tv Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.TvDb);

            this.bgw.ReportProgress(80, "Loading Media Path Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.ScanSeriesPick);

            this.bgw.ReportProgress(88, "Populating Movie Media Database");
            MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();

            this.bgw.ReportProgress(96, "Populating TV Media Database");
            MasterMediaDBFactory.PopulateMasterTVMediaDatabase();

            this.bgw.ReportProgress(97, "Validating Sets");
            MovieSetManager.ValidateSets();

            this.bgw.ReportProgress(100, "Done.");
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //lblProgress1.Text = e.UserState.ToString();
            //progress.EditValue = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.frmMain.Show();
            DatabaseIOFactory.AppLoading = false;
            MovieDBFactory.Instance.InvokeDatabaseChanged(new EventArgs());
            this.Hide();
        }
    }
}
