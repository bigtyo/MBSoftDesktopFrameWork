using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraReports.UI;
using System.Drawing;
using SentraUtility.Expression;
using System.ComponentModel;

namespace SentraWinFramework.Report
{
    [ToolboxBitmap(typeof(xrPictureVar))]
    [DesignTimeVisible(false)]
    public sealed class xrPictureVar : XRPictureBox
    {
        public string Function
        {
            get { return Text; }
            set { Text = value; }
        }

        protected override void OnBeforePrint(System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                Evaluator ev = ((xReport)RootReport).Evaluator;
                if (ev == null) return;

                Image ObjData = ev.Parse(xrFunction.ParseFormula(Report, Text)) as Image;
                if (ObjData != null) Image = ObjData;
            }
            finally
            {
                base.OnBeforePrint(e);
            }
        }
    }
}
