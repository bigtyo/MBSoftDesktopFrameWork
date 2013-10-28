using System;
using System.Collections.Generic;
using System.Text;
using SentraSecurity;
using SentraSolutionFramework.Entity;
using SentraGL.Document;

namespace SentraGL.Master
{
    [Relation(typeof(Akun), "IdAkun", "IdAkunLabaRugiSelisihKurs")]
    [EnableLog]
    public class MataUang : ParentEntity
    {
        public AutoUpdateBindingList<Akun> ListAkun;

        private string _KodeMataUang;
        [DataTypeChar(3, Default = null), PrimaryKey, EmptyError]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                if (_KodeMataUang == value) return;
                _KodeMataUang = value;
                LoadEntity();
            }
        }

        private string _NamaMataUang;
        [DataTypeVarChar(50), EmptyError]
        public string NamaMataUang
        {
            get { return _NamaMataUang; }
            set { _NamaMataUang = value; }
        }

        private bool _Aktif;
        [DataTypeBoolean]
        public bool Aktif
        {
            get { return _Aktif; }
            set { _Aktif = value; }
        }

        private string _IdAkunLabaRugiSelisihKurs;
        [DataTypeVarChar(50, BrowseHidden = true)]
        public string IdAkunLabaRugiSelisihKurs
        {
            get { return _IdAkunLabaRugiSelisihKurs; }
            set
            {
                _IdAkunLabaRugiSelisihKurs = value;
                DataChanged();
            }
        }

        private string _NoAkunLabaRugiSelisihKurs;
        [DataTypeLoadSql(typeof(Akun), 
            "IdAkun=IdAkunLabaRugiSelisihKurs", "NoAkun")]
        public string NoAkunLabaRugiSelisihKurs
        {
            get { return _NoAkunLabaRugiSelisihKurs; }
        }

        private string _NamaAkunLabaRugiSelisihKurs;
        [DataTypeLoadSql(typeof(Akun),
            "IdAkun=IdAkunLabaRugiSelisihKurs", "NamaAkun")]
        public string NamaAkunLabaRugiSelisihKurs
        {
            get { return _NamaAkunLabaRugiSelisihKurs; }
        }

        protected override void ValidateError()
        {
            if (_KodeMataUang.Length != 3)
                AddError("KodeMataUang",
                    "Kode mata uang harus 3(tiga) huruf");
        }

        protected override void InitUI()
        {
            ListAkun = FastLoadEntities<Akun>(
                "IdAkun,NoAkun,NamaAkun", 
                "Aktif<>0 AND Posting<>0  AND KelompokAkun=" +
                FormatSqlValue(enKelompokAkun.Pendapatan__BiayaLain_Lain),
                "NoAkun", true);
            EditAfterSaveNew = true;
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListAkun.Close();
        }

        public void ShowPilihJurnal()
        {
            EntityColumnShow ecs = new EntityColumnShow("NoJurnal, TglJurnal");
            ecs.ListChild.Add(new ChildColumnShow("JurnalDetil", "NoAkun, NamaAkun, Debit, Kredit"));
            ChooseSingleEntity<Jurnal>(string.Empty, string.Empty, true,
                "Pilih Jurnal", ecs);
        }
    }

    public class ClassTest 
    {
        public string Hello()
        {
            return "Hai";
        }

        public string Test = "Test123";
    }
}
