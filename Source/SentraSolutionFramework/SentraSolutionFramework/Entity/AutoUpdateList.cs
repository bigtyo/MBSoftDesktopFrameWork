using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Persistance;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework.Entity
{
    public enum EntityListChangedType
    {
        EntityAdded,
        EntityEdited,
        EntityDeleted
    }

    public delegate void OnListRefresh();
    public delegate void OnEntityListChanged(
    EntityListChangedType ChangedType, BusinessEntity NewEntity);

    // Fitur Kurang : Auto Update View...

    //[DebuggerNonUserCode]
    public static class AutoUpdateList
    {
        internal static Dictionary<Type, SortedList<int, WeakReference>> ListAU = 
            new Dictionary<Type, SortedList<int, WeakReference>>();

        private static int LastKey = 0;

        internal static int AddList(Type EntityType, 
            IAutoUpdateList List)
        {
            SortedList<int, WeakReference> _List;

            if (!ListAU.TryGetValue(EntityType, out _List))
            {
                _List = new SortedList<int, WeakReference>();
                ListAU.Add(EntityType, _List);
            }
            _List.Add(LastKey, new WeakReference(List));
            return LastKey++;
        }
        internal static void RemoveList(Type EntityType, int Key)
        {
            SortedList<int, WeakReference> _List;

            if (!ListAU.TryGetValue(EntityType, out _List)) return;
            _List.Remove(Key);
        }

        #region GetAutoUpdateDataTable
        internal static AutoUpdateDataTable GetAutoUpdateDataTable(
            Type EntityType, DataPersistance dp,
            string ColumnList, string Condition, 
            string OrderCondition, bool UseCache)
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(EntityType, out _List))
                    return new AutoUpdateDataTable(EntityType, dp, 
                        ColumnList, Condition, OrderCondition, true);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateDataTable audt = wr.Target as 
                            AutoUpdateDataTable;
                        if (audt != null && audt._RefCtr >= 0 &&
                            audt.IsEqual(dp, ColumnList,
                            Condition, OrderCondition))
                        {
                            audt._RefCtr++;
                            return audt;
                        }
                    }
                }
            }
            return new AutoUpdateDataTable(EntityType, dp, ColumnList,
                Condition, OrderCondition, UseCache);
        }
        internal static AutoUpdateDataTable<TEntity> GetAutoUpdateDataTable<TEntity>(
            DataPersistance dp, string ColumnList, string Condition,
            string OrderCondition, bool UseCache) where TEntity : BusinessEntity
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(typeof(TEntity), out _List))
                    return new AutoUpdateDataTable<TEntity>(dp, 
                        ColumnList, Condition, OrderCondition, true);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateDataTable<TEntity> audt = wr.Target 
                            as AutoUpdateDataTable<TEntity>;
                        if (audt != null && audt._RefCtr >= 0 &&
                            audt.IsEqual(dp, ColumnList,
                            Condition, OrderCondition))
                        {
                            audt._RefCtr++;
                            return audt;
                        }
                    }
                }
            }
            return new AutoUpdateDataTable<TEntity>(dp, ColumnList,
                Condition, OrderCondition, UseCache);
        }

        internal static AutoUpdateDataTable GetAutoUpdateDataTable(
            Type EntityType, DataPersistance dp, string SqlSelect, 
            string SqlOrder, bool UseCache)
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(EntityType, out _List))
                    return new AutoUpdateDataTable(EntityType, dp,
                        SqlSelect, SqlOrder, true);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateDataTable audt = wr.Target as 
                            AutoUpdateDataTable;
                        if (audt != null && audt._RefCtr >= 0 &&
                            audt.IsEqual(dp, SqlSelect, SqlOrder))
                        {
                            audt._RefCtr++;
                            return audt;
                        }
                    }
                }
            }
            return new AutoUpdateDataTable(EntityType, dp, SqlSelect,
                SqlOrder, UseCache);
        }
        internal static AutoUpdateDataTable<TEntity> 
            GetAutoUpdateDataTable<TEntity>(
            DataPersistance dp, string SqlSelect, string SqlOrder, bool UseCache) 
            where TEntity : BusinessEntity
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(typeof(TEntity), out _List))
                    return new AutoUpdateDataTable<TEntity>(dp, 
                        SqlSelect, SqlOrder, true);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateDataTable<TEntity> audt = wr.Target 
                            as AutoUpdateDataTable<TEntity>;
                        if (audt != null && audt._RefCtr >= 0 &&
                            audt.IsEqual(dp, SqlSelect, SqlOrder))
                        {
                            audt._RefCtr++;
                            return audt;
                        }
                    }
                }
            }
            return new AutoUpdateDataTable<TEntity>(dp, SqlSelect, 
                SqlOrder, UseCache);
        }
        #endregion

        #region GetAutoUpdateBindingList
        internal static AutoUpdateBindingList<TEntity> 
            GetAutoUpdateBindingList<TEntity>(
            DataPersistance dp, string ColumnList, string Condition,
            string OrderCondition, bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(typeof(TEntity), out _List))
                    return new AutoUpdateBindingList<TEntity>(dp,
                        ColumnList, Condition, OrderCondition, true, 
                        AddEmptyRow);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateBindingList<TEntity> aubl = wr.Target 
                            as AutoUpdateBindingList<TEntity>;
                        if (aubl != null && aubl._RefCtr >= 0 &&
                            aubl.IsEqual(dp, ColumnList, Condition,
                            OrderCondition, AddEmptyRow))
                        {
                            aubl._RefCtr++;
                            return aubl;
                        }
                    }
                }
            }
            return new AutoUpdateBindingList<TEntity>(dp,
                ColumnList, Condition, OrderCondition, UseCache, 
                AddEmptyRow);
        }
        internal static AutoUpdateBindingList<TEntity> 
            GetAutoUpdateBindingList<TEntity>(
            DataPersistance dp, string Condition,
            string OrderCondition, bool CallLoadRule, 
            bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(typeof(TEntity), out _List))
                    return new AutoUpdateBindingList<TEntity>(dp,
                        Condition, OrderCondition, CallLoadRule, true, 
                        AddEmptyRow);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateBindingList<TEntity> aubl = wr.Target 
                            as AutoUpdateBindingList<TEntity>;
                        if (aubl != null && aubl._RefCtr >= 0 &&
                            aubl.IsEqual(dp, Condition, OrderCondition,
                            CallLoadRule, AddEmptyRow))
                        {
                            aubl._RefCtr++;
                            return aubl;
                        }
                    }
                }
            }
            return new AutoUpdateBindingList<TEntity>(dp,
                Condition, OrderCondition, CallLoadRule, UseCache, 
                AddEmptyRow);
        }
        internal static AutoUpdateBindingList<TEntity> 
            GetAutoUpdateBindingList<TEntity>(
            DataPersistance dp, string SqlSelect, string OrderCondition, 
            bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            if (UseCache)
            {
                SortedList<int, WeakReference> _List;

                if (!ListAU.TryGetValue(typeof(TEntity), out _List))
                    return new AutoUpdateBindingList<TEntity>(dp,
                        SqlSelect, OrderCondition, true, AddEmptyRow);

                int i = 0;
                while (i < _List.Count)
                {
                    WeakReference wr = _List.Values[i];
                    if (!wr.IsAlive)
                        _List.RemoveAt(i);
                    else
                    {
                        i++;
                        AutoUpdateBindingList<TEntity> aubl = wr.Target 
                            as AutoUpdateBindingList<TEntity>;
                        if (aubl != null && aubl._RefCtr >= 0 &&
                            aubl.IsEqual(dp, SqlSelect, AddEmptyRow))
                        {
                            aubl._RefCtr++;
                            return aubl;
                        }
                    }
                }
            }
            return new AutoUpdateBindingList<TEntity>(dp, SqlSelect, 
                OrderCondition, UseCache, AddEmptyRow);
        }
        #endregion

        #region EntityAdded, Edited, Deleted, or Refreshed
        public static void EntityAdded(DataPersistance dp, 
            Type EntityType, BusinessEntity NewEntity)
        {
            if (dp.Trx != null)
            {
                dp.AULEntityAdded(EntityType, NewEntity);
                return;
            }

            SortedList<int, WeakReference> ListEl;
            if (ListAU.TryGetValue(EntityType, out ListEl))
            {

                int i = 0;
                while (i < ListEl.Count)
                {
                    WeakReference wr = ListEl.Values[i];
                    if (!wr.IsAlive)
                        ListEl.RemoveAt(i);
                    else
                    {
                        ((IAutoUpdateList)wr.Target)
                            .AddEntity(dp, NewEntity);
                        i++;
                    }
                }
            }

            //!!!!if (MetaData.ViewDependents.TryGetValue(ent
        }
        public static void EntityEdited(DataPersistance dp, 
            Type EntityType, BusinessEntity EditedEntity)
        {
            if (dp.Trx != null)
            {
                dp.AULEntityEdited(EntityType, EditedEntity);
                return;
            }

            SortedList<int, WeakReference> ListEl;
            if (!ListAU.TryGetValue(EntityType, out ListEl)) return;

            int i = 0;
            while (i < ListEl.Count)
            {
                WeakReference wr = ListEl.Values[i];
                if (!wr.IsAlive)
                    ListEl.RemoveAt(i);
                else
                {
                    i++;
                    ((IAutoUpdateList)wr.Target).EditEntity(dp,
                        EditedEntity);
                }
            }
        }
        public static void EntityDeleted(DataPersistance dp, 
            Type EntityType, BusinessEntity DeletedEntity)
        {
            if (dp.Trx != null)
            {
                dp.AULEntityDeleted(EntityType, DeletedEntity);
                return;
            }

            SortedList<int, WeakReference> ListEl;
            if (!ListAU.TryGetValue(EntityType, out ListEl)) return;

            int i = 0;
            while (i < ListEl.Count)
            {
                WeakReference wr = ListEl.Values[i];
                if (!wr.IsAlive)
                    ListEl.RemoveAt(i);
                else
                {
                    i++;
                    ((IAutoUpdateList)wr.Target)
                        .DeleteEntity(dp, DeletedEntity);
                }
            }
        }
        public static void EntityRefreshed(DataPersistance dp,
            Type EntityType)
        {
            if (dp.Trx != null)
            {
                dp.AULEntityRefreshed(EntityType);
                return;
            }

            SortedList<int, WeakReference> _List;

            if (!ListAU.TryGetValue(EntityType, out _List))
                return;
            Ulang:
            try
            {
                foreach (WeakReference wr in _List.Values)
                    if (wr.IsAlive)
                        ((IAutoUpdateList)wr.Target).Refresh();
            }
            catch
            {
                goto Ulang;
            }
        }
        #endregion

        public static void ClearAllCache()
        {
            ListAU.Clear();
        }

        internal static void ThrowError(Exception ex)
        {
            if (BaseUtility.IsDebugMode) throw ex;
        }
    }

    public interface IAutoUpdateList
    {
        void AddEntity(DataPersistance dp, BusinessEntity NewEntity);
        void EditEntity(DataPersistance dp, BusinessEntity NewEntity);
        void DeleteEntity(DataPersistance dp, BusinessEntity DeletedEntity);
        OnListRefresh OnListRefresh { get; }
        int RefCtr { get; set; }
        TableDef Td { get; }
        IList GetList();
        void Refresh();

        // Digunakan oleh ActivityList:
        DataPersistance Dp { get; }
        string SqlQuery { get; }
        string SqlOrder { get; }
    }

    //[DebuggerNonUserCode]
    public class AutoUpdateDataTable : DataTable, IAutoUpdateList
    {
        private TableDef td;

        private DataPersistance _dp;
        private DataPersistance dp
        {
            get { return _dp ?? BaseFramework.GetDefaultDp(EntityType.Assembly); }
            set { _dp = value; }
        }

        private Type EntityType;
        private string SqlQuery;
        private bool WhereExist;
        private string OrderCondition;
        private int Key;
        internal int _RefCtr;
        private DateTime _LastQuery;

        public event OnEntityListChanged OnEntityListChanged;
        public event OnListRefresh OnListRefresh;

        public DateTime LastQuery { get { return _LastQuery; } }

        internal bool IsEqual(DataPersistance dp, string SqlQuery, string OrderBy)
        {
            return this.dp.Equals(dp ??
                BaseFramework.GetDefaultDp(EntityType.Assembly)) &&
                this.SqlQuery.Equals(SqlQuery) &&
                this.OrderCondition.Equals(OrderBy);
        }
        internal bool IsEqual(DataPersistance dp, string ColumnList,
            string Condition, string OrderCondition)
        {
            return this.dp.Equals(dp ?? BaseFramework.GetDefaultDp(EntityType.Assembly)) &&
                this.SqlQuery.Equals(BuildSqlQuery(
                ColumnList, Condition)) &&
                this.OrderCondition.Equals(OrderCondition);
        }

        private string BuildPKWhere(BusinessEntity Entity)
        {
            string RetVal = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
                RetVal = string.Concat(RetVal, " AND ",
                    fld._FieldName, "=",
                    dp.FormatSqlValue(fld.GetValue(Entity),
                    fld._DataTypeAtr._DataType));

            return RetVal.Substring(5);
        }

        private string FormatAccessSqlValue(DataPersistance dp, 
            object Value, DataType DataType)
        {
            if (Value == null) return dp.FormatSqlValue(Value);
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
                    return dp.FormatSqlValue(Value, DataType);
            }
        }

        private string BuildPKWhereSelect(BusinessEntity Entity)
        {
            string RetVal = string.Empty;

            foreach (FieldDef fld in td.KeyFields.Values)
                if (Columns.Contains(fld._FieldName))
                    RetVal = string.Concat(RetVal, " AND ",
                        fld._FieldName, "=",
                        FormatAccessSqlValue(dp, fld.GetValue(Entity),
                        fld._DataTypeAtr._DataType));

            return RetVal.Length > 0 ? RetVal.Substring(5) : 
                string.Empty;
        }

        private string BuildSqlQuery(string ColumnList, string Condition)
        {
            if (ColumnList.Length > 0)
            {
                string strTemp;

                if (Condition.Length > 0)
                {
                    strTemp = " WHERE " + Condition;
                    WhereExist = true;
                }
                else
                {
                    strTemp = string.Empty;
                    WhereExist = false;
                }

                string strTemp2 = td.GetSqlFieldSelect(ColumnList);
                if (strTemp2.Contains(" AS "))
                    return string.Concat("SELECT * FROM (SELECT ", strTemp2,
                        " FROM ", td._TableName, ") AS X", strTemp);
                else
                    return string.Concat("SELECT ",
                        strTemp2,
                        " FROM ", td._TableName, strTemp);
            }
            else
            {
                WhereExist = IsWhereExist(Condition);
                return Condition;
            }
        }

        private bool IsWhereExist(string SqlQuery)
        {
            int i = SqlQuery.LastIndexOf("WHERE", 
                StringComparison.OrdinalIgnoreCase);
            if (i > 0)
            {
                int NumSign = 0;
                for (int j = i + 5; j < SqlQuery.Length; j++)
                    switch (SqlQuery[j])
                    {
                        case '(':
                            NumSign++;
                            break;
                        case ')':
                            NumSign--;
                            break;
                    }
                return NumSign == 0;
            }
            return false;
        }

        internal AutoUpdateDataTable(Type EntityType, DataPersistance dp,
            string ColumnList, string Condition, string OrderCondition, bool UseCache)
        {
            this.EntityType = EntityType;
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp;
            Key = AutoUpdateList.AddList(EntityType, this);

            SqlQuery = BuildSqlQuery(ColumnList, Condition);
            this.OrderCondition = OrderCondition;

            dp.ValidateTableDef(td);
            Refresh();
            _RefCtr = UseCache ? 1 : -1;
        }
        internal AutoUpdateDataTable(Type EntityType, DataPersistance dp,
            string SqlSelect, string SqlOrder, bool UseCache) : this(EntityType, 
            dp, string.Empty, SqlSelect, SqlOrder, UseCache) { }

        public void Refresh()
        {
            if (dp.Trx != null) dp.AulRefreshed(this);
            string SqlSelect = OrderCondition.Length > 0 ?
                string.Concat(SqlQuery, " ORDER BY ", OrderCondition) : SqlQuery;
            dp.OpenDataTable(this, SqlSelect);
            _LastQuery = dp.GetDbDateTime();
            foreach (DataColumn dc in Columns)
                dc.ReadOnly = false;
            if (_RefCtr == -1)
            {
                if (OnListRefresh != null) OnListRefresh();
            }
            else
            {
                SortedList<int, WeakReference> ListEl;
                if (!AutoUpdateList.ListAU.TryGetValue(EntityType, out ListEl)) return;

                int i = 0;
                while (i < ListEl.Count)
                {
                    WeakReference wr = ListEl.Values[i];
                    if (!wr.IsAlive)
                        ListEl.RemoveAt(i);
                    else
                    {
                        i++;
                        OnListRefresh olr = ((IAutoUpdateList)wr
                            .Target).OnListRefresh;
                        if (olr != null)
                            olr();
                    }
                }
            }
        }
        public void Refresh(string SqlSelect)
        {
            SqlQuery = BuildSqlQuery(string.Empty, SqlSelect);
            OrderCondition = string.Empty;
            Refresh();
        }
        public void Refresh(string ColumnList, string Condition,
            string OrderCondition)
        {
            SqlQuery = BuildSqlQuery(ColumnList, Condition);
            this.OrderCondition = OrderCondition;
            Refresh();
        }

        #region IAutoUpdateList Members
        void IAutoUpdateList.AddEntity(DataPersistance dp, BusinessEntity NewEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            string strPK = BuildPKWhere(NewEntity);
            string Query;

            if (strPK.Length == 0)
                Query = SqlQuery;
            else if (WhereExist)
                Query = string.Concat(SqlQuery, " AND (", strPK, ") ");
            else
                Query = string.Concat(SqlQuery, " WHERE (", strPK, ") ");

            IDataReader rdr = dp.ExecuteReader(Query);
            try
            {
                if (rdr.Read())     //Kalo PK Data Baru Valid
                {
                    object[] Values = new object[rdr.FieldCount];
                    rdr.GetValues(Values);

                    ParentEntity px = NewEntity as ParentEntity;

                    // kalo data lama ada, edit
                    DataRow[] drs = Select(BuildPKWhereSelect(NewEntity));
                    if (drs.Length == 0) // Data baru tidak ada, tambahkan
                    {
                        Rows.Add(Values);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(
                                EntityListChangedType.EntityAdded,
                                NewEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                AutoUpdateList.ThrowError(ex);
            }
            finally
            {
                rdr.Close();
            }
        }
        void IAutoUpdateList.EditEntity(DataPersistance dp, BusinessEntity NewEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            string strPK = BuildPKWhere(NewEntity);
            string Query;

            if (strPK.Length == 0)
                Query = SqlQuery;
            else if (WhereExist)
                Query = string.Concat(SqlQuery, " AND (", strPK, ") ");
            else
                Query = string.Concat(SqlQuery, " WHERE (", strPK, ") ");

            IDataReader rdr = dp.ExecuteReader(Query);
            try
            {
                if (rdr.Read())     //Kalo PK Data Baru Valid
                {
                    object[] Values = new object[rdr.FieldCount];
                    rdr.GetValues(Values);

                    ParentEntity px = NewEntity as ParentEntity;

                    if (px == null || px._Original == null)
                    {
                        // kalo data lama ada, edit
                        DataRow[] drs = Select(BuildPKWhereSelect(NewEntity));
                        if (drs.Length > 0)
                        {
                            DataRow dr = drs[0];
                            for (int i = 0; i < Values.Length; i++)
                                dr[i] = Values[i];
                        }
                        else  // Data baru tidak ada, tambahkan
                            Rows.Add(Values);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(
                                EntityListChangedType.EntityEdited,
                                NewEntity);
                    }
                    else
                    {
                        // kalo data lama ada, edit
                        DataRow[] drs = Select(BuildPKWhereSelect(px._Original));
                        if (drs.Length > 0)
                        {
                            DataRow dr = drs[0];
                            for (int i = 0; i < Values.Length; i++)
                                dr[i] = Values[i];
                        }
                        else  // Data baru tidak ada, tambahkan
                            Rows.Add(Values);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(EntityListChangedType.EntityEdited,
                                NewEntity);
                    }
                }
                else   // Data Baru tidak valid
                {
                    // Kalo data lama ada, hapus
                    ParentEntity px = NewEntity as ParentEntity;
                    if (px != null && px._Original != null)
                    {
                        DataRow[] drs = Select(BuildPKWhereSelect(px._Original));
                        if (drs.Length > 0)
                            Rows.Remove(drs[0]);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                AutoUpdateList.ThrowError(ex);
            }
            finally
            {
                rdr.Close();
            }
        }
        void IAutoUpdateList.DeleteEntity(DataPersistance dp, BusinessEntity DeletedEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            try
            {
                DataRow[] drs = Select(BuildPKWhereSelect(DeletedEntity));
                if (drs.Length > 0)
                {
                    Rows.Remove(drs[0]);
                    if (OnEntityListChanged != null)
                        OnEntityListChanged(EntityListChangedType.EntityDeleted, DeletedEntity);
                }
            }
            catch (Exception ex)
            {
                AutoUpdateList.ThrowError(ex);
            }
        }
        OnListRefresh IAutoUpdateList.OnListRefresh
        {
            get { return OnListRefresh; }
        }
        int IAutoUpdateList.RefCtr
        {
            get { return _RefCtr; }
            set { _RefCtr = value; }
        }
        TableDef IAutoUpdateList.Td { get { return td; } }
        IList IAutoUpdateList.GetList() { return ((IListSource)this).GetList(); }

        DataPersistance IAutoUpdateList.Dp { get { return _dp; } }
        string IAutoUpdateList.SqlQuery
        {
            get
            {
                return OrderCondition.Length > 0 ?
                    string.Concat(SqlQuery, " ORDER BY ", 
                    OrderCondition) : SqlQuery;
            }
        }
        string IAutoUpdateList.SqlOrder
        {
            get { return OrderCondition; }
        }
        #endregion

        public void Close()
        {
            if (_RefCtr == -1)
                AutoUpdateList.RemoveList(EntityType, Key);
            else if (_RefCtr > 0) 
                _RefCtr--;
        }
    }

    //[DebuggerNonUserCode]
    public class AutoUpdateDataTable<TEntity> : AutoUpdateDataTable
        where TEntity : BaseEntity
    {
        internal AutoUpdateDataTable(DataPersistance dp, string SqlSelect, 
            string SqlOrder, bool UseCache) 
            : base(typeof(TEntity), dp, string.Empty, SqlSelect, 
            SqlOrder, UseCache) { }
        internal AutoUpdateDataTable(DataPersistance dp,
            string ColumnList, string Condition,
            string OrderCondition, bool UseCache)
            : base(typeof(TEntity), dp, ColumnList,
            Condition, OrderCondition, UseCache) { }
    }

    //[DebuggerNonUserCode]
    public class AutoUpdateBindingList<TEntity> :
        BindingListEx<TEntity>, IAutoUpdateList 
        where TEntity : BaseEntity, new()
    {
        private TableDef td;

        private DataPersistance _dp;
        private DataPersistance dp
        {
            get { return _dp ?? BaseFramework.GetDefaultDp(typeof(TEntity).Assembly); }
            set { _dp = value; }
        }

        private Type EntityType;
        private int Key;
        private bool IsFastLoad;
        private string ColumnList;
        private string Condition;
        private string OrderCondition;
        private bool CallLoadRule;
        internal int _RefCtr;
        private DateTime _LastQuery;
        private bool IsDirectSelect;
        internal bool AddEmptyRow;

        public event OnEntityListChanged OnEntityListChanged;
        public event OnListRefresh OnListRefresh;

        public DateTime LastQuery { get { return _LastQuery; } }

        private string BuildPKWhere(BusinessEntity Entity)
        {
            string RetVal = string.Empty;
            foreach (FieldDef fld in td.KeyFields.Values)
                RetVal = string.Concat(RetVal, " AND ",
                    fld._FieldName, "=",
                    dp.FormatSqlValue(fld.GetValue(Entity),
                    fld.DataType));

            return RetVal.Substring(5);
        }

        internal AutoUpdateBindingList(DataPersistance dp,
            string ColumnList, string Condition,
            string OrderCondition, bool UseCache, bool AddEmptyRow)
        {
            EntityType = BaseFactory.GetObjType(typeof(TEntity));

            td = MetaData.GetTableDef(EntityType);

            this.dp = dp;
            Key = AutoUpdateList.AddList(EntityType, this);

            this.dp.ListFastLoadEntities<TEntity>(this,
                ColumnList, Condition, OrderCondition, AddEmptyRow);
            IsFastLoad = true;
            IsDirectSelect = false;
            this.ColumnList = ColumnList;
            this.Condition = Condition;
            this.OrderCondition = OrderCondition;
            _RefCtr = UseCache ? 1 : -1;
            this.AddEmptyRow = AddEmptyRow;
            this.AllowEdit = false;
            this.AllowNew = false;
            this.AllowRemove = false;
        }

        internal AutoUpdateBindingList(DataPersistance dp,
            string SqlSelect, string OrderCondition, bool UseCache, 
            bool AddEmptyRow)
        {
            EntityType = BaseFactory.GetObjType(typeof(TEntity));
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp;
            Key = AutoUpdateList.AddList(EntityType, this);

            this.dp.ListFastLoadEntitiesUsingSqlSelect<TEntity>(this, SqlSelect, 
                OrderCondition, AddEmptyRow);
            IsFastLoad = true;
            IsDirectSelect = true;
            Condition = string.Empty;
            this.OrderCondition = OrderCondition;
            CallLoadRule = false;
            this.ColumnList = SqlSelect;
            _RefCtr = UseCache ? 1 : -1;
            this.AddEmptyRow = AddEmptyRow;
            this.AllowEdit = false;
            this.AllowNew = false;
            this.AllowRemove = false;
        }

        internal AutoUpdateBindingList(DataPersistance dp,
            string Condition, string OrderCondition,
            bool CallLoadRule, bool UseCache, bool AddEmptyRow)
        {
            EntityType = BaseFactory.GetObjType(typeof(TEntity));
            td = MetaData.GetTableDef(EntityType);

            this.dp = dp;
            Key = AutoUpdateList.AddList(EntityType, this);

            this.dp.ListLoadEntities<TEntity>(this, Condition,
                OrderCondition, CallLoadRule, AddEmptyRow);
            IsFastLoad = false;
            IsDirectSelect = false;
            ColumnList = string.Empty;
            this.Condition = Condition;
            this.OrderCondition = OrderCondition;
            this.CallLoadRule = CallLoadRule;
            _RefCtr = UseCache ? 1 : -1;
            this.AddEmptyRow = AddEmptyRow;
            this.AllowEdit = false;
            this.AllowNew = false;
            this.AllowRemove = false;
        }

        #region Refresh
        /// <summary>
        /// Refresh according to the last inititialization used
        /// </summary>
        public void Refresh()
        {
            if (dp.Trx != null) dp.AulRefreshed(this);
            try
            {
                if (IsDirectSelect)
                    dp.ListFastLoadEntitiesUsingSqlSelect<TEntity>(this,
                        ColumnList, OrderCondition, AddEmptyRow);
                else if (IsFastLoad)
                    dp.ListFastLoadEntities<TEntity>(this, ColumnList,
                        Condition, OrderCondition, AddEmptyRow);
                else
                    dp.ListLoadEntities<TEntity>(this, Condition,
                        OrderCondition, CallLoadRule, AddEmptyRow);
                _LastQuery = dp.GetDbDateTime();
                if (_RefCtr == -1)
                {
                    if (OnListRefresh != null) OnListRefresh();
                }
                else
                {
                    SortedList<int, WeakReference> ListEl;
                    if (!AutoUpdateList.ListAU.TryGetValue(EntityType,
                        out ListEl)) return;

                    int i = 0;
                    while (i < ListEl.Count)
                    {
                        WeakReference wr = ListEl.Values[i];
                        if (!wr.IsAlive)
                            ListEl.RemoveAt(i);
                        else
                        {
                            i++;
                            OnListRefresh olr = ((IAutoUpdateList)wr
                                .Target).OnListRefresh;
                            if (olr != null)
                                olr();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AutoUpdateList.ThrowError(ex);
            }
        }

        /// <summary>
        /// Refresh with LoadEntities
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="OrderCondition"></param>
        /// <param name="CallLoadRule"></param>
        /// <param name="AddEmptyRow"></param>
        public void Refresh(string Condition, string OrderCondition, 
            bool CallLoadRule, bool AddEmptyRow)
        {
            ColumnList = string.Empty;
            this.Condition = Condition;
            this.OrderCondition = OrderCondition;
            this.CallLoadRule = CallLoadRule;
            IsFastLoad = false;
            IsDirectSelect = false;
            this.AddEmptyRow = AddEmptyRow;
            Refresh();
        }

        /// <summary>
        /// Refresh with FastLoadEntities
        /// </summary>
        /// <param name="ColumnList"></param>
        /// <param name="Condition"></param>
        /// <param name="OrderCondition"></param>
        /// <param name="AddEmptyRow"></param>
        public void Refresh(string ColumnList, string Condition,
            string OrderCondition, bool AddEmptyRow)
        {
            this.ColumnList = ColumnList;
            this.Condition = Condition;
            this.OrderCondition = OrderCondition;
            IsFastLoad = true;
            IsDirectSelect = false;
            this.AddEmptyRow = AddEmptyRow;
            Refresh();
        }

        /// <summary>
        /// Refresh with FastLoadEntites Direct Select
        /// </summary>
        /// <param name="SqlSelect"></param>
        /// <param name="OrderCondition"></param>
        /// <param name="AddEmptyRow"></param>
        public void RefreshUsingSqlSelect(string SqlSelect, 
            string OrderCondition, bool AddEmptyRow)
        {
            this.ColumnList = SqlSelect;
            this.Condition = string.Empty;
            this.OrderCondition = OrderCondition;
            IsFastLoad = true;
            IsDirectSelect = true;
            this.AddEmptyRow = AddEmptyRow;
            Refresh();
        }
        #endregion

        #region IAutoUpdateList Members
        void IAutoUpdateList.AddEntity(DataPersistance dp, BusinessEntity NewEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            try
            {
                ParentEntity px = NewEntity as ParentEntity;
                TEntity FindEntity = NewEntity as TEntity;

                bool DataExist;

                if (!IsDirectSelect)
                {
                    string strPK = BuildPKWhere(NewEntity);
                    if (strPK.Length == 0)
                        throw new ApplicationException(string.Concat(
                            "Entity '", NewEntity.GetType().Name,
                            "' tidak memiliki PrimaryKey"));

                    string mCondition;
                    if (Condition.Length > 0)
                        mCondition = string.Concat(Condition,
                            " AND (", strPK, ")");
                    else
                        mCondition = strPK;

                    DataExist = dp.Find.IsExists(EntityType, mCondition); // PK Baru Valid
                }
                else
                {
                    string strPK = BuildPKWhere(NewEntity);
                    if (strPK.Length == 0)
                        throw new ApplicationException(string.Concat(
                            "Entity '", NewEntity.GetType().Name,
                            "' tidak memiliki PrimaryKey"));
                    string FirstKey = string.Empty; ;

                    foreach (string Key in MetaData.GetTableDef(EntityType).KeyFields.Keys)
                    {
                        FirstKey = Key;
                        break;
                    }

                    DataExist = dp.Find.IsExists(string.Concat("SELECT ",
                        FirstKey, " FROM (",
                        ColumnList, ") AS X WHERE ", strPK));  // PK Baru Valid

                }

                if (DataExist)  // PK Baru Valid
                {
                    #region kalo data lama ada, keluar
                    foreach (TEntity Entity in this)
                    {
                        bool IsEqual = true;

                        foreach (FieldDef fld in td.KeyFields.Values)
                            if (!fld.GetValue(Entity).Equals(
                                fld.GetValue(FindEntity)))
                            {
                                IsEqual = false;
                                break;
                            }
                        if (IsEqual) return;
                    }
                    #endregion

                    // data lama tidak ada tambahkan
                    if (IsFastLoad)
                        Add(MetaData.FastClone(NewEntity) as TEntity);
                    else
                        Add(MetaData.Clone(NewEntity) as TEntity);
                    if (OnEntityListChanged != null)
                        OnEntityListChanged(EntityListChangedType.EntityAdded, NewEntity);
                }
            }
            catch (Exception ex)
            {
               // AutoUpdateList.ThrowError(ex);
            }
        }
        void IAutoUpdateList.EditEntity(DataPersistance dp, BusinessEntity NewEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            try
            {
                TEntity FindEntity;

                ParentEntity px = NewEntity as ParentEntity;
                if (px != null && px._Original != null)
                    FindEntity = px._Original as TEntity;
                else
                    FindEntity = NewEntity as TEntity;

                bool DataExist;

                if (!IsDirectSelect)
                {
                    string strPK = BuildPKWhere(NewEntity);
                    if (strPK.Length == 0)
                        throw new ApplicationException(string.Concat(
                            "Entity '", NewEntity.GetType().Name,
                            "' tidak memiliki PrimaryKey"));

                    string mCondition;
                    if (Condition.Length > 0)
                        mCondition = string.Concat(Condition,
                            " AND (", strPK, ")");
                    else
                        mCondition = strPK;

                    DataExist = dp.Find.IsExists(EntityType, mCondition); // PK Baru Valid
                }
                else
                {
                    string strPK = BuildPKWhere(NewEntity);
                    if (strPK.Length == 0)
                        throw new ApplicationException(string.Concat(
                            "Entity '", NewEntity.GetType().Name,
                            "' tidak memiliki PrimaryKey"));
                    string FirstKey = string.Empty; ;

                    foreach (string Key in MetaData.GetTableDef(EntityType).KeyFields.Keys)
                    {
                        FirstKey = Key;
                        break;
                    }

                    DataExist = dp.Find.IsExists(string.Concat("SELECT ",
                        FirstKey, " FROM (",
                        ColumnList, ") AS X WHERE ", strPK));  // PK Baru Valid

                }

                if (DataExist)  // PK Baru Valid
                {
                    if (IsFastLoad)
                    {
                        #region kalo data lama ada, edit
                        int Index = 0;
                        foreach (TEntity Entity in this)
                        {
                            bool IsEqual = true;

                            foreach (FieldDef fld in td.KeyFields.Values)
                                if (!fld.GetValue(Entity).Equals(
                                    fld.GetValue(FindEntity)))
                                {
                                    IsEqual = false;
                                    break;
                                }
                            if (IsEqual)
                            {
                                MetaData.FastClone(Entity, NewEntity);
                                if (OnEntityListChanged != null)
                                    OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                                OnListChanged(new ListChangedEventArgs(
                                    ListChangedType.ItemChanged, Index));
                                return;
                            }
                            Index++;
                        }
                        #endregion

                        // data lama tidak ada tambahkan
                        Add(MetaData.FastClone(NewEntity) as TEntity);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                    }
                    else
                    {
                        #region kalo data lama ada, edit
                        int Index = 0;
                        foreach (TEntity Entity in this)
                        {
                            bool IsEqual = true;
                            foreach (FieldDef fld in td.KeyFields.Values)
                                if (!fld.GetValue(Entity).Equals(
                                    fld.GetValue(FindEntity)))
                                {
                                    IsEqual = false;
                                    break;
                                }
                            if (IsEqual)
                            {
                                MetaData.Clone(Entity, NewEntity);
                                if (OnEntityListChanged != null)
                                    OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                                OnListChanged(new ListChangedEventArgs(
                                    ListChangedType.ItemChanged, Index));
                                return;
                            }
                            Index++;
                        }
                        #endregion

                        // data lama tidak ada tambahkan
                        Add(MetaData.Clone(NewEntity) as TEntity);
                        if (OnEntityListChanged != null)
                            OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                    }
                }
                else
                {
                    #region Kalo data lama ada, hapus
                    int Ctr = 0;
                    foreach (TEntity Entity in this)
                    {
                        bool IsEqual = true;
                        foreach (FieldDef fld in td.KeyFields.Values)
                            if (!fld.GetValue(Entity).Equals(
                                fld.GetValue(FindEntity)))
                            {
                                IsEqual = false;
                                break;
                            }
                        if (IsEqual)
                        {
                            AllowRemove = true;
                            RemoveItem(Ctr);
                            AllowRemove = false;
                            if (OnEntityListChanged != null)
                                OnEntityListChanged(EntityListChangedType.EntityEdited, NewEntity);
                            return;
                        }
                        Ctr++;
                    }
                    #endregion
                }
            }
            catch //(Exception ex)
            {
                //AutoUpdateList.ThrowError(ex);
            }
        }

        void IAutoUpdateList.DeleteEntity(DataPersistance dp, BusinessEntity DeletedEntity)
        {
            if (!dp.ConnectionString.Equals(this.dp.ConnectionString)) return;

            #region Kalo data lama ada, hapus
            try
            {
                int Index = -1;
                int Ctr = 0;
                foreach (TEntity Entity in this)
                {
                    bool IsEqual = true;

                    foreach (FieldDef fld in td.KeyFields.Values)
                        if (!fld.GetValue(Entity).Equals(
                            fld.GetValue(DeletedEntity)))
                        {
                            IsEqual = false;
                            break;
                        }
                    if (IsEqual)
                    {
                        Index = Ctr;
                        break;
                    }
                    Ctr++;
                }
                if (Index >= 0)
                {
                    AllowRemove = true;
                    RemoveItem(Index);
                    AllowRemove = false;
                }
            }
            catch (Exception ex)
            {
               // AutoUpdateList.ThrowError(ex);
            }
            #endregion
        }
        OnListRefresh IAutoUpdateList.OnListRefresh
        {
            get { return OnListRefresh; }
        }
        int IAutoUpdateList.RefCtr
        {
            get { return _RefCtr; }
            set { _RefCtr = value; }
        }
        TableDef IAutoUpdateList.Td { get { return td; } }
        IList IAutoUpdateList.GetList() { return this; }

        DataPersistance IAutoUpdateList.Dp { get { return _dp; } }
        string IAutoUpdateList.SqlQuery
        {
            get
            {
                if (IsDirectSelect) return ColumnList;
                if (IsFastLoad)
                {
                    if (Condition.Length > 0)
                        return string.Concat("SELECT ",
                            ColumnList, " FROM ", td._TableName, 
                            " WHERE ", Condition);
                    else
                        return string.Concat("SELECT ",
                        ColumnList, " FROM ", td._TableName);
                }
                return td.GetSqlSelect(dp, true, false);
            }
        }
        string IAutoUpdateList.SqlOrder
        {
            get { return OrderCondition; }
        }
        #endregion

        public void Close()
        {
            if (_RefCtr == -1)
                AutoUpdateList.RemoveList(EntityType, Key);
            else if (_RefCtr > 0)
                _RefCtr--;
        }

        internal bool IsEqual(DataPersistance dp, 
            string ColumnList, string Condition, 
            string OrderCondition, bool AddEmptyRow)
        {
            return this.AddEmptyRow == AddEmptyRow &&
                this.dp.Equals(dp ??
                BaseFramework.GetDefaultDp(typeof(TEntity).Assembly)) &&
                this.ColumnList.Equals(ColumnList) &&
                this.Condition.Equals(Condition) &&
                this.OrderCondition.Equals(OrderCondition);
        }

        internal bool IsEqual(DataPersistance dp, string SqlSelect, 
            bool AddEmptyRow)
        {
            return this.AddEmptyRow == AddEmptyRow && this.dp.Equals(
                dp ?? BaseFramework.GetDefaultDp(typeof(TEntity).Assembly)) &&
                this.ColumnList.Equals(SqlSelect);
        }

        internal bool IsEqual(DataPersistance dp,
            string Condition, string OrderCondition, 
            bool CallLoadRule, bool AddEmptyRow)
        {
            return this.AddEmptyRow == AddEmptyRow && 
                this.dp.Equals(dp ??
                BaseFramework.GetDefaultDp(typeof(TEntity).Assembly)) &&
                this.Condition.Equals(Condition) &&
                this.OrderCondition.Equals(OrderCondition) &&
                this.CallLoadRule == CallLoadRule;
        }
    }
}
