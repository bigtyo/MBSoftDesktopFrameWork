using System.Windows.Forms;
namespace SentraWinSecurity.License
{
    partial class frmRegistration
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.modulesBindingSource = new BindingSource(this.components);
            this.registrationBindingSource = new BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colModuleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModuleValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modulesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Nama Aplikasi";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 62);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Diregistrasi Oleh";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 88);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(87, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Nama Perusahaan";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.modulesBindingSource;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(120, 199);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(222, 91);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // modulesBindingSource
            // 
            this.modulesBindingSource.DataMember = "Modules";
            this.modulesBindingSource.DataSource = this.registrationBindingSource;
            // 
            // registrationBindingSource
            // 
            this.registrationBindingSource.DataSource = typeof(SentraSecurity.License.Registration);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colModuleName,
            this.colModuleValue});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView1.OptionsFilter.AllowFilterEditor = false;
            this.gridView1.OptionsFilter.AllowMRUFilterList = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.OptionsView.ShowPreviewLines = false;
            // 
            // colModuleName
            // 
            this.colModuleName.Caption = "Nama Modul";
            this.colModuleName.FieldName = "ModuleName";
            this.colModuleName.Name = "colModuleName";
            this.colModuleName.OptionsColumn.ReadOnly = true;
            this.colModuleName.OptionsFilter.AllowAutoFilter = false;
            this.colModuleName.OptionsFilter.AllowFilter = false;
            this.colModuleName.Visible = true;
            this.colModuleName.VisibleIndex = 0;
            this.colModuleName.Width = 103;
            // 
            // colModuleValue
            // 
            this.colModuleValue.Caption = "Nilai";
            this.colModuleValue.FieldName = "ModuleValue";
            this.colModuleValue.Name = "colModuleValue";
            this.colModuleValue.Visible = true;
            this.colModuleValue.VisibleIndex = 1;
            this.colModuleValue.Width = 98;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(16, 111);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(55, 13);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "Batas Pakai";
            // 
            // labelControl5
            // 
            this.labelControl5.DataBindings.Add(new Binding("Text", this.registrationBindingSource, "AppName", true));
            this.labelControl5.Location = new System.Drawing.Point(120, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(65, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Nama Aplikasi";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new Binding("EditValue", this.registrationBindingSource, "UserName", true, DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new System.Drawing.Point(120, 59);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(222, 20);
            this.textEdit1.TabIndex = 0;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new Binding("EditValue", this.registrationBindingSource, "CompanyName", true, DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit2.Location = new System.Drawing.Point(120, 85);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(222, 20);
            this.textEdit2.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(16, 199);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(54, 13);
            this.labelControl6.TabIndex = 0;
            this.labelControl6.Text = "Data Modul";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Location = new System.Drawing.Point(120, 111);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Latihan/ Demo (Max 200 transaksi)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Sampai dengan"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "Tanpa Batas Waktu")});
            this.radioGroup1.Size = new System.Drawing.Size(222, 82);
            this.radioGroup1.TabIndex = 2;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.BackColor = System.Drawing.Color.White;
            this.labelControl7.Appearance.Options.UseBackColor = true;
            this.labelControl7.Location = new System.Drawing.Point(275, 145);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(26, 13);
            this.labelControl7.TabIndex = 4;
            this.labelControl7.Text = "bulan";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(168, 364);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(97, 25);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "&Registrasi";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(16, 296);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(63, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "No Registrasi";
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(271, 363);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(71, 25);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "&Tutup";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(16, 322);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(64, 13);
            this.labelControl9.TabIndex = 0;
            this.labelControl9.Text = "Kode Aktivasi";
            // 
            // textEdit4
            // 
            this.textEdit4.DataBindings.Add(new Binding("EditValue", this.registrationBindingSource, "ActivationCode", true));
            this.textEdit4.Location = new System.Drawing.Point(120, 319);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(222, 20);
            this.textEdit4.TabIndex = 7;
            this.textEdit4.KeyDown += new KeyEventHandler(this.textEdit4_KeyDown);
            // 
            // spinEdit1
            // 
            this.spinEdit1.DataBindings.Add(new Binding("EditValue", this.registrationBindingSource, "MonthLimitation", true));
            this.spinEdit1.EditValue = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.spinEdit1.Enabled = false;
            this.spinEdit1.Location = new System.Drawing.Point(214, 142);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(55, 20);
            this.spinEdit1.TabIndex = 3;
            // 
            // labelControl10
            // 
            this.labelControl10.DataBindings.Add(new Binding("Text", this.registrationBindingSource, "RegistrationNo", true, DataSourceUpdateMode.OnPropertyChanged));
            this.labelControl10.Location = new System.Drawing.Point(120, 296);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(65, 13);
            this.labelControl10.TabIndex = 0;
            this.labelControl10.Text = "Nama Aplikasi";
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(16, 34);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(76, 13);
            this.labelControl11.TabIndex = 0;
            this.labelControl11.Text = "Mesin Database";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(120, 34);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(65, 13);
            this.labelControl12.TabIndex = 0;
            this.labelControl12.Text = "Nama Aplikasi";
            // 
            // frmRegistration
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(358, 400);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.textEdit4);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "frmRegistration";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Register Aplikasi";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modulesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.SpinEdit spinEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private System.Windows.Forms.BindingSource modulesBindingSource;
        private System.Windows.Forms.BindingSource registrationBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colModuleName;
        private DevExpress.XtraGrid.Columns.GridColumn colModuleValue;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl12;
    }
}