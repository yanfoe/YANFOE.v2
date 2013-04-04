// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="TvTopMenu.xaml.cs">
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
//   Interaction logic for TvTopMenu.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.UserControls.TvControls
{
    #region Required Namespaces

    using System.Windows;
    using System.Windows.Controls;

    using YANFOE.UI.Dialogs.TV;

    #endregion

    /// <summary>
    ///   Interaction logic for TvTopMenu.xaml
    /// </summary>
    public partial class TvTopMenu : UserControl
    {
        #region Static Fields

        /// <summary>
        ///   The type property.
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            "Type", typeof(SaveType), typeof(TvTopMenu));

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref="TvTopMenu" /> class.
        /// </summary>
        public TvTopMenu()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Enums

        /// <summary>
        ///   The save type.
        /// </summary>
        public enum SaveType
        {
            /// <summary>
            ///   The all.
            /// </summary>
            All, 

            /// <summary>
            ///   The save series.
            /// </summary>
            SaveSeries, 

            /// <summary>
            ///   The save season.
            /// </summary>
            SaveSeason, 

            /// <summary>
            ///   The save episode.
            /// </summary>
            SaveEpisode
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the type.
        /// </summary>
        public SaveType Type
        {
            get
            {
                return (SaveType)this.GetValue(TypeProperty);
            }

            set
            {
                this.SetValue(TypeProperty, value);
            }
        }

        #endregion

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var update = new WndUpdateShows(true);
            update.ShowDialog();
        }
    }
}