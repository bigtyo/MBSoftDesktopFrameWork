using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraGL.Document;

namespace SentraGL
{
    [NoKeyEntity]
    public class SetingPerusahaan : ParentEntity 
    {
        [DataTypeVarChar(100)]
        public string NamaPerusahaan;

        [DataTypeChar(3)]
        public string KodePerusahaan;

        [DataTypeVarChar(250)]
        public string AlamatPerusahaan;

        [DataTypeVarChar(100)]
        public string Kota;

        [DataTypeVarChar(50)]
        public string Telpon;

        [DataTypeVarChar(50)]
        public string Fax;

        [DataTypeImage]
        public Image LogoPerusahaan;

        private bool _MultiMataUang;
        [DataTypeBoolean(Default = false)]
        public bool MultiMataUang
        {
            get { return _MultiMataUang; }
            set
            {
                _MultiMataUang = value;
                DataChanged();
            }
        }

        [DataTypeBoolean(Default = false)]
        public bool MultiDepartemen;

        [DataTypeBoolean(Default = false)]
        public bool MultiProyek;

        [DataTypeChar(3, Default = "IDR")]
        public string MataUangDasar = "IDR";

        private DateTime _TglMulaiSistemBaru;
        public DateTime TglMulaiSistemBaru
        {
            get { return _TglMulaiSistemBaru; }
            set { _TglMulaiSistemBaru = value; }
        }

        private string _IdAkunLabaRugiTahunBerjalan = string.Empty;
        public string IdAkunLabaRugiTahunBerjalan
        {
            get { return _IdAkunLabaRugiTahunBerjalan; }
            internal set { _IdAkunLabaRugiTahunBerjalan = value; }
        }

        private string _IdAkunLabaRugiTahunLalu = string.Empty;
        public string IdAkunLabaRugiTahunLalu
        {
            get { return _IdAkunLabaRugiTahunLalu; }
            internal set { _IdAkunLabaRugiTahunLalu = value; }
        }

        protected override void ValidateError()
        {
            if (PrimaryKeyUpdateable && KodePerusahaan.Length != 3)
                AddError("KodePerusahaan", 
                    "Kode Perusahaan Harus 3 digit");
            if (_TglMulaiSistemBaru.Day != 1)
                AddError("TglMulaiSistemBaru",
                    "Sistem harus dimulai Tgl 1 Bulan tertentu");

            //DateTime TglJurnalMin = (DateTime)Find.FirstValue<Jurnal>("TglJurnal",
            //    string.Empty, "TglJurnal", DateTime.MinValue);
            //if (TglJurnalMin != DateTime.MinValue)
            //{
            //    if (_TglMulaiSistemBaru > TglJurnalMin)
            //        AddError("TglMulaiSistemBaru",
            //            "Sudah ada transaksi jurnal dgn tgl kurang dari Tgl tersebut");
            //}
        }

        protected override void AfterSaveUpdate()
        {
            if (!Find.IsExists<MataUang>("KodeMataUang=" +
                FormatSqlValue(MataUangDasar)))
            {
                MataUang mu = new MataUang();
                mu.KodeMataUang = MataUangDasar;
                mu.NamaMataUang = "Rupiah";
                mu.Aktif = true;
                mu.SaveNew();
            }

            if (GetOriginal<SetingPerusahaan>().MataUangDasar !=
                MataUangDasar)
                Akun.SetMataUangDasar(Dp, MataUangDasar);

            BaseFramework.TransDate.Reload();
            DateTime TmpTgl = BaseFramework.TransDate.StartDate;
            BaseFramework.TransDate.StartDate = _TglMulaiSistemBaru;

            if (!Find.IsExists<SaldoAwalAkun>(string.Empty)) return;

            if (TmpTgl != _TglMulaiSistemBaru)
            {
                TmpTgl = TmpTgl.AddDays(-1);
                int OldThn = TmpTgl.Year;
                int OldBln = TmpTgl.Month;
                TmpTgl = _TglMulaiSistemBaru.AddDays(-1);
                int NewThn = TmpTgl.Year;
                int NewBln = TmpTgl.Month;

                ExecuteNonQuery(@"UPDATE RingkasanAkun SET Tahun=@NewThn,
                    Bulan=@NewBln WHERE Tahun=@OldThn AND Bulan=@OldBln",
                new FieldParam("NewThn", NewThn),
                new FieldParam("NewBln", NewBln),
                new FieldParam("OldThn", OldThn),
                new FieldParam("OldBln", OldBln));
            }
        }

        protected override void AfterSaveNew()
        {
            if (!Find.IsExists<MataUang>("KodeMataUang=" +
                FormatSqlValue(MataUangDasar)))
            {
                MataUang mu = new MataUang();
                mu.KodeMataUang = MataUangDasar;
                mu.NamaMataUang = "Rupiah";
                mu.Aktif = true;
                mu.SaveNew();
            }

            if (GetOriginal<SetingPerusahaan>() == null || 
                GetOriginal<SetingPerusahaan>().MataUangDasar != MataUangDasar)
                Akun.SetMataUangDasar(Dp, MataUangDasar); 
            
            BaseFramework.TransDate.StartDate = _TglMulaiSistemBaru;
        }
        
        protected override void AfterLoadFound()
        {
            #region Baca Akun Laba Rugi Thn Berjalan/ Lalu
            Akun ak = new Akun();
            if (ak.FastLoadEntity("IdAkun", "KelompokAkun=@0",
                new FieldParam("0", enKelompokAkun.Laba__RugiTahunBerjalan)))
                _IdAkunLabaRugiTahunBerjalan = ak.IdAkun;

            if (ak.FastLoadEntity("IdAkun", "KelompokAkun=@0",
                new FieldParam("0", enKelompokAkun.Laba__RugiTahunLalu)))
                _IdAkunLabaRugiTahunLalu = ak.IdAkun;
            #endregion

            _TglMulaiSistemBaru = BaseFramework
                .TransDate.StartDate;
        }

        protected override void AfterLoadNotFound()
        {
            AfterLoadFound();
        }

        protected override void InitUI()
        {
            FormMode = FormMode.FormEdit;
        }
    }
}
