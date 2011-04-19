using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace YANFOE.UI.Dialogs.General
{
    public partial class FrmShowNotification : DevExpress.XtraEditors.XtraForm
    {
        public FrmShowNotification()
        {
            InitializeComponent();
        }

        public FrmShowNotification(string title = null)
        {
            InitializeComponent();
            if (title != null)
            {
                ltTitle.Text = title;
            }
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}