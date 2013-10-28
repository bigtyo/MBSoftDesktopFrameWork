using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Drawing;

namespace Examples03
{
    public class Pelangggan : ParentEntity
    {
        [AutoNumberKey("PLG-CCCC", "CCCC")]
        public string NoPelanggan;
        [DataTypeVarChar(50)]
        public string NamaPelanggan;
        [DataTypeVarChar(50)]
        public string Alamat;
        [DataTypeBoolean(Default = true)]
        public bool Aktif;
        [DataTypeImage]
        public Image Photo;
    }
}
