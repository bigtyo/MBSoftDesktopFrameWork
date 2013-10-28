using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;

namespace Template
{
    public class DbObject1 : DbObject
    {
        public override string GetDDLCreate(DataPersistance Dp)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
