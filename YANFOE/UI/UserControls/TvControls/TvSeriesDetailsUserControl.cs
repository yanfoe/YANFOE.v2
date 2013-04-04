// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TvSeriesDetailsUserControl.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   TvSeriesDetailsUserControl user control
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.UserControls.TvControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using DevExpress.XtraEditors.Controls;

    /// <summary>
    /// TvSeriesDetailsUserControl user control
    /// </summary>
    public partial class TvSeriesDetailsUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "TvSeriesDetailsUserControl" /> class.
        /// </summary>
        public TvSeriesDetailsUserControl()
        {
            this.InitializeComponent();

            this.SetupBindings();

            this.tvTopMenuUserControl1.Type = SaveType.SaveSeries;

            Factories.TvDBFactory.CurrentSeriesChanged += this.TvDBFactory_CurrentSeriesChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populates the genres combobox
        /// </summary>
        private void PopulateGenres()
        {
            var genreList = new List<string>();

            foreach (CheckedListBoxItem item in this.cmbGenre.Properties.Items)
            {
                item.CheckState = CheckState.Unchecked;
                genreList.Add(item.Description);
            }

            var currentGenres = Factories.TvDBFactory.CurrentSeries.Genre;

            for (int i = 0; i < currentGenres.Count; i++)
            {
                var genre = this.cmbGenre.Properties.Items[i];
                var check = (from g in genreList where g == genre.Description select g).SingleOrDefault();

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

        /// <summary>
        /// Setups the databindings.
        /// </summary>
        private void SetupBindings()
        {
            this.layoutControlGroup.DataBindings.Clear();
            this.layoutControlGroup.DataBindings.Add("Enabled", Factories.TvDBFactory.CurrentSeries, "NotLocked");

            this.txtTitle.DataBindings.Clear();
            this.txtTitle.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "SeriesName");

            this.txtOverview.DataBindings.Clear();
            this.txtOverview.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Overview");

            this.txtLanguage.DataBindings.Clear();
            this.txtLanguage.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Language");

            this.txtCert.DataBindings.Clear();
            this.txtCert.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "ContentRating");

            this.cmbGenre.DataBindings.Clear();
            this.cmbGenre.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "GenreAsString");

            this.txtFirstAired.DataBindings.Clear();
            this.txtFirstAired.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "FirstAired", true);

            this.txtNetwork.DataBindings.Clear();
            this.txtNetwork.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Network");

            this.txtNetworkID.DataBindings.Clear();
            this.txtNetworkID.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "NetworkID");

            this.txtStatus.DataBindings.Clear();
            this.txtStatus.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Status");

            this.txtRuntime.DataBindings.Clear();
            this.txtRuntime.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Runtime");

            this.txtRating.DataBindings.Clear();
            this.txtRating.DataBindings.Add("Text", Factories.TvDBFactory.CurrentSeries, "Rating");

            this.grdActors.DataSource = Factories.TvDBFactory.CurrentSeries.Actors;

            this.PopulateGenres();
        }

        /// <summary>
        /// Handles the CurrentSeriesChanged event of the TvDBFactory control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void TvDBFactory_CurrentSeriesChanged(object sender, EventArgs e)
        {
            this.SetupBindings();
        }

        #endregion
    }
}