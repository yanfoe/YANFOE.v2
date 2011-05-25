// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseIOFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Internal
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    using Newtonsoft.Json;

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
        /// Timer for the Start save dialog
        /// </summary>
        private static readonly Timer tmr = new Timer();
        
        public static int SavingCount;

        /// <summary>
        /// The start save dialog
        /// </summary>
        private static FrmSavingDB frmSavingDB = new FrmSavingDB();

        public static bool AppLoading { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="DatabaseIOFactory"/> class. 
        /// </summary>
        static DatabaseIOFactory()
        {
            tmr.Tick += tmr_Tick;
        }

        #endregion

        #region Events

        /// <summary>
        /// The database dirty changed.
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
            /// The null value
            /// </summary>
            None, 

            /// <summary>
            /// The media path db.
            /// </summary>
            MediaPathDb, 

            /// <summary>
            /// The movie db.
            /// </summary>
            MovieDb, 

            /// <summary>
            /// The movie sets.
            /// </summary>
            MovieSets, 

            /// <summary>
            /// The tv db.
            /// </summary>
            TvDb, 

            /// <summary>
            /// The scan series pick db
            /// </summary>
            ScanSeriesPick, 

            /// <summary>
            /// Save All DB's
            /// </summary>
            All
        }

        #endregion

        #region Properties

        private static bool databaseDirty;

        /// <summary>
        /// Gets or sets a value indicating whether DatabaseDirty.
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
        /// Gets or sets a value indicating whether SavingMovieDB.
        /// </summary>
        public static bool SavingMovieDB { get; set; }

        /// <summary>
        /// Gets or sets SavingMovieMax.
        /// </summary>
        public static int SavingMovieMax { get; set; }

        /// <summary>
        /// Gets or sets SavingMovieValue.
        /// </summary>
        public static int SavingMovieValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SavingTVDB.
        /// </summary>
        public static bool SavingTVDB { get; set; }

        /// <summary>
        /// Gets or sets SavingTVDBMax.
        /// </summary>
        public static int SavingTVDBMax { get; set; }

        /// <summary>
        /// Gets or sets SavingTVDBValue.
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
            string[] files = FileHelper.GetFilesRecursive(path, "*.movie.gz").ToArray();

            MovieDBFactory.MovieDatabase.Clear();

            foreach (string file in files)
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

                MovieDBFactory.MovieDatabase.Add(movieModel);

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

            foreach (string file in files)
            {
                string json = Gzip.Decompress(file);

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

                TvDBFactory.TvDatabase.Add(series.SeriesName, series);
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
            var bgwSaveMovieDB = new BackgroundWorker();
            bgwSaveMovieDB.DoWork += bgwSaveMovieDB_DoWork;
            bgwSaveMovieDB.RunWorkerAsync();
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
        /// Saves the tv DB.
        /// </summary>
        private static void SaveTvDB()
        {
            var bgwSaveTvDB = new BackgroundWorker();
            bgwSaveTvDB.DoWork += bgwSaveTvDB_DoWork;
            bgwSaveTvDB.RunWorkerCompleted += bgwSaveTvDB_RunWorkerCompleted;
            bgwSaveTvDB.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the DoWork event of the SavingTVDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void SavingTVDB_DoWork(object sender, DoWorkEventArgs e)
        {
            var series = e.Argument as Series;
            string path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            string title = FileNaming.RemoveIllegalChars(series.SeriesName);

            string writePath = path + title + ".Series";
            string json = JsonConvert.SerializeObject(series);
            Gzip.CompressString(json, writePath + ".gz");

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

            foreach (MediaPathModel mediaPath in MediaPathDBFactory.MediaPathDB)
            {
                string json = JsonConvert.SerializeObject(mediaPath);
                Gzip.CompressString(json, path + count + ".MediaPath.gz");

                count++;
            }

            SavingCount--;
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveMovieDBWork_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;

            var movieModel = e.Argument as MovieModel;

            string title = FileNaming.RemoveIllegalChars(movieModel.Title);

            string writePath = path + title + ".movie";

            bool writeText = false;
            bool writeImages = false;

            if (!movieModel.DatabaseSaved)
            {
                writeImages = true;
            }

            if (!movieModel.DatabaseSaved || movieModel.ChangedText)
            {
                writeText = true;
            }

            if (writeText)
            {
                movieModel.DatabaseSaved = true;
                string json = JsonConvert.SerializeObject(movieModel);
                Gzip.CompressString(json, writePath + ".gz");
            }

            string posterPath = path + title + ".poster.jpg";
            string fanartPath = path + title + ".fanart.jpg";

            if (movieModel.SmallPoster != null && (movieModel.ChangedPoster || writeImages))
            {
                movieModel.SmallPoster.Save(posterPath);
            }

            if (movieModel.SmallFanart != null && (movieModel.ChangedFanart || writeImages))
            {
                movieModel.SmallFanart.Save(fanartPath);
            }

            SavingCount--;
        }

        /// <summary>
        /// Handles the DoWork event of the bgwSaveMovieDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveMovieDB_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            SavingMovieDB = true;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            var bgw1 = new BackgroundWorker();
            var bgw2 = new BackgroundWorker();
            var bgw3 = new BackgroundWorker();
            var bgw4 = new BackgroundWorker();
            var bgw5 = new BackgroundWorker();
            var bgw6 = new BackgroundWorker();

            bgw1.DoWork += bgwSaveMovieDBWork_DoWork;
            bgw2.DoWork += bgwSaveMovieDBWork_DoWork;
            bgw3.DoWork += bgwSaveMovieDBWork_DoWork;
            bgw4.DoWork += bgwSaveMovieDBWork_DoWork;
            bgw5.DoWork += bgwSaveMovieDBWork_DoWork;
            bgw6.DoWork += bgwSaveMovieDBWork_DoWork;

            int count = 0;
            int max = MovieDBFactory.MovieDatabase.Count;

            if (max == 0)
            {
                SavingCount--;
                return;
            }

            SavingMovieMax = MovieDBFactory.MovieDatabase.Count;

            do
            {
                SavingMovieValue = count;

                MovieModel movieModel = MovieDBFactory.MovieDatabase[count];

                if (!bgw1.IsBusy)
                {
                    count++;
                    bgw1.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else if (!bgw2.IsBusy)
                {
                    count++;
                    bgw2.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else if (!bgw3.IsBusy)
                {
                    count++;
                    bgw3.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else if (!bgw4.IsBusy)
                {
                    count++;
                    bgw4.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else if (!bgw5.IsBusy)
                {
                    count++;
                    bgw5.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else if (!bgw6.IsBusy)
                {
                    count++;
                    bgw6.RunWorkerAsync(movieModel);
                    Application.DoEvents();
                }
                else
                {
                    Application.DoEvents();
                    Thread.Sleep(50);
                }
            }
            while (count < max);

            SavingMovieDB = false;
            SavingCount--;
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

        /// <summary>
        /// Handles the DoWork event of the bgwSaveTvDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveTvDB_DoWork(object sender, DoWorkEventArgs e)
        {
            SavingCount++;

            var bgw1 = new BackgroundWorker();
            var bgw2 = new BackgroundWorker();
            var bgw3 = new BackgroundWorker();
            var bgw4 = new BackgroundWorker();
            var bgw5 = new BackgroundWorker();
            var bgw6 = new BackgroundWorker();

            bgw1.DoWork += SavingTVDB_DoWork;
            bgw2.DoWork += SavingTVDB_DoWork;
            bgw3.DoWork += SavingTVDB_DoWork;
            bgw4.DoWork += SavingTVDB_DoWork;
            bgw5.DoWork += SavingTVDB_DoWork;
            bgw6.DoWork += SavingTVDB_DoWork;

            SavingTVDBMax = TvDBFactory.TvDatabase.Count;
            SavingTVDBValue = 0;

            var path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.DeleteFilesInFolder(path);

            foreach (var series in TvDBFactory.TvDatabase)
            {
                bool processed = false;

                do
                {
                    if (!bgw1.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw1.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else if (!bgw2.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw2.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else if (!bgw3.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw3.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else if (!bgw4.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw4.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else if (!bgw5.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw5.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else if (!bgw6.IsBusy)
                    {
                        SavingTVDBValue++;
                        bgw6.RunWorkerAsync(series.Value);
                        Application.DoEvents();
                        processed = true;
                    }
                    else
                    {
                        Application.DoEvents();
                        Thread.Sleep(50);
                    }
                }
                while (processed == false);
            }

            SavingCount--;
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the bgwSaveTvDB control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void bgwSaveTvDB_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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