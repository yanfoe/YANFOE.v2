// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="WndWelcomePage.xaml.cs">
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
//   Interaction logic for WndWelcomePage.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.Dialogs.General
{
    #region Required Namespaces

    using System.Diagnostics;
    using System.Windows;

    using YANFOE.Settings;

    using Application = YANFOE.Settings.ConstSettings.Application;

    #endregion

    /// <summary>
    ///   Interaction logic for Welcome Page Window.
    /// </summary>
    public partial class WndWelcomePage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WndWelcomePage"/> class.
        /// </summary>
        public WndWelcomePage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The button donate_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnDonateClick(object sender, RoutedEventArgs e)
        {
            Process.Start(Application.DonateUrl);
        }

        /// <summary>
        /// The button o k_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnOkClick(object sender, RoutedEventArgs e)
        {
            Get.Ui.ShowWelcomeMessage = (bool)(!this.chkNeverShowAgain.IsChecked);
            this.Close();
        }

        #endregion
    }
}