using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraGL.Master;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Properties;

namespace SentraGL.Report.LabaRugi
{
    internal partial class rptLabaRugiTahunan : ReportForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        LabaRugiTahunan LabaRugi = new LabaRugiTahunan();

        public rptLabaRugiTahunan()
        {
            InitializeComponent();
            BaseFramework.DefaultDp
                .ValidateTableDef<ViewMutasiAkunDetil>();
            labaRugiTahunanBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                BaseGL.RingkasanAkun.Update(new DateTime(LabaRugi.Tahun, 12, 31));
                List<FieldParam> Parameters = new List<FieldParam>();
                Parameters.Add(new FieldParam("0", LabaRugi.Tahun));

                string DataSource = string.Concat(
@"SELECT NoAkun,NamaAkun,KelompokAkun,UrutanKelompok,Januari,Februari,
Maret,April,Mei,Juni,Juli,Agustus,September,Oktober,Nopember,Desember 
FROM ViewMutasiAkunDetil WHERE Tahun=@0 AND JenisAkun=", 
Dp.FormatSqlValue(enJenisAkun.Laba__Rugi), " AND Aktif<>0");

                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                retVal.Add("Tahun", LabaRugi.Tahun);
                retVal.Add("DataSource", DataSource);
                retVal.Add("DataSourceParams", Parameters);
                retVal.Add("DataSourceOrder", "UrutanKelompok,NoAkun");
                return retVal;
            }
            set { }
        }
    }

    internal class LabaRugiTahunan
    {
        private int _Tahun = DateTime.Today.Year;
        public int Tahun
        {
            get { return _Tahun; }
            set { _Tahun = value; }
        }
    }
}
