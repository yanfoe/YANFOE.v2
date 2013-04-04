// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="ImportMoviesFactory.cs">
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
//   The factory for the initial movie import routines
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Import
{
    #region Required Namespaces

    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YANFOE.Factories.InOut;
    using YANFOE.Factories.Media;
    using YANFOE.Factories.Sets;
    using YANFOE.Factories.UI;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools;
    using YANFOE.Tools.Importing;
    using YANFOE.Tools.IO;
    using YANFOE.Tools.ThirdParty;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   The factory for the initial movie import routines
    /// </summary>
    public class ImportMoviesFactory : FactoryBase
    {
        #region Static Fields

        /// <summary>
        ///   The instance.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static ImportMoviesFactory Instance = new ImportMoviesFactory();

        #endregion

        #region Fields

        /// <summary>
        ///   The cancel import.
        /// </summary>
        private bool cancelImport;

        /// <summary>
        ///   Gets or sets the current record.
        /// </summary>
        /// <value> The current record. </value>
        private MovieModel currentRecord;

        /// <summary>
        ///   Gets or sets the import database.
        /// </summary>
        /// <value> The import database. </value>
        private ThreadedBindingList<MovieModel> importDatabase;

        /// <summary>
        ///   Gets or sets the import duplicates database.
        /// </summary>
        /// <value> The import duplicates database. </value>
        private ThreadedBindingList<MovieModel> importDuplicatesDatabase;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Prevents a default instance of the <see cref="ImportMoviesFactory" /> class from being created. 
        ///   Initializes static members of the <see cref="ImportMoviesFactory" /> class.
        /// </summary>
        private ImportMoviesFactory()
        {
            this.ImportDatabase = new ThreadedBindingList<MovieModel>();
            this.ImportDuplicatesDatabase = new ThreadedBindingList<MovieModel>();
            this.CurrentRecord = new MovieModel();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the current record.
        /// </summary>
        public MovieModel CurrentRecord
        {
            get
            {
                return this.currentRecord;
            }

            set
            {
                this.currentRecord = value;
                this.OnPropertyChanged("CurrentRecord");
            }
        }

        /// <summary>
        ///   Gets or sets the import database.
        /// </summary>
        public ThreadedBindingList<MovieModel> ImportDatabase
        {
            get
            {
                return this.importDatabase;
            }

            set
            {
                this.importDatabase = value;
                this.OnPropertyChanged("ImportDatabase");
            }
        }

        /// <summary>
        ///   Gets or sets the import duplicates database.
        /// </summary>
        public ThreadedBindingList<MovieModel> ImportDuplicatesDatabase
        {
            get
            {
                return this.importDuplicatesDatabase;
            }

            set
            {
                this.importDuplicatesDatabase = value;
                this.OnPropertyChanged("ImportDuplicatesDatabase");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The cancel movie import.
        /// </summary>
        public void CancelMovieImport()
        {
            this.cancelImport = true;
        }

        /// <summary>
        ///   Converts the media path import database into a MovieModel DB.
        /// </summary>
        public void ConvertMediaPathImportToDB()
        {
            this.cancelImport = false;

            var db = MediaPathDBFactory.Instance.MediaPathMoviesUnsorted;

            var count = 0;

            MovieDBFactory.Instance.ImportProgressMaximum = db.Count;

            this.ImportDatabase.Clear();
            this.ImportDuplicatesDatabase.Clear();

            var getFiles = new string[1];
            var currentGetPathFiles = string.Empty;

            Windows7UIFactory.StartProgressState(db.Count);

            foreach (var file in db)
            {
                if (this.cancelImport)
                {
                    break;
                }

                MovieDBFactory.Instance.ImportProgressCurrent = count;

                MovieDBFactory.Instance.ImportProgressStatus =
                    string.Format("Processing: " + file.PathAndFileName.Replace("{", "{{").Replace("}", "}}"));

                if (file.Path != currentGetPathFiles)
                {
                    getFiles = FileHelper.GetFilesRecursive(file.Path).ToArray();
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
                    var detect = DetectType.FindVideoSource(file.PathAndFileName);

                    if (!string.IsNullOrEmpty(detect))
                    {
                        videoSource = detect;
                    }
                }

                string title = MovieNaming.GetMovieName(file.PathAndFileName, file.MediaPathType);
                var movieModel = new MovieModel
                    {
                        Title = title, 
                        Year = MovieNaming.GetMovieYear(file.PathAndFileName), 
                        ScraperGroup = file.ScraperGroup, 
                        VideoSource = videoSource, 
                        NfoPathOnDisk = this.FindNFO(file.FilenameWithOutExt, this.FindFilePath(title, file), getFiles), 
                        PosterPathOnDisk =
                            this.FindPoster(file.FilenameWithOutExt, this.FindFilePath(title, file), getFiles), 
                        FanartPathOnDisk =
                            this.FindFanart(file.FilenameWithOutExt, this.FindFilePath(title, file), getFiles)
                    };

                if (!string.IsNullOrEmpty(movieModel.NfoPathOnDisk))
                {
                    OutFactory.LoadMovie(movieModel);
                    movieModel.ChangedText = false;
                }

                var result =
                    (from m in this.ImportDatabase
                     where m.Title.ToLower().Trim() == movieModel.Title.ToLower().Trim()
                     select m).ToList();

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

                    // Does the movie exist in our current DB?
                    var result2 = (from m in MovieDBFactory.Instance.MovieDatabase
                                   where m.Title.ToLower().Trim() == movieModel.Title.ToLower().Trim()
                                   select m).ToList();
                    if (result2.Count > 0)
                    {
                        if (movieModel.Year != null)
                        {
                            var r = (from m in result2 where m.Year == movieModel.Year select m).ToList();
                            if (r.Count > 0)
                            {
                                // We already have a movie with that name and year, mark as dupe
                                this.ImportDuplicatesDatabase.Add(movieModel);
                            }
                        }
                        else
                        {
                            // No year, so we can't ensure it's a dupe
                            this.ImportDuplicatesDatabase.Add(movieModel);
                        }
                    }

                    // Add it to the list anyway, since there's no implementation of any action on duplicates.
                    this.ImportDatabase.Add(movieModel);
                }
                else
                {
                    var r = (from m in result where m.Year == movieModel.Year select m).ToList();
                    if (Regex.IsMatch(
                        file.PathAndFileName.ToLower(), @"(disc|disk|part|cd|vob|ifo|bup)", RegexOptions.IgnoreCase))
                    {
                        // Only associate with an existing movie if its not a dupe
                        result[0].AssociatedFiles.AddToMediaCollection(file);
                    }
                    else if (r.Count == 0)
                    {
                        // Same title, different year
                        this.ImportDatabase.Add(movieModel);
                    }
                    else
                    {
                        // Dont count a disc or part as a dupe or movies with different years
                        this.ImportDuplicatesDatabase.Add(movieModel);

                        // Add it to the list anyway, since there's no implementation of any action on duplicates.
                        this.ImportDatabase.Add(movieModel);
                    }
                }

                count++;
                Windows7UIFactory.SetProgressValue(count);
            }

            Windows7UIFactory.StopProgressState();
        }

        /// <summary>
        /// Finds fanart on disk
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// Found file or string.Empty 
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public string FindFanart(string fileName, string path, string[] fileList = null)
        {
            return Find.FindMovieFanart(fileName, path, fileList);
        }

        /// <summary>
        /// The find file path.
        /// </summary>
        /// <param name="title">
        /// The title. 
        /// </param>
        /// <param name="file">
        /// The file. 
        /// </param>
        /// <param name="setName">
        /// The set name. 
        /// </param>
        /// <returns>
        /// The <see cref="string"/> . 
        /// </returns>
        public string FindFilePath(string title, MediaPathFileModel file, string setName = "")
        {
            string p = file.Path;

            /*if (!Get.InOutCollection.CurrentMovieSaveSettings.BlurayPosterNameTemplate.Contains("<path>"))
            {
                p = Path.GetDirectoryName(Get.InOutCollection.CurrentMovieSaveSettings.BlurayPosterNameTemplate);
            }
            else if (!Get.InOutCollection.CurrentMovieSaveSettings.DvdPosterNameTemplate.Contains("<path>"))
            {
                p = Path.GetDirectoryName(Get.InOutCollection.CurrentMovieSaveSettings.DvdPosterNameTemplate);
            }
            else if (!Get.InOutCollection.CurrentMovieSaveSettings.NormalPosterNameTemplate.Contains("<path>"))
            {
                p = Path.GetDirectoryName(Get.InOutCollection.CurrentMovieSaveSettings.NormalPosterNameTemplate);
            }*/
            p =
                p.Replace("<path>", file.Path).Replace("<filename>", file.FilenameWithOutExt).Replace(
                    "<ext>", file.FilenameExt).Replace("<setname>", setName).Replace("<title>", title);

            return p;
        }

        /// <summary>
        /// Finds NFO on disk
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// Found file or string.Empty 
        /// </returns>
        public string FindNFO(string fileName, string path, string[] fileList = null)
        {
            return Find.FindMovieNFO(fileName, path, fileList);
        }

        /// <summary>
        /// Finds poster on disk
        /// </summary>
        /// <param name="fileName">
        /// Name of the file. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        /// <param name="fileList">
        /// The file list. 
        /// </param>
        /// <returns>
        /// Found file or string.Empty 
        /// </returns>
        public string FindPoster(string fileName, string path, string[] fileList = null)
        {
            return Find.FindMoviePoster(fileName, path, fileList);
        }

        /// <summary>
        ///   Merges the import database with main movie database
        /// </summary>
        public void MergeImportDatabaseWithMain()
        {
            this.ValidateDatabaseExistence();
            MovieDBFactory.Instance.MergeWithDatabase(this.ImportDatabase);
            MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();
            MovieDBFactory.Instance.MergeWithDatabase(
                this.ImportDuplicatesDatabase, MovieDBFactory.MovieDBTypes.Duplicates);
            MovieSetManager.ScanForSetImages();
        }

        /// <summary>
        ///   The validate database existence.
        /// </summary>
        public void ValidateDatabaseExistence()
        {
            for (int i = 0; i < this.ImportDatabase.Count; i++)
            {
                var file = this.ImportDatabase[i];
                if (file.AssociatedFiles.Media.Count == 0 || !File.Exists(file.AssociatedFiles.Media[0].PathAndFilename))
                {
                    this.ImportDatabase.Remove(file);
                }
            }
        }

        #endregion
    }
}