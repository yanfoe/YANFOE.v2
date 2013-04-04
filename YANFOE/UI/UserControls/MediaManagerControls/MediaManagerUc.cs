// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaManagerUc.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.MediaManagerControls
{
    using System;
    using System.ComponentModel;

    using DevExpress.Utils;
    using DevExpress.XtraEditors;

    using YANFOE.UI.Dialogs.General;

    public partial class MediaManagerUc : XtraUserControl
    {
        private FrmProcessingMoviesStatus processingMoviesStatus = new FrmProcessingMoviesStatus();

        public MediaManagerUc()
        {
            InitializeComponent();

            this.xtraTabControl1.ShowTabHeader = DefaultBoolean.False;

            mediaPathManager1.ImportMoviesClicked += this.mediaPathsUc1_ImportMoviesClicked;
            mediaPathManager1.ImportTvClicked += this.mediaPathsUc1_ImportTvClicked;

            importMoviesUc1.OkClicked += this.importMoviesUc1_OkClicked;
            importMoviesUc1.CancelClicked += this.importMoviesUc1_CancelClicked;
        }

        void mediaPathsUc1_ImportTvClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void mediaPathsUc1_ImportMoviesClicked(object sender, EventArgs e)
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += this.bgw_DoWork;
            bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;

            bgw.RunWorkerAsync();

            this.processingMoviesStatus.ShowDialog();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            importMoviesUc1.Reset();
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.MoveToMoviesTab();
            this.processingMoviesStatus.Hide();
        }

        void importMoviesUc1_OkClicked(object sender, EventArgs e)
        {
            this.MovieToMediaPaths();
        }

        void importMoviesUc1_CancelClicked(object sender, EventArgs e)
        {
            this.MovieToMediaPaths();
        }

        private void MovieToMediaPaths()
        {
            this.xtraTabControl1.SelectedTabPage = this.tabMediaPaths;
        }

        private void MoveToMoviesTab()
        {
            this.xtraTabControl1.SelectedTabPage = this.tabMovies;
        }

        private void mediaPathManager1_Load(object sender, EventArgs e)
        {

        }
    }
}
