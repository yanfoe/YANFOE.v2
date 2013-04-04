using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YANFOE.UI.Dialogs.General
{
    /// <summary>
    /// Interaction logic for WndWelcomePage.xaml
    /// </summary>
    public partial class WndWelcomePage : Window
    {
        public WndWelcomePage()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Settings.Get.Ui.ShowWelcomeMessage = !chkNeverShowAgain.IsChecked ?? false;
            this.Close();
        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Settings.ConstSettings.Application.DonateUrl);
        }
    }
}
