using System.Windows.Forms;
namespace SentraWinFramework
{
    partial class frmAturTampilan
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = true;
            this.radioGroup1.Location = new System.Drawing.Point(82, 12);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Caramel"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Money Twins"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Lilian"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "The Asphalt World"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "iMaginary"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Coffee"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Liquid Sky"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "London Liquid Sky"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Glass Oceans"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Stardust"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Black"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Blue"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Office 2007 Blue"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Office 2007 Black"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Office 2007 Silver"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Office 2007 Green"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Office 2007 Pink"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Holiday - Winter"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Love - Pink"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Mac - Black"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Summer"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Pumpkin"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Dark Side")});
            this.radioGroup1.Size = new System.Drawing.Size(265, 238);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::SentraWinFramework.Properties.Resources.monitor_brush;
            this.pictureEdit1.Location = new System.Drawing.Point(11, 30);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Size = new System.Drawing.Size(65, 108);
            this.pictureEdit1.TabIndex = 1;
            // 
            // frmAturTampilan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 265);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.radioGroup1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmAturTampilan";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Atur Tampilan";
            this.KeyDown += new KeyEventHandler(this.frmAturTampilan_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}