// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MoveCopy.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Renamer
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.UI.Dialogs.General;

    /// <summary>
    /// The move copy.
    /// </summary>
    public class MoveCopy
    {
        #region Constants and Fields

        /// <summary>
        /// A timer object
        /// </summary>
        private readonly Timer tmr = new Timer();

        /// <summary>
        /// The dialog.
        /// </summary>
        private FrmMoveDialog dialog;

        /// <summary>
        /// The eta value.
        /// </summary>
        private string eta;

        /// <summary>
        /// The file system.
        /// </summary>
        private FileSystem fileSystem;

        /// <summary>
        /// The from path.
        /// </summary>
        private string fromPath = string.Empty;

        /// <summary>
        /// The progress current.
        /// </summary>
        private int progressCurrent;

        /// <summary>
        /// The progress max.
        /// </summary>
        private int progressMax;

        /// <summary>
        /// The too path.
        /// </summary>
        private string tooPath = string.Empty;

        #endregion

        #region Public Methods

        /// <summary>
        /// Function to move value from a to b
        /// </summary>
        /// <param name="moveFrom">The file from.</param>
        /// <param name="moveToo">The file too.</param>
        public void Move(string moveFrom, string moveToo)
        {
            this.tooPath = moveToo;
            this.fromPath = moveFrom;

            this.tmr.Tick += this.Tmr_Tick;
            this.tmr.Interval = 400;
            this.tmr.Start();

            this.fileSystem = new FileSystem();

            this.fileSystem.CopyProgress += this.FileSystem_CopyProgress;

            var bgw = new BackgroundWorker();

            bgw.DoWork += this.Bgw_DoWork;
            bgw.RunWorkerCompleted += this.Bgw_RunWorkerCompleted;

            bgw.RunWorkerAsync();

            this.dialog = new FrmMoveDialog { Too = this.tooPath, From = this.fromPath };
            this.dialog.ShowDialog();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
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
        /// Handles the RunWorkerCompleted event of the bgw control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.tmr.Stop();
            this.dialog.Close();
            this.dialog = null;
        }

        /// <summary>
        /// Handles the CopyProgress event of the fileSystem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CopyProgressEventArgs"/> instance containing the event data.</param>
        private void FileSystem_CopyProgress(object sender, CopyProgressEventArgs e)
        {
            this.progressCurrent = (int)e.CopiedBytes / 1024;
            this.progressMax = (int)e.TotalBytes / 1024;

            this.eta = e.Eta.ToString();
        }

        /// <summary>
        /// Handles the Tick event of the tmr control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Tmr_Tick(object sender, EventArgs e)
        {
            this.dialog.ProgressCurrent = this.progressCurrent;
            this.dialog.ProgressMax = this.progressMax;
            this.dialog.ETA = this.eta;
        }

        #endregion
    }
}