// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="MoveCopy.cs">
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
//   The move copy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Renamer
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Timers;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;

    #endregion

    /// <summary>
    ///   The move copy.
    /// </summary>
    public class MoveCopy
    {
        #region Fields

        /// <summary>
        ///   A timer object
        /// </summary>
        private readonly Timer tmr = new Timer();

        /// <summary>
        ///   The file system.
        /// </summary>
        private FileSystem fileSystem;

        /// <summary>
        ///   The from path.
        /// </summary>
        private string fromPath = string.Empty;

        /// <summary>
        ///   The too path.
        /// </summary>
        private string tooPath = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the eta value.
        /// </summary>
        public string ETA { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Function to move value from a to b
        /// </summary>
        /// <param name="moveFrom">
        /// The file from. 
        /// </param>
        /// <param name="moveToo">
        /// The file too. 
        /// </param>
        public void Move(string moveFrom, string moveToo)
        {
            this.tooPath = moveToo;
            this.fromPath = moveFrom;

            this.tmr.Elapsed += this.Tmr_Tick;
            this.tmr.Interval = 400;
            this.tmr.Start();

            this.fileSystem = new FileSystem();

            this.fileSystem.CopyProgress += this.FileSystemCopyProgress;

            var bgw = new BackgroundWorker();

            bgw.DoWork += this.BgwDoWork;
            bgw.RunWorkerCompleted += this.BgwRunWorkerCompleted;

            bgw.RunWorkerAsync();

            // this.dialog = new FrmMoveDialog { Too = this.tooPath, From = this.fromPath };
            // this.dialog.ShowDialog();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwDoWork(object sender, DoWorkEventArgs e)
        {
            if (File.Exists(this.tooPath))
            {
                File.Delete(this.tooPath);
                Log.WriteToLog(LogSeverity.Info, 0, "Deleted File", this.tooPath);
            }

            if (Path.GetPathRoot(this.fromPath) == Path.GetPathRoot(this.tooPath))
            {
                File.Move(this.fromPath, this.tooPath);
            }
            else
            {
                this.fileSystem.MoveFile(this.fromPath, this.tooPath);
            }

            Log.WriteToLog(LogSeverity.Info, 0, "Moved File", this.fromPath + " > " + this.tooPath);
        }

        /// <summary>
        /// Handles the RunWorkerCompleted event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data. 
        /// </param>
        private void BgwRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.tmr.Stop();

            // this.dialog.Close();
            // this.dialog = null;
        }

        /// <summary>
        /// Handles the CopyProgress event of the fileSystem control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="CopyProgressEventArgs"/> instance containing the event data. 
        /// </param>
        private void FileSystemCopyProgress(object sender, CopyProgressEventArgs e)
        {
            this.ETA = e.Eta.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The timer tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Tmr_Tick(object sender, EventArgs e)
        {
            // this.dialog.ProgressCurrent = this.progressCurrent;
            // this.dialog.ProgressMax = this.progressMax;
            // this.dialog.ETA = this.eta;
        }

        #endregion
    }
}