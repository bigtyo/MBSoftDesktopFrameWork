using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocProyek
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
            Label keteranganLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocProyek));
            this.KodeProyekLabel = new Label();
            this.namaProyekLabel = new Label();
            this.idIndukLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.ProyekBindingSource = new BindingSource(this.components);
            this.listProyekBindingSource = new BindingSource(this.components);
            this.idIndukLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.KodeProyekTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaProyekTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.keteranganMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.postingCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.colKodeProyek = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colNamaProyek = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colPosting = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            idIndukLabel = new Label();
            keteranganLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProyekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KodeProyekTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaProyekTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idIndukLabel
            // 
            idIndukLabel.AutoSize = true;
            idIndukLabel.Location = new System.Drawing.Point(50, 35);
            idIndukLabel.Name = "idIndukLabel";
            idIndukLabel.Size = new System.Drawing.Size(38, 13);
            idIndukLabel.TabIndex = 0;
            idIndukLabel.Text = "Induk:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(21, 112);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 7;
            keteranganLabel.Text = "Keterangan:";
            // 
            // KodeProyekLabel
            // 
            this.KodeProyekLabel.AutoSize = true;
            this.KodeProyekLabel.Location = new System.Drawing.Point(17, 61);
            this.KodeProyekLabel.Name = "KodeProyekLabel";
            this.KodeProyekLabel.Size = new System.Drawing.Size(71, 13);
            this.KodeProyekLabel.TabIndex = 3;
            this.KodeProyekLabel.Text = "Kode Proyek:";
            // 
            // namaProyekLabel
            // 
            this.namaProyekLabel.AutoSize = true;
            this.namaProyekLabel.Location = new System.Drawing.Point(14, 87);
            this.namaProyekLabel.Name = "namaProyekLabel";
            this.namaProyekLabel.Size = new System.Drawing.Size(74, 13);
            this.namaProyekLabel.TabIndex = 5;
            this.namaProyekLabel.Text = "Nama Proyek:";
            // 
            // idIndukLookUpEdit
            // 
            this.idIndukLookUpEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "IdInduk", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit.Location = new System.Drawing.Point(94, 32);
            this.idIndukLookUpEdit.Name = "idIndukLookUpEdit";
            this.idIndukLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idIndukLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeProyek", "Kode Proyek", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaProyek", "Nama Proyek", 70)});
            this.idIndukLookUpEdit.Properties.DataSource = this.listProyekBindingSource;
            this.idIndukLookUpEdit.Properties.DisplayMember = "KodeProyek";
            this.idIndukLookUpEdit.Properties.NullText = "";
            this.idIndukLookUpEdit.Properties.PopupWidth = 350;
            this.idIndukLookUpEdit.Properties.ValueMember = "IdProyek";
            this.idIndukLookUpEdit.Size = new System.Drawing.Size(107, 20);
            this.idIndukLookUpEdit.TabIndex = 4;
            // 
            // ProyekBindingSource
            // 
            this.ProyekBindingSource.DataSource = typeof(SentraGL.Master.Proyek);
            // 
            // listProyekBindingSource
            // 
            this.listProyekBindingSource.DataMember = "ListProyek";
            this.listProyekBindingSource.DataSource = this.ProyekBindingSource;
            // 
            // idIndukLookUpEdit1
            // 
            this.idIndukLookUpEdit1.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "IdInduk", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idIndukLookUpEdit1.Location = new System.Drawing.Point(207, 31);
            this.idIndukLookUpEdit1.Name = "idIndukLookUpEdit1";
            this.idIndukLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idIndukLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeProyek", "Kode Proyek", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaProyek", "Nama Proyek", 70)});
            this.idIndukLookUpEdit1.Properties.DataSource = this.listProyekBindingSource;
            this.idIndukLookUpEdit1.Properties.DisplayMember = "NamaProyek";
            this.idIndukLookUpEdit1.Properties.NullText = "";
            this.idIndukLookUpEdit1.Properties.PopupWidth = 350;
            this.idIndukLookUpEdit1.Properties.ValueMember = "IdProyek";
            this.idIndukLookUpEdit1.Size = new System.Drawing.Size(202, 20);
            this.idIndukLookUpEdit1.TabIndex = 5;
            // 
            // KodeProyekTextEdit
            // 
            this.KodeProyekTextEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "KodeProyek", true));
            this.KodeProyekTextEdit.Location = new System.Drawing.Point(94, 58);
            this.KodeProyekTextEdit.Name = "KodeProyekTextEdit";
            this.KodeProyekTextEdit.Size = new System.Drawing.Size(107, 20);
            this.KodeProyekTextEdit.TabIndex = 0;
            // 
            // namaProyekTextEdit
            // 
            this.namaProyekTextEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "NamaProyek", true));
            this.namaProyekTextEdit.Location = new System.Drawing.Point(94, 84);
            this.namaProyekTextEdit.Name = "namaProyekTextEdit";
            this.namaProyekTextEdit.Size = new System.Drawing.Size(315, 20);
            this.namaProyekTextEdit.TabIndex = 1;
            // 
            // keteranganMemoEdit
            // 
            this.keteranganMemoEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "Keterangan", true));
            this.keteranganMemoEdit.Location = new System.Drawing.Point(94, 110);
            this.keteranganMemoEdit.Name = "keteranganMemoEdit";
            this.keteranganMemoEdit.Size = new System.Drawing.Size(315, 44);
            this.keteranganMemoEdit.TabIndex = 2;
            // 
            // postingCheckEdit
            // 
            this.postingCheckEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "Posting", true));
            this.postingCheckEdit.Location = new System.Drawing.Point(94, 160);
            this.postingCheckEdit.Name = "postingCheckEdit";
            this.postingCheckEdit.Properties.Caption = "Posting";
            this.postingCheckEdit.Size = new System.Drawing.Size(75, 18);
            this.postingCheckEdit.TabIndex = 3;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.ProyekBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(207, 59);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 18);
            this.aktifCheckEdit.TabIndex = 11;
            this.aktifCheckEdit.TabStop = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(472, 220);
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
            this.colKodeProyek,
            this.colNamaProyek,
            this.colPosting});
            this.treeList1.DataSource = this.ProyekBindingSource;
            this.treeList1.KeyFieldName = "IdProyek";
            this.treeList1.Location = new System.Drawing.Point(441, 12);
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
            this.treeList1.Size = new System.Drawing.Size(141, 202);
            this.treeList1.TabIndex = 1;
            this.treeList1.TabStop = false;
            this.treeList1.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeList1_GetSelectImage);
            // 
            // colKodeProyek
            // 
            this.colKodeProyek.Caption = "KodeProyek";
            this.colKodeProyek.FieldName = "KodeProyek";
            this.colKodeProyek.MinWidth = 27;
            this.colKodeProyek.Name = "colKodeProyek";
            this.colKodeProyek.OptionsColumn.AllowMove = false;
            this.colKodeProyek.OptionsColumn.AllowSort = false;
            this.colKodeProyek.OptionsColumn.FixedWidth = true;
            this.colKodeProyek.OptionsColumn.ReadOnly = true;
            this.colKodeProyek.Visible = true;
            this.colKodeProyek.VisibleIndex = 0;
            this.colKodeProyek.Width = 103;
            // 
            // colNamaProyek
            // 
            this.colNamaProyek.Caption = "NamaProyek";
            this.colNamaProyek.FieldName = "NamaProyek";
            this.colNamaProyek.Name = "colNamaProyek";
            this.colNamaProyek.OptionsColumn.AllowMove = false;
            this.colNamaProyek.OptionsColumn.AllowSort = false;
            this.colNamaProyek.OptionsColumn.ReadOnly = true;
            this.colNamaProyek.Visible = true;
            this.colNamaProyek.VisibleIndex = 1;
            this.colNamaProyek.Width = 381;
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
            // groupControl1
            // 
            this.groupControl1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)));
            this.groupControl1.Controls.Add(this.aktifCheckEdit);
            this.groupControl1.Controls.Add(this.postingCheckEdit);
            this.groupControl1.Controls.Add(keteranganLabel);
            this.groupControl1.Controls.Add(this.keteranganMemoEdit);
            this.groupControl1.Controls.Add(this.namaProyekLabel);
            this.groupControl1.Controls.Add(this.namaProyekTextEdit);
            this.groupControl1.Controls.Add(this.KodeProyekLabel);
            this.groupControl1.Controls.Add(this.KodeProyekTextEdit);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit1);
            this.groupControl1.Controls.Add(idIndukLabel);
            this.groupControl1.Controls.Add(this.idIndukLookUpEdit);
            this.groupControl1.Location = new System.Drawing.Point(7, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(425, 202);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // DocProyek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 255);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.treeList1);
            this.Name = "DocProyek";
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProyekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listProyekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idIndukLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KodeProyekTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaProyekTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.postingCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource ProyekBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit idIndukLookUpEdit1;
        private DevExpress.XtraEditors.TextEdit KodeProyekTextEdit;
        private DevExpress.XtraEditors.TextEdit namaProyekTextEdit;
        private DevExpress.XtraEditors.MemoEdit keteranganMemoEdit;
        private DevExpress.XtraEditors.CheckEdit postingCheckEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private System.Windows.Forms.BindingSource listProyekBindingSource;
        private System.Windows.Forms.Label KodeProyekLabel;
        private System.Windows.Forms.Label namaProyekLabel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colKodeProyek;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colNamaProyek;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colPosting;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}
