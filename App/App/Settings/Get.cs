// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Get.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using BitFactory.Logging;

    using DevExpress.XtraEditors;

    using Newtonsoft.Json;

    using YANFOE.InternalApps.Logs;
    using YANFOE.Settings.UserSettings;
    using YANFOE.Tools.Text;

    /// <summary>
    /// Collection of settings avalable to YANFOE.
    /// </summary>
    public static class Get
    {
        #region Constants and Fields

        /// <summary>
        /// The countries settings
        /// </summary>
        private static Countries countries;

        /// <summary>
        /// The file system paths settings
        /// </summary>
        private static FileSystemPaths fileSystemPaths;

        /// <summary>
        /// The genres settings
        /// </summary>
        private static Genres genres;

        /// <summary>
        /// The image settings
        /// </summary>
        private static Image image;

        /// <summary>
        /// The in out collection settings
        /// </summary>
        private static InOut inOutCollection;

        /// <summary>
        /// The keywords settings
        /// </summary>
        private static Keywords keywords;

        /// <summary>
        /// The localization settings
        /// </summary>
        private static Localization localization;

        /// <summary>
        /// The log settings
        /// </summary>
        private static LogSettings logSettings;

        /// <summary>
        /// The look and feel settings
        /// </summary>
        private static LookAndFeel lookAndFeel;

        /// <summary>
        /// The media settings
        /// </summary>
        private static Media media;

        private static MediaInfoSettings mediaInfo;

        /// <summary>
        /// The scraper settings
        /// </summary>
        private static Scraper scraper;

        /// <summary>
        /// The UiSettings settings
        /// </summary>
        private static UiSettings ui;

        /// <summary>
        /// The web settings
        /// </summary>
        private static WebSettings web;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Get"/> class.
        /// </summary>
        static Get()
        {
            LoadAll();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Countries.
        /// </summary>
        public static Countries Countries
        {
            get
            {
                return countries ?? (countries = new Countries());
            }

            set
            {
                countries = value;
            }
        }

        /// <summary>
        /// Gets or sets a collection of IO paths.
        /// </summary>
        /// <value>The file system paths.</value>
        public static FileSystemPaths FileSystemPaths
        {
            get
            {
                return fileSystemPaths ?? (fileSystemPaths = new FileSystemPaths());
            }

            set
            {
                fileSystemPaths = value;
            }
        }

        /// <summary>
        /// Gets or sets Genres.
        /// </summary>
        public static Genres Genres
        {
            get
            {
                return genres ?? (genres = new Genres());
            }

            set
            {
                genres = value;
            }
        }

        /// <summary>
        /// Gets or sets settings relating to images.
        /// </summary>
        /// <value>The image.</value>
        public static Image Image
        {
            get
            {
                return image ?? (image = new Image());
            }

            set
            {
                image = value;
            }
        }

        /// <summary>
        /// Gets or sets the in out collection.
        /// </summary>
        /// <value>The in out collection.</value>
        public static InOut InOutCollection
        {
            get
            {
                return inOutCollection ?? (inOutCollection = new InOut());
            }

            set
            {
                inOutCollection = value;
            }
        }

        /// <summary>
        /// Gets or sets keywords that will be used for filtering.
        /// </summary>
        /// <value>The keywords.</value>
        public static Keywords Keywords
        {
            get
            {
                return keywords ?? (keywords = new Keywords());
            }

            set
            {
                keywords = value;
            }
        }

        /// <summary>
        /// Gets or sets the localization.
        /// </summary>
        /// <value>The localization.</value>
        public static Localization Localization
        {
            get
            {
                return localization ?? (localization = new Localization());
            }

            set
            {
                localization = value;
            }
        }

        public static LogSettings LogSettings
        {
            get
            {
                return logSettings ?? (logSettings = new LogSettings());
            }

            set
            {
                logSettings = value;
            }
        }

        /// <summary>
        /// Gets or sets LookAndFeel.
        /// </summary>
        public static LookAndFeel LookAndFeel
        {
            get
            {
                return lookAndFeel ?? (lookAndFeel = new LookAndFeel());
            }

            set
            {
                lookAndFeel = value;
            }
        }

        /// <summary>
        /// Gets or sets Media.
        /// </summary>
        public static Media Media
        {
            get
            {
                return media ?? (media = new Media());
            }

            set
            {
                media = value;
            }
        }

        public static MediaInfoSettings MediaInfo
        {
            get
            {
                return mediaInfo ?? (mediaInfo = new MediaInfoSettings());
            }

            set
            {
                mediaInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets Scraper.
        /// </summary>
        public static Scraper Scraper
        {
            get
            {
                return scraper ?? (scraper = new Scraper());
            }

            set
            {
                scraper = value;
            }
        }

        /// <summary>
        /// Gets or sets Ui.
        /// </summary>
        public static UiSettings Ui
        {
            get
            {
                return ui ?? (ui = new UiSettings());
            }

            set
            {
                ui = value;
            }
        }

        /// <summary>
        /// Gets or sets settings related to web.
        /// </summary>
        /// <value>The web settings object</value>
        public static WebSettings Web
        {
            get
            {
                return web ?? (web = new WebSettings());
            }

            set
            {
                web = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The load all.
        /// </summary>
        public static void LoadAll()
        {
            LoadCountrySetting();
            LoadFileSystemPathsSettings();
            LoadGenresSettings();
            LoadImageSettings();
            LoadInOutCollectionSettings();
            LoadKeywordsSettings();
            LoadLocalizationSettings();
            LoadLogSettings();
            LoadLookAndFeelSettings();
            LoadMediaSettings();
            LoadMediaInfoSettings();
            LoadScraperSettings();
            LoadUiSettings();
            LoadWebSettings();
        }

        /// <summary>
        /// Save all settings
        /// </summary>
        public static void SaveAll()
        {
            SaveSettings(
                new dynamic[]
                    {
                        Countries, FileSystemPaths, Genres, Image, InOutCollection, Keywords, Localization, LogSettings, LookAndFeel, 
                        Media, MediaInfo, Scraper, Ui, Web
                    });
        }

        #endregion

        #region Methods

        /// <summary>
        /// The load country setting.
        /// </summary>
        private static void LoadCountrySetting()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Countries.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);
                countries = JsonConvert.DeserializeObject(json, typeof(Countries)) as Countries;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Countries settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load file system paths settings.
        /// </summary>
        private static void LoadFileSystemPathsSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "FileSystemPaths.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                fileSystemPaths = JsonConvert.DeserializeObject(json, typeof(FileSystemPaths)) as FileSystemPaths;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load FileSystemPaths settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load genres settings.
        /// </summary>
        private static void LoadGenresSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Genres.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                genres = JsonConvert.DeserializeObject(json, typeof(Genres)) as Genres;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Genres settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load image settings.
        /// </summary>
        private static void LoadImageSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Image.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                image = JsonConvert.DeserializeObject(json, typeof(Image)) as Image;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Image settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load in out collection settings.
        /// </summary>
        private static void LoadInOutCollectionSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "InOut.txt";

                if (!File.Exists(path))
                {
                    inOutCollection = new InOut();
                    inOutCollection.SetCurrentSettings(YANFOE.Tools.Enums.NFOType.YAMJ);
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                inOutCollection = JsonConvert.DeserializeObject(json, typeof(InOut)) as InOut;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load InOut settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load keywords settings.
        /// </summary>
        private static void LoadKeywordsSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Keywords.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                keywords = JsonConvert.DeserializeObject(json, typeof(Keywords)) as Keywords;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Keywords settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load localization settings.
        /// </summary>
        private static void LoadLocalizationSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Localization.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                localization = JsonConvert.DeserializeObject(json, typeof(Localization)) as Localization;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Localization settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        private static void LoadLogSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "LogSettings.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                logSettings = JsonConvert.DeserializeObject(json, typeof(LogSettings)) as LogSettings;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load log settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load look and feel settings.
        /// </summary>
        private static void LoadLookAndFeelSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "LookAndFeel.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                lookAndFeel = JsonConvert.DeserializeObject(json, typeof(LookAndFeel)) as LookAndFeel;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load LookAndFeel settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        private static void LoadMediaInfoSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "MediaInfo.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                mediaInfo = JsonConvert.DeserializeObject(json, typeof(MediaInfoSettings)) as MediaInfoSettings;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load LookAndFeel settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load media settings.
        /// </summary>
        private static void LoadMediaSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Media.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                media = JsonConvert.DeserializeObject(json, typeof(Media)) as Media;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Media settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load scraper settings.
        /// </summary>
        private static void LoadScraperSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Scraper.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                scraper = JsonConvert.DeserializeObject(json, typeof(Scraper)) as Scraper;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Scraper settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load ui settings.
        /// </summary>
        private static void LoadUiSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "UiSettings.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                ui = JsonConvert.DeserializeObject(json, typeof(UiSettings)) as UiSettings;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load UiSettings settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The load web settings.
        /// </summary>
        private static void LoadWebSettings()
        {
            try
            {
                string path = FileSystemPaths.PathSettings + "Web.txt";

                if (!File.Exists(path))
                {
                    return;
                }

                string json = IO.ReadTextFromFile(path);

                web = JsonConvert.DeserializeObject(json, typeof(WebSettings)) as WebSettings;
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to load Web settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        /// <summary>
        /// The save settings.
        /// </summary>
        /// <param name="objs">The object to save</param>
        private static void SaveSettings(IEnumerable<dynamic> objs)
        {
            try
            {
                foreach (dynamic obj in objs)
                {
                    dynamic path = FileSystemPaths.PathSettings +
                                   obj.GetType().ToString().Replace("YANFOE.Settings.UserSettings.", string.Empty) +
                                   ".txt";
                    IO.WriteTextToFile(path, JsonConvert.SerializeObject(obj));
                }
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show("Failed to save settings. Please check log for more info.");
                Log.WriteToLog(LogSeverity.Error, 0, exception.Message, exception.StackTrace);
            }
        }

        #endregion
    }
}