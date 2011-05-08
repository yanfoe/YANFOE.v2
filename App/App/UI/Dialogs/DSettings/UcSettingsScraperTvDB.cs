// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsScraperTvDB.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the UcSettingsScraperTvDB type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.DSettings
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Xml;

    public partial class UcSettingsScraperTvDB : DevExpress.XtraEditors.XtraUserControl
    {
        private string languagesXML = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsScraperTvDB"/> class.
        /// </summary>
        public UcSettingsScraperTvDB()
        {
            InitializeComponent();

            var bgw = new BackgroundWorker();
            bgw.DoWork += this.bgw_DoWork;
            bgw.RunWorkerCompleted += this.bgw_RunWorkerCompleted;
            
            picLoading.Visible = true;
            bgw.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            languagesXML = Downloader.ProcessDownload(
                string.Format(
                    "http://www.thetvdb.com/api/{0}/languages.xml", Settings.ConstSettings.Application.TvdbApi),
                DownloadType.Html,
                Section.Tv).RemoveCharacterReturn();
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            picLoading.Visible = false;

            var matches = Regex.Matches(
                languagesXML,
                @"<Language>\s{4}<name>(?<name>.*?)</name>\s{4}<abbreviation>(?<abbr>.*?)</abbreviation>\s{4}<id>(?<id>.*?)</id>\s{2}</Language>");

            var languages = (from Match match in matches select string.Format("{0} ({1})", match.Groups["name"].Value, match.Groups["abbr"].Value)).ToList();
        
            foreach (var lang in (from l in languages orderby l ascending select l).ToList())
            {
                cmbLanguages.Properties.Items.Add(lang);
            }

            cmbLanguages.Text = Settings.Get.Scraper.TvDBLanguage;
        }

        private void cmbLanguages_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Settings.Get.Scraper.TvDBLanguage = cmbLanguages.Text;
        }
    }
}
