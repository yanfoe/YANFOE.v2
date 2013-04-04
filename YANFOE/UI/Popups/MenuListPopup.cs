namespace YANFOE.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DevExpress.XtraBars;

    using YANFOE.Factories;
    using YANFOE.Models.MovieModels;
    using YANFOE.Properties;

    public static class MenuListPopup
    {
        public static void Generate(BarManager barManager, PopupMenu popupMenu, List<MovieModel> movieModel)
        {
            popupMenu.ClearLinks();

            GenerateLock(barManager, popupMenu, movieModel);
            GenerateUnlock(barManager, popupMenu, movieModel);
        }

        private static string MovieListTagToString(List<MovieModel> movieModels)
        {
            var stringBuilder = new StringBuilder();
            foreach (var movie in movieModels)
            {
                stringBuilder.Append(movie.MovieUniqueId + "|");
            }

            return stringBuilder.ToString();
        }

        private static List<MovieModel> MovieListTagToList(string movieModelsString)
        {
            var movieIds = movieModelsString.Split('|').ToList();
            var movieList = new List<MovieModel>();

            foreach (var id in movieIds)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    movieList.Add(MovieDBFactory.GetMovie(id));
                }
            }

            return movieList;
        }

        /// <summary>
        /// Generates the lock item
        /// </summary>
        /// <param name="barManager">The bar manager.</param>
        /// <param name="movieModel">The movie model.</param>
        private static void GenerateLock(BarManager barManager, PopupMenu popupMenu, List<MovieModel> movieModels)
        {
            if (movieModels.Count == 1 && movieModels[0].Locked)
            {
                return;
            }

            var item = new BarButtonItem(barManager, "Lock")
                {
                    Tag = MovieListTagToString(movieModels), 
                    Glyph = Resources.locked32
                };

            item.ItemClick += lock_ItemClick;

            popupMenu.AddItem(item);
        }

        /// <summary>
        /// Handles the ItemClick event of the lock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private static void lock_ItemClick(object sender, ItemClickEventArgs e)
        {
            var movieList = MovieListTagToList(e.Item.Tag.ToString());

            foreach (var movie in movieList)
            {
                movie.Locked = true;
            }
        }

        private static void GenerateUnlock(BarManager barManager, PopupMenu popupMenu, List<MovieModel> movieModels)
        {
            if (movieModels.Count == 1 && !movieModels[0].Locked)
            {
                return;
            }

            var item = new BarButtonItem(barManager, "Unlock")
            {
                Tag = MovieListTagToString(movieModels),
                Glyph = Resources.unlock32
            };

            item.ItemClick += unlock_ItemClick;

            popupMenu.AddItem(item);
        }

        /// <summary>
        /// Handles the ItemClick event of the unlock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraBars.ItemClickEventArgs"/> instance containing the event data.</param>
        private static void unlock_ItemClick(object sender, ItemClickEventArgs e)
        {
            var movieList = MovieListTagToList(e.Item.Tag.ToString());

            foreach (var movie in movieList)
            {
                movie.Locked = false;
            }
        }

    }
}
