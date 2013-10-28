using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaPerbandinganLevel : ReportEntity
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
        
        private int _LevelCetak = 4;
        [DataTypeInteger]
        public int LevelCetak
        {
            get { return _LevelCetak; }
            set { _LevelCetak = value; }
        }

        public NeracaPerbandinganLevel()
        {
            _TglNeraca2 = new DateTime(DateTime.Today.Year,
                DateTime.Today.Month, 1);
            _TglNeraca1 = _TglNeraca2.AddMonths(-3).AddDays(-1);

            _TglNeraca2 = _TglNeraca2.AddDays(-1);
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            BaseGL.RingkasanAkun.Update(_TglNeraca2 > _TglNeraca1 ?
                _TglNeraca2 : _TglNeraca1);

            string NilaiNeraca1 = Dp.GetSqlCoalesce("Neraca1", 0);
            string NilaiNeraca2 = Dp.GetSqlCoalesce("Neraca2", 0);

            DataSource = string.Concat(
                "SELECT NoAkun,NamaAkun,LevelAkun,UrutanCetak,Posting,",
                NilaiNeraca1, " as Neraca1,",
                NilaiNeraca2, " as Neraca2,",
                NilaiNeraca2, "-", NilaiNeraca1, " as Selisih,",
                Dp.GetSqlIifNoFormat(NilaiNeraca1 + "=0",
                    Dp.GetSqlIifNoFormat(NilaiNeraca2 + "<0", "-100",
                        Dp.GetSqlIifNoFormat(NilaiNeraca2 + ">0", "100", "null")),
                    string.Concat("(", NilaiNeraca2, "-", NilaiNeraca1, ")*100/",
                    Dp.GetSqlAbs(NilaiNeraca1))), " as [% Selisih] FROM (SELECT UrutanCetak,NoAkun,NamaAkun,LevelAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca1, false, "1", Parameters),
                ") p WHERE p.IdAkun=q.IdAkun OR p.IdAkun LIKE q.IdAkun+'.%') as Neraca1,(SELECT SUM(Debit-Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca2, false, "2", Parameters),
                ") r WHERE r.IdAkun=q.IdAkun OR r.IdAkun LIKE q.IdAkun+'.%') as Neraca2 FROM Akun q WHERE LevelAkun<=@0 AND JenisAkun<>",
                FormatSqlValue(enJenisAkun.Laba__Rugi),
                ") XX WHERE Aktif<>0");
            Parameters.Add(new FieldParam("0", _LevelCetak));
            DataSourceOrder = "UrutanCetak";
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
