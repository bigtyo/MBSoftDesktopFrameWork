using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;

namespace SentraSecurity
{
    public class AdminRole
    {
        private SecurityLogin _LoginObj;

        internal AdminRole(SecurityLogin LoginObj)
        { _LoginObj = LoginObj; }

        public void Add(string RoleName, bool IsActive, 
            bool UseDateLimit, DateTime StartDate, DateTime EndDate)
        {
            Role r = new Role();
            r.Dp = _LoginObj.Dp;
            r.ProductName = BaseFramework.ProductName;
            r.RoleName = RoleName;
            r.IsActive = IsActive;
            r.UseDateLimit = UseDateLimit;
            r.StartDate = StartDate;
            r.EndDate = EndDate;
            r.SaveNew();
        }
        public void Delete(string RoleName)
        {
            Role r = new Role();
            r.Dp = _LoginObj.Dp;
            r.ProductName = BaseFramework.ProductName;
            r.RoleName = RoleName;
            r.SaveDelete();
        }
        public void Update(string OldRoleName,
            string NewRoleName, bool NewIsActive, bool NewUseDateLimit, 
            DateTime NewStartDate, DateTime NewEndDate)
        {
            Role r = new Role();
            r.Dp = _LoginObj.Dp;
            r.ProductName = BaseFramework.ProductName;
            r.RoleName = OldRoleName;
            r.LoadEntity();

            r.PrimaryKeyUpdateable = true;
            r.RoleName = NewRoleName;
            r.IsActive = NewIsActive;
            r.UseDateLimit = NewUseDateLimit;
            r.StartDate = NewStartDate;
            r.EndDate = NewEndDate;
            r.SaveUpdate();
        }

        public Role QueryRole(string RoleName)
        {
            Role r = new Role();
            r.Dp = _LoginObj.Dp;
            r.ProductName = BaseFramework.ProductName;
            r.RoleName = RoleName;

            return r.LoadEntity() ? r : null;
        }
    }
}
