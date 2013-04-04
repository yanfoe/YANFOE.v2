using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using YANFOE.Models.TvModels.TVDB;
using YANFOE.Tools.UI;

namespace YANFOE.UI.Dialogs.TV
{

    /// <summary>
    /// Interaction logic for WndSelectSeries.xaml
    /// </summary>
    public partial class WndSelectSeries : Window, INotifyPropertyChanged
    {
        #region Constants and Fields

        /// <summary>
        /// The search details.
        /// </summary>
        private ThreadedBindingList<SearchDetails> searchDetails;
        public ThreadedBindingList<SearchDetails> SearchDetails
        {
            get { return searchDetails; }
            set { searchDetails = value; OnPropertyChanged("SearchDetails"); }
        }

        /// <summary>
        /// The search term.
        /// </summary>
        private string searchTerm;

        #endregion

        public WndSelectSeries(ThreadedBindingList<SearchDetails> searchDetails, string searchTerm)
        {
            InitializeComponent();

            // TODO: Complete member initialization
            this.SearchDetails = searchDetails;
            this.searchTerm = searchTerm;
            SelectedSeries = searchDetails[0];
        }

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether Cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets or sets SelectedSeries.
        /// </summary>
        public SearchDetails SelectedSeries { get; set; }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
