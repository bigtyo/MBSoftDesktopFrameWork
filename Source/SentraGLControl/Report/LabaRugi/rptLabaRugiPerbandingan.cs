using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraUtility;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Master;
using SentraGL.Properties;

namespace SentraGL.Report.LabaRugi
{
    public partial class rptLabaRugiPerbandingan : ReportForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        LabaRugiPerbandingan LabaRugi = new LabaRugiPerbandingan();

        public rptLabaRugiPerbandingan()
        {
            InitializeComponent();
            labaRugiPerbandinganBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                BaseGL.RingkasanAkun.Update(LabaRugi.GetTglMaksimum());
                string strNilai1 = Dp.GetSqlCoalesce("LabaRugi1", 0);
                string strNilai2 = Dp.GetSqlCoalesce("LabaRugi2", 0);
                List<FieldParam> Parameters = new List<FieldParam>();

                string DataSource = string.Concat(
                    "SELECT NoAkun,NamaAkun,UrutanKelompok,KelompokAkun,",
                    strNilai1, " as LabaRugi1,", strNilai2, " as LabaRugi2,",
                    strNilai2, "-", strNilai1, " as Selisih,",
                        Dp.GetSqlIifNoFormat(strNilai1 + "=0",
                        Dp.GetSqlIifNoFormat(strNilai2 + "<0", "-100",
                            Dp.GetSqlIifNoFormat(strNilai2 + ">0", "100", "null")),
                        string.Concat("(", strNilai2, "-", strNilai1, ")*100/",
                        Dp.GetSqlAbs(strNilai1))), " as [% Selisih] FROM (SELECT UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi1, LabaRugi.TglAkhirLabaRugi1, false, "1", Parameters), 
                    ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi1,(SELECT SUM(Debit-Kredit) FROM (",
                    BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi2, LabaRugi.TglAkhirLabaRugi2, false, "2", Parameters), 
                    ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi2 FROM Akun b WHERE Posting<>0 AND JenisAkun=", 
                    Dp.FormatSqlValue(enJenisAkun.Laba__Rugi),
                    ") X WHERE Aktif<>0");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);

                retVal.Add("TglAwal1", LabaRugi.TglAwalLabaRugi1);
                retVal.Add("TglAkhir1", LabaRugi.TglAkhirLabaRugi1);

                retVal.Add("TglAwal2", LabaRugi.TglAwalLabaRugi2);
                retVal.Add("TglAkhir2", LabaRugi.TglAkhirLabaRugi2);

                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "UrutanKelompok,NoAkun");
                return retVal;

            }
            set { }
        }
    }

    internal class LabaRugiPerbandingan
    {
        private DateTime _TglAwalLabaRugi1;
        public DateTime TglAwalLabaRugi1
        {
            get { return _TglAwalLabaRugi1; }
            set { _TglAwalLabaRugi1 = value; }
        }

        private DateTime _TglAkhirLabaRugi1;
        public DateTime TglAkhirLabaRugi1
        {
            get { return _TglAkhirLabaRugi1; }
            set { _TglAkhirLabaRugi1 = value; }
        }

        private DateTime _TglAwalLabaRugi2;
        public DateTime TglAwalLabaRugi2
        {
            get { return _TglAwalLabaRugi2; }
            set { _TglAwalLabaRugi2 = value; }
        }

        private DateTime _TglAkhirLabaRugi2;
        public DateTime TglAkhirLabaRugi2
        {
            get { return _TglAkhirLabaRugi2; }
            set { _TglAkhirLabaRugi2 = value; }
        }

        public LabaRugiPerbandingan()
        {
            _TglAwalLabaRugi2 = BaseUtility.GetStartMonth(
                DateTime.Today);
            _TglAwalLabaRugi1 = BaseUtility.GetStartMonth(
                _TglAwalLabaRugi2.AddMonths(-3));

            _TglAkhirLabaRugi1 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi1);
            _TglAkhirLabaRugi2 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi2);
        }

        public DateTime GetTglMaksimum()
        {
            DateTime Tmp = _TglAkhirLabaRugi2;
            if (_TglAwalLabaRugi2 > Tmp) Tmp = _TglAwalLabaRugi2;

            if (_TglAkhirLabaRugi1 > Tmp) Tmp = _TglAkhirLabaRugi1;
            if (_TglAwalLabaRugi1 > Tmp) Tmp = _TglAwalLabaRugi1;

            return Tmp;
        }
    }
}
