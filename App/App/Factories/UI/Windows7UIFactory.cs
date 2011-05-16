namespace YANFOE.Factories.UI
{
    using System;

    using Microsoft.WindowsAPICodePack.Taskbar;

    public static class Windows7UIFactory
    {
        private static int progressMax { get; set; }

        static Windows7UIFactory()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }

        public static void SetProgressState(TaskbarProgressBarState state)
        {
            TaskbarManager.Instance.SetProgressState(state);
        }

        public static void SetProgressValue(int value)
        {
            TaskbarManager.Instance.SetProgressValue(value, progressMax); 
        }

        public static void StartProgressState(int max)
        {
            progressMax = max;
            ResumeProgressState();
        }

        public static void StopProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            progressMax = 0;
        }

        public static void ErrorProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
        }

        public static void PauseProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
        }

        public static void ResumeProgressState()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
        }
    }
}
