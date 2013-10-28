using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraUtility.Expression;

namespace SentraGL.Report
{
    public class PosisiAkun : ReportEntity
    {
        private int _LevelCetak = 4;
        public int LevelCetak
        {
            get { return _LevelCetak; }
            set
            {
                _LevelCetak = value;
                FormChanged();
            }
        }

        private DateTime _TglPosisiAkun = DateTime.Today;
        public DateTime TglPosisiAkun
        {
            get { return _TglPosisiAkun; }
            set { _TglPosisiAkun = value; }
        }

        protected override void GetDataSource(out string DataSource, 
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            if (_LevelCetak == 3)
                AddError("LevelCetak", "xxx");
            DataSource = string.Concat(
                "SELECT NoAkun,NamaAkun,LevelAkun,UrutanCetak,Posting,JenisAkun,",
                Dp.GetSqlCoalesce("Saldo", 0),
                " AS Saldo FROM (SELECT NoAkun,NamaAkun,LevelAkun,UrutanCetak,Posting,Aktif,JenisAkun,",
                "(SELECT SUM(Debit-Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlPosisiAkun(_TglPosisiAkun, "1", Parameters),
                ") pp WHERE pp.IdAkun=gg.IdAkun OR pp.IdAkun LIKE gg.IdAkun+'.%') as Saldo FROM Akun gg WHERE LevelAkun<=@0) ll WHERE Aktif<>0");
            DataSourceOrder = "UrutanCetak,NoAkun";
            Parameters.Add(new FieldParam("0", _LevelCetak));
        }

        protected override string GetColumnHidden()
        {
            return "LevelAkun,UrutanCetak";
        }

        protected override void BeforePrint(Evaluator ev)
        {
            ev.ObjValues.Add("Umum", BaseGL.ReportUmum);
        }
    }
}
