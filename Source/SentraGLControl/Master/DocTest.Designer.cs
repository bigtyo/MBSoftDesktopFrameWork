using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SentraGL.Master
{
    partial class DocTest
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
            Label akunLabel;
            this.akunLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.testBindingSource = new BindingSource(this.components);
            this.listAkunBindingSource = new BindingSource(this.components);
            akunLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.akunLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // akunLabel
            // 
            akunLabel.AutoSize = true;
            akunLabel.Location = new System.Drawing.Point(20, 15);
            akunLabel.Name = "akunLabel";
            akunLabel.Size = new System.Drawing.Size(35, 13);
            akunLabel.TabIndex = 1;
            akunLabel.Text = "Akun:";
            // 
            // akunLookUpEdit
            // 
            this.akunLookUpEdit.DataBindings.Add(new Binding("EditValue", this.testBindingSource, "Akun", true));
            this.akunLookUpEdit.Location = new System.Drawing.Point(61, 12);
            this.akunLookUpEdit.Name = "akunLookUpEdit";
            this.akunLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.akunLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "No Akun", 30),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "Nama Akun", 70)});
            this.akunLookUpEdit.Properties.DataSource = this.listAkunBindingSource;
            this.akunLookUpEdit.Properties.DisplayMember = "NamaAkun";
            this.akunLookUpEdit.Properties.ValueMember = "NoAkun";
            this.akunLookUpEdit.Size = new System.Drawing.Size(166, 20);
            this.akunLookUpEdit.TabIndex = 2;
            // 
            // testBindingSource
            // 
            this.testBindingSource.DataSource = typeof(SentraGL.Master.Test);
            // 
            // listAkunBindingSource
            // 
            this.listAkunBindingSource.DataMember = "ListAkun";
            this.listAkunBindingSource.DataSource = this.testBindingSource;
            // 
            // DocTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(256, 54);
            this.Controls.Add(akunLabel);
            this.Controls.Add(this.akunLookUpEdit);
            this.Name = "DocTest";
            this.StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.akunLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listAkunBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource testBindingSource;
        private DevExpress.XtraEditors.LookUpEdit akunLookUpEdit;
        private System.Windows.Forms.BindingSource listAkunBindingSource;
    }
}
