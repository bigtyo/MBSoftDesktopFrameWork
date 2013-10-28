using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using SentraSolutionFramework.Persistance;

namespace SentraSolutionFramework.Entity
{
    public abstract class EntityList
    {
        internal abstract void AddEntity(DataPersistance dp, object NewEntity);
        internal abstract void EditEntity(DataPersistance dp, object OldEntity, object NewEntity);
        internal abstract void DeleteEntity(DataPersistance dp, object DeletedEntity);

        protected WeakReference wrdt;
        internal bool IsAlive { get { return wrdt.IsAlive; } }

        #region EntityList Memory
        private static int LastKey = 1;
        internal static SortedList<string, SortedList<int, EntityList>>
            _List = new SortedList<string, SortedList<int, EntityList>>();
        internal static int RegisterEntityList(Type EntityType, EntityList el) 
        {
            SortedList<int, EntityList> ListEL;
            if (!_List.TryGetValue(EntityType.Name, out ListEL)) 
            {
                ListEL = new SortedList<int, EntityList>();
                _List.Add(EntityType.Name, ListEL);
            }
            ListEL.Add(LastKey, el);
            return LastKey++;
        }
        internal static void UnregisterEntityList(Type EntityType, int KeyId) 
        {
            SortedList<int, EntityList> ListEL;
            if (!_List.TryGetValue(EntityType.Name, out ListEL)) return;
            ListEL.Remove(KeyId);
        }
        internal static void ReregisterEntityList(Type EntityType, int KeyId, EntityList el)
        {
            SortedList<int, EntityList> ListEL;
            if (!_List.TryGetValue(EntityType.Name, out ListEL)) return;
            ListEL[KeyId] = el;
        }
        #endregion

        #region DataTable/ ListEntity Registering
        public static void LoadDataTable(Type EntityType, DataPersistance dp,
            string Condition, string OrderCondition, out DataTable dt, ref int KeyId)
        {
            if (KeyId == 0)
                KeyId = RegisterEntityList(EntityType, new EntityDataTable(
                    EntityType, dp, Condition, OrderCondition, out dt));
            else
                ReregisterEntityList(EntityType, KeyId, new EntityDataTable(
                    EntityType, dp, Condition, OrderCondition, out dt));
        }
        public static void LoadDataTable(Type EntityType, DataPersistance dp, 
            string SqlSelect, out DataTable dt, ref int KeyId)
        {
            if (KeyId == 0)
                KeyId = RegisterEntityList(EntityType, new EntityDataTable(
                    EntityType, dp, SqlSelect, out dt));
            else
                ReregisterEntityList(EntityType, KeyId, new EntityDataTable(
                    EntityType, dp, SqlSelect, out dt));
        }
        public static void LoadDataTable(Type EntityType, DataPersistance dp,
            string ColumnList, string Condition, string OrderCondition, 
            out DataTable dt, ref int KeyId)
        {
            if (KeyId == 0)
                KeyId = RegisterEntityList(EntityType, new EntityDataTable(
                    EntityType, dp, ColumnList, Condition, OrderCondition, out dt));
            else
                ReregisterEntityList(EntityType, KeyId, new EntityDataTable(
                    EntityType, dp, ColumnList, Condition, OrderCondition, out dt));
        }

        public static void LoadDataTable<TEntity>(DataPersistance dp,
            string Condition, string OrderCondition, out DataTable dt, 
            ref int KeyId) where TEntity : class
        {
            LoadDataTable(typeof(TEntity), dp, Condition, OrderCondition,
                out dt, ref KeyId);
        }
        public static void LoadDataTable<TEntity>(DataPersistance dp,
            string SqlSelect, out DataTable dt,
            ref int KeyId) where TEntity : class
        {
            LoadDataTable(typeof(TEntity), dp, SqlSelect,
                out dt, ref KeyId);
        }
        public static void LoadDataTable<TEntity>(DataPersistance dp,
            string ColumnList, string Condition, string OrderCondition, 
            out DataTable dt, ref int KeyId) where TEntity : class
        {
            LoadDataTable(typeof(TEntity), dp, ColumnList, 
                Condition, OrderCondition, out dt, ref KeyId);
        }

        public static void LoadEntities<TEntity>(DataPersistance dp,
            string Condition, string OrderCondition, bool CallLoadRule,
            out IList<TEntity> ListEntity, ref int KeyId)
            where TEntity : class, new()
        {
            if (KeyId == 0)
                KeyId = RegisterEntityList(typeof(TEntity), 
                    new EntityBindingList<TEntity>(dp, Condition, 
                    OrderCondition, CallLoadRule, out ListEntity));
            else
                ReregisterEntityList(typeof(TEntity), KeyId, 
                    new EntityBindingList<TEntity>(dp, Condition, 
                    OrderCondition, CallLoadRule, out ListEntity));
        }

