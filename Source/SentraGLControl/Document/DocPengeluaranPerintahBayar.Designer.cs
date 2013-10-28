using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Document
{
    partial class DocPengeluaranPerintahBayar
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
            Label jenisTransaksiLabel;
            Label tglPengeluaranLabel;
            Label catatanLabel;
            Label tglKliringLabel;
            Label noCekGiroLabel;
            Label keperluanLabel;
            Label terimaDariLabel;
            Label noKuitansiLabel;
            Label idAkunLabel;
            Label noPerintahBayarLabel;
            Label totalNilaiLabel;
            Label statusLabel;
            Label tglPerintahBayarLabel;
            this.statusTransaksiTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.listKasBindingSource = new BindingSource(this.components);
            this.tglKliringDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.noCekGiroTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.mataUangLabel1 = new Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.keperluanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.terimaDariTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.noKuitansiTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.catatanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.tglPengeluaranDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.idAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.noPerintahBayarButtonEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.totalNilaiSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.statusTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.tglPerintahBayarDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.PerintahBayarBindingSource = new BindingSource(this.components);
            jenisTransaksiLabel = new Label();
            tglPengeluaranLabel = new Label();
            catatanLabel = new Label();
            tglKliringLabel = new Label();
            noCekGiroLabel = new Label();
            keperluanLabel = new Label();
            terimaDariLabel = new Label();
            noKuitansiLabel = new Label();
            idAkunLabel = new Label();
            noPerintahBayarLabel = new Label();
            totalNilaiLabel = new Label();
            statusLabel = new Label();
            tglPerintahBayarLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.statusTransaksiTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglKliringDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglKliringDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noCekGiroTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keperluanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.terimaDariTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noKuitansiTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.catatanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPerintahBayarButtonEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalNilaiSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPerintahBayarDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPerintahBayarDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // jenisTransaksiLabel
            // 
            jenisTransaksiLabel.AutoSize = true;
            jenisTransaksiLabel.Location = new System.Drawing.Point(31, 171);
            jenisTransaksiLabel.Name = "jenisTransaksiLabel";
            jenisTransaksiLabel.Size = new System.Drawing.Size(83, 13);
            jenisTransaksiLabel.TabIndex = 63;
            jenisTransaksiLabel.Text = "Jenis Transaksi:";
            // 
            // tglPengeluaranLabel
            // 
            tglPengeluaranLabel.AutoSize = true;
            tglPengeluaranLabel.Location = new System.Drawing.Point(26, 93);
            tglPengeluaranLabel.Name = "tglPengeluaranLabel";
            tglPengeluaranLabel.Size = new System.Drawing.Size(88, 13);
            tglPengeluaranLabel.TabIndex = 61;
            tglPengeluaranLabel.Text = "Tgl Pengeluaran:";
            // 
            // catatanLabel
            // 
            catatanLabel.AutoSize = true;
            catatanLabel.Location = new System.Drawing.Point(64, 145);
            catatanLabel.Name = "catatanLabel";
            catatanLabel.Size = new System.Drawing.Size(50, 13);
            catatanLabel.TabIndex = 60;
            catatanLabel.Text = "Catatan:";
            // 
            // tglKliringLabel
            // 
            tglKliringLabel.AutoSize = true;
            tglKliringLabel.Location = new System.Drawing.Point(187, 10);
            tglKliringLabel.Name = "tglKliringLabel";
            tglKliringLabel.Size = new System.Drawing.Size(56, 13);
            tglKliringLabel.TabIndex = 22;
            tglKliringLabel.Text = "Tgl Kliring:";
            // 
            // noCekGiroLabel
            // 
            noCekGiroLabel.AutoSize = true;
            noCekGiroLabel.Location = new System.Drawing.Point(5, 10);
            noCekGiroLabel.Name = "noCekGiroLabel";
            noCekGiroLabel.Size = new System.Drawing.Size(71, 13);
            noCekGiroLabel.TabIndex = 0;
            noCekGiroLabel.Text = "No Cek/ Giro:";
            // 
            // keperluanLabel
            // 
            keperluanLabel.AutoSize = true;
            keperluanLabel.Location = new System.Drawing.Point(368, 67);
            keperluanLabel.Name = "keperluanLabel";
            keperluanLabel.Size = new System.Drawing.Size(59, 13);
            keperluanLabel.TabIndex = 57;
            keperluanLabel.Text = "Keperluan:";
            // 
            // terimaDariLabel
            // 
            terimaDariLabel.AutoSize = true;
            terimaDariLabel.Location = new System.Drawing.Point(342, 41);
            terimaDariLabel.Name = "terimaDariLabel";
            terimaDariLabel.Size = new System.Drawing.Size(85, 13);
            terimaDariLabel.TabIndex = 55;
            terimaDariLabel.Text = "Nama Penerima:";
            // 
            // noKuitansiLabel
            // 
            noKuitansiLabel.AutoSize = true;
            noKuitansiLabel.Location = new System.Drawing.Point(363, 15);
            noKuitansiLabel.Name = "noKuitansiLabel";
            noKuitansiLabel.Size = new System.Drawing.Size(64, 13);
            noKuitansiLabel.TabIndex = 51;
            noKuitansiLabel.Text = "No Kuitansi:";
            // 
            // idAkunLabel
            // 
            idAkunLabel.AutoSize = true;
            idAkunLabel.Location = new System.Drawing.Point(56, 41);
            idAkunLabel.Name = "idAkunLabel";
            idAkunLabel.Size = new System.Drawing.Size(58, 13);
            idAkunLabel.TabIndex = 48;
            idAkunLabel.Text = "Nama Kas:";
            // 
            // noPerintahBayarLabel
            // 
            noPerintahBayarLabel.AutoSize = true;
            noPerintahBayarLabel.Location = new System.Drawing.Point(7, 15);
            noPerintahBayarLabel.Name = "noPerintahBayarLabel";
            noPerintahBayarLabel.Size = new System.Drawing.Size(98, 13);
            noPerintahBayarLabel.TabIndex = 47;
            noPerintahBayarLabel.Text = "No Perintah Bayar:";
            // 
            // totalNilaiLabel
            // 
            totalNilaiLabel.AutoSize = true;
            totalNilaiLabel.Location = new System.Drawing.Point(57, 119);
            totalNilaiLabel.Name = "totalNilaiLabel";
            totalNilaiLabel.Size = new System.Drawing.Size(57, 13);
            totalNilaiLabel.TabIndex = 66;
            totalNilaiLabel.Text = "Total Nilai:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(385, 93);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(42, 13);
            statusLabel.TabIndex = 66;
            statusLabel.Text = "Status:";
            // 
            // tglPerintahBayarLabel
            // 
            tglPerintahBayarLabel.AutoSize = true;
            tglPerintahBayarLabel.Location = new System.Drawing.Point(15, 67);
            tglPerintahBayarLabel.Name = "tglPerintahBayarLabel";
            tglPerintahBayarLabel.Size = new System.Drawing.Size(99, 13);
            tglPerintahBayarLabel.TabIndex = 67;
            tglPerintahBayarLabel.Text = "Tgl Perintah Bayar:";
            // 
            // statusTransaksiTextEdit
            // 
            this.statusTransaksiTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "StatusTransaksi", true));
            this.statusTransaksiTextEdit.Location = new System.Drawing.Point(120, 194);
            this.statusTransaksiTextEdit.Name = "statusTransaksiTextEdit";
            this.statusTransaksiTextEdit.Size = new System.Drawing.Size(112, 20);
            this.statusTransaksiTextEdit.TabIndex = 9;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "JenisTransaksi", true, DataSourceUpdateMode.OnPropertyChanged));
            this.comboBoxEdit1.Location = new System.Drawing.Point(120, 168);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(112, 20);
            this.comboBoxEdit1.TabIndex = 8;
            // 
            // listKasBindingSource
            // 
            this.listKasBindingSource.DataMember = "ListKas";
            this.listKasBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // tglKliringDateEdit
            // 
            this.tglKliringDateEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "TglKliring", true));
            this.tglKliringDateEdit.EditValue = null;
            this.tglKliringDateEdit.Location = new System.Drawing.Point(249, 7);
            this.tglKliringDateEdit.Name = "tglKliringDateEdit";
            this.tglKliringDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglKliringDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglKliringDateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglKliringDateEdit.TabIndex = 2;
            // 
            // noCekGiroTextEdit
            // 
            this.noCekGiroTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NoCekGiro", true));
            this.noCekGiroTextEdit.Location = new System.Drawing.Point(78, 7);
            this.noCekGiroTextEdit.Name = "noCekGiroTextEdit";
            this.noCekGiroTextEdit.Size = new System.Drawing.Size(100, 20);
            this.noCekGiroTextEdit.TabIndex = 1;
            // 
            // mataUangLabel1
            // 
            this.mataUangLabel1.DataBindings.Add(new Binding("Text", this.PerintahBayarBindingSource, "MataUang", true));
            this.mataUangLabel1.Location = new System.Drawing.Point(238, 119);
            this.mataUangLabel1.Name = "mataUangLabel1";
            this.mataUangLabel1.Size = new System.Drawing.Size(52, 19);
            this.mataUangLabel1.TabIndex = 64;
            this.mataUangLabel1.Text = "xx";
            this.mataUangLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(tglKliringLabel);
            this.panelControl1.Controls.Add(this.tglKliringDateEdit);
            this.panelControl1.Controls.Add(this.noCekGiroTextEdit);
            this.panelControl1.Controls.Add(noCekGiroLabel);
            this.panelControl1.DataBindings.Add(new Binding("Visible", this.PerintahBayarBindingSource, "TampilkanCekGiro", true, DataSourceUpdateMode.OnPropertyChanged));
            this.panelControl1.Location = new System.Drawing.Point(241, 161);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(359, 35);
            this.panelControl1.TabIndex = 56;
            // 
            // keperluanTextEdit
            // 
            this.keperluanTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Keperluan", true));
            this.keperluanTextEdit.Location = new System.Drawing.Point(433, 64);
            this.keperluanTextEdit.Name = "keperluanTextEdit";
            this.keperluanTextEdit.Size = new System.Drawing.Size(157, 20);
            this.keperluanTextEdit.TabIndex = 6;
            this.keperluanTextEdit.TabStop = false;
            // 
            // terimaDariTextEdit
            // 
            this.terimaDariTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NamaPenerima", true));
            this.terimaDariTextEdit.Location = new System.Drawing.Point(433, 38);
            this.terimaDariTextEdit.Name = "terimaDariTextEdit";
            this.terimaDariTextEdit.Size = new System.Drawing.Size(157, 20);
            this.terimaDariTextEdit.TabIndex = 5;
            this.terimaDariTextEdit.TabStop = false;
            // 
            // noKuitansiTextEdit
            // 
            this.noKuitansiTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NoKuitansi", true));
            this.noKuitansiTextEdit.Location = new System.Drawing.Point(433, 12);
            this.noKuitansiTextEdit.Name = "noKuitansiTextEdit";
            this.noKuitansiTextEdit.Size = new System.Drawing.Size(157, 20);
            this.noKuitansiTextEdit.TabIndex = 4;
            // 
            // catatanTextEdit
            // 
            this.catatanTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Catatan", true));
            this.catatanTextEdit.Location = new System.Drawing.Point(120, 142);
            this.catatanTextEdit.Name = "catatanTextEdit";
            this.catatanTextEdit.Size = new System.Drawing.Size(470, 20);
            this.catatanTextEdit.TabIndex = 7;
            // 
            // tglPengeluaranDateEdit
            // 
            this.tglPengeluaranDateEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "TglPengeluaran", true));
            this.tglPengeluaranDateEdit.EditValue = null;
            this.tglPengeluaranDateEdit.Location = new System.Drawing.Point(120, 90);
            this.tglPengeluaranDateEdit.Name = "tglPengeluaranDateEdit";
            this.tglPengeluaranDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglPengeluaranDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglPengeluaranDateEdit.Size = new System.Drawing.Size(112, 20);
            this.tglPengeluaranDateEdit.TabIndex = 2;
            // 
            // idAkunLookUpEdit
            // 
            this.idAkunLookUpEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "IdKas", true));
            this.idAkunLookUpEdit.Location = new System.Drawing.Point(120, 38);
            this.idAkunLookUpEdit.Name = "idAkunLookUpEdit";
            this.idAkunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idAkunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Kas", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Kas", 70)});
            this.idAkunLookUpEdit.Properties.DataSource = this.listKasBindingSource;
            this.idAkunLookUpEdit.Properties.DisplayMember = "NamaAkun";
            this.idAkunLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idAkunLookUpEdit.Size = new System.Drawing.Size(170, 20);
            this.idAkunLookUpEdit.TabIndex = 1;
            this.idAkunLookUpEdit.TabStop = false;
            // 
            // noPerintahBayarButtonEdit
            // 
            this.noPerintahBayarButtonEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NoPerintahBayar", true));
            this.noPerintahBayarButtonEdit.Location = new System.Drawing.Point(120, 12);
            this.noPerintahBayarButtonEdit.Name = "noPerintahBayarButtonEdit";
            this.noPerintahBayarButtonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut(Keys.None), "Pilih Perintah Bayar", "SkipLock")});
            this.noPerintahBayarButtonEdit.Size = new System.Drawing.Size(170, 20);
            this.noPerintahBayarButtonEdit.TabIndex = 0;
            this.noPerintahBayarButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.noPerintahBayarButtonEdit_ButtonClick);
            // 
            // totalNilaiSpinEdit
            // 
            this.totalNilaiSpinEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "TotalNilai", true));
            this.totalNilaiSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.totalNilaiSpinEdit.Location = new System.Drawing.Point(120, 116);
            this.totalNilaiSpinEdit.Name = "totalNilaiSpinEdit";
            this.totalNilaiSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.totalNilaiSpinEdit.Size = new System.Drawing.Size(112, 20);
            this.totalNilaiSpinEdit.TabIndex = 3;
            this.totalNilaiSpinEdit.TabStop = false;
            // 
            // statusTextEdit
            // 
            this.statusTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Status", true));
            this.statusTextEdit.Location = new System.Drawing.Point(433, 90);
            this.statusTextEdit.Name = "statusTextEdit";
            this.statusTextEdit.Size = new System.Drawing.Size(100, 20);
            this.statusTextEdit.TabIndex = 67;
            this.statusTextEdit.TabStop = false;
            // 
            // tglPerintahBayarDateEdit
            // 
            this.tglPerintahBayarDateEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "TglPerintahBayar", true));
            this.tglPerintahBayarDateEdit.EditValue = null;
            this.tglPerintahBayarDateEdit.Location = new System.Drawing.Point(120, 64);
            this.tglPerintahBayarDateEdit.Name = "tglPerintahBayarDateEdit";
            this.tglPerintahBayarDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglPerintahBayarDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglPerintahBayarDateEdit.Size = new System.Drawing.Size(112, 20);
            this.tglPerintahBayarDateEdit.TabIndex = 68;
            this.tglPerintahBayarDateEdit.TabStop = false;
            // 
            // PerintahBayarBindingSource
            // 
            this.PerintahBayarBindingSource.DataSource = typeof(SentraGL.Document.PerintahBayar);
            // 
            // DocPengeluaranPerintahBayar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(621, 226);
            this.Controls.Add(tglPerintahBayarLabel);
            this.Controls.Add(this.tglPerintahBayarDateEdit);
            this.Controls.Add(statusLabel);
            this.Controls.Add(this.statusTextEdit);
            this.Controls.Add(totalNilaiLabel);
            this.Controls.Add(this.totalNilaiSpinEdit);
            this.Controls.Add(this.noPerintahBayarButtonEdit);
            this.Controls.Add(this.statusTransaksiTextEdit);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(jenisTransaksiLabel);
            this.Controls.Add(this.mataUangLabel1);
            this.Controls.Add(tglPengeluaranLabel);
            this.Controls.Add(catatanLabel);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.keperluanTextEdit);
            this.Controls.Add(this.terimaDariTextEdit);
            this.Controls.Add(this.noKuitansiTextEdit);
            this.Controls.Add(this.catatanTextEdit);
            this.Controls.Add(this.tglPengeluaranDateEdit);
            this.Controls.Add(this.idAkunLookUpEdit);
            this.Controls.Add(keperluanLabel);
            this.Controls.Add(terimaDariLabel);
            this.Controls.Add(noKuitansiLabel);
            this.Controls.Add(idAkunLabel);
            this.Controls.Add(noPerintahBayarLabel);
            this.Name = "DocPengeluaranPerintahBayar";
            ((System.ComponentModel.ISupportInitialize)(this.statusTransaksiTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglKliringDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglKliringDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noCekGiroTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keperluanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.terimaDariTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noKuitansiTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.catatanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPerintahBayarButtonEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalNilaiSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPerintahBayarDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPerintahBayarDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource PerintahBayarBindingSource;
        private DevExpress.XtraEditors.TextEdit statusTransaksiTextEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private System.Windows.Forms.BindingSource listKasBindingSource;
        private DevExpress.XtraEditors.DateEdit tglKliringDateEdit;
        private DevExpress.XtraEditors.TextEdit noCekGiroTextEdit;
        private System.Windows.Forms.Label mataUangLabel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit keperluanTextEdit;
        private DevExpress.XtraEditors.TextEdit terimaDariTextEdit;
        private DevExpress.XtraEditors.TextEdit noKuitansiTextEdit;
        private DevExpress.XtraEditors.TextEdit catatanTextEdit;
        private DevExpress.XtraEditors.DateEdit tglPengeluaranDateEdit;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit;
        private DevExpress.XtraEditors.ButtonEdit noPerintahBayarButtonEdit;
        private DevExpress.XtraEditors.SpinEdit totalNilaiSpinEdit;
        private DevExpress.XtraEditors.TextEdit statusTextEdit;
        private DevExpress.XtraEditors.DateEdit tglPerintahBayarDateEdit;
    }
}
