using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocJenisPengeluaranKas
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
            Label idAkunLabel;
            this.jenisPengeluaranLabel = new Label();
            this.keteranganLabel = new Label();
            this.jenisArusKasLabel = new Label();
            this.jenisPengeluaranTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.jenisPengeluaranKasBindingSource = new BindingSource(this.components);
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.internalCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.listAkunBindingSource = new BindingSource(this.components);
            this.idAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            idAkunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPengeluaranTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPengeluaranKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // idAkunLabel
            // 
            idAkunLabel.AutoSize = true;
            idAkunLabel.Location = new System.Drawing.Point(80, 95);
            idAkunLabel.Name = "idAkunLabel";
            idAkunLabel.Size = new System.Drawing.Size(35, 13);
            idAkunLabel.TabIndex = 11;
            idAkunLabel.Text = "Akun:";
            // 
            // jenisPengeluaranLabel
            // 
            this.jenisPengeluaranLabel.AutoSize = true;
            this.jenisPengeluaranLabel.Location = new System.Drawing.Point(20, 19);
            this.jenisPengeluaranLabel.Name = "jenisPengeluaranLabel";
            this.jenisPengeluaranLabel.Size = new System.Drawing.Size(98, 13);
            this.jenisPengeluaranLabel.TabIndex = 1;
            this.jenisPengeluaranLabel.Text = "Jenis Pengeluaran:";
            // 
            // keteranganLabel
            // 
            this.keteranganLabel.AutoSize = true;
            this.keteranganLabel.Location = new System.Drawing.Point(47, 44);
            this.keteranganLabel.Name = "keteranganLabel";
            this.keteranganLabel.Size = new System.Drawing.Size(67, 13);
            this.keteranganLabel.TabIndex = 2;
            this.keteranganLabel.Text = "Keterangan:";
            // 
            // jenisArusKasLabel
            // 
            this.jenisArusKasLabel.AutoSize = true;
            this.jenisArusKasLabel.Location = new System.Drawing.Point(34, 70);
            this.jenisArusKasLabel.Name = "jenisArusKasLabel";
            this.jenisArusKasLabel.Size = new System.Drawing.Size(80, 13);
            this.jenisArusKasLabel.TabIndex = 5;
            this.jenisArusKasLabel.Text = "Jenis Arus Kas:";
            // 
            // jenisPengeluaranTextEdit
            // 
            this.jenisPengeluaranTextEdit.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "JenisPengeluaran", true));
            this.jenisPengeluaranTextEdit.Location = new System.Drawing.Point(120, 15);
            this.jenisPengeluaranTextEdit.Name = "jenisPengeluaranTextEdit";
            this.jenisPengeluaranTextEdit.Size = new System.Drawing.Size(210, 20);
            this.jenisPengeluaranTextEdit.TabIndex = 0;
            // 
            // jenisPengeluaranKasBindingSource
            // 
            this.jenisPengeluaranKasBindingSource.DataSource = typeof(SentraGL.Master.JenisPengeluaranKas);
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(120, 41);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(375, 20);
            this.keteranganTextEdit.TabIndex = 2;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(342, 16);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(52, 19);
            this.aktifCheckEdit.TabIndex = 1;
            this.aktifCheckEdit.TabStop = false;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "JenisArusKas", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(120, 66);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(123, 20);
            this.comboBoxEdit1.TabIndex = 3;
            // 
            // internalCheckEdit
            // 
            this.internalCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "Internal", true));
            this.internalCheckEdit.Location = new System.Drawing.Point(342, 67);
            this.internalCheckEdit.Name = "internalCheckEdit";
            this.internalCheckEdit.Properties.Caption = "Internal";
            this.internalCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.internalCheckEdit.TabIndex = 4;
            this.internalCheckEdit.TabStop = false;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "IdAkun", true, DataSourceUpdateMode.OnPropertyChanged));
            this.lookUpEdit1.Location = new System.Drawing.Point(250, 92);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.lookUpEdit1.Properties.DataSource = this.listAkunBindingSource;
            this.lookUpEdit1.Properties.DisplayMember = "NamaAkun";
            this.lookUpEdit1.Properties.ValueMember = "IdAkun";
            this.lookUpEdit1.Size = new System.Drawing.Size(245, 20);
            this.lookUpEdit1.TabIndex = 6;
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.jenisPengeluaranKasBindingSource;
            // 
            // idAkunLookUpEdit
            // 
            this.idAkunLookUpEdit.DataBindings.Add(new Binding("EditValue", this.jenisPengeluaranKasBindingSource, "IdAkun", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idAkunLookUpEdit.Location = new System.Drawing.Point(121, 92);
            this.idAkunLookUpEdit.Name = "idAkunLookUpEdit";
            this.idAkunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.idAkunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.idAkunLookUpEdit.Properties.DataSource = this.listAkunBindingSource;
            this.idAkunLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.idAkunLookUpEdit.Properties.ValueMember = "IdAkun";
            this.idAkunLookUpEdit.Size = new System.Drawing.Size(123, 20);
            this.idAkunLookUpEdit.TabIndex = 5;
            // 
            // frmJenisPengeluaranKas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 124);
            this.Controls.Add(idAkunLabel);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.idAkunLookUpEdit);
            this.Controls.Add(this.internalCheckEdit);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.jenisArusKasLabel);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(this.keteranganLabel);
            this.Controls.Add(this.keteranganTextEdit);
            this.Controls.Add(this.jenisPengeluaranLabel);
            this.Controls.Add(this.jenisPengeluaranTextEdit);
            this.Name = "frmJenisPengeluaranKas";
            ((System.ComponentModel.ISupportInitialize)(this.jenisPengeluaranTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPengeluaranKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource jenisPengeluaranKasBindingSource;
        private DevExpress.XtraEditors.TextEdit jenisPengeluaranTextEdit;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.CheckEdit internalCheckEdit;
        private System.Windows.Forms.Label jenisPengeluaranLabel;
        private System.Windows.Forms.Label keteranganLabel;
        private System.Windows.Forms.Label jenisArusKasLabel;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
    }
}
