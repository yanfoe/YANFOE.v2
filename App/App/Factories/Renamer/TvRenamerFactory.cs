// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvRenamerFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Renamer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;

    /// <summary>
    /// The tv renamer factory.
    /// </summary>
    public static class TvRenamerFactory
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="TvRenamerFactory"/> class. 
        /// </summary>
        static TvRenamerFactory()
        {
            EpisodeNameTemplate = "<episodename>";
            EpisodeNumber1Template = "<episode1>";
            EpisodeNumber2Template = "<episode2>";

            SeasonNumber1Template = "<season1>";
            SeasonNumber2Template = "<season2>";
            SeriesNameTemplate = "<seriesname>";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the episode name template.
        /// </summary>
        /// <value>
        /// The episode name template.
        /// </value>
        public static string EpisodeNameTemplate { get; set; }

        /// <summary>
        /// Gets or sets EpisodeNumber1Template.
        /// </summary>
        public static string EpisodeNumber1Template { get; set; }

        /// <summary>
        /// Gets or sets EpisodeNumber2Template.
        /// </summary>
        public static string EpisodeNumber2Template { get; set; }

        /// <summary>
        /// Gets or sets the season number1 template.
        /// </summary>
        /// <value>
        /// The season number1 template.
        /// </value>
        public static string SeasonNumber1Template { get; set; }

        /// <summary>
        /// Gets or sets the season number2 template.
        /// </summary>
        /// <value>
        /// The season number2 template.
        /// </value>
        public static string SeasonNumber2Template { get; set; }

        /// <summary>
        /// Gets or sets the series name template.
        /// </summary>
        /// <value>
        /// The series name template.
        /// </value>
        public static string SeriesNameTemplate { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Process a file rename
        /// </summary>
        /// <param name="pathFrom">
        /// Path from.
        /// </param>
        /// <param name="renameTo">
        /// Rename too.
        /// </param>
        /// <returns>
        /// The remained path.
        /// </returns>
        public static string DoRename(string pathFrom, string renameTo)
        {
            renameTo = FileSystemCharChange.To(renameTo);

            var fileName = Path.GetFileNameWithoutExtension(pathFrom);
            var filePath = Path.GetDirectoryName(pathFrom);

            // Bluray))
            if (MovieNaming.IsBluRay(pathFrom))
            {
                string folderPathFrom = MovieNaming.GetBluRayPath(pathFrom) + MovieNaming.GetBluRayName(pathFrom);
                string folderPathTo = MovieNaming.GetBluRayPath(pathFrom) + renameTo;

                if (folderPathFrom != folderPathTo)
                {
                    try
                    {
                        Directory.Move(folderPathFrom, folderPathTo);
                        return folderPathTo;
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }

                return folderPathFrom;
            }

            // DVD
            if (MovieNaming.IsDVD(pathFrom))
            {
                string folderPathFrom = MovieNaming.GetDvdPath(pathFrom) + MovieNaming.GetDvdName(pathFrom);
                string folderPathTo = MovieNaming.GetDvdPath(pathFrom) + renameTo;

                if (folderPathFrom != folderPathTo)
                {
                    try
                    {
                        Directory.Move(folderPathFrom, folderPathTo);
                        return folderPathTo;
                    }
                    catch
                    {
                        return string.Empty;
                    }
                }

                return folderPathFrom;
            }


            // File
            string pathTo = Path.GetDirectoryName(pathFrom) + Path.DirectorySeparatorChar + renameTo +
                            Path.GetExtension(pathFrom);

            foreach (var subExt in Get.InOutCollection.SubtitleExtentions)
            {
                var possibleFileName = filePath + Path.DirectorySeparatorChar + fileName + "." + subExt;

                if (File.Exists(possibleFileName))
                {
                    try
                    {
                        File.Move(possibleFileName, filePath + Path.DirectorySeparatorChar + renameTo + "." + subExt);
                    }
                    catch
                    {
                        Log.WriteToLog(LogSeverity.Error, 0, "Could not rename", possibleFileName + " -> " + filePath + Path.DirectorySeparatorChar + renameTo + subExt);
                    }
                }
            }

            try
            {
                File.Move(pathFrom, pathTo);
            }
            catch
            {
                return pathFrom;
            }

            return pathTo;
        }

        /// <summary>
        /// Renames the episode.
        /// </summary>
        /// <param name="episode">
        /// The episode.
        /// </param>
        /// <returns>
        /// The rename episode.
        /// </returns>
        public static string RenameEpisode(Episode episode)
        {
            string seriesName;

            string season1 = string.Empty;
            string season2 = string.Empty;

            string episode1 = string.Empty;
            string episode2 = string.Empty;

            string episodeName = string.Empty;

            bool doRename;

            if (episode == null)
            {
                seriesName = "Star Trek: Deep Space Nine";

                season1 = "2";
                season2 = "02";
                episode1 = "5";
                episode2 = "05";

                episodeName = "Cardassians";
                doRename = false;
            }
            else
            {
                episode1 = string.Empty;
                episode2 = string.Empty;

                seriesName = episode.GetSeriesName();
                int? seasonNumber = episode.SeasonNumber;

                List<Episode> episodesContaining = GetEpisodesContainingFile(episode);

                season1 = seasonNumber.ToString();
                season2 = string.Format("{0:00}", seasonNumber);

                doRename = false;

                if (episodesContaining.Count == 1)
                {
                    episode1 = episode.EpisodeNumber.ToString();
                    episode2 = string.Format("{0:00}", episode.EpisodeNumber);
                    doRename = true;
                    episodeName = episode.EpisodeName;
                }
                else
                {
                    int count = 0;

                    foreach (Episode ep in episodesContaining)
                    {
                        if (ep.EpisodeNumber == episode.EpisodeNumber && count == 0)
                        {
                            doRename = true;
                        }

                        episode1 += ep.EpisodeNumber;
                        episode2 += string.Format("{0:00}", ep.EpisodeNumber);

                        episodeName += string.Format("{0} ", ep.EpisodeName);
                        count++;
                    }
                    episodeName = episodeName.TrimEnd();
                }

            }

            string episodeTemplate = Get.InOutCollection.EpisodeNamingTemplate;

            episodeTemplate = episodeTemplate.Replace(SeriesNameTemplate, seriesName);

            episodeTemplate = episodeTemplate.Replace(SeasonNumber1Template, season1);
            episodeTemplate = episodeTemplate.Replace(SeasonNumber2Template, season2);

            episodeTemplate = episodeTemplate.Replace(EpisodeNumber1Template, episode1);
            episodeTemplate = episodeTemplate.Replace(EpisodeNumber2Template, episode2);

            episodeTemplate = episodeTemplate.Replace(EpisodeNameTemplate, episodeName);

            if (episode != null)
            {
                if (doRename)
                {
                    string newPath = DoRename(episode.FilePath.FileNameAndPath, episodeTemplate);

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        string pathAddition;

                        if (MovieNaming.IsBluRay(episode.FilePath.FileNameAndPath))
                        {
                            pathAddition =
                                episode.FilePath.FileNameAndPath.Replace(
                                    MovieNaming.GetBluRayPath(episode.FilePath.FileNameAndPath) +
                                    MovieNaming.GetBluRayName(episode.FilePath.FileNameAndPath), 
                                    string.Empty);

                            episode.FilePath.FileNameAndPath = newPath + pathAddition;
                        }
                        else if (MovieNaming.IsDVD(episode.FilePath.FileNameAndPath))
                        {
                            pathAddition =
                                episode.FilePath.FileNameAndPath.Replace(
                                    MovieNaming.GetDvdPath(episode.FilePath.FileNameAndPath) +
                                    MovieNaming.GetDvdName(episode.FilePath.FileNameAndPath), 
                                    string.Empty);

                            episode.FilePath.FileNameAndPath = newPath + pathAddition;
                        }
                        else
                        {
                            episode.FilePath.FileNameAndPath = newPath;
                        }
                    }
                }
            }

            return episodeTemplate;
        }

        /// <summary>
        /// The rename series.
        /// </summary>
        /// <param name="series">
        /// The series.
        /// </param>
        public static void RenameSeries(Series series)
        {
            foreach (var season in series.Seasons)
            {
                foreach (Episode episode in season.Value.Episodes)
                {
                    if (!string.IsNullOrEmpty(episode.FilePath.FileNameAndPath))
                    {
                        RenameEpisode(episode);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Finds the episodes containing the file in this episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>List of epides</returns>
        private static List<Episode> GetEpisodesContainingFile(Episode episode)
        {
            return (from e in episode.GetSeason().Episodes.AsParallel().AsOrdered()
                    where e.FilePath.FileNameAndPath == episode.FilePath.FileNameAndPath
                    orderby e.EpisodeNumber
                    select e).ToList();
        }

        #endregion
    }
}