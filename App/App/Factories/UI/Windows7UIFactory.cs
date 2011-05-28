namespace YANFOE.Factories.UI
{
    using System;

    using Microsoft.WindowsAPICodePack.Taskbar;

    public static class Windows7UIFactory
    {
        private static bool goodOSVersion = false;

        public static event EventHandler ProgressChanged = delegate { };

        public static void InvokeProgressChanged(EventArgs e)
        {
            EventHandler handler = ProgressChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        public static int ProgressMax { get; set; }

        public static int ProgressValue { get; set; }

        static Windows7UIFactory()
        {
            OperatingSystem os = Environment.OSVersion;

            if (os.Version.Build > 7600)
            {
                goodOSVersion = false;

                goodOSVersion = true;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            }
        }

        public static void SetProgressState(TaskbarProgressBarState state)
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(state);
            InvokeProgressChanged(new EventArgs());
        }

        public static void SetProgressValue(int value)
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressValue(value, ProgressMax);
            ProgressValue = value;
            InvokeProgressChanged(new EventArgs());
        }

        public static void StartProgressState(int max)
        {
            if (!goodOSVersion)
            {
                return;
            }

            ProgressMax = max;
            ResumeProgressState();
        }

        public static void StopProgressState()
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            ProgressMax = 0;
            InvokeProgressChanged(new EventArgs());
        }

        public static void ErrorProgressState()
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
            InvokeProgressChanged(new EventArgs());
        }

        public static void PauseProgressState()
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            InvokeProgressChanged(new EventArgs());
        }

        public static void ResumeProgressState()
        {
            if (!goodOSVersion)
            {
                return;
            }

            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            InvokeProgressChanged(new EventArgs());
        }
    }
}
