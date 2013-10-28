namespace SentraGL.Report
{
    partial class rptPosisiAkun
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
            System.Windows.Forms.Label levelCetakLabel;
            System.Windows.Forms.Label tglPosisiAkunLabel;
            this.PosisiAkunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.levelCetakSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.tglNeracaDateEdit = new DevExpress.XtraEditors.DateEdit();
            levelCetakLabel = new System.Windows.Forms.Label();
            tglPosisiAkunLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PosisiAkunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // levelCetakLabel
            // 
            levelCetakLabel.AutoSize = true;
            levelCetakLabel.Location = new System.Drawing.Point(246, 19);
            levelCetakLabel.Name = "levelCetakLabel";
            levelCetakLabel.Size = new System.Drawing.Size(67, 13);
            levelCetakLabel.TabIndex = 11;
            levelCetakLabel.Text = "Level Cetak:";
            // 
            // tglPosisiAkunLabel
            // 
            tglPosisiAkunLabel.AutoSize = true;
            tglPosisiAkunLabel.Location = new System.Drawing.Point(14, 19);
            tglPosisiAkunLabel.Name = "tglPosisiAkunLabel";
            tglPosisiAkunLabel.Size = new System.Drawing.Size(81, 13);
            tglPosisiAkunLabel.TabIndex = 10;
            tglPosisiAkunLabel.Text = "Tgl Posisi Akun:";
            // 
            // PosisiAkunBindingSource
            // 
            this.PosisiAkunBindingSource.DataSource = typeof(SentraGL.Report.PosisiAkun);
            // 
            // levelCetakSpinEdit
            // 
            this.levelCetakSpinEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PosisiAkunBindingSource, "LevelCetak", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.levelCetakSpinEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Location = new System.Drawing.Point(319, 16);
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
            this.levelCetakSpinEdit.TabIndex = 9;
            // 
            // tglNeracaDateEdit
            // 
            this.tglNeracaDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PosisiAkunBindingSource, "TglPosisiAkun", true));
            this.tglNeracaDateEdit.EditValue = null;
            this.tglNeracaDateEdit.Location = new System.Drawing.Point(101, 16);
            this.tglNeracaDateEdit.Name = "tglNeracaDateEdit";
            this.tglNeracaDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeracaDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglNeracaDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglNeracaDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglNeracaDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeracaDateEdit.Size = new System.Drawing.Size(116, 20);
            this.tglNeracaDateEdit.TabIndex = 8;
            // 
            // rptPosisiAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 51);
            this.Controls.Add(this.levelCetakSpinEdit);
            this.Controls.Add(this.tglNeracaDateEdit);
            this.Controls.Add(levelCetakLabel);
            this.Controls.Add(tglPosisiAkunLabel);
            this.Name = "rptPosisiAkun";
            ((System.ComponentModel.ISupportInitialize)(this.PosisiAkunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit levelCetakSpinEdit;
        private System.Windows.Forms.BindingSource PosisiAkunBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeracaDateEdit;
    }
}
