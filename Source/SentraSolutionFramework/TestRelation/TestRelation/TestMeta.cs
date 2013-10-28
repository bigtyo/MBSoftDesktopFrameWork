using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace TestRelation
{
    [ParentRelation(typeof(Pelanggan), ParentUpdate.UpdateCascade,
        ParentDelete.DeleteCascade)]
    public class Lokasi : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(20)]
        public string Lokasi;
        [DataTypeVarChar(20)]
        public string NamaLokasi;
    }

    [ParentRelation(typeof(BarangPelanggan), ParentUpdate.UpdateRestrict,
        ParentDelete.DeleteRestrict)]
    public class Pelanggan : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(20)]
        public string Lokasi;
        [PrimaryKey, DataTypeVarChar(20)]
        public string NoPelanggan;
        [DataTypeVarChar(50)]
        public string NamaPelanggan;
    }

    public class BarangPelanggan : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(20)]
        public string Lokasi;
        [PrimaryKey, DataTypeVarChar(20)]
        public string NoPelanggan;
        [PrimaryKey, DataTypeVarChar(20)]
        public string NoBarang;
        [DataTypeVarChar(20)]
        public string NamaBarang;
    }
}
