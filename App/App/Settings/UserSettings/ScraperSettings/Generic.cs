// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Generic.cs" company="The YANFOE Project">
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

namespace YANFOE.Settings.UserSettings.ScraperSettings
{
    using System.Globalization;

    /// <summary>
    /// The runtime type.
    /// </summary>
    public enum RuntimeType
    {
        /// <summary>
        /// RuntimeType: minutes.
        /// </summary>
        Minutes, 

        /// <summary>
        /// RuntimeType: Hh_MMm.
        /// </summary>
        Hh_MMm
    }

    /// <summary>
    /// The generic.
    /// </summary>
    public class Generic
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Generic"/> class.
        /// </summary>
        public Generic()
        {
            this.FormatDateTime = false;
            this.FormatDateTimeValue = string.Empty;

            this.MaximumActors = 10;

            this.NumberDecimalSeperator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

            this.NfoEncoding = "utf-8";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether FormatDateTime.
        /// </summary>
        public bool FormatDateTime { get; set; }

        /// <summary>
        /// Gets or sets FormatDateTimeValue.
        /// </summary>
        public string FormatDateTimeValue { get; set; }

        /// <summary>
        /// Gets or sets FormatRuntime.
        /// </summary>
        public RuntimeType FormatRuntime { get; set; }

        /// <summary>
        /// Gets or sets MaximumActors.
        /// </summary>
        public int MaximumActors { get; set; }

        /// <summary>
        /// Gets or sets NfoEncoding.
        /// </summary>
        public string NfoEncoding { get; set; }

        /// <summary>
        /// Gets or sets NumberDecimalSeperator.
        /// </summary>
        public string NumberDecimalSeperator { get; set; }

        #endregion
    }
}