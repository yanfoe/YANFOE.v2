// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsTvIO.cs" company="The YANFOE Project">
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
    using System;

    using DevExpress.XtraEditors;

    using YANFOE.Settings;
    using YANFOE.Tools;

    /// <summary>
    /// The uc settings tv io.
    /// </summary>
    public partial class UcSettingsTvIO : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsTvIO"/> class.
        /// </summary>
        public UcSettingsTvIO()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the initial binding.
        /// </summary>
        private void SetInitialBinding()
        {
            this.txtSeriesNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeriesNfoTemplate", true);
            this.txtSeriesPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeriesPosterTemplate", true);
            this.txtSeriesBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeriesBannerTemplate", true);
            this.txtSeriesFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeriesFanartTemplate", true);

            this.txtDVDSeriesNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeriesNfoTemplate");
            this.txtDVDSeriesPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeriesPosterTemplate");
            this.txtDVDSeriesBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeriesBannerTemplate");
            this.txtDVDSeriesFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeriesFanartTemplate");

            this.txtBluraySeriesNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeriesNfoTemplate");
            this.txtBluraySeriesPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeriesPosterTemplate");
            this.txtBluraySeriesBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeriesBannerTemplate");
            this.txtBluraySeriesFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeriesFanartTemplate");

            this.txtSeasonPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeasonPosterTemplate");
            this.txtSeasonFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeasonFanartTemplate");
            this.txtSeasonBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "SeasonBannerTemplate");

            this.txtDVDSeasonPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeasonPosterTemplate");
            this.txtDVDSeasonFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeasonFanartTemplate");
            this.txtDVDSeasonBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDSeasonBannerTemplate");

            this.txtBluraySeasonPoster.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeasonPosterTemplate");
            this.txtBluraySeasonFanart.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeasonFanartTemplate");
            this.txtBluraySeasonBanner.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BluraySeasonBannerTemplate");

            this.txtEpisodeNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "EpisodeNFOTemplate");
            this.txtDVDEpisodeNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDEpisodeNFOTemplate");
            this.txtBlurayEpisodeNFO.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BlurayEpisodeNFOTemplate");

            this.txtEpisodeScreenshot.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "EpisodeScreenshotTemplate");
            this.txtDVDEpisodeScreenshot.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "DVDEpisodeScreenshotTemplate");
            this.txtBlurayEpisodeScreenshot.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentTvSaveSettings, "BlurayEpisodeScreenshotTemplate");
        }

        /// <summary>
        /// Handles the EditValueChanged event of the TxtBluray control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TxtBluray_EditValueChanged(object sender, EventArgs e)
        {
            this.txtBluraySeriesNFOPreview.Text = GeneratePath.TvSeries(
                null, Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesNfoTemplate, this.txtBlurayTestPath.Text);

            this.txtBluraySeriesPosterPreview.Text =
                GeneratePath.TvSeries(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesPosterTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBluraySeriesBannerPreview.Text =
                GeneratePath.TvSeries(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesBannerTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBluraySeriesFanartPreview.Text =
                GeneratePath.TvSeries(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesFanartTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBluraySeasonPosterPreview.Text =
                GeneratePath.TvSeason(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonPosterTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBluraySeasonFanartPreview.Text =
                GeneratePath.TvSeason(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonFanartTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBluraySeasonBannerPreview.Text =
                GeneratePath.TvSeason(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonBannerTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";

            this.txtBlurayEpisodeNFOPreview.Text = GeneratePath.TvEpisode(
                null, Get.InOutCollection.CurrentTvSaveSettings.BlurayEpisodeNFOTemplate, this.txtBlurayTestPath.Text);

            this.txtBlurayEpisodeScreenshotPreview.Text =
                GeneratePath.TvEpisode(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.BlurayEpisodeScreenshotTemplate, 
                    this.txtBlurayTestPath.Text) + ".jpg";
        }

        /// <summary>
        /// Handles the Load event of the UcSettingsTvIO control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UcSettingsTvIO_Load(object sender, EventArgs e)
        {
            this.SetInitialBinding();
        }

        /// <summary>
        /// Handles the EditValueChanged event of the txtDVD control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TxtDVD_EditValueChanged(object sender, EventArgs e)
        {
            this.txtDVDSeriesNFOPreview.Text = GeneratePath.TvSeries(
                null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesNfoTemplate, this.txtDVDTestPath.Text);
            this.txtDVDSeriesPosterPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesPosterTemplate, this.txtDVDTestPath.Text) +
                ".jpg";
            this.txtDVDSeriesBannerPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesBannerTemplate, this.txtDVDTestPath.Text) +
                ".jpg";
            this.txtDVDSeriesFanartPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesFanartTemplate, this.txtDVDTestPath.Text) +
                ".jpg";

            this.txtDVDSeasonPosterPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonPosterTemplate, this.txtDVDTestPath.Text) +
                ".jpg";
            this.txtDVDSeasonFanartPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonFanartTemplate, this.txtDVDTestPath.Text) +
                ".jpg";
            this.txtDVDSeasonBannerPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonFanartTemplate, this.txtDVDTestPath.Text) +
                ".jpg";

            this.txtDVDEpisodeNFOPreview.Text = GeneratePath.TvEpisode(
                null, Get.InOutCollection.CurrentTvSaveSettings.DVDEpisodeNFOTemplate, this.txtDVDTestPath.Text);
            this.txtDVDEpisodeScreenshotPreview.Text =
                GeneratePath.TvEpisode(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.DVDEpisodeScreenshotTemplate, 
                    this.txtDVDTestPath.Text) + ".jpg";
        }

        /// <summary>
        /// Handles the EditValueChanged event of the txtNormal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TxtNormal_EditValueChanged(object sender, EventArgs e)
        {


            this.txtSeriesNFOPreview.Text = GeneratePath.TvSeries(
                null, Get.InOutCollection.CurrentTvSaveSettings.SeriesNfoTemplate, this.txtNormalTestPath.Text);
            this.txtSeriesPosterPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeriesPosterTemplate, this.txtNormalTestPath.Text) +
                ".jpg";
            this.txtSeriesBannerPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeriesBannerTemplate, this.txtNormalTestPath.Text) +
                ".jpg";
            this.txtSeriesFanartPreview.Text =
                GeneratePath.TvSeries(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeriesFanartTemplate, this.txtNormalTestPath.Text) +
                ".jpg";

            this.txtSeasonPosterPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeasonPosterTemplate, this.txtNormalTestPath.Text) +
                ".jpg";
            this.txtSeasonFanartPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeasonFanartTemplate, this.txtNormalTestPath.Text) +
                ".jpg";
            this.txtSeasonBannerPreview.Text =
                GeneratePath.TvSeason(
                    null, Get.InOutCollection.CurrentTvSaveSettings.SeasonBannerTemplate, this.txtNormalTestPath.Text) +
                ".jpg";

            this.txtEpisodeNFOPreview.Text = GeneratePath.TvEpisode(
                null, Get.InOutCollection.CurrentTvSaveSettings.EpisodeNFOTemplate, this.txtNormalTestPath.Text);
            this.txtEpisodeScreenshotPreview.Text =
                GeneratePath.TvEpisode(
                    null, 
                    Get.InOutCollection.CurrentTvSaveSettings.EpisodeScreenshotTemplate, 
                    this.txtNormalTestPath.Text) + ".jpg";
        }

        #endregion
    }
}