// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmSavingDB.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.General
{
    using System.Threading;

    public partial class FrmSavingDB : DevExpress.XtraEditors.XtraForm
    {
        private bool tvFinished;

        private bool movieFinished;

        public FrmSavingDB()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            picMovieDatabase.Image = Properties.Resources.delete32;
            picTvDatabase.Image = Properties.Resources.delete32;
            progSavingMovieDB.EditValue = 0;
            progSavingTvDB.EditValue = 0;
            this.tvFinished = false;
            this.movieFinished = false;
        }

        public void SetMovieDBMax(int value)
        {
            progSavingMovieDB.Properties.Maximum = value - 1;
        }

        public void SetMovieValue(int value)
        {
            progSavingMovieDB.EditValue = value;
        }

        public void SetTVDBMax(int value)
        {
            progSavingTvDB.Properties.Maximum = value - 1;
        }

        public void SetTVDBValue(int value)
        {
            progSavingTvDB.EditValue = value;
        }

        public void MovieDBFinished()
        {
            picMovieDatabase.Image = Properties.Resources.accept32;
            this.movieFinished = true;
            this.Check();
        }

        private void Check()
        {
            if (this.movieFinished && this.tvFinished)
            {
                this.Hide();
            }
        }

        public void TvDBFinished()
        {
            picTvDatabase.Image = Properties.Resources.accept32;
            this.tvFinished = true;
            this.Check();
        }
    }
}