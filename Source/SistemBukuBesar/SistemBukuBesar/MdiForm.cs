using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using SentraWinFramework;
using SentraWinSecurity;
using SentraGL.Master;
using SentraGL;
using SentraSolutionFramework;
using SentraSecurity;
using SentraGL.Document;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace SistemBukuBesar
{
    public partial class MdiForm : RibbonForm
    {
        public MdiForm()
        {
            using (new WaitCursor())
            {
                InitializeComponent();

                BaseWinFramework.ShowProgressBar(Text);

                BaseSecurity.LoginWithRole = false;

                BaseFramework.DpEngine
                    .RegisterEngine<SqlServerPersistance>();
                BaseFramework.DpEngine
                    .RegisterEngine<AccessPersistance>();

                //BaseFramework.EnableWriteLog = true;

                ribbonControl1.SelectedPage = ribbonPage4;

                BaseWinSecurity.Init(this, null, null,
                    true, pgAksesSistem);

                BaseGL.SetingPerusahaan.OnEntityAction += 
                    new EntityAction(SetingPerusahaan_OnEntityAction); 
                
                TableDef td = MetaData.GetTableDef<Departemen>();
                FieldDef fld = td.GetFieldDef("DepartemenProduksi");
                fld.IsHidden = true;

                BaseWinSecurity.ListAdminButton.Add(mnPenguncianTgl);

                BaseWinSecurity.ListLoginButton.Add(mnNeraca);
                BaseWinSecurity.ListLoginButton.Add(mnLabaRugi);
                BaseWinSecurity.ListLoginButton.Add(mnMutasiAkun);
                BaseWinSecurity.ListLoginButton.Add(mnRingkasanAkun);

                string FolderName;

                #region Menu Keuangan
                FolderName = "Keuangan\\Transaksi";
                BaseWinSecurity.RegisterDocumentModule
                    <DocPenerimaanKasUmum>(PenerimaanKasUmum.ModuleName,
                    FolderName, mnPenerimaanKasUmum);
                BaseWinSecurity.RegisterDocumentModule
                    <DocPengeluaranKasUmum>("Pengeluaran Kas Umum", FolderName,
                    mnPengeluaranKasUmum);
                BaseWinSecurity.RegisterDocumentModule
                    <DocTransferAntarKas>("Transfer Antar Kas", FolderName,
                    mnTransferKas);
                BaseWinSecurity.RegisterDocumentModule
                    <DocPerintahBayar>("Perintah Bayar", FolderName,
                    mnPerintahBayar);
                BaseWinSecurity.RegisterDocumentModule
                    <DocPengeluaranPerintahBayar>(
                    "Pengeluaran Uang dari Perintah Bayar", FolderName,
                    mnPengeluaranDariPerintahBayar);

                FolderName = "Keuangan\\Master";
                BaseWinSecurity.RegisterDocumentModule
                    <DocJenisPenerimaanKas>("Jenis Penerimaan Kas", FolderName,
                    mnJenisPenerimaanKas);
                BaseWinSecurity.RegisterDocumentModule
                    <DocJenisPengeluaranKas>("Jenis Pengeluaran Kas", FolderName,
                    mnJenisPengeluaranKas);
                #endregion

                #region Menu Buku Besar
                FolderName = "Buku Besar\\Transaksi";
                BaseWinSecurity.RegisterDocumentModule
                    <DocJurnal>("Jurnal Umum", FolderName, mnJurnalUmum);
                BaseWinSecurity.RegisterDocumentModule
                    <DocKursHarian>("Kurs Harian", FolderName, mnKursHarian);

                FolderName = "Buku Besar\\Laporan";
                BaseWinGL.RegisterPopNeraca(FolderName, mnNeraca);
                BaseWinGL.RegisterPopLabaRugi(FolderName, mnLabaRugi);
                BaseWinGL.RegisterPosisiAkun(FolderName, mnPosisiAkun);
                BaseWinGL.RegisterPopMutasiAkun(FolderName, mnMutasiAkun);
                BaseWinGL.RegisterPopRingkasanAkun(FolderName, mnRingkasanAkun);

                FolderName = "Buku Besar\\Master";
                BaseWinSecurity.RegisterDocumentModule
                    <DocAkun>("Akun", FolderName, mnAkun);
                BaseWinSecurity.RegisterDocumentModule
                    <DocAturanJurnal>("Aturan Jurnal", FolderName, mnAturanJurnal);
                BaseWinSecurity.RegisterDocumentModule
                    <DocJenisDokSumberJurnal>("Jenis Dok. Sumber",
                    FolderName, mnJenisDokSumberJurnal);
                BaseWinSecurity.RegisterDocumentModule
                    <DocDepartemen>("Departemen", FolderName, mnDepartemen);
                BaseWinSecurity.RegisterDocumentModule
                    <DocProyek>("Proyek", FolderName, mnProyek);
                BaseWinSecurity.RegisterDocumentModule
                    <DocMataUang>("Mata Uang", FolderName, mnMataUang);

                BaseWinSecurity.RegisterDocumentModule
                    <DocSaldoAwalAkun>("Saldo Awal Akun",
                    "Sistem\\Saldo Awal", mnSaldoAwalAkun);
                BaseWinSecurity.RegisterDocumentModule
                    <DocNilaiTukarSaldoAwal>("Nilai Tukar Saldo Awal",
                    "Sistem\\Saldo Awal", mnNilaiTukarSaldoAwal);
                #endregion

                BaseWinSecurity.RegisterSingleDocumentModule<frmSetingPerusahaan>(
                    "Seting Perusahaan", "Sistem", mnSetingPerusahaan);
            }
        }

        void SetingPerusahaan_OnEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            if (ActionMode == enEntityActionMode.AfterLoadFound) 
            {
                SetingPerusahaan sp = ActionEntity as SetingPerusahaan;
                if (sp == null) return;

                BarItemVisibility vis = sp.MultiDepartemen ?
                    BarItemVisibility.Always : BarItemVisibility.Never;
                mnDepartemen.Visibility = vis;

                vis = sp.MultiProyek ?
                    BarItemVisibility.Always : BarItemVisibility.Never;
                mnProyek.Visibility = vis;

                vis = sp.MultiMataUang ?
                    BarItemVisibility.Always : BarItemVisibility.Never;
                mnMataUang.Visibility = vis;
                mnKursHarian.Visibility = vis;
                mnNilaiTukarSaldoAwal.Visibility = vis;
            }
        }

        private void mnPenguncianTgl_ItemClick(object sender, ItemClickEventArgs e)
        {
            eventClient1.ShowWarningList();
            //MataUang mu = new MataUang();
            //mu.LoadEntity("KodeMataUang='IDR'");
            //eventClient1.SendEntityChanged(mu);
            //BaseWinGL.ShowPenguncianTgl();
        }
    }
}