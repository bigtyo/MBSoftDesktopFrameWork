using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaPerbandinganLvl
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
            Label tglNeraca1Label;
            Label tglNeraca2Label;
            Label levelCetakLabel;
            this.neracaPerbandinganLevelBindingSource = new BindingSource(this.components);
            this.tglNeraca1DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglNeraca2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.levelCetakSpinEdit = new DevExpress.XtraEditors.SpinEdit();
            tglNeraca1Label = new Label();
            tglNeraca2Label = new Label();
            levelCetakLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaPerbandinganLevelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // neracaPerbandinganLevelBindingSource
            // 
            this.neracaPerbandinganLevelBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaPerbandinganLevel);
            // 
            // tglNeraca1Label
            // 
            tglNeraca1Label.AutoSize = true;
            tglNeraca1Label.Location = new System.Drawing.Point(14, 15);
            tglNeraca1Label.Name = "tglNeraca1Label";
            tglNeraca1Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca1Label.TabIndex = 0;
            tglNeraca1Label.Text = "Tgl Neraca1:";
            // 
            // tglNeraca1DateEdit
            // 
            this.tglNeraca1DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaPerbandinganLevelBindingSource, "TglNeraca1", true));
            this.tglNeraca1DateEdit.EditValue = null;
            this.tglNeraca1DateEdit.Location = new System.Drawing.Point(88, 12);
            this.tglNeraca1DateEdit.Name = "tglNeraca1DateEdit";
            this.tglNeraca1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca1DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca1DateEdit.Size = new System.Drawing.Size(113, 20);
            this.tglNeraca1DateEdit.TabIndex = 1;
            // 
            // tglNeraca2Label
            // 
            tglNeraca2Label.AutoSize = true;
            tglNeraca2Label.Location = new System.Drawing.Point(14, 41);
            tglNeraca2Label.Name = "tglNeraca2Label";
            tglNeraca2Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca2Label.TabIndex = 2;
            tglNeraca2Label.Text = "Tgl Neraca2:";
            // 
            // tglNeraca2DateEdit
            // 
            this.tglNeraca2DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaPerbandinganLevelBindingSource, "TglNeraca2", true));
            this.tglNeraca2DateEdit.EditValue = null;
            this.tglNeraca2DateEdit.Location = new System.Drawing.Point(88, 38);
            this.tglNeraca2DateEdit.Name = "tglNeraca2DateEdit";
            this.tglNeraca2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca2DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca2DateEdit.Size = new System.Drawing.Size(113, 20);
            this.tglNeraca2DateEdit.TabIndex = 3;
            // 
            // levelCetakLabel
            // 
            levelCetakLabel.AutoSize = true;
            levelCetakLabel.Location = new System.Drawing.Point(240, 15);
            levelCetakLabel.Name = "levelCetakLabel";
            levelCetakLabel.Size = new System.Drawing.Size(67, 13);
            levelCetakLabel.TabIndex = 4;
            levelCetakLabel.Text = "Level Cetak:";
            // 
            // levelCetakSpinEdit
            // 
            this.levelCetakSpinEdit.DataBindings.Add(new Binding("EditValue", this.neracaPerbandinganLevelBindingSource, "LevelCetak", true));
            this.levelCetakSpinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.levelCetakSpinEdit.Location = new System.Drawing.Point(313, 12);
            this.levelCetakSpinEdit.Name = "levelCetakSpinEdit";
            this.levelCetakSpinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.levelCetakSpinEdit.Size = new System.Drawing.Size(100, 20);
            this.levelCetakSpinEdit.TabIndex = 5;
            // 
            // rptNeracaPerbandinganLvl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 69);
            this.Controls.Add(levelCetakLabel);
            this.Controls.Add(this.levelCetakSpinEdit);
            this.Controls.Add(tglNeraca2Label);
            this.Controls.Add(this.tglNeraca2DateEdit);
            this.Controls.Add(tglNeraca1Label);
            this.Controls.Add(this.tglNeraca1DateEdit);
            this.Name = "rptNeracaPerbandinganLvl";
            ((System.ComponentModel.ISupportInitialize)(this.neracaPerbandinganLevelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.levelCetakSpinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaPerbandinganLevelBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeraca1DateEdit;
        private DevExpress.XtraEditors.DateEdit tglNeraca2DateEdit;
        private DevExpress.XtraEditors.SpinEdit levelCetakSpinEdit;

    }
}
