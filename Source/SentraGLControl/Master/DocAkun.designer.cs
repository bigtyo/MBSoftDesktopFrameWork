using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Master
{
    partial class DocAkun
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
            System.Windows.Forms.Label idIndukLabel;
            System.Windows.Forms.Label noAkunLabel;
            System.Windows.Forms.Label namaAkunLabel;
            System.Windows.Forms.Label keteranganLabel;
            System.Windows.Forms.Label jenisAkunLabel;
            System.Windows.Forms.Label kelompokAkunLabel;
            System.Windows.Forms.Label kodeMataUangLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocAkun));
            this.akunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idIndukLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listIndukAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.noAkunTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaAkunTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.postingCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.terkunciCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.kodeMataUangLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listMataUangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.akunMoneterCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colNoAkun = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNamaAkun = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPosting = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.idIndukLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.listIndukAkunBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.kelompokAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listKelompokAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            idIndukLabel = new System.Windows.Forms.Label();
            noAkunLabel = new System.Windows.Forms.Label();
            namaAkunLabel = new System.Windows.Forms.Label();
            keteranganLabel = new System.Windows.Forms.Label();
            jenisAkunLabel = new System.Windows.Forms.Label();
            kelompokAkunLabel = new System.Windows.Forms.Label();
            kodeMataUangLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listIndukAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAkunTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaAkunTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.terkunciCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunMoneterCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listIndukAkunBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kelompokAkunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKelompokAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idIndukLabel
            // 
            idIndukLabel.AutoSize = true;
            idIndukLabel.Location = new System.Drawing.Point(65, 29);
            idIndukLabel.Name = "idIndukLabel";
            idIndukLabel.Size = new System.Drawing.Size(38, 13);
            idIndukLabel.TabIndex = 0;
            idIndukLabel.Text = "Induk:";
            // 
            // noAkunLabel
            // 
            noAkunLabel.AutoSize = true;
            noAkunLabel.Location = new System.Drawing.Point(52, 54);
            noAkunLabel.Name = "noAkunLabel";
            noAkunLabel.Size = new System.Drawing.Size(51, 13);
            noAkunLabel.TabIndex = 3;
            noAkunLabel.Text = "No Akun:";
            // 
            // namaAkunLabel
            // 
            namaAkunLabel.AutoSize = true;
            namaAkunLabel.Location = new System.Drawing.Point(38, 80);
            namaAkunLabel.Name = "namaAkunLabel";
            namaAkunLabel.Size = new System.Drawing.Size(65, 13);
            namaAkunLabel.TabIndex = 5;
            namaAkunLabel.Text = "Nama Akun:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(36, 106);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 8;
            keteranganLabel.Text = "Keterangan:";
            // 
            // jenisAkunLabel
            // 
            jenisAkunLabel.AutoSize = true;
            jenisAkunLabel.Location = new System.Drawing.Point(41, 133);
            jenisAkunLabel.Name = "jenisAkunLabel";
            jenisAkunLabel.Size = new System.Drawing.Size(62, 13);
            jenisAkunLabel.TabIndex = 12;
            jenisAkunLabel.Text = "Jenis Akun:";
            // 
            // kelompokAkunLabel
            // 
            kelompokAkunLabel.AutoSize = true;
            kelompokAkunLabel.Location = new System.Drawing.Point(14, 12);
            kelompokAkunLabel.Name = "kelompokAkunLabel";
            kelompokAkunLabel.Size = new System.Drawing.Size(83, 13);
            kelompokAkunLabel.TabIndex = 16;
            kelompokAkunLabel.Text = "Kelompok Akun:";
            // 
            // kodeMataUangLabel
            // 
            kodeMataUangLabel.AutoSize = true;
            kodeMataUangLabel.Location = new System.Drawing.Point(7, 10);
            kodeMataUangLabel.Name = "kodeMataUangLabel";
            kodeMataUangLabel.Size = new System.Drawing.Size(90, 13);
            kodeMataUangLabel.TabIndex = 0;
            kodeMataUangLabel.Text = "Kode Mata Uang:";
            // 
            // akunBindingSource
            // 
            this.akunBindingSource.DataSource = typeof(SentraGL.Master.Akun);
            // 
            // idIndukLookUpEdit
            // 
            this.idIndukLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "IdInduk", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit.Location = new System.Drawing.Point(109, 25);
            this.idIndukLookUpEdit.Name = "idIndukLookUpEdit";
            this.idIndukLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idIndukLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idIndukLookUpEdit.Properties.DataSource = this.listIndukAkunBindingSource;
            this.idIndukLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idIndukLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idIndukLookUpEdit.Size = new System.Drawing.Size(105, 20);
            this.idIndukLookUpEdit.TabIndex = 12;
            // 
            // listIndukAkunBindingSource
            // 
            this.listIndukAkunBindingSource.DataMember = "ListIndukAkun";
            this.listIndukAkunBindingSource.DataSource = this.akunBindingSource;
            // 
            // noAkunTextEdit
            // 
            this.noAkunTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "NoAkun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.noAkunTextEdit.Location = new System.Drawing.Point(109, 51);
            this.noAkunTextEdit.Name = "noAkunTextEdit";
            this.noAkunTextEdit.Size = new System.Drawing.Size(105, 20);
            this.noAkunTextEdit.TabIndex = 0;
            // 
            // namaAkunTextEdit
            // 
            this.namaAkunTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "NamaAkun", true));
            this.namaAkunTextEdit.Location = new System.Drawing.Point(109, 77);
            this.namaAkunTextEdit.Name = "namaAkunTextEdit";
            this.namaAkunTextEdit.Size = new System.Drawing.Size(288, 20);
            this.namaAkunTextEdit.TabIndex = 3;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(237, 51);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "&Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(55, 19);
            this.aktifCheckEdit.TabIndex = 1;
            this.aktifCheckEdit.TabStop = false;
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(109, 103);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(288, 20);
            this.keteranganTextEdit.TabIndex = 4;
            // 
            // postingCheckEdit
            // 
            this.postingCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "Posting", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.postingCheckEdit.Location = new System.Drawing.Point(107, 155);
            this.postingCheckEdit.Name = "postingCheckEdit";
            this.postingCheckEdit.Properties.Caption = "&Posting";
            this.postingCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.postingCheckEdit.TabIndex = 6;
            // 
            // terkunciCheckEdit
            // 
            this.terkunciCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "Terkunci", true));
            this.terkunciCheckEdit.Location = new System.Drawing.Point(298, 51);
            this.terkunciCheckEdit.Name = "terkunciCheckEdit";
            this.terkunciCheckEdit.Properties.Caption = "terkunci";
            this.terkunciCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.terkunciCheckEdit.TabIndex = 2;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "JenisAkun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.comboBoxEdit1.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.akunBindingSource, "EnableJenisAkun", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(109, 129);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(105, 20);
            this.comboBoxEdit1.TabIndex = 5;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.kodeMataUangLookUpEdit);
            this.panelControl1.Controls.Add(kodeMataUangLabel);
            this.panelControl1.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.akunBindingSource, "AkunMoneterVisible", true));
            this.panelControl1.Location = new System.Drawing.Point(6, 199);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(330, 33);
            this.panelControl1.TabIndex = 9;
            // 
            // kodeMataUangLookUpEdit
            // 
            this.kodeMataUangLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "KodeMataUang", true));
            this.kodeMataUangLookUpEdit.Location = new System.Drawing.Point(103, 7);
            this.kodeMataUangLookUpEdit.Name = "kodeMataUangLookUpEdit";
            this.kodeMataUangLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.kodeMataUangLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeMataUang", "Mata Uang", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.kodeMataUangLookUpEdit.Properties.DataSource = this.listMataUangBindingSource;
            this.kodeMataUangLookUpEdit.Properties.DisplayMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Properties.ValueMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Size = new System.Drawing.Size(60, 20);
            this.kodeMataUangLookUpEdit.TabIndex = 1;
            // 
            // listMataUangBindingSource
            // 
            this.listMataUangBindingSource.DataMember = "ListMataUang";
            this.listMataUangBindingSource.DataSource = this.akunBindingSource;
            // 
            // akunMoneterCheckEdit
            // 
            this.akunMoneterCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "AkunMoneter", true));
            this.akunMoneterCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.akunBindingSource, "AkunMoneterVisible", true));
            this.akunMoneterCheckEdit.Location = new System.Drawing.Point(188, 155);
            this.akunMoneterCheckEdit.Name = "akunMoneterCheckEdit";
            this.akunMoneterCheckEdit.Properties.Caption = "Akun Moneter";
            this.akunMoneterCheckEdit.Size = new System.Drawing.Size(105, 19);
            this.akunMoneterCheckEdit.TabIndex = 7;
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.Empty.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.Options.UseForeColor = true;
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.treeList1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.treeList1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(153)))), ((int)(((byte)(182)))));
            this.treeList1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.treeList1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.treeList1.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.treeList1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.treeList1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.treeList1.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeList1.Appearance.GroupButton.Options.UseBorderColor = true;
            this.treeList1.Appearance.GroupButton.Options.UseForeColor = true;
            this.treeList1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.treeList1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.treeList1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.treeList1.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.treeList1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.treeList1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(153)))), ((int)(((byte)(182)))));
            this.treeList1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.treeList1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.treeList1.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.treeList1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeList1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(219)))), ((int)(((byte)(226)))));
            this.treeList1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(131)))), ((int)(((byte)(161)))));
            this.treeList1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.treeList1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.treeList1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(188)))));
            this.treeList1.Appearance.HorzLine.Options.UseBackColor = true;
            this.treeList1.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.OddRow.Options.UseBackColor = true;
            this.treeList1.Appearance.OddRow.Options.UseForeColor = true;
            this.treeList1.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.treeList1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(165)))), ((int)(((byte)(177)))));
            this.treeList1.Appearance.Preview.Options.UseBackColor = true;
            this.treeList1.Appearance.Preview.Options.UseForeColor = true;
            this.treeList1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.Row.Options.UseBackColor = true;
            this.treeList1.Appearance.Row.Options.UseForeColor = true;
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(197)))), ((int)(((byte)(205)))));
            this.treeList1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.treeList1.Appearance.TreeLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(148)))));
            this.treeList1.Appearance.TreeLine.Options.UseBackColor = true;
            this.treeList1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(164)))), ((int)(((byte)(188)))));
            this.treeList1.Appearance.VertLine.Options.UseBackColor = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colNoAkun,
            this.colNamaAkun,
            this.colPosting});
            this.treeList1.DataSource = this.akunBindingSource;
            this.treeList1.KeyFieldName = "IdAkun";
            this.treeList1.Location = new System.Drawing.Point(434, 8);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList1.OptionsView.EnableAppearanceOddRow = true;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.ParentFieldName = "IdInduk";
            this.treeList1.SelectImageList = this.imageCollection1;
            this.treeList1.Size = new System.Drawing.Size(307, 260);
            this.treeList1.TabIndex = 1;
            this.treeList1.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeList1_GetSelectImage);
            // 
            // colNoAkun
            // 
            this.colNoAkun.Caption = "No Akun";
            this.colNoAkun.FieldName = "NoAkun";
            this.colNoAkun.MinWidth = 27;
            this.colNoAkun.Name = "colNoAkun";
            this.colNoAkun.OptionsColumn.AllowEdit = false;
            this.colNoAkun.Visible = true;
            this.colNoAkun.VisibleIndex = 0;
            this.colNoAkun.Width = 47;
            // 
            // colNamaAkun
            // 
            this.colNamaAkun.AppearanceCell.BorderColor = System.Drawing.Color.Transparent;
            this.colNamaAkun.AppearanceCell.Options.HighPriority = true;
            this.colNamaAkun.AppearanceCell.Options.UseBorderColor = true;
            this.colNamaAkun.Caption = "Nama Akun";
            this.colNamaAkun.FieldName = "NamaAkun";
            this.colNamaAkun.Name = "colNamaAkun";
            this.colNamaAkun.OptionsColumn.AllowEdit = false;
            this.colNamaAkun.Visible = true;
            this.colNamaAkun.VisibleIndex = 1;
            this.colNamaAkun.Width = 91;
            // 
            // colPosting
            // 
            this.colPosting.Caption = "Posting";
            this.colPosting.FieldName = "Posting";
            this.colPosting.Name = "colPosting";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(634, 274);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(107, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "&Refresh Diagram";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // idIndukLookUpEdit1
            // 
            this.idIndukLookUpEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "IdInduk", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit1.Location = new System.Drawing.Point(220, 25);
            this.idIndukLookUpEdit1.Name = "idIndukLookUpEdit1";
            this.idIndukLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idIndukLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idIndukLookUpEdit1.Properties.DataSource = this.listIndukAkunBindingSource1;
            this.idIndukLookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.idIndukLookUpEdit1.Properties.ValueMember = "IdAkun";
            this.idIndukLookUpEdit1.Size = new System.Drawing.Size(177, 20);
            this.idIndukLookUpEdit1.TabIndex = 13;
            // 
            // listIndukAkunBindingSource1
            // 
            this.listIndukAkunBindingSource1.DataMember = "ListIndukAkun";
            this.listIndukAkunBindingSource1.DataSource = this.akunBindingSource;
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.kelompokAkunLookUpEdit);
            this.panelControl2.Controls.Add(kelompokAkunLabel);
            this.panelControl2.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.akunBindingSource, "Posting", true));
            this.panelControl2.Location = new System.Drawing.Point(6, 171);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(330, 29);
            this.panelControl2.TabIndex = 8;
            // 
            // kelompokAkunLookUpEdit
            // 
            this.kelompokAkunLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "KelompokAkun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.kelompokAkunLookUpEdit.Location = new System.Drawing.Point(103, 8);
            this.kelompokAkunLookUpEdit.Name = "kelompokAkunLookUpEdit";
            this.kelompokAkunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.kelompokAkunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaKelompokAkun", "Kelompok Akun", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.kelompokAkunLookUpEdit.Properties.DataSource = this.listKelompokAkunBindingSource;
            this.kelompokAkunLookUpEdit.Properties.DisplayMember = "NamaKelompokAkun";
            this.kelompokAkunLookUpEdit.Properties.ValueMember = "KelompokAkun";
            this.kelompokAkunLookUpEdit.Size = new System.Drawing.Size(215, 20);
            this.kelompokAkunLookUpEdit.TabIndex = 1;
            // 
            // listKelompokAkunBindingSource
            // 
            this.listKelompokAkunBindingSource.DataMember = "ListKelompokAkun";
            this.listKelompokAkunBindingSource.DataSource = this.akunBindingSource;
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.akunMoneterCheckEdit);
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(jenisAkunLabel);
            this.groupControl1.Controls.Add(this.terkunciCheckEdit);
            this.groupControl1.Controls.Add(this.postingCheckEdit);
            this.groupControl1.Controls.Add(keteranganLabel);
            this.groupControl1.Controls.Add(this.keteranganTextEdit);
            this.groupControl1.Controls.Add(this.aktifCheckEdit);
            this.groupControl1.Controls.Add(namaAkunLabel);
            this.groupControl1.Controls.Add(this.namaAkunTextEdit);
            this.groupControl1.Controls.Add(noAkunLabel);
            this.groupControl1.Controls.Add(this.noAkunTextEdit);
            this.groupControl1.Controls.Add(idIndukLabel);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit1);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit);
            this.groupControl1.Controls.Add(this.panelControl1);
            this.groupControl1.Controls.Add(this.panelControl2);
            this.groupControl1.Location = new System.Drawing.Point(12, 8);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(408, 260);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // DocAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(753, 304);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.treeList1);
            this.Name = "DocAkun";
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listIndukAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noAkunTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaAkunTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.terkunciCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunMoneterCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listIndukAkunBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kelompokAkunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKelompokAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource akunBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit;
        private DevExpress.XtraEditors.TextEdit noAkunTextEdit;
        private DevExpress.XtraEditors.TextEdit namaAkunTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
        private DevExpress.XtraEditors.CheckEdit postingCheckEdit;
        private DevExpress.XtraEditors.CheckEdit terkunciCheckEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.CheckEdit akunMoneterCheckEdit;
        private DevExpress.XtraEditors.LookUpEdit kodeMataUangLookUpEdit;
        private System.Windows.Forms.BindingSource listIndukAkunBindingSource;
        private System.Windows.Forms.BindingSource listMataUangBindingSource;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNoAkun;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNamaAkun;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit1;
        private System.Windows.Forms.BindingSource listIndukAkunBindingSource1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPosting;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit kelompokAkunLookUpEdit;
        private System.Windows.Forms.BindingSource listKelompokAkunBindingSource;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
