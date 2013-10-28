using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaBebas
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
            Label tglNeracaLabel;
            this.neracaBebasBindingSource = new BindingSource(this.components);
            this.tglNeracaDateEdit = new DevExpress.XtraEditors.DateEdit();
            tglNeracaLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaBebasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // neracaBebasBindingSource
            // 
            this.neracaBebasBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaBebas);
            // 
            // tglNeracaLabel
            // 
            tglNeracaLabel.AutoSize = true;
            tglNeracaLabel.Location = new System.Drawing.Point(36, 15);
            tglNeracaLabel.Name = "tglNeracaLabel";
            tglNeracaLabel.Size = new System.Drawing.Size(62, 13);
            tglNeracaLabel.TabIndex = 0;
            tglNeracaLabel.Text = "Tgl Neraca:";
            // 
            // tglNeracaDateEdit
            // 
            this.tglNeracaDateEdit.DataBindings.Add(new Binding("EditValue", this.neracaBebasBindingSource, "TglNeraca", true));
            this.tglNeracaDateEdit.EditValue = null;
            this.tglNeracaDateEdit.Location = new System.Drawing.Point(104, 12);
            this.tglNeracaDateEdit.Name = "tglNeracaDateEdit";
            this.tglNeracaDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeracaDateEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeracaDateEdit.Size = new System.Drawing.Size(130, 20);
            this.tglNeracaDateEdit.TabIndex = 1;
            // 
            // rptNeracaBebas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 44);
            this.Controls.Add(tglNeracaLabel);
            this.Controls.Add(this.tglNeracaDateEdit);
            this.Name = "rptNeracaBebas";
            ((System.ComponentModel.ISupportInitialize)(this.neracaBebasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaBebasBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeracaDateEdit;

    }
}
