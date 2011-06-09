namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    using YANFOE.Factories;
    using YANFOE.Factories.Apps.MediaInfo;
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
            this.PopulateDropdowns();
            this.PopulateFileInfo();
        }

        private void PopulateFileInfo()
        {
            txtVideoCodec.DataBindings.Clear();
            txtVideoCodec.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "Codec", true, DataSourceUpdateMode.OnPropertyChanged);
        
            txtWidth.DataBindings.Clear();
            txtWidth.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "Width", true, DataSourceUpdateMode.OnPropertyChanged);

            txtHeight.DataBindings.Clear();
            txtHeight.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "Height", true, DataSourceUpdateMode.OnPropertyChanged);
        
            txtFPSFull.DataBindings.Clear();
            txtFPSFull.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "FPS");

            txtFPSRounded.DataBindings.Clear();
            txtFPSRounded.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "FPSRounded");

            cmbAspectRatioDecimal.DataBindings.Clear();
            cmbAspectRatioDecimal.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "AspectRatioDecimal");

            cmbAspectRatio.DataBindings.Clear();
            cmbAspectRatio.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "AspectRatio");

            txtResolution.DataBindings.Clear();
            txtResolution.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie().FileInfo, "Resolution");

            chkInterlaced.DataBindings.Clear();
            chkInterlaced.DataBindings.Add("Checked", MovieDBFactory.GetCurrentMovie().FileInfo, "InterlacedScan");

            chkProgressive.DataBindings.Clear();
            chkProgressive.DataBindings.Add("Checked", MovieDBFactory.GetCurrentMovie().FileInfo, "ProgressiveScan");

            chkPal.DataBindings.Clear();
            chkPal.DataBindings.Add("Checked", MovieDBFactory.GetCurrentMovie().FileInfo, "Pal");

            chkNtsc.DataBindings.Clear();
            chkNtsc.DataBindings.Add("Checked", MovieDBFactory.GetCurrentMovie().FileInfo, "Ntsc");
        }

        private void PopulateDropdowns()
        {
            cmbAspectRatio.Properties.Items.AddRange(Settings.Get.MediaInfo.AspectRatio);
            cmbAspectRatioDecimal.Properties.Items.AddRange(Settings.Get.MediaInfo.AspectRatioDecimal);
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
            PopulateFileInfo();
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
                    var result = MediaInfoFactory.DoMediaInfoScan(cmbFiles.SelectedItem.ToString());

                    bgwE.Result = result;
                    MediaInfoFactory.InjectResponseModel(result, MovieDBFactory.GetCurrentMovie());
                };

            bgw.RunWorkerCompleted += (bgwSender, bgwE) =>
                {
                    this.PopulateMediaInfoModel(bgwE.Result as MiResponseModel);
                    btnMediainfoScan.Enabled = true;
                    PopulateFileInfo();
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

        private void cmbAspectRatioDecimal_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmbAspectRatio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
