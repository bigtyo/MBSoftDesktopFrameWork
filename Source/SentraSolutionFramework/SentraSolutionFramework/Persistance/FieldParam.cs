using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using System.Diagnostics;
using SentraUtility;
using System.Drawing;

namespace SentraSolutionFramework.Persistance
{
    [DebuggerNonUserCode]
    public sealed class FieldParam
    {
        public readonly string FieldName;
        public readonly DataType DataType;
        public readonly int Length;
        public readonly int Scale;

        private object _Value;
        public object Value
        {
            get { return _Value; }
            set
            {
                if (value == null) {
                    _Value = null;
                    return;
                }
                BusinessEntity be = value as BusinessEntity;
                if (be != null)
                {
                    if (fld != null)
                        Value = fld.GetValue(be);
                    else
                    {
                        fld = MetaData.GetTableDef(be.GetType())
                                .GetFieldDef(FieldName);
                        Value = fld.GetValue(be);
                    }
                }
                else
                {
                    switch (DataType)
                    {
                        case DataType.Date:
                            _Value = ((DateTime)value).Date;
                            break;
                        case DataType.Time:
                            DateTime dt = (DateTime)value;
    
                            _Value = new DateTime(1900, 1, 1).AddHours(dt.Hour)
                                .AddMinutes(dt.Minute).AddSeconds(dt.Second);
                            break;
                        default:
                            Type tp = value.GetType();
                            if (tp.IsEnum)
                                _Value = EnumDef.GetEnumName(tp, value);
                            else
                                _Value = value;
                            break;
                    }
                }
            }
        }
        
        private FieldDef fld;

        public FieldParam(string FieldName, object Value)
        {
            if (Value.GetType() == typeof(DataType))
            {
                switch ((DataType)Value)
                {
                    case DataType.VarChar:
                    case DataType.Char:
                        Length = 50;
                        break;
                    case DataType.Decimal:
                        Length = 19;
                        Scale = 4;
                        break;
                }
                this.FieldName = FieldName;
                this.DataType = (DataType)Value;
                return;
            }

            this.FieldName = FieldName;

            #region Set DataType
            Type tp = Value.GetType();
            if (tp.IsEnum)
            {
                Value = EnumDef.GetEnumName(tp, Value);
                DataType = DataType.VarChar;
                Length = Value.ToString().Length;
            }
            else if (tp == typeof(string))
            {
                DataType = DataType.VarChar;
                Length = Value.ToString().Length;
            }
            else if (tp == typeof(decimal) ||
               tp == typeof(Single) ||
                tp == typeof(double))
            {
                DataType = DataType.Decimal;
                Length = 19;
                Scale = 4;
            }
            else if (tp == typeof(int))
                DataType = DataType.Integer;
            else if (tp == typeof(bool))
                DataType = DataType.Boolean;
            else if (tp == typeof(DateTime))
                DataType = DataType.DateTime;
            else if (tp == typeof(Image))
                DataType = DataType.Image;
            else
                DataType = DataType.Binary;
            #endregion

            _Value = Value;
        }
        public FieldParam(string FieldName, DataType DataType, int Length)
        {
            switch (DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                case DataType.Decimal:
                    this.Length = Length;
                    break;
            }
            this.FieldName = FieldName;
            this.DataType = DataType;
        }
        public FieldParam(string FieldName, DataType DataType, int Precision, int Scale)
        {
            switch (DataType)
            {
                case DataType.VarChar:
                case DataType.Char:
                    Length = Precision;
                    break;
                case DataType.Decimal:
                    Length = Precision;
                    this.Scale = Scale;
                    break;
            }
            this.FieldName = FieldName;
            this.DataType = DataType;
        }
        public FieldParam(FieldDef fd)
        {
            FieldName = fd._FieldName;
            DataType = fd.DataType;
            Length = fd.Length;
            Scale = fd.Scale;
            fld = fd;
        }
        public FieldParam(string FieldName, FieldDef fd, object Value)
        {
            fld = fd;
            this.FieldName = FieldName;
            DataType = fd.DataType;
            Length = fd.Length;
            Scale = fd.Scale;

            if (Value != null)
            {
                Type tp = Value.GetType();
                if (tp.IsEnum)
                    _Value = EnumDef.GetEnumName(tp, Value);
                else
                    _Value = Value;
            }
            else
                _Value = null;
        }
        public FieldParam(string FieldName, FieldDef fd)
        {
            fld = fd;
            this.FieldName = FieldName;
            DataType = fd.DataType;
            Length = fd.Length;
            Scale = fd.Scale;
        }
        public FieldParam(FieldDef fd, BaseEntity Entity)
        {
            fld = fd;
            FieldName = fd._FieldName;
            DataType = fd.DataType;
            Length = fd.Length;
            Scale = fd.Scale;
            Value = fd.GetValue(Entity);
            if (Value == null)
                switch (fd.DataType)
                {
                    case DataType.Image:
                    case DataType.Binary:
                        break;
                    default:
                        Value = fd.GetDefaultValue();
                        break;
                }
        }
        public FieldParam(FieldDef fd, object Value)
        {
            fld = fd;
            FieldName = fd._FieldName;
            DataType = fd.DataType;
            Length = fd.Length;
            Scale = fd.Scale;
            this.Value = Value;
        }

        public FieldParam Clone()
        {
            return (FieldParam)MemberwiseClone();
        }
    }
}
