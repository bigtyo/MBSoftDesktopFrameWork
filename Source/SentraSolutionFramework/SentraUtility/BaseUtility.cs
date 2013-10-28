using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Collections;

namespace SentraUtility
{
    //[DebuggerNonUserCode]
    public static class BaseUtility
    {
        public static string DefaultFormatDecimal = "#,##0.##;(#,##0.##)";
        public static string DefaultFormatInteger = "#,##0";
        public static string DefaultFormatDate = "dd MMM yyyy";
        public static string DefaultFormatTime = "HH:mm";
        public static string DefaultFormatDateTime = "dd MMM yyyy HH:mm";
        public static string DefaultFormatBoolean = "ya;tidak";

        private static bool _IsDebugMode;
        public static bool IsDebugMode
        {
            get { return _IsDebugMode; }
            set { _IsDebugMode = value; }
        }

        static BaseUtility()
        {
            try
            {
                DebuggableAttribute[] da = (DebuggableAttribute[])
                    Assembly.GetEntryAssembly().GetCustomAttributes(
                    typeof(DebuggableAttribute), true);

                if ((da[0].DebuggingFlags &
                    DebuggableAttribute.DebuggingModes
                    .EnableEditAndContinue) == 0)
                    _IsDebugMode = false;  //Release Mode
                else
                    _IsDebugMode = true;  //Debug Mode
            }
            catch { }
        }

        public static DateTime GetStartMonth(DateTime Dt)
        {
            return new DateTime(Dt.Year, Dt.Month, 1);
        }
        public static DateTime GetEndMonth(DateTime Dt)
        {
            return new DateTime(Dt.Year, Dt.Month, 
                DateTime.DaysInMonth(Dt.Year, Dt.Month));
        }

        public static void OpenFileWithDefaultApp(string fileName)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Verb = "Open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        }

        public static string SplitName(string Name)
        {
            if (Name == "_") return string.Empty;
            Name = Name.Replace("__", "/").Replace('_', '-');
            if (Name.Length < 4 || Name.IndexOf(' ') >= 0) return Name;

            string RetVal = string.Empty;
            int LastPos = 0;
            int Length = Name.Length;

            for (int i = 1; i < Length; i++)
            {
                char c = Name[i];
                if (c >= 'A' && c <= 'Z')
                {
                    char cBefore = Name[i - 1];
                    if (cBefore == '-') continue;
                    if (i + 1 < Length)
                    {
                        char cAfter = Name[i + 1];
                        if (cBefore >= 'A' && cBefore <= 'Z' && 
                            (cAfter >= 'A' && cAfter <= 'Z' ||
                            cAfter >= '0' && cAfter <= '9' || 
                            cAfter == '/' || cAfter == '-')) continue;
                    }
                    else if (cBefore >= 'A' && cBefore <= 'Z') continue;
                    
                    RetVal = string.Concat(RetVal,
                        Name.Substring(LastPos, i - LastPos), " ");
                    LastPos = i;
                }
            }
            return string.Concat(RetVal,
                Name.Substring(LastPos, Length - LastPos));
        }

        public static string UnSplitName(string Name)
        {
            if (Name.Length == 0) return "_";
            return Name.Replace("/", "__").Replace('-', '_')
                .Replace(" ", string.Empty);
        }

        public static TVar ConvertFromString<TVar>(string Value)
        {
            Type tp = typeof(TVar);
            if (typeof(TVar) == typeof(string))
                return (TVar)(object)Value;
            else if (typeof(TVar) == typeof(decimal))
                return (TVar)((object)decimal.Parse(Value, DefaultCultureInfo));
            else if (typeof(TVar) == typeof(DateTime))
                return (TVar)((object)DateTime.Parse(Value, DefaultCultureInfo));
            else if (typeof(TVar) == typeof(int))
                return (TVar)((object)int.Parse(Value, DefaultCultureInfo));
            else if (typeof(TVar) == typeof(Single))
                return (TVar)((object)Single.Parse(Value, DefaultCultureInfo));
            else if (typeof(TVar) == typeof(bool))
                return (TVar)((object)bool.Parse(Value));
            else throw new ApplicationException("Format Not Supported !");
        }
        public static string ConvertToString(object Value)
        {
            Type tp = Value.GetType();

            if (tp == typeof(decimal))
                return ((decimal)(object)Value).ToString(DefaultCultureInfo);
            else if (tp == typeof(DateTime))
                return ((DateTime)(object)Value).ToString(DefaultCultureInfo);
            else if (tp == typeof(int))
                return ((int)(object)Value).ToString(DefaultCultureInfo);
            else if (tp == typeof(Single))
                return ((Single)(object)Value).ToString(DefaultCultureInfo);
            else
                return Value.ToString();
        }

