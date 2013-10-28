using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using SentraUtility;

namespace SentraSolutionFramework
{
    [TableName("_System_AppVariable")]
    [DebuggerNonUserCode]
    public class AppVariable : ParentEntity
    {
        [DataTypeVarChar(50), PrimaryKey]
        public string ModuleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string VarName;
        [DataTypeVarChar(2000)]
        public string VarValue;
        [DataTypeBinary]
        public byte[] BinValue;

        internal static TVar GetVariable<TVar>(DataPersistance dp, 
            string ModuleName, string VarName, TVar DefaultValue)
        {
            AppVariable Var = new AppVariable();

            Var.ModuleName = ModuleName;
            Var.VarName = VarName;

            if (!dp.LoadEntity(Var, false))
                return DefaultValue;

            Type tp = typeof(TVar);

            if (tp == typeof(string) ||
                tp == typeof(decimal) ||
                tp == typeof(DateTime) ||
                tp == typeof(int) ||
                tp == typeof(Single) ||
                tp == typeof(bool))
                return BaseUtility.ConvertFromString<TVar>(
                    (string)Var.VarValue);
            else if (tp == typeof(Image))
            {
                if (Var.BinValue == null)
                    return (TVar)(object)null;
                else
                    return (TVar)(object)Helper.ConvertByteArrayToImage(
                        Var.BinValue);
            }
            else
                return (TVar)(object)Var.BinValue;
        }
        internal static void SetVariable(DataPersistance dp, 
            string ModuleName, string VarName, object Value)
        {
            AppVariable Var = new AppVariable();

            Var.ModuleName = ModuleName;
            Var.VarName = VarName;
            Var.VarValue = string.Empty;
            if (Value == null)
                Var.BinValue = null;
            else
            {
                Type tp = Value.GetType();

                if (tp == typeof(string) ||
                    tp == typeof(decimal) ||
                    tp == typeof(DateTime) ||
                    tp == typeof(int) ||
                    tp == typeof(Single) ||
                    tp == typeof(bool))
                    Var.VarValue = BaseUtility.ConvertToString(Value);
                else if (tp == typeof(Bitmap))
                    Var.BinValue = Helper.ConvertImageToByteArray(
                        (Image)Value);
                else
                    Var.BinValue = (byte[])Value;
            }
            Var.Save(dp, false, false);
        }

        internal static AppVariables GetVariables(DataPersistance dp, 
            string ModuleName)
        {
            return new AppVariables(ModuleName, 
                dp.ListLoadEntities<AppVariable>(null, "ModuleName=@0",
                string.Empty, false, new FieldParam("0", ModuleName)));
        }
        internal static void SetVariables(DataPersistance dp, 
            AppVariables Vars)
        {
            using (EntityTransaction tr = new EntityTransaction(dp))
            {
                foreach (AppVariable Var in Vars.ListVar)
                    if (Var.BinValue == null)
                        SetVariable(dp, Var.ModuleName,
                            Var.VarName, Var.VarValue);
                    else
                        SetVariable(dp, Var.ModuleName,
                            Var.VarName, Var.BinValue);
                tr.CommitTransaction();
            }
        }
    }

    [DebuggerNonUserCode]
    public class AppVariables
    {
        internal IList<AppVariable> ListVar;
        private string ModuleName;

        internal AppVariables(string ModuleName,
            IList<AppVariable> ListVar)
        {
            this.ModuleName = ModuleName;
            this.ListVar = ListVar;
        }

        public TVar GetVariable<TVar>(string VarName, TVar DefaultValue)
        {
            foreach(AppVariable Var in ListVar)
                if (Var.VarName.Equals(VarName))
                {
                    Type tp = typeof(TVar);
                    if (tp == typeof(string) ||
                        tp == typeof(decimal) ||
                        tp == typeof(DateTime) ||
                        tp == typeof(int) ||
                        tp == typeof(Single) ||
                        tp == typeof(bool))
                        return BaseUtility.ConvertFromString<TVar>(
                            (string)Var.VarValue);
                    else if (tp == typeof(Image))
                    {
                        if (Var.BinValue == null)
                            return (TVar)(object)null;
                        else
                            return (TVar)(object)Helper.ConvertByteArrayToImage(Var.BinValue);
                    }
                    else
                        return (TVar)(object)Var.BinValue;
                }
            return DefaultValue;
        }
        public void SetVariable(string VarName, object Value)
        {
            foreach (AppVariable Var in ListVar)
                if (Var.VarName.Equals(VarName))
                {
                    Var.VarValue = string.Empty;
                    if (Value == null)
                        Var.BinValue = null;
                    else
                    {
                        Type tp = Value.GetType();
                        if (tp == typeof(string) ||
                            tp == typeof(decimal) ||
                            tp == typeof(DateTime) ||
                            tp == typeof(int) ||
                            tp == typeof(Single) ||
                            tp == typeof(bool))
                            Var.VarValue = BaseUtility.ConvertToString(Value);
                        else if (tp == typeof(Bitmap))
                            Var.BinValue = Helper.ConvertImageToByteArray((Image)Value);
                        else
                            Var.BinValue = (byte[])Value;
                        break;
                    }
                    return;
                }

            AppVariable NewVar = new AppVariable();

            NewVar.ModuleName = ModuleName;
            NewVar.VarName = VarName;
            NewVar.VarValue = string.Empty;
            if (Value == null)
                NewVar.BinValue = null;
            else
            {
                Type tp = Value.GetType();
                if (tp == typeof(string) ||
                    tp == typeof(decimal) ||
                    tp == typeof(DateTime) ||
                    tp == typeof(int) ||
                    tp == typeof(Single) ||
                    tp == typeof(bool))
                    NewVar.VarValue = BaseUtility.ConvertToString(Value);
                else if (tp == typeof(Bitmap))
                    NewVar.BinValue = Helper.ConvertImageToByteArray((Image)Value);
                else
                    NewVar.BinValue = (byte[])Value;
            }
            ListVar.Add(NewVar);
        }
    }
}
