using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;

namespace SentraSecurity
{
    public class SecurityAdmin
    {
        public AdminUser User;
        public AdminRole Role;
        public AdminRoleUser RoleUser;
        public AdminRoleModule RoleModule;

        public TVar GetRoleVariable<TVar>(string ProductName, string RoleName, 
            string ModuleName, string VarName, TVar DefaultValue)
        {
            return RoleVariable.GetVariable<TVar>(ProductName, RoleName, 
                ModuleName, VarName, DefaultValue);
        }
        public void SetRoleVariable(string ProductName, string RoleName, 
            string ModuleName, string VarName, object Value)
        {
            RoleVariable.SetVariable(ProductName, RoleName, ModuleName, VarName, Value);
        }

        public TVar GetUserVariable<TVar>(string UserName, string ModuleName, 
            string VarName, TVar DefaultValue)
        {
            return UserVariable.GetVariable<TVar>(UserName, ModuleName, VarName, DefaultValue);
        }
        public void SetUserVariable(string UserName, string ModuleName, 
            string VarName, object Value)
        {
            UserVariable.SetVariable(UserName, ModuleName, VarName, Value);
        }

        public TVar GetRoleUserVariable<TVar>(string ProductName, string RoleName, 
            string UserName, string ModuleName, string VarName, TVar DefaultValue)
        {
            return RoleUserVariable.GetVariable<TVar>(ProductName, RoleName, UserName, ModuleName, VarName, DefaultValue);
        }
        public void SetRoleUserVariable(string ProductName, string RoleName, 
            string UserName, string ModuleName, string VarName, object Value)
        {
            RoleUserVariable.SetVariable(ProductName, RoleName, UserName, ModuleName, VarName, Value);
        }

        internal SecurityAdmin(SecurityLogin LoginObj)
        {
            User = new AdminUser(LoginObj);
            Role = new AdminRole(LoginObj);
            RoleUser = new AdminRoleUser(LoginObj);
            RoleModule = new AdminRoleModule(LoginObj);
        }
    }

    public class AdminUser
    {
        SecurityLogin _LoginObj;

        internal AdminUser(SecurityLogin LoginObj)
        {
            _LoginObj = LoginObj;
        }

        public void ResetPassword(string UserName)
        {
            _LoginObj.Dp.ExecuteNonQuery(
                "UPDATE _System_User SET UserPassword=@0 WHERE UserName=@1",
                new FieldParam("0", string.Empty),
                new FieldParam("1", UserName));
        }

        public void Add(string UserName, string UserPassword, bool IsActive, 
            bool IsAdmin, bool UseDateLimit, DateTime StartDate, 
            DateTime EndDate)
        {
            User u = new User();
            u.Dp = _LoginObj.Dp;
            u.UserName = UserName;
            u.UserPassword = UserPassword;
            u.IsActive = IsActive;
            u.IsAdmin = IsAdmin;
            u.UseDateLimit = UseDateLimit;
            u.StartDate = StartDate;
            u.EndDate = EndDate;
            u.SaveNew();
        }
        public void Delete(string UserName)
        {
            User u = new User();
            u.Dp = _LoginObj.Dp;
            u.UserName = UserName;
            u.SaveDelete();
        }
        public void Update(string OldUserName,
            string NewUserName, bool NewIsActive, bool NewIsAdmin,
            bool NewUseDateLimit, DateTime StartDate, DateTime EndDate)
        {
            User u = new User();
            u.Dp = _LoginObj.Dp;
            u.UserName = OldUserName;
            if (u.LoadEntity())
            {
                u.PrimaryKeyUpdateable = true;
                u.UserName = NewUserName;
                u.IsActive = NewIsActive;
                u.IsAdmin = NewIsAdmin;
                u.UseDateLimit = NewUseDateLimit;
                u.StartDate = StartDate;
                u.EndDate = EndDate;
                u.SaveUpdate();
            }
        }

        public User QueryUser(string UserName)
        {
            User u = new User();
            u.Dp = _LoginObj.Dp;
            u.UserName = UserName;
            return u.LoadEntity() ? u : null;
        }
    }
}
