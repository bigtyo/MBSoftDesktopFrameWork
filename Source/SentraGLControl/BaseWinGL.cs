using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;
using SentraWinFramework;
using SentraGL.Properties;
using SentraGL.Report.Neraca;
using SentraWinSecurity;
using SentraGL.Report.LabaRugi;
using SentraGL.Report;
using SentraGL.Report.MutasiAkun;
using SentraGL.Report.RingkasanAkun;
using SentraSecurity;
using SentraGL.Document;
using SentraGL.Master;

namespace SentraGL
{
    public class BaseWinGL
    {
        public static PopupMenu RegisterPopNeraca(
            string FolderName,
            BarButtonItem ButtonItem)
        {
            ButtonItem.ActAsDropDown = true;
            ButtonItem.ButtonStyle = BarButtonStyle.DropDown;

            PopupMenu ppm = BaseWinFramework.CreatePopupMenu();
            ButtonItem.DropDownControl = ppm;

            FolderName += "\\Neraca";
            string ReportName;

            #region Register Report
            ReportName = "Neraca (Level)";
            BarButtonItem btn = BaseWinFramework.AddButtonItem(
                ReportName, false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaLvl>(
                ReportName, FolderName, btn, 
                ReportType.FreeLayout);
            ReportName = "Neraca (Kelompok)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false, 
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaKlp>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);

            ReportName = "Neraca Perbandingan (Level)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, true, 
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaPerbandinganLvl>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);
            ReportName = "Neraca Perbandingan (Kelompok)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaPerbandinganKlp>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);

            ReportName = "Neraca Multi Periode (Level)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, true,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaMultiPeriodeLvl>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);
            ReportName = "Neraca Multi Periode (Kelompok)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaMultiPeriodeKlp>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);

            ReportName = "Neraca Bulanan (Level)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, true,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaBulananLvl>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);
            ReportName = "Neraca Bulanan (Kelompok)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaBulananKlp>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);

            ReportName = "Neraca Bebas";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, true,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptNeracaBebas>(
                ReportName, FolderName, btn,
                ReportType.FreeLayout);
            #endregion

            return ppm;
        }

        public static PopupMenu RegisterPopLabaRugi(
            string FolderName,
            BarButtonItem ButtonItem)
        {
            ButtonItem.ActAsDropDown = true;
            ButtonItem.ButtonStyle = BarButtonStyle.DropDown;

            PopupMenu ppm = BaseWinFramework.CreatePopupMenu();
            ButtonItem.DropDownControl = ppm;

            FolderName += "\\Laba/ Rugi";

            #region Register Report
            BarButtonItem btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi", false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugi>(
                "Laba/ Rugi", FolderName, btn,
                ReportType.FreeLayout);

            btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi Bulanan", true,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugiBulanan>(
                "Laba/ Rugi Bulanan", FolderName, btn,
                ReportType.FreeLayout);

            btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi Perbandingan", false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugiPerbandingan>(
                "Laba/ Rugi Perbandingan", FolderName, btn,
                ReportType.FreeLayout);
            btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi Multi Periode", false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugiMultiPeriode>(
                "Laba/ Rugi Multi Periode", FolderName, btn,
                ReportType.FreeLayout);
            btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi Tahunan", false,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugiTahunan>(
                "Laba/ Rugi Tahunan", FolderName, btn,
                ReportType.FreeLayout);

            btn = BaseWinFramework.AddButtonItem(
                "Laba/ Rugi Bebas", true,
                ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptLabaRugiBebas>(
                "Laba/ Rugi Bebas", FolderName, btn,
                ReportType.FreeLayout);
            #endregion

            return ppm;
        }

        public static void RegisterPosisiAkun(
            string FolderName,
            BarButtonItem ButtonItem)
        {
            ButtonItem.ActAsDropDown = false;
            ButtonItem.ButtonStyle = BarButtonStyle.Default;

            BaseWinSecurity.RegisterReportModule<rptPosisiAkun>(
                "Posisi Akun", FolderName, ButtonItem,
                ReportType.Tabular);
        }
        public static void RegisterPopMutasiAkun(
            string FolderName,
            BarButtonItem ButtonItem)
        {
            ButtonItem.ActAsDropDown = true;
            ButtonItem.ButtonStyle = BarButtonStyle.DropDown;

            PopupMenu ppm = BaseWinFramework.CreatePopupMenu();
            ButtonItem.DropDownControl = ppm;

            FolderName += "\\Mutasi Akun";
            string ReportName;

            #region Register Report
            ReportName = "Mutasi Semua Akun";
            BarButtonItem btn = BaseWinFramework.AddButtonItem(
                ReportName, false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptMutasiSemuaAkun>(
                ReportName, FolderName, btn,
                ReportType.Tabular);

            ReportName = "Mutasi Akun Tertentu";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptMutasiAkunTertentu>(
                ReportName, FolderName, btn,
                ReportType.Tabular);
            #endregion
        }
        public static void RegisterPopRingkasanAkun(
            string FolderName,
            BarButtonItem ButtonItem)
        {
            ButtonItem.ActAsDropDown = true;
            ButtonItem.ButtonStyle = BarButtonStyle.DropDown;

            PopupMenu ppm = BaseWinFramework.CreatePopupMenu();
            ButtonItem.DropDownControl = ppm;

            FolderName += "\\Ringkasan Akun";
            string ReportName;

            #region Register Report
            ReportName = "Ringkasan Akun (Level)";
            BarButtonItem btn = BaseWinFramework.AddButtonItem(
                ReportName, false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptRingkasanAkunLvl>(
                ReportName, FolderName, btn,
                ReportType.Tabular);

            ReportName = "Ringkasan Akun (Kelompok)";
            btn = BaseWinFramework.AddButtonItem(
                ReportName, false, ppm, null, Resources.document);
            BaseWinSecurity.RegisterReportModule<rptRingkasanAkunKlp>(
                ReportName, FolderName, btn,
                ReportType.Tabular);
            #endregion
        }

        public static void ShowSetingPerusahaan()
        {
            BaseWinFramework.ShowDocumentFormDialog
                <frmSetingPerusahaan>();
        }
        public static void ShowPenguncianTgl()
        {
            frmLockDate frm = new frmLockDate();
            frm.ShowDialog(BaseWinFramework.MdiParent);
        }
    }
}
