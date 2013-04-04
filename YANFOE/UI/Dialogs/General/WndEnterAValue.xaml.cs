using System;
using System.Collections.Generic;
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

namespace YANFOE.UI.Dialogs.General
{
    /// <summary>
    /// Interaction logic for WndEnterAValue.xaml
    /// </summary>
    public partial class WndEnterAValue : Window
    {
        public enum EnterValueType
        {
            Text,
            Integer
        }

        public string Question { private get; set; }

        public string Response { get; private set; }

        public bool Cancelled { get; set; }

        public WndEnterAValue(string question = null, EnterValueType type = EnterValueType.Text)
        {
            InitializeComponent();

            txtInput.Focus();

            this.Question = question;

            if (question != null)
            {
                groupControl.Header = this.Question;
                txtInput.Text = string.Empty;
            }

            if (type == EnterValueType.Integer)
            {
                //textEdit.Properties.Mask.MaskType = MaskType.RegEx;
                //textEdit.Properties.Mask.EditMask = @"\d+";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Cancelled = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Response = this.txtInput.Text;
            this.Close();
        }
    }
}
