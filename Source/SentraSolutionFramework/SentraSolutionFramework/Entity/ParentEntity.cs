using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using SentraSolutionFramework.Persistance;
using System.Collections;
using System.Diagnostics;
using SentraUtility;
using SentraUtility.Expression;
using System.Windows.Forms;

namespace SentraSolutionFramework.Entity
{
    public enum FormMode
    {
        FormError, FormView, FormEdit, FormAddNew
    }

    /// <summary>
    /// Class yang membuat public field bisa dibinding ketika design-time.
    /// Berfungsi sama dgn Atribut: [TypeDescriptionProvider(typeof(EntityTDProvider))]
    /// </summary>
    [TypeDescriptionProvider(typeof(EntityTDProvider))]
    [DebuggerNonUserCode]
    public abstract class PublishFieldEntity { }

    public interface IBaseEntity
    {
        void BeforePrint(Evaluator ev);
        void AfterPrint();

        void DoAfterSendToPrinter();
    }

    /// <summary>
    /// Class Dasar Entity yang berhubungan dengan DataPersistance
    /// </summary>
    [TypeDescriptionProvider(typeof(EntityTDProvider))]
    //[DebuggerNonUserCode]
    public abstract class BaseEntity : INotifyPropertyChanged, 
        IDataErrorInfo, IBaseEntity
    {
        #region INotifyPropertyChanged Members
        private PropertyChangedEventHandler PropertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add { PropertyChanged += value; }
            remove { PropertyChanged -= value; }
        }
        #endregion

        public virtual void SetDefaultValue()
        {
            MetaData.GetTableDef(GetType()).SetDefault(this);
        }

        protected internal bool EntityOnLoad;

        public event OnAction onBeforeDataChanged;
        public virtual void DataChanged()
        {
            UpdateAutoFields();
            if (!EntityOnLoad && PropertyChanged != null)
            {
                if (onBeforeDataChanged != null) onBeforeDataChanged();
                PropertyChanged(this,
                    new PropertyChangedEventArgs(string.Empty));
            }
        }

        protected virtual void UpdateAutoFields() { }

