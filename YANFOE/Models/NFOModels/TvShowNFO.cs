// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="TvShowNFO.cs">
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
//   The tv show nfo model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.NFOModels
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The tv show nfo model.
    /// </summary>
    [Serializable]
    public class TvShowNFOModel
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="TvShowNFOModel" /> class.
        /// </summary>
        public TvShowNFOModel()
        {
            this.Ids = new Dictionary<string, string>();
            this.Title = string.Empty;
            this.Rating = -1;
            this.Season = -1;
            this.Episode = -1;
            this.DisplaySeason = -1;
            this.DisplayEpisode = -1;
            this.Plot = string.Empty;
            this.Thumb = new List<string>();
            this.Fanart = new List<string>();
            this.Mpaa = string.Empty;
            this.Certification = string.Empty;
            this.Trailer = new List<string>();
            this.Genre = new List<string>();
            this.Premiered = new DateTime();
            this.Company = string.Empty;
            this.Studio = string.Empty;
            this.Actors = new List<PersonModel>();
            this.Sets = new Dictionary<int, string>();
            this.FileInfoModel = new FileInfoModel();
            this.Episodes = new EpisodeDetailsModel();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets Actors.
        /// </summary>
        public List<PersonModel> Actors { get; set; }

        /// <summary>
        ///   Gets or sets Certification.
        /// </summary>
        public string Certification { get; set; }

        /// <summary>
        ///   Gets or sets Company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        ///   Gets or sets DisplayEpisode.
        /// </summary>
        public int DisplayEpisode { get; set; }

        /// <summary>
        ///   Gets or sets DisplaySeason.
        /// </summary>
        public int DisplaySeason { get; set; }

        /// <summary>
        ///   Gets or sets Episode.
        /// </summary>
        public int Episode { get; set; }

        /// <summary>
        ///   Gets or sets Episodes.
        /// </summary>
        public EpisodeDetailsModel Episodes { get; set; }

        /// <summary>
        ///   Gets or sets Fanart.
        /// </summary>
        public List<string> Fanart { get; set; }

        /// <summary>
        ///   Gets or sets FileInfoModel.
        /// </summary>
        public FileInfoModel FileInfoModel { get; set; }

        /// <summary>
        ///   Gets or sets Genre.
        /// </summary>
        public List<string> Genre { get; set; }

        /// <summary>
        ///   Gets or sets Ids.
        /// </summary>
        public Dictionary<string, string> Ids { get; set; }

        /// <summary>
        ///   Gets or sets Mpaa.
        /// </summary>
        public string Mpaa { get; set; }

        /// <summary>
        ///   Gets or sets Plot.
        /// </summary>
        public string Plot { get; set; }

        /// <summary>
        ///   Gets or sets Premiered.
        /// </summary>
        public DateTime Premiered { get; set; }

        /// <summary>
        ///   Gets or sets Rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        ///   Gets or sets Season.
        /// </summary>
        public int Season { get; set; }

        /// <summary>
        ///   Gets or sets Sets.
        /// </summary>
        public Dictionary<int, string> Sets { get; set; }

        /// <summary>
        ///   Gets or sets Studio.
        /// </summary>
        public string Studio { get; set; }

        /// <summary>
        ///   Gets or sets Thumb.
        /// </summary>
        public List<string> Thumb { get; set; }

        /// <summary>
        ///   Gets or sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///   Gets or sets Trailer.
        /// </summary>
        public List<string> Trailer { get; set; }

        #endregion
    }
}