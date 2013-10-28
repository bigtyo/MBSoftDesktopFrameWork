using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;

namespace SentraGL.Report.LabaRugi
{
    internal partial class rptLabaRugiBebas : ReportForm
    {
        LabaRugi LabaRugi = new LabaRugi();

        public rptLabaRugiBebas()
        {
            InitializeComponent();
            labaRugiBindingSource.DataSource = LabaRugi;
        }

        protected override Dictionary<string, object> FilterList
        {
            get
            {
                Dictionary<string, object> retVal = new Dictionary<string, object>();
                retVal.Add("Umum", BaseGL.ReportUmum);
                retVal.Add("TglAwal", LabaRugi.TglAwal);
                retVal.Add("TglAkhir", LabaRugi.TglAkhir);
                BaseGL.FungsiGL.Init();
                retVal.Add("GL", BaseGL.FungsiGL);
                return retVal;
            }
            set { }
        }
    }
}
