namespace SentraWinFramework
{
    partial class GridRowMover
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
            this.vScrollBar1 = new DevExpress.XtraEditors.VScrollBar();
            this.hyperLinkEdit1 = new DevExpress.XtraEditors.HyperLinkEdit();
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.LargeChange = 1;
            this.vScrollBar1.Location = new System.Drawing.Point(64, 24);
            this.vScrollBar1.Maximum = 1000;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 31);
            this.vScrollBar1.TabIndex = 37;
            this.vScrollBar1.Value = 500;
            this.vScrollBar1.ValueChanged += new System.EventHandler(this.vScrollBar1_ValueChanged);
            // 
            // hyperLinkEdit1
            // 
            this.hyperLinkEdit1.EditValue = "Hapus Baris";
            this.hyperLinkEdit1.Location = new System.Drawing.Point(0, 0);
            this.hyperLinkEdit1.Name = "hyperLinkEdit1";
            this.hyperLinkEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.hyperLinkEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.hyperLinkEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.hyperLinkEdit1.Size = new System.Drawing.Size(61, 18);
            this.hyperLinkEdit1.TabIndex = 38;
            this.hyperLinkEdit1.ToolTip = "(Ctrl-D) Hapus Baris Grid";
            this.hyperLinkEdit1.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEdit1_OpenLink);
            // 
            // GridRowMover
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hyperLinkEdit1);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = true;
            this.Name = "GridRowMover";
            this.Size = new System.Drawing.Size(87, 62);
            ((System.ComponentModel.ISupportInitialize)(this.hyperLinkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.VScrollBar vScrollBar1;
        private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEdit1;

    }
}
