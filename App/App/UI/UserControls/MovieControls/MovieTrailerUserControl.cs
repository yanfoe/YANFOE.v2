// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieTrailerUserControl.cs" company="The YANFOE Project">
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


namespace YANFOE.UI.UserControls.MovieControls
{
    using System;

    using YANFOE.Factories;
    using YANFOE.Tools.Models;

    public partial class MovieTrailerUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieTrailerUserControl"/> class.
        /// </summary>
        public MovieTrailerUserControl()
        {
            InitializeComponent();

            this.SetupEventBindings();
            this.SetupBindings();
        }

        #endregion

        #region Initializations
        
        /// <summary>
        /// Setup Data Bindings
        /// </summary>
        private void SetupBindings()
        {
            grdTrailers.DataSource = null;
            grdTrailers.DataSource = MovieDBFactory.GetCurrentMovie().AlternativeTrailers;
        }

        /// <summary>
        /// Setup Event Bindings
        /// </summary>
        private void SetupEventBindings()
        {
            Factories.MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;
        }

        #endregion

        #region Methods

        #endregion

        #region Events

        #region UI Initiations

        #endregion

        #region Code Initiations

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            this.SetupBindings();
        }


        #endregion

        private void grdViewTrailers_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            for (int i = 0; i < grdViewTrailers.RowCount; i++)
            {
                if (i != e.RowHandle)
                {
                    ((TrailerDetailsModel)grdViewTrailers.GetRow(i)).SelectedTrailer = false;
                }
                else if (((TrailerDetailsModel)grdViewTrailers.GetRow(i)).SelectedTrailer)
                {
                    MovieDBFactory.GetCurrentMovie().CurrentTrailerUrl =
                        ((TrailerDetailsModel)grdViewTrailers.GetRow(i)).UriFull.ToString();
                }
                else if (!((TrailerDetailsModel)grdViewTrailers.GetRow(i)).SelectedTrailer)
                {
                    MovieDBFactory.GetCurrentMovie().CurrentTrailerUrl = string.Empty;
                }
            }
        }


        #endregion

        private void chkSelectedTrailer_CheckedChanged(object sender, EventArgs e)
        {
            grdViewTrailers.PostEditor();
        }
    }
}
