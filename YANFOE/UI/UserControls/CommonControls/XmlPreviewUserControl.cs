// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlPreviewUserControl.cs" company="The YANFOE Project">
//   Copyright 2011 The YANFOE Project
// </copyright>
// <summary>
//   Defines the XmlPreviewUserControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace YANFOE.UI.UserControls.CommonControls
{
    using System;

    using YANFOE.Tools.Xml;

    public partial class XmlPreviewUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public XmlPreviewUserControl()
        {
            InitializeComponent();
        }

        public void SetXML(string xml)
        {
            XFormat.AddColouredText(xml, txtXmlPreview);
        }

        public void Clear()
        {
            txtXmlPreview.Clear();
        }
    }
}
