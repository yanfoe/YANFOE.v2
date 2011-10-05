// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Exporting.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Exporting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Xml;
    using System.Windows.Forms;
    using System.ComponentModel;

    using YANFOE.UI.Popups;
    using YANFOE.Factories;
    using YANFOE.Tools.Xml;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Tools.Extentions;

    using DevExpress.XtraEditors;
    using YANFOE.Models.MovieModels;

    using Antlr4.StringTemplate;

    class Exporting
    {
        public static void ExportMissingTvShowEpisodes()
        {
            var missingEpisodes = new List<EpisodeTreeList>();
            int i = 1;
            foreach (var series in TvDBFactory.TvDatabase)
            {
                if (series.Value.CountMissingEpisodes() > 0)
                {
                    var show = new EpisodeTreeList
                    {
                        id = i++,
                        seriesname = series.Value.SeriesName,
                        parent = 0
                    };
                    missingEpisodes.Add(show);
                    var episodes = series.Value.GetMissingEpisodes();
                    foreach (var episode in episodes)
                    {
                        var ep = new EpisodeTreeList
                        {
                            id = i++,
                            parent = show.id,
                            episodename = episode.EpisodeName,
                            seriesname = string.Format("{0}x{1:00}", episode.GetSeason().SeasonNumber, episode.EpisodeNumber)
                        };
                        missingEpisodes.Add(ep);

                        show.missingEpisodesCount++;
                    }
                }
            }

            if (missingEpisodes.Count == 0)
            {
                XtraMessageBox.Show("No missing episodes found", "No result");
            }
            else
            {
                ExportMissingEpisodes form = new ExportMissingEpisodes(missingEpisodes);
                form.Show();
            }
        }

        public static void ExportMovieList()
        {
            BindingList<MovieModel> movies;
            var list = new List<MovieTreeList>();

            if (MovieDBFactory.IsMultiSelected)
            {
                movies = MovieDBFactory.MultiSelectedMovies;
            }
            else
            {
                movies = MovieDBFactory.MovieDatabase;
            }

            int i = 1;
            foreach (var movie in movies)
            {
                var m = new MovieTreeList
                {
                    id = i++,
                    name = movie.Title,
                    year = movie.Year
                };
                list.Add(m);
            }

            if (list.Count == 0)
            {
                XtraMessageBox.Show("No movies found", "No result");
            }
            else
            {
                ExportMovieList form = new ExportMovieList(list);
                form.Show();
            }
        }

        public static void ExportTvShowList()
        {
            var list = new List<EpisodeTreeList>();
            int i = 1;
            foreach (var series in TvDBFactory.TvDatabase)
            {
                    var show = new EpisodeTreeList
                    {
                        id = i++,
                        seriesname = series.Value.SeriesName,
                        parent = 0
                    };
                    list.Add(show);
                    var episodes = series.Value.GetMissingEpisodes();
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
                            seriesname = string.Format("{0}x{1:00}", episode.GetSeason().SeasonNumber, episode.EpisodeNumber)
                        };
                        list.Add(ep);

                        show.missingEpisodesCount++;
                    }
            }

            if (list.Count == 0)
            {
                XtraMessageBox.Show("No movies found", "No result");
            }
            else
            {
                ExportMissingEpisodes form = new ExportMissingEpisodes(list, "Episode List ({0})");
                form.Show();
            }
        }

        public static bool ExportMoviesTemplate(string tName, string path = "")
        {
            var files = GetExportTemplates();
            var exportTemplate = (from s in files where s.name == tName select s).Single();
            string str = File.ReadAllText(exportTemplate.file);
            var template = new Template(str);

            BindingList<MovieModel> movies;
            var list = new List<MovieTreeList>();

            if (MovieDBFactory.IsMultiSelected)
            {
                movies = MovieDBFactory.MultiSelectedMovies;
            }
            else
            {
                movies = MovieDBFactory.MovieDatabase;
            }

            template.Add("movies", movies);

            string ext = "";
            if (path == "")
            {
                var form = new SimpleBrowseForm();
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    path = form.getInput();
                }
                else
                {
                    return false;
                }
            }

            ext = Path.GetExtension(path);
            if (ext == "") ext = exportTemplate.outputformat;

            File.WriteAllText(Path.Combine(path, "MovieList." + ext), template.Render());

            return true;
        }

        public static bool ExportMissingEpisodesTemplate(string tName, string path = "")
        {
            var files = GetExportTemplates();
            var exportTemplate = (from s in files where s.name == tName select s).Single();
            var template = new Template(File.ReadAllText(exportTemplate.file).ToString());
            
            var seriesList = new List<Series>();
            foreach (var series in TvDBFactory.TvDatabase)
            {
                if (series.Value.CountMissingEpisodes() > 0)
                {
                    var s = Extensions.Clone(series.Value);
                    s.Seasons.Clear();
                    seriesList.Add(s);
                    
                    var episodes = series.Value.GetMissingEpisodes();
                    foreach (var episode in episodes)
                    {
                        
                        if (!s.Seasons.ContainsKey(episode.GetSeason().SeasonNumber))
                        {
                            var season = Extensions.Clone(episode.GetSeason());
                            season.Episodes.Clear();
                            s.Seasons.Add(season.SeasonNumber, season);
                        }

                        var ep = Extensions.Clone(episode);
                        
                        s.Seasons[episode.GetSeason().SeasonNumber].Episodes.Add(ep);
                    }
                }
            }

            template.Add("series", seriesList);

            string ext = "";
            if (path == "")
            {
                var form = new SimpleBrowseForm();
                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    path = form.getInput();
                }
                else
                {
                    return false;
                }
            }

            ext = Path.GetExtension(path);
            if (ext == "") ext = exportTemplate.outputformat;

            File.WriteAllText(Path.Combine(path, "MissingEpisodesList." + ext), template.Render());

            return true;
        }

        public static List<ExportTemplate> GetExportTemplates(string type = "")
        {
            var tempInfo = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "Templates"), "template.xml", SearchOption.AllDirectories);
            var templates = new List<ExportTemplate>();

            foreach (var file in tempInfo)
            {
                XmlDocument xmlReader = XRead.OpenPath(file);
                if (xmlReader == null)
                {
                    XtraMessageBox.Show(
                        string.Format(
                            "The template configuration file '{0}' is invalid!",
                            Path.GetFileName(file)
                        ), 
                        "Invalid Export Template"
                    );
                }
                var templateName = XRead.GetString(xmlReader, "name", 1);

                try
                {
                    var author = new ExportTemplate.Author();

                    var xmlAuthor = xmlReader.GetElementsByTagName("author")[0].InnerXml;
                    XmlDocument temp = XRead.OpenXml("<x>" + xmlAuthor + "</x>");
                    if (type != "")
                    {
                        if (XRead.GetString(xmlReader, "type") != type)
                            continue;
                    }

                    templates.Add(new ExportTemplate
                    {
                        name = templateName,
                        date = XRead.GetString(xmlReader, "date"),
                        file = Path.Combine(Path.GetDirectoryName(file), XRead.GetString(xmlReader, "templatefile")),
                        description = XRead.GetString(xmlReader, "description"),
                        outputformat = XRead.GetString(xmlReader, "outputformat"),
                        type = XRead.GetString(xmlReader, "type"),
                        author = new ExportTemplate.Author
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
                        XtraMessageBox.Show(
                            string.Format(
                                "The template '{0}' is invalid!", 
                                templateName
                            ), 
                            "Invalid Export Template"
                        );
                    }
                    else
                    {
                        throw e;
                    }
                }

            }

            return templates;
        }

        public class ExportTemplate
        {
            public string name { get; set; }
            public string date { get; set; }
            public string file { get; set; }
            public string description { get; set; }
            public string outputformat { get; set; }
            public string type { get; set; }
            public Author author { get; set; }

            public class Author
            {
                public string name { get; set; }
                public string website { get; set; }
                public string email { get; set; }
            }
        }
    }
}