        public static void FastLoadEntities<TEntity>(DataPersistance dp,
            string ColumnList, string Condition, string OrderCondition, 
            out IList<TEntity> ListEntity, ref int KeyId)
            where TEntity : class, new()
        {
            if (KeyId == 0)
                KeyId = RegisterEntityList(typeof(TEntity),
                    new EntityBindingList<TEntity>(dp, ColumnList, 
                    Condition, OrderCondition, out ListEntity));
            else
                ReregisterEntityList(typeof(TEntity), KeyId,
                    new EntityBindingList<TEntity>(dp, ColumnList,
                    Condition, OrderCondition, out ListEntity));
        }

        public static void Dispose(Type EntityType, int KeyId)
        {
            UnregisterEntityList(EntityType, KeyId);
        }
        public static void Dispose<TEntity>(int KeyId)
            where TEntity : class
        {
            UnregisterEntityList(typeof(TEntity), KeyId);
        }

        #endregion

        public static void AddEntity(DataPersistance dp, Type EntityType, object NewEntity)
        {
            SortedList<int, EntityList> ListEl;
            if (!_List.TryGetValue(EntityType.Name, out ListEl)) return;

            int i = 0;
            while (i < ListEl.Count)
            {
                EntityList el = ListEl.Values[i];
                if (!el.IsAlive)
                    ListEl.RemoveAt(i);
                else
                {
                    i++;
                    el.AddEntity(dp, NewEntity);
                }
            }
        }
        public static void EditEntity(DataPersistance dp, Type EntityType, object OldEntity, object NewEntity)
        {
            SortedList<int, EntityList> ListEl;
            if (!_List.TryGetValue(EntityType.Name, out ListEl)) return;

            int i = 0;
            while (i < ListEl.Count)
            {
                EntityList el = ListEl.Values[i];
                if (!el.IsAlive)
                    ListEl.RemoveAt(i);
                else
                {
                    i++;
                    el.EditEntity(dp, OldEntity, NewEntity);
                }
            }
        }
        public static void DeleteEntity(DataPersistance dp, Type EntityType, object DeletedEntity)
        {
            SortedList<int, EntityList> ListEl;
            if (!_List.TryGetValue(EntityType.Name, out ListEl)) return;

            int i = 0;
            while (i < ListEl.Count)
            {
                EntityList el = ListEl.Values[i];
                if (!el.IsAlive)
                    ListEl.RemoveAt(i);
                else
                {
                    i++;
                    el.DeleteEntity(dp, DeletedEntity);
                }
            }
        }
    }

    internal class EntityDataTable : EntityList
    {
        private TableDef td;
        private DataPersistance dp;

        private Type EntityType;
        private string SqlQuery;

        public EntityDataTable(Type EntityType, DataPersistance dp,
            string SqlSelect, out DataTable dt)
        {
            this.EntityType = EntityType;
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp ?? BaseFramework.DefaultDataPersistance;

            dt = this.dp.OpenDataTable(null, SqlSelect);
            foreach (DataColumn dc in dt.Columns)
                dc.ReadOnly = false;

            wrdt = new WeakReference(dt);
            SqlQuery = BuildSqlQuery(string.Empty, SqlSelect);
        }

        public EntityDataTable(Type EntityType, DataPersistance dp,
            string ColumnList, string Condition,
            string OrderCondition, out DataTable dt)
        {
            this.EntityType = EntityType;
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp ?? BaseFramework.DefaultDataPersistance;

            dt = this.dp.OpenDataTable(null,
                EntityType, ColumnList, Condition, OrderCondition);
            foreach (DataColumn dc in dt.Columns)
                dc.ReadOnly = false;

            wrdt = new WeakReference(dt);
            SqlQuery = BuildSqlQuery(ColumnList, Condition);
        }

        public EntityDataTable(Type EntityType, DataPersistance dp,
            string Condition, string OrderCondition, out DataTable dt)
        {
            this.EntityType = EntityType;
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp ?? BaseFramework.DefaultDataPersistance;

            dt = this.dp.OpenDataTable(null,
                EntityType, Condition, OrderCondition);
            foreach (DataColumn dc in dt.Columns)
                dc.ReadOnly = false;

            wrdt = new WeakReference(dt);
            SqlQuery = BuildSqlQuery(td.BuildSqlFieldSelect(dp, 
                true, true), Condition);
        }

