using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    [Relation(typeof(MataUang))]
    public class NilaiTukarSaldoAwal : ParentEntity
    {
        public AutoUpdateBindingList<MataUang> ListMataUang;
        public AutoUpdateBindingList<NilaiTukarSaldoAwal> ListNilaiTukarSaldoAwal;

        private string _KodeMataUang;
        [PrimaryKey, DataTypeChar(3, Default = null), EmptyError]
        public string KodeMataUang
        {
            get { return _KodeMataUang; }
            set
            {
                _KodeMataUang = value;
                LoadEntity();
           }
        }

        private decimal _NilaiTukar;
        [DataTypeDecimal(Default = 1)]
        public decimal NilaiTukar
        {
            get { return _NilaiTukar; }
            set { _NilaiTukar = value; }
        }

        protected override void ValidateError()
        {
            if (_NilaiTukar <= 0)
                AddError("NilaiTukar", "Nilai Tukar tidak boleh kurang dari atau sama dengan nol");
        }

        protected override void InitUI()
        {
            ListMataUang = FastLoadEntities<MataUang>(
                "KodeMataUang,NamaMataUang",
                "KodeMataUang<>" + FormatSqlValue(BaseGL.SetingPerusahaan
                .MataUangDasar), "KodeMataUang", true);
            ListNilaiTukarSaldoAwal = LoadEntities<NilaiTukarSaldoAwal>(
                string.Empty, "KodeMataUang", true, true);
            AutoFormMode = true;
        }

        protected override void EndUI()
        {
            ListMataUang.Close();
            ListNilaiTukarSaldoAwal.Close();
        }

        public static decimal GetNilaiTukar(DataPersistance Dp, string MataUang)
        {
            return (decimal)Dp.Find.Value<NilaiTukarSaldoAwal>("NilaiTukar",
                "KodeMataUang=@0", 1m, new FieldParam("0", MataUang));
        }
    }
}
