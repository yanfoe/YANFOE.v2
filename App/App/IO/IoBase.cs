// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoBase.cs" company="The YANFOE Project">
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

namespace YANFOE.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    using BitFactory.Logging;

    using YANFOE.Factories;
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Factories.Sets;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Models.IOModels;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.SetsModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;
    using YANFOE.Settings.UserSettings.ScraperSettings;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.Restructure;
    using YANFOE.Tools.Text;

    /// <summary>
    /// Generic IO Handler methods
    /// </summary>
    public abstract class IoBase
    {
        #region Public Methods

        public NFOType Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the IO handler.
        /// </summary>
        /// <value>
        /// The name of the IO handler.
        /// </value>
        public string IOHandlerName { get; set; }

        /// <summary>
        /// Gets or sets the IO handler description.
        /// </summary>
        /// <value>
        /// The IO handler description.
        /// </value>
        public string IOHandlerDescription { get; set; }

        /// <summary>
        /// Gets or sets the IO handler URI.
        /// </summary>
        /// <value>
        /// The IO handler URI.
        /// </value>
        public Uri IOHandlerUri { get; set; }

        /// <summary>
        /// Copy file.
        /// </summary>
        /// <param name="pathFrom">
        /// The path from.
        /// </param>
        /// <param name="pathTo">
        /// The path to.
        /// </param>
        public void CopyFile(string pathFrom, string pathTo)
        {
            if (pathFrom != pathTo)
            {
                try
                {
                    File.Copy(pathFrom, pathTo, true);
                }
                catch (Exception exception)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
                }
            }
        }

        public string GetEpisodeImage(Episode episode, string stringFormat)
        {
            if (string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
            {
                return string.Empty;
            }

            return this.GetImageWithParse(episode.FilePath.FolderPath, episode.FilePath.FilenameWithOutExt, stringFormat);
        }

        public string GetSeasonImage(Season season, string stringFormat)
        {
            string firstEpisode = season.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            var path = Path.GetDirectoryName(firstEpisode);
            var fileName = Path.GetFileNameWithoutExtension(firstEpisode);

            return this.GetImageWithParse(path, fileName, stringFormat);
        }

        public string GetSeriesImage(Series series, string stringFormat)
        {
            string firstEpisode = series.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            var path = Path.GetDirectoryName(firstEpisode);
            var fileName = series.SeriesName.Trim();

            return this.GetImageWithParse(path, fileName, stringFormat);
        }

        private string GetImageWithParse(string path, string fileName, string stringFormat)
        {
            string hex = FileSystemCharChange.To(
                fileName,
                FileSystemCharChange.ConvertArea.Tv,
                FileSystemCharChange.ConvertType.Hex);

            string schar = FileSystemCharChange.To(
                fileName, FileSystemCharChange.ConvertArea.Tv, FileSystemCharChange.ConvertType.Char);

            string seriesNamePlain = string.Format(stringFormat, fileName);
            string seriesNameHex = string.Format(stringFormat, hex);
            string seriesNameChar = string.Format(stringFormat, schar);

            string checkPathPlain = Path.Combine(path, seriesNamePlain);
            string checkPathHex = Path.Combine(path, seriesNameHex);
            string checkPathChar = Path.Combine(path, seriesNameChar);

            if (File.Exists(checkPathPlain))
            {
                return checkPathPlain;
            }
            else if (File.Exists(checkPathHex))
            {
                return checkPathHex;
            }
            else if (File.Exists(checkPathChar))
            {
                return checkPathChar;
            }

            return string.Empty;
        }

        /// <summary>
        /// Do series replace.
        /// </summary>
        /// <param name="series">
        /// The series.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The do series replace.
        /// </returns>
        public string DoSeriesReplace(Series series, string value)
        {
            string firstEpisodeFileNamePath = series.GetFirstEpisode();
            string firstEpisodePath = Path.GetDirectoryName(firstEpisodeFileNamePath);
            string firstEpisodeFileNameNoExt = Path.GetFileNameWithoutExtension(firstEpisodeFileNamePath);

            string seriesName = FileSystemCharChange.To(series.SeriesName, FileSystemCharChange.ConvertArea.Tv);

            value = value.Replace("<firstEpisodePath>", firstEpisodePath);
            value = value.Replace("<seriesName>", seriesName);
            value = value.Replace(
                "<firstEpisodeOfSeason>", firstEpisodePath + Path.DirectorySeparatorChar + firstEpisodeFileNameNoExt);

            return value;
        }

        /// <summary>
        /// Get folder name.
        /// </summary>
        /// <param name="path">
        /// The path to extract folder name
        /// </param>
        /// <returns>
        /// The get folder name.
        /// </returns>
        public string GetFolderName(string path)
        {
            string[] folders = path.Split('\\');
            return folders[folders.Length - 1];
        }

        /// <summary>
        /// Get settings.
        /// </summary>
        /// <returns>
        /// XmlWriterSettings settings
        /// </returns>
        public XmlWriterSettings GetSettings()
        {
            return new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true, };
        }

        /// <summary>
        /// Process rating.
        /// </summary>
        /// <param name="rating">
        /// The rating.
        /// </param>
        /// <returns>
        /// The processed rating.
        /// </returns>
        public string ProcessRating(double? rating)
        {
            if (rating == null)
            {
                return string.Empty;
            }

            string replace = rating.ToString().Replace(".", Get.Scraper.Generic.NumberDecimalSeperator).Replace(
                ",", Get.Scraper.Generic.NumberDecimalSeperator);

            return replace;
        }

        /// <summary>
        /// Process release date.
        /// </summary>
        /// <param name="releaseDate">
        /// The release date.
        /// </param>
        /// <returns>
        /// The processed release date.
        /// </returns>
        public string ProcessReleaseDate(DateTime? releaseDate)
        {
            if (releaseDate == null)
            {
                return string.Empty;
            }

            if (releaseDate.Value.Year < 1800)
            {
                return null;
            }

            if (Get.Scraper.Generic.FormatDateTime)
            {
                return String.Format("{0:" + Get.Scraper.Generic.FormatDateTimeValue + "}", releaseDate);
            }

            return String.Format("{0:d}", releaseDate);
        }

        /// <summary>
        /// Process runtime.
        /// </summary>
        /// <param name="runtimeMins">
        /// The runtime mins.
        /// </param>
        /// <param name="runtimeAsHM">
        /// The runtime as hm.
        /// </param>
        /// <returns>
        /// The process runtime.
        /// </returns>
        public string ProcessRuntime(int? runtimeMins, string runtimeAsHM)
        {
            if (runtimeMins == null)
            {
                return string.Empty;
            }

            if (Get.Scraper.Generic.FormatRuntime == RuntimeType.Hh_MMm)
            {
                return runtimeAsHM;
            }

            return runtimeMins.ToString();
        }

        /// <summary>
        /// Saves the movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        public void SaveMovie(MovieModel movieModel)
        {
            string actualTrailerFileName = string.Empty;
            string actualTrailerFileNameExt = string.Empty;
            string actualFilePath = movieModel.AssociatedFiles.Media[0].FolderPath + "\\";
            string actualFileName = movieModel.AssociatedFiles.Media[0].FilenameWithOutExt;
            string currentTrailerUrl = movieModel.CurrentTrailerUrl;

            MovieSaveSettings movieSaveSettings = Get.InOutCollection.CurrentMovieSaveSettings;

            string nfoPath = string.IsNullOrEmpty(movieSaveSettings.NfoPath)
                                 ? actualFilePath
                                 : movieSaveSettings.NfoPath;

            var posterPathFrom = string.Empty;

            posterPathFrom = string.IsNullOrEmpty(movieModel.PosterPathOnDisk)
                             ? Downloader.ProcessDownload(
                                 movieModel.CurrentPosterImageUrl, DownloadType.Binary, Section.Movies)
                             : movieModel.PosterPathOnDisk;

            string fanartPathFrom = string.Empty;

            fanartPathFrom = string.IsNullOrEmpty(movieModel.FanartPathOnDisk)
                                 ? Downloader.ProcessDownload(
                                     movieModel.CurrentFanartImageUrl, DownloadType.Binary, Section.Movies)
                                 : movieModel.FanartPathOnDisk;

            string trailerPathFrom = Downloader.ProcessDownload(
                movieModel.CurrentTrailerUrl, DownloadType.AppleBinary, Section.Movies);

            string nfoXml = GenerateOutput.GenerateMovieOutput(movieModel);

            string nfoTemplate;
            string posterTemplate;
            string fanartTemplate;
            string trailerTemplate;
            string setPosterTemplate;
            string setFanartTemplate;

            if (MovieNaming.IsDVD(movieModel.GetBaseFilePath))
            {
                actualFilePath = MovieNaming.GetDvdPath(movieModel.GetBaseFilePath);
                actualFileName = MovieNaming.GetDvdName(movieModel.GetBaseFilePath);

                nfoTemplate = movieSaveSettings.DvdNfoNameTemplate;
                posterTemplate = movieSaveSettings.DvdPosterNameTemplate;
                fanartTemplate = movieSaveSettings.DvdFanartNameTemplate;
                trailerTemplate = movieSaveSettings.DvdTrailerNameTemplate;
                setPosterTemplate = movieSaveSettings.DvdSetPosterNameTemplate;
                setFanartTemplate = movieSaveSettings.DvdSetFanartNameTemplate;
            }
            else if (MovieNaming.IsBluRay(movieModel.GetBaseFilePath))
            {
                actualFilePath = MovieNaming.GetBluRayPath(movieModel.GetBaseFilePath);
                actualFileName = MovieNaming.GetBluRayName(movieModel.GetBaseFilePath);

                nfoTemplate = movieSaveSettings.BlurayNfoNameTemplate;
                posterTemplate = movieSaveSettings.BlurayPosterNameTemplate;
                fanartTemplate = movieSaveSettings.BlurayFanartNameTemplate;
                trailerTemplate = movieSaveSettings.BlurayTrailerNameTemplate;
                setPosterTemplate = movieSaveSettings.BluraySetPosterNameTemplate;
                setFanartTemplate = movieSaveSettings.BluraySetFanartNameTemplate;
            }
            else
            {
                nfoTemplate = movieSaveSettings.NormalNfoNameTemplate;
                posterTemplate = movieSaveSettings.NormalPosterNameTemplate;
                fanartTemplate = movieSaveSettings.NormalFanartNameTemplate;
                trailerTemplate = movieSaveSettings.NormalTrailerNameTemplate;
                setPosterTemplate = movieSaveSettings.NormalSetPosterNameTemplate;
                setFanartTemplate = movieSaveSettings.NormalSetFanartNameTemplate;
            }

            if (!string.IsNullOrEmpty(currentTrailerUrl))
            {
                actualTrailerFileName = currentTrailerUrl.Substring(
                    currentTrailerUrl.LastIndexOf('/') + 1,
                    currentTrailerUrl.LastIndexOf('.') - currentTrailerUrl.LastIndexOf('/') - 1);

                actualTrailerFileNameExt = currentTrailerUrl.Substring(currentTrailerUrl.LastIndexOf('.') + 1);
            }

            string nfoOutputName = nfoTemplate.Replace("<path>", actualFilePath).Replace("<filename>", actualFileName);

            string posterOutputName =
                posterTemplate.Replace("<path>", actualFilePath).Replace("<filename>", actualFileName).Replace(
                    "<ext>", "jpg");

            string fanartOutputName =
                fanartTemplate.Replace("<path>", actualFilePath).Replace("<filename>", actualFileName).Replace(
                    "<ext>", "jpg");

            string trailerOutputName =
                trailerTemplate.Replace("<path>", actualFilePath).Replace("<filename>", actualFileName).Replace(
                    "<trailername>", actualTrailerFileName).Replace("<ext>", actualTrailerFileNameExt);

            string setPosterOutputPath = setPosterTemplate.Replace("<path>", actualFilePath).Replace(
                "<filename>", actualFileName);

            string setFanartOutputPath = setFanartTemplate.Replace("<path>", actualFilePath).Replace(
                "<filename>", actualFileName);

            // Handle Set Images
            List<string> sets = MovieSetManager.GetSetsContainingMovie(movieModel);

            if (sets.Count > 0)
            {
                foreach (string setName in sets)
                {
                    MovieSetModel set = MovieSetManager.GetSet(setName);

                    MovieSetObjectModel setObjectModel =
                        (from s in set.Movies where s.MovieUniqueId == movieModel.MovieUniqueId select s).
                            SingleOrDefault();

                    string currentSetPosterPath = setPosterOutputPath.Replace("<setname>", FileSystemCharChange.To(setName, FileSystemCharChange.ConvertArea.Movie)).Replace(
                        "<ext>", "jpg");

                    string currentSetFanartPath = setFanartOutputPath.Replace("<setname>", FileSystemCharChange.To(setName, FileSystemCharChange.ConvertArea.Movie)).Replace(
                        "<ext>", "jpg");

                    if (setObjectModel.Order == 1)
                    {
                        if (File.Exists(set.PosterUrl))
                        {
                            File.Copy(set.PosterUrl, currentSetPosterPath, true);
                        }

                        if (File.Exists(set.FanartUrl))
                        {
                            File.Copy(set.FanartUrl, currentSetFanartPath, true);
                        }
                    }
                    else
                    {
                        if (File.Exists(set.PosterUrl) && File.Exists(currentSetPosterPath))
                        {
                            File.Delete(currentSetPosterPath);
                        }

                        if (File.Exists(set.FanartUrl) && File.Exists(currentSetFanartPath))
                        {
                            File.Delete(currentSetFanartPath);
                        }
                    }
                }
            }

            if (movieSaveSettings.IoType == MovieIOType.All || movieSaveSettings.IoType == MovieIOType.Nfo)
            {
                try
                {
                    this.WriteNFO(nfoXml, nfoOutputName);
                    movieModel.ChangedText = false;
                    Log.WriteToLog(
                        LogSeverity.Info, 0, "NFO Saved To Disk for " + movieModel.Title, nfoPath + nfoOutputName);
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, "Saving NFO Failed for " + movieModel.Title, ex.Message);
                }
            }

            if (movieSaveSettings.IoType == MovieIOType.All || movieSaveSettings.IoType == MovieIOType.Poster ||
                movieSaveSettings.IoType == MovieIOType.Images)
            {
                try
                {
                    if (!string.IsNullOrEmpty(posterPathFrom))
                    {
                        this.CopyFile(posterPathFrom, posterOutputName);
                        movieModel.ChangedPoster = false;
                        Log.WriteToLog(
                            LogSeverity.Info, 
                            0, 
                            "Poster Saved To Disk for " + movieModel.Title, 
                            posterPathFrom + " -> " + posterOutputName);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, "Saving Poster Failed for " + movieModel.Title, ex.Message);
                }
            }

            if (movieSaveSettings.IoType == MovieIOType.All || movieSaveSettings.IoType == MovieIOType.Fanart ||
                movieSaveSettings.IoType == MovieIOType.Images)
            {
                try
                {
                    if (!string.IsNullOrEmpty(fanartPathFrom))
                    {
                        this.CopyFile(fanartPathFrom, fanartOutputName);
                        movieModel.ChangedFanart = false;
                        Log.WriteToLog(
                            LogSeverity.Info, 
                            0, 
                            "Fanart Saved To Disk for " + movieModel.Title, 
                            fanartPathFrom + " -> " + fanartOutputName);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, "Saving Fanart Failed for " + movieModel.Title, ex.Message);
                }
            }

            if (movieSaveSettings.IoType == MovieIOType.All || movieSaveSettings.IoType == MovieIOType.Trailer)
            {
                try
                {
                    if (!string.IsNullOrEmpty(movieModel.CurrentTrailerUrl))
                    {
                        this.CopyFile(trailerPathFrom, trailerOutputName);
                        movieModel.ChangedTrailer = false;
                        Log.WriteToLog(
                            LogSeverity.Info,
                            0,
                            "Trailer Saved To Disk for " + movieModel.Title,
                            trailerPathFrom + " -> " + trailerOutputName);
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(LogSeverity.Error, 0, "Saving Trailer Failed for " + movieModel.Title, ex.Message);
                }
            }
        }

        /// <summary>
        /// TV path image get.
        /// </summary>
        /// <param name="path">
        /// The image path
        /// </param>
        /// <returns>
        /// The tv path image get.
        /// </returns>
        public string TvPathImageGet(string path)
        {
            return Downloader.ProcessDownload(
                path.ToLower().Contains("http://") ? path : TvDBFactory.GetImageUrl(path), 
                DownloadType.Binary, 
                Section.Tv);
        }

        /// <summary>
        /// The write nfo.
        /// </summary>
        /// <param name="xml">
        /// The xml to write
        /// </param>
        /// <param name="path">
        /// The path to write too
        /// </param>
        public void WriteNFO(string xml, string path)
        {
            IO.WriteTextToFile(path, xml);
        }

        /// <summary>
        /// The xml writer start.
        /// </summary>
        /// <param name="xmlWriter">The xml writer.</param>
        public void XmlWriterStart(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartDocument();
        }

        #endregion
    }
}