using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraGL.Master;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraGL.Document;

namespace SentraGL.Report
{
    public class FungsiGL
    {
        private DateTime MaxTgl = DateTime.MinValue;

        public void Init()
        {
            MaxTgl = DateTime.MinValue;
        }

        public decimal PosisiAkun(string NoAkun, DateTime TglPosisi)
        {
            bool UpdateTgl = TglPosisi > MaxTgl;
            if (UpdateTgl) MaxTgl = TglPosisi;

            return BaseGL.RingkasanAkun.PosisiAkun(
                GetIdAkun(NoAkun), TglPosisi, UpdateTgl);
        }

        #region Mutasi Akun Umum
        public decimal MutasiAkun(string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiAkun(
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiDebit(string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiDebit(
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiKredit(string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiKredit(
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        #endregion

        #region Mutasi Akun Departemen
        public decimal MutasiAkunDep(string NoDep, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiAkun(
                GetIdDepartemen(NoDep), string.Empty,
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiDebitDep(string NoDep, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiDebit(
                GetIdDepartemen(NoDep), string.Empty,
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiKreditDep(string NoDep, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiKredit(
                GetIdDepartemen(NoDep), string.Empty,
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        #endregion

        #region Mutasi Akun Proyek
        public decimal MutasiAkunProyek(string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiAkun(
                string.Empty, GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiDebitProyek(string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiDebit(
                string.Empty, GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiKreditProyek(string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiKredit(
                string.Empty, GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        #endregion

        #region Mutasi Akun Departemen-Proyek
        public decimal MutasiAkunDepProyek(string NoDep, string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiAkun(
                GetIdDepartemen(NoDep), GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiDebitDepProyek(string NoDep, string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiDebit(
                GetIdDepartemen(NoDep), GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        public decimal MutasiKreditDepProyek(string NoDep, string KodeProyek, string NoAkun, DateTime TglMulai, DateTime TglAkhir)
        {
            DateTime TmpTgl = TglAkhir > TglMulai ? TglAkhir : TglMulai;
            bool UpdateTgl = TmpTgl > MaxTgl;
            if (UpdateTgl) MaxTgl = TmpTgl;

            return BaseGL.RingkasanAkun.GetMutasiKredit(
                GetIdDepartemen(NoDep), GetIdProyek(KodeProyek),
                GetIdAkun(NoAkun), TglMulai, TglAkhir, UpdateTgl);
        }
        #endregion

        public string IdAkun(string NoAkun)
        {
            return (string)BaseFramework.DefaultDp.Find
                .Value<Akun>("IdAkun", "NoAkun=@0", string.Empty,
                new FieldParam("0", NoAkun));
        }
        public string NoAkun(string IdAkun)
        {
            return (string)BaseFramework.DefaultDp.Find.Value<Akun>(
                "NoAkun", "IdAkun=@0", string.Empty,
                new FieldParam("0", IdAkun));
        }
        public string NamaAkun(string NoAkun)
        {
            return (string)BaseFramework.DefaultDp.Find
                .Value<Akun>("NamaAkun", "NoAkun=@0", string.Empty,
                new FieldParam("0", NoAkun));
        }

        private string GetIdAkun(string NoAkun)
        {
            return (string)BaseFramework.DefaultDp.Find
                .Value<Akun>("IdAkun", "NoAkun=@0", string.Empty,
                new FieldParam("0", NoAkun));
        }
        private string GetIdProyek(string KodeProyek)
        {
            return (string)BaseFramework.DefaultDp.Find
                .Value<Proyek>("IdProyek", "KodeProyek=@0", string.Empty,
                new FieldParam("0", KodeProyek));
        }
        private string GetIdDepartemen(string KodeDepartemen)
        {
            return (string)BaseFramework.DefaultDp.Find
                .Value<Departemen>("IdDepartemen", "KodeDepartemen=@0", 
                string.Empty, new FieldParam("0", KodeDepartemen));
        }

        public decimal GetNilaiTukar(DateTime TglKurs, string KodeMataUang)
        {
            return KursHarian.GetNilaiTukar(TglKurs, KodeMataUang);
        }
    }
}
