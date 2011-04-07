// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmEnterAValue.cs" company="The YANFOE Project">
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
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.Dialogs.General
{
    using System;
    using System.Windows.Forms;

    using DevExpress.XtraEditors;

    public partial class FrmEnterAValue : XtraForm
    {
        public string Question { private get; set; }

        public string Response { get; private set; }

        public bool Cancelled { get; set; }

        public FrmEnterAValue(string question = null)
        {
            InitializeComponent();

            textEdit.Focus();

            Question = question;

            if (question != null)
            {
                groupControl.Text = this.Question;
                Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnOkClick(object sender, EventArgs e)
        {
            this.Response = this.textEdit.Text;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BtnCancelClick(object sender, EventArgs e)
        {
            this.Cancelled = true;
            this.Close();
        }

        /// <summary>
        /// Handles the KeyPress event of the textEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void TextEditKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.BtnOkClick(null, null);
            }
        }
    }
}