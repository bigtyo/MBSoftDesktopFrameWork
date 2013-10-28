using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections;
using SentraUtility;

namespace SentraSolutionFramework
{
    [DebuggerNonUserCode]
    public static class BaseFactory
    {
        private static Dictionary<Type, Type> ListObjType = new Dictionary<Type, Type>();
        private static Dictionary<Type, InstantiateObjectHandler> ListNewType =
            new Dictionary<Type, InstantiateObjectHandler>();

        public static void RegisterObjType<TObject, TReplacing>()
        {
            ListObjType[typeof(TObject)] = typeof(TReplacing);
        }
        public static void RegisterObjType(Type ObjectType, Type ReplacingType)
        {
            ListObjType[ObjectType] = ReplacingType;
        }

        public static Type GetObjType(Type ObjectType)
        {
            Type RetType;
            if (ListObjType.TryGetValue(ObjectType, out RetType))
                return GetObjType(RetType);
            else
                return ObjectType;
        }
        
        public static TObject CreateInstance<TObject>(params object[] args)
        {
            Type NewType = GetObjType(typeof(TObject));

            if (args == null)
            {
                InstantiateObjectHandler NewMethod;
                if (!ListNewType.TryGetValue(NewType, out NewMethod)) 
                {
                    NewMethod = DynamicMethodCompiler.CreateInstantiateObjectHandler(NewType);
                    ListNewType.Add(NewType, NewMethod);
                }
                return (TObject)NewMethod();
            }
            else
                return (TObject)Activator.CreateInstance(NewType, args);

        }
        public static object CreateInstance(Type ObjectType, params object[] args)
        {
            Type NewType = GetObjType(ObjectType);

            if (args == null)
            {
                InstantiateObjectHandler NewMethod;
                if (!ListNewType.TryGetValue(NewType, out NewMethod)) 
                {
                    NewMethod = DynamicMethodCompiler.CreateInstantiateObjectHandler(NewType);
                    ListNewType.Add(NewType, NewMethod);
                }
                return NewMethod();
            }
            else
                return Activator.CreateInstance(NewType, args);
        }
    }
}
