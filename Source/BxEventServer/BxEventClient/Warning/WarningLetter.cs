using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Warning
{
    public class WarningLetter : ParentEntity
    {
        private string _WarningLetterNumber;
        [PrimaryKey, DataTypeVarChar(20)]
        public string WarningLetterNumber
        {
            get { return _WarningLetterNumber; }
            set { _WarningLetterNumber = value; }
        }

        private string _WarningName;
        [DataTypeVarChar(50), EmptyError]
        public string WarningName
        {
            get { return _WarningName; }
            set { _WarningName = value; }
        }

        private string _ReffDocNumber;
        [DataTypeVarChar(20)]
        public string ReffDocNumber
        {
            get { return _ReffDocNumber; }
            set { _ReffDocNumber = value; }
        }

        public WarningLetter(string WarningName, 
            string WarningLetterNumber, string ReffDocNumber)
        {
            _WarningName = WarningName;
            _WarningLetterNumber = WarningLetterNumber;
            _ReffDocNumber = ReffDocNumber;
        }
    }
}
