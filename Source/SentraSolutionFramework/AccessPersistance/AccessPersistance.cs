using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Data;
using System.Reflection;
using System.IO;
using System.Data.OleDb;
using System.Drawing;
using SentraUtility;
using System.Diagnostics;

namespace SentraSolutionFramework.Persistance
{
    [DebuggerNonUserCode]
    public class AccessPersistance : DataPersistance
    {
        private string _DatabaseName;
        public string DatabaseName { get { return _DatabaseName; } }

        public override string EngineName
        {
            get { return "Access"; }
        }

        public override string GetSqlSelectTopN(string SqlSelect, 
            int N, string OrderBy)
        {
            int i = SqlSelect.IndexOf("SELECT", 
                StringComparison.OrdinalIgnoreCase);
            if (i < 0)
                throw new ApplicationException("Error Membaca Sql");

            if (OrderBy.Length>0)
                return string.Concat(SqlSelect.Insert(i + 6, " TOP " + 
                    N.ToString()), " ORDER BY ", OrderBy);
            else
                return SqlSelect.Insert(i + 6, " TOP " + N.ToString());
        }

        private bool CheckDatabase()
        {
            string dbName = _DatabaseName.Contains(".") ? 
                _DatabaseName : _DatabaseName + ".mdb";
            if (File.Exists(string.Concat(_FolderLocation, @"\", 
                dbName))) return false;
            if (!Directory.Exists(_FolderLocation))
                Directory.CreateDirectory(_FolderLocation);

            FileStream fs = null;
            try
            {
                Assembly objAssembly = Assembly.GetExecutingAssembly();
                Stream s = objAssembly.GetManifestResourceStream(
                    objAssembly.GetManifestResourceNames()[0]);
                byte[] buffer = new Byte[s.Length];

                s.Read(buffer, 0, (int)s.Length);
                fs = new FileStream(string.Concat(_FolderLocation,
                    @"\", dbName), FileMode.Create);
                fs.Write(buffer, 0, (int)s.Length);
                fs.Close();
            }
            finally
            {
                if (fs != null) fs.Close();
            }
            return true;
        }

        public AccessPersistance() { }

        public AccessPersistance(AccessPersistance Dp) :
            this(Dp.ConnectionString, Dp.AutoCreateDb, Dp.FolderLocation) { }

        public AccessPersistance(string ConnectionString,
            bool AutoCreateDb, string FolderLocation)
            : base(ConnectionString, AutoCreateDb, FolderLocation)
        {
            _DatabaseName = BaseUtility.GetValueFromConnectionString(
              ConnectionString, "Data Source").Trim();
            int i = _DatabaseName.LastIndexOf("\\");
            if (i >= 0)
                _DatabaseName = _DatabaseName.Substring(i + 1);
            InitConnection();
        }

        public AccessPersistance(string FolderLocation, 
            string DatabaseName, bool AutoCreateDb) : 
            base(CreateConnectionString(
            FolderLocation, DatabaseName), AutoCreateDb, FolderLocation)
        {
            _DatabaseName = DatabaseName.Trim();
            InitConnection();
        }

        private void InitConnection()
        {
            bool CreateNewDb = false;
            if (_AutoCreateDb) CreateNewDb = CheckDatabase();
            Connection = CreateConnection();
            if (CreateNewDb) CallAfterCreateDb();
            AfterInitConnection();
        }

        public static string CreateConnectionString(string FolderLocation,
            string DatabaseName) 
        {
            string TmpLoc = DatabaseName.Contains(".") ?
                DatabaseName : DatabaseName + ".mdb";
            
            if (FolderLocation.Length > 0)
                TmpLoc = string.Concat(FolderLocation, "\\", TmpLoc);

            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + TmpLoc;
        }

        #region Generic DataAccess
        protected override IDbDataAdapter CreateAdapter()
        { return new OleDbDataAdapter(); }
        protected override IDbCommand CreateCommand()
        {
            return new OleDbCommand();
        }

        protected override IDbConnection CreateConnection()
        { 
            return new OleDbConnection(_ConnectionString);
        }
        protected override IDataParameter CreateParameter(string ParamName, FieldParam Param)
        {
            OleDbParameter p = new OleDbParameter();
            p.ParameterName = GetSqlParam() + ParamName;
            switch (Param.DataType)
            {
                case DataType.VarChar:
                    p.OleDbType = OleDbType.VarChar;
                    p.Size = Param.Length;
                    break;
                case DataType.Char:
                    p.OleDbType = OleDbType.Char;
                    p.Size = Param.Length;
                    break;
                case DataType.Integer:
                    p.OleDbType = OleDbType.Integer;
                    break;
                case DataType.Decimal:
                    p.OleDbType = OleDbType.Decimal;
                    p.Precision = (byte)Param.Length;
                    p.Scale = (byte)Param.Scale;
                    break;
                case DataType.Boolean:
                    p.OleDbType = OleDbType.Boolean;
                    break;
                case DataType.DateTime:
                    p.OleDbType = OleDbType.Date;
                    break;
                case DataType.Date:
                    p.OleDbType = OleDbType.DBDate;
                    break;
                case DataType.Time:
                    p.OleDbType = OleDbType.DBTime;
                    break;
                case DataType.TimeStamp:
                    p.OleDbType = OleDbType.DBTimeStamp;
                    break;
                case DataType.Binary:
                    p.OleDbType = OleDbType.VarBinary;
                    if (Param.Value != null)
                        p.Value = Param.Value;
                    else
                        p.Value = DBNull.Value;
                    return p;
                case DataType.Image:
                    p.OleDbType = OleDbType.VarBinary;
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

        public override string FormatSqlValue(object Value)
        {
            if (Value == null) return base.FormatSqlValue(Value);
            Type tp = Value.GetType();

            if (tp == typeof(DateTime))
                return string.Concat("#", ((DateTime)Value)
                    .ToString("MM/dd/yyyy HH:mm"), "#");
            else if (tp == typeof(bool))
                return ((bool)Value) ? "true" : "false";
            else
                return base.FormatSqlValue(Value);
        }
        public override string FormatSqlValue(object Value, DataType DataType)
        {
            if (Value == null) return base.FormatSqlValue(Value);
            switch (DataType)
            {
                case DataType.Date:
                    return string.Concat("#",
                        ((DateTime)Value).ToString("MM/dd/yyyy"), "#");
                case DataType.DateTime:
                case DataType.TimeStamp:
                    return string.Concat("#",
                        ((DateTime)Value).ToString("MM/dd/yyyy HH:mm:ss"), "#");
                case DataType.Time:
                    return string.Concat("#",
                        ((DateTime)Value).ToString("HH:mm"), "#");
                case DataType.Boolean:
                    return ((bool)Value) ? "true" : "false";
                default:
                    return base.FormatSqlValue(Value, DataType);
            }
        }

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
            
            DataTable dt = ((OleDbConnection)Connection)
                .GetSchema(CollectionName, Restriction);
            if (MustClose) Connection.Close();
            return dt;
        }

        protected override void ProcessDbError(BusinessEntity Entity, Exception ex)
        {
            OleDbException oe = ex as OleDbException;
            if (oe != null)
                switch (oe.ErrorCode)
                {
                    case -2147467259:
                        ProcessUniqueFieldError(Entity);
                        break;
                    default:
                        throw ex;
                }
            else
                throw ex;
        }

        protected override bool IsTableExist(string TableName)
        {
            return GetSchema("Tables", new string[] 
                { null, null, TableName }).Rows.Count > 0;
        }

        public override string GetSqlNow() { return "Now()"; }
        public override string GetSqlDate() { return "Date()"; }
        public override string GetSqlSubString(string Text,
            int Start, int Length)
        {
            return string.Concat("MID(", Text, ",", Start.ToString(), 
                ",", Length.ToString(), ")");
        }
        public override string GetSqlCoalesce(string FieldName, 
            object DefaultValue)
        {
            return string.Concat("IIF(", FieldName, " IS Null,",
                FormatSqlValue(DefaultValue), ",", FieldName, ")");
        }
        public override string GetSqlIif(string Condition, 
            object TrueValue, object FalseValue)
        {
            return string.Concat("IIF(", Condition, ",", 
                FormatSqlValue(TrueValue), ",", 
                FormatSqlValue(FalseValue), ")");
        }

        public override string GetSqlCoalesceNoFormat(string FieldName, 
            string DefaultValue)
        {
            return string.Concat("IIF(", FieldName, " IS Null,",
                DefaultValue, ",", FieldName, ")");
        }
        public override string GetSqlIifNoFormat(string Condition, 
            string TrueValue, string FalseValue)
        {
            return string.Concat("IIF(", Condition, ",", TrueValue,
                ",", FalseValue, ")");
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
                    .Append(AccessType(fd)).Append(",");
            foreach (FieldDef fd in td.NonKeyFields.Values)
                if (fd.GetDTLSA() == null)
                    strBuild.Append(fd.FieldName)
                        .Append(AccessType(fd)).Append(",");
            if (td.KeyFields.Count > 0)
            {
                strBuild.Append("CONSTRAINT PK_").Append(td.TableName)
                    .Append(" PRIMARY KEY (");
                foreach (FieldDef fd in td.KeyFields.Values)
                    strBuild.Append(fd.FieldName).Append(",");

                strBuild.Remove(strBuild.Length - 1, 1).Append("))");
            }
            else
                strBuild.Remove(strBuild.Length - 1, 1).Append(")");

            int Ctr = 0;
            foreach (string IndexField in td.IndexedFields)
            {
                int i = IndexField.IndexOf('|');
                if (i == 6)
                    strBuild.Append("\0CREATE UNIQUE INDEX IX_");
                else
                    strBuild.Append("\0CREATE INDEX IX_");
                strBuild.Append(td.TableName).Append("_")
                    .Append(Ctr.ToString()).Append(" ON ")
                    .Append(td.TableName).Append("(").Append(
                    IndexField.Substring(i + 1)).Append(")");
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
            if (dbTd.IndexedFields.Count < j) 
                j = dbTd.IndexedFields.Count;

            #region Check updated Indexes
            for (i = 0; i < j; i++)
            {
                string index = MetaTd.IndexedFields[i];
                if (index.ToLower() != dbTd.IndexedFields[i].ToLower())
                {
                    strDropIndex.Append("\0ALTER TABLE ")
                        .Append(TableName)
                        .Append(" DROP CONSTRAINT IX_")
                        .Append(TableName).Append("_")
                        .Append(i);

                    int idx = index.IndexOf('|');
                    if (idx == 6)
                        strAddIndex.Append("\0CREATE UNIQUE INDEX IX_");
                    else
                        strAddIndex.Append("\0CREATE INDEX IX_");
                    strAddIndex.Append(TableName).Append("_")
                        .Append(i).Append(" ON ")
                        .Append(TableName).Append("(").Append(
                        index.Substring(idx + 1)).Append(")");
                }
            }
            #endregion

            #region Check Dropped Indexes
            j = dbTd.IndexedFields.Count;
            for(; i < j; i++)
                strDropIndex.Append("\0ALTER TABLE ").Append(TableName)
                    .Append(" DROP CONSTRAINT IX_")
                    .Append(TableName).Append("_").Append(i);
            #endregion

            #region Check Inserted Indexes
            j = MetaTd.IndexedFields.Count;
            for (; i < j; i++)
            {
                string IndexField = MetaTd.IndexedFields[i];
                int idx = IndexField.IndexOf('|');
                if (idx == 6)
                    strAddIndex.Append("\0CREATE UNIQUE INDEX IX_");
                else
                    strAddIndex.Append("\0CREATE INDEX IX_");
                strAddIndex.Append(TableName).Append("_")
                    .Append(i).Append(" ON ")
                    .Append(TableName).Append("(").Append(
                    IndexField.Substring(idx + 1)).Append(")");
            }
            #endregion

            #region Check Fields
            bool PKChanged = false;
            #region Check Deleted Fields and Updated Fields
            foreach(FieldDef dbFd in dbTd.KeyFields.Values)
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
                        .Append(AccessType(MtFd));
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
                            .Append(" DROP COLUMN ").Append(FieldName);
                        continue;
                    }
                    else PKChanged = true;

                if (!IsFieldEqual(MtFd, dbFd))
                    strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                        .Append(" ALTER COLUMN ").Append(FieldName)
                        .Append(AccessType(MtFd));
            }
            #endregion

            #region Check Inserted Fields
            foreach (FieldDef MtFd in MetaTd.KeyFields.Values)
            {
                FieldDef dbFd;
                if (!dbTd.KeyFields.TryGetValue(MtFd.FieldName, out dbFd))
                {
                    if (!dbTd.NonKeyFields.TryGetValue(MtFd.FieldName, out dbFd))
                    {
                        strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" ADD COLUMN ").Append(MtFd.FieldName)
                            .Append(AccessType(MtFd));
                        strAddColumn.Append("\0UPDATE ").Append(TableName)
                            .Append(" SET ").Append(MtFd.FieldName)
                            .Append("=").Append(FormatSqlValue(
                            MtFd.GetDefaultValue(), MtFd.DataType));
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
                        strAddColumn.Append("\0ALTER TABLE ").Append(TableName)
                            .Append(" ADD COLUMN ").Append(MtFd.FieldName)
                            .Append(AccessType(MtFd));
                        strAddColumn.Append("\0UPDATE ").Append(TableName)
                            .Append(" SET ").Append(MtFd.FieldName)
                            .Append("=").Append(FormatSqlValue(
                            MtFd.GetDefaultValue(), MtFd.DataType));
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

        private string AccessType(FieldDef fd)
        {
            switch (fd.DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    if (fd.Length < 256)
                        return string.Concat(" Text(", fd.Length, ")");
                    else
                        return " Memo";
                case DataType.DateTime:
                case DataType.Date:
                case DataType.Time:
                case DataType.TimeStamp:
                    return " DateTime";
                case DataType.Boolean:
                    return " Bit";
                case DataType.Binary:
                case DataType.Image:
                    return " OleObject";
                case DataType.Integer:
                    return " Int";
                case DataType.Decimal:
                    return " Currency";
                default:
                    return string.Empty;
            }
        }

        protected override TableDef BuildTableDef(string TableName)
        {
            throw new ApplicationException("Integritas Data " + TableName + 
                " Rusak, hubungi Pembuat DataPersistance");
        }

        protected override string GetSqlType(FieldParam Param)
        {
            switch (Param.DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    if (Param.Length < 256)
                        return string.Concat(" Text(", Param.Length, ")");
                    else
                        return " Memo";
                case DataType.DateTime:
                case DataType.Date:
                case DataType.Time:
                case DataType.TimeStamp:
                    return " DateTime";
                case DataType.Boolean:
                    return " Bit";
                case DataType.Binary:
                case DataType.Image:
                    return " OleObject";
                case DataType.Integer:
                    return " Int";
                case DataType.Decimal:
                    return " Currency";
                default:
                    return string.Empty;
            }
        }
    }
}
