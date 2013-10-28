using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Drawing;

namespace Examples02.Entity
{
    public class Pelanggan : ParentEntity
    {
        [AutoNumberKey("PLGYYMM-CCCC", "CCCC", "TglDaftar")]
        public string NoPelanggan;
        [DataTypeVarChar(50)]
        public string NamaPelanggan;
        [DataTypeVarChar(50)]
        public string Alamat;
        [DataTypeBoolean(Default=true)]
        public bool Aktif;
        [DataTypeDate]
        public DateTime TglDaftar;
        [DataTypeImage]
        public Image Photo;
        [DataTypeVarChar(20)]
        public string Telpon;
    }
}
