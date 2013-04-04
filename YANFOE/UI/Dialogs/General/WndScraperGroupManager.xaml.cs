using System;
using System.Collections.Generic;
using System.IO;
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
using YANFOE.Factories.Scraper;
using YANFOE.Scrapers.Movie.Models.ScraperGroup;
using YANFOE.Settings;
using YANFOE.UI.Dialogs.General;

namespace YANFOE.UI.Dialogs.Movies
{
    /// <summary>
    /// Interaction logic for WndScraperGroupManager.xaml
    /// </summary>
    public partial class WndScraperGroupManager : Window
    {

        public WndScraperGroupManager()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var enterAValue = new WndEnterAValue("Enter a scraper name");
            enterAValue.ShowDialog();

            if (!enterAValue.Cancelled)
            {
                var scraperGroup = new MovieScraperGroupModel { ScraperName = enterAValue.Response };

                MovieScraperGroupFactory.Instance.SerializeToXml(scraperGroup);

                this.cmbScraperGroupList.Text = enterAValue.Response;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (cmbScraperGroupList.SelectedItem != null && ((MovieScraperGroupModel)cmbScraperGroupList.SelectedItem).IsDirty)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Do you wish to save the current scraper first?",
                    "Are you sure?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MovieScraperGroupFactory.Instance.SerializeToXml((MovieScraperGroupModel)cmbScraperGroupList.SelectedItem);
                }
            }

            this.Close();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (cmbScraperGroupList.SelectedItem == null) return;

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you wish to delete this scraper group?",
                "Are you sure?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                MovieScraperGroupFactory.Instance.DeleteScraperGroup((MovieScraperGroupModel)cmbScraperGroupList.SelectedItem);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cmbScraperGroupList.SelectedItem == null) return;

            MovieScraperGroupFactory.Instance.SerializeToXml((MovieScraperGroupModel)cmbScraperGroupList.SelectedItem);
        }

        private void btnClearScraperGroup_Click(object sender, RoutedEventArgs e)
        {
            if (cmbScraperGroupList.SelectedItem == null) return;

            ((MovieScraperGroupModel)cmbScraperGroupList.SelectedItem).ClearScrapers();
        }

        private void btnAutopopulate_Click(object sender, RoutedEventArgs e)
        {
            // Column 1
            cmbTitle.Text = cmbAutopopulate.Text;
            cmbYear.Text = cmbAutopopulate.Text;
            cmbOriginalTitle.Text = cmbAutopopulate.Text;
            cmbRating.Text = cmbAutopopulate.Text;
            cmbTagline.Text = cmbAutopopulate.Text;
            cmbPlot.Text = cmbAutopopulate.Text;
            cmbOutline.Text = cmbAutopopulate.Text;
            cmbCertification.Text = cmbAutopopulate.Text;
            cmbWriters.Text = cmbAutopopulate.Text;
            cmbCountry.Text = cmbAutopopulate.Text;
            cmbMpaa.Text = cmbAutopopulate.Text;
            cmbTrailer.Text = cmbAutopopulate.Text;
            
            // Column 2
            cmbGenre.Text = cmbAutopopulate.Text;
            cmbLanguage.Text = cmbAutopopulate.Text;
            cmbRuntime.Text = cmbAutopopulate.Text;
            cmbTop250.Text = cmbAutopopulate.Text;
            cmbReleaseDate.Text = cmbAutopopulate.Text;
            cmbVotes.Text = cmbAutopopulate.Text;
            cmbCast.Text = cmbAutopopulate.Text;
            cmbDirector.Text = cmbAutopopulate.Text;
            cmbStudio.Text = cmbAutopopulate.Text;
            cmbFanart.Text = cmbAutopopulate.Text;
            cmbPoster.Text = cmbAutopopulate.Text;
        }
    }
}
