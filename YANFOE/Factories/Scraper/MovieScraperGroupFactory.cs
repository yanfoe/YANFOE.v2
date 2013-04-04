// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieScraperGroupFactory.cs">
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
//   Factory for all Movie Scraper Group functionality
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Scraper
{
    #region Required Namespaces

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using BitFactory.Logging;

    using Newtonsoft.Json;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Models.ScraperGroup;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Compression;
    using YANFOE.Tools.ThirdParty;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   Factory for all Movie Scraper Group functionality
    /// </summary>
    public class MovieScraperGroupFactory : FactoryBase
    {
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Implements Singleton.")]
        public static MovieScraperGroupFactory Instance = new MovieScraperGroupFactory();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MovieScraperGroupFactory"/> class from being created. 
        ///   Initializes static members of the <see cref="MovieScraperGroupFactory"/> class.
        /// </summary>
        private MovieScraperGroupFactory()
        {
            this.ScraperGroup = new ThreadedBindingList<string>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the scraper group.
        /// </summary>
        /// <value> The scraper group. </value>
        public ThreadedBindingList<string> ScraperGroup { get; set; }

        /// <summary>
        /// Gets the scraper groups on disk.
        /// </summary>
        public ThreadedBindingList<MovieScraperGroupModel> ScraperGroupsOnDisk
        {
            get
            {
                var list = new ThreadedBindingList<MovieScraperGroupModel>();

                string path = Get.FileSystemPaths.PathDirScraperGroupsMovies;

                try
                {
                    string[] scraperGroupList =
                        FileHelper.GetFilesRecursive(Get.FileSystemPaths.PathDirScraperGroupsMovies, "*.gz").ToArray();

                    foreach (string f in scraperGroupList)
                    {
                        list.Add(this.GetScaperGroupModel(Path.GetFileNameWithoutExtension(f)));
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteToLog(
                        LogSeverity.Error, 
                        0, 
                        "Could not load scrapers from disk", 
                        path + Environment.NewLine + ex.Message);
                }

                return list;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The delete scraper group.
        /// </summary>
        /// <param name="scraperGroupModel">
        /// The scraper group model.
        /// </param>
        public void DeleteScraperGroup(MovieScraperGroupModel scraperGroupModel)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupModel.ScraperName;

            try
            {
                File.Delete(path + ".gz");
                this.OnPropertyChanged("ScraperGroupsOnDisk");
            }
            catch (Exception ex)
            {
                Log.WriteToLog(
                    LogSeverity.Error, 0, "Could not delete scraper group", path + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Deserialize xml.
        /// </summary>
        /// <param name="scraperGroupName">
        /// The scraper group name 
        /// </param>
        /// <returns>
        /// A MovieScraperGroupModel object 
        /// </returns>
        public MovieScraperGroupModel DeserializeXml(string scraperGroupName)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupName + ".gz";

            try
            {
                if (File.Exists(path))
                {
                    string json = Gzip.Decompress(path);

                    var scraperGroupModel =
                        JsonConvert.DeserializeObject(json, typeof(MovieScraperGroupModel)) as MovieScraperGroupModel;
                    scraperGroupModel.IsDirty = false;
                    return scraperGroupModel;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(
                    LogSeverity.Error, 0, "Could not load scraper group", path + Environment.NewLine + ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Gets the scraper group model.
        /// </summary>
        /// <param name="scraperGroupName">
        /// The scraper group name. 
        /// </param>
        /// <returns>
        /// The scraper group model. 
        /// </returns>
        public MovieScraperGroupModel GetScaperGroupModel(string scraperGroupName)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupName + ".gz";

            if (File.Exists(path))
            {
                return this.DeserializeXml(scraperGroupName);
            }

            Log.WriteToLog(LogSeverity.Error, 0, "Could not load scraper group", path);

            return new MovieScraperGroupModel();
        }

        /// <summary>
        /// The serialize to xml.
        /// </summary>
        /// <param name="scraperGroupModel">
        /// The scraper Group Model.
        /// </param>
        public void SerializeToXml(MovieScraperGroupModel scraperGroupModel)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupModel.ScraperName;

            try
            {
                string json = JsonConvert.SerializeObject(scraperGroupModel);
                Gzip.CompressString(json, path + ".gz");
                scraperGroupModel.IsDirty = false;
                this.OnPropertyChanged("ScraperGroupsOnDisk");
            }
            catch (Exception ex)
            {
                Log.WriteToLog(
                    LogSeverity.Error, 0, "Could not save scraper group", path + Environment.NewLine + ex.Message);
            }
        }

        #endregion
    }
}