// ------------------------------------------------------------------------------------------------
// <copyright file="IoInterface.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the IoInterface interface.
// </summary>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0) 
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//
//   See this page: http://www.yanfoe.com/license
//   
//   For any reuse or distribution, you must make clear to others the 
//   license terms of this work.  
// </license>
// --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace YANFOE.IO
{
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.TvModels.Show;

    /// <summary>
    /// Interface for all IO handlers
    /// </summary>
    public interface IoInterface
    {
        /// <summary>
        /// Generates the movie output.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        /// <returns>Generates a Movie NFO</returns>
        string GenerateMovieOutput(MovieModel movieModel);

        /// <summary>
        /// Generates the series output.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Generates a XML output</returns>
        string GenerateSeriesOutput(Series series);

        /// <summary>
        /// Generates the single episode output.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="writeDocumentTags">if set to <c>true</c> [write document tags].</param>
        /// <returns>Episode Output</returns>
        string GenerateSingleEpisodeOutput(Episode episode, bool writeDocumentTags);

        /// <summary>
        /// Loads the movie.
        /// </summary>
        /// <param name="movieModel">The movie model.</param>
        void LoadMovie(MovieModel movieModel);

        /// <summary>
        /// Loads the series.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Loaded succeeded</returns>
        bool LoadSeries(Series series);

        /// <summary>
        /// Loads the season.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>Season object</returns>
        bool LoadSeason(Season season);

        /// <summary>
        /// Loads the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>Episode Object</returns>
        bool LoadEpisode(Episode episode);

        /// <summary>
        /// Saves the series.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <param name="type">The SeriesIOType type.</param>
        void SaveSeries(Series series, SeriesIOType type);

        /// <summary>
        /// Saves the season.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <param name="type">The SeasonIOType type.</param>
        void SaveSeason(Season season, SeasonIOType type);

        /// <summary>
        /// Saves the episode.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <param name="type">The EpisodeIOType type.</param>
        void SaveEpisode(Episode episode, EpisodeIOType type);

        /// <summary>
        /// Gets the series NFO.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Series NFO path</returns>
        string GetSeriesNFO(Series series);

        /// <summary>
        /// Gets the series poster.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Series poster path</returns>
        string GetSeriesPoster(Series series);

        /// <summary>
        /// Gets the series fanart.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Series Fanart path</returns>
        string GetSeriesFanart(Series series);

        /// <summary>
        /// Gets the series banner.
        /// </summary>
        /// <param name="series">The series.</param>
        /// <returns>Series Banner path</returns>
        string GetSeriesBanner(Series series);

        /// <summary>
        /// Gets the season poster.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>Season Poster path</returns>
        string GetSeasonPoster(Season season);

        /// <summary>
        /// Gets the season fanart.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>Season fanart path</returns>
        string GetSeasonFanart(Season season);

        /// <summary>
        /// Gets the season banner.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>Season banner path</returns>
        string GetSeasonBanner(Season season);

        /// <summary>
        /// Gets the episode NFO.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>Episode NFO path</returns>
        string GetEpisodeNFO(Episode episode);

        /// <summary>
        /// Gets the episode screenshot.
        /// </summary>
        /// <param name="episode">The episode.</param>
        /// <returns>Episode Screenshot path</returns>
        string GetEpisodeScreenshot(Episode episode);
    }
}
