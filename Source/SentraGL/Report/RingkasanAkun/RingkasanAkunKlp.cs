using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraUtility.Expression;

namespace SentraGL.Report.RingkasanAkun
{
    public class RingkasanAkunKlp : ReportEntity
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

        public RingkasanAkunKlp()
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

            DataSource = string.Concat(
                "SELECT JenisAkun,", BaseGL.RingkasanAkun.SqlUrutanJenisAkun(),
                " AS UrutanJenis,NoAkun,NamaAkun,UrutanKelompok,KelompokAkun,",
                strNilai1, " as SaldoAwal,", strNilai2, " as MutasiDebit,",
                strNilai3, " as MutasiKredit,(",
                strNilai1, "+", strNilai2, "-", strNilai3,
                ") AS SaldoAkhir FROM (SELECT JenisAkun,UrutanKelompok,KelompokAkun,NoAkun,NamaAkun,Posting,Aktif,(SELECT SUM(Debit-Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlPosisiAkun(AwalBlnIni, false, "1", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as SaldoAwal,(SELECT SUM(Debit) FROM (",
                BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnIni, AkhirBlnIni, false, "2", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as MutasiDebit,(SELECT SUM(Kredit) FROM (",
                BaseGL.RingkasanAkun.SqlMutasiAkun(AwalBlnIni, AkhirBlnIni, false, "3", Parameters),
                ") a WHERE a.IdAkun=b.IdAkun OR a.IdAkun LIKE b.IdAkun+'.%') as MutasiKredit FROM Akun b WHERE Posting<>0",
                ") X WHERE Aktif<>0");
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

        protected override string GetColumnHidden()
        {
            return "UrutanKelompok,JenisAkun,UrutanJenis";
        }
    }
}
