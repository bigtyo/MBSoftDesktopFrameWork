using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace Examples01.Entity
{
    [Index("NoAkun", true)]
    public class Akun : ParentEntity
    {
        private string _ParentID;
        private string _AkunInduk;
        private string _NamaAkunInduk;

        [AutoNestedKey("ParentID", "NonPosting")]
        public string AkunID;

        [DataTypeVarChar(40)]
        public string ParentID
        {
            get { return _ParentID; }
            set
            {
                _ParentID = value;
                object[] retVal = FindValues<Akun>("NoAkun,NamaAkun",
                    "AkunID=" + FormatSqlValue(value), "", null);
                _AkunInduk = (string)retVal[0];
                _NamaAkunInduk = (string)retVal[1];
                DataChanged();
            }
        }

        public string AkunInduk
        {
            get { return _AkunInduk; }
            set
            {
                if (value.Length == 0)
                {
                    _ParentID = "";
                    _NamaAkunInduk = "";
                }
                else
                {
                    object[] RetVal = FindValues<Akun>(
                        "AkunID,NamaAkun", "NoAkun=" + 
                        FormatSqlValue(value), "", null);
                    _ParentID = (string)RetVal[0];
                    _NamaAkunInduk = (string)RetVal[1];
                }
                DataChanged();
            }
        }
        public string NamaAkunInduk
        {
            get { return _NamaAkunInduk; }
        }

        [DataTypeBoolean(Default = false)]
        public bool NonPosting;
        [DataTypeVarChar(50)]
        public string NoAkun;
        [DataTypeVarChar(50)]
        public string NamaAkun;
    }
}
