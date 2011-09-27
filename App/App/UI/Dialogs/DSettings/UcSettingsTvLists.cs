namespace YANFOE.UI.Dialogs.DSettings
{
    using System.Linq;

    using YANFOE.Factories;
    using YANFOE.Models.TvModels.Show;

    public partial class UcSettingsTvLists : DevExpress.XtraEditors.XtraUserControl
    {
        public UcSettingsTvLists()
        {
            InitializeComponent();

            gridHidden.DataSource = TvDBFactory.HiddenTvDatabase;
        }

        private void btnRestore_Click(object sender, System.EventArgs e)
        {
            this.gridViewHidden.GetSelectedRows().Select(row => this.gridViewHidden.GetRow(row) as Series).
                ToList().ForEach(TvDBFactory.RestoreHiddenSeries);
        }
    }
}
