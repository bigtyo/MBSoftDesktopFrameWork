using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace SentraGL.Master
{
    public enum enJenisArusKas
    {
        Operasional,
        Investasi,
        Pendanaan
    }

    [Relation(typeof(Akun))]
    [Index("JenisArusKas")]
    public class JenisPenerimaanKas : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;
        
        private string _JenisPenerimaan;
        [PrimaryKey, DataTypeVarChar(100), EmptyError]
        public string JenisPenerimaan
        {
            get { return _JenisPenerimaan; }
            set { _JenisPenerimaan = value; }
        }

        private string _Keterangan;
        [DataTypeVarChar(200)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private enJenisArusKas _JenisArusKas;
        [DataTypeVarChar(50, Default = enJenisArusKas.Operasional)]
        public enJenisArusKas JenisArusKas
        {
            get { return _JenisArusKas; }
            set { _JenisArusKas = value; }
        }

        private string _IdAkun;
        [DataTypeVarChar(50)]
        public string IdAkun
        {
            get { return _IdAkun; }
            set
            {
                _IdAkun = value;
                DataChanged();
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

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
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

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>("IdAkun,NoAkun,NamaAkun",
                "Aktif<>0 AND Posting<>0 AND KelompokAkun<>" + 
                FormatSqlValue(enKelompokAkun.Kas__Bank), "NoAkun", true);
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }

        public JenisPenerimaanKas() { }

        public JenisPenerimaanKas(string JenisPenerimaan, string Keterangan,
            enJenisArusKas JenisArusKas, string IdAkun, bool Internal,
            bool Aktif)
        {
            _JenisPenerimaan = JenisPenerimaan;
            _Keterangan = Keterangan;
            _JenisArusKas = JenisArusKas;
            _IdAkun = IdAkun;
            _Internal = Internal;
            _Aktif = Aktif;
        }
    }

    [Relation(typeof(Akun))]
    [Index("JenisArusKas")]
    public class JenisPengeluaranKas : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;

        private string _JenisPengeluaran;
        [PrimaryKey, DataTypeVarChar(100), EmptyError]
        public string JenisPengeluaran
        {
            get { return _JenisPengeluaran; }
            set { _JenisPengeluaran = value; }
        }

        private string _Keterangan;
        [DataTypeVarChar(200)]
        public string Keterangan
        {
            get { return _Keterangan; }
            set { _Keterangan = value; }
        }

        private enJenisArusKas _JenisArusKas;
        [DataTypeVarChar(50, Default = enJenisArusKas.Operasional)]
        public enJenisArusKas JenisArusKas
        {
            get { return _JenisArusKas; }
            set { _JenisArusKas = value; }
        }

        private string _IdAkun;
        [DataTypeVarChar(50)]
        public string IdAkun
        {
            get { return _IdAkun; }
            set
            {
                _IdAkun = value;
                DataChanged();
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

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
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

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>("IdAkun,NoAkun,NamaAkun",
                "Aktif<>0 AND Posting<>0 AND KelompokAkun<>" +
                FormatSqlValue(enKelompokAkun.Kas__Bank), "NoAkun", true);
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }

        public JenisPengeluaranKas() { }

        public JenisPengeluaranKas(string JenisPengeluaran, string Keterangan,
            enJenisArusKas JenisArusKas, string IdAkun, bool Internal,
            bool Aktif)
        {
            _JenisPengeluaran = JenisPengeluaran;
            _Keterangan = Keterangan;
            _JenisArusKas = JenisArusKas;
            _IdAkun = IdAkun;
            _Internal = Internal;
            _Aktif = Aktif;
        }
    }
}
