using System.Windows.Forms;
namespace SentraGL.Report.LabaRugi
{
    partial class rptLabaRugi
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
            Label tglAwalLabel;
            Label tglAkhirLabel;
            this.labaRugiBindingSource = new BindingSource(this.components);
            this.tglAwalDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglAkhirDateEdit = new DevExpress.XtraEditors.DateEdit();
            tglAwalLabel = new Label();
            tglAkhirLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tglAwalLabel
            // 
            tglAwalLabel.AutoSize = true;
            tglAwalLabel.Location = new System.Drawing.Point(19, 15);
            tglAwalLabel.Name = "tglAwalLabel";
            tglAwalLabel.Size = new System.Drawing.Size(51, 13);
            tglAwalLabel.TabIndex = 1;
            tglAwalLabel.Text = "Tgl Awal:";
            // 
            // tglAkhirLabel
            // 
            tglAkhirLabel.AutoSize = true;
            tglAkhirLabel.Location = new System.Drawing.Point(214, 15);
            tglAkhirLabel.Name = "tglAkhirLabel";
            tglAkhirLabel.Size = new System.Drawing.Size(52, 13);
            tglAkhirLabel.TabIndex = 2;
            tglAkhirLabel.Text = "Tgl Akhir:";
            // 
            // labaRugiBindingSource
            // 
            this.labaRugiBindingSource.DataSource = typeof(SentraGL.Report.LabaRugi.LabaRugi);
            // 
            // tglAwalDateEdit
            // 
            this.tglAwalDateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiBindingSource, "TglAwal", true));
            this.tglAwalDateEdit.EditValue = null;
            this.tglAwalDateEdit.Location = new System.Drawing.Point(76, 12);
            this.tglAwalDateEdit.Name = "tglAwalDateEdit";
            this.tglAwalDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAwalDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAwalDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAwalDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAwalDateEdit.Size = new System.Drawing.Size(116, 20);
            this.tglAwalDateEdit.TabIndex = 0;
            // 
            // tglAkhirDateEdit
            // 
            this.tglAkhirDateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiBindingSource, "TglAkhir", true));
            this.tglAkhirDateEdit.EditValue = null;
            this.tglAkhirDateEdit.Location = new System.Drawing.Point(272, 12);
            this.tglAkhirDateEdit.Name = "tglAkhirDateEdit";
            this.tglAkhirDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAkhirDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAkhirDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAkhirDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAkhirDateEdit.Size = new System.Drawing.Size(116, 20);
            this.tglAkhirDateEdit.TabIndex = 1;
            // 
            // winLabaRugi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 44);
            this.Controls.Add(this.tglAkhirDateEdit);
            this.Controls.Add(this.tglAwalDateEdit);
            this.Controls.Add(tglAkhirLabel);
            this.Controls.Add(tglAwalLabel);
            this.Name = "winLabaRugi";
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource labaRugiBindingSource;
        private DevExpress.XtraEditors.DateEdit tglAwalDateEdit;
        private DevExpress.XtraEditors.DateEdit tglAkhirDateEdit;
    }
}
