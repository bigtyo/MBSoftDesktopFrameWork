using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaKlp : ReportEntity
    {
        private DateTime _TglNeraca = DateTime.Today;
        public DateTime TglNeraca
        {
            get { return _TglNeraca; }
            set { _TglNeraca = value; }
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            DataSource = string.Concat(
            @"SELECT JenisAkun,UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,",
            Dp.GetSqlCoalesce("Saldo", 0), @" as Saldo FROM 
(
  SELECT JenisAkun,UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Aktif,
  (
   SELECT SUM(Debit-Kredit) FROM 
   (", BaseGL.RingkasanAkun.SqlPosisiAkun(_TglNeraca, "1", Parameters),
               @") pp WHERE pp.IdAkun=qq.IdAkun
  ) as Saldo FROM Akun qq WHERE Posting<>0 AND JenisAkun<>",
              FormatSqlValue(enJenisAkun.Laba__Rugi),
            ") rr WHERE Aktif<>0");

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
