using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraGL;
using SentraGL.Report;

namespace SentraGL.Document
{
    public class KursHarian : ParentEntity
    {
        public AutoUpdateBindingList<MataUang> ListMataUang;
        public AutoUpdateBindingList<KursHarian> ListKursHarian;

        private DateTime _TglKurs;
        [PrimaryKey, DataTypeDate(Default = null), TransactionDate]
        public DateTime TglKurs
        {
            get { return _TglKurs; }
            set
            {
                _TglKurs = value;
                if (!LoadEntity())
                    _NilaiTukar = BaseGL.FungsiGL
                        .GetNilaiTukar(_TglKurs, _KodeMataUang);
                if (ListKursHarian != null)
                    ListKursHarian.Refresh("TglKurs=" + 
                        FormatSqlValue(_TglKurs, DataType.Date), 
                        "KodeMataUang", false, false);
                DataChanged();
            }
        }

        private string _KodeMataUang;
        [PrimaryKey, DataTypeChar(3, Default = null), EmptyError]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                _KodeMataUang = value;
                if (!LoadEntity())
                {
                    _NilaiTukar = BaseGL.FungsiGL.GetNilaiTukar(
                        _TglKurs, _KodeMataUang);
                }
                DataChanged();
            }
        }

        private decimal _NilaiTukar;
        [DataTypeDecimal(Default = 1)]
        public decimal NilaiTukar
        {
            get { return _NilaiTukar; }
            set { _NilaiTukar = value; }
        }

        protected override void AfterSetDefault()
        {
            KodeMataUang = _KodeMataUang;
        }

        private DateTime LastTgl = new DateTime();
        protected override void AfterLoadFound()
        {
            if (ListKursHarian != null && LastTgl != _TglKurs)
            {
                LastTgl = _TglKurs;
                ListKursHarian.Refresh("TglKurs=" + FormatSqlValue(
                    _TglKurs, DataType.Date), "KodeMataUang",
                    false, false);
            }
        }

        protected override void InitUI()
        {
            ListMataUang = FastLoadEntities<MataUang>(
                "KodeMataUang,NamaMataUang", 
                "Aktif<>0 AND KodeMataUang<>" +
                FormatSqlValue(BaseGL.SetingPerusahaan
                .MataUangDasar), "KodeMataUang", true);
            _TglKurs = DateTime.Today;
            ListKursHarian = LoadEntities<KursHarian>("TglKurs=" +
                FormatSqlValue(_TglKurs, DataType.Date), "KodeMataUang",
                false, true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListMataUang.Close();
            ListKursHarian.Close();
        }

        protected override void ValidateError()
        {
            if (_NilaiTukar <= 0) AddError("NilaiTukar", 
                "Nilai Tukar harus lebih dari atau sama dengan nol");
        }

        public static decimal GetNilaiTukar(DateTime TglKurs, string KodeMataUang)
        {
            if (KodeMataUang == BaseGL.SetingPerusahaan.MataUangDasar)
                return 1;
            return (decimal)BaseFramework.DefaultDp.Find.FirstValue<KursHarian>(
                "NilaiTukar", "KodeMataUang=@0 AND TglKurs<=@1", "TglKurs DESC", 1m,
                new FieldParam("0", KodeMataUang),
                new FieldParam("1", TglKurs));
        }
    }
}
