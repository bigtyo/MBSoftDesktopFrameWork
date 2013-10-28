using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace SentraUtility
{
    [Serializable]
    [DebuggerNonUserCode]
    public class BindingListEx<T> : BindingList<T>
    {
        public void Sort(string SortFields)
        {
            List<T> ListItems = (List<T>)Items;

            ListItems.Sort(new DynamicComparer<T>(SortFields));
            base.OnListChanged(new ListChangedEventArgs(
                ListChangedType.Reset, 0));
        }

        // Fix Serialization problem with InotifyPropertyChanged
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            List<T> items = new List<T>(Items);

            int index = 0;
            foreach (T item in items)
                base.SetItem(index++, item);
        }

        public decimal Sum(string FieldName)
        {
            if (base.Items.Count == 0) return 0;

            Type tp = typeof(T);
            PropertyInfo pi = tp.GetProperty(FieldName, 
                BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic);

            decimal RetVal = 0;
            if (pi != null)
                foreach (T Item in base.Items)
                    RetVal += (decimal)pi.GetValue(Item, null);
            else
            {
                FieldInfo fi = tp.GetField(FieldName, 
                    BindingFlags.Instance |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (fi != null)
                    foreach (T Item in base.Items)
                        RetVal += (decimal)fi.GetValue(Item);
                else
                    throw new ApplicationException(FieldName + " Not Found !");
            }
            return RetVal;
        }
        public decimal Avg(string FieldName)
        {
            if (Items.Count == 0) return 0;

            Type tp = typeof(T);
            PropertyInfo pi = tp.GetProperty(FieldName, BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic);

            decimal RetVal = 0;
            if (pi != null)
                foreach (T Item in base.Items)
                    RetVal += (decimal)pi.GetValue(Item, null);
            else
            {
                FieldInfo fi = tp.GetField(FieldName, BindingFlags.Instance |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (fi != null)
                    foreach (T Item in Items)
                        RetVal += (decimal)fi.GetValue(Item);
                else
                    throw new ApplicationException(FieldName + " Not Found !");
            }
            return RetVal / Items.Count;
        }
        public decimal Min(string FieldName)
        {
            if (base.Items.Count == 0) return 0;

            Type tp = typeof(T);
            PropertyInfo pi = tp.GetProperty(FieldName, BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic);

            decimal RetVal = decimal.MaxValue;
            if (pi != null)
                foreach (T Item in base.Items)
                {
                    decimal Tmp = (decimal)pi.GetValue(Item, null);
                    if (Tmp < RetVal) RetVal = Tmp;
                }
            else
            {
                FieldInfo fi = tp.GetField(FieldName, BindingFlags.Instance |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (fi != null)
                {
                    foreach (T Item in base.Items)
                    {
                        decimal Tmp = (decimal)fi.GetValue(Item);
                        if (Tmp < RetVal) RetVal = Tmp;
                    }
                }
                else
                    throw new ApplicationException(FieldName + " Not Found !");
            }
            return RetVal;
        }
        public decimal Max(string FieldName)
        {
            if (base.Items.Count == 0) return 0;

            Type tp = typeof(T);
            PropertyInfo pi = tp.GetProperty(FieldName, BindingFlags.Instance |
                BindingFlags.Public | BindingFlags.NonPublic);

            decimal RetVal = decimal.MinValue;
            if (pi != null)
                foreach (T Item in base.Items)
                {
                    decimal Tmp = (decimal)pi.GetValue(Item, null);
                    if (Tmp > RetVal) RetVal = Tmp;
                }
            else
            {
                FieldInfo fi = tp.GetField(FieldName, BindingFlags.Instance |
                    BindingFlags.Public | BindingFlags.NonPublic);
                if (fi != null)
                {
                    foreach (T Item in base.Items)
                    {
                        decimal Tmp = (decimal)fi.GetValue(Item);
                        if (Tmp > RetVal) RetVal = Tmp;
                    }
                }
                else
                    throw new ApplicationException(FieldName + " Not Found !");
            }
            return RetVal;
        }
    }
}
