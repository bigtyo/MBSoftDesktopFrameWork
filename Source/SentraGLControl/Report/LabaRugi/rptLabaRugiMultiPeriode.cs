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
    internal partial class rptLabaRugiMultiPeriode : ReportForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        LabaRugiMultiPeriode LabaRugi = new LabaRugiMultiPeriode();

        public rptLabaRugiMultiPeriode()
        {
            InitializeComponent();
            labaRugiMultiPeriodeBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                BaseGL.RingkasanAkun.Update(LabaRugi.GetTglMaksimum());
                List<FieldParam> Parameters = new List<FieldParam>();

                string DataSource = string.Concat(
"SELECT NoAkun,NamaAkun,UrutanKelompok,KelompokAkun,",
Dp.GetSqlCoalesce("LabaRugi1", 0), " as LabaRugi1,",
Dp.GetSqlCoalesce("LabaRugi2", 0), " as LabaRugi2,",
Dp.GetSqlCoalesce("LabaRugi3", 0), " as LabaRugi3,",
Dp.GetSqlCoalesce("LabaRugi4", 0), " as LabaRugi4 ",
@" FROM (SELECT UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi1, LabaRugi.TglAkhirLabaRugi1, false, "1", Parameters), ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi1,(SELECT SUM(Debit-Kredit) FROM (",
BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi2, LabaRugi.TglAkhirLabaRugi2, false, "2", Parameters), ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi2,(SELECT SUM(Debit-Kredit) FROM (",
BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi3, LabaRugi.TglAkhirLabaRugi3, false, "3", Parameters), ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi3,(SELECT SUM(Debit-Kredit) FROM (",
BaseGL.RingkasanAkun.SqlMutasiAkun(LabaRugi.TglAwalLabaRugi4, LabaRugi.TglAkhirLabaRugi4, false, "4", Parameters), ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as LabaRugi4 FROM Akun b WHERE Posting<>0 AND JenisAkun=", 
Dp.FormatSqlValue(enJenisAkun.Laba__Rugi),
") X WHERE Aktif<>0");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                
                retVal.Add("TglAwal1", LabaRugi.TglAwalLabaRugi1);
                retVal.Add("TglAkhir1", LabaRugi.TglAkhirLabaRugi1);

                retVal.Add("TglAwal2", LabaRugi.TglAwalLabaRugi2);
                retVal.Add("TglAkhir2", LabaRugi.TglAkhirLabaRugi2);

                retVal.Add("TglAwal3", LabaRugi.TglAwalLabaRugi3);
                retVal.Add("TglAkhir3", LabaRugi.TglAkhirLabaRugi3);

                retVal.Add("TglAwal4", LabaRugi.TglAwalLabaRugi4);
                retVal.Add("TglAkhir4", LabaRugi.TglAkhirLabaRugi4);

                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "UrutanKelompok,NoAkun");
                return retVal;
            }
            set { }
        }
    }

    internal class LabaRugiMultiPeriode
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

        private DateTime _TglAwalLabaRugi3;
        public DateTime TglAwalLabaRugi3
        {
            get { return _TglAwalLabaRugi3; }
            set { _TglAwalLabaRugi3 = value; }
        }

        private DateTime _TglAkhirLabaRugi3;
        public DateTime TglAkhirLabaRugi3
        {
            get { return _TglAkhirLabaRugi3; }
            set { _TglAkhirLabaRugi3 = value; }
        }

        private DateTime _TglAwalLabaRugi4;
        public DateTime TglAwalLabaRugi4
        {
            get { return _TglAwalLabaRugi4; }
            set { _TglAwalLabaRugi4 = value; }
        }

        private DateTime _TglAkhirLabaRugi4;
        public DateTime TglAkhirLabaRugi4
        {
            get { return _TglAkhirLabaRugi4; }
            set { _TglAkhirLabaRugi4 = value; }
        }

        public LabaRugiMultiPeriode()
        {
            _TglAwalLabaRugi4 = BaseUtility.GetStartMonth(
                DateTime.Today);
            _TglAwalLabaRugi3 = BaseUtility.GetStartMonth(
                _TglAwalLabaRugi4.AddMonths(-3));
            _TglAwalLabaRugi2 = BaseUtility.GetStartMonth(
                _TglAwalLabaRugi3.AddMonths(-3));
            _TglAwalLabaRugi1 = BaseUtility.GetStartMonth(
                _TglAwalLabaRugi2.AddMonths(-3));

            _TglAkhirLabaRugi4 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi4);
            _TglAkhirLabaRugi3 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi3);
            _TglAkhirLabaRugi2 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi2);
            _TglAkhirLabaRugi1 = BaseUtility.GetEndMonth(
                _TglAwalLabaRugi1);
        }

        public DateTime GetTglMaksimum()
        {
            DateTime Tmp = _TglAkhirLabaRugi4;
            if (_TglAwalLabaRugi4 > Tmp) Tmp = _TglAwalLabaRugi4;
            
            if (_TglAkhirLabaRugi3 > Tmp) Tmp = _TglAkhirLabaRugi3;
            if (_TglAwalLabaRugi3 > Tmp) Tmp = _TglAwalLabaRugi3;

            if (_TglAkhirLabaRugi2 > Tmp) Tmp = _TglAkhirLabaRugi2;
            if (_TglAwalLabaRugi2 > Tmp) Tmp = _TglAwalLabaRugi2;

            if (_TglAkhirLabaRugi1 > Tmp) Tmp = _TglAkhirLabaRugi1;
            if (_TglAwalLabaRugi1 > Tmp) Tmp = _TglAwalLabaRugi1;

            return Tmp;
        }
    }
}
