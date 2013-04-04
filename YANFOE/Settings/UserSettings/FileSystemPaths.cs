// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileSystemPaths.cs">
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
//   Settings related to file paths.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Globalization;
    using System.IO;

    using YANFOE.Tools.IO;

    #endregion

    /// <summary>
    ///   Settings related to file paths.
    /// </summary>
    [Serializable]
    public class FileSystemPaths
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="FileSystemPaths" /> class.
        /// </summary>
        public FileSystemPaths()
        {
            this.ConstructDefaultValues();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets PathDatabases.
        /// </summary>
        public string PathDatabases { get; set; }

        /// <summary>
        ///   Gets or sets the movies cache path.
        /// </summary>
        /// <value> The movies cache path. </value>
        public string PathDirCacheMovies { get; set; }

        /// <summary>
        ///   Gets or sets the TV cache path.
        /// </summary>
        /// <value> The TV cache path. </value>
        public string PathDirCacheTV { get; set; }

        /// <summary>
        ///   Gets or sets the TV series cache path.
        /// </summary>
        /// <value> The TV series cache path. </value>
        public string PathDirCacheTVSeries { get; set; }

        /// <summary>
        ///   Gets or sets the path for movie IO templates
        /// </summary>
        /// <value> The movie IO tempalates folder. </value>
        public string PathDirIOTemplatesMovies { get; set; }

        /// <summary>
        ///   Gets or sets the path for movie IO templates
        /// </summary>
        /// <value> The movie IO tempalates folder. </value>
        public string PathDirIOTemplatesTv { get; set; }

        /// <summary>
        ///   Gets or sets the movie scraper groups folder
        /// </summary>
        /// <value> The movie scraper groups folder. </value>
        public string PathDirScraperGroupsMovies { get; set; }

        /// <summary>
        ///   Gets or sets the tv scraper groups folder
        /// </summary>
        /// <value> The tv scraper groups folder. </value>
        public string PathDirScraperGroupsTv { get; set; }

        /// <summary>
        ///   Gets or sets the temp path.
        /// </summary>
        /// <value> The temp path. </value>
        public string PathDirTemp { get; set; }

        /// <summary>
        ///   Gets or sets the path file to the default tvdb.
        /// </summary>
        /// <value> The path file to the tv db. </value>
        public string PathFileTvdb { get; set; }

        /// <summary>
        ///   Gets or sets the path movies sets.
        /// </summary>
        /// <value> The path movies sets. </value>
        public string PathMoviesSets { get; set; }

        /// <summary>
        ///   Gets or sets PathSettings.
        /// </summary>
        public string PathSettings { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///   The construct default values.
        /// </summary>
        private void ConstructDefaultValues()
        {
            this.PathDatabases = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Databases{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);
            this.PathSettings = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Settings{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);

            this.PathMoviesSets = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Movie{1}Sets{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);

            this.PathDirCacheTV = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}TV{1}Cache{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);
            this.PathDirCacheTVSeries = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}TV{1}Cache{1}Series{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);
            this.PathDirCacheMovies = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Movie{1}Cache{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);

            this.PathDirScraperGroupsMovies = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Movie{1}ScraperGroups{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);
            this.PathDirScraperGroupsTv = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}TV{1}ScraperGroups{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);

            this.PathDirTemp = string.Format(
                CultureInfo.CurrentCulture, 
                "{0}{1}Temp{1}", 
                AppDomain.CurrentDomain.BaseDirectory, 
                Path.DirectorySeparatorChar);

            Folders.CheckExists(this.PathDatabases, true);
            Folders.CheckExists(this.PathSettings, true);
            Folders.CheckExists(this.PathFileTvdb, true);

            Folders.CheckExists(this.PathDirCacheTV, true);
            Folders.CheckExists(this.PathDirCacheTVSeries, true);
            Folders.CheckExists(this.PathDirCacheMovies, true);

            Folders.CheckExists(this.PathMoviesSets, true);

            Folders.CheckExists(this.PathDirScraperGroupsMovies, true);
            Folders.CheckExists(this.PathDirScraperGroupsTv, true);

            Folders.CheckExists(this.PathDirIOTemplatesMovies, true);
            Folders.CheckExists(this.PathDirIOTemplatesTv, true);

            Folders.CheckExists(this.PathDirTemp, true);
        }

        #endregion
    }
}