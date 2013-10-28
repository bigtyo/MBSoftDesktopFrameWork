using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using System.Data;
using System.Drawing;
using SentraUtility;

namespace SentraSecurity
{
    [TableName("_System_RoleVariable")]
    [Relation(typeof(Role),ParentUpdate.UpdateCascade, ParentDelete.DeleteCascade)]
    class RoleVariable : ParentEntity
    {
        [DataTypeVarChar(50), PrimaryKey]
        public string ProductName;
        [DataTypeVarChar(50), PrimaryKey]
        public string RoleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string ModuleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string VarName;
        [DataTypeVarChar(2000)]
        public string VarValue;
        [DataTypeBinary]
        public byte[] BinValue;

        public static TVar GetVariable<TVar>(string ProductName, string RoleName,
            string ModuleName, string VarName, TVar DefaultValue)
        {
            RoleVariable Var = new RoleVariable();
            Var.ProductName = ProductName;
            Var.RoleName = RoleName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;

            if (!Var.LoadEntity(false)) return DefaultValue;

            Type tp = typeof(TVar);
            if (tp == typeof(Image))
            {
                if (Var.BinValue == null)
                    return (TVar)(object)null;
                else
                    return (TVar)(object)Helper.ConvertByteArrayToImage(
                        Var.BinValue);
            }
            else if (tp == typeof(byte[]))
                return (TVar)(object)Var.BinValue;
            else
                return BaseUtility.ConvertFromString<TVar>(
                    (string)Var.VarValue);
        }
        public static void SetVariable(string ProductName, string RoleName, 
            string ModuleName, string VarName, object VarValue)
        {
            RoleVariable Var = new RoleVariable();
            Var.ProductName = ProductName;
            Var.RoleName = RoleName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;
            Var.VarValue = string.Empty;
            if (VarValue == null)
                Var.BinValue = null;
            else
            {
                Type tp = VarValue.GetType();
                if (tp == typeof(Bitmap) ||
                    tp == typeof(Image))
                {
                    Helper.ConvertImageToByteArray((Image)VarValue);
                    Var.BinValue = Helper.ConvertImageToByteArray(
                        (Image)VarValue);
                }
                else if (tp == typeof(byte[]))
                    Var.BinValue = (byte[])VarValue;
                else
                    Var.VarValue = BaseUtility.ConvertToString(VarValue);
            }
            Var.Save();
        }
    }

    [TableName("_System_UserVariable")]
    [Relation(typeof(User), ParentUpdate.UpdateCascade, ParentDelete.DeleteCascade)]
    class UserVariable : ParentEntity
    {
        [DataTypeVarChar(50), PrimaryKey]
        public string UserName;
        [DataTypeVarChar(50), PrimaryKey]
        public string ModuleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string VarName;
        [DataTypeVarChar(2000)]
        public string VarValue;
        [DataTypeBinary]
        public byte[] BinValue;

        public static TVar GetVariable<TVar>(string UserName, 
            string ModuleName, string VarName, TVar DefaultValue)
        {
            UserVariable Var = new UserVariable();
            Var.UserName = UserName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;

            if (!Var.LoadEntity(false)) return DefaultValue;

            Type tp = typeof(TVar);
            if (tp == typeof(Image))
            {
                if (Var.BinValue == null)
                    return (TVar)(object)null;
                else
                    return (TVar)(object)Helper.ConvertByteArrayToImage(
                        Var.BinValue);
            }
            else if (tp == typeof(byte[]))
                return (TVar)(object)Var.BinValue;
            else
                return BaseUtility.ConvertFromString<TVar>(
                    (string)Var.VarValue);
        }
        public static void SetVariable(string UserName, 
            string ModuleName, string VarName, object VarValue)
        {
            DataPersistance dp = BaseFramework.GetDefaultDp();
            UserVariable Var = new UserVariable();

            Var.UserName = UserName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;
            Var.VarValue = string.Empty;
            if (VarValue == null)
                Var.BinValue = null;
            else
            {
                Type tp = VarValue.GetType();
                if (tp == typeof(Bitmap) ||
                    tp == typeof(Image))
                {
                    Helper.ConvertImageToByteArray((Image)VarValue);
                    Var.BinValue = Helper.ConvertImageToByteArray(
                        (Image)VarValue);
                }
                else if (tp == typeof(byte[]))
                    Var.BinValue = (byte[])VarValue;
                else
                    Var.VarValue = BaseUtility.ConvertToString(VarValue);
            }
            Var.Save();
        }
    }

    [TableName("_System_RoleUserVariable")]
    [Relation(typeof(Role), ParentUpdate.UpdateCascade, ParentDelete.DeleteCascade)]
    [Relation(typeof(User), ParentUpdate.UpdateCascade, ParentDelete.DeleteCascade)]
    class RoleUserVariable : ParentEntity
    {
        [DataTypeVarChar(50), PrimaryKey]
        public string ProductName;
        [DataTypeVarChar(50), PrimaryKey]
        public string RoleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string UserName;
        [DataTypeVarChar(50), PrimaryKey]
        public string ModuleName;
        [DataTypeVarChar(50), PrimaryKey]
        public string VarName;
        [DataTypeVarChar(2000)]
        public string VarValue;
        [DataTypeBinary]
        public byte[] BinValue;

        public static TVar GetVariable<TVar>(string ProductName, 
            string RoleName, string UserName,
            string ModuleName, string VarName, TVar DefaultValue)
        {
            RoleUserVariable Var = new RoleUserVariable();

            Var.ProductName = ProductName;
            Var.RoleName = RoleName;
            Var.UserName = UserName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;

            if (!Var.LoadEntity(false)) return DefaultValue;

            Type tp = typeof(TVar);
            if (tp == typeof(Image))
            {
                if (Var.BinValue == null)
                    return (TVar)(object)null;
                else
                    return (TVar)(object)Helper.ConvertByteArrayToImage(Var.BinValue);
            }
            else if (tp == typeof(byte[]))
                return (TVar)(object)Var.BinValue;
            else
                return BaseUtility.ConvertFromString<TVar>(
                    (string)Var.VarValue);
        }
        public static void SetVariable(string ProductName,
            string RoleName, string UserName, 
            string ModuleName, string VarName, object VarValue)
        {
            DataPersistance dp = BaseFramework.GetDefaultDp();
            RoleUserVariable Var = new RoleUserVariable();

            Var.ProductName = ProductName;
            Var.RoleName = RoleName;
            Var.UserName = UserName;
            Var.ModuleName = ModuleName;
            Var.VarName = VarName;
            Var.VarValue = string.Empty;
            if (VarValue == null)
                Var.BinValue = null;
            else
            {
                Type tp = VarValue.GetType();
                if (tp == typeof(Bitmap) ||
                    tp == typeof(Image))
                {
                    Helper.ConvertImageToByteArray((Image)VarValue);
                    Var.BinValue = Helper.ConvertImageToByteArray(
                        (Image)VarValue);
                }
                else if (tp == typeof(byte[]))
                    Var.BinValue = (byte[])VarValue;
                else
                    Var.VarValue = BaseUtility.ConvertToString(VarValue);
            }
            Var.Save();
        }
    }
}
