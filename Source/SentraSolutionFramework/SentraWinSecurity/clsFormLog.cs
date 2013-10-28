using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using SentraSecurity;
using System.ComponentModel;

namespace SentraWinSecurity
{
    internal class clsFormLog : ParentEntity
    {
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; Refresh(); }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; Refresh();  }
        }

        public clsFormLog()
        {
            _LogAge = UserLog.LogAge;
            _EndDate = DateTime.Today;
            _StartDate = _EndDate.AddMonths(-1);
            Refresh();
        }

        public BindingList<UserLog> ListLog = new BindingList<UserLog>();

        public void Refresh()
        {
            BaseFramework.DefaultDp.ListLoadEntities<UserLog>(
                ListLog, string.Concat("LogTime>=", FormatSqlValue(
                _StartDate), " AND LogTime<", FormatSqlValue(_EndDate.AddDays(1))),
                "LogTime DESC", false);
        }

        public void ClearAllLog()
        {
            UserLog.ClearLog();
            Refresh();
        }

        private int _LogAge;
        public int LogAge
        {
            get { return _LogAge; }
            set
            {
                _LogAge = value;
                UserLog.LogAge = value;
            }
        }
    }
}
