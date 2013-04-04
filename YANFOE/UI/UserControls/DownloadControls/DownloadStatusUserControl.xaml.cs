using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using YANFOE.InternalApps.DownloadManager.Model;
using Timer = System.Timers.Timer;

namespace YANFOE.UI.UserControls.DownloadControls
{
    /// <summary>
    /// Interaction logic for DownloadStatusUserControl.xaml
    /// </summary>
    public partial class DownloadStatusUserControl : UserControl
    {
        
        public DownloadStatusUserControl()
        {
            InitializeComponent();

            baseGrid.DataContext = null;
        }

        public void UpdateProgress(string groupTitle, Progress prog)
        {
            groupControl.Header = groupTitle;
            baseGrid.DataContext = prog;
        }

        private void btnLog_Click(object sender, System.EventArgs e)
        {
            DownloadItem di = new DownloadItem();
            di.CookieContainer = new CookieContainer();
            di.IgnoreCache = true;
            di.Type = DownloadType.Html;
            di.Url = "http://www.facebook.com";
        }
    }
}
