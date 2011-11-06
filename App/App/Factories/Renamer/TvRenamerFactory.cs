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

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
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
            EpisodeMultiTemplate = "e<episode2>";

            MultiEpisodeFileTemplate = "<multi>";

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
        /// Gets or sets MultiEpisodeTemplate.
        /// </summary>
        public static string EpisodeMultiTemplate { get; set; }

        /// <summary>
        /// Gets or sets MultiEpisodeFileTemplate.
        /// </summary>
        public static string MultiEpisodeFileTemplate { get; set; }

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
            renameTo = FileSystemCharChange.To(renameTo, FileSystemCharChange.ConvertArea.Tv);

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
                        MasterMediaDBFactory.ChangeTvFileName(folderPathFrom, folderPathTo);
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
                        MasterMediaDBFactory.ChangeTvFileName(folderPathFrom, folderPathTo);
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
                MasterMediaDBFactory.ChangeTvFileName(pathFrom, pathTo);
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
            string seriesName = "";

            string season1 = string.Empty;
            string season2 = string.Empty;

            string episode1 = string.Empty;
            string episode2 = string.Empty;

            string episodeName = string.Empty;

            bool doRename;
            bool dummy = false;
            List<Episode> episodesContaining = new List<Episode>();

            if (episode != null && episode.ProductionCode == "dummy")
            {
                dummy = true;
                seriesName = "Star Trek: Deep Space Nine";
                episode = new Episode
                {
                    
                    EpisodeNumber = 5,
                    EpisodeName = "Cardassians",
                    SeasonNumber = 2
                };
                Episode ep2 = new Episode
                {
                    EpisodeNumber = 6,
                    EpisodeName = "Cardassians2",
                    SeasonNumber = 2
                }; 
                Episode ep3 = new Episode
                {
                    EpisodeNumber = 7,
                    EpisodeName = "Cardassians3",
                    SeasonNumber = 2
                };

                episodesContaining.Add(episode);
                episodesContaining.Add(ep2);
                episodesContaining.Add(ep3);
            }

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

                if (seriesName == "")
                {
                    seriesName = episode.GetSeriesName();
                }
                int? seasonNumber = episode.SeasonNumber;

                if (episodesContaining.Count == 0)
                {
                    episodesContaining = GetEpisodesContainingFile(episode);
                }

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
                    string multiTemplate = Get.InOutCollection.EpisodeMultiTemplate;
                    multiTemplate = multiTemplate.Replace(EpisodeNumber1Template, "{0}");
                    multiTemplate = multiTemplate.Replace(EpisodeNumber2Template, "{0:00}");

                    foreach (Episode ep in episodesContaining)
                    {
                        if (ep.EpisodeNumber == episode.EpisodeNumber && count == 0)
                        {
                            doRename = true;
                        }

                        episode1 += string.Format(multiTemplate, ep.EpisodeNumber);
                        episode2 += string.Format(multiTemplate, ep.EpisodeNumber);

                        count++;
                    }
                    episodeName = episode.EpisodeName;
                    episodeName = episodeName.TrimEnd();
                }

            }

            string episodeTemplate = Get.InOutCollection.EpisodeNamingTemplate;

            episodeTemplate = episodeTemplate.Replace(SeriesNameTemplate, seriesName);

            episodeTemplate = episodeTemplate.Replace(SeasonNumber1Template, season1);
            episodeTemplate = episodeTemplate.Replace(SeasonNumber2Template, season2);

            if (episodesContaining.Count <= 1)
            {
                episodeTemplate = episodeTemplate.Replace(MultiEpisodeFileTemplate, EpisodeMultiTemplate);
                episodeTemplate = episodeTemplate.Replace(EpisodeNumber1Template, episode1);
                episodeTemplate = episodeTemplate.Replace(EpisodeNumber2Template, episode2);
            }
            else
            {
                if (EpisodeMultiTemplate.Contains(EpisodeNumber1Template))
                {
                    episodeTemplate = episodeTemplate.Replace(MultiEpisodeFileTemplate, episode1);
                }
                else
                {
                    episodeTemplate = episodeTemplate.Replace(MultiEpisodeFileTemplate, episode2);
                }
            }

            episodeTemplate = episodeTemplate.Replace(EpisodeNameTemplate, episodeName);

            if (episode != null)
            {
                if (doRename && !dummy)
                {
                    string newPath = DoRename(episode.FilePath.PathAndFilename, episodeTemplate);

                    if (!string.IsNullOrEmpty(newPath))
                    {
                        string pathAddition;

                        if (MovieNaming.IsBluRay(episode.FilePath.PathAndFilename))
                        {
                            pathAddition =
                                episode.FilePath.PathAndFilename.Replace(
                                    MovieNaming.GetBluRayPath(episode.FilePath.PathAndFilename) +
                                    MovieNaming.GetBluRayName(episode.FilePath.PathAndFilename), 
                                    string.Empty);

                            episode.FilePath.PathAndFilename = newPath + pathAddition;
                            DatabaseIOFactory.SetDatabaseDirty();
                        }
                        else if (MovieNaming.IsDVD(episode.FilePath.PathAndFilename))
                        {
                            pathAddition =
                                episode.FilePath.PathAndFilename.Replace(
                                    MovieNaming.GetDvdPath(episode.FilePath.PathAndFilename) +
                                    MovieNaming.GetDvdName(episode.FilePath.PathAndFilename), 
                                    string.Empty);

                            episode.FilePath.PathAndFilename = newPath + pathAddition;
                            DatabaseIOFactory.SetDatabaseDirty();
                        }
                        else
                        {
                            episode.FilePath.PathAndFilename = newPath;
                            DatabaseIOFactory.SetDatabaseDirty();
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
                    if (!string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
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
            return (from e in episode.GetSeason().Episodes
                    where e.FilePath.PathAndFilename == episode.FilePath.PathAndFilename
                    orderby e.EpisodeNumber
                    select e).ToList();
        }

        #endregion
    }
}