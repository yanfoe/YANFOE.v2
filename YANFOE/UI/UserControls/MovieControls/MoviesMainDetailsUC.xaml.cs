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

namespace YANFOE.UI.UserControls.MovieControls
{
    /// <summary>
    /// Interaction logic for MoviesMainDetailsUC.xaml
    /// </summary>
    public partial class MoviesMainDetailsUC : UserControl
    {
        public MoviesMainDetailsUC()
        {
            InitializeComponent();
            HideMultiEdit();
        }

        private void ShowMultiEdit()
        {
            GridLength gridLength = new GridLength(200, GridUnitType.Star);
            multiEditRow.Height = gridLength;
            multiEditGrid.Visibility = Visibility.Visible;
        }
        
        private void HideMultiEdit()
        {
            multiEditRow.Height = GridLength.Auto;
            multiEditGrid.Visibility = Visibility.Collapsed;
        }

        private void btnActorTrash_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
