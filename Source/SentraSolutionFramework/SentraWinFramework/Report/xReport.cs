using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraReports.UI;
using SentraUtility.Expression;
using System.Globalization;
using SentraSolutionFramework;
using System.Diagnostics;
using System.Reflection;
using SentraSolutionFramework.Entity;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;

namespace SentraWinFramework.Report
{
    //[DebuggerNonUserCode]
    public class xReport : XtraReport
    {
        public IReportEntity ReportEntity;
        public Evaluator Evaluator;

        public object GetProperty(object Obj, string VarName)
        {
            PropertyInfo pi = Obj.GetType().GetProperty(VarName);
            if (pi != null)
                return pi.GetValue(Obj, null);
            FieldInfo fi = Obj.GetType().GetField(VarName);
            if (fi != null)
                return fi.GetValue(Obj);
            return null;
        }

        public void SetProperty(object Obj, string VarName, object Value)
        {
            PropertyInfo pi = Obj.GetType().GetProperty(VarName);
            if (pi != null)
                pi.SetValue(Obj, Value, null);
            else
            {
                FieldInfo fi = Obj.GetType().GetField(VarName);
                if (fi != null)
                    fi.SetValue(Obj, Value);
            }
        }

        public object GetValue(string VarName)
        {
            return Evaluator.Variables.GetValue(VarName);
        }
        public void SetValue(string VarName, object NewValue)
        {
            Evaluator.Variables.VarDictionary[VarName] = NewValue;
        }

        public object Parse(string Expression)
        {
            return Evaluator.Parse(Expression);
        }

        public decimal ToDecimal(string strValue)
        {
            try
            {
                return decimal.Parse(strValue, NumberStyles.Currency);
            }
            catch
            {
                return -987654321;
            }
        }

        public xReport()
        {
            Evaluator = BaseWinFramework.Evaluator;
            PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        void PrintingSystem_StartPrint(object sender, PrintDocumentEventArgs e)
        {
            e.PrintDocument.EndPrint += new PrintEventHandler(PrintDocument_EndPrint);
        }

        void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            ReportSingleEntity rse = DataSource as ReportSingleEntity;
            if (rse != null) rse.DoAfterSendToPrinter();
        }

        public xReport(Evaluator Evaluator)
        {
            this.Evaluator = Evaluator ?? BaseFactory
                .CreateInstance<Evaluator>();
            PrintingSystem.StartPrint += new PrintDocumentEventHandler(PrintingSystem_StartPrint);
        }

        protected override void BeforeReportPrint()
        {
            ReportSingleEntity rse = DataSource as ReportSingleEntity;
            if (rse != null)
                rse.DoBeforePrint(Evaluator);
            else if (ReportEntity != null)
                ((IBaseEntity)ReportEntity).BeforePrint(Evaluator);
            base.BeforeReportPrint();
        }

        protected override void AfterReportPrint()
        {
            ReportSingleEntity rse = DataSource as ReportSingleEntity;
            if (rse != null) rse.DoAfterPrint();
            base.AfterReportPrint();
        }
    }
}
