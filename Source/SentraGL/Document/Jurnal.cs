using System;
using System.Collections.Generic;
using System.Text;
using SentraSecurity;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSolutionFramework;
using SentraGL;
using SentraSolutionFramework.Persistance;
using SentraGL.Report;

namespace SentraGL.Document
{
    [Index("TglJurnal", false)]
    [EnableCancelEntity]
    [Index("Keterangan DESC", false)]
    public class Jurnal : DocumentEntity
    {
        #region AutoUpdateBindingList
        public AutoUpdateBindingList<Akun> ListAkun;
        public AutoUpdateBindingList<MasterAturanJurnal> ListAturan;
        public AutoUpdateBindingList<JenisDokSumberJurnal> ListJenisDokSumber;
        public AutoUpdateBindingList<MataUang> ListMataUang;
        public AutoUpdateBindingList<Proyek> ListProyek;
        public AutoUpdateBindingList<Departemen> ListDepartemen;
        #endregion

        #region Class Contructor
        public Jurnal()
        {
            CheckTransDate = false;
        }

        public Jurnal(string NoJurnal, DateTime TglJurnal, string NoDokSumber, string JenisDokSumber,
            bool Internal, string Keterangan, bool BuatJurnalPembalik,
            DateTime TglDibalik)
        {
            _NoJurnal = NoJurnal;
            _TglJurnal = TglJurnal;
            _NoDokSumber = NoDokSumber;
            _JenisDokSumber = JenisDokSumber;
            _Internal = Internal;
            _Keterangan = Keterangan;
            this.BuatJurnalPembalik = BuatJurnalPembalik;
            if (_BuatJurnalPembalik) _TglDibalik = TglDibalik;
            CheckTransDate = false;
        }
        #endregion

        #region Class Attribute
        private string _NoJurnal;
        [AutoNumberKey("XXX.JRYYMMCCCC", "CCCC", "TglJurnal"),
        DataTypeVarChar(50)]
        public string NoJurnal
        {
            get { return _NoJurnal; }
        }

        private DateTime _TglJurnal;
        [DataTypeDate, TransactionDate]
        public DateTime TglJurnal
        {
            get { return _TglJurnal; }
            set { _TglJurnal = value; }
        }

        private string _AturanJurnal = string.Empty;
        public string AturanJurnal
        {
            get { return _AturanJurnal; }
            set
            {
                if (_Internal) return;
                _AturanJurnal = value;
                MasterAturanJurnal Aj = new MasterAturanJurnal();
                Aj.LoadEntity("AturanJurnal=@0", false,
                    new FieldParam("0", _AturanJurnal));

                JurnalDetil.Clear();
                foreach (AturanJurnalDetil Ajd in Aj.AturanJurnalDetil)
                    JurnalDetil.Add(new JurnalDetil(this, Ajd));
                JurnalDetil.Refresh();
                AfterLoadFound();
                DataChanged();
            }
        }

        private string _NoDokSumber;
        [DataTypeVarChar(50)]
        [Description("No Dokumen Sumber Jurnal")]
        public string NoDokSumber
        {
            get { return _NoDokSumber; }
            set { _NoDokSumber = value; }
        }

        private string _JenisDokSumber;
        [DataTypeVarChar(50, Default=null), EmptyError]
        public string JenisDokSumber
        {
            get { return _JenisDokSumber; }
            set { _JenisDokSumber = value; }
        }
        
        private bool _Internal;
        [DataTypeBoolean(Default = false)]
        public bool Internal
        {
            get { return _Internal; }
        }

        private bool _MultiMataUang;
        public bool MultiMataUang
        {
            get { return _MultiMataUang; }
            set
            {
                _MultiMataUang = value;
                FormChanged();
            }
        }

