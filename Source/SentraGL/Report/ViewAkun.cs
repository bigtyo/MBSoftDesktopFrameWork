using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;

namespace SentraGL.Report
{
    [ViewEntity(true, typeof(Akun))]
    public class ViewPosisiAkun : ViewEntity
    {
        [DataTypeInteger] public int Tahun;
        [DataTypeVarChar(50)] public string IdInduk;
        [DataTypeVarChar(50)] public string IdAkun;
        [DataTypeVarChar(50)] public string NoAkun;
        [DataTypeVarChar(50)] public string NamaAkun;
        [DataTypeVarChar(50)] public string JenisAkun;
        [DataTypeVarChar(50)] public string KelompokAkun;
        [DataTypeInteger] public int UrutanKelompok;
        [DataTypeVarChar(50)] public string UrutanCetak;
        [DataTypeInteger] public int LevelAkun;
        [DataTypeBoolean] public bool Posting;
        [DataTypeBoolean] public bool Aktif;
        [DataTypeDecimal] public decimal Januari;
        [DataTypeDecimal] public decimal Februari;
        [DataTypeDecimal] public decimal Maret;
        [DataTypeDecimal] public decimal April;
        [DataTypeDecimal] public decimal Mei;
        [DataTypeDecimal] public decimal Juni;
        [DataTypeDecimal] public decimal Juli;
        [DataTypeDecimal] public decimal Agustus;
        [DataTypeDecimal] public decimal September;
        [DataTypeDecimal] public decimal Oktober;
        [DataTypeDecimal] public decimal Nopember;
        [DataTypeDecimal] public decimal Desember;

        public override string GetSqlDdl()
        {
            return @"SELECT b.Tahun,IdInduk,a.IdAkun,NoAkun,
NamaAkun,JenisAkun,KelompokAkun,UrutanKelompok,UrutanCetak,
LevelAkun,Posting,Aktif,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=1) as Januari,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=2) as Februari,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=3) as Maret,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=4) as April,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=5) as Mei,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=6) as Juni,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=7) as Juli,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=8) as Agustus,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=9) as September,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=10) as Oktober,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=11) as Nopember,

(SELECT SUM(Debit)-SUM(Kredit) FROM RingkasanAkun c WHERE 
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR c.IdAkun 
LIKE a.IdAkun+'.%') AND Bulan=12) as Desember

FROM Akun a,(SELECT DISTINCT Tahun FROM RingkasanAkun) b";
        }
    }

    [ViewEntity(true, typeof(Akun))]
    public class ViewPosisiAkunDetil : ViewEntity
    {
        [DataTypeInteger] public int Tahun;
        [DataTypeVarChar(50)] public string IdInduk;
        [DataTypeVarChar(50)] public string IdAkun;
        [DataTypeVarChar(50)] public string NoAkun;
        [DataTypeVarChar(50)] public string NamaAkun;
        [DataTypeVarChar(50)] public string JenisAkun;
        [DataTypeVarChar(50)] public string KelompokAkun;
        [DataTypeInteger] public int UrutanKelompok;
        [DataTypeBoolean] public bool Aktif;
        [DataTypeDecimal] public decimal Januari;
        [DataTypeDecimal] public decimal Februari;
        [DataTypeDecimal] public decimal Maret;
        [DataTypeDecimal] public decimal April;
        [DataTypeDecimal] public decimal Mei;
        [DataTypeDecimal] public decimal Juni;
        [DataTypeDecimal] public decimal Juli;
        [DataTypeDecimal] public decimal Agustus;
        [DataTypeDecimal] public decimal September;
        [DataTypeDecimal] public decimal Oktober;
        [DataTypeDecimal] public decimal Nopember;
        [DataTypeDecimal] public decimal Desember;

        public override string GetSqlDdl()
        {
            return @"SELECT b.Tahun,IdInduk,a.IdAkun,NoAkun,
NamaAkun,JenisAkun,KelompokAkun,UrutanKelompok,Aktif,

(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=1) as Januari,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=2) as Februari,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=3) as Maret,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=4) as April,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=5) as Mei,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=6) as Juni,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=7) as Juli,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=8) as Agustus,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=9) as September,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=10) as Oktober,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=11) as Nopember,
(SELECT Debit-Kredit FROM RingkasanAkun c WHERE c.Tahun=b.Tahun AND 
(c.IdAkun=a.IdAkun) AND Bulan=12) as Desember 

