using System.Windows.Forms;
namespace SentraWinSecurity
{
    partial class frmUserLog
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
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.clsFormLogBindingSource = new BindingSource(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.listLogBindingSource = new BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLogTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRoleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActionId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsFormLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listLogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEdit1
            // 
            this.dateEdit1.DataBindings.Add(new Binding("EditValue", this.clsFormLogBindingSource, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(60, 13);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.dateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit1.Properties.EditFormat.FormatString = "dd MMM yyyy";
            this.dateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit1.Properties.Mask.EditMask = "dd MMM yyyy";
            this.dateEdit1.Size = new System.Drawing.Size(116, 20);
            this.dateEdit1.TabIndex = 0;
            // 
            // clsFormLogBindingSource
            // 
            this.clsFormLogBindingSource.DataSource = typeof(SentraWinSecurity.clsFormLog);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(41, 13);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Tgl Mulai";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(190, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(15, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "s/d";
            // 
            // dateEdit2
            // 
            this.dateEdit2.DataBindings.Add(new Binding("EditValue", this.clsFormLogBindingSource, "EndDate", true, DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(221, 13);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit2.Properties.EditFormat.FormatString = "dd MMM yyyy";
            this.dateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit2.Properties.Mask.EditMask = "dd MMM yyyy";
            this.dateEdit2.Size = new System.Drawing.Size(116, 20);
            this.dateEdit2.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.gridControl1.DataSource = this.listLogBindingSource;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(13, 42);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(548, 305);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // listLogBindingSource
            // 
            this.listLogBindingSource.DataMember = "ListLog";
            this.listLogBindingSource.DataSource = this.clsFormLogBindingSource;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLogTime,
            this.colUserName,
            this.colRoleName,
            this.colDocumentId,
            this.colActionId,
            this.colDocumentNo,
            this.colDescription});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            // 
            // colLogTime
            // 
            this.colLogTime.Caption = "Tgl Jam Log";
            this.colLogTime.DisplayFormat.FormatString = "dd MMM yyyy HH:mm";
            this.colLogTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colLogTime.FieldName = "LogTime";
            this.colLogTime.Name = "colLogTime";
            this.colLogTime.Visible = true;
            this.colLogTime.VisibleIndex = 0;
            this.colLogTime.Width = 102;
            // 
            // colUserName
            // 
            this.colUserName.Caption = "Nama User";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 1;
            this.colUserName.Width = 91;
            // 
            // colRoleName
            // 
            this.colRoleName.Caption = "Nama Peran";
            this.colRoleName.FieldName = "RoleName";
            this.colRoleName.Name = "colRoleName";
            this.colRoleName.Visible = true;
            this.colRoleName.VisibleIndex = 2;
            this.colRoleName.Width = 101;
            // 
            // colDocumentId
            // 
            this.colDocumentId.Caption = "Jenis Dokumen";
            this.colDocumentId.FieldName = "DocumentId";
            this.colDocumentId.Name = "colDocumentId";
            this.colDocumentId.Visible = true;
            this.colDocumentId.VisibleIndex = 3;
            this.colDocumentId.Width = 105;
            // 
            // colActionId
            // 
            this.colActionId.Caption = "Jenis Tindakan";
            this.colActionId.FieldName = "ActionId";
            this.colActionId.Name = "colActionId";
            this.colActionId.Visible = true;
            this.colActionId.VisibleIndex = 4;
            this.colActionId.Width = 96;
            // 
            // colDocumentNo
            // 
            this.colDocumentNo.Caption = "No Dokumen";
            this.colDocumentNo.FieldName = "DocumentNo";
            this.colDocumentNo.Name = "colDocumentNo";
            this.colDocumentNo.Visible = true;
            this.colDocumentNo.VisibleIndex = 5;
            this.colDocumentNo.Width = 97;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Keterangan";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 6;
            this.colDescription.Width = 110;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.labelControl3.Location = new System.Drawing.Point(11, 361);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(145, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Otomatis Bersihkan Log Setiap";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.labelControl4.Location = new System.Drawing.Point(221, 361);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Bulan";
            // 
            // spinEdit1
            // 
            this.spinEdit1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.spinEdit1.DataBindings.Add(new Binding("EditValue", this.clsFormLogBindingSource, "LogAge", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(162, 358);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(51, 20);
            this.spinEdit1.TabIndex = 5;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8});
            this.barManager1.MaxItemId = 9;
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Cetak";
            this.barButtonItem6.Glyph = global::SentraWinSecurity.Properties.Resources.printer;
            this.barButtonItem6.Id = 6;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Refresh";
            this.barButtonItem7.Glyph = global::SentraWinSecurity.Properties.Resources.document_refresh;
            this.barButtonItem7.Id = 7;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "Copy";
            this.barButtonItem8.Glyph = global::SentraWinSecurity.Properties.Resources.copy;
            this.barButtonItem8.Id = 8;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = global::SentraWinSecurity.Properties.Resources.printer;
            this.simpleButton1.Location = new System.Drawing.Point(348, 11);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "&Cetak";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = global::SentraWinSecurity.Properties.Resources.document_refresh;
            this.simpleButton2.Location = new System.Drawing.Point(429, 11);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "&Refresh";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // frmUserLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 398);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateEdit2);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmUserLog";
            this.Text = "Log Aktivitas User";
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clsFormLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listLogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private System.Windows.Forms.BindingSource clsFormLogBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colLogTime;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colRoleName;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentId;
        private DevExpress.XtraGrid.Columns.GridColumn colActionId;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private System.Windows.Forms.BindingSource listLogBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}