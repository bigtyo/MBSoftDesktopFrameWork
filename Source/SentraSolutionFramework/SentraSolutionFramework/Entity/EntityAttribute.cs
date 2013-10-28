using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SentraUtility;
using SentraSolutionFramework.Persistance;

namespace SentraSolutionFramework.Entity
{
    public enum DataType
    {
        VarChar, Char, Decimal, Integer, Date, Time,
        DateTime, Boolean, Image, Binary, TimeStamp
    }
    public enum ParentUpdate { UpdateCascade, UpdateRestrict }
    public enum ParentDelete { DeleteCascade, DeleteRestrict }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field |
        AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class DescriptionAttribute : Attribute
    {
        private string _Description;
        public string Description
        {
            get { return _Description; }
        }

        public DescriptionAttribute(string Description)
        {
            _Description = Description;
        }
    }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class EnableCancelEntityAttribute : Attribute
    {
        private string CancelStatusFieldName;
        private string CancelStatus;
        private string CancelDateTimeFieldName;
        private string CancelUserFieldName;
        private string CancelNotesFieldName;

        public string GetCancelStatusFieldName()
        {
            return CancelStatusFieldName;
        }
        public string GetCancelStatus()
        {
            return CancelStatus;
        }
        public string GetCancelDateTimeFieldName()
        {
            return CancelDateTimeFieldName;
        }
        public string GetCancelUserFieldName()
        {
            return CancelUserFieldName;
        }
        public string GetCancelNotesFieldName()
        {
            return CancelNotesFieldName;
        }

        public EnableCancelEntityAttribute()
        {
            CancelStatusFieldName = "Status";
            CancelStatus = "Batal";
            CancelDateTimeFieldName = "TglBatal";
            CancelUserFieldName = "DibatalkanOleh";
            CancelNotesFieldName = "CatatanBatal";
        }

        /// <summary>
        /// Enable Cancel Status on Business Entity
        /// </summary>
        /// <param name="CancelStatusFieldName">If Empty="Status"</param>
        /// <param name="CancelStatus">If Empty="Batal"</param>
        /// <param name="CancelDateTimeFieldName">If Empty="TglBatal"</param>
        /// <param name="CancelUserFieldName">If Empty="DibatalkanOleh"</param>
        /// <param name="CancelNotesFieldName">If Empty="CatatanBatal"</param>
        public EnableCancelEntityAttribute(string CancelStatusFieldName, string CancelStatus,
            string CancelDateTimeFieldName, string CancelUserFieldName, string CancelNotesFieldName)
        {
            this.CancelStatusFieldName = CancelStatusFieldName.Length == 0 ? "Status" :
                CancelStatusFieldName;
            this.CancelStatus = CancelStatus.Length == 0 ? "Batal" : CancelStatus;
            this.CancelDateTimeFieldName = CancelDateTimeFieldName.Length == 0 ? "TglBatal" :
                CancelDateTimeFieldName;
            this.CancelUserFieldName = CancelUserFieldName.Length == 0 ? "DibatalkanOleh" :
                CancelUserFieldName;
            this.CancelNotesFieldName = CancelNotesFieldName.Length == 0 ? "CatatanBatal" :
                CancelNotesFieldName;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class EmptyErrorAttribute : Attribute 
    {
        internal string ErrorMessage;

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }

        public EmptyErrorAttribute()
        {
            ErrorMessage = string.Empty;
        }
        public EmptyErrorAttribute(string ErrorMessage)
        {
            this.ErrorMessage = ErrorMessage;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class ViewEntityAttribute : Attribute
    {
        internal bool PersistView;
        internal Type[] TypeDependends;

        public ViewEntityAttribute(bool PersistView,
            params Type[] TypeDependends)
        {
            this.PersistView = PersistView;
            this.TypeDependends = TypeDependends;
        }
    }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class RegisterServiceAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class NoKeyEntityAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = true, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class RelationAttribute : Attribute
    {
        internal Type _ParentType;
        public Type ChildType;

        internal string[] _ParentFields;
        internal string[] _ChildFields;
        internal ParentUpdate _ParentUpdateRule;
        internal ParentDelete _ParentDeleteRule;

        public Type GetParentType() { return _ParentType; }
        public Type GetChildType() { return ChildType; }
        public string[] GetParentFields() { return _ParentFields; }
        public string[] GetChildFields() { return _ChildFields; }
        public ParentUpdate GetParentUpdateRule() { return _ParentUpdateRule; }
        public ParentDelete GetParentDeleteRule() { return _ParentDeleteRule; }

        public RelationAttribute(Type ParentType,
            string ParentFields, string ChildFields)
        {
            _ParentType = ParentType;
            _ParentFields = ParentFields.Split(',');
            _ChildFields = ChildFields.Split(',');
            _ParentUpdateRule = ParentUpdate.UpdateCascade;
            _ParentDeleteRule = ParentDelete.DeleteRestrict;
        }
        public RelationAttribute(Type ParentType)
        {
            _ParentType = ParentType;
            _ParentFields = null;
            _ChildFields = null;
            _ParentUpdateRule = ParentUpdate.UpdateCascade;
            _ParentDeleteRule = ParentDelete.DeleteRestrict;
        }

        public RelationAttribute(Type ParentType,
            string ParentFields, string ChildFields,
            ParentUpdate ParentUpdateRule,
            ParentDelete ParentDeleteRule)
        {
            _ParentType = ParentType;
            _ParentFields = ParentFields.Split(',');
            _ChildFields = ChildFields.Split(',');
            _ParentUpdateRule = ParentUpdateRule;
            _ParentDeleteRule = ParentDeleteRule;
        }
        public RelationAttribute(Type ParentType,
            ParentUpdate ParentUpdateRule,
            ParentDelete ParentDeleteRule)
        {
            _ParentType = ParentType;
            _ParentFields = null;
            _ChildFields = null;
            _ParentUpdateRule = ParentUpdateRule;
            _ParentDeleteRule = ParentDeleteRule;
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();

            return retVal
                .Append("Parent: ").AppendLine(_ParentType.Name)
                .Append(" - Fields: ").AppendLine(string.Join(", ", _ParentFields))
                .Append("Child: ").AppendLine(ChildType.Name)
                .Append(" - Fields: ").AppendLine(string.Join(", ", _ChildFields))
                .ToString();
        }
    }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = true, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class IndexAttribute : Attribute
    {
        internal string _IndexedFields;
        internal bool _Unique;

        public string GetIndexedFields() { return _IndexedFields; }
        public bool GetUnique() { return _Unique; }

        public IndexAttribute(string IndexedFields)
        {
            this._IndexedFields = IndexedFields;
            this._Unique = false;
        }
        public IndexAttribute(string IndexedFields, bool Unique)
        {
            this._IndexedFields = IndexedFields;
            this._Unique = Unique;
        }
    }

    [AttributeUsage(AttributeTargets.Class,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class TableNameAttribute : Attribute
    {
        internal string _TableName;
        public TableNameAttribute(string TableName) { _TableName = TableName; }
    }

    #region DataTypeAttribute
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public abstract class DataTypeAttribute : Attribute
    {
        //internal string _FieldName;
        internal DataType _DataType;
        internal int _Length;
        internal int _Scale;
        
        public string FormatString;
        public DataType GetDataType() { return _DataType; }
        public int GetLength() { return _Length; }
        public int GetScale() { return _Scale; }
        public bool BrowseHidden;
        public bool FormHidden;
        public object Default;
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeVarCharAttribute : DataTypeAttribute
    {
        public DataTypeVarCharAttribute(int Length)
        {
            _DataType = DataType.VarChar;
            _Length = Length;
            Default = string.Empty;
            FormatString = string.Empty;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeCharAttribute : DataTypeAttribute
    {
        public DataTypeCharAttribute(int Length)
        {
            _DataType = DataType.Char;
            _Length = Length;
            Default = string.Empty;
            FormatString = string.Empty;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeDecimalAttribute : DataTypeAttribute
    {
        public DataTypeDecimalAttribute()
        {
            _Length = 19;
            _Scale = 4;
            _DataType = DataType.Decimal;
            Default = (decimal)0;
            FormatString = BaseUtility.DefaultFormatDecimal;
        }
        public DataTypeDecimalAttribute(int Precision, int Scale)
        {
            _Length = Precision;
            _Scale = Scale;
            _DataType = DataType.Decimal;
            Default = (decimal)0;
            FormatString = BaseUtility.DefaultFormatDecimal;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeIntegerAttribute : DataTypeAttribute
    {
        public DataTypeIntegerAttribute()
        {
            _DataType = DataType.Integer;
            Default = 0;
            FormatString = BaseUtility.DefaultFormatInteger;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeDateAttribute : DataTypeAttribute
    {
        public DataTypeDateAttribute()
        {
            _DataType = DataType.Date;
            Default = "Today";
            FormatString = BaseUtility.DefaultFormatDate;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeTimeAttribute : DataTypeAttribute
    {
        public DataTypeTimeAttribute()
        {
            _DataType = DataType.Time;
            Default = "Now";
            FormatString = BaseUtility.DefaultFormatTime;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeDateTimeAttribute : DataTypeAttribute
    {
        public DataTypeDateTimeAttribute()
        {
            _DataType = DataType.DateTime;
            Default = "Now";
            FormatString = BaseUtility.DefaultFormatDateTime;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeBooleanAttribute : DataTypeAttribute
    {
        public DataTypeBooleanAttribute()
        {
            _DataType = DataType.Boolean;
            Default = true;
            FormatString = BaseUtility.DefaultFormatBoolean;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeImageAttribute : DataTypeAttribute
    {
        public DataTypeImageAttribute()
        {
            _DataType = DataType.Image;
            FormatString = string.Empty;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeBinaryAttribute : DataTypeAttribute
    {
        public DataTypeBinaryAttribute()
        {
            _DataType = DataType.Binary;
            FormatString = string.Empty;
        }
    }
    [DebuggerNonUserCode]
    public sealed class DataTypeTimeStampAttribute : DataTypeAttribute
    {
        public DataTypeTimeStampAttribute()
        {
            _DataType = DataType.TimeStamp;
            Default = "Now";
            FormatString = BaseUtility.DefaultFormatDateTime;
        }
    }
    #endregion

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class PrintCounterAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class TransactionDateAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public sealed class DataTypeLoadSqlAttribute : Attribute
    {
        public bool BrowseHidden;
        public bool FormHidden;

        private string _SqlQuery = string.Empty;
        internal int GetSqlQueryLen() { return _SqlQuery.Length; }

        private string _ChildTableName;
        internal void UpdateChildTableName(string ChildTableName)
        {
            _ChildTableName = ChildTableName;
        }

        private void FindRealField(TableDef td, string FieldName,
            out TableDef RealTd, out FieldDef RealFd)
        {
            FieldDef fld = td.GetFieldDef(FieldName);
            if (fld._dtlsa == null)
            {
                RealTd = td;
                RealFd = fld;
                return;
            }
            FindRealField(MetaData.GetTableDef(fld._dtlsa._ParentType),
                fld._dtlsa.ParentFieldName, out RealTd, out RealFd);
        }

        private string GetSqlQueryNested(int Depth)
        {
            string retVal;
            string DepthStr = "x" + (Depth + 1).ToString();
            string _ct = string.Concat("=", Depth == 0 ? 
                _ChildTableName : "x" + Depth.ToString(), ".");

            TableDef tdParent = MetaData.GetTableDef(_ParentType);
            FieldDef fldParent = tdParent.GetFieldDef(ParentFieldName);

            if (fldParent._dtlsa == null)
                retVal = string.Concat("SELECT ", ParentFieldName,
                    " FROM ", tdParent._TableName, " AS ", DepthStr, 
                    " WHERE ");
            else
                retVal = string.Concat("SELECT (", fldParent._dtlsa
                    .GetSqlQueryNested(Depth + 1), ") AS ", ParentFieldName,
                    " FROM ", tdParent._TableName, " AS ", DepthStr,
                    " WHERE ");

            string[] tmpStr = _RelationKeyField.Split(',');
            int NumRel = tmpStr.Length;


            //TableDef tdChild = MetaData.GetTableDef(_ChildTableName);
            // Kalo Child Virtual belum bisa...
            string tmpStr2 = tmpStr[0].Trim();
            string[] tmpData = tmpStr2.Split('=');
            if (tmpData.Length == 1)
            {
                //FieldDef fldChild = tdChild.GetFieldDef(tmpStr2);
                //if (fldChild._dtlsa != null)

                retVal = string.Concat(retVal, DepthStr, ".",
                    tmpStr2, _ct, tmpStr2);
            }
            else
            {
                retVal = string.Concat(retVal, DepthStr, ".",
                    tmpData[0], _ct, tmpData[1]);
            }
            for (int i = 1; i < NumRel; i++)
            {
                tmpStr2 = tmpStr[i];

                tmpData = tmpStr2.Split('=');
                if (tmpData.Length == 1)
                    retVal = string.Concat(retVal, " AND ", 
                        DepthStr, ".", tmpStr2, _ct, tmpStr2);
                else
                    retVal = string.Concat(retVal, " AND ",
                        DepthStr, ".", tmpData[0], _ct, tmpData[1]);
            }
            return retVal;
        }

        public string GetSqlQuery()
        {
            if (_SqlQuery.Length == 0)
                _SqlQuery = GetSqlQueryNested(0);
            return _SqlQuery;
        }

        private string _SqlQueryValue;
        FieldParam[] QueryValueParams; 

        internal string GetSqlQueryValue(BaseEntity Entity, 
            out FieldParam[] Parameters)
        {
            if (_SqlQueryValue != null)
            {
                foreach (FieldParam fldp in QueryValueParams)
                    fldp.Value = Entity;
            }
            else
            {
                string retVal;
                TableDef tdParent = MetaData.GetTableDef(_ParentType);
                FieldDef fldParent = tdParent.GetFieldDef(ParentFieldName);

                if (fldParent._dtlsa == null)
                    retVal = string.Concat("SELECT ", ParentFieldName,
                        " FROM ", tdParent._TableName, " AS x1 WHERE ");
                else
                    retVal = string.Concat("SELECT (", fldParent._dtlsa
                        .GetSqlQueryNested(1), ") AS ", ParentFieldName,
                        " FROM ", tdParent._TableName, " AS x1 WHERE ");

                string[] tmpStr = _RelationKeyField.Split(',');
                int NumRel = tmpStr.Length;

                TableDef td = MetaData.GetTableDef(Entity.GetType());
                FieldDef fld;

                QueryValueParams = new FieldParam[NumRel];

                string tmpStr2 = tmpStr[0].Trim();
                string[] tmpData = tmpStr2.Split('=');
                if (tmpData.Length == 1)
                {
                    retVal = string.Concat(retVal, "x1.",
                        tmpStr2, "=@0");
                    fld = td.GetFieldDef(tmpStr2);
                }
                else
                {
                    retVal = string.Concat(retVal, "x1.",
                        tmpData[0], "=@0");
                    fld = td.GetFieldDef(tmpData[1]);
                }
                QueryValueParams[0] = new FieldParam("0", fld,
                    fld.GetValue(Entity));

                for (int i = 1; i < NumRel; i++)
                {
                    tmpStr2 = tmpStr[i];

                    tmpData = tmpStr2.Split('=');
                    if (tmpData.Length == 1)
                    {
                        retVal = string.Concat(retVal, 
                            " AND x1.", tmpStr2, "=@", i.ToString());
                        fld = td.GetFieldDef(tmpStr2);
                    }
                    else
                    {
                        retVal = string.Concat(retVal, 
                            " AND x1.", tmpData[0], "=@", i.ToString());
                        fld = td.GetFieldDef(tmpData[1]);
                    }
                    QueryValueParams[i] = new FieldParam(i.ToString(), 
                        fld, fld.GetValue(Entity));
                }
                _SqlQueryValue = retVal;
            }
            Parameters = QueryValueParams;
            return _SqlQueryValue;
        }

        internal Type _ParentType;
        internal string _RelationKeyField;
        internal DataType _DataType;

        /// <summary>
        /// Diisi Nama Field Parent apabila tidak sama dengan Field Child
        /// </summary>
        internal string ParentFieldName = string.Empty;

        /// <summary>
        /// Load otomatis data menggunakan perintah Sql
        /// </summary>
        /// <param name="ParentType">Entity Induk yg akan dicari</param>
        /// <param name="RelationKeyField">Isi dgn Field Relasi Parent-Child atau bila berbeda nama, isi dgn: '[KeyFieldParent]=[KeyFieldChild],...</param>
        public DataTypeLoadSqlAttribute(Type ParentType, string RelationKeyField)
        {
            _ParentType = ParentType;
            _RelationKeyField = RelationKeyField;
        }

        /// <summary>
        /// Load otomatis data menggunakan perintah Sql
        /// </summary>
        /// <param name="ParentType">Entity Induk yg akan dicari</param>
        /// <param name="RelationKeyField">Isi dgn Field Relasi Parent-Child atau bila berbeda nama, isi dgn: '[KeyFieldParent]=[KeyFieldChild],...</param>
        public DataTypeLoadSqlAttribute(Type ParentType, string RelationKeyField, string ParentFieldName)
        {
            _ParentType = ParentType;
            _RelationKeyField = RelationKeyField;
            this.ParentFieldName = ParentFieldName;
        }
        public DataTypeLoadSqlAttribute(string SqlQuery, DataType DataType)
        {
            _SqlQuery = SqlQuery;
            _DataType = DataType;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false, Inherited = true)]
    [DebuggerNonUserCode]
    public class PrimaryKeyAttribute : Attribute { }

    [DebuggerNonUserCode]
    public sealed class CounterKeyAttribute : PrimaryKeyAttribute { }

    [DebuggerNonUserCode]
    public sealed class AutoNumberKeyAttribute : PrimaryKeyAttribute, 
        IAutoNumberAttribute
    {
        internal string _Template;
        internal string _CounterText;
        internal string _DateField;
        internal string _FieldName;

        public string GetTemplate() { return _Template; }
        public string GetCounterText() { return _CounterText; }
        public string GetDateField() { return _DateField; }
        public string GetFieldName() { return _FieldName; }

        public AutoNumberKeyAttribute(string Template,
            string CounterText, string DateField)
        {
            this._Template = Template;
            this._CounterText = CounterText;
            this._DateField = DateField;
        }
        public AutoNumberKeyAttribute(string Template,
            string CounterText)
        {
            this._Template = Template;
            this._CounterText = CounterText;
            this._DateField = string.Empty;
        }
    }

    internal interface IAutoNumberAttribute
    {
        string GetTemplate();
        string GetFieldName();
        string GetDateField();
        string GetCounterText();
    }

    [DebuggerNonUserCode]
    public sealed class AutoNestedKeyAttribute : PrimaryKeyAttribute
    {
        internal string _ParentField;
        internal string _FieldName;

        public string GetParentField() { return _ParentField; }
        public string GetFieldName() { return _FieldName; }

        public AutoNestedKeyAttribute(string ParentField)
        {
            _ParentField = ParentField;
        }
    }

    [DebuggerNonUserCode]
    public sealed class AutoNumberAttribute : Attribute, 
        IAutoNumberAttribute
    {
        internal string _Template;
        internal string _CounterText;
        internal string _DateField;
        internal string _FieldName;

        public string GetTemplate() { return _Template; }
        public string GetCounterText() { return _CounterText; }
        public string GetDateField() { return _DateField; }
        public string GetFieldName() { return _FieldName; }

        public AutoNumberAttribute(string Template,
            string CounterText, string DateField)
        {
            this._Template = Template;
            this._CounterText = CounterText;
            this._DateField = DateField;
        }
        public AutoNumberAttribute(string Template,
            string CounterText)
        {
            this._Template = Template;
            this._CounterText = CounterText;
            this._DateField = string.Empty;
        }
    }
}
