using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraSecurity
{
    public enum enLogType
    {
        LogNone = 0,
        LogAdd = 1,
        LogEdit = 2,
        LogDelete = 4,
        LogAll = 7
    }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = true, Inherited = true)]
    [DebuggerNonUserCode]
    public class EnableLogAttribute : Attribute
    {
        internal enLogType LogType = 0;

        public enLogType GetLogType() { return LogType; }

        public EnableLogAttribute()
        {
            LogType = enLogType.LogAll;
        }
        public EnableLogAttribute(enLogType LogType)
        {
            this.LogType = LogType;
        }
    }
}