        private string _Keterangan;
        [DataTypeVarChar(1000)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private bool _BuatJurnalPembalik;
        [DataTypeBoolean(Default = false)]
        public bool BuatJurnalPembalik
        {
            get { return _BuatJurnalPembalik; }
            set
            {
                _BuatJurnalPembalik = value;
                if (value)
                    _TglDibalik = new DateTime(_TglJurnal.AddMonths(1).Year,
                        _TglJurnal.AddMonths(1).Month, 1);
                else
                    _TglDibalik = new DateTime(1900, 1, 1);
                DataChanged();
            }
        }

        private DateTime _TglDibalik;
        [DataTypeDate(Default = "01/01/1900")]
        public DateTime TglDibalik
        {
            get { return _TglDibalik; }
            set { _TglDibalik = value; }
        }

        private string _NoJurnalPembalik;
        [DataTypeVarChar(50)]
        public string NoJurnalPembalik
        {
            get { return _NoJurnalPembalik; }
        }

        public EntityCollection<JurnalDetil> JurnalDetil;
        #endregion

        private int _CetakKe;
        [DataTypeInteger, PrintCounter]
        public int CetakKe
        {
            get { return _CetakKe; }
        }

        #region Override Methods

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun",
              "Aktif<>0 AND Posting<>0", "NoAkun", true);
            ListMataUang = FastLoadEntities<MataUang>(
                "KodeMataUang,NamaMataUang",
                "Aktif<>0", "KodeMataUang", true);
            ListDepartemen = FastLoadEntities<Departemen>(
                "IdDepartemen,KodeDepartemen,NamaDepartemen",
                "Posting<>0 AND Aktif<>0", "KodeDepartemen",
                true, true);
            ListProyek = FastLoadEntities<Proyek>(
                "IdProyek,KodeProyek,NamaProyek",
                "Posting<>0 AND Aktif<>0", "KodeProyek", 
                true, true);
            ListAturan = FastLoadEntities<MasterAturanJurnal>(
                "AturanJurnal", "Aktif<>0", "AturanJurnal", 
                true, true);
            ListJenisDokSumber = FastLoadEntities<JenisDokSumberJurnal>(
                "JenisDokSumber", "Aktif<>0 AND Otomatis=0", 
                "JenisDokSumber", true);
            _JenisDokSumber = Dp.GetVariable<string>(
                "JurnalVar", "JenisDokSumber", string.Empty);
            LastJenisDokSumber = _JenisDokSumber;
            CheckTransDate = true;
        }

        private static string LastJenisDokSumber;
        protected override void EndUI()
        {
            ListAkun.Close();
            ListMataUang.Close();
            ListDepartemen.Close();
            ListProyek.Close();
            ListAturan.Close();
            ListJenisDokSumber.Close();
            Dp.SetVariable("JurnalVar", "JenisDokSumber", LastJenisDokSumber);
        }

