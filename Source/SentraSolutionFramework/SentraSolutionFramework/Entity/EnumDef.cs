using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SentraUtility;
using System.Collections;

namespace SentraSolutionFramework.Entity
{
    [DebuggerNonUserCode]
    public static class EnumDef
    {
        [DebuggerNonUserCode]
        private class EnumDataCache
        {
            private Dictionary<string, object> DictValues = new Dictionary<string,object>();
            private Dictionary<object, string> DictNames = new Dictionary<object,string>();

            public EnumDataCache(Type EnumType)
            {
                string[] Names = Enum.GetNames(EnumType);
                Array Values = Enum.GetValues(EnumType);

                for (int i = 0; i < Names.Length; i++)
                {
                    string TmpStr = BaseUtility.SplitName(Names[i]);
                    object TmpObj = Values.GetValue(i);

                    DictValues.Add(TmpStr, TmpObj);
                    DictNames.Add(TmpObj, TmpStr);
                }
            }
            public string GetName(object Key)
            {
                return DictNames[Key];
            }
            public object GetValue(string Name)
            {
                object RetVal;
                if (DictValues.TryGetValue(Name, out RetVal))
                    return RetVal;
                else
                {
                    foreach (object obj in DictValues.Values)
                        return obj;
                    return 0;
                }
            }

            public ICollection GetNames()
            {
                return DictValues.Keys;
            }
        }

        private static Dictionary<Type, EnumDataCache> EnumCache = 
            new Dictionary<Type,EnumDataCache>();

        public static TEnum GetEnumValue<TEnum>(object DataName)
        {
            return (TEnum)GetEnumValue(typeof(TEnum), DataName);
        }
        public static object GetEnumValue(Type EnumType, object DataName)
        {
            EnumDataCache dc;
            if (!EnumCache.TryGetValue(EnumType, out dc))
            {
                dc = new EnumDataCache(EnumType);
                EnumCache.Add(EnumType, dc);
            }
            if (DataName.GetType() == typeof(string))
                return dc.GetValue((string)DataName);
            else
                return DataName;
        }

        public static ICollection GetEnumNames<TEnum>()
        {
            return GetEnumNames(typeof(TEnum));
        }
        public static ICollection GetEnumNames(Type EnumType)
        {
            EnumDataCache dc;
            if (!EnumCache.TryGetValue(EnumType, out dc))
            {
                dc = new EnumDataCache(EnumType);
                EnumCache.Add(EnumType, dc);
            }
            return dc.GetNames();
        }

        public static string GetEnumName<TEnum>(object Value)
        {
            return GetEnumName(typeof(TEnum), Value);
        }
        public static string GetEnumName(Type EnumType, object Value)
        {
            if (EnumType == typeof(string)) return (string)Value;

            EnumDataCache dc;
            if (!EnumCache.TryGetValue(EnumType, out dc))
            {
                dc = new EnumDataCache(EnumType);
                EnumCache.Add(EnumType, dc);
            }
            if (Value.GetType() == typeof(int))
                Value = Enum.ToObject(EnumType, Value);
            return dc.GetName(Value);
        }
    }
}
