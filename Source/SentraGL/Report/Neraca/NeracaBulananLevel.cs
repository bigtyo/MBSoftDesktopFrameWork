using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Master;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaBulananLevel : ReportEntity
    {
        private int _Tahun = DateTime.Today.Year;
        public int Tahun
        {
            get { return _Tahun; }
            set { _Tahun = value; }
        }

        private int _LevelCetak = 4;
        [DataTypeInteger]
        public int LevelCetak
        {
            get { return _LevelCetak; }
            set { _LevelCetak = value; }
        }

        public NeracaBulananLevel()
        {
            Dp.ValidateTableDef<ViewPosisiAkun>();
        }

        protected override void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            BaseGL.RingkasanAkun.Update(new DateTime(_Tahun, 12, 31));

            Parameters.Add(new FieldParam("0", _LevelCetak));
            Parameters.Add(new FieldParam("1", _Tahun));
            Parameters.Add(new FieldParam("2", enJenisAkun.Laba__Rugi));

            DataSource = Dp.GetSqlSelect<ViewPosisiAkun>(string.Empty,
                "LevelAkun<=@0 AND Tahun=@1 AND JenisAkun<>@2 AND Aktif<>0");

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
