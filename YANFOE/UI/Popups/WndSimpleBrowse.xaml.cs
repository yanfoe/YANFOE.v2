using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YANFOE.UI.Popups
{
    using WinForms = System.Windows.Forms;
    /// <summary>
    /// Interaction logic for WndSimpleBrowse.xaml
    /// </summary>
    public partial class WndSimpleBrowse : Window
    {
        private string _input;
        private browseType _type;


        private FolderBrowserDialog folderBrowserDialog1;
        private OpenFileDialog openFileDialog1;

        public WndSimpleBrowse(browseType type = browseType.Folder)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            _type = type;
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this._input = this.textEdit1.Text;
            this.Close();
        }

        public string getInput()
        {
            return _input;
        }
        
        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            if (_type == browseType.Folder)
            {
                this.folderBrowserDialog1.ShowNewFolderButton = true;
                this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;
                if (this.folderBrowserDialog1.ShowDialog() == WinForms.DialogResult.OK)
                {
                    this.textEdit1.Text = this.folderBrowserDialog1.SelectedPath;
                }
            }
            else
            {
                this.openFileDialog1.InitialDirectory = Environment.SpecialFolder.Desktop.ToString();
                this.openFileDialog1.Multiselect = false;
                if (this.openFileDialog1.ShowDialog() == WinForms.DialogResult.OK)
                {
                    this.textEdit1.Text = this.openFileDialog1.FileName;
                }
            }
        }

        public enum browseType
        {
            Folder = 1,
            File
        }
    }
}
