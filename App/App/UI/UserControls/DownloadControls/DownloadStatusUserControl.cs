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
    using System.Net;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.InternalApps.DownloadManager.Model;

    public partial class DownloadStatusUserControl : XtraUserControl
    {
        public Progress Progress;

        private Timer timer;

        public DownloadStatusUserControl()
        {
            InitializeComponent();
            
            Progress = new Progress();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += this.TimerTick;
            timer.Start();
        }

        void TimerTick(object sender, System.EventArgs e)
        {
            this.txtStatus.Text = this.Progress.Message;
            this.prgBar.Position = this.Progress.Percent;
        }

        public void UpdateProgress(string groupTitle, Progress prog)
        {
            this.grpControl.Text = groupTitle;
            this.Progress = prog;
        }

        private void grpControl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLog_Click(object sender, System.EventArgs e)
        {
            DownloadItem di = new DownloadItem();
            di.CookieContainer = new CookieContainer();
            di.IgnoreCache = true;
            di.Type = DownloadType.Html;
            di.Url = "http://www.facebook.com";
        }
    }
}
