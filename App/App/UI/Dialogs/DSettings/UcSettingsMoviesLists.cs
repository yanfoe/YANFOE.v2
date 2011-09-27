namespace YANFOE.UI.Dialogs.DSettings
{
    using System.Linq;

    using YANFOE.Factories;
    using YANFOE.Models.MovieModels;

    public partial class UcSettingsMoviesLists : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSettingsMoviesLists()
        {
            InitializeComponent();
            listEditGenre.SetDataSource(Settings.Get.Genres.CustomGenres);

            grdHiddenMovies.DataSource = MovieDBFactory.HiddenMovieDatabase;
        }

        private void btnRestore_Click(object sender, System.EventArgs e)
        {
            this.grdViewHiddenMovie.GetSelectedRows().Select(row => this.grdViewHiddenMovie.GetRow(row) as MovieModel).
                ToList().ForEach(c => { MovieDBFactory.RestoreHiddenMovie(c); });
        }
    }
}
