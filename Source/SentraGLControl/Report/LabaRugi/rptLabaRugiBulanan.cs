using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Master;
using SentraGL.Properties;

namespace SentraGL.Report.LabaRugi
{
    internal partial class rptLabaRugiBulanan : ReportForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        LabaRugiBulanan LabaRugi = new LabaRugiBulanan();

        public rptLabaRugiBulanan()
        {
            InitializeComponent();
            labaRugiBulananBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                DateTime AwalBlnIni = new DateTime(LabaRugi.Tahun,
                    (int)LabaRugi.Bulan, 1);
                DateTime AkhirBlnIni = AwalBlnIni.AddMonths(1)
                    .AddDays(-1);
                DateTime AwalBlnLalu = AwalBlnIni.AddMonths(-1);
                DateTime AkhirBlnLalu = AwalBlnIni.AddDays(-1);
                DateTime AwalTahun = new DateTime(LabaRugi.Tahun, 1, 1);

                BaseGL.RingkasanAkun.Update(AkhirBlnIni);
                string strNilai1 = Dp.GetSqlCoalesce("LRBulanIni", 0);
                string strNilai2 = Dp.GetSqlCoalesce("LRBulanLalu", 0);
                string strNilai3 = Dp.GetSqlCoalesce("LRSampaiBulanIni", 0);
                List<FieldParam> Parameters = new List<FieldParam>();

                string DataSource = string.Concat(
                    "SELECT NoAkun,NamaAkun,UrutanKelompok,KelompokAkun,",
                    strNilai1, " as LRBulanIni,", strNilai2, " as LRBulanLalu,",
                    strNilai3, " as LRSampaiBulanIni FROM (SELECT UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnIni, AkhirBlnIni, false, "1", Parameters),
                    ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LRBulanIni,(SELECT SUM(Debit-Kredit) FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnLalu, AkhirBlnLalu, false, "2", Parameters),
                    ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LRBulanLalu,(SELECT SUM(Debit-Kredit) FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(AwalTahun, AkhirBlnIni, false, "3", Parameters),
                    ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LRSampaiBulanIni FROM Akun b WHERE Posting<>0 AND JenisAkun=",
                    Dp.FormatSqlValue(enJenisAkun.Laba__Rugi),
                    ") X WHERE Aktif<>0");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                retVal.Add("Tahun", LabaRugi.Tahun);
                retVal.Add("Bulan", (int)LabaRugi.Bulan);
                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "UrutanKelompok,NoAkun");
                return retVal;
            }
            set { }
        }
    }

    internal class LabaRugiBulanan
    {
        private int _Tahun;
        public int Tahun
        {
            get { return _Tahun; }
            set { _Tahun = value; }
        }

        private enNamaBulan _Bulan;
        public enNamaBulan Bulan
        {
            get { return _Bulan; }
            set { _Bulan = value; }
        }

        public LabaRugiBulanan()
        {
            DateTime Tgl = DateTime.Today.AddMonths(-1);
            _Tahun = Tgl.Year;
            _Bulan = (enNamaBulan)Tgl.Month;
        }
    }
}