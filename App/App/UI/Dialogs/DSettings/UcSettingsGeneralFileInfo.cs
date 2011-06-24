namespace YANFOE.UI.Dialogs.DSettings
{
    using System.Windows.Forms;

    public partial class UcSettingsGeneralFileInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSettingsGeneralFileInfo()
        {
            InitializeComponent();

            SetupBindings();
        }

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
    }
}
