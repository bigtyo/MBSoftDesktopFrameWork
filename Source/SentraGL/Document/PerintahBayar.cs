using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSecurity;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using System.Collections;

namespace SentraGL.Document
{
    public enum enFormPerintahBayar
    {
        PerintahBayar,
        PengeluaranUang
    }

    public enum enStatusPerintahBayar
    {
        BelumDibayar,
        SudahDibayar
    }

    [Relation(typeof(Akun), "IdAkun", "IdKas")]
    public class PerintahBayar : DocumentEntity
    {
        private static string ModuleName = "Perintah Bayar";

        #region Class Contructor
        public PerintahBayar() { }

        public PerintahBayar(string NoPerintahBayar, string IdAkunKas,
            DateTime TglPerintahBayar, string NamaPenerima, 
            string Keperluan, string Catatan, bool Internal)
        {
            _NoPerintahBayar = NoPerintahBayar;
            _IdKas = IdAkunKas;
            _TglPerintahBayar = TglPerintahBayar;
            _NamaPenerima = NamaPenerima;
            _Keperluan = Keperluan;
            _Catatan = Catatan;
            _Internal = Internal;
        }
        #endregion

        #region AutoUpdateBindingList
        public AutoUpdateBindingList<JenisPengeluaranKas> ListJenisPengeluaran;
        public AutoUpdateBindingList<Akun> ListKas;
        public AutoUpdateBindingList<Proyek> ListProyek;
        public AutoUpdateBindingList<Departemen> ListDepartemen;
        #endregion

        public enFormPerintahBayar FormMode;

        #region Class Attribute Perintah Bayar
        private string _NoPerintahBayar;
        [AutoNumberKey("XXX.PBRYYMMCCCC", "CCCC", "TglPerintahBayar"), DataTypeVarChar(50)]
        public string NoPerintahBayar
        {
            get { return _NoPerintahBayar; }
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

        private DateTime _TglPerintahBayar;
        [DataTypeDate, TransactionDate]
        public DateTime TglPerintahBayar
        {
            get { return _TglPerintahBayar; }
            set { _TglPerintahBayar = value; }
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

        private bool _Internal;
        [DataTypeBoolean(Default = false)]
        public bool Internal
        {
            get { return _Internal; }
        }

        private enStatusPerintahBayar _Status;
        [DataTypeVarChar(15, Default = enStatusPerintahBayar.BelumDibayar)]
        public enStatusPerintahBayar Status
        {
            get { return _Status; }
        }
        
        public EntityCollection<PerintahBayarDetil> PerintahBayarDetil;
        #endregion

        #region Class Attribute Pengeluaran Perintah Bayar
        private string _NoKuitansi;
        [DataTypeVarChar(50)]
        public string NoKuitansi
        {
            get { return _NoKuitansi; }
            set { _NoKuitansi = value; }
        }

        private DateTime _TglPengeluaran;
        [DataTypeDate]
        public DateTime TglPengeluaran
        {
            get { return _TglPengeluaran; }
            set { _TglPengeluaran = value; }
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
        #endregion

        #region Override Methods
        protected override void BeforeSaveNew()
        {
            if (_Status == enStatusPerintahBayar.BelumDibayar)
            {
                _TglPengeluaran = new DateTime(1900, 1, 1);
                _TglJatuhTempo = _TglPengeluaran;
                _TglKliring = _TglPengeluaran;
            }
        }

        protected override string GetAutoNumberTemplate()
        {
            return string.Concat(BaseGL.SetingPerusahaan
                .KodePerusahaan, ".", "PBRYYMMCCCC");
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
                "PerintahBayarVar", "IdKas", string.Empty);
        }

        protected override void EndUI()
        {
            ListKas.Close();
            ListJenisPengeluaran.Close();
            ListDepartemen.Close();
            ListProyek.Close();
            Dp.SetVariable("PerintahBayarVar",
                "IdKas", _IdKas);
        }

        protected override void AfterInitNavigator(IBaseUINavigator Navigator)
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang)
                AutoFormMode = true;
        }

        protected override void ValidateError()
        {
            if (PerintahBayarDetil.Count == 0)
                AddError("NoPengeluaranKas",
                    "Detil Pengeluaran Kas tidak boleh kosong");
            if (_TotalNilai < 0)
                AddError("TotalNilai",
                    "Total Nilai harus lebih besar atau sama dengan Nol");
        }

        protected override void onChildDataChanged(string ChildName, BusinessEntity ChildObject)
        {
            _TotalNilai = PerintahBayarDetil.Sum("NilaiPengeluaran");
            DataChanged();
        }

        public override bool IsVisible(string FieldName)
        {
            switch (FieldName)
            {
                case "Internal":
                    return _Internal;
                case "MultiMataUang":
                    return BaseGL.SetingPerusahaan.MultiMataUang;
                case "NoKuitansi":
                case "TglPengeluaran":
                case "JenisTransaksi":
                case "NoCekGiro":
                case "TglJatuhTempo":
                case "TglKliring":
                    return FormMode == enFormPerintahBayar.PengeluaranUang;
                case "StatusTransaksi":
                    return _JenisTransaksi != enJenisPembayaran.Tunai &&
                        FormMode == enFormPerintahBayar.PengeluaranUang;
                default:
                    return true;
            }
        }

