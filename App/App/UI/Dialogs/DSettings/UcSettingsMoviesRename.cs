namespace YANFOE.UI.Dialogs.DSettings
{
    using YANFOE.Settings;

    public partial class UcSettingsMoviesRename : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsMoviesRename"/> class.
        /// </summary>
        public UcSettingsMoviesRename()
        {
            InitializeComponent();

            this.chkReplaceWithChar.DataBindings.Add("Checked", Get.InOutCollection, "MovieIOReplaceWithChar");
            this.chkReplaceWithHex.DataBindings.Add("Checked", Get.InOutCollection, "MovieIOReplaceWithHex");
            this.txtReplaceCharWith.DataBindings.Add("Text", Get.InOutCollection, "MovieIOReplaceChar");
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkReplaceWithChar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkReplaceWithChar_CheckedChanged(object sender, System.EventArgs e)
        {
            txtReplaceCharWith.Enabled = chkReplaceWithChar.Checked;
        }
    }
}
