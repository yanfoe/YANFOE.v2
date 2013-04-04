// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Log.cs">
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
//   Handles all logging
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.Logs
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.InternalApps.Logs.Model;
    using YANFOE.Settings;
    using YANFOE.Tools.UI;

    #endregion

    /// <summary>
    ///   Handles all logging
    /// </summary>
    public static class Log
    {
        #region Static Fields

        /// <summary>
        ///   The main file log.
        /// </summary>
        private static readonly FileLogger MainFileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "mainFileLog.txt"));

        /// <summary>
        ///   Collection of loggers by LoggerName
        /// </summary>
        private static readonly IDictionary<LoggerName, FileLogger> FileLoggers;

        /// <summary>
        ///   Collection of loggers by Thread MovieUniqueId
        /// </summary>
        private static readonly IDictionary<LoggerName, ThreadedBindingList<LogModel>> InternalLoggers;

        /// <summary>
        ///   The log state.
        /// </summary>
        private static readonly Dictionary<LoggerName, LogState> LoggerLogState;

        /// <summary>
        ///   The main internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> MainInternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 1 file log.
        /// </summary>
        private static readonly FileLogger Thread1FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread1FileLog.txt"));

        /// <summary>
        ///   The thread 1 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread1InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 2 file log.
        /// </summary>
        private static readonly FileLogger Thread2FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread2FileLog.txt"));

        /// <summary>
        ///   The thread 2 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread2InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 3 file log.
        /// </summary>
        private static readonly FileLogger Thread3FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread3FileLog.txt"));

        /// <summary>
        ///   The thread 3 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread3InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 4 file log.
        /// </summary>
        private static readonly FileLogger Thread4FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread4FileLog.txt"));

        /// <summary>
        ///   The thread 4 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread4InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 5 file log.
        /// </summary>
        private static readonly FileLogger Thread5FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread5FileLog.txt"));

        /// <summary>
        ///   The thread 5 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread5InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 6 file log.
        /// </summary>
        private static readonly FileLogger Thread6FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread6FileLog.txt"));

        /// <summary>
        ///   The thread 6 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread6InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 7 file log.
        /// </summary>
        private static readonly FileLogger Thread7FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread7FileLog.txt"));

        /// <summary>
        ///   The thread 7 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread7InternalLog = new ThreadedBindingList<LogModel>();

        /// <summary>
        ///   The thread 4 file log.
        /// </summary>
        private static readonly FileLogger Thread8FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture, 
                    "{0}{1}Logs{1}{2}", 
                    Directory.GetCurrentDirectory(), 
                    Path.DirectorySeparatorChar, 
                    "thread8FileLog.txt"));

        /// <summary>
        ///   The thread 8 internal log.
        /// </summary>
        private static readonly ThreadedBindingList<LogModel> Thread8InternalLog = new ThreadedBindingList<LogModel>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="Log" /> class.
        /// </summary>
        static Log()
        {
            LogThreshold = LogSeverity.Debug;
            LoggerLogState = new Dictionary<LoggerName, LogState>
                {
                    { LoggerName.GeneralLog, LogState.Started }, 
                    { LoggerName.Downloader1, LogState.Started }, 
                    { LoggerName.Downloader2, LogState.Started }, 
                    { LoggerName.Downloader3, LogState.Started }, 
                    { LoggerName.Downloader4, LogState.Started }, 
                    { LoggerName.Downloader5, LogState.Started }, 
                    { LoggerName.Downloader6, LogState.Started }, 
                    { LoggerName.Downloader7, LogState.Started }, 
                    { LoggerName.Downloader8, LogState.Started }
                };

            FileLoggers = new Dictionary<LoggerName, FileLogger>
                {
                    { LoggerName.GeneralLog, MainFileLog }, 
                    { LoggerName.Downloader1, Thread1FileLog }, 
                    { LoggerName.Downloader2, Thread2FileLog }, 
                    { LoggerName.Downloader3, Thread3FileLog }, 
                    { LoggerName.Downloader4, Thread4FileLog }, 
                    { LoggerName.Downloader5, Thread5FileLog }, 
                    { LoggerName.Downloader6, Thread6FileLog }, 
                    { LoggerName.Downloader7, Thread7FileLog }, 
                    { LoggerName.Downloader8, Thread8FileLog }
                };

            InternalLoggers = new Dictionary<LoggerName, ThreadedBindingList<LogModel>>
                {
                    { LoggerName.GeneralLog, MainInternalLog }, 
                    { LoggerName.Downloader1, Thread1InternalLog }, 
                    { LoggerName.Downloader2, Thread2InternalLog }, 
                    { LoggerName.Downloader3, Thread3InternalLog }, 
                    { LoggerName.Downloader4, Thread4InternalLog }, 
                    { LoggerName.Downloader5, Thread5InternalLog }, 
                    { LoggerName.Downloader6, Thread6InternalLog }, 
                    { LoggerName.Downloader7, Thread7InternalLog }, 
                    { LoggerName.Downloader8, Thread8InternalLog }
                };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the log threshold.
        /// </summary>
        public static LogSeverity LogThreshold { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Clear internal logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        public static void ClearInternalLogger(LoggerName loggerName)
        {
            InternalLoggers[loggerName].Clear();
        }

        /// <summary>
        /// Disable logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        public static void DisableLogger(LoggerName loggerName)
        {
            LoggerLogState[loggerName] = LogState.Stopped;
        }

        /// <summary>
        /// Enable logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        public static void EnableLogger(LoggerName loggerName)
        {
            LoggerLogState[loggerName] = LogState.Started;
        }

        /// <summary>
        /// Get internal logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        /// <returns>
        /// The internal loggers 
        /// </returns>
        public static ThreadedBindingList<LogModel> GetInternalLogger(LoggerName loggerName)
        {
            return InternalLoggers[loggerName];
        }

        /// <summary>
        /// The logger status.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        /// <returns>
        /// Return log state 
        /// </returns>
        public static LogState LoggerStatus(LoggerName loggerName)
        {
            return LoggerLogState[loggerName];
        }

        /// <summary>
        /// Pause logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        public static void PauseLogger(LoggerName loggerName)
        {
            LoggerLogState[loggerName] = LogState.Paused;
        }

        /// <summary>
        /// Set file path.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        /// <param name="path">
        /// The file path. 
        /// </param>
        public static void SetFilePath(LoggerName loggerName, string path)
        {
            FileLoggers[loggerName].FileName = path;
        }

        /// <summary>
        /// Write to log.
        /// </summary>
        /// <param name="logSeverity">
        /// The log severity. 
        /// </param>
        /// <param name="loggerName">
        /// The logger name. 
        /// </param>
        /// <param name="category">
        /// The category. 
        /// </param>
        /// <param name="message">
        /// The message. 
        /// </param>
        public static void WriteToLog(LogSeverity logSeverity, LoggerName loggerName, string category, string message)
        {
            if (!Get.LogSettings.EnableLog)
            {
                return;
            }

            if (LoggerLogState[loggerName] == LogState.Stopped)
            {
                return;
            }

            FileLogger fileLogger = FileLoggers[loggerName];

            ThreadedBindingList<LogModel> internalLogger = InternalLoggers[loggerName];

            if (logSeverity >= LogThreshold)
            {
                LogFile(logSeverity, fileLogger, category, message);
                LogInternal(logSeverity, internalLogger, category, message);
            }
        }

        /// <summary>
        /// Write to log.
        /// </summary>
        /// <param name="logSeverity">
        /// The log severity. 
        /// </param>
        /// <param name="threadID">
        /// The thread id. 
        /// </param>
        /// <param name="category">
        /// The category. 
        /// </param>
        /// <param name="message">
        /// The message. 
        /// </param>
        public static void WriteToLog(LogSeverity logSeverity, int threadID, string category, string message)
        {
            var loggerName = (LoggerName)Enum.ToObject(typeof(LoggerName), threadID);

            WriteToLog(logSeverity, loggerName, category, message);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Check file logger.
        /// </summary>
        /// <param name="fileLogger">
        /// The file logger. 
        /// </param>
        /// <returns>
        /// The check file logger. 
        /// </returns>
        private static bool CheckFileLogger(FileLogger fileLogger)
        {
            return fileLogger.FileName != string.Empty;
        }

        /// <summary>
        /// The log file.
        /// </summary>
        /// <param name="severity">
        /// The severity. 
        /// </param>
        /// <param name="fileLogger">
        /// The file logger. 
        /// </param>
        /// <param name="category">
        /// The category. 
        /// </param>
        /// <param name="message">
        /// The message. 
        /// </param>
        private static void LogFile(LogSeverity severity, FileLogger fileLogger, string category, string message)
        {
            if (CheckFileLogger(fileLogger))
            {
                fileLogger.Log(severity, category, message);
            }
        }

        /// <summary>
        /// The log internal.
        /// </summary>
        /// <param name="severity">
        /// The severity. 
        /// </param>
        /// <param name="internalLogger">
        /// The internal logger. 
        /// </param>
        /// <param name="category">
        /// The category. 
        /// </param>
        /// <param name="message">
        /// The message. 
        /// </param>
        private static void LogInternal(
            LogSeverity severity, ThreadedBindingList<LogModel> internalLogger, string category, string message)
        {
            internalLogger.Add(LogModel.Create(severity, category, message));
        }

        #endregion
    }
}