// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YAMJ.cs" company="The YANFOE Project">
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
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Xml;

    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Factories.Renamer;
    using YANFOE.Factories.Sets;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.SetsModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.IO;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.Xml;

    /// <summary>
    /// YAMJ IO Handler
    /// </summary>
    public class YAMJ : IoBase, IoInterface
    {
        #region Implemented Interfaces

        #region IoInterface

        /// <summary>
        /// Generates the movie output.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        /// <returns>
        /// Generates a Movie NFO
        /// </returns>
        public string GenerateMovieOutput(MovieModel movieModel)
        {
            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, this.GetSettings()))
                {
                    this.XmlWriterStart(xmlWriter);

                    xmlWriter.WriteStartElement("movie");

                    // Title
                    XWrite.WriteEnclosedElement(xmlWriter, "title", movieModel.Title);

                    // Year
                    XWrite.WriteEnclosedElement(xmlWriter, "year", movieModel.Year);

                    // Top 250
                    XWrite.WriteEnclosedElement(xmlWriter, "top250", movieModel.Top250);

                    // Release Date
                    XWrite.WriteEnclosedElement(
                        xmlWriter, "releasedate", this.ProcessReleaseDate(movieModel.ReleaseDate));

                    // Rating
                    XWrite.WriteEnclosedElement(xmlWriter, "rating", this.ProcessRating(movieModel.Rating));

                    // Votes
                    XWrite.WriteEnclosedElement(xmlWriter, "votes", movieModel.Votes);

                    // Plot
                    XWrite.WriteEnclosedElement(xmlWriter, "plot", movieModel.Plot);

                    // Tagline
                    XWrite.WriteEnclosedElement(xmlWriter, "tagline", movieModel.Tagline);

                    // Runtime
                    XWrite.WriteEnclosedElement(
                        xmlWriter, "runtime", this.ProcessRuntime(movieModel.Runtime, movieModel.RuntimeInHourMin));

                    // Mpaa
                    XWrite.WriteEnclosedElement(xmlWriter, "mpaa", movieModel.Mpaa);

                    // Certification
                    XWrite.WriteEnclosedElement(xmlWriter, "certification", movieModel.Certification);

                    // Watched
                    XWrite.WriteEnclosedElement(xmlWriter, "watched", movieModel.Watched);

                    // Imdb MovieUniqueId
                    string imdbid = movieModel.ImdbId;
                    if (!string.IsNullOrEmpty(imdbid))
                    {
                        imdbid = string.Format("tt{0}", imdbid);
                    }

                    XWrite.WriteEnclosedElement(xmlWriter, "id", imdbid);

                    // Tmdb MovieUniqueId
                    XWrite.WriteEnclosedElement(xmlWriter, "id", movieModel.TmdbId, "moviedb", "tmdb");

                    // Genre
                    foreach (string genre in movieModel.Genre)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "genre", genre);
                    }

                    // Credits
                    if (movieModel.Writers.Count > 0)
                    {
                        xmlWriter.WriteStartElement("credits");
                        foreach (PersonModel writer in movieModel.Writers)
                        {
                            XWrite.WriteEnclosedElement(xmlWriter, "writer", writer.Name);
                        }

                        xmlWriter.WriteEndElement();
                    }

                    // Director
                    string writerList = movieModel.WritersAsString.Replace(",", " / ");
                    XWrite.WriteEnclosedElement(xmlWriter, "director", writerList);

                    // Company
                    XWrite.WriteEnclosedElement(xmlWriter, "company", movieModel.SetStudio);

                    // Actor
                    int count = 1;
                    foreach (PersonModel actor in movieModel.Cast)
                    {
                        count++;

                        xmlWriter.WriteStartElement("actor");

                        XWrite.WriteEnclosedElement(xmlWriter, "name", actor.Name);
                        XWrite.WriteEnclosedElement(xmlWriter, "role", actor.Role);
                        XWrite.WriteEnclosedElement(xmlWriter, "thumb", actor.ImageUrl);

                        xmlWriter.WriteEndElement();

                        if (count == 10)
                        {
                            break;
                        }
                    }

                    // Sets
                    List<SetReturnModel> sets = MovieSetManager.GetSetReturnList(movieModel);
                    if (sets.Count > 0)
                    {
                        xmlWriter.WriteStartElement("movie");

                        foreach (SetReturnModel set in sets)
                        {
                            if (set.Order == null)
                            {
                                XWrite.WriteEnclosedElement(xmlWriter, "set", set.SetName);
                            }
                            else
                            {
                                XWrite.WriteEnclosedElement(
                                    xmlWriter, "set", set.SetName, "order", set.Order.ToString());
                            }
                        }

                        xmlWriter.WriteEndElement();
                    }

                    XWrite.WriteEnclosedElement(xmlWriter, "videosource", movieModel.VideoSource);

                    xmlWriter.WriteEndElement();
                }

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Generates the series output.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>
        /// Generates a XML output
        /// </returns>
        public string GenerateSeriesOutput(Series series)
        {
            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, this.GetSettings()))
                {
                    this.XmlWriterStart(xmlWriter);

                    xmlWriter.WriteStartElement("tvshow");

                    // Id
                    XWrite.WriteEnclosedElement(xmlWriter, "id", series.SeriesID);

                    // Title
                    XWrite.WriteEnclosedElement(xmlWriter, "title", series.SeriesName);

                    // Rating
                    XWrite.WriteEnclosedElement(xmlWriter, "rating", series.Rating);

                    // Plot
                    XWrite.WriteEnclosedElement(xmlWriter, "plot", series.Overview);

                    // Certification
                    XWrite.WriteEnclosedElement(xmlWriter, "certification", series.ContentRating);

                    // Genre
                    foreach (string genre in series.Genre)
                    {
                        XWrite.WriteEnclosedElement(xmlWriter, "genre", genre);
                    }

                    // Premiered
                    if (series.FirstAired != null)
                    {
                        XWrite.WriteEnclosedElement(
                            xmlWriter, "premiered", series.FirstAired.Value.ToString("yyyy-MM-dd"));
                    }

                    // Company
                    XWrite.WriteEnclosedElement(xmlWriter, "company", series.Network);

                    // Country
                    XWrite.WriteEnclosedElement(xmlWriter, "country", series.Country);

                    // Actor
                    foreach (PersonModel actor in series.Actors)
                    {
                        xmlWriter.WriteStartElement("actor");
                        XWrite.WriteEnclosedElement(xmlWriter, "name", actor.Name);
                        XWrite.WriteEnclosedElement(xmlWriter, "role", actor.Role);
                        XWrite.WriteEnclosedElement(xmlWriter, "thumb", actor.ImageUrl);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Generates the single episode output.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="writeDocumentTags">if set to <c>true</c> [write document tags].</param>
        /// <returns>
        /// Episode Output
        /// </returns>
        public string GenerateSingleEpisodeOutput(Episode episode, bool writeDocumentTags)
        {
            using (var stringWriter = new StringWriterWithEncoding(Encoding.UTF8))
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, this.GetSettings()))
                {
                    if (writeDocumentTags)
                    {
                        this.XmlWriterStart(xmlWriter);
                    }

                    xmlWriter.WriteStartElement("episodedetails");

                    // Season
                    XWrite.WriteEnclosedElement(xmlWriter, "season", episode.SeasonNumber);

                    // Episode
                    XWrite.WriteEnclosedElement(xmlWriter, "episode", episode.EpisodeNumber);

                    // Title
                    XWrite.WriteEnclosedElement(xmlWriter, "title", episode.EpisodeName);

                    // Plot
                    XWrite.WriteEnclosedElement(xmlWriter, "plot", episode.Overview);

                    xmlWriter.WriteEndElement();

                    if (writeDocumentTags)
                    {
                        xmlWriter.WriteEndDocument();
                    }
                }

                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Gets the episode NFO.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>
        /// Episode NFO path
        /// </returns>
        public string GetEpisodeNFO(Episode episode)
        {
            string fullPath = episode.FilePath.FileNameAndPath;

            string path = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the episode screenshot.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>
        /// Episode Screenshot path
        /// </returns>
        public string GetEpisodeScreenshot(Episode episode)
        {
            string fullPath = episode.FilePath.FileNameAndPath;

            string path = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".videoimage.jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the season banner.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>
        /// Season banner path
        /// </returns>
        public string GetSeasonBanner(Season season)
        {
            string firstEpisode = season.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);
            string fileName = Path.GetFileNameWithoutExtension(firstEpisode);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".banner.jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the season fanart.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>
        /// Season fanart path
        /// </returns>
        public string GetSeasonFanart(Season season)
        {
            string firstEpisode = season.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);
            string fileName = Path.GetFileNameWithoutExtension(firstEpisode);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".fanart.jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the season poster.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>
        /// Season Poster path
        /// </returns>
        public string GetSeasonPoster(Season season)
        {
            string firstEpisode = season.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);
            string fileName = Path.GetFileNameWithoutExtension(firstEpisode);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the series banner.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>
        /// Series Banner path
        /// </returns>
        public string GetSeriesBanner(Series series)
        {
            string firstEpisode = series.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);
            string seriesName = string.Format("Set_{0}_1", series.SeriesName.Trim());

            string checkPath = path + Path.DirectorySeparatorChar + seriesName + ".banner.jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the series fanart.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>
        /// Series Fanart path
        /// </returns>
        public string GetSeriesFanart(Series series)
        {
            string firstEpisode = series.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);

            string seriesName = string.Format("Set_{0}_1", series.SeriesName.Trim());

            string checkPath = path + Path.DirectorySeparatorChar + seriesName + ".fanart.jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the series NFO.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>
        /// Series NFO path
        /// </returns>
        public string GetSeriesNFO(Series series)
        {
            string firstEpisode = series.GetFirstEpisode();

            string path = Path.GetDirectoryName(firstEpisode);
            string fileName = Path.GetFileNameWithoutExtension(firstEpisode);

            string checkPath = path + Path.DirectorySeparatorChar + fileName + ".nfo";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the series poster.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>
        /// Series poster path
        /// </returns>
        public string GetSeriesPoster(Series series)
        {
            string firstEpisode = series.GetFirstEpisode();

            if (string.IsNullOrEmpty(firstEpisode))
            {
                return string.Empty;
            }

            string path = Path.GetDirectoryName(firstEpisode);
            string seriesName = string.Format("Set_{0}_1", series.SeriesName.Trim());

            string checkPath = path + Path.DirectorySeparatorChar + seriesName + ".jpg";

            if (File.Exists(checkPath))
            {
                return checkPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Loads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>
        /// Episode Object
        /// </returns>
        public bool LoadEpisode(Episode episode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        public void LoadMovie(MovieModel movieModel)
        {
            XmlDocument xmlReader = XRead.OpenPath(movieModel.NfoPathOnDisk);

            if (xmlReader == null)
            {
                return;
            }

            // Ids
            XmlNodeList ids = xmlReader.GetElementsByTagName("id");
            foreach (XmlElement id in ids)
            {
                if (id.Attributes["moviedb"] == null)
                {
                    movieModel.ImdbId = id.InnerXml.Replace("tt", string.Empty);
                }
                else
                {
                    switch (id.Attributes["moviedb"].Value)
                    {
                        case "tmdb":
                            movieModel.TmdbId = id.InnerXml;
                            break;
                    }
                }
            }

            // Title
            movieModel.Title = XRead.GetString(xmlReader, "title");

            // Year
            movieModel.Year = XRead.GetInt(xmlReader, "year");

            // Release Date
            movieModel.ReleaseDate = XRead.GetDateTime(xmlReader, "releasedate");

            // Rating
            movieModel.Rating = XRead.GetDouble(xmlReader, "rating");

            // Votes
            movieModel.Votes = XRead.GetInt(xmlReader, "votes");

            // Plot
            movieModel.Plot = XRead.GetString(xmlReader, "plot");

            // Outline
            movieModel.Outline = XRead.GetString(xmlReader, "outline");

            // Tagline
            movieModel.Tagline = XRead.GetString(xmlReader, "tagline");

            // Runtime
            string check = XRead.GetString(xmlReader, "runtime");
            if (check.Contains("m"))
            {
                movieModel.RuntimeInHourMin = check;
            }
            else
            {
                movieModel.Runtime = XRead.GetInt(xmlReader, "runtime");
            }

            // Mpaa 
            movieModel.Mpaa = XRead.GetString(xmlReader, "mpaa");

            // Certification
            movieModel.Certification = XRead.GetString(xmlReader, "certification");

            // Company
            movieModel.SetStudio = XRead.GetString(xmlReader, "company");
            if (movieModel.SetStudio == string.Empty)
            {
                movieModel.SetStudio = XRead.GetString(xmlReader, "studio");
            }

            // Genre
            string genreList = XRead.GetString(xmlReader, "genre");
            movieModel.GenreAsString = genreList.Replace(" / ", ",");

            // Credits
            movieModel.Writers.Clear();
            XmlNodeList writers = xmlReader.GetElementsByTagName("writer");
            foreach (XmlElement writer in writers)
            {
                movieModel.Writers.Add(new PersonModel(writer.InnerXml));
            }

            // Director
            string directorList = XRead.GetString(xmlReader, "director");
            movieModel.DirectorAsString = directorList.Replace(" / ", ",");

            // Country
            movieModel.CountryAsString = XRead.GetString(xmlReader, "country");

            // Language
            movieModel.LanguageAsString = XRead.GetString(xmlReader, "language");

            // Actors
            XmlNodeList actors = xmlReader.GetElementsByTagName("actor");
            movieModel.Cast.Clear();

            foreach (XmlElement actor in actors)
            {
                XmlDocument document = XRead.OpenXml("<x>" + actor.InnerXml + "</x>");

                string name = XRead.GetString(document, "name");
                string role = XRead.GetString(document, "role");
                string thumb = XRead.GetString(document, "thumb");

                movieModel.Cast.Add(new PersonModel(name, thumb, role));
            }

            movieModel.VideoSource = XRead.GetString(xmlReader, "videosource");

            // Watched
            movieModel.Watched = XRead.GetBool(xmlReader, "watched");

            // Sets
            XmlNodeList sets = xmlReader.GetElementsByTagName("set");

            foreach (XmlElement set in sets)
            {
                if (set.HasAttribute("order"))
                {
                    int order = int.Parse(set.GetAttribute("order"));

                    MovieSetManager.AddMovieToSet(movieModel, set.InnerXml, order);
                }
                else
                {
                    MovieSetManager.AddMovieToSet(movieModel, set.InnerXml);
                }
            }
        }

        /// <summary>
        /// Loads the season.
        /// </summary>
        /// <param name="season">
        /// The season.
        /// </param>
        /// <returns>
        /// Season object
        /// </returns>
        public bool LoadSeason(Season season)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the series.
        /// </summary>
        /// <param name="series">
        /// The series.
        /// </param>
        /// <returns>
        /// Loaded succeeded
        /// </returns>
        public bool LoadSeries(Series series)
        {
            string seriesName = series.GetSeriesNameOnDisk();
            string seriesPath = series.GetSeriesPath();

            if (string.IsNullOrEmpty(seriesName) || string.IsNullOrEmpty(seriesPath))
            {
                return false;
            }

            string nfo = Find.FindNFO(seriesName, seriesPath);

            if (string.IsNullOrEmpty(nfo))
            {
                return false;
            }

            XmlDocument doc = XRead.OpenPath(nfo);

            series.SeriesName = XRead.GetString(doc, "title");
            series.SeriesID = XRead.GetUInt(doc, "id");
            series.Rating = XRead.GetDouble(doc, "rating");
            series.Overview = XRead.GetString(doc, "plot");
            series.ContentRating = XRead.GetString(doc, "certification");
            series.Genre = XRead.GetStrings(doc, "genre").ToBindingList();
            series.FirstAired = XRead.GetDateTime(doc, "premiered", "yyyy-MM-dd");
            series.Network = XRead.GetString(doc, "country");

            if (doc.GetElementsByTagName("actor").Count > 0)
            {
                series.Actors = new BindingList<PersonModel>();

                foreach (XmlNode actor in doc.GetElementsByTagName("actor"))
                {
                    string xmlActor = actor.InnerXml;

                    XmlDocument docActor = XRead.OpenXml("<x>" + xmlActor + "</x>");

                    string name = XRead.GetString(docActor, "name");
                    string role = XRead.GetString(docActor, "role");
                    string imageurl = XRead.GetString(docActor, "thumb");

                    var personModel = new PersonModel(name, role, imageurl);

                    series.Actors.Add(personModel);
                }
            }

            return true;
        }

        /// <summary>
        /// Saves the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="type">The EpisodeIOType type.</param>
        public void SaveEpisode(Episode episode, EpisodeIOType type)
        {
            if (episode.Secondary)
            {
                return;
            }

            if (string.IsNullOrEmpty(episode.FilePath.FileNameAndPath))
            {
                return;
            }

            string nfoTemplate;
            string screenshotTemplate;

            if (MovieNaming.IsDVD(episode.FilePath.FileNameAndPath))
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDEpisodeNFOTemplate;
                screenshotTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDEpisodeScreenshotTemplate;
            }
            else if (MovieNaming.IsBluRay(episode.FilePath.FileNameAndPath))
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.BlurayEpisodeNFOTemplate;
                screenshotTemplate = Get.InOutCollection.CurrentTvSaveSettings.BlurayEpisodeScreenshotTemplate;
            }
            else
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.EpisodeNFOTemplate;
                screenshotTemplate = Get.InOutCollection.CurrentTvSaveSettings.EpisodeScreenshotTemplate;
            }

            // Nfo
            if (type == EpisodeIOType.All || type == EpisodeIOType.Nfo)
            {
                string nfoPathTo = GeneratePath.TvEpisode(episode, nfoTemplate);

                this.WriteNFO(this.GenerateSingleEpisodeOutput(episode, true), nfoPathTo);
                episode.ChangedText = false;
            }

            // Screenshot
            if (type == EpisodeIOType.Screenshot || type == EpisodeIOType.All)
            {
                if (!string.IsNullOrEmpty(episode.FilePath.FileNameAndPath))
                {
                    string screenshotPathFrom;

                    if (!string.IsNullOrEmpty(episode.EpisodeScreenshotPath) &&
                        File.Exists(episode.EpisodeScreenshotPath))
                    {
                        screenshotPathFrom = episode.EpisodeScreenshotPath;
                    }
                    else
                    {
                        screenshotPathFrom = this.TvPathImageGet(episode.EpisodeScreenshotUrl);
                    }

                    string screenshotPathTo = GeneratePath.TvEpisode(episode, screenshotTemplate);

                    this.CopyFile(screenshotPathFrom, screenshotPathTo + ".jpg");
                    episode.ChangedScreenshot = false;
                }
            }
        }

        /// <summary>
        /// Saves the season.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <param name="type">The SeasonIOType type.</param>
        public void SaveSeason(Season season, SeasonIOType type)
        {
            if (season.HasEpisodeWithPath())
            {
                string posterTemplate;
                string fanartTemplate;
                string bannerTemplate;

                string firstEpisodePath = season.GetFirstEpisode();

                if (MovieNaming.IsBluRay(firstEpisodePath))
                {
                    posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonPosterTemplate;
                    fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonFanartTemplate;
                    bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeasonBannerTemplate;
                }
                else if (MovieNaming.IsDVD(firstEpisodePath))
                {
                    posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonPosterTemplate;
                    fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonFanartTemplate;
                    bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeasonBannerTemplate;
                }
                else
                {
                    posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeasonPosterTemplate;
                    fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeasonFanartTemplate;
                    bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeasonBannerTemplate;
                }

                // Poster
                if (type == SeasonIOType.All || type == SeasonIOType.Poster)
                {
                    if (!string.IsNullOrEmpty(season.PosterUrl) || string.IsNullOrEmpty(season.PosterPath))
                    {
                        string posterPathFrom;

                        if (!string.IsNullOrEmpty(season.PosterPath) && File.Exists(season.PosterPath))
                        {
                            posterPathFrom = season.PosterPath;
                        }
                        else
                        {
                            posterPathFrom = this.TvPathImageGet(season.PosterUrl);
                        }

                        string posterPathTo = GeneratePath.TvSeason(season, posterTemplate);

                        this.CopyFile(posterPathFrom, posterPathTo + ".jpg");
                        season.ChangedPoster = false;
                    }
                }

                // Fanart
                if (type == SeasonIOType.All || type == SeasonIOType.Fanart)
                {
                    if (!string.IsNullOrEmpty(season.FanartUrl) || !string.IsNullOrEmpty(season.FanartPath))
                    {
                        string fanartPathFrom;

                        if (!string.IsNullOrEmpty(season.FanartPath) && File.Exists(season.FanartPath))
                        {
                            fanartPathFrom = season.FanartPath;
                        }
                        else
                        {
                            fanartPathFrom = this.TvPathImageGet(season.FanartUrl);
                        }

                        string fanartPathTo = GeneratePath.TvSeason(season, fanartTemplate);

                        this.CopyFile(fanartPathFrom, fanartPathTo + ".jpg");
                        season.ChangedFanart = false;
                    }
                }

                // Banner
                if (type == SeasonIOType.All || type == SeasonIOType.Banner)
                {
                    if (!string.IsNullOrEmpty(season.BannerUrl) || !string.IsNullOrEmpty(season.BannerPath))
                    {
                        string bannerPathFrom;

                        if (!string.IsNullOrEmpty(season.BannerPath) && File.Exists(season.BannerPath))
                        {
                            bannerPathFrom = season.BannerPath;
                        }
                        else
                        {
                            bannerPathFrom = this.TvPathImageGet(season.BannerUrl);
                        }

                        string bannerPathTo = GeneratePath.TvSeason(season, bannerTemplate);

                        this.CopyFile(bannerPathFrom, bannerPathTo + ".jpg");
                        season.ChangedBanner = false;
                    }
                }
            }
        }

        /// <summary>
        /// Saves the series.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <param name="type">The SeriesIOType type.</param>
        public void SaveSeries(Series series, SeriesIOType type)
        {
            string path = series.GetSeriesPath();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string nfoTemplate;
            string posterTemplate;
            string fanartTemplate;
            string bannerTemplate;

            string firstEpisodePath = series.GetFirstEpisode();

            if (Get.InOutCollection.RenameTV)
            {
                TvRenamerFactory.RenameSeries(series);
            }

            if (MovieNaming.IsBluRay(firstEpisodePath))
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesNfoTemplate;
                posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesPosterTemplate;
                fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesFanartTemplate;
                bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.BluraySeriesBannerTemplate;
            }
            else if (MovieNaming.IsDVD(firstEpisodePath))
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesNfoTemplate;
                posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesPosterTemplate;
                fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesFanartTemplate;
                bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.DVDSeriesBannerTemplate;
            }
            else
            {
                nfoTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeriesNfoTemplate;
                posterTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeriesPosterTemplate;
                fanartTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeriesFanartTemplate;
                bannerTemplate = Get.InOutCollection.CurrentTvSaveSettings.SeriesBannerTemplate;
            }

            if (type == SeriesIOType.All || type == SeriesIOType.Nfo)
            {
                // Nfo
                string nfoPathTo = GeneratePath.TvSeries(series, nfoTemplate);

                this.WriteNFO(this.GenerateSeriesOutput(series), nfoPathTo);
                series.ChangedText = false;
            }

            // Poster
            if (type == SeriesIOType.All || type == SeriesIOType.Images || type == SeriesIOType.Poster)
            {
                if (!string.IsNullOrEmpty(series.PosterUrl) || !string.IsNullOrEmpty(series.PosterPath))
                {
                    string posterPathFrom;

                    if (!string.IsNullOrEmpty(series.PosterPath) && File.Exists(series.PosterPath))
                    {
                        posterPathFrom = series.PosterPath;
                    }
                    else
                    {
                        posterPathFrom = this.TvPathImageGet(series.PosterUrl);
                    }

                    string posterPathTo = GeneratePath.TvSeries(series, posterTemplate);

                    this.CopyFile(posterPathFrom, posterPathTo + ".jpg");
                    series.ChangedPoster = false;
                }
            }

            // Fanart
            if (type == SeriesIOType.All || type == SeriesIOType.Images || type == SeriesIOType.Fanart)
            {
                if (!string.IsNullOrEmpty(series.FanartUrl) || !string.IsNullOrEmpty(series.FanartPath))
                {
                    string fanartPathFrom;

                    if (!string.IsNullOrEmpty(series.FanartPath) && File.Exists(series.FanartPath))
                    {
                        fanartPathFrom = series.FanartPath;
                    }
                    else
                    {
                        fanartPathFrom = this.TvPathImageGet(series.FanartUrl);
                    }

                    string fanartPathTo = GeneratePath.TvSeries(series, fanartTemplate);

                    this.CopyFile(fanartPathFrom, fanartPathTo + ".jpg");
                    series.ChangedFanart = false;
                }
            }

            // Banner
            if (type == SeriesIOType.All || type == SeriesIOType.Images || type == SeriesIOType.Banner)
            {
                if (!string.IsNullOrEmpty(series.SeriesBannerUrl) || !string.IsNullOrEmpty(series.SeriesBannerPath))
                {
                    string bannerPathFrom;

                    if (!string.IsNullOrEmpty(series.SeriesBannerPath) && File.Exists(series.SeriesBannerPath))
                    {
                        bannerPathFrom = series.SeriesBannerPath;
                    }
                    else
                    {
                        bannerPathFrom = this.TvPathImageGet(series.SeriesBannerUrl);
                    }

                    string bannerPathTo = GeneratePath.TvSeries(series, bannerTemplate);

                    this.CopyFile(bannerPathFrom, bannerPathTo + ".jpg");
                    series.ChangedBanner = false;
                }
            }
        }

        #endregion

        #endregion
    }
}