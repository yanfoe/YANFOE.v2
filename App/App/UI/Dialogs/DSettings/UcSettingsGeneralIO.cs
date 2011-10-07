namespace YANFOE.UI.Dialogs.DSettings
{
    using System;
    using System.Collections.Generic;

    using DevExpress.XtraEditors.Controls;

    using YANFOE.IO;
    using YANFOE.Tools.Enums;

    public partial class UcSettingsGeneralIO : DevExpress.XtraEditors.XtraUserControl
    {
        private static List<IoInterface> handlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="UcSettingsGeneralIO"/> class.
        /// </summary>
        public UcSettingsGeneralIO()
        {
            InitializeComponent();

            handlers = IOHandler.ReturnAllHandlers();

            foreach (var handler in handlers)
            {
                if (handler.ShowInSettings)
                {
                    var radio = new RadioGroupItem(handler, handler.IOHandlerName);
                    radioHandlers.Properties.Items.Add(radio);
                }
            }

            this.SetCurrentTypeInUI(Settings.Get.InOutCollection.IoType);
        }

        private void SetCurrentTypeInUI(NFOType ioType)
        {
            for (int index = 0; index < this.radioHandlers.Properties.Items.Count; index++)
            {
                RadioGroupItem type = this.radioHandlers.Properties.Items[index];
                var t = type.Value as IoInterface;
                if (ioType == t.Type)
                {
                    this.radioHandlers.SelectedIndex = index;
                }
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the radioHandlers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void radioHandlers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = radioHandlers.Properties.Items[radioHandlers.SelectedIndex];
            var handler = item.Value as IoInterface;

            txtName.Text = handler.IOHandlerName;
            txtDescription.Text = handler.IOHandlerDescription;
            txtUrl.Text = handler.IOHandlerUri.ToString();

            Settings.Get.InOutCollection.IoType = handler.Type;
        }
    }
}
