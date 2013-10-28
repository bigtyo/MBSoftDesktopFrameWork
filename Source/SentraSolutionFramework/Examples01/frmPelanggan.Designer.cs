namespace Examples01
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
            System.Windows.Forms.Label tglRegisterLabel;
            System.Windows.Forms.Label kotaLabel1;
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.pelangganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.noPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.alamatTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.tglRegisterDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.photoPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.kotaComboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.colNoPelanggan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNamaPelanggan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlamat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTglRegister = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAktif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPhoto = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTglJamUpdate = new DevExpress.XtraGrid.Columns.GridColumn();
            noPelangganLabel = new System.Windows.Forms.Label();
            namaPelangganLabel = new System.Windows.Forms.Label();
            alamatLabel = new System.Windows.Forms.Label();
            tglRegisterLabel = new System.Windows.Forms.Label();
            kotaLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglRegisterDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kotaComboBoxEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // noPelangganLabel
            // 
            noPelangganLabel.AutoSize = true;
            noPelangganLabel.Location = new System.Drawing.Point(29, 39);
            noPelangganLabel.Name = "noPelangganLabel";
            noPelangganLabel.Size = new System.Drawing.Size(77, 13);
            noPelangganLabel.TabIndex = 2;
            noPelangganLabel.Text = "No Pelanggan:";
            // 
            // namaPelangganLabel
            // 
            namaPelangganLabel.AutoSize = true;
            namaPelangganLabel.Location = new System.Drawing.Point(15, 65);
            namaPelangganLabel.Name = "namaPelangganLabel";
            namaPelangganLabel.Size = new System.Drawing.Size(91, 13);
            namaPelangganLabel.TabIndex = 3;
            namaPelangganLabel.Text = "Nama Pelanggan:";
            // 
            // alamatLabel
            // 
            alamatLabel.AutoSize = true;
            alamatLabel.Location = new System.Drawing.Point(65, 91);
            alamatLabel.Name = "alamatLabel";
            alamatLabel.Size = new System.Drawing.Size(44, 13);
            alamatLabel.TabIndex = 5;
            alamatLabel.Text = "Alamat:";
            // 
            // tglRegisterLabel
            // 
            tglRegisterLabel.AutoSize = true;
            tglRegisterLabel.Location = new System.Drawing.Point(40, 168);
            tglRegisterLabel.Name = "tglRegisterLabel";
            tglRegisterLabel.Size = new System.Drawing.Size(68, 13);
            tglRegisterLabel.TabIndex = 10;
            tglRegisterLabel.Text = "Tgl Register:";
            // 
            // kotaLabel1
            // 
            kotaLabel1.AutoSize = true;
            kotaLabel1.Location = new System.Drawing.Point(74, 117);
            kotaLabel1.Name = "kotaLabel1";
            kotaLabel1.Size = new System.Drawing.Size(33, 13);
            kotaLabel1.TabIndex = 20;
            kotaLabel1.Text = "Kota:";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.LineColor = System.Drawing.SystemColors.ControlText;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(386, 18);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Pelanggan Baru";
            // 
            // pelangganBindingSource
            // 
            this.pelangganBindingSource.DataSource = typeof(Examples01.Entity.Pelanggan);
            // 
            // noPelangganTextEdit
            // 
            this.noPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "NoPelanggan", true));
            this.noPelangganTextEdit.Location = new System.Drawing.Point(113, 36);
            this.noPelangganTextEdit.Name = "noPelangganTextEdit";
            this.noPelangganTextEdit.Size = new System.Drawing.Size(100, 20);
            this.noPelangganTextEdit.TabIndex = 12;
            // 
            // namaPelangganTextEdit
            // 
            this.namaPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "NamaPelanggan", true));
            this.namaPelangganTextEdit.Location = new System.Drawing.Point(113, 62);
            this.namaPelangganTextEdit.Name = "namaPelangganTextEdit";
            this.namaPelangganTextEdit.Size = new System.Drawing.Size(144, 20);
            this.namaPelangganTextEdit.TabIndex = 0;
            // 
            // alamatTextEdit
            // 
            this.alamatTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Alamat", true));
            this.alamatTextEdit.Location = new System.Drawing.Point(113, 88);
            this.alamatTextEdit.Name = "alamatTextEdit";
            this.alamatTextEdit.Size = new System.Drawing.Size(144, 20);
            this.alamatTextEdit.TabIndex = 1;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(113, 140);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 3;
            // 
            // tglRegisterDateEdit
            // 
            this.tglRegisterDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "TglRegister", true));
            this.tglRegisterDateEdit.EditValue = null;
            this.tglRegisterDateEdit.Location = new System.Drawing.Point(113, 165);
            this.tglRegisterDateEdit.Name = "tglRegisterDateEdit";
            this.tglRegisterDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglRegisterDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglRegisterDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglRegisterDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglRegisterDateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglRegisterDateEdit.TabIndex = 4;
            // 
            // photoPictureEdit
            // 
            this.photoPictureEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.photoPictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Photo", true));
            this.photoPictureEdit.Location = new System.Drawing.Point(273, 36);
            this.photoPictureEdit.Name = "photoPictureEdit";
            this.photoPictureEdit.Size = new System.Drawing.Size(121, 149);
            this.photoPictureEdit.TabIndex = 5;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(12, 198);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(59, 23);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "&Baru";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton2.Location = new System.Drawing.Point(77, 198);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(30, 23);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "|<";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton3.Location = new System.Drawing.Point(113, 198);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(30, 23);
            this.simpleButton3.TabIndex = 8;
            this.simpleButton3.Text = "<<";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton4.Location = new System.Drawing.Point(149, 198);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(30, 23);
            this.simpleButton4.TabIndex = 9;
            this.simpleButton4.Text = ">>";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton5.Location = new System.Drawing.Point(183, 198);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(30, 23);
            this.simpleButton5.TabIndex = 10;
            this.simpleButton5.Text = ">|";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton6.Location = new System.Drawing.Point(319, 198);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(75, 23);
            this.simpleButton6.TabIndex = 11;
            this.simpleButton6.Text = "&Simpan";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton7.Location = new System.Drawing.Point(218, 198);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(55, 23);
            this.simpleButton7.TabIndex = 13;
            this.simpleButton7.Text = "&Hapus";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton8.Location = new System.Drawing.Point(342, 7);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(56, 23);
            this.simpleButton8.TabIndex = 14;
            this.simpleButton8.Text = "&Reload";
            this.simpleButton8.Click += new System.EventHandler(this.simpleButton8_Click);
            // 
            // kotaComboBoxEdit
            // 
            this.kotaComboBoxEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelangganBindingSource, "Kota", true));
            this.kotaComboBoxEdit.Location = new System.Drawing.Point(113, 114);
            this.kotaComboBoxEdit.Name = "kotaComboBoxEdit";
            this.kotaComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.kotaComboBoxEdit.Properties.ImmediatePopup = true;
            this.kotaComboBoxEdit.Properties.Items.AddRange(new object[] {
            "Surabaya",
            "Malang",
            "Jember",
            "Mojokerto"});
            this.kotaComboBoxEdit.Size = new System.Drawing.Size(100, 20);
            this.kotaComboBoxEdit.TabIndex = 21;
            // 
            // colNoPelanggan
            // 
            this.colNoPelanggan.Caption = "NoPelanggan";
            this.colNoPelanggan.FieldName = "NoPelanggan";
            this.colNoPelanggan.Name = "colNoPelanggan";
            this.colNoPelanggan.Visible = true;
            this.colNoPelanggan.VisibleIndex = 0;
            // 
            // colNamaPelanggan
            // 
            this.colNamaPelanggan.Caption = "NamaPelanggan";
            this.colNamaPelanggan.FieldName = "NamaPelanggan";
            this.colNamaPelanggan.Name = "colNamaPelanggan";
            this.colNamaPelanggan.Visible = true;
            this.colNamaPelanggan.VisibleIndex = 1;
            // 
            // colAlamat
            // 
            this.colAlamat.Caption = "Alamat";
            this.colAlamat.FieldName = "Alamat";
            this.colAlamat.Name = "colAlamat";
            this.colAlamat.Visible = true;
            this.colAlamat.VisibleIndex = 2;
            // 
            // colKota
            // 
            this.colKota.Caption = "Kota";
            this.colKota.FieldName = "Kota";
            this.colKota.Name = "colKota";
            this.colKota.Visible = true;
            this.colKota.VisibleIndex = 3;
            // 
            // colTglRegister
            // 
            this.colTglRegister.Caption = "TglRegister";
            this.colTglRegister.FieldName = "TglRegister";
            this.colTglRegister.Name = "colTglRegister";
            this.colTglRegister.Visible = true;
            this.colTglRegister.VisibleIndex = 4;
            // 
            // colAktif
            // 
            this.colAktif.Caption = "Aktif";
            this.colAktif.FieldName = "Aktif";
            this.colAktif.Name = "colAktif";
            this.colAktif.Visible = true;
            this.colAktif.VisibleIndex = 5;
            // 
            // colPhoto
            // 
            this.colPhoto.Caption = "Photo";
            this.colPhoto.FieldName = "Photo";
            this.colPhoto.Name = "colPhoto";
            this.colPhoto.Visible = true;
            this.colPhoto.VisibleIndex = 6;
            // 
            // colTglJamUpdate
            // 
            this.colTglJamUpdate.Caption = "TglJamUpdate";
            this.colTglJamUpdate.FieldName = "TglJamUpdate";
            this.colTglJamUpdate.Name = "colTglJamUpdate";
            this.colTglJamUpdate.Visible = true;
            this.colTglJamUpdate.VisibleIndex = 7;
            // 
            // frmPelanggan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 233);
            this.Controls.Add(kotaLabel1);
            this.Controls.Add(this.kotaComboBoxEdit);
            this.Controls.Add(this.simpleButton8);
            this.Controls.Add(this.simpleButton7);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.photoPictureEdit);
            this.Controls.Add(tglRegisterLabel);
            this.Controls.Add(this.tglRegisterDateEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(alamatLabel);
            this.Controls.Add(this.alamatTextEdit);
            this.Controls.Add(namaPelangganLabel);
            this.Controls.Add(this.namaPelangganTextEdit);
            this.Controls.Add(noPelangganLabel);
            this.Controls.Add(this.noPelangganTextEdit);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmPelanggan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Pelanggan";
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglRegisterDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kotaComboBoxEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.BindingSource pelangganBindingSource;
        private DevExpress.XtraEditors.TextEdit noPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit namaPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit alamatTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.DateEdit tglRegisterDateEdit;
        private DevExpress.XtraEditors.PictureEdit photoPictureEdit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private DevExpress.XtraEditors.ComboBoxEdit kotaComboBoxEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colNoPelanggan;
        private DevExpress.XtraGrid.Columns.GridColumn colNamaPelanggan;
        private DevExpress.XtraGrid.Columns.GridColumn colAlamat;
        private DevExpress.XtraGrid.Columns.GridColumn colKota;
        private DevExpress.XtraGrid.Columns.GridColumn colTglRegister;
        private DevExpress.XtraGrid.Columns.GridColumn colAktif;
        private DevExpress.XtraGrid.Columns.GridColumn colPhoto;
        private DevExpress.XtraGrid.Columns.GridColumn colTglJamUpdate;
    }
}