        public override bool IsChildReadOnly(string ChildName)
        {
            return FormMode == enFormPerintahBayar.PengeluaranUang;
        }

        public override bool IsReadOnly(string FieldName)
        {
            switch (FieldName)
            {
                case "IdKas":
                case "TglPerintahBayar":
                case "NamaPenerima":
                case "Keperluan":
                    return FormMode == enFormPerintahBayar.PengeluaranUang;
                default:
                    return false;
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
                switch (FormMode)
                {
                    case enFormPerintahBayar.PerintahBayar:
                        return !Internal &&
                            _Status == enStatusPerintahBayar.BelumDibayar;
                    default:  //enFormPerintahBayar.PengeluaranUang:
                        return _Status == enStatusPerintahBayar.BelumDibayar;
                }
            }
        }

        public override bool AllowDelete
        {
            get
            {
                switch (FormMode)
                {
                    case enFormPerintahBayar.PerintahBayar:
                        return !Internal && _Status == enStatusPerintahBayar.BelumDibayar;
                    default:    //enFormPerintahBayar.PengeluaranUang:
                        return _Status == enStatusPerintahBayar.SudahDibayar;
                }
            }
        }

        protected override string GetBrowseFilter()
        {
            switch (FormMode)
            {
                case enFormPerintahBayar.PerintahBayar:
                    return string.Empty;
                default:    //enFormPerintahBayar.PengeluaranUang:
                    return "Status=" + 
                        Dp.FormatSqlValue(enStatusPerintahBayar.SudahDibayar);
            }
        }

        protected override string GetFieldTransactionDate()
        {
            if (FormMode == enFormPerintahBayar.PerintahBayar)
                return "TglPerintahBayar";
            else
                return "TglPengeluaran";
        }

        protected override void BeforeSaveUpdate()
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang)
            {
                if (_TglPengeluaran < _TglPerintahBayar)
                    AddError("TglPengeluaran",
                        "Tgl Pengeluaran harus >= Tgl Perintah Bayar");
                if (_TglPengeluaran < BaseFramework.TransDate.MinDate)
                    AddError("TglPengeluaran",
                        "Tgl Pengeluaran lebih kecil dari Tgl Transaksi Minimal boleh dilakukan");

                if (!IsErrorExist())
                    _Status = enStatusPerintahBayar.SudahDibayar;
            }
        }

        protected override void AfterSaveUpdate()
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang)
            {
                PengeluaranKasUmum pk = new PengeluaranKasUmum(string.Empty,
                    _IdKas, _NoKuitansi, _TglPengeluaran, _NamaPenerima,
                    _Keperluan, _Catatan, ModuleName, _NoPerintahBayar,
                    _JenisTransaksi, _NoCekGiro, _TglJatuhTempo, true);
                foreach (PerintahBayarDetil pbd in PerintahBayarDetil)
                    pk.PengeluaranKasDetil.Add(new PengeluaranKasUmumDetil(
                        pk, pbd.IdDepartemen, pbd.IdProyek, pbd.JenisPengeluaran,
                        pbd.NilaiPengeluaran, pbd.Keterangan));
                pk.SaveNew();
            }
        }

        public override int SaveDelete(DataPersistance Dp, bool CallDeleteRule)
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang)
            {
                _Status = enStatusPerintahBayar.BelumDibayar;
                base.SaveUpdate(Dp, false, false);

                PengeluaranKasUmum.Hapus(Dp, ModuleName, _NoPerintahBayar);
                return 1;
            } 
            else
                return base.SaveDelete(Dp, CallDeleteRule);
        }

        protected override void BeforeSaveDelete()
        {
            if (_Status == enStatusPerintahBayar.SudahDibayar)
                AddError("NoPerintahBayar", "Tidak dapat menghapus Perintah Bayar apabila Uang sudah dikeluarkan");
        }

        protected override void AfterSetDefault()
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang)
            {
                _TglPerintahBayar = new DateTime(1900, 1, 1);
                _IdKas = string.Empty;
                _NamaKas = string.Empty;
            }
        }

        protected override void AfterLoadFound()
        {
            if (FormMode == enFormPerintahBayar.PengeluaranUang &&
                _TglPengeluaran.Year == 1900)
                _TglPengeluaran = DateTime.Today;
        }
        #endregion

        public void PilihPerintahBayar()
        {
            PerintahBayar pb = ChooseSingleEntity<PerintahBayar>(
                "Status=@0 AND TglPerintahBayar<=@1",
                "NoPerintahBayar", false, "Pilih Perintah Bayar",
                new EntityColumnShow(
                "NoPerintahBayar,NamaPenerima,Keperluan,TotalNilai,TglPerintahBayar"),
                new FieldParam("0", enStatusPerintahBayar.BelumDibayar),
                new FieldParam("1", _TglPengeluaran));
            if (pb != null)
            {
                _NoPerintahBayar = pb._NoPerintahBayar;
                LoadEntity();
            }
        }
    }

    [Relation(typeof(JenisPengeluaranKas))]
    public class PerintahBayarDetil : ChildEntity<PerintahBayar>
    {
        public PerintahBayarDetil() { }

        public PerintahBayarDetil(PerintahBayar Parent, string IdDepartemen,
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
