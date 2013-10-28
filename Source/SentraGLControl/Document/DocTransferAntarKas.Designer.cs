using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Document
{
    partial class DocTransferAntarKas
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
            Label idKasAsalLabel;
            Label noTransferLabel;
            Label tglTransferLabel;
            Label idKasTujuanLabel;
            Label nilaiTransferLabel;
            Label keteranganLabel;
            this.idKasAsalLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.transferAntarKasBindingSource = new BindingSource(this.components);
            this.listKasBindingSource = new BindingSource(this.components);
            this.noTransferTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.tglTransferDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.idKasAsalLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.idKasTujuanLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listKasBindingSource1 = new BindingSource(this.components);
            this.idKasTujuanLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.nilaiTransferSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            idKasAsalLabel = new Label();
            noTransferLabel = new Label();
            tglTransferLabel = new Label();
            idKasTujuanLabel = new Label();
            nilaiTransferLabel = new Label();
            keteranganLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.idKasAsalLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferAntarKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noTransferTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglTransferDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglTransferDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasAsalLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasTujuanLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasTujuanLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTransferSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // idKasAsalLabel
            // 
            idKasAsalLabel.AutoSize = true;
            idKasAsalLabel.Location = new System.Drawing.Point(28, 69);
            idKasAsalLabel.Name = "idKasAsalLabel";
            idKasAsalLabel.Size = new System.Drawing.Size(51, 13);
            idKasAsalLabel.TabIndex = 1;
            idKasAsalLabel.Text = "Kas Asal:";
            // 
            // noTransferLabel
            // 
            noTransferLabel.AutoSize = true;
            noTransferLabel.Location = new System.Drawing.Point(11, 15);
            noTransferLabel.Name = "noTransferLabel";
            noTransferLabel.Size = new System.Drawing.Size(68, 13);
            noTransferLabel.TabIndex = 2;
            noTransferLabel.Text = "No Transfer:";
            // 
            // tglTransferLabel
            // 
            tglTransferLabel.AutoSize = true;
            tglTransferLabel.Location = new System.Drawing.Point(10, 43);
            tglTransferLabel.Name = "tglTransferLabel";
            tglTransferLabel.Size = new System.Drawing.Size(69, 13);
            tglTransferLabel.TabIndex = 4;
            tglTransferLabel.Text = "Tgl Transfer:";
            // 
            // idKasTujuanLabel
            // 
            idKasTujuanLabel.AutoSize = true;
            idKasTujuanLabel.Location = new System.Drawing.Point(15, 95);
            idKasTujuanLabel.Name = "idKasTujuanLabel";
            idKasTujuanLabel.Size = new System.Drawing.Size(64, 13);
            idKasTujuanLabel.TabIndex = 7;
            idKasTujuanLabel.Text = "Kas Tujuan:";
            // 
            // nilaiTransferLabel
            // 
            nilaiTransferLabel.AutoSize = true;
            nilaiTransferLabel.Location = new System.Drawing.Point(5, 121);
            nilaiTransferLabel.Name = "nilaiTransferLabel";
            nilaiTransferLabel.Size = new System.Drawing.Size(74, 13);
            nilaiTransferLabel.TabIndex = 10;
            nilaiTransferLabel.Text = "Nilai Transfer:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(12, 147);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 12;
            keteranganLabel.Text = "Keterangan:";
            // 
            // idKasAsalLookUpEdit
            // 
            this.idKasAsalLookUpEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "IdKasAsal", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idKasAsalLookUpEdit.Location = new System.Drawing.Point(85, 66);
            this.idKasAsalLookUpEdit.Name = "idKasAsalLookUpEdit";
            this.idKasAsalLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idKasAsalLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Kas", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Kas", 70)});
            this.idKasAsalLookUpEdit.Properties.DataSource = this.listKasBindingSource;
            this.idKasAsalLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idKasAsalLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idKasAsalLookUpEdit.Size = new System.Drawing.Size(135, 20);
            this.idKasAsalLookUpEdit.TabIndex = 2;
            // 
            // transferAntarKasBindingSource
            // 
            this.transferAntarKasBindingSource.DataSource = typeof(SentraGL.Document.TransferAntarKas);
            // 
            // listKasBindingSource
            // 
            this.listKasBindingSource.DataMember = "ListKas";
            this.listKasBindingSource.DataSource = this.transferAntarKasBindingSource;
            // 
            // noTransferTextEdit
            // 
            this.noTransferTextEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "NoTransfer", true));
            this.noTransferTextEdit.Location = new System.Drawing.Point(85, 12);
            this.noTransferTextEdit.Name = "noTransferTextEdit";
            this.noTransferTextEdit.Size = new System.Drawing.Size(135, 20);
            this.noTransferTextEdit.TabIndex = 3;
            // 
            // tglTransferDateEdit
            // 
            this.tglTransferDateEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "TglTransfer", true));
            this.tglTransferDateEdit.EditValue = null;
            this.tglTransferDateEdit.Location = new System.Drawing.Point(85, 40);
            this.tglTransferDateEdit.Name = "tglTransferDateEdit";
            this.tglTransferDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglTransferDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglTransferDateEdit.Size = new System.Drawing.Size(135, 20);
            this.tglTransferDateEdit.TabIndex = 5;
            // 
            // idKasAsalLookUpEdit1
            // 
            this.idKasAsalLookUpEdit1.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "IdKasAsal", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idKasAsalLookUpEdit1.Location = new System.Drawing.Point(225, 66);
            this.idKasAsalLookUpEdit1.Name = "idKasAsalLookUpEdit1";
            this.idKasAsalLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idKasAsalLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Kas", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Kas", 70)});
            this.idKasAsalLookUpEdit1.Properties.DataSource = this.listKasBindingSource;
            this.idKasAsalLookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.idKasAsalLookUpEdit1.Properties.ValueMember = "IdAkun";
            this.idKasAsalLookUpEdit1.Size = new System.Drawing.Size(189, 20);
            this.idKasAsalLookUpEdit1.TabIndex = 7;
            // 
            // idKasTujuanLookUpEdit
            // 
            this.idKasTujuanLookUpEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "IdKasTujuan", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idKasTujuanLookUpEdit.Location = new System.Drawing.Point(85, 92);
            this.idKasTujuanLookUpEdit.Name = "idKasTujuanLookUpEdit";
            this.idKasTujuanLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idKasTujuanLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Kas", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Kas", 70)});
            this.idKasTujuanLookUpEdit.Properties.DataSource = this.listKasBindingSource1;
            this.idKasTujuanLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idKasTujuanLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idKasTujuanLookUpEdit.Size = new System.Drawing.Size(135, 20);
            this.idKasTujuanLookUpEdit.TabIndex = 8;
            // 
            // listKasBindingSource1
            // 
            this.listKasBindingSource1.DataMember = "ListKas";
            this.listKasBindingSource1.DataSource = this.transferAntarKasBindingSource;
            // 
            // idKasTujuanLookUpEdit1
            // 
            this.idKasTujuanLookUpEdit1.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "IdKasTujuan", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idKasTujuanLookUpEdit1.Location = new System.Drawing.Point(225, 92);
            this.idKasTujuanLookUpEdit1.Name = "idKasTujuanLookUpEdit1";
            this.idKasTujuanLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idKasTujuanLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Kas", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Kas", 70)});
            this.idKasTujuanLookUpEdit1.Properties.DataSource = this.listKasBindingSource1;
            this.idKasTujuanLookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.idKasTujuanLookUpEdit1.Properties.ValueMember = "IdAkun";
            this.idKasTujuanLookUpEdit1.Size = new System.Drawing.Size(189, 20);
            this.idKasTujuanLookUpEdit1.TabIndex = 10;
            // 
            // nilaiTransferSpinEdit
            // 
            this.nilaiTransferSpinEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "NilaiKasAsal", true));
            this.nilaiTransferSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nilaiTransferSpinEdit.Location = new System.Drawing.Point(85, 118);
            this.nilaiTransferSpinEdit.Name = "nilaiTransferSpinEdit";
            this.nilaiTransferSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nilaiTransferSpinEdit.Size = new System.Drawing.Size(135, 20);
            this.nilaiTransferSpinEdit.TabIndex = 11;
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new Binding("EditValue", this.transferAntarKasBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(85, 144);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(329, 20);
            this.keteranganTextEdit.TabIndex = 13;
            // 
            // DocTransferAntarKas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(489, 174);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganTextEdit);
            this.Controls.Add(nilaiTransferLabel);
            this.Controls.Add(this.nilaiTransferSpinEdit);
            this.Controls.Add(this.idKasTujuanLookUpEdit1);
            this.Controls.Add(idKasTujuanLabel);
            this.Controls.Add(this.idKasTujuanLookUpEdit);
            this.Controls.Add(this.idKasAsalLookUpEdit1);
            this.Controls.Add(tglTransferLabel);
            this.Controls.Add(this.tglTransferDateEdit);
            this.Controls.Add(noTransferLabel);
            this.Controls.Add(this.noTransferTextEdit);
            this.Controls.Add(idKasAsalLabel);
            this.Controls.Add(this.idKasAsalLookUpEdit);
            this.Name = "DocTransferAntarKas";
            ((System.ComponentModel.ISupportInitialize)(this.idKasAsalLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transferAntarKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noTransferTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglTransferDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglTransferDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasAsalLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasTujuanLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idKasTujuanLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTransferSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource transferAntarKasBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idKasAsalLookUpEdit;
        private DevExpress.XtraEditors.TextEdit noTransferTextEdit;
        private DevExpress.XtraEditors.DateEdit tglTransferDateEdit;
        private DevExpress.XtraEditors.LookUpEdit idKasAsalLookUpEdit1;
        private DevExpress.XtraEditors.LookUpEdit idKasTujuanLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit idKasTujuanLookUpEdit1;
        private DevExpress.XtraEditors.SpinEdit nilaiTransferSpinEdit;
        private System.Windows.Forms.BindingSource listKasBindingSource;
        private System.Windows.Forms.BindingSource listKasBindingSource1;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
    }
}
