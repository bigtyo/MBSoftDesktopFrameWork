namespace Examples01
{
    partial class frmPenjualan
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
            System.Windows.Forms.Label noPenjualanLabel;
            System.Windows.Forms.Label statusLabel;
            System.Windows.Forms.Label tglJualLabel;
            System.Windows.Forms.Label keteranganLabel;
            System.Windows.Forms.Label totalLabel;
            System.Windows.Forms.Label diskonLabel;
            System.Windows.Forms.Label totalAkhirLabel;
            System.Windows.Forms.Label noPelangganLabel;
            System.Windows.Forms.Label namaPelangganLabel;
            this.noPenjualanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.penjualanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.statusLabel1 = new System.Windows.Forms.Label();
            this.tglJualDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.itemPenjualanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.itemPenjualanGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHargaSatuan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.colKodeItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colNamaItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colJumlah = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCtr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.totalCalcEdit = new DevExpress.XtraEditors.CalcEdit();
            this.diskonCalcEdit = new DevExpress.XtraEditors.CalcEdit();
            this.totalAkhirCalcEdit = new DevExpress.XtraEditors.CalcEdit();
            this.pelangganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.noPelangganLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.namaPelangganLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.uiNavigator1 = new SentraWinFramework.UINavigator();
            noPenjualanLabel = new System.Windows.Forms.Label();
            statusLabel = new System.Windows.Forms.Label();
            tglJualLabel = new System.Windows.Forms.Label();
            keteranganLabel = new System.Windows.Forms.Label();
            totalLabel = new System.Windows.Forms.Label();
            diskonLabel = new System.Windows.Forms.Label();
            totalAkhirLabel = new System.Windows.Forms.Label();
            noPelangganLabel = new System.Windows.Forms.Label();
            namaPelangganLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.noPenjualanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penjualanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglJualDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPenjualanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPenjualanGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalCalcEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diskonCalcEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalAkhirCalcEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // noPenjualanLabel
            // 
            noPenjualanLabel.AutoSize = true;
            noPenjualanLabel.Location = new System.Drawing.Point(31, 23);
            noPenjualanLabel.Name = "noPenjualanLabel";
            noPenjualanLabel.Size = new System.Drawing.Size(74, 13);
            noPenjualanLabel.TabIndex = 1;
            noPenjualanLabel.Text = "No Penjualan:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new System.Drawing.Point(320, 49);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new System.Drawing.Size(42, 13);
            statusLabel.TabIndex = 6;
            statusLabel.Text = "Status:";
            // 
            // tglJualLabel
            // 
            tglJualLabel.AutoSize = true;
            tglJualLabel.Location = new System.Drawing.Point(315, 23);
            tglJualLabel.Name = "tglJualLabel";
            tglJualLabel.Size = new System.Drawing.Size(47, 13);
            tglJualLabel.TabIndex = 8;
            tglJualLabel.Text = "Tgl Jual:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(295, 75);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 11;
            keteranganLabel.Text = "Keterangan:";
            // 
            // totalLabel
            // 
            totalLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            totalLabel.AutoSize = true;
            totalLabel.Location = new System.Drawing.Point(431, 244);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new System.Drawing.Size(35, 13);
            totalLabel.TabIndex = 13;
            totalLabel.Text = "Total:";
            // 
            // diskonLabel
            // 
            diskonLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            diskonLabel.AutoSize = true;
            diskonLabel.Location = new System.Drawing.Point(422, 270);
            diskonLabel.Name = "diskonLabel";
            diskonLabel.Size = new System.Drawing.Size(42, 13);
            diskonLabel.TabIndex = 15;
            diskonLabel.Text = "Diskon:";
            // 
            // totalAkhirLabel
            // 
            totalAkhirLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            totalAkhirLabel.AutoSize = true;
            totalAkhirLabel.Location = new System.Drawing.Point(404, 296);
            totalAkhirLabel.Name = "totalAkhirLabel";
            totalAkhirLabel.Size = new System.Drawing.Size(62, 13);
            totalAkhirLabel.TabIndex = 17;
            totalAkhirLabel.Text = "Total Akhir:";
            // 
            // noPelangganLabel
            // 
            noPelangganLabel.AutoSize = true;
            noPelangganLabel.Location = new System.Drawing.Point(27, 49);
            noPelangganLabel.Name = "noPelangganLabel";
            noPelangganLabel.Size = new System.Drawing.Size(77, 13);
            noPelangganLabel.TabIndex = 19;
            noPelangganLabel.Text = "No Pelanggan:";
            // 
            // namaPelangganLabel
            // 
            namaPelangganLabel.AutoSize = true;
            namaPelangganLabel.Location = new System.Drawing.Point(13, 75);
            namaPelangganLabel.Name = "namaPelangganLabel";
            namaPelangganLabel.Size = new System.Drawing.Size(91, 13);
            namaPelangganLabel.TabIndex = 20;
            namaPelangganLabel.Text = "Nama Pelanggan:";
            // 
            // noPenjualanTextEdit
            // 
            this.noPenjualanTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "NoPenjualan", true));
            this.noPenjualanTextEdit.Location = new System.Drawing.Point(111, 20);
            this.noPenjualanTextEdit.Name = "noPenjualanTextEdit";
            this.noPenjualanTextEdit.Size = new System.Drawing.Size(100, 20);
            this.noPenjualanTextEdit.TabIndex = 2;
            // 
            // penjualanBindingSource
            // 
            this.penjualanBindingSource.DataSource = typeof(Examples01.Entity.Penjualan);
            // 
            // statusLabel1
            // 
            this.statusLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.penjualanBindingSource, "Status", true));
            this.statusLabel1.Location = new System.Drawing.Point(371, 49);
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(103, 17);
            this.statusLabel1.TabIndex = 7;
            // 
            // tglJualDateEdit
            // 
            this.tglJualDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "TglJual", true));
            this.tglJualDateEdit.EditValue = null;
            this.tglJualDateEdit.Location = new System.Drawing.Point(371, 20);
            this.tglJualDateEdit.Name = "tglJualDateEdit";
            this.tglJualDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglJualDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglJualDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglJualDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglJualDateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglJualDateEdit.TabIndex = 9;
            // 
            // itemPenjualanBindingSource
            // 
            this.itemPenjualanBindingSource.DataMember = "ItemPenjualan";
            this.itemPenjualanBindingSource.DataSource = this.penjualanBindingSource;
            // 
            // itemPenjualanGridControl
            // 
            this.itemPenjualanGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.itemPenjualanGridControl.DataSource = this.itemPenjualanBindingSource;
            this.itemPenjualanGridControl.EmbeddedNavigator.Name = "";
            this.itemPenjualanGridControl.Location = new System.Drawing.Point(16, 109);
            this.itemPenjualanGridControl.MainView = this.gridView1;
            this.itemPenjualanGridControl.Name = "itemPenjualanGridControl";
            this.itemPenjualanGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemLookUpEdit2,
            this.repositoryItemCalcEdit1});
            this.itemPenjualanGridControl.Size = new System.Drawing.Size(555, 117);
            this.itemPenjualanGridControl.TabIndex = 10;
            this.itemPenjualanGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHargaSatuan,
            this.colKodeItem,
            this.colNamaItem,
            this.colTotal,
            this.colJumlah,
            this.colCtr});
            this.gridView1.GridControl = this.itemPenjualanGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colHargaSatuan
            // 
            this.colHargaSatuan.Caption = "HargaSatuan";
            this.colHargaSatuan.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colHargaSatuan.FieldName = "HargaSatuan";
            this.colHargaSatuan.Name = "colHargaSatuan";
            this.colHargaSatuan.Visible = true;
            this.colHargaSatuan.VisibleIndex = 2;
            this.colHargaSatuan.Width = 93;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.DisplayFormat.FormatString = "#,##0.##";
            this.repositoryItemCalcEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // colKodeItem
            // 
            this.colKodeItem.Caption = "KodeItem";
            this.colKodeItem.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colKodeItem.FieldName = "KodeItem";
            this.colKodeItem.Name = "colKodeItem";
            this.colKodeItem.Visible = true;
            this.colKodeItem.VisibleIndex = 0;
            this.colKodeItem.Width = 85;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeItem", "KodeItem", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaItem", "NamaItem", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit1.DataSource = this.itemBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "KodeItem";
            this.repositoryItemLookUpEdit1.ImmediatePopup = true;
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            this.repositoryItemLookUpEdit1.PopupFormWidth = 300;
            this.repositoryItemLookUpEdit1.PopupWidth = 300;
            this.repositoryItemLookUpEdit1.ValueMember = "KodeItem";
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(Examples01.Entity.Item);
            // 
            // colNamaItem
            // 
            this.colNamaItem.Caption = "NamaItem";
            this.colNamaItem.ColumnEdit = this.repositoryItemLookUpEdit2;
            this.colNamaItem.FieldName = "NamaItem";
            this.colNamaItem.Name = "colNamaItem";
            this.colNamaItem.Visible = true;
            this.colNamaItem.VisibleIndex = 1;
            this.colNamaItem.Width = 172;
            // 
            // repositoryItemLookUpEdit2
            // 
            this.repositoryItemLookUpEdit2.AutoHeight = false;
            this.repositoryItemLookUpEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit2.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeItem", "KodeItem", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaItem", "NamaItem", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit2.DataSource = this.itemBindingSource;
            this.repositoryItemLookUpEdit2.DisplayMember = "NamaItem";
            this.repositoryItemLookUpEdit2.Name = "repositoryItemLookUpEdit2";
            this.repositoryItemLookUpEdit2.NullText = "";
            this.repositoryItemLookUpEdit2.PopupWidth = 300;
            this.repositoryItemLookUpEdit2.ValueMember = "NamaItem";
            // 
            // colTotal
            // 
            this.colTotal.Caption = "Total";
            this.colTotal.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colTotal.FieldName = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Visible = true;
            this.colTotal.VisibleIndex = 4;
            this.colTotal.Width = 62;
            // 
            // colJumlah
            // 
            this.colJumlah.Caption = "Jumlah";
            this.colJumlah.ColumnEdit = this.repositoryItemCalcEdit1;
            this.colJumlah.FieldName = "Jumlah";
            this.colJumlah.Name = "colJumlah";
            this.colJumlah.Visible = true;
            this.colJumlah.VisibleIndex = 3;
            this.colJumlah.Width = 62;
            // 
            // colCtr
            // 
            this.colCtr.Caption = "Ctr";
            this.colCtr.FieldName = "Ctr";
            this.colCtr.Name = "colCtr";
            this.colCtr.Width = 76;
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(371, 72);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(143, 20);
            this.keteranganTextEdit.TabIndex = 12;
            // 
            // totalCalcEdit
            // 
            this.totalCalcEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalCalcEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "Total", true));
            this.totalCalcEdit.Location = new System.Drawing.Point(471, 241);
            this.totalCalcEdit.Name = "totalCalcEdit";
            this.totalCalcEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.totalCalcEdit.Properties.DisplayFormat.FormatString = "#,##0.##";
            this.totalCalcEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.totalCalcEdit.Properties.ReadOnly = true;
            this.totalCalcEdit.Size = new System.Drawing.Size(100, 20);
            this.totalCalcEdit.TabIndex = 14;
            // 
            // diskonCalcEdit
            // 
            this.diskonCalcEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.diskonCalcEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "Diskon", true));
            this.diskonCalcEdit.Location = new System.Drawing.Point(471, 267);
            this.diskonCalcEdit.Name = "diskonCalcEdit";
            this.diskonCalcEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.diskonCalcEdit.Properties.DisplayFormat.FormatString = "#,##0.##";
            this.diskonCalcEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.diskonCalcEdit.Size = new System.Drawing.Size(100, 20);
            this.diskonCalcEdit.TabIndex = 16;
            // 
            // totalAkhirCalcEdit
            // 
            this.totalAkhirCalcEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.totalAkhirCalcEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "TotalAkhir", true));
            this.totalAkhirCalcEdit.Location = new System.Drawing.Point(471, 293);
            this.totalAkhirCalcEdit.Name = "totalAkhirCalcEdit";
            this.totalAkhirCalcEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.totalAkhirCalcEdit.Properties.DisplayFormat.FormatString = "#,##0.##";
            this.totalAkhirCalcEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.totalAkhirCalcEdit.Size = new System.Drawing.Size(100, 20);
            this.totalAkhirCalcEdit.TabIndex = 18;
            // 
            // pelangganBindingSource
            // 
            this.pelangganBindingSource.DataSource = typeof(Examples01.Entity.Pelanggan);
            // 
            // noPelangganLookUpEdit
            // 
            this.noPelangganLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "NoPelanggan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.noPelangganLookUpEdit.Location = new System.Drawing.Point(111, 46);
            this.noPelangganLookUpEdit.Name = "noPelangganLookUpEdit";
            this.noPelangganLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.noPelangganLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoPelanggan", "NoPelanggan", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaPelanggan", "NamaPelanggan", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.noPelangganLookUpEdit.Properties.DataSource = this.pelangganBindingSource;
            this.noPelangganLookUpEdit.Properties.DisplayMember = "NoPelanggan";
            this.noPelangganLookUpEdit.Properties.NullText = "";
            this.noPelangganLookUpEdit.Properties.PopupWidth = 300;
            this.noPelangganLookUpEdit.Properties.ValueMember = "NoPelanggan";
            this.noPelangganLookUpEdit.Size = new System.Drawing.Size(123, 20);
            this.noPelangganLookUpEdit.TabIndex = 20;
            // 
            // namaPelangganLookUpEdit
            // 
            this.namaPelangganLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.penjualanBindingSource, "NamaPelanggan", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.namaPelangganLookUpEdit.Location = new System.Drawing.Point(111, 72);
            this.namaPelangganLookUpEdit.Name = "namaPelangganLookUpEdit";
            this.namaPelangganLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.namaPelangganLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoPelanggan", "NoPelanggan", 30, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaPelanggan", "NamaPelanggan", 70, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.namaPelangganLookUpEdit.Properties.DataSource = this.pelangganBindingSource;
            this.namaPelangganLookUpEdit.Properties.DisplayMember = "NamaPelanggan";
            this.namaPelangganLookUpEdit.Properties.NullText = "";
            this.namaPelangganLookUpEdit.Properties.PopupWidth = 300;
            this.namaPelangganLookUpEdit.Properties.ValueMember = "NamaPelanggan";
            this.namaPelangganLookUpEdit.Size = new System.Drawing.Size(169, 20);
            this.namaPelangganLookUpEdit.TabIndex = 21;
            // 
            // uiNavigator1
            // 
            this.uiNavigator1.BindingSource = this.penjualanBindingSource;
            this.uiNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiNavigator1.Location = new System.Drawing.Point(0, 324);
            this.uiNavigator1.Name = "uiNavigator1";
            this.uiNavigator1.Size = new System.Drawing.Size(585, 75);
            this.uiNavigator1.TabIndex = 22;
            // 
            // frmPenjualan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 399);
            this.Controls.Add(this.uiNavigator1);
            this.Controls.Add(namaPelangganLabel);
            this.Controls.Add(this.namaPelangganLookUpEdit);
            this.Controls.Add(noPelangganLabel);
            this.Controls.Add(this.noPelangganLookUpEdit);
            this.Controls.Add(totalAkhirLabel);
            this.Controls.Add(this.totalAkhirCalcEdit);
            this.Controls.Add(diskonLabel);
            this.Controls.Add(this.diskonCalcEdit);
            this.Controls.Add(totalLabel);
            this.Controls.Add(this.totalCalcEdit);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganTextEdit);
            this.Controls.Add(this.itemPenjualanGridControl);
            this.Controls.Add(tglJualLabel);
            this.Controls.Add(this.tglJualDateEdit);
            this.Controls.Add(statusLabel);
            this.Controls.Add(this.statusLabel1);
            this.Controls.Add(noPenjualanLabel);
            this.Controls.Add(this.noPenjualanTextEdit);
            this.Name = "frmPenjualan";
            this.Text = "Penjualan";
            ((System.ComponentModel.ISupportInitialize)(this.noPenjualanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penjualanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglJualDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPenjualanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPenjualanGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalCalcEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diskonCalcEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.totalAkhirCalcEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelangganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource penjualanBindingSource;
        private DevExpress.XtraEditors.TextEdit noPenjualanTextEdit;
        private System.Windows.Forms.Label statusLabel1;
        private DevExpress.XtraEditors.DateEdit tglJualDateEdit;
        private System.Windows.Forms.BindingSource itemPenjualanBindingSource;
        private DevExpress.XtraGrid.GridControl itemPenjualanGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colHargaSatuan;
        private DevExpress.XtraGrid.Columns.GridColumn colKodeItem;
        private DevExpress.XtraGrid.Columns.GridColumn colNamaItem;
        private DevExpress.XtraGrid.Columns.GridColumn colTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colJumlah;
        private DevExpress.XtraGrid.Columns.GridColumn colCtr;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.BindingSource itemBindingSource;
        private DevExpress.XtraEditors.CalcEdit totalCalcEdit;
        private DevExpress.XtraEditors.CalcEdit diskonCalcEdit;
        private DevExpress.XtraEditors.CalcEdit totalAkhirCalcEdit;
        private System.Windows.Forms.BindingSource pelangganBindingSource;
        private DevExpress.XtraEditors.LookUpEdit noPelangganLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit namaPelangganLookUpEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private SentraWinFramework.UINavigator uiNavigator1;
    }
}