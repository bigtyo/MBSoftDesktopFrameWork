using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSecurity;
using SentraSolutionFramework;
using SentraGL;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    [EnableLog]
    public class MasterAturanJurnal : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;
        public AutoUpdateBindingList<MataUang> ListMataUang;
        public AutoUpdateBindingList<Proyek> ListProyek;
        public AutoUpdateBindingList<Departemen> ListDepartemen;
        
        private string _AturanJurnal;
        [DataTypeVarChar(50), PrimaryKey, EmptyError]
        public string AturanJurnal
        {
            get { return _AturanJurnal; }
            set { _AturanJurnal = value; }
        }

        private string _Keterangan;
        [DataTypeVarChar(250)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
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

        public EntityCollection<AturanJurnalDetil> AturanJurnalDetil;

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun",
                "Aktif<>0 AND Posting<>0", "NoAkun", true);
            ListMataUang = FastLoadEntities<MataUang>(
                "KodeMataUang,NamaMataUang",
                "Aktif<>0", "KodeMataUang", true);
            ListDepartemen = FastLoadEntities<Departemen>
                ("IdDepartemen,KodeDepartemen,NamaDepartemen",
                "Posting<>0 AND Aktif<>0", "KodeDepartemen", 
                true, true);
            ListProyek = FastLoadEntities<Proyek>
                ("IdProyek,KodeProyek,NamaProyek",
                "Posting<>0 AND Aktif<>0", "KodeProyek", 
                true, true);
            isInitUI = true;
        }

        protected override void EndUI()
        {
            ListAkun.Close();
            ListMataUang.Close();
            ListDepartemen.Close();
            ListProyek.Close();
        }

        protected override void AfterLoadFound()
        {
            foreach (AturanJurnalDetil ajd in AturanJurnalDetil)
            {
                if (ajd.DebitKurs != 0 || ajd.KreditKurs != 0)
                {
                    MultiMataUang = true;
                    return;
                }
            }
            MultiMataUang = false;
        }

        public override bool IsVisible(string FieldName)
        {
            if (!BaseGL.SetingPerusahaan.MultiMataUang &&
                FieldName == "MultiMataUang")
                return false;
            else
                return true;
        }

        bool isInitUI = false;
        bool isChildChange = false;

        protected override void onChildDataChanged(string ChildName, BusinessEntity ChildObject)
        {
            if (isInitUI && !isChildChange && ChildObject != null)
            {
                isChildChange = true;

                int Jum = AturanJurnalDetil.Count;
                if (Jum == 0) return;

                if (!object.ReferenceEquals(ChildObject, 
                    AturanJurnalDetil[0]))
                {
                    AturanJurnalDetil Jd = (AturanJurnalDetil)ChildObject;
                    AturanJurnalDetil Jd0 = AturanJurnalDetil[0];
                    if (Jd.Debit == 1 || Jd.Kredit == 1 || 
                        (Jd.Debit == 0 && Jd.Kredit == 0))
                    {
                        Jd.Keterangan = AturanJurnalDetil[0].Keterangan;
                        decimal Jumlah = 0;
                        foreach (AturanJurnalDetil Jd1 in AturanJurnalDetil)
                            if (!object.ReferenceEquals(Jd1, Jd))
                                Jumlah += (Jd1.Debit - Jd1.Kredit);
                        if (Jumlah > 0)
                            Jd.Kredit = Jumlah;
                        else if (Jum < 0)
                            Jd.Debit = -Jumlah;
                    }
                }
                isChildChange = false;
            }
        }

        public override bool IsChildColumnVisible(string ChildName, string ColumnName)
        {
            switch (ColumnName)
            {
                case "KodeMataUang":
                case "DebitKurs":
                case "KreditKurs":
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
    }

    [Relation(typeof(Akun))]
    [Relation(typeof(Departemen))]
    [Relation(typeof(Proyek))]
    public class AturanJurnalDetil : ChildEntity<MasterAturanJurnal>
    {
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
            set
            {
                _KodeProyek = value;
            }
        }

        private string _IdAkun;
        [DataTypeVarChar(50, BrowseHidden = true), EmptyError]
        public string IdAkun
        {
            get { return _IdAkun; }
            set
            {
                _IdAkun = value;
                UpdateLoadSqlFields(
                    "NoAkun,NamaAkun,AkunMoneter,KodeMataUang");
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
                    _Kredit = 0;
                    DataChanged();
                }
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
                    DataChanged();
                }
            }
        }

        private string _KodeMataUang;
        [DataTypeVarChar(3)]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                if (!_AkunMoneter) _KodeMataUang = value;
            }
        }

        private decimal _DebitKurs;
        [DataTypeDecimal]
        public decimal DebitKurs
        {
            get { return _DebitKurs; }
            set
            {
                if (_AkunMoneter) return;
                _DebitKurs = value;
                if (_DebitKurs != 0)
                {
                    _KreditKurs = 0;
                    DataChanged();
                }
            }
        }

        private decimal _KreditKurs;
        [DataTypeDecimal]
        public decimal KreditKurs
        {
            get { return _KreditKurs; }
            set
            {
                if (_AkunMoneter) return;
                _KreditKurs = value;
                if (_KreditKurs != 0)
                {
                    _DebitKurs = 0;
                    DataChanged();
                }
            }
        }

        private string _Keterangan;
        [DataTypeVarChar(250)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }
    }
}
