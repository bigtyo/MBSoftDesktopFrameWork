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
using SentraGL.Properties;
using SentraGL.Report.MutasiAkun;

namespace SentraGL.Report
{
    internal partial class rptPosisiAkun : ReportForm
    {
        public rptPosisiAkun()
        {
            InitializeComponent();
        }

        protected override void GridSelected(object Data)
        {
            PosisiAkun pa = (PosisiAkun)PosisiAkunBindingSource.DataSource;
            BaseWinFramework.SingleEntityForm
                .ShowTabular<rptMutasiAkunTertentu>(string.Empty, null, null,
                ((DataRow)Data)["NoAkun"],
                new DateTime(pa.TglPosisiAkun.Year, 1, 1),
                pa.TglPosisiAkun);

        }
    }
}
