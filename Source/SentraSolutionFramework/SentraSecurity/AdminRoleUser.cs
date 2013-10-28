using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;

namespace SentraSecurity
{
    public class AdminRoleUser
    {
        private SecurityLogin _LoginObj;

        internal AdminRoleUser(SecurityLogin LoginObj)
        { _LoginObj = LoginObj; }

        public void Add(string RoleName, string UserName)
        {
            RoleUser ru = new RoleUser();
            ru.Dp = _LoginObj.Dp;
            ru.ProductName = BaseFramework.ProductName;
            ru.RoleName = RoleName;
            ru.UserName = UserName;
            ru.SaveNew();
        }
        public void DeleteRole(string RoleName)
        {
            _LoginObj.Dp.ExecuteNonQuery(
                "DELETE FROM _System_RoleUser WHERE RoleName=@0 AND ProductName=@1",
                new FieldParam("0", RoleName),
                new FieldParam("1", BaseFramework.ProductName));
        }
        public void DeleteUser(string UserName)
        {
            _LoginObj.Dp.ExecuteNonQuery(
                "DELETE FROM _System_RoleUser WHERE UserName=@0 AND ProductName=@1",
                new FieldParam("0", UserName),
                new FieldParam("1", BaseFramework.ProductName));
        }
    }
}
