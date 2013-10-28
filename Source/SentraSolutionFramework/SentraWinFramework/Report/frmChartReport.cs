using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SentraWinFramework.Report
{
    public partial class frmChartReport : XtraForm
    {
        public frmChartReport()
        {
            InitializeComponent();
        }

        public void ShowForm(EntityForm EntityForm, string ReportName)
        {
        }

        internal void ShowForm2(EntityForm EntityForm, string ModuleName, string FreeFilter, object TransStartDate, object TransEndDate, object[] Parameters)
        {
        }

        internal void ShowForm3(string FreeFilter, object TransStartDate, object TransEndDate, object[] Parameters)
        {
        }
    }
}