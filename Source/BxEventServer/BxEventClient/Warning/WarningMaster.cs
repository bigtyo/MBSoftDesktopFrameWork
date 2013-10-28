using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Warning
{
    public class WarningMaster : ParentEntity
    {
        private string _TableSourceName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string TableSourceName
        {
            get { return _TableSourceName; }
            set { _TableSourceName = value; }
        }

        private string _WarningName;
        [PrimaryKey, DataTypeVarChar(100), EmptyError]
        public string WarningName
        {
            get { return _WarningName; }
            set { _WarningName = value; }
        }

        private string _WarningQuery;
        [DataTypeVarChar(4000)]
        public string WarningQuery
        {
            get { return _WarningQuery; }
            set { _WarningQuery = value; }
        }

        private string _NumDayToWarningLetter;
        [DataTypeVarChar(20)]
        public string NumDayToWarningLetter
        {
            get { return _NumDayToWarningLetter; }
            set { _NumDayToWarningLetter = value; }
        }
    }
}
