namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.ComponentModel;

    using YANFOE.Factories;
    using YANFOE.Factories.Apps.MediaInfo.Models;
    using YANFOE.Tools.Xml;

    public partial class MediaInfoUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public enum FileInfoType
        {
            Movie,
            TV
        }

        public FileInfoType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaInfoUserControl"/> class.
        /// </summary>
        public MediaInfoUserControl()
        {
            InitializeComponent();

            this.InitialSetup();
        }

        private void InitialSetup()
        {
            if (this.Type == FileInfoType.Movie)
            {
                this.SetupMovieEventBinding();
            }
        }

        private void SetupMovieEventBinding()
        {
            MovieDBFactory.CurrentMovieChanged += (sender, e) => this.RefreshMovieBindings();
        }

        private void RefreshMovieBindings()
        {
            txtMediaInfoOutput.Clear();
            cmbFiles.Properties.Items.Clear();
            foreach (var file in MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media)
            {
                cmbFiles.Properties.Items.Add(file.FilePath);
            }

            cmbFiles.SelectedIndex = 0;
            PopulateMediaInfoModel(MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media[0].MiResponseModel);
        }

        /// <summary>
        /// Handles the Click event of the btnMediainfoScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnMediainfoScan_Click(object sender, EventArgs e)
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += (bgwSender, bgwE) =>
            {
                bgwE.Result =
                    Factories.Apps.MediaInfo.MediaInfoFactory.DoMediaInfoScan(cmbFiles.SelectedItem.ToString());
            };

            bgw.RunWorkerCompleted += (bgwSender, bgwE) =>
                {
                    this.PopulateMediaInfoModel(bgwE.Result as MiResponseModel);
                    btnMediainfoScan.Enabled = true;
                };

            btnMediainfoScan.Enabled = false;

            bgw.RunWorkerAsync();
        }

        private void PopulateMediaInfoModel(MiResponseModel miResponseModel)
        {
            if (miResponseModel == null)
            {
                return;
            }

            XFormat.AddColouredText(miResponseModel.ScanXML, txtMediaInfoOutput);
        }

        private void MediaInfoUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
