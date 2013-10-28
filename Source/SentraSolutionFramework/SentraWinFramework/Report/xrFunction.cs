using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraReports.UI;
using System.Drawing;
using SentraUtility.Expression;
using SentraSolutionFramework;
using System.ComponentModel;
using SentraUtility;

namespace SentraWinFramework.Report
{
    [ToolboxBitmap(typeof(xrFunction))]
    [DesignTimeVisible(false)]
    public sealed class xrFunction : XRLabel
    {
        private string _FormatString = "#,###.##";
        private string TmpFormula = string.Empty;

        public string FormatString
        {
            get { return _FormatString; }
            set { _FormatString = value; }
        }

        protected override void OnBeforePrint(System.Drawing.Printing.PrintEventArgs e)
        {
            if (TmpFormula.Length == 0) TmpFormula = Text;
            string strFormula = TmpFormula;
            
            object ObjData;
            try
            {
                if (strFormula == null || strFormula.Length == 0) return;
                ObjData = Report.GetCurrentColumnValue(strFormula);
                if (ObjData != null)
                {
                    try
                    {
                        Type tp = ObjData.GetType();
                        if (tp == typeof(decimal))
                        {
                            decimal TmpDec = (decimal)ObjData;
                            if (_FormatString.Length > 0)
                                Text = TmpDec.ToString(_FormatString);
                            else
                                Text = ObjData.ToString();
                        }
                        else if (tp == typeof(DateTime))
                        {
                            DateTime dt = (DateTime)ObjData;
                            if (_FormatString.Length > 0)
                                Text = dt.ToString(_FormatString);
                            else
                                Text = ObjData.ToString();
                        }
                        else
                            Text = ObjData.ToString();
                    }
                    catch
                    {
                        Text = ObjData.ToString();
                    }
                    return;
                }

                // Cek sbg ekspresi
                Evaluator ev = ((xReport)RootReport).Evaluator;
                if (ev == null) return;

                ObjData = ev.Parse(ParseFormula(Report, strFormula));
                if (ObjData != null)
                {
                    try
                    {
                        Type tp = ObjData.GetType();
                        if (tp == typeof(decimal))
                        {
                            decimal TmpDec = (decimal)ObjData;
                            if (_FormatString.Length > 0)
                                Text = TmpDec.ToString(_FormatString);
                            else
                                Text = ObjData.ToString();
                        }
                        else if (tp == typeof(DateTime))
                        {
                            DateTime dt = (DateTime)ObjData;
                            if (_FormatString.Length > 0)
                                Text = dt.ToString(_FormatString);
                            else
                                Text = ObjData.ToString();
                        }
                        else
                            Text = ObjData.ToString();
                    }
                    catch
                    {
                        Text = ObjData.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Text = "#Error:" + ex.Message;
            }
            finally
            {
                base.OnBeforePrint(e);
            }
        }

        public static string ParseFormula(XtraReportBase Rpt, string strFormula)
        {
            while (true)
            {
                int Index = strFormula.IndexOf("GetCurrentColumnValue");
                if (Index < 0) return strFormula;

                int StartParam = strFormula.IndexOf("(", Index);
                if (Index < 0) return strFormula;

                int EndParam = strFormula.IndexOf(")", StartParam);
                if (Index < 0) return strFormula;

                object TmpValue = Rpt.GetCurrentColumnValue(strFormula.Substring(StartParam + 2, EndParam - StartParam - 3));

                if (TmpValue == null)
                    throw new ApplicationException(string.Concat("Field ",
                        strFormula.Substring(StartParam + 1, EndParam - StartParam - 1), " tidak ditemukan"));

                string OldValue = strFormula.Substring(Index, EndParam - Index + 1);

                Type tp = TmpValue.GetType();
                if (tp == typeof(string))
                    strFormula = strFormula.Replace(OldValue,
                        string.Concat("\"", ((string)TmpValue).Replace("\"", "\\\""), "\""));
                else if (tp == typeof(decimal))
                    strFormula = strFormula.Replace(OldValue,
                        ((decimal)TmpValue).ToString(BaseUtility.DefaultCultureInfo));
                else if (tp == typeof(bool))
                    strFormula = strFormula.Replace(OldValue,
                            ((bool)TmpValue).ToString(BaseUtility.DefaultCultureInfo));
                else if (tp == typeof(DateTime)) strFormula = strFormula.Replace(OldValue, string.Concat("#",
                     ((DateTime)TmpValue).ToString(BaseUtility.DefaultCultureInfo), "#"));
            }
        }
    }
}
