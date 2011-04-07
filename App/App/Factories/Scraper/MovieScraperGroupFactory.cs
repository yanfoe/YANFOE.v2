// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieScraperGroupFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Scraper
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Xml.Serialization;

    using BitFactory.Logging;

    using DevExpress.XtraEditors;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Scrapers.Movie.Models.ScraperGroup;
    using YANFOE.Settings;
    using YANFOE.Tools.ThirdParty;

    /// <summary>
    /// Factory for all Movie Scraper Group functionality
    /// </summary>
    public static class MovieScraperGroupFactory
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="MovieScraperGroupFactory"/> class.
        /// </summary>
        static MovieScraperGroupFactory()
        {
            ScraperGroup = new BindingList<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the scraper group.
        /// </summary>
        /// <value>
        /// The scraper group.
        /// </value>
        public static BindingList<string> ScraperGroup { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deserialize xml.
        /// </summary>
        /// <param name="scraperGroupName">The scraper group name</param>
        /// <returns>
        /// A MovieScraperGroupModel object
        /// </returns>
        public static MovieScraperGroupModel DeserializeXml(string scraperGroupName)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupName + ".xml";

            try
            {
                var deserializer = new XmlSerializer(typeof(MovieScraperGroupModel));

                if (File.Exists(path))
                {
                    using (TextReader textReader = new StreamReader(path))
                    {
                        return (MovieScraperGroupModel)deserializer.Deserialize(textReader);
                    }
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
        /// Gets the scaper group model.
        /// </summary>
        /// <param name="scraperGroupName">The scraper group name.</param>
        /// <returns>The scaper group model.</returns>
        public static MovieScraperGroupModel GetScaperGroupModel(string scraperGroupName)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + scraperGroupName + ".xml";

            if (File.Exists(path))
            {
                return DeserializeXml(scraperGroupName);
            }

            Log.WriteToLog(LogSeverity.Error, 0, "Could not load scraper group", path);

            return new MovieScraperGroupModel();
        }

        /// <summary>
        /// Populates a combobox with scraper groups
        /// </summary>
        /// <param name="cmbScraperGroupList">The combobox.</param>
        /// <returns>Scraper group list</returns>
        public static BindingList<string> GetScraperGroupsOnDisk(ComboBoxEdit cmbScraperGroupList)
        {
            var list = new BindingList<string>();

            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies;

            try
            {
                cmbScraperGroupList.Properties.Items.Clear();

                string[] scraperGroupList =
                    FastDirectoryEnumerator.EnumarateFilesPathList(
                        Get.FileSystemPaths.PathDirScraperGroupsMovies, "*.xml");

                foreach (string f in scraperGroupList)
                {
                    cmbScraperGroupList.Properties.Items.Add(Path.GetFileNameWithoutExtension(f));
                    list.Add(Path.GetFileNameWithoutExtension(f));
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(
                    LogSeverity.Error, 0, "Could not load scrapers from disk", path + Environment.NewLine + ex.Message);
            }

            return list;
        }

        /// <summary>
        /// The serialize to xml.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public static void SerializeToXml(MovieScraperGroupModel movie)
        {
            string path = Get.FileSystemPaths.PathDirScraperGroupsMovies + movie.ScraperName + ".xml";

            try
            {
                var serializer = new XmlSerializer(typeof(MovieScraperGroupModel));
                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, movie);
                textWriter.Close();
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