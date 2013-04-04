// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="VersionUpdateFactory.cs">
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
//   The version update factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.Versioning
{
    #region Required Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YANFOE.InternalApps.DownloadManager;
    using YANFOE.InternalApps.DownloadManager.Model;
    using YANFOE.Properties;
    using YANFOE.Settings.ConstSettings;
    using YANFOE.Tools.Enums;
    using YANFOE.Tools.Extentions;
    using YANFOE.UI.UserControls.CommonControls;

    #endregion

    /// <summary>
    /// The version update factory.
    /// </summary>
    public static class VersionUpdateFactory
    {
        #region Static Fields

        /// <summary>
        /// The update link.
        /// </summary>
        public static string UpdateLink = string.Empty;

        /// <summary>
        /// The update tip.
        /// </summary>
        public static SuperToolTip UpdateTip = new SuperToolTip();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="VersionUpdateFactory"/> class.
        /// </summary>
        static VersionUpdateFactory()
        {
            ImageStatus = Resources.accept32;
            UpdateTip.Items.AddTitle("YANFOE has not checked for an update.");
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   Occurs when [version update changed].
        /// </summary>
        [field: NonSerialized]
        public static event EventHandler VersionUpdateChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the image status.
        /// </summary>
        public static Image ImageStatus { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The check for update.
        /// </summary>
        public static void CheckForUpdate()
        {
            InvokeVersionUpdateChanged(new EventArgs());

            var bgwUpdate = new BackgroundWorker();
            bgwUpdate.DoWork += BgwUpdateDoWork;
            bgwUpdate.RunWorkerCompleted += BgwUpdateRunWorkerCompleted;
            bgwUpdate.RunWorkerAsync();
        }

        /// <summary>
        /// Invokes the version update changed.
        /// </summary>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        public static void InvokeVersionUpdateChanged(EventArgs e)
        {
            EventHandler handler = VersionUpdateChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// The update available.
        /// </summary>
        /// <param name="foundReleases">
        /// The found releases.
        /// </param>
        public static void UpdateAvailable(FoundReleases foundReleases)
        {
            ImageStatus = Resources.new24;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("New Update Available!");
            UpdateTip.Items.Add("Double click to download the latest release build.");
            UpdateLink = string.Format(
                "https://github.com/yanfoe/YANFOE.v2/downloads/YANFOE.v{0}.{1}-{2}.Build.{3}.zip", 
                foundReleases.Major, 
                foundReleases.Minor, 
                foundReleases.Milestone, 
                foundReleases.BuildNumber);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The check for update failed.
        /// </summary>
        private static void CheckForUpdateFailed()
        {
            ImageStatus = Resources.delete32;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("Update Search Failed.");
            UpdateTip.Items.Add("Double click to manually check for update");
            UpdateLink = "https://github.com/yanfoe/YANFOE.v2/downloads";
        }

        /// <summary>
        /// The no new update.
        /// </summary>
        /// <param name="foundReleases">
        /// The found releases.
        /// </param>
        private static void NoNewUpdate(FoundReleases foundReleases)
        {
            ImageStatus = Resources.accept32;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle("You are using the latest build of YANFOE.");
            UpdateTip.Items.Add("Current build: " + foundReleases.BuildNumber);
            UpdateLink = string.Empty;
        }

        /// <summary>
        /// The using unreleased version.
        /// </summary>
        /// <param name="foundReleases">
        /// The found releases.
        /// </param>
        private static void UsingUnreleasedVersion(FoundReleases foundReleases)
        {
            ImageStatus = Resources.warning24;
            InvokeVersionUpdateChanged(new EventArgs());
            UpdateTip = new SuperToolTip();
            UpdateTip.Items.AddTitle(
                string.Format("You are using an unofficial build of YANFOE ({0}).", Application.ApplicationBuild));
            UpdateTip.Items.Add(string.Format("Double click to download build {0}", foundReleases.BuildNumber));
            UpdateLink = string.Format(
                "https://github.com/yanfoe/YANFOE.v2/downloads/YANFOE.v{0}.{1}-{2}.Build.{3}.zip", 
                foundReleases.Major, 
                foundReleases.Minor, 
                foundReleases.Milestone, 
                foundReleases.BuildNumber);
        }

        /// <summary>
        /// The update_ do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void BgwUpdateDoWork(object sender, DoWorkEventArgs e)
        {
            var downloads = Downloader.ProcessDownload(
                "https://github.com/yanfoe/YANFOE.v2/downloads", DownloadType.Html, Section.Movies, true);

            var matches = Regex.Matches(
                downloads, 
                @"YANFOE\.v(?<major>\d)\.(?<minor>\d{1,2})-(?<milestone>.*?)\.Build\.(?<buildnumber>\d+?)\.zip");

            var releases = new List<FoundReleases>();

            foreach (Match match in matches)
            {
                var buildNumber = match.Groups["buildnumber"].Value;

                var check = releases.SingleOrDefault(c => c.BuildNumber == buildNumber);

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

        /// <summary>
        /// The update_ run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void BgwUpdateRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var result = e.Result as FoundReleases;

            if (result == null)
            {
                CheckForUpdateFailed();
            }
            else if (Application.ApplicationBuild == result.BuildNumber)
            {
                NoNewUpdate(result);
            }
            else if (Application.ApplicationBuild.ToInt() > result.BuildNumber.ToInt())
            {
                UsingUnreleasedVersion(result);
            }
            else
            {
                UpdateAvailable(result);
            }
        }

        #endregion

        /// <summary>
        /// The found releases.
        /// </summary>
        public class FoundReleases
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the build number.
            /// </summary>
            public string BuildNumber { get; set; }

            /// <summary>
            /// Gets or sets the major.
            /// </summary>
            public int Major { get; set; }

            /// <summary>
            /// Gets or sets the milestone.
            /// </summary>
            public string Milestone { get; set; }

            /// <summary>
            /// Gets or sets the minor.
            /// </summary>
            public int Minor { get; set; }

            #endregion
        }
    }
}