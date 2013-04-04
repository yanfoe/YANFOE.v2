// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportMissingEpisodes.cs" company="The YANFOE Project">
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
    using System.IO;

    using DevExpress.XtraEditors;
    using DevExpress.XtraBars;

    using YANFOE.Tools.Exporting;

    public partial class ExportMissingEpisodes : DevExpress.XtraEditors.XtraForm
    {
        public ExportMissingEpisodes(List<EpisodeTreeList> source, string title = null)
        {
            InitializeComponent();
            var templates = Exporting.GetExportTemplates("tv");
            if (templates.Count > 0)
            {
                var submenu = new BarSubItem(this.barManager1, "Templates");
                foreach (var template in templates)
                {
                    var menu = new BarButtonItem(this.barManager1, template.name);
                    menu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.popupMenuExportTo_ItemClick);
                    submenu.AddItem(menu);
                }
                this.popupMenuExportTo.AddItem(submenu);
            }
            this.treeList1.DataSource = source;
            this.Text = string.Format(title ?? "Missing Episodes List ({0})", source.Count);
        }

        private void popupMenuExportTo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.dropDownExportTo.Text = e.Item.Caption;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (this.dropDownExportTo.Text == "Export to ...")
            {
                XtraMessageBox.Show("Please select export format", "Error");
                return;
            }

            var form = new SimpleBrowseForm();
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                var ext = Path.HasExtension(form.getInput());

                switch (this.dropDownExportTo.Text)
                {
                    case "HTML":
                        this.treeList1.ExportToHtml(ext ? form.getInput() : Path.Combine(form.getInput(), "MissingEpisodesList.html"));
                        break;
                    case "PDF":
                        this.treeList1.ExportToPdf(ext ? form.getInput() : Path.Combine(form.getInput(), "MissingEpisodesList.pdf"));
                        break;
                    case "XML":
                        this.treeList1.ExportToXml(ext ? form.getInput() : Path.Combine(form.getInput(), "MissingEpisodesList.xml"));
                        break;
                    default:
                        // Should never happen
                        //XtraMessageBox.Show("Selected method is not supported!", "Error!");
                        Exporting.ExportMissingEpisodesTemplate(this.dropDownExportTo.Text, form.getInput());
                        break;
                }
            }
            else
            {
                return;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}