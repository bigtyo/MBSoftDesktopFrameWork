using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Entity
{
    public class DocumentEvent : ParentEntity
    {
        private string _DocumentNo;
        [PrimaryKey, DataTypeVarChar(50)]
        public string DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        private string _EventName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string EventName
        {
            get { return _EventName; }
            set { _EventName = value; }
        }

        private string _EventDescription;
        [DataTypeVarChar(100)]
        public string EventDescription
        {
            get { return _EventDescription; }
            set { _EventDescription = value; }
        }

        private DateTime _DueDate;
        [DataTypeDateTime]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
    }
}