        private string BuildPKWhere(object Entity)
        {
            string RetVal = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
                RetVal = string.Concat(RetVal, " AND ",
                    dp.FormatSqlObject(fld.FieldName), "=",
                    dp.FormatSqlValue(fld.GetValue(Entity), 
                    fld.DataType));

            return RetVal.Substring(5);
        }
        private string BuildSqlQuery(string ColumnList, string Condition)
        {
            string SqlQuery;

            if (ColumnList.Length > 0)
                return string.Concat("SELECT ",
                    td.BuildSqlFieldSelect(dp, ColumnList),
                    " FROM ", td._TableName, " WHERE ", 
                    Condition.Length > 0 ? Condition : "1=1");
            else
            {
                int i = Condition.LastIndexOf("WHERE", 
                    StringComparison.OrdinalIgnoreCase);
                if (i < 0)
                    return string.Concat(Condition, " WHERE 1=1");
                else
                {
                    int l = Condition.Length;
                    int sumx = 0;
                    for (int j = i + 5; j < l; j++)
                    {
                        char x = Condition[j];
                        if (x == '(') sumx++;
                        else if (x == ')') sumx--;
                    }
                    return sumx == 0 ? Condition : string.Concat(
                        Condition, " WHERE 1=1");
                }
            }
        }

        internal override void AddEntity(DataPersistance dp, object NewEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;

            string strPK = BuildPKWhere(NewEntity);
            string Query = strPK.Length > 0 ? string.Concat(SqlQuery,
                " AND (", strPK, ")") : SqlQuery;

            IDataReader rdr = dp.ExecuteReader(Query);
            try
            {
                if (rdr.Read()) // Kalo PK data baru valid, tambahkan
                {
                    object[] Values = new object[rdr.FieldCount];

                    rdr.GetValues(Values);
                    ((DataTable)wrdt.Target).Rows.Add(Values);
                }
            }
            finally
            {
                rdr.Close();
            }
        }
        internal override void EditEntity(DataPersistance dp, object OldEntity, object NewEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;

            string strPK = BuildPKWhere(NewEntity);
            string Query = strPK.Length > 0 ? string.Concat(SqlQuery,
                " AND (", strPK, ")") : SqlQuery;

            DataTable dt = (DataTable)wrdt.Target;
            IDataReader rdr = dp.ExecuteReader(Query);
            try
            {
                if (rdr.Read())     //Kalo PK Data Baru Valid
                {
                    object[] Values = new object[rdr.FieldCount];
                    rdr.GetValues(Values);

                    if (OldEntity == null)  //Data baru tidak ada, tambahkan
                        dt.Rows.Add(Values);
                    else
                    {
                        // kalo data lama ada, edit
                        strPK = BuildPKWhere(OldEntity);
                        DataRow[] drs = dt.Select(BuildPKWhere(OldEntity));
                        if (drs.Length > 0)
                        {
                            DataRow dr = drs[0];
                            for (int i = 0; i < Values.Length; i++)
                                dr[i] = Values[i];
                        }
                        else  // Data baru tidak ada, tambahkan
                            dt.Rows.Add(Values);
                    }
                }
                else   // Data Baru tidak valid
                {
                    // Kalo data lama ada, hapus
                    DataRow[] drs = dt.Select(BuildPKWhere(OldEntity));
                    if (drs.Length > 0)
                        dt.Rows.Remove(drs[0]);
                }
            }
            finally
            {
                rdr.Close();
            }
        }
        internal override void DeleteEntity(DataPersistance dp, object DeletedEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;

            DataTable dt = (DataTable)wrdt.Target;
            DataRow[] drs = dt.Select(BuildPKWhere(DeletedEntity));

            if (drs.Length > 0)
                dt.Rows.Remove(drs[0]);
        }
    }

