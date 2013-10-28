using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework.Entity
{
    public interface IEntityCollection
    {
        void Init(BusinessEntity Parent, string ChildName);
        string ChildName { get; }
        bool OnLoad { get; set; }
        bool OnRowMove { get; set; }
        void Clear();
        void ClearError(int Index);
        void ClearError();
        bool IsErrorExist();
        bool IsEqual(IEntityCollection OtherCollection);
        string GetStrNotEqual(IEntityCollection iEntityCollection);
        int Count { get; }
        Type GetChildType();
        BusinessEntity GetValue(int Index);
        BusinessEntity GetParent();
    }

    [DebuggerNonUserCode]
    public sealed class EntityCollection<TChild> : 
        BindingListEx<TChild>, IEntityCollection
        where TChild : BusinessEntity
    {
        private BusinessEntity _Parent;
        private string _ChildName;

        private bool _OnLoad = false;

        private bool _OnRowMove;
        public bool OnRowMove
        {
            get { return _OnRowMove; }
            set { _OnRowMove = value; }
        }

        #region IEntityCollection Members
        string IEntityCollection.ChildName
        {
            get { return _ChildName; }
        }
        bool IEntityCollection.OnLoad
        {
            get { return _OnLoad; }
            set
            {
                _OnLoad = value;
                if (!_OnLoad)
                    base.OnListChanged(new ListChangedEventArgs(
                        ListChangedType.Reset, 0));
            }
        }
        void IEntityCollection.Init(BusinessEntity Parent, string ChildName)
        {
            _Parent = Parent;
            _ChildName = ChildName;
        }
        void IEntityCollection.Clear()
        {
            if (Count > 0)
            {
                Clear();
                base.OnListChanged(new ListChangedEventArgs(
                    ListChangedType.Reset, 0));
            }
        }
        void IEntityCollection.ClearError(int Index)
        {
            ((BusinessEntity)Items[Index]).ClearError();
        }
        void IEntityCollection.ClearError()
        {
            foreach(TChild Item in Items)
                Item.ClearError();
        }
        bool IEntityCollection.IsErrorExist()
        {
            foreach (TChild Item in Items)
                if (Item.IsErrorExist()) return true;
            return false;
        }
        bool IEntityCollection.IsEqual(IEntityCollection OtherCollection)
        {
            BindingList<TChild> OtherList = OtherCollection 
                as BindingList<TChild>;
            if (OtherList == null) return false;
            if (Count != OtherList.Count) return false;
            int i = 0;
            foreach (TChild Item in this)
                if (!OtherList[i++].Equals(Item))
                    return false;
            return true;
        }
        string IEntityCollection.GetStrNotEqual(IEntityCollection OtherCollection)
        {
            BindingList<TChild> OtherList = OtherCollection 
                as BindingList<TChild>;
            if (OtherList == null) 
                return "Pembanding Null";
            if (Count != OtherList.Count) 
                return "Jumlah Detil tidak sama";
            int i = 0;
            foreach (TChild Item in this)
            {
                string strNotEqual = OtherList[i++].GetStrNotEquals(Item);
                if (strNotEqual.Length > 0) return strNotEqual;
            }
            return string.Empty;
        }
        Type IEntityCollection.GetChildType()
        {
            return typeof(TChild);
        }
        BusinessEntity IEntityCollection.GetValue(int Index)
        {
            return this[Index];
        }
        BusinessEntity IEntityCollection.GetParent()
        {
            return _Parent;
        }
        #endregion

        public bool TryGetFocusedRowData<TType>(string FieldName, out TType Value)
        {
            if (_Parent.BaseUINavigator != null)
                return _Parent.BaseUINavigator
                    .TryGetFocusedRowValue<TType>(_ChildName, FieldName, out Value);
            else if (_Parent.UIEntity != null)
                return _Parent.UIEntity.TryGetFocusedRowValue<TType>(_ChildName, FieldName,
                    out Value);
            else
            {
                Value = default(TType);
                return false;
            }
        }

        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            e.NewObject = BaseFactory.CreateInstance<TChild>();
            base.OnAddingNew(e);
        }

        protected override void RemoveItem(int index)
        {
            if (_OnLoad || OnRowMove)
                base.RemoveItem(index);
            else
            {
                bool Cancel = false;
                BusinessEntity delChild = base[index];
                _Parent.BeforeRowChildDeleted(_ChildName, index, 
                    delChild, ref Cancel);
                if (!Cancel) base.RemoveItem(index);
                _Parent.AfterRowChildDeleted(_ChildName, index,
                    delChild);
            }
        }

        protected override void ClearItems()
        {
            if (Count > 0)
            {
                base.ClearItems();
                if (!_OnLoad) 
                    _Parent.onChildDataChanged(_ChildName, null);
            }
        }
        public void Refresh()
        {
            base.OnListChanged(new ListChangedEventArgs(
                ListChangedType.Reset, 0));
        }
        protected sealed override void OnListChanged(
            ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.Reset) 
                return;
            if (e.ListChangedType == ListChangedType.ItemAdded)
                ((IChildEntity)base[e.NewIndex]).Init(_Parent,
                    _ChildName, !_OnLoad);
            if (!_OnLoad)
            {
                if (e.ListChangedType == ListChangedType.ItemDeleted)
                    _Parent.onChildDataChanged(_ChildName, null);
                else
                    _Parent.onChildDataChanged(_ChildName, 
                        base[e.NewIndex]);
                base.OnListChanged(e);
            }
        }
    }
}
