// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportTvFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Import
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using BitFactory.Logging;

    using YANFOE.Factories.Media;
    using YANFOE.Models.TvModels;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Settings;
    using YANFOE.Settings.ConstSettings;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;

    /// <summary>
    /// The factory for all Import TV functions
    /// </summary>
    public static class ImportTvFactory
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="ImportTvFactory"/> class. 
        /// </summary>
        static ImportTvFactory()
        {
            Scan = new SortedDictionary<string, ScanSeries>();

            NotCatagorized = new BindingList<ScanNotCatagorized>();

            ScanSeriesPicks = new BindingList<ScanSeriesPick>();

            SeriesNameList = new BindingList<SeriesListModel>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets NotCatagorized.
        /// </summary>
        public static BindingList<ScanNotCatagorized> NotCatagorized { get; set; }

        /// <summary>
        /// Gets or sets Scan.
        /// </summary>
        public static SortedDictionary<string, ScanSeries> Scan { get; set; }

        /// <summary>
        /// Gets or sets ScanSeriesPicks.
        /// </summary>
        public static BindingList<ScanSeriesPick> ScanSeriesPicks { get; set; }

        /// <summary>
        /// Gets or sets SeriesNameList.
        /// </summary>
        public static BindingList<SeriesListModel> SeriesNameList { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add scan result.
        /// </summary>
        /// <param name="episodeDetails">
        /// The episode details.
        /// </param>
        public static void AddScanResult(EpisodeDetails episodeDetails)
        {
            var seriesName = episodeDetails.SeriesName;

            var name = seriesName;
            var check1 = (from s in Scan where s.Key.ToLower() == name.ToLower() select s.Key).SingleOrDefault();

            // Process Series
            if (check1 == null)
            {
                Scan.Add(seriesName, new ScanSeries());

                var check2 = (from s in SeriesNameList where s.SeriesName.ToLower() == seriesName.ToLower() select s.SeriesName.ToLower()).ToList();

                if (!check2.Contains(seriesName.ToLower()) && !string.IsNullOrEmpty(seriesName))
                {
                    string dir = Directory.GetParent(episodeDetails.FilePath).Name;
                    string path = string.Empty;
                    if (dir.StartsWith("season", true, System.Globalization.CultureInfo.CurrentCulture))
                    {
                        // Looks like it. Qualified series guess
                        path = Directory.GetParent(episodeDetails.FilePath).Parent.FullName;
                    }
                    else
                    {
                        // Best guess
                        path = Directory.GetParent(episodeDetails.FilePath).FullName;
                    }
                    
                    SeriesNameList.Add(
                        new SeriesListModel { WaitingForScan = true, SeriesName = seriesName, SeriesPath = path });
                }
            }
            else
            {
                seriesName = check1;
            }

            // Process Series
            if (!Scan[seriesName].Seasons.ContainsKey(episodeDetails.SeasonNumber))
            {
                Scan[seriesName].Seasons.Add(episodeDetails.SeasonNumber, new ScanSeason());
            }

            if (
                !Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.ContainsKey(
                    episodeDetails.EpisodeNumber))
            {
                Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.Add(
                    episodeDetails.EpisodeNumber, new ScanEpisode());
            }

            Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[episodeDetails.EpisodeNumber].
                FilePath = episodeDetails.FilePath;

            if (episodeDetails.SecondaryNumbers.Count > 0)
            {
                foreach (int secondary in episodeDetails.SecondaryNumbers)
                {
                    if (
                        !Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.ContainsKey(
                            secondary))
                    {
                        Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes.Add(
                            secondary, new ScanEpisode());
                    }

                    Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].FilePath =
                        episodeDetails.FilePath;
                    Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].Secondary =
                        true;
                    Scan[seriesName].Seasons[episodeDetails.SeasonNumber].Episodes[secondary].SecondaryTo
                        = episodeDetails.EpisodeNumber;
                }
            }

            var findList =
                (from s in MediaPathDBFactory.GetMediaPathTvUnsorted()
                 where s.PathAndFileName == episodeDetails.FilePath
                 select s).ToList();

            foreach (var path in findList)
            {
                MediaPathDBFactory.GetMediaPathTvUnsorted().Remove(path);
            }
        }

        /// <summary>
        /// The do import scan.
        /// </summary>
        public static void DoImportScan()
        {
            SeriesNameList = new BindingList<SeriesListModel>();
            Scan = new SortedDictionary<string, ScanSeries>();

            // Get file list
            var filters = Get.InOutCollection.VideoExtentions;

            var paths = MediaPathDBFactory.GetMediaPathTvUnsorted();

            var filteredFiles = (from file in paths
                                 let pathAndFileName = Path.GetExtension(file.PathAndFileName).ToLower()
                                 where filters.Any(filter => pathAndFileName.ToLower() == "." + filter.ToLower())
                                 select file.PathAndFileName).ToList();

            // Process each file and add to ScanDB);
            foreach (var f in filteredFiles)
            {
                var episodeDetails = GetEpisodeDetails(f);

                if (episodeDetails.TvMatchSuccess)
                {
                    AddScanResult(episodeDetails);
                }
                else
                {
                    NotCatagorized.Add(new ScanNotCatagorized { FilePath = f });
                }
            }

            MasterMediaDBFactory.PopulateMasterTvMediaDatabase();
        }

        /// <summary>
        /// Gets the episode details.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The episode details.</returns>
        public static EpisodeDetails GetEpisodeDetails(string filePath)
        {
            string logCategory = "ImportTvFactory > GetEpisodeDetails";

            var episodeDetails = new EpisodeDetails
                {
                    FilePath = filePath
                };

            string series;

            if (MovieNaming.IsDVD(filePath))
            {
                series = MovieNaming.GetDvdName(filePath);
            }
            else if (MovieNaming.IsBluRay(filePath))
            {
                series = MovieNaming.GetBluRayName(filePath);
            }
            else
            {
                series = Path.GetFileNameWithoutExtension(filePath);
            }

            InternalApps.Logs.Log.WriteToLog(
                LogSeverity.Debug, 0, string.Format("Getting episode details for: {0}", series), logCategory);

            // Check if dir structure is <root>/<series>/<season>/<episodes>
            string dir = Directory.GetParent(filePath).Name;
            string seriesGuess = string.Empty;
            if (dir.StartsWith("season", true, System.Globalization.CultureInfo.CurrentCulture))
            {
                // Looks like it. Qualified series guess
                seriesGuess = Directory.GetParent(filePath).Parent.Name;
            }

            InternalApps.Logs.Log.WriteToLog(
                LogSeverity.Debug,
                0,
                string.Format("Qualified series name guess, based on folder: {0}", seriesGuess),
                logCategory);

            var regex = Regex.Match(
                series,
                "(?<seriesName>.*?)" + DefaultRegex.Tv,
                RegexOptions.IgnoreCase);
            
            string rawSeriesName;
            if (regex.Success)
            {
                rawSeriesName = regex.Groups["seriesName"].Value.Trim();
                InternalApps.Logs.Log.WriteToLog(
                    LogSeverity.Debug, 0, string.Format("Series name from file: {0}", rawSeriesName), logCategory);

                var result = (from r in ScanSeriesPicks where r.SearchString == rawSeriesName select r)
                    .SingleOrDefault();

                string rawSeriesGuess = rawSeriesName.Replace(new string[] { ".", "_", "-" }, " ").Trim();

                if (seriesGuess != string.Empty)
                {
                    if (rawSeriesName == string.Empty)
                    {
                        // Anything's better than nothing
                        rawSeriesName = seriesGuess;
                    }
                    else if (rawSeriesName.StartsWith(seriesGuess))
                    {
                        // Regex might've picked up some random crap between series name and s01e01
                        rawSeriesName = seriesGuess;
                    }
                    else if (rawSeriesName.Length > 0 && (rawSeriesName.Substring(0, 1) == seriesGuess.Substring(0, 1) ||
                        rawSeriesGuess.ToLower().Replace("and", "&") == seriesGuess.ToLower().Replace("and", "&")))
                    {
                        // Regex might've picked up an acronym for the series
                        // TGaaG = Two Guys and a Girl
                        if (result == null)
                        {
                            rawSeriesName = seriesGuess;
                        }
                    }
                }
                else
                {
                    rawSeriesName = rawSeriesGuess;
                }

                InternalApps.Logs.Log.WriteToLog(
                    LogSeverity.Debug, 0, string.Format("Best series guess: {0}", rawSeriesName), logCategory);

                episodeDetails.SeriesName = result != null ? result.SeriesName : rawSeriesName;

                episodeDetails.SeriesName = Tools.Restructure.FileSystemCharChange.From(episodeDetails.SeriesName, FileSystemCharChange.ConvertArea.Tv);

            }

            var seasonMatch = Regex.Match(series, DefaultRegex.TvSeason, RegexOptions.IgnoreCase);
            if (seasonMatch.Success)
            {
                episodeDetails.SeasonNumber = seasonMatch.Groups[1].Value.GetNumber();
            }

            InternalApps.Logs.Log.WriteToLog(
                LogSeverity.Debug,
                0,
                string.Format("Extracted season number: {0}", episodeDetails.SeasonNumber),
                logCategory);

            var episodeMatch = Regex.Matches(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
            if (episodeMatch.Count > 0)
            {
                episodeDetails.TvMatchSuccess = true;
                if (episodeMatch.Count > 1)
                {
                    bool first = true;
                    foreach (Match match in episodeMatch)
                    {
                        if (first)
                        {
                            episodeDetails.EpisodeNumber = match.Value.GetNumber();
                            first = false;
                        }
                        else
                        {
                            episodeDetails.SecondaryNumbers.Add(match.Value.GetNumber());
                        }
                    }

                    InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Debug,
                        0,
                        string.Format("Extracted episode numbers ({0}): {1}",episodeDetails.SecondaryNumbers.Count,string.Join(", ", episodeDetails.SecondaryNumbers)),logCategory);
                }
                else
                {
                    var episodeMatch2 = Regex.Match(series, DefaultRegex.TvEpisode, RegexOptions.IgnoreCase);
                    episodeDetails.EpisodeNumber = episodeMatch2.Groups[1].Value.GetNumber();

                    InternalApps.Logs.Log.WriteToLog(
                        LogSeverity.Debug,
                        0,
                        string.Format("Extracted episode number: {0}", episodeDetails.EpisodeNumber),
                        logCategory);
                }
            }

            episodeDetails.SeriesName = episodeDetails.SeriesName.Replace(".", string.Empty).Trim();
            if (episodeDetails.SeriesName.EndsWith("-"))
            {
                episodeDetails.SeriesName = episodeDetails.SeriesName.TrimEnd('-').Trim();
            }

            var check =
                (from s in ScanSeriesPicks where s.SearchString == episodeDetails.SeriesName select s).SingleOrDefault();

            if (check != null)
            {
                episodeDetails.SeriesName = check.SeriesName;
            }

            InternalApps.Logs.Log.WriteToLog(
                LogSeverity.Debug, 0, string.Format("Final series name: {0}", episodeDetails.SeriesName), logCategory);

            return episodeDetails;
        }

        #endregion
    }
}