using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;

namespace SentraSecurity
{
    public class AdminRoleModule
    {
        private SecurityLogin _LoginObj;

        internal AdminRoleModule(SecurityLogin LoginObj)
        { 
            _LoginObj = LoginObj;
        }

        public void Add(string RoleName, string ModuleName, 
            string DataSecurity, bool AllDocumentData)
        {
            RoleModule rm = new RoleModule();
            rm.Dp = _LoginObj.Dp;
            rm.ProductName = BaseFramework.ProductName;
            rm.RoleName = RoleName;
            rm.ModuleName = ModuleName;
            rm.DataSecurity = DataSecurity;
            rm.AllDocumentData = AllDocumentData;
            rm.SaveNew();
        }
        public void Delete(string RoleName, string ModuleName)
        {
            RoleModule rm = new RoleModule();
            rm.Dp = _LoginObj.Dp;
            rm.ProductName = BaseFramework.ProductName;
            rm.RoleName = RoleName;
            rm.ModuleName = ModuleName;
            rm.SaveDelete();
        }
        public void Update(string RoleName,
            string ModuleName, string NewDataSecurity, bool AllDocumentData)
        {
            RoleModule rm = new RoleModule();
            rm.Dp = _LoginObj.Dp;
            rm.ProductName = BaseFramework.ProductName;
            rm.RoleName = RoleName;
            rm.ModuleName = ModuleName;
            rm.DataSecurity = NewDataSecurity;
            rm.AllDocumentData = AllDocumentData;
            rm.Save();
        }

        public IDataReader OpenDataReader(string RoleName)
        {
            return _LoginObj.Dp.ExecuteReader(string.Concat(
                @"SELECT ModuleName,DataSecurity,AllDocumentData FROM _System_RoleModule 
                WHERE RoleName=@0 AND ProductName=@1 AND ",
                _LoginObj.Dp.GetSqlLen("DataSecurity"), ">0"),
                new FieldParam("0", RoleName), new FieldParam("1", BaseFramework.ProductName));
        }

        public void UpdateDocumentVariable(string RoleName, 
            string ModuleName, string VariableName, 
            string DocumentName, List<string> ListDocKey)
        {
            RoleModuleDocument rmd = new RoleModuleDocument();
            rmd.Dp = _LoginObj.Dp;
            rmd.ProductName = BaseFramework.ProductName;
            rmd.RoleName = RoleName;
            rmd.ModuleName = ModuleName;
            rmd.VariableName = VariableName;
            rmd.DocumentName = DocumentName;

            using (EntityTransaction tr = new EntityTransaction(rmd.Dp))
            {
                _LoginObj.Dp.ExecuteNonQuery(@"DELETE FROM 
                    _System_RoleModuleDocument WHERE RoleName=@0 AND ModuleName=@1 AND 
                    VariableName=@2 AND ProductName=@3", 
                    new FieldParam("0", RoleName),
                    new FieldParam("1", ModuleName),
                    new FieldParam("2", VariableName),
                    new FieldParam("3", BaseFramework.ProductName));
                foreach (string DocumentKey in ListDocKey)
                {
                    rmd.DocumentKey = DocumentKey;
                    rmd.SaveNew();
                }
                tr.CommitTransaction();
            }
        }
        public IDataReader GetListDocumentVariable(string RoleName,
            string ModuleName, string VariableName)
        {
            return _LoginObj.Dp.ExecuteReader(@"SELECT DocumentKey FROM 
                _System_RoleModuleDocument WHERE RoleName=@0 AND ModuleName=@1 AND 
                VariableName=@2 AND ProductName=@3", new FieldParam("0", RoleName),
                    new FieldParam("1", ModuleName),
                    new FieldParam("2", VariableName),
                    new FieldParam("3", BaseFramework.ProductName));
        }
    }
}
