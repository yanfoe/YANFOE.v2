namespace YANFOE.UI.Dialogs.DSettings
{
    partial class UcSettingsGeneralUi
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbSkinList = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkEnableTVPathColumn = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.chkShowTVSeries0 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSkinList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableTVPathColumn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowTVSeries0.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmbSkinList);
            this.groupControl1.Location = new System.Drawing.Point(4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(231, 59);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Change UI Skin";
            // 
            // cmbSkinList
            // 
            this.cmbSkinList.Location = new System.Drawing.Point(6, 26);
            this.cmbSkinList.Name = "cmbSkinList";
            this.cmbSkinList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSkinList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbSkinList.Size = new System.Drawing.Size(215, 20);
            toolTipTitleItem1.Text = "Change UI Skin";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Change the look and feel of YANFOE.\r\nDefault skin is \"Foggy\"";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.cmbSkinList.SuperTip = superToolTip1;
            this.cmbSkinList.TabIndex = 0;
            // 
            // chkEnableTVPathColumn
            // 
            this.chkEnableTVPathColumn.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkEnableTVPathColumn.Location = new System.Drawing.Point(2, 22);
            this.chkEnableTVPathColumn.Name = "chkEnableTVPathColumn";
            this.chkEnableTVPathColumn.Properties.Caption = "Enable TV Episode Path && File Column";
            this.chkEnableTVPathColumn.Size = new System.Drawing.Size(228, 19);
            this.chkEnableTVPathColumn.TabIndex = 5;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkShowTVSeries0);
            this.groupControl2.Controls.Add(this.chkEnableTVPathColumn);
            this.groupControl2.Location = new System.Drawing.Point(3, 80);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(232, 100);
            this.groupControl2.TabIndex = 6;
            this.groupControl2.Text = "Visability";
            // 
            // chkShowTVSeries0
            // 
            this.chkShowTVSeries0.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkShowTVSeries0.Location = new System.Drawing.Point(2, 41);
            this.chkShowTVSeries0.Name = "chkShowTVSeries0";
            this.chkShowTVSeries0.Properties.Caption = "Show TV Series 0";
            this.chkShowTVSeries0.Size = new System.Drawing.Size(228, 19);
            this.chkShowTVSeries0.TabIndex = 6;
            // 
            // UcSettingsGeneralUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "UcSettingsGeneralUi";
            this.Size = new System.Drawing.Size(751, 570);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSkinList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableTVPathColumn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowTVSeries0.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSkinList;
        private DevExpress.XtraEditors.CheckEdit chkEnableTVPathColumn;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit chkShowTVSeries0;

    }
}
