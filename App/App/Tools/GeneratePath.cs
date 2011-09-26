// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneratePath.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools
{
    using System.IO;
    using System.Text.RegularExpressions;

    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Importing;

    public static class GeneratePath
    {
        public static string TvSeries(Series series, string replace, string fromFile, string altFirstEpisode = null)
        {
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
                    firstEpisodeFullPath = MovieNaming.GetDvdPath(altFirstEpisode) + MovieNaming.GetDvdName(altFirstEpisode);
                }
                else if (MovieNaming.IsBluRay(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetBluRayPath(altFirstEpisode) + MovieNaming.GetBluRayName(altFirstEpisode);
                }
                else
                {
                    firstEpisodeFullPath = altFirstEpisode;

                }

                seriesName = Regex.Match(
                    Path.GetFileNameWithoutExtension(firstEpisodeFullPath),
                    "(?<seriesName>.*?)" + Settings.ConstSettings.DefaultRegex.Tv,
                    RegexOptions.IgnoreCase).Groups["seriesName"].Value.Trim();
            }
            else
            {
                firstEpisodeFullPath = series.GetFirstEpisode();

                seriesName = Tools.Restructure.FileSystemCharChange.To(series.SeriesName);
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

            replace = replace.Replace(Settings.Get.InOutCollection.TvSeriesName, seriesName);
            replace = replace.Replace(Settings.Get.InOutCollection.TvFirstEpisodePathOfSeries, firstEpisodePath);
            replace = replace.Replace(Settings.Get.InOutCollection.TvSeriesPath, series.GetSeriesPath());

            return replace + Path.GetExtension(fromFile);
        }

        public static string TvSeason(Season season, string replace, string fromFile, string altFirstEpisode = null)
        {
            string firstEpisodeFullPath;
            string seriesName;
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
                    firstEpisodeFullPath = MovieNaming.GetDvdPath(altFirstEpisode) + MovieNaming.GetDvdName(altFirstEpisode);
                    firstEpisodeOfSeasonPath = MovieNaming.GetDvdPath(altFirstEpisode);
                    firstEpisodeOfSeason = MovieNaming.GetDvdName(altFirstEpisode);
                }
                else if (MovieNaming.IsBluRay(altFirstEpisode))
                {
                    firstEpisodeFullPath = MovieNaming.GetBluRayPath(altFirstEpisode) + MovieNaming.GetBluRayName(altFirstEpisode);
                    firstEpisodeOfSeasonPath = MovieNaming.GetBluRayPath(altFirstEpisode);
                    firstEpisodeOfSeason = MovieNaming.GetBluRayName(altFirstEpisode);
                }
                else
                {
                    firstEpisodeFullPath = altFirstEpisode;
                    firstEpisodeOfSeasonPath = Path.GetDirectoryName(altFirstEpisode);
                    firstEpisodeOfSeason = Path.GetFileNameWithoutExtension(altFirstEpisode);
                }

                seriesName = Regex.Match(Path.GetFileNameWithoutExtension(firstEpisodeFullPath), "(?<seriesName>.*?)" + Settings.ConstSettings.DefaultRegex.Tv, RegexOptions.IgnoreCase).Groups["seriesName"].Value.Trim();
                
            }
            else
            {
                firstEpisodeFullPath = season.GetFirstEpisode();
                seriesName = Restructure.FileSystemCharChange.To(season.GetSeries().SeriesName);

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


            replace = replace.Replace(Settings.Get.InOutCollection.TvSeriesName, seriesName);
            replace = replace.Replace(Settings.Get.InOutCollection.TvFirstEpisodePathOfSeries, firstEpisodePath);
            replace = replace.Replace(Settings.Get.InOutCollection.TvFirstEpisodeOfSeasonPath, firstEpisodeOfSeasonPath);
            replace = replace.Replace(Settings.Get.InOutCollection.TvFirstEpisodeOfSeason, firstEpisodeOfSeason);
            replace = replace.Replace(Settings.Get.InOutCollection.TvSeasonNumber, season.SeasonNumber.ToString());
            replace = replace.Replace(Settings.Get.InOutCollection.TvSeasonNumber2, string.Format("{0:d2}", season.SeasonNumber));
            replace = replace.Replace(Settings.Get.InOutCollection.TvSeriesPath, season.GetSeries().GetSeriesPath());

            return replace + Path.GetExtension(fromFile); ;
        }

        public static string TvEpisode(Episode episode, string replace, string fromFile, string altEpisode = null)
        {
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

            replace = replace.Replace(Settings.Get.InOutCollection.TvEpisodePath, episodePath);
            replace = replace.Replace(Settings.Get.InOutCollection.TvEpisodeFileName, episodeFileName);


            return replace + Path.GetExtension(fromFile);
        }
    }
}
