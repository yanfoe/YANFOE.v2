// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleBrowserForm.cs" company="The YANFOE Project">
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

namespace YANFOE.UI.Popups
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;


    public partial class SimpleBrowseForm : DevExpress.XtraEditors.XtraForm
    {
        private string _input;

        public SimpleBrowseForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this._input = this.textEdit1.Text;
            this.Close();
        }

        public string getInput()
        {
            return _input;
        }

        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowNewFolderButton = true;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textEdit1.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }
    }
}