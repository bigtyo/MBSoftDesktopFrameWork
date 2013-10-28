using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using SentraGL.Document;

namespace SentraGL.Report.MutasiAkun
{
    internal partial class rptMutasiSemuaAkun : ReportForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        MutasiSemuaAkun Lap = new MutasiSemuaAkun();

        public rptMutasiSemuaAkun()
        {
            InitializeComponent();
            mutasiSemuaAkunBindingSource.DataSource = Lap;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                string strTemp = string.Empty;
                string strEmpty = Dp.FormatSqlValue(string.Empty, DataType.VarChar);

                if (BaseGL.SetingPerusahaan.MultiDepartemen)
                    strTemp = string.Concat(
                        Dp.GetSqlIifNoFormat("IdDepartemen=" +
                        strEmpty, strEmpty,
                        "(SELECT KodeDepartemen FROM Departemen X WHERE X.IdDepartemen=IdDepartemen)"),
                        " AS KodeDepartemen,");
                if (BaseGL.SetingPerusahaan.MultiProyek)
                    strTemp = string.Concat(strTemp,
                        Dp.GetSqlIifNoFormat("IdProyek=" +
                        strEmpty, strEmpty,
                        "(SELECT KodeProyek FROM Proyek X WHERE X.IdProyek=IdProyek)"),
                        " AS KodeProyek,");

                List<FieldParam> Parameters = new List<FieldParam>();
                Parameters.Add(new FieldParam("0", Lap.TglAwal));
                Parameters.Add(new FieldParam("1", Lap.TglAkhir.AddDays(1)));

                string DataSource = string.Concat(@"SELECT J.NoJurnal,
TglJurnal,JenisDokSumber,NoDokSumber,BuatJurnalPembalik,",
strTemp, @"(SELECT NoAkun FROM Akun X WHERE X.IdAkun=D.IdAkun) AS NoAkun,
(SELECT NamaAkun FROM Akun X WHERE X.IdAkun=D.IdAkun) AS NamaAkun,
Debit,Kredit FROM Jurnal J INNER JOIN JurnalDetil D ON 
J.NoJurnal=D.NoJurnal WHERE TglJurnal>=@0 AND TglJurnal<@1");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                retVal.Add("TglAwal", Lap.TglAwal);
                retVal.Add("TglAkhir", Lap.TglAkhir);
                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "TglJurnal,NoJurnal");
                return retVal;
            }
            set { }
        }

        protected override void GridSelected(object Data)
        {
            BaseWinFramework.SingleEntityForm.ShowView(
                BaseWinFramework.GetModuleName(typeof(DocJurnal)),
                "NoJurnal=@0", new FieldParam("0", (string)
                ((DataRow)Data)["NoJurnal"]));
        }
    }

    internal class MutasiSemuaAkun
    {
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

        public MutasiSemuaAkun()
        {
            _TglAkhir = DateTime.Today.Date;
            _TglAwal = new DateTime(_TglAkhir.Year, _TglAkhir.Month, 1);
        }
    }
}
