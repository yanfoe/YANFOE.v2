// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Downloader.cs">
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
//   The manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.InternalApps.DownloadManager
{
    #region Required Namespaces

    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using BitFactory.Logging;

    using YANFOE.Factories.UI;
    using YANFOE.InternalApps.DownloadManager.Cache;
    using YANFOE.InternalApps.DownloadManager.Download;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.InternalApps.Logs;
    using YANFOE.Settings;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.UI;

    using Timer = System.Timers.Timer;

    #endregion

    /// <summary>
    ///   The manager.
    /// </summary>
    public static class Downloader
    {
        #region Static Fields

        /// <summary>
        ///   The background workers.
        /// </summary>
        private static readonly ThreadedBindingList<BackgroundWorker> BackgroundWorkers;

        /// <summary>
        ///   The current queue count.
        /// </summary>
        private static readonly object CurrentQueueLock;

        /// <summary>
        ///   The UI timer.
        /// </summary>
        private static readonly Timer UITimer;

        /// <summary>
        ///   The current queue count.
        /// </summary>
        private static int currentQueue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes static members of the <see cref="Downloader" /> class.
        /// </summary>
        static Downloader()
        {
            CurrentQueueLock = new object();
            CurrentQueue = 0;
            Progress = new ThreadedBindingList<Progress>();
            BackgroundWorkers = new ThreadedBindingList<BackgroundWorker>();

            for (int i = 0; i < Get.Web.DownloadThreads; i++)
            {
                Progress.Add(new Progress());
                BackgroundWorkers.Add(new BackgroundWorker());
                BackgroundWorkers[i].DoWork += BackgroundWorkerDoWork;
                BackgroundWorkers[i].RunWorkerCompleted += BackgroundWorkerRunWorkerCompleted;
            }

            UITimer = new Timer { Interval = 100 };
            UITimer.Elapsed += UITimerTick;
            UITimer.Start();

            BackgroundDownloadQue = new ThreadedBindingList<DownloadItem>();

            Downloading = new ThreadedBindingList<string>();

            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets BackgroundDownloadQueue.
        /// </summary>
        public static ThreadedBindingList<DownloadItem> BackgroundDownloadQue { get; private set; }

        /// <summary>
        ///   Gets or sets the current queue.
        /// </summary>
        /// <value> The current queue. </value>
        public static int CurrentQueue
        {
            get
            {
                lock (CurrentQueueLock)
                {
                    return currentQueue;
                }
            }

            set
            {
                lock (CurrentQueueLock)
                {
                    currentQueue = value;
                }
            }
        }

        /// <summary>
        ///   Gets or sets Downloading.
        /// </summary>
        public static ThreadedBindingList<string> Downloading { get; set; }

        /// <summary>
        ///   Gets or sets Progress indicators.
        /// </summary>
        public static ThreadedBindingList<Progress> Progress { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Add download item to background queue
        /// </summary>
        /// <param name="downloadItem">
        /// The download item. 
        /// </param>
        /// <param name="downloadPriority">
        /// The download Priority. 
        /// </param>
        public static void AddToBackgroundQueue(
            DownloadItem downloadItem, DownloadPriority downloadPriority = DownloadPriority.Normal)
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
                    var check =
                        (from d in BackgroundDownloadQue where d.Url == downloadItem.Url select d).SingleOrDefault();

                    if (check == null)
                    {
                        downloadItem.Priority = downloadPriority;
                        BackgroundDownloadQue.Add(downloadItem);
                    }
                }
            }
        }

        /// <summary>
        /// Check if url exists in the background queue
        /// </summary>
        /// <param name="url">
        /// The url to check 
        /// </param>
        /// <returns>
        /// The in background queue. 
        /// </returns>
        public static bool InBackgroundQueue(string url)
        {
            var count = 0;

            lock (BackgroundDownloadQue)
            {
                count += BackgroundDownloadQue.Count(item => item != null && item.Url == url);
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

            if (WebCache.CheckIfDownloadItemExistsInCache(item, true))
            {
                return item.Result.Result;
            }

            try
            {
                lock (CurrentQueueLock)
                {
                    currentQueue++;
                }

                Downloading.Add(url);

                if (InBackgroundQueue(url))
                {
                    RemoveFromBackgroundQueue(url);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToLog(LogSeverity.Error, 0, string.Format("Thread Download Setup {0}", url), ex.Message);
            }

            do
            {
                for (int i = 0; i < Get.Web.DownloadThreads; i++)
                {
                    if (!BackgroundWorkers[i].IsBusy)
                    {
                        try
                        {
                            item.ThreadID = i + 1;
                            item.Progress = Progress[i];

                            BackgroundWorkers[i].RunWorkerAsync(item);
                            found = true;

                            do
                            {
                                Thread.Sleep(50);
                                YANFOEApplication.DoEvents();
                            }
                            while (BackgroundWorkers[i].IsBusy);

                            if (Downloading.Contains(url))
                            {
                                Downloading.Remove(url);
                            }

                            lock (CurrentQueueLock)
                            {
                                currentQueue--;
                            }

                            return item.Result.Result;
                        }
                        catch (Exception ex)
                        {
                            Log.WriteToLog(
                                LogSeverity.Error, 0, string.Format("Thread {0} Downloading {1}", i, url), ex.Message);
                        }
                    }
                }

                Thread.Sleep(50);
            }
            while (found == false);

            lock (CurrentQueueLock)
            {
                currentQueue--;
            }

            return null;
        }

        /// <summary>
        /// The remove from background queue.
        /// </summary>
        /// <param name="url">
        /// The url to remove. 
        /// </param>
        public static void RemoveFromBackgroundQueue(string url)
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

            if (downloadItem != null)
            {
                downloadItem.Progress.Message = "Downloading " + downloadItem.Url;
                downloadItem.Progress.IsBusy = true;
                ProcessDownload(downloadItem);

                e.Result = downloadItem;
            }
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

            if (downloadItem != null)
            {
                downloadItem.Progress.Message = string.Empty;
                downloadItem.Progress.IsBusy = false;
                downloadItem.Progress.Percent = 0;
            }
        }

        /// <summary>
        /// Handles the DoWork event of the BackgroundWorker control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // TODO: Make this background worker properly end when the application is closing.
            do
            {
                if (CurrentQueue < 4 && Get.Web.EnableBackgroundQueProcessing)
                {
                    lock (BackgroundDownloadQue)
                    {
                        if (BackgroundDownloadQue.Count > 0)
                        {
                            var orderedQue =
                                (from q in BackgroundDownloadQue orderby q.Priority descending select q).ToList();

                            DownloadItem item = orderedQue[0];
                            BackgroundDownloadQue.Remove(item);

                            var bgw = new BackgroundWorker();
                            bgw.DoWork += BgwDoWork;
                            bgw.RunWorkerAsync(item);
                        }
                    }
                }

                Thread.Sleep(100);
            }
            while (true);
        }

        /// <summary>
        /// Handles the DoWork event of the control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.ComponentModel.DoWorkEventArgs"/> instance containing the event data. 
        /// </param>
        private static void BgwDoWork(object sender, DoWorkEventArgs e)
        {
            var item = e.Argument as DownloadItem;
            if (item != null)
            {
                ProcessDownload(item.Url, item.Type, item.Section, item.IgnoreCache, item.CookieContainer);
            }
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
                progress.IsBusy = false;
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
        private static void UITimerTick(object sender, EventArgs e)
        {
            for (int i = 0; i < Get.Web.DownloadThreads; i++)
            {
                ProgressManage(Progress[i], i);
            }
        }

        #endregion

        /// <summary>
        ///   The progress status.
        /// </summary>
        public class ProgressStatus
        {
            #region Public Properties

            /// <summary>
            ///   Gets or sets a value indicating whether this <see cref="ProgressStatus" /> is busy.
            /// </summary>
            /// <value> <c>true</c> if busy; otherwise, <c>false</c> . </value>
            public bool Busy { get; set; }

            #endregion
        }
    }
}