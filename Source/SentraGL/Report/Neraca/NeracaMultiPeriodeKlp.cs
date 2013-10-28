using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaMultiPeriodeKlp : ReportEntity
    {
        private DateTime _TglNeraca1;
        public DateTime TglNeraca1
        {
            get { return _TglNeraca1; }
            set { _TglNeraca1 = value; }
        }

        private DateTime _TglNeraca2;
        public DateTime TglNeraca2
        {
            get { return _TglNeraca2; }
            set { _TglNeraca2 = value; }
        }

        private DateTime _TglNeraca3;
        public DateTime TglNeraca3
        {
            get { return _TglNeraca3; }
            set { _TglNeraca3 = value; }
        }

        private DateTime _TglNeraca4;
        public DateTime TglNeraca4
        {
            get { return _TglNeraca4; }
            set { _TglNeraca4 = value; }
        }

        public NeracaMultiPeriodeKlp()
        {
            _TglNeraca4 = new DateTime(DateTime.Today.Year,
                DateTime.Today.Month, 1);
            _TglNeraca3 = _TglNeraca4.AddMonths(-3);
            _TglNeraca2 = _TglNeraca3.AddMonths(-3);
            _TglNeraca1 = _TglNeraca2.AddMonths(-3);

            _TglNeraca1 = _TglNeraca1.AddDays(-1);
            _TglNeraca2 = _TglNeraca2.AddDays(-1);
            _TglNeraca3 = _TglNeraca3.AddDays(-1);
            _TglNeraca4 = _TglNeraca4.AddDays(-1);
        }

        private DateTime GetTglMaksimum()
        {
            DateTime tmp = _TglNeraca1;
            if (_TglNeraca2 > tmp) tmp = _TglNeraca2;
            if (_TglNeraca3 > tmp) tmp = _TglNeraca3;
            if (_TglNeraca4 > tmp) tmp = _TglNeraca4;

            return tmp;
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            BaseGL.RingkasanAkun.Update(GetTglMaksimum());

            DataSource = string.Concat(
                "SELECT NoAkun,NamaAkun,JenisAkun,UrutanKelompok,KelompokAkun,",
                Dp.GetSqlCoalesce("Neraca1", 0), " as Neraca1,",
                Dp.GetSqlCoalesce("Neraca2", 0), " as Neraca2,",
                Dp.GetSqlCoalesce("Neraca3", 0), " as Neraca3,",
                Dp.GetSqlCoalesce("Neraca4", 0), " as Neraca4",
                @" FROM 
                (SELECT JenisAkun,UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Aktif,
                  (SELECT SUM(Debit-Kredit) FROM 
                    (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca1, false,
                  "1", Parameters),
                @") k WHERE k.IdAkun=q.IdAkun OR k.IdAkun LIKE q.IdAkun+'.%'
                  ) as Neraca1,
                  (SELECT SUM(Debit-Kredit) FROM 
                    (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca2, false,
                  "2", Parameters),
                @") l WHERE l.IdAkun=q.IdAkun OR l.IdAkun LIKE q.IdAkun+'.%'
                  ) as Neraca2,
                  (SELECT SUM(Debit-Kredit) FROM 
                    (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca3, false,
                  "3", Parameters),
                @") m WHERE m.IdAkun=q.IdAkun OR m.IdAkun LIKE q.IdAkun+'.%'
                  ) as Neraca3,
                  (SELECT SUM(Debit-Kredit) FROM 
                    (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca4, false,
                  "4", Parameters),
                @") n WHERE n.IdAkun=q.IdAkun OR n.IdAkun LIKE q.IdAkun+'.%'
                  ) as Neraca4 FROM Akun q WHERE Posting<>0 AND JenisAkun<>",
                FormatSqlValue(enJenisAkun.Laba__Rugi),
                ") XX WHERE Aktif<>0");
            DataSourceOrder = "UrutanKelompok,NoAkun";
        }

        //protected override void BeforeShowReport(
        //    Dictionary<string, object> Variables)
        //{
        //    Variables.Add("Umum", BaseGL.ReportUmum);
        //}

        protected override void BeforePrint(Evaluator ev)
        {
            ev.ObjValues.Add("Umum", BaseGL.ReportUmum);
        }
    }
}
