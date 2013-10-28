using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Master
{
    partial class DocNilaiTukarSaldoAwal
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
            Label kodeMataUangLabel;
            Label nilaiTukarLabel;
            this.nilaiTukarSaldoAwalBindingSource = new BindingSource(this.components);
            this.kodeMataUangLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listMataUangBindingSource = new BindingSource(this.components);
            this.nilaiTukarSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.listNilaiTukarSaldoAwalBindingSource = new BindingSource(this.components);
            this.nilaiTukarSaldoAwalGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKodeMataUang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNilaiTukar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            kodeMataUangLabel = new Label();
            nilaiTukarLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSaldoAwalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listNilaiTukarSaldoAwalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSaldoAwalGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // kodeMataUangLabel
            // 
            kodeMataUangLabel.AutoSize = true;
            kodeMataUangLabel.Location = new System.Drawing.Point(13, 15);
            kodeMataUangLabel.Name = "kodeMataUangLabel";
            kodeMataUangLabel.Size = new System.Drawing.Size(90, 13);
            kodeMataUangLabel.TabIndex = 0;
            kodeMataUangLabel.Text = "Kode Mata Uang:";
            // 
            // nilaiTukarLabel
            // 
            nilaiTukarLabel.AutoSize = true;
            nilaiTukarLabel.Location = new System.Drawing.Point(43, 41);
            nilaiTukarLabel.Name = "nilaiTukarLabel";
            nilaiTukarLabel.Size = new System.Drawing.Size(60, 13);
            nilaiTukarLabel.TabIndex = 2;
            nilaiTukarLabel.Text = "Nilai Tukar:";
            // 
            // nilaiTukarSaldoAwalBindingSource
            // 
            this.nilaiTukarSaldoAwalBindingSource.DataSource = typeof(SentraGL.Master.NilaiTukarSaldoAwal);
            // 
            // kodeMataUangLookUpEdit
            // 
            this.kodeMataUangLookUpEdit.DataBindings.Add(new Binding("EditValue", this.nilaiTukarSaldoAwalBindingSource, "KodeMataUang", true, DataSourceUpdateMode.OnPropertyChanged));
            this.kodeMataUangLookUpEdit.Location = new System.Drawing.Point(109, 12);
            this.kodeMataUangLookUpEdit.Name = "kodeMataUangLookUpEdit";
            this.kodeMataUangLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.kodeMataUangLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeMataUang", "Kode", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaMataUang", "Mata Uang", 80)});
            this.kodeMataUangLookUpEdit.Properties.DataSource = this.listMataUangBindingSource;
            this.kodeMataUangLookUpEdit.Properties.DisplayMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Properties.ValueMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Size = new System.Drawing.Size(100, 20);
            this.kodeMataUangLookUpEdit.TabIndex = 1;
            // 
            // listMataUangBindingSource
            // 
            this.listMataUangBindingSource.DataMember = "ListMataUang";
            this.listMataUangBindingSource.DataSource = this.nilaiTukarSaldoAwalBindingSource;
            // 
            // nilaiTukarSpinEdit
            // 
            this.nilaiTukarSpinEdit.DataBindings.Add(new Binding("EditValue", this.nilaiTukarSaldoAwalBindingSource, "NilaiTukar", true));
            this.nilaiTukarSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nilaiTukarSpinEdit.Location = new System.Drawing.Point(109, 38);
            this.nilaiTukarSpinEdit.Name = "nilaiTukarSpinEdit";
            this.nilaiTukarSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nilaiTukarSpinEdit.Size = new System.Drawing.Size(100, 20);
            this.nilaiTukarSpinEdit.TabIndex = 3;
            // 
            // listNilaiTukarSaldoAwalBindingSource
            // 
            this.listNilaiTukarSaldoAwalBindingSource.DataMember = "ListNilaiTukarSaldoAwal";
            this.listNilaiTukarSaldoAwalBindingSource.DataSource = this.nilaiTukarSaldoAwalBindingSource;
            // 
            // nilaiTukarSaldoAwalGridControl
            // 
            this.nilaiTukarSaldoAwalGridControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.nilaiTukarSaldoAwalGridControl.DataSource = this.listNilaiTukarSaldoAwalBindingSource;
            this.nilaiTukarSaldoAwalGridControl.EmbeddedNavigator.Name = "";
            this.nilaiTukarSaldoAwalGridControl.Location = new System.Drawing.Point(224, 12);
            this.nilaiTukarSaldoAwalGridControl.MainView = this.gridView1;
            this.nilaiTukarSaldoAwalGridControl.Name = "nilaiTukarSaldoAwalGridControl";
            this.nilaiTukarSaldoAwalGridControl.Size = new System.Drawing.Size(332, 220);
            this.nilaiTukarSaldoAwalGridControl.TabIndex = 4;
            this.nilaiTukarSaldoAwalGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKodeMataUang,
            this.colNilaiTukar});
            this.gridView1.GridControl = this.nilaiTukarSaldoAwalGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colKodeMataUang
            // 
            this.colKodeMataUang.Caption = "KodeMataUang";
            this.colKodeMataUang.FieldName = "KodeMataUang";
            this.colKodeMataUang.Name = "colKodeMataUang";
            this.colKodeMataUang.Visible = true;
            this.colKodeMataUang.VisibleIndex = 0;
            this.colKodeMataUang.Width = 112;
            // 
            // colNilaiTukar
            // 
            this.colNilaiTukar.Caption = "NilaiTukar";
            this.colNilaiTukar.FieldName = "NilaiTukar";
            this.colNilaiTukar.Name = "colNilaiTukar";
            this.colNilaiTukar.Visible = true;
            this.colNilaiTukar.VisibleIndex = 1;
            this.colNilaiTukar.Width = 117;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(481, 238);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "&Refresh";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // DocNilaiTukarSaldoAwal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(568, 264);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.nilaiTukarSaldoAwalGridControl);
            this.Controls.Add(nilaiTukarLabel);
            this.Controls.Add(this.nilaiTukarSpinEdit);
            this.Controls.Add(kodeMataUangLabel);
            this.Controls.Add(this.kodeMataUangLookUpEdit);
            this.Name = "DocNilaiTukarSaldoAwal";
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSaldoAwalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listNilaiTukarSaldoAwalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSaldoAwalGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource nilaiTukarSaldoAwalBindingSource;
        private DevExpress.XtraEditors.LookUpEdit kodeMataUangLookUpEdit;
        private System.Windows.Forms.BindingSource listMataUangBindingSource;
        private DevExpress.XtraEditors.SpinEdit nilaiTukarSpinEdit;
        private System.Windows.Forms.BindingSource listNilaiTukarSaldoAwalBindingSource;
        private DevExpress.XtraGrid.GridControl nilaiTukarSaldoAwalGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colKodeMataUang;
        private DevExpress.XtraGrid.Columns.GridColumn colNilaiTukar;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
