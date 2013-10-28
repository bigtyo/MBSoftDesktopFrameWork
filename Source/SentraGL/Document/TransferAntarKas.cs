using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;

namespace SentraGL.Document
{
    public class TransferAntarKas : DocumentEntity
    {
        public static string ModuleName = "Transfer Antar Kas";

        public AutoUpdateBindingList<Akun> ListKas;

        private string _NoTransfer;
        [AutoNumberKey("XXX.BTKYYMMCCCC", "CCCC", "TglTransfer"), DataTypeVarChar(50)]
        public string NoTransfer
        {
            get { return _NoTransfer; }
        }

        private DateTime _TglTransfer;
        [DataTypeDate, TransactionDate]
        public DateTime TglTransfer
        {
            get { return _TglTransfer; }
            set
            {
                _TglTransfer = value;
                _KursKasAsal = KursHarian.GetNilaiTukar(
                    _TglTransfer, _MataUangAsal);
                if (_MataUangAsal == _MataUangTujuan)
                    _KursKasTujuan = _KursKasAsal;
                else
                    _KursKasTujuan = KursHarian.GetNilaiTukar(
                        _TglTransfer, _MataUangTujuan);
                DataChanged();
            }
        }

        private string _IdKasAsal;
        [DataTypeVarChar(50, Default = null, BrowseHidden = true),
            EmptyError("Kas Asal tidak boleh kosong")]
        public string IdKasAsal
        {
            get { return _IdKasAsal; }
            set
            {
                _IdKasAsal = value;
                UpdateLoadSqlFields("NoKasAsal,NamaKasAsal,MataUangAsal");
                _KursKasAsal = KursHarian.GetNilaiTukar(_TglTransfer, _MataUangAsal);
            }
        }

        private string _NoKasAsal;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasAsal", "NoAkun")]
        public string NoKasAsal
        {
            get { return _NoKasAsal; }
        }

        private string _NamaKasAsal;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasAsal", "NamaAkun")]
        public string NamaKasAsal
        {
            get { return _NamaKasAsal; }
        }

        private string _MataUangAsal;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasAsal", "KodeMataUang")]
        public string MataUangAsal
        {
            get { return _MataUangAsal; }
            set { _MataUangAsal = value; }
        }

        private string _IdKasTujuan;
        [DataTypeVarChar(50, Default = null, BrowseHidden = true),
            EmptyError("Kas Tujuan tidak boleh kosong")]
        public string IdKasTujuan
        {
            get { return _IdKasTujuan; }
            set
            {
                _IdKasTujuan = value;
                UpdateLoadSqlFields("NoKasTujuan,NamaKasTujuan,MataUangTujuan");
                _KursKasTujuan = KursHarian.GetNilaiTukar(_TglTransfer, _MataUangTujuan);
            }
        }

        private string _NoKasTujuan;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasTujuan", "NoAkun")]
        public string NoKasTujuan
        {
            get { return _NoKasTujuan; }
        }

        private string _NamaKasTujuan;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasTujuan", "NamaAkun")]
        public string NamaKasTujuan
        {
            get { return _NamaKasTujuan; }
        }

        private string _MataUangTujuan;
        [DataTypeLoadSql(typeof(Akun), "IdAkun=IdKasTujuan", "KodeMataUang")]
        public string MataUangTujuan
        {
            get { return _MataUangTujuan; }
            set { _MataUangTujuan = value; }
        }

        private decimal _NilaiKasAsal;
        [DataTypeDecimal]
        public decimal NilaiKasAsal
        {
            get { return _NilaiKasAsal; }
            set
            {
                _NilaiKasAsal = value;
                if (_MataUangAsal == _MataUangTujuan)
                {
                    _NilaiKasTujuan = value;
                    DataChanged();
                }
            }
        }

        private decimal _KursKasAsal;
        [DataTypeDecimal]
        public decimal KursKasAsal
        {
            get { return _KursKasAsal; }
        }

        private decimal _NilaiKasTujuan;
        [DataTypeDecimal]
        public decimal NilaiKasTujuan
        {
            get { return _NilaiKasTujuan; }
            set
            {
                _NilaiKasTujuan = value;
                if (_MataUangAsal == _MataUangTujuan)
                {
                    _NilaiKasAsal = value;
                    DataChanged();
                }
            }
        }

        private decimal _KursKasTujuan;
        [DataTypeDecimal]
        public decimal KursKasTujuan
        {
            get { return _KursKasTujuan; }
        }

        private string _Keterangan;
        [DataTypeVarChar(50)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private bool _Internal;
        [DataTypeBoolean(Default = false)]
        public bool Internal
        {
            get { return _Internal; }
        }

        public override bool IsVisible(string FieldName)
        {
            switch (FieldName)
            {
                case "Internal":
                    return _Internal;
                case "KursKasAsal":
                case "KursKasTujuan":
                case "NilaiKasTujuan":
                case "MataUangAsal":
                case "MataUangTujuan":
                    return BaseGL.SetingPerusahaan.MultiMataUang;
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

        protected override void ValidateError()
        {
            if (_IdKasAsal == _IdKasTujuan)
                AddError("IdKasAsal", "Kas Asal tidak boleh sama dengan Kas Tujuan");
        }

        protected override void AfterSaveNew()
        {
            BuatJurnal(string.Empty);
        }

        protected override void AfterSaveUpdate()
        {
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoTransfer);
            if (NoJurnal.Length > 0) BuatJurnal(NoJurnal);
        }

        protected override void AfterSaveDelete()
        {
            string NoJurnal = Jurnal.CariNoJurnal(Dp, ModuleName, _NoTransfer);
            if (NoJurnal.Length > 0) Jurnal.Hapus(Dp, NoJurnal);
        }

        private void BuatJurnal(string NoJurnal)
        {
            Jurnal Jr = new Jurnal(NoJurnal, _TglTransfer, _NoTransfer,
                ModuleName, true, _Keterangan, false, DateTime.MinValue);

            Jr.JurnalDetil.Add(new JurnalDetil(Jr, string.Empty,
                string.Empty, _IdKasTujuan, string.Empty, _NilaiKasTujuan,
                0, _KursKasTujuan, _Keterangan));

            if (_NilaiKasAsal != _NilaiKasTujuan)
                AddError("NilaiKasAsal", "Mata Uang Kas Asal dan Kas Tujuan harus sama !");
                //Jr.JurnalDetil.Add(new JurnalDetil(Jr, string.Empty,
                //    string.Empty, _IdKasAsal, string.Empty, _NilaiKasAsal,
                //    0, _NilaiKasAsal * _KursKasAsal, _Keterangan));

            Jr.JurnalDetil.Add(new JurnalDetil(Jr, string.Empty,
                string.Empty, _IdKasAsal, string.Empty, 0, _NilaiKasAsal,
                _KursKasAsal, _Keterangan));

            if (NoJurnal.Length == 0)
                Jr.SaveNew();
            else
                Jr.SaveUpdate();
        }

        protected override void InitUI()
        {
            ListKas = FastLoadEntities<Akun>("IdAkun,NoAkun,NamaAkun",
                "KelompokAkun=" + FormatSqlValue(enKelompokAkun.Kas__Bank),
                "NoAkun", true);
        }

        protected override void EndUI()
        {
            ListKas.Close();
        }

        protected override string GetAutoNumberTemplate()
        {
            return BaseGL.SetingPerusahaan.KodePerusahaan + ".BTKYYMMCCCC";
        }
    }
}
