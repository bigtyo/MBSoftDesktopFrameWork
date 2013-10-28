using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using SentraUtility;
using SentraSolutionFramework.Persistance;

namespace SentraSecurity
{
    [NoKeyEntity]
    [Index("LogTime")]
    [TableName("_System_UserLog")]
    public class UserLog : ParentEntity
    {
        [DataTypeTimeStamp]
        public DateTime LogTime;
        [DataTypeVarChar(50)]
        public string UserName;
        [DataTypeVarChar(50)]
        public string RoleName;
        [DataTypeVarChar(100)]
        public string DocumentId;
        [DataTypeVarChar(200)]
        public string ActionId;
        [DataTypeVarChar(100)]
        public string DocumentNo;
        [DataTypeVarChar(2000)]
        public string Description;

        public static string StrNewAction = "Baru";
        public static string StrEditAction = "Edit";
        public static string StrDeleteAction = "Hapus";

        public static void AddLog(string DocumentId, string ActionId,
            string DocumentNo, string Description)
        {
            UserLog log = new UserLog();
            log.UserName = BaseSecurity.CurrentLogin.CurrentUser;
            log.RoleName = BaseSecurity.CurrentLogin.CurrentRole;
            log.DocumentId = BaseUtility.SplitName(DocumentId);
            log.ActionId = ActionId;
            log.DocumentNo = DocumentNo;
            log.Description = Description;
            log.SaveNew(false, false);
        }

        public static void CheckAutoClearLog()
        {
            TableDef td = MetaData.GetTableDef<UserLog>();
            DataPersistance Dp = td.GetDataPersistance(BaseFramework.GetDefaultDp());
            Dp.ValidateTableDef<UserLog>();
            Dp.ExecuteNonQuery("DELETE FROM _System_UserLog WHERE LogTime<@0",
                new FieldParam("0", DateTime.Today.AddMonths(-LogAge)));
        }

        public static int LogAge
        {
            get { return BaseFramework.GetDefaultDp()
                .GetVariable<int>("System", "LogAge", 12); }
            set { BaseFramework.GetDefaultDp()
                .SetVariable("System", "LogAge", value); }
        }

        public static void ClearLog()
        {
            TableDef td = MetaData.GetTableDef<UserLog>();
            DataPersistance Dp = td.GetDataPersistance(BaseFramework.GetDefaultDp());
            Dp.ValidateTableDef<UserLog>();
            Dp.ExecuteNonQuery("DELETE FROM _System_UserLog");
        }

        public static Dictionary<string, EnableLogAttribute> LogList =
            new Dictionary<string, EnableLogAttribute>();

        public static EnableLogAttribute GetLogAtr(Type ObjType)
        {
            EnableLogAttribute RetVal;

            if (!LogList.TryGetValue(ObjType.Name, out RetVal))
            {
                EnableLogAttribute[] _ListType = (EnableLogAttribute[])
                    ObjType.GetCustomAttributes(typeof(EnableLogAttribute), true);
                if (_ListType.Length == 0)
                    RetVal = new EnableLogAttribute(enLogType.LogNone);
                else
                    RetVal = _ListType[0];
                LogList.Add(ObjType.Name, RetVal);
            }
            return RetVal;
        }

        public static void EnableLog<TEntity>(enLogType LogType)
            where TEntity : BusinessEntity
        {
            EnableLog(typeof(TEntity), LogType);
        }

        public static void EnableLog(Type EntityType, enLogType LogType)
        {
            EnableLogAttribute RetVal;

            if (!LogList.TryGetValue(EntityType.Name, out RetVal))
            {
                RetVal = new EnableLogAttribute(LogType);
                LogList.Add(EntityType.Name, RetVal);
            }
            RetVal.LogType = LogType;
        }
    }
}
