// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieMainDetailsUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.MovieControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraLayout.Utils;

    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Models;

    public partial class MovieMainDetailsUserControl : XtraUserControl
    {
        private string currentGenre;

        public MovieMainDetailsUserControl()
        {
            InitializeComponent();

            Factories.MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;

            this.currentGenre = string.Empty;

            this.GenerateSources();
        }

        private void GenerateSources()
        {
            var sourceDB = Settings.Get.Keywords.Sources;

            foreach (var source in sourceDB)
            {
                cmbSource.Properties.Items.Add(source.Key);
            }
        }

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            this.SetupForm();
            this.ClearBindings();
            this.CreateBindings();

            Factories.Scraper.MovieScraperGroupFactory.GetScraperGroupsOnDisk(cmbScraperGroup);

        }

        private void SetupForm()
        {
            const string noValue = "No Value";
            const string noChange = "Mixed Values";

            string valueToSet;

            if (Factories.MovieDBFactory.GetCurrentMovie().MultiSelectModel)
            {
                this.layMultiGroup.Visibility = LayoutVisibility.Always;
                this.layActorsGroup.Visibility = LayoutVisibility.Never;
                valueToSet = noChange;
            }
            else
            {
                this.layMultiGroup.Visibility = LayoutVisibility.Never;
                this.layActorsGroup.Visibility = LayoutVisibility.Always;
                valueToSet = noValue;
            }

            txtYear.Properties.NullValuePrompt = valueToSet;
        }

        private void ClearBindings()
        {
            grdActors.DataBindings.Clear();

            layActorsGroup.DataBindings.Clear();

            btnActorUp.DataBindings.Clear();
            btnActorDown.DataBindings.Clear();
            btnActorTrash.DataBindings.Clear();

            cmbScraperGroup.DataBindings.Clear();

            txtTitle.DataBindings.Clear();
            txtYear.DataBindings.Clear();
            txtPlot.DataBindings.Clear();
            txtOutline.DataBindings.Clear();
            txtTagline.DataBindings.Clear();
            txtOriginalTitle.DataBindings.Clear();
            cmbStudio.DataBindings.Clear();
            dateReleased.DataBindings.Clear();
            txtDirectors.DataBindings.Clear();
            txtWriters.DataBindings.Clear();
            cmbGenre.DataBindings.Clear();
            cmbLanguage.DataBindings.Clear();
            cmbCountry.DataBindings.Clear();
            txtRuntime.DataBindings.Clear();
            txtRating.DataBindings.Clear();
            cmbMpaa.DataBindings.Clear();
            cmbSource.DataBindings.Clear();
            txtCert.DataBindings.Clear();
            txtTop250.DataBindings.Clear();

            layoutControl1.DataBindings.Clear();

            grdActors.DataSource = null;
        }

        private void CreateBindings()
        {
            grdActors.DataSource = Factories.MovieDBFactory.GetCurrentMovie().Cast;
            layActorsGroup.DataBindings.Add("Enabled", Factories.MovieDBFactory.GetCurrentMovie(), "ActorsEnabled");

            cmbScraperGroup.DataBindings.Add(
                "Text",
                Factories.MovieDBFactory.GetCurrentMovie(),
                "ScraperGroup",
                true,
                DataSourceUpdateMode.OnPropertyChanged);

            txtTitle.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Title");
            txtTitle.DataBindings.Add("Enabled", Factories.MovieDBFactory.GetCurrentMovie(), "TitleEnabled", true);
            txtYear.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Year", true);
            txtPlot.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Plot", true);
            txtOutline.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Outline", true);
            txtTagline.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Tagline", true);
            txtOriginalTitle.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "OriginalTitle", true);
            cmbStudio.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "SetStudio", true);
            dateReleased.DataBindings.Add("DateTime", Factories.MovieDBFactory.GetCurrentMovie(), "ReleaseDate", true);
            txtDirectors.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "DirectorAsString", true);
            txtWriters.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "WritersAsString", true);
            cmbMpaa.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Mpaa", true);
            cmbLanguage.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "LanguageAsString", true);
            cmbCountry.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "CountryAsString");
            txtRuntime.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "RuntimeInHourMin", true);
            txtRating.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Rating", true);
            cmbSource.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "VideoSource");
            txtCert.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Certification", true);
            txtTop250.DataBindings.Add("Text", Factories.MovieDBFactory.GetCurrentMovie(), "Top250", true);

            layoutControl1.DataBindings.Add("Enabled", Factories.MovieDBFactory.GetCurrentMovie(), "Unlocked");

            this.PopulateStudios();
            this.PopulateGenres();
            this.PopulateCountries();

            if (string.IsNullOrEmpty(cmbStudio.Text) && cmbStudio.Properties.Items.Count > 0)
            {
                cmbStudio.Text = cmbStudio.Properties.Items[0].ToString();
            }
        }

        private void PopulateStudios()
        {
            cmbStudio.Properties.Items.Clear();

            foreach (var studio in Factories.MovieDBFactory.GetCurrentMovie().Studios)
            {
                cmbStudio.Properties.Items.Add(studio);
            }
        }

        private void PopulateGenres()
        {
            ScraperList type;

            Enum.TryParse(Factories.MovieDBFactory.GetCurrentMovie().ScraperGroup, out type);

            if (this.currentGenre == type.ToString())
            {
                foreach (CheckedListBoxItem item in cmbGenre.Properties.Items)
                {
                    item.CheckState = CheckState.Unchecked;
                }
            }

            cmbGenre.Properties.Items.Clear();

            this.currentGenre = type.ToString();

            if (!Settings.Get.Genres.GenreDictionary.ContainsKey(type))
            {
                cmbGenre.Properties.Items.Add("No Scraper Group Selected");
            }

            var genreList = Settings.Get.Genres.GenreDictionary[type];

            foreach (var genre in genreList)
            {
                cmbGenre.Properties.Items.Add(genre);
            }

            var currentGenres = Factories.MovieDBFactory.GetCurrentMovie().Genre;

            for (int i = 0; i < currentGenres.Count; i++)
            {
                var genre = this.cmbGenre.Properties.Items[i];
                var check = (from g in Settings.Get.Genres.GenreDictionary[type] where g == genre.Value.ToString() select g).SingleOrDefault();

                if (check == null)
                {
                    this.cmbGenre.Properties.Items.Add(genre);
                    genreList.Add(genre.Value.ToString());
                }

                var index = this.cmbGenre.Properties.Items.IndexOf(genre);

                if (genreList.Contains(genre.Value))
                {
                    this.cmbGenre.Properties.Items[index].CheckState = CheckState.Checked;
                }
            }
        }

        private void PopulateCountries()
        {
            ScraperList type;

            Enum.TryParse(Factories.MovieDBFactory.GetCurrentMovie().ScraperGroup, out type);

            cmbCountry.Properties.Items.Clear();

            if (!Settings.Get.Countries.CountryDictionary.ContainsKey(type))
            {
                cmbCountry.Properties.Items.Add("No Scraper Group Selected");
            }

            var countryList = Settings.Get.Countries.CountryDictionary[type];
            var currentCountries = Factories.MovieDBFactory.GetCurrentMovie().Country;

            foreach (var country in countryList)
            {
                cmbCountry.Properties.Items.Add(country);
            }

            for (int i = 0; i < cmbCountry.Properties.Items.Count; i++)
            {
                var country = this.cmbCountry.Properties.Items[i];
                var check = (from g in countryList where g == country.Value.ToString() select g).SingleOrDefault();

                if (check == null)
                {
                    this.cmbCountry.Properties.Items.Add(country);
                    countryList.Add(country.Value.ToString());
                }

                var index = this.cmbCountry.Properties.Items.IndexOf(country);

                if (currentCountries.Contains(country.Value))
                {
                    this.cmbCountry.Properties.Items[index].CheckState = CheckState.Checked;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnActorTrash control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnActorTrash_Click(object sender, EventArgs e)
        {
            var selectIndexes = gridViewActors.GetSelectedRows();

            var actorsToRemove = new List<PersonModel>();

            foreach (var index in selectIndexes)
            {
                var personModel = gridViewActors.GetRow(index) as PersonModel;
                actorsToRemove.Add(personModel);
            }

            foreach (var actor in actorsToRemove)
            {
                Factories.MovieDBFactory.GetCurrentMovie().Cast.Remove(actor);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the gridViewActors control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DevExpress.Data.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void gridViewActors_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            btnActorTrash.Enabled = gridViewActors.GetSelectedRows().Length > 0;
        }
    }
}
