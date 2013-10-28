using System.Windows.Forms;

namespace SentraWinSecurity.ConnectionForm
{
    partial class frmAccess
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit2 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.buttonEdit3 = new DevExpress.XtraEditors.ButtonEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(5, 23);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Koneksi ke Database yang sudah ada"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Buat File Database Baru")});
            this.radioGroup1.Size = new System.Drawing.Size(216, 50);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(20, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Nama Database";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(20, 132);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Template Database";
            this.checkEdit1.Size = new System.Drawing.Size(120, 19);
            this.checkEdit1.TabIndex = 6;
            this.checkEdit1.Visible = false;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(146, 79);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(186, 20);
            this.buttonEdit1.TabIndex = 1;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.Enabled = false;
            this.buttonEdit2.Location = new System.Drawing.Point(146, 131);
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit2.Size = new System.Drawing.Size(186, 20);
            this.buttonEdit2.TabIndex = 3;
            this.buttonEdit2.Visible = false;
            this.buttonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 108);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Nama Database";
            this.labelControl1.Visible = false;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(146, 106);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(186, 20);
            this.textEdit1.TabIndex = 2;
            this.textEdit1.Visible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Controls.Add(this.buttonEdit2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.checkEdit1);
            this.groupControl1.Controls.Add(this.buttonEdit3);
            this.groupControl1.Controls.Add(this.buttonEdit1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Location = new System.Drawing.Point(8, 10);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(354, 173);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Info Database";
            // 
            // buttonEdit3
            // 
            this.buttonEdit3.Location = new System.Drawing.Point(288, 53);
            this.buttonEdit3.Name = "buttonEdit3";
            this.buttonEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit3.Size = new System.Drawing.Size(44, 20);
            this.buttonEdit3.TabIndex = 1;
            this.buttonEdit3.Visible = false;
            this.buttonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit3_ButtonClick);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(287, 189);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "&Tutup";
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(183, 189);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(98, 23);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "&Simpan";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmAccess
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(374, 222);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "frmAccess";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Koneksi ke Database Access";
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit3;
    }
}