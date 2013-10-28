using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaLevel : ReportEntity
    {
        private DateTime _TglNeraca;
        public DateTime TglNeraca
        {
            get { return _TglNeraca; }
            set
            {
                _TglNeraca = value;
            }
        }

        private int _LevelCetak;
        [DataTypeInteger]
        public int LevelCetak
        {
            get { return _LevelCetak; }
            set
            {
                _LevelCetak = value;
            }
        }

        protected override void InitUI()
        {
            _TglNeraca = DateTime.Today;
            _LevelCetak = 4;
        }

        protected override void GetDataSource(out string DataSource, 
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            DataSource = string.Concat(
@"SELECT NoAkun,NamaAkun,LevelAkun,UrutanCetak,Posting,",
Dp.GetSqlCoalesce("Saldo", 0), @" as Saldo FROM (SELECT UrutanCetak,
NoAkun,NamaAkun,LevelAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) 
FROM (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca, "1", Parameters),
@") pp WHERE pp.IdAkun=qq.IdAkun OR pp.IdAkun LIKE qq.IdAkun
+'.%') as Saldo FROM Akun qq WHERE LevelAkun<=@0 AND JenisAkun<>",
Dp.FormatSqlValue(enJenisAkun.Laba__Rugi), ") zz WHERE Aktif<>0");
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
