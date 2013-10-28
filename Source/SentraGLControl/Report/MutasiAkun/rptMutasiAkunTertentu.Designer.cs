using System.Windows.Forms;
namespace SentraGL.Report.MutasiAkun
{
    partial class rptMutasiAkunTertentu
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
            Label tglNeraca2Label;
            Label tglNeraca1Label;
            Label akunLabel;
            Label saldoAwalLabel;
            Label saldoAkhirLabel;
            this.tglNeraca2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.mutasiAkunTertentuBindingSource = new BindingSource(this.components);
            this.tglNeraca1DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.akunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.listAkunBindingSource = new BindingSource(this.components);
            this.saldoAwalSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.saldoAkhirSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tglNeraca2Label = new Label();
            tglNeraca1Label = new Label();
            akunLabel = new Label();
            saldoAwalLabel = new Label();
            saldoAkhirLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutasiAkunTertentuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAkhirSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tglNeraca2Label
            // 
            tglNeraca2Label.AutoSize = true;
            tglNeraca2Label.Location = new System.Drawing.Point(216, 16);
            tglNeraca2Label.Name = "tglNeraca2Label";
            tglNeraca2Label.Size = new System.Drawing.Size(52, 13);
            tglNeraca2Label.TabIndex = 21;
            tglNeraca2Label.Text = "Tgl Akhir:";
            // 
            // tglNeraca1Label
            // 
            tglNeraca1Label.AutoSize = true;
            tglNeraca1Label.Location = new System.Drawing.Point(28, 15);
            tglNeraca1Label.Name = "tglNeraca1Label";
            tglNeraca1Label.Size = new System.Drawing.Size(51, 13);
            tglNeraca1Label.TabIndex = 20;
            tglNeraca1Label.Text = "Tgl Awal:";
            // 
            // akunLabel
            // 
            akunLabel.AutoSize = true;
            akunLabel.Location = new System.Drawing.Point(392, 15);
            akunLabel.Name = "akunLabel";
            akunLabel.Size = new System.Drawing.Size(35, 13);
            akunLabel.TabIndex = 21;
            akunLabel.Text = "Akun:";
            // 
            // saldoAwalLabel
            // 
            saldoAwalLabel.AutoSize = true;
            saldoAwalLabel.Location = new System.Drawing.Point(16, 51);
            saldoAwalLabel.Name = "saldoAwalLabel";
            saldoAwalLabel.Size = new System.Drawing.Size(63, 13);
            saldoAwalLabel.TabIndex = 22;
            saldoAwalLabel.Text = "Saldo Awal:";
            // 
            // saldoAkhirLabel
            // 
            saldoAkhirLabel.AutoSize = true;
            saldoAkhirLabel.Location = new System.Drawing.Point(257, 51);
            saldoAkhirLabel.Name = "saldoAkhirLabel";
            saldoAkhirLabel.Size = new System.Drawing.Size(64, 13);
            saldoAkhirLabel.TabIndex = 23;
            saldoAkhirLabel.Text = "Saldo Akhir:";
            // 
            // tglNeraca2DateEdit
            // 
            this.tglNeraca2DateEdit.DataBindings.Add(new Binding("EditValue", this.mutasiAkunTertentuBindingSource, "TglAkhir", true));
            this.tglNeraca2DateEdit.EditValue = null;
            this.tglNeraca2DateEdit.Location = new System.Drawing.Point(274, 12);
            this.tglNeraca2DateEdit.Name = "tglNeraca2DateEdit";
            this.tglNeraca2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca2DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeraca2DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeraca2DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeraca2DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca2DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca2DateEdit.TabIndex = 19;
            // 
            // mutasiAkunTertentuBindingSource
            // 
            this.mutasiAkunTertentuBindingSource.DataSource = typeof(SentraGL.Report.MutasiAkun.MutasiAkunTertentu);
            // 
            // tglNeraca1DateEdit
            // 
            this.tglNeraca1DateEdit.DataBindings.Add(new Binding("EditValue", this.mutasiAkunTertentuBindingSource, "TglAwal", true));
            this.tglNeraca1DateEdit.EditValue = null;
            this.tglNeraca1DateEdit.Location = new System.Drawing.Point(85, 13);
            this.tglNeraca1DateEdit.Name = "tglNeraca1DateEdit";
            this.tglNeraca1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca1DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeraca1DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeraca1DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeraca1DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca1DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca1DateEdit.TabIndex = 18;
            // 
            // akunLookUpEdit
            // 
            this.akunLookUpEdit.DataBindings.Add(new Binding("EditValue", this.mutasiAkunTertentuBindingSource, "Akun", true));
            this.akunLookUpEdit.Location = new System.Drawing.Point(433, 12);
            this.akunLookUpEdit.Name = "akunLookUpEdit";
            this.akunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Redo)});
            this.akunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.akunLookUpEdit.Properties.DataSource = this.listAkunBindingSource;
            this.akunLookUpEdit.Properties.DisplayMember = "NamaAkun";
            this.akunLookUpEdit.Properties.ValueMember = "IdAkun";
            this.akunLookUpEdit.Size = new System.Drawing.Size(162, 20);
            this.akunLookUpEdit.TabIndex = 22;
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.mutasiAkunTertentuBindingSource;
            // 
            // saldoAwalSpinEdit
            // 
            this.saldoAwalSpinEdit.DataBindings.Add(new Binding("EditValue", this.mutasiAkunTertentuBindingSource, "SaldoAwal", true));
            this.saldoAwalSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.saldoAwalSpinEdit.Location = new System.Drawing.Point(85, 48);
            this.saldoAwalSpinEdit.Name = "saldoAwalSpinEdit";
            this.saldoAwalSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.saldoAwalSpinEdit.Properties.ReadOnly = true;
            this.saldoAwalSpinEdit.Size = new System.Drawing.Size(141, 20);
            this.saldoAwalSpinEdit.TabIndex = 23;
            // 
            // saldoAkhirSpinEdit
            // 
            this.saldoAkhirSpinEdit.DataBindings.Add(new Binding("EditValue", this.mutasiAkunTertentuBindingSource, "SaldoAkhir", true));
            this.saldoAkhirSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.saldoAkhirSpinEdit.Location = new System.Drawing.Point(327, 48);
            this.saldoAkhirSpinEdit.Name = "saldoAkhirSpinEdit";
            this.saldoAkhirSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.saldoAkhirSpinEdit.Properties.ReadOnly = true;
            this.saldoAkhirSpinEdit.Size = new System.Drawing.Size(141, 20);
            this.saldoAkhirSpinEdit.TabIndex = 24;
            // 
            // rptMutasiAkunTertentu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 76);
            this.Controls.Add(saldoAkhirLabel);
            this.Controls.Add(this.saldoAkhirSpinEdit);
            this.Controls.Add(saldoAwalLabel);
            this.Controls.Add(this.saldoAwalSpinEdit);
            this.Controls.Add(akunLabel);
            this.Controls.Add(this.akunLookUpEdit);
            this.Controls.Add(this.tglNeraca2DateEdit);
            this.Controls.Add(this.tglNeraca1DateEdit);
            this.Controls.Add(tglNeraca2Label);
            this.Controls.Add(tglNeraca1Label);
            this.Name = "rptMutasiAkunTertentu";
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutasiAkunTertentuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAwalSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saldoAkhirSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit tglNeraca2DateEdit;
        private System.Windows.Forms.BindingSource mutasiAkunTertentuBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeraca1DateEdit;
        private DevExpress.XtraEditors.LookUpEdit akunLookUpEdit;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
        private DevExpress.XtraEditors.SpinEdit saldoAwalSpinEdit;
        private DevExpress.XtraEditors.SpinEdit saldoAkhirSpinEdit;
    }
}
