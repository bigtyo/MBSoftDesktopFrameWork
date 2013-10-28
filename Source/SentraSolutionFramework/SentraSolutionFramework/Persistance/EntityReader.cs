using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using System.Data;
using SentraSolutionFramework.Entity;
using SentraUtility;
using System.Reflection;

namespace SentraSolutionFramework.Persistance
{
    public class EntityReader<TEntity> : IDisposable
        where TEntity : new()
    {
        private bool ReadOnNewEntity;
        private DataPersistance Dp;
        private IDataReader rdr;
        private string SqlSelect;
        private FieldParam[] Parameters;
        private TEntity _Entity;
        private SetHandler[] Setter;
        private Type[] EnumType;

        public TEntity Entity
        {
            get { return _Entity; }
        }

        public EntityReader(DataPersistance Dp, string SqlSelect, 
            bool ReadOnNewEntity, params FieldParam[] Parameters)
        {
            this.Dp = Dp;
            this.SqlSelect = SqlSelect;
            this.Parameters = Parameters;
            this.ReadOnNewEntity = ReadOnNewEntity;
            if (!ReadOnNewEntity)
                _Entity = new TEntity();
        }
        public EntityReader(DataPersistance Dp, string SqlSelect,
            params FieldParam[] Parameters) :
            this(Dp, SqlSelect, false, Parameters) { }

        public EntityReader(DataPersistance Dp, string FieldSelect,
            string Condition, string OrderBy, bool ReadOnNewEntity,
            params FieldParam[] Parameters)
        {
            this.Dp = Dp;
            this.SqlSelect = MetaData.GetTableDef(typeof(TEntity))
                .GetSqlSelect(Dp, FieldSelect, Condition, OrderBy);
            this.Parameters = Parameters;
            this.ReadOnNewEntity = ReadOnNewEntity;
            if (!ReadOnNewEntity)
                _Entity = new TEntity();
        }

        public EntityReader(DataPersistance Dp, string FieldSelect,
            string Condition, string OrderBy, params FieldParam[] Parameters) :
            this(Dp, FieldSelect, Condition, OrderBy, false, Parameters) { }

        private void BuildSetter()
        {
            Setter = new SetHandler[rdr.FieldCount];
            EnumType = new Type[rdr.FieldCount];

            Type tp = typeof(TEntity);
            TableDef td = MetaData.GetTableDef(tp);
            for (int i = 0; i < rdr.FieldCount; i++)
            {
                string Name = rdr.GetName(i);
                FieldDef fld = td.GetFieldDef(Name);
                if (fld != null)
                {
                    Setter[i] = fld.GetLoadSetHandler();
                    if (fld.IsEnum)
                        EnumType[i] = fld.FieldType;
                }
                else
                {
                    MemberInfo[] mis = tp.GetMember("_" + Name, 
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (mis.Length == 0)
                        mis = tp.GetMember(Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (mis.Length == 0)
                        Setter[i] = null;
                    else
                    {
                        if (mis[0].MemberType == MemberTypes.Field)
                        {
                            Type ft = ((FieldInfo)mis[0]).FieldType;
                            if (ft.IsEnum) EnumType[i] = ft;
                        }
                        else
                        {
                            Type ft = ((PropertyInfo)mis[0]).PropertyType;
                            if (ft.IsEnum) EnumType[i] = ft;
                        }
                        Setter[i] = DynamicMethodCompiler.CreateSetHandler(mis[0]);
                    }
                }
            }
        }

        public bool Read()
        {
            if (rdr == null)
            {
                rdr = Dp.ExecuteReader(SqlSelect, Parameters);
                BuildSetter();
            }
            if (rdr.Read())
            {
                if (ReadOnNewEntity)
                    _Entity = new TEntity();

                for (int i = 0; i < Setter.Length; i++)
                    if (Setter[i] != null)
                    {
                        object obj = rdr.GetValue(i);
                        if (obj == null || obj == DBNull.Value) obj = GetDefaultValue(rdr.GetFieldType(i));
                        if (EnumType[i] == null)
                            Setter[i](_Entity, obj);
                        else
                            Setter[i](_Entity, EnumDef.GetEnumValue(
                                EnumType[i], obj));
                    }

                return true;
            }
            return false;
        }

        private object GetDefaultValue(Type tp)
        {
            if(tp == typeof(string))
                return string.Empty;
            if (tp == typeof(int))
                return 0;
            if (tp == typeof(Single) || tp == typeof(decimal))
                return 0d;
            if (tp == typeof(bool))
                return true;
            if (tp == typeof(DateTime))
                return new DateTime(1900, 1, 1);
            return null;
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            if (rdr != null && !rdr.IsClosed) rdr.Close();
        }

        #endregion
    }
}
