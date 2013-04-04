// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Media.cs">
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
//   The media.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The media.
    /// </summary>
    [Serializable]
    public class Media : ModelBase
    {
        #region Fields

        /// <summary>
        ///   A Media Source Dictionary
        /// </summary>
        private Dictionary<string, List<string>> mediaSource;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Media" /> class.
        /// </summary>
        public Media()
        {
            this.ContructDefaultValues();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the list of possible media sources.
        /// </summary>
        /// <value> The media source. </value>
        public Dictionary<string, List<string>> MediaSource
        {
            get
            {
                if (this.mediaSource == null)
                {
                    this.ContructMediaSource();
                }

                return this.mediaSource;
            }

            set
            {
                this.mediaSource = value;
                this.OnPropertyChanged("MediaSource");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///   The contruct default values.
        /// </summary>
        private void ContructDefaultValues()
        {
            this.ContructMediaSource();
        }

        /// <summary>
        ///   The contruct media source.
        /// </summary>
        private void ContructMediaSource()
        {
            this.MediaSource = new Dictionary<string, List<string>>
                {
                    { "BluRay", new List<string> { "BDRIP", "BLURAYRIP", "BLU-RAY" } }, 
                    { "CAM", new List<string>() }, 
                    { "DSRip", new List<string>() }, 
                    { "D-THEATER", new List<string> { "DTH", "DTHEATER" } }, 
                    { "DVD", new List<string>() }, 
                    { "DVD5", new List<string>() }, 
                    { "DVD9", new List<string>() }, 
                    { "DVDRip", new List<string>() }, 
                    { "DVDSCR", new List<string>() }, 
                    { "HD2DVD", new List<string>() }, 
                    { "HDDVD", new List<string> { "HD-DVD", "HDDVDRIP" } }, 
                    { "HDTV", new List<string>() }, 
                    { "LINE", new List<string>() }, 
                    { "MVCD", new List<string>() }, 
                    { "PDTV", new List<string>() }, 
                    { "R5", new List<string>() }, 
                    { "SDTV", new List<string> { "TVRip", "PAL", "NTSC" } }, 
                    { "TS", new List<string>() }, 
                    { "VCD", new List<string>() }, 
                    { "VHSRip", new List<string>() }
                };
        }

        #endregion
    }
}