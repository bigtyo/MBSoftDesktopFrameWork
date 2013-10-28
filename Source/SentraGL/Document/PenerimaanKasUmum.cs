using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSecurity;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Document
{
    public enum enJenisPembayaran
    {
        Tunai //,
        //Cek__Giro
    }
    public enum enStatusTransaksiKas
    {
        _,
        BelumKliring,
        SudahKliring,
        GagalKliring
    }

    [Relation(typeof(Akun), "IdAkun", "IdKas")]
    [Index("TglKliring", false)]
    public class PenerimaanKasUmum : DocumentEntity
    {
        public static string ModuleName = "Penerimaan Kas Umum";

        #region Class Contructor
        public PenerimaanKasUmum() { }

        public PenerimaanKasUmum(string NoPenerimaanKas, string IdAkunKas, 
            string NoKuitansi, DateTime TglPenerimaan,
            string TerimaDari, string Keperluan, string Catatan, 
            string JenisDokSumber, string NoDokSumber,
            enJenisPembayaran JenisTransaksi, string NoCekGiro, 
            DateTime TglJatuhTempo, bool Internal)
        {
            _NoPenerimaanKas = NoPenerimaanKas;
            _IdKas = IdAkunKas;
            _NoKuitansi = NoKuitansi;
            _TglPenerimaan = TglPenerimaan;
            _TerimaDari = TerimaDari;
            _Keperluan = Keperluan;
            _Catatan = Catatan;
            _JenisDokSumber = JenisDokSumber;
            _NoDokSumber = NoDokSumber;
            this.JenisTransaksi = JenisTransaksi;
            _NoCekGiro = NoCekGiro;
            _TglJatuhTempo = TglJatuhTempo;
            _Internal = Internal;
        }
        #endregion

        #region AutoUpdateBindingList
        public AutoUpdateBindingList<JenisPenerimaanKas> ListJenisPenerimaan;
        public AutoUpdateBindingList<Akun> ListKas;
        public AutoUpdateBindingList<Proyek> ListProyek;
        public AutoUpdateBindingList<Departemen> ListDepartemen;
        #endregion

        #region Class Attribute
        private string _NoPenerimaanKas;
        [AutoNumberKey("XXX.BKMYYMMCCCC", "CCCC", "TglPenerimaan"), DataTypeVarChar(50)]
        public string NoPenerimaanKas
        {
            get { return _NoPenerimaanKas; }
        }

        private string _IdKas;
        [DataTypeVarChar(50, Default = null, BrowseHidden = true), 
            EmptyError("Kas tidak boleh kosong")]
        public string IdKas
        {
            get { return _IdKas; }
            set
            {
                _IdKas = value;
                UpdateLoadSqlFields();
            }
        }

        private string _NoKas;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKas", "NoAkun")] 
        public string NoKas
        {
            get { return _NoKas; }
        }

        private string _NamaKas;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKas", "NamaAkun")]
        public string NamaKas
        {
            get { return _NamaKas; }
        }

        private string _NoKuitansi;
        [DataTypeVarChar(50)]
        public string NoKuitansi
        {
            get { return _NoKuitansi; }
            set { _NoKuitansi = value; }
        }

        private DateTime _TglPenerimaan;
        [DataTypeDate, TransactionDate]
        public DateTime TglPenerimaan
        {
            get { return _TglPenerimaan; }
            set { _TglPenerimaan = value; }
        }

        private string _TerimaDari;
        [DataTypeVarChar(50), EmptyError]
        public string TerimaDari
        {
            get { return _TerimaDari; }
            set { _TerimaDari = value; }
        }

        private string _Keperluan;
        [DataTypeVarChar(150), EmptyError]
        public string Keperluan
        {
            get { return _Keperluan; }
            set { _Keperluan = value; }
        }

        private string _JenisDokSumber;
        [DataTypeVarChar(50)]
        public string JenisDokSumber
        {
            get { return _JenisDokSumber; }
            set { _JenisDokSumber = value; }
        }

        private string _NoDokSumber;
        [DataTypeVarChar(50)]
        public string NoDokSumber
        {
            get { return _NoDokSumber; }
            set { _NoDokSumber = value; }
        }

        private string _Catatan;
        [DataTypeVarChar(200)]
        public string Catatan
        {
            get { return _Catatan; }
            set { _Catatan = value; }
        }

        private decimal _TotalNilai;
        [DataTypeDecimal, EmptyError]
        public decimal TotalNilai
        {
            get { return _TotalNilai; }
        }

        private string _MataUang;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKas", "KodeMataUang")]
        public string MataUang
        {
            get { return _MataUang; }
        }

        private enJenisPembayaran _JenisTransaksi;
        [DataTypeVarChar(20, Default = null)]
        public enJenisPembayaran JenisTransaksi
        {
            get { return _JenisTransaksi; }
            set
            {
                _JenisTransaksi = value;
                if (value == enJenisPembayaran.Tunai)
                {
                    _StatusTransaksi = enStatusTransaksiKas._;
                    _TglKliring = _TglPenerimaan;
                    _TglJatuhTempo = _TglKliring;
                }
                else
                {
                    _StatusTransaksi = enStatusTransaksiKas.BelumKliring;
                    _TglKliring = new DateTime(1900, 1, 1);
                }
                DataChanged();
                FormChanged();
            }
        }

        public bool TampilkanCekGiro
        {
            get
            {
                return false;
                //return _JenisTransaksi ==
                //    enJenisPembayaran.Cek__Giro;
            }
        }

        private string _NoCekGiro;
        [DataTypeVarChar(50)]
        public string NoCekGiro
        {
            get { return _NoCekGiro; }
            set { _NoCekGiro = value; }
        }

        private enStatusTransaksiKas _StatusTransaksi;
        [DataTypeVarChar(20, Default = null)]
        public enStatusTransaksiKas StatusTransaksi
        {
            get { return _StatusTransaksi; }
        }

        private DateTime _TglJatuhTempo;
        [DataTypeDate]
        public DateTime TglJatuhTempo
        {
            get { return _TglJatuhTempo; }
            set { _TglJatuhTempo = value; }
        }

        private DateTime _TglKliring;
        [DataTypeDate(Default = "1/1/1900")]
        public DateTime TglKliring
        {
            get { return _TglKliring; }
        }

        private bool _Internal;
        [DataTypeBoolean(Default=false)]
        public bool Internal
        {
            get { return _Internal; }
        }

        public EntityCollection<PenerimaanKasUmumDetil> PenerimaanKasDetil;
        #endregion

        #region Override Methods
        protected override string GetAutoNumberTemplate()
        {
            return BaseGL.SetingPerusahaan.KodePerusahaan + ".BKMYYMMCCCC";
        }

        protected override void InitUI()
        {
            ListKas = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun",
                "Aktif<>0 AND KelompokAkun=" +
                FormatSqlValue(enKelompokAkun.Kas__Bank),
                "NoAkun", true);
            ListJenisPenerimaan =
                FastLoadEntities<JenisPenerimaanKas>(
                "JenisPenerimaan", "Aktif<>0",
                "JenisPenerimaan", true);
            ListDepartemen = FastLoadEntities<Departemen>
                ("IdDepartemen,KodeDepartemen,NamaDepartemen",
                "Posting<>0 AND Aktif<>0", "KodeDepartemen",
                true, true);
            ListProyek = FastLoadEntities<Proyek>
                ("IdProyek,KodeProyek,NamaProyek",
                "Posting<>0 AND Aktif<>0", "KodeProyek",
                true, true);
            _IdKas = Dp.GetVariable<string>(
                "PenerimaanKasUmumVar", "IdKas", string.Empty);
        }

        protected override void EndUI()
        {
            ListKas.Close();
            ListJenisPenerimaan.Close();
            ListDepartemen.Close();
            ListProyek.Close();
            Dp.SetVariable("PenerimaanKasUmumVar", 
                "IdKas", _IdKas);
        }

        protected override void ValidateError()
        {
            if (PenerimaanKasDetil.Count == 0)
                AddError("NoPenerimaanKas", 
                    "Detil Penerimaan Kas tidak boleh kosong");
            if (_TotalNilai < 0)
                AddError("TotalNilai",
                    "Total Nilai harus lebih besar atau sama dengan Nol");

            if (_StatusTransaksi == enStatusTransaksiKas._)
            {
                _TglKliring = _TglPenerimaan;
                _TglJatuhTempo = _TglPenerimaan;
            }
        }

        protected override void onChildDataChanged(string ChildName, BusinessEntity ChildObject)
        {
            _TotalNilai = PenerimaanKasDetil.Sum("NilaiPenerimaan");
            DataChanged();
        }

        protected override void AfterSaveNew()
        {
            if (_StatusTransaksi == enStatusTransaksiKas._)
                BuatJurnal(string.Empty);
        }

        protected override void AfterSaveUpdate()
        {
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoPenerimaanKas);
            if (NoJurnal.Length > 0) BuatJurnal(NoJurnal);
        }

        protected override void AfterSaveDelete()
        {
            //if (_JenisTransaksi == enJenisPembayaran.Cek__Giro &&
            //    _StatusTransaksi == enStatusTransaksiKas.BelumKliring)
            //    return;
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoPenerimaanKas);
            if (NoJurnal.Length > 0) Jurnal.Hapus(Dp, NoJurnal);
        }

        public override bool IsVisible(string FieldName)
        {
            switch (FieldName)
            {
                case "Internal":
                    return _Internal;
                case "MultiMataUang":
                    return BaseGL.SetingPerusahaan.MultiMataUang;
                case "StatusTransaksi":
                    return _JenisTransaksi != enJenisPembayaran.Tunai;
                default:
                    return true;
            }
        }

        protected override void AfterSetDefault()
        {
            if (_StatusTransaksi == enStatusTransaksiKas._)
            {
                _TglKliring = _TglPenerimaan;
                _TglJatuhTempo = _TglPenerimaan;
            }
        }

        public override bool IsChildColumnVisible(string ChildName, string ColumnName)
        {
            switch (ColumnName)
            {
                case "IdDepartemen":
                case "KodeDepartemen":
                    return BaseGL.SetingPerusahaan.MultiDepartemen;
                case "IdProyek":
                case "KodeProyek":
                    return BaseGL.SetingPerusahaan.MultiProyek;
                default:
                    return true;
            }
        }

        public override bool AllowEdit
        {
            get
            {
                return !_Internal;
            }
        }

        public override bool AllowDelete
        {
            get
            {
                return !_Internal;
            }
        }
        #endregion

        private void BuatJurnal(string NoJurnal)
        {
            if (_StatusTransaksi == enStatusTransaksiKas._ ||
                _StatusTransaksi == enStatusTransaksiKas.SudahKliring)
            {
                Jurnal jr = new Jurnal(NoJurnal, _TglKliring,
                    _NoPenerimaanKas, ModuleName,
                    true, _Catatan, false, DateTime.MinValue);

                jr.JurnalDetil.Add(new JurnalDetil(jr,
                    string.Empty, string.Empty,
                    _IdKas, string.Empty, _TotalNilai, 0, 0, _Catatan));

                foreach (PenerimaanKasUmumDetil pkd in PenerimaanKasDetil)
                {
                    decimal Debit, Kredit;
                    JenisPenerimaanKas jpk = new JenisPenerimaanKas();
                    if (jpk.FastLoadEntity("IdAkun",
                        "JenisPenerimaan=@0",
                        new FieldParam("0", pkd.JenisPenerimaan)))
                    {
                        if (pkd.NilaiPenerimaan > 0)
                        {
                            Debit = 0;
                            Kredit = pkd.NilaiPenerimaan;
                        }
                        else
                        {
                            Debit = -pkd.NilaiPenerimaan;
                            Kredit = 0;
                        }
                        jr.JurnalDetil.Add(new JurnalDetil(jr,
                            pkd.IdDepartemen, pkd.IdProyek,
                            jpk.IdAkun, pkd.JenisPenerimaan, Debit, Kredit, 0,
                            pkd.Keterangan));
                    }
                    else
                    {
                        pkd.AddError("JenisPenerimaan",
                            "Jenis Penerimaan tidak valid");
                        return;
                    }
                }
                if (NoJurnal.Length == 0)
                    jr.SaveNew();
                else
                    jr.SaveUpdate();
            }
            else
                Jurnal.Hapus(Dp, ModuleName, _NoPenerimaanKas);
        }

        protected override void BeforeSaveNew()
        {
            if (_StatusTransaksi == enStatusTransaksiKas._)
            {
                _TglKliring = _TglPenerimaan;
                _TglJatuhTempo = _TglPenerimaan;
            }
        }

        protected override void BeforeSaveUpdate()
        {
            BeforeSaveNew();
        }

        public void Kliring(DateTime TglKliring, enStatusTransaksiKas StatusKliring)
        {
            if (_StatusTransaksi == enStatusTransaksiKas._) return;
            switch (StatusTransaksi)
            {
                case enStatusTransaksiKas._:
                    return;
                case enStatusTransaksiKas.BelumKliring:
                    _TglKliring = new DateTime(1900, 1, 1);
                    break;
                default:
                    _TglKliring = TglKliring;
                    break;
            }
            _StatusTransaksi = StatusKliring;
            SaveUpdate(true, true);
        }
        public static void Kliring(string NoPenerimaan)
        {
            throw new ApplicationException("Not Implemented yet !");
        }

        #region Static Methods
        public static void Hapus(DataPersistance Dp, string NoPenerimaanKas)
        {
            FieldParam ParamNoPenerimaanKas = new FieldParam("0", NoPenerimaanKas);
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, NoPenerimaanKas);

            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                if (NoJurnal.Length > 0) Jurnal.Hapus(Dp, NoJurnal);

                Dp.ExecuteNonQuery(
                    "DELETE FROM PenerimaanKasUmum WHERE NoPenerimaanKas=@0",
                    ParamNoPenerimaanKas);
                Dp.ExecuteNonQuery(
                    "DELETE FROM PenerimaanKasUmumDetil WHERE NoPenerimaanKas=@0",
                    ParamNoPenerimaanKas);

                tr.CommitTransaction();
            }
        }
        public static void Hapus(DataPersistance Dp, string JenisDokSumber,
            string NoDokSumber)
        {
            string NoPenerimaanKas = CariNoPenerimaanKas(Dp, 
                JenisDokSumber, NoDokSumber);
            if (NoPenerimaanKas.Length > 0)
                Hapus(Dp, NoPenerimaanKas);
        }

        public static string CariNoPenerimaanKas(DataPersistance Dp,
            string JenisDokSumber, string NoDokSumber)
        {
            return (string)Dp.Find.Value<PenerimaanKasUmum>("NoPenerimaanKas",
                "JenisDokSumber=@jds AND NoDokSumber=@nds", string.Empty,
                new FieldParam("jds", JenisDokSumber),
                new FieldParam("nds", NoDokSumber));
        }
        #endregion
    }

    [Relation(typeof(JenisPenerimaanKas))]
    public class PenerimaanKasUmumDetil : ChildEntity<PenerimaanKasUmum>
    {
        public PenerimaanKasUmumDetil() { }

        public PenerimaanKasUmumDetil(PenerimaanKasUmum Parent, string IdDepartemen,
            string IdProyek, string JenisPenerimaan, decimal NilaiPenerimaan,
            string Keterangan) : base(Parent)
        {
            _IdDepartemen = IdDepartemen;
            _IdProyek = IdProyek;
            _JenisPenerimaan = JenisPenerimaan;
            _NilaiPenerimaan = NilaiPenerimaan;
            _Keterangan = Keterangan;
        }

        [CounterKey]
        private int NoUrut;

        private string _IdDepartemen;
        [DataTypeVarChar(50, BrowseHidden = true)]
        public string IdDepartemen
        {
            get { return _IdDepartemen; }
            set
            {
                _IdDepartemen = value;
                UpdateLoadSqlFields("KodeDepartemen");
            }
        }

        private string _KodeDepartemen;
        [DataTypeLoadSql(typeof(Departemen), "IdDepartemen")]
        public string KodeDepartemen
        {
            get { return _KodeDepartemen; }
        }

        private string _IdProyek;
        [DataTypeVarChar(50, BrowseHidden = true)]
        public string IdProyek
        {
            get { return _IdProyek; }
            set
            {
                _IdProyek = value;
                UpdateLoadSqlFields("KodeProyek");
            }
        }

        private string _KodeProyek;
        [DataTypeLoadSql(typeof(Proyek), "IdProyek")]
        public string KodeProyek
        {
            get { return _KodeProyek; }
        }

        private string _JenisPenerimaan;
        [DataTypeVarChar(100), EmptyError]
        public string JenisPenerimaan
        {
            get { return _JenisPenerimaan; }
            set { _JenisPenerimaan = value; }
        }

        private decimal _NilaiPenerimaan;
        [DataTypeDecimal, EmptyError]
        public decimal NilaiPenerimaan
        {
            get { return _NilaiPenerimaan; }
            set
            {
                _NilaiPenerimaan = value;
                DataChanged();
            }
        }

        private string _Keterangan;
        [DataTypeVarChar(150)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        [DataTypeTimeStamp]
        private DateTime TglJamUpdate;
    }
}
