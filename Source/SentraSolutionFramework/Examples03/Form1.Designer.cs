namespace Examples03
{
    partial class Form1
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
            System.Windows.Forms.Label noPelangganLabel;
            System.Windows.Forms.Label namaPelangganLabel;
            System.Windows.Forms.Label alamatLabel;
            this.noPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.pelanggganBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.namaPelangganTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.alamatTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.aktifCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.uiNavigator1 = new SentraWinFramework.UINavigator();
            this.photoPictureEdit = new DevExpress.XtraEditors.PictureEdit();
            noPelangganLabel = new System.Windows.Forms.Label();
            namaPelangganLabel = new System.Windows.Forms.Label();
            alamatLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelanggganBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // noPelangganLabel
            // 
            noPelangganLabel.AutoSize = true;
            noPelangganLabel.Location = new System.Drawing.Point(37, 27);
            noPelangganLabel.Name = "noPelangganLabel";
            noPelangganLabel.Size = new System.Drawing.Size(78, 13);
            noPelangganLabel.TabIndex = 1;
            noPelangganLabel.Text = "No Pelanggan:";
            // 
            // namaPelangganLabel
            // 
            namaPelangganLabel.AutoSize = true;
            namaPelangganLabel.Location = new System.Drawing.Point(23, 53);
            namaPelangganLabel.Name = "namaPelangganLabel";
            namaPelangganLabel.Size = new System.Drawing.Size(92, 13);
            namaPelangganLabel.TabIndex = 2;
            namaPelangganLabel.Text = "Nama Pelanggan:";
            // 
            // alamatLabel
            // 
            alamatLabel.AutoSize = true;
            alamatLabel.Location = new System.Drawing.Point(73, 79);
            alamatLabel.Name = "alamatLabel";
            alamatLabel.Size = new System.Drawing.Size(42, 13);
            alamatLabel.TabIndex = 4;
            alamatLabel.Text = "Alamat:";
            // 
            // noPelangganTextEdit
            // 
            this.noPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelanggganBindingSource, "NoPelanggan", true));
            this.noPelangganTextEdit.Location = new System.Drawing.Point(121, 24);
            this.noPelangganTextEdit.Name = "noPelangganTextEdit";
            this.noPelangganTextEdit.Size = new System.Drawing.Size(100, 20);
            this.noPelangganTextEdit.TabIndex = 2;
            // 
            // pelanggganBindingSource
            // 
            this.pelanggganBindingSource.DataSource = typeof(Examples03.Pelangggan);
            // 
            // namaPelangganTextEdit
            // 
            this.namaPelangganTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelanggganBindingSource, "NamaPelanggan", true));
            this.namaPelangganTextEdit.Location = new System.Drawing.Point(121, 50);
            this.namaPelangganTextEdit.Name = "namaPelangganTextEdit";
            this.namaPelangganTextEdit.Size = new System.Drawing.Size(100, 20);
            this.namaPelangganTextEdit.TabIndex = 3;
            // 
            // alamatTextEdit
            // 
            this.alamatTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelanggganBindingSource, "Alamat", true));
            this.alamatTextEdit.Location = new System.Drawing.Point(121, 76);
            this.alamatTextEdit.Name = "alamatTextEdit";
            this.alamatTextEdit.Size = new System.Drawing.Size(100, 20);
            this.alamatTextEdit.TabIndex = 5;
            // 
            // aktifCheckEdit
            // 
            this.aktifCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelanggganBindingSource, "Aktif", true));
            this.aktifCheckEdit.Location = new System.Drawing.Point(121, 102);
            this.aktifCheckEdit.Name = "aktifCheckEdit";
            this.aktifCheckEdit.Properties.Caption = "Aktif";
            this.aktifCheckEdit.Size = new System.Drawing.Size(75, 19);
            this.aktifCheckEdit.TabIndex = 7;
            // 
            // uiNavigator1
            // 
            this.uiNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiNavigator1.Location = new System.Drawing.Point(0, 146);
            this.uiNavigator1.Name = "uiNavigator1";
            this.uiNavigator1.Size = new System.Drawing.Size(586, 75);
            this.uiNavigator1.TabIndex = 8;
            // 
            // photoPictureEdit
            // 
            this.photoPictureEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.pelanggganBindingSource, "Photo", true));
            this.photoPictureEdit.Location = new System.Drawing.Point(242, 12);
            this.photoPictureEdit.Name = "photoPictureEdit";
            this.photoPictureEdit.Size = new System.Drawing.Size(100, 96);
            this.photoPictureEdit.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 221);
            this.Controls.Add(this.photoPictureEdit);
            this.Controls.Add(this.uiNavigator1);
            this.Controls.Add(this.aktifCheckEdit);
            this.Controls.Add(alamatLabel);
            this.Controls.Add(this.alamatTextEdit);
            this.Controls.Add(namaPelangganLabel);
            this.Controls.Add(this.namaPelangganTextEdit);
            this.Controls.Add(noPelangganLabel);
            this.Controls.Add(this.noPelangganTextEdit);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.noPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelanggganBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.namaPelangganTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alamatTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktifCheckEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.photoPictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource pelanggganBindingSource;
        private DevExpress.XtraEditors.TextEdit noPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit namaPelangganTextEdit;
        private DevExpress.XtraEditors.TextEdit alamatTextEdit;
        private DevExpress.XtraEditors.CheckEdit aktifCheckEdit;
        private SentraWinFramework.UINavigator uiNavigator1;
        private DevExpress.XtraEditors.PictureEdit photoPictureEdit;
    }
}

