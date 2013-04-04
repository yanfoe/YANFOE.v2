// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="FileCleanUp.cs">
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
//   The file clean up.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Tools.Clean
{
    #region Required Namespaces

    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Settings;

    #endregion

    /// <summary>
    /// The file clean up.
    /// </summary>
    public static class FileCleanUp
    {
        #region Enums

        /// <summary>
        /// The move file flags.
        /// </summary>
        [Flags]
        internal enum MoveFileFlags
        {
            /// <summary>
            /// The none.
            /// </summary>
            None = 0, 

            /// <summary>
            /// The replace existing.
            /// </summary>
            ReplaceExisting = 1, 

            /// <summary>
            /// The copy allowed.
            /// </summary>
            CopyAllowed = 2, 

            /// <summary>
            /// The delay until reboot.
            /// </summary>
            DelayUntilReboot = 4, 

            /// <summary>
            /// The write through.
            /// </summary>
            WriteThrough = 8, 

            /// <summary>
            /// The create hardlink.
            /// </summary>
            CreateHardlink = 16, 

            /// <summary>
            /// The fail if not trackable.
            /// </summary>
            FailIfNotTrackable = 32, 
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The clean folder.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="recursive">
        /// The recursive.
        /// </param>
        /// <param name="force">
        /// The force.
        /// </param>
        public static void CleanFolder(string path, bool recursive = true, bool force = true)
        {
            RemoveNonYanfoeFiles(path, recursive, force);
            RemoveEmptyFolders(path);
        }

        /// <summary>
        /// The remove empty folders.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public static void RemoveEmptyFolders(string path)
        {
            string LogCategory = "Clean > FileCleanUp > RemoveEmptyFolders";
            Log.WriteToLog(
                LogSeverity.Debug, 
                LoggerName.GeneralLog, 
                LogCategory, 
                string.Format("Removing empty folders in {0}", path));

            foreach (var directory in Directory.GetDirectories(path))
            {
                RemoveEmptyFolders(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                    Log.WriteToLog(
                        LogSeverity.Info, 
                        LoggerName.GeneralLog, 
                        LogCategory, 
                        string.Format("Removed empty folders {0}", directory));
                }
            }
        }

        /// <summary>
        /// The remove non yanfoe files.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="recursive">
        /// The recursive.
        /// </param>
        /// <param name="force">
        /// The force.
        /// </param>
        public static void RemoveNonYanfoeFiles(string path, bool recursive = true, bool force = true)
        {
            string LogCategory = "Clean > FileCleanUp > RemoveNonYanfoeFiles";
            string options = recursive ? force ? "recursive, force" : "recursive" : force ? "force" : "none";
            Log.WriteToLog(
                LogSeverity.Debug, 
                LoggerName.GeneralLog, 
                LogCategory, 
                string.Format("Deleting non YANFOE files from {0} with options: {1}", path, options));

            // Get file list
            string[] paths;
            if (recursive)
            {
                paths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            }
            else
            {
                paths = Directory.GetFiles(path, "*.*");
            }

            var saveExts = Get.InOutCollection.SubtitleExtentions;
            saveExts.AddRange(Get.InOutCollection.VideoExtentions);
            saveExts.AddRange(Get.InOutCollection.MusicExtentions);

            // Attempt to delete file
            foreach (var p in paths)
            {
                Log.WriteToLog(LogSeverity.Debug, LoggerName.GeneralLog, LogCategory, string.Format("Checking {0}", p));

                var ext = Path.GetExtension(p).Replace(".", string.Empty).ToLower();
                if (saveExts.Contains(ext))
                {
                    Log.WriteToLog(
                        LogSeverity.Debug, 
                        LoggerName.GeneralLog, 
                        LogCategory, 
                        string.Format("Skipping {0}. {1} is safe listed.", p, Path.GetExtension(p)));
                    continue;
                }

                try
                {
                    if ((File.GetAttributes(p) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        Log.WriteToLog(
                            LogSeverity.Debug, 
                            LoggerName.GeneralLog, 
                            LogCategory, 
                            string.Format("Removing 'read-only' flag from file {0}", p));
                        File.SetAttributes(p, FileAttributes.Normal);
                    }

                    File.Delete(p);
                    Log.WriteToLog(
                        LogSeverity.Debug, LoggerName.GeneralLog, LogCategory, string.Format("Deleted {0}", p));
                }
                catch (Exception ex)
                {
                    if (ex is IOException || ex is UnauthorizedAccessException)
                    {
                        if (force)
                        {
                            if (!MoveFileEx(p, null, MoveFileFlags.DelayUntilReboot))
                            {
                                Log.WriteToLog(
                                    LogSeverity.Error, 
                                    LoggerName.GeneralLog, 
                                    LogCategory, 
                                    string.Format("Unable to schedule a delete on reboot for file {0}", path));
                            }

                            Log.WriteToLog(
                                LogSeverity.Info, 
                                LoggerName.GeneralLog, 
                                LogCategory, 
                                string.Format("Deleting {0} on next reboot", p));
                        }
                        else
                        {
                            Log.WriteToLog(
                                LogSeverity.Info, 
                                LoggerName.GeneralLog, 
                                LogCategory, 
                                string.Format("Skipping {0}. File is in use.", p));
                        }
                    }
                    else if (ex is ArgumentException || ex is ArgumentNullException || ex is DirectoryNotFoundException
                             || ex is NotSupportedException || ex is PathTooLongException)
                    {
                        // Wtf?
                        Log.WriteToLog(
                            LogSeverity.Error, 
                            LoggerName.GeneralLog, 
                            LogCategory, 
                            string.Format("Skipping {0}. Unknown error occured: {1}", p, ex.Message));
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The move file ex.
        /// </summary>
        /// <param name="lpExistingFileName">
        /// The lp existing file name.
        /// </param>
        /// <param name="lpNewFileName">
        /// The lp new file name.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName, MoveFileFlags dwFlags);

        #endregion
    }
}