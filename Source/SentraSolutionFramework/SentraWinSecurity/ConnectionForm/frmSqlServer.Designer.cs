using System.Windows.Forms;

namespace SentraWinSecurity.ConnectionForm
{
    partial class frmSqlServer
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.buttonEdit2 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit3 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nama Server";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(111, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(235, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 121);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Nama Database";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(138, 118);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "Refresh")});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(208, 20);
            this.comboBoxEdit1.TabIndex = 3;
            this.comboBoxEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.comboBoxEdit1_ButtonClick);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(63, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Jenis Koneksi";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(107, 58);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Integrated"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Standar")});
            this.radioGroup1.Size = new System.Drawing.Size(192, 26);
            this.radioGroup1.TabIndex = 1;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Enabled = false;
            this.labelControl4.Location = new System.Drawing.Point(111, 90);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Nama User";
            // 
            // labelControl5
            // 
            this.labelControl5.Enabled = false;
            this.labelControl5.Location = new System.Drawing.Point(111, 116);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(46, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Password";
            // 
            // textEdit2
            // 
            this.textEdit2.Enabled = false;
            this.textEdit2.Location = new System.Drawing.Point(169, 87);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(106, 20);
            this.textEdit2.TabIndex = 2;
            // 
            // textEdit3
            // 
            this.textEdit3.Enabled = false;
            this.textEdit3.Location = new System.Drawing.Point(169, 113);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.PasswordChar = '*';
            this.textEdit3.Size = new System.Drawing.Size(106, 20);
            this.textEdit3.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.DialogResult = DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(187, 377);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(98, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "&Simpan";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(291, 377);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "&Tutup";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Controls.Add(this.textEdit3);
            this.groupControl1.Controls.Add(this.textEdit2);
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(9, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(357, 148);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Info Server";
            // 
            // radioGroup2
            // 
            this.radioGroup2.Location = new System.Drawing.Point(12, 18);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup2.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Koneksi ke Database yang sudah ada"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Buat Database Baru dari File Backup"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Attach File Database yang sudah ada"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Buat File Database Baru")});
            this.radioGroup2.Size = new System.Drawing.Size(216, 94);
            this.radioGroup2.TabIndex = 0;
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup2_SelectedIndexChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.checkEdit1);
            this.groupControl2.Controls.Add(this.textEdit4);
            this.groupControl2.Controls.Add(this.buttonEdit2);
            this.groupControl2.Controls.Add(this.buttonEdit3);
            this.groupControl2.Controls.Add(this.buttonEdit1);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Controls.Add(this.radioGroup2);
            this.groupControl2.Controls.Add(this.comboBoxEdit1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Location = new System.Drawing.Point(9, 166);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(357, 205);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "Info Database";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(16, 171);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Template Database";
            this.checkEdit1.Size = new System.Drawing.Size(120, 19);
            this.checkEdit1.TabIndex = 5;
            this.checkEdit1.Visible = false;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // textEdit4
            // 
            this.textEdit4.Location = new System.Drawing.Point(305, 49);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(41, 20);
            this.textEdit4.TabIndex = 1;
            this.textEdit4.Visible = false;
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.Location = new System.Drawing.Point(138, 144);
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit2.Size = new System.Drawing.Size(208, 20);
            this.buttonEdit2.TabIndex = 4;
            this.buttonEdit2.Visible = false;
            this.buttonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
            // 
            // buttonEdit3
            // 
            this.buttonEdit3.Location = new System.Drawing.Point(305, 75);
            this.buttonEdit3.Name = "buttonEdit3";
            this.buttonEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit3.Size = new System.Drawing.Size(41, 20);
            this.buttonEdit3.TabIndex = 2;
            this.buttonEdit3.Visible = false;
            this.buttonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit3_ButtonClick);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Enabled = false;
            this.buttonEdit1.Location = new System.Drawing.Point(138, 170);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(208, 20);
            this.buttonEdit1.TabIndex = 6;
            this.buttonEdit1.Visible = false;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(244, 23);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(102, 13);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "File Backup Database";
            this.labelControl6.Visible = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(16, 147);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(79, 13);
            this.labelControl7.TabIndex = 9;
            this.labelControl7.Text = "Folder Database";
            this.labelControl7.Visible = false;
            // 
            // frmSqlServer
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(376, 412);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "frmSqlServer";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Koneksi ke Database Sql Server";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit3;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}