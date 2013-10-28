using System;
using System.Collections.Generic;
using System.Text;
using SentraSecurity;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    [Index("KodeProyek", true)]
    [Index("IdInduk", false)]
    [EnableLog]
    [Relation(typeof(Proyek), "IdProyek", "IdInduk")]
    public class Proyek : ParentEntity
    {
        public AutoUpdateBindingList<Proyek> ListProyek;

        private string _IdProyek;
        [AutoNestedKey("IdInduk"), DataTypeVarChar(50, BrowseHidden = true)]
        public string IdProyek
        {
            get { return _IdProyek; }
        }

        private string _KodeProyek;
        [DataTypeVarChar(50, Default = null), EmptyError]
        public string KodeProyek
        {
            get { return _KodeProyek; }
            set
            {
                if (_KodeProyek == value) return;
                _KodeProyek = value;
                LoadEntity("KodeProyek=@0", new FieldParam("0", value));
            }
        }

        private string _NamaProyek;
        [DataTypeVarChar(70), EmptyError]
        public string NamaProyek
        {
            get { return _NamaProyek; }
            set { _NamaProyek = value; }
        }

        private string _IdInduk;
        [DataTypeVarChar(50, BrowseHidden = true, Default = null)]
        public string IdInduk
        {
            get { return _IdInduk; }
            set
            {
                _IdInduk = value;

                Proyek pry = new Proyek();
                if (pry.FastLoadEntity(
                    "KodeProyek,UrutanCetak,NamaProyek",
                    "IdProyek=@0", new FieldParam("0", _IdInduk)))
                {
                    _KodeInduk = pry._KodeProyek;
                    UrutanInduk = pry.UrutanCetak;
                    _NamaInduk = pry._NamaProyek;
                }
                else
                {
                    _KodeInduk = string.Empty;
                    UrutanInduk = string.Empty;
                    _NamaInduk = string.Empty;
                }
                DataChanged();
            }
        }

        private string _KodeInduk;
        [DataTypeLoadSql(typeof(Proyek), "IdProyek=IdInduk", "KodeProyek")]
        public string KodeInduk
        {
            get { return _KodeInduk; }
        }

        private string _NamaInduk;
        [DataTypeLoadSql(typeof(Proyek), "IdProyek=IdInduk", "NamaProyek")]
        public string NamaInduk
        {
            get { return _NamaInduk; }
        }

        [DataTypeLoadSql(typeof(Proyek), "IdProyek=IdInduk", "UrutanCetak")]
        private string UrutanInduk;

        private string _Keterangan;
        [DataTypeVarChar(250)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private bool _Posting;
        [DataTypeBoolean]
        public bool Posting
        {
            get { return _Posting; }
            set { _Posting = value; }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
        }

        [DataTypeInteger]
        private int LevelProyek;

        [DataTypeVarChar(250)]
        private string UrutanCetak;

        protected override void InitUI()
        {
            ListProyek = FastLoadEntities<Proyek>
                ("IdProyek,KodeProyek,NamaProyek", 
                "Posting=0 AND Aktif<>0", "KodeProyek", true, true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListProyek.Close();
        }

        private void CekIndukProyek()
        {
            if (_IdInduk.Length == 0) return;

            object Posting;
            if (Find.TryFindValue<Proyek>("Posting", "IdProyek=@0", 
                out Posting, new FieldParam("0", _IdInduk)))
            {
                if ((bool)Posting)
                    AddError("IdInduk", 
                        "Proyek Induk adalah Proyek Posting");
            } else
                AddError("IdInduk",
                    "Proyek Induk tidak ada di database");
        }

        protected override void ValidateError()
        {
            _NamaProyek = _NamaProyek.Trim();
            _KodeProyek = _KodeProyek.Trim();
            if (_IdInduk.Length == 0)
                LevelProyek = 1;
            else
                LevelProyek = _IdInduk.Split('.').Length + 2;
            UrutanCetak = string.Concat(UrutanInduk, "|", _KodeProyek);
        }

        protected override void AfterSaveNew()
        {
            CekIndukProyek();
        }

        protected override void BeforeSaveUpdate()
        {
            if (GetOriginal<Proyek>()._IdInduk != _IdInduk)
                AddError("IdInduk", "Induk tidak bisa diubah");
        }

        protected override void AfterSaveUpdate()
        {
            CekIndukProyek();

            if (_Posting && Find.IsExists<Proyek>("IdInduk=@0", 
                new FieldParam("0", _IdProyek)))
            {
                AddError("Posting", 
                    "Proyek Posting Tidak Boleh Mempunyai Anak");
            }

            if (!GetOriginal<Proyek>().Posting &&
                _KodeProyek != GetOriginal<Proyek>().KodeProyek)
            {
                string KodeProyekOrg = GetOriginal<Proyek>().KodeProyek;
                ExecuteNonQuery(string.Concat(
                    "UPDATE Proyek SET UrutanCetak=@0+", 
                    Dp.GetSqlSubString("UrutanCetak", 
                    GetOriginal<Proyek>().UrutanCetak.Length + 1, 250), 
                    " WHERE UrutanCetak LIKE @1"),
                    new FieldParam("0", UrutanCetak),
                    new FieldParam("1", GetOriginal<Proyek>()
                        .UrutanCetak+ "|%"));
                //ExecuteNonQuery(string.Concat(
                //    "UPDATE Proyek SET KodeProyek=", 
                //    FormatSqlValue(_KodeProyek), "+", 
                //    DataPersistance.GetSqlSubString("KodeProyek", 
                //    KodeProyekOrg.Length + 1, 100),
                //    " WHERE IdProyek LIKE ",
                //    FormatSqlValue(IdProyek + ".%"), " AND ", 
                //    DataPersistance.GetSqlSubString("KodeProyek", 1, 
                //    KodeProyekOrg.Length), "=", 
                //    FormatSqlValue(GetOriginal<Proyek>().KodeProyek)));
            }
        }
    }
}
