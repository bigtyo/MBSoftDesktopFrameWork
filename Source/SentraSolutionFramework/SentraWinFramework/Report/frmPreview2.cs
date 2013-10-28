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

namespace SentraWinFramework.Report
{
    internal sealed partial class frmPreview2 : XtraForm
    {
        public frmPreview2()
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
            Show();
            BringToFront();
        }

        public void ShowForm(Form MdiParent, string ReportName, PrintingSystem ps)
        {
            this.MdiParent = MdiParent;
            Text = "Preview Cetak " + ReportName;
            printControl1.PrintingSystem = ps;
            Show();
            BringToFront();
        }

        private void frmPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            Visible = false;
            Application.DoEvents();
        }
    }
}