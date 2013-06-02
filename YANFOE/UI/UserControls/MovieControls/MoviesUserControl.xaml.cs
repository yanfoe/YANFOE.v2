// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MoviesUserControl.xaml.cs">
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
//   Interaction logic for MoviesUserControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.UserControls.MovieControls
{
    #region Required Namespaces

    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    using YANFOE.Factories.Scraper;
    using YANFOE.Models.MovieModels;
    using YANFOE.Tools.Extentions;
    using YANFOE.Tools.UI;
    using YANFOE.UI.UserControls.CommonControls;
    using YANFOE.Factories.InOut.Enum;
    using YANFOE.Factories;
    using System.Diagnostics;
    using System.IO;

    #endregion

    /// <summary>
    ///   Interaction logic for MoviesUserControl.xaml
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", 
        Justification = "Reviewed. Suppression is OK here.")]
    public partial class MoviesUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="MoviesUserControl" /> class.
        /// </summary>
        public MoviesUserControl()
        {
            this.InitializeComponent();

            this.tooltipController.GetTooltipInfo += this.TooltipControllerGetTooltipInfo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The btn load from web_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The e. 
        /// </param>
        private void BtnLoadFromWebClick(object sender, RoutedEventArgs e)
        {
            var count = this.gridViewByTitle.SelectedItems.Count;

            if (count == 1)
            {
                var movie = (MovieModel)this.gridViewByTitle.SelectedItems[0];
                MovieScrapeFactory.RunSingleScrape(movie);

                // gridViewByTitle.SelectedIndex = -1;
            }
            else if (count > 1)
            {
                var movieList = new ThreadedBindingList<MovieModel>();
                foreach (MovieModel movie in this.gridViewByTitle.SelectedItems)
                {
                    movieList.Add(movie);
                }

                MovieScrapeFactory.RunMultiScrape(movieList);

                // gridViewByTitle.SelectedIndex = -1;
            }
        }

        private void BtnLockClick(object sender, RoutedEventArgs e)
        {
            foreach (MovieModel movie in this.gridViewByTitle.SelectedItems)
            {
                movie.Locked = !movie.Locked;
            }
        }

        private void btnWatchedClick(object sender, RoutedEventArgs e)
        {
            foreach (MovieModel movie in this.gridViewByTitle.SelectedItems)
            {
                movie.Watched = !movie.Watched;
            }
        }

        private void BtnMarkedClick(object sender, RoutedEventArgs e)
        {
            foreach (MovieModel movie in this.gridViewByTitle.SelectedItems)
            {
                movie.Marked = !movie.Marked;
            }
        }

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            this.StartSaveMovie();
        }

        private void StartSaveMovie(MovieIOType type = MovieIOType.All)
        {
            foreach (MovieModel movie in this.gridViewByTitle.SelectedItems)
            {
                MovieDBFactory.Instance.MultiSelectedMovies.Add(movie);
            }
            Factories.InOut.OutFactory.SaveMovie(type);
        }

        private void btnOpenFolderClick(object sender, RoutedEventArgs e)
        {
            if (this.gridViewByTitle.SelectedItems.Count != 1)
            {
                MessageBox.Show("This is only supported for one selection");
                return;
            }
            string argument = string.Format(
                @"/select,""{0}""",
                File.Exists((this.gridViewByTitle.SelectedItems[0] as MovieModel).AssociatedFiles.Media[0].PathAndFilename)
                    ? (this.gridViewByTitle.SelectedItems[0] as MovieModel).AssociatedFiles.Media[0].PathAndFilename
                    : (this.gridViewByTitle.SelectedItems[0] as MovieModel).AssociatedFiles.Media[0].FolderPath);

            Process.Start("explorer.exe", argument);
        }
        

        private void btnOpenFileClick(object sender, RoutedEventArgs e)
        {
            if (this.gridViewByTitle.SelectedItems.Count != 1)
            {
                MessageBox.Show("This is only supported for one selection");
                return;
            }
            if (File.Exists((this.gridViewByTitle.SelectedItems[0] as MovieModel).AssociatedFiles.Media[0].PathAndFilename))
            {
                Process.Start((this.gridViewByTitle.SelectedItems[0] as MovieModel).AssociatedFiles.Media[0].PathAndFilename);
            }
        }

        /// <summary>
        /// The tooltip controller_ get tooltip info.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="args">
        /// The args. 
        /// </param>
        private void TooltipControllerGetTooltipInfo(object sender, GetToolTipInfoHandlerArgs args)
        {
            int row, col;

            this.gridViewByTitle.GetRowColumn(args.Element, args.MousePosition, out row, out col);

            args.Info = new SuperToolTipControlInfo("test");
        }

        #endregion
    }
}