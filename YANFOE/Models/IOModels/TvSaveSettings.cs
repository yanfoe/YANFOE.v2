// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="TvSaveSettings.cs">
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
// <summary>
//   The tv save settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.IOModels
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   The tv save settings.
    /// </summary>
    [Serializable]
    public class TvSaveSettings
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="TvSaveSettings" /> class.
        /// </summary>
        public TvSaveSettings()
        {
            this.SeriesNfoPath = string.Empty;

            this.SeriesPosterPath = string.Empty;

            this.SeriesFanartPath = string.Empty;

            this.SeriesBannerPath = string.Empty;

            this.SeriesSingleBannerTemplate = string.Empty;

            this.SeriesSingleFanartTemplate = string.Empty;

            this.SeriesSingleNfoTemplate = string.Empty;

            this.SeriesSinglePosterTemplate = string.Empty;

            this.SeriesMultiNfoTemplate = string.Empty;

            this.SeriesMultiPosterTemplate = string.Empty;

            this.SeriesMultiFanartTemplate = string.Empty;

            this.SeriesMultiBannerTemplate = string.Empty;

            this.SeasonBannerTemplate = string.Empty;

            this.SeasonFanartTemplate = string.Empty;

            this.SeasonPosterTemplate = string.Empty;

            this.EpisodeNFOTemplate = string.Empty;

            this.EpisodeScreenshotTemplate = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets BlurayEpisodeNFOTemplate.
        /// </summary>
        public string BlurayEpisodeNFOTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BlurayEpisodeScreenshotTemplate.
        /// </summary>
        public string BlurayEpisodeScreenshotTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeasonBannerTemplate.
        /// </summary>
        public string BluraySeasonBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeasonFanartTemplate.
        /// </summary>
        public string BluraySeasonFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeasonPosterTemplate.
        /// </summary>
        public string BluraySeasonPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeriesBannerTemplate.
        /// </summary>
        public string BluraySeriesBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeriesFanartTemplate.
        /// </summary>
        public string BluraySeriesFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeriesNfoTemplate.
        /// </summary>
        public string BluraySeriesNfoTemplate { get; set; }

        /// <summary>
        ///   Gets or sets BluraySeriesPosterTemplate.
        /// </summary>
        public string BluraySeriesPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDEpisodeNFOTemplate.
        /// </summary>
        public string DVDEpisodeNFOTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDEpisodeScreenshotTemplate.
        /// </summary>
        public string DVDEpisodeScreenshotTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeasonBannerTemplate.
        /// </summary>
        public string DVDSeasonBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeasonFanartTemplate.
        /// </summary>
        public string DVDSeasonFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeasonPosterTemplate.
        /// </summary>
        public string DVDSeasonPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeriesBannerTemplate.
        /// </summary>
        public string DVDSeriesBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeriesFanartTemplate.
        /// </summary>
        public string DVDSeriesFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeriesNfoTemplate.
        /// </summary>
        public string DVDSeriesNfoTemplate { get; set; }

        /// <summary>
        ///   Gets or sets DVDSeriesPosterTemplate.
        /// </summary>
        public string DVDSeriesPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets EpisodeNFOTemplate.
        /// </summary>
        public string EpisodeNFOTemplate { get; set; }

        /// <summary>
        ///   Gets or sets EpisodeScreenshotTemplate.
        /// </summary>
        public string EpisodeScreenshotTemplate { get; set; }

        /// <summary>
        ///   Gets or sets RenameEpisodeTemplate.
        /// </summary>
        public string RenameEpisodeTemplate { get; set; }

        /// <summary>
        ///   Gets or sets RenameSeasonTemplate.
        /// </summary>
        public string RenameSeasonTemplate { get; set; }

        /// <summary>
        ///   Gets or sets RenameSeriesTemplate.
        /// </summary>
        public string RenameSeriesTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeasonBannerTemplate.
        /// </summary>
        public string SeasonBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeasonFanartTemplate.
        /// </summary>
        public string SeasonFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeasonPosterTemplate.
        /// </summary>
        public string SeasonPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesBannerPath.
        /// </summary>
        public string SeriesBannerPath { get; set; }

        /// <summary>
        ///   Gets or sets SeriesBannerTemplate.
        /// </summary>
        public string SeriesBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesFanartPath.
        /// </summary>
        public string SeriesFanartPath { get; set; }

        /// <summary>
        ///   Gets or sets SeriesFanartTemplate.
        /// </summary>
        public string SeriesFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesMultiBannerTemplate.
        /// </summary>
        public string SeriesMultiBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesMultiFanartTemplate.
        /// </summary>
        public string SeriesMultiFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesMultiNfoTemplate.
        /// </summary>
        public string SeriesMultiNfoTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesMultiPosterTemplate.
        /// </summary>
        public string SeriesMultiPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesNfoPath.
        /// </summary>
        public string SeriesNfoPath { get; set; }

        /// <summary>
        ///   Gets or sets SeriesNfoTemplate.
        /// </summary>
        public string SeriesNfoTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesPosterPath.
        /// </summary>
        public string SeriesPosterPath { get; set; }

        /// <summary>
        ///   Gets or sets SeriesPosterTemplate.
        /// </summary>
        public string SeriesPosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesSingleBannerTemplate.
        /// </summary>
        public string SeriesSingleBannerTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesSingleFanartTemplate.
        /// </summary>
        public string SeriesSingleFanartTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesSingleNfoTemplate.
        /// </summary>
        public string SeriesSingleNfoTemplate { get; set; }

        /// <summary>
        ///   Gets or sets SeriesSinglePosterTemplate.
        /// </summary>
        public string SeriesSinglePosterTemplate { get; set; }

        /// <summary>
        ///   Gets or sets TvStructure.
        /// </summary>
        public string TvStructure { get; set; }

        #endregion
    }
}