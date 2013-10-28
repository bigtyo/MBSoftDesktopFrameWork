using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraGL.Report.LabaRugi;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Report.MutasiAkun;
using SentraGL.Master;
using SentraGL.Properties;

namespace SentraGL.Report.RingkasanAkun
{
    public partial class rptRingkasanAkunKlp : ReportForm
    {
        public rptRingkasanAkunKlp()
        {
            InitializeComponent();
        }

        protected override void GridSelected(object Data)
        {
            RingkasanAkunKlp Rpt = (RingkasanAkunKlp)
                ringkasanAkunKlpBindingSource.DataSource;

            DateTime AwalBlnIni = new DateTime(Rpt.Tahun,
                (int)Rpt.Bulan, 1);
            DateTime AkhirBlnIni = AwalBlnIni.AddMonths(1)
                .AddDays(-1);

            BaseWinFramework.SingleEntityForm
                .ShowTabular<rptMutasiAkunTertentu>(string.Empty,
                    null, null, new object[] { ((DataRow)Data)["NoAkun"], 
                        AwalBlnIni, AkhirBlnIni });
        }
    }
}
