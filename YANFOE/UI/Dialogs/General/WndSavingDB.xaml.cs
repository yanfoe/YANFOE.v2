// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="WndSavingDB.xaml.cs">
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
// <summary>
//   Interaction logic for WndSavingDB.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.Dialogs.General
{
    #region Required Namespaces

    using System.Windows;

    #endregion

    /// <summary>
    ///   Interaction logic for WndSavingDB.xaml
    /// </summary>
    public partial class WndSavingDB : Window
    {
        #region Fields

        /// <summary>
        /// The movie finished.
        /// </summary>
        private bool movieFinished;

        /// <summary>
        /// The tv finished.
        /// </summary>
        private bool tvFinished;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WndSavingDB"/> class.
        /// </summary>
        public WndSavingDB()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The movie db finished.
        /// </summary>
        public void MovieDBFinished()
        {
            // picMovieDatabase.Image = Properties.Resources.accept32;
            this.movieFinished = true;
            this.Check();
        }

        /// <summary>
        /// The reset.
        /// </summary>
        public void Reset()
        {
            // picMovieDatabase.Image = Properties.Resources.delete32;
            // picTvDatabase.Image = Properties.Resources.delete32;
            // progSavingMovieDB.EditValue = 0;
            // progSavingTvDB.EditValue = 0;
            this.tvFinished = false;
            this.movieFinished = false;
        }

        /// <summary>
        /// The set movie db max.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetMovieDBMax(int value)
        {
            // progSavingMovieDB.Properties.Maximum = value - 1;
        }

        /// <summary>
        /// The set movie value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetMovieValue(int value)
        {
            // progSavingMovieDB.EditValue = value;
        }

        /// <summary>
        /// The set tvdb max.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetTVDBMax(int value)
        {
            // progSavingTvDB.Properties.Maximum = value - 1;
        }

        /// <summary>
        /// The set tvdb value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void SetTVDBValue(int value)
        {
            // progSavingTvDB.EditValue = value;
        }

        /// <summary>
        /// The tv db finished.
        /// </summary>
        public void TvDBFinished()
        {
            // picTvDatabase.Image = Properties.Resources.accept32;
            this.tvFinished = true;
            this.Check();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check.
        /// </summary>
        private void Check()
        {
            if (this.movieFinished && this.tvFinished)
            {
                this.Hide();
            }
        }

        #endregion
    }
}