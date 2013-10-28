using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    public class SaldoAwalAkun : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;
        public AutoUpdateBindingList<SaldoAwalAkun> ListSaldoAwal;

        private string _IdAkun;
        [PrimaryKey, EmptyError,
            DataTypeVarChar(50, Default=null, BrowseHidden = true)]
        public string IdAkun
        {
            get { return _IdAkun; }
            set
            {
                _IdAkun = value;
                LoadEntity();
            }
        }

        private enJenisAkun _JenisAkun;
        [DataTypeLoadSql(typeof(Akun), "IdAkun")]
        public enJenisAkun JenisAkun
        {
            get { return _JenisAkun; }
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

        private decimal _SaldoAwal;
        [DataTypeDecimal]
        public decimal SaldoAwal
        {
            get { return _SaldoAwal; }
            set { _SaldoAwal = value; }
        }

        public static decimal GetSaldoAwal(DataPersistance Dp, string IdAkun)
        {
            return (decimal)Dp.Find.SumValue<SaldoAwalAkun>("SaldoAwal",
                "IdAkun=@0 OR IdAkun LIKE @1", 0m,
                new FieldParam("0", IdAkun),
                new FieldParam("1", IdAkun + ".%"));
        }

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun",
                "Posting<>0 AND JenisAkun<>" + 
                FormatSqlValue(enJenisAkun.Laba__Rugi), 
                "NoAkun", true);
            ListSaldoAwal = LoadEntities<SaldoAwalAkun>(
                string.Empty, "NoAkun", true, true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListAkun.Close();
            ListSaldoAwal.Close();
        }

        protected override void AfterSaveDelete()
        {
            BaseGL.RingkasanAkun.SaldoAwalHapus(_IdAkun);
        }

        protected override void AfterSaveNew()
        {
            BaseGL.RingkasanAkun.SaldoAwalBaru(_IdAkun, _SaldoAwal);
        }

        protected override void AfterSaveUpdate()
        {
            BaseGL.RingkasanAkun.SaldoAwalUpdate(_IdAkun, _SaldoAwal);
        }
    }
}
