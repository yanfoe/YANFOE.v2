// --------------------------------------------------------------------------------------------------------------------
// <copyright company="The YANFOE Project" file="LogsUserControl.xaml.cs">
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
//   Interaction logic for LogsUserControl.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace YANFOE.UI.UserControls.LogControls
{
    #region Required Namespaces

    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    using BitFactory.Logging;

    using YANFOE.InternalApps.Logs;
    using YANFOE.InternalApps.Logs.Enums;
    using YANFOE.Settings;

    #endregion

    /// <summary>
    ///   Interaction logic for LogsUserControl.xaml
    /// </summary>
    public partial class LogsUserControl : UserControl
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LogsUserControl"/> class.
        /// </summary>
        public LogsUserControl()
        {
            this.InitializeComponent();

            Binding b = new Binding("EnableLog");
            b.Source = Get.LogSettings;
            BindingOperations.SetBinding(this.chbEnableLog, ToggleButton.IsCheckedProperty, b);

            this.chbEnableLog.Content = (this.chbEnableLog.IsChecked == true) ? "Log Enabled" : "Log Disabled";

            int index = 1;
            switch (Log.LogThreshold)
            {
                case LogSeverity.Debug:
                    this.radioButton1.IsChecked = true;
                    break;
                case LogSeverity.Info:
                    this.radioButton2.IsChecked = true;
                    break;
                case LogSeverity.Status:
                    this.radioButton3.IsChecked = true;
                    break;
                case LogSeverity.Warning:
                    this.radioButton4.IsChecked = true;
                    break;
                case LogSeverity.Error:
                    this.radioButton5.IsChecked = true;
                    break;
                case LogSeverity.Critical:
                    this.radioButton6.IsChecked = true;
                    break;
                case LogSeverity.Fatal:
                    this.radioButton7.IsChecked = true;
                    break;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Click event of the btnClearLog control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            Log.ClearInternalLogger(LoggerName.GeneralLog);
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbEnableLog control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void chbEnableLog_Click(object sender, RoutedEventArgs e)
        {
            this.chbEnableLog.Content = (this.chbEnableLog.IsChecked == true) ? "Log Enabled" : "Log Disabled";
        }

        /// <summary>
        /// The radio button_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void radioButton_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RadioButton changed = (RadioButton)sender;

            if (changed.IsChecked == true)
            {
                var logTreshold = (LogSeverity)Enum.ToObject(typeof(LogSeverity), int.Parse(changed.Tag.ToString()));
                Log.LogThreshold = logTreshold;
            }
        }

        #endregion
    }
}