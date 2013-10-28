using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.MutasiAkun
{
    public class MutasiAkunTertentu : ReportEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;

        private string _Akun = string.Empty;
        public string Akun
        {
            get { return _Akun; }
            set { _Akun = value; }
        }

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

        private decimal _SaldoAwal;
        public decimal SaldoAwal
        {
            get { return _SaldoAwal; }
        }

        private decimal _SaldoAkhir;
        public decimal SaldoAkhir
        {
            get { return _SaldoAkhir; }
        }

        private void UpdateSaldo()
        {
            FungsiGL fgl = BaseGL.FungsiGL;
            fgl.Init();
            string NoAkun = fgl.NoAkun(Akun);
            _SaldoAkhir = fgl.PosisiAkun(NoAkun, TglAkhir);
            _SaldoAwal = fgl.PosisiAkun(NoAkun, TglAwal.AddDays(-1));
        }

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>("IdAkun,NoAkun,NamaAkun",
                "Posting<>0 AND Aktif<>0", "NoAkun", true);
            _TglAkhir = DateTime.Today;
            _TglAwal = new DateTime(_TglAkhir.Year, _TglAkhir.Month, 1);
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }

        protected override void GetDataSource(out string DataSource, 
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            string strTemp = string.Empty;
            string strEmpty = FormatSqlValue(string.Empty,
                DataType.VarChar);

            UpdateSaldo();

            if (BaseGL.SetingPerusahaan.MultiDepartemen)
                strTemp = string.Concat(Dp.GetSqlIifNoFormat("IdDepartemen=" +
                    strEmpty, strEmpty,
                    "(SELECT KodeDepartemen FROM Departemen X WHERE X.IdDepartemen=IdDepartemen)"),
                    " AS KodeDepartemen,");
            if (BaseGL.SetingPerusahaan.MultiProyek)
                strTemp = string.Concat(strTemp, Dp.GetSqlIifNoFormat("IdProyek=" +
                    strEmpty, strEmpty,
                    "(SELECT KodeProyek FROM Proyek X WHERE X.IdProyek=IdProyek)"),
                    " AS KodeProyek,");

            Parameters.Add(new FieldParam("0", _TglAwal));
            Parameters.Add(new FieldParam("1", _TglAkhir.AddDays(1)));
            Parameters.Add(new FieldParam("2", _Akun));

            DataSource = string.Concat(@"SELECT J.NoJurnal,
                TglJurnal,JenisDokSumber,NoDokSumber,BuatJurnalPembalik,",
                strTemp, @"Debit,Kredit,J.Keterangan AS Uraian FROM Jurnal J INNER JOIN JurnalDetil D ON 
                J.NoJurnal=D.NoJurnal WHERE TglJurnal>=@0 AND TglJurnal<@1 
                AND IdAkun=@2");
            DataSourceOrder = "TglJurnal,NoJurnal";
        }

        //protected override void BeforeShowReport(Dictionary<string, object> Variables)
        //{
        //    Variables.Add("Umum", BaseGL.ReportUmum);
        //    Variables.Add("NoAkun", BaseGL.FungsiGL.NoAkun(_Akun));
        //}

        protected override void BeforePrint(Evaluator ev)
        {
            ev.ObjValues.Add("Umum", BaseGL.ReportUmum);
            ev.Variables.Add("NoAkun", BaseGL.FungsiGL.NoAkun(_Akun));
        }

        protected override void ShowView(params object[] Parameters)
        {
            _Akun = BaseGL.FungsiGL.IdAkun((string)Parameters[0]);
            _TglAwal = (DateTime)Parameters[1];
            _TglAkhir = (DateTime)Parameters[2];
        }
    }
}
