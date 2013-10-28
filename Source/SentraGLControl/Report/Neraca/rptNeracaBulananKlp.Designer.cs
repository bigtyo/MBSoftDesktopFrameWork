using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaBulananKlp
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
            Label tahunLabel;
            this.neracaBulananKlpBindingSource = new BindingSource(this.components);
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tahunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaBulananKlpBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(40, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 0;
            tahunLabel.Text = "Tahun:";
            // 
            // neracaBulananKlpBindingSource
            // 
            this.neracaBulananKlpBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaBulananKlp);
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.neracaBulananKlpBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(87, 12);
            this.tahunSpinEdit.Name = "tahunSpinEdit";
            this.tahunSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tahunSpinEdit.Properties.DisplayFormat.FormatString = "0000";
            this.tahunSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Properties.EditFormat.FormatString = "0000";
            this.tahunSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Size = new System.Drawing.Size(65, 20);
            this.tahunSpinEdit.TabIndex = 1;
            // 
            // rptNeracaBulananKlp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 42);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "rptNeracaBulananKlp";
            ((System.ComponentModel.ISupportInitialize)(this.neracaBulananKlpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaBulananKlpBindingSource;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;

    }
}
