// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrailerDetailsModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.Models
{
    using System;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Tools.Enums;

    /// <summary>
    /// The trailer details model.
    /// </summary>
    [Serializable]
    public class TrailerDetailsModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The resolution.
        /// </summary>
        private int resolution;

        /// <summary>
        /// The trailer movie title.
        /// </summary>
        private string trailerMovieTitle;

        /// <summary>
        /// The trailer type.
        /// </summary>
        private string trailerType;

        /// <summary>
        /// The selectedtrailer indicator.
        /// </summary>
        private bool selectedTrailer;

        /// <summary>
        /// The uri full.
        /// </summary>
        private Uri uriFull;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TrailerDetailsModel"/> class.
        /// </summary>
        public TrailerDetailsModel()
        {
            this.uriFull = null;
            this.resolution = 0;
            this.trailerType = "None";
            this.trailerMovieTitle = "N/A";
            this.selectedTrailer = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrailerDetailsModel"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="res">
        /// The res.
        /// </param>
        /// <param name="trailertype">
        /// The trailertype.
        /// </param>
        /// <param name="trailermovietitle">
        /// The trailermovietitle.
        /// </param>
        /// <param name="selectedtrailer">
        /// The selectedtrailer. (Defaults to false)
        /// </param>
        public TrailerDetailsModel(string url, int res, string trailertype, string trailermovietitle, bool selectedtrailer = false)
        {
            this.uriFull = new Uri(url);
            this.resolution = res;
            this.trailerType = trailertype;
            this.trailerMovieTitle = trailermovietitle;
            this.selectedTrailer = selectedtrailer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets Resolution.
        /// </summary>
        public int Resolution
        {
            get
            {
                return this.resolution;
            }

            set
            {
                this.resolution = value;
                this.OnPropertyChanged("Resolution");
            }
        }

        /// <summary>
        /// Gets or sets TrailerMovieTitle.
        /// </summary>
        public string TrailerMovieTitle
        {
            get
            {
                return this.trailerMovieTitle;
            }

            set
            {
                this.trailerMovieTitle = value;
                this.OnPropertyChanged("TrailerMovieTitle");
            }
        }

        /// <summary>
        /// Gets or sets TrailerType.
        /// </summary>
        public string TrailerType
        {
            get
            {
                return this.trailerType;
            }

            set
            {
                this.trailerType = value;
                this.OnPropertyChanged("TrailerType");
            }
        }

        /// <summary>
        /// Gets or sets SelectedTrailer attribute.
        /// </summary>
        public bool SelectedTrailer
        {
            get
            {
                return this.selectedTrailer;
            }

            set
            {
                this.selectedTrailer = value;
                this.OnPropertyChanged("SelectedTrailer");
            }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The image URL.</value>
        public Uri UriFull
        {
            get
            {
                return this.uriFull;
            }

            set
            {
                this.uriFull = value;
                this.OnPropertyChanged("Url");
            }
        }

        #endregion
    }
}