// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Exporting.cs">
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
//   The exporting.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Exporting
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Xml;

    using Antlr4.StringTemplate;

    using YANFOE.Factories;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.UI;
    using YANFOE.Tools.Xml;
    using YANFOE.UI.Popups;

    #endregion

    /// <summary>
    /// The exporting.
    /// </summary>
    internal class Exporting
    {
        #region Public Methods and Operators

        /// <summary>
        /// The export missing episodes template.
        /// </summary>
        /// <param name="tName">
        /// The t name.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ExportMissingEpisodesTemplate(string tName, string path = "")
        {
            var files = GetExportTemplates();
            var exportTemplate = (from s in files where s.name == tName select s).Single();
            var template = new Template(File.ReadAllText(exportTemplate.file));

            var seriesList = new List<Series>();
            foreach (var series in TVDBFactory.Instance.TVDatabase)
            {
                if (series.CountMissingEpisodes() > 0)
                {
                    var s = series.Clone();
                    s.Seasons.Clear();
                    seriesList.Add(s);

                    var episodes = series.GetMissingEpisodes();
                    foreach (var episode in episodes)
                    {
                        if (s.Seasons.All(x => x.SeasonNumber != episode.SeasonNumber))
                        {
                            var season = episode.GetSeason().Clone();
                            season.Episodes.Clear();
                            s.Seasons.Add(season);
                        }

                        var ep = episode.Clone();

                        s.Seasons[episode.GetSeason().SeasonNumber].Episodes.Add(ep);
                    }
                }
            }

            template.Add("series", seriesList);

            string ext = string.Empty;
            if (path == string.Empty)
            {
                var form = new WndSimpleBrowse();
                form.ShowDialog();
                if (form.DialogResult == true)
                {
                    path = form.getInput();
                }
                else
                {
                    return false;
                }
            }

            ext = Path.GetExtension(path);
            if (ext == string.Empty)
            {
                ext = exportTemplate.outputformat;
            }

            File.WriteAllText(Path.Combine(path, "MissingEpisodesList." + ext), template.Render());

            return true;
        }

        /// <summary>
        /// The export missing tv show episodes.
        /// </summary>
        public static void ExportMissingTvShowEpisodes()
        {
            var missingEpisodes = new List<EpisodeTreeList>();
            int i = 1;
            foreach (var series in TVDBFactory.Instance.TVDatabase)
            {
                if (series.CountMissingEpisodes() > 0)
                {
                    var show = new EpisodeTreeList { id = i++, seriesname = series.SeriesName, parent = 0 };
                    missingEpisodes.Add(show);
                    var episodes = series.GetMissingEpisodes();
                    foreach (var episode in episodes)
                    {
                        var ep = new EpisodeTreeList
                            {
                                id = i++, 
                                parent = show.id, 
                                episodename = episode.EpisodeName, 
                                seriesname =
                                    string.Format("{0}x{1:00}", episode.GetSeason().SeasonNumber, episode.EpisodeNumber)
                            };
                        missingEpisodes.Add(ep);

                        show.missingEpisodesCount++;
                    }
                }
            }

            if (missingEpisodes.Count == 0)
            {
                MessageBox.Show("No missing episodes found", "No result");
            }
            else
            {
                // ExportMissingEpisodes form = new ExportMissingEpisodes(missingEpisodes);
                // form.Show();
            }
        }

        /// <summary>
        /// The export movie list.
        /// </summary>
        public static void ExportMovieList()
        {
            ThreadedBindingList<MovieModel> movies;
            var list = new List<MovieTreeList>();

            if (MovieDBFactory.Instance.IsMultiSelected)
            {
                movies = MovieDBFactory.Instance.MultiSelectedMovies;
            }
            else
            {
                movies = MovieDBFactory.Instance.MovieDatabase;
            }

            int i = 1;
            foreach (var movie in movies)
            {
                var m = new MovieTreeList { id = i++, name = movie.Title, year = movie.Year };
                list.Add(m);
            }

            if (list.Count == 0)
            {
                MessageBox.Show("No movies found", "No result");
            }
            else
            {
                // ExportMovieList form = new ExportMovieList(list);
                // form.Show();
            }
        }

        /// <summary>
        /// The export movies template.
        /// </summary>
        /// <param name="tName">
        /// The t name.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ExportMoviesTemplate(string tName, string path = "")
        {
            var files = GetExportTemplates();
            var exportTemplate = (from s in files where s.name == tName select s).Single();
            string str = File.ReadAllText(exportTemplate.file);
            var template = new Template(str);

            ThreadedBindingList<MovieModel> movies;
            var list = new List<MovieTreeList>();

            if (MovieDBFactory.Instance.IsMultiSelected)
            {
                movies = MovieDBFactory.Instance.MultiSelectedMovies;
            }
            else
            {
                movies = MovieDBFactory.Instance.MovieDatabase;
            }

            template.Add("movies", movies);

            string ext = string.Empty;
            if (path == string.Empty)
            {
                var form = new WndSimpleBrowse();
                form.ShowDialog();
                if (form.DialogResult == true)
                {
                    path = form.getInput();
                }
                else
                {
                    return false;
                }
            }

            ext = Path.GetExtension(path);
            if (ext == string.Empty)
            {
                ext = exportTemplate.outputformat;
            }

            File.WriteAllText(Path.Combine(path, "MovieList." + ext), template.Render());

            return true;
        }

        /// <summary>
        /// The export tv show list.
        /// </summary>
        public static void ExportTvShowList()
        {
            var list = new List<EpisodeTreeList>();
            int i = 1;
            foreach (var series in TVDBFactory.Instance.TVDatabase)
            {
                var show = new EpisodeTreeList { id = i++, seriesname = series.SeriesName, parent = 0 };
                list.Add(show);
                var episodes = series.GetMissingEpisodes();
                foreach (var episode in episodes)
                {
                    if (string.IsNullOrEmpty(episode.FilePath.PathAndFilename))
                    {
                        continue;
                    }

                    var ep = new EpisodeTreeList
                        {
                            id = i++, 
                            parent = show.id, 
                            episodename = episode.EpisodeName, 
                            seriesname =
                                string.Format("{0}x{1:00}", episode.GetSeason().SeasonNumber, episode.EpisodeNumber)
                        };
                    list.Add(ep);

                    show.missingEpisodesCount++;
                }
            }

            if (list.Count == 0)
            {
                MessageBox.Show("No movies found", "No result");
            }
            else
            {
                // ExportMissingEpisodes form = new ExportMissingEpisodes(list, "Episode List ({0})");
                // form.Show();
            }
        }

        /// <summary>
        /// The get export templates.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public static List<ExportTemplate> GetExportTemplates(string type = "")
        {
            var tempInfo = Directory.GetFiles(
                Path.Combine(Environment.CurrentDirectory, "Templates"), "template.xml", SearchOption.AllDirectories);
            var templates = new List<ExportTemplate>();

            foreach (var file in tempInfo)
            {
                XmlDocument xmlReader = XRead.OpenPath(file);
                if (xmlReader == null)
                {
                    MessageBox.Show(
                        string.Format("The template configuration file '{0}' is invalid!", Path.GetFileName(file)), 
                        "Invalid Export Template");
                }

                var templateName = XRead.GetString(xmlReader, "name", 1);

                try
                {
                    var author = new ExportTemplate.Author();

                    var xmlAuthor = xmlReader.GetElementsByTagName("author")[0].InnerXml;
                    XmlDocument temp = XRead.OpenXml("<x>" + xmlAuthor + "</x>");
                    if (type != string.Empty)
                    {
                        if (XRead.GetString(xmlReader, "type") != type)
                        {
                            continue;
                        }
                    }

                    templates.Add(
                        new ExportTemplate
                            {
                                name = templateName, 
                                date = XRead.GetString(xmlReader, "date"), 
                                file =
                                    Path.Combine(Path.GetDirectoryName(file), XRead.GetString(xmlReader, "templatefile")), 
                                description = XRead.GetString(xmlReader, "description"), 
                                outputformat = XRead.GetString(xmlReader, "outputformat"), 
                                type = XRead.GetString(xmlReader, "type"), 
                                author =
                                    new ExportTemplate.Author
                                        {
                                            name = XRead.GetString(temp, "name"), 
                                            website = XRead.GetString(temp, "website"), 
                                            email = XRead.GetString(temp, "email")
                                        }
                            });
                }
                catch (Exception e)
                {
                    if (e is XmlException || e is InvalidOperationException)
                    {
                        MessageBox.Show(
                            string.Format("The template '{0}' is invalid!", templateName), "Invalid Export Template");
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            return templates;
        }

        #endregion

        /// <summary>
        /// The export template.
        /// </summary>
        public class ExportTemplate
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the author.
            /// </summary>
            public Author author { get; set; }

            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            public string date { get; set; }

            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// Gets or sets the file.
            /// </summary>
            public string file { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// Gets or sets the outputformat.
            /// </summary>
            public string outputformat { get; set; }

            /// <summary>
            /// Gets or sets the type.
            /// </summary>
            public string type { get; set; }

            #endregion

            /// <summary>
            /// The author.
            /// </summary>
            public class Author
            {
                #region Public Properties

                /// <summary>
                /// Gets or sets the email.
                /// </summary>
                public string email { get; set; }

                /// <summary>
                /// Gets or sets the name.
                /// </summary>
                public string name { get; set; }

                /// <summary>
                /// Gets or sets the website.
                /// </summary>
                public string website { get; set; }

                #endregion
            }
        }
    }
}