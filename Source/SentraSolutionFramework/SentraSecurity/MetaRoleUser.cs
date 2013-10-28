using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Data;
using SentraSolutionFramework;

namespace SentraSecurity
{
    [TableName("_System_Role")]
    public class Role : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50), EmptyError]
        public string ProductName;
        [PrimaryKey, DataTypeVarChar(100), EmptyError]
        public string RoleName;
        
        [DataTypeBoolean]
        public bool IsActive;
        [DataTypeBoolean]
        public bool UseDateLimit;
        [DataTypeDate]
        public DateTime StartDate;
        [DataTypeDate]
        public DateTime EndDate;

        public IList<string> ListUser;

        protected override void AfterLoadFound()
        {
            ListUser = new List<string>();

            DataTable dt = Dp.OpenDataTable(
                "SELECT UserName FROM _System_RoleUser WHERE ProductName=@1 AND RoleName=@0",
                new FieldParam("0", RoleName), new FieldParam("1", BaseFramework.ProductName));
            foreach (DataRow dr in dt.Rows)
                ListUser.Add((string)dr[0]);
        }

        internal static DataTable GetListRole(DataPersistance dp, 
            bool AllRole)
        {
            dp.ValidateTableDef<Role>();
            if (AllRole)
                return dp.OpenDataTable(
                    "SELECT RoleName FROM _System_Role WHERE ProductName=@0 ORDER BY RoleName",
                    new FieldParam("0", BaseFramework.ProductName));
            else
                return dp.OpenDataTable(string.Concat(
                    "SELECT RoleName FROM _System_Role WHERE ProductName=@0 AND IsActive=", 
                    dp.FormatSqlValue(true, DataType.Boolean), 
                    " AND (UseDateLimit=",
                    dp.FormatSqlValue(false, DataType.Boolean), 
                    " OR ", dp.GetSqlNow(),
                    " BETWEEN StartDate AND EndDate) ORDER BY RoleName"),
                    new FieldParam("0", BaseFramework.ProductName));
        }
    }

    [TableName("_System_User")]
    public class User : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50), EmptyError]
        public string UserName;

        [DataTypeVarChar(20)]
        public string UserPassword;
        [DataTypeBoolean]
        public bool IsActive;
        [DataTypeBoolean]
        public bool IsAdmin;
        [DataTypeBoolean]
        public bool UseDateLimit;
        [DataTypeDate]
        public DateTime StartDate;
        [DataTypeDate]
        public DateTime EndDate;

        public IList<string> ListRole;

        protected override void AfterLoadFound()
        {
            ListRole = new List<string>();
            DataTable dt = Dp.OpenDataTable(
                "SELECT RoleName FROM _System_RoleUser WHERE ProductName=@1 AND UserName=@0",
                new FieldParam("0", UserName), new FieldParam("1", BaseFramework.ProductName));
            foreach (DataRow dr in dt.Rows)
                ListRole.Add((string)dr[0]);
        }

        internal static bool IsUserAdminExist(DataPersistance dp)
        {
            string TrueStr = dp.FormatSqlValue(true);
            string FalseStr = dp.FormatSqlValue(false);

            string SqlQuery = string.Concat(
                @"SELECT U.UserName FROM (_System_User AS U 
                INNER JOIN _System_RoleUser AS RU ON 
                U.UserName=RU.UserName) INNER JOIN _System_Role R ON 
                RU.RoleName=R.RoleName AND RU.ProductName=R.ProductName WHERE 
                R.ProductName=@0 AND U.IsAdmin=", TrueStr, 
                " AND U.IsActive=", TrueStr, 
                 " AND (U.UseDateLimit=", FalseStr, " OR ", 
                 dp.GetSqlNow(), @" BETWEEN U.StartDate AND 
                U.EndDate) AND R.IsActive=", TrueStr, 
                " AND (R.UseDateLimit=", FalseStr, " OR ", 
                dp.GetSqlNow(), " BETWEEN R.StartDate AND R.EndDate)");
            return dp.Find.IsExists(SqlQuery,
                new FieldParam("0", BaseFramework.ProductName));
        }
        internal static bool IsUserAdmin(DataPersistance dp, string UserName)
        {
            return (bool)dp.Find.Value<User>("IsAdmin", 
                "UserName=@0", false, 
                new FieldParam("0", UserName));
        }
        internal static DataTable GetListUser(DataPersistance dp)
        {
            return dp.OpenDataTable("SELECT UserName FROM _System_User ORDER BY UserName");
        }
    }

    [Relation(typeof(Role), ParentUpdate.UpdateCascade, 
        ParentDelete.DeleteCascade)]
    [Relation(typeof(User), ParentUpdate.UpdateCascade, 
        ParentDelete.DeleteCascade)]
    [TableName("_System_RoleUser")]
    class RoleUser : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string ProductName;
        [PrimaryKey, DataTypeVarChar(100)]
        public string RoleName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string UserName;

        public static bool CanLogin(DataPersistance dp, 
            string RoleName, string UserName, string UserPassword)
        {
            string TrueStr = dp.FormatSqlValue(true);
            string FalseStr = dp.FormatSqlValue(false);

            dp.ValidateTableDef<User>();
            dp.ValidateTableDef<RoleUser>();

            string SqlQuery;

            if (BaseSecurity.LoginWithRole)
            {
                SqlQuery = string.Concat(@"SELECT U.UserName 
                FROM (_System_User AS U INNER JOIN _System_RoleUser AS RU ON 
                U.UserName=RU.UserName) INNER JOIN _System_Role AS R ON 
                RU.RoleName=R.RoleName AND RU.ProductName=R.ProductName 
                WHERE RU.ProductName=@3 AND U.IsActive=", TrueStr,
                    @" AND U.UserName=@0 AND U.UserPassword=@1
                AND (U.UseDateLimit=", FalseStr, " OR ", dp.GetSqlNow(),
                    " BETWEEN U.StartDate AND U.EndDate) AND R.IsActive=",
                    TrueStr, @" AND R.RoleName=@2 AND 
                (R.UseDateLimit=", FalseStr, " OR ", dp.GetSqlNow(),
                    " BETWEEN R.StartDate AND R.EndDate)");

                return dp.Find.IsExists(SqlQuery,
                    new FieldParam("0", UserName),
                    new FieldParam("1", UserPassword),
                    new FieldParam("2", RoleName),
                    new FieldParam("3", BaseFramework.ProductName));
            }
            else
            {
                SqlQuery = string.Concat(@"SELECT U.UserName 
                FROM (_System_User AS U INNER JOIN _System_RoleUser AS RU ON 
                U.UserName=RU.UserName) INNER JOIN _System_Role AS R ON 
                RU.RoleName=R.RoleName AND RU.ProductName=R.ProductName 
                WHERE RU.ProductName=@3 AND U.IsActive=", TrueStr,
                    @" AND U.UserName=@0 AND U.UserPassword=@1
                AND (U.UseDateLimit=", FalseStr, " OR ", dp.GetSqlNow(),
                    " BETWEEN U.StartDate AND U.EndDate) AND R.IsActive=",
                    TrueStr, @" AND
                (R.UseDateLimit=", FalseStr, " OR ", dp.GetSqlNow(),
                    " BETWEEN R.StartDate AND R.EndDate)");

                return dp.Find.IsExists(SqlQuery,
                    new FieldParam("0", UserName),
                    new FieldParam("1", UserPassword),
                    new FieldParam("3", BaseFramework.ProductName));
            }
        }
        public static DataTable GetListUser(DataPersistance dp, string RoleName)
        {
            return dp.OpenDataTable(string.Concat("SELECT ", 
                dp.FormatSqlValue(false, DataType.Boolean),
                @" AS Pilih,UserName AS NamaUser FROM _System_RoleUser 
                WHERE RoleName=@0 AND ProductName=@1"),
                new FieldParam("0", RoleName), 
                new FieldParam("1", BaseFramework.ProductName));
        }
        public static DataTable GetListRole(DataPersistance dp, string UserName)
        {
            return dp.OpenDataTable(string.Concat("SELECT ", 
                dp.FormatSqlValue(false, DataType.Boolean),
                @" AS Pilih,RoleName AS NamaPeran FROM _System_RoleUser 
                WHERE UserName=@0 AND ProductName=@1"),
                new FieldParam("0", UserName),
                new FieldParam("1", BaseFramework.ProductName));
        }
    }

    [Relation(typeof(Role), ParentUpdate.UpdateCascade, 
        ParentDelete.DeleteCascade)]
    [TableName("_System_RoleModule")]
    class RoleModule : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string ProductName;
        [PrimaryKey, DataTypeVarChar(100)]
        public string RoleName;
        [PrimaryKey, DataTypeVarChar(100)]
        public string ModuleName;

        [DataTypeBoolean(Default=false)]
        public bool AllDocumentData;

        [DataTypeVarChar(2000)]
        public string DataSecurity;
    }

    [Relation(typeof(RoleModule), 
        ParentUpdate.UpdateCascade, 
        ParentDelete.DeleteCascade)]
    [TableName("_System_RoleModuleDocument")]
    class RoleModuleDocument : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string ProductName;
        [PrimaryKey, DataTypeVarChar(100)]
        public string RoleName;
        [PrimaryKey, DataTypeVarChar(100)]
        public string ModuleName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string VariableName;
        [PrimaryKey, DataTypeVarChar(50)]
        public string DocumentKey;

        [DataTypeVarChar(50)]
        public string DocumentName;
    }
}
