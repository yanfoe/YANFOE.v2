// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="Windows7UIFactory.cs">
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
//   The windows 7 ui factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.Factories.UI
{
    #region Required Namespaces

    using System;

    using Microsoft.WindowsAPICodePack.Taskbar;

    #endregion

    /// <summary>
    /// The windows 7 UI factory.
    /// </summary>
    public static class Windows7UIFactory
    {
        #region Static Fields

        /// <summary>
        /// The good OS version.
        /// </summary>
        private static readonly bool GoodOsVersion;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="Windows7UIFactory"/> class.
        /// </summary>
        static Windows7UIFactory()
        {
            OperatingSystem os = Environment.OSVersion;

            if (os.Version.Build > 7600)
            {
                GoodOsVersion = false;

                GoodOsVersion = true;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The progress changed.
        /// </summary>
        public static event EventHandler ProgressChanged = delegate { };

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the progress max.
        /// </summary>
        public static int ProgressMax { get; set; }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        public static int ProgressValue { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The error progress state.
        /// </summary>
        public static void ErrorProgressState()
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
            InvokeProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The invoke progress changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        public static void InvokeProgressChanged(EventArgs e)
        {
            EventHandler handler = ProgressChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        /// <summary>
        /// The pause progress state.
        /// </summary>
        public static void PauseProgressState()
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            InvokeProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The resume progress state.
        /// </summary>
        public static void ResumeProgressState()
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            InvokeProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The set progress state.
        /// </summary>
        /// <param name="state">
        /// The state.
        /// </param>
        public static void SetProgressState(TaskbarProgressBarState state)
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(state);
            InvokeProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The set progress value.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public static void SetProgressValue(int value)
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressValue(value, ProgressMax);
            ProgressValue = value;
            InvokeProgressChanged(new EventArgs());
        }

        /// <summary>
        /// The start progress state.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public static void StartProgressState(int max)
        {
            if (!GoodOsVersion)
            {
                return;
            }

            ProgressMax = max;
            ResumeProgressState();
        }

        /// <summary>
        /// The stop progress state.
        /// </summary>
        public static void StopProgressState()
        {
            if (!GoodOsVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            ProgressMax = 0;
            InvokeProgressChanged(new EventArgs());
        }

        #endregion
    }
}