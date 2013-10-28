using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraGL.Master;

namespace SentraGL.Document
{
    [ViewEntity(true, typeof(PenerimaanKasUmum), typeof(PengeluaranKasUmum))]
    public class ViewTransaksiKas : ViewEntity
    {
        [DataTypeVarChar(50)] public string NamaModul;
        [DataTypeVarChar(50)] public string NoTransaksi;
        [DataTypeInteger] public int NoUrut;
        [DataTypeVarChar(50)] public string IdKas;
        [DataTypeVarChar(50)] public string NoKuitansi;
        [DataTypeDate] public DateTime TglTransaksi;
        [DataTypeVarChar(150)] public string Keperluan;
        [DataTypeVarChar(150)] public string Keterangan;
        [DataTypeVarChar(50)] public string IdDepartemen;
        [DataTypeVarChar(50)] public string IdProyek;
        [DataTypeVarChar(50)] public enJenisArusKas JenisArusKas;
        [DataTypeVarChar(100)] public string JenisTransaksiKas;
        [DataTypeDecimal] public decimal Nilai;
        [DataTypeDateTime] public DateTime TglJamUpdate;

        public override string GetSqlDdl()
        {
            return string.Concat("SELECT ",
                FormatSqlValue(PenerimaanKasUmum.ModuleName),
                @",d.NoPenerimaanKas,NoUrut,IdKas,NoKuitansi,TglKliring,
                Keperluan,Keterangan,IdDepartemen,IdProyek,(SELECT JenisArusKas
                FROM JenisPenerimaanKas jp WHERE jp.JenisPenerimaan=
                d.JenisPenerimaan) AS JenisArusKas,JenisPenerimaan,NilaiPenerimaan,
                TglJamUpdate FROM PenerimaanKasUmum p INNER JOIN 
                PenerimaanKasUmumDetil d ON p.NoPenerimaanKas=d.NoPenerimaanKas
                WHERE StatusTransaksi=", 
                FormatSqlValue(enStatusTransaksiKas._),
                " OR StatusTransaksi=", 
                FormatSqlValue(enStatusTransaksiKas.SudahKliring));
        }

    }
}
