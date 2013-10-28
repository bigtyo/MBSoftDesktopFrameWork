using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Document
{
    partial class DocKursHarian
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
            Label tglKursLabel;
            Label kodeMataUangLabel;
            Label nilaiTukarLabel;
            this.tglKursDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.kursHarianBindingSource = new BindingSource(this.components);
            this.kodeMataUangLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listMataUangBindingSource = new BindingSource(this.components);
            this.nilaiTukarSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.listKursHarianBindingSource = new BindingSource(this.components);
            this.kursHarianGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colKodeMataUang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNilaiTukar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            tglKursLabel = new Label();
            kodeMataUangLabel = new Label();
            nilaiTukarLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.tglKursDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursHarianBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKursHarianBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursHarianGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tglKursLabel
            // 
            tglKursLabel.AutoSize = true;
            tglKursLabel.Location = new System.Drawing.Point(25, 15);
            tglKursLabel.Name = "tglKursLabel";
            tglKursLabel.Size = new System.Drawing.Size(49, 13);
            tglKursLabel.TabIndex = 1;
            tglKursLabel.Text = "Tgl Kurs:";
            // 
            // kodeMataUangLabel
            // 
            kodeMataUangLabel.AutoSize = true;
            kodeMataUangLabel.Location = new System.Drawing.Point(11, 42);
            kodeMataUangLabel.Name = "kodeMataUangLabel";
            kodeMataUangLabel.Size = new System.Drawing.Size(63, 13);
            kodeMataUangLabel.TabIndex = 2;
            kodeMataUangLabel.Text = "Mata Uang:";
            // 
            // nilaiTukarLabel
            // 
            nilaiTukarLabel.AutoSize = true;
            nilaiTukarLabel.Location = new System.Drawing.Point(14, 67);
            nilaiTukarLabel.Name = "nilaiTukarLabel";
            nilaiTukarLabel.Size = new System.Drawing.Size(60, 13);
            nilaiTukarLabel.TabIndex = 4;
            nilaiTukarLabel.Text = "Nilai Tukar:";
            // 
            // tglKursDateEdit
            // 
            this.tglKursDateEdit.DataBindings.Add(new Binding("EditValue", this.kursHarianBindingSource, "TglKurs", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglKursDateEdit.EditValue = null;
            this.tglKursDateEdit.Location = new System.Drawing.Point(80, 12);
            this.tglKursDateEdit.Name = "tglKursDateEdit";
            this.tglKursDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglKursDateEdit.Size = new System.Drawing.Size(112, 20);
            this.tglKursDateEdit.TabIndex = 2;
            // 
            // kursHarianBindingSource
            // 
            this.kursHarianBindingSource.DataSource = typeof(SentraGL.Document.KursHarian);
            // 
            // kodeMataUangLookUpEdit
            // 
            this.kodeMataUangLookUpEdit.DataBindings.Add(new Binding("EditValue", this.kursHarianBindingSource, "KodeMataUang", true, DataSourceUpdateMode.OnPropertyChanged));
            this.kodeMataUangLookUpEdit.Location = new System.Drawing.Point(80, 38);
            this.kodeMataUangLookUpEdit.Name = "kodeMataUangLookUpEdit";
            this.kodeMataUangLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.kodeMataUangLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("KodeMataUang", "Mata Uang", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaMataUang", "Nama Mata Uang", 70)});
            this.kodeMataUangLookUpEdit.Properties.DataSource = this.listMataUangBindingSource;
            this.kodeMataUangLookUpEdit.Properties.DisplayMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Properties.ValueMember = "KodeMataUang";
            this.kodeMataUangLookUpEdit.Size = new System.Drawing.Size(112, 20);
            this.kodeMataUangLookUpEdit.TabIndex = 3;
            // 
            // listMataUangBindingSource
            // 
            this.listMataUangBindingSource.DataMember = "ListMataUang";
            this.listMataUangBindingSource.DataSource = this.kursHarianBindingSource;
            // 
            // nilaiTukarSpinEdit
            // 
            this.nilaiTukarSpinEdit.DataBindings.Add(new Binding("EditValue", this.kursHarianBindingSource, "NilaiTukar", true));
            this.nilaiTukarSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.nilaiTukarSpinEdit.Location = new System.Drawing.Point(80, 64);
            this.nilaiTukarSpinEdit.Name = "nilaiTukarSpinEdit";
            this.nilaiTukarSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.nilaiTukarSpinEdit.Size = new System.Drawing.Size(112, 20);
            this.nilaiTukarSpinEdit.TabIndex = 5;
            // 
            // listKursHarianBindingSource
            // 
            this.listKursHarianBindingSource.DataMember = "ListKursHarian";
            this.listKursHarianBindingSource.DataSource = this.kursHarianBindingSource;
            // 
            // kursHarianGridControl
            // 
            this.kursHarianGridControl.DataSource = this.listKursHarianBindingSource;
            this.kursHarianGridControl.EmbeddedNavigator.Name = "";
            this.kursHarianGridControl.Location = new System.Drawing.Point(211, 12);
            this.kursHarianGridControl.MainView = this.gridView1;
            this.kursHarianGridControl.Name = "kursHarianGridControl";
            this.kursHarianGridControl.Size = new System.Drawing.Size(187, 220);
            this.kursHarianGridControl.TabIndex = 6;
            this.kursHarianGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colKodeMataUang,
            this.colNilaiTukar});
            this.gridView1.GridControl = this.kursHarianGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowFilter = false;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.KeyUp += new KeyEventHandler(this.gridView1_KeyUp);
            this.gridView1.MouseUp += new MouseEventHandler(this.gridView1_MouseUp);
            // 
            // colKodeMataUang
            // 
            this.colKodeMataUang.Caption = "MataUang";
            this.colKodeMataUang.FieldName = "KodeMataUang";
            this.colKodeMataUang.Name = "colKodeMataUang";
            this.colKodeMataUang.OptionsColumn.FixedWidth = true;
            this.colKodeMataUang.Visible = true;
            this.colKodeMataUang.VisibleIndex = 0;
            // 
            // colNilaiTukar
            // 
            this.colNilaiTukar.Caption = "NilaiTukar";
            this.colNilaiTukar.FieldName = "NilaiTukar";
            this.colNilaiTukar.Name = "colNilaiTukar";
            this.colNilaiTukar.Visible = true;
            this.colNilaiTukar.VisibleIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(323, 238);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 7;
            this.simpleButton1.Text = "&Refresh";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmKursHarian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(412, 345);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.kursHarianGridControl);
            this.Controls.Add(nilaiTukarLabel);
            this.Controls.Add(this.nilaiTukarSpinEdit);
            this.Controls.Add(kodeMataUangLabel);
            this.Controls.Add(this.kodeMataUangLookUpEdit);
            this.Controls.Add(tglKursLabel);
            this.Controls.Add(this.tglKursDateEdit);
            this.Name = "frmKursHarian";
            ((System.ComponentModel.ISupportInitialize)(this.tglKursDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursHarianBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMataUangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nilaiTukarSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listKursHarianBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kursHarianGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource kursHarianBindingSource;
        private DevExpress.XtraEditors.DateEdit tglKursDateEdit;
        private DevExpress.XtraEditors.LookUpEdit kodeMataUangLookUpEdit;
        private BindingSource listMataUangBindingSource;
        private DevExpress.XtraEditors.SpinEdit nilaiTukarSpinEdit;
        private System.Windows.Forms.BindingSource listKursHarianBindingSource;
        private DevExpress.XtraGrid.GridControl kursHarianGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colKodeMataUang;
        private DevExpress.XtraGrid.Columns.GridColumn colNilaiTukar;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
