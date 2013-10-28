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
    [Relation(typeof(Akun), "IdAkun", "IdKas")]
    [Index("TglKliring", false)]
    public class PengeluaranKasUmum : DocumentEntity
    {
        public static string ModuleName = "Pengeluaran Kas Umum";

        #region Class Contructor
        public PengeluaranKasUmum() { }

        public PengeluaranKasUmum(string NoPengeluaranKas, string IdAkunKas,
            string NoKuitansi, DateTime TglPengeluaran,
            string NamaPenerima, string Keperluan, string Catatan,
            string JenisDokSumber, string NoDokSumber,
            enJenisPembayaran JenisTransaksi, string NoCekGiro,
            DateTime TglJatuhTempo, bool Internal)
        {
            _NoPengeluaranKas = NoPengeluaranKas;
            _IdKas = IdAkunKas;
            _NoKuitansi = NoKuitansi;
            _TglPengeluaran = TglPengeluaran;
            _NamaPenerima = NamaPenerima;
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
        public AutoUpdateBindingList<JenisPengeluaranKas> ListJenisPengeluaran;
        public AutoUpdateBindingList<Akun> ListKas;
        public AutoUpdateBindingList<Proyek> ListProyek;
        public AutoUpdateBindingList<Departemen> ListDepartemen;
        #endregion

        #region Class Attribute
        private string _NoPengeluaranKas;
        [AutoNumberKey("XXX.BKKYYMMCCCC", "CCCC", "TglPengeluaran"), DataTypeVarChar(50)]
        public string NoPengeluaranKas
        {
            get { return _NoPengeluaranKas; }
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

        private DateTime _TglPengeluaran;
        [DataTypeDate, TransactionDate]
        public DateTime TglPengeluaran
        {
            get { return _TglPengeluaran; }
            set { _TglPengeluaran = value; }
        }

        private string _NamaPenerima;
        [DataTypeVarChar(50), EmptyError]
        public string NamaPenerima
        {
            get { return _NamaPenerima; }
            set { _NamaPenerima = value; }
        }

        private string _Keperluan;
        [DataTypeVarChar(150), EmptyError]
        public string Keperluan
        {
            get { return _Keperluan; }
            set { _Keperluan = value; }
        }

        private string _Catatan;
        [DataTypeVarChar(200)]
        public string Catatan
        {
            get { return _Catatan; }
            set { _Catatan = value; }
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
                    _TglKliring = _TglPengeluaran;
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
        [DataTypeBoolean(Default = false)]
        public bool Internal
        {
            get { return _Internal; }
        }

        public EntityCollection<PengeluaranKasUmumDetil> PengeluaranKasDetil;
        #endregion

        #region Override Methods
        protected override string GetAutoNumberTemplate()
        {
            return BaseGL.SetingPerusahaan.KodePerusahaan + ".BKKYYMMCCCC";
        }

        protected override void InitUI()
        {
            ListKas = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun",
                "Aktif<>0 AND KelompokAkun=" +
                FormatSqlValue(enKelompokAkun.Kas__Bank),
                "NoAkun", true);
            ListJenisPengeluaran =
                FastLoadEntities<JenisPengeluaranKas>(
                "JenisPengeluaran", "Aktif<>0",
                "JenisPengeluaran", true);
            ListDepartemen = FastLoadEntities<Departemen>
                ("IdDepartemen,KodeDepartemen,NamaDepartemen",
                "Posting<>0 AND Aktif<>0", "KodeDepartemen",
                true, true);
            ListProyek = FastLoadEntities<Proyek>
                ("IdProyek,KodeProyek,NamaProyek",
                "Posting<>0 AND Aktif<>0", "KodeProyek",
                true, true);
            _IdKas = Dp.GetVariable<string>(
                "PengeluaranKasUmumVar", "IdKas", string.Empty);
        }

        protected override void EndUI()
        {
            ListKas.Close();
            ListJenisPengeluaran.Close();
            ListDepartemen.Close();
            ListProyek.Close();
            Dp.SetVariable("PengeluaranKasUmumVar",
                "IdKas", _IdKas);
        }

        protected override void ValidateError()
        {
            if (PengeluaranKasDetil.Count == 0)
                AddError("NoPengeluaranKas",
                    "Detil Pengeluaran Kas tidak boleh kosong");
            if (_TotalNilai < 0)
                AddError("TotalNilai",
                    "Total Nilai harus lebih besar atau sama dengan Nol");
        }

        protected override void onChildDataChanged(string ChildName, BusinessEntity ChildObject)
        {
            _TotalNilai = PengeluaranKasDetil.Sum("NilaiPengeluaran");
            DataChanged();
        }

        protected override void BeforeSaveNew()
        {
            if (_StatusTransaksi == enStatusTransaksiKas._)
            {
                _TglKliring = _TglPengeluaran;
                _TglJatuhTempo = _TglPengeluaran;
            }
        }

        protected override void BeforeSaveUpdate()
        {
            BeforeSaveNew();
        }

        protected override void AfterSaveNew()
        {
            if (_StatusTransaksi == enStatusTransaksiKas._)
                BuatJurnal(string.Empty);
        }

        protected override void AfterSaveUpdate()
        {
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoPengeluaranKas);
            if (NoJurnal.Length > 0) BuatJurnal(NoJurnal);
        }

        protected override void AfterSaveDelete()
        {
            //if (_JenisTransaksi == enJenisPembayaran.Cek__Giro &&
            //    _StatusTransaksi == enStatusTransaksiKas.BelumKliring)
            //    return;
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoPengeluaranKas);
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
                _TglKliring = _TglPengeluaran;
                _TglJatuhTempo = _TglPengeluaran;
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
                    _NoPengeluaranKas, ModuleName,
                    true, _Catatan, false, DateTime.MinValue);

                foreach (PengeluaranKasUmumDetil pkd in PengeluaranKasDetil)
                {
                    decimal Debit, Kredit; 
                    
                    JenisPengeluaranKas jpk = new JenisPengeluaranKas();
                    if (jpk.FastLoadEntity("IdAkun",
                        "JenisPengeluaran=@0",
                        new FieldParam("0", pkd.JenisPengeluaran)))
                    {
                        if (pkd.NilaiPengeluaran > 0)
                        {
                            Debit = pkd.NilaiPengeluaran;
                            Kredit = 0;
                        }
                        else
                        {
                            Debit = 0;
                            Kredit = -pkd.NilaiPengeluaran;
                        }
                        jr.JurnalDetil.Add(new JurnalDetil(jr,
                            pkd.IdDepartemen, pkd.IdProyek,
                            jpk.IdAkun, pkd.JenisPengeluaran, Debit, Kredit, 0,
                            pkd.Keterangan));
                    }
                    else
                    {
                        pkd.AddError("JenisPengeluaran",
                            "Jenis Pengeluaran tidak valid");
                        return;
                    }
                }

                jr.JurnalDetil.Add(new JurnalDetil(jr,
                    string.Empty, string.Empty,
                    _IdKas, string.Empty, 0, _TotalNilai, 0, _Catatan));
                if (NoJurnal.Length == 0)
                    jr.SaveNew();
                else
                    jr.SaveUpdate();
            }
            else
                Jurnal.Hapus(Dp, ModuleName, _NoPengeluaranKas);
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
        public static void Kliring(string NoPengeluaran)
        {
            throw new ApplicationException("Not Implemented yet !");
        }

        #region Static Methods
        public static void Hapus(DataPersistance Dp, string NoPengeluaranKas)
        {
            FieldParam ParamNoPengeluaranKas = new FieldParam("0", NoPengeluaranKas);
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, NoPengeluaranKas);

            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                if (NoJurnal.Length > 0) Jurnal.Hapus(Dp, NoJurnal);

                Dp.ExecuteNonQuery(
                    "DELETE FROM PengeluaranKasUmum WHERE NoPengeluaranKas=@0",
                    ParamNoPengeluaranKas);
                Dp.ExecuteNonQuery(
                    "DELETE FROM PengeluaranKasUmumDetil WHERE NoPengeluaranKas=@0",
                    ParamNoPengeluaranKas);

                tr.CommitTransaction();
            }
        }
        public static void Hapus(DataPersistance Dp, string JenisDokSumber,
            string NoDokSumber)
        {
            string NoPengeluaranKas = CariNoPengeluaranKas(Dp,
                JenisDokSumber, NoDokSumber);
            if (NoPengeluaranKas.Length > 0)
                Hapus(Dp, NoPengeluaranKas);
        }

        public static string CariNoPengeluaranKas(DataPersistance Dp,
            string JenisDokSumber, string NoDokSumber)
        {
            return (string)Dp.Find.Value<PengeluaranKasUmum>("NoPengeluaranKas",
                "JenisDokSumber=@jds AND NoDokSumber=@nds", string.Empty,
                new FieldParam("jds", JenisDokSumber),
                new FieldParam("nds", NoDokSumber));
        }
        #endregion
    }

    [Relation(typeof(JenisPengeluaranKas))]
    public class PengeluaranKasUmumDetil : ChildEntity<PengeluaranKasUmum>
    {
        public PengeluaranKasUmumDetil() { }

        public PengeluaranKasUmumDetil(PengeluaranKasUmum Parent, string IdDepartemen,
            string IdProyek, string JenisPengeluaran, decimal NilaiPengeluaran,
            string Keterangan)
            : base(Parent)
        {
            _IdDepartemen = IdDepartemen;
            _IdProyek = IdProyek;
            _JenisPengeluaran = JenisPengeluaran;
            _NilaiPengeluaran = NilaiPengeluaran;
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

        private string _JenisPengeluaran;
        [DataTypeVarChar(100), EmptyError]
        public string JenisPengeluaran
        {
            get { return _JenisPengeluaran; }
            set { _JenisPengeluaran = value; }
        }

        private decimal _NilaiPengeluaran;
        [DataTypeDecimal, EmptyError]
        public decimal NilaiPengeluaran
        {
            get { return _NilaiPengeluaran; }
            set
            {
                _NilaiPengeluaran = value;
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
    }
}
