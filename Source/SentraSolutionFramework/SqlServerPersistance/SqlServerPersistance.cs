using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Data;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading;
using SentraUtility;
using System.Diagnostics;

namespace SentraSolutionFramework.Persistance
{
    // SET ARITHABORT ON bisa mempercepat proses...katanya ???
    //[DebuggerNonUserCode]
    public class SqlServerPersistance : DataPersistance
    {
        private string _ServerName;
        private string _DatabaseName;
        private bool _IntegratedSecurity;
        private string _UserName;
        private string _Password;

        public string ServerName { get { return _ServerName; } }
        public string DatabaseName { get { return _DatabaseName; } }
        public bool IntegratedSecurity { get { return _IntegratedSecurity; } }
        public string UserName { get { return _UserName; } }
        public string Password { get { return _Password; } }

        public override string EngineName
        {
            get { return "Sql Server"; }
        }

        protected override void ProcessDbError(BusinessEntity Entity, 
            Exception ex)
        {
            SqlException se = ex as SqlException;
            if (se != null)
                switch (se.ErrorCode)
                {
                    case -2146232060:
                        ProcessUniqueFieldError(Entity);
                        break;
                    default:
                        throw ex;
                }
            else
                throw ex;
        }

        public override string GetSqlSelectTopN(string SqlSelect, 
            int N, string OrderBy)
        {
            int i = SqlSelect.IndexOf("SELECT", 
                StringComparison.OrdinalIgnoreCase);
            if (i < 0)
                throw new ApplicationException("Error Membaca Sql");

            if (OrderBy.Length > 0)
                return string.Concat(SqlSelect.Insert(i + 6, " TOP " +
                    N.ToString()), " ORDER BY ", OrderBy);
            else
                return SqlSelect.Insert(i + 6, " TOP " + N.ToString());
        }

        public static List<string> GetListConnection(string AppName, 
            string ServerName, bool IntegratedSecurity, 
            string UserName, string Password)
        {
            List<string> RetVal = new List<string>();
            try
            {
                DataPersistance TmpCn = new SqlServerPersistance(ServerName,
                    "master", false, string.Empty, IntegratedSecurity,
                    UserName, Password);
                DataTable dt = TmpCn.OpenDataTable(
                    "select name From sysdatabases where name not in ('master','model','msdb','tempdb')");
                foreach (DataRow dr in dt.Rows)
                {
                    string TmpStr = (string)dr[0];
                    try
                    {
                        if (TmpCn.Find.IsExists(string.Concat("select VarName FROM [",
                            TmpStr, "].dbo._System_AppVariable WHERE ModuleName='System' AND VarName='AppName' AND VarValue='",
                            AppName, "'")))
                            RetVal.Add(TmpStr);
                    }
                    catch { }
                }
            }
            catch { }
            return RetVal;
        }

        private bool CreateDatabase()
        {
            SqlConnection TmpCn = null;
            try
            {
                TmpCn = (SqlConnection)CreateConnection();
                TmpCn.Open();
                TmpCn.Close();
                return false;
            }
            catch
            {
                string SqlCreate = string.Concat("CREATE DATABASE [",
                    _DatabaseName, "] ON (NAME=[", _DatabaseName,
                    "],FILENAME='", _FolderLocation, @"\", _DatabaseName,
                    ".mdf') LOG ON (NAME=[", _DatabaseName, 
                    "_Log],FILENAME='", _FolderLocation, @"\", _DatabaseName,
                    "_Log.ldf')");

                TmpCn = new SqlConnection(CreateConnectionString(
                    _ServerName, "master", _IntegratedSecurity, 
                    _UserName, _Password));
                TmpCn.Open();

                if (File.Exists(string.Concat(_FolderLocation, @"\",
                    _DatabaseName, ".mdf")))
                {
                    SqlCommand Cmd = new SqlCommand(SqlCreate +
                        " FOR ATTACH", TmpCn);
                    Cmd.ExecuteNonQuery();
                    TmpCn.Close();
                }
                else
                {
                    if (!Directory.Exists(_FolderLocation))
                        Directory.CreateDirectory(_FolderLocation);
                    SqlCommand Cmd = new SqlCommand(SqlCreate, TmpCn);
                    Cmd.ExecuteNonQuery();
                    TmpCn.Close();
                }
                int Ctr = 0;
                SqlConnection Cn;
            Ulang:
                try
                {
                    Cn = (SqlConnection)CreateConnection();
                    Cn.Open();
                }
                catch
                {
                    Ctr++;
                    Thread.Sleep(1000 * Ctr);
                    goto Ulang;
                }
                Cn.Close();
                return true;
            }
        }