        public static string Dictionary2String(
            Dictionary<string, object> Data)
        {
            if (Data == null || Data.Count == 0) return string.Empty;
            
            StringBuilder RetVal = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<string, object> d in Data)
            {
                if (d.Key.StartsWith("DataSource")) continue;
                Type tp = d.Value.GetType();
                if (tp == typeof(string))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("str").Append('\x1').Append(d.Value);
                else if (tp == typeof(int))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("int").Append('\x1').Append(((int)d.Value).ToString(DefaultCultureInfo));
                else if (tp == typeof(Single))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("dec").Append('\x1').Append(((Single)d.Value).ToString(DefaultCultureInfo));
                else if (tp == typeof(double))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("dec").Append('\x1').Append(((double)d.Value).ToString(DefaultCultureInfo));
                else if (tp == typeof(decimal))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("dec").Append('\x1').Append(((decimal)d.Value).ToString(DefaultCultureInfo));
                else if (tp == typeof(bool))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("bool").Append('\x1').Append(d.Value.ToString());
                else if (tp == typeof(DateTime))
                    RetVal.Append('\x1').Append(d.Key).Append('\x1')
                        .Append("dt").Append('\x1')
                        .Append(((DateTime)d.Value).ToString(DefaultCultureInfo));
                else
                    continue;
                i++;
            }
            if (i > 0)
                return RetVal.Insert(0, i).ToString();
            else
                return string.Empty;
        }

        public static CultureInfo DefaultCultureInfo = new CultureInfo("en-US");

        public static void String2Dictionary(string Data, Dictionary<string, object> RetVal)
        {
            try
            {
                string[] ListData = Data.Split('\x1');
                int NumData = int.Parse(ListData[0]);
                for (int i = 0; i < NumData; i++)
                {
                    switch (ListData[2 + i * 3])
                    {
                        case "str":
                            RetVal[ListData[1 + i * 3]] = ListData[3 + i * 3];
                            break;
                        case "int":
                            RetVal[ListData[1 + i * 3]] = int.Parse(
                                ListData[3 + i * 3], DefaultCultureInfo);
                            break;
                        case "dec":
                            RetVal[ListData[1 + i * 3]] = decimal.Parse(
                                ListData[3 + i * 3], DefaultCultureInfo);
                            break;
                        case "bool":
                            RetVal[ListData[1 + i * 3]] = bool.Parse(
                                ListData[3 + i * 3]);
                            break;
                        case "dt":
                            RetVal[ListData[1 + i * 3]] = DateTime.Parse(
                                ListData[3 + i * 3], DefaultCultureInfo);
                            break;
                    }
                }
            }
            catch { }
        }

        //public static Type GetFieldOrPropType(Type EntityType, string FieldOrPropName)
        //{
        //    MemberInfo mi;
        //    PropertyInfo pi = EntityType.GetProperty(FieldOrPropName);
        //    Type RetType;
        //    if (pi == null)
        //    {
        //        FieldInfo fi = EntityType.GetField(FieldOrPropName);
        //        if (fi == null)
        //            throw new ApplicationException(string.Concat(
        //                FieldOrPropName, " tidak ada pada kelas ",
        //                EntityType.ToString()));
        //        RetType = fi.FieldType;
        //        mi = fi;
        //    }
        //    else
        //    {
        //        RetType = pi.PropertyType;
        //        mi = pi;
        //    }
        //    return RetType;
        //}

        public static string GetValueFromConnectionString(
            string ConnectionString, string VarName)
        {
            int i = ConnectionString.IndexOf(VarName, StringComparison.OrdinalIgnoreCase);
            if (i == -1) return string.Empty;

            i = ConnectionString.IndexOf('=', i + VarName.Length);
            if (i == -1) return string.Empty;

            int j = ConnectionString.IndexOf(';', i + 1);
            if (j < 0) return ConnectionString.Substring(i + 1);
            return ConnectionString.Substring(i + 1, j - i - 1);
        }

