using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Entity
{
    public enum enEventType
    {
        Warning,
        WarningRecurring,
        Information
    }

    public class MasterEvent : ParentEntity
    {
        private string _EventName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }

        private bool _NeedActionListener;
        [DataTypeBoolean]
        public bool NeedActionListener
        {
            get { return _NeedActionListener; }
            set { _NeedActionListener = value; }
        }

        private enEventType _EventType;
        [DataTypeVarChar(30, Default = enEventType.Warning)]
        public enEventType EventType
        {
            get { return _EventType; }
            set { _EventType = value; }
        }

        private int _WarningDayBeforeDueDate;
        [DataTypeInteger]
        public int WarningDayBeforeDueDate
        {
            get { return _WarningDayBeforeDueDate; }
            set { _WarningDayBeforeDueDate = value; }
        }
    }
}
