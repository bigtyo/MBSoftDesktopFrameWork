using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Drawing;

namespace Examples01.Entity
{
    public class Pelanggan : ParentEntity, 
        IRuleSaveNew, IRuleSaveUpdate
    {
        [AutoNumberKey("PLGYYMM-CCCC", "CCCC", "TglRegister")]
        public string NoPelanggan;

        [DataTypeVarChar(50)]
        public string NamaPelanggan;
        [DataTypeVarChar(50)]
        public string Alamat;
        [DataTypeVarChar(50)]
        public string Kota;
        [DataTypeDate]
        public DateTime TglRegister;
        [DataTypeBoolean(Default=true)]
        public bool Aktif;

        [DataTypeImage]
        public Image Photo;
        [DataTypeTimeStamp]
        public DateTime TglJamUpdate;

        private void ValidateSave()
        {
            if (Kota.Length == 0)
                throw new ApplicationException("Kota tidak boleh kosong");
        }

        #region IRuleSaveNew Members
        void IRuleSaveNew.BeforeSaveNew() { ValidateSave(); }
        void IRuleSaveNew.AfterSaveNew() { }
        #endregion

        #region IRuleSaveUpdate Members
        void IRuleSaveUpdate.BeforeSaveUpdate() { ValidateSave(); }
        void IRuleSaveUpdate.AfterSaveUpdate() { }
        #endregion
    }
}
