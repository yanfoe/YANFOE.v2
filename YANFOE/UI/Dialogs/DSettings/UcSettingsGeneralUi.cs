// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UcSettingsGeneralUi.cs" company="The YANFOE Project">
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

    using YANFOE.Settings;

    /// <summary>
    /// UcSettingsGeneralUi backing class
    /// </summary>
    public partial class UcSettingsGeneralUi : XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsGeneralUi"/> class.
        /// </summary>
        public UcSettingsGeneralUi()
        {
            InitializeComponent();

            this.PopulateSkinList();

            this.chkEnableTVPathColumn.DataBindings.Add("Checked", Get.Ui, "EnableTVPathColumn");
            this.chkShowTVSeries0.DataBindings.Add("Checked", Get.Ui, "HideSeasonZero");
        }

        /// <summary>
        /// Populates the skin list.
        /// </summary>
        private void PopulateSkinList()
        {
            Tools.UI.Skin.PopulateSkinListCombo(cmbSkinList);
        }
    }
}
