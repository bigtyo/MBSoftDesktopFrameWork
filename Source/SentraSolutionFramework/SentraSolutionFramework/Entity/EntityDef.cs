using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections;
using SentraSolutionFramework.Persistance;
using System.Diagnostics;
using System.Drawing;
using SentraUtility;
using System.Data;

namespace SentraSolutionFramework.Entity
{
    [DebuggerNonUserCode]
    internal sealed class DataTypeAtr : DataTypeAttribute
    {
        public DataTypeAtr(DataType DataType,
            int Length, int Scale)
        {
            _DataType = DataType;
            _Length = Length;
            _Scale = Scale;
            this.FormatString = string.Empty;
        }
    }

    internal enum MemberType
    {
        PropertyOnRun,
        FieldOnRun,
        PropertyOnLoadAndRun
    }

    //[DebuggerNonUserCode]
    public sealed class FieldDef
    {
        private MemberType _MemberType;

        private GetHandler GetPropMethod;
        private SetHandler SetPropMethod;
        private GetHandler GetFieldMethod;
        private SetHandler SetFieldMethod;

        public string Description = string.Empty;

        public EmptyErrorAttribute EmptyErrorAtr;
        
        public bool IsEnum;
        public ICollection GetEnumNames()
        {
            return EnumDef.GetEnumNames(FieldType);
        }

        public bool IsEmpty(BusinessEntity Entity)
        {
            if (IsEnum) return false;
            switch (_DataTypeAtr._DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    return ((string)GetValue(Entity)).Trim().Length == 0;
                case DataType.Integer:
                    return ((int)GetValue(Entity)) == 0;
                case DataType.Decimal:
                    return ((decimal)GetValue(Entity)) == 0;
                case DataType.Image:
                case DataType.Binary:
                    return GetValue(Entity) == null;
                case DataType.Boolean:
                    return ((bool)GetValue(Entity)) == false;
                default:
                    return false;
            }
        }

        public Type FieldType;

        internal bool _IsParentField;
        internal bool _IsPrimaryKey;

        internal DataTypeAttribute _DataTypeAtr;
        public DataTypeAttribute DataTypeAtr { get { return _DataTypeAtr; } }

        internal bool _IsReadOnly;
        internal DataTypeLoadSqlAttribute _dtlsa;
        internal bool _IsPublic;

        public bool IsBrowseHidden
        {
            get
            {
                return _dtlsa == null ?
                    _DataTypeAtr.BrowseHidden : _dtlsa.BrowseHidden;
            }
            set
            {
                if (_dtlsa == null) _DataTypeAtr.BrowseHidden = value;
                else _dtlsa.BrowseHidden = value;
            }
        }

        public bool IsFormHidden
        {
            get
            {
                return _dtlsa == null ?
                    _DataTypeAtr.FormHidden : _dtlsa.FormHidden;
            }
            set
            {
                if (_dtlsa == null) 
                    _DataTypeAtr.FormHidden = value;
                else 
                    _dtlsa.FormHidden = value;
            }
        }

