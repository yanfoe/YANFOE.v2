// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="GeneratePath.cs">
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
//   The generate path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools
{
    #region Required Namespaces

    using System.IO;
    using System.Text.RegularExpressions;

    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;
    using YANFOE.Settings.ConstSettings;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;

    #endregion

    /// <summary>
    /// The generate path.
    /// </summary>
    public static class GeneratePath
    {
        #region Public Methods and Operators

        /// <summary>
        /// The tv episode.
        /// </summary>
        /// <param name="episode">
        /// The episode.
        /// </param>
        /// <param name="replace">
        /// The replace.
        /// </param>
        /// <param name="fromFile">
        /// The from file.
        /// </param>
        /// <param name="altEpisode">
        /// The alt episode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string TvEpisode(Episode episode, string replace, string fromFile, string altEpisode = null)
        {
            if (episode == null)
            {
                episode = new Episode();
                episode.FilePath = new MediaModel { PathAndFilename = @"c:\testshow\season 1\test show.s01e01.avi" };
            }

            string episodePath;
            string episodeFileName;

            if (altEpisode == null)
            {
                altEpisode = episode.CurrentFilenameAndPath;
            }

            if (altEpisode == string.Empty)
            {
                return string.Empty;
            }

            if (MovieNaming.IsDVD(altEpisode))
            {
                episodePath = MovieNaming.GetDvdPath(altEpisode);
                episodeFileName = MovieNaming.GetDvdName(altEpisode);
            }
            else if (MovieNaming.IsBluRay(altEpisode))
            {
                episodePath = MovieNaming.GetBluRayPath(altEpisode);
                episodeFileName = MovieNaming.GetBluRayName(altEpisode);
            }
            else
            {
                episodePath = Path.GetDirectoryName(altEpisode);
                episodeFileName = Path.GetFileNameWithoutExtension(altEpisode);
            }

            replace = replace.Replace(Get.InOutCollection.TvEpisodePath, episodePath);
            replace = replace.Replace(Get.InOutCollection.TvEpisodeFileName, episodeFileName);

            return replace + Path.GetExtension(fromFile);
        }

        /// <summary>
        /// The tv season.
        /// </summary>
        /// <param name="season">
        /// The season.
        /// </param>
        /// <param name="replace">
        /// The replace.
        /// </param>
        /// <param name="fromFile">
        /// The from file.
        /// </param>
        /// <param name="altFirstEpisode">
        /// The alt first episode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string TvSeason(Season season, string replace, string fromFile, string altFirstEpisode = null)
        {
            string seriesName = "test show";
            string firstEpisodeFullPath = @"c:\test show\season 1\test show.s01e01.avi";
            bool settings = false;

            if (season == null)
            {
                season = new Season();
                season.Episodes.Add(
                    new Episode
                        {
                            FilePath = new MediaModel { PathAndFilename = @"c:\test show\season 1\test show.s01e01.avi" }
                        });
                settings = true;
            }

            string firstEpisodeOfSeasonPath;
            string firstEpisodeOfSeason;

            if (altFirstEpisode != null)
            {
                if (altFirstEpisode == string.Empty)
                {
                    return string.Empty;
                }

                if (MovieNaming.IsDVD(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetDvdPath(altFirstEpisode)
                                           + MovieNaming.GetDvdName(altFirstEpisode);
                    firstEpisodeOfSeasonPath = MovieNaming.GetDvdPath(altFirstEpisode);
                    firstEpisodeOfSeason = MovieNaming.GetDvdName(altFirstEpisode);
                }
                else if (MovieNaming.IsBluRay(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetBluRayPath(altFirstEpisode)
                                           + MovieNaming.GetBluRayName(altFirstEpisode);
                    firstEpisodeOfSeasonPath = MovieNaming.GetBluRayPath(altFirstEpisode);
                    firstEpisodeOfSeason = MovieNaming.GetBluRayName(altFirstEpisode);
                }
                else
                {
                    firstEpisodeFullPath = altFirstEpisode;
                    firstEpisodeOfSeasonPath = Path.GetDirectoryName(altFirstEpisode);
                    firstEpisodeOfSeason = Path.GetFileNameWithoutExtension(altFirstEpisode);
                }

                seriesName =
                    Regex.Match(
                        Path.GetFileNameWithoutExtension(firstEpisodeFullPath), 
                        "(?<seriesName>.*?)" + DefaultRegex.Tv, 
                        RegexOptions.IgnoreCase).Groups["seriesName"].Value.Trim();
            }
            else
            {
                if (!settings)
                {
                    firstEpisodeFullPath = season.GetFirstEpisode();
                    seriesName = FileSystemCharChange.To(
                        season.GetSeries().SeriesName, FileSystemCharChange.ConvertArea.Tv);
                }

                if (MovieNaming.IsDVD(firstEpisodeFullPath))
                {
                    firstEpisodeOfSeasonPath = MovieNaming.GetDvdPath(firstEpisodeFullPath);
                    firstEpisodeOfSeason = MovieNaming.GetDvdName(firstEpisodeFullPath);
                }
                else if (MovieNaming.IsBluRay(firstEpisodeFullPath))
                {
                    firstEpisodeOfSeasonPath = MovieNaming.GetBluRayPath(firstEpisodeFullPath);
                    firstEpisodeOfSeason = MovieNaming.GetBluRayName(firstEpisodeFullPath);
                }
                else
                {
                    firstEpisodeOfSeasonPath = Path.GetDirectoryName(firstEpisodeFullPath);
                    firstEpisodeOfSeason = Path.GetFileNameWithoutExtension(firstEpisodeFullPath);
                }
            }

            var firstEpisodePath = Path.GetDirectoryName(firstEpisodeFullPath);

            replace = replace.Replace(Get.InOutCollection.TvSeriesName, seriesName);
            replace = replace.Replace(Get.InOutCollection.TvFirstEpisodePathOfSeries, firstEpisodePath);
            replace = replace.Replace(Get.InOutCollection.TvFirstEpisodeOfSeasonPath, firstEpisodeOfSeasonPath);
            replace = replace.Replace(Get.InOutCollection.TvFirstEpisodeOfSeason, firstEpisodeOfSeason);
            replace = replace.Replace(Get.InOutCollection.TvSeasonNumber, season.SeasonNumber.ToString());
            replace = replace.Replace(Get.InOutCollection.TvSeasonNumber2, string.Format("{0:d2}", season.SeasonNumber));

            if (settings)
            {
                replace = replace.Replace(Get.InOutCollection.TvSeriesPath, @"c:\testshow\season 1\");
            }
            else
            {
                replace = replace.Replace(Get.InOutCollection.TvSeriesPath, season.GetSeries().GetSeriesPath());
            }

            return replace + Path.GetExtension(fromFile);
            
        }

        /// <summary>
        /// The tv series.
        /// </summary>
        /// <param name="series">
        /// The series.
        /// </param>
        /// <param name="replace">
        /// The replace.
        /// </param>
        /// <param name="fromFile">
        /// The from file.
        /// </param>
        /// <param name="altFirstEpisode">
        /// The alt first episode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string TvSeries(Series series, string replace, string fromFile, string altFirstEpisode = null)
        {
            if (series == null)
            {
                series = new Series();
                series.SeriesName = "Test series";
                series.Seasons.Add(new Season());
                series.Seasons[1].Episodes.Add(
                    new Episode
                        {
                           FilePath = new MediaModel { PathAndFilename = @"c:\testshow\season 1\test show.s01e01.avi" } 
                        });
            }

            string firstEpisodeFullPath;
            string seriesName;

            if (altFirstEpisode != null)
            {
                if (altFirstEpisode == string.Empty)
                {
                    return string.Empty;
                }

                if (MovieNaming.IsDVD(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetDvdPath(altFirstEpisode)
                                           + MovieNaming.GetDvdName(altFirstEpisode);
                }
                else if (MovieNaming.IsBluRay(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetBluRayPath(altFirstEpisode)
                                           + MovieNaming.GetBluRayName(altFirstEpisode);
                }
                else
                {
                    firstEpisodeFullPath = altFirstEpisode;
                }

                seriesName =
                    Regex.Match(
                        Path.GetFileNameWithoutExtension(firstEpisodeFullPath), 
                        "(?<seriesName>.*?)" + DefaultRegex.Tv, 
                        RegexOptions.IgnoreCase).Groups["seriesName"].Value.Trim();
            }
            else
            {
                firstEpisodeFullPath = series.GetFirstEpisode();

                seriesName = FileSystemCharChange.To(series.SeriesName, FileSystemCharChange.ConvertArea.Tv);
            }

            string firstEpisodePath;

            if (MovieNaming.IsDVD(firstEpisodeFullPath))
            {
                firstEpisodePath = MovieNaming.GetDvdPath(firstEpisodeFullPath);
            }
            else if (MovieNaming.IsBluRay(firstEpisodeFullPath))
            {
                firstEpisodePath = MovieNaming.GetBluRayPath(firstEpisodeFullPath);
            }
            else
            {
                firstEpisodePath = Path.GetDirectoryName(firstEpisodeFullPath);
            }

            replace = replace.Replace(Get.InOutCollection.TvSeriesName, seriesName);
            replace = replace.Replace(Get.InOutCollection.TvFirstEpisodePathOfSeries, firstEpisodePath);
            replace = replace.Replace(Get.InOutCollection.TvSeriesPath, series.GetSeriesPath());

            return replace + Path.GetExtension(fromFile);
        }

        #endregion
    }
}