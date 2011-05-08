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
            try
            {
                // Use the RichTextBox to create the initial RTF code
                richTextBox.Clear();

                if (string.IsNullOrEmpty(strTextToAdd))
                {
                    return;
                }

                richTextBox.AppendText(strTextToAdd);
                string strRtf = richTextBox.Rtf;
                richTextBox.Clear();

                /* 
                 * ADD COLOUR TABLE TO THE HEADER FIRST 
                 * */

                // Search for colour table info, if it exists (which it shouldn't)
                // remove it and replace with our one
                int tableStart = strRtf.IndexOf("colortbl;");

                if (tableStart != -1)
                {
                    // find end of colortbl tab by searching
                    // forward from the colortbl tab itself
                    int tableEnd = strRtf.IndexOf('}', tableStart);
                    strRtf = strRtf.Remove(tableStart, tableEnd - tableStart);

                    // now insert new colour table at index of old colortbl tag
                    strRtf = strRtf.Insert(
                        tableStart,
                        "colortbl ;\\red255\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
                }
                else
                {
                    // find index of start of header
                    int startIndex = strRtf.IndexOf("\\rtf");

                    // get index of where we'll insert the colour table
                    // try finding opening bracket of first property of header first                
                    int insertLoc = strRtf.IndexOf('{', startIndex);

                    // if there is no property, we'll insert colour table
                    // just before the end bracket of the header
                    if (insertLoc == -1)
                    {
                        insertLoc = strRtf.IndexOf('}', startIndex) - 1;
                    }

                    // insert the colour table at our chosen location                
                    strRtf = strRtf.Insert(
                        insertLoc,
                        "{\\colortbl ;\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue255;}");
                }

                for (int i = 0; i < strRtf.Length; i++)
                {
                    if (strRtf[i] == '<')
                    {
                        // add RTF tags after symbol 
                        // Check for comments tags 
                        strRtf = strRtf[i + 1] == '!' ? strRtf.Insert(i + 4, "\\cf2 ") : strRtf.Insert(i + 1, "\\cf1 ");

                        // add RTF before symbol
                        strRtf = strRtf.Insert(i, "\\cf3 ");

                        // skip forward past the characters we've just added
                        // to avoid getting trapped in the loop
                        i += 6;
                    }
                    else if (strRtf[i] == '>')
                    {
                        // add RTF tags after character
                        strRtf = strRtf.Insert(i + 1, "\\cf0 ");

                        // Check for comments tags
                        if (strRtf[i - 1] == '-')
                        {
                            strRtf = strRtf.Insert(i - 2, "\\cf3 ");

                            // skip forward past the 6 characters we've just added
                            i += 8;
                        }
                        else
                        {
                            strRtf = strRtf.Insert(i, "\\cf3 ");

                            // skip forward past the 6 characters we've just added
                            i += 6;
                        }
                    }
                }

                richTextBox.Rtf = strRtf;
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, ex.Message, ex.StackTrace);
            }
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