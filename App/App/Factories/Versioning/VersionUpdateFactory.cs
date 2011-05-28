// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionUpdateFactory.cs" company="The YANFOE Project">
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

namespace YANFOE.Factories.Versioning
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;

    using DevExpress.Utils;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Properties;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;

    public static class VersionUpdateFactory
    {
        private static Image imageStatus;

        public static SuperToolTip UpdateTip = new SuperToolTip();

        public static string UpdateLink = string.Empty;

        public static Image ImageStatus
        {
            get
            {
                return imageStatus;
            }

            set
            {
                imageStatus = value;
            }
        }

        static VersionUpdateFactory()
        {
            ImageStatus = Resources.accept24;
            UpdateTip.Items.AddTitle("YANFOE has not checked for an update.");
        }

        /// <summary>
        /// Occurs when [version update changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler VersionUpdateChanged = delegate { };

        /// <summary>
        /// Invokes the version update changed.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public static void InvokeVersionUpdateChanged(EventArgs e)
        {
            EventHandler handler = VersionUpdateChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        public static void CheckForUpdate()
        {
            InvokeVersionUpdateChanged(new EventArgs());

            var bgwUpdate = new BackgroundWorker();
            bgwUpdate.DoWork += bgwUpdate_DoWork;
            bgwUpdate.RunWorkerCompleted += bgwUpdate_RunWorkerCompleted;
            bgwUpdate.RunWorkerAsync();
        }

        private static void bgwUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            var downloads = Downloader.ProcessDownload(
                "https://github.com/yanfoe/YANFOE.v2/downloads", DownloadType.Html, Section.Movies, true);

            var matches = Regex.Matches(
                downloads,
                @"YANFOE\.v(?<major>\d)\.(?<minor>\d{1,2})-(?<milestone>.*?)\.Build\.(?<buildnumber>\d+?)\.rar");

            var releases = new List<FoundReleases>();

            foreach (Match match in matches)
            {
                var buildNumber = match.Groups["buildnumber"].Value;

                var check = releases.Where(c => c.BuildNumber == buildNumber).SingleOrDefault();

                if (check == null)
                {
                    releases.Add(
                        new FoundReleases
                            {
                                Major = match.Groups["major"].Value.ToInt(),
                                Minor = match.Groups["minor"].Value.ToInt(),
                                BuildNumber = match.Groups["buildnumber"].Value,
                                Milestone = match.Groups["milestone"].Value
                            });
                }
            }

            var getSortedList = (from r in releases orderby r.BuildNumber descending select r).ToList();

            if (getSortedList.Count > 0)
            {
                e.Result = getSortedList[0];
            }
        }

        private static void bgwUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as FoundReleases;

            if (result == null)
            {
                CheckForUpdateFailed(result);
            }

            if (YANFOE.Settings.ConstSettings.Application.ApplicationBuild == result.BuildNumber)
            {
                NoNewUpdate(result);
            }
            else if (YANFOE.Settings.ConstSettings.Application.ApplicationBuild.ToInt() > result.BuildNumber.ToInt())
            {
                UsingUnreleasedVersion(result);
            }
            else
            {
                UpdateAvailable(result);
            }
        }

        private static void CheckForUpdateFailed(FoundReleases foundReleases)
        {
            ImageStatus = Resources.delete32;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("Update Search Failed.");
            UpdateTip.Items.Add("Double click to manually check for update");
            UpdateLink = "https://github.com/yanfoe/YANFOE.v2/downloads";
        }

        private static void NoNewUpdate(FoundReleases foundReleases)
        {
            ImageStatus = Resources.accept24;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("You are using the latest build of YANFOE.");
            UpdateTip.Items.Add("Current build: " + foundReleases.BuildNumber);
            UpdateLink = string.Empty;
        }

        private static void UsingUnreleasedVersion(FoundReleases foundReleases)
        {
            ImageStatus = Resources.warning24;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle(string.Format("You are using an unofficial build of YANFOE ({0}).", YANFOE.Settings.ConstSettings.Application.ApplicationBuild));
            UpdateTip.Items.Add(string.Format("Double click to download build {0}", foundReleases.BuildNumber));
            UpdateLink = string.Format("https://github.com/yanfoe/YANFOE.v2/downloads/YANFOE.v{0}.{1}-{2}.Build.{3}.rar", foundReleases.Major, foundReleases.Minor, foundReleases.Milestone, foundReleases.BuildNumber);
        }

        public static void UpdateAvailable(FoundReleases foundReleases)
        {
            ImageStatus = Resources.new24;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("New Update Available!");
            UpdateTip.Items.Add("Double click to download the latest release build.");
            UpdateLink = string.Format("https://github.com/yanfoe/YANFOE.v2/downloads/YANFOE.v{0}.{1}-{2}.Build.{3}.rar", foundReleases.Major, foundReleases.Minor, foundReleases.Milestone, foundReleases.BuildNumber);
        }

        public class FoundReleases
        {
            public int Major { get; set; }

            public int Minor { get; set; }

            public string Milestone { get; set; }

            public string BuildNumber { get; set; }
        }
    }
}
