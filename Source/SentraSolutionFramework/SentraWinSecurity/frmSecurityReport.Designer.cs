using System.Windows.Forms;

namespace SentraWinSecurity
{
    partial class frmSecurityReport
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
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit10 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit11 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit2 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit12 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit10.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit11.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit12.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.checkEdit1);
            this.groupControl2.Controls.Add(this.checkEdit4);
            this.groupControl2.Controls.Add(this.checkEdit10);
            this.groupControl2.Controls.Add(this.checkEdit11);
            this.groupControl2.Controls.Add(this.checkEdit2);
            this.groupControl2.Controls.Add(this.checkEdit12);
            this.groupControl2.Location = new System.Drawing.Point(12, 43);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(402, 182);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Hak Akses Laporan";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(18, 145);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "&Lihat Laporan";
            this.checkEdit1.Size = new System.Drawing.Size(141, 19);
            this.checkEdit1.TabIndex = 5;
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(18, 107);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "S&impan Layout Laporan";
            this.checkEdit4.Size = new System.Drawing.Size(141, 19);
            this.checkEdit4.TabIndex = 4;
            this.checkEdit4.CheckedChanged += new System.EventHandler(this.checkEdit12_CheckedChanged);
            // 
            // checkEdit10
            // 
            this.checkEdit10.Location = new System.Drawing.Point(18, 82);
            this.checkEdit10.Name = "checkEdit10";
            this.checkEdit10.Properties.Caption = "&Simpan Laporan";
            this.checkEdit10.Size = new System.Drawing.Size(117, 19);
            this.checkEdit10.TabIndex = 3;
            this.checkEdit10.CheckedChanged += new System.EventHandler(this.checkEdit12_CheckedChanged);
            // 
            // checkEdit11
            // 
            this.checkEdit11.Location = new System.Drawing.Point(18, 57);
            this.checkEdit11.Name = "checkEdit11";
            this.checkEdit11.Properties.Caption = "De&sain Cetak";
            this.checkEdit11.Size = new System.Drawing.Size(117, 19);
            this.checkEdit11.TabIndex = 2;
            this.checkEdit11.CheckedChanged += new System.EventHandler(this.checkEdit12_CheckedChanged);
            // 
            // checkEdit2
            // 
            this.checkEdit2.Location = new System.Drawing.Point(340, 0);
            this.checkEdit2.Name = "checkEdit2";
            this.checkEdit2.Properties.Caption = "Semua";
            this.checkEdit2.Size = new System.Drawing.Size(57, 19);
            this.checkEdit2.TabIndex = 0;
            this.checkEdit2.CheckedChanged += new System.EventHandler(this.checkEdit2_CheckedChanged);
            // 
            // checkEdit12
            // 
            this.checkEdit12.Location = new System.Drawing.Point(18, 32);
            this.checkEdit12.Name = "checkEdit12";
            this.checkEdit12.Properties.Caption = "&Cetak";
            this.checkEdit12.Size = new System.Drawing.Size(117, 19);
            this.checkEdit12.TabIndex = 1;
            this.checkEdit12.CheckedChanged += new System.EventHandler(this.checkEdit12_CheckedChanged);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(339, 236);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "&Batal";
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(258, 236);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "&Simpan";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(96, 12);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "labelControl2";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(73, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Nama Laporan:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkedListBoxControl1);
            this.groupControl1.Controls.Add(this.checkEdit3);
            this.groupControl1.Location = new System.Drawing.Point(228, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(186, 182);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Nama Laporan";
            this.groupControl1.Visible = false;
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Location = new System.Drawing.Point(6, 24);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(175, 153);
            this.checkedListBoxControl1.TabIndex = 0;
            // 
            // checkEdit3
            // 
            this.checkEdit3.Location = new System.Drawing.Point(124, 0);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "Semua";
            this.checkEdit3.Size = new System.Drawing.Size(57, 19);
            this.checkEdit3.TabIndex = 0;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            // 
            // frmSecurityReport
            // 
            this.AcceptButton = this.simpleButton1;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(426, 271);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSecurityReport";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hak Akses Laporan";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit10.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit11.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit12.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit10;
        private DevExpress.XtraEditors.CheckEdit checkEdit11;
        private DevExpress.XtraEditors.CheckEdit checkEdit12;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdit2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl1;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;

    }
}