    internal class EntityBindingList<TEntity> : EntityList
        where TEntity : class, new()
    {
        private TableDef td;
        private DataPersistance dp;

        private Type EntityType;
        private string SqlQuery;
        private bool IsLoadEntity;
        private FieldDef[] FieldList;

        public EntityBindingList(DataPersistance dp,
            string Condition, string OrderCondition, 
            bool CallLoadRule, out IList<TEntity> ListEntity)
        {
            EntityType = typeof(TEntity);
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp ?? BaseFramework.DefaultDataPersistance;

            ListEntity = this.dp.LoadEntities<TEntity>(
                null, Condition, OrderCondition, CallLoadRule);
            wrdt = new WeakReference(ListEntity);
            SqlQuery = BuildSqlQuery(Condition);
            IsLoadEntity = true;
        }

        public EntityBindingList(DataPersistance dp,
            string ColumnList, string Condition,
            string OrderCondition, out IList<TEntity> ListEntity)
        {
            EntityType = typeof(TEntity);
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp ?? BaseFramework.DefaultDataPersistance;

            ListEntity = this.dp.FastLoadEntities<TEntity>(null,
                ColumnList, Condition, OrderCondition);
            wrdt = new WeakReference(ListEntity);
            SqlQuery = BuildSqlQuery(Condition);
            IsLoadEntity = false;
            string[] Cols = ColumnList.Split(',');
            FieldList = new FieldDef[Cols.Length];
            for (int i = 0; i < Cols.Length; i++)
                FieldList[i] = td.GetFieldDef(Cols[i].Trim());
        }

        private string BuildPKWhere(object Entity)
        {
            string RetVal = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
                RetVal = string.Concat(RetVal, " AND ",
                    dp.FormatSqlObject(fld.FieldName), "=",
                    dp.FormatSqlValue(fld.GetValue(Entity), fld.DataType));

            return RetVal.Substring(5);
        }
        private string BuildSqlQuery(string Condition)
        {
            return Condition.Length > 0 ? string.Concat(
                "SELECT 1 FROM ", td._TableName, " WHERE ", Condition) :
                string.Concat("SELECT 1 FROM ", td._TableName,
                "WHERE 1=1");
        }

        internal override void AddEntity(DataPersistance dp, object NewEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;

            string strPK = BuildPKWhere(NewEntity);
            string Query = strPK.Length > 0 ? string.Concat(SqlQuery, 
                " AND (", strPK, ")") : SqlQuery;

            if (dp.Find.IsExists(Query))
            {
                if (IsLoadEntity)
                    ((List<TEntity>)wrdt.Target).Add(
                        (TEntity)MetaData.Clone(NewEntity, true));
                else
                    ((List<TEntity>)wrdt.Target).Add(
                        (TEntity)MetaData.FastClone(NewEntity));
            }
        }
        internal override void EditEntity(DataPersistance dp, object OldEntity, object NewEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;

            string strPK = BuildPKWhere(OldEntity);
            string Query = strPK.Length > 0 ? string.Concat(SqlQuery,
                " AND (", strPK, ")") : SqlQuery;

            if (dp.Find.IsExists(Query))
            {
                List<TEntity> ListEntity = (List<TEntity>)wrdt.Target;

                if (!IsLoadEntity)
                {
                    foreach (TEntity Entity in ListEntity)
                    {
                        bool IsFound = true;
                        foreach (FieldDef fld in FieldList)
                            if (fld.GetValue(OldEntity) !=
                                fld.GetValue(Entity))
                            {
                                IsFound = false;
                                break;
                            }
                        if (IsFound)
                        {
                            MetaData.FastClone(Entity, NewEntity);
                            return;
                        }
                    }
                    ListEntity.Add(
                        (TEntity)MetaData.FastClone(NewEntity));
                }
                else
                {
                    foreach (TEntity Entity in ListEntity)
                    {
                        bool IsFound = true;
                        foreach (FieldDef fld in td.KeyFields.Values)
                            if (fld.GetValue(OldEntity) !=
                                fld.GetValue(Entity))
                            {
                                IsFound = false;
                                break;
                            }
                        if (IsFound)
                        {
                            MetaData.Clone(Entity, NewEntity, true);
                            return;
                        }
                    }
                    ListEntity.Add(
                        (TEntity)MetaData.Clone(NewEntity, true));
                }
            }
        }
        internal override void DeleteEntity(DataPersistance dp, object DeletedEntity)
        {
            if (!object.ReferenceEquals(dp, this.dp)) return;
            
            List<TEntity> ListEntity = (List<TEntity>)wrdt.Target;
            int IndexPos = -1;

            if (!IsLoadEntity)
                foreach (TEntity Entity in ListEntity)
                {
                    IndexPos++;
                    bool IsFound = true;
                    foreach (FieldDef fld in FieldList)
                        if (fld.GetValue(DeletedEntity) !=
                            fld.GetValue(Entity))
                        {
                            IsFound = false;
                            break;
                        }
                    if (IsFound) break;
                }
            else
                foreach (TEntity Entity in ListEntity)
                {
                    IndexPos++;
                    bool IsFound = true;
                    foreach (FieldDef fld in FieldList)
                        if (fld.GetValue(DeletedEntity) !=
                            fld.GetValue(Entity))
                        {
                            IsFound = false;
                            break;
                        }
                    if (IsFound) break;
                }
            if (IndexPos>=0) ListEntity.RemoveAt(IndexPos);
        }
    }
}
