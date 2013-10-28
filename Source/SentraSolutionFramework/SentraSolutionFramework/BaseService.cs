using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using SentraSolutionFramework.Entity;

namespace SentraSolutionFramework
{
    [DebuggerNonUserCode]
    public static class BaseService
    {
        public static Dictionary<Type, object> ListObjService = 
            new Dictionary<Type, object>();

        public static void RegisterService<IService>
            (IService ObjService) where IService : BusinessEntity
        {
            ListObjService[typeof(IService)] = ObjService;
        }
        public static void RegisterService(Type IService, 
            object ObjService)
        {
            ListObjService[IService] = ObjService;
        }

        public static object Get(Type IService)
        {
            object RetObj;
            ListObjService.TryGetValue(IService, out RetObj);

            return RetObj;
        }

        public static IService Get<IService>() 
            where IService : BusinessEntity
        {
            object RetObj;
            ListObjService.TryGetValue(typeof(IService), out RetObj);

            return (IService)RetObj;
        }
    }
}
