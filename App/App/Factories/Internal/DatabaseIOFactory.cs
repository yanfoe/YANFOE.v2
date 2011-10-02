// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseIOFactory.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   The database io factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Factories.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Newtonsoft.Json;

    using BitFactory.Logging;
    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;

    using YANFOE.Factories.Import;
    using YANFOE.Factories.Media;
    using YANFOE.Factories.Sets;
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
    using YANFOE.UI.Dialogs.General;

    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// The database io factory.
    /// </summary>
    public static class DatabaseIOFactory
    {
        #region Constants and Fields

        /// <summary>
        /// The saving count.
        /// </summary>
        public static int SavingCount;

        /// <summary>
        ///   Timer for the Start save dialog
        /// </summary>
        private static readonly Timer tmr = new Timer();

        /// <summary>
        /// The database dirty.
        /// </summary>
        private static bool databaseDirty;

        /// <summary>
        ///   The start save dialog
        /// </summary>
        private static FrmSavingDB frmSavingDB = new FrmSavingDB();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref = "DatabaseIOFactory" /> class.
        /// </summary>
        static DatabaseIOFactory()
        {
            tmr.Tick += tmr_Tick;
        }

        #endregion

        #region Events

        /// <summary>
        ///   The database dirty changed.
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler DatabaseDirtyChanged = delegate { };

        #endregion

        #region Enums

        /// <summary>
        /// The output name.
        /// </summary>
        public enum OutputName
        {
            /// <summary>
            ///   The null value
            /// </summary>
            None, 

            /// <summary>
            ///   The media path db.
            /// </summary>
            MediaPathDb, 

            /// <summary>
            ///   The movie db.
            /// </summary>
            MovieDb, 

            /// <summary>
            ///   The movie sets.
            /// </summary>
            MovieSets, 

            /// <summary>
            ///   The tv db.
            /// </summary>
            TvDb, 

            /// <summary>
            ///   The scan series pick db
            /// </summary>
            ScanSeriesPick, 

            /// <summary>
            ///   Save All DB's
            /// </summary>
            All
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether AppLoading.
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

        #region Public Methods

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
                    LoadMediaPathDb();
                    break;
                case OutputName.MovieSets:
                    LoadMovieSets();
                    break;
                case OutputName.TvDb:
                    LoadTvDB();
                    break;
                case OutputName.ScanSeriesPick:
                    LoadScanSeriesPick();
                    break;
                case OutputName.All:
                    LoadMovieDB();
                    LoadMovieSets();
                    LoadMediaPathDb();
                    MasterMediaDBFactory.PopulateMasterMovieMediaDatabase();
                    LoadTvDB();
                    LoadScanSeriesPick();
                    MasterMediaDBFactory.PopulateMasterTvMediaDatabase();
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
            if (SavingCount > 0)
            {
                return;
            }

            switch (type)
            {
                case OutputName.MovieDb:
                    SaveMovieDB();
                    break;
                case OutputName.MediaPathDb:
                    SaveMediaPathDb();
                    break;
                case OutputName.MovieSets:
                    SaveMovieSets();
                    break;
                case OutputName.TvDb:
                    SaveTvDB();
                    break;
                case OutputName.ScanSeriesPick:
                    SaveScanSeriesPick();
                    break;
                case OutputName.All:
                    StartSaveDialog();
                    SaveMovieDB();
                    SaveMediaPathDb();
                    SaveMovieSets();
                    SaveTvDB();
                    SaveScanSeriesPick();
                    DatabaseDirty = false;
                    break;
            }
        }

        /// <summary>
        /// The set database dirty.
        /// </summary>
        public static void SetDatabaseDirty()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the media path db.
        /// </summary>
        private static void LoadMediaPathDb()
        {
            if (MediaPathDBFactory.MediaPathDB == null)
            {
                MediaPathDBFactory.MediaPathDB = new BindingList<MediaPathModel>();
            }

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MediaPathDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            IEnumerable<string> files = Directory.EnumerateFiles(path, "*.MediaPath.gz", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string json = Gzip.Decompress(file);
                var mediaPath = JsonConvert.DeserializeObject(json, typeof(MediaPathModel)) as MediaPathModel;
                MediaPathDBFactory.MediaPathDB.Add(mediaPath);
            }
        }

        /// <summary>
        /// Loads the movie DB.
        /// </summary>
        private static void LoadMovieDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            MovieDBFactory.MovieDatabase.Clear();

            var files = FileHelper.GetFilesRecursive(path, "*.movie.gz").ToArray();
            LoadMovies(path, files, MovieDBFactory.MovieDatabase);

            files = FileHelper.GetFilesRecursive(path, "*.hiddenmovie.gz").ToArray();
            LoadMovies(path, files, MovieDBFactory.HiddenMovieDatabase);
        }

        private static void LoadMovies(string path, string[] files, BindingList<MovieModel> database)
        {
            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            Parallel.ForEach(
                files,
                parallelOptions,
                file =>
                    {
                        string json = Gzip.Decompress(file);

                        var movieModel = JsonConvert.DeserializeObject(json, typeof(MovieModel)) as MovieModel;

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

                        if (movieModel != null)
                        {
                            lock (database)
                            {
                                database.Add(movieModel);
                            }
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


                    });
        }

        /// <summary>
        /// Loads the movie sets db.
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
        /// Loads the scan series pick db
        /// </summary>
        private static void LoadScanSeriesPick()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.ScanSeriesPick + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            if (File.Exists(path + "SeriesPickSeriesPick.gz"))
            {
                string json = Gzip.Decompress(path + "SeriesPickSeriesPick.gz");

                ImportTvFactory.ScanSeriesPicks =
                    JsonConvert.DeserializeObject(json, typeof(BindingList<ScanSeriesPick>)) as
                    BindingList<ScanSeriesPick>;
            }
        }

        /// <summary>
        /// Loads the TV DB db
        /// </summary>
        private static void LoadTvDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            List<string> files = FileHelper.GetFilesRecursive(path, "*.Series.gz");

            TvDBFactory.TvDatabase.Clear();

            var hidden = path + "hidden.hiddenSeries.gz";

            if (File.Exists(hidden))
            {
                var json = Gzip.Decompress(hidden);
                TvDBFactory.HiddenTvDatabase =
                    JsonConvert.DeserializeObject(json, typeof(BindingList<Series>)) as BindingList<Series>;
            }

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 1 };

            bool tvDBLock = false;

            var loadedSeries = new List<Series>();

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
                            for (int index = 0; index < season.Value.Episodes.Count; index++)
                            {
                                var episode = season.Value.Episodes[index];
                                if (episode.FilePath.PathAndFilename != string.Empty
                                    && !File.Exists(episode.FilePath.PathAndFilename))
                                {
                                    Log.WriteToLog(
                                        LogSeverity.Info,
                                        LoggerName.GeneralLog,
                                        "Internal > DatabaseIOFactory > LoadTvDB",
                                        string.Format(
                                            "Deleting {0}. Episode not found on the filesystem",
                                            episode.FilePath.PathAndFilename));
                                    // We should check for network path and make sure the file has actually been deleted or removed
                                    File.Delete(file);
                                    series.Seasons[season.Key].Episodes.Remove(episode);
                                }
                            }
                        }

                        lock (loadedSeries)
                        {
                            loadedSeries.Add(series);
                        }
                    });

            foreach (var series in loadedSeries)
            {
                if (series.SeriesName != string.Empty)

                {
                    TvDBFactory.TvDatabase.Add(series.SeriesName, series);
                }
            }

            TvDBFactory.GeneratePictureGallery();
            TvDBFactory.GenerateMasterSeriesList();
        }

        /// <summary>
        /// Saves the media path db.
        /// </summary>
        private static void SaveMediaPathDb()
        {
            var bgwSaveMediaPathDb = new BackgroundWorker();
            bgwSaveMediaPathDb.DoWork += bgwSaveMediaPathDb_DoWork;
            bgwSaveMediaPathDb.RunWorkerAsync();
        }

        /// <summary>
        /// Saves the movie DB.
        /// </summary>
        private static void SaveMovieDB()
        {
            SavingCount++;

            SavingMovieDB = true;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            SavingMovieValue = 0;
            SavingMovieMax = 0;

            SaveMovies(MovieDBFactory.MovieDatabase);
            SaveMovies(MovieDBFactory.HiddenMovieDatabase);

            SavingMovieDB = false;

            SavingCount--;

            frmSavingDB.TvDBFinished();
        }

        /// <summary>
        /// Saves the movie sets db
        /// </summary>
        private static void SaveMovieSets()
        {
            var bgwSaveMovieSets = new BackgroundWorker();
            bgwSaveMovieSets.DoWork += bgwSaveMovieSets_DoWork;
            bgwSaveMovieSets.RunWorkerAsync();
        }

        /// <summary>
        /// Saves the scan series pick.
        /// </summary>
        private static void SaveScanSeriesPick()
        {
            var bgwSaveScanSeriesPick = new BackgroundWorker();
            bgwSaveScanSeriesPick.DoWork += bgwSaveScanSeriesPick_DoWork;
            bgwSaveScanSeriesPick.RunWorkerAsync();
        }

        /// <summary>
        /// Opens the StartSaveDialog
        /// </summary>
        private static void StartSaveDialog()
        {
            frmSavingDB = new FrmSavingDB();
            SavingMovieValue = -1;
            SavingTVDBValue = -1;
            frmSavingDB.Reset();
            frmSavingDB.Show();
            tmr.Start();
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSaveMediaPathDb control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveMediaPathDb_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MediaPathDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            int count = 0;

            for (int index = 0; index < MediaPathDBFactory.MediaPathDB.Count; index++)
            {
                MediaPathModel mediaPath = MediaPathDBFactory.MediaPathDB[index];
                string json = JsonConvert.SerializeObject(mediaPath);
                Gzip.CompressString(json, path + count + ".MediaPath.gz");

                count++;
            }

            SavingCount--;
        }

        private static void SaveMovies(BindingList<MovieModel> database)
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
                        SavingCount++;

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

                        Application.DoEvents();

                        SavingMovieValue++;
                    });

            frmSavingDB.MovieDBFinished();

            SavingMovieDB = false;
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSaveMovieSets control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveMovieSets_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieSets + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            foreach (MovieSetModel set in MovieSetManager.CurrentDatabase)
            {
                string json = JsonConvert.SerializeObject(set);
                Gzip.CompressString(json, path + FileNaming.RemoveIllegalChars(set.SetName) + ".MovieSet.gz");
            }

            SavingCount--;
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSaveScanSeriesPick control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveScanSeriesPick_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.ScanSeriesPick + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);

            const string WritePath = "SeriesPick";
            string json = JsonConvert.SerializeObject(ImportTvFactory.ScanSeriesPicks);
            Gzip.CompressString(json, path + WritePath + "SeriesPick.gz");

            SavingCount--;
        }

        private static void SaveTvDB()
        {
            SavingCount++;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            string writePath = path + "hidden.hiddenSeries";
            string json = JsonConvert.SerializeObject(TvDBFactory.HiddenTvDatabase);
            Gzip.CompressString(json, writePath + ".gz");

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 6 };

            SavingTVDBMax = TvDBFactory.TvDatabase.Count;
            SavingTVDBValue = 0;

            Parallel.ForEach(
                TvDBFactory.TvDatabase,
                parallelOptions,
                series =>
                    {
                        path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
                        var title = FileNaming.RemoveIllegalChars(series.Value.SeriesName);

                        writePath = path + title + ".Series";
                        json = JsonConvert.SerializeObject(series.Value);
                        Gzip.CompressString(json, writePath + ".gz");

                        if (series.Value.SmallBanner != null)
                        {
                            var smallBanner = new Bitmap(series.Value.SmallBanner);
                            smallBanner.Save(path + title + ".banner.jpg");
                        }

                        if (series.Value.SmallFanart != null)
                        {
                            var smallFanart = new Bitmap(series.Value.SmallFanart);
                            smallFanart.Save(path + title + ".fanner.jpg");
                        }

                        if (series.Value.SmallPoster != null)
                        {
                            var smallPoster = new Bitmap(series.Value.SmallPoster);
                            smallPoster.Save(path + title + ".poster.jpg");
                        }

                        SavingTVDBValue++;

                        Application.DoEvents();
                    });

            SavingCount--;
            frmSavingDB.TvDBFinished();
        }

        /// <summary>
        /// Handles the Tick event of the tmr control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private static void tmr_Tick(object sender, EventArgs e)
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