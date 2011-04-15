// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieIdentierUserControl1.cs" company="The YANFOE Project">
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
    using System.ComponentModel;

    using YANFOE.Factories;
    using YANFOE.Scrapers.Movie.Models.Search;

    public partial class MovieIdentierUserControl1 : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        #endregion

        #region Constructor

        public MovieIdentierUserControl1()
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
            txtImdbID.DataBindings.Clear();
            txtImdbID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "ImdbId");

            txtTmdbId.DataBindings.Clear();
            txtTmdbId.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "TmdbId");
        
            txtAllocineID.DataBindings.Clear();
            txtAllocineID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "AllocineId");

            txtFilmAffinityID.DataBindings.Clear();
            txtFilmAffinityID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmAffinityId");

            txtFilmDeltaID.DataBindings.Clear();
            txtFilmDeltaID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmDeltaId");

            txtFilmUpID.DataBindings.Clear();
            txtFilmUpID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmUpId");

            txtFilmWebID.DataBindings.Clear();
            txtFilmWebID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmWebId");

            txtImpawardsID.DataBindings.Clear();
            txtImpawardsID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "ImpawardsId");

            txtKinopoiskID.DataBindings.Clear();
            txtKinopoiskID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "KinopoiskId");
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
            SetupBindings();
        }


        #endregion

        /// <summary>
        /// Handles the Click event of the btnYANFOEQuickSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnYANFOEQuickSearch_Click(object sender, EventArgs e)
        {
            this.DoQuickSearch();
        }

        private void DoQuickSearch()
        {
            var bgw = new BackgroundWorker();

            bgw.DoWork += this.bgw_DoWork;
            bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;

            btnYANFOEQuickSearch.Enabled = false;

            bgw.RunWorkerAsync();

        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var results = new BindingList<QueryResult>();
            var query = new Query
            {
                Results = results,
                Title = MovieDBFactory.GetCurrentMovie().Title,
                Year = MovieDBFactory.GetCurrentMovie().Year.ToString()
            };

            Factories.Scraper.MovieScrapeFactory.QuickSearchTmdb(query);

            e.Result = query;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnYANFOEQuickSearch.Enabled = true;
        }

        #endregion
    }
}
