using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraGL.Document;
using SentraGL.Report;

namespace SentraGL
{
    [Relation(typeof(Akun))]
    public class RingkasanAkun : ParentEntity
    {
        [DataTypeInteger, PrimaryKey]
        private int Tahun;
        [DataTypeInteger, PrimaryKey]
        private int Bulan;
        [DataTypeVarChar(50), PrimaryKey]
        private string IdAkun;
        [DataTypeDecimal]
        private decimal Debit;
        [DataTypeDecimal]
        private decimal Kredit;
        [DataTypeDecimal]
        private decimal MutasiDebit;
        [DataTypeDecimal]
        private decimal MutasiKredit;

        internal RingkasanAkun()
        {
            strJenisLabaRugi = FormatSqlValue(enJenisAkun.Laba__Rugi, 
                DataType.VarChar);

            Dp.ValidateTableDef<RingkasanAkun>();
            Dp.ValidateTableDef<RingkasanAkunKurs>();
            Dp.ValidateTableDef<ViewSaldoAwalKurs>();
        }

        /// <summary>
        /// Menghapus seluruh data summary sampai dengan TglHapus
        /// </summary>
        /// <param name="TglHapus"></param>
        public void Hapus(DateTime TglHapus)
        {
            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                FieldParam p0 = new FieldParam("0", TglHapus.Year);
                FieldParam p1 = new FieldParam("1", TglHapus.Month);
                Dp.ExecuteNonQuery(@"DELETE FROM RingkasanAkun 
                    WHERE Tahun>@0 OR Tahun=@0 AND Bulan>=@1", p0, p1);

                Dp.ExecuteNonQuery(@"DELETE FROM RingkasanAkunKurs 
                    WHERE Tahun>@0 OR Tahun=@0 AND Bulan>=@1", p0, p1);

                tr.CommitTransaction();
            }
        }

        #region Baru, Update, Hapus Saldo Awal
        public void SaldoAwalBaru(string IdAkun, decimal SaldoAwal) 
        {
            if (SaldoAwal == 0) return;

            decimal Kurs = 1;
            bool MataUangDasar = true;
            #region Cari Nilai Tukar
            if (BaseGL.SetingPerusahaan.MultiMataUang)
            {
                Akun ak = new Akun(IdAkun);
                if (ak.FastLoadEntity("KodeMataUang"))
                {
                    MataUangDasar = ak.KodeMataUang == BaseGL.SetingPerusahaan
                        .MataUangDasar;
                    if (!MataUangDasar)
                        Kurs = NilaiTukarSaldoAwal.GetNilaiTukar(Dp,
                            ak.KodeMataUang);
                }
            }
            #endregion

            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                #region Buat RingkasanAkun Baru
                RingkasanAkun ra = new RingkasanAkun();
                DateTime TglSaldoAwal = BaseGL.SetingPerusahaan
                    .TglMulaiSistemBaru.AddDays(-1);
                ra.Tahun = TglSaldoAwal.Year;
                ra.Bulan = TglSaldoAwal.Month;
                ra.IdAkun = IdAkun;
                ra.SaveNew();
                #endregion

                #region Buat RingkasanAkunKurs Baru
                if (!MataUangDasar)
                {
                    RingkasanAkunKurs rak = new RingkasanAkunKurs();
                    rak.Tahun = ra.Tahun;
                    rak.Bulan = ra.Bulan;
                    rak.IdAkun = IdAkun;
                    rak.Save();
                }
                #endregion

                #region Update Debit/ Kredit di RingkasanAkun/ RingkasanAkunKurs
                if (SaldoAwal > 0)
                {
                    Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET
                        Debit=Debit+@0 WHERE IdAkun=@1",
                        new FieldParam("0", SaldoAwal),
                        new FieldParam("1", IdAkun));
                    if (!MataUangDasar)
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs
                            SET DebitKurs=DebitKurs+@0 WHERE IdAkun=@1",
                            new FieldParam("0", SaldoAwal / Kurs),
                            new FieldParam("1", IdAkun));
                }
                else
                {
                    Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET
                        Kredit=Kredit+@0 WHERE IdAkun=@1",
                        new FieldParam("0", -SaldoAwal),
                        new FieldParam("1", IdAkun));
                    if (!MataUangDasar)
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET
                            KreditKurs=KreditKurs+@0 WHERE IdAkun=@1",
                            new FieldParam("0", -SaldoAwal / Kurs),
                            new FieldParam("1", IdAkun));
                }
                #endregion

                tr.CommitTransaction();
            }
        }

        public void SaldoAwalUpdate(string IdAkun, decimal SaldoAwal)
        {
            if (SaldoAwal == 0)
            {
                SaldoAwalHapus(IdAkun);
                return;
            }

            decimal Kurs = 1;
            bool MataUangDasar = true;
            #region Cari Nilai Tukar
            if (BaseGL.SetingPerusahaan.MultiMataUang)
            {
                Akun ak = new Akun(IdAkun);
                if (ak.FastLoadEntity("KodeMataUang"))
                {
                    MataUangDasar = ak.KodeMataUang == BaseGL.SetingPerusahaan
                        .MataUangDasar;
                    if (!MataUangDasar)
                        Kurs = NilaiTukarSaldoAwal.GetNilaiTukar(Dp,
                            ak.KodeMataUang);
                }
            }
            #endregion

            RingkasanAkun ra = new RingkasanAkun();
            RingkasanAkunKurs rak = new RingkasanAkunKurs();

            #region Cari Nilai Lama RingkasanAkun
            DateTime TglSaldoAwal = BaseGL.SetingPerusahaan
                .TglMulaiSistemBaru.AddDays(-1);
            ra.Tahun = TglSaldoAwal.Year;
            ra.Bulan = TglSaldoAwal.Month;
            ra.IdAkun = IdAkun;
            if (!ra.LoadEntity())
                throw new ApplicationException("Saldo Awal tidak ditemukan !");
            #endregion

            #region Cari Nilai Lama RingkasanAkunKurs
            if (!MataUangDasar)
            {
                rak.IdAkun = IdAkun;
                rak.Tahun = ra.Tahun;
                rak.Bulan = ra.Bulan;
                if (!rak.LoadEntity())
                    throw new ApplicationException("Saldo Awal Kurs tidak ditemukan !");
            }
            #endregion

            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                #region Update RingkasanAkun dan RingkasanAkunKurs
                FieldParam p0 = new FieldParam("0", DataType.Decimal);
                FieldParam p1 = new FieldParam("1", IdAkun);
                FieldParam p2 = new FieldParam("2", DataType.Decimal);

                if (SaldoAwal > 0)
                {
                    if (ra.Debit > 0)
                    {
                        #region SaldoAwal > 0 and ra.Debit > 0
                        p0.Value = SaldoAwal - ra.Debit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Debit=Debit+@0 WHERE IdAkun=@1", p0, p1);
                        if (!MataUangDasar)
                        {
                            p0.Value = SaldoAwal / Kurs - rak.DebitKurs;
                            Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                DebitKurs=DebitKurs+@0 WHERE IdAkun=@1", p0, p1);
                        }
                        #endregion
                    }
                    else
                    {
                        #region SaldoAwal > 0 and ra.Kredit > 0
                        p0.Value = SaldoAwal;
                        p2.Value = ra.Kredit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Debit=Debit+@0,Kredit=Kredit-@2 WHERE IdAkun=@1",
                            p0, p2, p1);
                        if (!MataUangDasar)
                        {
                            p0.Value = SaldoAwal / Kurs;
                            p2.Value = rak.KreditKurs;
                            Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                DebitKurs=DebitKurs+@0,KreditKurs=KreditKurs-@2
                                WHERE IdAkun=@1", p0, p2, p1);
                        }
                        #endregion
                    }
                }
                else
                {
                    if (ra.Kredit > 0)
                    {
                        #region SaldoAWal < 0 and ra.Kredit > 0
                        p0.Value = -SaldoAwal - ra.Kredit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Kredit=Kredit+@0 WHERE IdAkun=@1", p0, p1);
                        if (!MataUangDasar)
                        {
                            p0.Value = -SaldoAwal / Kurs - rak.KreditKurs;
                            Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                KreditKurs=KreditKurs+@0 WHERE IdAkun=@1", p0, p1);
                        }
                        #endregion
                    }
                    else
                    {
                        #region SaldoAwal < 0 and ra.Debit > 0
                        p0.Value = -SaldoAwal;
                        p2.Value = ra.Debit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Kredit=Kredit+@0,Debit=Debit-@2 WHERE IdAkun=@1",
                            p0, p2, p1);
                        if (!MataUangDasar)
                        {
                            p0.Value = -SaldoAwal / Kurs;
                            p2.Value = rak.DebitKurs;
                            Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                KreditKurs=KreditKurs+@0,DebitKurs=DebitKurs-@2 
                                WHERE IdAkun=@1", p0, p2, p1);
                        }
                        #endregion
                    }
                }
                #endregion

                tr.CommitTransaction();
            }
        }

        public void SaldoAwalHapus(string IdAkun) 
        {
            bool MataUangDasar = true;
            #region Cari MataUangDasar
            if (BaseGL.SetingPerusahaan.MultiMataUang)
            {
                Akun ak = new Akun(IdAkun);
                if (ak.FastLoadEntity("KodeMataUang"))
                    MataUangDasar = ak.KodeMataUang == BaseGL.SetingPerusahaan
                        .MataUangDasar;
            }
            #endregion

            RingkasanAkun ra = new RingkasanAkun();
            DateTime TglSaldoAwal = BaseGL.SetingPerusahaan
                .TglMulaiSistemBaru.AddDays(-1);
            ra.Tahun = TglSaldoAwal.Year;
            ra.Bulan = TglSaldoAwal.Month;
            ra.IdAkun = IdAkun;

            if (ra.LoadEntity())
            {
                FieldParam p0 = new FieldParam("0", DataType.Decimal);
                FieldParam p1 = new FieldParam("1", IdAkun);

                using (EntityTransaction tr = new EntityTransaction(Dp))
                {
                    #region Hapus RingkasanAkun
                    ra.SaveDelete();
                    if (ra.Debit != 0)
                    {
                        p0.Value = ra.Debit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Debit=Debit-@0 WHERE IdAkun=@1", p0, p1);
                    }
                    else
                    {
                        p0.Value = ra.Kredit;
                        Dp.ExecuteNonQuery(@"UPDATE RingkasanAkun SET 
                            Kredit=Kredit-@0 WHERE IdAkun=@1", p0, p1);
                    }
                    #endregion

                    #region Hapus RingkasanAkurKurs
                    if (!MataUangDasar)
                    {
                        RingkasanAkunKurs rak = new RingkasanAkunKurs();
                        rak.IdAkun = IdAkun;
                        rak.Tahun = ra.Tahun;
                        rak.Bulan = ra.Bulan;
                        if (rak.LoadEntity())
                        {
                            rak.SaveDelete();
                            if (rak.DebitKurs != 0)
                            {
                                p0.Value = rak.DebitKurs;
                                Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                    DebitKurs=DebitKurs-@0 WHERE IdAkun=@1", p0, p1);
                            }
                            else
                            {
                                p0.Value = rak.KreditKurs;
                                Dp.ExecuteNonQuery(@"UPDATE RingkasanAkunKurs SET 
                                    KreditKurs=KreditKurs-@0 WHERE IdAkun=@1", p0, p1);
                            }
                        }
                    }
                    #endregion

                    tr.CommitTransaction();
                }
            }
        }
        #endregion

        private string strJenisLabaRugi;
        private int BlnAkhirDiRingkasan, ThnAkhirDiRingkasan;
        private string strLabaRugiLalu, strLabaRugiBerjalan;

        public string SqlUrutanJenisAkun()
        {
            return Dp.GetSqlIifNoFormat("JenisAkun=" +
                Dp.FormatSqlValue(enJenisAkun.Aktiva), "1",
                Dp.GetSqlIifNoFormat("JenisAkun=" +
                Dp.FormatSqlValue(enJenisAkun.Kewajiban), "2",
                Dp.GetSqlIifNoFormat("JenisAkun=" +
                Dp.FormatSqlValue(enJenisAkun.Modal), "3", "4")));
        }

        public void Update(DateTime TglUpdate)
        {
            DateTime Tmp;
            object[] Hasil;

            SetingPerusahaan sp = BaseGL.SetingPerusahaan;
            strLabaRugiLalu = FormatSqlValue(
                sp.IdAkunLabaRugiTahunLalu, DataType.VarChar);
            strLabaRugiBerjalan = FormatSqlValue(
                sp.IdAkunLabaRugiTahunBerjalan, DataType.VarChar);

            #region Buat TglUpdate=TglAkhirBulan
            if (TglUpdate.AddDays(1).Month == TglUpdate.Month)
                TglUpdate = new DateTime(TglUpdate.Year,
                    TglUpdate.Month, 1).AddDays(-1);
            #endregion

            #region Baca Ringkasan Akun
            if (Find.TryFindFirstValues<RingkasanAkun>("Tahun,Bulan",
                string.Empty, "Tahun DESC,Bulan DESC", out Hasil))
            {
                ThnAkhirDiRingkasan = (int)Hasil[0];
                BlnAkhirDiRingkasan = (int)Hasil[1];

                if (ThnAkhirDiRingkasan > TglUpdate.Year ||
                    ThnAkhirDiRingkasan == TglUpdate.Year &&
                    BlnAkhirDiRingkasan >= TglUpdate.Month)
                {
                    ThnAkhirDiRingkasan = TglUpdate.Year;
                    BlnAkhirDiRingkasan = TglUpdate.Month;
                    return;
                }
            }
            else
            {
                BlnAkhirDiRingkasan = 0;
                DateTime TglRingkasan = sp.TglMulaiSistemBaru.AddDays(-1);

                ThnAkhirDiRingkasan = TglRingkasan.Year;
                BlnAkhirDiRingkasan = TglRingkasan.Month;

                // Hitung Saldo Awal..

                ExecuteNonQuery(string.Concat(
                    @"INSERT INTO RingkasanAkun(Tahun,Bulan,IdAkun,
                    Debit,Kredit,MutasiDebit,MutasiKredit) SELECT @Thn,@Bln,
                    IdAkun,", Dp.GetSqlIifNoFormat("SaldoAwal>0", "SaldoAwal", "0"),
                    ",", Dp.GetSqlIifNoFormat("SaldoAwal<0", "-SaldoAwal", "0"),
                    ",0,0 FROM SaldoAwalAkun"), new FieldParam("Thn", ThnAkhirDiRingkasan),
                    new FieldParam("Bln", BlnAkhirDiRingkasan));

                ExecuteNonQuery(string.Concat(
                    @"INSERT INTO RingkasanAkunKurs(Tahun,Bulan,IdAkun,
                    DebitKurs,KreditKurs,MutasiDebitKurs,MutasiKreditKurs) SELECT @Thn,@Bln,
                    IdAkun,", Dp.GetSqlIifNoFormat("SaldoAwal>0", "SaldoAwal*" +
                    Dp.GetSqlCoalesceNoFormat("NilaiTukar", "1"), "0"),
                    ",", Dp.GetSqlIifNoFormat("SaldoAwal<0", "-SaldoAwal*" +
                    Dp.GetSqlCoalesceNoFormat("NilaiTukar", "1"), "0"),
                    @",0,0 FROM ViewSaldoAwalKurs"), 
                    new FieldParam("Thn", ThnAkhirDiRingkasan),
                    new FieldParam("Bln", BlnAkhirDiRingkasan));
            }
            #endregion

            // Kondisi: TglAkhirDiRingkasan < TglUpdate

            //#region Cari Bulan dan Tahun Terakhir di Trans Jurnal
            //if (Find.TryFindFirstValues<Jurnal>("TglJurnal",
            //    "TglJurnal<@0", "TglJurnal DESC", out Hasil,
            //    new FieldParam("0", TglUpdate.AddDays(1))))
            //{
            //    Tmp = (DateTime)Hasil[0];
            //    if (Tmp.Month != TglUpdate.Month ||
            //        Tmp.Year != TglUpdate.Year)
            //        TglUpdate = new DateTime(Tmp.Year, Tmp.Month,
            //            DateTime.DaysInMonth(Tmp.Year, Tmp.Month));
            //}
            //else
            //    return;
            //#endregion

            if (BlnAkhirDiRingkasan > 0)
            {
                if (TglUpdate.Year < ThnAkhirDiRingkasan ||
                    TglUpdate.Year == ThnAkhirDiRingkasan &&
                    TglUpdate.Month <= BlnAkhirDiRingkasan)
                    return;
            }
            else if (Find.TryFindFirstValues<Jurnal>("TglJurnal",
                "TglJurnal<@0", "TglJurnal", out Hasil,
                new FieldParam("0", TglUpdate.AddDays(1))))
            {
                // Carikan TglAkhirDiRingkasan biar tidak nol..
                Tmp = ((DateTime)Hasil[0]).AddMonths(-1);
                ThnAkhirDiRingkasan = Tmp.Year;
                BlnAkhirDiRingkasan = Tmp.Month;
            }
            else
                return; // tidak mungkin terjadi...

            #region Cek IdAkun LR Thn Berjalan dan LR Thn Lalu
            if (sp.IdAkunLabaRugiTahunBerjalan.Length == 0)
                throw new ApplicationException(
                    "Akun Laba Rugi Tahun Berjalan belum ditentukan");
            if (sp.IdAkunLabaRugiTahunLalu.Length == 0)
                throw new ApplicationException(
                    "Akun Laba Rugi Tahun Lalu belum ditentukan");
            #endregion

            using (EntityTransaction Tr = new EntityTransaction(Dp))
            {
                FieldParam pBulan = new FieldParam(
                    "pBulan", DataType.Integer);
                FieldParam pTahun = new FieldParam(
                    "pTahun", DataType.Integer);
                FieldParam pTglJurnal = new FieldParam(
                    "pTglJurnal", DataType.Date);
                FieldParam pTglJurnalAddM1 = new FieldParam(
                    "pTglJurnalAddM1", DataType.Date);
                FieldParam pThnAkhirDiRingkasan = new FieldParam(
                    "pThnAkhirDiRingkasan", DataType.Integer);
                FieldParam pBlnAkhirDiRingkasan = new FieldParam(
                    "pBlnAkhirDiRingkasan", DataType.Integer);

                Tmp = new DateTime(ThnAkhirDiRingkasan,
                    BlnAkhirDiRingkasan, 1);

                while (ThnAkhirDiRingkasan < TglUpdate.Year ||
                    ThnAkhirDiRingkasan == TglUpdate.Year &&
                    BlnAkhirDiRingkasan < TglUpdate.Month)
                {
                    Tmp = Tmp.AddMonths(1);

                    pBulan.Value = Tmp.Month;
                    pTahun.Value = Tmp.Year;
                    pTglJurnal.Value = Tmp;
                    pTglJurnalAddM1.Value = Tmp.AddMonths(1);
                    pThnAkhirDiRingkasan.Value = ThnAkhirDiRingkasan;
                    pBlnAkhirDiRingkasan.Value = BlnAkhirDiRingkasan;

                    #region Multi MataUang
                    if (sp.MultiMataUang)
                        ExecuteNonQuery(@"INSERT INTO RingkasanAkunKurs(
Tahun,Bulan,IdAkun,DebitKurs,KreditKurs,
MutasiDebitKurs,MutasiKreditKurs) 
SELECT @pTahun,@pBulan,IdAkun,SUM(DebitKurs),SUM(KreditKurs),
SUM(MutasiDebitKurs),SUM(MutasiKreditKurs) FROM 
( SELECT IdAkun,DebitKurs,KreditKurs,
    DebitKurs as MutasiDebitKurs,KreditKurs as MutasiKreditKurs FROM 
    Jurnal a INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal
    WHERE TglJurnal>=@pTglJurnal AND TglJurnal<@pTglJurnalAddM1 AND 
    EXISTS (SELECT IdAkun FROM Akun WHERE b.IdAkun=Akun.IdAkun 
    AND AkunMoneter<>0 AND MataUangDasar=0) 
 UNION ALL 
 SELECT IdAkun,DebitKurs,KreditKurs,0 as MutasiDebitKurs,
    0 as MutasiKreditKurs FROM RingkasanAkunKurs WHERE 
    Tahun=@pThnAkhirDiRingkasan AND Bulan=@pBlnAkhirDiRingkasan
) X WHERE 
DebitKurs<>0 OR KreditKurs<>0 GROUP BY IdAkun", pBulan, pTahun,
                            pTglJurnal, pTglJurnalAddM1, 
                            pThnAkhirDiRingkasan, pBlnAkhirDiRingkasan);
                    #endregion

                    #region Update Ringkasan
                    if (BlnAkhirDiRingkasan < 12)
                        // Jumlahkan Jurnal Detil + 
                        // JurnalDetil yg JenisLabaRugi sbg LabaRugiBerjalan +
                        // RingkasanAkun yang terakhir
                        #region Query..
                        ExecuteNonQuery(string.Concat(
@"INSERT INTO RingkasanAkun(Tahun,Bulan,IdAkun,Debit,Kredit,
MutasiDebit,MutasiKredit) 
SELECT @pTahun,@pBulan,IdAkun,SUM(Debit),SUM(Kredit),
SUM(MutasiDebit),SUM(MutasiKredit) FROM 
(
  SELECT IdAkun,Debit,Kredit,Debit as MutasiDebit,
  Kredit as MutasiKredit FROM Jurnal a INNER JOIN JurnalDetil b 
  ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@pTglJurnal AND 
  TglJurnal<@pTglJurnalAddM1 
  UNION ALL 
  SELECT ", strLabaRugiBerjalan, @" as IdAkun,Debit,Kredit,
  Debit as MutasiDebit,Kredit as MutasiKredit FROM Jurnal a 
  INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal 
  WHERE TglJurnal>=@pTglJurnal AND TglJurnal<@pTglJurnalAddM1 
  AND EXISTS 
  (
    SELECT IdAkun FROM Akun WHERE b.IdAkun=Akun.IdAkun 
    AND JenisAkun=", strJenisLabaRugi, @"
  ) 
  UNION ALL 
  SELECT IdAkun,Debit,Kredit,0 as MutasiDebit,0 as MutasiKredit 
  FROM RingkasanAkun WHERE Tahun=@pThnAkhirDiRingkasan AND 
  Bulan=@pBlnAkhirDiRingkasan
) X WHERE Debit<>0 OR Kredit<>0 GROUP BY IdAkun"), pTglJurnal,
                        pTahun, pBulan, pThnAkhirDiRingkasan, 
                        pBlnAkhirDiRingkasan, pTglJurnalAddM1);
                        #endregion
                    else
                        // Jumlahkan Jurnal Detil + 
                        // JurnalDetil yg JenisLabaRugi sbg LabaRugiBerjalan +
                        // RingkasanAkun yg terakhir yg bkn laba/rugi,labarugi berjalan/lalu +
                        // RingkasanAkun yg terakhir akun labarugi lalu/berjalan sbg laba/rugi lalu
                        #region Query..
                        ExecuteNonQuery(string.Concat(
@"INSERT INTO RingkasanAkun(Tahun,Bulan,IdAkun,Debit,Kredit,
MutasiDebit,MutasiKredit)
SELECT @pTahun,@pBulan,IdAkun,SUM(Debit),SUM(Kredit),
SUM(MutasiDebit),SUM(MutasiKredit) FROM 
(
  SELECT IdAkun,Debit,Kredit,Debit as MutasiDebit,
  Kredit as MutasiKredit FROM Jurnal a INNER JOIN JurnalDetil b 
  ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@pTglJurnal AND 
  TglJurnal<@pTglJurnalAddM1 
  UNION ALL 
  SELECT ", strLabaRugiBerjalan, @" as IdAkun,Debit,Kredit,
  Debit as MutasiDebit,Kredit as MutasiKredit FROM Jurnal a 
  INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal 
  WHERE TglJurnal>=@pTglJurnal AND TglJurnal<@pTglJurnalAddM1
  AND EXISTS 
  (
    SELECT IdAkun FROM Akun WHERE b.IdAkun=Akun.IdAkun AND 
    JenisAkun=", strJenisLabaRugi, @"
  ) 
  UNION ALL 
  SELECT IdAkun,Debit,Kredit,0 as MutasiDebit,0 as MutasiKredit 
  FROM RingkasanAkun b WHERE Tahun=@pThnAkhirDiRingkasan AND 
  Bulan=@pBlnAkhirDiRingkasan AND 
  NOT EXISTS 
  (
     SELECT IdAkun FROM Akun WHERE b.IdAkun=Akun.IdAkun 
     AND JenisAkun=", strJenisLabaRugi, @"
  ) AND b.IdAkun<>", strLabaRugiBerjalan, @"
  AND b.IdAkun<>", strLabaRugiLalu, @"
  UNION ALL 
  SELECT ", strLabaRugiLalu, @" as IdAkun,Debit,Kredit,
  0 AS MutasiDebit,0 AS MutasiKredit FROM RingkasanAkun b 
  WHERE Tahun=@pThnAkhirDiRingkasan AND Bulan=@pBlnAkhirDiRingkasan AND 
  (b.IdAkun=", strLabaRugiBerjalan, 
  " OR b.IdAkun=", strLabaRugiLalu, @")
) X WHERE Debit<>0 OR Kredit<>0 GROUP BY IdAkun"),
                            pTahun, pBulan, pThnAkhirDiRingkasan,
                            pBlnAkhirDiRingkasan, pTglJurnal, pTglJurnalAddM1);
                        #endregion
                    #endregion

                    BlnAkhirDiRingkasan = Tmp.Month;
                    ThnAkhirDiRingkasan = Tmp.Year;
                }

                Tr.CommitTransaction();
            }
        }

        #region SqlPosisiAkun
        public string SqlPosisiAkun(DateTime TglLaporan, 
            string ParamId, List<FieldParam> Parameters)
        {
            return SqlPosisiAkun(TglLaporan, true, 
                ParamId, Parameters);
        }
        public string SqlPosisiAkun(DateTime TglLaporan,
            bool UpdateRingkasan, string ParamId,
            List<FieldParam> Parameters)
        {
            if (UpdateRingkasan || 
                TglLaporan.Year > ThnAkhirDiRingkasan ||
                TglLaporan.Month > BlnAkhirDiRingkasan && 
                TglLaporan.Year == ThnAkhirDiRingkasan) 
                Update(TglLaporan);

            if (TglLaporan.AddDays(1).Month != TglLaporan.Month)
            {
                if (BlnAkhirDiRingkasan == 0)
                {
                    return "SELECT IdAkun,Debit,Kredit FROM RingkasanAkun WHERE 1=0";
                }
                else
                {
                    Parameters.Add(new FieldParam(ParamId + "Tahun", 
                        TglLaporan.Year));
                    Parameters.Add(new FieldParam(ParamId + "Bulan", 
                        TglLaporan.Month));
                    return 
@"SELECT IdAkun,Debit,Kredit FROM RingkasanAkun WHERE Tahun=@Tahun 
AND Bulan=@Bulan".Replace("@","@" + ParamId);
                }
            }
            else
            {
                DateTime TglAwal = new DateTime(TglLaporan.Year,
                    TglLaporan.Month, 1);
                Parameters.Add(new FieldParam(ParamId + "TglAwal", 
                    TglAwal));
                Parameters.Add(new FieldParam(ParamId + "TglAkhir", 
                    TglLaporan.AddDays(1)));

                string strRangeTgl = "TglJurnal>=@TglAwal AND TglJurnal<@TglAkhir";

                if (BlnAkhirDiRingkasan == 0)
                {
                    return string.Concat(@"SELECT IdAkun,
SUM(Debit) as Debit,SUM(Kredit) as Kredit FROM (SELECT IdAkun,
Debit,Kredit FROM Jurnal a INNER JOIN JurnalDetil b ON 
a.NoJurnal=b.NoJurnal WHERE ", strRangeTgl, " UNION ALL SELECT ", 
                        strLabaRugiBerjalan, @" as IdAkun,Debit,Kredit 
FROM Jurnal a INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal
WHERE ", strRangeTgl, @" AND EXISTS (SELECT IdAkun FROM Akun WHERE 
b.IdAkun=Akun.IdAkun AND JenisAkun=", strJenisLabaRugi, 
")) X WHERE Debit<>0 OR Kredit<>0 GROUP BY IdAkun")
.Replace("@", "@" + ParamId);
                }
                else if (TglAwal.Month > 1)
                {
                    DateTime Tmp = TglLaporan.AddMonths(-1);
                    Parameters.Add(new FieldParam(ParamId + "TmpTahun",
                        Tmp.Year));
                    Parameters.Add(new FieldParam(ParamId + "TmpBulan",
                        Tmp.Month));

                    return string.Concat(@"SELECT IdAkun,
SUM(Debit) as Debit,SUM(Kredit) as Kredit FROM (SELECT IdAkun,Debit,
Kredit FROM Jurnal a INNER JOIN JurnalDetil b ON 
a.NoJurnal=b.NoJurnal WHERE ", strRangeTgl, " UNION ALL SELECT ", 
                        strLabaRugiBerjalan, @" as IdAkun,Debit,Kredit 
FROM Jurnal a INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal
WHERE ", strRangeTgl, @" AND EXISTS (SELECT IdAkun FROM Akun WHERE 
b.IdAkun=Akun.IdAkun AND JenisAkun=", strJenisLabaRugi, 
                        @") UNION ALL SELECT IdAkun,Debit,Kredit FROM 
RingkasanAkun WHERE Tahun=@TmpTahun AND Bulan=@TmpBulan) X WHERE 
Debit<>0 OR Kredit<>0 GROUP BY IdAkun").Replace("@", "@" + ParamId);
                }
                else
                {
                    DateTime Tmp = TglLaporan.AddMonths(-1);
                    Parameters.Add(new FieldParam(ParamId + "TmpTahun",
                        Tmp.Year));
                    Parameters.Add(new FieldParam(ParamId + "TmpBulan",
                        Tmp.Month));

                     return string.Concat(@"SELECT IdAkun,
SUM(Debit) as Debit,SUM(Kredit) as Kredit FROM (SELECT IdAkun,Debit,
Kredit FROM Jurnal a INNER JOIN JurnalDetil b ON 
a.NoJurnal=b.NoJurnal WHERE ", strRangeTgl, " UNION ALL SELECT ", 
                        strLabaRugiBerjalan, @" as IdAkun,Debit,Kredit 
FROM Jurnal a INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal 
WHERE ", strRangeTgl, @" AND EXISTS (SELECT IdAkun FROM Akun WHERE 
b.IdAkun=Akun.IdAkun AND JenisAkun=", strJenisLabaRugi, 
                        @") UNION ALL SELECT IdAkun,Debit,Kredit FROM 
RingkasanAkun b WHERE Tahun=@TmpTahun AND Bulan=@TmpBulan AND 
NOT EXISTS (SELECT IdAkun FROM Akun WHERE b.IdAkun=Akun.IdAkun
AND JenisAkun=", strJenisLabaRugi, ") AND b.IdAkun<>", 
                        strLabaRugiBerjalan, " AND b.IdAkun<>", 
                        strLabaRugiLalu, " UNION ALL SELECT ",
                        strLabaRugiLalu, @" as IdAkun,Debit,Kredit FROM 
RingkasanAkun b WHERE Tahun=@TmpTahun AND Bulan=@TmpBulan AND 
(b.IdAkun=", strLabaRugiBerjalan, " OR b.IdAkun=", strLabaRugiLalu,
")) X WHERE Debit<>0 OR Kredit<>0 GROUP BY IdAkun")
.Replace("@", "@" + ParamId);
                }
            }
        }
        #endregion

        #region PosisiAkun
        public decimal PosisiAkun(string IdAkun, DateTime TglLaporan)
        {
            return PosisiAkun(IdAkun, TglLaporan, true);
        }
        public decimal PosisiAkun(string IdAkun, DateTime TglLaporan, 
            bool UpdateRingkasan)
        {
            if (TglLaporan < BaseGL.SetingPerusahaan.TglMulaiSistemBaru)
                return SaldoAwalAkun.GetSaldoAwal(Dp, IdAkun);

            List<FieldParam> Parameters = new List<FieldParam>();
            string Query = SqlPosisiAkun(TglLaporan, 
                UpdateRingkasan, "1", Parameters);
            
            Parameters.Add(new FieldParam("0", IdAkun));
            Parameters.Add(new FieldParam("1", IdAkun + ".%"));
            return (decimal)Find.Value(string.Concat(
                "SELECT SUM(Debit-Kredit) FROM (", Query,
                ") A WHERE IdAkun=@0 OR IdAkun LIKE @1"), decimal.Zero,
                Parameters.ToArray());
        }
        #endregion

        #region SqlMutasiAkun
        public string SqlMutasiAkun(DateTime TglMulai, DateTime TglAkhir,
            string ParamId, List<FieldParam> Parameters)
        {
            return SqlMutasiAkun(TglMulai, TglAkhir, true,
                ParamId, Parameters);
        }
        public string SqlMutasiAkun(DateTime TglMulai, DateTime TglAkhir, 
            bool UpdateRingkasan, string ParamId,
            List<FieldParam> Parameters)
        {
            if (TglMulai > TglAkhir)
            {
                DateTime TglTemp = TglMulai;
                TglMulai = TglAkhir;
                TglAkhir = TglTemp;
            }
            if (UpdateRingkasan || 
                TglAkhir.Year > ThnAkhirDiRingkasan ||
                TglAkhir.Month > BlnAkhirDiRingkasan &&
                TglAkhir.Year == ThnAkhirDiRingkasan) 
                Update(TglAkhir);

            if (TglMulai.Year == TglAkhir.Year)
            {
                #region Tahun Sama
                if (TglMulai.Month == TglAkhir.Month)
                {
                    #region Tahun Sama, Bulan Sama
                    if (TglMulai.Day == 1 && TglAkhir.Day ==
                        DateTime.DaysInMonth(TglAkhir.Year, TglAkhir.Month))
                    {
                        Parameters.Add(new FieldParam(ParamId + "Tahun", 
                            TglAkhir.Year));
                        Parameters.Add(new FieldParam(ParamId + "Bulan", 
                            TglAkhir.Month));

                        return @"SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@Tahun 
AND Bulan=@Bulan".Replace("@", "@" + ParamId);
                    }
                    else
                    {
                        Parameters.Add(new FieldParam(ParamId + "TglMulai", 
                            TglMulai));
                        Parameters.Add(new FieldParam(ParamId + "TglAkhir", 
                            TglAkhir.AddDays(1)));

                        return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM JurnalDetil a INNER JOIN Jurnal b 
ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TglAkhir GROUP BY IdAkun".Replace("@", "@" + ParamId);
                    }
                    #endregion
                }
                else
                {
                    #region Tahun Sama, Bulan Berbeda
                    if (TglMulai.Day == 1 && TglAkhir.Day ==
                        DateTime.DaysInMonth(TglAkhir.Year, TglAkhir.Month))
                    {
                        Parameters.Add(new FieldParam(
                            ParamId + "TahunAkhir", TglAkhir.Year));
                        Parameters.Add(new FieldParam(
                            ParamId + "BulanAkhir", TglAkhir.Month));
                        Parameters.Add(new FieldParam(
                            ParamId + "BulanAwal", TglMulai.Month));
                        return @"SELECT IdAkun,SUM(MutasiDebit) as Debit,
SUM(MutasiKredit) as Kredit FROM RingkasanAkun WHERE 
Tahun=@TahunAkhir AND Bulan BETWEEN @BulanAwal AND 
@BulanAkhir GROUP BY IdAkun".Replace("@", "@" + ParamId);
                    }
                    else
                    {
                        if (TglMulai.AddMonths(1).Month == TglAkhir.Month)
                        {
                            #region Tahun Sama, Bulan Selisih 1 bulan
                            if (TglMulai.Day == 1)
                            {
                                Parameters.Add(new FieldParam(
                                    ParamId + "TahunAwal", TglMulai.Year));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAwal", TglMulai.Month));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir1",
                                    new DateTime(TglAkhir.Year, 
                                    TglAkhir.Month, 1)));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir2",
                                    TglAkhir.AddDays(1)));

                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@TahunAwal
AND Bulan=@BulanAwal UNION ALL SELECT IdAkun,Debit,Kredit FROM 
JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal 
WHERE TglJurnal>=@TglAkhir1 AND TglJurnal<@TglAkhir2) X 
GROUP BY IdAkun".Replace("@", "@" + ParamId);
                            }
                            else if (TglAkhir.Day == DateTime.DaysInMonth(
                                TglAkhir.Year, TglAkhir.Month))
                            {
                                Parameters.Add(new FieldParam(
                                    ParamId + "TahunAkhir", TglAkhir.Year));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAkhir", TglAkhir.Month));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglMulai1", TglMulai));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglMulai2",
                                    new DateTime(TglMulai.Year, 
                                    TglMulai.Month,
                                    DateTime.DaysInMonth(TglMulai.Year, 
                                    TglMulai.Month))));
                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@TahunAkhir
AND Bulan=@BulanAkhir UNION ALL SELECT IdAkun,Debit,Kredit FROM 
JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal 
WHERE TglJurnal>=@TglMulai1 AND TglJurnal<@TglMulai2) X GROUP BY IdAkun";
                            }
                            else
                            {
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglMulai", TglMulai));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir", TglAkhir));
                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM JurnalDetil a INNER JOIN Jurnal b ON 
a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TglAkhir GROUP BY IdAkun".Replace("@", "@" + ParamId);
                            }
                            #endregion
                        }
                        else
                        {
                            #region Tahun Sama, Bulan Selisih > 1 bulan
                            if (TglMulai.Day == 1)
                            {
                                Parameters.Add(new FieldParam(
                                    ParamId + "TahunMulai",
                                    TglMulai.Year));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanMulai",
                                    TglMulai.Month));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAkhir",
                                    TglAkhir.Month - 1));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir1",
                                    new DateTime(TglAkhir.Year, 
                                    TglAkhir.Month, 1)));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir2",
                                    TglAkhir.AddDays(1)));
                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@TahunMulai 
