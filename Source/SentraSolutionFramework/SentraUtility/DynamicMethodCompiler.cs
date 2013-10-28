using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;

namespace SentraUtility
{
    public delegate object GetHandler(object source);
    public delegate void SetHandler(object source, object value);
    public delegate object InstantiateObjectHandler();
    public delegate object InvokeHandler(object source, params object[] paramters);

    [DebuggerNonUserCode]
    public static class DynamicMethodCompiler
    {
        public static InstantiateObjectHandler CreateInstantiateObjectHandler(Type type)
        {
            ConstructorInfo constructorInfo = type.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | 
                BindingFlags.Instance, null, new Type[0], null);
            if (constructorInfo == null)
                return null;

            DynamicMethod dynamicMethod = new DynamicMethod("io", 
                MethodAttributes.Static | MethodAttributes.Public, 
                CallingConventions.Standard, typeof(object), null, type, true);
            ILGenerator generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Ret);
            return (InstantiateObjectHandler)dynamicMethod.CreateDelegate(
                typeof(InstantiateObjectHandler));
        }

        #region CreateGetHandler
        public static GetHandler CreateGetHandler(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Property)
                return CreateGetHandler((PropertyInfo)memberInfo);
            else
                return CreateGetHandler((FieldInfo)memberInfo);
        }
        public static GetHandler CreateGetHandler(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null || !propertyInfo.CanRead) return null;

            MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
            DynamicMethod dynamicGet = CreateGetDynamicMethod(propertyInfo.DeclaringType);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Call, getMethodInfo);
            BoxIfNeeded(getMethodInfo.ReturnType, getGenerator);
            getGenerator.Emit(OpCodes.Ret);

            return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
        }
        public static GetHandler CreateGetHandler(FieldInfo fieldInfo)
        {
            if (fieldInfo == null) return null;

            DynamicMethod dynamicGet = CreateGetDynamicMethod(fieldInfo.DeclaringType);
            ILGenerator getGenerator = dynamicGet.GetILGenerator();

            getGenerator.Emit(OpCodes.Ldarg_0);
            getGenerator.Emit(OpCodes.Ldfld, fieldInfo);
            BoxIfNeeded(fieldInfo.FieldType, getGenerator);
            getGenerator.Emit(OpCodes.Ret);

            return (GetHandler)dynamicGet.CreateDelegate(typeof(GetHandler));
        }
        #endregion

        #region CreateSetHandler
        public static SetHandler CreateSetHandler(MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Property)
                return CreateSetHandler((PropertyInfo)memberInfo);
            else
                return CreateSetHandler((FieldInfo)memberInfo);
        }
        public static SetHandler CreateSetHandler(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null || !propertyInfo.CanWrite) return null;

            MethodInfo setMethodInfo = propertyInfo.GetSetMethod(true);
            DynamicMethod dynamicSet = CreateSetDynamicMethod(propertyInfo.DeclaringType);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(setMethodInfo.GetParameters()[0].ParameterType, 
                setGenerator);
            setGenerator.Emit(OpCodes.Call, setMethodInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
        }
        public static SetHandler CreateSetHandler(FieldInfo fieldInfo)
        {
            if (fieldInfo == null) return null;

            DynamicMethod dynamicSet = CreateSetDynamicMethod(fieldInfo.DeclaringType);
            ILGenerator setGenerator = dynamicSet.GetILGenerator();

            setGenerator.Emit(OpCodes.Ldarg_0);
            setGenerator.Emit(OpCodes.Ldarg_1);
            UnboxIfNeeded(fieldInfo.FieldType, setGenerator);
            setGenerator.Emit(OpCodes.Stfld, fieldInfo);
            setGenerator.Emit(OpCodes.Ret);

            return (SetHandler)dynamicSet.CreateDelegate(typeof(SetHandler));
        }
        #endregion

        public static InvokeHandler CreateMethodInvoker(
            MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty,
                             typeof(object), new Type[] { 
                                 typeof(object), typeof(object[]) },
                             methodInfo.DeclaringType.Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
                paramTypes[i] = ps[i].ParameterType;
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i]);
            }
            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(i, il);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(paramTypes[i], il);
                il.Emit(OpCodes.Stloc, locals[i]);
            }
            il.Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < paramTypes.Length; i++)
                il.Emit(OpCodes.Ldloc, locals[i]);
            il.EmitCall(OpCodes.Call, methodInfo, null);
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                BoxIfNeeded(methodInfo.ReturnType, il);
            il.Emit(OpCodes.Ret);
            InvokeHandler invoder =
              (InvokeHandler)dynamicMethod.CreateDelegate(
              typeof(InvokeHandler));
            return invoder;
        }

        #region Private Methods
        private static void EmitFastInt(int value, ILGenerator ilGenerator)
        {
            // for small integers, emit the proper opcode
            switch (value)
            {
                case -1:
                    ilGenerator.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    ilGenerator.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    ilGenerator.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    ilGenerator.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    ilGenerator.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    ilGenerator.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    ilGenerator.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    ilGenerator.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            // for bigger values emit the short or long opcode
            if (value > -129 && value < 128)
                ilGenerator.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            else
                ilGenerator.Emit(OpCodes.Ldc_I4, value);
        }

        private static void EmitCastToReference(Type type, 
            ILGenerator ilGenerator)
        {
            if (type.IsValueType)
                ilGenerator.Emit(OpCodes.Unbox_Any, type);
            else
                ilGenerator.Emit(OpCodes.Castclass, type);
        }

        private static DynamicMethod CreateGetDynamicMethod(Type type)
        {
            return new DynamicMethod(string.Empty, typeof(object), 
                new Type[] { typeof(object) }, type, true);
        }
        private static DynamicMethod CreateSetDynamicMethod(Type type)
        {
            return new DynamicMethod(string.Empty, typeof(void), 
                new Type[] { typeof(object), typeof(object) }, type, true);
        }

        private static void BoxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
                generator.Emit(OpCodes.Box, type);
        }
        private static void UnboxIfNeeded(Type type, ILGenerator generator)
        {
            if (type.IsValueType)
                generator.Emit(OpCodes.Unbox_Any, type);
        }
        #endregion
    }
}
