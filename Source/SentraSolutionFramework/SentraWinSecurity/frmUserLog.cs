using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using DevExpress.XtraPrinting;
using SentraWinFramework.Report;

namespace SentraWinSecurity
{
    public partial class frmUserLog : XtraForm
    {
        clsFormLog log = new clsFormLog();
        private const string _ReportName = "User Log";

        public frmUserLog()
        {
            InitializeComponent();
            clsFormLogBindingSource.DataSource = log;
            listLogBindingSource.DataSource = log.ListLog;
            BaseWinFramework.WinForm.Grid.SetGridCanCopy(gridView1, true);
            BaseWinFramework.WinForm.Grid.SetGridCanSelectAll(gridView1, true);
        }

        void link_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string reportHeader = string.Concat("Aktivitas User Tgl ",
                dateEdit1.DateTime.ToString("dd MMM yyyy"), " s/d ",
                dateEdit2.DateTime.ToString("dd MMM yyyy"));
            e.Graph.StringFormat = new BrickStringFormat(
                StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.DoEvents();
            gridView1.CopyToClipboard();
        }

        ReportPreview rp;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Create a PrintingSystem component.
            PrintingSystem ps = new PrintingSystem();
            // Create a link that will print a control.
            PrintableComponentLink link = new PrintableComponentLink(ps);
            // Specify the control to be printed.
            link.Component = gridControl1;
            // Subscribe to the CreateReportHeaderArea event used to generate the report header.
            link.CreateReportHeaderArea += new CreateAreaEventHandler(link_CreateReportHeaderArea);

            link.CreateDocument();

            if (rp == null) rp = new ReportPreview();
            rp.ShowForm(_ReportName, link.PrintingSystem);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            log.Refresh();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (BaseWinSecurity._ShowDocProc != null &&
                gridView1.FocusedRowHandle >= 0)
                BaseWinSecurity._ShowDocProc(
                    (string)gridView1.GetFocusedRowCellValue(colDocumentId),
                    (string)gridView1.GetFocusedRowCellValue(colActionId),
                    (string)gridView1.GetFocusedRowCellValue(colDocumentNo),
                    (string)gridView1.GetFocusedRowCellValue(colDescription));
        }
    }
}