AND Bulan BETWEEN @BulanMulai AND @BulanAkhir UNION ALL 
SELECT IdAkun,Debit,Kredit FROM JurnalDetil a INNER JOIN 
Jurnal b ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglAkhir1
AND TglJurnal<@TglAkhir2) X GROUP BY IdAkun".Replace("@", "@" + ParamId);
                            }
                            else if (TglAkhir.Day == DateTime.DaysInMonth(
                                TglAkhir.Year, TglAkhir.Month))
                            {
                                DateTime TmpDate = TglMulai.AddMonths(1);
                                Parameters.Add(new FieldParam(
                                    ParamId + "TahunAkhir",
                                    TglAkhir.Year));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAkhir",
                                    TglAkhir.Month));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAwal",
                                    TglMulai.Month + 1));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglMulai",
                                    TglMulai));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TmpDate",
                                    new DateTime(TmpDate.Year, 
                                    TmpDate.Month, 1)));

                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@TahunAkhir 
AND Bulan BETWEEN @BulanAkhir AND @BulanAwal UNION ALL 
SELECT IdAkun,Debit,Kredit FROM JurnalDetil a INNER JOIN 
Jurnal b ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai
AND TglJurnal<@TmpDate) X GROUP BY IdAkun";
                            }
                            else
                            {
                                DateTime TmpDate = TglMulai.AddMonths(1);
                                Parameters.Add(new FieldParam(
                                    ParamId + "TahunAkhir",
                                    TglAkhir.Year));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAkhir",
                                    TglAkhir.Month - 1));
                                Parameters.Add(new FieldParam(
                                    ParamId + "BulanAwal",
                                    TglMulai.Month + 1));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglMulai",
                                    TglMulai));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TmpDate",
                                    new DateTime(TmpDate.Year, 
                                    TmpDate.Month,
                                    DateTime.DaysInMonth(TmpDate.Year, 
                                    TmpDate.Month))));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir1",
                                    new DateTime(TglAkhir.Year, 
                                    TglAkhir.Month, 1)));
                                Parameters.Add(new FieldParam(
                                    ParamId + "TglAkhir2",
                                    TglAkhir));
                                return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@TahunAkhir
