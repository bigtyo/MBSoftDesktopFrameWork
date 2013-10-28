using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace BxEventServer
{
    [ViewEntity(true)]
    public class GetJumlahJamDPB : ViewEntity
    {
        public override string GetSqlDdl()
        {
            return @"SELECT @JmlHari=CASE @Dep
                    WHEN 'KEUANGAN' THEN 1 
                    WHEN 'PROD' THEN
                        (CASE @Seksi 
                            WHEN 'ASSEMBLING' THEN 2
                            WHEN 'GUDANG' THEN 2
                            WHEN 'UTILITY' THEN 3
                        ELSE 1 END)
                    WHEN 'R&D' THEN
                        (CASE @Seksi
                            WHEN 'PENGEMBANGAN MESIN' THEN 2
                            WHEN 'PENGEMBANGAN METER AIR' THEN 2
                            WHEN 'QC' THEN 2
                        ELSE 1 END)
                    ELSE 1 END";
        }

        public override FieldParam[] GetParams()
        {
            return new FieldParam[] 
            { 
                new FieldParam("Dep",DataType.VarChar),
                new FieldParam("Bag", DataType.VarChar),
                new FieldParam("Seksi", DataType.VarChar)
            };
        }

        public override FieldParam GetReturnType()
        {
            return new FieldParam("JmlHari", DataType.Integer);
        }
    }
}
