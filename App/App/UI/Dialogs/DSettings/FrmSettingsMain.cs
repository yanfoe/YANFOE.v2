// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmSettingsMain.cs" company="The YANFOE Project">
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
    using System;

    using DevExpress.Utils;

    /// <summary>
    /// FrmSettingsMain Form
    /// </summary>
    public partial class FrmSettingsMain : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmSettingsMain"/> class.
        /// </summary>
        public FrmSettingsMain()
        {
            InitializeComponent();

            tabMain.ShowTabHeader = DefaultBoolean.False;
            tabControlGeneral.ShowTabHeader = DefaultBoolean.False;
            tabControlMovies.ShowTabHeader = DefaultBoolean.False;
            tabControlScrapers.ShowTabHeader = DefaultBoolean.False;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemGeneralUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void NavItemGeneralUI_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabGeneral;
            tabControlGeneral.SelectedTabPage = tabGeneralUi;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemGeneralWeb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void NavItemGeneralWeb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabGeneral;
            tabControlGeneral.SelectedTabPage = tabGeneralWeb;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemMoviesIO control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void NavItemMoviesOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabMovies;
            tabControlMovies.SelectedTabPage = this.tabMovieOut;
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Settings.Get.LoadAll();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            Settings.Get.SaveAll();
            this.Close();
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemTvFileOut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void NavItemTvFileOut_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabTv;
            tabTvControl.SelectedTabPage = tabTvFileOut;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemTvRename control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void NavItemTvRename_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabTv;
            tabTvControl.SelectedTabPage = tabTvRename;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemMovieIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void navItemMovieIn_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabMovies;
            tabControlMovies.SelectedTabPage = this.tabMovieIn;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemScraperImdb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void navItemScraperImdb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabScrapers;
            tabControlScrapers.SelectedTabPage = this.tabIMDB;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemScraperTmdb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void navItemScraperTmdb_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabScrapers;
            tabControlScrapers.SelectedTabPage = this.tabTMDB;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemScraperTvDB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void navItemScraperTvDB_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabScrapers;
            tabControlScrapers.SelectedTabPage = this.tabTVDB;
        }

        /// <summary>
        /// Handles the LinkClicked event of the navItemGeneralFileInfo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.XtraNavBar.NavBarLinkEventArgs"/> instance containing the event data.</param>
        private void navItemGeneralFileInfo_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabGeneral;
            tabControlGeneral.SelectedTabPage = this.tabGeneralFileInfo;
        }

        private void NavItemMoviesLists_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            tabMain.SelectedTabPage = tabMovies;
            tabControlMovies.SelectedTabPage = this.tabMovieLists;
        }
    }
}