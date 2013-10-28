using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace SentraWinFramework.Report
{
    internal interface IFreeReport
    {
        void ShowForm(EntityForm EntityForm,
            string ReportName);

        void ShowForm2(EntityForm EntityForm,
            string ReportName, string FreeFilter,
            object TransStartDate, object TransEndDate,
            object[] Parameters);
        void ShowForm3(string FreeFilter,
            object TransStartDate, object TransEndDate,
            object[] Parameters);
    }
}
