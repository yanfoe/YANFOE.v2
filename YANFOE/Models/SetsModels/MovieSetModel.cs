// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieSetModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.SetsModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Models.MovieModels;
    using YANFOE.Properties;
    using YANFOE.Tools;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The movie set model.
    /// </summary>
    [Serializable]
    public class MovieSetModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The fanart url.
        /// </summary>
        private string fanartUrl;

        /// <summary>
        /// The poster url.
        /// </summary>
        private string posterUrl;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieSetModel"/> class.
        /// </summary>
        public MovieSetModel()
        {
            this.ID = Guid.NewGuid().ToString();
            this.SetName = string.Empty;
            this.Movies = new BindingList<MovieSetObjectModel>();
            this.PosterUrl = string.Empty;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets Fanart.
        /// </summary>
        public Image Fanart
        {
            get
            {
                if (string.IsNullOrEmpty(this.FanartUrl))
                {
                    return Resources.picturefaded128;
                }

                string fanartPath = Downloader.ProcessDownload(this.fanartUrl, DownloadType.Binary, Section.Movies);

                return ImageHandler.LoadImage(fanartPath);
            }
        }

        /// <summary>
        /// Gets or sets FanartUrl.
        /// </summary>
        public string FanartUrl
        {
            get
            {
                return this.fanartUrl;
            }

            set
            {
                if (this.fanartUrl != value)
                {
                    this.fanartUrl = value;
                    this.OnPropertyChanged("FanartUrl");
                    this.OnPropertyChanged("Fanart");
                }
            }
        }

        /// <summary>
        /// Gets or sets MovieUniqueId.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets Movies.
        /// </summary>
        public BindingList<MovieSetObjectModel> Movies { get; set; }

        /// <summary>
        /// Gets Poster.
        /// </summary>
        public Image Poster
        {
            get
            {
                if (string.IsNullOrEmpty(this.PosterUrl))
                {
                    return Resources.picturefaded128;
                }

                string posterPath = Downloader.ProcessDownload(this.PosterUrl, DownloadType.Binary, Section.Movies);

                return ImageHandler.LoadImage(posterPath);
            }
        }

        /// <summary>
        /// Gets or sets PosterUrl.
        /// </summary>
        public string PosterUrl
        {
            get
            {
                return this.posterUrl;
            }

            set
            {
                if (this.posterUrl != value)
                {
                    this.posterUrl = value;
                    this.OnPropertyChanged("PosterUrl");
                    this.OnPropertyChanged("Poster");
                }
            }
        }

        /// <summary>
        /// Gets or sets SetName.
        /// </summary>
        public string SetName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The add movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        /// <param name="order">
        /// The order.
        /// </param>
        public void AddMovie(MovieModel movieModel, int? order = null)
        {
            int orderVal = 0;

            if (order == null)
            {
                orderVal = this.Movies.Count + 1;
            }
            else
            {
                orderVal = (int)order;
            }

            this.Movies.Add(new MovieSetObjectModel { MovieUniqueId = movieModel.MovieUniqueId, Order = orderVal });
        }

        /// <summary>
        /// Determines whether the specified id contains movie.
        /// </summary>
        /// <param name="id">
        /// The movie guid.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified id contains movie; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsMovie(string id)
        {
            List<MovieSetObjectModel> check = (from m in this.Movies where m.MovieUniqueId == id select m).ToList();

            return check.Count > 0;
        }

        /// <summary>
        /// The move movie down.
        /// </summary>
        /// <param name="movieUniqueId">
        /// The movie unique id.
        /// </param>
        public void MoveMovieDown(string movieUniqueId)
        {
            MovieSetObjectModel movie =
                (from m in this.Movies where m.MovieUniqueId == movieUniqueId select m).SingleOrDefault();

            if (movie.Order == this.Movies.Count)
            {
                return;
            }

            MovieSetObjectModel nextMovie =
                (from m in this.Movies where m.Order == (movie.Order + 1) select m).SingleOrDefault();

            nextMovie.Order--;
            movie.Order++;
        }

        /// <summary>
        /// The move movie up.
        /// </summary>
        /// <param name="movieUniqueId">
        /// The movie unique id.
        /// </param>
        public void MoveMovieUp(string movieUniqueId)
        {
            MovieSetObjectModel movie =
                (from m in this.Movies where m.MovieUniqueId == movieUniqueId select m).SingleOrDefault();

            if (movie.Order == 1)
            {
                return;
            }

            MovieSetObjectModel previousMovie =
                (from m in this.Movies where m.Order == (movie.Order - 1) select m).SingleOrDefault();

            previousMovie.Order++;
            movie.Order--;
        }

        /// <summary>
        /// Removes the movie.
        /// </summary>
        /// <param name="movieModel">
        /// The movie model.
        /// </param>
        public void RemoveMovie(MovieModel movieModel)
        {
            MovieSetObjectModel foundObj =
                (from m in this.Movies where m.MovieUniqueId == movieModel.MovieUniqueId select m).SingleOrDefault();

            if (foundObj == null)
            {
                return;
            }

            this.Movies.Remove(foundObj);
        }

        #endregion
    }
}