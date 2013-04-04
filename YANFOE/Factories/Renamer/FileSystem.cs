// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileSystem.cs">
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
//   The file system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Renamer
{
    #region Required Namespaces

    using System;
    using System.IO;

    #endregion

    /// <summary>
    ///   The file system.
    /// </summary>
    public class FileSystem
    {
        // Private delegate linked list (explicitly defined)
        #region Fields

        /// <summary>
        ///   The _buffer size.
        /// </summary>
        private int bufferSize = 3 * 1024 * 1024;

        /// <summary>
        ///   The copy progress event handler delegate.
        /// </summary>
        private EventHandler<CopyProgressEventArgs> copyProgressEventHandlerDelegate;

        #endregion

        #region Delegates

        /// <summary>
        ///   This represents the delegate method prototype that event receivers must implement
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="args"> The <see cref="FileCopyCompletedEventArgs" /> instance containing the event data. </param>
        public delegate void FileCopyCompletedEventHandler(object sender, FileCopyCompletedEventArgs args);

        #endregion

        #region Public Events

        /// <summary>
        ///   Provide feedback for file processing progress
        /// </summary>
        public event EventHandler<CopyProgressEventArgs> CopyProgress
        {
            // Explicit event definition with accessor methods
            add
            {
                this.copyProgressEventHandlerDelegate =
                    (EventHandler<CopyProgressEventArgs>)Delegate.Combine(this.copyProgressEventHandlerDelegate, value);
            }

            remove
            {
                this.copyProgressEventHandlerDelegate =
                    (EventHandler<CopyProgressEventArgs>)Delegate.Remove(this.copyProgressEventHandlerDelegate, value);
            }
        }

        /// <summary>
        ///   Occurs when [file copy completed].
        /// </summary>
        public event FileCopyCompletedEventHandler FileCopyCompleted;

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets BufferSize.
        /// </summary>
        public int BufferSize
        {
            get
            {
                return this.bufferSize;
            }

            set
            {
                this.bufferSize = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Copies the sourceFile to the outFile
        /// </summary>
        /// <param name="sourceFile">
        /// Source file to be copied 
        /// </param>
        /// <param name="outFile">
        /// File copy destination 
        /// </param>
        /// <returns>
        /// A boolean value that indicate a successful copy finished. 
        /// </returns>
        public bool CopyFile(string sourceFile, string outFile)
        {
            var fi = new FileInfo(sourceFile);
            long totalBytes = fi.Length;
            bool success = true;

            if (totalBytes == 0)
            {
                // no file data
                File.Create(outFile).Close();
            }
            else
            {
                var readStream = new FileStream(sourceFile, FileMode.Open);
                var writeStream = new FileStream(outFile, FileMode.CreateNew);
                int readBytes = 1;
                DateTime startTime = DateTime.Now;
                long totalCopiedBytes = 0;
                var buffer = new byte[this.bufferSize];

                while (readBytes > 0)
                {
                    readBytes = readStream.Read(buffer, 0, this.bufferSize);
                    totalCopiedBytes += readBytes;
                    writeStream.Write(buffer, 0, readBytes);

                    double m = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                    double speed = totalCopiedBytes / m;
                    double eta = (totalBytes - totalCopiedBytes) / speed;

                    var evt = new CopyProgressEventArgs(
                        (decimal)totalCopiedBytes / totalBytes, totalCopiedBytes, totalBytes, eta, m);
                    this.OnCopyProgress(evt);

                    if (evt.Cancel)
                    {
                        success = false;
                        break;
                    }
                }

                writeStream.Close();
                readStream.Close();
            }

            // If everthing is ok copy file attributes to the newly created file.
            if (success)
            {
                File.SetCreationTime(outFile, File.GetCreationTime(sourceFile));
                File.SetLastWriteTime(outFile, File.GetLastWriteTime(sourceFile));
                File.SetAttributes(outFile, File.GetAttributes(sourceFile));
            }
            else
            {
                if (File.Exists(outFile))
                {
                    File.Delete(outFile);
                }
            }

            this.OnFileCopyCompleted(new FileCopyCompletedEventArgs(success));
            return success;
        }

        /// <summary>
        /// Move file.
        /// </summary>
        /// <param name="sourceFile">
        /// The source file. 
        /// </param>
        /// <param name="outFile">
        /// The out file. 
        /// </param>
        /// <returns>
        /// The move file. 
        /// </returns>
        public bool MoveFile(string sourceFile, string outFile)
        {
            if (this.CopyFile(sourceFile, outFile))
            {
                File.Delete(sourceFile);
                return true;
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is the method that is responsible for notifying
        ///   receivers that the event occurred
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnCopyProgress(CopyProgressEventArgs e)
        {
            if (this.copyProgressEventHandlerDelegate != null)
            {
                this.copyProgressEventHandlerDelegate(this, e);
            }
        }

        /// <summary>
        /// This is the method that is responsible for notifying
        ///   receivers that the event occurred
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected virtual void OnFileCopyCompleted(FileCopyCompletedEventArgs e)
        {
            if (this.FileCopyCompleted != null)
            {
                this.FileCopyCompleted(this, e);
            }
        }

        #endregion
    }
}