// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Keywords.cs">
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
//   Keywords of strings to filter from movie filenames
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Settings.UserSettings
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;

    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   Keywords of strings to filter from movie filenames
    /// </summary>
    [Serializable]
    public class Keywords
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Keywords" /> class.
        /// </summary>
        public Keywords()
        {
            this.ContructDefaultValues();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a collection containing codecs to ignore.
        /// </summary>
        /// <value> A collection containing values to ignore. </value>
        public List<string> Codecs { get; set; }

        /// <summary>
        ///   Gets or sets a collection containing values to ignore.
        /// </summary>
        /// <value> A collection containing values to ignore. </value>
        public List<string> IgnoreNames { get; set; }

        /// <summary>
        ///   Gets or sets Resolutions.
        /// </summary>
        public List<string> Resolutions { get; set; }

        /// <summary>
        ///   Gets or sets Sources.
        /// </summary>
        public Dictionary<string, List<string>> Sources { get; set; }

        /// <summary>
        ///   Gets or sets a collection containing tags to ignore.
        /// </summary>
        /// <value> A collection containing values to ignore. </value>
        public List<string> Tags { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///   The get sources as list.
        /// </summary>
        /// <returns> Sources collection </returns>
        public ThreadedBindingList<string> GetSourcesAsList()
        {
            var outputList = new ThreadedBindingList<string>();

            foreach (var keyPair in this.Sources)
            {
                outputList.Add(keyPair.Key);

                foreach (string valueItems in keyPair.Value)
                {
                    outputList.Add(valueItems);
                }
            }

            return outputList;
        }

        #endregion

        #region Methods

        /// <summary>
        ///   Contructs the values.
        /// </summary>
        private void ContructDefaultValues()
        {
            this.GenerateIgnoreNames();

            this.GenerateCodecs();

            this.GenerateTags();

            this.GenerateSources();

            this.GenerateResolutions();
        }

        /// <summary>
        ///   The generate codecs.
        /// </summary>
        private void GenerateCodecs()
        {
            this.Codecs = new List<string> { "xvid", "divx", "h264", "mp3", "ogg", "x264" };
        }

        /// <summary>
        ///   The generate ignore names.
        /// </summary>
        private void GenerateIgnoreNames()
        {
            this.IgnoreNames = new List<string>
                {
                    "LIMITED", 
                    "KLAXXON", 
                    "AXXO", 
                    "NahNc3", 
                    "PUKKA", 
                    "iDHD", 
                    "PROPER", 
                    "REPACK", 
                    "DSR", 
                    "STV", 
                    "UNRATED", 
                    "RERIP", 
                    "Special Edition", 
                    "Extended Edition", 
                    "Sp Edition"
                };
        }

        /// <summary>
        ///   The generate resolutions.
        /// </summary>
        private void GenerateResolutions()
        {
            this.Resolutions = new List<string> { "1080p", "1080i", "720p", "576p", "480p" };
        }

        /// <summary>
        ///   The generate sources.
        /// </summary>
        private void GenerateSources()
        {
            this.Sources = new Dictionary<string, List<string>> { { "HDDVD", new List<string>() } };

            this.Sources["HDDVD"].AddRange(new[] { "HD-DVD", "HDDVDRIP", "HD DVD" });

            this.Sources.Add("BluRay", new List<string>());
            this.Sources["BluRay"].AddRange(new[] { "BLU-RAY" });

            this.Sources.Add("BDRip", new List<string>());
            this.Sources["BDRip"].AddRange(new[] { "BDRIP", "BLURAYRIP" });

            this.Sources.Add("CAM", new List<string>());

            this.Sources.Add("DSRip", new List<string>());

            this.Sources.Add("D-THEATER", new List<string>());
            this.Sources["D-THEATER"].AddRange(new[] { "DTH", "DTHEATER" });

            this.Sources.Add("DVD", new List<string>());

            this.Sources.Add("DVD5", new List<string>());

            this.Sources.Add("DVD9", new List<string>());

            this.Sources.Add("DVDRip", new List<string>());
            this.Sources["DVDRip"].AddRange(new[] { "DVDR" });

            this.Sources.Add("DVDSCR", new List<string>());

            this.Sources.Add("HD2DVD", new List<string>());

            this.Sources.Add("HDTV", new List<string>());

            this.Sources.Add("HRHDTV", new List<string>());

            this.Sources.Add("LINE", new List<string>());

            this.Sources.Add("MVCD", new List<string>());

            this.Sources.Add("PDTV", new List<string>());

            this.Sources.Add("R5", new List<string>());

            this.Sources.Add("RC", new List<string>());

            this.Sources.Add("SDTV", new List<string>());
            this.Sources["SDTV"].AddRange(new[] { "TVRip", "PAL", "NTSC" });

            this.Sources.Add("TS", new List<string>());

            this.Sources.Add("VCD", new List<string>());

            this.Sources.Add("VHSRip", new List<string>());
        }

        /// <summary>
        ///   The generate tags.
        /// </summary>
        private void GenerateTags()
        {
            this.Tags = new List<string>
                {
                    "HDTV", 
                    "PDTV", 
                    "DVDRip", 
                    "DVDSCR", 
                    "DSRip", 
                    "CAM", 
                    "R5", 
                    "RC", 
                    "LINE", 
                    "HD2DVD", 
                    "DVD", 
                    "DVD5", 
                    "DVD9", 
                    "HRHDTV", 
                    "MVCD", 
                    "VCD", 
                    "VHSRip", 
                    "BluRay", 
                    "HDDVD", 
                    "D-THEATER", 
                    "SDTV", 
                    "DTS", 
                    "AC3", 
                    "5.1", 
                    "DD2.0", 
                    "DD5.1", 
                    "Sub", 
                    "eng", 
                    "mp3", 
                    "BRRip", 
                    "HD", 
                    "MP4"
                };
        }

        #endregion
    }
}