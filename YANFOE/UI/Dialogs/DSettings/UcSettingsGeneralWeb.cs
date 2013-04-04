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
    public partial class UcSettingsGeneralWeb : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsGeneralWeb"/> class.
        /// </summary>
        public UcSettingsGeneralWeb()
        {
            InitializeComponent();

            this.chkProcessBackgroundDownloader.DataBindings.Add("Checked", Settings.Get.Web, "EnableBackgroundQueProcessing");
            this.chkAddtoBackgroundDownloader.DataBindings.Add("Checked", Settings.Get.Web, "EnableAddToBackgroundQue");

            this.txtDownloadThreads.DataBindings.Add("Value", Settings.Get.Web, "DownloadThreads");

            this.chkUseAProxyServer.DataBindings.Add("Checked", Settings.Get.Web, "EnableProxy");
            this.txtProxyAddress.DataBindings.Add("Text", Settings.Get.Web, "ProxyIP");
            this.txtProxyPort.DataBindings.Add("Text", Settings.Get.Web, "ProxyPort", true);
            this.txtProxyUsername.DataBindings.Add("Text", Settings.Get.Web, "ProxyUserName");
            this.txtProxyPassword.DataBindings.Add("Text", Settings.Get.Web, "ProxyPassword");
        }
    }
}
