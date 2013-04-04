// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmSelectSeries.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.TV
{
    public partial class FrmTvRestructure : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmTvRestructure"/> class.
        /// </summary>
        public FrmTvRestructure()
        {
            InitializeComponent();

            txtSeriesNameTemplate.DataBindings.Add(
                "Text", Settings.Get.InOutCollection.CurrentTvSaveSettings, "RenameSeriesTemplate", true);

            txtSeasonNameTemplate.DataBindings.Add(
                "Text", Settings.Get.InOutCollection.CurrentTvSaveSettings, "RenameSeasonTemplate", true);

            txtEpisodeNameTemplate.DataBindings.Add(
                "Text", Settings.Get.InOutCollection.CurrentTvSaveSettings, "RenameEpisodeTemplate", true);

            txtTvStructure.DataBindings.Add(
                "Text", Settings.Get.InOutCollection.CurrentTvSaveSettings, "TvStructure", true);
        }
    }
}