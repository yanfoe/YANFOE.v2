// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieTopMenuUserControl.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the MovieTopMenuUserControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.UserControls.MovieControls
{
    using System;

    using YANFOE.Factories;

    public partial class MovieTopMenuUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieTopMenuUserControl"/> class.
        /// </summary>
        public MovieTopMenuUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnLoadFromWeb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnLoadFromWeb_Click(object sender, EventArgs e)
        {
            MovieDBFactory.InvokeCurrentMovieValueChanged(new EventArgs());

            var count = MovieDBFactory.MovieDatabase.Count;

            if (count == 1)
            {
                Factories.Scraper.MovieScrapeFactory.RunSingleScrape(MovieDBFactory.GetCurrentMovie());
            }
            else if (count > 1)
            {
                Factories.Scraper.MovieScrapeFactory.RunMultiScrape(MovieDBFactory.MultiSelectedMovies);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            MovieDBFactory.InvokeStartSaveMovie(new EventArgs());
        }
    }
}
