namespace Template
{
    partial class MdiMenu1
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barFile = new DevExpress.XtraBars.BarSubItem();
            this.btnLogin = new DevExpress.XtraBars.BarButtonItem();
            this.btnLogout = new DevExpress.XtraBars.BarButtonItem();
            this.btnUbahPassword = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetingTampilan = new DevExpress.XtraBars.BarButtonItem();
            this.btnKeluar = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barSistem = new DevExpress.XtraBars.BarSubItem();
            this.btnUserDanHakAkses = new DevExpress.XtraBars.BarButtonItem();
            this.btnLogAktivitas = new DevExpress.XtraBars.BarButtonItem();
            this.btnManajemenDatabase = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barFile,
            this.btnLogin,
            this.btnLogout,
            this.btnUbahPassword,
            this.btnSetingTampilan,
            this.btnKeluar,
            this.barSubItem2,
            this.barSistem,
            this.barButtonItem1,
            this.barMdiChildrenListItem1,
            this.btnUserDanHakAkses,
            this.btnLogAktivitas,
            this.btnManajemenDatabase});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 14;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Custom 2";
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 3";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barFile),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSistem)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Custom 3";
            // 
            // barFile
            // 
            this.barFile.Caption = "&File";
            this.barFile.Id = 0;
            this.barFile.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLogin),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLogout),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUbahPassword, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSetingTampilan),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnKeluar, true)});
            this.barFile.Name = "barFile";
            // 
            // btnLogin
            // 
            this.btnLogin.Caption = "&Login";
            this.btnLogin.Id = 1;
            this.btnLogin.Name = "btnLogin";
            // 
            // btnLogout
            // 
            this.btnLogout.Caption = "Log&out";
            this.btnLogout.Id = 2;
            this.btnLogout.Name = "btnLogout";
            // 
            // btnUbahPassword
            // 
            this.btnUbahPassword.Caption = "&Ubah Password";
            this.btnUbahPassword.Id = 3;
            this.btnUbahPassword.Name = "btnUbahPassword";
            // 
            // btnSetingTampilan
            // 
            this.btnSetingTampilan.Caption = "&Seting Tampilan...";
            this.btnSetingTampilan.Id = 4;
            this.btnSetingTampilan.Name = "btnSetingTampilan";
            // 
            // btnKeluar
            // 
            this.btnKeluar.Caption = "&Keluar";
            this.btnKeluar.Id = 5;
            this.btnKeluar.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.btnKeluar.Name = "btnKeluar";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "&";
            this.barSubItem2.Id = 6;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // barSistem
            // 
            this.barSistem.Caption = "&Sistem";
            this.barSistem.Id = 7;
            this.barSistem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnUserDanHakAkses),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnLogAktivitas),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnManajemenDatabase)});
            this.barSistem.Name = "barSistem";
            // 
            // btnUserDanHakAkses
            // 
            this.btnUserDanHakAkses.Caption = "&User dan Hak Akses";
            this.btnUserDanHakAkses.Id = 11;
            this.btnUserDanHakAkses.Name = "btnUserDanHakAkses";
            // 
            // btnLogAktivitas
            // 
            this.btnLogAktivitas.Caption = "&Log Aktivitas";
            this.btnLogAktivitas.Id = 12;
            this.btnLogAktivitas.Name = "btnLogAktivitas";
            // 
            // btnManajemenDatabase
            // 
            this.btnManajemenDatabase.Caption = "&Manajemen Database";
            this.btnManajemenDatabase.Id = 13;
            this.btnManajemenDatabase.Name = "btnManajemenDatabase";
            // 
            // bar3
            // 
            this.bar3.BarName = "Custom 4";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Custom 4";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 8;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barMdiChildrenListItem1
            // 
            this.barMdiChildrenListItem1.Caption = "barMdiChildrenListItem1";
            this.barMdiChildrenListItem1.Id = 10;
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // MdiFramework1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 355);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "MdiFramework1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MdiFramework1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barFile;
        private DevExpress.XtraBars.BarButtonItem btnLogin;
        private DevExpress.XtraBars.BarButtonItem btnLogout;
        private DevExpress.XtraBars.BarButtonItem btnUbahPassword;
        private DevExpress.XtraBars.BarButtonItem btnSetingTampilan;
        private DevExpress.XtraBars.BarButtonItem btnKeluar;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarSubItem barSistem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarMdiChildrenListItem barMdiChildrenListItem1;
        private DevExpress.XtraBars.BarButtonItem btnUserDanHakAkses;
        private DevExpress.XtraBars.BarButtonItem btnLogAktivitas;
        private DevExpress.XtraBars.BarButtonItem btnManajemenDatabase;
    }
}