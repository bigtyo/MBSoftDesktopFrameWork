using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;

namespace BxEventClient.Warning
{
    [ViewEntity(true)]
    public class GetTglAkhirWarning : ViewEntity
    {
        public override string GetSqlDdl()
        {
            return @"DECLARE @TotalHari AS INT, @Dw AS INT
SET @TotalHari=0

WHILE @TotalHari<@JmlHari
BEGIN
    SELECT @TglMulai = @TglMulai + 1, @Dw = DATEPART(WeekDay, @TglMulai)
	IF @Dw<>1 AND @Dw<>7 AND NOT EXISTS(SELECT TglLibur FROM HariLiburDetil WHERE TglLibur=@Tglmulai)
		SET @TotalHari = @TotalHari + 1
END
SET @RetVal=@TglMulai";
        }

        public override FieldParam[] GetParams()
        {
            return new FieldParam[] 
            { 
                new FieldParam("TglMulai", DataType.Date),
                new FieldParam("JmlHari", DataType.Integer)
            };
        }

        public override FieldParam GetReturnType()
        {
            return new FieldParam("RetVal", DataType.Date);
        }
    }
}
