using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocAturanJurnal
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label aturanJurnalLabel;
            System.Windows.Forms.Label keteranganLabel;
            this.masterAturanJurnalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aturanJurnalTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.keteranganMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.aturanJurnalDetilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.aturanJurnalDetilGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdDepartemen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listDepartemenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colIdProyek = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listProyekBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colNoAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colNamaAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colKredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKodeMataUang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit5 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.listMataUangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colDebitKurs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKreditKurs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKeterangan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridRowMover21 = new SentraWinFramework.GridRowMover2();
            this.multiMataUangCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            aturanJurnalLabel = new System.Windows.Forms.Label();
            keteranganLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.masterAturanJurnalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalDetilBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalDetilGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiMataUangCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // aturanJurnalLabel
            // 
            aturanJurnalLabel.AutoSize = true;
            aturanJurnalLabel.Location = new System.Drawing.Point(52, 16);
            aturanJurnalLabel.Name = "aturanJurnalLabel";
            aturanJurnalLabel.Size = new System.Drawing.Size(76, 13);
            aturanJurnalLabel.TabIndex = 1;
            aturanJurnalLabel.Text = "Aturan Jurnal:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(61, 42);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 3;
            keteranganLabel.Text = "Keterangan:";
            // 
            // masterAturanJurnalBindingSource
            // 
            this.masterAturanJurnalBindingSource.DataSource = typeof(SentraGL.Master.MasterAturanJurnal);
            // 
            // aturanJurnalTextEdit
            // 
            this.aturanJurnalTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.masterAturanJurnalBindingSource, "AturanJurnal", true));
            this.aturanJurnalTextEdit.Location = new System.Drawing.Point(134, 13);
            this.aturanJurnalTextEdit.Name = "aturanJurnalTextEdit";
            this.aturanJurnalTextEdit.Size = new System.Drawing.Size(282, 20);
            this.aturanJurnalTextEdit.TabIndex = 0;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.masterAturanJurnalBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(422, 14);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(53, 19);
            this.aktifCheckEdit.TabIndex = 1;
            this.aktifCheckEdit.TabStop = false;
            // 
            // keteranganMemoEdit
            // 
            this.keteranganMemoEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.masterAturanJurnalBindingSource, "Keterangan", true));
            this.keteranganMemoEdit.Location = new System.Drawing.Point(134, 39);
            this.keteranganMemoEdit.Name = "keteranganMemoEdit";
            this.keteranganMemoEdit.Size = new System.Drawing.Size(449, 40);
            this.keteranganMemoEdit.TabIndex = 3;
            // 
            // aturanJurnalDetilBindingSource
            // 
            this.aturanJurnalDetilBindingSource.DataMember = "AturanJurnalDetil";
            this.aturanJurnalDetilBindingSource.DataSource = this.masterAturanJurnalBindingSource;
            // 
            // aturanJurnalDetilGridControl
            // 
            this.aturanJurnalDetilGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.aturanJurnalDetilGridControl.DataSource = this.aturanJurnalDetilBindingSource;
            this.aturanJurnalDetilGridControl.Location = new System.Drawing.Point(12, 85);
            this.aturanJurnalDetilGridControl.MainView = this.gridView1;
            this.aturanJurnalDetilGridControl.Name = "aturanJurnalDetilGridControl";
            this.aturanJurnalDetilGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemLookUpEdit3,
            this.repositoryItemLookUpEdit4,
            this.repositoryItemLookUpEdit5,
            this.repositoryItemCalcEdit1});
            this.aturanJurnalDetilGridControl.Size = new System.Drawing.Size(634, 192);
            this.aturanJurnalDetilGridControl.TabIndex = 4;
            this.aturanJurnalDetilGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdDepartemen,
            this.colIdProyek,
            this.colNoAkun,
            this.colNamaAkun,
            this.colDebit,
            this.colKredit,
            this.colKodeMataUang,
            this.colDebitKurs,
            this.colKreditKurs,
            this.colKeterangan});
            this.gridView1.GridControl = this.aturanJurnalDetilGridControl;
            this.gridView1.Name = "gridView1";
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
            this.colIdDepartemen.Width = 94;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)), "Refresh Data (Ctrl+R)")});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeDepartemen", "Kode Departemen", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaDepartemen", "Nama Departemen", 70)});
            this.repositoryItemLookUpEdit2.DataSource = this.listDepartemenBindingSource;
            this.repositoryItemLookUpEdit2.DisplayMember = "KodeDepartemen";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.PopupWidth = 350;
            this.repositoryItemLookUpEdit2.ValueMember = "IdDepartemen";
            // 
            // listDepartemenBindingSource
            // 
            this.listDepartemenBindingSource.DataMember = "ListDepartemen";
            this.listDepartemenBindingSource.DataSource = this.masterAturanJurnalBindingSource;
            // 
            // colIdProyek
            // 
            this.colIdProyek.Caption = "Proyek";
            this.colIdProyek.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colIdProyek.FieldName = "IdProyek";
            this.colIdProyek.Name = "colIdProyek";
            this.colIdProyek.Visible = true;
            this.colIdProyek.VisibleIndex = 1;
            this.colIdProyek.Width = 88;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)), "Refresh Data (Ctrl+R)")});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeProyek", "Kode Proyek", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaProyek", "Nama Proyek", 70)});
            this.repositoryItemLookUpEdit1.DataSource = this.listProyekBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "KodeProyek";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.PopupWidth = 350;
            this.repositoryItemLookUpEdit1.ValueMember = "IdProyek";
            // 
            // listProyekBindingSource
            // 
            this.listProyekBindingSource.DataMember = "ListProyek";
            this.listProyekBindingSource.DataSource = this.masterAturanJurnalBindingSource;
            // 
            // colNoAkun
            // 
            this.colNoAkun.Caption = "NoAkun";
            this.colNoAkun.ColumnEdit = this.repositoryItemLookUpEdit3;
            this.colNoAkun.FieldName = "IdAkun";
            this.colNoAkun.Name = "colNoAkun";
            this.colNoAkun.Visible = true;
            this.colNoAkun.VisibleIndex = 2;
            // 
            // repositoryItemLookUpEdit3
            // 
            this.repositoryItemLookUpEdit3.AutoHeight = false;
            this.repositoryItemLookUpEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit3.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.repositoryItemLookUpEdit3.DataSource = this.listAkunBindingSource;
            this.repositoryItemLookUpEdit3.DisplayMember = "NoAkun";
            this.repositoryItemLookUpEdit3.Name = "repositoryItemLookUpEdit3";
            this.repositoryItemLookUpEdit3.NullText = "";
            this.repositoryItemLookUpEdit3.PopupWidth = 350;
            this.repositoryItemLookUpEdit3.ValueMember = "IdAkun";
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.masterAturanJurnalBindingSource;
            // 
            // colNamaAkun
            // 
            this.colNamaAkun.Caption = "NamaAkun";
            this.colNamaAkun.ColumnEdit = this.repositoryItemLookUpEdit4;
            this.colNamaAkun.FieldName = "IdAkun";
            this.colNamaAkun.Name = "colNamaAkun";
            this.colNamaAkun.Visible = true;
            this.colNamaAkun.VisibleIndex = 3;
            this.colNamaAkun.Width = 168;
            // 
            // repositoryItemLookUpEdit4
            // 
            this.repositoryItemLookUpEdit4.AutoHeight = false;
            this.repositoryItemLookUpEdit4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)), "Refresh Data (Ctrl+R)")});
            this.repositoryItemLookUpEdit4.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.repositoryItemLookUpEdit4.DataSource = this.listAkunBindingSource;
            this.repositoryItemLookUpEdit4.DisplayMember = "NamaAkun";
            this.repositoryItemLookUpEdit4.Name = "repositoryItemLookUpEdit4";
            this.repositoryItemLookUpEdit4.NullText = "";
            this.repositoryItemLookUpEdit4.PopupWidth = 350;
            this.repositoryItemLookUpEdit4.ValueMember = "IdAkun";
            // 
            // colDebit
            // 
            this.colDebit.Caption = "Debit";
            this.colDebit.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colDebit.DisplayFormat.FormatString = "{0:#,###.##;(#,###.##)}";
            this.colDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDebit.FieldName = "Debit";
            this.colDebit.Name = "colDebit";
            this.colDebit.SummaryItem.DisplayFormat = "{0:#,##0.##;(#,##0.##)}";
            this.colDebit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDebit.Visible = true;
            this.colDebit.VisibleIndex = 4;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "{0:#,###.####;(#,###.####)}";
            this.repositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemCalcEdit1.EditFormat.FormatString = "{0:#,###.####;(#,###.####)}";
            this.repositoryItemCalcEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // colKredit
            // 
            this.colKredit.Caption = "Kredit";
            this.colKredit.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colKredit.DisplayFormat.FormatString = "{0:#,###.##;(#,###.##)}";
            this.colKredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colKredit.FieldName = "Kredit";
            this.colKredit.Name = "colKredit";
            this.colKredit.SummaryItem.DisplayFormat = "{0:#,##0.##;(#,##0.##)}";
            this.colKredit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colKredit.Visible = true;
            this.colKredit.VisibleIndex = 5;
            // 
            // colKodeMataUang
            // 
            this.colKodeMataUang.Caption = "MataUang";
            this.colKodeMataUang.ColumnEdit = this.repositoryItemLookUpEdit5;
            this.colKodeMataUang.FieldName = "KodeMataUang";
            this.colKodeMataUang.Name = "colKodeMataUang";
            this.colKodeMataUang.Visible = true;
            this.colKodeMataUang.VisibleIndex = 6;
            // 
            // repositoryItemLookUpEdit5
            // 
            this.repositoryItemLookUpEdit5.AutoHeight = false;
            this.repositoryItemLookUpEdit5.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit5.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeMataUang", "Kode Mata Uang", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaMataUang", "Nama Mata Uang", 70)});
            this.repositoryItemLookUpEdit5.DataSource = this.listMataUangBindingSource;
            this.repositoryItemLookUpEdit5.DisplayMember = "KodeMataUang";
            this.repositoryItemLookUpEdit5.Name = "repositoryItemLookUpEdit5";
            this.repositoryItemLookUpEdit5.NullText = "";
            this.repositoryItemLookUpEdit5.PopupWidth = 350;
            this.repositoryItemLookUpEdit5.ValueMember = "KodeMataUang";
            // 
            // listMataUangBindingSource
            // 
            this.listMataUangBindingSource.DataMember = "ListMataUang";
            this.listMataUangBindingSource.DataSource = this.masterAturanJurnalBindingSource;
            // 
            // colDebitKurs
            // 
            this.colDebitKurs.Caption = "DebitKurs";
            this.colDebitKurs.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colDebitKurs.DisplayFormat.FormatString = "{0:#,###.####;(#,###.####)}";
            this.colDebitKurs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colDebitKurs.FieldName = "DebitKurs";
            this.colDebitKurs.Name = "colDebitKurs";
            this.colDebitKurs.Visible = true;
            this.colDebitKurs.VisibleIndex = 7;
            // 
            // colKreditKurs
            // 
            this.colKreditKurs.Caption = "KreditKurs";
            this.colKreditKurs.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colKreditKurs.DisplayFormat.FormatString = "{0:#,###.####;(#,###.####)}";
            this.colKreditKurs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colKreditKurs.FieldName = "KreditKurs";
            this.colKreditKurs.Name = "colKreditKurs";
            this.colKreditKurs.Visible = true;
            this.colKreditKurs.VisibleIndex = 8;
            // 
            // colKeterangan
            // 
            this.colKeterangan.Caption = "Keterangan";
            this.colKeterangan.FieldName = "Keterangan";
            this.colKeterangan.Name = "colKeterangan";
            this.colKeterangan.Visible = true;
            this.colKeterangan.VisibleIndex = 9;
            this.colKeterangan.Width = 117;
            // 
            // gridRowMover21
            // 
            this.gridRowMover21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRowMover21.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.gridRowMover21.Appearance.Options.UseBackColor = true;
            this.gridRowMover21.GridControl = this.aturanJurnalDetilGridControl;
            this.gridRowMover21.Location = new System.Drawing.Point(652, 85);
            this.gridRowMover21.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridRowMover21.LookAndFeel.UseWindowsXPTheme = true;
            this.gridRowMover21.Name = "gridRowMover21";
            this.gridRowMover21.Size = new System.Drawing.Size(28, 64);
            this.gridRowMover21.TabIndex = 5;
            // 
            // multiMataUangCheckEdit
            // 
            this.multiMataUangCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.masterAturanJurnalBindingSource, "MultiMataUang", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.multiMataUangCheckEdit.Location = new System.Drawing.Point(481, 14);
            this.multiMataUangCheckEdit.Name = "multiMataUangCheckEdit";
            this.multiMataUangCheckEdit.Properties.Caption = "Multi Mata Uang";
            this.multiMataUangCheckEdit.Size = new System.Drawing.Size(102, 19);
            this.multiMataUangCheckEdit.TabIndex = 2;
            this.multiMataUangCheckEdit.Tag = "";
            // 
            // DocAturanJurnal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(686, 289);
            this.Controls.Add(this.aturanJurnalDetilGridControl);
            this.Controls.Add(this.multiMataUangCheckEdit);
            this.Controls.Add(this.gridRowMover21);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganMemoEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(aturanJurnalLabel);
            this.Controls.Add(this.aturanJurnalTextEdit);
            this.Name = "DocAturanJurnal";
            ((System.ComponentModel.ISupportInitialize)(this.masterAturanJurnalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalDetilBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aturanJurnalDetilGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.multiMataUangCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource masterAturanJurnalBindingSource;
        private DevExpress.XtraEditors.TextEdit aturanJurnalTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.MemoEdit keteranganMemoEdit;
        private System.Windows.Forms.BindingSource aturanJurnalDetilBindingSource;
        private DevExpress.XtraGrid.GridControl aturanJurnalDetilGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colNamaAkun;
        private DevExpress.XtraGrid.Columns.GridColumn colNoAkun;
        private DevExpress.XtraGrid.Columns.GridColumn colIdDepartemen;
        private DevExpress.XtraGrid.Columns.GridColumn colKodeMataUang;
        private DevExpress.XtraGrid.Columns.GridColumn colIdProyek;
        private DevExpress.XtraGrid.Columns.GridColumn colDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colKredit;
        private DevExpress.XtraGrid.Columns.GridColumn colKeterangan;
        private DevExpress.XtraGrid.Columns.GridColumn colKreditKurs;
        private DevExpress.XtraGrid.Columns.GridColumn colDebitKurs;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.BindingSource listProyekBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit4;
        private System.Windows.Forms.BindingSource listDepartemenBindingSource;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit5;
        private System.Windows.Forms.BindingSource listMataUangBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private SentraWinFramework.GridRowMover2 gridRowMover21;
        private DevExpress.XtraEditors.CheckEdit multiMataUangCheckEdit;
    }
}
