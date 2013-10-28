namespace Examples01
{
    partial class frmAkun
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
            System.Windows.Forms.Label noAkunLabel;
            System.Windows.Forms.Label namaAkunLabel;
            System.Windows.Forms.Label akunIndukLabel;
            System.Windows.Forms.Label akunIDLabel;
            System.Windows.Forms.Label parentIDLabel;
            this.noAkunTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaAkunTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.uiNavigator1 = new SentraWinFramework.UINavigator();
            this.nonPostingCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.akunIndukLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.akunIDTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.parentIDTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.namaAkunIndukLabel1 = new System.Windows.Forms.Label();
            this.akunBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.akunBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            noAkunLabel = new System.Windows.Forms.Label();
            namaAkunLabel = new System.Windows.Forms.Label();
            akunIndukLabel = new System.Windows.Forms.Label();
            akunIDLabel = new System.Windows.Forms.Label();
            parentIDLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.noAkunTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaAkunTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonPostingCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunIndukLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunIDTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentIDTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // noAkunLabel
            // 
            noAkunLabel.AutoSize = true;
            noAkunLabel.Location = new System.Drawing.Point(30, 31);
            noAkunLabel.Name = "noAkunLabel";
            noAkunLabel.Size = new System.Drawing.Size(52, 13);
            noAkunLabel.TabIndex = 1;
            noAkunLabel.Text = "No Akun:";
            // 
            // namaAkunLabel
            // 
            namaAkunLabel.AutoSize = true;
            namaAkunLabel.Location = new System.Drawing.Point(16, 83);
            namaAkunLabel.Name = "namaAkunLabel";
            namaAkunLabel.Size = new System.Drawing.Size(66, 13);
            namaAkunLabel.TabIndex = 2;
            namaAkunLabel.Text = "Nama Akun:";
            // 
            // akunIndukLabel
            // 
            akunIndukLabel.AutoSize = true;
            akunIndukLabel.Location = new System.Drawing.Point(17, 57);
            akunIndukLabel.Name = "akunIndukLabel";
            akunIndukLabel.Size = new System.Drawing.Size(65, 13);
            akunIndukLabel.TabIndex = 6;
            akunIndukLabel.Text = "Akun Induk:";
            // 
            // akunIDLabel
            // 
            akunIDLabel.AutoSize = true;
            akunIDLabel.Location = new System.Drawing.Point(423, 15);
            akunIDLabel.Name = "akunIDLabel";
            akunIDLabel.Size = new System.Drawing.Size(49, 13);
            akunIDLabel.TabIndex = 8;
            akunIDLabel.Text = "Akun ID:";
            // 
            // parentIDLabel
            // 
            parentIDLabel.AutoSize = true;
            parentIDLabel.Location = new System.Drawing.Point(417, 41);
            parentIDLabel.Name = "parentIDLabel";
            parentIDLabel.Size = new System.Drawing.Size(55, 13);
            parentIDLabel.TabIndex = 10;
            parentIDLabel.Text = "Parent ID:";
            // 
            // noAkunTextEdit
            // 
            this.noAkunTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "NoAkun", true));
            this.noAkunTextEdit.Location = new System.Drawing.Point(88, 28);
            this.noAkunTextEdit.Name = "noAkunTextEdit";
            this.noAkunTextEdit.Size = new System.Drawing.Size(135, 20);
            this.noAkunTextEdit.TabIndex = 2;
            // 
            // namaAkunTextEdit
            // 
            this.namaAkunTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "NamaAkun", true));
            this.namaAkunTextEdit.Location = new System.Drawing.Point(88, 80);
            this.namaAkunTextEdit.Name = "namaAkunTextEdit";
            this.namaAkunTextEdit.Size = new System.Drawing.Size(197, 20);
            this.namaAkunTextEdit.TabIndex = 3;
            // 
            // uiNavigator1
            // 
            this.uiNavigator1.BindingSource = this.akunBindingSource;
            this.uiNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiNavigator1.Location = new System.Drawing.Point(0, 202);
            this.uiNavigator1.Name = "uiNavigator1";
            this.uiNavigator1.Size = new System.Drawing.Size(598, 74);
            this.uiNavigator1.TabIndex = 4;
            this.uiNavigator1.onAfterSaveEdit += new SentraWinFramework.AfterAction(this.uiNavigator1_onAfterSaveEdit);
            this.uiNavigator1.onAfterSaveNew += new SentraWinFramework.AfterAction(this.uiNavigator1_onAfterSaveNew);
            // 
            // nonPostingCheckEdit
            // 
            this.nonPostingCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "NonPosting", true));
            this.nonPostingCheckEdit.Location = new System.Drawing.Point(88, 106);
            this.nonPostingCheckEdit.Name = "nonPostingCheckEdit";
            this.nonPostingCheckEdit.Properties.Caption = "Akun Kelompok";
            this.nonPostingCheckEdit.Size = new System.Drawing.Size(100, 19);
            this.nonPostingCheckEdit.TabIndex = 6;
            // 
            // akunIndukLookUpEdit
            // 
            this.akunIndukLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "AkunInduk", true));
            this.akunIndukLookUpEdit.Location = new System.Drawing.Point(88, 54);
            this.akunIndukLookUpEdit.Name = "akunIndukLookUpEdit";
            this.akunIndukLookUpEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.akunIndukLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.akunIndukLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NoAkun", "NoAkun", 43, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("NamaAkun", "NamaAkun", 57, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.akunIndukLookUpEdit.Properties.DataSource = this.akunBindingSource1;
            this.akunIndukLookUpEdit.Properties.DisplayMember = "NoAkun";
            this.akunIndukLookUpEdit.Properties.NullText = "";
            this.akunIndukLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.akunIndukLookUpEdit.Properties.ValueMember = "NoAkun";
            this.akunIndukLookUpEdit.Size = new System.Drawing.Size(135, 20);
            this.akunIndukLookUpEdit.TabIndex = 7;
            // 
            // akunIDTextEdit
            // 
            this.akunIDTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "AkunID", true));
            this.akunIDTextEdit.Location = new System.Drawing.Point(478, 12);
            this.akunIDTextEdit.Name = "akunIDTextEdit";
            this.akunIDTextEdit.Properties.ReadOnly = true;
            this.akunIDTextEdit.Size = new System.Drawing.Size(100, 20);
            this.akunIDTextEdit.TabIndex = 9;
            // 
            // parentIDTextEdit
            // 
            this.parentIDTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.akunBindingSource, "ParentID", true));
            this.parentIDTextEdit.Location = new System.Drawing.Point(478, 38);
            this.parentIDTextEdit.Name = "parentIDTextEdit";
            this.parentIDTextEdit.Properties.ReadOnly = true;
            this.parentIDTextEdit.Size = new System.Drawing.Size(100, 20);
            this.parentIDTextEdit.TabIndex = 11;
            // 
            // namaAkunIndukLabel1
            // 
            this.namaAkunIndukLabel1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.akunBindingSource, "NamaAkunInduk", true));
            this.namaAkunIndukLabel1.Location = new System.Drawing.Point(229, 54);
            this.namaAkunIndukLabel1.Name = "namaAkunIndukLabel1";
            this.namaAkunIndukLabel1.Size = new System.Drawing.Size(100, 20);
            this.namaAkunIndukLabel1.TabIndex = 13;
            // 
            // akunBindingSource
            // 
            this.akunBindingSource.DataSource = typeof(Examples01.Entity.Akun);
            // 
            // akunBindingSource1
            // 
            this.akunBindingSource1.DataSource = typeof(Examples01.Entity.Akun);
            // 
            // frmAkun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 276);
            this.Controls.Add(this.namaAkunIndukLabel1);
            this.Controls.Add(parentIDLabel);
            this.Controls.Add(this.parentIDTextEdit);
            this.Controls.Add(akunIDLabel);
            this.Controls.Add(this.akunIDTextEdit);
            this.Controls.Add(akunIndukLabel);
            this.Controls.Add(this.akunIndukLookUpEdit);
            this.Controls.Add(this.nonPostingCheckEdit);
            this.Controls.Add(this.uiNavigator1);
            this.Controls.Add(namaAkunLabel);
            this.Controls.Add(this.namaAkunTextEdit);
            this.Controls.Add(noAkunLabel);
            this.Controls.Add(this.noAkunTextEdit);
            this.Name = "frmAkun";
            this.Text = "frmAkun";
            ((System.ComponentModel.ISupportInitialize)(this.noAkunTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaAkunTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nonPostingCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunIndukLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunIDTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.parentIDTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.akunBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource akunBindingSource;
        private DevExpress.XtraEditors.TextEdit noAkunTextEdit;
        private DevExpress.XtraEditors.TextEdit namaAkunTextEdit;
        private SentraWinFramework.UINavigator uiNavigator1;
        private DevExpress.XtraEditors.CheckEdit nonPostingCheckEdit;
        private DevExpress.XtraEditors.LookUpEdit akunIndukLookUpEdit;
        private System.Windows.Forms.BindingSource akunBindingSource1;
        private DevExpress.XtraEditors.TextEdit akunIDTextEdit;
        private DevExpress.XtraEditors.TextEdit parentIDTextEdit;
        private System.Windows.Forms.Label namaAkunIndukLabel1;
    }
}