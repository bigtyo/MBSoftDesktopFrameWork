using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;

namespace SentraWinFramework.Report
{
    public class ReportPreview
    {
        internal XtraForm CurrentForm;

        public void ShowForm(string ReportName, XtraReport Rpt)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                if (CurrentForm == null || CurrentForm.IsDisposed)
                    CurrentForm = new frmPreview();

                frmPreview p = (frmPreview)CurrentForm;
                p.ShowForm(BaseWinFramework._MdiParent, 
                    ReportName, Rpt);
            }
            else
            {
                if (CurrentForm == null || CurrentForm.IsDisposed)
                    CurrentForm = new frmPreview2();

                frmPreview2 p = (frmPreview2)CurrentForm;
                p.ShowForm(BaseWinFramework._MdiParent, 
                    ReportName, Rpt);
            }
        }

        public void ShowForm(string ReportName, PrintingSystem ps)
        {
            if (BaseWinFramework.mdiRibbonControl != null)
            {
                if (CurrentForm == null || CurrentForm.IsDisposed)
                    CurrentForm = new frmPreview();

                frmPreview p = (frmPreview)CurrentForm;
                p.ShowForm(BaseWinFramework._MdiParent,
                    ReportName, ps);
            }
            else
            {
                if (CurrentForm == null || CurrentForm.IsDisposed)
                    CurrentForm = new frmPreview2();

                frmPreview2 p = (frmPreview2)CurrentForm;
                p.ShowForm(BaseWinFramework._MdiParent,
                    ReportName, ps);
            }
        }

        public void BringToFront()
        {
            CurrentForm.BringToFront();
        }
    }
}