        /// <summary>
        /// Is Hidden on Form and Browse Mode
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return IsBrowseHidden && IsFormHidden;
            }
            set
            {
                IsBrowseHidden = value;
                IsFormHidden = value;
            }
        }

        public bool IsPublic { get { return _IsPublic; } }

        public DataTypeLoadSqlAttribute GetDTLSA()
        { return _dtlsa; }

        internal string _FieldName;
        public string FieldName
        {
            get { return _FieldName; }
        }

        public DataType DataType { get { return _DataTypeAtr._DataType; } }
        public int Length { get { return _DataTypeAtr._Length; } }
        public int Scale { get { return _DataTypeAtr._Scale; } }
        public string FormatString
        {
            get { return _DataTypeAtr.FormatString; }
            set { _DataTypeAtr.FormatString = value; }
        }
        public bool IsPrimaryKey { get { return _IsPrimaryKey; } }
        public bool IsParentField { get { return _IsParentField; } }
        public bool IsReadOnly { get { return _IsReadOnly; } }
        public object GetDataTypeDefault()
        {
            switch (_DataTypeAtr._DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    return string.Empty;
                case DataType.Boolean:
                    return true;
                case DataType.Decimal:
                    return 0m;
                case DataType.Integer:
                    return 0;
                case DataType.Date:
                    return BaseFramework.DefaultDp.GetDbDate();
                case DataType.DateTime:
                case DataType.TimeStamp:
                case DataType.Time:
                    return BaseFramework.DefaultDp.GetDbDateTime();
                default:
                    return null;
            }
        }
        public object GetDefaultValue()
        {
            if (_DataTypeAtr.Default == null)
            {
                switch (_DataTypeAtr._DataType)
                {
                    case DataType.VarChar:
                    case DataType.Char:
                        return string.Empty;
                    case DataType.Boolean:
                        return true;
                    case DataType.Decimal:
                        return (decimal)0;
                    case DataType.Integer:
                        return 0;
                    case DataType.Date:
                        return BaseFramework.DefaultDp.GetDbDate();
                    case DataType.DateTime:
                    case DataType.Time:
                    case DataType.TimeStamp:
                        return BaseFramework.DefaultDp.GetDbDateTime();
                    default:
                        return null;
                }
            }
            switch (_DataTypeAtr._DataType)
            {
                case DataType.Date:
                case DataType.DateTime:
                case DataType.Time:
                case DataType.TimeStamp:
                    if (_DataTypeAtr.Default.GetType() == typeof(string))
                        switch((string)_DataTypeAtr.Default) 
                        {
                            case "Today":
                                return BaseFramework.DefaultDp.GetDbDate();
                            case "Now":
                                return BaseFramework.DefaultDp.GetDbDateTime(); ;
                            default:
                                return DateTime.Parse((string)_DataTypeAtr.Default,
                                    BaseUtility.DefaultCultureInfo);
                        }
                    break;
            }
            return _DataTypeAtr.Default;
        }
        public object GetValue(BaseEntity Entity)
        {
            if (Entity == null) return null;
            object RetVal;
            if (_IsParentField)
            {
                Entity = ((BusinessEntity)Entity).GetParentEntity();
                RetVal = MetaData.GetTableDef(Entity.GetType())
                    .GetFieldDef(_FieldName).GetValue(Entity);
            }
            else
            {
                if (_MemberType == MemberType.FieldOnRun)
                    RetVal = GetFieldMethod(Entity);
                else if (GetPropMethod != null)
                    RetVal = GetPropMethod(Entity);
                else
                    RetVal = _pi.GetValue(Entity, null);
            }
            return RetVal ?? GetDefaultValue();
        }
        public void SetValue(BaseEntity Entity, object newValue)
        {
            if (_IsReadOnly || _IsParentField) return;
            if (newValue == null || newValue == DBNull.Value)
                SetDefault(Entity, true);
            else
            {
                if (_DataTypeAtr._DataType == DataType.Image &&
                    newValue.GetType() == typeof(byte[]))
                {
                    if (_MemberType == MemberType.FieldOnRun)
                        SetFieldMethod(Entity, Helper.ConvertByteArrayToImage(
                            (byte[])newValue));
                    else if (SetPropMethod != null)
                        SetPropMethod(Entity, Helper.ConvertByteArrayToImage(
                            (byte[])newValue));
                    else if (_pi != null)
                        _pi.SetValue(Entity, Helper.ConvertByteArrayToImage(
                            (byte[])newValue), null);
                }
                else
                {
                    if (_MemberType == MemberType.FieldOnRun)
                    {
                        if (!IsEnum)
                            SetFieldMethod(Entity, newValue);
                        else
                            SetFieldMethod(Entity, EnumDef.GetEnumValue(FieldType, newValue));
                    }
                    else if (SetPropMethod != null)
                    {
                        if (!IsEnum)
                            SetPropMethod(Entity, newValue);
                        else
                            SetPropMethod(Entity, EnumDef.GetEnumValue(FieldType, newValue));
                    }
                    else if (_pi != null)
                    {
                        if (!IsEnum)
                            _pi.SetValue(Entity, newValue, null);
                        else
                            _pi.SetValue(Entity, EnumDef.GetEnumValue(FieldType, newValue), null);
                    }
                }
            }
        }

        internal SetHandler GetLoadSetHandler()
        {
            if (_MemberType == MemberType.PropertyOnLoadAndRun && !_IsReadOnly)
                return SetPropMethod;
            else
                return SetFieldMethod;
        }

        internal void SetLoadValue(BaseEntity Entity, object newValue)
        {
            if (_IsParentField) return;
            try
            {
                if (newValue == null || newValue == DBNull.Value)
                    SetDefault(Entity, true);
                else
                {
                    if (_DataTypeAtr._DataType == DataType.Image &&
                        newValue.GetType() == typeof(byte[]))
                    {
                        if (_MemberType == MemberType.PropertyOnLoadAndRun &&
                            !_IsReadOnly)
                        {
                            if (SetPropMethod != null)
                                SetPropMethod(Entity, Helper.ConvertByteArrayToImage(
                                    (byte[])newValue));
                            else if (_pi != null)
                                _pi.SetValue(Entity, Helper.ConvertByteArrayToImage(
                                    (byte[])newValue), null);
                        }
                        else if (SetFieldMethod != null)
                            SetFieldMethod(Entity, Helper.ConvertByteArrayToImage(
                                (byte[])newValue));
                    }
                    else
                    {
                        if (_MemberType == MemberType.PropertyOnLoadAndRun)
                        {
                            if (SetPropMethod != null)
                            {
                                if (!IsEnum)
                                    SetPropMethod(Entity, newValue);
                                else
                                    SetPropMethod(Entity, EnumDef.GetEnumValue(FieldType,
                                        newValue));
                            }
                            else if (_pi != null)
                            {
                                if (!IsEnum)
                                    _pi.SetValue(Entity, newValue, null);
                                else
                                    _pi.SetValue(Entity, EnumDef.GetEnumValue(FieldType,
                                        newValue), null);
                            }
                        }
                        else
                        {
                            if (!IsEnum && SetFieldMethod != null)
                                SetFieldMethod(Entity, newValue);
                            else
                                SetFieldMethod(Entity, EnumDef.GetEnumValue(FieldType,
                                    newValue));
                        }
                    }
                }
            }
            catch
            {
                throw new ApplicationException(string.Format(ErrorEntity.DataTypeConversion,
                    _FieldName, newValue.GetType().ToString(), _DataTypeAtr._DataType.ToString()));
            }
        }

        private void SetRealDefault(BaseEntity Entity, bool Force)
        {
            switch (_DataTypeAtr._DataType)
            {
                case DataType.Char:
                case DataType.VarChar:
                    if (_MemberType == MemberType.PropertyOnLoadAndRun 
                        && !_IsReadOnly)
                    {
                        if (GetPropMethod != null)
                        {
                            if (Force || GetPropMethod(Entity) == null)
                            {
                                if (IsEnum)
                                    SetPropMethod(Entity, 0);
                                else
                                    SetPropMethod(Entity, string.Empty);
                            }
                        }
                        else if (_pi != null)
                        {
                            if (Force || _pi.GetValue(Entity, null) == null)
                            {
                                if (IsEnum)
                                    _pi.SetValue(Entity, 0, null);
                                else
                                    _pi.SetValue(Entity, string.Empty, null);
                            }
                        }
                    }
                    else if (Force || GetFieldMethod(Entity) == null)
                    {
                        if (IsEnum)
                            SetFieldMethod(Entity, 0);
                        else
                            SetFieldMethod(Entity, string.Empty);
                    }
                    break;
                case DataType.Date:
                    if (_MemberType == MemberType.PropertyOnLoadAndRun
                        && !_IsReadOnly)
                    {
                        if (SetPropMethod != null)
                        {
                            if (Force ||
                                (DateTime)GetPropMethod(Entity) == DateTime.MinValue)
                                SetPropMethod(Entity, Entity.Dp.GetDbDate());
                        }
                        else if (_pi != null)
                        {
                            if (Force ||
                                (DateTime)_pi.GetValue(Entity, null) == DateTime.MinValue)
                                _pi.SetValue(Entity, Entity.Dp.GetDbDate(), null);
                        }
                    }
                    else if (Force || (DateTime)GetFieldMethod(Entity) == DateTime.MinValue)
                        SetFieldMethod(Entity, Entity.Dp.GetDbDate());
                    break;
                case DataType.DateTime:
                case DataType.TimeStamp:
                case DataType.Time:
                    if (_MemberType == MemberType.PropertyOnLoadAndRun 
                        && !_IsReadOnly)
                    {
                        if (SetPropMethod != null)
                        {
                            if (Force || (DateTime)GetPropMethod(Entity) == DateTime.MinValue)
                                SetPropMethod(Entity, Entity.Dp.GetDbDateTime());
                        }
                        else if (_pi != null)
                        {
                            if (Force || (DateTime)_pi.GetValue(Entity, null) == DateTime.MinValue)
                                _pi.SetValue(Entity, Entity.Dp.GetDbDateTime(), null);
                        }
                    }
                    else if (Force || (DateTime)GetFieldMethod(Entity) == DateTime.MinValue)
                        SetFieldMethod(Entity, Entity.Dp.GetDbDateTime());
                    break;
            }
        }

        /// <summary>
        /// Set Entity Value to Entity with default value
        /// </summary>
        /// <param name="Entity"></param>
        public void SetDefault(BaseEntity Entity)
        {
            SetDefault(Entity, false);
        }

        /// <summary>
        /// Set Entity Value to Entity with Default Value, and Force it to data type default if Default Value is null.
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="Force"></param>
        public void SetDefault(BaseEntity Entity, bool Force)
        {
            if (_IsParentField) return;

            object newVal;

            if (_DataTypeAtr.Default == null)
                switch (_DataTypeAtr._DataType)
                {
                    case DataType.Binary:
                    case DataType.Image:
                        newVal = null;
                        goto SetVar;
                    default:
                        SetRealDefault(Entity, Force);
                        return;
                }

            switch (_DataTypeAtr._DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    if (IsEnum)
                        newVal = EnumDef.GetEnumValue(FieldType, _DataTypeAtr.Default);
                    else
                        newVal = _DataTypeAtr.Default;
                    break;
                case DataType.Date:
                case DataType.DateTime:
                case DataType.Time:
                case DataType.TimeStamp:
                    if (_DataTypeAtr.Default.GetType() == typeof(string))
                    {
                        if ((string)_DataTypeAtr.Default == "Today")
                            newVal = Entity.Dp.GetDbDate();
                        else if ((string)_DataTypeAtr.Default == "Now")
                            newVal = Entity.Dp.GetDbDateTime();
                        else
                            newVal = DateTime.Parse((string)
                                _DataTypeAtr.Default,
                                BaseUtility.DefaultCultureInfo);
                    }
                    else
                        newVal = Convert.ToDateTime(_DataTypeAtr.Default,
                            BaseUtility.DefaultCultureInfo);
                    break;
                case DataType.Integer:
                    newVal = Convert.ToInt32(_DataTypeAtr.Default);
                    break;
                case DataType.Decimal:
                    newVal = Convert.ToDecimal(_DataTypeAtr.Default);
                    break;
                default:
                    newVal = _DataTypeAtr.Default;
                    break;
            }
            SetVar:
            try
            {
                if (_MemberType == MemberType.PropertyOnLoadAndRun &&
                    !_IsReadOnly)
                {
                    if (SetPropMethod != null)
                        SetPropMethod(Entity, newVal);
                    else if (_pi != null)
                        _pi.SetValue(Entity, newVal, null);
                }
                else
                    SetFieldMethod(Entity, newVal);
            }
            catch { }
        }

        public void ChangeDefaultValue(object NewDefault)
        {
            _DataTypeAtr.Default = NewDefault;
        }

        // untuk field turunan pasti parentfield dan primarykey
        internal FieldDef(FieldDef ParentFd)
        {
            //_pi = ParentFd._pi;
            //_fi = ParentFd._fi;

            _MemberType = ParentFd._MemberType;
            _DataTypeAtr = ParentFd._DataTypeAtr;
            _FieldName = ParentFd._FieldName;

            IsEnum = ParentFd.IsEnum;

            _IsParentField = true;
            _IsPrimaryKey = true;
            _IsPublic = false;

            FieldType = ParentFd.FieldType;
        }

        // field original, pasti bukan parentfield
        internal FieldDef(MemberInfo mi, DataTypeAttribute DataTypeAtr,
            bool IsPrimaryKey)
        {
            FieldInfo _fi;
            PropertyInfo _pi = mi as PropertyInfo;

            if (_pi != null)
            {
                _fi = _pi.DeclaringType.GetField("_" + mi.Name,
                   BindingFlags.NonPublic | BindingFlags.Public |
                   BindingFlags.Instance);
                _MemberType = _fi == null ?
                    MemberType.PropertyOnLoadAndRun :
                    MemberType.PropertyOnRun;
                _IsPublic = _pi.GetGetMethod(false) != null;
                IsEnum = _pi.PropertyType.IsEnum;
                FieldType = _pi.PropertyType;
                GetPropMethod = DynamicMethodCompiler.CreateGetHandler(_pi);
                SetPropMethod = DynamicMethodCompiler.CreateSetHandler(_pi);
                GetFieldMethod = DynamicMethodCompiler.CreateGetHandler(_fi);
                SetFieldMethod = DynamicMethodCompiler.CreateSetHandler(_fi);
            }
            else
            {
                _fi = mi as FieldInfo;
                _MemberType = MemberType.FieldOnRun;
                _IsPublic = _fi.IsPublic;
                IsEnum = _fi.FieldType.IsEnum;
                FieldType = _fi.FieldType;
                GetFieldMethod = DynamicMethodCompiler.CreateGetHandler(_fi);
                SetFieldMethod = DynamicMethodCompiler.CreateSetHandler(_fi);
            }

            _DataTypeAtr = DataTypeAtr;
            _FieldName = mi.Name;
            switch (_DataTypeAtr._DataType)
            {
                case DataType.Decimal:
                    _DataTypeAtr.Default = Convert.ToDecimal(
                        _DataTypeAtr.Default);
                    break;
            }

            _IsPrimaryKey = IsPrimaryKey;
            _IsReadOnly = mi.MemberType == MemberTypes.Property ?
                !((PropertyInfo)mi).CanWrite : false;
        }

        // field hasil buat sendiri, utk membandingkan dgn tipe original database
        internal FieldDef(string FieldName, DataType DataType, 
            int Length, int Scale)
        {
            _DataTypeAtr = new DataTypeAtr(DataType, 
                Length, Scale);
            _FieldName = FieldName;
        }

        private PropertyInfo _pi;
        // field buatan sendiri utk Cancel Entity
        internal FieldDef(string FieldName, PropertyInfo pi,
            DataTypeAttribute dt, bool IsPrimaryKey)
        {
            _DataTypeAtr = dt;
            _FieldName = FieldName;
            _IsReadOnly = false;
            _IsPrimaryKey = IsPrimaryKey;
            _pi = pi;
            _MemberType = MemberType.PropertyOnLoadAndRun;
        }

        // field dari DataTypeLoadSql
        internal FieldDef(MemberInfo mi, string TableName, 
            FieldDef ParentFd, DataTypeLoadSqlAttribute dtlsa)
        {
            _FieldName = mi.Name;

            FieldInfo _fi;
            PropertyInfo _pi = mi as PropertyInfo;

            if (_pi != null)
            {
                _fi = _pi.DeclaringType.GetField("_" + mi.Name,
                    BindingFlags.NonPublic | BindingFlags.Public |
                    BindingFlags.Instance);
                _MemberType = _fi == null ?
                    MemberType.PropertyOnLoadAndRun :
                    MemberType.PropertyOnRun;
                _IsPublic = _pi.GetGetMethod(false) != null;
                IsEnum = _pi.PropertyType.IsEnum;
                FieldType = _pi.PropertyType;
                GetPropMethod = DynamicMethodCompiler.CreateGetHandler(_pi);
                SetPropMethod = DynamicMethodCompiler.CreateSetHandler(_pi);
                GetFieldMethod = DynamicMethodCompiler.CreateGetHandler(_fi);
                SetFieldMethod = DynamicMethodCompiler.CreateSetHandler(_fi);
            }
            else
            {
                _fi = mi as FieldInfo;
                _MemberType = MemberType.FieldOnRun;
                _IsPublic = _fi.IsPublic;
                IsEnum = _fi.FieldType.IsEnum;
                FieldType = _fi.FieldType;
                GetFieldMethod = DynamicMethodCompiler.CreateGetHandler(_fi);
                SetFieldMethod = DynamicMethodCompiler.CreateSetHandler(_fi);
            }
            _IsParentField = false;
            _IsPrimaryKey = false;
            _dtlsa = dtlsa;

            if (_dtlsa.GetSqlQueryLen() == 0)
            {
                if (ParentFd != null)
                    _DataTypeAtr = ParentFd._DataTypeAtr;
                _dtlsa.UpdateChildTableName(TableName);
            }
            else
            {
                switch (_dtlsa._DataType)
                {
                    case DataType.VarChar:
                        _DataTypeAtr = new DataTypeVarCharAttribute(100);
                        break;
                    case DataType.Decimal:
                        _DataTypeAtr = new DataTypeDecimalAttribute(19, 4);
                        break;
                    case DataType.Boolean:
                        _DataTypeAtr = new DataTypeBooleanAttribute();
                        break;
                    case DataType.Char:
                        _DataTypeAtr = new DataTypeCharAttribute(100);
                        break;
                    case DataType.Date:
                        _DataTypeAtr = new DataTypeDateAttribute();
                        break;
                    case DataType.DateTime:
                        _DataTypeAtr = new DataTypeDateTimeAttribute();
                        break;
                    case DataType.Integer:
                        _DataTypeAtr = new DataTypeIntegerAttribute();
                        break;
                    case DataType.Time:
                        _DataTypeAtr = new DataTypeTimeAttribute();
                        break;
                    case DataType.TimeStamp:
                        _DataTypeAtr = new DataTypeTimeStampAttribute();
                        break;
                    case DataType.Binary:
                        _DataTypeAtr = new DataTypeBinaryAttribute();
                        break;
                    case DataType.Image:
                        _DataTypeAtr = new DataTypeImageAttribute();
                        break;
                }
            }
            _IsReadOnly = mi.MemberType == MemberTypes.Property ?
                !((PropertyInfo)mi).CanWrite : false;
        }

        internal void CheckFieldType(string TableName)
        {
            if (!BaseUtility.IsDebugMode) return;

            if (IsEnum)
                switch (_DataTypeAtr._DataType)
                {
                    case DataType.VarChar:
                    case DataType.Char:
                        break;
                    default:
                        throw new ApplicationException(string.Format(
                            ErrorMetaData.EnumValueMustString, _FieldName));
                }
            if (_DataTypeAtr != null)
                switch (_DataTypeAtr._DataType)
                {
                    case DataType.Boolean:
                        if (FieldType != typeof(bool))
                            goto ErrorField;
                        break;
                    case DataType.Char:
                    case DataType.VarChar:
                        if (FieldType != typeof(string) && !FieldType.IsEnum)
                            goto ErrorField;
                        break;
                    case DataType.Date:
                    case DataType.DateTime:
                    case DataType.Time:
                    case DataType.TimeStamp:
                        if (FieldType != typeof(DateTime))
                            goto ErrorField;
                        break;
                    case DataType.Decimal:
                        if (FieldType != typeof(decimal) &&
                            FieldType != typeof(Single) &&
                            FieldType != typeof(double))
                            goto ErrorField;
                        break;
                    case DataType.Image:
                        if (FieldType != typeof(Image))
                            goto ErrorField;
                        break;
                    case DataType.Integer:
                        if (FieldType != typeof(int))
                            goto ErrorField;
                        break;
                }
            return;
        ErrorField:
            throw new ApplicationException(string.Format(
                ErrorMetaData.FieldTypeNotMatch, TableName,
                _FieldName));
        }

        private string DataTypeToString()
        {
            string retVal = _DataTypeAtr._DataType.ToString();
            switch (_DataTypeAtr._DataType)
            {
                case DataType.Char:
                case DataType.VarChar:
                    return string.Concat(retVal, "(",
                        Length.ToString(), ")");
                case DataType.Decimal:
                    return string.Concat(retVal, "(",
                        Length.ToString(), ",", Scale.ToString(), ")");
                default:
                    return retVal;
            }
        }
        public override string ToString()
        {
            string retVal = string.Concat(_FieldName, " (", 
                DataTypeToString(), ")");

            if (_IsPrimaryKey) retVal += ", (PK)";
            if (_IsParentField) retVal += ", (ParentField)";
            if (_DataTypeAtr.Default != null)
            {
                string tmpStr = _DataTypeAtr.Default.ToString();
                if (tmpStr.Length != 0 && tmpStr != "0" &&
                    tmpStr != "False" && tmpStr != "Now" && tmpStr != "Today")
                    retVal += ", (Default: " + _DataTypeAtr.Default.ToString() + ")";
            }
            return retVal;
        }
    }

    [DebuggerNonUserCode]
    public sealed class EntityCollDef
    {
        private GetHandler GetMethod;
        private SetHandler SetMethod;

        private Type ConstructionType;

        internal Type ChildType;

        public TableDef GetTableDef()
        {
            return MetaData.GetTableDef(ChildType);
        }

        private string _FieldName;
        public string FieldName { get { return _FieldName; } }

        public Type GetChildType() { return ChildType; }
        public Type GetConstructionType() { return ConstructionType; }

        private InstantiateObjectHandler NewMethod;
        public IEntityCollection CreateNew(BusinessEntity Parent)
        {
            IEntityCollection iec = (IEntityCollection)NewMethod();
            iec.Init(Parent, FieldName);
            SetMethod(Parent, iec);
            return iec;
        }

        public IList GetValue(BaseEntity Entity)
        {
            //if (mi.MemberType == MemberTypes.Field)
            //    return (IList)((FieldInfo)mi).GetValue(Entity);
            //else
            //    return (IList)((PropertyInfo)mi).GetValue(Entity, null);
            return (IList)GetMethod(Entity);
        }
        public IEntityCollection GetCollValue(BaseEntity Entity)
        {
            //if (mi.MemberType == MemberTypes.Field)
            //    return (IEntityCollection)((FieldInfo)mi).GetValue(Entity);
            //else
            //    return (IEntityCollection)((PropertyInfo)mi).GetValue(Entity, null);
            return (IEntityCollection)GetMethod(Entity);
        }

        public void SetValue(BusinessEntity Entity, 
            IList newValue)
        {
            //if (mi.MemberType == MemberTypes.Field)
            //    ((FieldInfo)mi).SetValue(Entity, newValue);
            //else
            //    ((PropertyInfo)mi).SetValue(Entity, newValue, null);
            SetMethod(Entity, newValue);
        }

        internal string ToString(int Level)
        {
            return MetaData.GetTableDef(ChildType).ToString(Level);
        }
        public override string ToString() { return ToString(0); }

        internal EntityCollDef(MemberInfo mi)
        {
            //this.mi = mi;
            _FieldName = mi.Name;
            Type xType;
            if (mi.MemberType == MemberTypes.Field)
                xType = ((FieldInfo)mi).FieldType;
            else
                xType = ((PropertyInfo)mi).PropertyType;

            Type[] Types = xType.GetGenericArguments();
            ConstructionType = xType.GetGenericTypeDefinition()
                .MakeGenericType(Types);
            ChildType = Types[0];
            NewMethod = DynamicMethodCompiler
                .CreateInstantiateObjectHandler(ConstructionType);
            GetMethod = DynamicMethodCompiler.CreateGetHandler(mi);
            SetMethod = DynamicMethodCompiler.CreateSetHandler(mi);
        }
    }

    public delegate void AfterValidate(DataPersistance Dp, TableDef td);

    //[DebuggerNonUserCode]
    public sealed class TableDef
    {
        internal Type _ClassType;
        internal Type _ParentClassType;

        public FieldDef fldPrintCounter;

        public string Description = string.Empty;

        internal ViewEntityAttribute _dva;
        public bool IsDataView { get { return _dva != null; } }

        public event AfterValidate OnAfterValidate;
        public bool IsPersistView
        {
            get { return _dva == null ? false : _dva.PersistView; }
        }

        public EnableCancelEntityAttribute EnableCancelEntityAtr;

        private Dictionary<string, int> ListExist = new Dictionary<string, int>();
        internal bool GetIsExist(DataPersistance dp)
        {
            return ListExist.ContainsKey(dp.ConnectionString);
        }
        internal void SetIsExist(DataPersistance dp)
        {
            int TmpInt;
            if (!ListExist.TryGetValue(dp.ConnectionString, out TmpInt))
            {
                ListExist.Add(dp.ConnectionString, 0);
                if (OnAfterValidate != null) OnAfterValidate(dp, this);
            }
        }
        internal void SetIsExist(DataPersistance dp, bool IsExist)
        {
            int TmpInt;

            if (!ListExist.TryGetValue(dp.ConnectionString, out TmpInt))
            {
                if (IsExist)
                {
                    ListExist.Add(dp.ConnectionString, 0);
                    if (OnAfterValidate != null) OnAfterValidate(dp, this);
                }
            }
            else if (!IsExist)
                ListExist.Remove(dp.ConnectionString);
        }

        public Type ClassType { get { return _ClassType; } }
        public Type ParentClassType { get { return _ParentClassType; } }
        public Dictionary<string, FieldDef> KeyFields = new Dictionary<string, FieldDef>();
        public Dictionary<string, FieldDef> NonKeyFields = new Dictionary<string, FieldDef>();
        public List<EntityCollDef> ChildEntities = new List<EntityCollDef>();
        public List<string> IndexedFields = new List<string>();
 
        public List<RelationAttribute> ParentRelations = new List<RelationAttribute>();
        public List<RelationAttribute> ChildRelations = new List<RelationAttribute>();

        public List<string> GetListFieldNames(bool PublicFieldOnly)
        {
            List<string> RetVal = new List<string>();

            if (PublicFieldOnly)
            {
                foreach (FieldDef fld in KeyFields.Values)
                    if (fld.IsPublic) RetVal.Add(fld._FieldName);
                foreach (FieldDef fld in NonKeyFields.Values)
                    if (fld.IsPublic) RetVal.Add(fld._FieldName);
            }
            else
            {
                foreach (FieldDef fld in KeyFields.Values)
                    RetVal.Add(fld._FieldName);
                foreach (FieldDef fld in NonKeyFields.Values)
                    RetVal.Add(fld._FieldName);
            }
            return RetVal;
        }

        public bool IsFieldKeyEqual(BusinessEntity Entity1, BusinessEntity Entity2)
        {
            foreach (FieldDef fld in KeyFields.Values)
                if (!fld.GetValue(Entity1).Equals(
                    fld.GetValue(Entity2)))
                    return false;
            return true;
        }

        internal string GetAsmVersion()
        {
            string retVal = _ClassType.Assembly.FullName;
            int StartPos = retVal.IndexOf("Version=", 0) + 8;
            int EndPos = retVal.IndexOf(',', StartPos + 7);
            retVal = retVal.Substring(StartPos, EndPos - StartPos);
            if (retVal.EndsWith(".0"))
                throw new ApplicationException(string.Format(
                    ErrorMetaData.AsmVersionMustAutoIncrement, 
                    _ClassType.Assembly.GetName()));
            return retVal;
        }
        internal string GetAsmName()
        {
            string retVal = _ClassType.Assembly.FullName;
            return retVal.Substring(0, retVal.IndexOf(','));
        }

        internal List<AutoNumberKeyAttribute> _AutoNumberKeyAtr;
        internal AutoNestedKeyAttribute _AutoNestedKeyAtr;
        internal FieldDef _fldCounterField;
        internal List<AutoNumberAttribute> _AutoNumberAtr;

        public string GetDocumentId(BaseEntity Entity)
        {
            string RetVal = string.Empty;
            foreach (FieldDef fld in KeyFields.Values)
                RetVal = string.Concat(fld.GetValue(Entity), ", ");
            return RetVal.Substring(0, RetVal.Length - 2);
        }

        public FieldDef fldTransactionDate;
        public FieldDef fldTimeStamp;

        public TableDef(string TableName) { _TableName = TableName; }
        internal TableDef(Type ClassType) { _ClassType = ClassType; }
        internal TableDef(string TableName, string strMetaData)
        {
            _TableName = TableName;
            string[] arrStr = strMetaData.Split('|');
            int idx = 1;

            int jml = int.Parse(arrStr[0]);
            for (int i = 0; i < jml; i++)
            {
                KeyFields.Add(arrStr[idx], new FieldDef(arrStr[idx],
                    (DataType)int.Parse(arrStr[idx + 1]),
                    int.Parse(arrStr[idx + 2]),
                    int.Parse(arrStr[idx + 3])));
                idx += 4;
            }
            jml = int.Parse(arrStr[idx++]);
            for (int i = 0; i < jml; i++)
            {
                NonKeyFields.Add(arrStr[idx], new FieldDef(arrStr[idx],
                    (DataType)int.Parse(arrStr[idx + 1]),
                    int.Parse(arrStr[idx + 2]),
                    int.Parse(arrStr[idx + 3])));
                idx += 4;
            }
            jml = int.Parse(arrStr[idx++]);
            for (int i = 0; i < jml; i++)
                IndexedFields.Add(string.Concat(arrStr[idx++], 
                    "|", arrStr[idx++]));
        }

        internal FieldParam[] TableParams;
        internal TableDef(ViewEntityAttribute dva, Type ClassType)
        {
            _dva = dva;
            _ClassType = ClassType;
            if (_dva.PersistView)
            {
                ViewEntity ve = (ViewEntity)BaseFactory.CreateInstance(ClassType);
                TableParams = ve.GetParams();
            }
        }

        public void SetDefault(BaseEntity Entity)
        {
            bool CallDataChanged = true;

            if (Entity.EntityOnLoad)
                CallDataChanged = false;
            else
                Entity.EntityOnLoad = true;

            try
            {
                IRuleInitUI px = Entity as IRuleInitUI;

                if (px != null)
                    px.BeforeSetDefault(this);

                foreach (FieldDef fd in KeyFields.Values)
                    fd.SetDefault(Entity);
                foreach (FieldDef fd in NonKeyFields.Values)
                    fd.SetDefault(Entity);

                if (px != null)
                {
                    foreach (EntityCollDef ecd in ChildEntities)
                        ((IEntityCollection)ecd.GetValue(
                            (ParentEntity)px)).Clear();
                    px.AfterSetDefault();
                }
                ParentEntity pe = Entity as ParentEntity;
                if (pe != null) pe.IsLoadedEntity = false;
            }
            finally
            {
                Entity.ClearError();
                if (CallDataChanged)
                {
                    Entity.EntityOnLoad = false;
                    Entity.DataChanged();
                }
            }
        }
        public void SetDefault(BusinessEntity Entity, string ExcludeFields)
        {
            BusinessEntity be = (BusinessEntity)Entity;
            be.EntityOnLoad = true;
            try
            {
                string[] ArrExf = ExcludeFields.Split(',');
                List<string> ListExf = new List<string>();
                foreach (string exf in ArrExf)
                    ListExf.Add(exf.Trim());

                IRuleInitUI px = Entity as IRuleInitUI;
                if (px != null)
                    px.BeforeSetDefault(this);

                int i;
                foreach (FieldDef fd in KeyFields.Values)
                {
                    i = ListExf.IndexOf(fd._FieldName);
                    if (i < 0)
                        fd.SetDefault(Entity);
                    else
                        ListExf.RemoveAt(i);
                }
                foreach (FieldDef fd in NonKeyFields.Values)
                {
                    i = ListExf.IndexOf(fd._FieldName);
                    if (i < 0)
                        fd.SetDefault(Entity);
                    else
                        ListExf.RemoveAt(i);
                }

                if (px != null)
                {
                    foreach (EntityCollDef ecd in ChildEntities)
                        ((IEntityCollection)ecd.GetValue(
                            (ParentEntity)px)).Clear();
                    px.AfterSetDefault();
                }
                ParentEntity pe = Entity as ParentEntity;
                if (pe != null) pe.IsLoadedEntity = false;
            }
            finally
            {
                be.EntityOnLoad = false;
                Entity.ClearError();
                Entity.DataChanged();
            }
        }

        public FieldDef GetFieldDef(string FieldName)
        {
            FieldDef fld;

            FieldName = FieldName.Trim();
            if (KeyFields.TryGetValue(FieldName, out fld)) return fld;
            NonKeyFields.TryGetValue(FieldName, out fld);
            return fld;
        }

        public IEnumerable<FieldDef> Fields
        {
            get
            {
                foreach (FieldDef fld in KeyFields.Values)
                    yield return fld;
                foreach (FieldDef fld in NonKeyFields.Values)
                    yield return fld;
            }
        }

        internal string _TableName;
        public string TableName
        {
            get { return _TableName; }
            set
            {
                _TableName = value;
                ListExist.Clear();
            }
        }

        [DebuggerNonUserCode]
        private class StrHeaderView
        {
            public string SqlHeaderView;
            public string SqlDDLHeaderView;

            public StrHeaderView(string SqlHeaderView, string SqlDDLHeaderView)
            {
                this.SqlHeaderView = SqlHeaderView;
                this.SqlDDLHeaderView = SqlDDLHeaderView;
            }
        }
        private Dictionary<DataPersistance, StrHeaderView> DictHeaderView;
        public string GetSqlHeaderView(DataPersistance Dp)
        {
            if (TableParams == null) return _TableName;

            StrHeaderView strParam;
            if (DictHeaderView == null) 
                DictHeaderView = new Dictionary<DataPersistance, StrHeaderView>();
            if (!DictHeaderView.TryGetValue(Dp, out strParam))
            {
                strParam = new StrHeaderView(string.Concat(_TableName, "(",
                    ((IDataPersistance)Dp).BuildParam(TableParams), ")"),
                    string.Concat(_TableName, "(",
                    ((IDataPersistance)Dp).BuildDDLParam(TableParams), ")"));
                DictHeaderView.Add(Dp, strParam);
            }
            return strParam.SqlHeaderView;
        }
        public string GetSqlDDLHeaderView(DataPersistance Dp)
        {
            if (TableParams == null) return _TableName;

            StrHeaderView strParam;
            if (DictHeaderView == null)
                DictHeaderView = new Dictionary<DataPersistance, StrHeaderView>();
            if (!DictHeaderView.TryGetValue(Dp, out strParam))
            {
                strParam = new StrHeaderView(string.Concat(_TableName, "(",
                    ((IDataPersistance)Dp).BuildParam(TableParams), ")"),
                    string.Concat(_TableName, "(",
                    ((IDataPersistance)Dp).BuildDDLParam(TableParams), ")"));
                DictHeaderView.Add(Dp, strParam);
            }
            return strParam.SqlDDLHeaderView;
        }

        public DataPersistance GetDataPersistance(DataPersistance DefaultDp)
        {
            DataPersistance Dp;
            if (BaseFramework.DictAsmDp.TryGetValue(_ClassType.Assembly, out Dp))
                return Dp;
            return DefaultDp;
        }

        internal string GetStrMetaData(DataPersistance Dp)
        {
            if (_dva != null)
            {
                ViewEntity ve = (ViewEntity)BaseFactory.CreateInstance(_ClassType);
                ve.Dp = Dp;
                return ve.GetSqlDdl();
            }

            StringBuilder retVal = new StringBuilder();

            retVal.Append(KeyFields.Count);
            foreach (FieldDef fd in KeyFields.Values)
                retVal.Append('|').Append(fd._FieldName)
                    .Append('|').Append((int)fd._DataTypeAtr._DataType)
                    .Append('|').Append(fd._DataTypeAtr._Length)
                    .Append('|').Append(fd._DataTypeAtr._Scale);

            StringBuilder tmpStr = new StringBuilder();
            int Ctr = 0;
            foreach (FieldDef fd in NonKeyFields.Values)
                if (fd._dtlsa == null)
                    tmpStr.Append('|').Append(fd._FieldName)
                        .Append('|').Append((int)fd._DataTypeAtr._DataType)
                        .Append('|').Append(fd._DataTypeAtr._Length)
                        .Append('|').Append(fd._DataTypeAtr._Scale);
                else Ctr++;
            retVal.Append('|').Append(NonKeyFields.Count - Ctr)
                .Append(tmpStr.ToString());

            retVal.Append('|').Append(IndexedFields.Count);
            foreach (string idx in IndexedFields)
                retVal.Append('|').Append(idx);

            return retVal.ToString();
        }
        public string GetSqlSelect(DataPersistance Dp, 
            bool PublicOnly, bool RealFieldOnly)
        {
            StringBuilder SqlSelect = new StringBuilder();

            if (PublicOnly)
            {
                foreach (FieldDef fld in KeyFields.Values)
                    if (fld._IsPublic)
                        SqlSelect.Append(",").Append(
                            fld._FieldName);
                foreach (FieldDef fld in NonKeyFields.Values)
                    if (fld._IsPublic)
                    {
                        if (fld._dtlsa == null)
                            SqlSelect.Append(",").Append(
                                fld._FieldName);
                        else if(!RealFieldOnly)
                            SqlSelect.Append(",(").Append(
                                fld._dtlsa.GetSqlQuery())
                                .Append(") AS ").Append(
                                fld._FieldName);
                    }
            }
            else
            {
                foreach (FieldDef fld in KeyFields.Values)
                    SqlSelect.Append(",").Append(fld._FieldName);
                foreach (FieldDef fld in NonKeyFields.Values)
                    if (fld._dtlsa == null)
                        SqlSelect.Append(",").Append(fld._FieldName);
                    else if (!RealFieldOnly)
                        SqlSelect.Append(",(").Append(
                            fld._dtlsa.GetSqlQuery())
                            .Append(") AS ").Append(fld._FieldName);
            }

            if (_dva != null && !_dva.PersistView)
                SqlSelect.Remove(0, 1).Insert(0, "SELECT ")
                    .Append(" FROM (").Append(GetStrMetaData(Dp))
                    .Append(") AS ").Append(_TableName);
            else
                SqlSelect.Remove(0, 1).Insert(0, "SELECT ")
                    .Append(" FROM ").Append(GetSqlHeaderView(Dp));

            return string.Concat("SELECT * FROM (",
                SqlSelect.ToString(), ") AS X");
        }

        public string GetSqlSelect(DataPersistance Dp,
            string FieldList, string Condition, string OrderBy)
        {
            StringBuilder SqlSelect = new StringBuilder();

            foreach (FieldDef fld in KeyFields.Values)
                SqlSelect.Append(",").Append(fld._FieldName);
            foreach (FieldDef fld in NonKeyFields.Values)
                if (fld._dtlsa == null)
                    SqlSelect.Append(",").Append(fld._FieldName);
                else
                    SqlSelect.Append(",(").Append(fld._dtlsa
                        .GetSqlQuery()).Append(") AS ")
                        .Append(fld._FieldName);

            if (_dva != null && !_dva.PersistView)
                SqlSelect.Remove(0, 1).Insert(0, "SELECT ")
                    .Append(" FROM (").Append(GetStrMetaData(Dp))
                    .Append(") AS ").Append(_TableName);
            else
                SqlSelect.Remove(0, 1).Insert(0, "SELECT ")
                    .Append(" FROM ").Append(GetSqlHeaderView(Dp));

            SqlSelect.Insert(0, string.Concat("SELECT ", FieldList, " FROM ("))
                .Append(") AS X");
            if (Condition.Length > 0)
                SqlSelect.Append(" WHERE ").Append(Condition);
            if (OrderBy.Length > 0)
                SqlSelect.Append(" ORDER BY ").Append(OrderBy);

            return SqlSelect.ToString();
        }

        public FieldDef this[string FieldName]
        {
            get { return GetFieldDef(FieldName); }
        }

        internal string ToString(int Level)
        {
            StringBuilder TmpStr = new StringBuilder();
            string TmpSpace = TmpStr.Append(' ', Level * 5).ToString();
            TmpStr.Append("ClassType: ").AppendLine(_ClassType.Name);
            TmpStr.Append(TmpSpace).Append("TableName: ").AppendLine(_TableName);
            TmpStr.AppendLine(string.Empty).Append(TmpSpace).AppendLine("KeyFields:");
            foreach (FieldDef fd in KeyFields.Values)
                TmpStr.Append(TmpSpace).Append("- ").AppendLine(fd.ToString());
            if (NonKeyFields.Values.Count > 0)
                TmpStr.Append(TmpSpace).AppendLine("Fields:");
            foreach (FieldDef fd in NonKeyFields.Values)
                TmpStr.Append(TmpSpace).Append("- ").AppendLine(fd.ToString());
            if (IndexedFields.Count > 0)
                TmpStr.Append(TmpSpace).AppendLine("Indexes:");
            foreach (string idx in IndexedFields)
                TmpStr.Append(TmpSpace).AppendLine(idx);

            if (ChildEntities.Count > 0)
            {
                StringBuilder strTemp = new StringBuilder();

                strTemp.AppendLine(string.Empty).Append(TmpSpace)
                    .AppendLine("Child Entities:");
                foreach (EntityCollDef ecd in ChildEntities)
                    strTemp.AppendLine(ecd.ToString(Level + 1));
                TmpStr.Append(strTemp.ToString());
            }

            return TmpStr.ToString().TrimEnd();
        }
        public override string ToString() { return ToString(0); }

        public string GetSqlFieldSelect(bool PrimaryKey, 
            bool NonKey, bool PublicOnly, bool RealFieldOnly)
        {
            StringBuilder TmpStr = new StringBuilder();

            if (PublicOnly)
            {
                if (PrimaryKey)
                    foreach (FieldDef fld in KeyFields.Values)
                        if(fld._IsPublic)
                            TmpStr.Append(",").Append(
                                fld._FieldName);

                if (NonKey)
                    foreach (FieldDef fld in NonKeyFields.Values)
                        if (fld._IsPublic)
                            if (fld._dtlsa == null)
                                TmpStr.Append(",").Append(
                                    fld._FieldName);
                            else if (!RealFieldOnly)
                                TmpStr.Append(",(").Append(
                                    fld._dtlsa.GetSqlQuery())
                                    .Append(") AS ").Append(
                                    fld._FieldName);
            }
            else
            {
                if (PrimaryKey)
                    foreach (FieldDef fld in KeyFields.Values)
                        TmpStr.Append(",").Append(fld._FieldName);

                if (NonKey)
                    foreach (FieldDef fld in NonKeyFields.Values)
                        if (fld._dtlsa == null)
                            TmpStr.Append(",").Append(fld._FieldName);
                        else if (!RealFieldOnly)
                            TmpStr.Append(",(").Append(
                                fld._dtlsa.GetSqlQuery())
                                .Append(") AS ").Append(fld._FieldName);
            }
            return TmpStr.Length > 0 ?
                TmpStr.Remove(0, 1).ToString() : string.Empty;
        }
        public string GetStrFieldNames(bool PrimaryKey,
            bool NonKey, bool PublicOnly, bool RealFieldOnly)
        {
            StringBuilder TmpStr = new StringBuilder();

            if (PublicOnly)
            {
                if (PrimaryKey)
                    foreach (FieldDef fld in KeyFields.Values)
                        if (fld._IsPublic)
                            TmpStr.Append(",").Append(
                                fld._FieldName);

                if (NonKey)
                    foreach (FieldDef fld in NonKeyFields.Values)
                        if (fld._IsPublic && (fld._dtlsa == null || !RealFieldOnly))
                            TmpStr.Append(",").Append(
                                fld._FieldName);
            }
            else
            {
                if (PrimaryKey)
                    foreach (FieldDef fld in KeyFields.Values)
                        TmpStr.Append(",").Append(fld._FieldName);

                if (NonKey)
                    foreach (FieldDef fld in NonKeyFields.Values)
                        if (fld._dtlsa == null || !RealFieldOnly)
                            TmpStr.Append(",").Append(fld._FieldName);
            }
            return TmpStr.Length > 0 ?
                TmpStr.Remove(0, 1).ToString() : string.Empty;
        }

        public string GetSqlFieldSelect(string FieldList)
        {
            StringBuilder TmpStr = new StringBuilder();

            if (FieldList.Length == 0 || FieldList == "*")
            {
                foreach (FieldDef fld in KeyFields.Values)
                    TmpStr.Append(",").Append(fld._FieldName);
                foreach (FieldDef fld in NonKeyFields.Values)
                    if (fld._dtlsa == null)
                        TmpStr.Append(",").Append(fld._FieldName);
                    else
                        TmpStr.Append(",(").Append(fld._dtlsa
                            .GetSqlQuery()).Append(") AS ")
                            .Append(fld._FieldName);
            }
            else
            {
                string[] Fields = FieldList.Split(',');

                foreach (string fldName in Fields)
                {
                    FieldDef fld = GetFieldDef(fldName);
                    if (fld._dtlsa == null)
                        TmpStr.Append(",").Append(fld._FieldName);
                    else
                        TmpStr.Append(",(").Append(fld._dtlsa.GetSqlQuery())
                            .Append(") AS ").Append(fld._FieldName);
                }
            }
            
            if (TmpStr.Length > 0) 
                return TmpStr.Remove(0, 1).ToString();
            else
                return string.Empty;
        }

        public int GetRealFieldCount()
        {
            int RetVal = 0;
            foreach (FieldDef fld in KeyFields.Values)
                if (fld._dtlsa == null) RetVal++;
            foreach (FieldDef fld in NonKeyFields.Values)
                if (fld._dtlsa == null) RetVal++;
            return RetVal;
        }

        public void UpdateLoadSqlFields(string FieldList, 
            BaseEntity Entity)
        {
            IDataReader dr = null;
            try
            {
                foreach (FieldDef fld in NonKeyFields.Values)
                    if (fld._dtlsa != null && (
                        FieldList.Length == 0 ||
                        FieldList.Contains(fld._FieldName)))
                    {
                        FieldParam[] Params;
                        DataPersistance Dp = BaseFramework.GetDefaultDp(
                            fld._dtlsa._ParentType.Assembly);
                        dr = Dp.ExecuteReader(fld._dtlsa
                            .GetSqlQueryValue(Entity, out Params),
                            Params);
                        object tmpVal;
                        if (dr.Read())
                            tmpVal = dr[0];
                        else
                            tmpVal = fld.GetDefaultValue();
                        dr.Close();
                        fld.SetLoadValue(Entity, tmpVal);
                    }
            }
            finally
            {
                if (dr != null && !dr.IsClosed) dr.Close();
            }
        }

    }
}
