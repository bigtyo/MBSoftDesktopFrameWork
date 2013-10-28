using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocMataUang
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
            Label kodeMataUangLabel;
            Label namaMataUangLabel;
            Label idAkunLabaRugiSelisihKursLabel;
            this.kodeMataUangTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.mataUangBindingSource = new BindingSource(this.components);
            this.namaMataUangTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.idAkunLabaRugiSelisihKursLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listAkunBindingSource = new BindingSource(this.components);
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            kodeMataUangLabel = new Label();
            namaMataUangLabel = new Label();
            idAkunLabaRugiSelisihKursLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mataUangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaMataUangTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLabaRugiSelisihKursLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // kodeMataUangLabel
            // 
            kodeMataUangLabel.AutoSize = true;
            kodeMataUangLabel.Location = new System.Drawing.Point(78, 17);
            kodeMataUangLabel.Name = "kodeMataUangLabel";
            kodeMataUangLabel.Size = new System.Drawing.Size(90, 13);
            kodeMataUangLabel.TabIndex = 1;
            kodeMataUangLabel.Text = "Kode Mata Uang:";
            // 
            // namaMataUangLabel
            // 
            namaMataUangLabel.AutoSize = true;
            namaMataUangLabel.Location = new System.Drawing.Point(75, 43);
            namaMataUangLabel.Name = "namaMataUangLabel";
            namaMataUangLabel.Size = new System.Drawing.Size(93, 13);
            namaMataUangLabel.TabIndex = 2;
            namaMataUangLabel.Text = "Nama Mata Uang:";
            // 
            // idAkunLabaRugiSelisihKursLabel
            // 
            idAkunLabaRugiSelisihKursLabel.AutoSize = true;
            idAkunLabaRugiSelisihKursLabel.Location = new System.Drawing.Point(27, 69);
            idAkunLabaRugiSelisihKursLabel.Name = "idAkunLabaRugiSelisihKursLabel";
            idAkunLabaRugiSelisihKursLabel.Size = new System.Drawing.Size(141, 13);
            idAkunLabaRugiSelisihKursLabel.TabIndex = 5;
            idAkunLabaRugiSelisihKursLabel.Text = "Akun Laba Rugi Selisih Kurs:";
            // 
            // kodeMataUangTextEdit
            // 
            this.kodeMataUangTextEdit.DataBindings.Add(new Binding("EditValue", this.mataUangBindingSource, "KodeMataUang", true, DataSourceUpdateMode.OnPropertyChanged));
            this.kodeMataUangTextEdit.Location = new System.Drawing.Point(174, 14);
            this.kodeMataUangTextEdit.Name = "kodeMataUangTextEdit";
            this.kodeMataUangTextEdit.Size = new System.Drawing.Size(47, 20);
            this.kodeMataUangTextEdit.TabIndex = 0;
            // 
            // mataUangBindingSource
            // 
            this.mataUangBindingSource.DataSource = typeof(SentraGL.Master.MataUang);
            // 
            // namaMataUangTextEdit
            // 
            this.namaMataUangTextEdit.DataBindings.Add(new Binding("EditValue", this.mataUangBindingSource, "NamaMataUang", true));
            this.namaMataUangTextEdit.Location = new System.Drawing.Point(174, 40);
            this.namaMataUangTextEdit.Name = "namaMataUangTextEdit";
            this.namaMataUangTextEdit.Size = new System.Drawing.Size(152, 20);
            this.namaMataUangTextEdit.TabIndex = 1;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.mataUangBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(227, 15);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 5;
            this.aktifCheckEdit.TabStop = false;
            // 
            // idAkunLabaRugiSelisihKursLookUpEdit
            // 
            this.idAkunLabaRugiSelisihKursLookUpEdit.DataBindings.Add(new Binding("EditValue", this.mataUangBindingSource, "IdAkunLabaRugiSelisihKurs", true));
            this.idAkunLabaRugiSelisihKursLookUpEdit.Location = new System.Drawing.Point(174, 66);
            this.idAkunLabaRugiSelisihKursLookUpEdit.Name = "idAkunLabaRugiSelisihKursLookUpEdit";
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.DataSource = this.listAkunBindingSource;
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.NullText = "";
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.PopupWidth = 350;
            this.idAkunLabaRugiSelisihKursLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idAkunLabaRugiSelisihKursLookUpEdit.Size = new System.Drawing.Size(152, 20);
            this.idAkunLabaRugiSelisihKursLookUpEdit.TabIndex = 2;
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.mataUangBindingSource;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.DataBindings.Add(new Binding("EditValue", this.mataUangBindingSource, "IdAkunLabaRugiSelisihKurs", true));
            this.lookUpEdit1.Location = new System.Drawing.Point(174, 92);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut((Keys.Control | Keys.R)), "Refresh Data (Ctrl+R)")});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 40),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.lookUpEdit1.Properties.DataSource = this.listAkunBindingSource;
            this.lookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.PopupWidth = 350;
            this.lookUpEdit1.Properties.ValueMember = "IdAkun";
            this.lookUpEdit1.Size = new System.Drawing.Size(273, 20);
            this.lookUpEdit1.TabIndex = 3;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(372, 11);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // DocMataUang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(467, 128);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(idAkunLabaRugiSelisihKursLabel);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.idAkunLabaRugiSelisihKursLookUpEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(namaMataUangLabel);
            this.Controls.Add(this.namaMataUangTextEdit);
            this.Controls.Add(kodeMataUangLabel);
            this.Controls.Add(this.kodeMataUangTextEdit);
            this.Name = "DocMataUang";
            ((System.ComponentModel.ISupportInitialize)(this.kodeMataUangTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mataUangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaMataUangTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLabaRugiSelisihKursLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource mataUangBindingSource;
        private DevExpress.XtraEditors.TextEdit kodeMataUangTextEdit;
        private DevExpress.XtraEditors.TextEdit namaMataUangTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.LookUpEdit idAkunLabaRugiSelisihKursLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}
