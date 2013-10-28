using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Data;
using SentraUtility;

namespace SentraSecurity
{
    public delegate void LogonChanged();

    public class SecurityLogin
    {
        private DataPersistance _Dp;
        public DataPersistance Dp
        {
            get { return _Dp ?? BaseFramework.DefaultDp; }
            set { _Dp = value; }
        }

        public SecurityLogin(DataPersistance DataPersistance)
        {
            this.Dp = DataPersistance;
        }

        public TVar GetRoleVariable<TVar>(string ProductName, string ModuleName, 
            string VarName, TVar DefaultValue)
        {
            return RoleVariable.GetVariable<TVar>(ProductName, _CurrentRole, 
                ModuleName, VarName, DefaultValue);
        }
        public void SetRoleVariable(string ProductName, string ModuleName, 
            string VarName, object Value)
        {
            RoleVariable.SetVariable(ProductName, _CurrentRole, ModuleName, 
                VarName, Value);
        }

        public TVar GetUserVariable<TVar>(string ModuleName, 
            string VarName, TVar DefaultValue)
        {
            string TmpUser = _CurrentRole.Length == 0 ? string.Empty : _CurrentUser;
            return UserVariable.GetVariable<TVar>(TmpUser, 
                ModuleName, VarName, DefaultValue);
        }
        public void SetUserVariable(string ModuleName, 
            string VarName, object Value)
        {
            string TmpUser = _CurrentRole.Length == 0 ? string.Empty : _CurrentUser;
            UserVariable.SetVariable(TmpUser, ModuleName, 
                VarName, Value);
        }

        public TVar GetRoleUserVariable<TVar>(string ProductName, string ModuleName, 
            string VarName, TVar DefaultValue)
        {
            string TmpUser = _CurrentRole.Length == 0 ? string.Empty : _CurrentUser;
            return RoleUserVariable.GetVariable<TVar>(ProductName, _CurrentRole, 
                TmpUser, ModuleName, VarName, DefaultValue);
        }
        public void SetRoleUserVariable(string ProductName, string ModuleName, 
            string VarName, object Value)
        {
            string TmpUser = _CurrentRole.Length == 0 ? string.Empty : _CurrentUser;
            RoleUserVariable.SetVariable(ProductName, _CurrentRole, TmpUser, 
                ModuleName, VarName, Value);
        }

        private string _CurrentUser = string.Empty;
        private string _CurrentRole = string.Empty;
        private string _CurrentConnection = string.Empty;
        private string _CurrentPassword = string.Empty;
        private bool _IsUserAdmin = false;

        public event LogonChanged onLogon;
        public event LogonChanged OnLogout;

        public bool IsUserAdmin { get { return _IsUserAdmin; } }
        public string CurrentUser { get { return _CurrentUser; } }
        public string CurrentRole { get { return _CurrentRole; } }
        public string CurrentPassword { get { return _CurrentPassword; } }
        public string CurrentConnection { get { return _CurrentConnection; } }

