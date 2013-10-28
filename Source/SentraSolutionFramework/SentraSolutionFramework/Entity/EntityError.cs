using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraSolutionFramework.Entity
{
    [DebuggerNonUserCode]
    public static class ErrorMetaData
    {
        public static string DataTypeCancelDateTimeIsIncorrect =
            "(Tabel {0}) Tipe Data Field {1} harus DateTime";
        public static string DataTypeCancelUserIsIncorrect =
            "(Tabel {0}) Tipe Data Field {1} harus VarChar";
        public static string DataTypeCancelNotesIsIncorrect =
            "(Tabel {0}) Tipe Data Field {1} harus VarChar";
        public static string DataTypeCancelStatusIsIncorrect =
            "(Tabel {0}) Tipe Data Field {1} harus VarChar";

        public static string AsmVersionMustAutoIncrement =
            "(Assembly '{0}') Assembly Version harus Auto Increment";

        public static string EnumValueMustString =
            "(Field '{0}') Tipe Data harus Char/ VarChar karena merupakan enum";

        public static string DuplicateTimeStamp =
            "Tabel {0} memiliki lebih dari satu field TimeStamp";
        public static string ParentFieldNotFound =
            "ParentField '{0}' untuk Nested Key tidak ditemukan";
        public static string FieldTypeNotMatch =
            "(Tabel '{0}' Field '{1}') Tipe Data tidak sesuai";

        public static string ViewEntityError =
            "(Tabel '{0}') SetViewSqlQuery hanya dapat dilakukan pada ViewEntity";

        public static string CounterFieldOnParent =
            "(Field '{0}') Field Counter hanya boleh ada di ChildEntity";
        public static string AutoNestedFieldOnChild =
            "(Field '{0}') Field AutoNested hanya boleh ada di ParentEntity";
        public static string CounterFieldNotExist =
            "ChildEntity harus memiliki Counter Field";

        public static string RelationParentField =
            "(Relasi '{0}'-'{1}') Tidak semua Field Key Parent ada dalam ParentFields";
        public static string RelationFieldCount =
            "(Relasi '{0}'-'{1}') Jumlah Field Relasi Parent dan Child tidak sama";
        public static string RelationFieldTypeNotMatch =
            "(Relasi '{0}'-'{1}') Tipe Data '{2}' tidak sama dengan tipe data '{3}'";
        public static string RelParentFieldNotFound =
            "(Relasi '{0}'-'{1}') Field Induk '{2}' tidak ditemukan";
        public static string RelChildFieldNotFound =
            "(Relasi '{0}'-'{1}') Field Anak '{2}' tidak ditemukan";

        public static string IndexFieldNotFound =
            "(Field Index '{0}') Field Index Tidak ditemukan !";

        public static string DataTypeLoadSqlPk =
            "(Key '{0}') Primary Key tidak boleh bertipe DataTypeLoadSql";
        public static string DataTypeLoadSqlParentNotFound =
            "(DataTypeSql pada Field '{0}' Tabel '{1}' ) Parent Field tidak ditemukan";

        public static string DataTypeNotFound =
            "(Key '{0}') Key harus memiliki tipe data";
        public static string DateFieldNotFound =
            "(DateField '{0}') Property/ Field tidak ditemukan";
        public static string DateFieldError =
            "(DateField '{0}') Property/ Field harus bertipe DateTime";
        public static string MultiDataType =
            "(Field '{0}') Tidak boleh memiliki lebih daru satu DataType";
        public static string KeyTimeStamp =
            "(Field '{0}') Tidak boleh ada Field TimeStamp sebagai PrimaryKey";
        public static string MultiFieldName =
            "(Field '{0}') Tidak boleh didefinisikan lebih dari satu kali";
        public static string MultiCounter =
            "(Field '{0}') Tidak boleh ada lebih dari satu Field Counter";
        public static string MultiNestedKey =
            "(Field '{0}') Tidak boleh ada lebih dari satu Field AutoNestedKey";
        public static string KeyNotFound =
            "Tidak memiliki Primary Key";
        public static string BuildMetaData =
            "Error Pembuatan MetaData Class '{0}' :\n{1}";
        public static string CounterTextNotFound =
            "(Tabel '{0}') CounterText pada Template '{1}' tidak ditemukan !";

        public static string TableMetaDataNotFound =
            "(Tabel '{0}') MetaData tidak ditemukan !";
    }

    [DebuggerNonUserCode]
    public static class ErrorPersistance
    {
        public static string BinaryByValue =
            "({0}) Tipe Data Image/ Binary tidak dapat dikirim ByValue";
        public static string SaveError =
            "(Tabel '{0}') Gagal menyimpan Entity !";
        public static string HigherVersionTable =
            "(Tabel '{0}') Versi Database lebih tinggi dari Versi Program !";
        public static string IncompleteParams =
            "Parameter Command tidak lengkap !";
        public static string DataNotFound =
            "(Tabel '{0}') Data sudah diubah oleh user lain, ambil ulang data !";
        public static string OlderEngine =
            "(Tabel '{0}') Versi MetaData ({1}) < Versi Database Aktif ({2}) !";

        public static string ParentUpdateRestrict =
            "'{0}' tidak dapat diubah karena sudah digunakan dalam '{1}' !";
        public static string ParentDeleteRestrict =
            "'{0}' tidak dapat dihapus karena sudah digunakan dalam '{1}' !";
        public static string ChildUpdateRestrict =
            "'{0}' tidak dapat diupdate karena tidak ada data dalam tabel '{1}' !";
        public static string ChildInsertRestrict =
            "'{0}' tidak dapat ditambah karena tidak ada data dalam tabel '{1}' !";

    }

    [DebuggerNonUserCode]
    public static class ErrorEntity
    {
        public static string ParentUpdateNotSupported =
            "(Relasi '{0}'-'{1}') Entitas Anak tidak dapat digunakan sebagai Parent dari Relation !";
        public static string LoadNotFound =
            "(Entity '{0}') Data Tidak Ditemukan !";
        public static string InsertZero =
            "(Entity '{0}') Tidak bisa menambah Data ke Database !";
        public static string FieldNotFound =
            "(Entity '{0}') Field '{1}' tidak ditemukan !";
        public static string TransDateLessThanMinDate =
            "Tgl Transaksi lebih kecil dari Tgl Transaksi yang diperbolehkan";
        public static string OrigTransDateLessThanMinDate =
            "Tgl Transaksi data lama lebih kecil dari Tgl Transaksi yang diperbolehkan";
        public static string TimeStampFieldNotFound =
            "Entity '{0}' tidak memiliki Field TimeStamp";
        public static string DataTypeConversion =
            "Error Konversi Field '{0}' dari Tipe Data '{1}' ke '{2}'.";
    }
}
