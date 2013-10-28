using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaBulananKlp : ReportEntity
    {
        private int _Tahun = DateTime.Today.Year;
        public int Tahun
        {
            get { return _Tahun; }
            set { _Tahun = value; }
        }

        public NeracaBulananKlp()
        {
            Dp.ValidateTableDef<ViewPosisiAkunDetil>();
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            BaseGL.RingkasanAkun.Update(new DateTime(_Tahun, 12, 31));

            Parameters.Add(new FieldParam("0", _Tahun));
            Parameters.Add(new FieldParam("1", enJenisAkun.Laba__Rugi));
            DataSource = @"SELECT NoAkun,NamaAkun,UrutanKelompok,JenisAkun,KelompokAkun,
                Januari,Februari,Maret,April,Mei,Juni,Juli,Agustus,September,
                Oktober,Nopember,Desember FROM ViewPosisiAkunDetil WHERE Tahun=@0 AND 
                JenisAkun<>@1 AND Aktif<>0";

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
