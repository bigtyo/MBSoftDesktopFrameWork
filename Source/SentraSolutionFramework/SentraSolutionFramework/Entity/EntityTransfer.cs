using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Persistance;
using System.Data;
using System.Diagnostics;

namespace SentraSolutionFramework.Entity
{
    [DebuggerNonUserCode]
    public class EntityTransfer
    {
        public DataPersistance DpSource;
        public DataPersistance DpDestination;

        public List<TableTransfer> ListTable = new List<TableTransfer>();
        public List<TypeTransfer> ListObjectType = new List<TypeTransfer>();

        public bool TransferData()
        {
            TableVersion tv = new TableVersion();

            using (EntityTransaction Tr = 
                new EntityTransaction(DpDestination)) 
            {
                foreach (TableTransfer tbl in ListTable) 
                {
                    tv.TableName= tbl.TableName;
                    DpSource.LoadEntity(tv, false);

                    TransferTable(new TableDef(tbl.TableName, tv.CreateStr),
                        tbl.Condition);
                }
                foreach (TypeTransfer typ in ListObjectType)
                {
                    TableDef td = MetaData.GetTableDef(typ.ObjectType);
                    TransferTable(td, typ.Condition);
                    foreach (EntityCollDef ecd in td.ChildEntities)
                        TransferTable(MetaData.GetTableDef(ecd.ChildType), 
                            typ.Condition);
                }
                Tr.CommitTransaction();
            }
            return true;
        }

        private void TransferTable(TableDef td, string Condition)
        {
            string strWhere = Condition.Length > 0 ?
                " WHERE " + Condition : string.Empty;

            DpDestination.ValidateTableDef(td);

            FieldParam[] ListParam = new FieldParam[
                td.KeyFields.Count + td.NonKeyFields.Count];
            int paramCtr = 0;
            StringBuilder strFieldsDest = new StringBuilder();
            StringBuilder strFieldsSource = new StringBuilder();
            foreach (FieldDef fld in td.KeyFields.Values) 
            {
                strFieldsDest.Append(",").Append(fld._FieldName);
                strFieldsSource.Append(",").Append(fld._FieldName);
                ListParam[paramCtr++] = new FieldParam(fld);
            }
            foreach(FieldDef fld in td.NonKeyFields.Values) 
            {
                strFieldsDest.Append(",").Append(fld._FieldName);
                strFieldsSource.Append(",").Append(fld._FieldName);
                ListParam[paramCtr++] = new FieldParam(fld);
            }

            string strTemp = strFieldsDest.Remove(0, 1).ToString();
            string strInsert = string.Concat("INSERT INTO ", 
                td._TableName, "(", 
                strTemp, ") VALUES (", 
                strTemp.Replace(",", ",@").Insert(0, "@"), ")");

            strTemp = strFieldsSource.Remove(0, 1).ToString();
            DataTable dt = DpSource.OpenDataTable(string.Concat(
                "SELECT ", strTemp, " FROM ", td._TableName, strWhere));

            int j = dt.Columns.Count;
            IDbCommand Cmd = DpDestination.CreateCommand(strInsert,
                CommandType.Text, ListParam);
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < j; i++)
                    Cmd.Parameters[i] = dr[i];
                Cmd.ExecuteNonQuery();
            }
        }
    }

    [DebuggerNonUserCode]
    public class TableTransfer
    {
        public string TableName;
        public string Condition;

        public TableTransfer(string TableName, string Condition)
        {
            this.TableName = TableName;
            this.Condition = Condition;
        }
    }

    [DebuggerNonUserCode]
    public class TypeTransfer
    {
        public Type ObjectType;
        public string Condition;

        public TypeTransfer(Type ObjectType, string Condition)
        {
            this.ObjectType = ObjectType;
            this.Condition = Condition;
        }
    }

}
