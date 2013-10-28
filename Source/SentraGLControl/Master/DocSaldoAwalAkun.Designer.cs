using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Master
{
    partial class DocSaldoAwalAkun
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
            System.Windows.Forms.Label idAkunLabel;
            System.Windows.Forms.Label saldoAwalLabel;
            this.saldoAwalAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idAkunLookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.saldoAwalSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.saldoAwalAkunGridControl = new DevExpress.XtraGrid.GridControl();
            this.listSaldoAwalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colJenisAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNoAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNamaAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSaldoAwal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdAkun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            idAkunLabel = new System.Windows.Forms.Label();
            saldoAwalLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalAkunGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSaldoAwalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // idAkunLabel
            // 
            idAkunLabel.AutoSize = true;
            idAkunLabel.Location = new System.Drawing.Point(37, 15);
            idAkunLabel.Name = "idAkunLabel";
            idAkunLabel.Size = new System.Drawing.Size(35, 13);
            idAkunLabel.TabIndex = 1;
            idAkunLabel.Text = "Akun:";
            // 
            // saldoAwalLabel
            // 
            saldoAwalLabel.AutoSize = true;
            saldoAwalLabel.Location = new System.Drawing.Point(10, 66);
            saldoAwalLabel.Name = "saldoAwalLabel";
            saldoAwalLabel.Size = new System.Drawing.Size(63, 13);
            saldoAwalLabel.TabIndex = 3;
            saldoAwalLabel.Text = "Saldo Awal:";
            // 
            // saldoAwalAkunBindingSource
            // 
            this.saldoAwalAkunBindingSource.DataSource = typeof(SentraGL.Master.SaldoAwalAkun);
            // 
            // idAkunLookUpEdit
            // 
            this.idAkunLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.saldoAwalAkunBindingSource, "IdAkun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.idAkunLookUpEdit.Location = new System.Drawing.Point(79, 11);
            this.idAkunLookUpEdit.Name = "idAkunLookUpEdit";
            this.idAkunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idAkunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idAkunLookUpEdit.Properties.DataSource = this.listAkunBindingSource;
            this.idAkunLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idAkunLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idAkunLookUpEdit.Size = new System.Drawing.Size(122, 20);
            this.idAkunLookUpEdit.TabIndex = 2;
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.saldoAwalAkunBindingSource;
            // 
            // idAkunLookUpEdit1
            // 
            this.idAkunLookUpEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.saldoAwalAkunBindingSource, "IdAkun", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.idAkunLookUpEdit1.Location = new System.Drawing.Point(79, 37);
            this.idAkunLookUpEdit1.Name = "idAkunLookUpEdit1";
            this.idAkunLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.idAkunLookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idAkunLookUpEdit1.Properties.DataSource = this.listAkunBindingSource;
            this.idAkunLookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.idAkunLookUpEdit1.Properties.ValueMember = "IdAkun";
            this.idAkunLookUpEdit1.Size = new System.Drawing.Size(223, 20);
            this.idAkunLookUpEdit1.TabIndex = 3;
            // 
            // saldoAwalSpinEdit
            // 
            this.saldoAwalSpinEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.saldoAwalAkunBindingSource, "SaldoAwal", true));
            this.saldoAwalSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.saldoAwalSpinEdit.Location = new System.Drawing.Point(79, 63);
            this.saldoAwalSpinEdit.Name = "saldoAwalSpinEdit";
            this.saldoAwalSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.saldoAwalSpinEdit.Size = new System.Drawing.Size(122, 20);
            this.saldoAwalSpinEdit.TabIndex = 4;
            // 
            // saldoAwalAkunGridControl
            // 
            this.saldoAwalAkunGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.saldoAwalAkunGridControl.DataSource = this.listSaldoAwalBindingSource;
            this.saldoAwalAkunGridControl.Location = new System.Drawing.Point(319, 14);
            this.saldoAwalAkunGridControl.MainView = this.gridView1;
            this.saldoAwalAkunGridControl.Name = "saldoAwalAkunGridControl";
            this.saldoAwalAkunGridControl.Size = new System.Drawing.Size(457, 294);
            this.saldoAwalAkunGridControl.TabIndex = 5;
            this.saldoAwalAkunGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // listSaldoAwalBindingSource
            // 
            this.listSaldoAwalBindingSource.DataMember = "ListSaldoAwal";
            this.listSaldoAwalBindingSource.DataSource = this.saldoAwalAkunBindingSource;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colJenisAkun,
            this.colNoAkun,
            this.colNamaAkun,
            this.colSaldoAwal,
            this.colIdAkun});
            this.gridView1.GridControl = this.saldoAwalAkunGridControl;
            this.gridView1.GroupCount = 1;
            this.gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView1.GroupFormat = "Total {1}: {2}";
            this.gridView1.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SaldoAwal", null, "{0:#,##0;(#,##0);0}")});
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colJenisAkun, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colJenisAkun
            // 
            this.colJenisAkun.Caption = "JenisAkun";
            this.colJenisAkun.FieldName = "JenisAkun";
            this.colJenisAkun.Name = "colJenisAkun";
            this.colJenisAkun.OptionsColumn.FixedWidth = true;
            this.colJenisAkun.OptionsColumn.ReadOnly = true;
            this.colJenisAkun.Visible = true;
            this.colJenisAkun.VisibleIndex = 0;
            this.colJenisAkun.Width = 70;
            // 
            // colNoAkun
            // 
            this.colNoAkun.Caption = "NoAkun";
            this.colNoAkun.FieldName = "NoAkun";
            this.colNoAkun.Name = "colNoAkun";
            this.colNoAkun.OptionsColumn.AllowMove = false;
            this.colNoAkun.OptionsColumn.FixedWidth = true;
            this.colNoAkun.OptionsColumn.ReadOnly = true;
            this.colNoAkun.Visible = true;
            this.colNoAkun.VisibleIndex = 0;
            this.colNoAkun.Width = 78;
            // 
            // colNamaAkun
            // 
            this.colNamaAkun.Caption = "NamaAkun";
            this.colNamaAkun.FieldName = "NamaAkun";
            this.colNamaAkun.Name = "colNamaAkun";
            this.colNamaAkun.OptionsColumn.AllowMove = false;
            this.colNamaAkun.OptionsColumn.ReadOnly = true;
            this.colNamaAkun.Visible = true;
            this.colNamaAkun.VisibleIndex = 1;
            this.colNamaAkun.Width = 118;
            // 
            // colSaldoAwal
            // 
            this.colSaldoAwal.Caption = "SaldoAwal";
            this.colSaldoAwal.FieldName = "SaldoAwal";
            this.colSaldoAwal.Name = "colSaldoAwal";
            this.colSaldoAwal.OptionsColumn.AllowMove = false;
            this.colSaldoAwal.OptionsColumn.ReadOnly = true;
            this.colSaldoAwal.SummaryItem.DisplayFormat = "{0:#,##0;(#,##0);0}";
            this.colSaldoAwal.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colSaldoAwal.Visible = true;
            this.colSaldoAwal.VisibleIndex = 2;
            this.colSaldoAwal.Width = 80;
            // 
            // colIdAkun
            // 
            this.colIdAkun.Caption = "IdAkun";
            this.colIdAkun.FieldName = "IdAkun";
            this.colIdAkun.Name = "colIdAkun";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(701, 314);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "&Refresh";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // DocSaldoAwalAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(788, 341);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.saldoAwalAkunGridControl);
            this.Controls.Add(saldoAwalLabel);
            this.Controls.Add(this.saldoAwalSpinEdit);
            this.Controls.Add(this.idAkunLookUpEdit1);
            this.Controls.Add(idAkunLabel);
            this.Controls.Add(this.idAkunLookUpEdit);
            this.Name = "DocSaldoAwalAkun";
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalAkunGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listSaldoAwalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource saldoAwalAkunBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit1;
        private DevExpress.XtraEditors.SpinEdit saldoAwalSpinEdit;
        private DevExpress.XtraGrid.GridControl saldoAwalAkunGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colNoAkun;
        private DevExpress.XtraGrid.Columns.GridColumn colNamaAkun;
        private DevExpress.XtraGrid.Columns.GridColumn colSaldoAwal;
        private DevExpress.XtraGrid.Columns.GridColumn colJenisAkun;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.BindingSource listSaldoAwalBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colIdAkun;
    }
}
