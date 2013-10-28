using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace SentraUtility
{
    [DebuggerNonUserCode]
    public class ValueSortedList<TItem> : IList<TItem>
    {
        private List<TItem> _Items = new List<TItem>();
        private bool UniqueItem;

        #region IList<TItem> Members
        public int IndexOf(TItem item) { return _Items.BinarySearch(item); }
        public void Insert(int index, TItem item)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public void RemoveAt(int index) { _Items.RemoveAt(index); }
        public TItem this[int index]
        {
            get { return _Items[index]; }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        #endregion

        public ValueSortedList(bool UniqueItem) { this.UniqueItem = UniqueItem; }

        #region ICollection<TItem> Members
        public int Count { get { return _Items.Count; } }
        public void Add(TItem item)
        {
            int i = _Items.BinarySearch(item);
            if (i < 0)
                i = ~i;
            else if (UniqueItem)
                throw new ApplicationException(string.Format(
                    ErrorUtility.ItemNotUnique, item.ToString()));
            _Items.Insert(i, item);
        }
        public void Clear() { _Items.Clear(); }
        public bool Contains(TItem item) { return _Items.BinarySearch(item) >= 0; }
        public void CopyTo(TItem[] array, int arrayIndex)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public bool IsReadOnly { get { return false; } }
        public bool Remove(TItem item)
        {
            int i = _Items.BinarySearch(item);
            if (i < 0) return false;
            _Items.RemoveAt(i);
            return true;
        }
        #endregion

        #region IEnumerable<TItem> Members
        public IEnumerator<TItem> GetEnumerator() { return _Items.GetEnumerator(); }
        #endregion

        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator() { return _Items.GetEnumerator(); }
        #endregion
    }
}
