// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsMoviesIO.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.DSettings
{
    using DevExpress.XtraEditors;

    /// <summary>
    /// UcSettingsTvIOKey Backing Class
    /// </summary>
    public partial class UcSettingsTvIOKey : XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsTvIOKey"/> class.
        /// </summary>
        public UcSettingsTvIOKey()
        {
            InitializeComponent();

            this.SetBindings();
        }

        /// <summary>
        /// Sets the bindings.
        /// </summary>
        private void SetBindings()
        {
            txtSeriesName.Text = Settings.Get.InOutCollection.TvSeriesName;

            txtFirstEpisodeInSeries.Text = Settings.Get.InOutCollection.TvFirstEpisodePathOfSeries;

            txtFirstEpisodeInSeasonPath.Text = Settings.Get.InOutCollection.TvFirstEpisodeOfSeasonPath;

            txtFirstEpisodeInSeason.Text = Settings.Get.InOutCollection.TvFirstEpisodeOfSeason;

            txtEpisodePath.Text = Settings.Get.InOutCollection.TvEpisodePath;

            txtEpisodeFileName.Text = Settings.Get.InOutCollection.TvEpisodeFileName;
        }
    }
}