        public static DateTime GetBuildTime<TObject>() where TObject : class
        {
            return new System.IO.FileInfo(
                typeof(TObject).Module.FullyQualifiedName).LastWriteTime;
        }

        public static DateTime GetBuildTime(Type ObjType)
        {
            return new System.IO.FileInfo(
                ObjType.Module.FullyQualifiedName).LastWriteTime;
        }

        public static string SpellDecimal(decimal Value)
        {
            string[] ListSatuan = new string[] { 
                "Nol ", "Satu ", "Dua ", 
                "Tiga ", "Empat ", "Lima ",
                "Enam ", "Tujuh ", "Delapan ", "Sembilan " };

            string strData = Value.ToString(DefaultCultureInfo);
            int NumDigit = strData.IndexOf('.');
            if (NumDigit < 0) NumDigit = strData.Length;

            if (NumDigit > 18) return "(Overflow)";

            int n = NumDigit % 3;
            if (n > 0)
            {
                n = 3 - n;
                strData = "00".Substring(0, n) + strData;
                NumDigit += n;
            }

            string retVal = string.Empty;
            int i;
            for (i = 0; i < NumDigit; i += 3)
            {
                bool RatusPuluh = false;
                int oldLen = retVal.Length;
                n = int.Parse(strData[i].ToString());
                if (n > 0)
                {
                    if (n == 1)
                        retVal += "Seratus ";
                    else
                        retVal = string.Concat(retVal, ListSatuan[n], "Ratus ");
                    RatusPuluh = true;
                }
                n = int.Parse(strData[i + 1].ToString());
                bool Belas = false;
                if (n > 0)
                {
                    if (n == 1)
                        Belas = true;
                    else
                        retVal = string.Concat(retVal, ListSatuan[n], "Puluh ");
                    RatusPuluh = true;
                }
                n = int.Parse(strData[i + 2].ToString());
                if (Belas)
                    switch (n)
                    {
                        case 0:
                            retVal += "Sepuluh ";
                            break;
                        case 1:
                            retVal += "Sebelas ";
                            break;
                        default:
                            retVal = string.Concat(retVal, ListSatuan[n], "Belas ");
                            break;
                    }
                else if (n > 0)
                    retVal += ListSatuan[n];
                if (oldLen != retVal.Length)
                {
                    switch (NumDigit - i)
                    {
                        case 6: //Ribu
                            if (retVal.EndsWith("Satu ") && !RatusPuluh)
                                retVal = retVal.Substring(0, retVal.Length - 5) + "Seribu ";
                            else
                                retVal += "Ribu ";
                            break;
                        case 9: //Juta
                            retVal += "Juta ";
                            break;
                        case 12: //Milyard
                            retVal += "Milyard ";
                            break;
                        case 15: //Trilyun
                            retVal += "Trilyun ";
                            break;
                    }
                }
            }
            if (i < strData.Length)
            {
                if (retVal.Length == 0) retVal = "Nol ";
                retVal += "Koma ";
                for (i++; i < strData.Length; i++)
                    retVal += ListSatuan[int.Parse(strData[i].ToString())];

            }
            return retVal.Length > 0 ? retVal.Trim() : "Nol";
        }

        public static string GetFileName(string FileFolder)
        {
            int i = FileFolder.LastIndexOf('\\');
            if (i >= 0)
                FileFolder = FileFolder.Substring(i + 1);
            i = FileFolder.LastIndexOf('.');
            if (i > 0)
                return FileFolder.Substring(0, i);
            else
                return FileFolder;
        }
        public static string GetFileNameExt(string FileFolder)
        {
            int i = FileFolder.LastIndexOf('\\');
            if (i >= 0)
                FileFolder = FileFolder.Substring(i + 1);
            return FileFolder;
        }
        public static string GetFolderName(string FileFolder)
        {
            int i = FileFolder.LastIndexOf('\\');
            if (i >= 0)
                FileFolder = FileFolder.Substring(0, i);
            return FileFolder;
        }
    }
}
