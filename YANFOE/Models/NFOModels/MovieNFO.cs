// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieNFO.cs" company="The YANFOE Project">
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
//   The movie nfo model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.Models.NFOModels
{
    using System;
    using System.Collections.Generic;

    using YANFOE.Tools.Models;

    /// <summary>
    /// The movie nfo model.
    /// </summary>
    [Serializable]
    public class MovieNFOModel
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieNFOModel"/> class.
        /// </summary>
        public MovieNFOModel()
        {
            this.FileName = string.Empty;
            this.FilePath = string.Empty;
            this.FileLastChanged = new DateTime();
            this.Ids = new Dictionary<string, string>();
            this.Title = string.Empty;
            this.OriginalTitle = string.Empty;
            this.Rating = -1;
            this.Top250 = -1;
            this.Votes = -1;
            this.Outline = string.Empty;
            this.Plot = string.Empty;
            this.Tagline = string.Empty;
            this.Runtime = -1;
            this.Premiered = new DateTime();
            this.Thumb = new List<string>();
            this.Fanart = new List<string>();
            this.Mpaa = string.Empty;
            this.Certification = string.Empty;
            this.Playcount = -1;
            this.Watched = string.Empty;
            this.FileNameAndPath = string.Empty;
            this.Trailer = new List<string>();
            this.Genre = new List<string>();
            this.Credits = new List<PersonModel>();
            this.Directors = new List<PersonModel>();
            this.Company = string.Empty;
            this.Studio = string.Empty;
            this.Actors = new List<PersonModel>();
            this.Sets = new Dictionary<int, string>();
            this.FileInfoModel = new FileInfoModel();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Actors.
        /// </summary>
        public List<PersonModel> Actors { get; set; }

        /// <summary>
        /// Gets or sets Certification.
        /// </summary>
        public string Certification { get; set; }

        /// <summary>
        /// Gets or sets Company.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets Credits.
        /// </summary>
        public List<PersonModel> Credits { get; set; }

        // Writers

        /// <summary>
        /// Gets or sets Directors.
        /// </summary>
        public List<PersonModel> Directors { get; set; }

        /// <summary>
        /// Gets or sets Fanart.
        /// </summary>
        public List<string> Fanart { get; set; }

        /// <summary>
        /// Gets or sets FileInfoModel.
        /// </summary>
        public FileInfoModel FileInfoModel { get; set; }

        /// <summary>
        /// Gets or sets FileLastChanged.
        /// </summary>
        public DateTime FileLastChanged { get; set; }

        /// <summary>
        /// Gets or sets FileName.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets FileNameAndPath.
        /// </summary>
        public string FileNameAndPath { get; set; }

        /// <summary>
        /// Gets or sets FilePath.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets Genre.
        /// </summary>
        public List<string> Genre { get; set; }

        /// <summary>
        /// Gets or sets Ids.
        /// </summary>
        public Dictionary<string, string> Ids { get; set; }

        /// <summary>
        /// Gets or sets Mpaa.
        /// </summary>
        public string Mpaa { get; set; }

        /// <summary>
        /// Gets or sets OriginalTitle.
        /// </summary>
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Gets or sets Outline.
        /// </summary>
        public string Outline { get; set; }

        /// <summary>
        /// Gets or sets Playcount.
        /// </summary>
        public int Playcount { get; set; }

        /// <summary>
        /// Gets or sets Plot.
        /// </summary>
        public string Plot { get; set; }

        /// <summary>
        /// Gets or sets Premiered.
        /// </summary>
        public DateTime Premiered { get; set; }

        /// <summary>
        /// Gets or sets Rating.
        /// </summary>
        public double Rating { get; set; }

        /// <summary>
        /// Gets or sets Runtime.
        /// </summary>
        public int Runtime { get; set; }

        /// <summary>
        /// Gets or sets Sets.
        /// </summary>
        public Dictionary<int, string> Sets { get; set; }

        /// <summary>
        /// Gets or sets Studio.
        /// </summary>
        public string Studio { get; set; }

        /// <summary>
        /// Gets or sets Tagline.
        /// </summary>
        public string Tagline { get; set; }

        /// <summary>
        /// Gets or sets Thumb.
        /// </summary>
        public List<string> Thumb { get; set; }

        /// <summary>
        /// Gets or sets Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets Top250.
        /// </summary>
        public int Top250 { get; set; }

        /// <summary>
        /// Gets or sets Trailer.
        /// </summary>
        public List<string> Trailer { get; set; }

        /// <summary>
        /// Gets or sets Votes.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Gets or sets Watched.
        /// </summary>
        public string Watched { get; set; }

        #endregion
    }
}