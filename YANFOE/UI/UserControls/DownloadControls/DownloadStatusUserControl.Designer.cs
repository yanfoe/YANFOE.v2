namespace YANFOE.UI.UserControls.DownloadControls
{
    partial class DownloadStatusUserControl
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
            this.grpControl = new DevExpress.XtraEditors.GroupControl();
            this.prgBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtStatus = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).BeginInit();
            this.grpControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prgBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.prgBar);
            this.grpControl.Controls.Add(this.txtStatus);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControl.Location = new System.Drawing.Point(0, 0);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(710, 235);
            this.grpControl.TabIndex = 0;
            this.grpControl.Text = "groupControl1";
            this.grpControl.Paint += new System.Windows.Forms.PaintEventHandler(this.grpControl_Paint);
            // 
            // prgBar
            // 
            this.prgBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgBar.Location = new System.Drawing.Point(2, 42);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(706, 191);
            this.prgBar.TabIndex = 1;
            // 
            // txtStatus
            // 
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtStatus.Location = new System.Drawing.Point(2, 22);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(706, 20);
            this.txtStatus.TabIndex = 0;
            // 
            // DownloadStatusUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpControl);
            this.Name = "DownloadStatusUserControl";
            this.Size = new System.Drawing.Size(710, 235);
            ((System.ComponentModel.ISupportInitialize)(this.grpControl)).EndInit();
            this.grpControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.prgBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatus.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpControl;
        private DevExpress.XtraEditors.ProgressBarControl prgBar;
        private DevExpress.XtraEditors.TextEdit txtStatus;




    }
}
