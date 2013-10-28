using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaLvl
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
            Label levelCetakLabel;
            Label tglNeracaLabel;
            this.levelCetakSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.neracaBindingSource = new BindingSource(this.components);
            this.tglNeracaDateEdit = new DevExpress.XtraEditors.DateEdit();
            levelCetakLabel = new Label();
            tglNeracaLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neracaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // levelCetakLabel
            // 
            levelCetakLabel.AutoSize = true;
            levelCetakLabel.Location = new System.Drawing.Point(238, 15);
            levelCetakLabel.Name = "levelCetakLabel";
            levelCetakLabel.Size = new System.Drawing.Size(67, 13);
            levelCetakLabel.TabIndex = 7;
            levelCetakLabel.Text = "Level Cetak:";
            // 
            // tglNeracaLabel
            // 
            tglNeracaLabel.AutoSize = true;
            tglNeracaLabel.Location = new System.Drawing.Point(25, 15);
            tglNeracaLabel.Name = "tglNeracaLabel";
            tglNeracaLabel.Size = new System.Drawing.Size(62, 13);
            tglNeracaLabel.TabIndex = 5;
            tglNeracaLabel.Text = "Tgl Neraca:";
            // 
            // levelCetakSpinEdit
            // 
            this.levelCetakSpinEdit.DataBindings.Add(new Binding("EditValue", this.neracaBindingSource, "LevelCetak", true, DataSourceUpdateMode.OnPropertyChanged));
            this.levelCetakSpinEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Location = new System.Drawing.Point(311, 12);
            this.levelCetakSpinEdit.Name = "levelCetakSpinEdit";
            this.levelCetakSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.levelCetakSpinEdit.Properties.DisplayFormat.FormatString = "##";
            this.levelCetakSpinEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.levelCetakSpinEdit.Properties.EditFormat.FormatString = "##";
            this.levelCetakSpinEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.levelCetakSpinEdit.Properties.IsFloatValue = false;
            this.levelCetakSpinEdit.Properties.Mask.EditMask = "d";
            this.levelCetakSpinEdit.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Size = new System.Drawing.Size(67, 20);
            this.levelCetakSpinEdit.TabIndex = 1;
            // 
            // neracaBindingSource
            // 
            this.neracaBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaLevel);
            // 
            // tglNeracaDateEdit
            // 
            this.tglNeracaDateEdit.DataBindings.Add(new Binding("EditValue", this.neracaBindingSource, "TglNeraca", true, DataSourceUpdateMode.OnPropertyChanged));
            this.tglNeracaDateEdit.EditValue = null;
            this.tglNeracaDateEdit.Location = new System.Drawing.Point(93, 12);
            this.tglNeracaDateEdit.Name = "tglNeracaDateEdit";
            this.tglNeracaDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeracaDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeracaDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeracaDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeracaDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeracaDateEdit.Size = new System.Drawing.Size(116, 20);
            this.tglNeracaDateEdit.TabIndex = 0;
            // 
            // rptNeracaLvl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 44);
            this.Controls.Add(this.levelCetakSpinEdit);
            this.Controls.Add(this.tglNeracaDateEdit);
            this.Controls.Add(levelCetakLabel);
            this.Controls.Add(tglNeracaLabel);
            this.Name = "rptNeracaLvl";
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neracaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit levelCetakSpinEdit;
        private System.Windows.Forms.BindingSource neracaBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeracaDateEdit;
    }
}
