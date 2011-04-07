// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Skin.cs" company="The YANFOE Project">
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

namespace YANFOE.Tools.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.XtraEditors;

    using YANFOE.Settings;

    /// <summary>
    /// UI / Skin Tools
    /// </summary>
    public static class Skin
    {
        #region Public Methods

        /// <summary>
        /// The populate skin list combo.
        /// </summary>
        /// <param name="cmbSkinList">
        /// The cmb skin list.
        /// </param>
        public static void PopulateSkinListCombo(ComboBoxEdit cmbSkinList)
        {
            cmbSkinList.SelectedIndexChanged += CmbSkinList_SelectedIndexChanged;

            IEnumerable<string> skinList = from SkinContainer s in SkinManager.Default.Skins
                                           orderby s.SkinName
                                           select s.SkinName;

            foreach (string skinName in skinList)
            {
                if (!skinName.Contains("DevExpress") && !skinName.Contains("2008"))
                {
                    cmbSkinList.Properties.Items.Add(skinName);
                }
            }

            cmbSkinList.DataBindings.Add("Text", Get.Ui, "Skin");
        }

        /// <summary>
        /// Temporarily change skin on certain dates.
        /// </summary>
        /// <returns>
        /// Ssh... don't spoil the supprise!
        /// </returns>
        public static string SetCurrentSkin()
        {
            if (DateTime.Now.Month == 12 && DateTime.Now.Day == 25)
            {
                return "Xmas 2008 Blue";
            }

            if (DateTime.Now.Month == 10 && DateTime.Now.Day == 31)
            {
                return "Pumpkin";
            }

            return Get.Ui.Skin;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbSkinList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void CmbSkinList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBoxEdit;
            string skinName = comboBox.Text;
            UserLookAndFeel.Default.SkinName = skinName;
        }

        #endregion
    }
}