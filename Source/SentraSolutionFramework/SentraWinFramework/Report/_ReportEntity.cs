using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data;
using SentraSolutionFramework.Entity;
using System.Collections;
using SentraUtility;
using System.Data;
using System.ComponentModel;
using System.Diagnostics;

namespace SentraWinFramework.Report
{
    internal class _ReportEntity : IRelationList, IList, IDataDictionary
    {
        TableDef tdSource;
        IList _DataSource;

        public _ReportEntity(object DataSource)
        {
            IAutoUpdateList aul = DataSource as IAutoUpdateList;
            if (aul != null)
            {
                tdSource = aul.Td;
                _DataSource = aul.GetList();
            }
            else
            {
                IList ds = DataSource as IList;
                if (ds != null)
                    tdSource = MetaData.GetTableDef(DataSource.GetType().GetGenericArguments()[0]);
                else
                {
                    ds = new BindingList<object>();
                    ds.Add(DataSource);
                    tdSource = MetaData.GetTableDef(DataSource.GetType());
                }
                _DataSource = ds;
            }
        }

        #region IRelationList Members
        IList IRelationList.GetDetailList(int index, int relationIndex)
        {
            return tdSource.ChildEntities[relationIndex]
              .GetValue((ParentEntity)_DataSource[index]);
        }
        string IRelationList.GetRelationName(int index, int relationIndex)
        { return BaseUtility.SplitName(tdSource.ChildEntities[relationIndex].FieldName); }
        bool IRelationList.IsMasterRowEmpty(int index, int relationIndex)
        { return false; }
        int IRelationList.RelationCount
        { get { return tdSource.ChildEntities.Count; } }
        #endregion

        #region IList Members
        int IList.Add(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        bool IList.Contains(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        int IList.IndexOf(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.Insert(int index, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        bool IList.IsFixedSize { get { return true; } }
        bool IList.IsReadOnly { get { return true; } }
        void IList.Remove(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        object IList.this[int index]
        {
            get { return _DataSource[index]; }
            set { }
        }
        #endregion

        #region ICollection Members
        void ICollection.CopyTo(Array array, int index)
        { }
        int ICollection.Count
        { get { return _DataSource.Count; } }
        bool ICollection.IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        object ICollection.SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region IDataDictionary Members

        string IDataDictionary.GetDataSourceDisplayName()
        {
            return BaseUtility.SplitName(tdSource.TableName);
        }

        string IDataDictionary.GetObjectDisplayName(string dataMember)
        {
            return BaseUtility.SplitName(
                dataMember.Substring(dataMember
                .LastIndexOf('.') + 1));
        }

        #endregion
    }

    internal class ReportDataTable : IRelationList, IList, IDataDictionary
    {
        IList _DataSource;
        string TableName;

        public ReportDataTable(DataTable DataSource)
        {
            _DataSource = ((IListSource)DataSource).GetList();
            TableName = DataSource.TableName;
        }
        public ReportDataTable(DataView DataSource)
        {
            _DataSource = ((IListSource)DataSource.ToTable()).GetList();
            TableName = DataSource.Table.TableName;
        }

        #region IRelationList Members
        IList IRelationList.GetDetailList(int index, int relationIndex)
        {
            return null;
        }
        string IRelationList.GetRelationName(int index, int relationIndex)
        { return string.Empty; }
        bool IRelationList.IsMasterRowEmpty(int index, int relationIndex)
        { return false; }
        int IRelationList.RelationCount
        { get { return 0; } }
        #endregion

        #region IList Members
        int IList.Add(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.Clear()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        bool IList.Contains(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        int IList.IndexOf(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.Insert(int index, object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        bool IList.IsFixedSize { get { return true; } }
        bool IList.IsReadOnly { get { return true; } }
        void IList.Remove(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.RemoveAt(int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        object IList.this[int index]
        {
            get { return _DataSource[index]; }
            set { }
        }
        #endregion

        #region ICollection Members
        void ICollection.CopyTo(Array array, int index)
        {
            //for (int i = 0; i < _DataSource.Count; i++)
            //    array.SetValue(_DataSource[i], i);
        }
        int ICollection.Count
        { get { return _DataSource.Count; } }
        bool ICollection.IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        object ICollection.SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region IDataDictionary Members

        string IDataDictionary.GetDataSourceDisplayName()
        {
            return BaseUtility.SplitName(TableName);
        }

        string IDataDictionary.GetObjectDisplayName(string dataMember)
        {
            return BaseUtility.SplitName(
                dataMember.Substring(dataMember
                .LastIndexOf('.') + 1));
        }

        #endregion
    }
}
