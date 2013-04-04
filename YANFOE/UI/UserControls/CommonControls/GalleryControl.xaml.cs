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
using Newtonsoft.Json;

namespace YANFOE.UI.UserControls.CommonControls
{
    /// <summary>
    /// Interaction logic for GalleryControl.xaml
    /// </summary>
    public partial class GalleryControl : UserControl
    {
        public GalleryControl()
        {
            InitializeComponent();
        }
    }

    public class GalleryItemGroup
    {
        public List<GalleryItem> Items { get; set; }

        public GalleryItemGroup()
        {
            Items = new List<GalleryItem>();
        }
    }

    public class GalleryItem
    {
        public GalleryItem(System.Drawing.Image image, string caption, string description)
        {
            Image = image;
            Caption = caption;
            Description = description;
        }

        public string Caption { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public System.Drawing.Image Image { get; set; }
        public SuperToolTip SuperTip { get; set; }
        public object Tag { get; set; }
    }
}
