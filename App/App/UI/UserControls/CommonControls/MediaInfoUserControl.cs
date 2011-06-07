namespace YANFOE.UI.UserControls.CommonControls
{
    using System;

    using YANFOE.Factories;
    using YANFOE.Factories.Apps.MediaInfo.Models;

    public partial class MediaInfoUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public enum FileInfoType
        {
            Movie,
            TV
        }

        public FileInfoType Type { get; set; }

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
            cmbFiles.Properties.Items.Clear();
            foreach (var file in MovieDBFactory.GetCurrentMovie().AssociatedFiles.Media)
            {
                cmbFiles.Properties.Items.Add(file.FilePath);
            }

            cmbFiles.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the Click event of the btnMediainfoScan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnMediainfoScan_Click(object sender, EventArgs e)
        {
            var mediaInfoScanModel = Factories.Apps.MediaInfo.MediaInfoFactory.DoMediaInfoScan(cmbFiles.SelectedItem.ToString());

            this.PopulateMediaInfoModel(mediaInfoScanModel);
        }

        private void PopulateMediaInfoModel(MiResponseModel miResponseModel)
        {
            throw new NotImplementedException();
        }
    }
}
