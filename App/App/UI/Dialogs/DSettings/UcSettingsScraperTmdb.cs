// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsScraperTmdb.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the UcSettingsScraperTmdb type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.DSettings
{
    public partial class UcSettingsScraperTmdb : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsScraperTmdb"/> class.
        /// </summary>
        public UcSettingsScraperTmdb()
        {
            InitializeComponent();

            radioPosterSize.SelectedIndex = Settings.Get.Scraper.TmDBDownloadPosterSize;
            radioFanartSize.SelectedIndex = Settings.Get.Scraper.TmDBDownloadFanartSize;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the radioPosterSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioPosterSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.Get.Scraper.TmDBDownloadPosterSize = radioPosterSize.SelectedIndex;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the radioFanartSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioFanartSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.Get.Scraper.TmDBDownloadFanartSize = radioFanartSize.SelectedIndex;
        }
    }
}