        public bool Login(string CurrentConnection, string RoleName,
            string UserName, string UserPassword)
        {
            Dp.ValidateTableDef<RoleUser>();
            Dp.ValidateTableDef<Role>();
            Dp.ValidateTableDef<User>();
            Dp.ValidateTableDef<UserVariable>();
            Dp.ValidateTableDef<RoleVariable>();
            Dp.ValidateTableDef<RoleUserVariable>();

            if (RoleUser.CanLogin(Dp, RoleName, UserName, UserPassword))
            {
                if (_CurrentUser.Length > 0 && OnLogout != null)
                {
                    string TmpUser = _CurrentUser;
                    _CurrentUser = string.Empty;
                    try
                    {
                        OnLogout();
                    }
                    catch (Exception ex)
                    {
                        _CurrentUser = TmpUser;
                        throw ex;
                    }
                }
                _CurrentUser = UserName;
                _CurrentPassword = UserPassword;
                _IsUserAdmin = User.IsUserAdmin(Dp, UserName);
                _CurrentConnection = CurrentConnection;

                IList<RoleModule> RAMList;

                if (BaseSecurity.LoginWithRole)
                {
                    RAMList = Dp.ListLoadEntities<RoleModule>(null,
                        "RoleName=@0 AND ProductName=@1",
                        string.Empty, false, new FieldParam("0", RoleName),
                        new FieldParam("1", BaseFramework.ProductName));
                    _CurrentRole = RoleName;
                }
                else
                {
                    RAMList = Dp.ListFastLoadEntitiesUsingSqlSelect<RoleModule>(null,
                        string.Concat(@"SELECT DISTINCT ModuleName, AllDocumentData, 
                        DataSecurity FROM _System_RoleModule rm 
                        INNER JOIN _System_RoleUser ru ON
                        rm.ProductName=ru.ProductName AND rm.RoleName=ru.RoleName
                        INNER JOIN _System_Role r ON
                        r.ProductName=rm.ProductName AND rm.RoleName=r.RoleName
                        WHERE r.IsActive=@0 AND (UseDateLimit=@1 OR ",
                        Dp.GetSqlDate(), @" BETWEEN r.StartDate AND r.EndDate) 
                        AND ru.UserName=@2 AND ru.ProductName=@3 AND ", 
                        Dp.GetSqlLen("DataSecurity"), ">0"),
                        string.Empty, false, new FieldParam("0", true),
                        new FieldParam("1", false), new FieldParam("2", UserName),
                        new FieldParam("3", BaseFramework.ProductName));
                    _CurrentRole = "(Peran Aktif)";
                }

                BaseSecurity.ClearModuleAccessList();

                foreach(RoleModule ram in RAMList)
                {
                    ModuleAccess ma = BaseSecurity.GetModuleAccess(ram.ModuleName);
                    if (ma != null)
                    {
                        Dictionary<string, object> TmpVars = new Dictionary<string,object>();
                        BaseUtility.String2Dictionary(ram.DataSecurity, TmpVars);
                        foreach (KeyValuePair<string, object> kvp in TmpVars)
                            if (kvp.Value.GetType() == typeof(bool))
                            {
                                if ((bool)kvp.Value)
                                    ma.Variables[kvp.Key] = true;
                                else if (!ma.Variables.ContainsKey(kvp.Key))
                                    ma.Variables[kvp.Key] = false;
                            }
                            else
                                ma.Variables[kvp.Key] = kvp.Value;
                        ma.AllDocumentData = ma.AllDocumentData || ram.AllDocumentData;
                    }
                }
            }
            else if (UserName == "Admin" &&
                !User.IsUserAdminExist(Dp))
            {
                if (_CurrentUser.Length > 0 && OnLogout != null)
                {
                    string TmpUser = _CurrentUser;
                    _CurrentUser = string.Empty;
                    try
                    {
                        OnLogout();
                    }
                    catch (Exception ex)
                    {
                        _CurrentUser = TmpUser;
                        throw ex;
                    }
                }
                _CurrentRole = string.Empty;
                _CurrentUser = UserName;
                _CurrentPassword = UserPassword;
                _IsUserAdmin = true;
                _CurrentConnection = CurrentConnection;

                foreach (ModuleAccess ma in BaseSecurity
                    .ModuleAccessList.Values)
                    ma.AllDocumentData = true;
            }
            else
                return false;

            Dp.ValidateTableDef<RoleModule>();
            Dp.ValidateTableDef<RoleModuleDocument>();

            foreach (BusinessEntity Service in BaseService.ListObjService
                .Values)
            {
                if (!Service.LoadEntity())
                    Service.SetDefaultValue();
            }

            if (onLogon != null) onLogon();
            return true;
        }
        public void Logout()
        {
            _CurrentConnection = string.Empty;
            _CurrentUser = string.Empty;
            _CurrentPassword = string.Empty;
            _CurrentRole = string.Empty;
            _IsUserAdmin = false;
            _Admin = null;
            BaseSecurity.ClearModuleAccessList();
            if (OnLogout != null) OnLogout();
        }

        public ModuleAccess GetModuleAccess(string ModuleName)
        {
            return BaseSecurity.GetModuleAccess(ModuleName);
        }

        public bool ChangePassword(string OldPassword, string NewPassword)
        {
            if (_CurrentRole.Length == 0) return false;

            TableDef tdUser = MetaData.GetTableDef(typeof(User));
            return Dp.ExecuteNonQuery(@"UPDATE _System_User SET 
UserPassword=@0 WHERE UserName=@1 AND UserPassword=@2",
                new FieldParam("0", NewPassword),
                new FieldParam("1", _CurrentUser),
                new FieldParam("2", OldPassword)) > 0;
        }
        public bool IsLogged()
        {
            return _CurrentUser.Length > 0;
        }

        public DataTable GetListRole(string UserName)
        {
            return RoleUser.GetListRole(Dp, UserName);
        }

        public DataTable GetListUser(string RoleName)
        {
            return RoleUser.GetListRole(Dp, RoleName);
        }
        public DataTable GetListUser()
        {
            return User.GetListUser(Dp);
        }
        public DataTable GetListRole(bool AllRole)
        {
            return Role.GetListRole(Dp, AllRole);
        }

        private SecurityAdmin _Admin;
        public SecurityAdmin Admin
        {
            get
            {
                if (!_IsUserAdmin) return null;
                if (_Admin == null) _Admin = new SecurityAdmin(this);
                return _Admin;
            }
        }

        public string GetSqlSelectDocumentVariableKey(
            string ModuleName, string VariableName)
        {
            return string.Concat(
                "SELECT DocumentKey FROM _System_RoleModuleDocument WHERE ProductName=",
                Dp.FormatSqlValue(BaseFramework.ProductName), 
                " AND RoleName=", Dp.FormatSqlValue(_CurrentRole),
                " AND ModuleName=", Dp.FormatSqlValue(ModuleName),
                " AND VariableName=", Dp.FormatSqlValue(VariableName));
        }
    }
}
