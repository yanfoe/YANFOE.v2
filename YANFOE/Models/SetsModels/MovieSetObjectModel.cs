// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MovieSetObjectModel.cs">
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
//   The movie set object model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Models.SetsModels
{
    #region Required Namespaces

    using System;

    using YANFOE.Factories;
    using YANFOE.Models.MovieModels;

    #endregion

    /// <summary>
    ///   The movie set object model.
    /// </summary>
    [Serializable]
    public class MovieSetObjectModel
    {
        #region Fields

        /// <summary>
        ///   The order.
        /// </summary>
        private int order;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets MovieName.
        /// </summary>
        public string MovieName
        {
            get
            {
                MovieModel movie = MovieDBFactory.Instance.GetMovie(this.MovieUniqueId);
                string response = string.Empty;

                if (movie != null)
                {
                    response = movie.Title;
                }

                return response;
            }
        }

        /// <summary>
        ///   Gets or sets MovieUniqueId.
        /// </summary>
        public string MovieUniqueId { get; set; }

        /// <summary>
        ///   Gets or sets Order.
        /// </summary>
        public int Order
        {
            get
            {
                return this.order;
            }

            set
            {
                this.order = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Set the movie ID
        /// </summary>
        /// <param name="movieGuid">
        /// The movie guid. 
        /// </param>
        public void Add(string movieGuid)
        {
            this.MovieUniqueId = movieGuid;
        }

        /// <summary>
        ///   Get movie model using unique ID
        /// </summary>
        /// <returns> Movie Model </returns>
        public MovieModel GetMovieModel()
        {
            return MovieDBFactory.Instance.GetMovie(this.MovieUniqueId);
        }

        #endregion
    }
}