// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Progress.cs">
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
//   The progress.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.DownloadManager.Model
{
    #region Required Namespaces

    using System;

    using YANFOE.Tools.Models;

    #endregion

    /// <summary>
    ///   The progress.
    /// </summary>
    [Serializable]
    public class Progress : ModelBase
    {
        #region Fields

        /// <summary>
        /// The is busy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        ///   The message field
        /// </summary>
        private string message;

        /// <summary>
        ///   The percent field
        /// </summary>
        private int percent;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="Progress" /> class.
        /// </summary>
        public Progress()
        {
            this.Message = string.Empty;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.OnPropertyChanged("IsBusy");
            }
        }

        /// <summary>
        ///   Gets or sets Message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }

            set
            {
                this.message = value;
                this.OnPropertyChanged("Message");
            }
        }

        /// <summary>
        ///   Gets or sets Percent.
        /// </summary>
        public int Percent
        {
            get
            {
                return this.percent;
            }

            set
            {
                this.percent = value;
                this.OnPropertyChanged("Percent");
            }
        }

        #endregion
    }
}