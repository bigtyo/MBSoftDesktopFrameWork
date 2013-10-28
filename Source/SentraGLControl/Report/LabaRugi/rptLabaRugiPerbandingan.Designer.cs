using System.Windows.Forms;
namespace SentraGL.Report.LabaRugi
{
    partial class rptLabaRugiPerbandingan
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
            Label tglAkhirLabaRugi2Label;
            Label tglAwalLabaRugi2Label;
            Label tglAkhirLabaRugi1Label;
            Label tglAwalLabaRugi1Label;
            this.tglAkhirLabaRugi2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labaRugiPerbandinganBindingSource = new BindingSource(this.components);
            this.tglAwalLabaRugi2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglAkhirLabaRugi1DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglAwalLabaRugi1DateEdit = new DevExpress.XtraEditors.DateEdit();
            tglAkhirLabaRugi2Label = new Label();
            tglAwalLabaRugi2Label = new Label();
            tglAkhirLabaRugi1Label = new Label();
            tglAwalLabaRugi1Label = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirLabaRugi2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiPerbandinganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalLabaRugi2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirLabaRugi1DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalLabaRugi1DateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tglAkhirLabaRugi2Label
            // 
            tglAkhirLabaRugi2Label.AutoSize = true;
            tglAkhirLabaRugi2Label.Location = new System.Drawing.Point(541, 16);
            tglAkhirLabaRugi2Label.Name = "tglAkhirLabaRugi2Label";
            tglAkhirLabaRugi2Label.Size = new System.Drawing.Size(18, 13);
            tglAkhirLabaRugi2Label.TabIndex = 32;
            tglAkhirLabaRugi2Label.Text = "sd";
            // 
            // tglAwalLabaRugi2Label
            // 
            tglAwalLabaRugi2Label.AutoSize = true;
            tglAwalLabaRugi2Label.Location = new System.Drawing.Point(356, 16);
            tglAwalLabaRugi2Label.Name = "tglAwalLabaRugi2Label";
            tglAwalLabaRugi2Label.Size = new System.Drawing.Size(81, 13);
            tglAwalLabaRugi2Label.TabIndex = 31;
            tglAwalLabaRugi2Label.Text = "Tgl Laba Rugi2:";
            // 
            // tglAkhirLabaRugi1Label
            // 
            tglAkhirLabaRugi1Label.AutoSize = true;
            tglAkhirLabaRugi1Label.Location = new System.Drawing.Point(207, 16);
            tglAkhirLabaRugi1Label.Name = "tglAkhirLabaRugi1Label";
            tglAkhirLabaRugi1Label.Size = new System.Drawing.Size(18, 13);
            tglAkhirLabaRugi1Label.TabIndex = 30;
            tglAkhirLabaRugi1Label.Text = "sd";
            // 
            // tglAwalLabaRugi1Label
            // 
            tglAwalLabaRugi1Label.AutoSize = true;
            tglAwalLabaRugi1Label.Location = new System.Drawing.Point(22, 16);
            tglAwalLabaRugi1Label.Name = "tglAwalLabaRugi1Label";
            tglAwalLabaRugi1Label.Size = new System.Drawing.Size(81, 13);
            tglAwalLabaRugi1Label.TabIndex = 29;
            tglAwalLabaRugi1Label.Text = "Tgl Laba Rugi1:";
            // 
            // tglAkhirLabaRugi2DateEdit
            // 
            this.tglAkhirLabaRugi2DateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiPerbandinganBindingSource, "TglAkhirLabaRugi2", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglAkhirLabaRugi2DateEdit.EditValue = null;
            this.tglAkhirLabaRugi2DateEdit.Location = new System.Drawing.Point(565, 12);
            this.tglAkhirLabaRugi2DateEdit.Name = "tglAkhirLabaRugi2DateEdit";
            this.tglAkhirLabaRugi2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAkhirLabaRugi2DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAkhirLabaRugi2DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAkhirLabaRugi2DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAkhirLabaRugi2DateEdit.Size = new System.Drawing.Size(95, 20);
            this.tglAkhirLabaRugi2DateEdit.TabIndex = 28;
            // 
            // labaRugiPerbandinganBindingSource
            // 
            this.labaRugiPerbandinganBindingSource.DataSource = typeof(SentraGL.Report.LabaRugi.LabaRugiPerbandingan);
            // 
            // tglAwalLabaRugi2DateEdit
            // 
            this.tglAwalLabaRugi2DateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiPerbandinganBindingSource, "TglAwalLabaRugi2", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglAwalLabaRugi2DateEdit.EditValue = null;
            this.tglAwalLabaRugi2DateEdit.Location = new System.Drawing.Point(443, 12);
            this.tglAwalLabaRugi2DateEdit.Name = "tglAwalLabaRugi2DateEdit";
            this.tglAwalLabaRugi2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAwalLabaRugi2DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAwalLabaRugi2DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAwalLabaRugi2DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAwalLabaRugi2DateEdit.Size = new System.Drawing.Size(92, 20);
            this.tglAwalLabaRugi2DateEdit.TabIndex = 27;
            // 
            // tglAkhirLabaRugi1DateEdit
            // 
            this.tglAkhirLabaRugi1DateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiPerbandinganBindingSource, "TglAkhirLabaRugi1", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglAkhirLabaRugi1DateEdit.EditValue = null;
            this.tglAkhirLabaRugi1DateEdit.Location = new System.Drawing.Point(231, 12);
            this.tglAkhirLabaRugi1DateEdit.Name = "tglAkhirLabaRugi1DateEdit";
            this.tglAkhirLabaRugi1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAkhirLabaRugi1DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAkhirLabaRugi1DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAkhirLabaRugi1DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAkhirLabaRugi1DateEdit.Size = new System.Drawing.Size(95, 20);
            this.tglAkhirLabaRugi1DateEdit.TabIndex = 26;
            // 
            // tglAwalLabaRugi1DateEdit
            // 
            this.tglAwalLabaRugi1DateEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiPerbandinganBindingSource, "TglAwalLabaRugi1", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglAwalLabaRugi1DateEdit.EditValue = null;
            this.tglAwalLabaRugi1DateEdit.Location = new System.Drawing.Point(109, 12);
            this.tglAwalLabaRugi1DateEdit.Name = "tglAwalLabaRugi1DateEdit";
            this.tglAwalLabaRugi1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglAwalLabaRugi1DateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglAwalLabaRugi1DateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglAwalLabaRugi1DateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglAwalLabaRugi1DateEdit.Size = new System.Drawing.Size(92, 20);
            this.tglAwalLabaRugi1DateEdit.TabIndex = 25;
            // 
            // winLabaRugiPerbandingan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 41);
            this.Controls.Add(this.tglAkhirLabaRugi2DateEdit);
            this.Controls.Add(this.tglAwalLabaRugi2DateEdit);
            this.Controls.Add(this.tglAkhirLabaRugi1DateEdit);
            this.Controls.Add(this.tglAwalLabaRugi1DateEdit);
            this.Controls.Add(tglAkhirLabaRugi2Label);
            this.Controls.Add(tglAwalLabaRugi2Label);
            this.Controls.Add(tglAkhirLabaRugi1Label);
            this.Controls.Add(tglAwalLabaRugi1Label);
            this.Name = "winLabaRugiPerbandingan";
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirLabaRugi2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiPerbandinganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalLabaRugi2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAkhirLabaRugi1DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglAwalLabaRugi1DateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource labaRugiPerbandinganBindingSource;
        private DevExpress.XtraEditors.DateEdit tglAkhirLabaRugi2DateEdit;
        private DevExpress.XtraEditors.DateEdit tglAwalLabaRugi2DateEdit;
        private DevExpress.XtraEditors.DateEdit tglAkhirLabaRugi1DateEdit;
        private DevExpress.XtraEditors.DateEdit tglAwalLabaRugi1DateEdit;
    }
}
