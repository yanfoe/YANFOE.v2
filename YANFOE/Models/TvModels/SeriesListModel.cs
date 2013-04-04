// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SeriesListModel.cs" company="The YANFOE Project">
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

namespace YANFOE.Models.TvModels
{
    using System;
    using System.Collections.Generic;

    using YANFOE.Models.TvModels.Scan;
    using YANFOE.Tools.Models;

    /// <summary>
    /// The series list model.
    /// </summary>
    [Serializable]
    public class SeriesListModel : ModelBase
    {
        #region Constants and Fields

        /// <summary>
        /// The scan complete.
        /// </summary>
        private bool scanComplete;

        /// <summary>
        /// The series name.
        /// </summary>
        private string seriesName;

        /// <summary>
        /// The series path.
        /// </summary>
        private string seriesPath;

        /// <summary>
        /// The skipped.
        /// </summary>
        private bool skipped;

        /// <summary>
        /// The waiting for scan.
        /// </summary>
        private bool waitingForScan;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesListModel"/> class.
        /// </summary>
        public SeriesListModel()
        {
            this.SeriesName = string.Empty;
            this.waitingForScan = true;

            ScanSeriesPicks = new List<ScanSeriesPick>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets ScanSeriesPicks.
        /// </summary>
        public static List<ScanSeriesPick> ScanSeriesPicks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ScanComplete.
        /// </summary>
        public bool ScanComplete
        {
            get
            {
                return this.scanComplete;
            }

            set
            {
                this.scanComplete = value;
                this.OnPropertyChanged("ScanComplete");
            }
        }

        /// <summary>
        /// Gets or sets SeriesName.
        /// </summary>
        public string SeriesName
        {
            get
            {
                return this.seriesName;
            }

            set
            {
                this.seriesName = value;
                this.OnPropertyChanged("SeriesName");
            }
        }

        /// <summary>
        /// Gets or sets SeriesPath.
        /// </summary>
        public string SeriesPath
        {
            get
            {
                return this.seriesPath;
            }

            set
            {
                this.seriesPath = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Skipped.
        /// </summary>
        public bool Skipped
        {
            get
            {
                return this.skipped;
            }

            set
            {
                this.skipped = value;
                this.OnPropertyChanged("Skipped");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether WaitingForScan.
        /// </summary>
        public bool WaitingForScan
        {
            get
            {
                return this.waitingForScan;
            }

            set
            {
                this.waitingForScan = value;
                this.OnPropertyChanged("WaitingForScan");
            }
        }

        #endregion
    }
}