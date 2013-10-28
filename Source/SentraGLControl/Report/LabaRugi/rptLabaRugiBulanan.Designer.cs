using System.Windows.Forms;
namespace SentraGL.Report.LabaRugi
{
    partial class rptLabaRugiBulanan
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Label tahunLabel;
            Label namaBulanLabel;
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labaRugiBulananBindingSource = new BindingSource(this.components);
            this.autoFormat1 = new SentraWinFramework.AutoFormat(this.components);
            tahunLabel = new Label();
            namaBulanLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiBulananBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoFormat1)).BeginInit();
            this.SuspendLayout();
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(56, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 1;
            tahunLabel.Text = "Tahun:";
            // 
            // namaBulanLabel
            // 
            namaBulanLabel.AutoSize = true;
            namaBulanLabel.Location = new System.Drawing.Point(190, 15);
            namaBulanLabel.Name = "namaBulanLabel";
            namaBulanLabel.Size = new System.Drawing.Size(37, 13);
            namaBulanLabel.TabIndex = 2;
            namaBulanLabel.Text = "Bulan:";
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.labaRugiBulananBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(103, 12);
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
            this.tahunSpinEdit.TabIndex = 2;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.labaRugiBulananBindingSource, "Bulan", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(233, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(99, 20);
            this.comboBoxEdit1.TabIndex = 3;
            // 
            // labaRugiBulananBindingSource
            // 
            this.labaRugiBulananBindingSource.DataSource = typeof(SentraGL.Report.LabaRugi.LabaRugiBulanan);
            // 
            // autoFormat1
            // 
            this.autoFormat1.OwnerForm = this;
            // 
            // frmLabaRugiBulanan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 40);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(namaBulanLabel);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "frmLabaRugiBulanan";
            this.Text = "frmLabaRugiBulanan";
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.labaRugiBulananBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.autoFormat1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource labaRugiBulananBindingSource;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private SentraWinFramework.AutoFormat autoFormat1;
    }
}