namespace YANFOE.UI.Dialogs.General
{
    using System.ComponentModel;
    using System.Windows.Forms;

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;

    public partial class FrmLoadingYANFOE : DevExpress.XtraEditors.XtraForm
    {
        private BackgroundWorker bgw = new BackgroundWorker();

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmLoadingYANFOE"/> class.
        /// </summary>
        public FrmLoadingYANFOE()
        {
            InitializeComponent();

            lblYANFOETitle.Text = Settings.ConstSettings.Application.ApplicationName + " " +
                                  Settings.ConstSettings.Application.ApplicationVersion;

            this.bgw.DoWork += this.bgw_DoWork;
            this.bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            this.bgw.ProgressChanged += this.bgw_ProgressChanged;
            this.bgw.WorkerReportsProgress = true;
            this.bgw.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            this.bgw.ReportProgress(17, "Loading Movie Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MovieDb);

            this.bgw.ReportProgress(34, "Loading Media Path Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MediaPathDb);

            this.bgw.ReportProgress(51, "Loading Media Sets Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.MovieSets);

            this.bgw.ReportProgress(68, "Loading Tv Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.TvDb);

            this.bgw.ReportProgress(80, "Loading Media Path Database");
            DatabaseIOFactory.Load(DatabaseIOFactory.OutputName.ScanSeriesPick);

            this.bgw.ReportProgress(88, "Populating Movie Media Database");
            MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();

            this.bgw.ReportProgress(96, "Populating TV Media Database");
            MasterMediaDBFactory.PopulateMasterTvMediaDatabase();

            this.bgw.ReportProgress(100, "Done.");
        }

        /// <summary>
        /// Handles the ProgressChanged event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProgress1.Text = e.UserState.ToString();
            progress.EditValue = e.ProgressPercentage;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var frmMain = new FrmMain();
            frmMain.Show();
            this.Hide();
        }
    }
}