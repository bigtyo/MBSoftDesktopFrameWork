using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSecurity;

namespace SentraGL.Master
{
    [EnableLog]
    public class JenisDokSumberJurnal : ParentEntity
    {
        private string _JenisDokSumber;
        [DataTypeVarChar(50), PrimaryKey, EmptyError]
        public string JenisDokSumber
        {
            get { return _JenisDokSumber; }
            set { _JenisDokSumber = value; }
        }

        private string _Keterangan;
        [DataTypeVarChar(150)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
        }

        private bool _Otomatis;
        [DataTypeBoolean(Default = false)]
        public bool Otomatis
        {
            get { return _Otomatis; }
        }

        public JenisDokSumberJurnal() { }

        public JenisDokSumberJurnal(string JenisDokSumber, bool Otomatis)
        {
            _JenisDokSumber = JenisDokSumber;
            _Otomatis = Otomatis;
            _Aktif = true;
            _Keterangan = string.Empty;
        }

        public override bool AllowDelete
        {
            get { return !_Otomatis; }
        }

        public override bool AllowEdit
        {
            get { return !_Otomatis; }
        }

        public override bool IsVisible(string FieldName)
        {
            if (FieldName != "Otomatis") return true;

            if (FormMode == FormMode.FormAddNew)
                return false;
            else
                return _Otomatis;
        }
    }
}
