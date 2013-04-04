// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrmMoveDialog.cs" company="The YANFOE Project">
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
    using DevExpress.XtraEditors;

    /// <summary>
    /// The frm move dialog.
    /// </summary>
    public partial class FrmMoveDialog : XtraForm
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmMoveDialog"/> class.
        /// </summary>
        public FrmMoveDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ETA.
        /// </summary>
        /// <value>
        /// The ETA.
        /// </value>
        public string ETA
        {
            get
            {
                return this.txtEta.Text;
            }

            set
            {
                this.txtEta.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public string From
        {
            get
            {
                return this.txtFrom.Text;
            }

            set
            {
                this.txtFrom.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the progress current.
        /// </summary>
        /// <value>
        /// The progress current.
        /// </value>
        public int ProgressCurrent
        {
            get
            {
                return (int)this.progressBarControl1.EditValue;
            }

            set
            {
                this.progressBarControl1.EditValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the progress max.
        /// </summary>
        /// <value>
        /// The progress max.
        /// </value>
        public int ProgressMax
        {
            get
            {
                return this.progressBarControl1.Properties.Maximum;
            }

            set
            {
                this.progressBarControl1.Properties.Maximum = value;
            }
        }

        /// <summary>
        /// Gets or sets the too.
        /// </summary>
        /// <value>
        /// The too.
        /// </value>
        public string Too
        {
            get
            {
                return this.txtTo.Text;
            }

            set
            {
                this.txtTo.Text = value;
            }
        }

        #endregion
    }
}