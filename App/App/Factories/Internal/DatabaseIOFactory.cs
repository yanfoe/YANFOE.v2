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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;

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

    /// <summary>
    /// The database io factory.
    /// </summary>
    public static class DatabaseIOFactory
    {
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

        #region Public Methods

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
                    SaveMovieDB();
                    SaveMediaPathDb();
                    SaveMovieSets();
                    SaveTvDB();
                    SaveScanSeriesPick();
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the media path db.
        /// </summary>
        private static void LoadMediaPathDb()
        {
            MediaPathDBFactory.MediaPathDB = new BindingList<MediaPathModel>();

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
            FileData[] files = FastDirectoryEnumerator.GetFiles(path, "*.movie.gz", SearchOption.TopDirectoryOnly);

            MovieDBFactory.MovieDatabase.Clear();

            foreach (FileData file in files)
            {
                string json = Gzip.Decompress(file.Path);

                var movieModel = JsonConvert.DeserializeObject(json, typeof(MovieModel)) as MovieModel;

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

            FileData[] files = FastDirectoryEnumerator.GetFiles(path, "*.MovieSet.gz", SearchOption.TopDirectoryOnly);

            MovieSetManager.CurrentDatabase.Clear();

            foreach (FileData file in files)
            {
                string json = Gzip.Decompress(file.Path);
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

            FileData[] files = FastDirectoryEnumerator.GetFiles(path, "*.Series.gz", SearchOption.TopDirectoryOnly);

            TvDBFactory.TvDatabase.Clear();

            foreach (FileData file in files)
            {
                string json = Gzip.Decompress(file.Path);

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
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MediaPathDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.RemoveAllFilesInFolder(path);

            int count = 0;

            foreach (MediaPathModel mediaPath in MediaPathDBFactory.MediaPathDB)
            {
                string json = JsonConvert.SerializeObject(mediaPath);
                Gzip.CompressString(json, path + count + ".MediaPath.gz");

                count++;
            }
        }

        /// <summary>
        /// Saves the movie DB.
        /// </summary>
        private static void SaveMovieDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.RemoveAllFilesInFolder(path);

            foreach (MovieModel movieModel in MovieDBFactory.MovieDatabase)
            {
                string title = FileNaming.RemoveIllegalChars(movieModel.Title);

                string writePath = path + title + ".movie";
                string json = JsonConvert.SerializeObject(movieModel);
                Gzip.CompressString(json, writePath + ".gz");

                if (movieModel.SmallPoster != null)
                {
                    movieModel.SmallPoster.Save(path + title + ".poster.jpg");
                }

                if (movieModel.SmallFanart != null)
                {
                    movieModel.SmallFanart.Save(path + title + ".fanart.jpg");
                }
            }
        }

        /// <summary>
        /// Saves the movie sets db
        /// </summary>
        private static void SaveMovieSets()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.MovieSets + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.RemoveAllFilesInFolder(path);

            foreach (MovieSetModel set in MovieSetManager.CurrentDatabase)
            {
                string json = JsonConvert.SerializeObject(set);
                Gzip.CompressString(json, path + FileNaming.RemoveIllegalChars(set.SetName) + ".MovieSet.gz");
            }
        }

        /// <summary>
        /// Saves the scan series pick.
        /// </summary>
        private static void SaveScanSeriesPick()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.ScanSeriesPick + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.RemoveAllFilesInFolder(path);

            const string WritePath = "SeriesPick";
            string json = JsonConvert.SerializeObject(ImportTvFactory.ScanSeriesPicks);
            Gzip.CompressString(json, path + WritePath + "SeriesPick.gz");
        }

        /// <summary>
        /// Saves the tv DB.
        /// </summary>
        private static void SaveTvDB()
        {
            string path = Get.FileSystemPaths.PathDatabases + OutputName.TvDb + Path.DirectorySeparatorChar;
            Directory.CreateDirectory(path);
            Folders.RemoveAllFilesInFolder(path);

            foreach (var series in TvDBFactory.TvDatabase)
            {
                string title = FileNaming.RemoveIllegalChars(series.Key);

                // Save Series
                string writePath = path + title + ".Series";
                string json = JsonConvert.SerializeObject(series.Value);
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
            }
        }

        #endregion
    }
}