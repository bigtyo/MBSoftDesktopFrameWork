using System.Windows.Forms;
namespace SentraGL.Master
{
    partial class DocJenisDokSumberJurnal
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
            Label jenisDokSumberLabel;
            Label keteranganLabel;
            this.jenisDokSumberJurnalBindingSource = new BindingSource(this.components);
            this.jenisDokSumberTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.keteranganMemoEdit = new DevExpress.XtraEditors.MemoEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.otomatisCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            jenisDokSumberLabel = new Label();
            keteranganLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.jenisDokSumberJurnalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisDokSumberTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.otomatisCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // jenisDokSumberLabel
            // 
            jenisDokSumberLabel.AutoSize = true;
            jenisDokSumberLabel.Location = new System.Drawing.Point(15, 20);
            jenisDokSumberLabel.Name = "jenisDokSumberLabel";
            jenisDokSumberLabel.Size = new System.Drawing.Size(95, 13);
            jenisDokSumberLabel.TabIndex = 1;
            jenisDokSumberLabel.Text = "Jenis Dok Sumber:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(43, 45);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 2;
            keteranganLabel.Text = "Keterangan:";
            // 
            // jenisDokSumberJurnalBindingSource
            // 
            this.jenisDokSumberJurnalBindingSource.DataSource = typeof(SentraGL.Master.JenisDokSumberJurnal);
            // 
            // jenisDokSumberTextEdit
            // 
            this.jenisDokSumberTextEdit.DataBindings.Add(new Binding("EditValue", this.jenisDokSumberJurnalBindingSource, "JenisDokSumber", true));
            this.jenisDokSumberTextEdit.Location = new System.Drawing.Point(116, 16);
            this.jenisDokSumberTextEdit.Name = "jenisDokSumberTextEdit";
            this.jenisDokSumberTextEdit.Size = new System.Drawing.Size(289, 20);
            this.jenisDokSumberTextEdit.TabIndex = 0;
            // 
            // keteranganMemoEdit
            // 
            this.keteranganMemoEdit.DataBindings.Add(new Binding("EditValue", this.jenisDokSumberJurnalBindingSource, "Keterangan", true));
            this.keteranganMemoEdit.Location = new System.Drawing.Point(116, 42);
            this.keteranganMemoEdit.Name = "keteranganMemoEdit";
            this.keteranganMemoEdit.Size = new System.Drawing.Size(289, 42);
            this.keteranganMemoEdit.TabIndex = 1;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisDokSumberJurnalBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(411, 17);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 2;
            this.aktifCheckEdit.TabStop = false;
            // 
            // otomatisCheckEdit
            // 
            this.otomatisCheckEdit.DataBindings.Add(new Binding("EditValue", this.jenisDokSumberJurnalBindingSource, "Otomatis", true));
            this.otomatisCheckEdit.Location = new System.Drawing.Point(411, 43);
            this.otomatisCheckEdit.Name = "otomatisCheckEdit";
            this.otomatisCheckEdit.Properties.Caption = "Otomatis";
            this.otomatisCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.otomatisCheckEdit.TabIndex = 3;
            this.otomatisCheckEdit.TabStop = false;
            // 
            // winJenisDokSumberJurnal
            // 
            this.ClientSize = new System.Drawing.Size(492, 95);
            this.Controls.Add(this.otomatisCheckEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganMemoEdit);
            this.Controls.Add(jenisDokSumberLabel);
            this.Controls.Add(this.jenisDokSumberTextEdit);
            this.Name = "winJenisDokSumberJurnal";
            ((System.ComponentModel.ISupportInitialize)(this.jenisDokSumberJurnalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jenisDokSumberTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganMemoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.otomatisCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource jenisDokSumberJurnalBindingSource;
        private DevExpress.XtraEditors.TextEdit jenisDokSumberTextEdit;
        private DevExpress.XtraEditors.MemoEdit keteranganMemoEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.CheckEdit otomatisCheckEdit;
    }
}
