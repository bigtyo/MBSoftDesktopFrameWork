using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace SentraUtility.Expression
{
    internal enum eMemberType
    {
        eField, eProperty, eFunction
    }

    [DebuggerNonUserCode]
    internal class ObjMember
    {
        private FieldInfo fi;
        private PropertyInfo pi;
        private MethodInfo mi;
        private eMemberType MemberType;

        public ObjMember(FieldInfo fi)
        {
            this.fi = fi;
            MemberType = eMemberType.eField;
        }
        public ObjMember(PropertyInfo pi)
        {
            this.pi = pi;
            MemberType = eMemberType.eProperty;
        }
        public ObjMember(MethodInfo mi)
        {
            this.mi = mi;
            MemberType = eMemberType.eFunction;
        }

        public object GetValue(object Entity, params object[] Parameters)
        {
            switch (MemberType)
            {
                case eMemberType.eField:
                    return fi.GetValue(Entity);
                case eMemberType.eProperty:
                    return pi.GetValue(Entity, null);
                default:
                    return mi.Invoke(Entity, Parameters);
            }
        }

        public override string ToString()
        {
            switch (MemberType)
            {
                case eMemberType.eField:
                    return fi.Name;
                case eMemberType.eProperty:
                    return pi.Name;
                default:
                    if (mi.DeclaringType.Name == "Object") return string.Empty;
                    string RetVal = mi.Name + "(";
                    ParameterInfo[] pis = mi.GetParameters();
                    if (pis.Length > 0)
                    {
                        foreach (ParameterInfo p in pis)
                            RetVal = string.Concat(RetVal, p.Name, ", ");
                        RetVal = RetVal.Substring(0, RetVal.Length - 2);
                    }
                    return RetVal + ")";
            }
        }
    }

    public class Objects
    {
        //Daftar Definisi Obyek yang diregistrasi
        private static Dictionary<string, Dictionary<string, ObjMember>> ObjDict =
            new Dictionary<string, Dictionary<string, ObjMember>>();

        //Daftar Obyek yang diregistrasi
        public Dictionary<string, object> ObjValues =
            new Dictionary<string, object>();

        public void Add(string ObjName, object ObjValue)
        {
            Dictionary<string, ObjMember> Members;
            Type ObjType = ObjValue.GetType();

            if (!ObjDict.TryGetValue(ObjType.Name, out Members))
            {
                Members = new Dictionary<string, ObjMember>();
                ObjDict.Add(ObjType.Name, Members);

                FieldInfo[] fis = ObjType.GetFields();
                foreach (FieldInfo fi in fis)
                    Members.Add(fi.Name, new ObjMember(fi));

                PropertyInfo[] pis = ObjType.GetProperties();
                foreach (PropertyInfo pi in pis)
                    Members.Add(pi.Name, new ObjMember(pi));

                MethodInfo[] mis = ObjType.GetMethods();
                foreach (MethodInfo mi in mis)
                {
                    string TmpStr = mi.Name;

                    if (TmpStr.Length > 4)
                        TmpStr = TmpStr.Substring(0, 4);

                    if (TmpStr != "get_" && TmpStr != "set_"  &&
                        mi.ReturnType.Name != "Void" &&
                        mi.DeclaringType.Name != "Object")
                        try
                        {
                            Members.Add(mi.Name, new ObjMember(mi));
                        }
                        catch { }
                }

            }
            ObjValues[ObjName] = ObjValue;
        }

        public List<string> GetListObjectName()
        {
            return new List<string>(ObjValues.Keys);
        }

        public List<string> GetListFunction(string ObjName)
        {
            object ObjValue;
            if (!ObjValues.TryGetValue(ObjName, out ObjValue))
                throw new ApplicationException(string.Concat(
                    "Obyek ", ObjName, " tidak ditemukan"));

            Dictionary<string, ObjMember> Members;
            if (!ObjDict.TryGetValue(ObjValue.GetType().Name, out Members))
                throw new ApplicationException(string.Concat(
                    "Obyek ", ObjName, " tidak ditemukan"));

            List<string> RetVal = new List<string>();
            foreach (ObjMember objm in Members.Values)
            {
                string Tmpf = objm.ToString();
                if (Tmpf.Length > 0) RetVal.Add(Tmpf);
            }
            return RetVal;
        }

        public object GetValue(string ObjName,
            string MemberName, params object[] Parameters)
        {
            object ObjValue;
            if (!ObjValues.TryGetValue(ObjName, out ObjValue))
            {
                if (ObjName.Length > 0)
                    throw new ApplicationException(string.Concat(
                        "Obyek ", ObjName, " tidak ditemukan"));
                else
                    throw new ApplicationException(string.Concat(
                        "Variabel '", MemberName, "' tidak ditemukan"));
            }

            Dictionary<string, ObjMember> Members;
            if (!ObjDict.TryGetValue(ObjValue.GetType().Name, out Members))
            {
                if (ObjName.Length > 0)
                    throw new ApplicationException(string.Concat(
                        "Obyek ", ObjName, " tidak ditemukan"));
                else
                    throw new ApplicationException(string.Concat(
                        "Variabel '", MemberName, "' tidak ditemukan"));
            }

            ObjMember ObjMember;
            if (Members.TryGetValue(MemberName, out ObjMember))
                return ObjMember.GetValue(ObjValue, Parameters);
            else if (ObjName.Length > 0)
                throw new ApplicationException(string.Concat(
                    "Obyek ", ObjName, " Member ", MemberName,
                    " tidak ditemukan"));
            else
                throw new ApplicationException(string.Concat(
                    "Variabel '", MemberName, "' tidak ditemukan"));
        }
        public void Remove(string ObjName)
        {
            ObjValues.Remove(ObjName);
        }

        public int Count { get { return ObjValues.Count; } }
    }
}
