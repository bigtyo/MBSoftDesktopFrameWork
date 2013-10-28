using System.Windows.Forms;
namespace SentraGL.Report.MutasiAkun
{
    partial class rptMutasiSemuaAkun
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
            this.tglNeraca2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.mutasiSemuaAkunBindingSource = new BindingSource(this.components);
            this.tglNeraca1DateEdit = new DevExpress.XtraEditors.DateEdit();
            tglNeraca2Label = new Label();
            tglNeraca1Label = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutasiSemuaAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tglNeraca2Label
            // 
            tglNeraca2Label.AutoSize = true;
            tglNeraca2Label.Location = new System.Drawing.Point(232, 16);
            tglNeraca2Label.Name = "tglNeraca2Label";
            tglNeraca2Label.Size = new System.Drawing.Size(52, 13);
            tglNeraca2Label.TabIndex = 17;
            tglNeraca2Label.Text = "Tgl Akhir:";
            // 
            // tglNeraca1Label
            // 
            tglNeraca1Label.AutoSize = true;
            tglNeraca1Label.Location = new System.Drawing.Point(32, 15);
            tglNeraca1Label.Name = "tglNeraca1Label";
            tglNeraca1Label.Size = new System.Drawing.Size(51, 13);
            tglNeraca1Label.TabIndex = 16;
            tglNeraca1Label.Text = "Tgl Awal:";
            // 
            // tglNeraca2DateEdit
            // 
            this.tglNeraca2DateEdit.DataBindings.Add(new Binding("EditValue", this.mutasiSemuaAkunBindingSource, "TglAkhir", true));
            this.tglNeraca2DateEdit.EditValue = null;
            this.tglNeraca2DateEdit.Location = new System.Drawing.Point(290, 11);
            this.tglNeraca2DateEdit.Name = "tglNeraca2DateEdit";
            this.tglNeraca2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca2DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeraca2DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeraca2DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeraca2DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca2DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca2DateEdit.TabIndex = 15;
            // 
            // mutasiSemuaAkunBindingSource
            // 
            this.mutasiSemuaAkunBindingSource.DataSource = typeof(SentraGL.Report.MutasiAkun.MutasiSemuaAkun);
            // 
            // tglNeraca1DateEdit
            // 
            this.tglNeraca1DateEdit.DataBindings.Add(new Binding("EditValue", this.mutasiSemuaAkunBindingSource, "TglAwal", true));
            this.tglNeraca1DateEdit.EditValue = null;
            this.tglNeraca1DateEdit.Location = new System.Drawing.Point(89, 12);
            this.tglNeraca1DateEdit.Name = "tglNeraca1DateEdit";
            this.tglNeraca1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca1DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeraca1DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeraca1DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeraca1DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca1DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca1DateEdit.TabIndex = 14;
            // 
            // rptMutasiSemuaAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 40);
            this.Controls.Add(this.tglNeraca2DateEdit);
            this.Controls.Add(this.tglNeraca1DateEdit);
            this.Controls.Add(tglNeraca2Label);
            this.Controls.Add(tglNeraca1Label);
            this.Name = "rptMutasiSemuaAkun";
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mutasiSemuaAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit tglNeraca2DateEdit;
        private System.Windows.Forms.BindingSource mutasiSemuaAkunBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeraca1DateEdit;
    }
}
