// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Banner.cs">
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
//   The banner type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.TvModels.TVDB
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Xml;

    using YANFOE.Tools.Xml;

    #endregion

    /// <summary>
    ///   The banner type.
    /// </summary>
    public enum BannerType
    {
        /// <summary>
        ///   The fanart.
        /// </summary>
        Fanart, 

        /// <summary>
        ///   The poster.
        /// </summary>
        Poster, 

        /// <summary>
        ///   The season.
        /// </summary>
        Season, 

        /// <summary>
        ///   The series.
        /// </summary>
        Series, 

        /// <summary>
        ///   No type specified.
        /// </summary>
        None
    }

    /// <summary>
    ///   The banner type 2.
    /// </summary>
    public enum BannerType2
    {
        /// <summary>
        ///   Resolution 1920 x 1080.
        /// </summary>
        r1920x1080, 

        /// <summary>
        ///   Resolution 1280 x 720.
        /// </summary>
        r1280x720, 

        /// <summary>
        ///   Resolution 680 x 1000.
        /// </summary>
        r680x1000, 

        /// <summary>
        ///   Season type.
        /// </summary>
        season, 

        /// <summary>
        ///   Seasonwide type.
        /// </summary>
        seasonwide, 

        /// <summary>
        ///   Graphical type.
        /// </summary>
        graphical, 

        /// <summary>
        ///   Text type.
        /// </summary>
        text, 

        /// <summary>
        ///   Blank type.
        /// </summary>
        blank
    }

    /// <summary>
    ///   The banner.
    /// </summary>
    [Serializable]
    public class Banner
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Banner" /> class.
        /// </summary>
        public Banner()
        {
            this.Fanart = new List<BannerDetails>();
            this.Poster = new List<BannerDetails>();
            this.Season = new List<BannerDetails>();
            this.Series = new List<BannerDetails>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Fanart.
        /// </summary>
        public List<BannerDetails> Fanart { get; set; }

        /// <summary>
        ///   Gets or sets Poster.
        /// </summary>
        public List<BannerDetails> Poster { get; set; }

        /// <summary>
        ///   Gets or sets Season.
        /// </summary>
        public List<BannerDetails> Season { get; set; }

        /// <summary>
        ///   Gets or sets Series.
        /// </summary>
        public List<BannerDetails> Series { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Populates the object with specified XML.
        /// </summary>
        /// <param name="xml">
        /// The XML used to populate the object.. 
        /// </param>
        public void Populate(string xml)
        {
            this.Fanart.Clear();
            this.Poster.Clear();
            this.Season.Clear();
            this.Series.Clear();

            var docMain = new XmlDocument();
            docMain.LoadXml(xml);

            var nodes = docMain.GetElementsByTagName("Banner");

            foreach (XmlNode node in nodes)
            {
                var doc = new XmlDocument();
                doc.LoadXml(node.OuterXml);

                var bannerDetails = new BannerDetails
                    {
                        ID = XRead.GetUInt(doc, "id"), 
                        BannerPath = XRead.GetString(doc, "BannerPath"), 
                        Colors = XRead.GetString(doc, "Colors"), 
                        Language = XRead.GetString(doc, "Language"), 
                        SeriesName = XRead.GetString(doc, "SeriesName"), 
                        ThumbnailPath = XRead.GetString(doc, "ThumbnailPath"), 
                        VignettePath = XRead.GetString(doc, "VignettePath"), 
                        Season = XRead.GetString(doc, "Season"), 
                        Rating = XRead.GetString(doc, "Rating"), 
                        RatingCount = XRead.GetString(doc, "RatingCount")
                    };

                switch (XRead.GetString(doc, "BannerType2"))
                {
                    case "1920x1080":
                        bannerDetails.BannerType2 = BannerType2.r1920x1080;
                        break;
                    case "1280x720":
                        bannerDetails.BannerType2 = BannerType2.r1280x720;
                        break;
                    case "600x1000":
                        bannerDetails.BannerType2 = BannerType2.r680x1000;
                        break;
                    case "season":
                        bannerDetails.BannerType2 = BannerType2.season;
                        break;
                    case "seasonwide":
                        bannerDetails.BannerType2 = BannerType2.seasonwide;
                        break;
                    case "graphical":
                        bannerDetails.BannerType2 = BannerType2.graphical;
                        break;
                    case "text":
                        bannerDetails.BannerType2 = BannerType2.text;
                        break;
                    case "blank":
                        bannerDetails.BannerType2 = BannerType2.blank;
                        break;
                }

                switch (XRead.GetString(doc, "BannerType"))
                {
                    case "fanart":
                        bannerDetails.BannerType = BannerType.Fanart;
                        this.Fanart.Add(bannerDetails);
                        break;
                    case "poster":
                        bannerDetails.BannerType = BannerType.Poster;
                        this.Poster.Add(bannerDetails);
                        break;
                    case "season":
                        bannerDetails.BannerType = BannerType.Season;
                        this.Season.Add(bannerDetails);
                        break;
                    case "series":
                        bannerDetails.BannerType = BannerType.Series;
                        this.Series.Add(bannerDetails);
                        break;
                }
            }
        }

        #endregion
    }
}