using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Data;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using SentraUtility;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace SentraSolutionFramework.Persistance
{
    /// <summary>
    /// Catatan :
    /// - Variabel Parameter pada SqlText harus diawali : @
    /// - Variabel Parameter pada FieldParam, Parameters tidak perlu diberi @
    /// - Untuk Select TOP N harus menggunakan GetSqlSelectTopN
    /// </summary>
    [TableName("_System_TableVersion")]
    [DebuggerNonUserCode]
    internal class TableVersion : ParentEntity
    {
        [PrimaryKey, DataTypeVarChar(50)]
        public string TableName;

        [DataTypeVarChar(4000)]
        public string CreateStr;
        [DataTypeVarChar(100)]
        public string AsmName;
        [DataTypeVarChar(20)]
        public string DbVersion;
    }

    internal interface IDataPersistance
    {
        string BuildParam(FieldParam[] TableParams);
        string BuildDDLParam(FieldParam[] TableParams);
    }

    //BUG KALO ADA PERUBAHAN FIELD INDEX DI DROP DULU...
    //kalo semua field dihapus error..
    //harusnya buat table baru, copykan data ke table baru, delete table lama
    //kelemahan cara ini mungkin lambat bila data besar..
    //keunggulan : cara konversi data lama ke baru bisa dikendalikan.

    //[DebuggerNonUserCode]
    public abstract class DataPersistance : IDataPersistance
    {
        public DataPersistance() { }

        public void SetCommandParameter(IDbCommand Cmd, string ParamName, 
            object NewValue)
        {
            ((IDataParameter)Cmd.Parameters["@" + ParamName]).Value = NewValue;
        }

        public void SetCommandParameter(IDbCommand Cmd, int ParamIndex, 
            object NewValue)
        {
            ((IDataParameter)Cmd.Parameters[ParamIndex]).Value = NewValue;
        }

        internal IDataReader _ExecuteReader(IDbCommand Cmd)
        {
            IDataReader rdr;
            try
            {
                rdr = Cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    rdr = Cmd.ExecuteReader();
                }
                else
                    throw ex;
            }
            return rdr;
        }
        private int _ExecuteNonQuery(IDbCommand Cmd)
        {
            int RetVal;
            try
            {
                RetVal = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    RetVal = Cmd.ExecuteNonQuery();
                }
                else
                    throw ex;
            }
            return RetVal;
        }

        private DataPersistance _InitPersistanceEntity;
        public DataPersistance InitPersitanceEntity
        {
            get { return _InitPersistanceEntity; }
            set
            {
                try
                {
                    if (value == null ||
                        !value.IsTableExist("_System_TableVersion"))
                    {
                        _InitPersistanceEntity = null;
                        return;
                    }
                    _InitPersistanceEntity = value;
                }
                catch
                {
                    _InitPersistanceEntity = null;
                }
            }
        }

        protected void CallAfterCreateDb()
        {
            SetVariable("System", "AppName", BaseFramework.ProductName);
        }

        internal static string GetEngineName<TEngine>()
            where TEngine : DataPersistance, new()
        {
            DataPersistance Tmp = new TEngine();
            return Tmp.EngineName;
        }

        public abstract string EngineName { get; }

        private DateTime _LastCalc;
        private TimeSpan _DiffTime;
        private void CalcDbDateTime()
        {
            if (_LastCalc.Year == 1 || DateTime.Now.Subtract(_LastCalc).Hours > 24)
            {
                _DiffTime = DateTime.Now.Subtract((DateTime)Find.Value("SELECT " +
                       GetSqlNow(), DateTime.Now));
                _LastCalc = DateTime.Now;
            }
        }

        public DateTime GetDbDateTime()
        {
            CalcDbDateTime();
            return DateTime.Now.Subtract(_DiffTime);
        }

        public DateTime GetDbDate()
        {
            CalcDbDateTime();
            return DateTime.Now.Subtract(_DiffTime).Date;
        }
        
        public DataPersistance Clone()
        {
            return (DataPersistance)BaseFactory.CreateInstance(GetType(), this);
        }

        public string GetSqlSelect<TEntity>(string FieldList,
            string Condition) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef<TEntity>();
            ValidateTableDef(td);

            if (Condition.Length > 0) Condition = " WHERE " + Condition;

            string TmpStr = td.GetSqlFieldSelect(FieldList);
            if (TmpStr.Contains(" AS "))
                return string.Concat("SELECT * FROM (SELECT ", TmpStr,
                    " FROM ", td.GetSqlHeaderView(this), ") AS X", Condition);
            else
                return string.Concat("SELECT ", TmpStr,
                    " FROM ", td.GetSqlHeaderView(this), Condition);
        }

        public abstract string GetSqlSelectTopN(string SqlSelect, int N, string OrderBy);

        public IDbConnection Connection;
        protected internal IDbTransaction Trx;
        protected IDataReader DataReader;

        protected abstract bool IsTableExist(string TableName);

        protected void AfterInitConnection()
        {
            if (BaseFramework.AutoClearMetaDataVersion && BaseUtility.IsDebugMode)
                ClearMetaDataVersion();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;

            return _ConnectionString.Equals(((DataPersistance)obj)._ConnectionString);
        }

        protected string _ConnectionString;
        public string ConnectionString
        {
            get { return _ConnectionString; }
        }

        protected bool _AutoCreateDb;
        protected string _FolderLocation;

        public bool AutoCreateDb { get { return _AutoCreateDb; } }
        public string FolderLocation { get { return _FolderLocation; } }

        // sekali diisi connection string tidak dapat diubah...
        public DataPersistance(string ConnectionString, 
            bool AutoCreateDb, string FolderLocation)
        {
            Find = new DataFinder();
            Find.Dp = this;

            _ConnectionString = ConnectionString;
            _AutoCreateDb = AutoCreateDb;
            _FolderLocation = FolderLocation;
            EnableWriteLog = BaseFramework.EnableWriteLog;
        }

        #region Generic DataAccess
        protected abstract IDbConnection CreateConnection();
        protected abstract IDbDataAdapter CreateAdapter();
        protected abstract IDataParameter CreateParameter(string ParamName, 
            FieldParam Param);
        protected abstract IDbCommand CreateCommand();

        protected IDataParameter CreateParameter(string ParamName, FieldParam Param,
            ParameterDirection Dir)
        {
            IDataParameter p = CreateParameter(ParamName, Param);
            p.Direction = Dir;
            return p;
        }
        #endregion

        #region FormatSql
        public virtual string FormatSqlValue(object Value)
        {
            if (Value == null || Value == DBNull.Value)
                return "null";

            Type tp = Value.GetType();
            if (tp == typeof(string))
                return string.Concat("'",
                    ((string)Value).Replace("'", "''"), "'");
            else if (tp == typeof(int))
                return Value.ToString();
            else if (tp == typeof(Single))
                return ((Single)Value).ToString(BaseUtility.DefaultCultureInfo);
            else if (tp == typeof(double))
                return ((double)Value).ToString(BaseUtility.DefaultCultureInfo);
            else if (tp == typeof(decimal))
                return ((decimal)Value).ToString(BaseUtility.DefaultCultureInfo);
            else if (tp == typeof(bool))
                return ((bool)Value) ? "true" : "false";
            else if (tp == typeof(DateTime))
                return string.Concat("'",
                    ((DateTime)Value).ToString("MM/dd/yyyy HH:mm"), "'");
            else if (tp.IsEnum)
                return string.Concat("'", EnumDef.GetEnumName(tp, Value)
                    .Replace("'", "''"), "'");
            else
                throw new ApplicationException(string.Format(
                    ErrorPersistance.BinaryByValue, "FormatSqlValue"));
        }
        public virtual string FormatSqlValue(object Value, 
            DataType DataType)
        {
            if (Value == null || Value == DBNull.Value) 
                return "null";
            switch (DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    Type ValueType = Value.GetType();
                    if (!ValueType.IsEnum)
                        return string.Concat("'",
                            ((string)Value).Replace("'", "''"), "'");
                    else
                        return string.Concat("'",
                            (EnumDef.GetEnumName(ValueType, Value))
                            .Replace("'", "''"), "'");
                case DataType.Boolean:
                    return ((bool)Value) ? "true" : "false";
                case DataType.Date:
                    return string.Concat("'",
                        ((DateTime)Value).ToString("MM/dd/yyyy"), "'");
                case DataType.DateTime:
                    return string.Concat("'",
                        ((DateTime)Value)
                        .ToString("MM/dd/yyyy HH:mm:ss"), "'");
                case DataType.TimeStamp:
                    return string.Concat("'",
                        ((DateTime)Value)
                        .ToString("MM/dd/yyyy HH:mm:ss.fff"), "'");
                case DataType.Time:
                    return string.Concat("'",
                        ((DateTime)Value).ToString("HH:mm"), "'");
                case DataType.Integer:
                    return ((int)Value).ToString();
                case DataType.Decimal:
                    return ((decimal)Value).ToString(BaseUtility
                        .DefaultCultureInfo);
                default: // DataType.Image || DataType.Binary
                    throw new ApplicationException(string.Format(
                        ErrorPersistance.BinaryByValue, "FormatSqlValue"));
            }
        }
        #endregion

        #region GetSql
        public virtual string GetSqlCoalesce(string FieldName, 
            object DefaultValue)
        {
            return string.Concat("COALESCE(", 
                FieldName, ",", FormatSqlValue(DefaultValue), ")");
        }
        public virtual string GetSqlIif(string Condition, 
            object TrueValue, object FalseValue)
        {
            return string.Concat("CASE WHEN ", Condition, " THEN ", 
                FormatSqlValue(TrueValue),
                " ELSE ", FormatSqlValue(FalseValue), " END");
        }

        public virtual string GetSqlDateAdd(string Date1, string Date2)
        {
            Date2 = Date2.TrimStart();
            if (Date2[0] == '-')
                return Date1 + Date2;
            else
                return string.Concat(Date1, "+", Date2);
        }

        public virtual string GetSqlCoalesceNoFormat(string TestExpr, 
            string DefaultExpr)
        {
            return string.Concat("COALESCE(", TestExpr, ",", 
                DefaultExpr, ")");
        }
        public virtual string GetSqlIifNoFormat(string Condition, 
            string TrueValue, string FalseValue)
        {
            return string.Concat("CASE WHEN ", Condition, " THEN ", 
                TrueValue, " ELSE ", FalseValue, " END");
        }
        public virtual string GetSqlAbs(string FieldName)
        {
            return string.Concat("ABS(", FieldName, ")");
        }

        protected internal virtual string GetSqlMax() { return "MAX"; }
        protected internal virtual string GetSqlMin() { return "MIN"; }
        protected internal virtual string GetSqlSum() { return "SUM"; }
        protected internal virtual string GetSqlAvg() { return "AVG"; }
        protected internal virtual string GetSqlCount() { return "COUNT"; }
        public virtual string GetSqlOrderDesc() { return " DESC"; }
        public virtual string GetSqlSubString(string Text, 
            int Start, int Length) { return string.Concat("SUBSTRING(",
                Text, ",", Start.ToString(), ",", Length.ToString(), ")"); }
        protected virtual string GetSqlParam() { return "@"; }
        public abstract string GetSqlNow();
        public abstract string GetSqlDate();

        protected abstract string GetSqlType(FieldParam Param);

        protected abstract string GetSqlCreateTable(TableDef td);
        protected abstract string GetSqlUpdateTable(TableDef MetaTd, TableDef dbTd);
        protected virtual string GetSqlUpdateView(TableDef td)
        {
            if (td._dva == null || !td._dva.PersistView)
                return string.Empty;
            else
            {
                ViewEntity ve = (ViewEntity)BaseFactory.CreateInstance(td.ClassType);
                ve.Dp = this;
                if (td.TableParams == null)
                    return string.Concat("ALTER VIEW ", td._TableName,
                        " AS\n", ve.GetSqlDdl());
                else
                {
                    FieldParam retField = ve.GetReturnType();
                    if (retField == null)
                        return string.Concat("ALTER FUNCTION ", td.GetSqlDDLHeaderView(this),
                            "\nRETURNS TABLE AS RETURN (\n", ve.GetSqlDdl(), "\n)");
                    else
                        return string.Concat("ALTER FUNCTION ", td.GetSqlDDLHeaderView(this),
                            "\nRETURNS ", GetSqlType(retField), " AS\nBEGIN\nDECLARE @",
                            retField.FieldName, " ", GetSqlType(retField), "\n",
                            ve.GetSqlDdl(), "\nRETURN @", retField.FieldName, "\nEND");
                }
            }
        }
        protected virtual string GetSqlCreateView(TableDef td)
        {
            if (td._dva == null || !td._dva.PersistView)
                return string.Empty;
            else
            {
                ViewEntity ve = (ViewEntity)BaseFactory.CreateInstance(td.ClassType);
                ve.Dp = this;
                if (td.TableParams == null)
                    return string.Concat("CREATE VIEW ", td._TableName,
                        " AS\n", ve.GetSqlDdl());
                else
                {
                    FieldParam RetField = ve.GetReturnType();
                    if (RetField == null)
                        return string.Concat("CREATE FUNCTION ", td.GetSqlDDLHeaderView(this),
                           "\nRETURNS TABLE AS RETURN (\n", ve.GetSqlDdl(), "\n)");
                    else
                        return string.Concat("CREATE FUNCTION ", td.GetSqlDDLHeaderView(this),
                           "\nRETURNS ", GetSqlType(RetField), " AS\nBEGIN\nDECLARE @",
                           RetField.FieldName, " ", GetSqlType(RetField), "\n",
                           ve.GetSqlDdl(), "\nRETURN @", RetField.FieldName, "\nEND");
                }
            }
        }
        #endregion

        #region ValidateTableDef
        private void ValidateChild(DataPersistance DpVal, TableDef tdParent,
            Dictionary<string, int> ValidatedTable)
        {
            TableVersion tv = new TableVersion();
            foreach (EntityCollDef ecd in tdParent.ChildEntities)
            {
                TableDef tdChild = MetaData.GetTableDef(ecd.ChildType);
                if (tdChild.KeyFields.Count == tdParent.KeyFields.Count)
                {
                    tdChild.SetIsExist(DpVal);
                    continue;
                }
                if (!tdChild.GetIsExist(DpVal))
                {
                    if (BaseFramework.AutoUpdateMetaData || !BaseUtility.IsDebugMode)
                    {
                        if (DpVal.LoadEntity(tv, "TableName=@0", false,
                            new FieldParam("0", tdChild._TableName)))
                        {
                            #region Table Exist On TableVersion
                            string ObjVersion = tdChild.GetAsmVersion();
                            string MetaAsmName = tdChild.GetAsmName();
                            int intCompare = 0;

                            if (MetaAsmName == tv.AsmName)
                                intCompare = DpVal.CompareDbVersion(
                                    ObjVersion, tv.DbVersion);
                            else
                            {
                                intCompare = -1;
                                tv.AsmName = MetaAsmName;
                            }

                            if (intCompare > 0 && !BaseFramework.AutoClearMetaDataVersion)
                                throw new ApplicationException(string.Format(
                                    ErrorPersistance.OlderEngine,
                                    tdChild._TableName, ObjVersion, tv.DbVersion));

                            if (intCompare < 0 || BaseFramework.AutoClearMetaDataVersion)
                            {
                                string MetaStr = tdChild.GetStrMetaData(DpVal);
                                using (EntityTransaction tr = new
                                    EntityTransaction(DpVal))
                                {
                                    if (MetaStr != tv.CreateStr)
                                    {
                                        DpVal.ExecuteInternalBatchNonQuery(DpVal.GetSqlUpdateTable(
                                            tdChild, new TableDef(tdChild._TableName,
                                            tv.CreateStr)), false);
                                        tv.CreateStr = MetaStr;
                                    }
                                    tv.DbVersion = ObjVersion;
                                    DpVal.SaveUpdateEntity(tv, false, false);
                                    tr.CommitTransaction();
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Table Not Exist On TableVersion
                            tv.TableName = tdChild._TableName;
                            tv.AsmName = tdChild.GetAsmName();
                            tv.DbVersion = tdChild.GetAsmVersion();

                            if (DpVal.IsTableExist(tdChild._TableName))
                            {
                                TableDef tdDb = DpVal.BuildTableDef(tdChild._TableName);
                                tv.CreateStr = tdDb.GetStrMetaData(DpVal);
                                using (EntityTransaction tr = new
                                    EntityTransaction(DpVal))
                                {
                                    if (tdChild.GetStrMetaData(DpVal) != tv.CreateStr)
                                    {
                                        DpVal.ExecuteInternalBatchNonQuery(DpVal.GetSqlUpdateTable(
                                            tdChild, tdDb), false);
                                        tv.CreateStr = tdChild.GetStrMetaData(DpVal);
                                    }
                                    DpVal.SaveNewEntity(tv, false, false);
                                    tr.CommitTransaction();
                                }
                            }
                            else
                            {
                                using (EntityTransaction tr = new
                                    EntityTransaction(DpVal))
                                {
                                    DpVal.ExecuteInternalBatchNonQuery(
                                        DpVal.GetSqlCreateTable(tdChild), false);

                                    tv.CreateStr = tdChild.GetStrMetaData(DpVal);
                                    DpVal.SaveNewEntity(tv, false, false);
                                    DpVal.InsertInitField(tdChild);
                                    tr.CommitTransaction();
                                }
                            }
                            #endregion
                        }
                    }
                    tdChild.SetIsExist(DpVal);
                    if (DpVal.Trx != null) ListTableChanged.Add(tdChild);

                    #region Validate DataTypeLoadSql Reff
                    foreach (FieldDef fld in tdChild.NonKeyFields.Values)
                        if (fld._dtlsa != null &&
                            fld._dtlsa._ParentType != null &&
                            fld._dtlsa._ParentType != tdChild.ClassType)
                            pvValidateTableDef(MetaData.GetTableDef(
                                fld._dtlsa._ParentType), ValidatedTable);
                    #endregion
                }
                if (tdChild.ChildEntities.Count > 0)
                    ValidateChild(DpVal, tdChild, ValidatedTable);
            }
        }

        public void ValidateTableDef(TableDef td)
        {
            Dictionary<string, int> ValidatedTable = new Dictionary<string, int>();
            pvValidateTableDef(td, ValidatedTable);
        }

        private void pvValidateTableDef(TableDef td, 
            Dictionary<string, int> ValidatedTable)
        {
            DataPersistance DpVal = td.GetDataPersistance(this);
            if (td.GetIsExist(DpVal) || ValidatedTable.ContainsKey(td._TableName)) return;
            ValidatedTable[td._TableName] = 1;

            if (td._ParentClassType != null)
                pvValidateTableDef(MetaData.GetTableDef(td._ParentClassType), ValidatedTable);
            else if (td._dva != null)    //View
            {
                if (!td._dva.PersistView) td.SetIsExist(DpVal);

                Type[] ListType = td._dva.TypeDependends;
                if (ListType != null)
                    foreach (Type tp in ListType)
                        pvValidateTableDef(MetaData.GetTableDef(tp), ValidatedTable);
                if (!td._dva.PersistView)
                {
                    td.SetIsExist(DpVal);
                    if (DpVal.Trx != null) ListTableChanged.Add(td);
                    return;
                }
            }

            #region Check If Validate TableVersion...
            TableVersion tv = new TableVersion();
            if (td._TableName == "_System_TableVersion")
            {
                try
                {
                    DpVal.ExecuteInternalNonQuery("SELECT TableName FROM _System_TableVersion WHERE 1=0");
                }
                catch
                {
                    DpVal.ExecuteInternalNonQuery(DpVal.GetSqlCreateTable(td));
                }

                td.SetIsExist(DpVal);
                if (DpVal.Trx != null) ListTableChanged.Add(td);
                return;
            }
            #endregion

            #region Cek If Entity Contains Field Or Not
            if (td._dva == null)
            {
                bool FieldExist = td.KeyFields.Count > 0;
                if (!FieldExist)
                    foreach (FieldDef fld in td.NonKeyFields.Values)
                        if (fld._dtlsa == null)
                        {
                            FieldExist = true;
                            break;
                        }
                if (!FieldExist)  // Tidak punya persistance field..
                {
                    td.SetIsExist(DpVal);
                    return;
                }
            }
            #endregion

            if (BaseFramework.AutoUpdateMetaData || !BaseUtility.IsDebugMode)
            {
                if (DpVal.LoadEntity(tv, "TableName=@0", false,
                    new FieldParam("0", td._TableName)))
                {
                    #region If Table Exist On TableVersion
                    string ObjVersion = td.GetAsmVersion();
                    string MetaAsmName = td.GetAsmName();
                    int intCompare = 0;

                    if (MetaAsmName == tv.AsmName)
                        intCompare = DpVal.CompareDbVersion(
                            ObjVersion, tv.DbVersion);
                    else
                    {
                        intCompare = -1;
                        tv.AsmName = MetaAsmName;
                    }

                    if (intCompare > 0 && !BaseFramework.AutoClearMetaDataVersion)
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.OlderEngine,
                            td._TableName, ObjVersion, tv.DbVersion));

                    if (intCompare < 0 || BaseFramework.AutoClearMetaDataVersion)
                    {
                        string MetaStr = td.GetStrMetaData(DpVal);

                        using (EntityTransaction tr = new EntityTransaction(DpVal))
                        {
                            if (MetaStr != tv.CreateStr)
                            {
                                if (td._dva == null)
                                    DpVal.ExecuteInternalBatchNonQuery(DpVal.GetSqlUpdateTable(
                                        td, new TableDef(td._TableName,
                                        tv.CreateStr)), false);
                                else if (td._dva.PersistView)
                                    DpVal.ExecuteInternalNonQuery(DpVal.GetSqlUpdateView(td));
                                tv.CreateStr = MetaStr;
                            }
                            tv.DbVersion = ObjVersion;
                            DpVal.SaveUpdateEntity(tv, false, false);
                            tr.CommitTransaction();
                        }
                    }
                    #endregion
                }
                else
                {
                    #region If Table Not Exist On TableVersion
                    tv.TableName = td._TableName;
                    tv.AsmName = td.GetAsmName();
                    tv.DbVersion = td.GetAsmVersion();

                    if (DpVal.IsTableExist(td._TableName))
                    {
                        TableDef tdDb = DpVal.BuildTableDef(td._TableName);
                        tv.CreateStr = tdDb.GetStrMetaData(DpVal);
                        using (EntityTransaction tr = new EntityTransaction(DpVal))
                        {
                            if (td.GetStrMetaData(DpVal) != tv.CreateStr)
                            {
                                if (td._dva == null)
                                    DpVal.ExecuteInternalBatchNonQuery(DpVal.GetSqlUpdateTable(
                                    td, tdDb), false);
                                else
                                    DpVal.ExecuteInternalNonQuery(DpVal.GetSqlUpdateView(td));
                                tv.CreateStr = td.GetStrMetaData(DpVal);
                            }
                            DpVal.SaveNewEntity(tv, false, false);
                            tr.CommitTransaction();
                        }
                    }
                    else
                    {
                        using (EntityTransaction tr = new EntityTransaction(DpVal))
                        {
                            if (td._dva == null)
                                DpVal.ExecuteInternalBatchNonQuery(
                                    DpVal.GetSqlCreateTable(td), false);
                            else if (td._dva.PersistView)
                                DpVal.ExecuteInternalNonQuery(DpVal.GetSqlCreateView(td));

                            tv.CreateStr = td.GetStrMetaData(DpVal);
                            DpVal.SaveNewEntity(tv, false, false);
                            DpVal.InsertInitField(td);
                            tr.CommitTransaction();
                        }
                    }
                    #endregion
                }
            }

            #region Validate DataTypeLoadSql Reff
            foreach (FieldDef fld in td.NonKeyFields.Values)
                if (fld._dtlsa != null &&
                    fld._dtlsa._ParentType != null &&
                    fld._dtlsa._ParentType != td.ClassType)
                    pvValidateTableDef(MetaData.GetTableDef(fld._dtlsa._ParentType),
                        ValidatedTable);
            #endregion

            if (td.ChildEntities.Count > 0) ValidateChild(DpVal, td, ValidatedTable);
            td.SetIsExist(DpVal);
            if (DpVal.Trx != null) ListTableChanged.Add(td);
            return;
        }

        private int CompareDbVersion(string ObjVersion, 
            string MetaVersion)
        {
            if (ObjVersion == MetaVersion) return 0;
            if (MetaVersion.Length == 0) return -1;
            string[] str1 = ObjVersion.Split('.');
            string[] str2 = MetaVersion.Split('.');
            int Cnt = str1.Length;
            for (int i = 0; i < Cnt; i++)
            {
                if (str1[i] != str2[i])
                {
                    int Num1 = Convert.ToInt32(str1[i]);
                    int Num2 = Convert.ToInt32(str2[i]);
                    return Num2 - Num1;
                }
            }
            return 0;
        }
        public void ValidateTableDef<TEntityType>()
        {
            ValidateTableDef(MetaData.GetTableDef(typeof(TEntityType)));
        }

        private void InsertInitField(TableDef td)
        {
            if (td._dva != null || _InitPersistanceEntity == null) return;
            try
            {
                if (!_InitPersistanceEntity
                    .IsTableExist(td._TableName)) return;

                FieldParam[] Params = new FieldParam[
                    td.KeyFields.Count + td.NonKeyFields.Count];
                string FieldSelect;
                string FieldValue = string.Empty;
                string ValueSelect = string.Empty;
                int CtrParam = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    FieldValue = string.Concat(FieldValue, ",",
                        fld._FieldName);
                    ValueSelect = string.Concat(ValueSelect, ",@",
                        fld._FieldName);
                    Params[CtrParam++] = new FieldParam(fld);
                }
                FieldSelect = FieldValue;
                foreach (FieldDef fld in td.NonKeyFields.Values)
                    if (fld._dtlsa == null)
                    {
                        FieldValue = string.Concat(FieldValue, ",",
                            fld._FieldName);
                        if (fld._DataTypeAtr._DataType == DataType.TimeStamp)
                            ValueSelect = string.Concat(ValueSelect, ",",
                                GetSqlNow());
                        else
                        {
                            FieldSelect = string.Concat(FieldSelect, ",",
                                fld._FieldName);
                            ValueSelect = string.Concat(ValueSelect, ",@",
                                fld._FieldName);
                            Params[CtrParam++] = new FieldParam(fld);
                        }
                    }

                if (CtrParam < Params.Length)
                    Array.Resize<FieldParam>(ref Params, CtrParam);

                string SqlInsert = string.Concat("INSERT INTO ",
                    td._TableName, "(", 
                    FieldValue.Substring(1), ") VALUES (", 
                    ValueSelect.Substring(1), ")");

                BatchCommand batch = new BatchCommand();
                IDataReader rdr = _InitPersistanceEntity
                    .ExecuteReader(string.Concat("SELECT ",
                        FieldSelect.Substring(1), " FROM ",
                        td._TableName));
                
                try
                {
                    if (rdr.Read())
                    {
                        for (int i = 0; i < CtrParam; i++)
                            Params[i].Value = rdr[i];
                        batch.AddCommand(SqlInsert, Params);
                    }
                    while (rdr.Read())
                    {
                        Params = (FieldParam[])Params.Clone();
                        for (int i = 0; i < CtrParam; i++)
                        {
                            Params[i] = Params[i].Clone();
                            Params[i].Value = rdr[i];
                        }
                        batch.AddCommand(SqlInsert, Params);
                    }
                    ExecuteBatchNonQuery(batch, true);
                }
                finally
                {
                    rdr.Close();
                }
            }
            catch { }
        }
        #endregion

        protected virtual void ClearMetaDataVersion()
        {
            try
            {
                ExecuteNonQuery("UPDATE _System_TableVersion SET DbVersion=" +
                    FormatSqlValue(string.Empty));
            }
            catch { }
        }

        protected abstract TableDef BuildTableDef(string TableName);

        public virtual string GetSqlLen(string FieldName)
        {
            return string.Concat("LEN(", FieldName, ")");
        }

        protected virtual void ProcessDbError(BusinessEntity Entity, Exception ex)
        {
            throw ex;
        }

        protected virtual void ProcessUniqueFieldError(BusinessEntity Entity)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            string TmpStr = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
                if (!fld.IsBrowseHidden && !fld.IsFormHidden)
                    TmpStr = string.Concat(TmpStr, "'",
                        fld.FieldName, "' atau ");
            foreach (string strIndex in td.IndexedFields)
            {
                string[] TmpIndex = strIndex.Split('|');
                if (TmpIndex[0] != "Unique") continue;
                string[] TmpIndexes = TmpIndex[1].Split(',');
                string NewUnique = string.Empty;
                foreach (string idxField in TmpIndexes)
                {
                    string TmpField = idxField.Trim();
                    if (TmpField.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase))
                        NewUnique = string.Concat(NewUnique, "'",
                            TmpField.Substring(0, TmpField.Length - 4).TrimEnd(),
                            "' dan ");
                    else if (TmpField.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
                        NewUnique = string.Concat(NewUnique, "'",
                            TmpField.Substring(0, TmpField.Length - 5).TrimEnd(),
                            "' dan ");
                    else
                        NewUnique = string.Concat(NewUnique, "'",
                            idxField.Trim(), "' dan ");
                }
                if (TmpIndexes.Length > 1)
                    TmpStr = string.Concat(TmpStr, "(", NewUnique.Substring(0,
                        NewUnique.Length - 5), ") atau ");
                else
                    TmpStr = string.Concat(TmpStr, NewUnique.Substring(0,
                        NewUnique.Length - 5), " atau ");
            }
            if (TmpStr.Length == 0)
                throw new Exception("Data dengan isi sama sudah ada di database");
            else
                throw new Exception(string.Concat("Data ",
                    TmpStr.Substring(0, TmpStr.Length - 6),
                    " dengan isi sama sudah ada di database"));
        }

        private string GetNextAutoNumber(TableDef td, BusinessEntity Entity,
            IAutoNumberAttribute ana)
        {
            string FieldAutoNumber = ana.GetFieldName();
            string TemplateStr = Entity.GetAutoNumberTemplate(FieldAutoNumber);
            if (TemplateStr.Length == 0)
                TemplateStr = Entity.GetAutoNumberTemplate();

            if (TemplateStr.Length == 0)
                TemplateStr = ana.GetTemplate();

            // Update DatePart
            if (ana.GetDateField().Length > 0)
            {
                DateTime TransDate = (DateTime)td.GetFieldDef(
                    ana.GetDateField()).GetValue(Entity);

                if (TransDate == BaseFramework.ServerDateOrDateTime)
                    TransDate = GetDbDate();
                string strMonth = TransDate.Month < 10 ?
                    "0" + TransDate.Month.ToString() :
                    TransDate.Month.ToString();

                TemplateStr = TemplateStr.Replace("YY",
                    TransDate.Year.ToString().Substring(2))
                    .Replace("MM", strMonth);
            }

            string tmpAnd = string.Empty;
            List<FieldParam> Params = new List<FieldParam>();

            if (td.GetFieldDef(ana.GetFieldName())._IsPrimaryKey)
                foreach (FieldDef fld in td.KeyFields.Values)
                    if (fld._FieldName != FieldAutoNumber)
                    {
                        tmpAnd = string.Concat(tmpAnd, " AND ",
                            fld._FieldName, "=@", fld._FieldName);
                        Params.Add(new FieldParam(fld, Entity));
                    }

            // SELECT MAX(SUBSTRING({0},{1},{2}))+1 FROM {3} 
            // WHERE {0} LIKE {4} AND Keys...

            string CounterText = ana.GetCounterText();
            string strCtr = new StringBuilder().Append('_',
                CounterText.Length).ToString();
            object oValue;
            Params.Add(new FieldParam("0",
                TemplateStr.Replace(CounterText, strCtr)));
            if (Find.TryFindMaxValue(GetSqlSubString(FieldAutoNumber,
                TemplateStr.IndexOf(CounterText) + 1,
                CounterText.Length), td._TableName,
                string.Concat(FieldAutoNumber, " LIKE @0", tmpAnd),
                out oValue, Params.ToArray()))
                strCtr = ((int)(int.Parse((string)oValue) + 1)).ToString();
            else
            {
                strCtr = Entity.GetStartCounter(FieldAutoNumber).ToString();
                if (strCtr == "1")
                    strCtr = Entity.GetStartCounter().ToString();
            }
            return TemplateStr.Replace(CounterText, new StringBuilder()
                .Append('0', CounterText.Length - strCtr.Length)
                .Append(strCtr).ToString());
        }
        private string GetNextAutoNumber(TableDef td, BusinessEntity Entity,
            IAutoNumberAttribute ana, ref string TemplateStr,
            ref string CounterText, ref int NextCtr)
        {
            if (NextCtr == 0)
            {
                string FieldAutoNumber = ana.GetFieldName();
                TemplateStr = Entity.GetAutoNumberTemplate(FieldAutoNumber);
                if (TemplateStr.Length == 0)
                    TemplateStr = Entity.GetAutoNumberTemplate();

                if (TemplateStr.Length == 0)
                    TemplateStr = ana.GetTemplate();

                // Update DatePart
                if (ana.GetDateField().Length > 0)
                {
                    DateTime TransDate = (DateTime)td.GetFieldDef(
                        ana.GetDateField()).GetValue(Entity);

                    if (TransDate == BaseFramework.ServerDateOrDateTime)
                        TransDate = GetDbDate();

                    string strMonth = TransDate.Month < 10 ?
                        "0" + TransDate.Month.ToString() :
                        TransDate.Month.ToString();

                    TemplateStr = TemplateStr.Replace("YY",
                        TransDate.Year.ToString().Substring(2))
                        .Replace("MM", strMonth);
                }

                string tmpAnd = string.Empty;
                List<FieldParam> Params = new List<FieldParam>();

                if (td.GetFieldDef(ana.GetFieldName())._IsPrimaryKey)
                    foreach (FieldDef fld in td.KeyFields.Values)
                        if (fld._FieldName != FieldAutoNumber)
                        {
                            tmpAnd = string.Concat(tmpAnd, " AND ",
                                fld._FieldName, "=@", fld._FieldName);
                            Params.Add(new FieldParam(fld, Entity));
                        }

                // SELECT MAX(SUBSTRING({0},{1},{2}))+1 FROM {3} 
                // WHERE {0} LIKE {4} AND Keys...

                CounterText = ana.GetCounterText();
                string strCtr = new StringBuilder().Append('_',
                    CounterText.Length).ToString();
                object oValue;
                Params.Add(new FieldParam("0",
                    TemplateStr.Replace(CounterText, strCtr)));
                if (Find.TryFindMaxValue(GetSqlSubString(FieldAutoNumber,
                    TemplateStr.IndexOf(CounterText) + 1,
                    CounterText.Length), td._TableName,
                    string.Concat(FieldAutoNumber, " LIKE @0", tmpAnd),
                    out oValue, Params.ToArray()))
                    NextCtr = int.Parse((string)oValue) + 1;
                else
                    NextCtr = 1;
            }
            string strOut = NextCtr.ToString();
            NextCtr++;
            return TemplateStr.Replace(CounterText, new StringBuilder()
                .Append('0', CounterText.Length - strOut.Length)
                .Append(strOut).ToString());
        }

        protected string GetNextNestedKey(TableDef td, BusinessEntity Entity)
        {
            FieldDef fldParent = td.GetFieldDef(td._AutoNestedKeyAtr._ParentField);
            if (fldParent == null)
                throw new ApplicationException(string.Format(
                    ErrorMetaData.ParentFieldNotFound,
                    string.Concat(td._ClassType.Name, ".",
                    td._AutoNestedKeyAtr._ParentField)));

            FieldDef fldKey = td.GetFieldDef(td._AutoNestedKeyAtr._FieldName);
            string Key = (string)fldParent.GetValue(Entity);

            object TmpVal;
            if (Find.TryFindFirstValue(fldKey._FieldName,
                td._TableName, fldParent._FieldName + "=@0",
                string.Concat(GetSqlLen(fldKey._FieldName), " DESC,",
                fldKey._FieldName, " DESC"), out TmpVal,
                new FieldParam("0", Key)))
            {
                string strKey = (string)TmpVal;
                int LastPos = strKey.LastIndexOf('.');
                int MaxCtr;
                if (LastPos < 0)
                    MaxCtr = int.Parse(strKey) + 1;
                else
                    MaxCtr = int.Parse(strKey.Substring(LastPos + 1)) + 1;
                return string.Concat(
                    strKey.Substring(0, LastPos + 1),
                    MaxCtr.ToString());
            }
            return Key.Length == 0 ? "1" : Key + ".1";
        }

        private bool IsExist(string[] ArrayData, string Data, int NumData)
        {
            for (int i = 0; i < NumData; i++)
                if (ArrayData[i] == Data)
                    return true;
            return false;
        }

        #region UpdateAutoFields
        private class TmpField
        {
            FieldDef fld;
            BusinessEntity Entity;

            public void Clear()
            {
                fld.SetLoadValue(Entity, string.Empty);
            }

            public TmpField(FieldDef fld, BusinessEntity Entity)
            {
                this.fld = fld;
                this.Entity = Entity;
            }
        }

        private void UpdateAutoFields(TableDef td, BusinessEntity Entity, 
            List<TmpField> ListChanged)
        {
            if (td._AutoNumberKeyAtr != null)
            {
                foreach (AutoNumberKeyAttribute anka in td._AutoNumberKeyAtr)
                {
                    FieldDef fld = td.GetFieldDef(anka._FieldName);
                    if (((string)fld.GetValue(Entity)).Trim().Length == 0)
                    {
                        fld.SetLoadValue(Entity, GetNextAutoNumber(td, Entity, anka));
                        ListChanged.Add(new TmpField(fld, Entity));
                    }
                }
            }
            if (td._AutoNestedKeyAtr != null)
            {
                FieldDef fld = td.GetFieldDef(td._AutoNestedKeyAtr._FieldName);
                if (((string)fld.GetValue(Entity)).Trim().Length == 0)
                {
                    fld.SetLoadValue(Entity, GetNextNestedKey(td, Entity));
                    ListChanged.Add(new TmpField(fld, Entity));
                }
            }
            if (td._AutoNumberAtr != null)
            {
                foreach (AutoNumberAttribute ana in td._AutoNumberAtr)
                {
                    FieldDef fld = td.GetFieldDef(ana._FieldName);
                    if (((string)fld.GetValue(Entity)).Trim().Length == 0)
                    {
                        fld.SetLoadValue(Entity, GetNextAutoNumber(td, Entity, ana));
                        ListChanged.Add(new TmpField(fld, Entity));
                    }
                }
            }
            UpdateChildAutoField(td, Entity, ListChanged);
        }
        private void UpdateChildAutoField(TableDef td, BusinessEntity Entity,
            List<TmpField> ListChanged)
        {
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                IList Cols = ecd.GetValue(Entity);
                if (Cols.Count == 0) continue;

                TableDef tdChild = MetaData.GetTableDef(
                    ecd.ChildType);
                bool HasChild = tdChild.ChildEntities.Count > 0;

                if (tdChild._fldCounterField != null)
                {
                    int Ctr = 1;
                    foreach (BusinessEntity ChildE in Cols)
                        tdChild._fldCounterField
                            .SetLoadValue(ChildE, Ctr++);
                }

                if (tdChild._AutoNumberKeyAtr != null)
                    foreach (AutoNumberKeyAttribute anka in tdChild._AutoNumberKeyAtr)
                    {
                        FieldDef fld = tdChild.GetFieldDef(anka._FieldName);

                        string TemplateStr = string.Empty;
                        string CounterText = string.Empty;
                        int NextCtr = 0;
                        foreach (BusinessEntity ChildE in Cols)
                            if (((string)fld.GetValue(ChildE)).Trim().Length == 0)
                            {
                                fld.SetLoadValue(ChildE, GetNextAutoNumber(tdChild,
                                    ChildE, anka, ref TemplateStr,
                                    ref CounterText, ref NextCtr));
                                ListChanged.Add(new TmpField(fld, ChildE));
                            }
                    }
                if (tdChild._AutoNumberAtr != null)
                    foreach (AutoNumberAttribute ana in tdChild._AutoNumberAtr)
                    {
                        FieldDef fld = tdChild.GetFieldDef(ana._FieldName);

                        string TemplateStr = string.Empty;
                        string CounterText = string.Empty;
                        int NextCtr = 0;
                        foreach (BusinessEntity ChildE in Cols)
                            if (((string)fld.GetValue(ChildE)).Trim().Length == 0)
                            {
                                fld.SetLoadValue(ChildE, GetNextAutoNumber(tdChild,
                                    ChildE, ana, ref TemplateStr,
                                    ref CounterText, ref NextCtr));
                                ListChanged.Add(new TmpField(fld, ChildE));
                            }
                    }
                foreach (BusinessEntity ChildE in Cols)
                    UpdateChildAutoField(tdChild, ChildE, ListChanged);
            }
        }
        #endregion

        #region SaveNewEntity
        private void BuildSqlInsertTable(TableDef td, BusinessEntity Entity,
            BatchCommand BatchCmd) //, List<FieldDef> ListReloadField)
        {
            StringBuilder SqlInsert = new StringBuilder("INSERT INTO ");
            StringBuilder ValueList = new StringBuilder();
            FieldParam[] Params = new FieldParam[td.KeyFields.Count +
                td.NonKeyFields.Count];

            SqlInsert.Append(td._TableName).Append("(");
            int fldCtr = 0;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                SqlInsert.Append(fld._FieldName).Append(",");
                ValueList.Append(",@").Append(fld._FieldName);
                Params[fldCtr++] = new FieldParam(fld, Entity);
            }
            foreach (FieldDef fld in td.NonKeyFields.Values)
            {
                if (fld._dtlsa == null)
                {
                    SqlInsert.Append(fld._FieldName)
                        .Append(",");
                    switch (fld.DataType)
                    {
                        case DataType.Date:
                            if ((DateTime)fld.GetValue(Entity) == BaseFramework.ServerDateOrDateTime)
                            {
                                ValueList.Append(",").Append(GetSqlDate());
                                //ListReloadField.Add(fld);
                            }
                            else
                            {
                                ValueList.Append(",@")
                                    .Append(fld._FieldName);
                                Params[fldCtr++] =
                                    new FieldParam(fld, Entity);
                            }
                            break;
                        case DataType.DateTime:
                            if ((DateTime)fld.GetValue(Entity) == BaseFramework.ServerDateOrDateTime)
                            {
                                ValueList.Append(",").Append(GetSqlNow());
                                //ListReloadField.Add(fld);
                            }
                            else
                            {
                                ValueList.Append(",@")
                                    .Append(fld._FieldName);
                                Params[fldCtr++] =
                                    new FieldParam(fld, Entity);
                            }
                            break;
                        case DataType.TimeStamp:
                            ValueList.Append(",").Append(GetSqlNow());
                            //ListReloadField.Add(fld);
                            break;
                        default:
                            ValueList.Append(",@")
                                .Append(fld._FieldName);
                            Params[fldCtr++] = 
                                new FieldParam(fld, Entity);
                            break;
                    }
                }
            }
            if (fldCtr < Params.Length)
                Array.Resize<FieldParam>(ref Params, fldCtr);
            BatchCmd.AddCommand(SqlInsert.Remove(SqlInsert.Length - 1, 1)
                .Append(") VALUES (").Append(ValueList.Remove(0, 1)
                .ToString()).Append(")").ToString(), Params);
        }

        private void CheckChilRelationInsert(TableDef tdChild,
            BusinessEntity ObjChild)
        {
            foreach (RelationAttribute cra in tdChild.ChildRelations)
            {
                TableDef tdParent = MetaData.GetTableDef(cra._ParentType);
                ValidateTableDef(tdParent);
                string SqlTest = string.Empty;
                for (int i = 0; i < cra._ParentFields.Length; i++)
                {
                    FieldDef fldChild = tdChild.GetFieldDef(
                        cra._ChildFields[i]);
                    if (fldChild == null)
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.RelChildFieldNotFound,
                            cra._ParentType.Name, cra.ChildType.Name,
                            cra._ChildFields[i]));
                    object objValue = fldChild.GetValue(ObjChild);

                    if ((fldChild.DataType == DataType.VarChar ||
                        fldChild.DataType == DataType.Char) &&
                        objValue.ToString().Length == 0) continue;
                    SqlTest = string.Concat(SqlTest, " AND ",
                        cra._ParentFields[i], "=",
                        FormatSqlValue(
                        objValue, fldChild._DataTypeAtr._DataType));
                }
                if (SqlTest.Length > 0)
                {
                    SqlTest = string.Concat("SELECT ",
                        cra._ParentFields[0],
                        " FROM ", tdParent.GetSqlHeaderView(this),
                        " WHERE ", SqlTest.Substring(5));
                    if (!Find.IsExists(SqlTest))
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.ChildInsertRestrict,
                            tdChild._TableName, tdParent._TableName));
                }
            }
        }

        private void __SaveNewEntity(TableDef td, BusinessEntity Entity, 
            BatchCommand BatchCmd)
        {
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                IList Cols = ecd.GetValue(Entity);
                if (Cols.Count == 0) continue;

                TableDef tdChild = MetaData.GetTableDef(
                    ecd.ChildType);
                if (tdChild.KeyFields.Count == td.KeyFields.Count) continue;

                bool HasChild = tdChild.ChildEntities.Count > 0;

                foreach (BusinessEntity ChildE in Cols)
                    if (ChildE.IsValidToSave())
                    {
                        if (tdChild.ChildRelations.Count > 0)
                            CheckChilRelationInsert(tdChild, ChildE);

                        BuildSqlInsertTable(tdChild, ChildE, BatchCmd);
                        if (HasChild)
                            __SaveNewEntity(tdChild, ChildE, BatchCmd);
                    }
            }
        }

        public int SaveNewEntity(ParentEntity Entity, bool CallSaveRule,
            bool CallValidateError)
        {
            return Entity.SaveNew(this, CallSaveRule, CallValidateError);
        }

        // dipanggil oleh business entity
        internal int _SaveNewEntity(ParentEntity Entity, bool CallSaveRule, 
            bool CallValidateError)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);

            IRuleInitUI pe = (IRuleInitUI)Entity;

            if (CallValidateError)
            {
                Entity.ClearError();
                Entity.ValidateError();
            }

            if (!Entity.IsTransDateValid())
                Entity.AddError(td.fldTransactionDate._FieldName,
                    string.Concat(BaseUtility.SplitName(td.fldTransactionDate._FieldName),
                    " tidak boleh kurang dari Tgl Penguncian Transaksi"));

            List<TmpField> ListChanged = new List<TmpField>();
            //List<FieldDef> ListReloadField = new List<FieldDef>();

            BatchCommand BatchCmd = new BatchCommand();
            try
            {
                int RetVal = 0;
                using (EntityTransaction tr = new EntityTransaction(this))
                {
                    BaseFramework.DoEntityAction(Entity, 
                        enEntityActionMode.BeforeSaveNew);
                    if (CallSaveRule) pe.BeforeSaveNew();

                    UpdateAutoFields(td, Entity, ListChanged);

                    if (CallValidateError) Entity.CheckEmptyFields(td);

                    if (Entity.IsErrorExist())
                        throw new ApplicationException(Entity.GetErrorString());

                    #region DoSaveNew
                    BuildSqlInsertTable(td, Entity, BatchCmd);

                    if (td.ChildEntities.Count > 0)
                        __SaveNewEntity(td, Entity, BatchCmd);
                    try
                    {
                        RetVal = ExecuteInternalBatchNonQuery(BatchCmd, true);
                    }
                    catch (Exception ex)
                    {
                        ProcessDbError(Entity, ex);
                    }
                    if (RetVal == 0)
                        throw new ApplicationException(string.Format(
                            ErrorEntity.InsertZero, td._TableName));
                    #endregion

                    if (td.ChildRelations.Count > 0)
                        CheckChilRelationInsert(td, Entity);

                    if (CallSaveRule) pe.AfterSaveNew();
                    BaseFramework.DoEntityAction(Entity, enEntityActionMode.AfterSaveNew);
                    if (Entity.IsErrorExist())
                        throw new ApplicationException(Entity.GetErrorString());
                    tr.CommitTransaction();
                }
                AutoUpdateList.EntityAdded(this, td.ClassType, Entity);
                //Entity.ClearError();
                if (Entity._Original != null)
                    MetaData.CloneToOriginal(Entity);
                Entity.IsLoadedEntity = true;

                //if (ListReloadField.Count > 0)
                //{
                // Reload semua field termasuk anak2nya..
                // belum perlu ya.. kasihan...
                //}
                return RetVal;
            }
            catch (Exception ex)
            {
                try
                {
                    pe.ErrorAfterSaveNew();
                }
                catch (Exception ex2)
                {
                    ex = ex2;
                }
                foreach (TmpField tmp in ListChanged)
                    tmp.Clear();
                throw ex;
            }
        }
        #endregion

        #region SaveUpdateEntity
        // ParentUpdate hanya berlaku untuk ParentEntity, Child tidak berlaku, karena :
        // isi child bisa berubah-ubah, gimana mencari old Primary Key ?
        // kalau itu terjawab, ParentUpdate bisa ada di child...
        // Update Where OrigEntity = null
        private void BuildSqlUpdateTable(TableDef td, 
            BusinessEntity NewEntity, BatchCommand BatchCmd, 
            string UpdateCondition, List<FieldDef> ListReloadField)
        {
            if (td.NonKeyFields.Count == 0) return;
            StringBuilder SqlUpdate = new StringBuilder("UPDATE ");
            SqlUpdate.Append(td._TableName).Append(" SET ");
            List<FieldParam> Params = new List<FieldParam>();

            foreach (FieldDef fld in td.NonKeyFields.Values)
            {
                if (fld._dtlsa != null) continue;
                switch (fld.DataType)
                {
                    case DataType.Date:
                        if ((DateTime)fld.GetValue(NewEntity) == BaseFramework.ServerDateOrDateTime)
                        {
                            SqlUpdate.Append(fld._FieldName).Append("=")
                                .Append(GetSqlDate()).Append(",");
                            ListReloadField.Add(fld);
                        }
                        else
                        {
                            SqlUpdate.Append(fld._FieldName)
                                .Append("=@").Append(fld._FieldName).Append(",");
                            Params.Add(new FieldParam(fld, NewEntity));
                        }
                        break;
                    case DataType.DateTime:
                        if ((DateTime)fld.GetValue(NewEntity) == BaseFramework.ServerDateOrDateTime)
                        {
                            SqlUpdate.Append(fld._FieldName).Append("=")
                                .Append(GetSqlNow()).Append(",");
                            ListReloadField.Add(fld);
                        }
                        else
                        {
                            SqlUpdate.Append(fld._FieldName)
                                .Append("=@").Append(fld._FieldName).Append(",");
                            Params.Add(new FieldParam(fld, NewEntity));
                        }
                        break;
                    case DataType.TimeStamp:
                        SqlUpdate.Append(fld._FieldName).Append("=")
                            .Append(GetSqlNow()).Append(",");
                        ListReloadField.Add(fld);
                        break;
                    default:
                        SqlUpdate.Append(fld._FieldName)
                            .Append("=@").Append(fld._FieldName).Append(",");
                        Params.Add(new FieldParam(fld, NewEntity));
                        break;
                }
            }

            SqlUpdate.Remove(SqlUpdate.Length - 1, 1);

            if (td.fldTimeStamp != null)
            {
                Params.Add(new FieldParam(td.fldTimeStamp, NewEntity));
                if (UpdateCondition.Length > 0)
                    SqlUpdate.Append(" WHERE ")
                        .Append(UpdateCondition).Append(" AND ")
                        .Append(td.fldTimeStamp._FieldName)
                        .Append("=@").Append(td.fldTimeStamp
                        ._FieldName);
                else
                    SqlUpdate.Append(" WHERE ")
                        .Append(td.fldTimeStamp._FieldName)
                        .Append("=@").Append(td.fldTimeStamp._FieldName);
            }
            else if (UpdateCondition.Length > 0)
                SqlUpdate.Append(" WHERE ")
                    .Append(UpdateCondition);
            BatchCmd.AddCommand(SqlUpdate.ToString(), Params);
        }

        // Update Where OrigEntity Exist
        private void BuildSqlUpdateTable(TableDef td,
            BusinessEntity NewEntity, BusinessEntity OriEntity, BatchCommand BatchCmd,
            string UpdateCondition, out bool KeyChanged, List<FieldDef> ListReloadField)
        {
            StringBuilder SqlUpdate = new StringBuilder();
            KeyChanged = false;
            List<FieldParam> Params = new List<FieldParam>();

            foreach (FieldDef fld in td.KeyFields.Values)
            {
                object OldVal = fld.GetValue(OriEntity);
                object NewVal = fld.GetValue(NewEntity);

                if (!object.Equals(NewVal, OldVal))
                {
                    KeyChanged = true;
                    SqlUpdate.Append(fld._FieldName).Append("=@")
                        .Append(fld._FieldName).Append(",");
                    Params.Add(new FieldParam(fld, NewVal));
                }
            }

            #region Belum Perlu...
            //Dictionary<string, int> ListUniqueFields = new Dictionary<string, int>();
            //if (!KeyChanged && td.ParentRelations.Count > 0)
            //    foreach (string strIndex in td.IndexedFields)
            //    {
            //        string[] TmpIndex = strIndex.Split('|');
            //        if (TmpIndex[0] != "Unique") continue;
            //        string[] TmpIndexes = TmpIndex[1].Split(',');
            //        string NewUnique = string.Empty;
            //        foreach (string idxField in TmpIndexes)
            //        {
            //            string FldName;
            //            string TmpField = idxField.Trim();
            //            if (TmpField.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase))
            //                FldName = TmpField.Substring(0, TmpField.Length - 4).TrimEnd();
            //            else if (TmpField.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
            //                FldName = TmpField.Substring(0, TmpField.Length - 5).TrimEnd();
            //            else
            //                FldName = TmpField;
            //            ListUniqueFields[FldName] = 0;
            //        }
            //    }
            #endregion

            foreach (FieldDef fld in td.NonKeyFields.Values)
            {
                if (fld._dtlsa != null) continue;
                switch (fld._DataTypeAtr._DataType)
                {
                    case DataType.Date:
                        if ((DateTime)fld.GetValue(NewEntity) == BaseFramework.ServerDateOrDateTime)
                        {
                            SqlUpdate.Append(fld._FieldName).Append("=")
                                .Append(GetSqlDate()).Append(",");
                            ListReloadField.Add(fld);
                        }
                        else
                        {
                            object NewVal = fld.GetValue(NewEntity);
                            if (NewVal == null) NewVal = fld.GetDefaultValue();
                            object OldVal = fld.GetValue(OriEntity);
                            if (OldVal == null) OldVal = fld.GetDefaultValue();
                            if (!object.Equals(OldVal, NewVal))
                            {
                                SqlUpdate.Append(fld._FieldName)
                                    .Append("=@").Append(fld._FieldName).Append(",");
                                Params.Add(new FieldParam(fld, NewVal));
                            }
                        }
                        break;
                    case DataType.DateTime:
                        if ((DateTime)fld.GetValue(NewEntity) == BaseFramework.ServerDateOrDateTime)
                        {
                            SqlUpdate.Append(fld._FieldName).Append("=")
                                .Append(GetSqlNow()).Append(",");
                            ListReloadField.Add(fld);
                        }
                        else
                        {
                            object NewVal = fld.GetValue(NewEntity);
                            if (NewVal == null) NewVal = fld.GetDefaultValue();
                            object OldVal = fld.GetValue(OriEntity);
                            if (OldVal == null) OldVal = fld.GetDefaultValue();
                            if (!object.Equals(OldVal, NewVal))
                            {
                                SqlUpdate.Append(fld._FieldName)
                                    .Append("=@").Append(fld._FieldName).Append(",");
                                Params.Add(new FieldParam(fld, NewVal));
                            }
                        }
                        break;
                    case DataType.TimeStamp:
                        SqlUpdate.Append(fld._FieldName).Append("=")
                            .Append(GetSqlNow()).Append(",");
                        ListReloadField.Add(fld);
                        break;
                    default:
                        {
                            object NewVal = fld.GetValue(NewEntity);
                            if (NewVal == null) NewVal = fld.GetDefaultValue();
                            object OldVal = fld.GetValue(OriEntity);
                            if (OldVal == null) OldVal = fld.GetDefaultValue();
                            if (!object.Equals(OldVal, NewVal))
                            {
                                SqlUpdate.Append(fld._FieldName)
                                    .Append("=@").Append(fld._FieldName).Append(",");
                                Params.Add(new FieldParam(fld, NewVal));
                                //if (!KeyChanged && ListUniqueFields.ContainsKey(fld._FieldName))
                                //    KeyChanged = true;
                            }
                        }
                        break;
                }
            }

            if (SqlUpdate.Length == 0) return;

            SqlUpdate.Insert(0, string.Concat("UPDATE ",
                td._TableName, " SET "));

            SqlUpdate.Remove(SqlUpdate.Length - 1, 1);

            if (td.fldTimeStamp != null)
            {
                Params.Add(new FieldParam(td.fldTimeStamp, 
                    OriEntity));
                if (UpdateCondition.Length > 0)
                    UpdateCondition += " AND ";
                SqlUpdate.Append(" WHERE ").Append(UpdateCondition)
                    .Append(td.fldTimeStamp._FieldName)
                    .Append("=@").Append(td.fldTimeStamp
                    ._FieldName);
            }
            else if (UpdateCondition.Length > 0)
                    SqlUpdate.Append(" WHERE ").Append(UpdateCondition);

            BatchCmd.AddCommand(SqlUpdate.ToString(), Params);
        }

        [DebuggerNonUserCode]
        private class OldNewValue
        {
            public string FieldName;
            public DataType FieldType;
            public object OldValue;
            public object NewValue;
            public OldNewValue(string FieldName, DataType FieldType,
                object OldValue, object NewValue)
            {
                this.FieldName = FieldName;
                this.FieldType = FieldType;
                this.OldValue = OldValue;
                this.NewValue = NewValue;
            }
        }

        private void CheckParentUpdate(TableDef tdParent,
            BusinessEntity NewEntity, BusinessEntity OrigEntity, BatchCommand BatchCmd)
        {
            foreach (RelationAttribute pra in tdParent.ParentRelations)
                if (pra._ParentUpdateRule == ParentUpdate.UpdateRestrict)
                {
                    string SqlTest = string.Empty;
                    for (int i = 0; i < pra._ParentFields.Length; i++)
                    {
                        FieldDef fldParent = tdParent.GetFieldDef(pra._ParentFields[i]);
                        if (fldParent == null)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.RelParentFieldNotFound,
                                pra._ParentType.Name, pra.ChildType.Name,
                                pra._ParentFields[i]));
                        SqlTest = string.Concat(SqlTest,
                            " AND ", pra._ChildFields[i], "=",
                            FormatSqlValue(fldParent.GetValue(OrigEntity),
                            fldParent._DataTypeAtr._DataType));
                    }
                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    SqlTest = string.Concat("SELECT ", pra._ChildFields[0],
                        " FROM ", tdChild._TableName, " WHERE ",
                        SqlTest.Substring(5));
                    if (Find.IsExists(SqlTest))
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.ParentUpdateRestrict,
                            tdParent._TableName, tdChild._TableName));
                }
                else
                {
                    string SqlUpdate = string.Empty;
                    string SqlUpdate2 = string.Empty;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    List<OldNewValue> UpdateFields = new List<OldNewValue>();
                    for (int i = 0; i < pra._ParentFields.Length; i++)
                    {
                        FieldDef fldParent = tdParent.GetFieldDef(pra._ParentFields[i]);
                        if (fldParent == null)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.RelParentFieldNotFound,
                                pra._ParentType.Name, pra.ChildType.Name,
                                pra._ParentFields[i]));

                        object origValue = fldParent.GetValue(OrigEntity);
                        object newValue = fldParent.GetValue(NewEntity);
                        string ChildFieldName = pra._ChildFields[i];

                        SqlUpdate = string.Concat(SqlUpdate,
                            " AND ", ChildFieldName, "=",
                            FormatSqlValue(origValue,
                            fldParent._DataTypeAtr._DataType));

                        if (origValue != newValue)
                        {
                            SqlUpdate2 = string.Concat(SqlUpdate2,
                                ",", ChildFieldName, "=",
                                FormatSqlValue(newValue,
                                fldParent._DataTypeAtr._DataType));
                            FieldDef fldChild = tdChild.GetFieldDef(ChildFieldName);
                            if (fldChild == null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.RelChildFieldNotFound,
                                    pra._ParentType.Name, pra.ChildType.Name,
                                    ChildFieldName));
                            if (fldChild.IsParentField)
                                UpdateFields.Add(new OldNewValue(ChildFieldName,
                                    fldParent.DataType, origValue, newValue));
                        }
                    }
                    if (SqlUpdate2.Length > 0)
                    {
                        string UpdateTs = string.Empty;

                        if (tdChild.fldTimeStamp != null)
                            UpdateTs = string.Concat(
                                tdChild.fldTimeStamp
                                ._FieldName, "=",
                                GetSqlNow(), ",");

                        BatchCmd.AddCommand(string.Concat("UPDATE ",
                            tdChild._TableName, " SET ",
                            UpdateTs, SqlUpdate2.Substring(1), " WHERE ",
                            SqlUpdate.Substring(5)));
                        if (UpdateFields.Count > 0)
                            CheckParentUpdate2(tdChild, UpdateFields, BatchCmd);
                    }
                }
        }

        private void CheckParentUpdate2(TableDef tdParent,
            List<OldNewValue> UpdateFields, BatchCommand BatchCmd)
        {
            foreach (RelationAttribute pra in tdParent.ParentRelations)
                if (pra._ParentUpdateRule == ParentUpdate.UpdateRestrict)
                {
                    string SqlTest = string.Empty;
                    foreach (OldNewValue onv in UpdateFields)
                        for (int i = 0; i < pra._ParentFields.Length; i++)
                            if (pra._ParentFields[i] == onv.FieldName)
                            {
                                SqlTest = string.Concat(SqlTest, " AND ",
                                    pra._ChildFields[i], "=",
                                    FormatSqlValue(onv.OldValue, onv.FieldType));
                                break;
                            }
                    if (SqlTest.Length == 0) continue;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    SqlTest = string.Concat("SELECT ", pra._ChildFields[0],
                        " FROM ", tdChild._TableName, " WHERE ",
                        SqlTest.Substring(5));
                    if (Find.IsExists(SqlTest))
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.ParentUpdateRestrict,
                            tdParent._TableName, tdChild._TableName));
                }
                else
                {
                    string SqlUpdate = string.Empty;
                    string SqlUpdate2 = string.Empty;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    List<OldNewValue> UpdateChildFields = new List<OldNewValue>();
                    foreach (OldNewValue onv in UpdateFields)
                        for (int i = 0; i < pra._ParentFields.Length; i++)
                        {
                            if (pra._ParentFields[i] != onv.FieldName) continue;
                            FieldDef fldParent = tdParent.GetFieldDef(onv.FieldName);
                            if (fldParent == null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.RelParentFieldNotFound,
                                    pra._ParentType.Name, pra.ChildType.Name,
                                    pra._ParentFields[i]));

                            string ChildFieldName = pra._ChildFields[i];

                            SqlUpdate = string.Concat(SqlUpdate,
                                " AND ", ChildFieldName, "=",
                                FormatSqlValue(onv.OldValue, onv.FieldType));

                            SqlUpdate2 = string.Concat(SqlUpdate2,
                                ",", ChildFieldName, "=",
                                FormatSqlValue(onv.NewValue, onv.FieldType));
                            FieldDef fldChild = tdChild.GetFieldDef(ChildFieldName);
                            if (fldChild == null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.RelParentFieldNotFound,
                                    pra._ParentType.Name, pra.ChildType.Name,
                                    ChildFieldName));

                            if (fldChild.IsParentField)
                            {
                                onv.FieldName = ChildFieldName;
                                UpdateChildFields.Add(onv);
                            }
                            break;
                        }
                    if (SqlUpdate2.Length > 0)
                    {
                        string UpdateTs = string.Empty;

                        if (tdChild.fldTimeStamp != null)
                            UpdateTs = string.Concat(
                                tdChild.fldTimeStamp
                                ._FieldName, "=",
                                GetSqlNow(), ",");

                        BatchCmd.AddCommand(string.Concat("UPDATE ",
                            tdChild._TableName, " SET ",
                            UpdateTs, SqlUpdate2.Substring(1), " WHERE ",
                            SqlUpdate.Substring(5)));

                        if (UpdateChildFields.Count > 0)
                            CheckParentUpdate2(tdChild,
                                UpdateChildFields, BatchCmd);
                    }
                }
        }

        private void __SaveUpdateEntity(TableDef td, BusinessEntity Entity,
            BatchCommand BatchCmd)
        {
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                TableDef tdChild = MetaData.GetTableDef(ecd.GetChildType());
                if (tdChild.KeyFields.Count == td.KeyFields.Count) continue;

                foreach (BusinessEntity ChildBE in ecd.GetValue(Entity))
                    if (ChildBE.IsValidToSave())
                    {
                        if (tdChild.ChildRelations.Count > 0)
                            CheckChilRelationInsert(tdChild, ChildBE);

                        BuildSqlInsertTable(tdChild, ChildBE, BatchCmd);
                        if (tdChild.ChildEntities.Count > 0)
                            __SaveUpdateEntity(tdChild, ChildBE, BatchCmd);
                    }
            }
        }

        private void _RecursiveDeleteTable(BatchCommand BatchCmd, 
            TableDef td, string Condition, 
            params FieldParam[] Parameters)
        {
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                TableDef tdChild = MetaData.GetTableDef(
                    ecd.GetChildType());
                if (tdChild.KeyFields.Count == td.KeyFields.Count) continue;

                BatchCmd.AddCommand(string.Concat("DELETE FROM ",
                    tdChild._TableName,
                    Condition.Length > 0 ? " WHERE " + Condition :
                    string.Empty), Parameters);
                _RecursiveDeleteTable(BatchCmd, tdChild,
                    Condition, Parameters);
            }
        }

        public int SaveUpdateEntity(ParentEntity NewEntity,
            bool CallSaveRule, bool CallValidateError)
        {
            return NewEntity.SaveUpdate(this, CallSaveRule, CallValidateError);
        }

        // dipanggil oleh business entity
        internal int _SaveUpdateEntity(ParentEntity NewEntity, 
            bool CallSaveRule, bool CallValidateError)
        {
            IRuleInitUI pe = (IRuleInitUI)NewEntity;

            TableDef td = MetaData.GetTableDef(NewEntity.GetType());
            ValidateTableDef(td);

            if (CallValidateError)
            {
                NewEntity.ClearError();
                NewEntity.ValidateError();
            }

            if (!NewEntity.IsTransDateValid())
                NewEntity.AddError(td.fldTransactionDate._FieldName,
                    BaseUtility.SplitName(td.fldTransactionDate
                    ._FieldName) + " tidak boleh kurang dari Tgl Penguncian Transaksi");

            string UpdateCondition = string.Empty;
            string CheckCondition = string.Empty;

            BatchCommand BatchCmd = new BatchCommand();

            int RetVal = 0;
            bool DataChanged = false;
            List<TmpField> ListChanged = new List<TmpField>();
            List<FieldDef> ListReloadField = new List<FieldDef>();

            try
            {
                if ((!NewEntity.IsLoadedEntity) && td.fldTimeStamp != null)
                {
                    FieldParam[] Params = new FieldParam[td.KeyFields.Count];
                    if (Params.Length > 0)
                    {
                        string Criteria = string.Empty;
                        int Ctr = 0;
                        foreach (FieldDef fld in td.KeyFields.Values)
                        {
                            Criteria = string.Concat(Criteria, " AND ",
                                fld._FieldName, "=@", Ctr.ToString());
                            Params[Ctr] = new FieldParam(Ctr.ToString(), fld.GetValue(NewEntity));
                            Ctr++;
                        }
                        object TmpObj;
                        if (Find.TryFindValue(string.Concat("SELECT ",
                            td.fldTimeStamp._FieldName, " FROM ", td._TableName, " WHERE ",
                            Criteria.Substring(5)), out TmpObj, Params))
                            td.fldTimeStamp.SetLoadValue(NewEntity, TmpObj);
                    }
                }

                using (EntityTransaction tr = new EntityTransaction(this))
                {
                    BaseFramework.DoEntityAction(NewEntity, enEntityActionMode.BeforeSaveUpdate);

                    if (CallSaveRule) pe.BeforeSaveUpdate();

                    _CheckTimeStampUpdate(td, NewEntity);

                    UpdateAutoFields(td, NewEntity, ListChanged);

                    if (CallValidateError) NewEntity.CheckEmptyFields(td);

                    if (NewEntity.IsErrorExist())
                        throw new ApplicationException(NewEntity
                            .GetErrorString());

                    bool ParentChanged = false;
                    #region BuildSqlUpdateTable
                    if (NewEntity._Original == null)
                    {
                        foreach (FieldDef fd in td.KeyFields.Values)
                            UpdateCondition = string.Concat(UpdateCondition,
                                " AND ", fd._FieldName, "=",
                                FormatSqlValue(fd.GetValue(NewEntity),
                                fd._DataTypeAtr._DataType));
                        if (UpdateCondition.Length > 0)
                            UpdateCondition = UpdateCondition.Remove(0, 5);
                        CheckCondition = UpdateCondition;
                        BuildSqlUpdateTable(td, NewEntity, BatchCmd,
                            UpdateCondition, ListReloadField);
                    }
                    else
                    {
                        if (td.fldTimeStamp != null)
                        {
                            foreach (FieldDef fd in td.KeyFields.Values)
                            {
                                UpdateCondition = string.Concat(UpdateCondition,
                                    " AND ", fd._FieldName, "=",
                                    FormatSqlValue(fd.GetValue(NewEntity._Original),
                                    fd._DataTypeAtr._DataType));

                                CheckCondition = string.Concat(CheckCondition,
                                    " AND ", fd._FieldName, "=",
                                    FormatSqlValue(fd.GetValue(NewEntity),
                                    fd._DataTypeAtr._DataType));
                            }
                            if (UpdateCondition.Length > 0)
                            {
                                UpdateCondition = UpdateCondition.Substring(5);
                                CheckCondition = CheckCondition.Substring(5);
                            }
                        }
                        else
                        {
                            foreach (FieldDef fd in td.KeyFields.Values)
                                UpdateCondition = string.Concat(UpdateCondition,
                                    " AND ", fd._FieldName, "=",
                                    FormatSqlValue(fd.GetValue(NewEntity._Original),
                                    fd._DataTypeAtr._DataType));

                            if (UpdateCondition.Length > 0)
                                UpdateCondition = UpdateCondition.Substring(5);
                        }

                        BuildSqlUpdateTable(td, NewEntity, NewEntity._Original,
                            BatchCmd, UpdateCondition, out ParentChanged, 
                            ListReloadField);
                    }
                    #endregion

                    if (td.ChildEntities.Count > 0)
                    {
                        _RecursiveDeleteTable(BatchCmd, td,
                            UpdateCondition);
                        __SaveUpdateEntity(td, NewEntity, BatchCmd);
                    }
                    if (BatchCmd.CommandCount > 0)
                    {
                        DataChanged = true;
                        try
                        {
                            RetVal = ExecuteInternalBatchNonQuery(BatchCmd, false);
                        }
                        catch (Exception ex)
                        {
                            ProcessDbError(NewEntity, ex);
                        }
                        if (RetVal == 0 && !BatchCmd.OnlyDeleteCommand())
                            throw new ApplicationException(string.Format(
                                ErrorPersistance.DataNotFound, td._TableName));
                        if (td.ChildRelations.Count > 0)
                            CheckChilRelationInsert(td, NewEntity);

                        if (ParentChanged && td.ParentRelations.Count > 0)
                        {
                            BatchCmd.ClearCommand();
                            CheckParentUpdate(td, NewEntity, NewEntity._Original,
                                BatchCmd);
                            if (BatchCmd.CommandCount > 0)
                                ExecuteInternalBatchNonQuery(BatchCmd, false);
                        }
                    }
                    if (CallSaveRule) pe.AfterSaveUpdate();
                    BaseFramework.DoEntityAction(NewEntity, enEntityActionMode.AfterSaveUpdate);
                    if (NewEntity.IsErrorExist())
                        throw new ApplicationException(NewEntity
                            .GetErrorString());

                    tr.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                try
                {
                    pe.ErrorAfterSaveUpdate();
                }
                catch (Exception ex2)
                {
                    ex = ex2;
                }

                foreach (TmpField tmp in ListChanged)
                    tmp.Clear();
                throw ex;
            }

            if (DataChanged)
            {
                object[] Tmp;

                // Update Time Stamp Field if Exist
                if (ListReloadField.Count > 0)
                {
                    string TmpStr = string.Empty;
                    foreach (FieldDef fld in ListReloadField)
                        TmpStr = string.Concat(TmpStr, ",", fld._FieldName);
                    if (!Find.TryFindValues(TmpStr.Substring(1), 
                        td._TableName, CheckCondition, out Tmp))
                        throw new ApplicationException(string.Concat(
                            ErrorPersistance.DataNotFound, td._TableName));
                    for (int i = 0; i < Tmp.Length; i++)
                        ListReloadField[i].SetLoadValue(NewEntity, Tmp[i]);
                }

                // kalo ada anak yg punya serverdateortime tidak diupdate..
                // kasihan...
                foreach (EntityCollDef ecd in td.ChildEntities)
                {
                    TableDef tdChild = ecd.GetTableDef();
                    FieldDef fldTS = tdChild.fldTimeStamp;

                    if (fldTS != null)
                    {
                        string SqlQuery = string.Empty;
                        FieldParam[] p = new FieldParam[tdChild.KeyFields.Count];

                        int i = 0;
                        foreach (FieldDef fld in tdChild.KeyFields.Values)
                        {
                            SqlQuery = string.Concat(SqlQuery, " AND ", fld._FieldName,
                                   "=@", i.ToString());
                            p[i] = new FieldParam(i.ToString(), fld);
                            i++;
                        }
                        IDbCommand CmdData = CreateCommand(string.Concat("SELECT ", fldTS._FieldName,
                            " FROM ", tdChild._TableName, " WHERE ", SqlQuery.Substring(5)),
                            CommandType.Text, p);

                        bool MustClose;
                        if (Connection.State == ConnectionState.Closed)
                        {
                            Connection.Open();
                            MustClose = true;
                        }
                        else MustClose = false;

                        // Update AutoKey Field...
                        foreach (BaseEntity be in (IList)ecd.GetCollValue(NewEntity))
                        {
                            i = 0;
                            foreach (FieldDef fld in tdChild.KeyFields.Values)
                                ((IDataParameter)CmdData.Parameters[i++]).Value =
                                    fld.GetValue(be);

                            IDataReader rdr = CmdData.ExecuteReader();
                            if (rdr.Read())
                                fldTS.SetLoadValue(be, (DateTime)rdr[0]);
                            else
                            {
                                rdr.Close();
                                if (MustClose) Connection.Close();
                                throw new ApplicationException(string.Concat(
                                    ErrorPersistance.DataNotFound, td._TableName));
                            }
                            rdr.Close();
                        }
                        if (MustClose) Connection.Close();
                    }
                }

                AutoUpdateList.EntityEdited(this, td.ClassType, NewEntity);
            }
            if (NewEntity._Original != null)
                MetaData.CloneToOriginal(NewEntity);

            return RetVal;
        }

        private void _CheckTimeStampUpdate(TableDef td, BaseEntity NewEntity)
        {
            ParentEntity pe = NewEntity as ParentEntity;
            BaseEntity TmpEntity;
            if (pe == null)
                TmpEntity = NewEntity;
            else
                TmpEntity = (BaseEntity) pe._Original ?? NewEntity;

            if (pe != null && td.fldTimeStamp != null && (DateTime)td.fldTimeStamp
                .GetValue(TmpEntity) != DateTime.MinValue)
            {
                string strQuery = string.Empty;
                FieldParam[] Params = new FieldParam[td.KeyFields.Count + 1];
                int Ctr = 1;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    strQuery = string.Concat(strQuery, " AND ", fld._FieldName, "=@", 
                        Ctr.ToString());
                    Params[Ctr] = new FieldParam(Ctr.ToString(), fld.GetValue(NewEntity));
                    Ctr++;
                }
                Params[0] = new FieldParam("0", td.fldTimeStamp.GetValue(TmpEntity));
                if (!Find.IsExists(string.Concat("SELECT ", td.fldTimeStamp.FieldName,
                    " FROM ", td._TableName, " WHERE ", td.fldTimeStamp.FieldName, "=@0", 
                    strQuery), Params))
                    throw new ApplicationException(string.Format(
                        ErrorPersistance.DataNotFound, td._TableName));
            }
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                TableDef tdChild = ecd.GetTableDef();
                IList ListEntity = ecd.GetValue(NewEntity);

                FieldDef fldTimeStamp = tdChild.fldTimeStamp;
                if (fldTimeStamp != null)
                {
                    string strQuery = string.Empty;
                    FieldParam[] Params = new FieldParam[tdChild.KeyFields.Count + 1];
                    int Ctr = 1;

                    foreach (FieldDef fld in tdChild.KeyFields.Values)
                    {
                        strQuery = string.Concat(" AND ", fld._FieldName, "=@",
                            Ctr.ToString());
                        Params[Ctr] = new FieldParam(Ctr.ToString(), fld);
                        Ctr++;
                    }
                    strQuery = string.Concat("SELECT ", fldTimeStamp._FieldName,
                        " FROM ", tdChild._TableName, " WHERE ",
                        fldTimeStamp._FieldName,
                        "=@0", strQuery);
                    Params[0] = new FieldParam("0", fldTimeStamp);

                    foreach (BaseEntity objEntity in ListEntity)
                    {
                        if ((DateTime)fldTimeStamp.GetValue(objEntity) != DateTime.MinValue)
                        {
                            Params[0].Value = fldTimeStamp.GetValue(objEntity);

                            Ctr = 1;
                            foreach (FieldDef fld in tdChild.KeyFields.Values)
                                Params[Ctr++].Value = fld.GetValue(objEntity);

                            if (!Find.IsExists(strQuery, Params))
                                throw new ApplicationException(string.Format(
                                    ErrorPersistance.DataNotFound, tdChild._TableName));
                        }
                    }
                }
                if (tdChild.ChildEntities.Count > 0)
                    foreach (BaseEntity objEntity in ListEntity)
                        _CheckTimeStampUpdate(tdChild, objEntity);
            }
        }
        #endregion

        public int FastSaveUpdateEntity(BusinessEntity Entity,
            string FieldNames, params object[] ParentFieldValues)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);

            IChildEntity icd = Entity as IChildEntity;
            if (icd != null)
            {
                BusinessEntity be = icd.CreateParent();
                TableDef tdParent = MetaData.GetTableDef(be.GetType());
                int i = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                    if (fld.IsParentField)
                        tdParent.GetFieldDef(fld.FieldName)
                            .SetLoadValue(be, ParentFieldValues[i++]);
            }

            List<TmpField> ListChanged = new List<TmpField>();
            UpdateAutoFields(td, Entity, ListChanged);

            string UpdateCondition = string.Empty;
            List<FieldParam> ParamsUpdate = new List<FieldParam>();
            string FieldUpdate = string.Empty;

            #region BuildSqlUpdateTable
            foreach (FieldDef fd in td.KeyFields.Values)
            {
                UpdateCondition = string.Concat(UpdateCondition,
                    " AND ", fd._FieldName, "=@", fd._FieldName);
                ParamsUpdate.Add(new FieldParam(fd, Entity));
            }

            if (UpdateCondition.Length > 0)
                UpdateCondition = UpdateCondition.Remove(0, 5);
            #endregion

            if (td.fldTimeStamp != null)
                FieldUpdate = string.Concat(",", td.fldTimeStamp._FieldName, "=", GetSqlNow());

            foreach(string fldName in FieldNames.Split(','))
            {
                FieldDef fld = td.GetFieldDef(fldName);
                if (fld == null)
                    throw new ApplicationException(string.Concat(
                        "Field '", fldName, "' tidak ditemukan !"));
                switch (fld._DataTypeAtr._DataType)
                {
                    case DataType.Date:
                        if ((DateTime)fld.GetValue(Entity) == BaseFramework.ServerDateOrDateTime)
                            FieldUpdate = string.Concat(FieldUpdate, ",",
                                fldName, "=", GetSqlDate());
                        else
                        {
                            FieldUpdate = string.Concat(FieldUpdate, ",",
                                fldName, "=@", fldName);
                            ParamsUpdate.Add(new FieldParam(fld, Entity));
                        }
                        break;
                    case DataType.DateTime:
                        if ((DateTime)fld.GetValue(Entity) == BaseFramework.ServerDateOrDateTime)
                            FieldUpdate = string.Concat(FieldUpdate, ",",
                                fldName, "=", GetSqlNow());
                        else
                        {
                            FieldUpdate = string.Concat(FieldUpdate, ",",
                                fldName, "=@", fldName);
                            ParamsUpdate.Add(new FieldParam(fld, Entity));
                        }
                        break;
                    case DataType.TimeStamp:
                        break;
                    default:
                        FieldUpdate = string.Concat(FieldUpdate, ",",
                            fldName, "=@", fldName);
                        ParamsUpdate.Add(new FieldParam(fld, Entity));
                        break;
                }
            }
            try
            {
                return ExecuteNonQuery(string.Concat(
                    "UPDATE ", td._TableName, " SET ",
                    FieldUpdate.Remove(0, 1), " WHERE ",
                    UpdateCondition), ParamsUpdate);
            }
            catch (Exception ex)
            {
                foreach (TmpField tmp in ListChanged)
                    tmp.Clear();
                throw ex;
            }
        }

        public int FastSaveNewEntity(BusinessEntity Entity,
            params object[] ParentFieldValues)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);

            IChildEntity icd = Entity as IChildEntity;
            if (icd != null)
            {
                BusinessEntity be = icd.CreateParent();
                TableDef tdParent = MetaData.GetTableDef(be.GetType());
                int i = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                    if (fld.IsParentField)
                        tdParent.GetFieldDef(fld.FieldName)
                            .SetLoadValue(be, ParentFieldValues[i++]);
            }

            List<TmpField> ListChanged = new List<TmpField>();
            UpdateAutoFields(td, Entity, ListChanged);

            #region BuildSqlInsertTable
            string strFields = string.Empty;
            string strValues = string.Empty;
            string strKeyCondition = string.Empty;
            List<FieldParam> ParamsNew = new List<FieldParam>();

            foreach (FieldDef fd in td.KeyFields.Values)
            {
                strFields = string.Concat(strFields, ",", fd._FieldName);
                strValues = string.Concat(strValues, ",@", fd._FieldName);
                if (!object.ReferenceEquals(fd, td._fldCounterField))
                {
                    ParamsNew.Add(new FieldParam(fd, Entity));
                    strKeyCondition = string.Concat(" AND ", fd._FieldName, "=@",
                        fd._FieldName);
                }
            }

            if (td._fldCounterField != null && strKeyCondition.Length > 0)
            {
                int Ctr = (int)Find.MaxValue(td._fldCounterField._FieldName,
                    td._TableName, strKeyCondition.Substring(5), 0, ParamsNew.ToArray()) + 1;
                td._fldCounterField.SetLoadValue(Entity, Ctr);
                ParamsNew.Add(new FieldParam(td._fldCounterField, Entity));
            }

            foreach (FieldDef fd in td.NonKeyFields.Values)
            {
                strFields = string.Concat(strFields, ",", fd._FieldName);
                if (fd._DataTypeAtr._DataType != DataType.TimeStamp)
                {
                    strValues = string.Concat(strValues, ",@", fd._FieldName);
                    ParamsNew.Add(new FieldParam(fd, Entity));
                }
                else
                    strValues = string.Concat(strValues, ",", GetSqlNow());
            }
            #endregion

            if (strFields.Length == 0) return 0;
            try
            {
                return ExecuteNonQuery(string.Concat(
                    "INSERT INTO ", td._TableName, "(",
                    strFields.Substring(1), ") VALUES (",
                    strValues.Substring(1), ")"), ParamsNew);
            }
            catch (Exception ex)
            {
                foreach (TmpField tmp in ListChanged)
                    tmp.Clear();
                throw ex;
            }
        }

        #region LoadEntity
        private void LoadChildEntity(TableDef td, BaseEntity Entity,
            string Criteria, FieldParam[] Parameters)
        {
            int ParamLen = Parameters.Length;
            foreach (EntityCollDef ec in td.ChildEntities)
            {
                Array.Resize<FieldParam>(ref Parameters, ParamLen);
                IList ListChild = ec.GetValue(Entity);

                ((IEntityCollection)ListChild).OnLoad = true;
                ListChild.Clear();

                TableDef tdChild = MetaData.GetTableDef(ec.ChildType);
                if (tdChild.KeyFields.Count == td.KeyFields.Count)
                    continue;

                string strTemp = tdChild.GetSqlFieldSelect(true, true, false, false);

                string SqlSelect;
                string SqlCount;
                string FieldSelectName = string.Empty; ;

                foreach(FieldDef fld in tdChild.Fields)
                    if (fld._dtlsa == null)
                    {
                        FieldSelectName = fld._FieldName;
                        break;
                    }
                if (strTemp.Contains(" AS "))
                {
                    SqlSelect = string.Concat("SELECT ", tdChild.GetStrFieldNames(
                        true, true, false, false), " FROM (SELECT ",
                        strTemp, " FROM ",
                        tdChild._TableName, ") AS X WHERE ",
                        Criteria);
                    SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                        FieldSelectName, ") FROM (SELECT ",
                        strTemp, " FROM ",
                        tdChild._TableName, ") AS X WHERE ",
                        Criteria);
                }
                else
                {
                    SqlSelect = string.Concat("SELECT ",
                        strTemp, " FROM ",
                        tdChild._TableName, " WHERE ", Criteria);
                    SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                        FieldSelectName, ") FROM ",
                        tdChild._TableName, " WHERE ", Criteria);
                }
                if (tdChild._fldCounterField != null)
                    SqlSelect = string.Concat(SqlSelect, " ORDER BY ",
                        tdChild._fldCounterField._FieldName);

                int NumRecord = (int)Find.Value(SqlCount, 0, Parameters);
                if (NumRecord == 0)
                    ((IEntityCollection)ListChild).OnLoad = false;
                else
                {
                    BaseEntity[] ListBE = new BaseEntity[NumRecord];
                    for (int i = 0; i < NumRecord; i++)
                        ListBE[i] = (BaseEntity)BaseFactory.CreateInstance(ec.ChildType);

                    IDataReader rdr = null;

                    try
                    {
                        IDbCommand Cmd = CreateCommand(SqlSelect,
                            CommandType.Text, Parameters);

                        int i = 0;
                        rdr = _ExecuteReader(Cmd);
                        while (rdr.Read())
                        {
                            int j = 0;
                            BaseEntity be = ListBE[i];
                            be.EntityOnLoad = true;

                            tdChild._fldCounterField.SetLoadValue(be, ++i);

                            foreach (FieldDef fld in tdChild.KeyFields.Values)
                                fld.SetLoadValue(be, rdr[j++]);

                            foreach (FieldDef fd in tdChild.NonKeyFields.Values)
                                fd.SetLoadValue(be, rdr[j++]);
                            be.EntityOnLoad = false;
                            ListChild.Add(be);
                        }
                    }
                    finally
                    {
                        ((IEntityCollection)ListChild).OnLoad = false;
                        if (rdr != null && !rdr.IsClosed) rdr.Close();
                    }
                }
                if (tdChild.ChildEntities.Count == 0) continue;
                if (tdChild._fldCounterField != null)
                {
                    string FieldName = string.Concat(tdChild
                        ._fldCounterField._FieldName, "=@", tdChild
                        ._fldCounterField._FieldName);
                    string TmpCriteria = Criteria.Length > 0 ?
                        Criteria + " AND " : string.Empty;
                    int i = 1;

                    int NumParam = Parameters == null ? 0 : Parameters.Length;
                    if (NumParam == 0)
                        Parameters = new FieldParam[1];
                    else
                        Array.Resize<FieldParam>(ref Parameters, NumParam + 1);

                    Parameters[NumParam] = new FieldParam(
                        tdChild._fldCounterField, 1);
                    foreach (BaseEntity ec2 in ListChild)
                    {
                        Parameters[NumParam].Value = i++;
                        LoadChildEntity(tdChild, ec2,
                            TmpCriteria + FieldName, Parameters);
                    }
                }
            }
        }

        public bool LoadEntity(BaseEntity Entity, bool CallLoadRule)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);
            string Condition = string.Empty;

            FieldParam[] Parameters = null;
            if (td.KeyFields.Count > 0)
            {
                Parameters = new FieldParam[td.KeyFields.Count];
                int ctrParam = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    Condition = string.Concat(Condition, " AND ",
                        fld._FieldName, "=@X", fld._FieldName);
                    Parameters[ctrParam++] = new FieldParam("X" +
                        fld._FieldName, fld, fld.GetValue(Entity));
                }
            }
            if (Condition.Length > 0)
                return LoadEntity(Entity, Condition.Substring(5), 
                    CallLoadRule, Parameters);
            else
                return LoadEntity(Entity, string.Empty, CallLoadRule, 
                    Parameters);
        }

        public bool LoadEntity(BaseEntity Entity, string Condition, 
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);

            string SqlSelect = td.GetSqlFieldSelect(true, 
                true, false, false);
            if (SqlSelect.Length == 0) return false;

            if (Condition.Length == 0)
                SqlSelect = string.Concat("SELECT ",
                    SqlSelect, " FROM ", td.GetSqlHeaderView(this));
            else
            {
                if (!SqlSelect.Contains(" AS "))
                    SqlSelect = string.Concat("SELECT ",
                        SqlSelect, " FROM ", td.GetSqlHeaderView(this),
                        " WHERE ", Condition);
                else
                    SqlSelect = string.Concat("SELECT ", td.GetStrFieldNames(
                        true, true, false, false), " FROM (SELECT ",
                        SqlSelect, " FROM ", td.GetSqlHeaderView(this), ") AS X WHERE ",
                        Condition);
            }
            IDbCommand Cmd = CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;

            FormMode LastFormMode = FormMode.FormError;
            bool FormModeChanged = false;
            ParentEntity px = Entity as ParentEntity;
            try
            {
                if (px != null)
                {
                    LastFormMode = px.FormMode;
                    if (px.FormMode != FormMode.FormEdit ||
                        px.FormMode != FormMode.FormView)
                    {
                        px._FormMode = FormMode.FormView;
                        FormModeChanged = true;
                    }
                }

                CallLoadRule = CallLoadRule & px != null;
                BaseFramework.DoEntityAction(Entity, enEntityActionMode.BeforeLoad);
                if (CallLoadRule)
                    ((IRuleInitUI)px).BeforeLoad();

                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);

                if (!rdr.Read())
                {
                    rdr.Close();
                    if (CallLoadRule)
                        ((IRuleInitUI)px).AfterLoadNotFound();
                    BaseFramework.DoEntityAction(Entity, enEntityActionMode.AfterLoadNotFound);

                    return false;
                }
                Entity.EntityOnLoad = true;

                foreach (FieldDef fd in td.KeyFields.Values)
                    fd.SetLoadValue(Entity, rdr[fd._FieldName]);

                foreach (FieldDef fd in td.NonKeyFields.Values)
                    fd.SetLoadValue(Entity, rdr[fd._FieldName]);

                rdr.Close();

                if (td.ChildEntities.Count > 0)
                {
                    Condition = string.Empty;
                    FieldParam[] ListParam = new FieldParam[td.KeyFields.Count];
                    int i = 0;
                    foreach (FieldDef fld in td.KeyFields.Values)
                    {
                        Condition = string.Concat(Condition, " AND ",
                           fld._FieldName, "=@", fld._FieldName);
                        ListParam[i++] = new FieldParam(fld, Entity);
                    }
                    LoadChildEntity(td, px, Condition.Substring(5),
                        ListParam);
                }

                if (CallLoadRule)
                    ((IRuleInitUI)px).AfterLoadFound();
                BaseFramework.DoEntityAction(Entity, enEntityActionMode.AfterLoadFound);

                Entity.ClearError();
                if (px != null)
                {
                    px.IsLoadedEntity = true;
                    if (px._Original != null)
                        MetaData.CloneToOriginal(px);
                }

                if (FormModeChanged && px != null)
                    px.FormMode = px._FormMode;
                return true;
            }
            catch (Exception ex)
            {
                if (px != null) px.FormMode = LastFormMode;
                throw ex;
            }
            finally
            {
                if (Entity.EntityOnLoad)
                {
                    Entity.EntityOnLoad = false;
                    Entity.DataChanged();
                }
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }
        #endregion

        #region FastLoadEntity
        public bool FastLoadEntity(BusinessEntity Entity,
            string FieldNames)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);
            string Condition = string.Empty;

            FieldParam[] Params = new FieldParam[td.KeyFields.Count];
            int i = 0;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                Condition = string.Concat(Condition, " AND ",
                    fld._FieldName, "=@", fld._FieldName);
                Params[i++] = new FieldParam(fld, Entity);
            }
            if (Condition.Length > 0) Condition = Condition.Substring(5);
            return FastLoadEntity(Entity, FieldNames, Condition, Params);
        }

        public bool FastLoadEntity(BaseEntity Entity, 
            string FieldNames, string Condition,
            params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(Entity.GetType());
            ValidateTableDef(td);

            string[] Cols;
            if (FieldNames == "*" || FieldNames.Length == 0)
                Cols = td.GetListFieldNames(true).ToArray();
            else
                Cols = FieldNames.Split(',');

            FieldDef[] flds = new FieldDef[Cols.Length];

            StringBuilder SqlSelectBld = new StringBuilder();
            StringBuilder SqlFields = new StringBuilder();
            for (int i = 0; i < Cols.Length; i++)
            {
                flds[i] = td.GetFieldDef(Cols[i].Trim());
                SqlFields.Append(",").Append(flds[i]._FieldName);
                if (flds[i]._dtlsa != null)
                    SqlSelectBld.Append(",(")
                        .Append(flds[i]._dtlsa.GetSqlQuery())
                        .Append(") AS ").Append(flds[i]._FieldName);
            }

            string SqlSelect = SqlSelectBld.Length > 0 ?
                string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM (SELECT *",
                    SqlSelectBld.ToString(), " FROM ",
                    td.GetSqlHeaderView(this), ") AS X") :
                string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM ",
                    td.GetSqlHeaderView(this));

            if (Condition.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Condition);
            IDbCommand Cmd = CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;

            try
            {
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);

                if (!rdr.Read()) return false;
                Entity.EntityOnLoad = true;

                for (int i = 0; i < flds.Length; i++)
                    flds[i].SetLoadValue(Entity, rdr[i]);

                rdr.Close();

                return true;
            }
            finally
            {
                if (Entity.EntityOnLoad)
                {
                    Entity.EntityOnLoad = false;
                    Entity.DataChanged();
                }
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }
        #endregion

        #region ListLoadEntities
        public IList<TEntity> ListLoadEntities<TEntity>(
            IList<TEntity> RetList,
            string Condition, string OrderCondition, bool CallLoadRule,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            return ListLoadEntities<TEntity>(RetList, 
                Condition, OrderCondition, CallLoadRule, false, Parameters);
        }
        
        public IList<BaseEntity> ListLoadEntities(Type ObjType, 
            IList<BaseEntity> RetList,
            string Condition, string OrderCondition, 
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            return ListLoadEntities(ObjType, RetList, Condition,
                OrderCondition, CallLoadRule, false, Parameters);
        }

        public IList<TEntity> ListLoadEntities<TEntity>(
            IList<TEntity> RetList, string Condition,
            string OrderCondition, bool CallLoadRule,
            bool AddEmptyRow, params FieldParam[] Parameters)
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            ValidateTableDef(td);

            string SqlTemp = td.GetSqlFieldSelect(true,
                true, false, false);
            string SqlCount;
            string FieldSelectName = "*";
            string SqlSelect;

            foreach (FieldDef fld in td.Fields)
                if (fld._dtlsa == null)
                {
                    FieldSelectName = fld._FieldName;
                    break;
                }

            if (SqlTemp.Contains(" AS "))
            {
                SqlSelect = string.Concat("SELECT * FROM (SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this), ") AS X");
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM (SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this), ") AS X");
            }
            else
            {
                SqlSelect = string.Concat("SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this));
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM ", td.GetSqlHeaderView(this));
            }

            if (Condition.Length > 0)
            {
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Condition);
                SqlCount = string.Concat(SqlCount, " WHERE ", Condition);
            }

            if (OrderCondition.Length > 0) SqlSelect = string.Concat(
                  SqlSelect, " ORDER BY ", OrderCondition);

            bool UseOldList;
            BindingList<TEntity> TempList;

            bool IsParent = td._ClassType.IsSubclassOf(typeof(ParentEntity));
            CallLoadRule = CallLoadRule && IsParent;

            if (RetList == null)
            {
                RetList = new BindingListEx<TEntity>();
                UseOldList = false;
                TempList = (BindingList<TEntity>)RetList;
            }
            else
            {
                RetList.Clear();
                UseOldList = true;
                TempList = new BindingList<TEntity>();
            }

            if (AddEmptyRow)
            {
                TEntity Entity = BaseFactory.CreateInstance<TEntity>();
                Entity.SetDefaultValue();
                TempList.Add(Entity);
            }

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            int NumRecord = (int)Find.Value(SqlCount, 0, Parameters);
            if (NumRecord > 0)
            {
                TEntity[] ListBE = new TEntity[NumRecord];
                for (int i = 0; i < NumRecord; i++)
                    ListBE[i] = BaseFactory.CreateInstance<TEntity>();

                IDbCommand Cmd = CreateCommand(SqlSelect,
                    CommandType.Text, Parameters);

                IDataReader rdr = null;
                int CtrBE = 0;
                try
                {
                    if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                    rdr = _ExecuteReader(Cmd);

                    while (rdr.Read())
                    {
                        TEntity Ent = ListBE[CtrBE++];

                        //Not Call for Performance Reason...
                        //BaseFramework.DoEntityAction(Ent, enActionMode.BeforeLoad);
                        if (CallLoadRule) ((IRuleInitUI)Ent).BeforeLoad();

                        int i = 0;
                        foreach (FieldDef fld in td.KeyFields.Values)
                            fld.SetLoadValue(Ent, rdr[i++]);

                        foreach (FieldDef fld in td.NonKeyFields.Values)
                            fld.SetLoadValue(Ent, rdr[i++]);

                        TempList.Add(Ent);
                    }
                }
                finally
                {
                    if (rdr != null) rdr.Close();
                }

                if (td.ChildEntities.Count > 0)
                {
                    FieldParam[] ListParam =
                        new FieldParam[td.KeyFields.Count];

                    string tmpKeyCondition = string.Empty;
                    int NumKey = 0;
                    foreach (FieldDef fld in td.KeyFields.Values)
                    {
                        tmpKeyCondition = string.Concat(tmpKeyCondition,
                            " AND ", fld._FieldName,
                            "=@", fld._FieldName);
                        ListParam[NumKey++] = new FieldParam(fld);
                    }
                    if (tmpKeyCondition.Length > 0)
                        tmpKeyCondition = tmpKeyCondition.Remove(0, 5);
                    foreach (ParentEntity Entity in (IList)TempList)
                    {
                        NumKey = 0;
                        foreach (FieldDef fld in td.KeyFields.Values)
                            ListParam[NumKey++].Value = fld.GetValue(Entity);
                        LoadChildEntity(td, Entity,
                            tmpKeyCondition, ListParam);
                    }
                }
            }
            if (IsParent)
            {
                if (CallLoadRule)
                    foreach (IRuleInitUI Entity in (IList)TempList)
                    {
                        Entity.AfterLoadFound();
                        BaseFramework.DoEntityAction((BaseEntity)Entity, enEntityActionMode.AfterLoadFound);
                        if (UseOldList)
                            RetList.Add((TEntity)Entity);
                        ((ParentEntity)Entity).IsLoadedEntity = true;
                    }
                else
                    foreach (BaseEntity Entity in (IList)TempList)
                    {
                        BaseFramework.DoEntityAction(Entity, enEntityActionMode.AfterLoadFound);
                        if (UseOldList)
                            RetList.Add((TEntity)Entity);
                        ((ParentEntity)Entity).IsLoadedEntity = true;
                    }
            }
            else if (UseOldList)
                foreach (BaseEntity Entity in (IList)TempList)
                    RetList.Add((TEntity)Entity);

            if (MustClose) Connection.Close();
            return RetList;
        }

        public IList<BaseEntity> ListLoadEntities(
            Type ObjType, IList<BaseEntity> RetList,
            string Condition, string OrderCondition,
            bool CallLoadRule, bool AddEmptyRow,
            params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(ObjType);
            ValidateTableDef(td);

            string SqlTemp = td.GetSqlFieldSelect(true,
                true, false, false);
            string SqlSelect;
            string SqlCount;
            string FieldSelectName = "*";

            foreach (FieldDef fld in td.Fields)
                if (fld._dtlsa == null)
                {
                    FieldSelectName = fld._FieldName;
                    break;
                }

            if (SqlTemp.Contains(" AS "))
            {
                SqlSelect = string.Concat("SELECT * FROM (SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this), ") AS X");
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM (SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this), ") AS X");
            }
            else
            {
                SqlSelect = string.Concat("SELECT ",
                    SqlTemp, " FROM ", td.GetSqlHeaderView(this));
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM ", td.GetSqlHeaderView(this));
            }

            if (Condition.Length > 0)
            {
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Condition);
                SqlCount = string.Concat(SqlCount, " WHERE ", Condition);
            }

            if (OrderCondition.Length > 0) SqlSelect = string.Concat(
                  SqlSelect, " ORDER BY ", OrderCondition);

            bool UseOldList;
            BindingList<BaseEntity> TempList;

            if (RetList == null)
            {
                RetList = new BindingListEx<BaseEntity>();
                UseOldList = false;
                TempList = (BindingList<BaseEntity>)RetList;
            }
            else
            {
                RetList.Clear();
                UseOldList = true;
                TempList = new BindingList<BaseEntity>();
            }

            if (AddEmptyRow)
            {
                BaseEntity Entity = (BaseEntity)BaseFactory.CreateInstance(ObjType);
                Entity.SetDefaultValue();
                RetList.Add(Entity);
            }

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            int NumRecord = (int)Find.Value(SqlCount, 0, Parameters);
            BaseEntity[] ListBE = new BaseEntity[NumRecord];
            for (int i = 0; i < NumRecord; i++)
                ListBE[i] = (BaseEntity)BaseFactory.CreateInstance(ObjType);
            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            IDataReader rdr = null;
            int CtrBE = 0;
            try
            {
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);

                while (rdr.Read())
                {
                    BaseEntity Entity = ListBE[CtrBE++];

                    int i = 0;
                    foreach (FieldDef fld in td.KeyFields.Values)
                        fld.SetLoadValue(Entity, rdr[i++]);

                    foreach (FieldDef fld in td.NonKeyFields.Values)
                        fld.SetLoadValue(Entity, rdr[i++]);

                    RetList.Add(Entity);
                }

                rdr.Close();

                if (td.ChildEntities.Count > 0)
                {
                    FieldParam[] ListParam =
                        new FieldParam[td.KeyFields.Count];

                    string tmpKeyCondition = string.Empty;
                    int NumKey = 0;
                    foreach (FieldDef fld in td.KeyFields.Values)
                    {
                        tmpKeyCondition = string.Concat(tmpKeyCondition,
                            " AND ", fld._FieldName,
                            "=@", fld._FieldName);
                        ListParam[NumKey++] = new FieldParam(fld);
                    }
                    if (tmpKeyCondition.Length > 0)
                        tmpKeyCondition = tmpKeyCondition.Remove(0, 5);
                    foreach (ParentEntity Entity in (IList)TempList)
                    {
                        NumKey = 0;
                        foreach (FieldDef fld in td.KeyFields.Values)
                            ListParam[NumKey++].Value = fld.GetValue(Entity);
                        LoadChildEntity(td, Entity,
                            tmpKeyCondition, ListParam);
                    }
                }

                bool IsParent = false;
                foreach (BaseEntity Entity in RetList)
                {
                    IsParent = (Entity as ParentEntity) != null;
                    break;
                }

                if (IsParent)
                {
                    if (CallLoadRule)
                        foreach (IRuleInitUI Entity in (IList)TempList)
                        {
                            Entity.AfterLoadFound();
                            BaseFramework.DoEntityAction((BaseEntity)Entity, enEntityActionMode.AfterLoadFound);
                            if (UseOldList)
                                RetList.Add((BaseEntity)Entity);
                            ((ParentEntity)Entity).IsLoadedEntity = true;
                        }
                    else
                        foreach (BaseEntity Entity in (IList)TempList)
                        {
                            BaseFramework.DoEntityAction(Entity, enEntityActionMode.AfterLoadFound);
                            if (UseOldList)
                                RetList.Add((BaseEntity)Entity);
                            ((ParentEntity)Entity).IsLoadedEntity = true;
                        }
                }
                else if (UseOldList)
                    foreach (BaseEntity Entity in (IList)TempList)
                        RetList.Add((BaseEntity)Entity);

                return RetList;
            }
            finally
            {
                if (rdr != null && !rdr.IsClosed) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }
        #endregion

        #region LoadEntities
        public AutoUpdateBindingList<TEntity> LoadEntities<TEntity>(
            string Condition, string OrderCondition, bool CallLoadRule,
            bool UseCache) where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, Condition, OrderCondition, CallLoadRule,
                UseCache, false);
        }

        public AutoUpdateBindingList<TEntity> LoadEntities<TEntity>(
            string Condition, string OrderCondition, bool CallLoadRule,
            bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, Condition, OrderCondition, CallLoadRule, 
                UseCache, AddEmptyRow);
        }
        #endregion

        #region ListFastLoadEntities
        public IList<BaseEntity> ListFastLoadEntities(Type ObjType,
            IList<BaseEntity> RetList, string ColumnList, 
            string Conditions, string OrderCondition,
            params FieldParam[] Parameters)
        {
            return ListFastLoadEntities(ObjType, RetList,
                ColumnList, Conditions, OrderCondition, false,
                Parameters);
        }

        public IList<TEntity> ListFastLoadEntities<TEntity>(
            IList<TEntity> RetList, string ColumnList, 
            string Conditions, string OrderCondition,
            params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return ListFastLoadEntities<TEntity>(RetList, ColumnList, 
                Conditions, OrderCondition, false, Parameters);
        }

        public IList<TEntity> 
            ListFastLoadEntitiesUsingSqlSelect<TEntity>(
            IList<TEntity> RetList, string SqlSelect, 
            string OrderCondition, params FieldParam[] Parameters)
            where TEntity : new()
        {
            return ListFastLoadEntitiesUsingSqlSelect<TEntity>(RetList, SqlSelect,
                OrderCondition, false, Parameters);
        }

        public IList<BaseEntity> ListFastLoadEntities(Type ObjType,
            IList<BaseEntity> RetList, string FieldNames,
            string Conditions, string OrderCondition,
            bool AddEmptyRow, params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(ObjType);
            ValidateTableDef(td);

            string[] Cols;
            if (FieldNames.Length == 0 || FieldNames == "*")
                Cols = td.GetListFieldNames(true).ToArray();
            else
                Cols = FieldNames.Split(',');

            FieldDef[] flds = new FieldDef[Cols.Length];

            StringBuilder SqlSelectBld = new StringBuilder();
            StringBuilder SqlFields = new StringBuilder();
            for (int i = 0; i < Cols.Length; i++)
            {
                flds[i] = td.GetFieldDef(Cols[i].Trim());
                SqlFields.Append(",").Append(flds[i]._FieldName);
                if (flds[i]._dtlsa != null)
                    SqlSelectBld.Append(",(")
                        .Append(flds[i]._dtlsa.GetSqlQuery())
                        .Append(") AS ").Append(flds[i]._FieldName);
            }

            string SqlSelect;
            string SqlCount;
            string FieldSelectName = "*";

            foreach(FieldDef fld in td.Fields)
                if (fld._dtlsa == null) 
                {
                    FieldSelectName = fld._FieldName;
                    break;
                }

            if (SqlSelectBld.Length > 0)
            {
                SqlSelect = string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM (SELECT *",
                    SqlSelectBld.ToString(), " FROM ",
                    td.GetSqlHeaderView(this), ") AS X");
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM (SELECT *",
                    SqlSelectBld.ToString(), " FROM ",
                    td.GetSqlHeaderView(this), ") AS X");
            }
            else
            {
                SqlSelect = string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM ", td.GetSqlHeaderView(this));
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM ", td.GetSqlHeaderView(this));
            }

            if (Conditions.Length > 0)
            {
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
                SqlCount = string.Concat(SqlCount, " WHERE ", Conditions);
            }
            if (OrderCondition.Length > 0) SqlSelect = string.Concat(
                  SqlSelect, " ORDER BY ", OrderCondition);

            int NumRecord = (int)Find.Value(SqlCount, 0, Parameters);
            BaseEntity[] ListBE = new BaseEntity[NumRecord];
            for (int i = 0; i < NumRecord; i++)
                ListBE[i] = (BaseEntity)BaseFactory.CreateInstance(ObjType);

            bool UseOldList;
            BindingList<BaseEntity> TempList;

            if (RetList == null)
            {
                RetList = new BindingListEx<BaseEntity>();
                UseOldList = false;
                TempList = (BindingList<BaseEntity>)RetList;
            }
            else
            {
                RetList.Clear();
                UseOldList = true;
                TempList = new BindingList<BaseEntity>();
            }

            if (AddEmptyRow)
            {
                BaseEntity Entity = (BaseEntity)BaseFactory
                    .CreateInstance(ObjType);
                Entity.SetDefaultValue();
                RetList.Add(Entity);
            }

            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);

                BaseEntity Entity;

                int ListCtr = 0;
                while (rdr.Read())
                {
                    Entity = ListBE[ListCtr++];

                    for (int i = 0; i < Cols.Length; i++)
                        flds[i].SetLoadValue(Entity, rdr[i]);

                    TempList.Add(Entity);
                }

                rdr.Close();

                if (UseOldList)
                    foreach (BaseEntity be in TempList)
                        RetList.Add(be);

                return RetList;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }

        public IList<TEntity> ListFastLoadEntities<TEntity>(
            IList<TEntity> RetList, string FieldNames,
            string Conditions, string OrderCondition,
            bool AddEmptyRow, params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            ValidateTableDef(td);

            string[] Cols;
            if (FieldNames.Length == 0 || FieldNames == "*")
                Cols = td.GetListFieldNames(true).ToArray();
            else
                Cols = FieldNames.Split(',');

            FieldDef[] flds = new FieldDef[Cols.Length];

            StringBuilder SqlSelectBld = new StringBuilder();
            StringBuilder SqlFields = new StringBuilder();
            for (int i = 0; i < Cols.Length; i++)
            {
                flds[i] = td.GetFieldDef(Cols[i].Trim());
                SqlFields.Append(",").Append(flds[i]._FieldName);
                if (flds[i]._dtlsa != null)
                    SqlSelectBld.Append(",(")
                        .Append(flds[i]._dtlsa.GetSqlQuery())
                        .Append(") AS ").Append(flds[i]._FieldName);
            }

            string SqlSelect;
            string SqlCount;
            string FieldSelectName = "*";

            foreach(FieldDef fld in td.Fields)
                if (fld._dtlsa == null)
                {
                    FieldSelectName = fld._FieldName;
                    break;
                }

            if (SqlSelectBld.Length > 0)
            {
                SqlSelect = string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM (SELECT *",
                    SqlSelectBld.ToString(), " FROM ",
                    td.GetSqlHeaderView(this), ") AS X");
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM (SELECT *",
                    SqlSelectBld.ToString(), " FROM ",
                    td.GetSqlHeaderView(this), ") AS X");
            }
            else
            {
                SqlSelect = string.Concat("SELECT ", SqlFields.ToString()
                    .Substring(1), " FROM ", td.GetSqlHeaderView(this));
                SqlCount = string.Concat("SELECT ", GetSqlCount(), "(",
                    FieldSelectName, ") FROM ", td.GetSqlHeaderView(this));
            }

            if (Conditions.Length > 0)
            {
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
                SqlCount = string.Concat(SqlCount, " WHERE ", Conditions);
            }
            if (OrderCondition.Length > 0) SqlSelect = string.Concat(
                  SqlSelect, " ORDER BY ", OrderCondition);

            int NumRecord = (int)Find.Value(SqlCount, 0, Parameters);
            TEntity[] ListBE = new TEntity[NumRecord];
            for (int i = 0; i < NumRecord; i++)
                ListBE[i] = BaseFactory.CreateInstance<TEntity>();

            bool UseOldList;
            BindingList<TEntity> TempList;

            if (RetList == null)
            {
                RetList = new BindingListEx<TEntity>();
                UseOldList = false;
                TempList = (BindingList<TEntity>)RetList;
            }
            else
            {
                RetList.Clear();
                UseOldList = true;
                TempList = new BindingList<TEntity>();
            }

            if (AddEmptyRow)
            {
                TEntity Entity = BaseFactory.CreateInstance<TEntity>();
                Entity.SetDefaultValue();
                RetList.Add(Entity);
            }

            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);
                TEntity Entity;
                int ListBECtr = 0;
                while (rdr.Read())
                {
                    Entity = ListBE[ListBECtr++];

                    for (int i = 0; i < Cols.Length; i++)
                        flds[i].SetLoadValue(Entity, rdr[i]);

                    TempList.Add(Entity);
                }

                rdr.Close();

                if (UseOldList)
                    foreach (TEntity be in TempList)
                        RetList.Add(be);

                return RetList;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }

        public IList<TEntity>
            ListFastLoadEntitiesUsingSqlSelect<TEntity>(
            IList<TEntity> RetList, string SqlSelect,
            string OrderCondition, bool AddEmptyRow,
            params FieldParam[] Parameters)
            where TEntity : new()
        {
            if (OrderCondition.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " ORDER BY ", OrderCondition);

            if (RetList != null)
                RetList.Clear();
            else
                RetList = new List<TEntity>();
            if (AddEmptyRow)
            {
                TEntity Tmp = BaseFactory.CreateInstance<TEntity>();
                BaseEntity be = Tmp as BaseEntity;
                if (be != null)
                    be.SetDefaultValue();
                RetList.Add(Tmp);
            }
            using (EntityReader<TEntity> rdr = new EntityReader<TEntity>(this,
                SqlSelect, true, Parameters))
            {
                while(rdr.Read())
                    RetList.Add(rdr.Entity);
            }
            return RetList;

            //TableDef td = MetaData.GetTableDef(typeof(TEntity));
            //ValidateTableDef(td);

            //bool UseOldList;
            //BindingList<TEntity> TempList;

            //if (RetList == null)
            //{
            //    RetList = new BindingListEx<TEntity>();
            //    UseOldList = false;
            //    TempList = (BindingList<TEntity>)RetList;
            //}
            //else
            //{
            //    RetList.Clear();
            //    UseOldList = true;
            //    TempList = new BindingList<TEntity>();
            //}

            //if (AddEmptyRow)
            //{
            //    TEntity Entity = BaseFactory.CreateInstance<TEntity>();
            //    if (td != null)
            //        ((BaseEntity)Entity).SetDefaultValue();
            //    RetList.Add(Entity);
            //}

            //int NumRecord = (int)Find.Value(string.Concat("SELECT ", 
            //    GetSqlCount(), "(*) FROM (",SqlSelect, ") AS X"), 0, Parameters);
            //TEntity[] ListBE = new TEntity[NumRecord];
            //for (int i = 0; i < NumRecord; i++)
            //    ListBE[i] = BaseFactory.CreateInstance<TEntity>();

            //if (OrderCondition.Length > 0)
            //    SqlSelect = string.Concat(SqlSelect, " ORDER BY ", OrderCondition);

            //IDbCommand Cmd = CreateCommand(SqlSelect,
            //    CommandType.Text, Parameters);

            //bool MustClose;
            //if (Connection.State != ConnectionState.Open)
            //{
            //    Connection.Open();
            //    MustClose = true;
            //}
            //else
            //    MustClose = false;

            //IDataReader rdr = null;
            //try
            //{
            //    if (_EnableWriteLog) WriteLog(Cmd.CommandText);
            //    rdr = _ExecuteReader(Cmd);

            //    FieldDef[] flds = new FieldDef[rdr.FieldCount];
            //    for (int i = 0; i < rdr.FieldCount; i++)
            //        flds[i] = td.GetFieldDef(rdr.GetName(i));

            //    TEntity Entity;
            //    int ListBECtr = 0;
            //    while (rdr.Read())
            //    {
            //        Entity = ListBE[ListBECtr++];

            //        for (int i = 0; i < rdr.FieldCount; i++)
            //            if (flds[i] != null)
            //                flds[i].SetLoadValue(Entity, rdr[i]);

            //        TempList.Add(Entity);
            //    }

            //    rdr.Close();

            //    if (UseOldList)
            //        foreach (TEntity be in TempList)
            //            RetList.Add(be);

            //    return RetList;
            //}
            //finally
            //{
            //    if (rdr != null) rdr.Close();
            //    if (MustClose) Connection.Close();
            //}
        }
        #endregion

        #region FastLoadEntities
        public AutoUpdateBindingList<TEntity> FastLoadEntities<TEntity>(
            string ColumnList, string Condition, string OrderCondition,
            bool UseCache) where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, ColumnList, Condition, OrderCondition,
                UseCache, false);
        }

        public AutoUpdateBindingList<TEntity>
            FastLoadEntitiesUsingSqlSelect<TEntity>(
            string SqlSelect, string OrderCondition, bool UseCache)
            where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, SqlSelect, OrderCondition, UseCache, false);
        }

        public AutoUpdateBindingList<TEntity> FastLoadEntities<TEntity>(
            string FieldNames, string Condition,
            string OrderCondition, bool UseCache, 
            bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, FieldNames, Condition, OrderCondition, 
                UseCache, AddEmptyRow);
        }

        public AutoUpdateBindingList<TEntity> 
            FastLoadEntitiesUsingSqlSelect<TEntity>(
            string SqlSelect, string OrderCondition, bool UseCache, 
            bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return AutoUpdateList.GetAutoUpdateBindingList<TEntity>(
                this, SqlSelect, OrderCondition, UseCache, AddEmptyRow);
        }
        #endregion

        public void ForceRefresh<TEntity>()
            where TEntity : BaseEntity
        {
            AutoUpdateList.EntityRefreshed(this, typeof(TEntity));
        }

        #region SaveDeleteEntity
        private void CheckParentDelete(TableDef tdParent,
            ParentEntity DeleteEntity, BatchCommand BatchCmd)
        {
            foreach (RelationAttribute pra in tdParent.ParentRelations)
                if (pra._ParentDeleteRule == ParentDelete.DeleteRestrict)
                {
                    string SqlTest = string.Empty;
                    for (int i = 0; i < pra._ParentFields.Length; i++)
                    {
                        FieldDef fldParent = tdParent.GetFieldDef(pra._ParentFields[i]);
                        if (fldParent == null)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.RelParentFieldNotFound,
                                pra._ParentType.Name, pra.ChildType.Name,
                                pra._ParentFields[i]));

                        SqlTest = string.Concat(SqlTest,
                            " AND ", pra._ChildFields[i], "=",
                            FormatSqlValue(fldParent.GetValue(DeleteEntity),
                                fldParent._DataTypeAtr._DataType));
                    }
                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    SqlTest = string.Concat("SELECT ", pra._ChildFields[0],
                        " FROM ", tdChild.GetSqlHeaderView(this), " WHERE ",
                        SqlTest.Substring(5));
                    if (Find.IsExists(SqlTest))
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.ParentDeleteRestrict,
                            tdParent._TableName, tdChild._TableName));
                }
                else
                {
                    string SqlDelete = string.Empty;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    List<OldNewValue> DeleteValues = new List<OldNewValue>();
                    for (int i = 0; i < pra._ParentFields.Length; i++)
                    {
                        FieldDef fldParent = tdParent.GetFieldDef(pra._ParentFields[i]);
                        if (fldParent == null)
                            throw new ApplicationException(string.Format(
                                ErrorMetaData.RelParentFieldNotFound,
                                pra._ParentType.Name, pra.ChildType.Name,
                                pra._ParentFields[i]));

                        object DeleteValue = fldParent.GetValue(DeleteEntity);

                        SqlDelete = string.Concat(SqlDelete,
                            " AND ", pra._ChildFields[i], "=",
                            FormatSqlValue(DeleteValue,
                                fldParent._DataTypeAtr._DataType));
                        FieldDef fldChild = tdChild.GetFieldDef(pra._ChildFields[i]);
                        if (fldChild.IsParentField)
                            DeleteValues.Add(new OldNewValue(pra._ChildFields[i],
                                fldParent.DataType, DeleteValue, null));
                    }
                    BatchCmd.AddCommand(string.Concat("DELETE FROM ",
                        tdChild._TableName, " WHERE ",
                        SqlDelete.Substring(5)));
                    if (DeleteValues.Count > 0)
                        CheckParentDelete2(tdChild, DeleteValues, BatchCmd);
                }
        }

        private void CheckParentDelete2(TableDef tdParent,
            List<OldNewValue> DeleteValues, BatchCommand BatchCmd)
        {
            foreach (RelationAttribute pra in tdParent.ParentRelations)
                if (pra._ParentDeleteRule == ParentDelete.DeleteRestrict)
                {
                    string SqlTest = string.Empty;
                    foreach (OldNewValue onv in DeleteValues)
                        for (int i = 0; i < pra._ParentFields.Length; i++)
                            if (pra._ParentFields[i] == onv.FieldName)
                            {
                                FieldDef fldParent = tdParent.GetFieldDef(onv.FieldName);
                                SqlTest = string.Concat(SqlTest,
                                    " AND ", pra._ChildFields[i], "=",
                                    FormatSqlValue(onv.OldValue, onv.FieldType));
                                break;
                            }
                    if (SqlTest.Length == 0) continue;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    SqlTest = string.Concat("SELECT ", pra._ChildFields[0],
                        " FROM ", tdChild._TableName, " WHERE ",
                        SqlTest.Substring(5));
                    if (Find.IsExists(SqlTest))
                        throw new ApplicationException(string.Format(
                            ErrorPersistance.ParentDeleteRestrict,
                            tdParent._TableName, tdChild._TableName));
                }
                else
                {
                    string SqlDelete = string.Empty;

                    TableDef tdChild = MetaData.GetTableDef(pra.ChildType);
                    ValidateTableDef(tdChild);

                    List<OldNewValue> ChildDeleteValues = new List<OldNewValue>();
                    foreach (OldNewValue onv in DeleteValues)
                        for (int i = 0; i < pra._ParentFields.Length; i++)
                        {
                            if (pra._ParentFields[i] != onv.FieldName) continue;
                            FieldDef fldParent = tdParent.GetFieldDef(pra._ParentFields[i]);
                            if (fldParent == null)
                                throw new ApplicationException(string.Format(
                                    ErrorMetaData.RelParentFieldNotFound,
                                    pra._ParentType.Name, pra.ChildType.Name,
                                    pra._ParentFields[i]));

                            string ChildfieldName = pra._ChildFields[i];

                            SqlDelete = string.Concat(SqlDelete,
                                " AND ", ChildfieldName, "=",
                                FormatSqlValue(onv.OldValue, onv.FieldType));

                            FieldDef fldChild = tdChild.GetFieldDef(ChildfieldName);
                            if (fldChild.IsParentField)
                            {
                                onv.FieldName = ChildfieldName;
                                ChildDeleteValues.Add(onv);
                            }
                            break;
                        }
                    BatchCmd.AddCommand(string.Concat("DELETE FROM ",
                        tdChild._TableName, " WHERE ",
                        SqlDelete.Substring(5)));
                    if (ChildDeleteValues.Count > 0)
                        CheckParentDelete2(tdChild, ChildDeleteValues, BatchCmd);
                }
        }

        public int SaveDeleteEntity(ParentEntity DelEntity, bool CallDeleteRule)
        {
            return DelEntity.SaveDelete(this, CallDeleteRule);
        }

        // dipanggil oleh business entity
        internal int _SaveDeleteEntity(ParentEntity DelEntity, bool CallDeleteRule)
        {
            ParentEntity RealDelEntity =
                DelEntity.GetOriginal() ?? DelEntity;

            TableDef td = MetaData.GetTableDef(DelEntity.GetType());
            ValidateTableDef(td);

            DelEntity.ClearError();

            if (!DelEntity.IsTransDateValid())
                DelEntity.AddError(td.fldTransactionDate._FieldName,
                    string.Concat(BaseUtility.SplitName(td.fldTransactionDate._FieldName),
                    " tidak boleh kurang dari Tgl Penguncian Transaksi"));

            string DeleteCondition = string.Empty;
            int NumFldTimeStamp = td.fldTimeStamp != null && 
                DelEntity.GetOriginal() != null ? 1 : 0;

            FieldParam[] Params = new FieldParam[
                td.KeyFields.Count + NumFldTimeStamp];
            int CtrParam = 0;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                DeleteCondition = string.Concat(DeleteCondition,
                    " AND ", fld._FieldName, "=@", fld._FieldName);
                Params[CtrParam++] = new FieldParam(fld, RealDelEntity);
            }

            string tmpTS = string.Empty;
            // Kalo menghapus entity tanpa original entity
            // timestamp tidak usah diikutkan...
            if (NumFldTimeStamp > 0 && DelEntity.GetOriginal() != null)
            {
                tmpTS = string.Concat(" AND ", 
                    td.fldTimeStamp._FieldName, "=@", 
                    td.fldTimeStamp._FieldName);
                Params[CtrParam++] = new FieldParam(
                    td.fldTimeStamp, RealDelEntity);
            }

            string SqlDelete;
            if (DeleteCondition.Length > 0)
            {
                DeleteCondition = DeleteCondition.Substring(5);

                SqlDelete = string.Concat("DELETE FROM ",
                    td._TableName, " WHERE ", 
                    DeleteCondition + tmpTS);
            }
            else
                SqlDelete = string.Concat("DELETE FROM ",
                    td._TableName);

            BatchCommand BatchCmd = new BatchCommand();
             if (td.ChildEntities.Count > 0)
            {
                FieldParam[] delParams;
                if (NumFldTimeStamp > 0)
                {
                    CtrParam = Params.Length - NumFldTimeStamp;
                    delParams = new FieldParam[CtrParam];
                    Array.Copy(Params, delParams, CtrParam);
                }
                else
                    delParams = Params;
                _RecursiveDeleteTable(BatchCmd, td,
                    DeleteCondition, delParams);
            }
            using (EntityTransaction tr = new EntityTransaction(this))
            {
                BaseFramework.DoEntityAction(DelEntity, enEntityActionMode.BeforeSaveDelete);
                IRuleInitUI pe = (IRuleInitUI)DelEntity;
                if (CallDeleteRule) pe.BeforeSaveDelete();
                if (DelEntity.IsErrorExist())
                    throw new ApplicationException(
                        DelEntity.GetErrorString());

                _CheckTimeStampDelete(td, RealDelEntity);

                int RetVal = 0;
                try
                {
                    RetVal = ExecuteInternalNonQuery(SqlDelete, Params);
                }
                catch (Exception ex)
                {
                    ProcessDbError(DelEntity, ex);
                }
                if (RetVal == 0)
                    throw new ApplicationException(string.Format(
                        ErrorPersistance.DataNotFound, td._TableName));

                CheckParentDelete(td, RealDelEntity, BatchCmd);

                try
                {
                    RetVal += ExecuteInternalBatchNonQuery(BatchCmd, false);
                }
                catch (Exception ex)
                {
                    ProcessDbError(DelEntity, ex);
                }
                if (CallDeleteRule) pe.AfterSaveDelete();
                BaseFramework.DoEntityAction(DelEntity, enEntityActionMode.AfterSaveDelete);

                if (DelEntity.IsErrorExist())
                {
                    pe.ErrorAfterSaveDelete();
                    throw new ApplicationException(DelEntity
                        .GetErrorString());
                }
                tr.CommitTransaction();

                AutoUpdateList.EntityDeleted(this, td.ClassType,
                    RealDelEntity);

                return RetVal;
            }
        }

        private void _CheckTimeStampDelete(TableDef td, BaseEntity TmpEntity)
        {
            ParentEntity pe = TmpEntity as ParentEntity;

            if (pe != null && td.fldTimeStamp != null && (DateTime)td.fldTimeStamp
                .GetValue(TmpEntity) != DateTime.MinValue)
            {
                string strQuery = string.Empty;
                FieldParam[] Params = new FieldParam[td.KeyFields.Count + 1];
                int Ctr = 1;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    strQuery = string.Concat(strQuery, " AND ", fld._FieldName, "=@",
                        Ctr.ToString());
                    Params[Ctr] = new FieldParam(Ctr.ToString(), fld.GetValue(TmpEntity));
                    Ctr++;
                }
                Params[0] = new FieldParam("0", td.fldTimeStamp.GetValue(TmpEntity));
                if (!Find.IsExists(string.Concat("SELECT ", td.fldTimeStamp.FieldName,
                    " FROM ", td._TableName, " WHERE ", td.fldTimeStamp.FieldName, "=@0",
                    strQuery), Params))
                    throw new ApplicationException(string.Format(
                        ErrorPersistance.DataNotFound, td._TableName));
            }
            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                TableDef tdChild = ecd.GetTableDef();
                IList ListEntity = ecd.GetValue(TmpEntity);

                FieldDef fldTimeStamp = tdChild.fldTimeStamp;
                if (fldTimeStamp != null)
                {
                    string strQuery = string.Empty;
                    FieldParam[] Params = new FieldParam[tdChild.KeyFields.Count + 1];
                    int Ctr = 1;
                    foreach (FieldDef fld in tdChild.KeyFields.Values)
                    {
                        strQuery = string.Concat(" AND ", fld._FieldName, "=@",
                            Ctr.ToString());
                        Params[Ctr] = new FieldParam(Ctr.ToString(), fld);
                        Ctr++;
                    }
                    strQuery = string.Concat("SELECT ", fldTimeStamp._FieldName,
                        " FROM ", tdChild._TableName, " WHERE ",
                        fldTimeStamp._FieldName,
                        "=@0", strQuery);
                    Params[0] = new FieldParam("0", fldTimeStamp);

                    foreach (BaseEntity objEntity in ListEntity)
                    {
                        if ((DateTime)fldTimeStamp.GetValue(objEntity) != DateTime.MinValue)
                        {
                            Params[0].Value = fldTimeStamp.GetValue(objEntity);

                            Ctr = 1;
                            foreach (FieldDef fld in tdChild.KeyFields.Values)
                                Params[Ctr++].Value = fld.GetValue(objEntity);

                            if (!Find.IsExists(strQuery, Params))
                                throw new ApplicationException(string.Format(
                                    ErrorPersistance.DataNotFound, tdChild._TableName));
                        }
                    }
                }
                if (tdChild.ChildEntities.Count > 0)
                    foreach (BaseEntity objEntity in ListEntity)
                        _CheckTimeStampDelete(tdChild, objEntity);
            }
        }
        #endregion

        public IDbCommand CreateCommand(string SqlCommand,
            CommandType CommandType, params FieldParam[] Parameters)
        {
            if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }

            IDbCommand Cmd = CreateCommand();
            Cmd.Connection = Connection;
            Cmd.CommandText = PrepareSql(SqlCommand);
            Cmd.CommandType = CommandType;
            Cmd.Transaction = Trx;
            Cmd.CommandTimeout = 1000;

            if (Parameters != null && Parameters.Length > 0)
            {
                if (GetSqlParam() != "@")
                    Cmd.CommandText = Cmd.CommandText.Replace(
                        "@", GetSqlParam());
                foreach (FieldParam param in Parameters)
                    Cmd.Parameters.Add(CreateParameter(
                        param.FieldName, param));
            }
            return Cmd;
        }
        public IDbCommand CreateCommand(string SqlCommand,
            CommandType CommandType, List<FieldParam> Parameters)
        {
            if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }

            IDbCommand Cmd = CreateCommand();
            Cmd.Connection = Connection;
            Cmd.CommandText = PrepareSql(SqlCommand);
            Cmd.CommandType = CommandType;
            Cmd.Transaction = Trx;
            Cmd.CommandTimeout = 1000;

            if (Parameters != null && Parameters.Count > 0)
            {
                if (GetSqlParam() != "@")
                    Cmd.CommandText = Cmd.CommandText.Replace(
                        "@", GetSqlParam());
                foreach (FieldParam param in Parameters)
                    Cmd.Parameters.Add(CreateParameter(
                        param.FieldName, param));
            }
            return Cmd;
        }

        private string PrepareSql(string OrigSql)
        {
            if (GetSqlOrderDesc() != " DESC")
                OrigSql = OrigSql.Replace(" DESC", GetSqlOrderDesc());
            return OrigSql;
        }

        private bool _EnableWriteLog;
        public bool EnableWriteLog
        {
            get { return _EnableWriteLog; }
            set
            {
                _EnableWriteLog = value && BaseUtility.IsDebugMode;
                if (_EnableWriteLog)
                {
                    string SqlLogFileName = string.Concat(
                         Application.StartupPath, "\\",
                         EngineName, "_SqlLog.txt");
                    try
                    {
                        File.Delete(SqlLogFileName);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Write Data to SqlLog Data only on Debugging Mode
        /// </summary>
        /// <param name="LogData"></param>
        public void WriteLog(string LogData)
        {
            if (!EnableWriteLog) return;
            string SqlLogFileName = string.Concat(
                Application.StartupPath, "\\",
                EngineName, "_SqlLog.txt");
            using (StreamWriter swSqlLog = new 
                StreamWriter(SqlLogFileName, true))
            {
                swSqlLog.WriteLine(GetDbDateTime().ToString(
                    "dd MMM yyyy-hh:mm:ss  ") + LogData);
            }
        }

        public DataFinder Find;

        private int ExecuteCmdNonQuery(IDbCommand Cmd)
        {
            string TmpStr = Cmd.CommandText.ToUpper();
            bool UpdateCmd;

            int i = TmpStr.IndexOf("UPDATE", StringComparison.Ordinal);
            if (i >= 0)
            {
                i += 6;
                UpdateCmd = true;
            }
            else
            {
                UpdateCmd = false;
                i = TmpStr.IndexOf("INSERT INTO", StringComparison.Ordinal);
                if (i >= 0)
                    i += 11;
                else
                {
                    i = TmpStr.IndexOf("DELETE FROM", StringComparison.Ordinal);
                    if (i < 0) return _ExecuteNonQuery(Cmd);
                    i += 11;
                }
            }

            while (char.IsSeparator(TmpStr, i)) i++;
            int j = i;
            int TmpStrLen = TmpStr.Length;

            Ulang:
            if (TmpStr[j] == '[')
            {
                while (j < TmpStrLen && TmpStr[j] != ']') j++;
                if (TmpStr[j + 1] == '.') j++;
                goto Ulang;
            }
            else
                while (j < TmpStrLen && !char.IsSeparator(TmpStr, j) &&
                    TmpStr[j] != '(' && TmpStr[j] != '\n' && TmpStr[j] != '\r') j++;

            TmpStr = j == TmpStrLen ? Cmd.CommandText.Substring(i) :
                Cmd.CommandText.Substring(i, j - i);

            string TableName;
            j = TmpStr.LastIndexOf('.');
            if (j < 0)
                TableName = TmpStr;
            else
                TableName = TmpStr.Substring(j + 1);
            TableDef Td = MetaData.GetTableDef(TableName);
            if (Td != null)
            {
                if (UpdateCmd && Td.fldTimeStamp != null)
                {
                    j = Cmd.CommandText.IndexOf("SET", StringComparison.Ordinal);
                    Cmd.CommandText = string.Concat(
                        Cmd.CommandText.Substring(0, j + 4),
                        Td.fldTimeStamp._FieldName, "=", GetSqlNow(), ",",
                        Cmd.CommandText.Substring(j + 4));

                }
                int RetVal = _ExecuteNonQuery(Cmd);
                AutoUpdateList.EntityRefreshed(this, Td._ClassType);
                return RetVal;
            }
            else
            {
                if (UpdateCmd && BaseUtility.IsDebugMode)
                    throw new ApplicationException(string.Format(
                        ErrorMetaData.TableMetaDataNotFound, TmpStr));

                return _ExecuteNonQuery(Cmd);
            }
        }

        public int ExecuteNonQuery(string SqlCommand, 
            params FieldParam[] Parameters)
        {
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = CreateCommand(SqlCommand, 
                    CommandType.Text, Parameters);
                if (_EnableWriteLog) WriteLog(SqlCommand);
                return ExecuteCmdNonQuery(Cmd);
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }
        public int ExecuteNonQuery(string SqlCommand,
            List<FieldParam> Parameters)
        {
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = CreateCommand(SqlCommand,
                    CommandType.Text, Parameters);
                if (_EnableWriteLog) WriteLog(SqlCommand);
                return ExecuteCmdNonQuery(Cmd);
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }

        private int ExecuteInternalNonQuery(string SqlCommand,
            params FieldParam[] Parameters)
        {
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = CreateCommand(SqlCommand,
                    CommandType.Text, Parameters);
                if (_EnableWriteLog) WriteLog(SqlCommand);
                return _ExecuteNonQuery(Cmd);
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }
        private int ExecuteInternalNonQuery(string SqlCommand,
            List<FieldParam> Parameters)
        {
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = CreateCommand(SqlCommand,
                    CommandType.Text, Parameters);
                if (_EnableWriteLog) WriteLog(SqlCommand);
                return _ExecuteNonQuery(Cmd);
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }

        #region ExecuteBatchNonQuery
        public int ExecuteBatchNonQuery(string SqlBatchCommand, 
            bool StopIfReturnZero)
        {
            int RetVal = 0;
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                string[] SqlStr = SqlBatchCommand.Split('\0');
                IDbCommand Cmd = null;

                for (int j = 0; j < SqlStr.Length; j++)
                    if (SqlStr[j].Length > 0)
                    {
                        if (Cmd == null)
                            Cmd = CreateCommand(SqlStr[j], 
                                CommandType.Text);
                        else
                            Cmd.CommandText = PrepareSql(SqlStr[j]);
                        if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                        int i = ExecuteCmdNonQuery(Cmd);
                        if (StopIfReturnZero && i == 0) return 0;
                        RetVal += i;
                    }

                return RetVal;
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }
        public int ExecuteBatchNonQuery(BatchCommand BatchCommand, 
            bool StopIfReturnZero)
        {
            int CmdCount = BatchCommand.CommandCount;
            if (CmdCount == 0) return 0;
            int retVal = 0;
            IDbCommand Cmd = null;

            using (EntityTransaction tr = new EntityTransaction(this))
            {
                for (int j = 0; j < CmdCount; j++)
                {
                    string[] SqlCommands = BatchCommand.GetSqlCommand(j).Split('\0');
                    for (int k = 0; k < SqlCommands.Length; k++)
                    {
                        if (SqlCommands[k].Length == 0) continue;
                        if (Cmd == null)
                            Cmd = CreateCommand(SqlCommands[k], 
                                CommandType.Text,
                                BatchCommand.GetParameters(j));
                        else
                        {
                            Cmd.CommandText = PrepareSql(SqlCommands[k]);
                            Cmd.Parameters.Clear();

                            FieldParam[] Params = BatchCommand.GetParameters(j);
                            if (Params != null && Params.Length > 0)
                            {
                                if (GetSqlParam() != "@")
                                    Cmd.CommandText = Cmd.CommandText
                                        .Replace("@", GetSqlParam());

                                foreach (FieldParam Param in Params)
                                    Cmd.Parameters.Add(CreateParameter(
                                        Param.FieldName, Param));
                            }
                        }
                        if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                        int i = ExecuteCmdNonQuery(Cmd);
                        if (StopIfReturnZero && i == 0) return 0;
                        retVal += i;
                    }
                }
                tr.CommitTransaction();
            }
            return retVal;
        }
        private int ExecuteInternalBatchNonQuery(BatchCommand BatchCommand,
            bool StopIfReturnZero)
        {
            int CmdCount = BatchCommand.CommandCount;
            if (CmdCount == 0) return 0;
            int retVal = 0;
            IDbCommand Cmd = null;

            using (EntityTransaction tr = new EntityTransaction(this))
            {
                for (int j = 0; j < CmdCount; j++)
                {
                    string[] SqlCommands = BatchCommand.GetSqlCommand(j).Split('\0');
                    for (int k = 0; k < SqlCommands.Length; k++)
                    {
                        if (SqlCommands[k].Length == 0) continue;
                        if (Cmd == null)
                            Cmd = CreateCommand(SqlCommands[k],
                                CommandType.Text,
                                BatchCommand.GetParameters(j));
                        else
                        {
                            Cmd.CommandText = PrepareSql(SqlCommands[k]);
                            Cmd.Parameters.Clear();

                            FieldParam[] Params = BatchCommand.GetParameters(j);
                            if (Params != null && Params.Length > 0)
                            {
                                if (GetSqlParam() != "@")
                                    Cmd.CommandText = Cmd.CommandText
                                        .Replace("@", GetSqlParam());

                                foreach (FieldParam Param in Params)
                                    Cmd.Parameters.Add(CreateParameter(
                                        Param.FieldName, Param));
                            }
                        }
                        if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                        int i = _ExecuteNonQuery(Cmd);
                        if (StopIfReturnZero && i == 0) return 0;
                        retVal += i;
                    }
                }
                tr.CommitTransaction();
            }
            return retVal;
        }
        private int ExecuteInternalBatchNonQuery(string SqlBatchCommand,
            bool StopIfReturnZero)
        {
            int RetVal = 0;
            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                string[] SqlStr = SqlBatchCommand.Split('\0');
                IDbCommand Cmd = null;

                for (int j = 0; j < SqlStr.Length; j++)
                    if (SqlStr[j].Length > 0)
                    {
                        if (Cmd == null)
                            Cmd = CreateCommand(SqlStr[j],
                                CommandType.Text);
                        else
                            Cmd.CommandText = PrepareSql(SqlStr[j]);
                        if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                        int i = _ExecuteNonQuery(Cmd);
                        if (StopIfReturnZero && i == 0) return 0;
                        RetVal += i;
                    }

                return RetVal;
            }
            finally
            {
                if (MustClose) Connection.Close();
            }
        }
        #endregion

        public IList<TObject> ListFromSqlSelect<TObject>(string SqlSelect, 
            params FieldParam[] Parameters) where TObject : new()
        {
            int NumRecord = (int)Find.Value(string.Concat("SELECT ",
                GetSqlCount(), "(*) FROM (", SqlSelect, ") AS X"), 0, Parameters);
            TObject[] ListBE = new TObject[NumRecord];
            for (int i = 0; i < NumRecord; i++)
                ListBE[i] = BaseFactory.CreateInstance<TObject>();

            IList<TObject> RetVal = new List<TObject>();
            if (NumRecord == 0) return RetVal;

            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            if (Connection.State != ConnectionState.Open)
                Connection.Open();
            else if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }
            if (_EnableWriteLog) WriteLog(Cmd.CommandText);

            List<SetHandler> DictSet = new List<SetHandler>();

            DataReader = _ExecuteReader(Cmd);
            try
            {
                int ListBECtr = 0;
                if (DataReader.Read())
                {
                    Type tp = typeof(TObject);
                    int FieldCount = DataReader.FieldCount;
                    for (int i = 0; i < FieldCount; i++)
                    {
                        MemberInfo[] mi = tp.GetMember(DataReader.GetName(i),
                            BindingFlags.Instance | BindingFlags.Public |
                            BindingFlags.NonPublic);
                        if (mi != null && mi.Length > 0)
                            DictSet.Add(DynamicMethodCompiler
                                .CreateSetHandler(mi[0]));
                        else
                            DictSet.Add(null);
                    }
                    TObject Tmp = ListBE[ListBECtr++];
                    for (int i = 0; i < FieldCount; i++)
                        if (DictSet[i] != null)
                            DictSet[i](Tmp, DataReader[i]);
                    RetVal.Add(Tmp);

                    while (DataReader.Read())
                    {
                        Tmp = ListBE[ListBECtr++];
                        for (int i = 0; i < FieldCount; i++)
                            if (DictSet[i] != null)
                                DictSet[i](Tmp, DataReader[i]);
                        RetVal.Add(Tmp);
                    }
                }
            }
            finally
            {
                DataReader.Close();
                DataReader = null;
            }
            return RetVal;
        }

        #region TimeStamp
        public void TimeStampSetValue<TEntity>(string Condition, params
            FieldParam[] Parameters) where TEntity : BusinessEntity
        {
            TimeStampSetValue(typeof(TEntity), Condition, Parameters);
        }

        public void TimeStampSetValue(Type EntityType, string Condition, 
            params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(EntityType);
            if (td.fldTimeStamp != null)
                ExecuteNonQuery(string.Concat("UPDATE ", td.TableName, " SET ",
                    td.fldTimeStamp._FieldName, "=", GetSqlNow(), 
                    Condition.Length > 0 ? " WHERE " + Condition : string.Empty), 
                    Parameters);
            else
                throw new ApplicationException(string.Format(
                    ErrorEntity.TimeStampFieldNotFound, td._TableName));
        }

        public DateTime TimeStampGetValue<TEntity>(string Condition,
            params FieldParam[] Parameters) where TEntity : BusinessEntity
        {
            return TimeStampGetValue(typeof(TEntity), Condition, Parameters);
        }

        public DateTime TimeStampGetValue(Type EntityType, string Condition,
            params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(EntityType);
            if (td.fldTimeStamp != null)
                return (DateTime)Find.FirstValue(td.fldTimeStamp._FieldName, td.GetSqlHeaderView(this),
                    Condition, td.fldTimeStamp._FieldName, GetDbDateTime(), Parameters);
            else
                throw new ApplicationException(string.Format(
                    ErrorEntity.TimeStampFieldNotFound, td._TableName));
        }
        #endregion

        #region ExecuteReader
        public IDataReader ExecuteReader(string SqlSelect, 
            params FieldParam[] Parameters)
        {
            IDbCommand Cmd = CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            if (Connection.State != ConnectionState.Open) 
                Connection.Open();
            else if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }

            if (_EnableWriteLog) WriteLog(Cmd.CommandText);
            DataReader = _ExecuteReader(Cmd);
            return DataReader;
        }

        public void CloseReader()
        {
            if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }
        }
        #endregion

        #region OpenDataTable
        public DataTable OpenDataTable(string SqlSelect, 
            params FieldParam[] Parameters)
        {
            IDbCommand Cmd = CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                DataTable dt = new DataTable();
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);
                dt.Load(rdr);
                return dt;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }
        public DataTable OpenDataTable(DataTable DtReturn, 
            string SqlSelect, params FieldParam[] Parameters)
        {
            IDbCommand Cmd = CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                if (DtReturn == null)
                    DtReturn = new DataTable();
                else
                    DtReturn.Clear();
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);
                DtReturn.Load(rdr);
                return DtReturn;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }

        public DataTable OpenDataTable(string SqlSelect,
            List<FieldParam> Parameters)
        {
            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                DataTable dt = new DataTable();
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);
                dt.Load(rdr);
                return dt;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }
        public DataTable OpenDataTable(DataTable DtReturn,
            string SqlSelect, List<FieldParam> Parameters)
        {
            IDbCommand Cmd = CreateCommand(SqlSelect,
                CommandType.Text, Parameters);

            bool MustClose;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                if (DtReturn == null)
                    DtReturn = new DataTable();
                else
                    DtReturn.Clear();
                if (_EnableWriteLog) WriteLog(Cmd.CommandText);
                rdr = _ExecuteReader(Cmd);
                DtReturn.Load(rdr);
                return DtReturn;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Connection.Close();
            }
        }

        public AutoUpdateDataTable OpenDataTable(Type EntityType,
            string SqlSelect, string SqlOrder, bool UseCache)
        {
            return AutoUpdateList.GetAutoUpdateDataTable(EntityType,
                this, SqlSelect, SqlOrder, UseCache);
        }
        #endregion

        #region Transaction
        // For ValidateTableDef..
        private List<TableDef> ListTableChanged = new List<TableDef>();

        // For ValidateDbObject..
        private List<DbObject> ListDbObjectChanged = new List<DbObject>();

        [DebuggerNonUserCode]
        private class aulEntityChanged 
        {
            internal enum enChangeType 
            {
                Added, Edited, Deleted
            }
            public enChangeType ChangeType;
            public BusinessEntity Entity;

            public aulEntityChanged(BusinessEntity Entity, 
                enChangeType ChangeType)
            {
                this.Entity = Entity;
                this.ChangeType = ChangeType;
            }
        }
        // If List<aulEntityChanged> is null then reset 
        private Dictionary<Type, List<aulEntityChanged>> ListAULChanged = 
            new Dictionary<Type,List<aulEntityChanged>>();

        internal void AULEntityAdded(Type EntityType, BusinessEntity NewEntity)
        {
            BaseEntity CloneEntity;
            try
            {
                CloneEntity = MetaData.Clone(NewEntity);
            }
            catch
            {
                return;
            }

            List<aulEntityChanged> ListAUL;

            if (!ListAULChanged.TryGetValue(EntityType, out ListAUL))
            {
                ListAUL = new List<aulEntityChanged>();
                ListAULChanged.Add(EntityType, ListAUL);
            }
            else
            {
                if (ListAUL == null) return; //resetted..
            }
            ListAUL.Add(new aulEntityChanged((ParentEntity)CloneEntity,
                aulEntityChanged.enChangeType.Added));
        }
        internal void AULEntityEdited(Type EntityType, BusinessEntity EditedEntity)
        {
            BaseEntity CloneEntity;
            try
            {
                CloneEntity = MetaData.Clone(EditedEntity);
            }
            catch
            {
                return;
            }

            List<aulEntityChanged> ListAUL;

            if (!ListAULChanged.TryGetValue(EntityType, out ListAUL))
            {
                ListAUL = new List<aulEntityChanged>();
                ListAULChanged.Add(EntityType, ListAUL);
            }
            else
            {
                if (ListAUL == null) return; //resetted..
            }
            ListAUL.Add(new aulEntityChanged((BusinessEntity)CloneEntity,
                aulEntityChanged.enChangeType.Edited));
        }
        internal void AULEntityDeleted(Type EntityType, BusinessEntity DeletedEntity)
        {
            BaseEntity CloneEntity;
            try
            {
                CloneEntity = MetaData.Clone(DeletedEntity);
            }
            catch
            {
                return;
            }

            List<aulEntityChanged> ListAUL;

            if (!ListAULChanged.TryGetValue(EntityType, out ListAUL))
            {
                ListAUL = new List<aulEntityChanged>();
                ListAULChanged.Add(EntityType, ListAUL);
            }
            else
            {
                if (ListAUL == null) return; //resetted..
            }
            ListAUL.Add(new aulEntityChanged((BusinessEntity)CloneEntity,
                aulEntityChanged.enChangeType.Deleted));
        }
        internal void AULEntityRefreshed(Type EntityType)
        {
            ListAULChanged[EntityType] = null;
        }

        private Dictionary<IAutoUpdateList, int> ListRefreshed = 
            new Dictionary<IAutoUpdateList, int>();
        internal void AulRefreshed(IAutoUpdateList aul)
        {
            ListRefreshed[aul] = 1;
        }

        internal void BeginTransaction()
        {
            if (Trx != null) return;

            if (Connection.State != ConnectionState.Open) Connection.Open();
            if (DataReader != null)
            {
                if (!DataReader.IsClosed) DataReader.Close();
                DataReader = null;
            }
            try
            {
                Trx = Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                if (Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                    Trx = Connection.BeginTransaction();
                }
                else
                    throw ex;
            }
        }
        internal void CommitTransaction()
        {
            if (Trx == null) return;

            try
            {
                if (DataReader != null)
                {
                    if (!DataReader.IsClosed) DataReader.Close();
                    DataReader = null;
                }
                Trx.Commit();
                Trx = null;
            }
            finally
            {
                ListTableChanged.Clear();
                ListDbObjectChanged.Clear();
                foreach (KeyValuePair<Type, List<aulEntityChanged>>
                    ListKvAUL in ListAULChanged)
                    if (ListKvAUL.Value == null)
                        AutoUpdateList.EntityRefreshed(this, ListKvAUL.Key);
                    else
                        foreach(aulEntityChanged aulEntity in ListKvAUL.Value)
                            switch (aulEntity.ChangeType)
                            {
                                case aulEntityChanged.enChangeType.Added:
                                    AutoUpdateList.EntityAdded(this,
                                        ListKvAUL.Key, aulEntity.Entity);
                                    break;
                                case aulEntityChanged.enChangeType.Edited:
                                    AutoUpdateList.EntityEdited(this,
                                        ListKvAUL.Key, aulEntity.Entity);
                                    break;
                                case aulEntityChanged.enChangeType.Deleted:
                                    AutoUpdateList.EntityDeleted(this,
                                        ListKvAUL.Key, aulEntity.Entity);
                                    break;
                            }
                ListAULChanged.Clear();
                ListRefreshed.Clear();
                Connection.Close();
            }
        }
        internal void RollbackTransaction()
        {
            if (Trx == null) return;

            try
            {
                if (DataReader != null)
                {
                    if (!DataReader.IsClosed) DataReader.Close();
                    DataReader = null;
                }
                Trx.Rollback();
                Trx = null;

                #region Revalidate All TableDef
                foreach (TableDef td in ListTableChanged) 
                {
                    DataPersistance DpVal = td.GetDataPersistance(this);
                    td.SetIsExist(DpVal, false);
                }

                TableDef[] Arry = new TableDef[ListTableChanged.Count];
                ListTableChanged.CopyTo(Arry);
                foreach (TableDef td in Arry)
                    ValidateTableDef(td);
                #endregion

                #region Revalidate All DbObject
                //foreach (DbObject obj in ListDbObjectChanged)
                //    obj.SetIsExist(this, false);

                DbObject[] ArryDb = new DbObject[ListDbObjectChanged.Count];
                ListDbObjectChanged.CopyTo(ArryDb);
                foreach (DbObject obj in ArryDb)
                    ValidateDbObject(obj);
                #endregion
            }
            finally
            {
                ListAULChanged.Clear();
                ListTableChanged.Clear();
                ListDbObjectChanged.Clear();
                foreach (IAutoUpdateList aul in ListRefreshed.Keys)
                    aul.Refresh();
                Connection.Close();
            }
        }
        #endregion

        #region Variables
        public TVar GetVariable<TVar>(string ModuleName, string VarName, TVar DefaultValue)
        {
            return AppVariable.GetVariable<TVar>(this, ModuleName, VarName, DefaultValue);
        }
        public void SetVariable(string ModuleName, string VarName, object Value)
        {
            AppVariable.SetVariable(this, ModuleName, VarName, Value);
        }

        public AppVariables GetVariables(string ModuleName)
        {
            return AppVariable.GetVariables(this, ModuleName);
        }
        public void SetVariables(AppVariables Vars)
        {
            AppVariable.SetVariables(this, Vars);
        }
        #endregion

        #region ValidateDbObject - Belum bisa multi database !!!
        public void ValidateDbObject<TObjType>() where TObjType : DbObject
        {
            ValidateDbObject(BaseFactory.CreateInstance<TObjType>());
        }
        public void ValidateDbObject(Type ObjType)
        {
            ValidateDbObject((DbObject)BaseFactory.CreateInstance(ObjType));
        }

        private void ValidateDbObject(DbObject obj)
        {
            //if (obj.GetIsExist(this)) return;
            if (!BaseFramework.AutoUpdateMetaData && BaseUtility.IsDebugMode) return;
            ValidateTableDef<TableVersion>();

            TableVersion tv = new TableVersion();
            string ObjName = obj.GetType().Name;

            if (LoadEntity(tv, "TableName=@0", false,
                new FieldParam("0", "_Obj_" + ObjName)))
            {
                string ObjVersion = obj.GetAsmVersion();
                string MetaAsmName = obj.GetAsmName();
                int intCompare = 0;

                if (MetaAsmName == tv.AsmName)
                    intCompare = string.Compare(tv.DbVersion,
                        ObjVersion, true);
                else
                {
                    intCompare = -1;
                    tv.AsmName = MetaAsmName;
                }

                if (intCompare > 0)
                    throw new ApplicationException(string.Format(
                        ErrorPersistance.OlderEngine,
                        ObjName, ObjVersion, tv.DbVersion));

                if (intCompare < 0)
                {
                    string MetaStr = obj.GetDDLCreate(this);

                    using (EntityTransaction tr = new EntityTransaction(this))
                    {
                        if (MetaStr != tv.CreateStr)
                        {
                            string TmpSql = obj.GetDDLUpdate(this);
                            if (TmpSql.Length == 0) TmpSql = MetaStr.Replace("CREATE ", "ALTER ");
                            ExecuteInternalNonQuery(TmpSql);
                            tv.CreateStr = MetaStr;
                        }
                        tv.DbVersion = ObjVersion;
                        SaveUpdateEntity(tv, false, false);
                        tr.CommitTransaction();
                    }
                }
            }
            else
            {
                tv.TableName = ObjName;
                tv.AsmName = obj.GetAsmName();
                tv.DbVersion = obj.GetAsmVersion();

                using (EntityTransaction tr = new EntityTransaction(this))
                {
                    tv.CreateStr = obj.GetDDLCreate(this);
                    ExecuteInternalNonQuery(tv.CreateStr);

                    SaveNewEntity(tv, false, false);
                    tr.CommitTransaction();
                }
            }
            //obj.SetIsExist(this);
            if (Trx != null) ListDbObjectChanged.Add(obj);
            return;
        }
        #endregion

        protected virtual string BuildParam(FieldParam[] TableParams)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected virtual string BuildDDLParam(FieldParam[] TableParams)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region IDataPersistance Members

        string IDataPersistance.BuildParam(FieldParam[] TableParams)
        {
            return BuildParam(TableParams);
        }

        string IDataPersistance.BuildDDLParam(FieldParam[] TableParams)
        {
            return BuildDDLParam(TableParams);
        }

        #endregion

        public int SaveCancelEntity(ParentEntity Entity, string CancelUser, 
            DateTime CancelDateTime, string CancelNotes)
        {
            return Entity._SaveCancel(this, CancelUser, CancelDateTime, CancelNotes);
        }

        internal int _SaveCancel(ParentEntity pe, string CancelUser, 
            DateTime CancelDateTime, string CancelNotes)
        {
            TableDef td = MetaData.GetTableDef(pe.GetType());
            EnableCancelEntityAttribute ece = td.EnableCancelEntityAtr;

            if (ece == null) return 0;

            FieldDef fldStatus = td.GetFieldDef(ece.GetCancelStatusFieldName());
            string OldStats = fldStatus.GetValue(pe).ToString();
            try
            {
                fldStatus.SetValue(pe, ece.GetCancelStatus());
                pe[ece.GetCancelDateTimeFieldName()] = CancelDateTime;
                pe[ece.GetCancelNotesFieldName()] = CancelNotes;
                pe[ece.GetCancelUserFieldName()] = CancelUser;
                using (EntityTransaction tr = new EntityTransaction(this))
                {
                    SaveUpdateEntity(pe, false, false);
                    tr.CommitTransaction();
                }
                return 1;
            }
            catch (Exception ex)
            {
                fldStatus.SetValue(pe, OldStats);
                pe[ece.GetCancelNotesFieldName()] = string.Empty;
                throw ex;
            }
        }
    }
}
