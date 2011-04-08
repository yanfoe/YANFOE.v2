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
    using System.ComponentModel;
    using System.IO;

    using DevExpress.XtraEditors;

    using YANFOE.Settings;
    using YANFOE.Tools.Importing;

    /// <summary>
    /// The uc settings movies io.
    /// </summary>
    public partial class UcSettingsMoviesIO : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsMoviesIO"/> class.
        /// </summary>
        public UcSettingsMoviesIO()
        {
            this.InitializeComponent();

            this.SetupDataBindings();

            Get.InOutCollection.CurrentMovieSaveSettings.PropertyChanged +=
                this.CurrentMovieSaveSettings_PropertyChanged;

            this.UpdateNormalDemos();
            this.UpdateBlurayDemos();
            this.UpdateDVDDemos();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the PropertyChanged event of the CurrentMovieSaveSettings control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void CurrentMovieSaveSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateNormalDemos();
            this.UpdateBlurayDemos();
            this.UpdateDVDDemos();
        }

        /// <summary>
        /// Setups the data bindings.
        /// </summary>
        private void SetupDataBindings()
        {
            this.txtNormalTestPath.DataBindings.Add("Text", Get.InOutCollection, "MovieNormalTestPath");
            this.txtDvdTestPath.DataBindings.Add("Text", Get.InOutCollection, "MovieDVDTestPath");
            this.txtBluRayTestPath.DataBindings.Add("Text", Get.InOutCollection, "MovieBlurayTestPath");

            this.txtNormalNfoPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalNfoNameTemplate");
            this.txtNormalPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalPosterNameTemplate");
            this.txtNormalFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalFanartNameTemplate");
            this.txtNormalTrailerPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalTrailerNameTemplate");
            this.txtNormalSetPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalSetPosterNameTemplate");
            this.txtNormalSetFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "NormalSetFanartNameTemplate");

            this.txtDVDNfoPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdNfoNameTemplate");
            this.txtDVDPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdPosterNameTemplate");
            this.txtDVDFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdFanartNameTemplate");
            this.txtDVDTrailerPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdTrailerNameTemplate");
            this.txtDvdSetPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdSetPosterNameTemplate");
            this.txtDvdSetFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "DvdSetFanartNameTemplate");

            this.txtBlurayNfoPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BlurayNfoNameTemplate");
            this.txtBlurayPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BlurayPosterNameTemplate");
            this.txtBlurayFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BlurayFanartNameTemplate");
            this.txtBlurayTrailerPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BlurayTrailerNameTemplate");
            this.txtBluraySetPosterPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BluraySetPosterNameTemplate");
            this.txtBluraySetFanartPath.DataBindings.Add(
                "Text", Get.InOutCollection.CurrentMovieSaveSettings, "BluraySetFanartNameTemplate");
        }

        /// <summary>
        /// Updates the bluray demos.
        /// </summary>
        private void UpdateBlurayDemos()
        {
            string demoPath = MovieNaming.GetBluRayPath(Get.InOutCollection.MovieBlurayTestPath) +
                              MovieNaming.GetBluRayName(Get.InOutCollection.MovieBlurayTestPath) +
                              Path.DirectorySeparatorChar;
            string demoFileName = MovieNaming.GetBluRayName(Get.InOutCollection.MovieBlurayTestPath);
            const string DemoExtention = "jpg";
            const string DemoSetName = "DemoSetName";

            this.txtBlurayNfoPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.BlurayNfoNameTemplate.Replace("<path>", demoPath).Replace(
                    "<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtBlurayPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.BlurayPosterNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtBlurayFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.BlurayFanartNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention);

            // Add demo for trailer

            this.txtBluraySetPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.BluraySetPosterNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);

            this.txtBluraySetFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.BluraySetFanartNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);
        }

        /// <summary>
        /// Updates the DVD demos.
        /// </summary>
        private void UpdateDVDDemos()
        {
            string demoPath = MovieNaming.GetDvdPath(Get.InOutCollection.MovieDVDTestPath) +
                              MovieNaming.GetDvdName(Get.InOutCollection.MovieDVDTestPath) + Path.DirectorySeparatorChar;
            string demoFileName = MovieNaming.GetDvdName(Get.InOutCollection.MovieDVDTestPath);
            const string DemoExtention = "jpg";
            const string DemoSetName = "DemoSetName";

            this.txtDVDNfoPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.DvdNfoNameTemplate.Replace("<path>", demoPath).Replace(
                    "<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtDVDPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.DvdPosterNameTemplate.Replace("<path>", demoPath).Replace(
                    "<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtDVDFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.DvdFanartNameTemplate.Replace("<path>", demoPath).Replace(
                    "<filename>", demoFileName).Replace("<ext>", DemoExtention);

            // Add demo for trailer

            this.txtDVDSetPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.DvdSetPosterNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);

            this.txtDVDSetFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.DvdSetFanartNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);
        }

        /// <summary>
        /// Updates the normal demos.
        /// </summary>
        private void UpdateNormalDemos()
        {
            string demoPath =
                Get.InOutCollection.MovieNormalTestPath.Replace(
                    Path.GetFileName(Get.InOutCollection.MovieNormalTestPath), string.Empty);
            string demoFileName = Path.GetFileNameWithoutExtension(Get.InOutCollection.MovieNormalTestPath);
            const string DemoExtention = "jpg";
            const string DemoSetName = "DemoSetName";

            this.txtNormalNFOPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.NormalNfoNameTemplate.Replace("<path>", demoPath).Replace(
                    "<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtNormalPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.NormalPosterNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention);

            this.txtNormalFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.NormalFanartNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention);

            // Add Demo for trailer

            this.txtNormalSetPosterPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.NormalSetPosterNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);

            this.txtNormalSetFanartPreview.Text =
                Get.InOutCollection.CurrentMovieSaveSettings.NormalSetFanartNameTemplate.Replace("<path>", demoPath).
                    Replace("<filename>", demoFileName).Replace("<ext>", DemoExtention).Replace(
                        "<setname>", DemoSetName);
        }

        #endregion
    }
}