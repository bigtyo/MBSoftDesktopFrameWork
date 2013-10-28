namespace Examples01
{
    partial class frmItem
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
            System.Windows.Forms.Label kodeItemLabel;
            System.Windows.Forms.Label namaItemLabel;
            System.Windows.Forms.Label keteranganLabel;
            System.Windows.Forms.Label tglRegisterLabel;
            this.kodeItemTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.namaItemTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.keteranganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.tglRegisterDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.photoPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.uiNavigator1 = new SentraWinFramework.UINavigator();
            kodeItemLabel = new System.Windows.Forms.Label();
            namaItemLabel = new System.Windows.Forms.Label();
            keteranganLabel = new System.Windows.Forms.Label();
            tglRegisterLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kodeItemTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaItemTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglRegisterDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // kodeItemLabel
            // 
            kodeItemLabel.AutoSize = true;
            kodeItemLabel.Location = new System.Drawing.Point(138, 25);
            kodeItemLabel.Name = "kodeItemLabel";
            kodeItemLabel.Size = new System.Drawing.Size(60, 13);
            kodeItemLabel.TabIndex = 1;
            kodeItemLabel.Text = "Kode Item:";
            // 
            // namaItemLabel
            // 
            namaItemLabel.AutoSize = true;
            namaItemLabel.Location = new System.Drawing.Point(135, 51);
            namaItemLabel.Name = "namaItemLabel";
            namaItemLabel.Size = new System.Drawing.Size(63, 13);
            namaItemLabel.TabIndex = 2;
            namaItemLabel.Text = "Nama Item:";
            // 
            // keteranganLabel
            // 
            keteranganLabel.AutoSize = true;
            keteranganLabel.Location = new System.Drawing.Point(131, 77);
            keteranganLabel.Name = "keteranganLabel";
            keteranganLabel.Size = new System.Drawing.Size(67, 13);
            keteranganLabel.TabIndex = 4;
            keteranganLabel.Text = "Keterangan:";
            // 
            // tglRegisterLabel
            // 
            tglRegisterLabel.AutoSize = true;
            tglRegisterLabel.Location = new System.Drawing.Point(129, 128);
            tglRegisterLabel.Name = "tglRegisterLabel";
            tglRegisterLabel.Size = new System.Drawing.Size(68, 13);
            tglRegisterLabel.TabIndex = 7;
            tglRegisterLabel.Text = "Tgl Register:";
            // 
            // kodeItemTextEdit
            // 
            this.kodeItemTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "KodeItem", true));
            this.kodeItemTextEdit.Location = new System.Drawing.Point(202, 22);
            this.kodeItemTextEdit.Name = "kodeItemTextEdit";
            this.kodeItemTextEdit.Size = new System.Drawing.Size(100, 20);
            this.kodeItemTextEdit.TabIndex = 2;
            // 
            // itemBindingSource
            // 
            this.itemBindingSource.DataSource = typeof(Examples01.Entity.Item);
            // 
            // namaItemTextEdit
            // 
            this.namaItemTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "NamaItem", true));
            this.namaItemTextEdit.Location = new System.Drawing.Point(202, 48);
            this.namaItemTextEdit.Name = "namaItemTextEdit";
            this.namaItemTextEdit.Size = new System.Drawing.Size(169, 20);
            this.namaItemTextEdit.TabIndex = 3;
            // 
            // keteranganTextEdit
            // 
            this.keteranganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "Keterangan", true));
            this.keteranganTextEdit.Location = new System.Drawing.Point(202, 74);
            this.keteranganTextEdit.Name = "keteranganTextEdit";
            this.keteranganTextEdit.Size = new System.Drawing.Size(169, 20);
            this.keteranganTextEdit.TabIndex = 5;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(202, 100);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "&Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 7;
            // 
            // tglRegisterDateEdit
            // 
            this.tglRegisterDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "TglRegister", true));
            this.tglRegisterDateEdit.EditValue = null;
            this.tglRegisterDateEdit.Location = new System.Drawing.Point(202, 125);
            this.tglRegisterDateEdit.Name = "tglRegisterDateEdit";
            this.tglRegisterDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tglRegisterDateEdit.Properties.DisplayFormat.FormatString = "dd MMM yyyy";
            this.tglRegisterDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.tglRegisterDateEdit.Properties.Mask.EditMask = "dd MMM yyyy";
            this.tglRegisterDateEdit.Size = new System.Drawing.Size(100, 20);
            this.tglRegisterDateEdit.TabIndex = 8;
            // 
            // photoPictureEdit
            // 
            this.photoPictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.itemBindingSource, "Photo", true));
            this.photoPictureEdit.Location = new System.Drawing.Point(12, 22);
            this.photoPictureEdit.Name = "photoPictureEdit";
            this.photoPictureEdit.Size = new System.Drawing.Size(100, 123);
            this.photoPictureEdit.TabIndex = 10;
            // 
            // uiNavigator1
            // 
            this.uiNavigator1.BindingSource = this.itemBindingSource;
            this.uiNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiNavigator1.Location = new System.Drawing.Point(0, 210);
            this.uiNavigator1.Name = "uiNavigator1";
            this.uiNavigator1.Size = new System.Drawing.Size(582, 74);
            this.uiNavigator1.TabIndex = 11;
            // 
            // frmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 284);
            this.Controls.Add(this.uiNavigator1);
            this.Controls.Add(this.photoPictureEdit);
            this.Controls.Add(tglRegisterLabel);
            this.Controls.Add(this.tglRegisterDateEdit);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(keteranganLabel);
            this.Controls.Add(this.keteranganTextEdit);
            this.Controls.Add(namaItemLabel);
            this.Controls.Add(this.namaItemTextEdit);
            this.Controls.Add(kodeItemLabel);
            this.Controls.Add(this.kodeItemTextEdit);
            this.Name = "frmItem";
            this.Text = "Master Item";
            ((System.ComponentModel.ISupportInitialize)(this.kodeItemTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaItemTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.keteranganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tglRegisterDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource itemBindingSource;
        private DevExpress.XtraEditors.TextEdit kodeItemTextEdit;
        private DevExpress.XtraEditors.TextEdit namaItemTextEdit;
        private DevExpress.XtraEditors.TextEdit keteranganTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private DevExpress.XtraEditors.DateEdit tglRegisterDateEdit;
        private DevExpress.XtraEditors.PictureEdit photoPictureEdit;
        private SentraWinFramework.UINavigator uiNavigator1;
    }
}