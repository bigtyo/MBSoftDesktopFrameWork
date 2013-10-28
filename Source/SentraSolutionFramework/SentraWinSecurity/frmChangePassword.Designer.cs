using System.Windows.Forms;

namespace SentraWinSecurity
{
    partial class frmChangePassword
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(214, 118);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(96, 26);
            this.simpleButton1.TabIndex = 3;
            this.simpleButton1.Text = "&Ubah Password";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(77, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Password Lama";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(214, 19);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.MaxLength = 50;
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(161, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(77, 60);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Password Baru";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(214, 57);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.MaxLength = 50;
            this.textEdit2.Properties.PasswordChar = '*';
            this.textEdit2.Size = new System.Drawing.Size(161, 20);
            this.textEdit2.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(77, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(123, 13);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Konfirmasi Password Baru";
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(214, 83);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.MaxLength = 50;
            this.textEdit3.Properties.PasswordChar = '*';
            this.textEdit3.Size = new System.Drawing.Size(161, 20);
            this.textEdit3.TabIndex = 2;
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(316, 118);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(59, 26);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "&Batal";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::SentraWinSecurity.Properties.Resources.keys;
            this.pictureEdit1.Location = new System.Drawing.Point(11, 22);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ReadOnly = true;
            this.pictureEdit1.Size = new System.Drawing.Size(60, 77);
            this.pictureEdit1.TabIndex = 5;
            // 
            // frmChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 158);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "frmChangePassword";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Ubah Password";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
    }
}