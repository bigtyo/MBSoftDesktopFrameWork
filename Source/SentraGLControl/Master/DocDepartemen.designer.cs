using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocDepartemen
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
            Label idIndukLabel;
            Label KodeDepartemenLabel;
            Label namaDepartemenLabel;
            Label keteranganLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocDepartemen));
            this.idIndukLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.departemenBindingSource = new BindingSource(this.components);
            this.listDepartemenBindingSource = new BindingSource(this.components);
            this.idIndukLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.KodeDepartemenTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaDepartemenTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.keteranganMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.postingCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.departemenProduksiCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colKodeDepartemen = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNamaDepartemen = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPosting = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDepartemenProduksi = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            idIndukLabel = new Label();
            KodeDepartemenLabel = new Label();
            namaDepartemenLabel = new Label();
            keteranganLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departemenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KodeDepartemenTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaDepartemenTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.departemenProduksiCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idIndukLabel
            // 
            idIndukLabel.AutoSize = true;
            idIndukLabel.Location = new System.Drawing.Point(71, 34);
            idIndukLabel.Name = "idIndukLabel";
            idIndukLabel.Size = new System.Drawing.Size(38, 13);
            idIndukLabel.TabIndex = 0;
            idIndukLabel.Text = "Induk:";
            // 
            // KodeDepartemenLabel
            // 
            KodeDepartemenLabel.AutoSize = true;
            KodeDepartemenLabel.Location = new System.Drawing.Point(12, 60);
            KodeDepartemenLabel.Name = "KodeDepartemenLabel";
            KodeDepartemenLabel.Size = new System.Drawing.Size(97, 13);
            KodeDepartemenLabel.TabIndex = 3;
            KodeDepartemenLabel.Text = "Kode Departemen:";
            // 
            // namaDepartemenLabel
            // 
            namaDepartemenLabel.AutoSize = true;
            namaDepartemenLabel.Location = new System.Drawing.Point(9, 86);
            namaDepartemenLabel.Name = "namaDepartemenLabel";
            namaDepartemenLabel.Size = new System.Drawing.Size(100, 13);
            namaDepartemenLabel.TabIndex = 5;
            namaDepartemenLabel.Text = "Nama Departemen:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(42, 112);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 7;
            keteranganLabel.Text = "Keterangan:";
            // 
            // idIndukLookUpEdit
            // 
            this.idIndukLookUpEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "IdInduk", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit.Location = new System.Drawing.Point(115, 31);
            this.idIndukLookUpEdit.Name = "idIndukLookUpEdit";
            this.idIndukLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idIndukLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeDepartemen", "Kode Departemen", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaDepartemen", "Nama Departemen", 70)});
            this.idIndukLookUpEdit.Properties.DataSource = this.listDepartemenBindingSource;
            this.idIndukLookUpEdit.Properties.DisplayMember = "KodeDepartemen";
            this.idIndukLookUpEdit.Properties.NullText = "";
            this.idIndukLookUpEdit.Properties.PopupWidth = 350;
            this.idIndukLookUpEdit.Properties.ValueMember = "IdDepartemen";
            this.idIndukLookUpEdit.Size = new System.Drawing.Size(101, 20);
            this.idIndukLookUpEdit.TabIndex = 5;
            // 
            // departemenBindingSource
            // 
            this.departemenBindingSource.DataSource = typeof(SentraGL.Master.Departemen);
            // 
            // listDepartemenBindingSource
            // 
            this.listDepartemenBindingSource.DataMember = "ListDepartemen";
            this.listDepartemenBindingSource.DataSource = this.departemenBindingSource;
            // 
            // idIndukLookUpEdit1
            // 
            this.idIndukLookUpEdit1.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "IdInduk", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit1.Location = new System.Drawing.Point(222, 30);
            this.idIndukLookUpEdit1.Name = "idIndukLookUpEdit1";
            this.idIndukLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idIndukLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeDepartemen", "Kode Departemen", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaDepartemen", "Nama Departemen", 70)});
            this.idIndukLookUpEdit1.Properties.DataSource = this.listDepartemenBindingSource;
            this.idIndukLookUpEdit1.Properties.DisplayMember = "NamaDepartemen";
            this.idIndukLookUpEdit1.Properties.NullText = "";
            this.idIndukLookUpEdit1.Properties.PopupWidth = 350;
            this.idIndukLookUpEdit1.Properties.ValueMember = "IdDepartemen";
            this.idIndukLookUpEdit1.Size = new System.Drawing.Size(191, 20);
            this.idIndukLookUpEdit1.TabIndex = 6;
            // 
            // KodeDepartemenTextEdit
            // 
            this.KodeDepartemenTextEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "KodeDepartemen", true, DataSourceUpdateMode.OnPropertyChanged));
            this.KodeDepartemenTextEdit.Location = new System.Drawing.Point(115, 57);
            this.KodeDepartemenTextEdit.Name = "KodeDepartemenTextEdit";
            this.KodeDepartemenTextEdit.Size = new System.Drawing.Size(158, 20);
            this.KodeDepartemenTextEdit.TabIndex = 0;
            // 
            // namaDepartemenTextEdit
            // 
            this.namaDepartemenTextEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "NamaDepartemen", true));
            this.namaDepartemenTextEdit.Location = new System.Drawing.Point(115, 83);
            this.namaDepartemenTextEdit.Name = "namaDepartemenTextEdit";
            this.namaDepartemenTextEdit.Size = new System.Drawing.Size(298, 20);
            this.namaDepartemenTextEdit.TabIndex = 1;
            // 
            // keteranganMemoEdit
            // 
            this.keteranganMemoEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "Keterangan", true));
            this.keteranganMemoEdit.Location = new System.Drawing.Point(115, 109);
            this.keteranganMemoEdit.Name = "keteranganMemoEdit";
            this.keteranganMemoEdit.Size = new System.Drawing.Size(298, 44);
            this.keteranganMemoEdit.TabIndex = 2;
            // 
            // postingCheckEdit
            // 
            this.postingCheckEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "Posting", true, DataSourceUpdateMode.OnPropertyChanged));
            this.postingCheckEdit.Location = new System.Drawing.Point(115, 159);
            this.postingCheckEdit.Name = "postingCheckEdit";
            this.postingCheckEdit.Properties.Caption = "Posting";
            this.postingCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.postingCheckEdit.TabIndex = 3;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(279, 58);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 11;
            this.aktifCheckEdit.TabStop = false;
            // 
            // departemenProduksiCheckEdit
            // 
            this.departemenProduksiCheckEdit.DataBindings.Add(new Binding("EditValue", this.departemenBindingSource, "DepartemenProduksi", true));
            this.departemenProduksiCheckEdit.DataBindings.Add(new Binding("Visible", this.departemenBindingSource, "Posting", true, DataSourceUpdateMode.OnPropertyChanged));
            this.departemenProduksiCheckEdit.Location = new System.Drawing.Point(196, 159);
            this.departemenProduksiCheckEdit.Name = "departemenProduksiCheckEdit";
            this.departemenProduksiCheckEdit.Properties.Caption = "Departemen Produksi";
            this.departemenProduksiCheckEdit.Size = new System.Drawing.Size(143, 19);
            this.departemenProduksiCheckEdit.TabIndex = 4;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(451, 209);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(110, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "&Refresh Diagram";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.treeList1.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.Empty.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(250)))));
            this.treeList1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.Options.UseForeColor = true;
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeList1.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.ControlLightLight;
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
            this.colKodeDepartemen,
            this.colNamaDepartemen,
            this.colPosting,
            this.colDepartemenProduksi});
            this.treeList1.DataSource = this.departemenBindingSource;
            this.treeList1.KeyFieldName = "IdDepartemen";
            this.treeList1.Location = new System.Drawing.Point(442, 12);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsBehavior.PopulateServiceColumns = true;
            this.treeList1.OptionsMenu.EnableColumnMenu = false;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList1.OptionsView.EnableAppearanceOddRow = true;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.ParentFieldName = "IdInduk";
            this.treeList1.SelectImageList = this.imageCollection1;
            this.treeList1.Size = new System.Drawing.Size(119, 191);
            this.treeList1.TabIndex = 1;
            this.treeList1.TabStop = false;
            this.treeList1.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeList1_GetSelectImage);
            // 
            // colKodeDepartemen
            // 
            this.colKodeDepartemen.Caption = "KodeDepartemen";
            this.colKodeDepartemen.FieldName = "KodeDepartemen";
            this.colKodeDepartemen.MinWidth = 27;
            this.colKodeDepartemen.Name = "colKodeDepartemen";
            this.colKodeDepartemen.OptionsColumn.AllowMove = false;
            this.colKodeDepartemen.OptionsColumn.AllowSort = false;
            this.colKodeDepartemen.OptionsColumn.FixedWidth = true;
            this.colKodeDepartemen.OptionsColumn.ReadOnly = true;
            this.colKodeDepartemen.Visible = true;
            this.colKodeDepartemen.VisibleIndex = 0;
            this.colKodeDepartemen.Width = 122;
            // 
            // colNamaDepartemen
            // 
            this.colNamaDepartemen.Caption = "NamaDepartemen";
            this.colNamaDepartemen.FieldName = "NamaDepartemen";
            this.colNamaDepartemen.Name = "colNamaDepartemen";
            this.colNamaDepartemen.OptionsColumn.AllowMove = false;
            this.colNamaDepartemen.OptionsColumn.AllowSort = false;
            this.colNamaDepartemen.OptionsColumn.ReadOnly = true;
            this.colNamaDepartemen.Visible = true;
            this.colNamaDepartemen.VisibleIndex = 1;
            this.colNamaDepartemen.Width = 362;
            // 
            // colPosting
            // 
            this.colPosting.Caption = "Posting";
            this.colPosting.FieldName = "Posting";
            this.colPosting.Name = "colPosting";
            // 
            // colDepartemenProduksi
            // 
            this.colDepartemenProduksi.Caption = "DepartemenProduksi";
            this.colDepartemenProduksi.FieldName = "DepartemenProduksi";
            this.colDepartemenProduksi.Name = "colDepartemenProduksi";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.departemenProduksiCheckEdit);
            this.groupControl1.Controls.Add(this.aktifCheckEdit);
            this.groupControl1.Controls.Add(this.postingCheckEdit);
            this.groupControl1.Controls.Add(keteranganLabel);
            this.groupControl1.Controls.Add(this.keteranganMemoEdit);
            this.groupControl1.Controls.Add(namaDepartemenLabel);
            this.groupControl1.Controls.Add(this.namaDepartemenTextEdit);
            this.groupControl1.Controls.Add(KodeDepartemenLabel);
            this.groupControl1.Controls.Add(this.KodeDepartemenTextEdit);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit1);
            this.groupControl1.Controls.Add(idIndukLabel);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(423, 190);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.simpleButton2.Location = new System.Drawing.Point(12, 208);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "simpleButton2";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // DocDepartemen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 244);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.treeList1);
            this.Name = "DocDepartemen";
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departemenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listDepartemenBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KodeDepartemenTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaDepartemenTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.departemenProduksiCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource departemenBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit1;
        private DevExpress.XtraEditors.TextEdit KodeDepartemenTextEdit;
        private DevExpress.XtraEditors.TextEdit namaDepartemenTextEdit;
        private DevExpress.XtraEditors.MemoEdit keteranganMemoEdit;
        private DevExpress.XtraEditors.CheckEdit postingCheckEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private System.Windows.Forms.BindingSource listDepartemenBindingSource;
        private DevExpress.XtraEditors.CheckEdit departemenProduksiCheckEdit;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKodeDepartemen;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNamaDepartemen;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPosting;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDepartemenProduksi;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}
