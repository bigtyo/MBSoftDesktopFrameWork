using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Drawing;

namespace Examples01.Entity
{
    public class Item : ParentEntity, IRuleSaveNew
    {
        [AutoNumberKey("ITMYYMM-CCCC", "CCCC", "TglRegister")]
        public string KodeItem;

        [DataTypeVarChar(50)]
        public string NamaItem;
        [DataTypeVarChar(50)]
        public string Keterangan;
        [DataTypeBoolean(Default = true)]
        public bool Aktif;
        [DataTypeDate]
        public DateTime TglRegister;
        [DataTypeImage]
        public Image Photo;

        #region IRuleSaveNew Members

        void IRuleSaveNew.BeforeSaveNew()
        {
            if (NamaItem.Length == 0)
                throw new ApplicationException("Nama Item tidak boleh kosong");
        }

        void IRuleSaveNew.AfterSaveNew()
        {
        }

        #endregion
    }
}
