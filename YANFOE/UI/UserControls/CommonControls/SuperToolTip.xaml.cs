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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YANFOE.UI.UserControls.CommonControls
{
    /// <summary>
    /// Interaction logic for SuperToolTip.xaml
    /// </summary>
    public partial class SuperToolTip : UserControl
    {
        public ToolTipItemCollection Items { get; set; }
        public bool AllowHtmlText { get; set; }

        public SuperToolTip()
        {
            InitializeComponent();
            Items = new ToolTipItemCollection();
        }
    }
    public class ToolTipItemCollection
    {
        public ToolTipTitleItem AddTitle(string text)
        {
            return new ToolTipTitleItem();
        }

        public ToolTipItem Add(string text)
        {
            return new ToolTipItem();
        }

        public int Add(BaseToolTipItem toolTipItem)
        {
            return 0;
        }
    }

    public class BaseToolTipItem
    {
        public System.Drawing.Image Image { get; set; }
        public string Text { get; set; }
    }

    public class ToolTipItem : BaseToolTipItem
    {

    }

    public class ToolTipTitleItem : BaseToolTipItem
    {

    }
}
