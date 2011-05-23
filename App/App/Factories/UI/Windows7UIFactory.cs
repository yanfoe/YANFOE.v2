namespace YANFOE.Factories.UI
{
    using System;

    using Microsoft.WindowsAPICodePack.Taskbar;

    public static class Windows7UIFactory
    {
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
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }

        public static void SetProgressState(TaskbarProgressBarState state)
        {
            TaskbarManager.Instance.SetProgressState(state);
            InvokeProgressChanged(new EventArgs());
        }

        public static void SetProgressValue(int value)
        {
            TaskbarManager.Instance.SetProgressValue(value, ProgressMax);
            ProgressValue = value;
            InvokeProgressChanged(new EventArgs());
        }

        public static void StartProgressState(int max)
        {
            ProgressMax = max;
            ResumeProgressState();
        }

        public static void StopProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            ProgressMax = 0;
            InvokeProgressChanged(new EventArgs());
        }

        public static void ErrorProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
            InvokeProgressChanged(new EventArgs());
        }

        public static void PauseProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            InvokeProgressChanged(new EventArgs());
        }

        public static void ResumeProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            InvokeProgressChanged(new EventArgs());
        }
    }
}
