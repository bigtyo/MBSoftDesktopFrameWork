using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SentraSolutionFramework.Persistance;
using System.Reflection;
using System.Collections;
using SentraUtility;
using SentraUtility.Expression;

namespace SentraSolutionFramework.Entity
{
    public interface IFilterForm
    {
        Dictionary<string, object> FilterList { get; set; }
    }

    public interface IShowView
    {
        void ShowView(params object[] Parameters);
    }

    [NoKeyEntity]
    public abstract class ReportEntity : BaseEntity, IReportEntity, 
        IFilterForm, IShowView
    {
        private OnAction OnReportRefresh;
        protected void RefreshReport()
        {
            if (OnReportRefresh != null) OnReportRefresh();
        }
        private OnAction OnFormChanged;
        protected void FormChanged()
        {
           if (OnFormChanged != null) OnFormChanged();
        }

        protected virtual void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            DataSource = string.Empty;
            DataSourceOrder = string.Empty;
        }

        protected virtual void GetDataSource(out Type ObjType,
            out string Condition, out string DataSourceOrder, 
            List<FieldParam> Parameters)
        {
            ObjType = null;
            Condition = string.Empty;
            DataSourceOrder = string.Empty;
        }

        protected virtual IList GetDataSource() { return null; }

        protected virtual string GetColumnHidden() { return string.Empty; }

        protected virtual string GetColumnFormat() { return string.Empty; }

        protected virtual void ShowView(params object[] Parameters) { }

        DataPersistance _Dp;
        public override DataPersistance Dp
        {
            get
            {
                return _Dp ?? BaseFramework.GetDefaultDp(GetType().Assembly);
            }
            set
            {
                _Dp = value;
            }
        }

        #region IReportEntity Members
        string IReportEntity.GetColumnHidden()
        {
            return GetColumnHidden();
        }

        void IReportEntity.GetVariables(Dictionary<string, object> Variables)
        {
            FieldInfo[] fis = GetType().GetFields(BindingFlags.Instance | 
                BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (FieldInfo fi in fis)
            {
                Type tp = fi.FieldType;
                if (tp.IsEnum)
                    Variables[fi.Name] = EnumDef.GetEnumName(tp, fi.GetValue(this));
                else if (tp == typeof(int))
                    Variables[fi.Name] = Convert.ToDecimal(fi.GetValue(this));
                else if (tp == typeof(decimal) || tp == typeof(string) ||
                    tp == typeof(DateTime) || tp == typeof(bool))
                    Variables[fi.Name] = fi.GetValue(this);
            }
            PropertyInfo[] pis = GetType().GetProperties(BindingFlags.Instance | 
                BindingFlags.Public | BindingFlags.DeclaredOnly);
            foreach (PropertyInfo pi in pis)
            {
                Type tp = pi.PropertyType;
                if (tp.IsEnum)
                    Variables[pi.Name] = EnumDef.GetEnumName(tp, pi.GetValue(this, null));
                else if (tp == typeof(int))
                    Variables[pi.Name] = Convert.ToDecimal(pi.GetValue(this, null));
                else if (tp == typeof(decimal) || tp == typeof(string) ||
                    tp == typeof(DateTime) || tp == typeof(bool))
                    Variables[pi.Name] = pi.GetValue(this, null);
            }

            //BeforeShowReport(Variables);
        }

        void IReportEntity.GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters)
        {
            GetDataSource(out DataSource, out DataSourceOrder, Parameters);

            if (DataSource.Length == 0)
            {
                Type ObjType;
                string Condition;

                GetDataSource(out ObjType, out Condition,
                    out DataSourceOrder, Parameters);
                if (ObjType == null)
                    throw new ApplicationException("DataSource Laporan belum ditentukan");
                TableDef td = MetaData.GetTableDef(ObjType);
                if (td != null)
                {
                    Dp.ValidateTableDef(td);
                    DataSource = "SELECT * FROM " + td.GetSqlHeaderView(Dp);
                    if (Condition.Length > 0)
                        DataSource = string.Concat(DataSource,
                            " WHERE ", Condition);
                } else
                    throw new ApplicationException("DataSource Laporan belum ditentukan");
            }
            DataChanged();
            FormChanged();
        }

