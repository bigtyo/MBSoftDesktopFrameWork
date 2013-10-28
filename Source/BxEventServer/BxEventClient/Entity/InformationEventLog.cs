using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Entity
{
    public class InformationEventLog : ParentEntity
    {
        private string _UserName;
        [PrimaryKey, DataTypeVarChar(20)]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private DateTime _DateTimeEvent;
        [PrimaryKey, DataTypeDateTime]
        public DateTime DateTimeEvent
        {
            get { return _DateTimeEvent; }
            set { _DateTimeEvent = value; }
        }

        private string _EventName;
        [DataTypeVarChar(50)]
        public string EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }

        private string _EventDescription;
        [DataTypeVarChar(200)]
        public string EventDescription
        {
            get { return _EventDescription; }
            set { _EventDescription = value; }
        }
    }
}
