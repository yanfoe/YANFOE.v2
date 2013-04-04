using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YANFOE.InternalApps.DownloadManager;

namespace YANFOE.UI.UserControls.DownloadControls
{
    /// <summary>
    /// Interaction logic for DownloadsUserControl.xaml
    /// </summary>
    public partial class DownloadsUserControl : UserControl
    {
        public DownloadsUserControl()
        {
            InitializeComponent();


            ucDownloadThread1.UpdateProgress("Thread 1", Downloader.Progress[0]);
            ucDownloadThread2.UpdateProgress("Thread 2", Downloader.Progress[1]);
            ucDownloadThread3.UpdateProgress("Thread 3", Downloader.Progress[2]);
            ucDownloadThread4.UpdateProgress("Thread 4", Downloader.Progress[3]);
            ucDownloadThread5.UpdateProgress("Thread 5", Downloader.Progress[4]);
            ucDownloadThread6.UpdateProgress("Thread 6", Downloader.Progress[5]);
            ucDownloadThread7.UpdateProgress("Thread 7", Downloader.Progress[6]);
            ucDownloadThread8.UpdateProgress("Thread 8", Downloader.Progress[7]);

        }
    }
}
