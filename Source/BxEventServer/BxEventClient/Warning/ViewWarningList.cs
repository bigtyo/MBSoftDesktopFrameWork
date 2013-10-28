using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;

namespace BxEventClient.Warning
{
    [ViewEntity(true, typeof(WarningMaster), typeof(WarningResponsible))]
    public class ViewWarningList : ViewEntity
    {
        public string WarningName;
        public string KodeDepartemen;
        public string KodeBagian;
        public string KodeSeksi;
        public string KodeGudang;
        public string ResponsibleUser;
        public string WarningQuery;
        public string NumDayToWarningLetter;
        public bool AutoWarningLetter;
        public string TableSourceName;

        public override string GetSqlDdl()
        {
            return @"SELECT CASE LEN(Pesan) WHEN 0 THEN wm.WarningName ELSE 
              Pesan END WarningName,KodeDepartemen,KodeBagian,KodeSeksi,KodeGudang,
              ResponsibleUser,WarningQuery,NumDayToWarningLetter,AutoWarningLetter,
              TableSourceName FROM WarningMaster wm
              INNER JOIN WarningResponsible wr ON wm.WarningName=wr.WarningName";
        }
    }
}
