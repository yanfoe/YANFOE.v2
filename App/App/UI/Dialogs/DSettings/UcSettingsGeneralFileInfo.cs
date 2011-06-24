namespace YANFOE.UI.Dialogs.DSettings
{
    using System.Windows.Forms;

    public partial class UcSettingsGeneralFileInfo : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsGeneralFileInfo"/> class.
        /// </summary>
        public UcSettingsGeneralFileInfo()
        {
            InitializeComponent();

            SetupBindings();
        }

        /// <summary>
        /// Setup the bindings.
        /// </summary>
        private void SetupBindings()
        {
            txtVideoOutput480.DataBindings.Add("Text", Settings.Get.MediaInfo, "VideoOutput480", true, DataSourceUpdateMode.OnPropertyChanged);
            txtVideoOutput720.DataBindings.Add("Text", Settings.Get.MediaInfo, "VideoOutput720", true, DataSourceUpdateMode.OnPropertyChanged);
            txtVideoOutput1080.DataBindings.Add("Text", Settings.Get.MediaInfo, "VideoOutput1080", true, DataSourceUpdateMode.OnPropertyChanged);
            chkOutputPwhenFis24.DataBindings.Add("Checked", Settings.Get.MediaInfo, "OutputPWhenFIs24", true, DataSourceUpdateMode.OnPropertyChanged);

            chkUseDecimalAR.DataBindings.Add("Checked", Settings.Get.MediaInfo, "UseDecimalAspectRatio", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUsePercentAR.DataBindings.Add("Checked", Settings.Get.MediaInfo, "UsePercentAspectRatio", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUseDARinsteadOfPAR.DataBindings.Add("Checked", Settings.Get.MediaInfo, "UseDARInsteadOfPAR", true, DataSourceUpdateMode.OnPropertyChanged);

            txtDefineRes480From.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution480From", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDefineRes480To.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution480To", true, DataSourceUpdateMode.OnPropertyChanged);

            txtDefineRes576From.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution576From", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDefineRes576To.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution576To", true, DataSourceUpdateMode.OnPropertyChanged);

            txtDefineRes720From.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution720From", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDefineRes720To.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution720To", true, DataSourceUpdateMode.OnPropertyChanged);

            txtDefineRes1080From.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution1080From", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDefineRes1080To.DataBindings.Add("Text", Settings.Get.MediaInfo, "Resolution1080To", true, DataSourceUpdateMode.OnPropertyChanged);

            txtKeyFramesPerSecond.Text = Settings.Get.MediaInfo.KeyFPS;
            txtKeyNTSCPal.Text = Settings.Get.MediaInfo.KeyNTSCPal;
            txtKeyResolution.Text = Settings.Get.MediaInfo.KeyResolution;
            txtKeyRoundedFramesPerSecond.Text = Settings.Get.MediaInfo.KeyRoundedFPS;
            txtKeyScanType.Text = Settings.Get.MediaInfo.KeyScanType;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtVideoOutput480 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtVideoOutput480_TextChanged(object sender, System.EventArgs e)
        {
            lblVideoOutput480.Text = Settings.Get.MediaInfo.DoReplaceDemo(txtVideoOutput480.Text, 704, 480);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtVideoOutput720 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtVideoOutput720_TextChanged(object sender, System.EventArgs e)
        {
            lblVideoOutput720.Text = Settings.Get.MediaInfo.DoReplaceDemo(txtVideoOutput720.Text, 1280, 720);
        }

        /// <summary>
        /// Handles the TextChanged event of the txtVideoOutput1080 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtVideoOutput1080_TextChanged(object sender, System.EventArgs e)
        {
            lblVideoOutput1080.Text = Settings.Get.MediaInfo.DoReplaceDemo(txtVideoOutput1080.Text, 1920, 1080);
        }
    }
}
