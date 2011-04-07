// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmScraperGroupManager.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Dialogs.Movies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;

    using YANFOE.Factories.Scraper;
    using YANFOE.Scrapers.Movie;
    using YANFOE.Scrapers.Movie.Models.ScraperGroup;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;
    using YANFOE.UI.Dialogs.General;

    /// <summary>
    /// The <see cref="FrmScraperGroupManager"/> class.
    /// </summary>
    public partial class FrmScraperGroupManager : XtraForm
    {
        #region Constants and Fields

        /// <summary>
        /// The current scraper dirty.
        /// </summary>
        private bool currentScraperDirty;

        /// <summary>
        /// The current scraper group.
        /// </summary>
        private MovieScraperGroupModel currentScraperGroup;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmScraperGroupManager"/> class.
        /// </summary>
        public FrmScraperGroupManager()
        {
            this.InitializeComponent();

            MovieScraperGroupFactory.GetScraperGroupsOnDisk(this.cmbScraperGroupList);
            this.SetupInitialDataBindings();
            this.SelectFirstScraperGroupEntry();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get controls.
        /// </summary>
        /// <param name="form">
        /// The form to extract control
        /// </param>
        /// <returns>
        /// List of controls on a form
        /// </returns>
        public static List<Control> GetControls(Control form)
        {
            var controlList = new List<Control>();

            foreach (Control childControl in form.Controls)
            {
                controlList.AddRange(GetControls(childControl));
                controlList.Add(childControl);
            }

            return controlList;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The populate combo.
        /// </summary>
        /// <param name="comboBox">
        /// The combo box.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        private static void PopulateCombo(ComboBoxEdit comboBox, BindingList<string> values)
        {
            foreach (string scraper in values)
            {
                comboBox.Properties.Items.Add(scraper);
            }
        }

        /// <summary>
        /// The populate combo with value.
        /// </summary>
        /// <param name="comboBox">
        /// The combo box.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        private static void PopulateComboWithValue(ComboBoxEdit comboBox, string value)
        {
            foreach (object s in comboBox.Properties.Items)
            {
                string compValue = s.ToString();

                if (compValue == value)
                {
                    comboBox.Text = compValue;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnAddClick(object sender, EventArgs e)
        {
            var enterAValue = new FrmEnterAValue("Enter a scraper name");
            enterAValue.ShowDialog();

            if (!enterAValue.Cancelled)
            {
                var scraperGroup = new MovieScraperGroupModel { ScraperName = enterAValue.Response };

                MovieScraperGroupFactory.SerializeToXml(scraperGroup);
                MovieScraperGroupFactory.GetScraperGroupsOnDisk(this.cmbScraperGroupList);
                this.currentScraperGroup = scraperGroup;

                this.currentScraperGroup = new MovieScraperGroupModel();
                this.RefreshDatabindings();

                this.cmbScraperGroupList.Text = enterAValue.Response;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAutopopulate control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnAutopopulateClick(object sender, EventArgs e)
        {
            PopulateComboWithValue(this.cmbTitle, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbYear, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbOrigionalTitle, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbRating, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbTagline, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbPlot, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbOutline, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbCertification, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbCountry, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbStudio, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbReleaseDate, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbTop250, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbVotes, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbLanguage, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbGenre, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbRuntime, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbCast, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbDirector, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbWriter, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbMpaa, this.cmbMpaa.Text);
            PopulateComboWithValue(this.cmbFanart, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbPosters, this.cmbAutoPopulate.Text);
            PopulateComboWithValue(this.cmbTrailer, this.cmbAutoPopulate.Text);
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            if (this.currentScraperDirty)
            {
                DialogResult result = XtraMessageBox.Show(
                    "Do you wish to save the current scraper first?", 
                    "Are you sure?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MovieScraperGroupFactory.SerializeToXml(this.currentScraperGroup);
                }
            }

            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnRefreshClick(object sender, EventArgs e)
        {
            this.CmbScraperGroupListEditValueChanging(null, null);
        }

        /// <summary>
        /// Handles the Click event of the btnRemove control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnRemoveClick(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show(
                "Are you sure you wish to delete this scraper group?", 
                "Are you sure?", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                File.Delete(
                    Get.FileSystemPaths.PathDirScraperGroupsMovies + this.currentScraperGroup.ScraperName + ".xml");
                MovieScraperGroupFactory.GetScraperGroupsOnDisk(this.cmbScraperGroupList);

                this.SelectFirstScraperGroupEntry();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void BtnSaveClick(object sender, EventArgs e)
        {
            MovieScraperGroupFactory.SerializeToXml(this.currentScraperGroup);

            this.cmbScraperGroupList.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            this.btnRefresh.Enabled = this.currentScraperDirty;

            this.currentScraperDirty = false;
        }

        /// <summary>
        /// Handles the Click event of the butClearScraperGroup control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void ButClearScraperGroupClick(object sender, EventArgs e)
        {
            this.currentScraperGroup.ClearScrapers();
        }

        /// <summary>
        /// Handles the EditValueChanged event of the cmbScraperGroupList control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void CmbScraperGroupListEditValueChanged(object sender, EventArgs e)
        {
            if (this.cmbScraperGroupList.Text != string.Empty)
            {
                this.currentScraperGroup = MovieScraperGroupFactory.DeserializeXml(this.cmbScraperGroupList.Text);
                this.RefreshDatabindings();
            }
        }

        /// <summary>
        /// Handles the EditValueChanging event of the cmbScraperGroupList control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.
        /// </param>
        private void CmbScraperGroupListEditValueChanging(object sender, ChangingEventArgs e)
        {
            if (this.currentScraperDirty)
            {
                DialogResult result = XtraMessageBox.Show(
                    "Do you wish to save the current scraper group?", 
                    "Are you sure?", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MovieScraperGroupFactory.SerializeToXml(this.currentScraperGroup);
                    this.currentScraperDirty = false;
                }
            }
        }

        /// <summary>
        /// The create new scraper group.
        /// </summary>
        private void CreateNewScraperGroup()
        {
            this.currentScraperGroup = new MovieScraperGroupModel();
        }

        /// <summary>
        /// Handles the PropertyChanged event of the currentScraperGroup control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void CurrentScraperGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.currentScraperDirty = true;
            this.dxErrorProvider1.UpdateBinding();
        }

        /// <summary>
        /// The refresh databindings.
        /// </summary>
        private void RefreshDatabindings()
        {
            this.cmbScraperGroupList.DataBindings.Clear();
            this.txtScraperDescription.DataBindings.Clear();

            this.cmbTitle.DataBindings.Clear();
            this.cmbYear.DataBindings.Clear();
            this.cmbOrigionalTitle.DataBindings.Clear();
            this.cmbRating.DataBindings.Clear();
            this.cmbTagline.DataBindings.Clear();
            this.cmbPlot.DataBindings.Clear();
            this.cmbOutline.DataBindings.Clear();
            this.cmbCertification.DataBindings.Clear();
            this.cmbCountry.DataBindings.Clear();
            this.cmbStudio.DataBindings.Clear();
            this.cmbReleaseDate.DataBindings.Clear();
            this.cmbTop250.DataBindings.Clear();
            this.cmbVotes.DataBindings.Clear();
            this.cmbLanguage.DataBindings.Clear();
            this.cmbGenre.DataBindings.Clear();
            this.cmbRuntime.DataBindings.Clear();
            this.cmbCast.DataBindings.Clear();
            this.cmbDirector.DataBindings.Clear();
            this.cmbWriter.DataBindings.Clear();
            this.cmbMpaa.DataBindings.Clear();
            this.cmbFanart.DataBindings.Clear();
            this.cmbPosters.DataBindings.Clear();
            this.cmbTrailer.DataBindings.Clear();

            this.cmbScraperGroupList.DataBindings.Add("Text", this.currentScraperGroup, "ScraperName");
            this.txtScraperDescription.DataBindings.Add("Text", this.currentScraperGroup, "ScraperDescription");

            this.cmbTitle.DataBindings.Add(
                "Text", this.currentScraperGroup, "Title", true, DataSourceUpdateMode.OnPropertyChanged);

            this.cmbYear.DataBindings.Add(
                "Text", this.currentScraperGroup, "Year", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbOrigionalTitle.DataBindings.Add(
                "Text", this.currentScraperGroup, "OrigionalTitle", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbRating.DataBindings.Add(
                "Text", this.currentScraperGroup, "Rating", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbTagline.DataBindings.Add(
                "Text", this.currentScraperGroup, "Tagline", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbPlot.DataBindings.Add(
                "Text", this.currentScraperGroup, "Plot", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbOutline.DataBindings.Add(
                "Text", this.currentScraperGroup, "Outline", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbCertification.DataBindings.Add(
                "Text", this.currentScraperGroup, "Certification", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbCountry.DataBindings.Add(
                "Text", this.currentScraperGroup, "Country", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbStudio.DataBindings.Add(
                "Text", this.currentScraperGroup, "Studio", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbReleaseDate.DataBindings.Add(
                "Text", this.currentScraperGroup, "ReleaseDate", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbTop250.DataBindings.Add(
                "Text", this.currentScraperGroup, "Top250", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbVotes.DataBindings.Add(
                "Text", this.currentScraperGroup, "Votes", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbLanguage.DataBindings.Add(
                "Text", this.currentScraperGroup, "Language", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbGenre.DataBindings.Add(
                "Text", this.currentScraperGroup, "Genre", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbRuntime.DataBindings.Add(
                "Text", this.currentScraperGroup, "Runtime", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbCast.DataBindings.Add(
                "Text", this.currentScraperGroup, "Cast", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbDirector.DataBindings.Add(
                "Text", this.currentScraperGroup, "Director", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbWriter.DataBindings.Add(
                "Text", this.currentScraperGroup, "Writers", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbMpaa.DataBindings.Add(
                "Text", this.currentScraperGroup, "Mpaa", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbFanart.DataBindings.Add(
                "Text", this.currentScraperGroup, "Fanart", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbPosters.DataBindings.Add(
                "Text", this.currentScraperGroup, "Poster", true, DataSourceUpdateMode.OnPropertyChanged);
            this.cmbTrailer.DataBindings.Add(
                "Text", this.currentScraperGroup, "Trailer", true, DataSourceUpdateMode.OnPropertyChanged);

            this.currentScraperGroup.PropertyChanged += this.CurrentScraperGroupPropertyChanged;

            this.dxErrorProvider1.DataSource = this.currentScraperGroup;
            this.dxErrorProvider1.ContainerControl = this;
        }

        /// <summary>
        /// The select first scraper group entry.
        /// </summary>
        private void SelectFirstScraperGroupEntry()
        {
            this.cmbScraperGroupList.Text = this.cmbScraperGroupList.Properties.Items.Count > 0
                                                ? this.cmbScraperGroupList.Properties.Items[0].ToString()
                                                : string.Empty;
        }

        /// <summary>
        /// Setup Data Bindings
        /// </summary>
        private void SetupInitialDataBindings()
        {
            this.CreateNewScraperGroup();

            PopulateCombo(this.cmbAutoPopulate, MovieScraperHandler.ReturnAllScrapersAsStringList());

            PopulateCombo(this.cmbTitle, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Title));
            PopulateCombo(this.cmbYear, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Year));
            PopulateCombo(
                this.cmbOrigionalTitle, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.OrigionalTitle));
            PopulateCombo(this.cmbRating, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Rating));
            PopulateCombo(this.cmbTagline, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Tagline));
            PopulateCombo(this.cmbPlot, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Plot));
            PopulateCombo(this.cmbOutline, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Outline));
            PopulateCombo(this.cmbCertification, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Tagline));
            PopulateCombo(this.cmbCountry, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Country));
            PopulateCombo(this.cmbStudio, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Studio));
            PopulateCombo(this.cmbReleaseDate, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.ReleaseDate));
            PopulateCombo(this.cmbTop250, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Top250));
            PopulateCombo(this.cmbVotes, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Votes));
            PopulateCombo(this.cmbLanguage, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Language));
            PopulateCombo(this.cmbGenre, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Genre));
            PopulateCombo(
                this.cmbRuntime, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Runtime, addMediaInfo: true));
            PopulateCombo(this.cmbCast, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Cast));
            PopulateCombo(this.cmbDirector, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Director));
            PopulateCombo(this.cmbWriter, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Writers));
            PopulateCombo(this.cmbMpaa, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Mpaa));
            PopulateCombo(this.cmbFanart, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Fanart));
            PopulateCombo(this.cmbPosters, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Poster));
            PopulateCombo(this.cmbTrailer, MovieScraperHandler.GetScrapersAsStringList(ScrapeFields.Trailer));

            this.RefreshDatabindings();
        }

        /// <summary>
        /// Handles the Tick event of the uiTimer control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void UITimerTick(object sender, EventArgs e)
        {
            this.btnSave.Enabled = this.currentScraperDirty && this.cmbScraperGroupList.Text != string.Empty;
            this.btnRefresh.Enabled = this.cmbScraperGroupList.Text != string.Empty;
            this.btnRemove.Enabled = this.cmbScraperGroupList.Text != string.Empty;
            this.txtScraperDescription.Enabled = this.cmbScraperGroupList.Text != string.Empty;

            GetControls(this);

            List<Control> availControls = GetControls(this);

            foreach (Control c in availControls)
            {
                if (c.GetType() == typeof(ComboBoxEdit))
                {
                    c.Enabled = this.cmbScraperGroupList.Text != string.Empty;
                }
            }
        }

        #endregion
    }
}