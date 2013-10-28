using System.Windows.Forms;
namespace SentraGL.Report.Neraca
{
    partial class rptNeracaKlp
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
            Label tglNeracaLabel1;
            this.neracaKlpBindingSource = new BindingSource(this.components);
            this.tglNeracaDateEdit1 = new DevExpress.XtraEditors.DateEdit();
            tglNeracaLabel1 = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.neracaKlpBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // neracaKlpBindingSource
            // 
            this.neracaKlpBindingSource.DataSource = typeof(SentraGL.Report.Neraca.NeracaKlp);
            // 
            // tglNeracaLabel1
            // 
            tglNeracaLabel1.AutoSize = true;
            tglNeracaLabel1.Location = new System.Drawing.Point(33, 15);
            tglNeracaLabel1.Name = "tglNeracaLabel1";
            tglNeracaLabel1.Size = new System.Drawing.Size(62, 13);
            tglNeracaLabel1.TabIndex = 7;
            tglNeracaLabel1.Text = "Tgl Neraca:";
            // 
            // tglNeracaDateEdit1
            // 
            this.tglNeracaDateEdit1.DataBindings.Add(new Binding("EditValue", this.neracaKlpBindingSource, "TglNeraca", true));
            this.tglNeracaDateEdit1.EditValue = null;
            this.tglNeracaDateEdit1.Location = new System.Drawing.Point(101, 12);
            this.tglNeracaDateEdit1.Name = "tglNeracaDateEdit1";
            this.tglNeracaDateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglNeracaDateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.tglNeracaDateEdit1.Size = new System.Drawing.Size(121, 20);
            this.tglNeracaDateEdit1.TabIndex = 8;
            // 
            // rptNeracaKlp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 45);
            this.Controls.Add(tglNeracaLabel1);
            this.Controls.Add(this.tglNeracaDateEdit1);
            this.Name = "rptNeracaKlp";
            ((System.ComponentModel.ISupportInitialize)(this.neracaKlpBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglNeracaDateEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource neracaKlpBindingSource;
        private DevExpress.XtraEditors.DateEdit tglNeracaDateEdit1;

    }
}
