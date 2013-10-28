using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraUtility.Expression;
using System.Windows.Forms;

namespace SentraWinFramework.Report
{
    internal class ReportLayout
    {
        public Form CurrentForm;

        // Dari Dokumen
        public void ShowForm(BusinessEntity DataSource, string ReportName,
            Evaluator Evaluator)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                frmReportLayout frm = new frmReportLayout();
                frm.ShowForm(BaseWinFramework._MdiParent, ReportName,
                    DataSource, Evaluator);
                CurrentForm = frm;
            }
            else
            {
                frmReportLayout2 frm = new frmReportLayout2();
                frm.ShowForm(BaseWinFramework._MdiParent, ReportName,
                    DataSource, Evaluator);
                CurrentForm = frm;
            }
        }

        // Dari Laporan Daftar
        public void ShowForm2(string ReportName,
            object DataSource, Evaluator Evaluator)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                frmReportLayout frm = new frmReportLayout();
                frm.ShowForm2(BaseWinFramework._MdiParent,
                    ReportName, DataSource, Evaluator);
                CurrentForm = frm;
            }
            else
            {
                frmReportLayout2 frm = new frmReportLayout2();
                frm.ShowForm2(BaseWinFramework._MdiParent,
                    ReportName, DataSource, Evaluator);
                CurrentForm = frm;
            }
        }

        // Dari Laporan Bebas
        public void ShowForm3(string ReportName,
            Evaluator Evaluator)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                frmReportLayout frm = new frmReportLayout();
                frm.ShowForm3(BaseWinFramework._MdiParent,
                    ReportName, Evaluator);
                CurrentForm = frm;
            }
            else
            {
                frmReportLayout2 frm = new frmReportLayout2();
                frm.ShowForm3(BaseWinFramework._MdiParent,
                    ReportName, Evaluator);
                CurrentForm = frm;
            }
        }

        // Dari Laporan Bebas pake DataSource
        public void ShowForm4(string ReportName,
            object DataSource, Evaluator Evaluator)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                frmReportLayout frm = new frmReportLayout();
                frm.ShowForm4(BaseWinFramework._MdiParent,
                    ReportName, DataSource, Evaluator);
                CurrentForm = frm;
            }
            else
            {
                frmReportLayout2 frm = new frmReportLayout2();
                frm.ShowForm4(BaseWinFramework._MdiParent,
                    ReportName, DataSource, Evaluator);
                CurrentForm = frm;
            }
        }

        public void BringToFront()
        {
            CurrentForm.BringToFront();
        }
    }
}
