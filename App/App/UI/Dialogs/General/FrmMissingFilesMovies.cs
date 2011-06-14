// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmMissingFilesMovies.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the FrmMissingFilesMovies type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.General
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using YANFOE.Factories;
    using YANFOE.Factories.Media;
    using YANFOE.Models.MovieModels;

    public partial class FrmMissingFilesMovies : DevExpress.XtraEditors.XtraForm
    {
        private List<MovieModel> movieModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMissingFilesMovies"/> class.
        /// </summary>
        public FrmMissingFilesMovies(List<MovieModel> movieModels, bool autoAccept = false)
        {
            InitializeComponent();
            this.movieModels = movieModels;

            if (autoAccept)
            {
                this.RemoveMovies();
                this.Close();
                return;
            }

            var movies = (from m in this.movieModels select new { Title = m.Title, Files = m.AssociatedFiles.FilesAsList() }).ToList();

            grdMovies.DataSource = movies;
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.RemoveMovies();

            this.Close();
        }

        private void RemoveMovies()
        {
            foreach (var movie in this.movieModels)
            {
                foreach (var file in movie.AssociatedFiles.Media)
                {
                    MasterMediaDBFactory.MasterMovieMediaDatabase.Remove(
                        (from m in MasterMediaDBFactory.MasterMovieMediaDatabase where m.FileNameAndPath == file.FileNameAndPath select m).
                            SingleOrDefault());
                }

                MovieDBFactory.MovieDatabase.Remove(movie);

            }
        }
    }
}