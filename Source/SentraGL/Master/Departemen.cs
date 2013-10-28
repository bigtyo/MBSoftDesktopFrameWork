using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSecurity;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    [Index("KodeDepartemen", true)]
    [Index("IdInduk", false)]
    [EnableLog]
    [Relation(typeof(Departemen), "IdDepartemen", "IdInduk")]
    public class Departemen : ParentEntity
    {
        public AutoUpdateBindingList<Departemen> ListDepartemen;

        private string _IdDepartemen;
        [AutoNestedKey("IdInduk"), DataTypeVarChar(50, BrowseHidden = true)]
        public string IdDepartemen
        {
            get { return _IdDepartemen; }
        }

        private string _KodeDepartemen;
        [DataTypeVarChar(50, Default = null), EmptyError]
        public string KodeDepartemen
        {
            get { return _KodeDepartemen; }
            set
            {
                if (_KodeDepartemen == value) return;
                _KodeDepartemen = value;
                LoadEntity("KodeDepartemen=@0", new FieldParam("0", value));
            }
        }

        protected override string GetBrowseColumns()
        {
            return "IdDepartemen, KodeDepartemen, NamaDepartemen";
        }

        private string _NamaDepartemen;
        [DataTypeVarChar(70), EmptyError]
        public string NamaDepartemen
        {
            get { return _NamaDepartemen; }
            set { _NamaDepartemen = value; }
        }

        private string _IdInduk;
        [DataTypeVarChar(50, BrowseHidden = true, Default = null)]
        public string IdInduk
        {
            get { return _IdInduk; }
            set
            {
                _IdInduk = value;

                Departemen dp = new Departemen();
                if (_IdInduk.Length > 0 && dp.FastLoadEntity(
                        "KodeDepartemen,UrutanCetak,NamaDepartemen",
                        "IdDepartemen=" + FormatSqlValue(_IdInduk)))
                {
                    _KodeInduk = dp._KodeDepartemen;
                    UrutanInduk = dp.UrutanCetak;
                    _NamaInduk = dp._NamaDepartemen;
                }
                else
                {
                    _KodeInduk = string.Empty;
                    UrutanInduk = string.Empty;
                    _NamaInduk = string.Empty;
                }
                if (FormMode != FormMode.FormError)
                    _KodeDepartemen = _KodeInduk;
                DataChanged();
            }
        }

        private string _KodeInduk;
        [DataTypeLoadSql(typeof(Departemen),
            "IdDepartemen=IdInduk", "KodeDepartemen")]
        public string KodeInduk
        {
            get { return _KodeInduk; }
        }

        private string _NamaInduk;
        [DataTypeLoadSql(typeof(Departemen),
            "IdDepartemen=IdInduk", "NamaDepartemen")]
        public string NamaInduk
        {
            get { return _NamaInduk; }
        }

        [DataTypeLoadSql(typeof(Departemen),
            "IdDepartemen=IdInduk", "UrutanCetak")]
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
            set
            {
                _Posting = value;
                _DepartemenProduksi = _Posting;
                DataChanged();
            }
        }

        private bool _DepartemenProduksi;
        [DataTypeBoolean]
        public bool DepartemenProduksi
        {
            get { return _DepartemenProduksi; }
            set { _DepartemenProduksi = value; }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
        }

        [DataTypeInteger]
        private int LevelDepartemen;

        [DataTypeVarChar(250)]
        private string UrutanCetak;

        protected override void InitUI()
        {
            ListDepartemen = FastLoadEntities<Departemen>
                ("IdDepartemen,KodeDepartemen,NamaDepartemen",
                "Posting=0 AND Aktif<>0", "KodeDepartemen", true, true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListDepartemen.Close();
        }

        private void CekIndukDepartemen()
        {
            if (_IdInduk.Length == 0) return;

            Departemen dp = new Departemen();

            if (dp.FastLoadEntity("Posting",
                "IdDepartemen=@0", new FieldParam("0", _IdInduk)))
            {
                if (dp.Posting)
                    AddError("IdInduk",
                        "Departemen Induk adalah Departemen Posting");
            }
            else
                AddError("IdInduk",
                    "Departemen Induk tidak ada di database");
        }

        protected override void ValidateError()
        {
            _NamaDepartemen = _NamaDepartemen.Trim();
            _KodeDepartemen = _KodeDepartemen.Trim();
            if (_KodeDepartemen.Length == 0)
                AddError("KodeDepartemen", 
                    "Kode Departemen Tidak Boleh Kosong");
            if (_NamaDepartemen.Length == 0)
                AddError("NamaDepartemen", 
                    "Nama Departemen Tidak Boleh Kosong");

            if (_IdInduk.Length == 0)
                LevelDepartemen = 1;
            else
                LevelDepartemen = _IdInduk.Split('.').Length + 2;
            UrutanCetak = string.Concat(UrutanInduk, "|", _KodeDepartemen);
        }

        protected override void AfterSaveNew()
        {
            CekIndukDepartemen();
        }

        protected override void BeforeSaveUpdate()
        {
            if (GetOriginal<Departemen>()._IdInduk != _IdInduk)
                AddError("IdInduk", "Induk tidak bisa diubah");
        }

        protected override void AfterSaveUpdate()
        {
            CekIndukDepartemen();

            if (_Posting  && Find.IsExists<Departemen>(
                "IdInduk=@0", new FieldParam("0", _IdDepartemen)))
                AddError("Posting", 
                    "Departemen Posting Tidak Boleh Mempunyai Anak");

            if (_KodeDepartemen != GetOriginal<Departemen>()
                .KodeDepartemen && !GetOriginal<Departemen>().Posting)
            {
                string KodeDepartemenOrg = GetOriginal<Departemen>()
                    ._KodeDepartemen;
                ExecuteNonQuery(string.Concat(
                    "UPDATE Departemen SET UrutanCetak=@0+", 
                    Dp.GetSqlSubString("UrutanCetak", 
                    GetOriginal<Departemen>().UrutanCetak
                    .Length + 1, 250), 
                    " WHERE UrutanCetak LIKE @1"),
                    new FieldParam("0", UrutanCetak),
                    new FieldParam("1", GetOriginal<Departemen>()
                        .UrutanCetak + "|%"));
                //ExecuteNonQuery(
                //    string.Concat(
                //    "UPDATE Departemen SET KodeDepartemen=", 
                //    FormatSqlValue(_KodeDepartemen), "+", 
                //    DataPersistance.GetSqlSubString("KodeDepartemen", 
                //    KodeDepartemenOrg.Length + 1, 100),
                //   " WHERE IdDepartemen LIKE ",
                //    FormatSqlValue(string.Concat(IdDepartemen, ".%")),
                //    " AND ", DataPersistance.GetSqlSubString(
                //    "KodeDepartemen", 1, KodeDepartemenOrg.Length), "=", 
                //    FormatSqlValue(GetOriginal<Departemen>()
                //    ._KodeDepartemen)));
            }
        }

        private string OldAkun = string.Empty;
        public void ShowAkun<TForm>()
        {
            Test tst = new Test();
            tst.Akun = OldAkun;

            if (ShowDialog<TForm>(tst))
            {
                OldAkun = tst.Akun;
                ShowMessage("Anda memilih Akun " + OldAkun);
            }
            else
                ShowMessage("Anda Batal Memilih Akun");
        }
    }
}
