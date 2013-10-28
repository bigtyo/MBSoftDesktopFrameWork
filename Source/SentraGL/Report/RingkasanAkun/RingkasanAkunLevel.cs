using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraUtility.Expression;

namespace SentraGL.Report.RingkasanAkun
{
    public class RingkasanAkunLevel : ReportEntity
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

        private int _Level = 4;
        [DataTypeInteger]
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        public RingkasanAkunLevel()
        {
            DateTime Tgl = DateTime.Today.AddMonths(-1);
            _Tahun = Tgl.Year;
            _Bulan = (enNamaBulan)Tgl.Month;
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            DateTime AwalBlnIni = new DateTime(_Tahun,
                (int)_Bulan, 1);
            DateTime AkhirBlnIni = AwalBlnIni.AddMonths(1)
                .AddDays(-1);

            BaseGL.RingkasanAkun.Update(AkhirBlnIni);
            string strNilai1 = Dp.GetSqlCoalesce("SaldoAwal", 0);
            string strNilai2 = Dp.GetSqlCoalesce("MutasiDebit", 0);
            string strNilai3 = Dp.GetSqlCoalesce("MutasiKredit", 0);

            Parameters.Add(new FieldParam("0", _Level));

            DataSource = string.Concat(
                "SELECT NoAkun,NamaAkun,LevelAkun,UrutanCetak,Posting,",
                strNilai1, " as SaldoAwal,", strNilai2, " as MutasiDebit,",
                strNilai3, " as MutasiKredit,(",
                strNilai1, "+", strNilai2, "-", strNilai3,
                ") AS SaldoAkhir FROM (SELECT UrutanCetak,NoAkun,NamaAkun,LevelAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlPosisiAkun(AwalBlnIni, false, "1", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as SaldoAwal,(SELECT SUM(Debit) FROM (",
                BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnIni, AkhirBlnIni, false, "2", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as MutasiDebit,(SELECT SUM(Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnIni, AkhirBlnIni, false, "3", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as MutasiKredit",
                " FROM Akun b WHERE LevelAkun<=@0) X WHERE Aktif<>0");
            DataSourceOrder = "UrutanCetak";
        }

        protected override string GetColumnHidden()
        {
            return "LevelAkun,UrutanCetak,Posting";
        }

        //protected override void BeforeShowReport(
        //    Dictionary<string, object> Variables)
        //{
        //      Variables.Add("Umum", BaseGL.ReportUmum);
        //}

        protected override void BeforePrint(Evaluator ev)
        {
            ev.ObjValues.Add("Umum", BaseGL.ReportUmum);
        }
    }
}