AND Bulan BETWEEN @BulanAkhir AND @BulanAwal UNION ALL 
SELECT IdAkun,Debit,Kredit FROM JurnalDetil a INNER JOIN Jurnal b
ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TmpDate UNION ALL SELECT IdAkun,Debit,Kredit FROM 
JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal 
WHERE TglJurnal>=@TglAkhir1 AND TglJurnal<@TglAkhir2) X
GROUP BY IdAkun".Replace("@", "@" + ParamId);
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                #region Tahun Berbeda
                if (TglMulai.Day == 1 && 
                    TglAkhir.Day == DateTime.DaysInMonth(TglAkhir.Year, 
                    TglAkhir.Month))
                {
                    Parameters.Add(new FieldParam(ParamId + "ThnMulai",
                        TglMulai.Year));
                    Parameters.Add(new FieldParam(ParamId + "BlnMulai",
                        TglMulai.Month));
                    Parameters.Add(new FieldParam(ParamId + "ThnAkhir",
                        TglAkhir.Year));
                    Parameters.Add(new FieldParam(ParamId + "BlnAkhir",
                        TglAkhir.Month));

                    #region Mulai Tgl 1 s/d Akhir Bulan
                    if (TglMulai.AddYears(1).Year == TglAkhir.Year)
                        return @"SELECT IdAkun,SUM(MutasiDebit) as Debit,
SUM(MutasiKredit) as Kredit FROM RingkasanAkun WHERE Tahun=@ThnMulai
AND Bulan>=@BlnMulai OR Tahun=@ThnAkhir AND Bulan<=@BlnAkhir
GROUP BY IdAkun".Replace("@", "@" + ParamId);
                    else
                        return @"SELECT IdAkun,SUM(MutasiDebit) as Debit,
SUM(MutasiKredit) as Kredit FROM RingkasanAkun WHERE Tahun=@ThnMulai
AND Bulan>=@BlnMulai OR Tahun=@ThnAkhir AND Bulan<=@BlnAkhir
OR Tahun>@ThnMulai AND Tahun<@ThnAkhir GROUP BY IdAkun"
                            .Replace("@", "@" + ParamId);
                    #endregion
                }
                else
                {
                    if (TglMulai.Month == 12 && 
                        TglAkhir.Month == 1 && 
                        TglMulai.Year + 1 == TglAkhir.Year)
                    {
                        #region Selisih 1 bulan Des dan Jan
                        if (TglMulai.Day == 1)
                        {
                            Parameters.Add(new FieldParam(ParamId + "Thn1",
                                TglMulai.Year));
                            Parameters.Add(new FieldParam(ParamId + "Tgl1",
                                new DateTime(TglAkhir.Year, 1, 1)));
                            Parameters.Add(new FieldParam(ParamId + "Tgl2",
                                TglAkhir.AddDays(1)));

                            return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@Thn1
AND Bulan=12 UNION ALL SELECT IdAkun,Debit,Kredit FROM JurnalDetil a
INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal WHERE 
TglJurnal>=@Tgl1 AND TglJurnal<@Tgl2) X GROUP BY IdAkun"
                                .Replace("@", "@" + ParamId);
                        }
                        else if (TglAkhir.Day == 31)
                        {
                            Parameters.Add(new FieldParam(ParamId + "Thn1",
                                TglAkhir.Year));
                            Parameters.Add(new FieldParam(ParamId + "Tgl1",
                                TglMulai));
                            Parameters.Add(new FieldParam(ParamId + "Tgl2",
                                new DateTime(TglAkhir.Year, 1, 1)));

                            return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@Thn1 AND 
Bulan=1 UNION ALL SELECT IdAkun,Debit,Kredit FROM JurnalDetil a 
INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal WHERE 
TglJurnal>=@Tgl1 AND TglJurnal<@Tgl2) X GROUP BY IdAkun"
                                .Replace("@", "@" + ParamId);
                        }
                        else
                        {
                            Parameters.Add(new FieldParam(ParamId + "TglMulai",
                                TglMulai));
                            Parameters.Add(new FieldParam(ParamId + "TglAkhir",
                                TglAkhir.AddDays(1)));

                            return @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM JurnalDetil a INNER JOIN Jurnal b ON 
a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TglAkhir GROUP BY IdAkun".Replace("@", "@" + ParamId);
                        }
                        #endregion
                    }
                    else
                    {
                        string strSelisihTahun =
                            TglMulai.AddYears(1).Year == TglAkhir.Year ?
                            string.Empty : 
                            " OR Tahun>@ThnMulai AND Tahun<@ThnAkhir";

                        #region Selisih > 1 bulan
                        if (TglMulai.Day == 1)
                        {
                            DateTime TmpDate = TglAkhir.AddMonths(-1);
                            Parameters.Add(new FieldParam(ParamId + 
                                "ThnMulai", TglMulai.Year));
                            Parameters.Add(new FieldParam(ParamId + 
                                "BlnMulai", TglMulai.Month));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TmpThn", TmpDate.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "TmpBln", TmpDate.Month));
                            Parameters.Add(new FieldParam(ParamId + 
                                "ThnAkhir", TglAkhir.Year));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TglAkhir1", new DateTime(TglAkhir.Year, 
                                TglAkhir.Month, 1)));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TglAkhir2", TglAkhir.AddDays(1)));

                            return string.Concat(
                                @"SELECT IdAkun,SUM(Debit) as Debit,
SUM(Kredit) as Kredit FROM (SELECT IdAkun,MutasiDebit as Debit,
MutasiKredit as Kredit FROM RingkasanAkun WHERE Tahun=@ThnMulai AND 
Bulan>=@BlnMulai OR Tahun=@TmpThn AND Bulan<=@TmpBln", 
                                strSelisihTahun, @" UNION ALL 
SELECT IdAkun,Debit,Kredit FROM JurnalDetil a INNER JOIN Jurnal b
ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglAkhir1 AND 
TglJurnal<@TglAkhir2) X GROUP BY IdAkun").Replace("@", "@" + ParamId);
                        }
                        else if (TglAkhir.Day == DateTime.DaysInMonth(
                            TglAkhir.Year, TglAkhir.Month))
                        {
                            DateTime TmpDate = TglMulai.AddMonths(1);

                            Parameters.Add(new FieldParam(ParamId + 
                                "ThnMulai", TglMulai.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "ThnAkhir", TglAkhir.Year));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TmpThn", TmpDate.Year));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TmpBln", TmpDate.Month));
                            Parameters.Add(new FieldParam(ParamId +
                                "ThnAkhir", TglAkhir.Year));
                            Parameters.Add(new FieldParam(ParamId + 
                                "TglMulai", TglMulai));
                            Parameters.Add(new FieldParam(ParamId +
                                "TglMulai2", new DateTime(TglMulai.Year, 
                                TglMulai.Month,
                                DateTime.DaysInMonth(TglMulai.Year, 
                                TglMulai.Month)).AddDays(1)));

                            return string.Concat(@"SELECT IdAkun,
SUM(Debit) as Debit,SUM(Kredit) as Kredit FROM (SELECT IdAkun,
MutasiDebit as Debit,MutasiKredit as Kredit FROM RingkasanAkun 
WHERE Tahun=@TmpThn AND Bulan>=@TmpBln OR Tahun=@ThnAkhir 
AND Bulan<=@BlnAkhir", strSelisihTahun, @" UNION ALL SELECT IdAkun,
Debit,Kredit FROM JurnalDetil a INNER JOIN Jurnal b ON 
a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TglMulai2) X GROUP BY IdAkun").Replace("@", "@" + ParamId);
                        }
                        else
                        {
                            DateTime Tmp1 = TglMulai.AddMonths(1);
                            DateTime Tmp2 = TglAkhir.AddMonths(-1);

                            Parameters.Add(new FieldParam(ParamId +
                                "ThnMulai", TglMulai.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "smaThnAkhir", TglAkhir.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "Tmp1Thn", Tmp1.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "Tmp1Bln", Tmp1.Month));
                            Parameters.Add(new FieldParam(ParamId +
                                "Tmp2Thn", Tmp2.Year));
                            Parameters.Add(new FieldParam(ParamId +
                                "Tmp2Bln", Tmp2.Month));
                            Parameters.Add(new FieldParam(ParamId +
                                "TglMulai", TglMulai));
                            Parameters.Add(new FieldParam(ParamId +
                                "Tmp1", new DateTime(Tmp1.Year, 
                                Tmp1.Month, DateTime.DaysInMonth(
                                Tmp1.Year, Tmp1.Month))));
                            Parameters.Add(new FieldParam(ParamId +
                                "TglAkhir", new DateTime(TglAkhir.Year, 
                                TglAkhir.Month, 1)));
                            Parameters.Add(new FieldParam(ParamId +
                                "TglAkhir2", TglAkhir.AddDays(1)));

                            return string.Concat(@"SELECT IdAkun,
SUM(Debit) as Debit,SUM(Kredit) as Kredit FROM (SELECT IdAkun,
MutasiDebit as Debit,MutasiKredit as Kredit FROM RingkasanAkun WHERE 
Tahun=@Tmp1Thn AND Bulan>=@Tmp1Bln OR Tahun=@Tmp2Thn AND 
Bulan<=@Tmp2Bln", strSelisihTahun, @" UNION ALL SELECT IdAkun,Debit,
Kredit FROM JurnalDetil a INNER JOIN Jurnal b ON 
a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@Tmp1 UNION ALL SELECT IdAkun,Debit,Kredit FROM 
JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal
WHERE TglJurnal>=@TglAkhir AND TglJurnal<@TglAkhir2) X
GROUP BY IdAkun");
                        }
                        #endregion
                    }
                }
                #endregion
            }
        }
        #endregion

        #region GetMutasi Akun/Debit/Kredit Tanpa Dep dan Proyek
        private decimal GetMutasi(string FieldMutasi, string IdAkun,
            DateTime TglMulai, DateTime TglAkhir,
            bool UpdateRingkasan)
        {
            List<FieldParam> Parameters = new List<FieldParam>();
            string Query = SqlMutasiAkun(TglMulai, TglAkhir, UpdateRingkasan,
                "1", Parameters);

            Parameters.Add(new FieldParam("0", IdAkun));
            Parameters.Add(new FieldParam("1", IdAkun + ".%"));
            return (decimal)Find.Value(string.Concat(
                "SELECT SUM(", FieldMutasi, ") FROM (",
                Query, ") A WHERE IdAkun=@0 OR IdAkun LIKE @1"),
                decimal.Zero, Parameters.ToArray());
        }
        public decimal GetMutasiAkun(string IdAkun, 
            DateTime TglMulai, DateTime TglAkhir, bool UpdateRingkasan)
        {
            return GetMutasi("Debit-Kredit", IdAkun, TglMulai,
                TglAkhir, UpdateRingkasan);
        }
        public decimal GetMutasiDebit(string IdAkun, 
            DateTime TglMulai, DateTime TglAkhir,
            bool UpdateRingkasan)
        {
            return GetMutasi("Debit", IdAkun, TglMulai,
                TglAkhir, UpdateRingkasan);
        }
        public decimal GetMutasiKredit(string IdAkun, 
            DateTime TglMulai, DateTime TglAkhir,
            bool UpdateRingkasan)
        {
            return GetMutasi("Kredit", IdAkun, TglMulai,
                TglAkhir, UpdateRingkasan);
        }
        #endregion

        public string SqlDetilMutasiAkun(DateTime TglMulai,
            DateTime TglAkhir, bool UpdateRingkasan, bool MultiMataUang,
            string ParamId, List<FieldParam> Parameters)
        {
            if (TglMulai > TglAkhir)
            {
                DateTime TglTemp = TglMulai;
                TglMulai = TglAkhir;
                TglAkhir = TglTemp;
            }
            if (UpdateRingkasan ||
                TglAkhir.Year > ThnAkhirDiRingkasan ||
                TglAkhir.Month > BlnAkhirDiRingkasan &&
                TglAkhir.Year == ThnAkhirDiRingkasan)
                Update(TglAkhir);

            Parameters.Add(new FieldParam(ParamId + "TglMulai", 
                TglMulai));
            Parameters.Add(new FieldParam(ParamId + "TglAkhir", 
                TglAkhir.AddDays(1)));

            if (MultiMataUang)
                return @"SELECT IdDepartemen,IdProyek,IdAkun,
a.KodeMataUang,DebitKurs,KreditKurs FROM JurnalDetil a 
INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal WHERE 
TglJurnal>=@TglMulai AND TglJurnal<@TglAkhir".Replace("@", "@" + ParamId);
            else
                return @"SELECT IdDepartemen,IdProyek,IdAkun,
Debit,Kredit FROM JurnalDetil a 
INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal WHERE 
TglJurnal>=@TglMulai AND TglJurnal<@TglAkhir".Replace("@", "@" + ParamId);
        }

        #region GetMutasi Akun/Debit/Kredit dgn Dep dan Proyek
        private decimal GetMutasi(string FieldMutasi,
            string IdDep, string IdProyek, string IdAkun, 
            DateTime TglMulai, DateTime TglAkhir, bool UpdateRingkasan)
        {
            List<FieldParam> Parameters = new List<FieldParam>();

            string Query = SqlDetilMutasiAkun(TglMulai, TglAkhir, 
                UpdateRingkasan, false, "1", Parameters);
            Parameters.Add(new FieldParam("IdAkun", IdAkun));
            Parameters.Add(new FieldParam("IdAkun2", IdAkun + ".%"));

            string Condition = string.Empty;
            if (IdDep.Length > 0)
            {
                Parameters.Add(new FieldParam("IdDep", IdDep));
                Parameters.Add(new FieldParam("IdDep2", IdDep + ".%"));
                Condition =
                    " AND (IdDepartemen=@IdDep OR IdDepartemen LIKE @IdDep2)";
            }
            if (IdProyek.Length > 0)
            {
                Parameters.Add(new FieldParam("IdProyek", IdProyek));
                Parameters.Add(new FieldParam("IdProyek2", IdProyek + ".%"));

                Condition = string.Concat(Condition,
                    @" AND (IdProyek=@IdProyek OR IdProyek LIKE @IdProyek2)");
            }
            return (decimal)Find.Value(string.Concat(
                "SELECT SUM(", FieldMutasi, ") FROM (", Query, 
                ") A WHERE (IdAkun=@IdAkun OR IdAkun LIKE @IdAkun2)", 
                Condition), decimal.Zero, Parameters.ToArray());
        }
        public decimal GetMutasiAkun(string IdDep, string IdProyek,
            string IdAkun, DateTime TglMulai, DateTime TglAkhir,
            bool UpdateRingkasan)
        {
            return GetMutasi("Debit-Kredit", IdDep, IdProyek,
                IdAkun, TglMulai, TglAkhir, UpdateRingkasan);
        }
        public decimal GetMutasiDebit(string IdDep, string IdProyek, 
            string IdAkun, DateTime TglMulai, DateTime TglAkhir, 
            bool UpdateRingkasan)
        {
            return GetMutasi("Debit", IdDep, IdProyek,
                IdAkun, TglMulai, TglAkhir, UpdateRingkasan);
        }
        public decimal GetMutasiKredit(string IdDep, string IdProyek, 
            string IdAkun, DateTime TglMulai, DateTime TglAkhir, 
            bool UpdateRingkasan)
        {
            return GetMutasi("Kredit", IdDep, IdProyek,
                IdAkun, TglMulai, TglAkhir, UpdateRingkasan);
        }
        #endregion

        public string SqlMutasiAkunPerDepartemen(DateTime TglMulai, DateTime TglAkhir, 
            string IdDepartemen, string ParamId, List<FieldParam> Parameters)
        {
            Parameters.Add(new FieldParam(ParamId + "TglMulai", TglMulai));
            Parameters.Add(new FieldParam(ParamId + "TglAkhir", TglAkhir.AddDays(1)));
            Parameters.Add(new FieldParam(ParamId + "IdDep1", IdDepartemen + ".%"));
            Parameters.Add(new FieldParam(ParamId + "IdDep2", IdDepartemen));

            return @"SELECT TglJurnal,a.IdAkun,Debit,Kredit FROM 
JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal WHERE TglJurnal>=@TglMulai AND 
TglJurnal<@TglAkhir AND (a.IdDepartemen LIKE @IdDep1 OR a.IdDepartemen = @IdDep2)".Replace(
                "@", "@" + ParamId);
        }

        public string SqlDetilMutasiAkunPerDepartemen(DateTime TglMulai, DateTime TglAkhir,
            string IdDepartemen, string ParamId, List<FieldParam> Parameters)
        {
            Parameters.Add(new FieldParam(ParamId + "TglMulai", TglMulai));
            Parameters.Add(new FieldParam(ParamId + "TglAkhir", TglAkhir.AddDays(1)));
            Parameters.Add(new FieldParam(ParamId + "IdDep1", IdDepartemen + ".%"));
            Parameters.Add(new FieldParam(ParamId + "IdDep2", IdDepartemen));

            if (BaseGL.SetingPerusahaan.MultiMataUang)
                return @"SELECT TglJurnal,a.NoJurnal,NamaDepartemen,NamaProyek,JenisDokSumber,
NoDokSumber,c.IdAkun,NoAkun,NamaAkun,Debit,Kredit,a.KodeMataUang,DebitKurs,KreditKurs,
a.Keterangan FROM (((JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal) 
INNER JOIN Akun c ON a.IdAkun = c.IdAkun) LEFT JOIN Departemen d ON 
a.IdDepartemen=d.IdDepartemen) LEFT JOIN Proyek e ON a.IdProyek=e.IdProyek 
WHERE TglJurnal>=@TglMulai AND TglJurnal<@TglAkhir AND (a.IdDepartemen LIKE @IdDep1 OR 
a.IdDepartemen=@IdDep2)".Replace("@", "@" + ParamId);
            else
                return @"SELECT TglJurnal,a.NoJurnal,NamaDepartemen,NamaProyek,JenisDokSumber,
NoDokSumber,c.IdAkun,NoAkun,NamaAkun,Debit,Kredit,a.Keterangan FROM 
(((JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal) INNER JOIN Akun c ON 
a.IdAkun=c.IdAkun) LEFT JOIN Departemen d ON a.IdDepartemen=d.IdDepartemen) LEFT JOIN 
Proyek e ON a.IdProyek=e.IdProyek WHERE TglJurnal>=@TglMulai AND TglJurnal<@TglAkhir 
AND (a.IdDepartemen LIKE @IdDep1 OR a.IdDepartemen=@IdDep2)".Replace("@", "@" + ParamId);
        }

        // Belum Dicek....
        public string SqlMutasiAkunPerProyek(DateTime TglMulai, DateTime TglAkhir, 
            string IdProyek)
        {
            string Query;
            Query = string.Concat(
              "SELECT TglJurnal,a.IdAkun,Debit,Kredit FROM ",
              " JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal ",
              " WHERE TglJurnal BETWEEN ", FormatSqlValue(TglMulai),
              " AND ", FormatSqlValue(TglAkhir), " AND (a.IdProyek LIKE ", FormatSqlValue(IdProyek + ".%"), " OR a.IdProyek = ", FormatSqlValue(IdProyek), ")");
            return Query;
        }
        public string SqlDetilMutasiAkunPerProyek(DateTime TglMulai, DateTime TglAkhir, 
            string IdProyek)
        {
            string Query;
            if (BaseGL.SetingPerusahaan.MultiMataUang)
            {
                Query = string.Concat(
                       "SELECT TglJurnal,a.NoJurnal,NamaDepartemen,NamaProyek,JenisDokSumber,NoDokSumber,c.IdAkun,NoAkun,NamaAkun,Debit,Kredit,a.KodeMataUang,DebitKurs,KreditKurs,a.Keterangan FROM ",
                       " (((JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal) LEFT JOIN Akun c ON a.IdAkun = c.IdAkun ) LEFT JOIN Proyek d ON a.IdProyek = d.IdProyek ) INNER JOIN Departemen e ON a.IdDepartemen = e.IdDepartemen",
                       " WHERE TglJurnal BETWEEN ", FormatSqlValue(TglMulai),
                       " AND ", FormatSqlValue(TglAkhir), " AND (a.IdProyek LIKE ", FormatSqlValue(IdProyek + ".%"), " OR a.IdProyek = ", FormatSqlValue(IdProyek), ")");
            }
            else
            {
                Query = string.Concat(
                  "SELECT TglJurnal,a.NoJurnal,NamaDepartemen,NamaProyek,JenisDokSumber,NoDokSumber,c.IdAkun,NoAkun,NamaAkun,Debit,Kredit,a.Keterangan FROM ",
                    " (((JurnalDetil a INNER JOIN Jurnal b ON a.NoJurnal=b.NoJurnal) LEFT JOIN Akun c ON a.IdAkun = c.IdAkun ) LEFT JOIN Proyek d ON a.IdProyek = d.IdProyek ) INNER JOIN Departemen e ON a.IdDepartemen = e.IdDepartemen",
                    " WHERE TglJurnal BETWEEN ", FormatSqlValue(TglMulai),
                  " AND ", FormatSqlValue(TglAkhir), " AND (a.IdProyek LIKE ", FormatSqlValue(IdProyek + ".%"), " OR a.IdProyek = ", FormatSqlValue(IdProyek), ")");
            }
            return Query;
        }

        public string SqlPosisiAkunPerMataUang(DateTime TglLaporan, DateTime TglAkhirTransaksi)
        {
            if (TglAkhirTransaksi < TglLaporan)
            {
                return string.Concat(
               "SELECT IdAkun,DebitKurs,KreditKurs FROM ",
               " RingkasanAkunKurs WHERE Tahun = ",
               FormatSqlValue(TglAkhirTransaksi.Year),
               " AND Bulan = ", FormatSqlValue(TglAkhirTransaksi.Month));
            }
            else
            {
                if (TglLaporan.AddDays(1).Month != TglLaporan.Month)
                {
                    return string.Concat(
                        "SELECT IdAkun,DebitKurs,KreditKurs FROM ",
                        " RingkasanAkunKurs WHERE Tahun = ",
                        FormatSqlValue(TglLaporan.Year),
                        " AND Bulan = ", FormatSqlValue(TglLaporan.Month));
                }
                else
                {
                    int LastBulan = TglLaporan.AddMonths(-1).Month;
                    int LastTahun = TglLaporan.AddMonths(-1).Year;
                    int mBulan = TglLaporan.Month;
                    int mTahun = TglLaporan.Year;
                    string Query;
                    Query =
                    string.Concat(
                   " SELECT IdAkun,SUM(DebitKurs) as DebitKurs,SUM(KreditKurs) as KreditKurs FROM (",
                    "SELECT IdAkun,DebitKurs,KreditKurs FROM Jurnal a INNER JOIN JurnalDetil b ON a.NoJurnal=b.NoJurnal WHERE ",
                    "TglJurnal BETWEEN ", FormatSqlValue(new DateTime(mTahun, mBulan, 1)),
                    " AND ", FormatSqlValue(TglLaporan),
                    " UNION ALL ",
                    " SELECT IdAkun,DebitKurs,KreditKurs FROM RingkasanAkunKurs WHERE ",
                    " Tahun = ", FormatSqlValue(LastTahun),
                    " AND Bulan =", FormatSqlValue(LastBulan),
                    " ) X WHERE DebitKurs<>0 OR KreditKurs <> 0 GROUP BY IdAkun");
                    return Query;
                }
            }
        }

        public decimal PosisiAkunPerMataUang(string IdAkun, DateTime TglLaporan)
        {
            string Query = SqlPosisiAkunPerMataUang(TglLaporan, TglLaporan);
            return (decimal)Find.Value(
                string.Concat(
                "SELECT SUM(DebitKurs-KreditKurs) FROM (",
                Query, ") A WHERE IdAkun = ", FormatSqlValue(IdAkun),
                " OR IdAkun LIKE  ", FormatSqlValue(IdAkun + ".%")), decimal.Zero);
        }
        public decimal PosisiAkunPerMataUang(string IdAkun, DateTime TglLaporan, 
            DateTime TglAkhirTransaksi)
        {
            string Query = SqlPosisiAkunPerMataUang(TglLaporan, TglAkhirTransaksi);
            return (decimal)Find.Value(
                string.Concat(
                "SELECT SUM(DebitKurs-KreditKurs) FROM (",
                Query, ") A WHERE IdAkun = ", FormatSqlValue(IdAkun),
                " OR IdAkun LIKE  ", FormatSqlValue(IdAkun + ".%")), decimal.Zero);
        }
    }

    [Relation(typeof(Akun))]
    internal class RingkasanAkunKurs : ParentEntity
    {
        // Data diupdate melalui RingkasanAkun
        [DataTypeInteger, PrimaryKey]
        public int Tahun;
        [DataTypeInteger, PrimaryKey]
        public int Bulan;
        [DataTypeVarChar(50), PrimaryKey]
        public string IdAkun;
        [DataTypeDecimal]
        public decimal DebitKurs;
        [DataTypeDecimal]
        public decimal KreditKurs;
        [DataTypeDecimal]
        public decimal MutasiDebitKurs;
        [DataTypeDecimal]
        public decimal MutasiKreditKurs;
    }
}
