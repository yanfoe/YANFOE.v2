// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImportMoviesFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Import
{
    using System.ComponentModel;
    using System.IO;
    using System.Linq;

    using YANFOE.Factories.Media;
    using YANFOE.Factories.Sets;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.ThirdParty;

    /// <summary>
    /// The factory for the initial movie import routines
    /// </summary>
    public static class ImportMoviesFactory
    {
        

        /// <summary>
        /// Initializes static members of the <see cref="ImportMoviesFactory"/> class. 
        /// </summary>
        static ImportMoviesFactory()
        {
            ImportDatabase = new BindingList<MovieModel>();
            CurrentRecord = new MovieModel();
        }

        private static bool cancelImport;

        /// <summary>
        /// Gets or sets the current record.
        /// </summary>
        /// <value>
        /// The current record.
        /// </value>
        public static MovieModel CurrentRecord { get; set; }

        /// <summary>
        /// Gets or sets the import database.
        /// </summary>
        /// <value>
        /// The import database.
        /// </value>
        public static BindingList<MovieModel> ImportDatabase { get; set; }

        /// <summary>
        /// Gets the import movie database.
        /// </summary>
        /// <returns>The current ImportDatabase</returns>
        public static BindingList<MovieModel> GetImportMovieDatabase()
        {
            return ImportDatabase;
        }

        public static void CancelMovieImport()
        {
            cancelImport = true;
        }

        /// <summary>
        /// Finds NFO on disk
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="path">The file path.</param>
        /// <param name="fileList">The file list.</param>
        /// <returns>Found file or string.Empty</returns>
        public static string FindNFO(string fileName, string path, string[] fileList = null)
        {
            return Tools.IO.Find.FindMovieNFO(fileName, path, fileList);
        }

        /// <summary>
        /// Finds poster on disk
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="path">The file path.</param>
        /// <param name="fileList">The file list.</param>
        /// <returns>Found file or string.Empty</returns>
        public static string FindPoster(string fileName, string path, string[] fileList = null)
        {
            return Tools.IO.Find.FindMoviePoster(fileName, path, fileList);
        }

        /// <summary>
        /// Finds fanart on disk
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="path">The file path.</param>
        /// <param name="fileList">The file list.</param>
        /// <returns>Found file or string.Empty</returns>
        public static string FindFanart(string fileName, string path, string[] fileList = null)
        {
            return Tools.IO.Find.FindMovieFanart(fileName, path, fileList);
        }

        /// <summary>
        /// Converts the media path import database into a MovieModel DB.
        /// </summary>
        public static void ConvertMediaPathImportToDB()
        {
            cancelImport = false;

            var db = MediaPathDBFactory.GetMediaPathMoviesUnsorted();

            var count = 0;

            MovieDBFactory.ImportProgressMaximum = db.Count;

            ImportDatabase.Clear();

            var getFiles = new string[1];
            var currentGetPathFiles = string.Empty;

            UI.Windows7UIFactory.StartProgressState(db.Count);

            foreach (var file in db)
            {
                if (cancelImport)
                {
                    break;
                }

                MovieDBFactory.ImportProgressCurrent = count;

                MovieDBFactory.ImportProgressStatus = string.Format("Processing: " + file.PathAndFileName);

                if (file.Path != currentGetPathFiles)
                {
                    getFiles = FileHelper.GetFilesRecursive(file.Path, "*.*").ToArray();
                    currentGetPathFiles = file.Path;
                }

                var videoSource = file.DefaultVideoSource;

                if (MovieNaming.IsBluRay(file.PathAndFileName))
                {
                    videoSource = "Bluray";
                }
                else if (MovieNaming.IsDVD(file.PathAndFileName))
                {
                    videoSource = "DVD";
                }
                else
                {
                    var detect = Tools.IO.DetectType.FindVideoSource(file.PathAndFileName);

                    if (!string.IsNullOrEmpty(detect))
                    {
                        videoSource = detect;
                    }
                }

                var movieModel = new MovieModel
                    {
                        Title = MovieNaming.GetMovieName(file.PathAndFileName, file.MediaPathType),
                        Year = MovieNaming.GetMovieYear(file.PathAndFileName),
                        ScraperGroup = file.ScraperGroup,
                        VideoSource = videoSource,
                        NfoPathOnDisk = FindNFO(file.FilenameWithOutExt, file.Path, getFiles),
                        PosterPathOnDisk = FindPoster(file.FilenameWithOutExt, file.Path, getFiles),
                        FanartPathOnDisk = FindFanart(file.FilenameWithOutExt, file.Path, getFiles)
                    };

                if (!string.IsNullOrEmpty(movieModel.NfoPathOnDisk))
                {
                    InOut.OutFactory.LoadMovie(movieModel);
                    movieModel.ChangedText = false;
                }

                var result = (from m in ImportDatabase where (m.Title.ToLower().Trim() == movieModel.Title.ToLower().Trim()) select m).ToList();

                if (result.Count == 0)
                {
                    if (!string.IsNullOrEmpty(movieModel.PosterPathOnDisk))
                    {
                        movieModel.GenerateSmallPoster(movieModel.PosterPathOnDisk);
                        movieModel.ChangedPoster = false;
                    }

                    if (!string.IsNullOrEmpty(movieModel.FanartPathOnDisk))
                    {
                        movieModel.GenerateSmallFanart(movieModel.FanartPathOnDisk);
                        movieModel.ChangedFanart = false;
                    }

                    movieModel.AssociatedFiles.AddToMediaCollection(file);
                    ImportDatabase.Add(movieModel);
                }
                else
                {
                    // result[0].AssociatedFiles.GetMediaCollection().Clear();
                    result[0].AssociatedFiles.AddToMediaCollection(file);
                }

                count++;
                UI.Windows7UIFactory.SetProgressValue(count);
            }

            UI.Windows7UIFactory.StopProgressState();
        }

        /// <summary>
        /// Merges the import database with main movie database
        /// </summary>
        public static void MergeImportDatabaseWithMain()
        {
            MovieDBFactory.MergeWithDatabase(ImportDatabase);
            MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();
            MovieSetManager.ScanForSetImages();
        }
    }
}