        public override bool IsChildColumnVisible(string ChildName, string ColumnName)
        {
            switch (ColumnName)
            {
                case "KodeMataUang":
                case "DebitKurs":
                case "KreditKurs":
                case "Kurs":
                    return _MultiMataUang;
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

        protected override void ValidateError()
        {
            decimal TotalDebit = 0;
            decimal TotalKredit = 0;

            if (!_BuatJurnalPembalik)
                _TglDibalik = new DateTime(1900, 1, 1);
            if (_TglDibalik <= _TglJurnal && _BuatJurnalPembalik)
                AddError("TglDibalik", 
                    "Tgl Dibalik tidak boleh kurang dari atau sama dengan tanggal jurnal");

            foreach (JurnalDetil Jd in JurnalDetil)
            {
                if (Jd.Debit == Jd.Kredit)
                    Jd.AddError("Debit", 
                        "Debit dan Kredit tidak boleh kosong");
                TotalDebit += Jd.Debit;
                TotalKredit += Jd.Kredit;
            }
            if (TotalDebit != TotalKredit)
                AddError("NoJurnal", 
                    "Total Debit Harus sama dengan Total Kredit");
            else if (TotalDebit == 0)
                AddError("NoJurnal", 
                    "Total Debit/ Kredit tidak boleh nol");
        }

        protected override void AfterSaveDelete()
        {
            Jurnal Jr = GetOriginal<Jurnal>();
            if (Jr == null || IsErrorExist()) return;

            if (Jr._BuatJurnalPembalik &&
                Jr._NoJurnalPembalik.Length > 0)
            {
                FieldParam Param1 = new FieldParam("0",
                    Jr._NoJurnalPembalik);
                ExecuteNonQuery(
                    "DELETE FROM Jurnal WHERE NoJurnal=@0",
                    Param1);
                ExecuteNonQuery(
                    "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                    Param1);
            }
            if (Jr._TglDibalik < Jr._TglJurnal)
                BaseGL.RingkasanAkun.Hapus(Jr._TglDibalik);
            else
                BaseGL.RingkasanAkun.Hapus(Jr._TglJurnal);
        }

        protected override void AfterSaveUpdate()
        {
            if (ListAkun == null || IsErrorExist()) return;

            Jurnal OrigJurnal = GetOriginal<Jurnal>();
            if (OrigJurnal == null) return;

            if (OrigJurnal._NoJurnalPembalik.Length > 0)
            {
                FieldParam Param1 = new FieldParam("0",
                    OrigJurnal._NoJurnalPembalik);
                ExecuteNonQuery(
                    "DELETE FROM Jurnal WHERE NoJurnal=@0",
                    Param1);
                ExecuteNonQuery(
                   "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                   Param1);
            }
            if (_BuatJurnalPembalik)
            {
                Jurnal JurnalBalik = (Jurnal)MetaData.Clone(
                    this);
                if (OrigJurnal._NoJurnalPembalik.Length > 0)
                    JurnalBalik._NoJurnal =
                        OrigJurnal._NoJurnalPembalik;

                JurnalBalik.BuatJurnalPembalik = false;
                JurnalBalik._Internal = true;
                JurnalBalik._NoJurnal = string.Empty;
                JurnalBalik._NoJurnalPembalik = string.Empty;
                JurnalBalik._NoDokSumber = _NoJurnal;
                JurnalBalik.JenisDokSumber = "Jurnal Pembalik";
                JurnalBalik._TglJurnal = _TglDibalik;
                foreach (JurnalDetil Jd in JurnalBalik.JurnalDetil)
                {
                    Jd.Debit = -Jd.Debit;
                    Jd.Kredit = -Jd.Kredit;
                }
                JurnalBalik.SaveNew(true, false);
                _NoJurnalPembalik = JurnalBalik._NoJurnal;
                ExecuteNonQuery(
                    "UPDATE Jurnal SET NoJurnalPembalik=@0 WHERE NoJurnal=@1",
                    new FieldParam("0", _NoJurnalPembalik),
                    new FieldParam("1", _NoJurnal));
            }
            DateTime Tmp = OrigJurnal._TglJurnal;
            if (Tmp > _TglJurnal) Tmp = _TglJurnal;
            BaseGL.RingkasanAkun.Hapus(Tmp);
            _AturanJurnal = string.Empty;
        }

        protected override void AfterSaveNew()
        {
            if (IsErrorExist()) return;

            if (_BuatJurnalPembalik)
            {
                Jurnal JurnalBalik = (Jurnal)MetaData.Clone(this);
                JurnalBalik.BuatJurnalPembalik = false;
                JurnalBalik._Internal = true;
                JurnalBalik._NoJurnal = string.Empty;
                JurnalBalik._NoJurnalPembalik = string.Empty;
                JurnalBalik._NoDokSumber = _NoJurnal;
                JurnalBalik.JenisDokSumber = "Jurnal Pembalik";
                JurnalBalik._TglJurnal = _TglDibalik;
                foreach (JurnalDetil Jd in JurnalBalik.JurnalDetil)
                {
                    Jd.DebitKurs = -Jd.DebitKurs;
                    Jd.KreditKurs = -Jd.KreditKurs;
                }
                JurnalBalik.SaveNew(true, false);
                _NoJurnalPembalik = JurnalBalik._NoJurnal;
                ExecuteNonQuery(
                    "UPDATE Jurnal SET NoJurnalPembalik=@0 WHERE NoJurnal=@1",
                    new FieldParam("0", _NoJurnalPembalik),
                    new FieldParam("1", _NoJurnal));
            }
            BaseGL.RingkasanAkun.Hapus(_TglJurnal);
            _AturanJurnal = string.Empty;
            LastJenisDokSumber = _JenisDokSumber;
        }

        protected override void onChildDataChanged(string ChildName,
            BusinessEntity ChildObject)
        {
            if (ChildObject == null || ListAkun == null ||
                JurnalDetil.Count == 0 ||
                object.ReferenceEquals(ChildObject, JurnalDetil[0]))
                return;

            JurnalDetil Jd = (JurnalDetil)ChildObject;
            JurnalDetil Jd0 = JurnalDetil[0];
            if (Jd.Debit == 1 || Jd.Kredit == 1 ||
                (Jd.Debit == 0 && Jd.Kredit == 0))
            {
                Jd.Keterangan = JurnalDetil[0].Keterangan;
                decimal Jumlah = 0;
                foreach (JurnalDetil Jd1 in JurnalDetil)
                {
                    if (!object.ReferenceEquals(Jd1, Jd))
                        Jumlah += (Jd1.Debit - Jd1.Kredit);
                }
                if (Jumlah > 0)
                    Jd.Kredit = Jumlah;
                else
                    Jd.Debit = -Jumlah;
            }
        }

        public override bool IsVisible(string FieldName)
        {
            switch (FieldName)
            {
                case "Internal":
                    return _Internal;
                case "MultiMataUang":
                    return BaseGL.SetingPerusahaan.MultiMataUang;
                default:
                    return true;
            }
        }

        public override bool IsReadOnly(string FieldName)
        {
            return _Internal;
        }

        public override bool IsChildReadOnly(string ChildName)
        {
            return _Internal;
        }

        protected override string GetAutoNumberTemplate()
        {
            return BaseGL.SetingPerusahaan.KodePerusahaan + ".JRYYMMCCCC";
        }

        protected override void AfterLoadFound()
        {
            if (!MultiMataUang) return;

            _AturanJurnal = string.Empty;
            foreach (JurnalDetil jd in JurnalDetil)
            {
                if (jd.Kurs != 1)
                {
                    MultiMataUang = true;
                    return;
                }
            }
            MultiMataUang = false;
        }
        
        public override bool AllowDelete
        {
            get
            {
                return !_Internal;
            }
        }

        public override bool AllowEdit
        {
            get
            {
                return !_Internal;
            }
        }
        protected override void BeforeSaveNew()
        {
            if (ListAkun != null && _TglJurnal < 
                BaseGL.SetingPerusahaan.TglMulaiSistemBaru)
                AddError("TglJurnal", 
                    "Tidak boleh menjurnal dengan Tgl Kurang dari Tgl Mulai Sistem Baru");
        }

        protected override void BeforeSaveUpdate()
        {
            if (ListAkun != null && (_TglJurnal <
                BaseGL.SetingPerusahaan.TglMulaiSistemBaru || 
                GetOriginal<Jurnal>().TglJurnal < 
                BaseGL.SetingPerusahaan.TglMulaiSistemBaru))
                AddError("TglJurnal",
                    "Tidak boleh mengubah jurnal dengan Tgl Kurang dari Tgl Mulai Sistem Baru");
        }

        protected override void BeforeSaveDelete()
        {
            if (ListAkun != null && (_TglJurnal <
                BaseGL.SetingPerusahaan.TglMulaiSistemBaru ||
                GetOriginal<Jurnal>().TglJurnal <
                BaseGL.SetingPerusahaan.TglMulaiSistemBaru))
                AddError("TglJurnal",
                    "Tidak boleh menghapus jurnal dengan Tgl Kurang dari Tgl Mulai Sistem Baru");
        }

        public override int SaveNew(DataPersistance Dp, 
            bool CallSaveRule, bool CallValidateError)
        {
            if (ListAkun == null && _TglJurnal < BaseGL.SetingPerusahaan
                .TglMulaiSistemBaru)
                return 0;
            return base.SaveNew(Dp, CallSaveRule, CallValidateError);
        }

        public override int SaveUpdate(DataPersistance Dp, 
            bool CallSaveRule, bool CallValidateError)
        {
            if (ListAkun == null && _TglJurnal < BaseGL.SetingPerusahaan.TglMulaiSistemBaru)
                return 0;
            else
                return base.SaveUpdate(Dp, CallSaveRule, CallValidateError);
        }

        public override int SaveDelete(DataPersistance Dp, bool CallDeleteRule)
        {
            if (ListAkun == null &&  _TglJurnal < BaseGL.SetingPerusahaan.TglMulaiSistemBaru)
                return 0;
            else
                return base.SaveDelete(Dp, CallDeleteRule);
        }
        #endregion

        #region Static Methods
        public static void Hapus(DataPersistance Dp, string NoJurnal)
        {
            FieldParam ParamNoJurnal = new FieldParam("0", NoJurnal);

            Jurnal Jr = new Jurnal();
            if (Jr.FastLoadEntity(
                "TglJurnal,BuatJurnalPembalik,NoJurnalPembalik,TglDibalik",
                "NoJurnal=@0", ParamNoJurnal))
            {
                using (EntityTransaction tr = new EntityTransaction(Dp))
                {
                    Dp.ExecuteNonQuery(
                        "DELETE FROM Jurnal WHERE NoJurnal=@0",
                        ParamNoJurnal);
                    Dp.ExecuteNonQuery(
                        "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                        ParamNoJurnal);
                    if (Jr.BuatJurnalPembalik &&
                        Jr._NoJurnalPembalik.Length > 0)
                    {
                        ParamNoJurnal.Value = Jr._NoJurnalPembalik;
                        Dp.ExecuteNonQuery(
                             "DELETE FROM Jurnal WHERE NoJurnal=@0",
                             ParamNoJurnal);
                        Dp.ExecuteNonQuery(
                            "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                            ParamNoJurnal);
                        if (Jr._TglDibalik < Jr._TglJurnal)
                            Jr._TglJurnal = Jr._TglDibalik;
                    }
                    BaseGL.RingkasanAkun.Hapus(Jr._TglJurnal);

                    tr.CommitTransaction();
                }
            }
        }
        public static void Hapus(DataPersistance Dp, string JenisDokSumber,
            string NoDokSumber)
        {
            Jurnal Jr = new Jurnal();
            if (Jr.FastLoadEntity(
                "NoJurnal,TglJurnal,BuatJurnalPembalik,NoJurnalPembalik,TglDibalik", 
                "JenisDokSumber=@jds AND NoDokSumber=@nds", 
                new FieldParam("jds", JenisDokSumber),
                new FieldParam("nds", NoDokSumber)))
            {
                FieldParam ParamNoJurnal = new FieldParam("0", Jr._NoJurnal);
                using (EntityTransaction tr = new EntityTransaction(Dp))
                {
                    Dp.ExecuteNonQuery(
                        "DELETE FROM Jurnal WHERE NoJurnal=@0",
                        ParamNoJurnal);
                    Dp.ExecuteNonQuery(
                        "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                        ParamNoJurnal);
                    if (Jr.BuatJurnalPembalik &&
                        Jr._NoJurnalPembalik.Length > 0)
                    {
                        ParamNoJurnal.Value = Jr._NoJurnalPembalik;
                        Dp.ExecuteNonQuery(
                             "DELETE FROM Jurnal WHERE NoJurnal=@0",
                             ParamNoJurnal);
                        Dp.ExecuteNonQuery(
                            "DELETE FROM JurnalDetil WHERE NoJurnal=@0",
                            ParamNoJurnal);
                        if (Jr._TglDibalik < Jr._TglJurnal)
                            Jr._TglJurnal = Jr._TglDibalik;
                    }
                    BaseGL.RingkasanAkun.Hapus(Jr._TglJurnal);

                    tr.CommitTransaction();
                }
            }
        }

        public static string CariNoJurnal(DataPersistance Dp, 
            string JenisDokSumber, string NoDokSumber)
        {
            return (string)Dp.Find.Value<Jurnal>("NoJurnal",
                "JenisDokSumber=@jds AND NoDokSumber=@nds", string.Empty,
                new FieldParam("jds", JenisDokSumber),
                new FieldParam("nds", NoDokSumber));
        }
        #endregion
    }

    [Relation(typeof(Akun))]
    [Index("IdDepartemen")]
    [Index("IdProyek")]
    [Index("IdAkun")]
    public class JurnalDetil : ChildEntity<Jurnal>
    {
        #region Class Constructor
        public JurnalDetil() { }

        public JurnalDetil(Jurnal Parent, AturanJurnalDetil Ajd)
            : base(Parent)
        {
            IdProyek = Ajd.IdProyek;
            IdDepartemen = Ajd.IdDepartemen;
            IdAkun = Ajd.IdAkun;
            if (Ajd.KodeMataUang.Length > 0)
                _KodeMataUang = Ajd.KodeMataUang;
            if (Ajd.KodeMataUang.Length == 0 || 
                Ajd.KodeMataUang == BaseGL.SetingPerusahaan.MataUangDasar ||
                !BaseGL.SetingPerusahaan.MultiMataUang)
            {
                _Debit = Ajd.Debit;
                _Kredit = Ajd.Kredit;
                _DebitKurs = _Debit;
                _KreditKurs = _Kredit;
                _Kurs = 1;
            }
            else
            {
                _DebitKurs = Ajd.DebitKurs;
                _KreditKurs = Ajd.KreditKurs;
                Kurs = BaseGL.FungsiGL.GetNilaiTukar(GetParent().TglJurnal, 
                    _KodeMataUang);
            }
        }

        public JurnalDetil(Jurnal Parent,
            string IdDepartemen, string IdProyek,
            string IdAkun, string IdSL,
            decimal DebitKurs, decimal KreditKurs, decimal Kurs,
            string Keterangan)
            : base(Parent)
        {
            Akun Ak = new Akun();
            if (!Ak.FastLoadEntity("KodeMataUang",
                "IdAkun=@0 AND Posting<>0",
                new FieldParam("0", IdAkun)))
                throw new ApplicationException("No Akun tidak valid !");

            _IdDepartemen = IdDepartemen;
            _IdProyek = IdProyek;
            _IdAkun = IdAkun;
            _SL = IdSL;
            _KodeMataUang = Ak.KodeMataUang;
            if (Kurs == 0)
                _Kurs = BaseGL.FungsiGL.GetNilaiTukar(
                    Parent.TglJurnal, _KodeMataUang);
            else
                _Kurs = Kurs;
            _DebitKurs = DebitKurs;
            _KreditKurs = KreditKurs;

            _Debit = DebitKurs * _Kurs;
            _Kredit = KreditKurs * _Kurs;

            _Keterangan = Keterangan;
        }
        #endregion

        #region Class Attribute
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
                _KodeDepartemen = (string)Find.Value<Departemen>
                    ("KodeDepartemen", "IdDepartemen=@0", string.Empty,
                    new FieldParam("0",_IdDepartemen));
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
                _KodeProyek = (string)Find.Value<Proyek>
                ("KodeProyek", "IdProyek=@0", string.Empty,
                new FieldParam("0", _IdProyek));
            }
        }

        private string _KodeProyek;
        [DataTypeLoadSql(typeof(Proyek), "IdProyek")]
        public string KodeProyek
        {
            get { return _KodeProyek; }
        }

        private string _IdAkun;
        [DataTypeVarChar(50, BrowseHidden = true), EmptyError]
        public string IdAkun
        {
            get { return _IdAkun; }
            set
            {
                _IdAkun = value;

                Akun ak = new Akun();

                if (ak.FastLoadEntity(
                    "NoAkun,NamaAkun,AkunMoneter,KodeMataUang",
                    "IdAkun=@0", new FieldParam("0", _IdAkun)))
                {
                    _NoAkun = ak.NoAkun;
                    _NamaAkun = ak.NamaAkun;
                    _AkunMoneter = ak.AkunMoneter;
                    if (_AkunMoneter)
                    {
                        _KodeMataUang = ak.KodeMataUang;
                        _Kurs = BaseGL.FungsiGL.GetNilaiTukar(
                            GetParent().TglJurnal, _KodeMataUang);
                    }
                    else
                    {
                        _KodeMataUang = BaseGL.SetingPerusahaan.MataUangDasar;
                        _Kurs = 1;
                    }
                    if (_Kurs <= 0) _Kurs = 1;
                    _KreditKurs = _Kredit / _Kurs;
                    _DebitKurs = _Debit / _Kurs;
                }
            }
        }

        private string _NoAkun;
        [DataTypeLoadSql(typeof(Akun), "IdAkun")]
        public string NoAkun
        {
            get { return _NoAkun; }
        }

        private string _NamaAkun;
        [DataTypeLoadSql(typeof(Akun), "IdAkun")]
        public string NamaAkun
        {
            get { return _NamaAkun; }
        }

        private bool _AkunMoneter;
        [DataTypeLoadSql(typeof(Akun), "IdAkun", BrowseHidden = true)]
        public bool AkunMoneter
        {
            get { return _AkunMoneter; }
        }

        private decimal _Debit;
        [DataTypeDecimal]
        public decimal Debit
        {
            get { return _Debit; }
            set
            {
                _Debit = value;
                if (_Debit != 0)
                {
                    _KreditKurs = 0;
                    _Kredit = 0;
                    if (_Kurs > 0)
                        _DebitKurs = _Debit / _Kurs;
                    else
                        _DebitKurs = 0;
                }
                DataChanged();
            }
        }

        private decimal _Kredit;
        [DataTypeDecimal]
        public decimal Kredit
        {
            get { return _Kredit; }
            set
            {
                _Kredit = value;
                if (_Kredit != 0)
                {
                    _Debit = 0;
                    _DebitKurs = 0;
                    if (_Kurs > 0)
                        _KreditKurs = _Kredit / _Kurs;
                    else
                        _KreditKurs = 0;
                }
                DataChanged();
            }
        }

        private string _KodeMataUang;
        [DataTypeVarChar(3)]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                if (!_AkunMoneter)
                {
                    _KodeMataUang = value;
                    _Kurs = BaseGL.FungsiGL.GetNilaiTukar(
                        GetParent().TglJurnal, _KodeMataUang);

                    if (_DebitKurs != 0 || _KreditKurs != 0)
                    {
                        if (_DebitKurs != 0)
                        {
                            _Debit = _DebitKurs * _Kurs;
                            _Kredit = 0;
                        }
                        else
                        {
                            _Kredit = _KreditKurs * _Kurs;
                            _Debit = 0;
                        }
                    }
                    else
                    {
                        if (_Debit != 0)
                        {
                            _DebitKurs = _Debit / _Kurs;
                            _KreditKurs = 0;
                        }
                        else
                        {
                            _KreditKurs = _Kredit / _Kurs;
                            _DebitKurs = 0;
                        }
                    }
                }
            }
        }

        private decimal _Kurs;
        [DataTypeDecimal]
        public decimal Kurs
        {
            get { return _Kurs; }
            set
            {
                if (_KodeMataUang != BaseGL.SetingPerusahaan.MataUangDasar)
                {
                    _Kurs = value;
                    if (_DebitKurs != 0)
                    {
                        _Debit = _DebitKurs * _Kurs;
                        _Kredit = 0;
                    }
                    else
                    {
                        _Kredit = _KreditKurs * _Kurs;
                        _Debit = 0;
                    }
                }
            }
        }

        private decimal _DebitKurs;
        [DataTypeDecimal]
        public decimal DebitKurs
        {
            get { return _DebitKurs; }
            set
            {
                _DebitKurs = value;
                if (_DebitKurs != 0)
                {
                    _Debit = _DebitKurs * _Kurs;
                    _Kredit = 0;
                    _KreditKurs = 0;
                }
                else
                    _Debit = 0;
            }
        }

        private decimal _KreditKurs;
        [DataTypeDecimal]
        public decimal KreditKurs
        {
            get { return _KreditKurs; }
            set
            {
                _KreditKurs = value;
                if (_KreditKurs != 0)
                {
                    _DebitKurs = 0;
                    _Debit = 0;
                    _Kredit = _KreditKurs * _Kurs;
                }
                else
                    _Kredit = 0;
            }
        }

        private string _Keterangan;
        [DataTypeVarChar(250)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private string _SL;
        [DataTypeVarChar(50, BrowseHidden = true)]
        public string SL
        {
            get { return _SL; }
        }
        #endregion

        #region Override Methods
        public override bool IsReadOnly(string FieldName)
        {
            if (GetParent().Internal) return true;
            switch (FieldName)
            {
                case "Kurs":
                    return _KodeMataUang == 
                        BaseGL.SetingPerusahaan.MataUangDasar;
                case "KodeMataUang":
                    return _AkunMoneter;
            }
            return false;
        }
        #endregion
    }
}