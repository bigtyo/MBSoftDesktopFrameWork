using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Data;
using SentraUtility;
using System.Diagnostics;

namespace SentraSecurity
{
    public class ModuleDataField
    {
        public string DataFieldName;
        public string EntityName;
        public string SqlQuery;

        public ModuleDataField(string DataFieldName,
            string EntityName, string SqlQuery)
        {
            this.DataFieldName = DataFieldName;
            this.EntityName = EntityName;
            this.SqlQuery = SqlQuery;
        }

        public ModuleDataField(string DataFieldName,
            Type EntityType, string SqlQuery)
        {
            this.DataFieldName = DataFieldName;
            this.EntityName = EntityType.Name;
            this.SqlQuery = SqlQuery;
        }
    }

    public class ModuleAccess
    {
        public string ModuleName;
        public string FolderName;
        public bool AllDocumentData;

        public Type FormSettingType;

        public Dictionary<string, object> Variables;

        public ModuleAccess(string ModuleName, string FolderName)
            : this(ModuleName, FolderName, null) { }
        public ModuleAccess(string ModuleName, string FolderName,
            Type FormSettingType)
        {
            this.ModuleName = ModuleName;
            this.FolderName = FolderName;
            this.FormSettingType = FormSettingType;
            Variables = new Dictionary<string, object>();
        }

        public List<ModuleDataField> ListDataField = 
            new List<ModuleDataField>();

        /// <summary>
        /// Variabel Standar :
        /// Dokumen : Akses Menu, Baru, Edit, Hapus, Lihat, Lihat Daftar, Cetak, Desain Cetak
        /// Laporan :
        /// </summary>
        /// <typeparam name="TVarType"></typeparam>
        /// <param name="VarName"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public TVarType GetVariable<TVarType>(string VarName, 
            TVarType DefaultValue)
        {
            object RetVal;

            if (Variables.TryGetValue(VarName, out RetVal))
                return (TVarType)RetVal;
            else
                return DefaultValue;
        }
        public void SetVariable<TVarType>(string VarName, TVarType Value)
        {
            Variables[VarName] = Value;
        }

        public override string ToString()
        {
            string RetVal = string.Empty;
            foreach (KeyValuePair<string, object> kvp in Variables)
            {
                if (kvp.Value.GetType() == typeof(bool))
                {
                    if ((bool)kvp.Value)
                        RetVal = string.Concat(RetVal, ", ", kvp.Key);
                }
                else if (kvp.Value != null)
                    RetVal = string.Concat(RetVal, ", [", kvp.Key, "]");
            }
            return RetVal.Length > 0 ? RetVal.Substring(2) : string.Empty;
        }
    }

    public static class BaseSecurity
    {
        public static SecurityLogin CurrentLogin = new SecurityLogin(null);

        public static bool LoginWithRole = true;

        static BaseSecurity()
        {
            EnableLog = true;
            HardwareIdentification.Value();
        }

        #region Log Management
        private static bool _EnableLog;
        public static bool EnableLog
        {
            get { return _EnableLog; }
            set
            {
                _EnableLog = value && 
                    !BaseUtility.IsDebugMode;

                if (_EnableLog)
                    BaseFramework.onEntityAction += new EntityAction(BaseFramework_onEntityAction);
                else
                    BaseFramework.onEntityAction -= new EntityAction(BaseFramework_onEntityAction);
            }
        }

        static void BaseFramework_onEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            TableDef td;
            EnableLogAttribute logAtr;

            switch (ActionMode)
            {
                case enEntityActionMode.AfterSaveNew:
                    td = MetaData.GetTableDef(ActionEntity.GetType());
                    logAtr = UserLog.GetLogAtr(td.ClassType);
                    if ((logAtr.LogType & enLogType.LogAdd) == enLogType.LogAdd)
                        UserLog.AddLog(td.ClassType.Name, UserLog.StrNewAction,
                            td.GetDocumentId(ActionEntity), string.Empty);
                    break;
                case enEntityActionMode.AfterSaveUpdate:
                    td = MetaData.GetTableDef(ActionEntity.GetType());
                    logAtr = UserLog.GetLogAtr(td.ClassType);
                    if ((logAtr.LogType & enLogType.LogEdit) == enLogType.LogEdit)
                        UserLog.AddLog(td.ClassType.Name, UserLog.StrEditAction,
                            td.GetDocumentId(ActionEntity), string.Empty);
                    break;
                case enEntityActionMode.AfterSaveDelete:
                    td = MetaData.GetTableDef(ActionEntity.GetType());
                    logAtr = UserLog.GetLogAtr(td.ClassType);
                    if ((logAtr.LogType & enLogType.LogDelete) == enLogType.LogDelete)
                        UserLog.AddLog(td.ClassType.Name, UserLog.StrDeleteAction,
                            td.GetDocumentId(ActionEntity), string.Empty);
                    break;
            }
        }
        #endregion

        #region ModuleAccess
        public static Dictionary<string, ModuleAccess> ModuleAccessList =
            new Dictionary<string, ModuleAccess>();

        public static void ClearModuleAccessList()
        {
            foreach (ModuleAccess ma in ModuleAccessList.Values)
            {
                ma.Variables.Clear();
                ma.AllDocumentData = false;
            }
        }
        public static ModuleAccess RegisterModuleAccess<TFormSettingModuleAccess>(
            string ModuleName, string FolderName)
            where TFormSettingModuleAccess : IModuleAccessForm
        {
            ModuleAccess RetVal = new ModuleAccess(ModuleName,
                FolderName, typeof(TFormSettingModuleAccess));
            ModuleAccessList.Add(ModuleName, RetVal);
            return RetVal;
        }
        public static ModuleAccess GetModuleAccess(string ModuleName)
        {
            ModuleAccess RetVal;
            
            ModuleAccessList.TryGetValue(ModuleName, out RetVal);
            return RetVal;
        }
        #endregion

        public static void UpdateDocument(Type EntityType, 
            string OldKey, string NewKey)
        {
            UpdateDocument(EntityType.Name, OldKey, NewKey);
        }
        public static void UpdateDocument(string DocName, 
            string OldKey, string NewKey)
        {
            if (OldKey == NewKey) return;

            CurrentLogin.Dp.ExecuteNonQuery(@"UPDATE 
                _System_RoleModuleDocument SET DocumentKey=@0 WHERE 
                DocumentName=@1 AND DocumentKey=@2 AND ProductName=@3",
                new FieldParam("0", NewKey),
                new FieldParam("1", DocName),
                new FieldParam("2", OldKey),
                new FieldParam("3", BaseFramework.ProductName));
        }

        public static void DeleteDocument(Type EntityType, 
            string DocKey)
        {
            DeleteDocument(EntityType.Name, DocKey);
        }
        public static void DeleteDocument(string DocName, 
            string DocKey)
        {
            CurrentLogin.Dp.ExecuteNonQuery(@"DELETE FROM 
                _System_RoleModuleDocument WHERE DocumentName=@0 
                AND DocumentKey=@1 AND ProductName=@2",
                new FieldParam("0", DocName),
                new FieldParam("1", DocKey),
                new FieldParam("2", BaseFramework.ProductName));
        }
    }
}