// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadStatusUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.DownloadControls
{
    using DevExpress.XtraEditors;

    using YANFOE.InternalApps.DownloadManager;

    public partial class DownloadsUserControl : XtraUserControl
    {
        public DownloadsUserControl()
        {
            InitializeComponent();

            ucDownloadThread1.UpdateProgress("Thread 1", Downloader.Progress[0]);
            ucDownloadThread2.UpdateProgress("Thread 2", Downloader.Progress[1]);
            ucDownloadThread3.UpdateProgress("Thread 3", Downloader.Progress[2]);
            ucDownloadThread4.UpdateProgress("Thread 4", Downloader.Progress[3]);
            ucDownloadThread5.UpdateProgress("Thread 5", Downloader.Progress[4]);
            ucDownloadThread6.UpdateProgress("Thread 6", Downloader.Progress[5]);
            ucDownloadThread7.UpdateProgress("Thread 7", Downloader.Progress[6]);
            ucDownloadThread8.UpdateProgress("Thread 8", Downloader.Progress[7]);

            gridControl1.DataSource = Downloader.BackgroundDownloadQue;
        }

        private void ucDownloadThread1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
