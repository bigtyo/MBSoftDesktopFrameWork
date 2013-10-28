using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using SentraWinFramework.Report;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using SentraUtility;
using SentraSolutionFramework.Entity;
using System.Diagnostics;

namespace SentraWinFramework
{
    public delegate void DataSelect(string Condition);
    public delegate Form BrowseData();

    internal enum BrowseType
    {
        None, Tabular, FreeLayout, Chart, PivotTable
    }

    public class EntityForm
    {
        private static Type UINType= typeof(UINavigator);

        internal Form _Form;
        internal Form _BrowseForm;

        internal BrowseType _BrowseType = BrowseType.None;

        private UINavigator _UIN;
        internal IEntityForm _Ief;

        internal Type FormType;
        internal Type FilterFormType;
        internal Type EntityType;

        internal string ModuleName;

        private DataPersistance _Dp;
        public DataPersistance DataPersistance
        {
            get
            {
                if (_Dp != null)
                    return _Dp;
                if (EntityType == null)
                    return BaseFramework.DefaultDp;
                return BaseFramework.GetDefaultDp(EntityType.Assembly);
            }
            set { _Dp = value; }
        }

        private string _DataFilter = string.Empty;
        public string DataFilter
        {
            get { return _DataFilter; }
            set
            {
                _DataFilter = value;
                if (_UIN != null)
                    _UIN.Navigator.Filter = value;
            }
        }
        
        public string BrowseColumns = string.Empty;

        public string BrowseSql = string.Empty;
        public string BrowseCondition = string.Empty;
        public string BrowseOrder = string.Empty;

        public Dictionary<string, string> BrowseFormat = new Dictionary<string, string>();

        public BrowseData DoBrowseData;

        private EntityForm(string ModuleName)
        {
            DoBrowseData = ShowTabular;
            this.ModuleName = ModuleName;
            this._DataFilter = string.Empty;
        }
        private EntityForm(string ModuleName, string DataFilter)
        {
            DoBrowseData = ShowTabular;
            this.ModuleName = ModuleName;
            this._DataFilter = DataFilter;
        }

        public EntityForm(Type FormType)
        {
            _Dp = BaseFramework.GetDefaultDp(FormType.Assembly);
            this.FormType = FormType;
            DoBrowseData = ShowTabular;
            this.ModuleName = BaseWinFramework.GetModuleName(FormType);
            this._DataFilter = string.Empty;
        }

        public EntityForm(Type FormType, string DataFilter)
        {
            _Dp = BaseFramework.GetDefaultDp(FormType.Assembly);
            this.FormType = FormType;
            DoBrowseData = ShowTabular;
            this.ModuleName = BaseWinFramework.GetModuleName(FormType);
            this._DataFilter = DataFilter;
        }

        public EntityForm(Type FormType, Type FilterFormType)
        {
            Type tp = FormType ?? FilterFormType;
            _Dp = BaseFramework.GetDefaultDp(tp.Assembly);
            this.FormType = FormType;
            DoBrowseData = ShowTabular;
            this.ModuleName = BaseWinFramework.GetModuleName(
                FormType ?? FilterFormType);
            this._DataFilter = string.Empty;
            this.FilterFormType = FilterFormType;
        }

        public EntityForm(Type FormType, Type FilterFormType, string DataFilter)
        {
            Type tp = FormType ?? FilterFormType;
            _Dp = BaseFramework.GetDefaultDp(tp.Assembly);
            this.FormType = FormType;
            DoBrowseData = ShowTabular;
            this.ModuleName = BaseWinFramework.GetModuleName(
                FormType ?? FilterFormType);
            this._DataFilter = DataFilter;
            this.FilterFormType = FilterFormType;
        }

        private void CheckEntityType()
        {
            if (FormType == null)
            {
                TypeChecked = true;
                return;
            }
            if (_Form == null || _Form.IsDisposed)
            {
                if (FormType.GetInterface("IEntityControl") != null)
                    _Form = new frmDocument(FormType);
                else if (FormType.IsSubclassOf(typeof(Form)))
                    _Form = BaseFactory.CreateInstance(FormType) as Form;
                else
                    throw new ApplicationException("FormType harus Form, IEntityControl");

                _Ief = _Form as IEntityForm;
                if (_Ief == null)
                {
                    foreach (Control Ctrl in _Form.Controls)
                        if (object.ReferenceEquals(Ctrl.GetType(), UINType))
                        {
                            _UIN = (UINavigator)Ctrl;
                            _UIN.EntityForm = this;
                            break;
                        }
                }
                else
                    _Ief.SetOwner(this);

                _Form.MdiParent = BaseWinFramework._MdiParent;
            }
            if (!TypeChecked)
            {
                EntityType = _UIN != null ? _UIN.GetEntity().GetType() :
                    _Ief != null ? _Ief.GetEntityType() : null;
                TypeChecked = true;
            }
        }

