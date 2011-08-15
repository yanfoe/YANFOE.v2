// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.Logs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.InternalApps.Logs.Model;

    /// <summary>
    /// Handles all logging for Yanfoe
    /// </summary>
    public static class Log
    {
        #region Constants and Fields

        /// <summary>
        /// The log treshold.
        /// </summary>
        public static LogSeverity logTreshold = LogSeverity.Info;

        /// <summary>
        /// The main file log.
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
        /// Collection of loggers by LoggerName
        /// </summary>
        private static readonly IDictionary<LoggerName, FileLogger> fileLoggers;

        /// <summary>
        /// Collection of loggers by Thread MovieUniqueId
        /// </summary>
        private static readonly IDictionary<LoggerName, BindingList<LogModel>> internalLoggers;

        /// <summary>
        /// The log state.
        /// </summary>
        private static readonly Dictionary<LoggerName, LogState> logState;

        /// <summary>
        /// The main internal log.
        /// </summary>
        private static readonly BindingList<LogModel> mainInternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 1 file log.
        /// </summary>
        private static readonly FileLogger thread1FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread1FileLog.txt"));

        /// <summary>
        /// The thread 1 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread1InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 2 file log.
        /// </summary>
        private static readonly FileLogger thread2FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread2FileLog.txt"));

        /// <summary>
        /// The thread 2 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread2InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 3 file log.
        /// </summary>
        private static readonly FileLogger thread3FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread3FileLog.txt"));

        /// <summary>
        /// The thread 3 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread3InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 4 file log.
        /// </summary>
        private static readonly FileLogger thread4FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread4FileLog.txt"));

        /// <summary>
        /// The thread 4 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread4InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 5 file log.
        /// </summary>
        private static readonly FileLogger thread5FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread5FileLog.txt"));

        /// <summary>
        /// The thread 5 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread5InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 6 file log.
        /// </summary>
        private static readonly FileLogger thread6FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread6FileLog.txt"));

        /// <summary>
        /// The thread 6 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread6InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 7 file log.
        /// </summary>
        private static readonly FileLogger thread7FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread7FileLog.txt"));

        /// <summary>
        /// The thread 7 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread7InternalLog = new BindingList<LogModel>();

        /// <summary>
        /// The thread 4 file log.
        /// </summary>
        private static readonly FileLogger thread8FileLog =
            new FileLogger(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}{1}Logs{1}{2}",
                    Directory.GetCurrentDirectory(),
                    Path.DirectorySeparatorChar,
                    "thread8FileLog.txt"));

        /// <summary>
        /// The thread 8 internal log.
        /// </summary>
        private static readonly BindingList<LogModel> thread8InternalLog = new BindingList<LogModel>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Log"/> class.
        /// </summary>
        static Log()
        {
            logState = new Dictionary<LoggerName, LogState>
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

            fileLoggers = new Dictionary<LoggerName, FileLogger>
                {
                    { LoggerName.GeneralLog, MainFileLog },
                    { LoggerName.Downloader1, thread1FileLog },
                    { LoggerName.Downloader2, thread2FileLog },
                    { LoggerName.Downloader3, thread3FileLog },
                    { LoggerName.Downloader4, thread4FileLog },
                    { LoggerName.Downloader5, thread5FileLog },
                    { LoggerName.Downloader6, thread6FileLog },
                    { LoggerName.Downloader7, thread7FileLog },
                    { LoggerName.Downloader8, thread8FileLog }
                };

            internalLoggers = new Dictionary<LoggerName, BindingList<LogModel>>
                {
                    { LoggerName.GeneralLog, mainInternalLog },
                    { LoggerName.Downloader1, thread1InternalLog },
                    { LoggerName.Downloader2, thread2InternalLog },
                    { LoggerName.Downloader3, thread3InternalLog },
                    { LoggerName.Downloader4, thread4InternalLog },
                    { LoggerName.Downloader5, thread5InternalLog },
                    { LoggerName.Downloader6, thread6InternalLog },
                    { LoggerName.Downloader7, thread7InternalLog },
                    { LoggerName.Downloader8, thread8InternalLog }
                };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clear internal logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public static void ClearInternalLogger(LoggerName loggerName)
        {
            internalLoggers[loggerName].Clear();
        }

        /// <summary>
        /// Disable logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public static void DisableLogger(LoggerName loggerName)
        {
            logState[loggerName] = LogState.Stopped;
        }

        /// <summary>
        /// Enable logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public static void EnableLogger(LoggerName loggerName)
        {
            logState[loggerName] = LogState.Started;
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
        public static BindingList<LogModel> GetInternalLogger(LoggerName loggerName)
        {
            return internalLoggers[loggerName];
        }

        /// <summary>
        /// The logger status.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        /// <returns>
        /// Return logstate
        /// </returns>
        public static LogState LoggerStatus(LoggerName loggerName)
        {
            return logState[loggerName];
        }

        /// <summary>
        /// Pause logger.
        /// </summary>
        /// <param name="loggerName">
        /// The logger name.
        /// </param>
        public static void PauseLogger(LoggerName loggerName)
        {
            logState[loggerName] = LogState.Paused;
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
            fileLoggers[loggerName].FileName = path;
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
        /// <param name="catagory">
        /// The catagory.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void WriteToLog(LogSeverity logSeverity, LoggerName loggerName, string catagory, string message)
        {
            if (!Settings.Get.LogSettings.EnableLog)
            {
                return;
            }

            if (logState[loggerName] == LogState.Stopped)
            {
                return;
            }

            FileLogger fileLogger = fileLoggers[loggerName];

            BindingList<LogModel> internalLogger = internalLoggers[loggerName];

            if (logSeverity >= Log.logTreshold)
            {
                LogFile(logSeverity, fileLogger, catagory, message);
                LogInternal(logSeverity, internalLogger, catagory, message);
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
        /// <param name="catagory">
        /// The catagory.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void WriteToLog(LogSeverity logSeverity, int threadID, string catagory, string message)
        {
            var loggerName = (LoggerName)Enum.ToObject(typeof(LoggerName), threadID);

            WriteToLog(logSeverity, loggerName, catagory, message);
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
        /// <param name="catagory">
        /// The catagory.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void LogFile(LogSeverity severity, FileLogger fileLogger, string catagory, string message)
        {
            if (CheckFileLogger(fileLogger))
            {
                fileLogger.Log(severity, catagory, message);
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
        /// <param name="catagory">
        /// The catagory.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        private static void LogInternal(
            LogSeverity severity, BindingList<LogModel> internalLogger, string catagory, string message)
        {
            internalLogger.Add(LogModel.Create(severity, catagory, message));
        }

        #endregion
    }
}