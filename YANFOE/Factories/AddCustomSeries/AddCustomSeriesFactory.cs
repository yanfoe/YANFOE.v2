// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="AddCustomSeriesFactory.cs">
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
//   The add custom series factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.AddCustomSeries
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The add custom series factory.
    /// </summary>
    public static class AddCustomSeriesFactory
    {
        #region Public Events

        /// <summary>
        ///   Occurs when [series changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesChanged = delegate { };

        /// <summary>
        ///   Occurs when [update series list].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler UpdateSeriesList = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the new series model.
        /// </summary>
        public static AddCustomSeriesModel NewSeriesModel { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The add blank to current series.
        /// </summary>
        public static void AddBlankToCurrentSeries()
        {
            GetSeasonFiles().Add(new AddCustomSeriesFilesModel());
        }

        /// <summary>
        /// The add files.
        /// </summary>
        /// <param name="filteredFiles">
        /// The filtered files. 
        /// </param>
        public static void AddFiles(List<string> filteredFiles)
        {
            var fileCollection = GetSeasonFiles();

            foreach (var file in filteredFiles)
            {
                var check = fileCollection.SingleOrDefault(f => f.FilePath == file);

                if (check == null)
                {
                    var match = Regex.Match(
                        Path.GetFileName(file), @"(?<![0-9])s{0,1}([0-9]{1,2})((?:(?:(e|\se)[0-9]+)+)|(?:(?:x[0-9]+)+))");

                    var episodeNumber = 0;

                    if (match.Success)
                    {
                        episodeNumber = match.Groups[2].Value.GetNumber();
                    }

                    fileCollection.Add(new AddCustomSeriesFilesModel { FilePath = file, EpisodeNumber = episodeNumber });
                }
            }

            InvokeSeriesChanged(new EventArgs());
        }

        /// <summary>
        /// The change selected series.
        /// </summary>
        /// <param name="seriesName">
        /// The series name. 
        /// </param>
        public static void ChangeSelectedSeries(string seriesName)
        {
            NewSeriesModel.SelectedSeries = seriesName.GetNumber();
            InvokeSeriesChanged(new EventArgs());
        }

        /// <summary>
        ///   The generate series.
        /// </summary>
        /// <returns> The <see cref="Series" /> . </returns>
        public static Series GenerateSeries()
        {
            var series = new Series
                {
                   SeriesName = NewSeriesModel.NewSeriesName, Seasons = new ThreadedBindingList<Season>() 
                };

            foreach (var seasonText in NewSeriesModel.SeriesList)
            {
                var season = new Season { SeasonNumber = seasonText.GetNumber() };

                var seasonFiles = GetSeasonFiles(seasonText.GetNumber());

                foreach (var file in seasonFiles)
                {
                    var filePath = new MediaModel { PathAndFilename = file.FilePath };

                    season.Episodes.Add(
                        new Episode
                            {
                                FilePath = filePath, 
                                EpisodeNumber = file.EpisodeNumber, 
                                EpisodeName = Path.GetFileName(file.FilePath)
                            });
                }

                series.Seasons.Add(season);
            }

            return series;
        }

        /// <summary>
        /// The get season files.
        /// </summary>
        /// <param name="selectedSeasonNumber">
        /// The selected season number. 
        /// </param>
        /// <returns>
        /// The <see cref="ThreadedBindingList"/> . 
        /// </returns>
        public static ThreadedBindingList<AddCustomSeriesFilesModel> GetSeasonFiles(int? selectedSeasonNumber = null)
        {
            var seasonNumber = NewSeriesModel.SelectedSeries;

            if (selectedSeasonNumber != null)
            {
                seasonNumber = (int)selectedSeasonNumber;
            }

            if (!NewSeriesModel.Files.ContainsKey(seasonNumber))
            {
                NewSeriesModel.Files.Add(seasonNumber, new ThreadedBindingList<AddCustomSeriesFilesModel>());
            }

            return NewSeriesModel.Files[seasonNumber];
        }

        /// <summary>
        /// Invokes the series changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public static void InvokeSeriesChanged(EventArgs e)
        {
            EventHandler handler = SeriesChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Invokes the update series list.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public static void InvokeUpdateSeriesList(EventArgs e)
        {
            EventHandler handler = UpdateSeriesList;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        ///   The reset.
        /// </summary>
        public static void Reset()
        {
            NewSeriesModel = new AddCustomSeriesModel();
        }

        #endregion
    }
}