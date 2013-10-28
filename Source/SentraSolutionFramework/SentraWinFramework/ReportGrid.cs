using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Collections;
using SentraUtility;
using DevExpress.Data;

namespace SentraWinFramework
{
    internal class ReportGrid : IRelationList, IList
    {
        IList _EntityList;
        EntityColumnShow ecs;
        public TableDef td;

        public ReportGrid(Type EntityType, IList EntityList, EntityColumnShow ecs)
        {
            _EntityList = EntityList;
            this.ecs = ecs;
            td = MetaData.GetTableDef(EntityType);
        }

        #region IRelationList Members
        IList IRelationList.GetDetailList(int index, int relationIndex)
        {
            return td.ChildEntities[relationIndex].GetValue((ParentEntity)_EntityList[index]);
        }
        string IRelationList.GetRelationName(int index, int relationIndex)
        {
            return td.ChildEntities[relationIndex].FieldName;
        }
        bool IRelationList.IsMasterRowEmpty(int index, int relationIndex)
        {
            return relationIndex >= td.ChildEntities.Count;
        }
        int IRelationList.RelationCount
        {
            get { return ecs.ListChild.Count; }
        }
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
        bool IList.IsFixedSize
        {
            get { return true; }
        }
        bool IList.IsReadOnly
        {
            get { return true; }
        }
        void IList.Remove(object value)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        void IList.RemoveAt(int index)
        {
            _EntityList.RemoveAt(index);
        }
        object IList.this[int index]
        {
            get
            {
                return _EntityList[index];
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        #endregion

        #region ICollection Members
        void ICollection.CopyTo(Array array, int index)
        {
            _EntityList.CopyTo(array, index);
        }
        int ICollection.Count
        {
            get { return _EntityList.Count; }
        }
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
            foreach (object o in _EntityList)
                yield return o;
        }
        #endregion
    }
}
