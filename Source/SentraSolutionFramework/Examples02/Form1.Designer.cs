namespace Examples02
{
    partial class frmPelanggan
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label noPelangganLabel;
            System.Windows.Forms.Label namaPelangganLabel;
            System.Windows.Forms.Label alamatLabel;
            System.Windows.Forms.Label tglDaftarLabel;
            System.Windows.Forms.Label telponLabel;
            this.noPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.pelangganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.namaPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.alamatTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.tglDaftarDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.photoPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.telponTextEdit = new DevExpress.XtraEditors.TextEdit();
            noPelangganLabel = new System.Windows.Forms.Label();
            namaPelangganLabel = new System.Windows.Forms.Label();
            alamatLabel = new System.Windows.Forms.Label();
            tglDaftarLabel = new System.Windows.Forms.Label();
            telponLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglDaftarDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.telponTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // noPelangganLabel
            // 
            noPelangganLabel.AutoSize = true;
            noPelangganLabel.Location = new System.Drawing.Point(31, 31);
            noPelangganLabel.Name = "noPelangganLabel";
            noPelangganLabel.Size = new System.Drawing.Size(78, 13);
            noPelangganLabel.TabIndex = 1;
            noPelangganLabel.Text = "No Pelanggan:";
            // 
            // namaPelangganLabel
            // 
            namaPelangganLabel.AutoSize = true;
            namaPelangganLabel.Location = new System.Drawing.Point(17, 57);
            namaPelangganLabel.Name = "namaPelangganLabel";
            namaPelangganLabel.Size = new System.Drawing.Size(92, 13);
            namaPelangganLabel.TabIndex = 2;
            namaPelangganLabel.Text = "Nama Pelanggan:";
            // 
            // alamatLabel
            // 
            alamatLabel.AutoSize = true;
            alamatLabel.Location = new System.Drawing.Point(67, 83);
            alamatLabel.Name = "alamatLabel";
            alamatLabel.Size = new System.Drawing.Size(42, 13);
            alamatLabel.TabIndex = 4;
            alamatLabel.Text = "Alamat:";
            // 
            // tglDaftarLabel
            // 
            tglDaftarLabel.AutoSize = true;
            tglDaftarLabel.Location = new System.Drawing.Point(52, 134);
            tglDaftarLabel.Name = "tglDaftarLabel";
            tglDaftarLabel.Size = new System.Drawing.Size(57, 13);
            tglDaftarLabel.TabIndex = 7;
            tglDaftarLabel.Text = "Tgl Daftar:";
            // 
            // noPelangganTextEdit
            // 
            this.noPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "NoPelanggan", true));
            this.noPelangganTextEdit.Location = new System.Drawing.Point(115, 28);
            this.noPelangganTextEdit.Name = "noPelangganTextEdit";
            this.noPelangganTextEdit.Size = new System.Drawing.Size(100, 20);
            this.noPelangganTextEdit.TabIndex = 2;
            // 
            // pelangganBindingSource
            // 
            this.pelangganBindingSource.DataSource = typeof(Examples02.Entity.Pelanggan);
            // 
            // namaPelangganTextEdit
            // 
            this.namaPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "NamaPelanggan", true));
            this.namaPelangganTextEdit.Location = new System.Drawing.Point(115, 54);
            this.namaPelangganTextEdit.Name = "namaPelangganTextEdit";
            this.namaPelangganTextEdit.Size = new System.Drawing.Size(100, 20);
            this.namaPelangganTextEdit.TabIndex = 3;
            // 
            // alamatTextEdit
            // 
            this.alamatTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Alamat", true));
            this.alamatTextEdit.Location = new System.Drawing.Point(115, 80);
            this.alamatTextEdit.Name = "alamatTextEdit";
            this.alamatTextEdit.Size = new System.Drawing.Size(100, 20);
            this.alamatTextEdit.TabIndex = 5;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(115, 106);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "&Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 7;
            // 
            // tglDaftarDateEdit
            // 
            this.tglDaftarDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "TglDaftar", true));
            this.tglDaftarDateEdit.EditValue = null;
            this.tglDaftarDateEdit.Location = new System.Drawing.Point(115, 131);
            this.tglDaftarDateEdit.Name = "tglDaftarDateEdit";
            this.tglDaftarDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglDaftarDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglDaftarDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglDaftarDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglDaftarDateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglDaftarDateEdit.TabIndex = 8;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(13, 188);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 9;
            this.simpleButton1.Text = "&Baru";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(94, 188);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(36, 23);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "|<";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(136, 188);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(36, 23);
            this.simpleButton3.TabIndex = 9;
            this.simpleButton3.Text = "<<";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Location = new System.Drawing.Point(178, 188);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(36, 23);
            this.simpleButton4.TabIndex = 9;
            this.simpleButton4.Text = ">>";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(220, 188);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(36, 23);
            this.simpleButton5.TabIndex = 9;
            this.simpleButton5.Text = ">|";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(262, 188);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(75, 23);
            this.simpleButton6.TabIndex = 9;
            this.simpleButton6.Text = "&Hapus";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Location = new System.Drawing.Point(361, 188);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(75, 23);
            this.simpleButton7.TabIndex = 9;
            this.simpleButton7.Text = "&Simpan";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // photoPictureEdit
            // 
            this.photoPictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Photo", true));
            this.photoPictureEdit.Location = new System.Drawing.Point(262, 28);
            this.photoPictureEdit.Name = "photoPictureEdit";
            this.photoPictureEdit.Size = new System.Drawing.Size(107, 123);
            this.photoPictureEdit.TabIndex = 17;
            // 
            // telponLabel
            // 
            telponLabel.AutoSize = true;
            telponLabel.Location = new System.Drawing.Point(65, 160);
            telponLabel.Name = "telponLabel";
            telponLabel.Size = new System.Drawing.Size(43, 13);
            telponLabel.TabIndex = 17;
            telponLabel.Text = "Telpon:";
            // 
            // telponTextEdit
            // 
            this.telponTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Telpon", true));
            this.telponTextEdit.Location = new System.Drawing.Point(114, 157);
            this.telponTextEdit.Name = "telponTextEdit";
            this.telponTextEdit.Size = new System.Drawing.Size(100, 20);
            this.telponTextEdit.TabIndex = 18;
            // 
            // frmPelanggan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 233);
            this.Controls.Add(telponLabel);
            this.Controls.Add(this.telponTextEdit);
            this.Controls.Add(this.photoPictureEdit);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton7);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(tglDaftarLabel);
            this.Controls.Add(this.tglDaftarDateEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(alamatLabel);
            this.Controls.Add(this.alamatTextEdit);
            this.Controls.Add(namaPelangganLabel);
            this.Controls.Add(this.namaPelangganTextEdit);
            this.Controls.Add(noPelangganLabel);
            this.Controls.Add(this.noPelangganTextEdit);
            this.Name = "frmPelanggan";
            this.Text = "Master Pelanggan";
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglDaftarDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.telponTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource pelangganBindingSource;
        private DevExpress.XtraEditors.TextEdit noPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit namaPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit alamatTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.DateEdit tglDaftarDateEdit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.PictureEdit photoPictureEdit;
        private DevExpress.XtraEditors.TextEdit telponTextEdit;
    }
}

