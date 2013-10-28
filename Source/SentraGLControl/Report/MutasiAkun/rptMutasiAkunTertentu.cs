using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraGL.Document;

namespace SentraGL.Report.MutasiAkun
{
    internal partial class rptMutasiAkunTertentu : ReportForm
    {
        public rptMutasiAkunTertentu()
        {
            InitializeComponent();
        }

        protected override void GridSelected(object Data)
        {
            BaseWinFramework.SingleEntityForm.ShowView(
                BaseWinFramework.GetModuleName(typeof(DocJurnal)),
                "NoJurnal=@0", new FieldParam("0",
                ((DataRow)Data)["NoJurnal"]));
        }
    }
}