        IList IReportEntity.GetDataSource()
        {
            IList RetVal = GetDataSource();
            DataChanged();
            FormChanged();
            return RetVal;
        }

        void IReportEntity.InitUI()
        {
            InitUI();
        }
        void IReportEntity.EndUI()
        {
            EndUI();
        }

        void IReportEntity.SetPersistanceField(Dictionary<string, object> Variables)
        {
            TableDef td = MetaData.GetTableDef(GetType());
            foreach (KeyValuePair<string, object> kvp in Variables)
            {
                FieldDef fld = td.GetFieldDef(kvp.Key);
                if (fld != null)
                {
                    object TmpVal;
                    switch (fld.DataType)
                    {
                        case DataType.Integer:
                            TmpVal = Convert.ToInt32(kvp.Value);
                            break;
                        default:
                            TmpVal = kvp.Value;
                            break;
                    }

                    try
                    {
                        fld.SetLoadValue(this, TmpVal);
                    }
                    catch { }
                }
            }
        }

        void IReportEntity.SetReportRefresh(OnAction ReportRefresh)
        {
            OnReportRefresh = ReportRefresh;
        }
        void IReportEntity.SetFormChanged(OnAction FormChanged)
        {
            OnFormChanged = FormChanged;
        }
        string IReportEntity.GetColumnFormat()
        {
            return GetColumnFormat();
        }
        #endregion

        protected virtual void InitUI() { }
        protected virtual void EndUI() { }

        public virtual bool IsReadOnly(string FieldName) { return false; }
        public virtual bool IsVisible(string FieldName) { return true; }

        protected void UpdateLoadSqlFields()
        {
            MetaData.GetTableDef(GetType())
                .UpdateLoadSqlFields(string.Empty, this);
            DataChanged();
        }

        protected void UpdateLoadSqlFields(string FieldList)
        {
            MetaData.GetTableDef(GetType())
                .UpdateLoadSqlFields(FieldList, this);
            DataChanged();
        }

        #region IFilterForm Members

        Dictionary<string, object> IFilterForm.FilterList
        {
            get
            {
                Dictionary<string, object> Variables = new Dictionary<string, object>();
                ((IReportEntity)this).GetVariables(Variables);

                IList ds = GetDataSource();
                if (ds != null)
                    Variables.Add("DataSource", ds);
                else
                {
                    string DataSource;
                    string DataSourceOrder;
                    List<FieldParam> Parameters = new List<FieldParam>();
                    GetDataSource(out DataSource, out DataSourceOrder, Parameters);
                    Variables.Add("DataSource", DataSource);
                    if (DataSourceOrder.Length > 0)
                        Variables.Add("DataSourceOrder", DataSourceOrder);
                    if (Parameters.Count > 0)
                        Variables.Add("DataSourceParams", Parameters);
                }
                string ColHidden = GetColumnHidden();
                if (ColHidden.Length > 0)
                    Variables.Add("ColumnHidden", ColHidden);
                return Variables;
            }
            set
            {
                ((IReportEntity)this).SetPersistanceField(value);
            }
        }

        #endregion

        #region IShowView Members

        void IShowView.ShowView(params object[] Parameters)
        {
            ShowView(Parameters);
        }

        #endregion
    }

    /// <summary>
    /// (For Internal Use)
    /// </summary>
    public interface IReportEntity
    {
        DataPersistance Dp { get; set; }

        void GetVariables(Dictionary<string, object> Variables);

        string GetColumnHidden();

        void GetDataSource(out string DataSource,
            out string DataSourceOrder, List<FieldParam> Parameters);

        IList GetDataSource();

        string GetColumnFormat();

        void InitUI();
        void EndUI();

        void SetPersistanceField(Dictionary<string, object> Variables);

        void DataChanged();
        void SetReportRefresh(OnAction ReportRefresh);
        void SetFormChanged(OnAction FormChanged);

        bool IsReadOnly(string FieldName);
        bool IsVisible(string FieldName);
    }
}
