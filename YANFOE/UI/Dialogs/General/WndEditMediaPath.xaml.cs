using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using YANFOE.Factories.Internal;
using YANFOE.Factories.Media;
using YANFOE.Models.GeneralModels.AssociatedFiles;
using YANFOE.Tools.IO;

namespace YANFOE.UI.Dialogs.General
{
    /// <summary>
    /// Interaction logic for WndEditMediaPath.xaml
    /// </summary>
    public partial class WndEditMediaPath : Window, INotifyPropertyChanged
    {
        public bool Confirmed;
        private MediaPathModel editingMediaPathModel = new MediaPathModel();

        public MediaPathModel EditingMediaPathModel
        {
            get { return editingMediaPathModel; } 
            private set { editingMediaPathModel = value; 
                    OnPropertyChanged("EditingMediaPathModel"); }
        }

        private MediaPathActionType actionType;

        public enum MediaPathActionType
        {
            Add,
            Edit
        }

        public WndEditMediaPath(MediaPathActionType type, MediaPathModel mediaPathModel)
        {
            InitializeComponent();
            
            this.actionType = type;
            this.EditingMediaPathModel = mediaPathModel;
        }


        /// <summary>
        /// BTNs the cancel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// BTNs the media path browse click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnMediaPathBrowseClick(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            dialog.SelectedPath = txtMediaPath.Text;

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.EditingMediaPathModel.MediaPath = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// BTNs the ok click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            if (chkFolderContainsMovies.IsChecked == false && chkFolderContainsTvShows.IsChecked == false)
            {
                MessageBox.Show("Please select an import type");
                return;
            }

            switch (actionType)
            {
                case MediaPathActionType.Add:
                    // This can probably be prettier
                    foreach (var model in MediaPathDBFactory.Instance.MediaPathDB)
                    {
                        if (model.MediaPath == this.EditingMediaPathModel.MediaPath)
                        {
                            MessageBox.Show("You can't add the same source twice!");
                            return;
                        }
                    }
                    MediaPathDBFactory.Instance.MediaPathDB.Add(this.EditingMediaPathModel);
                    break;
            }

            MediaPathDBFactory.Instance.CurrentMediaPath = this.EditingMediaPathModel;

            this.Close();

            DatabaseIOFactory.Save(DatabaseIOFactory.OutputName.MediaPathDb);

            ImportFiles.ScanMediaPath(MediaPathDBFactory.Instance.CurrentMediaPath);

        }
        
        /// <summary>
        /// Handles the CheckedChanged event of the chkFolderContainsMovies control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkFolderContainsMovies_CheckedChanged(object sender, RoutedEventArgs e)
        {
            groupMovieNaming.IsEnabled = chkFolderContainsMovies.IsChecked ?? false;
            groupMovieDefaults.IsEnabled = chkFolderContainsMovies.IsChecked ?? false;
        }
        
        /// <summary>
        /// Handles the Click event of the chkFolderContainsTvShows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void chkFolderContainsTvShows_CheckedChanged(object sender, RoutedEventArgs e)
        {
            chkImportUsingFileName.IsChecked = false;
            chkImportUsingParentFolderName.IsChecked = false;

            cmbSource.Text = string.Empty;
            cmbScraperGroup.Text = string.Empty;
        }

        /// <summary>
        /// Handles the TextChanged event of the txtMediaPath control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void txtMediaPath_TextChanged(object sender, TextChangedEventArgs e)
        {

            btnOK.IsEnabled = Directory.Exists(txtMediaPath.Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
