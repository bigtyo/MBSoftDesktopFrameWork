using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    internal partial class rptLabaRugi : ReportForm    
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        LabaRugi LabaRugi = new LabaRugi();

        public rptLabaRugi()
        {
            InitializeComponent();
            labaRugiBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                List<FieldParam> Parameters = new List<FieldParam>();
                string DataSource = string.Concat(
                    "SELECT NoAkun,NamaAkun,KelompokAkun,UrutanKelompok,", 
                    Dp.GetSqlCoalesce("Saldo", 0), " as LabaRugi FROM (SELECT NoAkun,NamaAkun,KelompokAkun,UrutanKelompok,(SELECT Debit-Kredit FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwal, 
                    LabaRugi.TglAkhir, true, "1", Parameters), 
                    ") a WHERE a.IdAkun=b.IdAkun) as Saldo FROM Akun b WHERE Posting<>0 AND JenisAkun=", 
                    Dp.FormatSqlValue(enJenisAkun.Laba__Rugi), 
                    ") X");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                retVal.Add("TglAwal", LabaRugi.TglAwal);
                retVal.Add("TglAkhir", LabaRugi.TglAkhir);
                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "UrutanKelompok,NoAkun");
                return retVal;
            }
            set { }
        }
    }

    internal class LabaRugi
    {
        private DateTime _TglAwal;
        public DateTime TglAwal
        {
            get { return _TglAwal; }
            set { _TglAwal = value; }
        }

        private DateTime _TglAkhir;
        public DateTime TglAkhir
        {
            get { return _TglAkhir; }
            set { _TglAkhir = value; }
        }

        public LabaRugi()
        {
            _TglAwal = DateTime.Today.AddMonths(-1);
            _TglAwal = new DateTime(_TglAwal.Year, _TglAwal.Month, 1);
            _TglAkhir = _TglAwal.AddMonths(1).AddDays(-1);
        }
    }
}