        public Form ShowForm()
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.DocumentView, true)) return null;

            using (new WaitCursor())
            {
                CheckEntityType();
                _Form.Show();
                if (_UIN != null) _UIN.SetSecurity(ModuleName);
                _Form.BringToFront();
                return _Form;
            }
        }
        public Form ShowNew()
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.DocumentNew, true)) return null;

            using (new WaitCursor())
            {
                CheckEntityType();
                _Form.Show();

                if (_UIN != null)
                {
                    _UIN.SetNew();
                    _UIN.SetSecurity(ModuleName);
                }
                else if (_Ief != null)
                    _Ief.ShowNew();
                _Form.BringToFront();
                return _Form;
            }
        }

        public Form ShowView(string Condition, 
            params FieldParam[] Parameters)
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.DocumentView, true)) return null;

            using (new WaitCursor())
            {
                CheckEntityType();
                _Form.Show();
                if (_UIN != null)
                {
                    _UIN.SetSecurity(ModuleName);
                    _UIN.FindData(Condition, Parameters);
                }
                else if (_Ief != null)
                    _Ief.ShowView(Condition, Parameters);

                _Form.BringToFront();
                return _Form;
            }
        }
        public Form ShowViewWithKey(string Key)
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.DocumentView, true)) return null;

            using (new WaitCursor())
            {
                CheckEntityType();
                _Form.Show();
                if (_UIN != null)
                {
                    _UIN.SetSecurity(ModuleName);
                    _UIN.FindKey(Key);
                }
                else if (_Ief != null)
                    _Ief.ShowViewWithKey(Key);

                _Form.BringToFront();
                return _Form;
            }
        }

        private bool TypeChecked = false;

        internal FieldDef fldTransactionDate;

        public Form ShowTabular()
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName, 
                SecurityVarName.ReportView, true)) return null;

            using (new WaitCursor())
            {
                if (!TypeChecked) CheckEntityType();

                if (_UIN != null)
                {
                    IRuleInitUI riu = (IRuleInitUI)_UIN.Entity;
                    _DataFilter = riu.GetBrowseFilter();
                    BrowseColumns = riu.GetBrowseColumns();
                    riu.GetBrowseSql(out BrowseSql, out BrowseCondition, out BrowseOrder);
                    BrowseFormat.Clear();
                    riu.GetBrowseFormat(BrowseFormat);

                    TableDef td = MetaData.GetTableDef(riu.GetType());
                    if (riu.GetFieldTransactionDate().Length == 0)
                        fldTransactionDate = td.fldTransactionDate;
                    else
                        fldTransactionDate = td.GetFieldDef(
                            riu.GetFieldTransactionDate());
                }

                if (_BrowseForm == null || _BrowseForm.IsDisposed || 
                    _BrowseType != BrowseType.Tabular)
                {
                    if (BaseWinFramework.TouchScreenVersion)
                        _BrowseForm = new frmGridReportTC();
                    else
                        _BrowseForm = new frmGridReport();
                    _BrowseForm.MdiParent = BaseWinFramework._MdiParent;
                    ((IBrowseForm)_BrowseForm).ShowForm(this, ModuleName);
                    _BrowseType = BrowseType.Tabular;
                }
                else
                    _BrowseForm.BringToFront();
                return _BrowseForm;
            }
        }
        public Form ShowFreeLayout()
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.ReportView, true)) return null;

            using (new WaitCursor())
            {
                if (!TypeChecked) CheckEntityType();

                if (_UIN != null)
                {
                    IRuleInitUI riu = (IRuleInitUI)_UIN.Entity;
                    _DataFilter = riu.GetBrowseFilter();
                    BrowseColumns = riu.GetBrowseColumns();
                    riu.GetBrowseSql(out BrowseSql, out BrowseCondition, out BrowseOrder);
                    BrowseFormat.Clear();
                    riu.GetBrowseFormat(BrowseFormat);
                }

                if (_BrowseForm == null || _BrowseForm.IsDisposed ||
                    _BrowseType != BrowseType.FreeLayout)
                {
                    if (BaseWinFramework.mdiRibbonControl != null)
                        _BrowseForm = new frmFreeReport();
                    else
                        _BrowseForm = new frmFreeReport2();

                    _BrowseForm.MdiParent = BaseWinFramework._MdiParent;
                    ((IFreeReport)_BrowseForm).ShowForm(this, ModuleName);
                    _BrowseType = BrowseType.FreeLayout;
                }
                else
                    _BrowseForm.BringToFront();
                return _BrowseForm;
            }
        }

        public Form ShowTabular(string FreeFilter, 
            object TransStartDate, object TransEndDate, 
            params object[] Parameters)
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.ReportView, true)) return null;

            using (new WaitCursor())
            {
                if (!TypeChecked) CheckEntityType();

                if (_UIN != null)
                {
                    IRuleInitUI riu = (IRuleInitUI)_UIN.Entity;
                    _DataFilter = riu.GetBrowseFilter();
                    BrowseColumns = riu.GetBrowseColumns();
                    riu.GetBrowseSql(out BrowseSql, out BrowseCondition, out BrowseOrder);
                    BrowseFormat.Clear();
                    riu.GetBrowseFormat(BrowseFormat);

                    TableDef td = MetaData.GetTableDef(riu.GetType());
                    if (riu.GetFieldTransactionDate().Length == 0)
                        fldTransactionDate = td.fldTransactionDate;
                    else
                        fldTransactionDate = td.GetFieldDef(
                            riu.GetFieldTransactionDate());
                }

                if (_BrowseForm == null || _BrowseForm.IsDisposed ||
                    _BrowseType != BrowseType.Tabular)
                {
                    if (BaseWinFramework.TouchScreenVersion)
                        _BrowseForm = new frmGridReportTC();
                    else
                        _BrowseForm = new frmGridReport();

                    _BrowseForm.MdiParent = BaseWinFramework._MdiParent;
                    _BrowseType = BrowseType.Tabular;
                    ((IBrowseForm)_BrowseForm).ShowForm2(this, ModuleName,
                        FreeFilter, TransStartDate, TransEndDate, Parameters);
                }
                else
                    ((IBrowseForm)_BrowseForm).ShowForm3(
                        FreeFilter, TransStartDate, TransEndDate, Parameters);

                return _BrowseForm;
            }
        }
        public Form ShowFreeLayout(string FreeFilter,
            object TransStartDate, object TransEndDate, 
            params object[] Parameters)
        {
            if (!BaseWinFramework.CheckModuleAccessWithError(ModuleName,
                SecurityVarName.ReportView, true)) return null;

            using (new WaitCursor())
            {
                if (!TypeChecked) CheckEntityType();

                if (_BrowseForm == null || _BrowseForm.IsDisposed ||
                    _BrowseType != BrowseType.FreeLayout)
                {
                    if (BaseWinFramework.mdiRibbonControl != null)
                        _BrowseForm = new frmFreeReport();
                    else
                        _BrowseForm = new frmFreeReport2();

                    _BrowseForm.MdiParent = BaseWinFramework._MdiParent;
                    _BrowseType = BrowseType.FreeLayout;
                    ((IFreeReport)_BrowseForm).ShowForm2(this, ModuleName,
                        FreeFilter, TransStartDate, TransEndDate, Parameters);
                }
                else
                    ((IFreeReport)_BrowseForm).ShowForm3(
                        FreeFilter, TransStartDate, TransEndDate, Parameters);
                return _BrowseForm;
            }
        }
    }

    [DebuggerNonUserCode]
    public class EntityForm<TForm> : EntityForm
        where TForm : new()
    {
        public EntityForm() : base(typeof(TForm)) { }
        public EntityForm(string DataFilter) : 
            base(typeof(TForm), DataFilter) { }
    }

    [DebuggerNonUserCode]
    public class EntityForm<TEntityForm, TFilterForm> : EntityForm
        where TEntityForm : new()
        where TFilterForm : IFilterForm
    {
        public EntityForm() 
            : base(typeof(TEntityForm), typeof(TFilterForm)) { }
        public EntityForm(string DataFilter) 
            : base(typeof(TEntityForm), typeof(TFilterForm), DataFilter) { }
    }
}