        #region IDataErrorInfo Members
        string IDataErrorInfo.Error
        {
            get { return _ErrorProps.Count > 0 ? "Data Error" : null; }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string ErrStr;

                _ErrorProps.TryGetValue(columnName, out ErrStr);
                return ErrStr;
            }
        }
        #endregion

        #region Error Info
        public bool IsErrorExist()
        {
            if (_ErrorProps.Count > 0) return true;
            foreach (EntityCollDef ecd in MetaData
                .GetTableDef(GetType()).ChildEntities)
                if (ecd.GetCollValue(this)
                    .IsErrorExist()) return true;
            return false;
        }
        public void AddError(string columnName, string ErrMessage)
        {
            _ErrorProps[columnName] = ErrMessage;
        }
        public void ClearError(string columnName)
        {
            _ErrorProps.Remove(columnName);
        }
        internal Dictionary<string, string> _ErrorProps = new Dictionary<string, string>();
        public void ClearError()
        {
            _ErrorProps.Clear();
            foreach (EntityCollDef ecd in MetaData
                .GetTableDef(GetType()).ChildEntities)
                ecd.GetCollValue(this).ClearError();
        }
        public string GetErrorString()
        {
            StringBuilder retVal = new StringBuilder();
            GetErrorString(string.Empty, -1, retVal);
            return retVal.ToString();
        }
        private void GetErrorString(string strSpace, int RowIndex,
            StringBuilder retVal)
        {
            if (!IsErrorExist()) return;

            foreach (string ErrStr in _ErrorProps.Values)
                if (RowIndex == -1)
                    retVal.Append(strSpace).Append("- ")
                        .AppendLine(ErrStr);
                else
                    retVal.Append(strSpace).Append("- ")
                        .Append("Baris ke ").Append(RowIndex)
                                .Append(": ").AppendLine(ErrStr);

            TableDef td = MetaData.GetTableDef(GetType());
            foreach (EntityCollDef ecd in td.ChildEntities)
                if (ecd.GetCollValue(this).IsErrorExist())
                {
                    retVal.Append(strSpace).Append("Data ")
                        .Append(BaseUtility.SplitName(ecd.FieldName))
                        .AppendLine(" :");
                    int i = 0;
                    foreach (BusinessEntity Entity in ecd.GetValue(this))
                    {
                        i++;
                        if (Entity.IsErrorExist())
                            Entity.GetErrorString(strSpace + " ", i, retVal);
                    }
                }
        }
        #endregion

        [Browsable(false)]
        public abstract DataPersistance Dp { get; set; }

        [Browsable(false)]
        protected DataFinder Find
        {
            get { return Dp.Find; }
        }

        protected void ShowMessage(string Message)
        {
            BaseFramework.AutoDialog.ShowMessage(Message, string.Empty);
        }
        protected void ShowMessage(string Message, string Caption)
        {
            BaseFramework.AutoDialog.ShowMessage(Message, Caption);
        }

        protected bool ShowDialog<TForm>(ParentEntity Entity)
        {
            return BaseFramework.AutoDialog.ShowDialog(typeof(TForm), Entity);
        }

        protected List<object[]> ShowSelectTable(
            string ReturnFields, string SqlSelect, string Caption, 
            string HideFields, params FieldParam[] Parameters)
        {
            return BaseFramework.AutoDialog.ShowSelectTable(Dp,
                ReturnFields, SqlSelect, Caption, HideFields, 
                Parameters);
        }

        protected xDialogResult ShowDialogTable(DataPersistance Dp,
            string SqlSelect, string Caption, string Message,
            xMessageBoxButtons Buttons, out bool IsDataExist,
            params FieldParam[] Parameters)
        {
            return BaseFramework.AutoDialog.ShowDialogTable1(Dp,
                SqlSelect, Caption, Message, Buttons, out IsDataExist,
                Parameters);
        }

        protected xDialogResult ShowDialogTable(DataPersistance Dp,
            string SqlSelect, string Caption, string Message,
            xMessageBoxButtons Buttons, out bool IsDataExist, 
            Dictionary<string, string> FormatCols, params FieldParam[] Parameters)
        {
            return BaseFramework.AutoDialog.ShowDialogTable2(Dp,
                SqlSelect, Caption, Message, Buttons, out IsDataExist,
                FormatCols, Parameters);
        }

        #region ChooseEntity
        protected IList ChooseEntity<TEntity>(
            IList<TEntity> ListSource, string Caption, 
            EntityColumnShow ecs) where TEntity : BaseEntity, new()
        {
            return BaseFramework.AutoDialog
                .ChooseEntity(typeof(TEntity), (IList)ListSource, 
                null, null, string.Empty, string.Empty, false, 
                Caption, ecs);
        }

        protected IList ChooseEntity<TEntity>(
            IList<TEntity> ListSource, string Caption,
            EntityColumnShow ecs, IList OldListSelect) where TEntity : BaseEntity, new()
        {
            return BaseFramework.AutoDialog
                .ChooseEntity(typeof(TEntity), (IList)ListSource,
                OldListSelect, null, string.Empty, string.Empty, false,
                Caption, ecs);
        }

        protected IList ChooseEntity<TEntity>(
            string Conditions, string OrderCondition, 
            bool CallLoadRule, string Caption,  
            EntityColumnShow ecs, params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return BaseFramework.AutoDialog
                .ChooseEntity(typeof(TEntity), null, null, Dp, 
                Conditions, OrderCondition, CallLoadRule,
                Caption, ecs, Parameters);
        }

        protected IList ChooseEntity<TEntity>(
            string Conditions, string OrderCondition,
            bool CallLoadRule, string Caption,
            EntityColumnShow ecs, IList OldListSelect,
            params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return BaseFramework.AutoDialog
                .ChooseEntity(typeof(TEntity), null, OldListSelect, Dp,
                Conditions, OrderCondition, CallLoadRule,
                Caption, ecs, Parameters);
        }
        #endregion

        #region ChooseSingleEntity
        protected TEntity ChooseSingleEntity<TEntity>(
            IList<TEntity> ListSource, string Caption, EntityColumnShow ecs)
            where TEntity : BaseEntity, new()
        {
            return (TEntity)BaseFramework.AutoDialog.ChooseSingleEntity(
                typeof(TEntity), (IList)ListSource, 
                null, string.Empty, string.Empty, false,
                Caption, ecs);
        }

        protected TEntity ChooseSingleEntity<TEntity>(
            string Conditions, string OrderCondition, 
            bool CallLoadRule, string Caption, 
            EntityColumnShow ecs, params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return (TEntity)BaseFramework.AutoDialog
                .ChooseSingleEntity(
                typeof(TEntity), null, Dp, Conditions, 
                OrderCondition, CallLoadRule, Caption, ecs, Parameters);
        }
        #endregion

        #region FormatSql
        public string FormatSqlValue(object Value)
        { return Dp.FormatSqlValue(Value); }
        public string FormatSqlValue(object Value, DataType DataType)
        { return Dp.FormatSqlValue(Value, DataType); }
        #endregion

        #region ExecuteNonQuery
        protected int ExecuteNonQuery(string SqlCommand,
            params FieldParam[] Parameters)
        {
            return Dp.ExecuteNonQuery(
                SqlCommand, Parameters);
        }

        protected int ExecuteBatchNonQuery(BatchCommand BatchCommand,
            bool StopIfReturnZero)
        {
            return Dp.ExecuteBatchNonQuery(
                BatchCommand, StopIfReturnZero);
        }
        protected int ExecuteBatchNonQuery(string SqlBatchCommand,
            bool StopIfReturnZero)
        {
            return Dp.ExecuteBatchNonQuery(
                SqlBatchCommand, StopIfReturnZero);
        }
        #endregion

        #region ListLoadEntities
        protected IList<BaseEntity> ListLoadEntities(Type ObjType, IList<BaseEntity> RetList,
            string Conditions, string OrderCondition,
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            return Dp.ListLoadEntities(ObjType, RetList,
                Conditions, OrderCondition, CallLoadRule, Parameters);
        }

        protected IList<TEntity> ListLoadEntities<TEntity>(IList<TEntity> RetList,
            string Conditions, string OrderCondition,
            bool CallLoadRule, params FieldParam[] Parameters)
            where TEntity : BaseEntity
        {
            return Dp.ListLoadEntities<TEntity>(RetList,
                Conditions, OrderCondition, CallLoadRule, Parameters);
        }

        protected IList<BaseEntity> ListLoadEntities(
            Type ObjType, IList<BaseEntity> RetList,
            string Conditions, string OrderCondition,
            bool CallLoadRule, bool AddEmptyRow,
            params FieldParam[] Parameters)
        {
            return Dp.ListLoadEntities(ObjType, RetList,
                Conditions, OrderCondition, CallLoadRule,
                AddEmptyRow, Parameters);
        }

        protected IList<TEntity> ListLoadEntities<TEntity>(
            IList<TEntity> RetList,
            string Conditions, string OrderCondition,
            bool CallLoadRule, bool AddEmptyRow,
            params FieldParam[] Parameters)
            where TEntity : BaseEntity
        {
            return Dp.ListLoadEntities<TEntity>(RetList,
                Conditions, OrderCondition, CallLoadRule,
                AddEmptyRow, Parameters);
        }
        #endregion

        #region LoadEntities
        protected AutoUpdateBindingList<TEntity> LoadEntities<TEntity>(
            string Condition, string OrderCondition,
            bool CallLoadRule, bool UseCache)
            where TEntity : BaseEntity, new()
        {
            return Dp.LoadEntities<TEntity>(
                Condition, OrderCondition, CallLoadRule, UseCache);
        }

        protected AutoUpdateBindingList<TEntity> LoadEntities<TEntity>(
            string Condition, string OrderCondition,
            bool CallLoadRule, bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return Dp.LoadEntities<TEntity>(
                Condition, OrderCondition, CallLoadRule,
                UseCache, AddEmptyRow);
        }
        #endregion

        #region ListFastLoadEntities
        protected IList<BaseEntity> ListFastLoadEntities(Type ObjType,
            IList<BaseEntity> RetList,
            string ColumnList, string Conditions,
            string OrderCondition, params FieldParam[] Parameters)
        {
            return Dp.ListFastLoadEntities(ObjType,
                RetList, ColumnList, Conditions, OrderCondition,
                Parameters);
        }

        protected IList<TEntity> ListFastLoadEntities<TEntity>(
            IList<TEntity> RetList,
            string ColumnList, string Conditions,
            string OrderCondition, params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return Dp.ListFastLoadEntities<TEntity>(
                RetList, ColumnList, Conditions,
                OrderCondition, Parameters);
        }

        protected IList<BaseEntity> ListFastLoadEntities(
            Type ObjType, IList<BaseEntity> RetList,
            string ColumnList, string Conditions,
            string OrderCondition, bool AddEmptyRow, params FieldParam[] Parameters)
        {
            return Dp.ListFastLoadEntities(ObjType,
                RetList, ColumnList, Conditions, OrderCondition, AddEmptyRow, Parameters);
        }

        protected IList<TEntity> ListFastLoadEntities<TEntity>(
            IList<TEntity> RetList,
            string ColumnList, string Conditions,
            string OrderCondition, bool AddEmptyRow, params FieldParam[] Parameters)
            where TEntity : BaseEntity, new()
        {
            return Dp.ListFastLoadEntities<TEntity>(RetList,
                ColumnList, Conditions, OrderCondition, AddEmptyRow, Parameters);
        }

        protected IList<TEntity>
            ListFastLoadEntitiesUsingSqlSelect<TEntity>(
            IList<TEntity> RetList, string SqlSelect,
            string OrderCondition, params FieldParam[] Parameters)
            where TEntity : new()
        {
            return Dp.ListFastLoadEntitiesUsingSqlSelect<TEntity>(
                RetList, SqlSelect, OrderCondition, Parameters);
        }

        protected IList<TEntity>
            ListFastLoadEntitiesUsingSqlSelect<TEntity>(
            IList<TEntity> RetList, string SqlSelect,
            string OrderCondition, bool AddEmptyRow,
            params FieldParam[] Parameters)
            where TEntity : new()
        {
            return Dp.ListFastLoadEntitiesUsingSqlSelect<TEntity>(
                RetList, SqlSelect, OrderCondition, AddEmptyRow, Parameters);
        }
        #endregion

        #region FastLoadEntities
        protected AutoUpdateBindingList<TEntity> FastLoadEntities<TEntity>(
            string ColumnList, string Condition,
            string OrderCondition, bool UseCache)
            where TEntity : BaseEntity, new()
        {
            return Dp.FastLoadEntities<TEntity>(
                ColumnList, Condition, OrderCondition, UseCache);
        }

        protected AutoUpdateBindingList<TEntity>
            FastLoadEntitiesUsingSqlSelect<TEntity>(
            string SqlSelect, string OrderCondition, bool UseCache)
            where TEntity : BaseEntity, new()
        {
            return Dp.FastLoadEntitiesUsingSqlSelect<TEntity>(
                SqlSelect, OrderCondition, UseCache);
        }

        protected AutoUpdateBindingList<TEntity> FastLoadEntities<TEntity>(
            string ColumnList, string Condition,
            string OrderCondition, bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return Dp.FastLoadEntities<TEntity>(
                ColumnList, Condition, OrderCondition, UseCache, AddEmptyRow);
        }

        protected AutoUpdateBindingList<TEntity>
            FastLoadEntitiesUsingSqlSelect<TEntity>(
            string SqlSelect, string OrderCondition,
            bool UseCache, bool AddEmptyRow)
            where TEntity : BaseEntity, new()
        {
            return Dp.FastLoadEntitiesUsingSqlSelect<TEntity>(
                SqlSelect, OrderCondition, UseCache, AddEmptyRow);
        }
        #endregion

        protected int FastSaveUpdateOtherEntity(ParentEntity Entity,
            string FieldNames)
        {
            return Dp.FastSaveUpdateEntity(Entity, FieldNames);
        }

        protected bool LoadOtherEntity(BusinessEntity Entity,
            string Criteria, bool CallLoadRule,
            params FieldParam[] Parameters)
        {
            return Dp.LoadEntity(Entity, Criteria,
                CallLoadRule, Parameters);
        }

        protected bool FastLoadOtherEntity(BusinessEntity Entity,
            string FieldNames, string Criteria,
            params FieldParam[] Parameters)
        {
            return Dp.FastLoadEntity(Entity,
                FieldNames, Criteria, Parameters);
        }

        public object this[string FieldName]
        {
            get
            {
                FieldDef fld = MetaData.GetTableDef(GetType())
                    .GetFieldDef(FieldName);
                if (fld != null)
                    return fld.GetValue(this);
                else
                {
                    MemberInfo[] mis = GetType().GetMember(FieldName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.GetField |
                        BindingFlags.GetProperty);
                    foreach(MemberInfo mi in mis)
                        switch (mi.MemberType)
                        {
                            case MemberTypes.Property:
                                return ((PropertyInfo)mi).GetValue(this, null);
                            case MemberTypes.Field:
                                return ((FieldInfo)mi).GetValue(this);
                        }
                    return null;
                }
            }
            set
            {
                FieldDef fld = MetaData.GetTableDef(GetType())
                    .GetFieldDef(FieldName);
                if (fld != null)
                    fld.SetValue(this, value);
                else
                {
                    MemberInfo[] mis = GetType().GetMember(FieldName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.GetField |
                        BindingFlags.GetProperty);
                    foreach (MemberInfo mi in mis)
                        switch (mi.MemberType)
                        {
                            case MemberTypes.Property:
                                ((PropertyInfo)mi).SetValue(this, value, null);
                                return;
                            case MemberTypes.Field:
                                ((FieldInfo)mi).SetValue(this, value);
                                return;
                        }
                }
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;

            BusinessEntity be = obj as BusinessEntity;
            if (be == null) return false;

            TableDef td = MetaData.GetTableDef(GetType());
            foreach (FieldDef fld in td.KeyFields.Values)
                if (!fld.GetValue(this).Equals(fld.GetValue(be)))
                    return false;
            foreach (FieldDef fld in td.NonKeyFields.Values)
                switch (fld.DataType)
                {
                    case DataType.TimeStamp:
                    case DataType.Image:
                    case DataType.Binary:
                        break;
                    default:
                        if (fld.GetValue(this) != null &&
                            !fld.GetValue(this).Equals(fld.GetValue(be)))
                            return false;
                        break;
                }

            foreach (EntityCollDef ecd in td.ChildEntities)
                if (!ecd.GetCollValue(this).IsEqual(
                    ecd.GetCollValue(be)))
                    return false;
            return true;
        }

        protected virtual void BeforePrint(Evaluator ev) { }
        protected virtual void AfterPrint() { }
        public virtual void AfterSendToPrinter() { }

        #region IBaseEntity Members
        void IBaseEntity.BeforePrint(Evaluator ev)
        {
            BeforePrint(ev);
        }
        void IBaseEntity.AfterPrint()
        {
            AfterPrint();
        }
        void IBaseEntity.DoAfterSendToPrinter()
        {
            AfterSendToPrinter();
        }
        #endregion

        internal BaseEntity ShallowClone()
        {
            return (BaseEntity)MemberwiseClone();
        }
    }

    /// <summary>
    /// Fot Internal Use Only..
    /// </summary>
    public interface IBusinessEntity
    {
        string GetStrNotEquals(object obj);
        string GetStrIsNotDefaultEntity();
        IBindingList GetEntityCollection(string CollName);
        void BeforeRowChildMoved(string ChildName, int MovedIndex,
            BusinessEntity MovedChild, ref bool Cancel);
        void AfterRowChildMoved(string ChildName, int MovedIndex,
            BusinessEntity MovedChild);
    }

    //[DebuggerNonUserCode]
    public abstract class BusinessEntity : BaseEntity, IBusinessEntity
    {
        public virtual bool IsReadOnly(string FieldName) { return false; }

        public abstract BusinessEntity GetParentEntity();
        public abstract ParentEntity GetRootEntity();

        /// <summary>
        /// Raised when Entity FormChanged (ReadOnly/Enabled Condition Changed) called
        /// </summary>
        public event OnAction OnFormChanged;
        protected void FormChanged()
        {
            BusinessEntity beParent = GetParentEntity() as BusinessEntity;
            if (beParent != null)
                beParent.FormChanged();
            else
                if (OnFormChanged != null) OnFormChanged();
        }

        protected internal EntityNavigator CurrentNavigator;
        protected internal IBaseUINavigator BaseUINavigator;

        [Browsable(false)]
        public IUIEntity UIEntity;

        #region IBusinessEntity
        string IBusinessEntity.GetStrNotEquals(object obj)
        {
            return GetStrNotEquals(obj);
        }
        string IBusinessEntity.GetStrIsNotDefaultEntity()
        {
            return GetStrIsNotDefaultEntity();
        }
        IBindingList IBusinessEntity.GetEntityCollection(string CollName)
        {
            return GetEntityCollection(CollName);
        }
        void IBusinessEntity.BeforeRowChildMoved(string ChildName, int MovedIndex, 
            BusinessEntity MovedChild, ref bool Cancel)
        {
            BeforeRowChildMoved(ChildName, MovedIndex, MovedChild, ref Cancel);
        }

        void IBusinessEntity.AfterRowChildMoved(string ChildName, int MovedIndex, 
            BusinessEntity MovedChild)
        {
            AfterRowChildMoved(ChildName, MovedIndex, MovedChild);
        }
        #endregion

        protected bool TryGetFocusedRowData<TType>(string FieldName, 
            out TType Value)
        {
            if (BaseUINavigator != null)
                return BaseUINavigator.TryGetFocusedRowValue<TType>(
                    string.Empty, FieldName, out Value);
            else if (UIEntity != null)
                return UIEntity.TryGetFocusedRowValue<TType>(
                    string.Empty, FieldName, out Value);
            else
            {
                Value = default(TType);
                return false;
            }
        }

        protected internal virtual string GetAutoNumberTemplate()
        {
            return string.Empty;
        }
        protected internal virtual string GetAutoNumberTemplate(string FieldName)
        {
            return string.Empty;
        }

        protected internal virtual int GetStartCounter() { return 1; }
        protected internal virtual int GetStartCounter(string FieldName) { return 1; }

        protected void UpdateLoadSqlFields(string FieldList)
        {
            MetaData.GetTableDef(GetType())
                .UpdateLoadSqlFields(FieldList, this);
            DataChanged();
        }

        protected void UpdateLoadSqlFields()
        {
            MetaData.GetTableDef(GetType())
                .UpdateLoadSqlFields(string.Empty, this);
            DataChanged();
        }

        public BusinessEntity()
        {
            TableDef td = MetaData.GetTableDef(GetType());
            foreach (EntityCollDef ecd in td.ChildEntities)
                ecd.CreateNew(this);
        }
        public BusinessEntity(DataPersistance Dp)
        {
            this.Dp = Dp;
            TableDef td = MetaData.GetTableDef(GetType());
            foreach (EntityCollDef ecd in td.ChildEntities)
                ecd.CreateNew(this);
        }

        protected internal virtual void AfterClone(BusinessEntity SourceClone) { }

        public override string ToString() { return ToString(0); }
        internal string ToString(int Level)
        {
            TableDef td = MetaData.GetTableDef(GetType());
            StringBuilder TmpStr = new StringBuilder();
            string TmpSpace = TmpStr.Append(' ', Level * 5).ToString();
            TmpStr.Length = 0;
            TmpStr.Append("<").Append(GetType().Name).Append(">").AppendLine(":");
            foreach (FieldDef fld in td.Fields)
            {
                object obj = fld.GetValue(this);
                string tmpStr2 = obj == null ? "(null)" : obj.ToString();
                TmpStr.Append(TmpSpace).Append(fld._FieldName)
                    .Append(fld.IsPrimaryKey ? "(PK)=" : "=")
                    .AppendLine(tmpStr2);
            }

            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                int i = 1;
                IList Cols = ecd.GetValue((ParentEntity)this);
                if (Cols.Count > 0)
                    TmpStr.Append(TmpSpace).Append(ecd.FieldName)
                        .AppendLine("=");
                else
                    TmpStr.Append(TmpSpace).Append(ecd.FieldName)
                        .AppendLine("=(Empty)");

                foreach (BusinessEntity objItem in Cols)
                {
                    TmpStr.Append(TmpSpace).Append(i.ToString())
                        .Append(" - ").AppendLine(
                        objItem.ToString(Level + 1));
                    i++;
                }
            }
            return TmpStr.ToString().TrimEnd();
        }

        /// <summary>
        /// For Internal used only.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal string GetStrNotEquals(object obj)
        {
            if (obj == null) return "Pembanding Null";
            if (object.ReferenceEquals(this, obj)) return string.Empty;

            BusinessEntity be = obj as BusinessEntity;
            if (be == null) return "Pembanding Bukan Entity";

            TableDef td = MetaData.GetTableDef(GetType());
            foreach (FieldDef fld in td.KeyFields.Values)
                if (!fld.GetValue(this).Equals(fld.GetValue(be)))
                    return "Field " + fld._FieldName;
            foreach (FieldDef fld in td.NonKeyFields.Values)
                switch (fld.DataType)
                {
                    case DataType.TimeStamp:
                    case DataType.Image:
                    case DataType.Binary:
                        break;
                    default:
                        if (fld.GetValue(this) != null &&
                            !fld.GetValue(this).Equals(fld.GetValue(be)))
                            return "Field " + fld._FieldName;
                        break;
                }

            foreach (EntityCollDef ecd in td.ChildEntities)
            {
                string strNotEqual = ecd.GetCollValue(this)
                    .GetStrNotEqual(ecd.GetCollValue(be));
                if (strNotEqual.Length > 0)
                    return string.Concat("Detil ",
                        ecd.FieldName, ": ", strNotEqual);
            }
            return string.Empty;
        }

        public bool IsDefaultEntity()
        {
            ParentEntity pe = this as ParentEntity;
            if (pe._Original == null) pe = null;

            TableDef td = MetaData.GetTableDef(GetType());
            foreach (FieldDef fld in td.KeyFields.Values)
                if (fld._DataTypeAtr.Default != null &&
                    !fld.GetValue(this).Equals(fld.GetDefaultValue()) && (pe == null || 
                    fld.GetValue(this) != fld.GetValue(pe._Original)))
                        return false;
            foreach (FieldDef fld in td.NonKeyFields.Values)
                switch (fld.DataType)
                {
                    case DataType.TimeStamp:
                    case DataType.Image:
                    case DataType.Binary:
                        break;
                    case DataType.DateTime:
                        if (fld._DataTypeAtr.Default != null && fld._dtlsa == null &&
                            fld._DataTypeAtr.Default.GetType() == typeof(DateTime) &&
                            !fld.GetValue(this).Equals(fld.GetDefaultValue()) && (pe == null ||
                            fld.GetValue(this) != fld.GetValue(pe._Original)))
                            return false;
                        break;
                    default:
                        if (fld._DataTypeAtr.Default != null && fld._dtlsa == null &&
                            fld.GetValue(this) != null && !fld.GetValue(this).Equals(
                            fld.GetDefaultValue()) && (pe == null || 
                            fld.GetValue(this) != fld.GetValue(pe._Original)))
                            return false;
                        break;
                }

            foreach (EntityCollDef ecd in td.ChildEntities)
                if (((IList)ecd.GetCollValue((ParentEntity)this)).Count > 0)
                    return false;
            return true;
        }

        /// <summary>
        /// For Internal used only.
        /// </summary>
        /// <returns></returns>
        internal string GetStrIsNotDefaultEntity()
        {
            TableDef td = MetaData.GetTableDef(this);
            foreach (FieldDef fld in td.KeyFields.Values)
                if (!fld.GetValue(this).Equals(fld.GetDefaultValue()))
                    return "Field " + fld._FieldName;
            foreach (FieldDef fld in td.NonKeyFields.Values)
                switch (fld.DataType)
                {
                    case DataType.TimeStamp:
                    case DataType.Image:
                    case DataType.Binary:
                        break;
                    case DataType.DateTime:
                        if (fld._DataTypeAtr.Default != null && fld._dtlsa == null &&
                            fld._DataTypeAtr.Default.GetType() == typeof(DateTime) &&
                            !fld.GetValue(this).Equals(fld.GetDefaultValue()))
                            return "Field " + fld._FieldName;
                        break;
                    default:
                        if (fld._DataTypeAtr.Default != null && fld._dtlsa == null &&
                            fld.GetValue(this) != null && 
                            !fld.GetValue(this).Equals(fld.GetDefaultValue()))
                            return "Field " + fld._FieldName;
                        break;
                }

            foreach (EntityCollDef ecd in td.ChildEntities)
                if (((IList)ecd.GetCollValue((ParentEntity)this)).Count > 0)
                    return "Jumlah Detil > 0";
            return string.Empty;
        }

        protected FieldDef GetFieldDef(string FieldName)
        {
            return MetaData.GetTableDef(GetType())
                .GetFieldDef(FieldName);
        }

        protected internal virtual void onChildDataChanged(
            string ChildName, BusinessEntity ChildObject) { }

        protected internal virtual void BeforeRowChildDeleted(
            string ChildName, int DeletedIndex,
            BusinessEntity DeletedChild, ref bool Cancel) { }

        protected internal virtual void AfterRowChildDeleted(
            string ChildName, int DeletedIndex,
            BusinessEntity DeletedChild) { }

        protected virtual void BeforeRowChildMoved(
            string ChildName, int MovedIndex,
            BusinessEntity MovedChild, ref bool Cancel) { }

        protected internal virtual void AfterRowChildMoved(
                    string ChildName, int MovedIndex,
                    BusinessEntity MovedChild) { }

        internal IBindingList GetEntityCollection(string CollName)
        {
            foreach (EntityCollDef ecd in MetaData
                .GetTableDef(GetType()).ChildEntities)
                if (ecd.FieldName == CollName)
                    return (IBindingList)ecd.GetCollValue(this);
            return null;
        }

        public virtual bool IsChildVisible(string ChildName) { return true; }
        public virtual bool IsChildReadOnly(string ChildName) { return false; }
        public virtual bool IsChildColumnReadOnly(string ChildName,
            string ColumnName) { return false; }
        public virtual bool IsChildColumnVisible(string ChildName,
            string ColumnName) { return true; }
        public virtual bool IsChildAllowNew(string ChildName)
        { return true; }

        public virtual bool IsValidToSave() { return true; }

        protected FieldDef GetChildFieldDef(string ChildName, string FieldName)
        {
            foreach (EntityCollDef ecd in MetaData
                .GetTableDef(GetType()).ChildEntities)
                if (ecd.FieldName == ChildName)
                    return ecd.GetTableDef().GetFieldDef(FieldName);

            return null;
        }

        internal void CheckEmptyFields(TableDef td)
        {
            if (IsValidToSave())
            {
                foreach (FieldDef fld in td.KeyFields.Values)
                    if (fld.EmptyErrorAtr != null && fld.IsEmpty(this))
                        AddError(fld._FieldName, fld.EmptyErrorAtr.ErrorMessage);

                foreach (FieldDef fld in td.NonKeyFields.Values)
                    if (fld.EmptyErrorAtr != null && fld.IsEmpty(this))
                        AddError(fld._FieldName, fld.EmptyErrorAtr.ErrorMessage);

                foreach (EntityCollDef ecd in td.ChildEntities)
                {
                    TableDef tdChild = ecd.GetTableDef();
                    foreach (BusinessEntity be in (IList)ecd.GetCollValue(this))
                        be.CheckEmptyFields(tdChild);
                }
            }
        }

        protected TRoot GetRootEntity<TRoot>()
            where TRoot : ParentEntity
        {
            return (TRoot)GetRootEntity();
        }

        #region LoadEntity
        public bool LoadEntity(string Criteria,
            params FieldParam[] Parameters)
        {
            return LoadEntity(Criteria, true, Parameters);
        }

        private bool OnLoadEntity;
        public virtual bool LoadEntity(string Criteria,
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            if (OnLoadEntity) return false;
            OnLoadEntity = true;
            try
            {
                return Dp.LoadEntity(this, Criteria,
                    CallLoadRule, Parameters);
            }
            finally
            {
                OnLoadEntity = false;
            }
        }

        public virtual bool LoadEntity(bool CallLoadRule)
        {
            if (OnLoadEntity) return false;
            OnLoadEntity = true;
            try
            {

                return Dp.LoadEntity(this, CallLoadRule);
            }
            finally
            {
                OnLoadEntity = false;
            }
        }
        public bool LoadEntity()
        {
            return LoadEntity(true);
        }

        /// <summary>
        /// Prevent Double Load Entity 
        /// May be bug when 2 Lookupedit refer to the same property..
        /// </summary>
        private bool OnLoadEntityPKNotChanged;
        public bool LoadEntityPKNotChanged()
        {
            if (OnLoadEntityPKNotChanged) return false;
            OnLoadEntityPKNotChanged = true;
            try
            {
                TableDef td = MetaData.GetTableDef(GetType());
                object[] OldPk = new object[td.KeyFields.Count];

                int i = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                    OldPk[i++] = fld.GetValue(this);

                if (LoadEntity(true))
                    return true;
                else
                {
                    i = 0;
                    foreach (FieldDef fld in td.KeyFields.Values)
                        fld.SetLoadValue(this, OldPk[i++]);
                    DataChanged();
                    return false;
                }
            }
            finally { OnLoadEntityPKNotChanged = false; }
        }
        #endregion

        #region FastLoadEntity
        public bool FastLoadEntity(string FieldNames,
            string Criteria,
            params FieldParam[] Parameters)
        {
            return Dp.FastLoadEntity(this, FieldNames,
                Criteria, Parameters);
        }

        public bool FastLoadEntity(string FieldNames)
        {
            return Dp.FastLoadEntity(
                this, FieldNames);
        }
        #endregion

        public int FastSaveUpdateEntity(string FieldNames,
            params object[] ParentFieldValues)
        {
            return Dp.FastSaveUpdateEntity(this, FieldNames, ParentFieldValues);
        }

        public int FastSaveNewEntity(params object[] ParentFieldValues)
        {
            return Dp.FastSaveNewEntity(this, ParentFieldValues);
        }
    }

    public delegate void OnAction();

    //[DebuggerNonUserCode]
    public abstract class ParentEntity : BusinessEntity, 
        IRuleInitUI, ICancelEntity
    {
        public event EntityAction OnEntityAction;

        #region Saving/ Loading Event
        protected virtual void BeforeSaveNew() { }
        protected virtual void AfterSaveNew() { }

        protected virtual void BeforeSaveUpdate() { }
        protected virtual void AfterSaveUpdate() { }

        protected virtual void BeforeSaveDelete() { }
        protected virtual void AfterSaveDelete() { }

        protected virtual void BeforeSaveCancel(string CancelUser,
            DateTime CancelDateTime, string CancelNotes) { }
        protected virtual void AfterSaveCancel(string CancelUser,
            DateTime CancelDateTime, string CancelNotes) { }

        protected virtual void BeforeLoad() { }
        protected virtual void AfterLoadFound() { }
        protected virtual void AfterLoadNotFound() { }

        protected virtual void BeforeSetDefault(TableDef td) { }
        protected virtual void AfterSetDefault() { }

        protected virtual void InitUI() { }
        protected virtual void EndUI() { }
        protected virtual void AfterInitNavigator(IBaseUINavigator Navigator) { }

        protected virtual void ErrorAfterSaveNew() { }
        protected virtual void ErrorAfterSaveUpdate() { }
        protected virtual void ErrorAfterSaveDelete() { }

        protected internal virtual void ValidateError() { }
        #endregion

        public virtual bool IsDataChanged()
        {
            switch (FormMode)
            {
                case FormMode.FormAddNew:
                    return !IsDefaultEntity();
                case FormMode.FormEdit:
                    return !Equals(GetOriginal());
                default:
                    return false;
            }
        }

        /// <summary>
        /// Raised when FormMode Changed
        /// </summary>
        public event OnAction OnFormModeChanged;

        [Browsable(false)]
        public bool IsLoadedEntity;

        /// <summary>
        /// Flag apakah setelah SaveNew langsung Edit atau Tidak. Default: Tidak
        /// </summary>
        protected bool EditAfterSaveNew;

        internal FormMode _FormMode;
        private bool OnSetFormMode;
        [Browsable(false)]
        public FormMode FormMode
        {
            get { return _FormMode; }
            set
            {
                if (OnSetFormMode && _FormMode == value) return;
                OnSetFormMode = true;
                try
                {
                    _FormMode = value;
                    if (OnFormModeChanged != null) OnFormModeChanged();
                }
                finally
                {
                    OnSetFormMode = false;
                }
            }
        }

        private bool _AutoFormMode;
        [Browsable(false)]
        public bool AutoFormMode
        {
            get { return _AutoFormMode; }
            set { _AutoFormMode = value; }
        }

        private DataPersistance _Dp;
        public override DataPersistance Dp
        {
            get
            {
                return _Dp ?? BaseFramework.GetDefaultDp(GetType().Assembly);
            }
            set
            {
                _Dp = value;
            }
        }

        internal ParentEntity _Original;
        public ParentEntity GetOriginal() { return _Original; }
        public TEntity GetOriginal<TEntity>()
            where TEntity : ParentEntity
        { return (TEntity)_Original; }

        public void SetOriginal(ParentEntity Original) { _Original = Original; }

        public int FastSaveUpdate(string FieldNames)
        {
            return Dp.FastSaveUpdateEntity(this, FieldNames);
        }

        [Browsable(false)]
        public virtual bool AllowAddNew { get { return true; } }
        [Browsable(false)]
        public virtual bool AllowEdit { get { return true; } }
        [Browsable(false)]
        public virtual bool AllowDelete { get { return true; } }

        public virtual bool IsVisible(string FieldName) { return true; }

        public virtual bool IsChildAllowDeleteRow(string ChildName) { return true; }
        public virtual bool IsChildAllowMovingRow(string ChildName) { return true; }

        protected bool CheckTransDate = true;

        public bool IsTransDateValid()
        {
            if (BaseFramework.TransDate.CheckTransDate && CheckTransDate)
            {
                FieldDef fld = MetaData.GetTableDef(GetType())
                    .fldTransactionDate;
                if (fld != null)
                {

                    if (BaseFramework.TransDate.ReloadMinDateBeforeSave)
                        BaseFramework.TransDate.ReloadMinDate();

                    if ((DateTime)fld.GetValue(this) <
                        BaseFramework.TransDate.MinDate)
                        return false;
                    if (_FormMode != FormMode.FormAddNew && _Original != null &&
                            (DateTime)fld.GetValue(_Original) < BaseFramework.TransDate.MinDate)
                        return false;
                }
            }
            return true;
        }

        #region IRuleInitUI Members
        void IRuleInitUI.BeforeSaveNew()
        {
            BeforeSaveNew();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeSaveNew);
        }
        void IRuleInitUI.AfterSaveNew()
        {
            //if (BaseFramework.TransDate.CheckTransDate && CheckTransDate)
            //{
            //    FieldDef fld = MetaData.GetTableDef(GetType())
            //        .fldTransactionDate;
            //    if (fld != null)
            //    {
            //        if (BaseFramework.TransDate.ReloadMinDateBeforeSave)
            //            BaseFramework.TransDate.ReloadMinDate();
            //        if ((DateTime)fld.GetValue(this) <
            //            BaseFramework.TransDate.MinDate)
            //            AddError(fld._FieldName,
            //                ErrorEntity.TransDateLessThanMinDate);
            //    }
            //}
            AfterSaveNew();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterSaveNew);
        }

        void IRuleInitUI.BeforeSaveUpdate()
        {
            BeforeSaveUpdate();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeSaveUpdate);
        }
        void IRuleInitUI.AfterSaveUpdate()
        {
            //if (BaseFramework.TransDate.CheckTransDate && CheckTransDate)
            //{
            //    FieldDef fld = MetaData.GetTableDef(GetType())
            //        .fldTransactionDate;
            //    if (fld != null)
            //    {
            //        if (BaseFramework.TransDate.ReloadMinDateBeforeSave)
            //            BaseFramework.TransDate.ReloadMinDate();
            //        if ((DateTime)fld.GetValue(this) <
            //            BaseFramework.TransDate.MinDate)
            //            AddError(fld._FieldName,
            //                ErrorEntity.TransDateLessThanMinDate);
            //        if (_Original != null &&
            //            (DateTime)fld.GetValue(_Original) < BaseFramework.TransDate.MinDate)
            //            AddError(fld._FieldName,
            //                ErrorEntity.OrigTransDateLessThanMinDate);
            //    }
            //}
            AfterSaveUpdate();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterSaveUpdate);
        }

        void IRuleInitUI.BeforeSaveDelete()
        {
            BeforeSaveDelete();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeSaveDelete);
        }
        void IRuleInitUI.AfterSaveDelete()
        {
            //if (BaseFramework.TransDate.CheckTransDate && CheckTransDate)
            //{
            //    FieldDef fld = MetaData.GetTableDef(GetType())
            //        .fldTransactionDate;
            //    if (fld != null)
            //    {
            //        if (BaseFramework.TransDate.ReloadMinDateBeforeSave)
            //            BaseFramework.TransDate.ReloadMinDate();
            //        if (_Original != null)
            //        {
            //            if ((DateTime)fld.GetValue(_Original) <
            //                BaseFramework.TransDate.MinDate)
            //                AddError(fld._FieldName,
            //                    ErrorEntity.OrigTransDateLessThanMinDate);
            //        }
            //        else if ((DateTime)fld.GetValue(this) <
            //            BaseFramework.TransDate.MinDate)
            //            AddError(fld._FieldName,
            //                ErrorEntity.OrigTransDateLessThanMinDate);
            //    }
            //}
            AfterSaveDelete();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterSaveDelete);
        }

        void IRuleInitUI.BeforeLoad()
        {
            BeforeLoad();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeLoad);
        }
        void IRuleInitUI.AfterLoadFound()
        {
            AfterLoadFound();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterLoadFound);
            UpdateAutoFields();
        }
        void IRuleInitUI.AfterLoadNotFound()
        {
            AfterLoadNotFound();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterLoadNotFound);
        }

        void IRuleInitUI.BeforeSetDefault(TableDef td)
        {
            BeforeSetDefault(td);
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeSetDefault);
        }
        void IRuleInitUI.AfterSetDefault()
        {
            AfterSetDefault();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterSetDefault);
            FormChanged();
            if (_Original != null)
                MetaData.Clone(_Original, this);
        }

        void IRuleInitUI.InitUI()
        {
            InitUI();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.InitUI);
        }
        void IRuleInitUI.EndUI()
        {
            EndUI();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.EndUI);
        }
        void IRuleInitUI.AfterInitNavigator(IBaseUINavigator Navigator)
        {
            AfterInitNavigator(Navigator);
        }

        void IRuleInitUI.ErrorAfterSaveNew()
        {
            ErrorAfterSaveNew();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.ErrorAfterSaveNew);
        }
        void IRuleInitUI.ErrorAfterSaveUpdate()
        {
            ErrorAfterSaveUpdate();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.ErrorAfterSaveUpdate);
        }
        void IRuleInitUI.ErrorAfterSaveDelete()
        {
            ErrorAfterSaveDelete();
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.ErrorAfterSaveDelete);
        }

        string IRuleInitUI.GetBrowseFilter()
        {
            return GetBrowseFilter();
        }

        string IRuleInitUI.GetBrowseColumns()
        {
            return GetBrowseColumns();
        }

        void IRuleInitUI.BeforeSaveCancel(string CancelUser, DateTime CancelDateTime,
            string CancelNotes)
        {
            BeforeSaveCancel(CancelUser, CancelDateTime, CancelNotes);
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.BeforeSaveCancel);
        }

        void IRuleInitUI.AfterSaveCancel(string CancelUser, DateTime CancelDateTime,
            string CancelNotes)
        {
            AfterSaveCancel(CancelUser, CancelDateTime, CancelNotes);
            if (OnEntityAction != null)
                OnEntityAction(this, enEntityActionMode.AfterSaveCancel);
        }

        void IRuleInitUI.GetBrowseSql(out string SqlSelect,
            out string SqlCondition, out string SqlOrderBy)
        {
            GetBrowseSql(out SqlSelect, out SqlCondition, out SqlOrderBy);
        }

        void IRuleInitUI.GetBrowseFormat(Dictionary<string, string> BrowseFormat)
        {
            GetBrowseFormat(BrowseFormat);
        }

        string IRuleInitUI.GetFieldTransactionDate()
        {
            return GetFieldTransactionDate();
        }

        void IRuleInitUI.UpdateLoadSqlFields()
        {
            UpdateLoadSqlFields();
        }

        bool IRuleInitUI.IsEditAfterSaveNew()
        {
            return EditAfterSaveNew;
        }
        #endregion

        #region SaveCancel
        private DateTime TmpTglBatal;

        public int SaveCancel(string CancelUser, DateTime CancelDateTime,
            string CancelNotes)
        {
            return _SaveCancel(Dp, CancelUser, CancelDateTime, CancelNotes);
        }

        public virtual int SaveCancel(DataPersistance Dp, string CancelUser,
            DateTime CancelDateTime, string CancelNotes)
        {
            return Dp._SaveCancel(this, CancelUser, CancelDateTime, CancelNotes);
        }

        internal int _SaveCancel(DataPersistance Dp, string CancelUser, DateTime CancelDateTime,
            string CancelNotes)
        {
            using (EntityTransaction tr = new EntityTransaction(Dp))
            {
                int RetVal = SaveCancel(Dp, CancelUser, CancelDateTime, CancelNotes);
                tr.CommitTransaction();
                return RetVal;
            }
        }
        #endregion

        #region SaveNew
        public int SaveNew()
        {
            return SaveNew(Dp, true, true);
        }
        public int SaveNew(bool CallSaveRule, 
            bool CallValidateError)
        {
            return SaveNew(Dp, CallSaveRule, CallValidateError);
        }

        public virtual int SaveNew(DataPersistance Dp, bool CallSaveRule,
            bool CallValidateError)
        {
            return Dp._SaveNewEntity(this,
                CallSaveRule, CallValidateError);
        }
        #endregion

        #region SaveUpdate
        public int SaveUpdate()
        {
            return SaveUpdate(Dp, true, true);
        }
        public int SaveUpdate(bool CallSaveRule, 
            bool CallValidateError)
        {
            return SaveUpdate(Dp, CallSaveRule, CallValidateError);
        }

        public virtual int SaveUpdate(DataPersistance Dp, bool CallSaveRule,
            bool CallValidateError)
        {
            return Dp._SaveUpdateEntity(this,
                CallSaveRule, CallValidateError);
        }
        #endregion

        #region Save New/Update based on FormMode
        /// <summary>
        /// Save based on FormMode
        /// </summary>
        /// <param name="CallSaveRule"></param>
        /// <param name="CallValidateError"></param>
        /// <returns></returns>
        public int Save()
        {
            return Save(Dp, true, true);
        }
        public int Save(bool CallSaveRule, bool CallValidateError)
        {
            return Save(Dp, CallSaveRule, CallValidateError);
        }
        public int Save(DataPersistance Dp, bool CallSaveRule, 
            bool CallValidateError)
        {
            switch (FormMode)
            {
                case FormMode.FormAddNew:
                    return SaveNew(Dp, CallSaveRule, CallValidateError);
                case FormMode.FormEdit:
                    return SaveUpdate(Dp, CallSaveRule, CallValidateError);
                default:
                    if (IsLoadedEntity)
                        return SaveUpdate(Dp, CallSaveRule, CallValidateError);
                    return SaveNewOrUpdate(Dp, CallSaveRule, CallValidateError);
            }
        }
        #endregion

        #region SaveDelete
        public int SaveDelete()
        {
            return SaveDelete(Dp, true);
        }
        public int SaveDelete(bool CallDeleteRule)
        {
            return SaveDelete(Dp, CallDeleteRule);
        }

        public virtual int SaveDelete(DataPersistance Dp, bool CallDeleteRule)
        {
            return Dp._SaveDeleteEntity(this, CallDeleteRule);
        }
        #endregion

        /// <summary>
        /// Allow Primary Key to be changed when calling SaveUpdateEntity
        /// </summary>
        [Browsable(false)]
        public bool PrimaryKeyUpdateable
        {
            get { return _Original != null; }
            set
            {
                if (value)
                {
                    if (_Original == null) 
                        _Original = (ParentEntity)MetaData.Clone(this);
                }
                else _Original = null;
            }
        }

        public override bool LoadEntity(string Criteria, 
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            if (Dp.LoadEntity(this,
                Criteria, CallLoadRule, Parameters))
            {
                //FormMode = FormMode.FormEdit;
                return true;
            }
            else
            {
                FormMode = FormMode.FormAddNew;
                UpdateLoadSqlFields();
                return false;
            }
        }
        public override bool LoadEntity(bool CallLoadRule)
        {
            if (Dp.LoadEntity(this, CallLoadRule))
            {
                //FormMode = FormMode.FormEdit;
                return true;
            }
            else
            {
                FormMode = FormMode.FormAddNew;
                UpdateLoadSqlFields();
                return false;
            }
        }

        private int SaveNewOrUpdate(DataPersistance Dp, bool CallSaveRule, bool CallValidateError)
        {
            TableDef td = MetaData.GetTableDef(GetType());
            string KeyCondition = string.Empty;

            FieldParam[] Params = new FieldParam[td.KeyFields.Count];
            int i = 0;
            foreach (FieldDef fld in td.KeyFields.Values)
            {
                KeyCondition = string.Concat(KeyCondition,
                    " AND ", fld._FieldName, "=@", fld._FieldName);
                Params[i++] = new FieldParam(fld, this);
            }

            if (KeyCondition.Length > 0)
                KeyCondition = KeyCondition.Substring(5);
            if (Dp.Find.IsExists(td._ClassType, KeyCondition, Params))
                return SaveUpdate(Dp, CallSaveRule, CallValidateError);
            else
                return SaveNew(Dp, CallSaveRule, CallValidateError);
        }

        public override BusinessEntity GetParentEntity()
        {
            return null;
        }

        public override ParentEntity GetRootEntity()
        {
            return this;
        }

        protected virtual string GetBrowseFilter() { return string.Empty; }

        protected virtual void GetBrowseFormat(Dictionary<string, string> BrowseFormat) { }

        protected virtual string GetBrowseColumns() { return string.Empty; }

        protected virtual void GetBrowseSql(out string SqlSelect, 
            out string SqlCondition, out string SqlOrderBy) 
        { 
            SqlSelect = string.Empty;
            SqlCondition = string.Empty;
            SqlOrderBy = string.Empty;
        }

        protected virtual string GetFieldTransactionDate()
        {
            return string.Empty;
        }

        #region ICancelEntity Members
        private string _CancelStatus;
        string ICancelEntity.CancelStatus
        {
            get { return _CancelStatus; }
            set { _CancelStatus = value; }
        }

        private DateTime _CancelDateTime;
        DateTime ICancelEntity.CancelDateTime
        {
            get { return _CancelDateTime; }
            set { _CancelDateTime = value; }
        }

        private string _CancelUser;
        string ICancelEntity.CancelUser
        {
            get { return _CancelUser; }
            set { _CancelUser = value; }
        }

        private string _CancelNotes;
        string ICancelEntity.CancelNotes
        {
            get { return _CancelNotes; }
            set { _CancelNotes = value; }
        }
        #endregion
    }

    /// <summary>
    /// (For Internal use only)
    /// </summary>
    public interface IRuleInitUI
    {
        void BeforeSaveNew();
        void AfterSaveNew();

        void BeforeSaveUpdate();
        void AfterSaveUpdate();

        void BeforeSaveDelete();
        void AfterSaveDelete();

        void BeforeLoad();
        void AfterLoadFound();
        void AfterLoadNotFound();

        void BeforeSetDefault(TableDef td);
        void AfterSetDefault();

        void InitUI();
        void EndUI();

        void BeforeSaveCancel(string CancelUser, DateTime CancelDateTime, string CancelNotes);
        void AfterSaveCancel(string CancelUser, DateTime CancelDateTime, string CancelNotes);

        void AfterInitNavigator(IBaseUINavigator Navigator);

        string GetBrowseFilter();
        string GetBrowseColumns();
        void GetBrowseSql(out string SqlSelect, out string SqlCondition, 
            out string SqlOrderBy);
        void GetBrowseFormat(Dictionary<string, string> BrowseFormat);

        string GetFieldTransactionDate();

        void UpdateLoadSqlFields();

        void ErrorAfterSaveNew();
        void ErrorAfterSaveUpdate();
        void ErrorAfterSaveDelete();

        bool IsEditAfterSaveNew();
    }

    [DebuggerNonUserCode]
    public abstract class ViewEntity : BaseEntity
    {
        DataPersistance _Dp;
        public override DataPersistance Dp
        {
            get
            {
                return _Dp ?? BaseFramework.GetDefaultDp(GetType().Assembly);
            }
            set
            {
                _Dp = value;
            }
        }

        public bool LoadEntity(string Criteria,
            bool CallLoadRule, params FieldParam[] Parameters)
        {
            bool retVal = Dp.LoadEntity(this, Criteria,
                CallLoadRule, Parameters);
            return retVal;
        }

        public bool FastLoadEntity(string FieldNames,
            string Criteria,
            params FieldParam[] Parameters)
        {
            return Dp.FastLoadEntity(this, FieldNames,
                Criteria, Parameters);
        }
        public virtual FieldParam GetReturnType() { return null; }

        public abstract string GetSqlDdl();
        public virtual FieldParam[] GetParams() { return null; }
    }

    public interface IChildEntity
    {
        void Init(BusinessEntity ParentEntity, string ChildName, 
            bool CallSetDefault);
        BusinessEntity CreateParent();
    }

    [DebuggerNonUserCode]
    public abstract class ChildEntity<TParent> :
        BusinessEntity, IChildEntity where TParent : BusinessEntity
    {
        private BusinessEntity _ParentEntity;
        private string _ChildName;

        public override DataPersistance Dp
        {
            get
            {
                return _ParentEntity != null ?
                    _ParentEntity.Dp :
                    BaseFramework.GetDefaultDp(GetType().Assembly);
            }
            set { }
        }

        private bool OnDataChanged;
        public override sealed void DataChanged()
        {
            if (OnDataChanged) return;
            OnDataChanged = true;
            try
            {
                // Bila ParentEntity == null, data belum diInisialisasi
                // DataChanged dipanggil ketika object ini ditambahkan ke
                // collection.
                base.DataChanged();
                if (_ParentEntity != null && !_ParentEntity.EntityOnLoad)
                    _ParentEntity.onChildDataChanged(_ChildName, this);
            }
            finally
            {
                OnDataChanged = false;
            }
        }
        
        public TParent GetParent() { return (TParent)_ParentEntity; }

        #region IChildEntity
        void IChildEntity.Init(BusinessEntity ParentEntity, string ChildName, bool CallSetDefault)
        {
            if (_ParentEntity == ParentEntity)
            {
                _ChildName = ChildName;
                return;
            }
            _ParentEntity = ParentEntity;
            _ChildName = ChildName;
            if (CallSetDefault)
            {
                EntityOnLoad = true;
                try
                {
                    MetaData.GetTableDef(GetType()).SetDefault(this);
                }
                finally
                {
                    EntityOnLoad = false;
                }
            }
        }
        BusinessEntity IChildEntity.CreateParent()
        {
            _ParentEntity = BaseFactory.CreateInstance<TParent>();
            return _ParentEntity;
        }
        #endregion

        public ChildEntity(TParent ParentEntity)
        {
            _ParentEntity = ParentEntity;
        }

        public ChildEntity() { }

        public override BusinessEntity GetParentEntity()
        {
            return _ParentEntity;
        }

        public override ParentEntity GetRootEntity()
        {
            if (_ParentEntity != null)
                return _ParentEntity.GetRootEntity();
            else
                return null;
        }
    }
}
