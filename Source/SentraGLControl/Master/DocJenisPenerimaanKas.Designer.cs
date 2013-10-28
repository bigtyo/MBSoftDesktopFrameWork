using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocJenisPenerimaanKas
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
            Label jenisPenerimaanLabel;
            Label keteranganLabel;
            Label jenisArusKasLabel;
            Label idAkunLabel;
            this.jenisPenerimaanTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.jenisPenerimaanKasBindingSource = new BindingSource(this.components);
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.internalCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.idAkunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listAkunBindingSource = new BindingSource(this.components);
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            jenisPenerimaanLabel = new Label();
            keteranganLabel = new Label();
            jenisArusKasLabel = new Label();
            idAkunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPenerimaanTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPenerimaanKasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // jenisPenerimaanLabel
            // 
            jenisPenerimaanLabel.AutoSize = true;
            jenisPenerimaanLabel.Location = new System.Drawing.Point(20, 19);
            jenisPenerimaanLabel.Name = "jenisPenerimaanLabel";
            jenisPenerimaanLabel.Size = new System.Drawing.Size(94, 13);
            jenisPenerimaanLabel.TabIndex = 1;
            jenisPenerimaanLabel.Text = "Jenis Penerimaan:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(47, 44);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 2;
            keteranganLabel.Text = "Keterangan:";
            // 
            // jenisArusKasLabel
            // 
            jenisArusKasLabel.AutoSize = true;
            jenisArusKasLabel.Location = new System.Drawing.Point(34, 70);
            jenisArusKasLabel.Name = "jenisArusKasLabel";
            jenisArusKasLabel.Size = new System.Drawing.Size(80, 13);
            jenisArusKasLabel.TabIndex = 5;
            jenisArusKasLabel.Text = "Jenis Arus Kas:";
            // 
            // idAkunLabel
            // 
            idAkunLabel.AutoSize = true;
            idAkunLabel.Location = new System.Drawing.Point(79, 95);
            idAkunLabel.Name = "idAkunLabel";
            idAkunLabel.Size = new System.Drawing.Size(35, 13);
            idAkunLabel.TabIndex = 8;
            idAkunLabel.Text = "Akun:";
            // 
            // jenisPenerimaanTextEdit
            // 
            this.jenisPenerimaanTextEdit.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "JenisPenerimaan", true));
            this.jenisPenerimaanTextEdit.Location = new System.Drawing.Point(120, 15);
            this.jenisPenerimaanTextEdit.Name = "jenisPenerimaanTextEdit";
            this.jenisPenerimaanTextEdit.Size = new System.Drawing.Size(210, 20);
            this.jenisPenerimaanTextEdit.TabIndex = 0;
            // 
            // jenisPenerimaanKasBindingSource
            // 
            this.jenisPenerimaanKasBindingSource.DataSource = typeof(SentraGL.Master.JenisPenerimaanKas);
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(120, 41);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(374, 20);
            this.keteranganTextEdit.TabIndex = 2;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(342, 16);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(52, 19);
            this.aktifCheckEdit.TabIndex = 1;
            this.aktifCheckEdit.TabStop = false;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "JenisArusKas", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(120, 66);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(123, 20);
            this.comboBoxEdit1.TabIndex = 3;
            // 
            // internalCheckEdit
            // 
            this.internalCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "Internal", true));
            this.internalCheckEdit.Location = new System.Drawing.Point(342, 67);
            this.internalCheckEdit.Name = "internalCheckEdit";
            this.internalCheckEdit.Properties.Caption = "Internal";
            this.internalCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.internalCheckEdit.TabIndex = 4;
            this.internalCheckEdit.TabStop = false;
            // 
            // idAkunLookUpEdit
            // 
            this.idAkunLookUpEdit.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "IdAkun", true, DataSourceUpdateMode.OnPropertyChanged));
            this.idAkunLookUpEdit.Location = new System.Drawing.Point(120, 92);
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
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.jenisPenerimaanKasBindingSource;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.DataBindings.Add(new Binding("EditValue", this.jenisPenerimaanKasBindingSource, "IdAkun", true, DataSourceUpdateMode.OnPropertyChanged));
            this.lookUpEdit1.Location = new System.Drawing.Point(249, 92);
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
            // DocJenisPenerimaanKas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 125);
            this.Controls.Add(idAkunLabel);
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.idAkunLookUpEdit);
            this.Controls.Add(this.internalCheckEdit);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(jenisArusKasLabel);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganTextEdit);
            this.Controls.Add(jenisPenerimaanLabel);
            this.Controls.Add(this.jenisPenerimaanTextEdit);
            this.Name = "DocJenisPenerimaanKas";
            this.WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.jenisPenerimaanTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisPenerimaanKasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.idAkunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource jenisPenerimaanKasBindingSource;
        private DevExpress.XtraEditors.TextEdit jenisPenerimaanTextEdit;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.CheckEdit internalCheckEdit;
        private DevExpress.XtraEditors.LookUpEdit idAkunLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
    }
}
