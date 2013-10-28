using System.Windows.Forms;
namespace SentraGL
{
    partial class frmSetingPerusahaan
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
            this.SuspendLayout();
            // 
            // frmSetingPerusahaan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(349, 132);
            this.Name = "frmSetingPerusahaan";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource setingPerusahaanBindingSource;
        private DevExpress.XtraEditors.TextEdit kodePerusahaanTextEdit;
        private DevExpress.XtraEditors.TextEdit namaPerusahaanTextEdit;
        private DevExpress.XtraEditors.TextEdit alamatPerusahaanTextEdit;
        private DevExpress.XtraEditors.TextEdit faxTextEdit;
        private DevExpress.XtraEditors.CheckEdit multiMataUangCheckEdit;
        private DevExpress.XtraEditors.CheckEdit multiDepartemenCheckEdit;
        private DevExpress.XtraEditors.CheckEdit multiProyekCheckEdit;
        private DevExpress.XtraEditors.TextEdit kotaTextEdit;
        private DevExpress.XtraEditors.TextEdit mataUangDasarTextEdit;
        private DevExpress.XtraEditors.PictureEdit logoPerusahaanPictureEdit;
        private DevExpress.XtraEditors.TextEdit telponTextEdit;
        private DevExpress.XtraEditors.DateEdit tglMulaiSistemBaruDateEdit;
    }
}
