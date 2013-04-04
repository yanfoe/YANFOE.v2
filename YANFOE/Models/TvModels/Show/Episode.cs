// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Episode.cs">
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
//   The episode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.Show
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Newtonsoft.Json;

    using YANFOE.Factories;
    using YANFOE.Factories.Apps.MediaInfo;
    using YANFOE.Factories.Internal;
    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Models.NFOModels;
    using YANFOE.Properties;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.Text;
    using YANFOE.Tools.UI;
    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    ///   The episode.
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class Episode : ModelBase
    {
        #region Fields

        /// <summary>
        ///   The episode guid.
        /// </summary>
        private readonly string guid;

        /// <summary>
        ///   The absolute number.
        /// </summary>
        private string absoluteNumber;

        /// <summary>
        ///   The combined episodenumber.
        /// </summary>
        private double? combinedEpisodenumber;

        /// <summary>
        ///   The combined season.
        /// </summary>
        private int? combinedSeason;

        /// <summary>
        ///   The dvd chapter.
        /// </summary>
        private int? dvdChapter;

        /// <summary>
        ///   The dvd discid.
        /// </summary>
        private int? dvdDiscid;

        /// <summary>
        ///   The dvd episodenumber.
        /// </summary>
        private double? dvdEpisodenumber;

        /// <summary>
        ///   The dvd season.
        /// </summary>
        private int? dvdSeason;

        /// <summary>
        ///   The episode flag image.
        /// </summary>
        private int? episodeImgFlag;

        /// <summary>
        ///   The episode name.
        /// </summary>
        private string episodeName;

        /// <summary>
        ///   The episode number.
        /// </summary>
        private int? episodeNumber;

        /// <summary>
        ///   The episode screenshot path.
        /// </summary>
        private string episodeScreenshotPath;

        /// <summary>
        ///   The filename.
        /// </summary>
        private string episodeScreenshotUrl;

        /// <summary>
        ///   The file path.
        /// </summary>
        private MediaModel filePath;

        /// <summary>
        ///   The first aired.
        /// </summary>
        private DateTime? firstAired;

        /// <summary>
        ///   The episode object id.
        /// </summary>
        private uint? id;

        /// <summary>
        ///   The imdbid.
        /// </summary>
        private string imdbid;

        /// <summary>
        ///   The is locked.
        /// </summary>
        private bool isLocked;

        /// <summary>
        ///   The language.
        /// </summary>
        private string language;

        /// <summary>
        ///   The lastupdated.
        /// </summary>
        private string lastupdated;

        /// <summary>
        ///   The overview.
        /// </summary>
        private string overview;

        /// <summary>
        ///   The production code.
        /// </summary>
        private string productionCode;

        /// <summary>
        ///   The rating.
        /// </summary>
        private double? rating;

        /// <summary>
        ///   The season number.
        /// </summary>
        private int? seasonNumber;

        /// <summary>
        ///   The seasonid.
        /// </summary>
        private uint? seasonid;

        /// <summary>
        ///   The seriesid.
        /// </summary>
        private uint? seriesid;

        /// <summary>
        ///   The video source.
        /// </summary>
        private string videoSource;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Episode" /> class.
        /// </summary>
        public Episode()
        {
            this.guid = GenerateRandomID.Generate();

            this.ID = null;
            this.CombinedEpisodenumber = null;
            this.CombinedSeason = null;
            this.DvdChapter = null;
            this.DvdDiscid = null;
            this.DvdEpisodenumber = null;
            this.DvdSeason = null;
            this.EpisodeImgFlag = null;
            this.EpisodeNumber = 0;
            this.FirstAired = null;
            this.IMDBID = string.Empty;
            this.Language = string.Empty;
            this.Overview = string.Empty;
            this.ProductionCode = string.Empty;
            this.Rating = null;
            this.SeasonNumber = 0;
            this.AbsoluteNumber = string.Empty;
            this.EpisodeScreenshotUrl = string.Empty;
            this.Lastupdated = string.Empty;
            this.Seasonid = null;
            this.Seriesid = null;
            this.Director = new ThreadedBindingList<PersonModel>();
            this.Writers = new ThreadedBindingList<PersonModel>();
            this.FilePath = new MediaModel();
            this.GuestStars = new ThreadedBindingList<PersonModel>();
            this.FileInfo = new FileInfoModel();

            this.FileInfo.PropertyChanged += (sender, e) =>
                {
                    DatabaseIOFactory.DatabaseDirty = true;
                    this.ChangedText = true;
                };
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when [media info changed].
        /// </summary>
        [field: NonSerialized]
        public event EventHandler MediaInfoChanged;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets AbsoluteNumber.
        /// </summary>
        public string AbsoluteNumber
        {
            get
            {
                return this.absoluteNumber;
            }

            set
            {
                if (this.absoluteNumber != value)
                {
                    this.absoluteNumber = value;
                    this.OnPropertyChanged("AbsoluteNumber", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedScreenshot.
        /// </summary>
        public bool ChangedScreenshot { get; set; }

        /// <summary>
        ///   Gets or sets a value indicating whether ChangedText.
        /// </summary>
        public bool ChangedText { get; set; }

        /// <summary>
        ///   Gets or sets CombinedEpisodenumber.
        /// </summary>
        public double? CombinedEpisodenumber
        {
            get
            {
                return this.combinedEpisodenumber;
            }

            set
            {
                if (this.combinedEpisodenumber != value)
                {
                    this.combinedEpisodenumber = value;
                    this.OnPropertyChanged("CombinedEpisodenumber", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets CombinedSeason.
        /// </summary>
        public int? CombinedSeason
        {
            get
            {
                return this.combinedSeason;
            }

            set
            {
                if (this.combinedSeason != value)
                {
                    this.combinedSeason = value;
                    this.OnPropertyChanged("CombinedSeason", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets CurrentFilePathStatusImage.
        /// </summary>
        public Image CurrentFilePathStatusImage
        {
            get
            {
                return string.IsNullOrEmpty(this.CurrentFilenameAndPath) ? Resources.reddrive : Resources.greendrive;
            }
        }

        /// <summary>
        ///   Gets or sets CurrentFilenameAndPath.
        /// </summary>
        public string CurrentFilenameAndPath
        {
            get
            {
                return this.filePath.PathAndFilename;
            }

            set
            {
                if (this.filePath.PathAndFilename != value)
                {
                    this.FilePath.PathAndFilename = value;
                    this.OnPropertyChanged("Path", true);
                    this.OnPropertyChanged("CurrentFilenameAndPath", true);
                    this.OnPropertyChanged("CurrentFilePathStatusImage");
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the director.
        /// </summary>
        /// <value> The director. </value>
        public ThreadedBindingList<PersonModel> Director { get; set; }

        /// <summary>
        ///   Gets or sets DirectorAsString.
        /// </summary>
        public string DirectorAsString
        {
            get
            {
                return this.Director.ToString(',');
            }

            set
            {
                if (this.Director != value.ToPersonList())
                {
                    this.Director = value.ToPersonList();
                    this.OnPropertyChanged("DirectorAsString", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets DvdChapter.
        /// </summary>
        public int? DvdChapter
        {
            get
            {
                return this.dvdChapter;
            }

            set
            {
                if (this.dvdChapter != value)
                {
                    this.dvdChapter = value;
                    this.OnPropertyChanged("DvdChapter", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the DVD discid.
        /// </summary>
        /// <value> The DVD discid. </value>
        public int? DvdDiscid
        {
            get
            {
                return this.dvdDiscid;
            }

            set
            {
                if (this.dvdDiscid != value)
                {
                    this.dvdDiscid = value;
                    this.OnPropertyChanged("DvdDiscid", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the DVD episodenumber.
        /// </summary>
        /// <value> The DVD episodenumber. </value>
        public double? DvdEpisodenumber
        {
            get
            {
                return this.dvdEpisodenumber;
            }

            set
            {
                if (this.dvdEpisodenumber != value)
                {
                    this.dvdEpisodenumber = value;
                    this.OnPropertyChanged("DvdEpisodenumber", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets the DVD season.
        /// </summary>
        /// <value> The DVD season. </value>
        public int? DvdSeason
        {
            get
            {
                return this.dvdSeason;
            }

            set
            {
                if (this.dvdSeason != value)
                {
                    this.dvdSeason = value;
                    this.OnPropertyChanged("DvdSeason", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets episodeImgFlag.
        /// </summary>
        public int? EpisodeImgFlag
        {
            get
            {
                return this.episodeImgFlag;
            }

            set
            {
                if (this.episodeImgFlag != value)
                {
                    this.episodeImgFlag = value;
                    this.OnPropertyChanged("episodeImgFlag", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets EpisodeName.
        /// </summary>
        public string EpisodeName
        {
            get
            {
                return this.episodeName;
            }

            set
            {
                if (this.episodeName != value)
                {
                    this.episodeName = value;
                    this.OnPropertyChanged("EpisodeName", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets EpisodeNumber.
        /// </summary>
        public int? EpisodeNumber
        {
            get
            {
                return this.episodeNumber;
            }

            set
            {
                if (this.episodeNumber != value)
                {
                    this.episodeNumber = value;
                    this.OnPropertyChanged("EpisodeNumber", true);
                    this.OnPropertyChanged("FilePath");
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets the fanart image.
        /// </summary>
        [JsonIgnore]
        public Image EpisodeScreenshotImage
        {
            get
            {
                string url;

                if (string.IsNullOrEmpty(this.EpisodeScreenshotPath))
                {
                    url = Downloader.ProcessDownload(
                        TVDBFactory.Instance.GetImageUrl(this.EpisodeScreenshotUrl), DownloadType.Binary, Section.Movies);
                    this.EpisodeScreenshotPath = url;
                }
                else
                {
                    url = this.EpisodeScreenshotPath;
                }

                if (!File.Exists(url))
                {
                    return null;
                }

                return ImageHandler.LoadImage(url);
            }
        }

        /// <summary>
        ///   Gets or sets EpisodeScreenshotPath.
        /// </summary>
        public string EpisodeScreenshotPath
        {
            get
            {
                return this.episodeScreenshotPath;
            }

            set
            {
                if (this.episodeScreenshotPath != value)
                {
                    this.episodeScreenshotPath = value;
                    this.OnPropertyChanged("EpisodeScreenshotPath", true);
                    this.ChangedText = true;
                    if (!string.IsNullOrEmpty(this.episodeScreenshotPath))
                    {
                        this.EpisodeScreenshotUrl = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets or sets Filename.
        /// </summary>
        public string EpisodeScreenshotUrl
        {
            get
            {
                return this.episodeScreenshotUrl;
            }

            set
            {
                if (this.episodeScreenshotUrl != value)
                {
                    this.episodeScreenshotUrl = value;
                    this.OnPropertyChanged("EpisodeScreenshotUrl", true);
                    this.OnPropertyChanged("FilePath");
                    this.ChangedText = true;
                    if (!string.IsNullOrEmpty(this.episodeScreenshotUrl))
                    {
                        this.EpisodeScreenshotPath = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        ///   Gets a value indicating whether file assigned.
        /// </summary>
        public bool FileAssigned
        {
            get
            {
                return !string.IsNullOrEmpty(this.FilePath.PathAndFilename);
            }
        }

        /// <summary>
        ///   Gets or sets the file info.
        /// </summary>
        public FileInfoModel FileInfo { get; set; }

        /// <summary>
        ///   Gets or sets Path.
        /// </summary>
        public MediaModel FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                if (this.filePath != value)
                {
                    this.filePath = value;
                    this.OnPropertyChanged("Path");
                    this.OnPropertyChanged("CurrentFilenameAndPath", true);
                    this.OnPropertyChanged("CurrentFilePathStatusImage");
                    this.OnPropertyChanged("FileAssigned");
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets FirstAired.
        /// </summary>
        public DateTime? FirstAired
        {
            get
            {
                return this.firstAired;
            }

            set
            {
                if (this.firstAired != value)
                {
                    this.firstAired = value;
                    this.OnPropertyChanged("FirstAired", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets GuestStars.
        /// </summary>
        public ThreadedBindingList<PersonModel> GuestStars { get; set; }

        /// <summary>
        ///   Gets Guid.
        /// </summary>
        public string Guid
        {
            get
            {
                return this.guid;
            }
        }

        /// <summary>
        ///   Gets or sets Id.
        /// </summary>
        public uint? ID
        {
            get
            {
                return this.id;
            }

            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged("ID", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets IMDBID.
        /// </summary>
        public string IMDBID
        {
            get
            {
                return this.imdbid;
            }

            set
            {
                if (this.imdbid != value)
                {
                    this.imdbid = value;
                    this.OnPropertyChanged("IMDBID", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether IsLocked.
        /// </summary>
        /// <value> <c>true</c> if this instance is locked; otherwise, <c>false</c> . </value>
        public bool IsLocked
        {
            get
            {
                return this.isLocked;
            }

            set
            {
                this.isLocked = value;
                this.OnPropertyChanged("IsLocked", true);
                this.OnPropertyChanged("NotLocked", true);
                this.OnPropertyChanged("LockedImage");
            }
        }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                if (this.language != value)
                {
                    this.language = value;
                    this.OnPropertyChanged("Language", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Lastupdated.
        /// </summary>
        public string Lastupdated
        {
            get
            {
                return this.lastupdated;
            }

            set
            {
                if (this.lastupdated != value)
                {
                    this.lastupdated = value;
                    this.OnPropertyChanged("Lastupdated", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets the locked image.
        /// </summary>
        public Image LockedImage
        {
            get
            {
                return this.IsLocked ? Resources.locked32 : Resources.unlock32;
            }
        }

        /// <summary>
        ///   Gets the media info image.
        /// </summary>
        [JsonIgnore]
        public Image MediaInfoImage
        {
            get
            {
                return this.ContainsMediaInfo() ? Resources.search32 : Resources.searchred;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether not locked.
        /// </summary>
        public bool NotLocked
        {
            get
            {
                return !this.isLocked;
            }
        }

        /// <summary>
        ///   Gets a value indicating whether NotSecondary.
        /// </summary>
        public bool NotSecondary
        {
            get
            {
                return this.SecondaryTo == null;
            }
        }

        /// <summary>
        ///   Gets or sets Overview.
        /// </summary>
        public string Overview
        {
            get
            {
                return this.overview;
            }

            set
            {
                if (this.overview != value)
                {
                    this.overview = value;
                    this.OnPropertyChanged("Overview", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets ProductionCode.
        /// </summary>
        public string ProductionCode
        {
            get
            {
                return this.productionCode;
            }

            set
            {
                if (this.productionCode != value)
                {
                    this.productionCode = value;
                    this.OnPropertyChanged("ProductionCode", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Rating.
        /// </summary>
        public double? Rating
        {
            get
            {
                return this.rating;
            }

            set
            {
                if (this.rating != value)
                {
                    this.rating = value;
                    this.OnPropertyChanged("Rating", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets SeasonNumber.
        /// </summary>
        public int? SeasonNumber
        {
            get
            {
                return this.seasonNumber;
            }

            set
            {
                if (this.seasonNumber != value)
                {
                    this.seasonNumber = value;
                    this.OnPropertyChanged("SeasonNumber", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Seasonid.
        /// </summary>
        public uint? Seasonid
        {
            get
            {
                return this.seasonid;
            }

            set
            {
                if (this.seasonid != value)
                {
                    this.seasonid = value;
                    this.OnPropertyChanged("Seasonid", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets a value indicating whether Secondary.
        /// </summary>
        public bool Secondary
        {
            get
            {
                return this.SecondaryTo != null;
            }
        }

        /// <summary>
        ///   Gets or sets SecondaryTo.
        /// </summary>
        public int? SecondaryTo { get; set; }

        /// <summary>
        ///   Gets or sets Seriesid.
        /// </summary>
        public uint? Seriesid
        {
            get
            {
                return this.seriesid;
            }

            set
            {
                if (this.seriesid != value)
                {
                    this.seriesid = value;
                    this.OnPropertyChanged("Seriesid", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets Status.
        /// </summary>
        public Image Status
        {
            get
            {
                if (string.IsNullOrEmpty(this.FilePath.PathAndFilename))
                {
                    return Resources.promo_red_faded16;
                }

                return Resources.promo_green_faded16;
            }
        }

        /// <summary>
        ///   Gets or sets VideoSource.
        /// </summary>
        public string VideoSource
        {
            get
            {
                return this.videoSource;
            }

            set
            {
                if (this.videoSource != value)
                {
                    this.videoSource = value;
                    this.OnPropertyChanged("VideoSource", true);
                    this.ChangedText = true;
                }
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether watched.
        /// </summary>
        [JsonIgnore]
        public bool Watched
        {
            get
            {
                if (string.IsNullOrEmpty(this.FilePath.FileName))
                {
                    return false;
                }

                return File.Exists(Path.Combine(new[] { this.FilePath.PathAndFilename + ".watched" }));
            }

            set
            {
                if (string.IsNullOrEmpty(this.FilePath.FileName))
                {
                    return;
                }

                var path = Path.Combine(new[] { this.FilePath.PathAndFilename + ".watched" });

                var exists = File.Exists(path);

                if (exists != value)
                {
                    if (exists)
                    {
                        File.Delete(path);
                    }
                    else
                    {
                        IO.WriteTextToFile(path, " ");
                    }

                    this.OnPropertyChanged("Watched");
                    this.OnPropertyChanged("WatchedImage", true);
                }
            }
        }

        /// <summary>
        ///   Gets the watched image.
        /// </summary>
        [JsonIgnore]
        public Image WatchedImage
        {
            get
            {
                return this.Watched ? Resources.watched_green : Resources.watched_red;
            }
        }

        /// <summary>
        ///   Gets or sets Writers.
        /// </summary>
        public ThreadedBindingList<PersonModel> Writers { get; set; }

        /// <summary>
        ///   Gets or sets WritersAsString.
        /// </summary>
        public string WritersAsString
        {
            get
            {
                return this.Writers.ToString(',');
            }

            set
            {
                if (this.Writers != value.ToPersonList())
                {
                    this.Writers = value.ToPersonList();
                    this.OnPropertyChanged("DirectorAsString", true);
                    this.ChangedText = true;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The do media info lookup.
        /// </summary>
        public void DoMediaInfoLookup()
        {
            var bgw = new BackgroundWorker();
            bgw.DoWork += (bgwSender, bgwE) =>
                {
                    var result = MediaInfoFactory.DoMediaInfoScan(this.FilePath.PathAndFilename);

                    bgwE.Result = result;
                    MediaInfoFactory.InjectResponseModel(result, this.FileInfo);
                };

            bgw.RunWorkerCompleted += (bgwSender, bgwE) =>
                {
                    this.OnPropertyChanged("MediaInfoImage");
                    this.InvokeMediaInfoChanged(new EventArgs());
                };

            bgw.RunWorkerAsync();
        }

        /// <summary>
        ///   Gets the season the episode belongs to.
        /// </summary>
        /// <returns> The season the episode belongs to. </returns>
        public Season GetSeason()
        {
            return (from series in TVDBFactory.Instance.TVDatabase
                    from season in series.Seasons
                    where season.Episodes.Any(episode => episode == this)
                    select season).FirstOrDefault();
        }

        /// <summary>
        ///   Gets the series the episode belongs to.
        /// </summary>
        /// <returns> The series the episode belongs to </returns>
        public Series GetSeries()
        {
            return (from series in TVDBFactory.Instance.TVDatabase
                    from season in series.Seasons
                    from episode in season.Episodes
                    where episode == this
                    select series).FirstOrDefault();
        }

        /// <summary>
        ///   Gets the name of the series the episode belongs to.
        /// </summary>
        /// <returns> The name of the series the episode belongs to. </returns>
        public string GetSeriesName()
        {
            foreach (var series in TVDBFactory.Instance.TVDatabase)
            {
                if (series.Seasons.SelectMany(season => season.Episodes).Any(episode => episode == this))
                {
                    return series.SeriesName;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// The invoke media info changed.
        /// </summary>
        /// <param name="e">
        /// The e. 
        /// </param>
        public void InvokeMediaInfoChanged(EventArgs e)
        {
            EventHandler handler = this.MediaInfoChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Populates the current episode with episodeXml.
        /// </summary>
        /// <param name="xml">
        /// The XML used to populate the values of the episode object. 
        /// </param>
        /// <returns>
        /// Population sucessful. 
        /// </returns>
        public bool Populate(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);

            string title = XRead.GetString(doc, "EpisodeName");

            if (string.IsNullOrEmpty(title))
            {
                return false;
            }

            this.ID = XRead.GetUInt(doc, "Id");
            this.CombinedEpisodenumber = XRead.GetDouble(doc, "Combined_episodenumber");
            this.CombinedSeason = XRead.GetInt(doc, "Combined_season");
            this.DvdChapter = XRead.GetInt(doc, "DVD_chapter");
            this.DvdDiscid = XRead.GetInt(doc, "DVD_discid");
            this.DvdEpisodenumber = XRead.GetDouble(doc, "DVD_episodenumber");
            this.DvdSeason = XRead.GetInt(doc, "DVD_season");
            this.Director = XRead.GetString(doc, "Director").ToPersonList('|');
            this.EpisodeImgFlag = XRead.GetInt(doc, "EpImgFlag");
            this.EpisodeName = title;
            this.EpisodeNumber = XRead.GetInt(doc, "EpisodeNumber");
            this.FirstAired = XRead.GetDateTime(doc, "FirstAired", "yyyy-MM-dd");
            this.GuestStars = XRead.GetString(doc, "GuestStars").ToPersonList('|', "http://www.thetvdb.com/banners/");
            this.IMDBID = XRead.GetString(doc, "IMDB_ID");
            this.Language = XRead.GetString(doc, "Language");
            this.Overview = XRead.GetString(doc, "Overview");
            this.ProductionCode = XRead.GetString(doc, "ProductionCode");
            this.Rating = XRead.GetDouble(doc, "Rating");
            this.SeasonNumber = XRead.GetInt(doc, "SeasonNumber");
            this.Writers = XRead.GetString(doc, "Director").ToPersonList('|');
            this.AbsoluteNumber = XRead.GetString(doc, "absolute_number");
            this.EpisodeScreenshotUrl = XRead.GetString(doc, "filename");
            this.Lastupdated = XRead.GetString(doc, "lastupdated");
            this.Seasonid = XRead.GetUInt(doc, "seasonid");
            this.Seriesid = XRead.GetUInt(doc, "seriesid");

            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        ///   The contains media info.
        /// </summary>
        /// <returns> The <see cref="bool" /> . </returns>
        private bool ContainsMediaInfo()
        {
            return File.Exists(this.FilePath.PathAndFilename + ".mediainfo");
        }

        #endregion
    }
}