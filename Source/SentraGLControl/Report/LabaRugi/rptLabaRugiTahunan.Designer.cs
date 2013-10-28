using System.Windows.Forms;
namespace SentraGL.Report.LabaRugi
{
    partial class rptLabaRugiTahunan
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
            this.labaRugiTahunanBindingSource = new BindingSource(this.components);
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tahunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiTahunanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(24, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 1;
            tahunLabel.Text = "Tahun:";
            // 
            // labaRugiTahunanBindingSource
            // 
            this.labaRugiTahunanBindingSource.DataSource = typeof(SentraGL.Report.LabaRugi.LabaRugiTahunan);
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiTahunanBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(71, 12);
            this.tahunSpinEdit.Name = "tahunSpinEdit";
            this.tahunSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tahunSpinEdit.Properties.IsFloatValue = false;
            this.tahunSpinEdit.Properties.Mask.EditMask = "d";
            this.tahunSpinEdit.Size = new System.Drawing.Size(70, 20);
            this.tahunSpinEdit.TabIndex = 2;
            // 
            // frmLabaRugiTahunan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 40);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "frmLabaRugiTahunan";
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiTahunanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource labaRugiTahunanBindingSource;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;
    }
}
