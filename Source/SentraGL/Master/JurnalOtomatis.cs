using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace SentraGL.Master
{
    public class JurnalOtomatis : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;
        public AutoUpdateBindingList<JurnalOtomatis> ListJurnalOtomatis;

        private string _NamaJurnalOtomatis;
        [DataTypeVarChar(50), PrimaryKey, EmptyError]
        public string NamaJurnalOtomatis
        {
            get { return _NamaJurnalOtomatis; }
            set { _NamaJurnalOtomatis = value; }
        }

        private string _ListVariabelData;
        public string ListVariabelData
        {
            get { return _ListVariabelData; }
        }

        public EntityCollection<DetilJurnalOtomatis> DetilJurnalOtomatis;

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun,KelompokAkun", "Posting<>0",
                "NoAkun", true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }

        public override bool AllowAddNew
        {
            get
            {
                return false;
            }
        }

        public override bool AllowDelete
        {
            get
            {
                return false;
            }
        }
    }

    public enum enPosisiJurnal { Debit, Kredit }

    public class DetilJurnalOtomatis : ChildEntity<JurnalOtomatis>
    {
        [CounterKey]
        private string NoUrut;

        private string _IdAkun;
        [DataTypeVarChar(50), EmptyError]
        public string IdAkun
        {
            get { return _IdAkun; }
            set { _IdAkun = value; }
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

        private enPosisiJurnal _PosisiJurnal;
        [DataTypeVarChar(10)]
        public enPosisiJurnal PosisiJurnal
        {
            get { return _PosisiJurnal; }
            set { _PosisiJurnal = value; }
        }

        private string _TemplateNilaiJurnal;
        [DataTypeVarChar(200), EmptyError]
        public string TemplateNilaiJurnal
        {
            get { return _TemplateNilaiJurnal; }
            set { _TemplateNilaiJurnal = value; }
        }

        private string _TemplateKeterangan;
        [DataTypeVarChar(100)]
        public string TemplateKeterangan
        {
            get { return _TemplateKeterangan; }
            set { _TemplateKeterangan = value; }
        }
    }
}
