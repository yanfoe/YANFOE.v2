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
    using System.Collections.Generic;
    using System.Windows.Forms;

    using DevExpress.Utils;

    using YANFOE.Factories;
    using YANFOE.Models.MovieModels;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Interfaces;
    using YANFOE.Tools.Enums;

    public partial class MovieIdentierUserControl1 : DevExpress.XtraEditors.XtraUserControl
    {
        #region Fields

        private List<IMovieScraper> scrapers { get; set; }

        #endregion

        #region Constructor

        public MovieIdentierUserControl1()
        {
            InitializeComponent();

            this.scrapers = MovieScraperHandler.ReturnAllScrapers();

            this.SetupEventBindings();
            this.SetupIdBoxes();
            this.SetupBindings();
            this.PopulateValuesFromScrapers();

            webBrowser.Url = YANFOE.Settings.Get.Web.DefaultURLInBrowser;
            webBrowser.ScriptErrorsSuppressed = true;
        }

        private void SetupIdBoxes()
        {
            foreach (IMovieScraper scraper in scrapers)
            {
                var item = layoutIds.Root.AddItem();
                var textBox = new DevExpress.XtraEditors.TextEdit { Name = "txtId" + scraper.ScraperName.ToString() };
                item.Name = scraper.ScraperName.ToString();
                item.Text = Tools.Importing.MovieNaming.AddSpacesToSentence(scraper.ScraperName.ToString()) + ":";
                textBox.SuperTip = new SuperToolTip();
                textBox.SuperTip.Items.Add(scraper.DefaultUrl);
                item.Control = textBox;
            }
        }

        private void PopulateValuesFromScrapers()
        {
            foreach (IMovieScraper scraper in scrapers)
            {
                if (scraper.IncludeInWebIDList)
                {
                    if (!string.IsNullOrEmpty(scraper.DefaultUrl))
                    {
                        txtAddressBar.Properties.Items.Add(scraper.DefaultUrl);
                    }
                }
            }
        }

        #endregion

        #region Initializations

        /// <summary>
        /// Setup Data Bindings
        /// </summary>
        private void SetupBindings()
        {
            foreach (IMovieScraper scraper in scrapers)
            {
                if (scraper.IncludeInWebIDList)
                {
                    var controls = layoutIds.Controls.Find("txtId" + scraper.ScraperName.ToString(), true);
                
                    controls[0].DataBindings.Clear();
                    controls[0].DataBindings.Add(
                        "Text", MovieDBFactory.GetCurrentMovie(), scraper.ScraperName.ToString() + "Id");
                }
            }

            //txtImdbID.DataBindings.Clear();
            //txtImdbID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "ImdbId");

            //txtTmdbId.DataBindings.Clear();
            //txtTmdbId.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "TmdbId");
        
            //txtAllocineID.DataBindings.Clear();
            //txtAllocineID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "AllocineId");

            //txtFilmAffinityID.DataBindings.Clear();
            //txtFilmAffinityID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmAffinityId");

            //txtFilmDeltaID.DataBindings.Clear();
            //txtFilmDeltaID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmDeltaId");

            //txtFilmUpID.DataBindings.Clear();
            //txtFilmUpID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmUpId");

            //txtFilmWebID.DataBindings.Clear();
            //txtFilmWebID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "FilmWebId");

            //txtImpawardsID.DataBindings.Clear();
            //txtImpawardsID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "ImpawardsId");

            //txtKinopoiskID.DataBindings.Clear();
            //txtKinopoiskID.DataBindings.Add("Text", MovieDBFactory.GetCurrentMovie(), "KinopoiskId");
        }

        /// <summary>
        /// Setup Event Bindings
        /// </summary>
        private void SetupEventBindings()
        {
            MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;
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

        #endregion

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            btnPrevious.Enabled = webBrowser.CanGoBack;
            btnNext.Enabled = webBrowser.CanGoForward;
            txtAddressBar.Text = webBrowser.Url.ToString();

            ChangeUrlForID();
        }

        private void ChangeUrlForID()
        {
            //var imdbCheck = Regex.Match(webBrowser.Url.ToString(), imdb);

            //if (imdbCheck.Success)
            //{
            //    btnUse.Enabled = true;
            //    return;
            //}
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            webBrowser.GoForward();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            webBrowser.GoBack();
        }

        private void webBrowser_ProgressChanged(object sender, System.Windows.Forms.WebBrowserProgressChangedEventArgs e)
        {
            progressBarControl.Properties.Maximum = (int)e.MaximumProgress;
            progressBarControl.Position = (int)e.CurrentProgress;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser.Refresh();
        }

        private void txtAddressBar_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                webBrowser.Url = new Uri(txtAddressBar.Text);
            }
        }
    }
}
