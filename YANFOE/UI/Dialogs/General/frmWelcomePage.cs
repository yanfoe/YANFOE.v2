// --------------------------------------------------------------------------------------------------------------------
// <copyright file="frmWelcomePage.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the frmWelcomePage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.General
{
    using System;
    using System.Diagnostics;

    public partial class frmWelcomePage : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmWelcomePage"/> class.
        /// </summary>
        public frmWelcomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the butOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void butOk_Click(object sender, EventArgs e)
        {
            Settings.Get.Ui.ShowWelcomeMessage = !chkNeverShowAgain.Checked;
            this.Close();
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.ConstSettings.Application.DonateUrl);
        }
    }
}