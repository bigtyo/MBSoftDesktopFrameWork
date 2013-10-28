using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaBulananLvl
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
            Label levelCetakLabel;
            this.neracaBulananLevelBindingSource = new BindingSource(this.components);
            this.tahunSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.levelCetakSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tahunLabel = new Label();
            levelCetakLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaBulananLevelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tahunLabel
            // 
            tahunLabel.AutoSize = true;
            tahunLabel.Location = new System.Drawing.Point(26, 15);
            tahunLabel.Name = "tahunLabel";
            tahunLabel.Size = new System.Drawing.Size(41, 13);
            tahunLabel.TabIndex = 0;
            tahunLabel.Text = "Tahun:";
            // 
            // levelCetakLabel
            // 
            levelCetakLabel.AutoSize = true;
            levelCetakLabel.Location = new System.Drawing.Point(155, 15);
            levelCetakLabel.Name = "levelCetakLabel";
            levelCetakLabel.Size = new System.Drawing.Size(67, 13);
            levelCetakLabel.TabIndex = 2;
            levelCetakLabel.Text = "Level Cetak:";
            // 
            // neracaBulananLevelBindingSource
            // 
            this.neracaBulananLevelBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaBulananLevel);
            // 
            // tahunSpinEdit
            // 
            this.tahunSpinEdit.DataBindings.Add(new Binding("EditValue", this.neracaBulananLevelBindingSource, "Tahun", true));
            this.tahunSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.tahunSpinEdit.Location = new System.Drawing.Point(73, 12);
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
            // levelCetakSpinEdit
            // 
            this.levelCetakSpinEdit.DataBindings.Add(new Binding("EditValue", this.neracaBulananLevelBindingSource, "LevelCetak", true));
            this.levelCetakSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Location = new System.Drawing.Point(228, 12);
            this.levelCetakSpinEdit.Name = "levelCetakSpinEdit";
            this.levelCetakSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.levelCetakSpinEdit.Size = new System.Drawing.Size(54, 20);
            this.levelCetakSpinEdit.TabIndex = 3;
            // 
            // rptNeracaBulananLvl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 48);
            this.Controls.Add(levelCetakLabel);
            this.Controls.Add(this.levelCetakSpinEdit);
            this.Controls.Add(tahunLabel);
            this.Controls.Add(this.tahunSpinEdit);
            this.Name = "rptNeracaBulananLvl";
            ((System.ComponentModel.ISupportInitialize)(this.neracaBulananLevelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tahunSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaBulananLevelBindingSource;
        private DevExpress.XtraEditors.SpinEdit tahunSpinEdit;
        private DevExpress.XtraEditors.SpinEdit levelCetakSpinEdit;

    }
}