        private List<string> ListTable;

        public SqlServerPersistance() { }

        public SqlServerPersistance(SqlServerPersistance Dp) :
            base(Dp.ConnectionString, Dp.AutoCreateDb, Dp.FolderLocation)
        {
            string TmpStr = BaseUtility
                .GetValueFromConnectionString(
                ConnectionString, "Integrated Security");

            _IntegratedSecurity = string.Compare(TmpStr, "SSPI",
                true) == 0 || string.Compare(TmpStr, "True", true) == 0;
            if (!_IntegratedSecurity)
            {
                _UserName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "User ID");
                _Password = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Password");
            }
            _ServerName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Server");
            _DatabaseName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Initial Catalog");

            Connection = CreateConnection(); 
            ListTable = Dp.ListTable;
        }

        public SqlServerPersistance(string ConnectionString, 
            bool AutoCreateDb, string FolderLocation) 
            : base(ConnectionString, AutoCreateDb, FolderLocation)
        {
            string TmpStr = BaseUtility
                .GetValueFromConnectionString(
                ConnectionString, "Integrated Security");

            _IntegratedSecurity = string.Compare(TmpStr, "SSPI", 
                true) == 0 || string.Compare(TmpStr, "True", true) == 0;
            if (!_IntegratedSecurity)
            {
                _UserName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "User ID");
                _Password = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Password");
            }
            _ServerName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Server");
            _DatabaseName = BaseUtility.GetValueFromConnectionString(
                    ConnectionString, "Initial Catalog");
            InitConnection();
        }

        public override string FormatSqlValue(object Value)
        {
            if (Value == null || Value == DBNull.Value)
                return "null";

            if (Value.GetType() == typeof(bool))
                return (bool)Value ? "CAST(1 AS BIT)" : "CAST(0 AS BIT)";
            else
                return base.FormatSqlValue(Value);
        }

        public override string FormatSqlValue(object Value, DataType DataType)
        {
            if (Value == null || Value == DBNull.Value)
                return "null";

            if (DataType == DataType.Boolean)
                return (bool)Value ? "CAST(1 AS BIT)" : "CAST(0 AS BIT)";
            else
                return base.FormatSqlValue(Value, DataType);
        }

        private void InitConnection()
        {
            ListTable = new List<string>();
            bool CreateNewDb = false;
            if (_AutoCreateDb) CreateNewDb = CreateDatabase();
            Connection = CreateConnection();

            if (CreateNewDb) CallAfterCreateDb();

            int i = 0;
            DataTable dt;
            string LastMessage = string.Empty;
        ulang:
            if (i == 5)
                throw new ApplicationException(LastMessage);
            try
            {
                dt = GetSchema("Tables", null);
            }
            catch(Exception ex)
            {
                LastMessage = ex.Message;
                Thread.Sleep(2000);
                i++;
                goto ulang;
            }
            foreach (DataRow dr in dt.Rows)
            {
                string TableName = (string)dr[2];
                int j = ListTable.BinarySearch(TableName);
                ListTable.Insert(~j, TableName);
            }
            AfterInitConnection();
        }

        public SqlServerPersistance(string ServerName,
            string DatabaseName, bool AutoCreateDb,
            string FolderLocation)
            : this(ServerName, DatabaseName, AutoCreateDb, 
            FolderLocation, true, string.Empty, 
            string.Empty) { }

        public static string CreateConnectionString(string ServerName, 
            string DatabaseName, bool IntegratedSecurity,
            string UserName, string Password)
        {
            return IntegratedSecurity ? string.Concat(
                "Integrated Security=SSPI;Initial Catalog=",
                DatabaseName, ";Server=", ServerName) :
                string.Concat( "User ID=", UserName, 
                ";Password=", Password,
                ";Initial Catalog=", DatabaseName, 
                ";Server=", ServerName);
        }

        public SqlServerPersistance(string ServerName, 
            string DatabaseName, bool AutoCreateDb, 
            string FolderLocation, bool IntegratedSecurity,
            string UserName, string Password)
            : base(CreateConnectionString(ServerName, DatabaseName,
            IntegratedSecurity, UserName, Password), 
            AutoCreateDb, FolderLocation)
        {
            _ServerName = ServerName;
            _DatabaseName = DatabaseName;
            _IntegratedSecurity = IntegratedSecurity;
            _UserName = UserName;
            _Password = Password;
            InitConnection();
        }

        #region Generic DataAccess
        protected override IDbDataAdapter CreateAdapter()
        { return new SqlDataAdapter(); }
        protected override IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        protected override IDbConnection CreateConnection()
        {
            return new SqlConnection(_ConnectionString);
        }
        protected override IDataParameter CreateParameter(
            string ParamName, FieldParam Param)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = GetSqlParam() + ParamName;
            switch (Param.DataType)
            {
                case DataType.VarChar:
                    p.DbType = DbType.String;
                    p.Size = Param.Length;
                    break;
                case DataType.Char:
                    p.DbType = DbType.StringFixedLength;
                    p.Size = Param.Length;
                    break;
                case DataType.Integer:
                    p.DbType = DbType.Int32;
                    break;
                case DataType.Decimal:
                    p.DbType = DbType.Decimal;
                    p.Precision = (byte)Param.Length;
                    p.Scale = (byte)Param.Scale;
                    break;
                case DataType.Boolean:
                    p.DbType = DbType.Boolean;
                    break;
                case DataType.DateTime:
                    p.DbType = DbType.DateTime;
                    break;
                case DataType.Date:
                case DataType.Time:
                case DataType.TimeStamp:
                    p.DbType = DbType.Date;
                    break;
                case DataType.Binary:
                    p.DbType = DbType.Binary;
                    if (Param.Value != null)
                        p.Value = Param.Value;
                    else
                        p.Value = DBNull.Value;
                    return p;
                case DataType.Image:
                    p.DbType = DbType.Binary;
                    if (Param.Value != null)
                        p.Value = Helper.ConvertImageToByteArray(
                            (Image)Param.Value);
                    else
                        p.Value = DBNull.Value;
                    return p;
            }
            if (Param.Value != null) p.Value = Param.Value;
            return p;
        }
        #endregion

        private DataTable GetSchema(string CollectionName, string[] Restriction)
        {
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            DataTable dt = ((SqlConnection)Connection)
                .GetSchema(CollectionName, Restriction);
            if (MustClose) Connection.Close();
            return dt;
        }

        protected override bool IsTableExist(string TableName)
        {
            return ListTable.BinarySearch(TableName) >= 0;
        }

        public override string GetSqlNow() { return "GetDate()"; }
        public override string GetSqlDate()
        {
            return "CAST(CAST(GetDate() AS VarChar(12)) AS DateTime)";
        }
        public override string GetSqlDateAdd(string Date1, string Date2)
        {
            Date2 = Date2.TrimStart();
            if (Date2[0] == '-')
                return string.Concat("CAST(",Date1, Date2, " AS INT)");
            else
                return string.Concat("CAST(", Date1, "+", Date2, " AS INT)");
        }
        
        private bool IsFieldEqual(FieldDef fd, FieldDef fd2)
        {
            return fd.DataType == fd2.DataType &&
                fd.Length == fd2.Length && fd.Scale == fd2.Scale;
        }

        protected override string GetSqlCreateTable(TableDef td)
        {
            StringBuilder strBuild = new StringBuilder();

            strBuild = strBuild.Append("CREATE TABLE ")
                .Append(td.TableName).Append("(");
            foreach (FieldDef fd in td.KeyFields.Values)
                strBuild.Append(fd.FieldName)
                    .Append(SqlServerType(fd))
                    .Append(" NOT NULL,");
            foreach (FieldDef fd in td.NonKeyFields.Values)
                if (fd.GetDTLSA() == null)
                {
                    if (fd.DataType == DataType.Image || 
                        fd.DataType == DataType.Binary)
                        strBuild.Append(fd.FieldName)
                            .Append(SqlServerType(fd))
                            .Append(",");
                    else
                        strBuild.Append(fd.FieldName)
                            .Append(SqlServerType(fd))
                            .Append(" NOT NULL,");
            }
            if (td.KeyFields.Count > 0)
            {
                strBuild.Append("CONSTRAINT PK_").Append(td.TableName)
                    .Append(" PRIMARY KEY CLUSTERED (");
                foreach (FieldDef fd in td.KeyFields.Values)
                    strBuild.Append(fd.FieldName).Append(",");

                strBuild.Remove(strBuild.Length - 1, 1).Append("))");
            }
            else
                strBuild.Remove(strBuild.Length - 1, 1).Append(")");

            int Ctr = 0;
            foreach (string index in td.IndexedFields)
            {
                int i = index.IndexOf('|');
                if (i == 6)
                    strBuild.Append("\0CREATE UNIQUE NONCLUSTERED INDEX IX_");
                else
                    strBuild.Append("\0CREATE NONCLUSTERED INDEX IX_");
                strBuild.Append(td.TableName).Append("_")
                    .Append(Ctr.ToString()).Append(" ON ")
                    .Append(td.TableName).Append("(").Append(
                    index.Substring(i + 1)).Append(")");
                Ctr++;
            }
            return strBuild.ToString();
        }
        protected override string GetSqlUpdateTable(TableDef MetaTd,
            TableDef dbTd)
        {
            string TableName = MetaTd.TableName;
            StringBuilder strDropIndex = new StringBuilder();
            StringBuilder strAddIndex = new StringBuilder();
            StringBuilder strDropColumn = new StringBuilder();
            StringBuilder strAddColumn = new StringBuilder();

            int i, j;

            j = MetaTd.IndexedFields.Count;
            if (dbTd.IndexedFields.Count < j) j = dbTd.IndexedFields.Count;

            #region Check updated Indexes
            for (i = 0; i < j; i++)
            {
                string index = MetaTd.IndexedFields[i];
                if (index.ToLower() != dbTd.IndexedFields[i].ToLower())
                {
                    strDropIndex.Append("\0DROP INDEX ").Append(TableName)
                        .Append(".IX_").Append(TableName)
                        .Append("_").Append(i);

                    int idx = index.IndexOf('|');
                    if (idx == 6)
                        strAddIndex.Append("\0CREATE UNIQUE NONCLUSTERED INDEX IX_");
                    else
                        strAddIndex.Append("\0CREATE NONCLUSTERED INDEX IX_");
                    strAddIndex.Append(TableName).Append("_")
                        .Append(i).Append(" ON ")
                        .Append(TableName).Append("(").Append(
                        index.Substring(idx + 1)).Append(")");
                }
            }
            #endregion

            #region Check Dropped Indexes
            j = dbTd.IndexedFields.Count;
            for (; i < j; i++)
                strDropIndex.Append("\0DROP INDEX ").Append(TableName)
                    .Append(".IX_").Append(TableName).Append("_")
                    .Append(i);
            #endregion

            #region Check Inserted Indexes
            j = MetaTd.IndexedFields.Count;
            for (; i < j; i++)
            {
                string index = MetaTd.IndexedFields[i];
                int idx = index.IndexOf('|');
                if (idx == 6)
                    strAddIndex.Append("\0CREATE UNIQUE NONCLUSTERED INDEX IX_");
                else
                    strAddIndex.Append("\0CREATE NONCLUSTERED INDEX IX_");
                strAddIndex.Append(TableName).Append("_")
                    .Append(i).Append(" ON ")
                    .Append(TableName).Append("(").Append(
                    index.Substring(idx + 1)).Append(")");
            }
            #endregion

            #region Check Fields
            bool PKChanged = false;
            #region Check Deleted Fields & Updated Fields
            foreach (FieldDef dbFd in dbTd.KeyFields.Values)
            {
                string FieldName = dbFd.FieldName;
                FieldDef MtFd;
                if (!MetaTd.KeyFields.TryGetValue(FieldName, out MtFd))
                    if (!MetaTd.NonKeyFields.TryGetValue(FieldName, out MtFd) ||
                        MtFd.GetDTLSA() != null)
                    {
                        strDropColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" DROP COLUMN ").Append(FieldName);
                        PKChanged = true;
                        continue;
                    }
                    else PKChanged = true;

                if (!IsFieldEqual(MtFd, dbFd))
                {
                    PKChanged = true;
                    strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                        .Append(" ALTER COLUMN ").Append(FieldName)
                        .Append(SqlServerType(MtFd))
                        .Append(" NOT NULL");
                }
            }
            foreach (FieldDef dbFd in dbTd.NonKeyFields.Values)
            {
                string FieldName = dbFd.FieldName;
                FieldDef MtFd;
                if (!MetaTd.NonKeyFields.TryGetValue(FieldName, out MtFd) ||
                    MtFd.GetDTLSA() != null)
                    if (!MetaTd.KeyFields.TryGetValue(FieldName, out MtFd))
                    {
                        strDropColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" DROP COLUMN ")
                            .Append(FieldName);
                        continue;
                    }
                    else PKChanged = true;

                if (!IsFieldEqual(MtFd, dbFd))
                {
                    if (MtFd.DataType == DataType.Image || MtFd.DataType == DataType.Binary)
                        strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" ALTER COLUMN ").Append(FieldName)
                            .Append(SqlServerType(MtFd));
                    else
                        strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" ALTER COLUMN ").Append(FieldName)
                            .Append(SqlServerType(MtFd))
                            .Append(" NOT NULL");
                }
            }
            #endregion

            #region Check Inserted Fields
            foreach (FieldDef MtFd in MetaTd.KeyFields.Values)
            {
                FieldDef dbFd;
                if (!dbTd.KeyFields.TryGetValue(MtFd.FieldName, out dbFd))
                {
                    if (!dbTd.NonKeyFields.TryGetValue(MtFd.FieldName, 
                        out dbFd))
                    {
                        strAddColumn.Append("\0ALTER TABLE ")
                            .Append(TableName).Append(" ADD ")
                            .Append(MtFd.FieldName)
                            .Append(SqlServerType(MtFd));
                        strAddColumn.Append("\0UPDATE ")
                            .Append(TableName)
                            .Append(" SET ").Append(MtFd.FieldName)
                            .Append("=").Append(FormatSqlValue(
                            MtFd.GetDefaultValue(), MtFd.DataType));

                        strAddColumn.Append("\0ALTER TABLE ")
                            .Append(TableName).Append(" ALTER COLUMN ")
                            .Append(MtFd.FieldName)
                            .Append(SqlServerType(MtFd))
                            .Append(" NOT NULL");
                    }
                    PKChanged = true;
                }
            }
            foreach (FieldDef MtFd in MetaTd.NonKeyFields.Values)
            {
                if (MtFd.GetDTLSA() != null) continue;
                FieldDef dbFd;
                if (!dbTd.NonKeyFields.TryGetValue(MtFd.FieldName, out dbFd))
                    if (!dbTd.KeyFields.TryGetValue(MtFd.FieldName, out dbFd))
                    {
                        strAddColumn.Append("\0ALTER TABLE ")
                            .Append(TableName).Append(" ADD ")
                            .Append(MtFd.FieldName)
                            .Append(SqlServerType(MtFd));

                        if (MtFd.DataType != DataType.Image &&
                            MtFd.DataType != DataType.Binary)
                        {
                            strAddColumn.Append("\0UPDATE ")
                                .Append(TableName).Append(" SET ")
                                .Append(MtFd.FieldName).Append("=")
                                .Append(FormatSqlValue(
                                MtFd.GetDefaultValue(), MtFd.DataType));

                            strAddColumn.Append("\0ALTER TABLE ")
                                .Append(TableName)
                                .Append(" ALTER COLUMN ")
                                .Append(MtFd.FieldName)
                                .Append(SqlServerType(MtFd))
                                .Append(" NOT NULL");
                        }
                    }
                    else PKChanged = true;
            }
            #endregion

            if (PKChanged)
            {
                strDropIndex.Append("\0ALTER TABLE ").Append(TableName)
                    .Append(" DROP CONSTRAINT PK_")
                    .Append(TableName);
                if (MetaTd.KeyFields.Count > 0)
                {
                    strAddIndex.Append("\0ALTER TABLE ").Append(TableName)
                        .Append(" ADD CONSTRAINT PK_").Append(TableName)
                        .Append(" PRIMARY KEY(");
                    foreach (FieldDef fld in MetaTd.KeyFields.Values)
                        strAddIndex.Append(fld.FieldName).Append(",");
                    strAddIndex.Remove(strAddIndex.Length - 1, 1).Append(")");
                }
            }
            #endregion

            if (strDropColumn.Length > 0)
                strDropIndex.Append(strDropColumn.ToString());
            if (strAddColumn.Length > 0)
                strDropIndex.Append(strAddColumn.ToString());
            if (strAddIndex.Length > 0)
                strDropIndex.Append(strAddIndex.ToString());
            if (strDropIndex.Length > 0)
                strDropIndex.Remove(0, 1);
            return strDropIndex.ToString();
        }

        private string SqlServerType(FieldDef fld)
        {
            return SqlServerType(fld.DataType, fld.Length, fld.Scale);
        }
        private string SqlServerType(FieldParam fldp)
        {
            return SqlServerType(fldp.DataType, fldp.Length, fldp.Scale);
        }
        private string SqlServerType(DataType dt, int Length, int Scale)
        {
            switch (dt)
            {
                case DataType.VarChar:
                case DataType.Char:
                    if (Length <= 8000)
                        return string.Concat(" VarChar(", Length, ")");
                    else
                        //return _UseSql2000Syntax ? " Text" : " VarChar(MAX)";
                        return " Text";
                case DataType.DateTime:
                case DataType.Date:
                case DataType.Time:
                case DataType.TimeStamp:
                    return " DateTime";
                case DataType.Boolean:
                    return " Bit";
                case DataType.Binary:
                case DataType.Image:
                    //return _UseSql2000Syntax ? " Image" : " Varbinary(MAX)";
                    return " Image";
                case DataType.Integer:
                    return " Int";
                case DataType.Decimal:
                    return string.Concat(" Numeric(", Length, 
                        ",", Scale,")");
                default:
                    return string.Empty;
            }
        }

        protected override TableDef BuildTableDef(string TableName)
        {
            throw new ApplicationException("Integritas Data " + TableName + 
                " Rusak, hubungi Pembuat DataPersistance");
        }

        protected override string BuildParam(FieldParam[] TableParams)
        {
            string RetVal = string.Empty;

            foreach (FieldParam prm in TableParams)
                RetVal = string.Concat(RetVal, ",@", prm.FieldName);
            return RetVal.Substring(1);
        }

        protected override string BuildDDLParam(FieldParam[] TableParams)
        {
            string RetVal = string.Empty;

            foreach (FieldParam prm in TableParams)
                RetVal = string.Concat(RetVal, ",@", prm.FieldName, " AS ",
                    SqlServerType(prm));
            return RetVal.Substring(1);
        }

        protected override string GetSqlType(FieldParam Param)
        {
            return SqlServerType(Param);
        }
    }
}
