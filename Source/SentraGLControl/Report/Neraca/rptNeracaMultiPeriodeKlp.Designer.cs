using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaMultiPeriodeKlp
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
            Label tglNeraca3Label;
            Label tglNeraca4Label;
            this.neracaMultiPeriodeKlpBindingSource = new BindingSource(this.components);
            this.tglNeraca1DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglNeraca2DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglNeraca3DateEdit = new DevExpress.XtraEditors.DateEdit();
            this.tglNeraca4DateEdit = new DevExpress.XtraEditors.DateEdit();
            tglNeraca1Label = new Label();
            tglNeraca2Label = new Label();
            tglNeraca3Label = new Label();
            tglNeraca4Label = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaMultiPeriodeKlpBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca3DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca3DateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca4DateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca4DateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // neracaMultiPeriodeKlpBindingSource
            // 
            this.neracaMultiPeriodeKlpBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaMultiPeriodeKlp);
            // 
            // tglNeraca1Label
            // 
            tglNeraca1Label.AutoSize = true;
            tglNeraca1Label.Location = new System.Drawing.Point(21, 15);
            tglNeraca1Label.Name = "tglNeraca1Label";
            tglNeraca1Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca1Label.TabIndex = 0;
            tglNeraca1Label.Text = "Tgl Neraca1:";
            // 
            // tglNeraca1DateEdit
            // 
            this.tglNeraca1DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaMultiPeriodeKlpBindingSource, "TglNeraca1", true));
            this.tglNeraca1DateEdit.EditValue = null;
            this.tglNeraca1DateEdit.Location = new System.Drawing.Point(95, 12);
            this.tglNeraca1DateEdit.Name = "tglNeraca1DateEdit";
            this.tglNeraca1DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca1DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca1DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca1DateEdit.TabIndex = 1;
            // 
            // tglNeraca2Label
            // 
            tglNeraca2Label.AutoSize = true;
            tglNeraca2Label.Location = new System.Drawing.Point(21, 41);
            tglNeraca2Label.Name = "tglNeraca2Label";
            tglNeraca2Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca2Label.TabIndex = 2;
            tglNeraca2Label.Text = "Tgl Neraca2:";
            // 
            // tglNeraca2DateEdit
            // 
            this.tglNeraca2DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaMultiPeriodeKlpBindingSource, "TglNeraca2", true));
            this.tglNeraca2DateEdit.EditValue = null;
            this.tglNeraca2DateEdit.Location = new System.Drawing.Point(95, 38);
            this.tglNeraca2DateEdit.Name = "tglNeraca2DateEdit";
            this.tglNeraca2DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca2DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca2DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca2DateEdit.TabIndex = 3;
            // 
            // tglNeraca3Label
            // 
            tglNeraca3Label.AutoSize = true;
            tglNeraca3Label.Location = new System.Drawing.Point(218, 15);
            tglNeraca3Label.Name = "tglNeraca3Label";
            tglNeraca3Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca3Label.TabIndex = 4;
            tglNeraca3Label.Text = "Tgl Neraca3:";
            // 
            // tglNeraca3DateEdit
            // 
            this.tglNeraca3DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaMultiPeriodeKlpBindingSource, "TglNeraca3", true));
            this.tglNeraca3DateEdit.EditValue = null;
            this.tglNeraca3DateEdit.Location = new System.Drawing.Point(292, 12);
            this.tglNeraca3DateEdit.Name = "tglNeraca3DateEdit";
            this.tglNeraca3DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca3DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca3DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca3DateEdit.TabIndex = 5;
            // 
            // tglNeraca4Label
            // 
            tglNeraca4Label.AutoSize = true;
            tglNeraca4Label.Location = new System.Drawing.Point(218, 41);
            tglNeraca4Label.Name = "tglNeraca4Label";
            tglNeraca4Label.Size = new System.Drawing.Size(68, 13);
            tglNeraca4Label.TabIndex = 6;
            tglNeraca4Label.Text = "Tgl Neraca4:";
            // 
            // tglNeraca4DateEdit
            // 
            this.tglNeraca4DateEdit.DataBindings.Add(new Binding("EditValue", this.neracaMultiPeriodeKlpBindingSource, "TglNeraca4", true));
            this.tglNeraca4DateEdit.EditValue = null;
            this.tglNeraca4DateEdit.Location = new System.Drawing.Point(292, 38);
            this.tglNeraca4DateEdit.Name = "tglNeraca4DateEdit";
            this.tglNeraca4DateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeraca4DateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeraca4DateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglNeraca4DateEdit.TabIndex = 7;
            // 
            // rptNeracaMultiPeriodeKlp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 70);
            this.Controls.Add(tglNeraca4Label);
            this.Controls.Add(this.tglNeraca4DateEdit);
            this.Controls.Add(tglNeraca3Label);
            this.Controls.Add(this.tglNeraca3DateEdit);
            this.Controls.Add(tglNeraca2Label);
            this.Controls.Add(this.tglNeraca2DateEdit);
            this.Controls.Add(tglNeraca1Label);
            this.Controls.Add(this.tglNeraca1DateEdit);
            this.Name = "rptNeracaMultiPeriodeKlp";
            ((System.ComponentModel.ISupportInitialize)(this.neracaMultiPeriodeKlpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca1DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca2DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca3DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca3DateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca4DateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeraca4DateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaMultiPeriodeKlpBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeraca1DateEdit;
        private DevExpress.XtraEditors.DateEdit tglNeraca2DateEdit;
        private DevExpress.XtraEditors.DateEdit tglNeraca3DateEdit;
        private DevExpress.XtraEditors.DateEdit tglNeraca4DateEdit;
    }
}
