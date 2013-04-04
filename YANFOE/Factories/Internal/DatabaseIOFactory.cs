// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="DatabaseIOFactory.cs">
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
//   The database io factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Internal
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;

    using BitFactory.Logging;

    using Newtonsoft.Json;

    using YANFOE.Factories.Import;
    using YANFOE.Factories.Media;
    using YANFOE.Factories.Sets;
    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.MovieModels;
    using YANFOE.Models.SetsModels;
    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Models.TvModels.Show;
    using YANFOE.Settings;
    using YANFOE.Tools;
    using YANFOE.Tools.Compression;
    using YANFOE.Tools.IO;
    using YANFOE.Tools.ThirdParty;
    using YANFOE.Tools.UI;
    using YANFOE.UI.Dialogs.General;

    #endregion

    /// <summary>
    ///   The database io factory.
    /// </summary>
    public static class DatabaseIOFactory
    {
        #region Static Fields

        /// <summary>
        ///   Timer for the Start save dialog
        /// </summary>
        private static readonly Timer Tmr = new Timer();

        /// <summary>
        ///   The database dirty.
        /// </summary>
        private static bool databaseDirty;

        /// <summary>
        ///   The start save dialog
        /// </summary>
        private static WndSavingDB frmSavingDB = new WndSavingDB();

        /// <summary>
        ///   The saving count.
        /// </summary>
        private static int savingCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="DatabaseIOFactory" /> class.
        /// </summary>
        static DatabaseIOFactory()
        {
            Tmr.Elapsed += TmrTick;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   The database dirty changed.
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler DatabaseDirtyChanged = delegate { };

        #endregion

        #region Enums

        /// <summary>
        ///   The output name.
        /// </summary>
        public enum OutputName
        {
            /// <summary>
            ///   The null value
            /// </summary>
            None, 

            /// <summary>
            ///   The media path DB.
            /// </summary>
            MediaPathDb, 

            /// <summary>
            ///   The movie DB.
            /// </summary>
            MovieDb, 

            /// <summary>
            ///   The movie sets.
            /// </summary>
            MovieSets, 

            /// <summary>
            ///   The TV DB.
            /// </summary>
            TvDb, 

            /// <summary>
            ///   The scan series pick DB
            /// </summary>
            ScanSeriesPick, 

            /// <summary>
            ///   Save All DB's
            /// </summary>
            All
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether AppLoading.
        /// </summary>
        public static bool AppLoading { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether DatabaseDirty.
        /// </summary>
        public static bool DatabaseDirty
        {
            get
            {
                return databaseDirty;
            }

            set
            {
                databaseDirty = value;
                InvokeDatabaseDirtyChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets or sets the saving count.
        /// </summary>
        public static int SavingCount
        {
            get
            {
                return savingCount;
            }

            set
            {
                savingCount = value;
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether SavingMovieDB.
        /// </summary>
        public static bool SavingMovieDB { get; set; }

        /// <summary>
        ///   Gets or sets SavingMovieMax.
        /// </summary>
        public static int SavingMovieMax { get; set; }

        /// <summary>
        ///   Gets or sets SavingMovieValue.
        /// </summary>
        public static int SavingMovieValue { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether SavingTVDB.
        /// </summary>
        public static bool SavingTVDB { get; set; }

        /// <summary>
        ///   Gets or sets SavingTVDBMax.
        /// </summary>
        public static int SavingTVDBMax { get; set; }

        /// <summary>
        ///   Gets or sets SavingTVDBValue.
        /// </summary>
        public static int SavingTVDBValue { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The invoke database dirty changed.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        public static void InvokeDatabaseDirtyChanged(EventArgs e)
        {
            EventHandler handler = DatabaseDirtyChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// Loads the specified output name.
        /// </summary>
        /// <param name="outputName">
        /// Name of the output. 
        /// </param>
        public static void Load(OutputName outputName)
        {
            switch (outputName)
            {
                case OutputName.MovieDb:
                    LoadMovieDB();
                    break;
                case OutputName.MediaPathDb:
                    LoadMediaPathDB();
                    break;
                case OutputName.MovieSets:
                    LoadMovieSets();
                    break;
                case OutputName.TvDb:
                    LoadTVDB();
                    break;
                case OutputName.ScanSeriesPick:
                    LoadScanSeriesPick();
                    break;
                case OutputName.All:
                    LoadMovieDB();
                    LoadMovieSets();
                    LoadMediaPathDB();
                    MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();
                    LoadTVDB();
                    LoadScanSeriesPick();
                    MasterMediaDBFactory.PopulateMasterTVMediaDatabase();
                    break;
            }
        }

        /// <summary>
        /// Saves the specified type.
        /// </summary>
        /// <param name="type">
        /// The OutputName type. 
        /// </param>
        public static void Save(OutputName type)
        {
            if (savingCount > 0)
            {
                return;
            }

            switch (type)
            {
                case OutputName.MovieDb:
                    SaveMovieDB();
                    break;
                case OutputName.MediaPathDb:
                    SaveMediaPathDB();
                    break;
                case OutputName.MovieSets:
                    SaveMovieSets();
                    break;
                case OutputName.TvDb:
                    SaveTVDB();
                    break;
                case OutputName.ScanSeriesPick:
                    SaveScanSeriesPick();
                    break;
                case OutputName.All:
                    StartSaveDialog();
                    SaveMovieDB();
                    SaveMediaPathDB();
                    SaveMovieSets();
                    SaveTVDB();
                    SaveScanSeriesPick();
                    DatabaseDirty = false;
                    break;
            }
        }

        /// <summary>
        ///   The set database dirty.
        /// </summary>
        public static void SetDatabaseDirty()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the BGWSaveMediaPathDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwSaveMediaPathDbDoWork(object sender, DoWorkEventArgs e)
        {
            savingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MediaPathDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            int count = 0;

            foreach (var json in MediaPathDBFactory.Instance.MediaPathDB.Select(JsonConvert.SerializeObject))
            {
                Gzip.CompressString(json, path + count + ".MediaPath.gz");

                count++;
            }

            savingCount--;
        }

        /// <summary>
        /// The save movie sets do work.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private static void BgwSaveMovieSetsDoWork(object sender, DoWorkEventArgs e)
        {
            savingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieSets + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            foreach (MovieSetModel set in MovieSetManager.CurrentDatabase)
            {
                string json = JsonConvert.SerializeObject(set);
                Gzip.CompressString(json, path + FileNaming.RemoveIllegalChars(set.SetName) + ".MovieSet.gz");
            }

            savingCount--;
        }

        /// <summary>
        /// The save scan series pick do work.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private static void BgwSaveScanSeriesPickDoWork(object sender, DoWorkEventArgs e)
        {
            savingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.ScanSeriesPick + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            const string WritePath = "SeriesPick";
            string json = JsonConvert.SerializeObject(ImportTvFactory.Instance.ScanSeriesPicks);
            Gzip.CompressString(json, path + WritePath + "SeriesPick.gz");

            savingCount--;
        }

        /// <summary>
        ///   Loads the media path DB.
        /// </summary>
        private static void LoadMediaPathDB()
        {
            if (MediaPathDBFactory.Instance.MediaPathDB == null)
            {
                MediaPathDBFactory.Instance.MediaPathDB = new ThreadedBindingList<MediaPathModel>();
            }

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MediaPathDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            IEnumerable<string> files = Directory.EnumerateFiles(path, "*.MediaPath.gz", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string json = Gzip.Decompress(file);
                var mediaPath = JsonConvert.DeserializeObject(json, typeof(MediaPathModel)) as MediaPathModel;
                MediaPathDBFactory.Instance.MediaPathDB.Add(mediaPath);
            }
        }

        /// <summary>
        ///   Loads the movie DB.
        /// </summary>
        private static void LoadMovieDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            MovieDBFactory.Instance.MovieDatabase.Clear();

            var files = FileHelper.GetFilesRecursive(path, "*.movie.gz").ToArray();
            LoadMovies(path, files, MovieDBFactory.Instance.MovieDatabase);

            files = FileHelper.GetFilesRecursive(path, "*.hiddenmovie.gz").ToArray();
            LoadMovies(path, files, MovieDBFactory.Instance.HiddenMovieDatabase);
        }

        /// <summary>
        ///   Loads the movie sets DB.
        /// </summary>
        private static void LoadMovieSets()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieSets + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            string[] files = FileHelper.GetFilesRecursive(path, "*.MovieSet.gz").ToArray();

            MovieSetManager.CurrentDatabase.Clear();

            foreach (string file in files)
            {
                string json = Gzip.Decompress(file);
                var set = JsonConvert.DeserializeObject(json, typeof(MovieSetModel)) as MovieSetModel;

                MovieSetManager.CurrentDatabase.Add(set);
            }
        }

        /// <summary>
        /// The load movies.
        /// </summary>
        /// <param name="path">
        /// The path. 
        /// </param>
        /// <param name="files">
        /// The files. 
        /// </param>
        /// <param name="database">
        /// The database. 
        /// </param>
        private static void LoadMovies(string path, string[] files, ThreadedBindingList<MovieModel> database)
        {
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            Parallel.ForEach(
                files, 
                parallelOptions, 
                file =>
                    {
                        string json = Gzip.Decompress(file);

                        var movieModel = JsonConvert.DeserializeObject(json, typeof(MovieModel)) as MovieModel;

                        if (movieModel != null)
                        {
                            if (json.Contains(@"ChangedText"":false"))
                            {
                                movieModel.ChangedText = false;
                            }

                            if (json.Contains(@"ChangedPoster"":false"))
                            {
                                movieModel.ChangedPoster = false;
                            }

                            if (json.Contains(@"ChangedFanart"":false"))
                            {
                                movieModel.ChangedFanart = false;
                            }

                            movieModel.DatabaseSaved = true;

                            if (movieModel.AssociatedFiles.GetMediaCollection().Count > 0)
                            {
                                if (!File.Exists(movieModel.AssociatedFiles.GetMediaCollection()[0].PathAndFilename))
                                {
                                    Log.WriteToLog(
                                        LogSeverity.Info, 
                                        LoggerName.GeneralLog, 
                                        "Internal > DatabaseIOFactory > LoadMovieDB", 
                                        string.Format(
                                            "Deleting {0}. Movie not found on the filesystem", 
                                            movieModel.AssociatedFiles.GetMediaCollection()[0].FileName));

                                    // We should check for network path and make sure the file has actually been deleted or removed
                                    File.Delete(file);
                                }
                            }

                            lock (database)
                            {
                                database.Add(movieModel);
                            }

                            string title = FileNaming.RemoveIllegalChars(movieModel.Title);

                            string poster = path + title + ".poster.jpg";
                            string fanart = path + title + ".fanart.jpg";

                            if (File.Exists(poster))
                            {
                                movieModel.SmallPoster = ImageHandler.LoadImage(poster);
                            }

                            if (File.Exists(fanart))
                            {
                                movieModel.SmallFanart = ImageHandler.LoadImage(fanart);
                            }
                        }
                    });
        }

        /// <summary>
        ///   Loads the scan series pick DB
        /// </summary>
        private static void LoadScanSeriesPick()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.ScanSeriesPick + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            if (File.Exists(path + "SeriesPickSeriesPick.gz"))
            {
                string json = Gzip.Decompress(path + "SeriesPickSeriesPick.gz");

                ImportTvFactory.Instance.ScanSeriesPicks =
                    JsonConvert.DeserializeObject(json, typeof(ThreadedBindingList<ScanSeriesPick>)) as
                    ThreadedBindingList<ScanSeriesPick>;
            }
        }

        /// <summary>
        ///   Loads the TV DB
        /// </summary>
        private static void LoadTVDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            List<string> files = FileHelper.GetFilesRecursive(path, "*.Series.gz");

            TVDBFactory.Instance.TVDatabase.Clear();

            var hidden = path + "hidden.hiddenSeries.gz";

            if (File.Exists(hidden))
            {
                var json = Gzip.Decompress(hidden);
                TVDBFactory.Instance.HiddenTVDB =
                    JsonConvert.DeserializeObject(json, typeof(ThreadedBindingList<Series>)) as
                    ThreadedBindingList<Series>;
            }

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            Parallel.ForEach(
                files, 
                parallelOptions, 
                file =>
                    {
                        var json = Gzip.Decompress(file);

                        var series = JsonConvert.DeserializeObject(json, typeof(Series)) as Series;

                        string title = FileNaming.RemoveIllegalChars(series.SeriesName);

                        string poster = path + title + ".poster.jpg";
                        string fanart = path + title + ".fanart.jpg";
                        string banner = path + title + ".banner.jpg";

                        if (File.Exists(poster))
                        {
                            series.SmallPoster = ImageHandler.LoadImage(poster);
                        }

                        if (File.Exists(fanart))
                        {
                            series.SmallFanart = ImageHandler.LoadImage(fanart);
                        }

                        if (File.Exists(banner))
                        {
                            series.SmallBanner = ImageHandler.LoadImage(banner);
                        }

                        foreach (var season in series.Seasons)
                        {
                            for (int index = 0; index < season.Episodes.Count; index++)
                            {
                                var episode = season.Episodes[index];
                                if (episode.FilePath.PathAndFilename != string.Empty
                                    && !File.Exists(episode.FilePath.PathAndFilename))
                                {
                                    Log.WriteToLog(
                                        LogSeverity.Info, 
                                        LoggerName.GeneralLog, 
                                        "Internal > DatabaseIOFactory > LoadTVDB", 
                                        string.Format(
                                            "Deleting {0}. Episode not found on the filesystem", 
                                            episode.FilePath.PathAndFilename));

                                    // We should check for network path and make sure the file has actually been deleted or removed
                                    File.Delete(file);
                                    series.Seasons.First(x => x.SeasonNumber == season.SeasonNumber).Episodes.Remove(
                                        episode);
                                }
                            }
                        }

                        lock (TVDBFactory.Instance.TVDatabase)
                        {
                            TVDBFactory.Instance.TVDatabase.Add(series);
                        }
                    });

            TVDBFactory.Instance.GeneratePictureGallery();
            TVDBFactory.Instance.GenerateMasterSeriesList();
        }

        /// <summary>
        ///   Saves the media path DB.
        /// </summary>
        private static void SaveMediaPathDB()
        {
            var bgwSaveMediaPathDb = new BackgroundWorker();
            bgwSaveMediaPathDb.DoWork += BgwSaveMediaPathDbDoWork;
            bgwSaveMediaPathDb.RunWorkerAsync();
        }

        /// <summary>
        ///   Saves the movie DB.
        /// </summary>
        private static void SaveMovieDB()
        {
            savingCount++;

            SavingMovieDB = true;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            SavingMovieValue = 0;
            SavingMovieMax = 0;

            SaveMovies(MovieDBFactory.Instance.MovieDatabase);
            SaveMovies(MovieDBFactory.Instance.HiddenMovieDatabase);

            SavingMovieDB = false;

            savingCount--;

            frmSavingDB.TvDBFinished();
        }

        /// <summary>
        ///   Saves the movie sets DB
        /// </summary>
        private static void SaveMovieSets()
        {
            var bgwSaveMovieSets = new BackgroundWorker();
            bgwSaveMovieSets.DoWork += BgwSaveMovieSetsDoWork;
            bgwSaveMovieSets.RunWorkerAsync();
        }

        /// <summary>
        /// The save movies.
        /// </summary>
        /// <param name="database">
        /// The database. 
        /// </param>
        private static void SaveMovies(ThreadedBindingList<MovieModel> database)
        {
            int max = database.Count;

            if (max == 0)
            {
                return;
            }

            SavingMovieMax += database.Count - 1;
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            Parallel.ForEach(
                database, 
                parallelOptions, 
                movie =>
                    {
                        savingCount++;

                        var path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;

                        var title = FileNaming.RemoveIllegalChars(movie.Title);

                        string writePath;

                        if (movie.Hidden)
                        {
                            writePath = path + title + ".hiddenmovie";
                        }
                        else
                        {
                            writePath = path + title + ".movie";
                        }

                        movie.DatabaseSaved = true;
                        var json = JsonConvert.SerializeObject(movie);
                        Gzip.CompressString(json, writePath + ".gz");

                        var posterPath = path + title + ".poster.jpg";
                        var fanartPath = path + title + ".fanart.jpg";

                        if (movie.SmallPoster != null)
                        {
                            movie.SmallPoster.Save(posterPath);
                        }

                        if (movie.SmallFanart != null)
                        {
                            movie.SmallFanart.Save(fanartPath);
                        }

                        SavingMovieValue++;
                    });

            frmSavingDB.MovieDBFinished();

            SavingMovieDB = false;
        }

        /// <summary>
        ///   Saves the scan series pick.
        /// </summary>
        private static void SaveScanSeriesPick()
        {
            var bgwSaveScanSeriesPick = new BackgroundWorker();
            bgwSaveScanSeriesPick.DoWork += BgwSaveScanSeriesPickDoWork;
            bgwSaveScanSeriesPick.RunWorkerAsync();
        }

        /// <summary>
        ///   The save TV DB.
        /// </summary>
        private static void SaveTVDB()
        {
            savingCount++;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;

            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            string writePath = path + "hidden.hiddenSeries";
            string json = JsonConvert.SerializeObject(TVDBFactory.Instance.HiddenTVDB);
            Gzip.CompressString(json, writePath + ".gz");

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            SavingTVDBMax = TVDBFactory.Instance.TVDatabase.Count;
            SavingTVDBValue = 0;

            Parallel.ForEach(
                TVDBFactory.Instance.TVDatabase, 
                parallelOptions, 
                series =>
                    {
                        var title = FileNaming.RemoveIllegalChars(series.SeriesName);
                        var seriesPath = string.Concat(
                            Get.FileSystemPaths.PathDatabases, 
                            OutputName.TvDb, 
                            Path.DirectorySeparatorChar, 
                            title, 
                            ".Series.gz");

                        json = JsonConvert.SerializeObject(series);
                        Gzip.CompressString(json, seriesPath);

                        if (series.SmallBanner != null)
                        {
                            var smallBanner = new Bitmap(series.SmallBanner);
                            smallBanner.Save(path + title + ".banner.jpg");
                        }

                        if (series.SmallFanart != null)
                        {
                            var smallFanart = new Bitmap(series.SmallFanart);
                            smallFanart.Save(path + title + ".fanner.jpg");
                        }

                        if (series.SmallPoster != null)
                        {
                            var smallPoster = new Bitmap(series.SmallPoster);
                            smallPoster.Save(path + title + ".poster.jpg");
                        }

                        SavingTVDBValue++;
                    });

            savingCount--;
            frmSavingDB.TvDBFinished();
        }

        /// <summary>
        ///   Opens the StartSaveDialog
        /// </summary>
        private static void StartSaveDialog()
        {
            frmSavingDB = new WndSavingDB();
            SavingMovieValue = -1;
            SavingTVDBValue = -1;
            frmSavingDB.Reset();
            frmSavingDB.Show();
            Tmr.Start();
        }

        /// <summary>
        /// The timer tick.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private static void TmrTick(object sender, EventArgs e)
        {
            frmSavingDB.SetMovieValue(SavingMovieValue);
            frmSavingDB.SetMovieDBMax(SavingMovieMax);
            frmSavingDB.SetTVDBValue(SavingTVDBValue);
            frmSavingDB.SetTVDBMax(SavingTVDBMax);

            if (SavingMovieValue == SavingMovieMax - 1)
            {
                frmSavingDB.MovieDBFinished();
            }

            if (SavingTVDBValue == SavingTVDBMax)
            {
                frmSavingDB.TvDBFinished();
            }
        }

        #endregion
    }
}