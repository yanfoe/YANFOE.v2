// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Downloader.cs" company="The YANFOE Project">
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

namespace YANFOE.InternalApps.DownloadManager
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Windows.Forms;

    using BitFactory.Logging;

    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Download;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;

    using Timer = System.Windows.Forms.Timer;

    /// <summary>
    /// The manager.
    /// </summary>
    public static class Downloader
    {
        #region Constants and Fields

        /// <summary>
        /// The background workers.
        /// </summary>
        private static readonly BindingList<BackgroundWorker> BackgroundWorkers;

        /// <summary>
        /// The thread count.
        /// </summary>
        private static readonly int numThreads;

        /// <summary>
        /// The current queue count.
        /// </summary>
        private static Object currentQueueLock;

        /// <summary>
        /// The current queue count.
        /// </summary>
        private static int currentQueue;

        /// <summary>
        /// The ui timer.
        /// </summary>
        private static readonly Timer uiTimer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Downloader"/> class. 
        /// </summary>
        static Downloader()
        {
            currentQueueLock = new Object();
            CurrentQue = 0;
            numThreads = 8;
            Progress = new BindingList<Progress>();
            BackgroundWorkers = new BindingList<BackgroundWorker>();

            for (int i = 0; i < numThreads; i++)
            {
                Progress.Add(new Progress());
                BackgroundWorkers.Add(new BackgroundWorker());
                BackgroundWorkers[i].DoWork += BackgroundWorkerDoWork;
                BackgroundWorkers[i].RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            }

            uiTimer = new Timer { Interval = 100 };
            uiTimer.Tick += UITimer_Tick;
            uiTimer.Start();

            BackgroundDownloadQue = new BindingList<DownloadItem>();

            Downloading = new BindingList<string>();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets BackgroundDownloadQue.
        /// </summary>
        public static BindingList<DownloadItem> BackgroundDownloadQue { get; set; }

        /// <summary>
        /// Gets or sets the current que.
        /// </summary>
        /// <value>
        /// The current que.
        /// </value>
        public static int CurrentQue
        {
            get
            {
                lock (currentQueueLock)
                {
                    return currentQueue;
                }
            }
            set
            {
                lock (currentQueueLock)
                {
                    currentQueue = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets Downloading.
        /// </summary>
        public static BindingList<string> Downloading { get; set; }

        /// <summary>
        /// Gets or sets Progress indicators.
        /// </summary>
        public static BindingList<Progress> Progress { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add downloaditem to background que
        /// </summary>
        /// <param name="downloadItem">
        /// The download item.
        /// </param>
        public static void AddToBackgroundQue(DownloadItem downloadItem)
        {
            if (Get.Web.EnableAddToBackgroundQue == false)
            {
                return;
            }

            string path = WebCache.GetPathFromUrl(downloadItem.Url, downloadItem.Section);

            if (!File.Exists(path))
            {
                lock (BackgroundDownloadQue)
                {
                    BackgroundDownloadQue.Add(downloadItem);
                }
            }
        }

        /// <summary>
        /// Check if url exists in the background que
        /// </summary>
        /// <param name="url">
        /// The url to check
        /// </param>
        /// <returns>
        /// The in background que.
        /// </returns>
        public static bool InBackgroundQue(string url)
        {
            var count = 0;

            lock (BackgroundDownloadQue)
            {
                for (int index = 0; index < BackgroundDownloadQue.Count; index++)
                {
                    var item = BackgroundDownloadQue[index];
                    if (item != null && item.Url == url)
                    {
                        count++;
                    }
                }
            }

            return count > 0;
        }

        /// <summary>
        /// Downloads the specified URL.
        /// </summary>
        /// <param name="url">
        /// The download URL.
        /// </param>
        /// <param name="type">
        /// The download type.
        /// </param>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <param name="skipCache">
        /// if set to <c>true</c> [skip cache].
        /// </param>
        /// <param name="cookieContainer">
        /// The cookie container.
        /// </param>
        /// <returns>
        /// The process download.
        /// </returns>
        public static string ProcessDownload(
            string url,
            DownloadType type,
            Section section,
            bool skipCache = false,
            CookieContainer cookieContainer = null)
        {
            bool found = false;

            if (url == null)
            {
                return string.Empty;
            }

            var item = new DownloadItem
            {
                Type = type,
                Url = url,
                Section = section,
                IgnoreCache = skipCache,
                CookieContainer = cookieContainer
            };

            try
            {
                lock (currentQueueLock)
                {
                    currentQueue++;
                }

                Downloading.Add(url);

                if (InBackgroundQue(url))
                {
                    RemoveFromBackgroundQue(url);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, string.Format("Thread Download Setup {0}", url), ex.Message);
            }

            do
            {
                for (int i = 0; i < numThreads; i++)
                {
                    if (!BackgroundWorkers[i].IsBusy)
                    {
                        try
                        {
                            item.ThreadID = i+1;
                            item.Progress = Progress[i];
                            BackgroundWorkers[i].RunWorkerAsync(item);
                            found = true;

                            do
                            {
                                Application.DoEvents();
                                Thread.Sleep(50);
                            }
                            while (BackgroundWorkers[i].IsBusy);

                            if (Downloading.Contains(url))
                            {
                                Downloading.Remove(url);
                            }

                            lock (currentQueueLock)
                            {
                                currentQueue--;
                            }

                            return item.Result.Result;
                        }
                        catch (Exception ex)
                        {
                            Log.WriteToLog(LogSeverity.Error, 0, string.Format("Thread {0} Downloading {1}", i, url), ex.Message);
                        }
                    }

                }

                Application.DoEvents();
                Thread.Sleep(50);
            }
            while (found == false);

            lock (currentQueueLock)
            {
                currentQueue--;
            }

            return null;
        }

        /// <summary>
        /// The remove from background que.
        /// </summary>
        /// <param name="url">
        /// The url to remove.
        /// </param>
        public static void RemoveFromBackgroundQue(string url)
        {
            lock (BackgroundDownloadQue)
            {
                for (int index = 0; index < BackgroundDownloadQue.Count; index++)
                {
                    var item = BackgroundDownloadQue[index];
                    if (item.Url == url)
                    {
                        BackgroundDownloadQue.Remove(item);
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Backgrounds the worker do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var downloadItem = e.Argument as DownloadItem;

            downloadItem.Progress.Message = "Downloading " + downloadItem.Url;

            ProcessDownload(downloadItem);

            e.Result = downloadItem;
        }

        /// <summary>
        /// Backgrounds the worker run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs"/> instance containing the event data.
        /// </param>
        private static void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var downloadItem = e.Result as DownloadItem;

            downloadItem.Progress.Message = string.Empty;
            downloadItem.Progress.Percent = 0;
        }

        /// <summary>
        /// Handles the DoWork event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.</param>
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                if (CurrentQue < numThreads && Get.Web.EnableBackgroundQueProcessing)
                {
                    lock (BackgroundDownloadQue)
                    {
                        if (BackgroundDownloadQue.Count > 0)
                        {
                            DownloadItem item = BackgroundDownloadQue[0];
                            BackgroundDownloadQue.Remove(item);

                            var bgw = new BackgroundWorker();
                            bgw.DoWork += Bgw_DoWork;
                            bgw.RunWorkerAsync(item);
                        }
                    }
                }

                Thread.Sleep(200);
            }
            while (true);
        }

        /// <summary>
        /// Handles the DoWork event of the bgw control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data.
        /// </param>
        private static void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var item = e.Argument as DownloadItem;
            ProcessDownload(item.Url, item.Type, item.Section, item.IgnoreCache, item.CookieContainer);
        }

        /// <summary>
        /// Processes the download.
        /// </summary>
        /// <param name="downloadItem">
        /// The download item.
        /// </param>
        private static void ProcessDownload(DownloadItem downloadItem)
        {
            if (downloadItem.Type == DownloadType.Binary)
            {
                Binary.Get(downloadItem);
            }
            else if (downloadItem.Type == DownloadType.AppleBinary)
            {
                AppleBinary.Get(downloadItem);
            }
            else
            {
                var html = new Html();
                html.Get(downloadItem);
            }
        }

        /// <summary>
        /// Progresses the manage.
        /// </summary>
        /// <param name="progress">
        /// The progress.
        /// </param>
        /// <param name="threadID">
        /// The thread MovieUniqueId.
        /// </param>
        private static void ProgressManage(Progress progress, int threadID)
        {
            if (string.IsNullOrEmpty(progress.Message))
            {
                progress.Message = threadID + ": Idle.";
                progress.Percent = 0;
            }
        }

        /// <summary>
        /// Handles the Tick event of the UITimer control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private static void UITimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < numThreads; i++)
            {
                ProgressManage(Progress[i], i);
            }
        }

        #endregion

        /// <summary>
        /// The progress status.
        /// </summary>
        public class ProgressStatus
        {
            #region Properties

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="ProgressStatus"/> is busy.
            /// </summary>
            /// <value>
            ///   <c>true</c> if busy; otherwise, <c>false</c>.
            /// </value>
            public bool Busy { get; set; }

            #endregion
        }
    }
}