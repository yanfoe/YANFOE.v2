using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YANFOE.Factories.InOut;

namespace YANFOE.UI.Dialogs.General
{
    /// <summary>
    /// Interaction logic for WndSavingDialog.xaml
    /// </summary>
    public partial class WndSavingDialog : Window
    {
        public WndSavingDialog()
        {
            InitializeComponent();
        }

        #region Properties

        public int EpisodeMax { get; set; }
        ///// <summary>
        ///// Gets or sets EpisodeMax.
        ///// </summary>
        //public int EpisodeMax
        //{
        //    get
        //    {
        //        return this.progressBarEpisode.Maximum;
        //    }

        //    set
        //    {
        //        this.progressBarEpisode.Maximum = value;
        //    }
        //}

        public int SeasonMax { get; set; }
        ///// <summary>
        ///// Gets or sets the season max.
        ///// </summary>
        ///// <value>
        ///// The season max.
        ///// </value>
        //public int SeasonMax
        //{
        //    get
        //    {
        //        return this.progressBarSeason.Maximum;
        //    }

        //    set
        //    {
        //        this.progressBarSeason.Maximum = value;
        //    }
        //}


        public int SeriesMax { get; set; }
        ///// <summary>
        ///// Gets or sets the series max.
        ///// </summary>
        ///// <value>
        ///// The series max.
        ///// </value>
        //public int SeriesMax
        //{
        //    get
        //    {
        //        return this.progressBarSeries.Maximum;
        //    }

        //    set
        //    {
        //        this.progressBarSeries.Maximum = value;
        //    }
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Click event of the BtnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            OutFactory.Cancel = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            //this.progressBarSeries.Value = OutFactory.ProgressModel.SeriesCurrent;
            //this.progressBarSeason.Value = OutFactory.ProgressModel.SeasonCurrent;
            //this.progressBarEpisode.Value = OutFactory.ProgressModel.EpisodeCurrent;

            //this.lblSeries.Text = OutFactory.ProgressModel.SeriesText;
            //this.lblSeason.Text = OutFactory.ProgressModel.SeasonText;
            //this.lblEpisode.Text = OutFactory.ProgressModel.EpisodeText;
        }

        #endregion
    }
}
