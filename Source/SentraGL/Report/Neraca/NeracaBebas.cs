using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraUtility.Expression;

namespace SentraGL.Report.Neraca
{
    public class NeracaBebas : ReportEntity
    {
        private DateTime _TglNeraca = DateTime.Today;
        public DateTime TglNeraca
        {
            get { return _TglNeraca; }
            set { _TglNeraca = value; }
        }

        //protected override void BeforeShowReport(
        //    Dictionary<string, object> Variables)
        //{
        //    Variables.Add("Umum", BaseGL.ReportUmum);
        //    BaseGL.FungsiGL.Init();
        //    Variables.Add("GL", BaseGL.FungsiGL);
        //}

        protected override void BeforePrint(Evaluator ev)
        {
            ev.ObjValues.Add("Umum", BaseGL.ReportUmum);
            
            BaseGL.FungsiGL.Init();
            ev.ObjValues.Add("GL", BaseGL.FungsiGL);
        }
    }
}
