// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvSeriesDetailsUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.TvControls
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;

    using DevExpress.XtraEditors.Controls;

    public partial class TvSeriesDetailsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TvSeriesDetailsUserControl"/> class.
        /// </summary>
        public TvSeriesDetailsUserControl()
        {
            InitializeComponent();

            this.SetupBindings();

            tvTopMenuUserControl1.Type = SaveType.SaveSeries;

            Factories.TvDBFactory.CurrentSeriesChanged += this.TvDBFactory_CurrentSeriesChanged;
        }

        /// <summary>
        /// Handles the CurrentSeriesChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void TvDBFactory_CurrentSeriesChanged(object sender, EventArgs e)
        {
            this.SetupBindings();
        }

        private void SetupBindings()
        {
            txtTitle.DataBindings.Clear();
            txtTitle.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "SeriesName");

            txtOverview.DataBindings.Clear();
            txtOverview.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Overview");

            txtLanguage.DataBindings.Clear();
            txtLanguage.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Language");

            txtCert.DataBindings.Clear();
            txtCert.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "ContentRating");

            cmbGenre.DataBindings.Clear();
            cmbGenre.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "GenreAsString");

            txtFirstAired.DataBindings.Clear();
            txtFirstAired.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "FirstAired", true);

            txtNetwork.DataBindings.Clear();
            txtNetwork.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Network");

            txtNetworkID.DataBindings.Clear();
            txtNetworkID.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "NetworkID");

            txtStatus.DataBindings.Clear();
            txtStatus.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Status");

            txtRuntime.DataBindings.Clear();
            txtRuntime.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Runtime");

            txtRating.DataBindings.Clear();
            txtRating.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Rating");

            grdActors.DataSource = Factories.TvDBFactory.CurrentSeries.Actors;

            this.PopulateGenres();
        }

        private void PopulateGenres()
        {
            var genreList = new List<string>();

            foreach (CheckedListBoxItem item in cmbGenre.Properties.Items)
            {
                item.CheckState = CheckState.Unchecked;
                genreList.Add(item.Description);
            }

            var currentGenres = Factories.TvDBFactory.CurrentSeries.Genre;

            for (int i = 0; i < currentGenres.Count; i++)
            {
                var genre = this.cmbGenre.Properties.Items[i];
                var check = (from g in genreList where g == genre.Description.ToString() select g).SingleOrDefault();

                if (check == null)
                {
                    this.cmbGenre.Properties.Items.Add(genre);
                    genreList.Add(genre.Value.ToString());
                }

                var index = this.cmbGenre.Properties.Items.IndexOf(genre);

                if (genreList.Contains(genre.Description))
                {
                    this.cmbGenre.Properties.Items[index].CheckState = CheckState.Checked;
                }
            }
        }
    }
}
