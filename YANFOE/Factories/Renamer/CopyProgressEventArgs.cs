// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="CopyProgressEventArgs.cs">
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
//   EventArgs derived type which holds the custom event fields
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Renamer
{
    #region Required Namespaces

    using System;

    #endregion

    /// <summary>
    ///   EventArgs derived type which holds the custom event fields
    /// </summary>
    public class CopyProgressEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        ///   Number of copied bytes
        /// </summary>
        public readonly long CopiedBytes;

        /// <summary>
        ///   Time Elapsed
        /// </summary>
        public readonly double ElapsedTime;

        /// <summary>
        ///   Estimated time left in seconds
        /// </summary>
        public readonly double Eta;

        /// <summary>
        ///   Percentage of copied bytes
        /// </summary>
        public readonly decimal Percentage;

        /// <summary>
        ///   Total bytes to be copied
        /// </summary>
        public readonly long TotalBytes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyProgressEventArgs"/> class.
        /// </summary>
        /// <param name="percentage">
        /// The percentage. 
        /// </param>
        /// <param name="copiedBytes">
        /// The copied bytes. 
        /// </param>
        /// <param name="totalBytes">
        /// The total bytes. 
        /// </param>
        /// <param name="eta">
        /// The remaining eta. 
        /// </param>
        /// <param name="elapsedTime">
        /// The elapsed time. 
        /// </param>
        public CopyProgressEventArgs(
            decimal percentage, long copiedBytes, long totalBytes, double eta, double elapsedTime)
        {
            this.Percentage = percentage;
            this.CopiedBytes = copiedBytes;
            this.TotalBytes = totalBytes;
            this.Eta = eta;
            this.ElapsedTime = elapsedTime;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether Cancel.
        /// </summary>
        public bool Cancel { get; set; }

        #endregion
    }
}