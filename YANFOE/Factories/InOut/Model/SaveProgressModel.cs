// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="SaveProgressModel.cs">
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
//   The save progress model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.InOut.Model
{
    #region Required Namespaces

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The save progress model.
    /// </summary>
    public class SaveProgressModel : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The episode current.
        /// </summary>
        private int episodeCurrent;

        /// <summary>
        ///   The episode total.
        /// </summary>
        private int episodeTotal;

        /// <summary>
        ///   The season current.
        /// </summary>
        private int seasonCurrent;

        /// <summary>
        ///   The season total.
        /// </summary>
        private int seasonTotal;

        /// <summary>
        ///   The series current.
        /// </summary>
        private int seriesCurrent;

        /// <summary>
        ///   The series total.
        /// </summary>
        private int seriesTotal;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether EnableEpisode.
        /// </summary>
        public bool EnableEpisode { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether EnableSeason.
        /// </summary>
        public bool EnableSeason { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether EnableSeries.
        /// </summary>
        public bool EnableSeries { get; set; }

        /// <summary>
        ///   Gets or sets EpisodeCurrent.
        /// </summary>
        public int EpisodeCurrent
        {
            get
            {
                return this.episodeCurrent;
            }

            set
            {
                this.episodeCurrent = value;
                this.OnPropertyChanged("EpisodeCurrent");
            }
        }

        /// <summary>
        ///   Gets or sets the episode text.
        /// </summary>
        /// <value> The episode text. </value>
        public string EpisodeText { get; set; }

        /// <summary>
        ///   Gets or sets EpisodeTotal.
        /// </summary>
        public int EpisodeTotal
        {
            get
            {
                return this.episodeTotal;
            }

            set
            {
                this.episodeTotal = value;
                this.OnPropertyChanged("EpisodeTotal");
            }
        }

        /// <summary>
        ///   Gets or sets SeasonCurrent.
        /// </summary>
        public int SeasonCurrent
        {
            get
            {
                return this.seasonCurrent;
            }

            set
            {
                this.seasonCurrent = value;
                this.OnPropertyChanged("SeasonCurrent");
            }
        }

        /// <summary>
        ///   Gets or sets the season text.
        /// </summary>
        /// <value> The season text. </value>
        public string SeasonText { get; set; }

        /// <summary>
        ///   Gets or sets SeasonTotal.
        /// </summary>
        public int SeasonTotal
        {
            get
            {
                return this.seasonTotal;
            }

            set
            {
                this.seasonTotal = value;
                this.OnPropertyChanged("SeasonTotal");
            }
        }

        /// <summary>
        ///   Gets or sets SeriesCurrent.
        /// </summary>
        public int SeriesCurrent
        {
            get
            {
                return this.seriesCurrent;
            }

            set
            {
                this.seriesCurrent = value;
                this.OnPropertyChanged("SeriesCurrent");
            }
        }

        /// <summary>
        ///   Gets or sets the series text.
        /// </summary>
        /// <value> The series text. </value>
        public string SeriesText { get; set; }

        /// <summary>
        ///   Gets or sets SeriesTotal.
        /// </summary>
        public int SeriesTotal
        {
            get
            {
                return this.seriesTotal;
            }

            set
            {
                this.seriesTotal = value;
                this.OnPropertyChanged("SeriesTotal");
            }
        }

        #endregion
    }
}