using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Master;
using SentraGL.Properties;

namespace SentraGL.Report.Neraca
{
    internal partial class rptNeracaMultiPeriodeLvl : ReportForm
    {
        public rptNeracaMultiPeriodeLvl()
        {
            InitializeComponent();
        }
    }

    internal class NeracaMultiPeriode
    {
        private DateTime _TglNeraca1;
        public DateTime TglNeraca1
        {
            get { return _TglNeraca1; }
            set { _TglNeraca1 = value; }
        }

        private DateTime _TglNeraca2;
        public DateTime TglNeraca2
        {
            get { return _TglNeraca2; }
            set { _TglNeraca2 = value; }
        }

        private DateTime _TglNeraca3;
        public DateTime TglNeraca3
        {
            get { return _TglNeraca3; }
            set { _TglNeraca3 = value; }
        }

        private DateTime _TglNeraca4;
        public DateTime TglNeraca4
        {
            get { return _TglNeraca4; }
            set { _TglNeraca4 = value; }
        }

        private int _LevelCetak=4;
        public int LevelCetak
        {
            get { return _LevelCetak; }
            set { _LevelCetak = value; }
        }

        public NeracaMultiPeriode()
        {
            _TglNeraca4 = new DateTime(DateTime.Today.Year,
                DateTime.Today.Month, 1);
            _TglNeraca3 = _TglNeraca4.AddMonths(-3);
            _TglNeraca2 = _TglNeraca3.AddMonths(-3);
            _TglNeraca1 = _TglNeraca2.AddMonths(-3);

            _TglNeraca1 = _TglNeraca1.AddDays(-1);
            _TglNeraca2 = _TglNeraca2.AddDays(-1);
            _TglNeraca3 = _TglNeraca3.AddDays(-1);
            _TglNeraca4 = _TglNeraca4.AddDays(-1);
        }

        public DateTime GetTglMaksimum()
        {
            DateTime tmp = _TglNeraca1;
            if (_TglNeraca2 > tmp) tmp = _TglNeraca2;
            if (_TglNeraca3 > tmp) tmp = _TglNeraca3;
            if (_TglNeraca4 > tmp) tmp = _TglNeraca4;

            return tmp;
        }
    }
}
