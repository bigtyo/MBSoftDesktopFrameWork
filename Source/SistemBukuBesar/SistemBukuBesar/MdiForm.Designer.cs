using System.Windows.Forms;
namespace SistemBukuBesar
{
    partial class MdiForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiForm));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.imageCollectionSmall = new DevExpress.Utils.ImageCollection(this.components);
            this.mnJurnalUmum = new DevExpress.XtraBars.BarButtonItem();
            this.mnAkun = new DevExpress.XtraBars.BarButtonItem();
            this.mnAturanJurnal = new DevExpress.XtraBars.BarButtonItem();
            this.mnJenisDokSumberJurnal = new DevExpress.XtraBars.BarButtonItem();
            this.mnDepartemen = new DevExpress.XtraBars.BarButtonItem();
            this.mnMataUang = new DevExpress.XtraBars.BarButtonItem();
            this.mnKursHarian = new DevExpress.XtraBars.BarButtonItem();
            this.mnPenerimaanKasUmum = new DevExpress.XtraBars.BarButtonItem();
            this.mnPengeluaranKasUmum = new DevExpress.XtraBars.BarButtonItem();
            this.mnJenisPenerimaanKas = new DevExpress.XtraBars.BarButtonItem();
            this.mnJenisPengeluaranKas = new DevExpress.XtraBars.BarButtonItem();
            this.mnNeraca = new DevExpress.XtraBars.BarButtonItem();
            this.mnLabaRugi = new DevExpress.XtraBars.BarButtonItem();
            this.mnProyek = new DevExpress.XtraBars.BarButtonItem();
            this.mnPosisiAkun = new DevExpress.XtraBars.BarButtonItem();
            this.mnSetingPerusahaan = new DevExpress.XtraBars.BarButtonItem();
            this.mnArusKas = new DevExpress.XtraBars.BarButtonItem();
            this.mnMutasiAkun = new DevExpress.XtraBars.BarButtonItem();
            this.mnRingkasanAkun = new DevExpress.XtraBars.BarButtonItem();
            this.mnTransferKas = new DevExpress.XtraBars.BarButtonItem();
            this.mnPenguncianTgl = new DevExpress.XtraBars.BarButtonItem();
            this.mnSaldoAwalAkun = new DevExpress.XtraBars.BarButtonItem();
            this.mnNilaiTukarSaldoAwal = new DevExpress.XtraBars.BarButtonItem();
            this.mnPerintahBayar = new DevExpress.XtraBars.BarButtonItem();
            this.mnPengeluaranDariPerintahBayar = new DevExpress.XtraBars.BarButtonItem();
            this.mnKliringCekGiro = new DevExpress.XtraBars.BarButtonItem();
            this.imageCollectionLarge = new DevExpress.Utils.ImageCollection(this.components);
            this.ribbonPage4 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup13 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup15 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage5 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage7 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.pgAksesSistem = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup18 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup19 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.eventClient1 = new BxEventClient.EventClient(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionLarge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonKeyTip = "";
            this.ribbonControl1.ApplicationIcon = global::SistemBukuBesar.Properties.Resources.cashier;
            this.ribbonControl1.Images = this.imageCollectionSmall;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.mnJurnalUmum,
            this.mnAkun,
            this.mnAturanJurnal,
            this.mnJenisDokSumberJurnal,
            this.mnDepartemen,
            this.mnMataUang,
            this.mnKursHarian,
            this.mnPenerimaanKasUmum,
            this.mnPengeluaranKasUmum,
            this.mnJenisPenerimaanKas,
            this.mnJenisPengeluaranKas,
            this.mnNeraca,
            this.mnLabaRugi,
            this.mnProyek,
            this.mnPosisiAkun,
            this.mnSetingPerusahaan,
            this.mnArusKas,
            this.mnMutasiAkun,
            this.mnRingkasanAkun,
            this.mnTransferKas,
            this.mnPenguncianTgl,
            this.mnSaldoAwalAkun,
            this.mnNilaiTukarSaldoAwal,
            this.mnPerintahBayar,
            this.mnPengeluaranDariPerintahBayar,
            this.mnKliringCekGiro});
            this.ribbonControl1.LargeImages = this.imageCollectionLarge;
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 90;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Right;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage4,
            this.ribbonPage5,
            this.ribbonPage7});
            this.ribbonControl1.SelectedPage = this.ribbonPage4;
            this.ribbonControl1.Size = new System.Drawing.Size(891, 143);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // imageCollectionSmall
            // 
            this.imageCollectionSmall.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionSmall.ImageStream")));
            // 
            // mnJurnalUmum
            // 
            this.mnJurnalUmum.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnJurnalUmum.Caption = "Jurnal Umum";
            this.mnJurnalUmum.Id = 18;
            this.mnJurnalUmum.ImageIndex = 20;
            this.mnJurnalUmum.Name = "mnJurnalUmum";
            // 
            // mnAkun
            // 
            this.mnAkun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnAkun.Caption = "Akun";
            this.mnAkun.Id = 26;
            this.mnAkun.ImageIndex = 45;
            this.mnAkun.Name = "mnAkun";
            // 
            // mnAturanJurnal
            // 
            this.mnAturanJurnal.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnAturanJurnal.Caption = "Aturan Jurnal";
            this.mnAturanJurnal.Id = 27;
            this.mnAturanJurnal.ImageIndex = 10;
            this.mnAturanJurnal.Name = "mnAturanJurnal";
            // 
            // mnJenisDokSumberJurnal
            // 
            this.mnJenisDokSumberJurnal.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnJenisDokSumberJurnal.Caption = "Jenis Dok. Sumber";
            this.mnJenisDokSumberJurnal.Id = 28;
            this.mnJenisDokSumberJurnal.ImageIndex = 31;
            this.mnJenisDokSumberJurnal.Name = "mnJenisDokSumberJurnal";
            // 
            // mnDepartemen
            // 
            this.mnDepartemen.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnDepartemen.Caption = "Departemen";
            this.mnDepartemen.Id = 29;
            this.mnDepartemen.ImageIndex = 44;
            this.mnDepartemen.Name = "mnDepartemen";
            // 
            // mnMataUang
            // 
            this.mnMataUang.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnMataUang.Caption = "Mata Uang";
            this.mnMataUang.Id = 30;
            this.mnMataUang.ImageIndex = 30;
            this.mnMataUang.Name = "mnMataUang";
            this.mnMataUang.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // mnKursHarian
            // 
            this.mnKursHarian.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnKursHarian.Caption = "Kurs Harian";
            this.mnKursHarian.Id = 32;
            this.mnKursHarian.ImageIndex = 11;
            this.mnKursHarian.Name = "mnKursHarian";
            this.mnKursHarian.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // mnPenerimaanKasUmum
            // 
            this.mnPenerimaanKasUmum.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnPenerimaanKasUmum.Caption = "Penerimaan Kas Umum";
            this.mnPenerimaanKasUmum.Id = 33;
            this.mnPenerimaanKasUmum.ImageIndex = 30;
            this.mnPenerimaanKasUmum.Name = "mnPenerimaanKasUmum";
            // 
            // mnPengeluaranKasUmum
            // 
            this.mnPengeluaranKasUmum.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnPengeluaranKasUmum.Caption = "Pengeluaran Kas Umum";
            this.mnPengeluaranKasUmum.Id = 34;
            this.mnPengeluaranKasUmum.ImageIndex = 14;
            this.mnPengeluaranKasUmum.Name = "mnPengeluaranKasUmum";
            // 
            // mnJenisPenerimaanKas
            // 
            this.mnJenisPenerimaanKas.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnJenisPenerimaanKas.Caption = "Jenis Penerimaan";
            this.mnJenisPenerimaanKas.Id = 36;
            this.mnJenisPenerimaanKas.ImageIndex = 19;
            this.mnJenisPenerimaanKas.Name = "mnJenisPenerimaanKas";
            // 
            // mnJenisPengeluaranKas
            // 
            this.mnJenisPengeluaranKas.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnJenisPengeluaranKas.Caption = "Jenis Pengeluaran";
            this.mnJenisPengeluaranKas.Id = 37;
            this.mnJenisPengeluaranKas.ImageIndex = 18;
            this.mnJenisPengeluaranKas.Name = "mnJenisPengeluaranKas";
            // 
            // mnNeraca
            // 
            this.mnNeraca.ActAsDropDown = true;
            this.mnNeraca.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnNeraca.Caption = "Neraca";
            this.mnNeraca.Id = 38;
            this.mnNeraca.ImageIndex = 16;
            this.mnNeraca.Name = "mnNeraca";
            // 
            // mnLabaRugi
            // 
            this.mnLabaRugi.ActAsDropDown = true;
            this.mnLabaRugi.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnLabaRugi.Caption = "Laba/ Rugi";
            this.mnLabaRugi.Id = 39;
            this.mnLabaRugi.ImageIndex = 13;
            this.mnLabaRugi.Name = "mnLabaRugi";
            // 
            // mnProyek
            // 
            this.mnProyek.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnProyek.Caption = "Proyek";
            this.mnProyek.Id = 40;
            this.mnProyek.ImageIndex = 25;
            this.mnProyek.Name = "mnProyek";
            // 
            // mnPosisiAkun
            // 
            this.mnPosisiAkun.Caption = "Posisi Akun";
            this.mnPosisiAkun.Id = 41;
            this.mnPosisiAkun.ImageIndex = 22;
            this.mnPosisiAkun.Name = "mnPosisiAkun";
            // 
            // mnSetingPerusahaan
            // 
            this.mnSetingPerusahaan.Caption = "Seting Perusahaan";
            this.mnSetingPerusahaan.Id = 42;
            this.mnSetingPerusahaan.ImageIndex = 43;
            this.mnSetingPerusahaan.Name = "mnSetingPerusahaan";
            // 
            // mnArusKas
            // 
            this.mnArusKas.Caption = "Arus Kas";
            this.mnArusKas.Id = 44;
            this.mnArusKas.ImageIndex = 15;
            this.mnArusKas.Name = "mnArusKas";
            // 
            // mnMutasiAkun
            // 
            this.mnMutasiAkun.ActAsDropDown = true;
            this.mnMutasiAkun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnMutasiAkun.Caption = "Mutasi Akun";
            this.mnMutasiAkun.Id = 45;
            this.mnMutasiAkun.ImageIndex = 59;
            this.mnMutasiAkun.Name = "mnMutasiAkun";
            // 
            // mnRingkasanAkun
            // 
            this.mnRingkasanAkun.ActAsDropDown = true;
            this.mnRingkasanAkun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnRingkasanAkun.Caption = "Ringkasan Akun";
            this.mnRingkasanAkun.Id = 46;
            this.mnRingkasanAkun.ImageIndex = 4;
            this.mnRingkasanAkun.Name = "mnRingkasanAkun";
            // 
            // mnTransferKas
            // 
            this.mnTransferKas.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnTransferKas.Caption = "Transfer Kas/ Bank";
            this.mnTransferKas.Id = 64;
            this.mnTransferKas.ImageIndex = 6;
            this.mnTransferKas.Name = "mnTransferKas";
            // 
            // mnPenguncianTgl
            // 
            this.mnPenguncianTgl.Caption = "Penguncian Tgl";
            this.mnPenguncianTgl.Id = 71;
            this.mnPenguncianTgl.ImageIndex = 34;
            this.mnPenguncianTgl.Name = "mnPenguncianTgl";
            this.mnPenguncianTgl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mnPenguncianTgl_ItemClick);
            // 
            // mnSaldoAwalAkun
            // 
            this.mnSaldoAwalAkun.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnSaldoAwalAkun.Caption = "Saldo Awal Akun";
            this.mnSaldoAwalAkun.Id = 81;
            this.mnSaldoAwalAkun.ImageIndex = 10;
            this.mnSaldoAwalAkun.Name = "mnSaldoAwalAkun";
            // 
            // mnNilaiTukarSaldoAwal
            // 
            this.mnNilaiTukarSaldoAwal.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnNilaiTukarSaldoAwal.Caption = "Nilai Tukar Saldo Awal";
            this.mnNilaiTukarSaldoAwal.Id = 82;
            this.mnNilaiTukarSaldoAwal.ImageIndex = 53;
            this.mnNilaiTukarSaldoAwal.Name = "mnNilaiTukarSaldoAwal";
            this.mnNilaiTukarSaldoAwal.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // mnPerintahBayar
            // 
            this.mnPerintahBayar.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnPerintahBayar.Caption = "Perintah Bayar";
            this.mnPerintahBayar.Id = 86;
            this.mnPerintahBayar.ImageIndex = 20;
            this.mnPerintahBayar.Name = "mnPerintahBayar";
            // 
            // mnPengeluaranDariPerintahBayar
            // 
            this.mnPengeluaranDariPerintahBayar.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnPengeluaranDariPerintahBayar.Caption = "Pengeluaran dari Perintah Bayar";
            this.mnPengeluaranDariPerintahBayar.Id = 87;
            this.mnPengeluaranDariPerintahBayar.ImageIndex = 15;
            this.mnPengeluaranDariPerintahBayar.Name = "mnPengeluaranDariPerintahBayar";
            // 
            // mnKliringCekGiro
            // 
            this.mnKliringCekGiro.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.mnKliringCekGiro.Caption = "Kliring Cek/ Giro";
            this.mnKliringCekGiro.Id = 88;
            this.mnKliringCekGiro.ImageIndex = 26;
            this.mnKliringCekGiro.Name = "mnKliringCekGiro";
            this.mnKliringCekGiro.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // imageCollectionLarge
            // 
            this.imageCollectionLarge.ImageSize = new System.Drawing.Size(32, 32);
            this.imageCollectionLarge.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollectionLarge.ImageStream")));
            // 
            // ribbonPage4
            // 
            this.ribbonPage4.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup13,
            this.ribbonPageGroup15});
            this.ribbonPage4.KeyTip = "";
            this.ribbonPage4.Name = "ribbonPage4";
            this.ribbonPage4.Text = "Keuangan";
            // 
            // ribbonPageGroup13
            // 
            this.ribbonPageGroup13.ItemLinks.Add(this.mnPenerimaanKasUmum);
            this.ribbonPageGroup13.ItemLinks.Add(this.mnPengeluaranKasUmum);
            this.ribbonPageGroup13.ItemLinks.Add(this.mnTransferKas);
            this.ribbonPageGroup13.ItemLinks.Add(this.mnPerintahBayar);
            this.ribbonPageGroup13.ItemLinks.Add(this.mnPengeluaranDariPerintahBayar);
            this.ribbonPageGroup13.ItemLinks.Add(this.mnKliringCekGiro);
            this.ribbonPageGroup13.KeyTip = "";
            this.ribbonPageGroup13.Name = "ribbonPageGroup13";
            this.ribbonPageGroup13.ShowCaptionButton = false;
            this.ribbonPageGroup13.Text = "Transaksi";
            // 
            // ribbonPageGroup15
            // 
            this.ribbonPageGroup15.ItemLinks.Add(this.mnJenisPenerimaanKas);
            this.ribbonPageGroup15.ItemLinks.Add(this.mnJenisPengeluaranKas);
            this.ribbonPageGroup15.KeyTip = "";
            this.ribbonPageGroup15.Name = "ribbonPageGroup15";
            this.ribbonPageGroup15.ShowCaptionButton = false;
            this.ribbonPageGroup15.Text = "Master";
            // 
            // ribbonPage5
            // 
            this.ribbonPage5.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4,
            this.ribbonPageGroup5,
            this.ribbonPageGroup6});
            this.ribbonPage5.KeyTip = "";
            this.ribbonPage5.Name = "ribbonPage5";
            this.ribbonPage5.Text = "Buku Besar";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.mnJurnalUmum);
            this.ribbonPageGroup4.ItemLinks.Add(this.mnKursHarian);
            this.ribbonPageGroup4.KeyTip = "";
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.ShowCaptionButton = false;
            this.ribbonPageGroup4.Text = "Transaksi";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.mnNeraca);
            this.ribbonPageGroup5.ItemLinks.Add(this.mnLabaRugi);
            this.ribbonPageGroup5.ItemLinks.Add(this.mnArusKas);
            this.ribbonPageGroup5.ItemLinks.Add(this.mnPosisiAkun);
            this.ribbonPageGroup5.ItemLinks.Add(this.mnMutasiAkun);
            this.ribbonPageGroup5.ItemLinks.Add(this.mnRingkasanAkun);
            this.ribbonPageGroup5.KeyTip = "";
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.ShowCaptionButton = false;
            this.ribbonPageGroup5.Text = "Laporan";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.mnAkun);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnAturanJurnal);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnJenisDokSumberJurnal);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnMataUang);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnDepartemen);
            this.ribbonPageGroup6.ItemLinks.Add(this.mnProyek);
            this.ribbonPageGroup6.KeyTip = "";
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.ShowCaptionButton = false;
            this.ribbonPageGroup6.Text = "Master";
            // 
            // ribbonPage7
            // 
            this.ribbonPage7.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pgAksesSistem,
            this.ribbonPageGroup18,
            this.ribbonPageGroup19});
            this.ribbonPage7.KeyTip = "";
            this.ribbonPage7.Name = "ribbonPage7";
            this.ribbonPage7.Text = "Sistem";
            // 
            // pgAksesSistem
            // 
            this.pgAksesSistem.KeyTip = "";
            this.pgAksesSistem.Name = "pgAksesSistem";
            this.pgAksesSistem.ShowCaptionButton = false;
            this.pgAksesSistem.Text = "Akses Sistem";
            // 
            // ribbonPageGroup18
            // 
            this.ribbonPageGroup18.ItemLinks.Add(this.mnSetingPerusahaan);
            this.ribbonPageGroup18.ItemLinks.Add(this.mnPenguncianTgl);
            this.ribbonPageGroup18.KeyTip = "";
            this.ribbonPageGroup18.Name = "ribbonPageGroup18";
            this.ribbonPageGroup18.ShowCaptionButton = false;
            this.ribbonPageGroup18.Text = "Seting Sistem";
            // 
            // ribbonPageGroup19
            // 
            this.ribbonPageGroup19.ItemLinks.Add(this.mnSaldoAwalAkun);
            this.ribbonPageGroup19.ItemLinks.Add(this.mnNilaiTukarSaldoAwal);
            this.ribbonPageGroup19.KeyTip = "";
            this.ribbonPageGroup19.Name = "ribbonPageGroup19";
            this.ribbonPageGroup19.ShowCaptionButton = false;
            this.ribbonPageGroup19.Text = "Saldo Awal";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 456);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(891, 25);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPagesAndTabControlHeader;
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // eventClient1
            // 
            this.eventClient1.ContainerControl = this;
            this.eventClient1.Password = "";
            // 
            // MdiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 481);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MdiForm";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Sistem Buku Besar";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionSmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollectionLarge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage4;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup13;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup15;
        private DevExpress.XtraBars.BarButtonItem mnJurnalUmum;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage7;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup pgAksesSistem;
        private DevExpress.XtraBars.BarButtonItem mnAkun;
        private DevExpress.XtraBars.BarButtonItem mnAturanJurnal;
        private DevExpress.XtraBars.BarButtonItem mnJenisDokSumberJurnal;
        private DevExpress.XtraBars.BarButtonItem mnDepartemen;
        private DevExpress.XtraBars.BarButtonItem mnMataUang;
        private DevExpress.XtraBars.BarButtonItem mnKursHarian;
        private DevExpress.XtraBars.BarButtonItem mnPenerimaanKasUmum;
        private DevExpress.XtraBars.BarButtonItem mnPengeluaranKasUmum;
        private DevExpress.XtraBars.BarButtonItem mnJenisPenerimaanKas;
        private DevExpress.XtraBars.BarButtonItem mnJenisPengeluaranKas;
        private DevExpress.XtraBars.BarButtonItem mnNeraca;
        private DevExpress.XtraBars.BarButtonItem mnLabaRugi;
        private DevExpress.XtraBars.BarButtonItem mnProyek;
        private DevExpress.XtraBars.BarButtonItem mnPosisiAkun;
        private DevExpress.XtraBars.BarButtonItem mnSetingPerusahaan;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup18;
        private DevExpress.XtraBars.BarButtonItem mnArusKas;
        private DevExpress.XtraBars.BarButtonItem mnMutasiAkun;
        private DevExpress.XtraBars.BarButtonItem mnRingkasanAkun;
        private DevExpress.Utils.ImageCollection imageCollectionSmall;
        private DevExpress.Utils.ImageCollection imageCollectionLarge;
        private DevExpress.XtraBars.BarButtonItem mnTransferKas;
        private DevExpress.XtraBars.BarButtonItem mnPenguncianTgl;
        private DevExpress.XtraBars.BarButtonItem mnSaldoAwalAkun;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup19;
        private DevExpress.XtraBars.BarButtonItem mnNilaiTukarSaldoAwal;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarButtonItem mnPerintahBayar;
        private DevExpress.XtraBars.BarButtonItem mnPengeluaranDariPerintahBayar;
        private DevExpress.XtraBars.BarButtonItem mnKliringCekGiro;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private BxEventClient.EventClient eventClient1;
    }
}

