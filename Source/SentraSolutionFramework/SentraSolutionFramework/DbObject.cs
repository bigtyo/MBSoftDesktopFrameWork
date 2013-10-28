using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;

namespace SentraSolutionFramework
{
    public abstract class DbObject
    {
        public abstract string GetDDLCreate(DataPersistance Dp);
        public virtual string GetDDLUpdate(DataPersistance Dp) { return string.Empty; }

        //private Dictionary<string, int> ListExist = new Dictionary<string, int>();
        //internal bool GetIsExist(DataPersistance dp)
        //{
        //    return ListExist.ContainsKey(dp.ConnectionString);
        //}
        //internal void SetIsExist(DataPersistance dp)
        //{
        //    int TmpInt;
        //    if (!ListExist.TryGetValue(dp.ConnectionString, out TmpInt))
        //    {
        //        ListExist.Add(dp.ConnectionString, 0);
        //    }
        //}
        //internal void SetIsExist(DataPersistance dp, bool IsExist)
        //{
        //    int TmpInt;

        //    if (!ListExist.TryGetValue(dp.ConnectionString, out TmpInt))
        //    {
        //        if (IsExist)
        //            ListExist.Add(dp.ConnectionString, 0);
        //    }
        //    else if (!IsExist)
        //        ListExist.Remove(dp.ConnectionString);
        //}

        internal string GetAsmVersion()
        {
            string retVal = GetType().Assembly.FullName;
            int StartPos = retVal.IndexOf("Version=", 0) + 8;
            int EndPos = retVal.IndexOf(',', StartPos + 7);
            retVal = retVal.Substring(StartPos, EndPos - StartPos);
            if (retVal.EndsWith(".0"))
                throw new ApplicationException(string.Format(
                    ErrorMetaData.AsmVersionMustAutoIncrement,
                    GetType().Assembly.GetName()));
            return retVal;
        }
        internal string GetAsmName()
        {
            string retVal = GetType().Assembly.FullName;
            return retVal.Substring(0, retVal.IndexOf(','));
        }
    }
}
