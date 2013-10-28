using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data;
using System.Collections;
using SentraSolutionFramework.Entity;
using SentraUtility;
using System.Reflection;
using System.ComponentModel;
using SentraUtility.Expression;
using SentraSolutionFramework.Persistance;

namespace SentraWinFramework.Report
{
    internal sealed class ReportSingleEntity : IRelationList,
        IList, IDataDictionary
    {
        BaseEntity _DataSource;
        TableDef tdSource;

        public void DoBeforePrint(Evaluator ev)
        {
            ((IBaseEntity)_DataSource).BeforePrint(ev);
        }

        public void DoAfterPrint()
        {
            ((IBaseEntity)_DataSource).AfterPrint();
        }

        public ReportSingleEntity(BaseEntity DataSource)
        {
            _DataSource = DataSource;
            tdSource = MetaData.GetTableDef(DataSource);
        }

        #region IRelationList Members
        System.Collections.IList IRelationList.GetDetailList(int index, int relationIndex)
        {
            return tdSource.ChildEntities[relationIndex]
              .GetValue(_DataSource);
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
            get { return _DataSource; }
            set { }
        }
        #endregion

        #region ICollection Members
        void ICollection.CopyTo(Array array, int index) { }
        int ICollection.Count { get { return 1; } }
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
            if (dataMember.Length == 0) return string.Empty;

            if (!dataMember.Contains("."))
            {
                Type tp = tdSource.ClassType;
                BrowsableAttribute[] bas = null;

                PropertyInfo pi = tp.GetProperty(dataMember);
                if (pi != null)
                {
                    if (pi.PropertyType.Name == "AutoUpdateBindingList`1")
                        return string.Empty;

                    bas = (BrowsableAttribute[])pi.GetCustomAttributes(
                        typeof(BrowsableAttribute), true);
                }
                else
                {
                    FieldInfo fi = tp.GetField(dataMember);
                    if (fi != null)
                    {
                        if (fi.FieldType.Name == "AutoUpdateBindingList`1")
                            return string.Empty;

                        bas = (BrowsableAttribute[])fi.GetCustomAttributes(
                            typeof(BrowsableAttribute), true);
                    }
                }
                if (bas != null && (bas.Length == 0 || bas[0].Browsable))
                    return BaseUtility.SplitName(dataMember);
                else
                    return string.Empty;
            }
            else
                return BaseUtility.SplitName(
                    dataMember.Substring(dataMember
                    .LastIndexOf('.') + 1));
        }

        #endregion

        public void DoAfterSendToPrinter()
        {
            TableDef td = MetaData.GetTableDef(_DataSource.GetType());
            if (td.fldPrintCounter != null)
            {
                DataPersistance Dp = _DataSource.Dp;
                string strTemp = string.Empty;
                FieldParam[] fps = new FieldParam[td.KeyFields.Count];
                int Ctr = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    strTemp = string.Concat(strTemp, " AND ", fld.FieldName, "=@", Ctr);
                    fps[Ctr] = new FieldParam(Ctr.ToString(), fld, fld.GetValue(_DataSource));
                }
                strTemp = strTemp.Substring(5);
                Dp.ExecuteNonQuery(string.Concat("UPDATE ",
                     td.TableName, " SET ", td.fldPrintCounter.FieldName,
                     "=", td.fldPrintCounter.FieldName, "+1 WHERE ", strTemp), fps);
            }
            ((IBaseEntity)_DataSource).DoAfterSendToPrinter();
        }
    }
}
