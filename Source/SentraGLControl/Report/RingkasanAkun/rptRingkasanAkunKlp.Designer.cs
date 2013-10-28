using System.Windows.Forms;
namespace SentraGL.Report.RingkasanAkun
{
    partial class rptRingkasanAkunKlp
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
            Label namaBulanLabel;
            Label tahunLabel;
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ringkasanAkunKlpBindingSource = new BindingSource(this.components);
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            namaBulanLabel = new Label();
            tahunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ringkasanAkunKlpBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // namaBulanLabel
            // 
            namaBulanLabel.AutoSize = true;
            namaBulanLabel.Location = new System.Drawing.Point(195, 15);
            namaBulanLabel.Name = "namaBulanLabel";
            namaBulanLabel.Size = new System.Drawing.Size(37, 13);
            namaBulanLabel.TabIndex = 6;
            namaBulanLabel.Text = "Bulan:";
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(61, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 4;
            tahunLabel.Text = "Tahun:";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.ringkasanAkunKlpBindingSource, "Bulan", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(238, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(99, 20);
            this.comboBoxEdit1.TabIndex = 7;
            // 
            // ringkasanAkunKlpBindingSource
            // 
            this.ringkasanAkunKlpBindingSource.DataSource = typeof(SentraGL.Report.RingkasanAkun.RingkasanAkunKlp);
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.ringkasanAkunKlpBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(108, 12);
            this.tahunSpinEdit.Name = "tahunSpinEdit";
            this.tahunSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tahunSpinEdit.Properties.DisplayFormat.FormatString = "####";
            this.tahunSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Properties.EditFormat.FormatString = "####";
            this.tahunSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Properties.IsFloatValue = false;
            this.tahunSpinEdit.Properties.Mask.EditMask = "d";
            this.tahunSpinEdit.Size = new System.Drawing.Size(63, 20);
            this.tahunSpinEdit.TabIndex = 5;
            // 
            // rptRingkasanAkunKlp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 42);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(namaBulanLabel);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "rptRingkasanAkunKlp";
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ringkasanAkunKlpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;
        private System.Windows.Forms.BindingSource ringkasanAkunKlpBindingSource;
    }
}
