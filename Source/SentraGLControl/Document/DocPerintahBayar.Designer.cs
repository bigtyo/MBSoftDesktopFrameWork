using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Document
{
    partial class DocPerintahBayar
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
            Label tglPengeluaranLabel;
            Label catatanLabel;
            Label keperluanLabel;
            Label terimaDariLabel;
            Label idAkunLabel;
            Label noPerintahBayarLabel;
            Label statusLabel;
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdDepartemen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listDepartemenBindingSource = new BindingSource(this.components);
            this.PerintahBayarBindingSource = new BindingSource(this.components);
            this.colIdProyek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listProyekBindingSource = new BindingSource(this.components);
            this.colJenisPengeluaran = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listJenisPengeluaranBindingSource = new BindingSource(this.components);
            this.colNilaiPengeluaran = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeterangan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PerintahBayarDetilGridControl = new DevExpress.XtraGrid.GridControl();
            this.PerintahBayarDetilBindingSource = new BindingSource(this.components);
            this.listKasBindingSource = new BindingSource(this.components);
            this.internalCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.mataUangLabel1 = new Label();
            this.gridRowMover21 = new SentraWinFramework.GridRowMover2();
            this.keperluanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.terimaDariTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.catatanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.tglPengeluaranDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.idAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.noPerintahBayarTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.statusTextEdit = new DevExpress.XtraEditors.TextEdit();
            tglPengeluaranLabel = new Label();
            catatanLabel = new Label();
            keperluanLabel = new Label();
            terimaDariLabel = new Label();
            idAkunLabel = new Label();
            noPerintahBayarLabel = new Label();
            statusLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listJenisPengeluaranBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarDetilGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarDetilBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keperluanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.terimaDariTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.catatanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPerintahBayarTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTextEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tglPengeluaranLabel
            // 
            tglPengeluaranLabel.AutoSize = true;
            tglPengeluaranLabel.Location = new System.Drawing.Point(58, 67);
            tglPengeluaranLabel.Name = "tglPengeluaranLabel";
            tglPengeluaranLabel.Size = new System.Drawing.Size(56, 13);
            tglPengeluaranLabel.TabIndex = 61;
            tglPengeluaranLabel.Text = "Tgl Bayar:";
            // 
            // catatanLabel
            // 
            catatanLabel.AutoSize = true;
            catatanLabel.Location = new System.Drawing.Point(64, 93);
            catatanLabel.Name = "catatanLabel";
            catatanLabel.Size = new System.Drawing.Size(50, 13);
            catatanLabel.TabIndex = 60;
            catatanLabel.Text = "Catatan:";
            // 
            // keperluanLabel
            // 
            keperluanLabel.AutoSize = true;
            keperluanLabel.Location = new System.Drawing.Point(353, 41);
            keperluanLabel.Name = "keperluanLabel";
            keperluanLabel.Size = new System.Drawing.Size(59, 13);
            keperluanLabel.TabIndex = 57;
            keperluanLabel.Text = "Keperluan:";
            // 
            // terimaDariLabel
            // 
            terimaDariLabel.AutoSize = true;
            terimaDariLabel.Location = new System.Drawing.Point(327, 15);
            terimaDariLabel.Name = "terimaDariLabel";
            terimaDariLabel.Size = new System.Drawing.Size(85, 13);
            terimaDariLabel.TabIndex = 55;
            terimaDariLabel.Text = "Nama Penerima:";
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
            noPerintahBayarLabel.Location = new System.Drawing.Point(16, 14);
            noPerintahBayarLabel.Name = "noPerintahBayarLabel";
            noPerintahBayarLabel.Size = new System.Drawing.Size(98, 13);
            noPerintahBayarLabel.TabIndex = 47;
            noPerintahBayarLabel.Text = "No Perintah Bayar:";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdDepartemen,
            this.colIdProyek,
            this.colJenisPengeluaran,
            this.colNilaiPengeluaran,
            this.colKeterangan});
            this.gridView1.GridControl = this.PerintahBayarDetilGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsMenu.EnableFooterMenu = false;
            this.gridView1.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colIdDepartemen
            // 
            this.colIdDepartemen.Caption = "Departemen";
            this.colIdDepartemen.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.colIdDepartemen.FieldName = "IdDepartemen";
            this.colIdDepartemen.Name = "colIdDepartemen";
            this.colIdDepartemen.Visible = true;
            this.colIdDepartemen.VisibleIndex = 0;
            this.colIdDepartemen.Width = 81;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeDepartemen", "Kode Departemen", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaDepartemen", "Nama Departemen", 70)});
            this.repositoryItemLookUpEdit2.DataSource = this.listDepartemenBindingSource;
            this.repositoryItemLookUpEdit2.DisplayMember = "KodeDepartemen";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.ValueMember = "IdDepartemen";
            // 
            // listDepartemenBindingSource
            // 
            this.listDepartemenBindingSource.DataMember = "ListDepartemen";
            this.listDepartemenBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // PerintahBayarBindingSource
            // 
            this.PerintahBayarBindingSource.DataSource = typeof(SentraGL.Document.PerintahBayar);
            // 
            // colIdProyek
            // 
            this.colIdProyek.Caption = "Proyek";
            this.colIdProyek.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colIdProyek.FieldName = "IdProyek";
            this.colIdProyek.Name = "colIdProyek";
            this.colIdProyek.Visible = true;
            this.colIdProyek.VisibleIndex = 1;
            this.colIdProyek.Width = 69;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.repositoryItemLookUpEdit3.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeProyek", "Kode Proyek", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaProyek", "Nama Proyek", 70)});
            this.repositoryItemLookUpEdit3.DataSource = this.listProyekBindingSource;
            this.repositoryItemLookUpEdit3.DisplayMember = "KodeProyek";
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.ValueMember = "IdProyek";
            // 
            // listProyekBindingSource
            // 
            this.listProyekBindingSource.DataMember = "ListProyek";
            this.listProyekBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // colJenisPengeluaran
            // 
            this.colJenisPengeluaran.Caption = "JenisPengeluaran";
            this.colJenisPengeluaran.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colJenisPengeluaran.FieldName = "JenisPengeluaran";
            this.colJenisPengeluaran.Name = "colJenisPengeluaran";
            this.colJenisPengeluaran.OptionsColumn.FixedWidth = true;
            this.colJenisPengeluaran.Visible = true;
            this.colJenisPengeluaran.VisibleIndex = 2;
            this.colJenisPengeluaran.Width = 170;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("JenisPengeluaran", "Jenis Pengeluaran", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.repositoryItemLookUpEdit1.DataSource = this.listJenisPengeluaranBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "JenisPengeluaran";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ValueMember = "JenisPengeluaran";
            // 
            // listJenisPengeluaranBindingSource
            // 
            this.listJenisPengeluaranBindingSource.DataMember = "ListJenisPengeluaran";
            this.listJenisPengeluaranBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // colNilaiPengeluaran
            // 
            this.colNilaiPengeluaran.AppearanceHeader.Options.UseTextOptions = true;
            this.colNilaiPengeluaran.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNilaiPengeluaran.Caption = "Nilai";
            this.colNilaiPengeluaran.FieldName = "NilaiPengeluaran";
            this.colNilaiPengeluaran.Name = "colNilaiPengeluaran";
            this.colNilaiPengeluaran.OptionsColumn.FixedWidth = true;
            this.colNilaiPengeluaran.SummaryItem.DisplayFormat = "{0:#,##0;(#,##0);0}";
            this.colNilaiPengeluaran.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colNilaiPengeluaran.Visible = true;
            this.colNilaiPengeluaran.VisibleIndex = 3;
            this.colNilaiPengeluaran.Width = 83;
            // 
            // colKeterangan
            // 
            this.colKeterangan.Caption = "Keterangan";
            this.colKeterangan.FieldName = "Keterangan";
            this.colKeterangan.Name = "colKeterangan";
            this.colKeterangan.Visible = true;
            this.colKeterangan.VisibleIndex = 4;
            this.colKeterangan.Width = 203;
            // 
            // PerintahBayarDetilGridControl
            // 
            this.PerintahBayarDetilGridControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.PerintahBayarDetilGridControl.DataSource = this.PerintahBayarDetilBindingSource;
            this.PerintahBayarDetilGridControl.EmbeddedNavigator.Name = "";
            this.PerintahBayarDetilGridControl.Location = new System.Drawing.Point(7, 116);
            this.PerintahBayarDetilGridControl.MainView = this.gridView1;
            this.PerintahBayarDetilGridControl.Name = "PerintahBayarDetilGridControl";
            this.PerintahBayarDetilGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemLookUpEdit3});
            this.PerintahBayarDetilGridControl.Size = new System.Drawing.Size(585, 176);
            this.PerintahBayarDetilGridControl.TabIndex = 58;
            this.PerintahBayarDetilGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // PerintahBayarDetilBindingSource
            // 
            this.PerintahBayarDetilBindingSource.DataMember = "PerintahBayarDetil";
            this.PerintahBayarDetilBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // listKasBindingSource
            // 
            this.listKasBindingSource.DataMember = "ListKas";
            this.listKasBindingSource.DataSource = this.PerintahBayarBindingSource;
            // 
            // internalCheckEdit
            // 
            this.internalCheckEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Internal", true));
            this.internalCheckEdit.Location = new System.Drawing.Point(239, 12);
            this.internalCheckEdit.Name = "internalCheckEdit";
            this.internalCheckEdit.Properties.Caption = "Internal";
            this.internalCheckEdit.Size = new System.Drawing.Size(72, 19);
            this.internalCheckEdit.TabIndex = 66;
            this.internalCheckEdit.TabStop = false;
            // 
            // mataUangLabel1
            // 
            this.mataUangLabel1.DataBindings.Add(new Binding("Text", this.PerintahBayarBindingSource, "MataUang", true));
            this.mataUangLabel1.Location = new System.Drawing.Point(238, 67);
            this.mataUangLabel1.Name = "mataUangLabel1";
            this.mataUangLabel1.Size = new System.Drawing.Size(52, 19);
            this.mataUangLabel1.TabIndex = 64;
            this.mataUangLabel1.Text = "xx";
            this.mataUangLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gridRowMover21
            // 
            this.gridRowMover21.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.gridRowMover21.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.gridRowMover21.Appearance.Options.UseBackColor = true;
            this.gridRowMover21.GridControl = this.PerintahBayarDetilGridControl;
            this.gridRowMover21.Location = new System.Drawing.Point(598, 116);
            this.gridRowMover21.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridRowMover21.LookAndFeel.UseWindowsXPTheme = true;
            this.gridRowMover21.Name = "gridRowMover21";
            this.gridRowMover21.Size = new System.Drawing.Size(28, 64);
            this.gridRowMover21.TabIndex = 62;
            // 
            // keperluanTextEdit
            // 
            this.keperluanTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Keperluan", true));
            this.keperluanTextEdit.Location = new System.Drawing.Point(418, 38);
            this.keperluanTextEdit.Name = "keperluanTextEdit";
            this.keperluanTextEdit.Size = new System.Drawing.Size(157, 20);
            this.keperluanTextEdit.TabIndex = 52;
            // 
            // terimaDariTextEdit
            // 
            this.terimaDariTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NamaPenerima", true));
            this.terimaDariTextEdit.Location = new System.Drawing.Point(418, 12);
            this.terimaDariTextEdit.Name = "terimaDariTextEdit";
            this.terimaDariTextEdit.Size = new System.Drawing.Size(157, 20);
            this.terimaDariTextEdit.TabIndex = 50;
            // 
            // catatanTextEdit
            // 
            this.catatanTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Catatan", true));
            this.catatanTextEdit.Location = new System.Drawing.Point(120, 90);
            this.catatanTextEdit.Name = "catatanTextEdit";
            this.catatanTextEdit.Size = new System.Drawing.Size(455, 20);
            this.catatanTextEdit.TabIndex = 53;
            // 
            // tglPengeluaranDateEdit
            // 
            this.tglPengeluaranDateEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "TglPerintahBayar", true));
            this.tglPengeluaranDateEdit.EditValue = null;
            this.tglPengeluaranDateEdit.Location = new System.Drawing.Point(120, 64);
            this.tglPengeluaranDateEdit.Name = "tglPengeluaranDateEdit";
            this.tglPengeluaranDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglPengeluaranDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglPengeluaranDateEdit.Size = new System.Drawing.Size(112, 20);
            this.tglPengeluaranDateEdit.TabIndex = 46;
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
            this.idAkunLookUpEdit.TabIndex = 45;
            // 
            // noPerintahBayarTextEdit
            // 
            this.noPerintahBayarTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "NoPerintahBayar", true));
            this.noPerintahBayarTextEdit.Location = new System.Drawing.Point(120, 12);
            this.noPerintahBayarTextEdit.Name = "noPerintahBayarTextEdit";
            this.noPerintahBayarTextEdit.Size = new System.Drawing.Size(112, 20);
            this.noPerintahBayarTextEdit.TabIndex = 59;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(370, 67);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(42, 13);
            statusLabel.TabIndex = 66;
            statusLabel.Text = "Status:";
            // 
            // statusTextEdit
            // 
            this.statusTextEdit.DataBindings.Add(new Binding("EditValue", this.PerintahBayarBindingSource, "Status", true));
            this.statusTextEdit.Location = new System.Drawing.Point(418, 64);
            this.statusTextEdit.Name = "statusTextEdit";
            this.statusTextEdit.Size = new System.Drawing.Size(157, 20);
            this.statusTextEdit.TabIndex = 67;
            // 
            // DocPerintahBayar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(632, 303);
            this.Controls.Add(statusLabel);
            this.Controls.Add(this.statusTextEdit);
            this.Controls.Add(this.internalCheckEdit);
            this.Controls.Add(this.mataUangLabel1);
            this.Controls.Add(this.gridRowMover21);
            this.Controls.Add(tglPengeluaranLabel);
            this.Controls.Add(catatanLabel);
            this.Controls.Add(this.keperluanTextEdit);
            this.Controls.Add(this.terimaDariTextEdit);
            this.Controls.Add(this.catatanTextEdit);
            this.Controls.Add(this.tglPengeluaranDateEdit);
            this.Controls.Add(this.idAkunLookUpEdit);
            this.Controls.Add(this.noPerintahBayarTextEdit);
            this.Controls.Add(this.PerintahBayarDetilGridControl);
            this.Controls.Add(keperluanLabel);
            this.Controls.Add(terimaDariLabel);
            this.Controls.Add(idAkunLabel);
            this.Controls.Add(noPerintahBayarLabel);
            this.Name = "DocPerintahBayar";
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listJenisPengeluaranBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarDetilGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PerintahBayarDetilBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keperluanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.terimaDariTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.catatanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglPengeluaranDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPerintahBayarTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusTextEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colIdDepartemen;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private System.Windows.Forms.BindingSource listDepartemenBindingSource;
        private BindingSource PerintahBayarBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colIdProyek;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private System.Windows.Forms.BindingSource listProyekBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colJenisPengeluaran;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.BindingSource listJenisPengeluaranBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colNilaiPengeluaran;
        private DevExpress.XtraGrid.Columns.GridColumn colKeterangan;
        private DevExpress.XtraGrid.GridControl PerintahBayarDetilGridControl;
        private System.Windows.Forms.BindingSource PerintahBayarDetilBindingSource;
        private System.Windows.Forms.BindingSource listKasBindingSource;
        private DevExpress.XtraEditors.CheckEdit internalCheckEdit;
        private System.Windows.Forms.Label mataUangLabel1;
        private SentraWinFramework.GridRowMover2 gridRowMover21;
        private DevExpress.XtraEditors.TextEdit keperluanTextEdit;
        private DevExpress.XtraEditors.TextEdit terimaDariTextEdit;
        private DevExpress.XtraEditors.TextEdit catatanTextEdit;
        private DevExpress.XtraEditors.DateEdit tglPengeluaranDateEdit;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit;
        private DevExpress.XtraEditors.TextEdit noPerintahBayarTextEdit;
        private DevExpress.XtraEditors.TextEdit statusTextEdit;
    }
}
