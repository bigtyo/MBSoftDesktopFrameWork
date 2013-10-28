using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System.IO;
using SentraUtility.Expression;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using System.Reflection;
using DevExpress.XtraBars.Ribbon;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmPreview : XtraForm
    {
        public frmPreview()
        {
            InitializeComponent();
        }

        public void ShowForm(Form MdiParent, string ReportName, XtraReport Rpt)
        {
            this.MdiParent = MdiParent;
            Text = "Preview Cetak " + ReportName;

            Rpt.ScriptReferences = new string[] { GetType().Assembly.Location };
            printControl1.PrintingSystem = Rpt.PrintingSystem;
            Rpt.CreateDocument();
            sbTotal.Caption = "Total Halaman: " +
                printControl1.PrintingSystem.Pages.Count;
            ribbonStatusBar1.Refresh();
            Rpt.PrintingSystem.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(PrintingSystem_AfterChange);
            Show();
            BringToFront();
        }

        void PrintingSystem_AfterChange(object sender, 
            DevExpress.XtraPrinting.ChangeEventArgs e)
        {
            switch (e.EventName)
            {
                case "BrickMove":
                    return;
                case "PageSettingsChanged":
                case "AfterMarginsChange":
                    sbTotal.Caption = "Total Halaman: " +
                        printControl1.PrintingSystem.Pages.Count;
                    ribbonStatusBar1.Refresh();
                    break;
            }
        }

        public void ShowForm(Form MdiParent, string ReportName, PrintingSystem ps)
        {
            this.MdiParent = MdiParent;
            Text = "Preview Cetak " + ReportName;
            printControl1.PrintingSystem = ps;
            sbTotal.Caption = "Total Halaman: " +
                printControl1.PrintingSystem.Pages.Count;
            ribbonStatusBar1.Refresh();
            ps.AfterChange += new DevExpress.XtraPrinting.ChangeEventHandler(PrintingSystem_AfterChange);
            Show();
            BringToFront();
        }

        private void frmPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            Application.DoEvents();
        }

        private void printControl1_SelectedPageChanged(object sender, PageEventArgs e)
        {
            sbHal.Caption = "Halaman Ke: " + 
                (printControl1.SelectedPageIndex + 1);
            ribbonStatusBar1.Refresh();
        }

        private void printControl1_ZoomChanged(object sender, EventArgs e)
        {
            sbZoom.Caption = string.Concat("Pembesaran: ", 
                decimal.Floor(Convert.ToDecimal(
                printControl1.Zoom * 100)), "%  ");
            ribbonStatusBar1.Refresh();
        }

        private void frmPreview_Activated(object sender, EventArgs e)
        {
            BaseWinFramework.mdiDesignRibbonController
                .XRDesignPanel = null;

            BaseWinFramework.mdiRibbonPrintController
                .PrintControl = printControl1;
        }
    }
}