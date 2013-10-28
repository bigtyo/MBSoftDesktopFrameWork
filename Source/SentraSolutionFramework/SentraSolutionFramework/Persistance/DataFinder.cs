using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Persistance;
using System.Data;
using SentraSolutionFramework.Entity;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework.Persistance
{
    [DebuggerNonUserCode]
    public sealed class DataFinder
    {
        internal DataPersistance Dp;

        internal DataFinder() { }

        private object NormalizeVarType(object OrigValue)
        {
            Type tp = OrigValue.GetType();

            if (tp == typeof(Single) ||
                tp == typeof(double))
                return OrigValue == typeof(decimal);
            else if (tp == typeof(DBNull))
                return null;
            else
                return OrigValue;
        }

        private string GetFieldName(TableDef td, string FieldName)
        {
            FieldDef fld = td.GetFieldDef(FieldName);
            if (fld == null)
                if (td._dva != null)
                    return FieldName;
                else
                    throw new ApplicationException(string.Format(ErrorEntity.FieldNotFound,
                        td._TableName, FieldName));
            return fld._dtlsa == null ? FieldName :
                string.Concat("(", fld._dtlsa.GetSqlQuery(), ")");
        }

        private object _ExecuteScalar(IDbCommand Cmd)
        {
            object TmpValue;

            bool MustClose;
            if (Cmd.Connection.State != ConnectionState.Open)
            {
                Cmd.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                TmpValue = Cmd.ExecuteScalar();
            }
            finally
            {
                if (MustClose) Cmd.Connection.Close();
            }
            return TmpValue;
        }

        #region Find Min, Max, Count, Sum, Avg Value
        public object MinValue(string FieldName, string TableName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlMin(), "(", FieldName,
                ") FROM ", TableName);

            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object MaxValue(string FieldName, string TableName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlMax(), "(", FieldName, ") FROM ", TableName);

            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object CountValue(string FieldName, string TableName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlCount(), "(", FieldName, ") FROM ", TableName);

            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object SumValue(string FieldName, string TableName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlSum(), "(", FieldName, ") FROM ", TableName);

            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object AvgValue(string FieldName, string TableName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlAvg(), "(", FieldName, ") FROM ", TableName);

            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        #endregion

        #region Find Min, Max, Count, Sum, Avg Value (Generic)
        public object MinValue<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlMin(), "(", 
                GetFieldName(td, FieldName), ") FROM ", 
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object MaxValue<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlMax(), "(", 
                GetFieldName(td, FieldName), ") FROM ",
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object SumValue<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlSum(), "(",
                GetFieldName(td, FieldName), ") FROM ",
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object CountValue<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlCount(), "(",
                GetFieldName(td, FieldName), ") FROM ",
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object AvgValue<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters) where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            object _Value;
            string SqlSelect = string.Concat("SELECT ",
                Dp.GetSqlAvg(), "(",
                GetFieldName(td, FieldName), ") FROM ",
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        #endregion

        #region FindValue
        public object Value(IDbCommand Cmd, object DefaultValue)
        {
            object _Value;
            return TryFindValue(Cmd, out _Value) ? _Value : DefaultValue;
        }

        public object Value(string FieldName, string TableName,
            string Conditions, object DefaultValue, 
            params FieldParam[] Parameters)
        {
            object _Value;
            return TryFindFirstValue(FieldName, TableName, Conditions,
                string.Empty, out _Value, Parameters) ?
                _Value : DefaultValue;
        }

        public object Value(string SqlSelect, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            return TryFindValue(SqlSelect, out _Value, Parameters) ?
                _Value : DefaultValue;
        }

        public object FirstValue(string FieldName, string TableName,
            string Conditions, string OrderFields, object DefaultValue,
            params FieldParam[] Parameters)
        {
            object _Value;
            return TryFindFirstValue(FieldName, TableName, Conditions,
                OrderFields, out _Value, Parameters) ?
                _Value : DefaultValue;
        }

        public object FirstValue(string SqlSelect, string OrderFields,
            object DefaultValue, params FieldParam[] Parameters)
        {
            object _Value;
            return TryFindFirstValue(SqlSelect, OrderFields,
                out _Value, Parameters) ? _Value : DefaultValue;
        }
        #endregion

        #region FindValue Generic
        public object Value<TEntity>(string FieldName,
            string Conditions, object DefaultValue,
            params FieldParam[] Parameters)
            where TEntity : BaseEntity
        {
            object _Value;
            return TryFindFirstValue<TEntity>(FieldName, Conditions,
                string.Empty, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        public object FirstValue<TEntity>(string FieldName,
            string Conditions, string OrderFields, object DefaultValue,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            object _Value;
            return TryFindFirstValue<TEntity>(FieldName, Conditions,
                OrderFields, out _Value, Parameters) ?
                _Value : DefaultValue;
        }
        #endregion

        #region TryFind Max,Min,Sum,Count,Avg Value
        public bool TryFindMaxValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlMax(),
                "(", FieldName, ") FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindMinValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlMin(),
                "(", FieldName, ") FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindSumValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlSum(),
                "(", FieldName, ") FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindCountValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlCount(),
                "(", FieldName, ") FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindAvgValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlAvg(),
                "(", FieldName, ") FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return TryFindValue(SqlSelect, out Value, Parameters);
        }
        #endregion

        #region TryFind Max,Min,Sum,Count,Avg Value (Generic)
        public bool TryFindMaxValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlMax(),
                "(", GetFieldName(td, FieldName), 
                ") FROM ", td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindMinValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlMin(),
                "(", GetFieldName(td, FieldName),
                ") FROM ", td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindSumValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlSum(),
                "(", GetFieldName(td, FieldName),
                ") FROM ", td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindCountValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlCount(),
                "(", GetFieldName(td, FieldName),
                ") FROM ", td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out Value, Parameters);
        }
        public bool TryFindAvgValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", Dp.GetSqlAvg(),
                "(", GetFieldName(td, FieldName),
                ") FROM ", td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            return Dp.Find.TryFindValue(SqlSelect, out Value, Parameters);
        }
        #endregion

        #region TryFindValue
        public bool TryFindFirstValue(string FieldName, string TableName,
            string Conditions, string OrderFields, out object Value,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ",
                FieldName, " FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect,
                    " WHERE ", Conditions);

            return TryFindValue(
                Dp.GetSqlSelectTopN(SqlSelect, 1, OrderFields),
                out Value, Parameters);
        }

        public bool TryFindValue(string FieldName, string TableName,
            string Conditions, out object Value,
            params FieldParam[] Parameters)
        {
            return TryFindFirstValue(FieldName, TableName, Conditions,
                string.Empty, out Value, Parameters);
        }
        
        // Real Find

        public bool TryFindFirstValue(string SqlSelect,
            string OrderFields, out object Value,
            params FieldParam[] Parameters)
        {
            return TryFindValue(
                Dp.GetSqlSelectTopN(SqlSelect, 1, OrderFields),
                out Value, Parameters);
        }
        public bool TryFindValue(string SqlSelect, out object Value,
            params FieldParam[] Parameters)
        {
            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            try
            {
                IDbCommand Cmd = Dp.CreateCommand(
                    SqlSelect, CommandType.Text, Parameters);
                
                Dp.WriteLog(SqlSelect);

                object TmpValue = _ExecuteScalar(Cmd);
                if (TmpValue == DBNull.Value || TmpValue == null)
                    Value = null;
                else
                {
                    Type tp = TmpValue.GetType();
                    if (tp == typeof(Single) ||
                        tp == typeof(double))
                        Value = TmpValue == typeof(decimal);
                    else
                        Value = TmpValue;
                }
                return Value != null;
            }
            finally
            {
                if (MustClose) Dp.Connection.Close();
            }
        }

        public bool TryFindValue(IDbCommand Cmd, out object Value)
        {
            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;
            try
            {
                Dp.WriteLog(Cmd.CommandText);

                object TmpValue = _ExecuteScalar(Cmd);

                if (TmpValue == DBNull.Value || TmpValue == null)
                    Value = null;
                else
                {
                    Type tp = TmpValue.GetType();
                    if (tp == typeof(Single) ||
                        tp == typeof(double))

                        Value = Convert.ToDecimal(TmpValue);
                    else
                        Value = TmpValue;
                }
                return Value != null;
            }
            finally
            {
                if (MustClose) Dp.Connection.Close();
            }
        }
        #endregion

        #region TryFindValue (Generic)
        public bool TryFindFirstValue<TEntity>(string FieldName,
            string Conditions, string OrderFields, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string SqlSelect = string.Concat("SELECT ", 
                GetFieldName(td, FieldName), " FROM ",
                td.GetSqlHeaderView(Dp));
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect,
                    " WHERE ", Conditions);

            return Dp.Find.TryFindValue(
                Dp.GetSqlSelectTopN(SqlSelect, 1, OrderFields),
                out Value, Parameters);
        }
        public bool TryFindValue<TEntity>(string FieldName,
            string Conditions, out object Value,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            return TryFindFirstValue<TEntity>(FieldName,
                Conditions, string.Empty, out Value, Parameters);
        }
        #endregion

        #region FindValues
        public object[] Values(string FieldNames, string TableName,
            string Conditions, params FieldParam[] Parameters)
        {
            object[] _Values;
            TryFindFirstValues(FieldNames, TableName,
                Conditions, string.Empty, out _Values, Parameters);
            return _Values;
        }
        public object[] FirstValues(string FieldNames, string TableName,
            string Conditions, string OrderFields,
            params FieldParam[] Parameters)
        {
            object[] _Values;
            TryFindFirstValues(FieldNames, TableName,
                Conditions, OrderFields, out _Values, Parameters);
            return _Values;
        }

        public object[] Values(string SqlSelect,
            params FieldParam[] Parameters)
        {
            object[] _Values;
            TryFindValues(SqlSelect, out _Values, Parameters);
            return _Values;
        }

        public object[] Values(IDbCommand Cmd)
        {
            object[] _Values;
            TryFindValues(Cmd, out _Values);
            return _Values;
        }
        #endregion

        #region FindValues (Generic)
        public object[] Values<TEntity>(string FieldNames,
            string Conditions, params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            object[] _Values;
            TryFindFirstValues<TEntity>(FieldNames, Conditions, 
                string.Empty, out _Values, Parameters);
            return _Values;
        }
        public object[] FirstValues<TEntity>(string FieldNames,
            string Conditions, string OrderFields,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            object[] _Values;
            TryFindFirstValues<TEntity>(FieldNames, Conditions,
                OrderFields, out _Values, Parameters);
            return _Values;
        }
        #endregion

        #region TryFindValues
        public bool TryFindValues(string FieldNames, string TableName,
            string Conditions, out object[] Values,
            params FieldParam[] Parameters)
        {
            return TryFindFirstValues(FieldNames, TableName, Conditions,
                string.Empty, out Values, Parameters);
        }
        
        // Real Find...
        public bool TryFindFirstValues(string FieldNames, string TableName,
            string Conditions, string OrderFields, out object[] Values,
            params FieldParam[] Parameters)
        {
            string SqlSelect = string.Concat("SELECT ",
                FieldNames, " FROM ", TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            IDbCommand Cmd = Dp.CreateCommand(
                Dp.GetSqlSelectTopN(SqlSelect, 1, OrderFields),
                CommandType.Text, Parameters);

            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                if (rdr.Read())
                {
                    Values = new object[rdr.FieldCount];
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Values[i] = NormalizeVarType(rdr[i]);
                    return true;
                }
                Values = null;
                return false;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }

        // Real Find...
        public bool TryFindValues(string SqlSelect, out object[] Values,
            params FieldParam[] Parameters)
        {
            IDbCommand Cmd = Dp.CreateCommand(SqlSelect, 
                CommandType.Text, Parameters);

            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                if (rdr.Read())
                {
                    Values = new object[rdr.FieldCount];
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Values[i] = NormalizeVarType(rdr[i]);
                    return true;
                }
                Values = null;
                return false;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }

        public bool TryFindValues(IDbCommand Cmd, out object[] Values)
        {
            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                if (rdr.Read())
                {
                    Values = new object[rdr.FieldCount];
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Values[i] = NormalizeVarType(rdr[i]);
                    return true;
                }
                Values = null;
                return false;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }
        #endregion

        #region TryFindValues (Generic)
        public bool TryFindValues<TEntity>(string FieldNames,
            string Conditions, out object[] Values,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            return TryFindFirstValues<TEntity>(FieldNames,
                Conditions, string.Empty, out Values, Parameters);
        }

        // Real Find
        public bool TryFindFirstValues<TEntity>(string FieldNames,
            string Conditions, string OrderFields, out object[] Values,
            params FieldParam[] Parameters) 
            where TEntity : BaseEntity
        {
            TableDef td = MetaData.GetTableDef(typeof(TEntity));
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);
            string[] FName = FieldNames.Split(',');
            string SqlSelect = string.Empty;

            bool IsLSQLExist = false;

            for (int i = 0; i < FName.Length; i++)
            {
                FieldDef fld = td.GetFieldDef(FName[i]);
                if (fld != null)
                {
                    if (fld._dtlsa == null)
                        SqlSelect = string.Concat(SqlSelect, ",",
                            fld._FieldName);
                    else
                    {
                        SqlSelect = string.Concat(SqlSelect, ",(",
                            fld._dtlsa.GetSqlQuery(), ") AS ",
                            fld._FieldName);
                        IsLSQLExist = true;
                    }
                }
                else
                    SqlSelect = string.Concat(SqlSelect, ",", FName[i].Trim());
            }
            SqlSelect = string.Concat("SELECT ", SqlSelect.Remove(0, 1), 
                " FROM ", td.GetSqlHeaderView(Dp));
            if (IsLSQLExist)
                SqlSelect = string.Concat("SELECT * FROM (", SqlSelect, ") AS ", td._TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            IDbCommand Cmd = Dp.CreateCommand(
                Dp.GetSqlSelectTopN(SqlSelect, 1, OrderFields),
                CommandType.Text, Parameters);

            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                if (rdr.Read())
                {
                    Values = new object[rdr.FieldCount];
                    for (int i = 0; i < rdr.FieldCount; i++)
                        Values[i] = NormalizeVarType(rdr[i]);
                    return true;
                }
                Values = null;
                return false;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }
        #endregion

        #region IsExists
        public bool IsExists(IDbCommand Cmd)
        {
            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                return rdr.Read();
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }
        public bool IsExists(string SqlSelect, 
            params FieldParam[] Parameters)
        {
            IDbCommand Cmd;
            if (SqlSelect.Length > 0)
                Cmd = Dp.CreateCommand(Dp.GetSqlSelectTopN(SqlSelect, 1, string.Empty),
                    CommandType.Text, Parameters);
            else
                Cmd = Dp.CreateCommand(Dp.GetSqlSelectTopN(SqlSelect, 1, string.Empty),
                    CommandType.Text, Parameters);

            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false; 
            
            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                return rdr.Read();
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }
        public bool IsExists<TEntity>(string Conditions, 
            params FieldParam[] Parameters)
            where TEntity : BaseEntity
        { return IsExists(typeof(TEntity), Conditions, Parameters); }
        public bool IsExists(Type EntityType, string Conditions,
            params FieldParam[] Parameters)
        {
            TableDef td = MetaData.GetTableDef(EntityType);
            DataPersistance Dp = td.GetDataPersistance(this.Dp);
            Dp.ValidateTableDef(td);

            string FieldName = string.Empty;

            foreach (FieldDef fd in td.KeyFields.Values)
            {
                FieldName = fd._FieldName;
                break;
            }

            string SqlSelectBld = string.Empty;
            foreach (FieldDef fd in td.NonKeyFields.Values)
                if (fd._dtlsa == null)
                {
                    if (FieldName.Length == 0)
                        FieldName = fd._FieldName;
                }
                else
                    SqlSelectBld = string.Concat(SqlSelectBld, ",(", 
                        fd._dtlsa.GetSqlQuery(), ") AS ", fd._FieldName);

            string SqlSelect = SqlSelectBld.Length == 0 ? string.Concat("SELECT ",
                FieldName, " FROM ", td.GetSqlHeaderView(Dp)) : string.Concat(
                "SELECT ", FieldName, " FROM (SELECT *", SqlSelectBld, " FROM ",
                td.GetSqlHeaderView(Dp), ") AS ", td._TableName);
            if (Conditions.Length > 0)
                SqlSelect = string.Concat(SqlSelect, " WHERE ", Conditions);
            SqlSelect = Dp.GetSqlSelectTopN(SqlSelect, 1, string.Empty);
            IDbCommand Cmd = Dp.CreateCommand(SqlSelect, CommandType.Text,
                Parameters);
            
            bool MustClose;
            if (Dp.Connection.State != ConnectionState.Open)
            {
                Dp.Connection.Open();
                MustClose = true;
            }
            else
                MustClose = false;

            IDataReader rdr = null;
            try
            {
                Dp.WriteLog(Cmd.CommandText);
                rdr = Dp._ExecuteReader(Cmd);
                return rdr.Read();
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (MustClose) Dp.Connection.Close();
            }
        }
        #endregion
    }
}
