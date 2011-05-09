// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsGeneralWeb.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.DSettings
{
    using YANFOE.Tools.Extentions;

    public partial class UcSettingsGeneralWeb : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsGeneralWeb"/> class.
        /// </summary>
        public UcSettingsGeneralWeb()
        {
            InitializeComponent();

            chkProcessBackgroundDownloader.DataBindings.Add("Checked", Settings.Get.Web, "EnableBackgroundQueProcessing");
            chkAddtoBackgroundDownloader.DataBindings.Add("Checked", Settings.Get.Web, "EnableAddToBackgroundQue");

            txtDownloadThreads.Value = Settings.Get.Web.DownloadThreads;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtDownloadThreads control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtDownloadThreads_TextChanged(object sender, System.EventArgs e)
        {
            Settings.Get.Web.DownloadThreads = txtDownloadThreads.Value.ToString().ToInt();
        }
    }
}