FROM Akun a,(SELECT DISTINCT Tahun FROM RingkasanAkun) b 
WHERE a.Posting<>0";
        }
    }

    [ViewEntity(true, typeof(Akun))]
    public class ViewMutasiAkun : ViewEntity
    {
        [DataTypeInteger] public int Tahun;
        [DataTypeVarChar(50)] public string IdInduk;
        [DataTypeVarChar(50)] public string IdAkun;
        [DataTypeVarChar(50)] public string NoAkun;
        [DataTypeVarChar(50)] public string NamaAkun;
        [DataTypeVarChar(50)] public string JenisAkun;
        [DataTypeVarChar(50)] public string KelompokAkun;
        [DataTypeInteger] public int UrutanKelompok;
        [DataTypeVarChar(50)] public string UrutanCetak;
        [DataTypeInteger] public int LevelAkun;
        [DataTypeBoolean] public bool Posting;
        [DataTypeBoolean] public bool Aktif;
        [DataTypeDecimal] public decimal Januari;
        [DataTypeDecimal] public decimal Februari;
        [DataTypeDecimal] public decimal Maret;
        [DataTypeDecimal] public decimal April;
        [DataTypeDecimal] public decimal Mei;
        [DataTypeDecimal] public decimal Juni;
        [DataTypeDecimal] public decimal Juli;
        [DataTypeDecimal] public decimal Agustus;
        [DataTypeDecimal] public decimal September;
        [DataTypeDecimal] public decimal Oktober;
        [DataTypeDecimal] public decimal Nopember;
        [DataTypeDecimal] public decimal Desember;

        public override string GetSqlDdl()
        {
            return @"SELECT b.Tahun,IdInduk,a.IdAkun,NoAkun,
NamaAkun,JenisAkun,KelompokAkun,UrutanKelompok,UrutanCetak,
LevelAkun,Posting,Aktif,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c 
WHERE c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=1) as Januari,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c 
WHERE c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=2) as Februari,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c 
WHERE c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=3) as Maret,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c 
WHERE c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=4) as April,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c 
WHERE c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=5) as Mei,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=6) as Juni,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=7) as Juli,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=8) as Agustus,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=9) as September,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=10) as Oktober,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=11) as Nopember,

(SELECT SUM(MutasiDebit)-SUM(MutasiKredit) FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun OR 
c.IdAkun LIKE a.IdAkun+'.%') AND Bulan=12) as Desember 

FROM Akun a,(SELECT DISTINCT Tahun FROM RingkasanAkun) b";
        }
    }

    [ViewEntity(true, typeof(Akun))]
    public class ViewMutasiAkunDetil : ViewEntity
    {
        [DataTypeInteger] public int Tahun;
        [DataTypeVarChar(50)] public string IdInduk;
        [DataTypeVarChar(50)] public string IdAkun;
        [DataTypeVarChar(50)] public string NoAkun;
        [DataTypeVarChar(50)] public string NamaAkun;
        [DataTypeVarChar(50)] public string JenisAkun;
        [DataTypeVarChar(50)] public string KelompokAkun;
        [DataTypeInteger] public int UrutanKelompok;
        [DataTypeBoolean] public bool Aktif;
        [DataTypeDecimal] public decimal Januari;
        [DataTypeDecimal] public decimal Februari;
        [DataTypeDecimal] public decimal Maret;
        [DataTypeDecimal] public decimal April;
        [DataTypeDecimal] public decimal Mei;
        [DataTypeDecimal] public decimal Juni;
        [DataTypeDecimal] public decimal Juli;
        [DataTypeDecimal] public decimal Agustus;
        [DataTypeDecimal] public decimal September;
        [DataTypeDecimal] public decimal Oktober;
        [DataTypeDecimal] public decimal Nopember;
        [DataTypeDecimal] public decimal Desember;

        public override string GetSqlDdl()
        {
            return @"SELECT b.Tahun,IdInduk,a.IdAkun,NoAkun,
NamaAkun,JenisAkun,KelompokAkun,UrutanKelompok,Aktif,

(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=1) as Januari,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=2) as Februari,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=3) as Maret,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=4) as April,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=5) as Mei,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=6) as Juni,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=7) as Juli,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=8) as Agustus,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=9) as September,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=10) as Oktober,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=11) as Nopember,
(SELECT MutasiDebit-MutasiKredit FROM RingkasanAkun c WHERE
c.Tahun=b.Tahun AND (c.IdAkun=a.IdAkun) AND Bulan=12) as Desember 

FROM Akun a,(SELECT DISTINCT Tahun FROM RingkasanAkun) b WHERE 
a.Posting<>0";
        }
    }

    [ViewEntity(true, typeof(SaldoAwalAkun), typeof(NilaiTukarSaldoAwal))]
    public class ViewSaldoAwalKurs : ViewEntity
    {
        [DataTypeVarChar(50)] public string IdAkun;
        [DataTypeDecimal] public decimal SaldoAwal;
        [DataTypeDecimal] public decimal NilaiTukar;

        public override string GetSqlDdl()
        {
            return @"SELECT S.IdAkun,SaldoAwal,NilaiTukar FROM (SaldoAwalAkun S 
INNER JOIN Akun A ON S.IdAkun=A.IdAkun) LEFT JOIN NilaiTukarSaldoAwal N ON 
A.KodeMataUang=N.KodeMataUang WHERE AkunMoneter<>0 AND MataUangDasar=0";
        }
    }
}
