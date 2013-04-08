// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="WndEditMediaPath.xaml.cs">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <license>
//   This software is licensed under a Creative Commons License
//   Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0)
//   http://creativecommons.org/licenses/by-nc-sa/3.0/
//   See this page: http://www.yanfoe.com/license
//   For any reuse or distribution, you must make clear to others the
//   license terms of this work.
// </license>
// <summary>
//   Interaction logic for Edit Media Path Window
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.Dialogs.General
{
    #region Required Namespaces

    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;

    using YANFOE.Factories.Internal;
    using YANFOE.Factories.Media;
    using YANFOE.Models.GeneralModels.AssociatedFiles;
    using YANFOE.Tools.IO;

    using MessageBox = System.Windows.MessageBox;

    #endregion

    /// <summary>
    ///   Interaction logic for Edit Media Path Window
    /// </summary>
    public partial class WndEditMediaPath : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        ///   The action type.
        /// </summary>
        private readonly MediaPathActionType actionType;

        /// <summary>
        ///   The editing media path model.
        /// </summary>
        private MediaPathModel editingMediaPathModel = new MediaPathModel();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WndEditMediaPath"/> class.
        /// </summary>
        /// <param name="type">
        /// The type. 
        /// </param>
        /// <param name="mediaPathModel">
        /// The media path model. 
        /// </param>
        public WndEditMediaPath(MediaPathActionType type, MediaPathModel mediaPathModel)
        {
            this.InitializeComponent();

            this.actionType = type;
            this.EditingMediaPathModel = mediaPathModel;
        }

        #endregion

        #region Public Events

        /// <summary>
        ///   The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Enums

        /// <summary>
        ///   The media path action type.
        /// </summary>
        public enum MediaPathActionType
        {
            /// <summary>
            ///   The add.
            /// </summary>
            Add, 

            /// <summary>
            ///   The edit.
            /// </summary>
            Edit
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets a value indicating whether confirmed.
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        ///   Gets the editing media path model.
        /// </summary>
        public MediaPathModel EditingMediaPathModel
        {
            get
            {
                return this.editingMediaPathModel;
            }

            private set
            {
                this.editingMediaPathModel = value;
                this.OnPropertyChanged("EditingMediaPathModel");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name. 
        /// </param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// BTNs the cancel click.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// BTNs the media path browse click.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void BtnMediaPathBrowseClick(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog { SelectedPath = this.txtMediaPath.Text };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.EditingMediaPathModel.MediaPath = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// BTNs the ok click.
        /// </summary>
        /// <param name="sender">
        /// The sender. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            if (this.chkFolderContainsMovies.IsChecked == false && this.chkFolderContainsTvShows.IsChecked == false)
            {
                MessageBox.Show("Please select an import type");
                return;
            }

            switch (this.actionType)
            {
                case MediaPathActionType.Add:

                    // This can probably be prettier
                    if (MediaPathDBFactory.Instance.MediaPathDB.Any(model => model.MediaPath == this.EditingMediaPathModel.MediaPath))
                    {
                        MessageBox.Show("You can't add the same source twice!");
                        return;
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
        /// Handles the TextChanged event of the txtMediaPath control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void TxtMediaPathTextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnOK.IsEnabled = Directory.Exists(this.txtMediaPath.Text);
        }

        #endregion
    }
}