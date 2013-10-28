using System.Windows.Forms;
namespace SentraGL.Report.RingkasanAkun
{
    partial class rptRingkasanAkunLvl
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
            Label bulanLabel;
            Label levelLabel;
            this.ringkasanAkunLevelBindingSource = new BindingSource(this.components);
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.levelSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tahunLabel = new Label();
            bulanLabel = new Label();
            levelLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.ringkasanAkunLevelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(18, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 1;
            tahunLabel.Text = "Tahun:";
            // 
            // bulanLabel
            // 
            bulanLabel.AutoSize = true;
            bulanLabel.Location = new System.Drawing.Point(146, 15);
            bulanLabel.Name = "bulanLabel";
            bulanLabel.Size = new System.Drawing.Size(37, 13);
            bulanLabel.TabIndex = 3;
            bulanLabel.Text = "Bulan:";
            // 
            // levelLabel
            // 
            levelLabel.AutoSize = true;
            levelLabel.Location = new System.Drawing.Point(343, 15);
            levelLabel.Name = "levelLabel";
            levelLabel.Size = new System.Drawing.Size(36, 13);
            levelLabel.TabIndex = 4;
            levelLabel.Text = "Level:";
            // 
            // ringkasanAkunLevelBindingSource
            // 
            this.ringkasanAkunLevelBindingSource.DataSource = typeof(SentraGL.Report.RingkasanAkun.RingkasanAkunLevel);
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.ringkasanAkunLevelBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(65, 12);
            this.tahunSpinEdit.Name = "tahunSpinEdit";
            this.tahunSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tahunSpinEdit.Properties.DisplayFormat.FormatString = "0000";
            this.tahunSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Properties.EditFormat.FormatString = "0000";
            this.tahunSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tahunSpinEdit.Size = new System.Drawing.Size(65, 20);
            this.tahunSpinEdit.TabIndex = 2;
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.DataBindings.Add(new Binding("EditValue", this.ringkasanAkunLevelBindingSource, "Bulan", true));
            this.comboBoxEdit1.Location = new System.Drawing.Point(189, 12);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(132, 20);
            this.comboBoxEdit1.TabIndex = 3;
            // 
            // levelSpinEdit
            // 
            this.levelSpinEdit.DataBindings.Add(new Binding("EditValue", this.ringkasanAkunLevelBindingSource, "Level", true));
            this.levelSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.levelSpinEdit.Location = new System.Drawing.Point(385, 12);
            this.levelSpinEdit.Name = "levelSpinEdit";
            this.levelSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.levelSpinEdit.Size = new System.Drawing.Size(57, 20);
            this.levelSpinEdit.TabIndex = 5;
            // 
            // rptRingkasanAkunLvl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 44);
            this.Controls.Add(levelLabel);
            this.Controls.Add(this.levelSpinEdit);
            this.Controls.Add(bulanLabel);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "rptRingkasanAkunLvl";
            ((System.ComponentModel.ISupportInitialize)(this.ringkasanAkunLevelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BindingSource ringkasanAkunLevelBindingSource;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.SpinEdit levelSpinEdit;

    }
}
