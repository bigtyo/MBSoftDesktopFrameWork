using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraUtility;
using SentraSolutionFramework.Entity;

namespace SentraSecurity.License
{
    public class Registration : PublishFieldEntity
    {
        private AppVariables Vars;
        private DataPersistance dp;

        private bool IsOldValidReg;

        private string _AppName;
        public string AppName
        {
            get { return _AppName; }
        }

        public string UserName;
        public string CompanyName;
        public int Limitation;
        public int MonthLimitation;

        private string _RegistrationNo;
        public string RegistrationNo
        {
            get { return _RegistrationNo; }
        }

        public string EngineName;
        public string ActivationCode;

        public bool IsValidActivationCode()
        {
            switch (Limitation)
            {
                case 0:
                    return true;
                case 1:
                    return ActivationCode.Equals(
                        HardwareIdentification.Pack(
                        string.Concat(_RegistrationNo, UserName, CompanyName,
                        Limitation.ToString(),
                        MonthLimitation.ToString(), EngineName)));
                default:
                    return ActivationCode.Equals(
                        HardwareIdentification.Pack(
                        string.Concat(_RegistrationNo, UserName, CompanyName,
                        Limitation.ToString(), EngineName)));
            }
        }

        public BindingList<ModuleRegistration> Modules;

        public bool Save()
        {
            if (IsOldValidReg && !IsValidActivationCode())
                return false;
            Vars.SetVariable("UserName", UserName);
            Vars.SetVariable("CompanyName", CompanyName);
            Vars.SetVariable("Limitation", Limitation);
            Vars.SetVariable("MonthLimitation", MonthLimitation);
            Vars.SetVariable(EngineName + _RegistrationNo, ActivationCode);

            dp.SetVariables(Vars);
            return IsValidActivationCode();
        }

        public Registration(string EngineName) 
            : this(BaseFramework.GetDefaultDp(), EngineName) { }
        public Registration(DataPersistance dp, string EngineName)
        {
            this.dp = dp;
            this.EngineName = EngineName;
            Vars = dp.GetVariables("License");

            _AppName = dp.GetVariable<string>("System", "AppName", 
                string.Empty);
            
            UserName = Vars.GetVariable<string>("UserName", 
                string.Empty);
            CompanyName = Vars.GetVariable<string>("CompanyName", 
                string.Empty);
            Limitation = Vars.GetVariable<int>("Limitation", 0);
            MonthLimitation = Vars.GetVariable<int>(
                "MonthLimitation", 6);
            _RegistrationNo = HardwareIdentification.Pack(
                HardwareIdentification.Value() + _AppName);
            ActivationCode = Vars.GetVariable<string>(
                EngineName + _RegistrationNo, string.Empty);
            if (Limitation < 0 || Limitation > 2) 
                Limitation = 0;
            IsOldValidReg = IsValidActivationCode() && Limitation != 0;
        }
    }

    public class ModuleRegistration
    {
        public string ModuleName;
        public string ModuleValue;
    }
}
