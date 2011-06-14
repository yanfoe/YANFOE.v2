// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddCustomSeriesFactory.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the AddCustomSeriesFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.AddCustomSeries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Extentions;

    public static class AddCustomSeriesFactory
    {
        public static AddCustomSeriesModel NewSeriesModel { get; set; }

        public static void Reset()
        {
            NewSeriesModel = new AddCustomSeriesModel();
        }

        public static void ChangeSelectedSeries(string seriesName)
        {
            NewSeriesModel.SelectedSeries = seriesName.GetNumber();
            InvokeSeriesChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when [update series list].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler UpdateSeriesList = delegate { };

        /// <summary>
        /// Occurs when [series changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler SeriesChanged = delegate { };

        /// <summary>
        /// Invokes the series changed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void InvokeUpdateSeriesList(EventArgs e)
        {
            EventHandler handler = UpdateSeriesList;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        public static BindingList<AddCustomSeriesFilesModel> GetSeasonFiles(int? selectedSeasonNumber = null)
        {
            var seasonNumber = NewSeriesModel.SelectedSeries;

            if (selectedSeasonNumber != null)
            {
                seasonNumber = (int)selectedSeasonNumber;
            }

            if (!NewSeriesModel.Files.ContainsKey(seasonNumber))
            {
                NewSeriesModel.Files.Add(seasonNumber, new BindingList<AddCustomSeriesFilesModel>());
            }

            return NewSeriesModel.Files[seasonNumber];
        }

        public static void AddFiles(List<string> filteredFiles)
        {
            var fileCollection = GetSeasonFiles();

            foreach (var file in filteredFiles)
            {
                var check = fileCollection.Where(f => f.FilePath == file).SingleOrDefault();

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

        public static Series GenerateSeries()
        {
            var series = new Series { SeriesName = NewSeriesModel.NewSeriesName };

            series.Seasons = new SortedList<int, Season>();

            foreach (var seasonText in NewSeriesModel.SeriesList)
            {
                var season = new Season { SeasonNumber = seasonText.GetNumber() };

                var seasonFiles = GetSeasonFiles(seasonText.GetNumber());

                foreach (var file in seasonFiles)
                {
                    var filePath = new MediaModel();

                    filePath.FileNameAndPath = file.FilePath;

                    season.Episodes.Add(new Episode {FilePath = filePath, EpisodeNumber = file.EpisodeNumber, EpisodeName = Path.GetFileName(file.FilePath) });
                }

                series.Seasons.Add(seasonText.GetNumber(), season);
            }

            return series;
        }

        public static void AddBlankToCurrentSeries()
        {
            GetSeasonFiles().Add(new AddCustomSeriesFilesModel());
        }
    }
}
