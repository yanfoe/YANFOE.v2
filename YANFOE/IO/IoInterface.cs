// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="IoInterface.cs">
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
//   Interface for all IO handlers
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.IO
{
    #region Required Namespaces

    using System;

    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Enums;

    #endregion

    /// <summary>
    ///   Interface for all IO handlers
    /// </summary>
    public interface IOInterface
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the io handler description.
        /// </summary>
        string IOHandlerDescription { get; set; }

        /// <summary>
        /// Gets or sets the io handler name.
        /// </summary>
        string IOHandlerName { get; set; }

        /// <summary>
        /// Gets or sets the io handler uri.
        /// </summary>
        Uri IOHandlerUri { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether show in settings.
        /// </summary>
        bool ShowInSettings { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        NFOType Type { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Generates the movie output.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        /// <returns>
        /// Generates a Movie NFO 
        /// </returns>
        string GenerateMovieOutput(MovieModel movieModel);

        /// <summary>
        /// Generates the series output.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Generates a XML output 
        /// </returns>
        string GenerateSeriesOutput(Series series);

        /// <summary>
        /// Generates the single episode output.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <param name="writeDocumentTags">
        /// if set to <c>true</c> [write document tags]. 
        /// </param>
        /// <returns>
        /// Episode Output 
        /// </returns>
        string GenerateSingleEpisodeOutput(Episode episode, bool writeDocumentTags);

        /// <summary>
        /// Gets the episode NFO.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <returns>
        /// Episode NFO path 
        /// </returns>
        string GetEpisodeNFO(Episode episode);

        /// <summary>
        /// Gets the episode screenshot.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <returns>
        /// Episode Screenshot path 
        /// </returns>
        string GetEpisodeScreenshot(Episode episode);

        /// <summary>
        /// Gets the file info block
        /// </summary>
        /// <param name="movie">
        /// The movie. 
        /// </param>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetFileInfo(MovieModel movie = null, Episode episode = null);

        /// <summary>
        /// Gets the season banner.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <returns>
        /// Season banner path 
        /// </returns>
        string GetSeasonBanner(Season season);

        /// <summary>
        /// Gets the season fanart.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <returns>
        /// Season fanart path 
        /// </returns>
        string GetSeasonFanart(Season season);

        /// <summary>
        /// Gets the season poster.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <returns>
        /// Season Poster path 
        /// </returns>
        string GetSeasonPoster(Season season);

        /// <summary>
        /// Gets the series banner.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Series Banner path 
        /// </returns>
        string GetSeriesBanner(Series series);

        /// <summary>
        /// Gets the series fanart.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Series Fanart path 
        /// </returns>
        string GetSeriesFanart(Series series);

        /// <summary>
        /// Gets the series NFO.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Series NFO path 
        /// </returns>
        string GetSeriesNFO(Series series);

        /// <summary>
        /// Gets the series poster.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Series poster path 
        /// </returns>
        string GetSeriesPoster(Series series);

        /// <summary>
        /// Loads the episode.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <returns>
        /// Episode Object 
        /// </returns>
        bool LoadEpisode(Episode episode);

        /// <summary>
        /// Loads the movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model. 
        /// </param>
        void LoadMovie(MovieModel movieModel);

        /// <summary>
        /// Loads the season.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <returns>
        /// Season object 
        /// </returns>
        bool LoadSeason(Season season);

        /// <summary>
        /// Loads the series.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <returns>
        /// Loaded succeeded 
        /// </returns>
        bool LoadSeries(Series series);

        /// <summary>
        /// Saves the episode.
        /// </summary>
        /// <param name="episode">
        /// The episode. 
        /// </param>
        /// <param name="type">
        /// The EpisodeIOType type. 
        /// </param>
        void SaveEpisode(Episode episode, EpisodeIOType type);

        /// <summary>
        /// Saves the season.
        /// </summary>
        /// <param name="season">
        /// The season. 
        /// </param>
        /// <param name="type">
        /// The SeasonIOType type. 
        /// </param>
        void SaveSeason(Season season, SeasonIOType type);

        /// <summary>
        /// Saves the series.
        /// </summary>
        /// <param name="series">
        /// The series. 
        /// </param>
        /// <param name="type">
        /// The SeriesIOType type. 
        /// </param>
        void SaveSeries(Series series, SeriesIOType type);

        #endregion
    }
}