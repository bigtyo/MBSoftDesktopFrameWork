using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SentraUtility.Expression
{
    public delegate object delegateGetValue(string VarName);

    [DebuggerNonUserCode]
    public class Variables
    {
        public Dictionary<string, object> VarDictionary =
            new Dictionary<string, object>();

        public delegateGetValue onGetValue;

        public void Add(string VarName, decimal VarValue)
        {
            VarDictionary[VarName] = VarValue;
        }
        public void Add(string VarName, DateTime VarValue)
        {
            VarDictionary[VarName] = VarValue;
        }
        public void Add(string VarName, Boolean VarValue)
        {
            VarDictionary[VarName] = VarValue;
        }
        public void Add(string VarName, string VarValue)
        {
            VarDictionary[VarName] = VarValue;
        }

        public object GetValue(string VarName)
        {
            object VarValue = null;
            if (VarDictionary.TryGetValue(VarName, out VarValue))
                return VarValue;
            if (onGetValue != null)
                return onGetValue(VarName);
            return null;
        }
        public void Remove(string VarName)
        {
            VarDictionary.Remove(VarName);
        }
        public void Clear() { VarDictionary.Clear(); }
    }
}
