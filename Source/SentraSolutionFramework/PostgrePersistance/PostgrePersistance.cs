using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Entity;
using Npgsql;
using NpgsqlTypes;
using System.Drawing;

namespace SentraSolutionFramework.Persistance
{
    public class PostgrePersistance : DataPersistance
    {
        public PostgrePersistance(string ConnectionString,
            bool AutoCreateDb, string FolderLocation)
            : base(ConnectionString, AutoCreateDb, FolderLocation)
        {
        }

        public override string GetSqlSelectTopN(string SqlSelect, int N, string OrderBy)
        {
            if (OrderBy.Length > 0)
                return string.Concat("SELECT * FROM (", SqlSelect,
                    ") x ORDER BY ", PrepareOrder(OrderBy),
                    " LIMIT ", N.ToString());
            else
                return string.Concat("SELECT * FROM (", SqlSelect,
                    ") x LIMIT ", N.ToString());
        }

        protected override bool IsTableExist(string TableName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override TableDef CreateTableDef(string TableName)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void InitConnection()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region Generic DataAccess
        protected override IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_ConnectionString);
        }
        protected override IDbDataAdapter CreateAdapter()
        {
            return new NpgsqlDataAdapter();
        }
        protected override IDataParameter CreateParameter(string ParamName, FieldParam Param)
        {
            NpgsqlParameter p = new NpgsqlParameter();
            p.ParameterName = GetSqlParam() + ParamName;
            switch (Param.DataType)
            {
                case DataType.VarChar:
                    p.NpgsqlDbType = NpgsqlDbType.Varchar;
                    p.Size = Param.Length;
                    break;
                case DataType.Char:
                    p.NpgsqlDbType = NpgsqlDbType.Char;
                    p.Size = Param.Length;
                    break;
                case DataType.Integer:
                    p.NpgsqlDbType = NpgsqlDbType.Integer;
                    break;
                case DataType.Decimal:
                    p.NpgsqlDbType = NpgsqlDbType.Numeric;
                    p.Precision = (byte)Param.Length;
                    p.Scale = (byte)Param.Scale;
                    break;
                case DataType.Boolean:
                    p.NpgsqlDbType = NpgsqlDbType.Boolean;
                    break;
                case DataType.DateTime:
                    p.NpgsqlDbType = NpgsqlDbType.Date;
                    break;
                case DataType.Date:
                    p.NpgsqlDbType = NpgsqlDbType.Date;
                    break;
                case DataType.Time:
                    p.NpgsqlDbType = NpgsqlDbType.Time;
                    break;
                case DataType.TimeStamp:
                    p.NpgsqlDbType = NpgsqlDbType.Date;
                    break;
                case DataType.Binary:
                    p.NpgsqlDbType = NpgsqlDbType.Bytea;
                    break;
                case DataType.Image:
                    p.NpgsqlDbType = NpgsqlDbType.Bytea;
                    if (Param.Value != null)
                        p.Value = Helper.ConvertImageToByteArray(
                            (Image)Param.Value);
                    return p;
            }
            if (Param.Value != null) p.Value = Param.Value;
            return p;
        }
        protected override IDbCommand CreateCommand()
        {
            return new NpgsqlCommand();
        }
        #endregion

        public override string GetSqlNow()
        {
            return "CURRENT_TIMESTAMP";
        }

        protected override string GetSqlCreateTable(TableDef td)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override string GetSqlUpdateTable(TableDef MetaTd, TableDef dbTd)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
