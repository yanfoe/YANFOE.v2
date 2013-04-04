// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmSelectSeries.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.TV
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.TvModels.TVDB;
    using YANFOE.Scrapers.TV;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Factories.Import;

    /// <summary>
    /// The frm select series.
    /// </summary>
    public partial class FrmSelectSeries : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        /// The search details.
        /// </summary>
        private IEnumerable<SearchDetails> searchDetails;

        /// <summary>
        /// The search term.
        /// </summary>
        private string searchTerm;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmSelectSeries"/> class.
        /// </summary>
        /// <param name="searchDetails">
        /// The search details.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        public FrmSelectSeries(IEnumerable<SearchDetails> searchDetails, string searchTerm)
        {
            this.InitializeComponent();

            this.Setup(searchDetails, searchTerm);

            this.CmbSearchResults_SelectedValueChanged(null, null);

            lblLanguage.Text = Settings.Get.Scraper.TvDBLanguageAbbr;
            var seriesname =
                        (from s in ImportTvFactory.SeriesNameList where s.SeriesName == searchTerm select s).SingleOrDefault();
            lblSeriesPath.Text = seriesname.SeriesPath;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets or sets SelectedSeries.
        /// </summary>
        public SearchDetails SelectedSeries { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The get allow skin.
        /// </summary>
        /// <returns>
        /// returns true
        /// </returns>
        protected override bool GetAllowSkin()
        {
            return true;
        }

        /// <summary>
        /// Handles the Click event of the ButCancel control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void ButCancel_Click(object sender, EventArgs e)
        {
            this.Cancelled = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the ButSearchAgain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void ButSearchAgain_Click(object sender, EventArgs e)
        {
            this.butSearchAgain.Enabled = false;
            this.butUse.Enabled = false;
            this.butCancel.Enabled = false;

            var theTvdb = new TheTvdb();

            this.searchDetails = theTvdb.SeriesSearch(this.txtSearchAgain.Text);
            this.Setup(this.searchDetails, this.txtSearchAgain.Text);

            this.butSearchAgain.Enabled = true;
            this.butUse.Enabled = true;
            this.butCancel.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the ButUse control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void ButUse_Click(object sender, EventArgs e)
        {
            //fix for situation when user click on "OK" button and didn't choose any option from cmbSearchResults
            if (this.cmbSearchResults.SelectedValue != null)
            {
                string id =
                    Regex.Match(this.cmbSearchResults.SelectedValue.ToString(), @"(?<id>\d*?):.*?").Groups["id"].Value;
                this.SelectedSeries = this.searchDetails.Single(s => s.ID.ToString() == id.ToString());
                this.Close();
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the CmbSearchResults control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void CmbSearchResults_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.cmbSearchResults.Items.Count == 0)
            {
                this.pictureBox1.Image = null;
                this.lblSearchTerm.Text = this.searchTerm;
                this.lblSeriesName.Text = string.Empty;
                this.lblFirstAired.Text = string.Empty;
                this.lblLanguage.Text = string.Empty;
                this.tbOverview.Text = string.Empty;
                this.butUse.Enabled = false;
                return;
            }

            string id =
                Regex.Match(this.cmbSearchResults.SelectedValue.ToString(), @"(?<id>\d*?):.*?").Groups["id"].Value;
            SearchDetails result = this.searchDetails.Single(s => s.ID.ToString() == id.ToString());

            this.lblSearchTerm.Text = this.searchTerm;

            this.lblSeriesName.Text = result.SeriesName;
            if (result.FirstAired != null)
            {
                this.lblFirstAired.Text = result.FirstAired.Value.ToString("yyyy-MM-dd");
            }

            this.lblLanguage.Text = result.Language;
            this.tbOverview.Text = result.OverView;

            if (!string.IsNullOrEmpty(result.Banner))
            {
                string url = result.Banner;
                url = TheTvdb.ReturnBannerDownloadPath(url, true);

                string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);

                this.pictureBox1.Image = ImageHandler.LoadImage(path);
            }

            this.butUse.Enabled = true;
        }

        /// <summary>
        /// Setups the specified search details.
        /// </summary>
        /// <param name="searchDetails">
        /// The search details.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        private void Setup(IEnumerable<SearchDetails> searchDetails, string searchTerm)
        {
            this.cmbSearchResults.Items.Clear();

            this.searchDetails = searchDetails;
            this.searchTerm = searchTerm;

            foreach (SearchDetails s in searchDetails)
            {
                this.cmbSearchResults.Items.Add(string.Format("{1}: {0}", s.SeriesName, s.ID));
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the TxtSearchAgain control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.
        /// </param>
        private void TxtSearchAgain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ButSearchAgain_Click(null, new EventArgs());
            }
        }

        #endregion
    }
}