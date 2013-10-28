using System;
using System.Collections.Generic;
using System.Text;
using SentraSecurity;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using SentraGL;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    public enum enJenisAkun
    {
        Aktiva,
        Kewajiban,
        Modal,
        Laba__Rugi
    }

    public enum enKelompokAkun
    {
        _,
        // JenisAkun Aktiva (1-11)
        Kas__Bank,
        PiutangUsaha,
        PiutangLain_Lain,
        PiutangPajak,
        Persediaan,
        UangMukaPembelian,
        BiayaDibayarDimuka,
        AktivaLancarLain_Lain,
        AktivaTetap,
        AkumulasiPenyusutanAktiva,
        AktivaLain_Lain,

        // JenisAkun Kewajiban (12-17)
        HutangUsaha,
        HutangLancarLain_Lain,
        HutangPajak,
        UangMukaPenjualan,
        PendapatanDiterimaDimuka,
        HutangJangkaPanjang,

        // JenisAkun Modal (18-21)
        ModalDisetor,
        PengambilanDeviden,
        Laba__RugiTahunBerjalan,
        Laba__RugiTahunLalu,

        // JenisAkun Laba/ Rugi (22-27)
        Pendapatan__BiayaPenjualan,
        Pendapatan__BiayaPabrik,
        HPP,
        Pendapatan__BiayaOperasional,
        Pendapatan__BiayaLain_Lain,
        Pendapatan__BiayaPajak
    }

    [EnableLog]
    [Relation(typeof(MataUang))]
    [Index("NoAkun", true)]
    [Index("IdInduk", false)]
    [Index("KelompokAkun", false)]
    [Index("UrutanCetak", false)]
    [Index("UrutanKelompok", false)]
    [Relation(typeof(Akun), "IdAkun", "IdInduk")]
    public class Akun : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListIndukAkun;
        public AutoUpdateBindingList<MataUang> ListMataUang;
        public List<MasterKelompokAkun> ListKelompokAkun;

        public Akun() { }

        public Akun(string IdAkun) { _IdAkun = IdAkun; }

        public Akun(string NoAkun, string NamaAkun, string NoInduk,
            enJenisAkun JenisAkun, enKelompokAkun KelompokAkun, string KodeMataUang,
            string Keterangan, bool Terkunci, bool Posting,
            bool Aktif)
        {
            if (NoInduk.Length > 0)
                IdInduk = (string)Find.FirstValue<Akun>("IdAkun",
                    "NoAkun=@0", string.Empty, string.Empty,
                    new FieldParam("0", NoInduk));
            else
                IdInduk = string.Empty;
            _NoAkun = NoAkun;
            _NamaAkun = NamaAkun;
            this.JenisAkun = JenisAkun;
            this.KelompokAkun = KelompokAkun;
            this.KodeMataUang = KodeMataUang;
            _Terkunci = Terkunci;
            _Keterangan = Keterangan;
            this.Posting = Posting;
            _Aktif = Aktif;

            FormMode = FormMode.FormAddNew;
        }

        private string _IdAkun;
        [AutoNestedKey("IdInduk"), DataTypeVarChar(50, 
            BrowseHidden = true)]
        public string IdAkun
        {
            get { return _IdAkun; }
        }

        private string _NoAkun;
        [DataTypeVarChar(50, Default = null), EmptyError]
        public string NoAkun
        {
            get { return _NoAkun; }
            set
            {
                if (_NoAkun == value) return;
                _NoAkun = value;
                LoadEntity("NoAkun=@0", new FieldParam("0", value));
            }
        }

        private string _NamaAkun;
        [DataTypeVarChar(70), EmptyError]
        public string NamaAkun
        {
            get { return _NamaAkun; }
            set { _NamaAkun = value; }
        }

        private string _IdInduk;
        [DataTypeVarChar(50, BrowseHidden = true, Default = null)]
        public string IdInduk
        {
            get
            {
                return (_IdInduk);
            }
            set
            {
                _IdInduk = value;
                Akun ak = new Akun();
                if (_IdInduk.Length > 0 && ak.FastLoadEntity(
                        "NoAkun,JenisAkun,UrutanCetak,NamaAkun",
                        "IdAkun=@0",  new FieldParam("0", _IdInduk)))
                {
                    _NoInduk = ak._NoAkun;
                    _JenisAkun = ak._JenisAkun;
                    UrutanInduk = ak.UrutanCetak;
                    _NamaInduk = ak._NamaAkun;
                }
                else
                {
                    _JenisAkun = enJenisAkun.Aktiva;
                    UrutanInduk = string.Empty;
                    _NoInduk = string.Empty;
                    _NamaInduk = string.Empty;
                }
                if (_Posting == false)
                {
                    if (ListKelompokAkun != null)
                        ListKelompokAkun.Clear();
                    _AkunMoneter = false;
                    _KodeMataUang = string.Empty;
                }
                else
                {
                    IsiListKelompokAkun(enKelompokAkun._);
                }
                DataChanged();
            }
        }

        private string _NoInduk;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdInduk", "NoAkun")]
        public string NoInduk
        {
            get { return _NoInduk; }
        }

        private string _NamaInduk;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdInduk", "NamaAkun")]
        public string NamaInduk
        {
            get { return _NamaInduk; }
        }

        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdInduk", "UrutanCetak")]
        private string UrutanInduk;

        private enJenisAkun _JenisAkun;
        [DataTypeVarChar(50, Default = null)]
        public enJenisAkun JenisAkun
        {
            get { return _JenisAkun; }
            set
            {
                if (_IdInduk.Length == 0)
                {
                    _JenisAkun = value;
                    if (_Posting == false)
                    {
                        if (ListKelompokAkun != null)
                            ListKelompokAkun.Clear();
                        _AkunMoneter = false;
                        _KodeMataUang = string.Empty;
                        _KelompokAkun = enKelompokAkun._;
                    }
                    else
                        IsiListKelompokAkun(enKelompokAkun._);
                }
                DataChanged();
            }
        }

        private enKelompokAkun _KelompokAkun;
        [DataTypeVarChar(50, Default = null)]
        public enKelompokAkun KelompokAkun
        {
            get { return _KelompokAkun; }
            set
            {
                _KelompokAkun = value;
                string NamaKelompokAkun = 
                    EnumDef.GetEnumName<enKelompokAkun>(value);

                _Terkunci = _Posting && (_KelompokAkun == enKelompokAkun.Kas__Bank);

                if (_Posting && (_KelompokAkun == enKelompokAkun.Kas__Bank || 
                    NamaKelompokAkun.StartsWith("Hutang") ||
                    NamaKelompokAkun.StartsWith("Piutang") || 
                    NamaKelompokAkun.StartsWith("Uang Muka")))
                {
                    _AkunMoneter = true;
                    _KodeMataUang = BaseGL.SetingPerusahaan.MataUangDasar;
                }
                else
                {
                    _AkunMoneter = false;
                    _KodeMataUang = string.Empty;
                }
                DataChanged();
            }
        }

        private string _KodeMataUang;
        [DataTypeVarChar(3, Default = null)]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                if (_AkunMoneter) _KodeMataUang = value;
            }
        }

        private string _Keterangan;
        [DataTypeVarChar(250)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        /// <summary>
        /// Jml RefAkun yang menggunakan akun ini
        /// </summary>
        [DataTypeInteger]
        private int JmlPengunci;

        private bool _Terkunci;
        /// <summary>
        /// Akun terkunci tidak dapat digunakan di Jurnal Umum
        /// </summary>
        [DataTypeBoolean(Default = false)]
        public bool Terkunci
        {
            get { return _Terkunci; }
        }

        private bool _Posting;
        [DataTypeBoolean]
        public bool Posting
        {
            get { return _Posting; }
            set
            {
                if (_Posting != value)
                {
                    _Posting = value;
                    if (_Posting == false)
                    {
                        if (ListKelompokAkun != null)
                            ListKelompokAkun.Clear();
                        _AkunMoneter = false;
                        _KodeMataUang = string.Empty;
                        _Terkunci = false;
                    }
                    else
                    {
                        IsiListKelompokAkun(enKelompokAkun._);
                        _Terkunci = KelompokAkun ==
                            enKelompokAkun.Kas__Bank;
                    }
                    DataChanged();
                }
            }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
        }

        [DataTypeInteger]
        private int LevelAkun;

        private bool _AkunMoneter;
        [DataTypeBoolean(BrowseHidden = true, Default = null)]
        public bool AkunMoneter
        {
            get { return _AkunMoneter; }
        }

        /// <summary>
        /// Flag apakah Akun ini menggunakan Akun Mata Uang Dasar/ tidak
        /// </summary>
        [DataTypeBoolean]
        private bool MataUangDasar;

        public bool AkunMoneterVisible
        {
            get
            {
                return _AkunMoneter && BaseGL
                    .SetingPerusahaan.MultiMataUang;
            }
        }

        /// <summary>
        /// Urutan Cetak Akun sesuai urutan induk-anak
        /// </summary>
        [DataTypeVarChar(250)]
        private string UrutanCetak;

        [DataTypeInteger]
        private int UrutanKelompok;

        public bool EnableJenisAkun
        {
            get
            {
                return _IdInduk.Length == 0 &&
                  (FormMode == FormMode.FormAddNew ||
                  FormMode == FormMode.FormEdit);
            }
        }

        private void IsiListKelompokAkun(enKelompokAkun Default)
        {
            if (ListKelompokAkun == null) return;
            string NamaKelompok;

            ListKelompokAkun.Clear();
            switch (_JenisAkun)
            {
                case enJenisAkun.Aktiva:
                    for (int i = 1; i <= 11; i++)
                        ListKelompokAkun.Add(new MasterKelompokAkun(i));
                    if (Default == enKelompokAkun._ && _Posting)
                    {
                        _KelompokAkun = enKelompokAkun.Kas__Bank;
                        DataChanged();
                    }
                    NamaKelompok = EnumDef
                        .GetEnumName<enKelompokAkun>(_KelompokAkun);
                    if (_Posting && (
                        _KelompokAkun == enKelompokAkun.Kas__Bank ||
                        NamaKelompok.StartsWith("Uang Muka") ||
                        NamaKelompok.StartsWith("Piutang")))
                    {
                        _AkunMoneter = true;
                        _KodeMataUang = BaseGL.SetingPerusahaan
                            .MataUangDasar;
                    }
                    else
                    {
                        _AkunMoneter = false;
                        _KodeMataUang = string.Empty;
                    }
                    break;
                case enJenisAkun.Kewajiban:
                    for (int i = 12; i <= 17; i++)
                        ListKelompokAkun.Add(new MasterKelompokAkun(i));
                    if (Default == enKelompokAkun._ && _Posting)
                    {
                        _KelompokAkun = enKelompokAkun.HutangUsaha;
                        DataChanged();
                    }
                    NamaKelompok = EnumDef
                        .GetEnumName<enKelompokAkun>(_KelompokAkun);
                    if (_Posting && (
                        NamaKelompok.StartsWith("Uang Muka") ||
                        NamaKelompok.StartsWith("Hutang")))
                    {
                        _AkunMoneter = true;
                        _KodeMataUang = BaseGL.SetingPerusahaan
                            .MataUangDasar;
                    }
                    else
                    {
                        _AkunMoneter = false;
                        _KodeMataUang = string.Empty;
                    }
                    break;
                case enJenisAkun.Modal:
                    for (int i = 18; i <= 21; i++)
                        ListKelompokAkun.Add(new MasterKelompokAkun(i));
                    if (Default == enKelompokAkun._ && _Posting)
                    {
                        _KelompokAkun = enKelompokAkun.ModalDisetor;
                        DataChanged();
                    }
                    _AkunMoneter = false;
                    _KodeMataUang = string.Empty;
                    break;
                case enJenisAkun.Laba__Rugi:
                    for (int i = 22; i <= 27; i++)
                        ListKelompokAkun.Add(new MasterKelompokAkun(i));
                    if (Default == enKelompokAkun._ && _Posting)
                    {
                        _KelompokAkun = enKelompokAkun
                            .Pendapatan__BiayaPabrik;
                        DataChanged();
                    }
                    _AkunMoneter = false;
                    _KodeMataUang = string.Empty;
                    break;
            }
        }

        protected override void InitUI()
        {
            ListIndukAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun", "Posting=0 AND Aktif<>0",
                "NoAkun", true, true);
            ListMataUang = FastLoadEntities<MataUang>(
                "KodeMataUang,NamaMataUang", "Aktif<>0", 
                "KodeMataUang", true);
            ListKelompokAkun = new List<MasterKelompokAkun>();
            Posting = true;
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListIndukAkun.Close();
            ListMataUang.Close();
        }

        private void CekIndukAkun()
        {
            if (_IdInduk.Length == 0) return;

            Akun ak = new Akun();

            if (ak.FastLoadEntity("Posting,JenisAkun",
                "IdAkun=@0", new FieldParam("0", _IdInduk)))
            {
                if (ak.Posting)
                    AddError("IdInduk", 
                        "Akun Induk adalah Akun Posting");
                if (_JenisAkun != ak._JenisAkun)
                    AddError("JenisAkun", string.Concat(
                        "Jenis akun harus sama dengan jenis akun induk (", 
                        EnumDef.GetEnumName<enJenisAkun>
                        (ak._JenisAkun), ")"));
            } else
                AddError("IdInduk",
                    "Akun Induk tidak ada di database");
        }

        protected override void ValidateError()
        {
            _NamaAkun = _NamaAkun.Trim();
            _NoAkun = _NoAkun.Trim();
            UrutanCetak = string.Concat(UrutanInduk, "|", _NoAkun);
            if (_Posting)
            {
                foreach (MasterKelompokAkun Mk in ListKelompokAkun)
                    if (Mk.KelompokAkun == _KelompokAkun)
                    {
                        UrutanKelompok = (int)Mk.KelompokAkun;
                        break;
                    }
            }
            else
            {
                UrutanKelompok = 0;
                _KelompokAkun = enKelompokAkun._;
            }
            if (!_AkunMoneter || !_Posting || 
                _JenisAkun == enJenisAkun.Modal ||
                _JenisAkun == enJenisAkun.Laba__Rugi)
            {
                _AkunMoneter = false;
                _KodeMataUang = string.Empty;
            }

            if (_AkunMoneter)
            {
                if (_KodeMataUang.Length == 0)
                    AddError("KodeMataUang", 
                        "Akun moneter harus memiliki mata uang");
                MataUangDasar = _KodeMataUang == BaseGL
                    .SetingPerusahaan.MataUangDasar;
            }
            else
                MataUangDasar = false;
            
            if (_IdInduk.Length == 0)
                LevelAkun = 1;
            else
                LevelAkun = _IdInduk.Split('.').Length + 1;
            DataChanged();
        }

        protected override void AfterSaveDelete()
        {
            Akun Ak = GetOriginal<Akun>();

            if (GetErrorString().Length == 0)
            {
                if (Ak._KelompokAkun == enKelompokAkun
                    .Laba__RugiTahunBerjalan)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunBerjalan = string.Empty;
                else if (Ak._KelompokAkun == enKelompokAkun
                    .Laba__RugiTahunLalu)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunLalu = string.Empty;
                BaseSecurity.DeleteDocument(GetType(), Ak._IdAkun);
                if (Ak._KelompokAkun == enKelompokAkun.Kas__Bank)
                    KurangiJmlPengunci(Dp, Ak._IdAkun);
            }
        }

        protected override void BeforeSaveNew()
        {
            string NoAkun1;
            if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunBerjalan)
            {
                NoAkun1 = (string)Find.Value<Akun>("NoAkun",
                    "KelompokAkun=@0", string.Empty,
                    new FieldParam("0", _KelompokAkun));
                if (NoAkun1.Length > 0) AddError("NoAkun", 
                    "Akun Laba/Rugi Tahun Berjalan sudah diset pada No akun" + NoAkun1);
            }
            else if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunLalu)
            {
                NoAkun1 = (string)Find.Value<Akun>("NoAkun",
                    "KelompokAkun=@0", string.Empty,
                    new FieldParam("0", _KelompokAkun));
                if (NoAkun1.Length > 0) AddError("NoAkun", 
                    "Akun Laba/Rugi Tahun Lalu sudah diset pada No akun" + NoAkun1);
            }
        }

        protected override void AfterSaveNew()
        {
            CekIndukAkun();
            if (GetErrorString().Length == 0)
            {
                if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunBerjalan)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunBerjalan = _IdAkun;
                else if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunLalu)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunLalu = _IdAkun;
            }
            if (_KelompokAkun == enKelompokAkun.Kas__Bank)
                TambahJmlPengunci(Dp, _IdAkun);
        }

        protected override void BeforeSaveUpdate()
        {
            string NoAkun1;
            if (GetOriginal<Akun>()._IdInduk != _IdInduk)
                AddError("IdInduk", "Induk tidak bisa diubah");

            if (_KelompokAkun != GetOriginal<Akun>().KelompokAkun)
            {
                if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunBerjalan)
                {
                    NoAkun1 = (string)Find.Value<Akun>("NoAkun",
                        "KelompokAkun=@0", string.Empty,
                        new FieldParam("0", _KelompokAkun)); 
                    if (NoAkun1.Length > 0) AddError("NoAkun", 
                        "Akun Laba/Rugi Tahun Berjalan sudah diset pada No akun" + NoAkun1);
                }
                else if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunLalu)
                {
                    NoAkun1 = (string)Find.Value<Akun>("NoAkun",
                        "KelompokAkun=@0", string.Empty,
                        new FieldParam("0", _KelompokAkun)); 
                    if (NoAkun1.Length > 0) AddError("NoAkun", 
                        "Akun Laba/Rugi Tahun Lalu sudah diset pada No akun" + NoAkun1);
                }
            }
        }

        protected override void AfterSaveUpdate()
        {
            CekIndukAkun();

            if (_Posting && Find.IsExists<Akun>("IdInduk=@0",
                new FieldParam("0", _IdAkun)))
                AddError("Posting", 
                    "Akun Posting Tidak Boleh Mempunyai Anak");

            if (_JenisAkun != GetOriginal<Akun>()._JenisAkun)
            {
                if (Find.IsExists<Akun>("IdInduk=@0",
                    new FieldParam("0", _IdAkun)))
                    AddError("JenisAkun", 
                        "Jenis Akun tidak dapat diubah karena sudah memiliki anak");
            }
            if (_NoAkun != GetOriginal<Akun>().NoAkun && 
                GetOriginal<Akun>().Posting == false)
            {
                string NoAkunOrg = GetOriginal<Akun>().NoAkun;
                ExecuteNonQuery(string.Concat(
                    "UPDATE Akun SET UrutanCetak=@0+", 
                    Dp.GetSqlSubString("UrutanCetak", 
                    GetOriginal<Akun>().UrutanCetak.Length + 1, 250), 
                    " WHERE UrutanCetak LIKE @1"),
                    new FieldParam("0", UrutanCetak),
                    new FieldParam("1", GetOriginal<Akun>()
                        .UrutanCetak + "|%"));
                //ExecuteNonQuery(
                //    string.Concat("UPDATE Akun SET NoAkun = ", 
                //    FormatSqlValue(_NoAkun), "+", 
                //    DataPersistance.GetSqlSubString("NoAkun", 
                //    NoAkunOrg.Length + 1, 100), " WHERE IdAkun LIKE ",
                //    FormatSqlValue(string.Concat(IdAkun, ".%")),
                //    " AND ", DataPersistance.GetSqlSubString("NoAkun", 
                //    1, NoAkunOrg.Length), " = ", FormatSqlValue(
                //    GetOriginal<Akun>().NoAkun)));
            }
            if (GetErrorString().Length == 0)
            {
                if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunBerjalan)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunBerjalan = _IdAkun;
                else if (_KelompokAkun == enKelompokAkun.Laba__RugiTahunLalu)
                    BaseGL.SetingPerusahaan
                        .IdAkunLabaRugiTahunLalu = _IdAkun;
                BaseSecurity.UpdateDocument(GetType(), 
                    GetOriginal<Akun>()._IdAkun, _IdAkun);
                if (_KelompokAkun != GetOriginal<Akun>()._KelompokAkun)
                {
                    if (GetOriginal<Akun>()._KelompokAkun == 
                        enKelompokAkun.Kas__Bank)
                        KurangiJmlPengunci(Dp, _IdAkun);

                    if (_KelompokAkun == enKelompokAkun.Kas__Bank)
                        TambahJmlPengunci(Dp, _IdAkun);
                }
            }
        }

        protected override void AfterLoadFound()
        {
            IsiListKelompokAkun(_KelompokAkun);
        }

        public override bool IsVisible(string FieldName)
        {
            if (FormMode == FormMode.FormAddNew && FieldName == "Terkunci")
                return false;
            else
                return true;
        }

        public override bool IsReadOnly(string FieldName)
        {
            if (!BaseGL.SetingPerusahaan.MultiMataUang && 
                FieldName == "KodeMataUang")
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Tambahkan jumlah counter pengunci pada akun
        /// </summary>
        /// <param name="Dp"></param>
        /// <param name="IdAkun"></param>
        public static void TambahJmlPengunci(DataPersistance Dp,
            string IdAkun)
        {
            Dp.ExecuteNonQuery(string.Concat(
                "UPDATE Akun SET Terkunci=",
                Dp.FormatSqlValue(true, DataType.Boolean), 
                ",JmlPengunci=JmlPengunci+1 WHERE IdAkun=@0"),
                new FieldParam("0", IdAkun));
        }

        /// <summary>
        /// Kurangi jumlah counter pengunci pada akun
        /// </summary>
        /// <param name="Dp"></param>
        /// <param name="IdAkun"></param>
        public static void KurangiJmlPengunci(DataPersistance Dp,
            string IdAkun)
        {
            Dp.ExecuteNonQuery(string.Concat(
                "UPDATE Akun SET JmlPengunci=", 
                Dp.GetSqlIifNoFormat("JmlPengunci<2", "0", 
                "JmlPengunci-1"), ",Terkunci=",
                Dp.GetSqlIif("JmlPengunci>0", true, false),
                " WHERE IdAkun=@0"), 
                new FieldParam("0", IdAkun));
        }

        /// <summary>
        /// Update Field MataUangDasr di semua akun
        /// </summary>
        /// <param name="Dp"></param>
        /// <param name="MataUang"></param>
        public static void SetMataUangDasar(DataPersistance Dp, 
            string MataUang)
        {
            Dp.ExecuteNonQuery(string.Concat(
                "UPDATE Akun SET MataUangDasar=",
                Dp.FormatSqlValue(false, DataType.Boolean),
                " WHERE MataUangDasar=",
                Dp.FormatSqlValue(true, DataType.Boolean)));
            Dp.ExecuteNonQuery(string.Concat(
                "UPDATE Akun SET MataUangDasar=",
                Dp.FormatSqlValue(true, DataType.Boolean),
                " WHERE KodeMataUang=@0"), 
                new FieldParam("0", MataUang));

        }
    }

    public class MasterKelompokAkun : PublishFieldEntity
    {
        public enKelompokAkun KelompokAkun;
        public string NamaKelompokAkun;

        internal MasterKelompokAkun(int KelompokAkun)
            : this((enKelompokAkun)KelompokAkun) { }
        internal MasterKelompokAkun(enKelompokAkun KelompokAkun)
        {
            this.KelompokAkun = KelompokAkun;
            this.NamaKelompokAkun = EnumDef
                .GetEnumName<enKelompokAkun>(KelompokAkun);
        }
    }
}