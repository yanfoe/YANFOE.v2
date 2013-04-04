// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="SearchDetails.cs">
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
//   The search details.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.TVDB
{
    #region Required Namespaces

    using System;
    using System.Drawing;
    using System.Xml;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Scrapers.TV;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;
    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    ///   The search details.
    /// </summary>
    [Serializable]
    public class SearchDetails : ModelBase
    {
        #region Fields

        /// <summary>
        ///   Gets or sets Banner.
        /// </summary>
        private string banner;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="SearchDetails" /> class.
        /// </summary>
        public SearchDetails()
        {
            this.UseId = string.Empty;

            this.SeriesID = string.Empty;
            this.Language = string.Empty;
            this.SeriesName = string.Empty;
            this.Banner = string.Empty;
            this.OverView = string.Empty;
            this.FirstAired = new DateTime();
            this.Imdbid = string.Empty;
            this.ID = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the banner.
        /// </summary>
        public string Banner
        {
            get
            {
                return this.banner;
            }

            set
            {
                this.banner = value;
                this.OnPropertyChanged("Banner");
                this.OnPropertyChanged("BannerImage");
            }
        }

        /// <summary>
        /// Gets the banner image.
        /// </summary>
        public Image BannerImage
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Banner))
                {
                    string url = this.Banner;
                    url = TheTvdb.ReturnBannerDownloadPath(url, true);

                    string path = Downloader.ProcessDownload(url, DownloadType.Binary, Section.Tv);

                    return ImageHandler.LoadImage(path);
                }

                return null;
            }
        }

        /// <summary>
        ///   Gets or sets Banners.
        /// </summary>
        public Banner Banners { get; set; }

        /// <summary>
        ///   Gets or sets FirstAired.
        /// </summary>
        public DateTime? FirstAired { get; set; }

        /// <summary>
        ///   Gets or sets ID.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        ///   Gets or sets Imdbid.
        /// </summary>
        public string Imdbid { get; set; }

        /// <summary>
        ///   Gets or sets Language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        ///   Gets or sets OverView.
        /// </summary>
        public string OverView { get; set; }

        /// <summary>
        ///   Gets or sets SeriesID.
        /// </summary>
        public string SeriesID { get; set; }

        /// <summary>
        ///   Gets or sets SeriesName.
        /// </summary>
        public string SeriesName { get; set; }

        /// <summary>
        ///   Gets or sets UseId.
        /// </summary>
        public string UseId { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Populates the series detail.
        /// </summary>
        /// <param name="doc">
        /// The xml document 
        /// </param>
        public void PopulateSeriesDetail(XmlDocument doc)
        {
            this.SeriesID = XRead.GetString(doc, "seriesid");
            this.Language = XRead.GetString(doc, "language");
            this.SeriesName = XRead.GetString(doc, "SeriesName");
            this.Banner = XRead.GetString(doc, "banner");
            this.OverView = XRead.GetString(doc, "Overview");
            this.FirstAired = XRead.GetDateTime(doc, "FirstAired", "yyyy-MM-dd");
            this.Imdbid = XRead.GetString(doc, "IMDB_ID");
            this.ID = XRead.GetString(doc, "id");
        }

        #endregion
    }
}