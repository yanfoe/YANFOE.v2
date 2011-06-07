// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NFOPreviewUserControl.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.UserControls.CommonControls
{
    using System;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using DevExpress.XtraEditors;

    using YANFOE.Factories;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Tools.Xml;

    /// <summary>
    /// The nfo preview user control.
    /// </summary>
    public partial class NFOPreviewUserControl : XtraUserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NFOPreviewUserControl"/> class.
        /// </summary>
        public NFOPreviewUserControl()
        {
            this.InitializeComponent();

            MovieDBFactory.CurrentMovieChanged += this.MovieDBFactory_CurrentMovieChanged;
            MovieDBFactory.CurrentMovieValueChanged += this.MovieDBFactory_CurrentMovieChanged;
        }

        #endregion

        #region Enums

        /// <summary>
        /// The tv or movies.
        /// </summary>
        public enum TvOrMovies
        {
            /// <summary>
            /// The tv episode.
            /// </summary>
            TvEpisode, 

            /// <summary>
            /// The tv series.
            /// </summary>
            TvSeries, 

            /// <summary>
            /// The movies.
            /// </summary>
            Movies
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets PreviewArea.
        /// </summary>
        public TvOrMovies PreviewArea { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The add coloured text.
        /// </summary>
        /// <param name="strTextToAdd">The str text to add.</param>
        /// <param name="richTextBox">The rich text box.</param>
        public static void AddColouredText(string strTextToAdd, RichTextBox richTextBox)
        {
            XFormat.AddColouredText(strTextToAdd, richTextBox);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the CurrentMovieChanged event of the MovieDBFactory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MovieDBFactory_CurrentMovieChanged(object sender, EventArgs e)
        {
            if (this.layoutControl1.TabIndex == 0)
            {
                AddColouredText(MovieDBFactory.GetCurrentMovie().YamjXml, this.rtbYAMJ);
            }
        }

        #endregion
    }
}