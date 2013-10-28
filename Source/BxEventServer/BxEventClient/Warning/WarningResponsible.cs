using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Warning
{
    //[NoKeyEntity]
    public class WarningResponsible : ParentEntity
    {
        private string _WarningName;
        [DataTypeVarChar(100), EmptyError, PrimaryKey]
        public string WarningName
        {
            get { return _WarningName; }
            set { _WarningName = value; }
        }

        private string _KodeDepartemen;
        [DataTypeVarChar(50), EmptyError, PrimaryKey]
        public string KodeDepartemen
        {
            get { return _KodeDepartemen; }
            set { _KodeDepartemen = value; }
        }

        private string _KodeBagian;
        [DataTypeVarChar(50), PrimaryKey]
        public string KodeBagian
        {
            get { return _KodeBagian; }
            set { _KodeBagian = value; }
        }

        private string _KodeSeksi;
        [DataTypeVarChar(50), PrimaryKey]
        public string KodeSeksi
        {
            get { return _KodeSeksi; }
            set { _KodeSeksi = value; }
        }

        private string _KodeGudang;
        [DataTypeVarChar(50), PrimaryKey]
        public string KodeGudang
        {
            get { return _KodeGudang; }
            set { _KodeGudang = value; }
        }

        private string _ResponsibleUser;
        [DataTypeVarChar(50), EmptyError, PrimaryKey]
        public string ResponsibleUser
        {
            get { return _ResponsibleUser; }
            set { _ResponsibleUser = value; }
        }

        private string _Pesan;
        [DataTypeVarChar(50)]
        public string Pesan
        {
            get { return _Pesan; }
            set { _Pesan = value; }
        }
        
        private bool _AutoWarningLetter;
        [DataTypeBoolean]
        public bool AutoWarningLetter
        {
            get { return _AutoWarningLetter; }
            set { _AutoWarningLetter = value; }
        }
    }
}
