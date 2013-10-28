using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraUtility;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SentraSolutionFramework;
using SentraUtility.Expression;
using SentraSecurity;
using System.Collections;
using System.IO;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmFreeReport2 : 
        frmReport, IFreeReport
    {
        private string CurrPrintLayoutId;

        public void ShowForm(EntityForm EntityForm, string ReportName)
        {
            _ReportName = ReportName;
            _EntityForm = EntityForm;

            if (EntityForm.ModuleName.Length > 0 && BaseSecurity.CurrentLogin
                .CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(EntityForm.ModuleName);

                if (!ma.GetVariable<bool>(SecurityVarName.ReportDesignPrint, false))
                    comboBoxEdit1.Properties.Buttons[2].Enabled = false;
            }

            _Evaluator = BaseFactory.CreateInstance<Evaluator>();
            if (_ReportName.Contains("Laporan"))
                Text = _ReportName;
            else
                Text = "Laporan " + _ReportName;

            _FilterForm = BaseFactory.CreateInstance(
                _EntityForm.FilterFormType) as IFilterForm;
            if (_FilterForm == null)
            {
                XtraMessageBox.Show("Form Filter harus implement Interface IFilterForm !",
                    "Error Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (_EntityForm.FilterFormType == null || _FilterForm as ReportEntity != null)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            else
            {
                XtraForm Frm = _FilterForm as XtraForm;
                if (Frm != null)
                    InitFilterForm(Frm, xtraScrollableControl1,
                        splitContainerControl1);
                else
                    QueryReportEntity(xtraScrollableControl1,
                        splitContainerControl1);
            }

            _ShowViewForm = _FilterForm as IShowView;

            List<string> ListItems = DocPrintBrowseLayout
                .GetListLayout("F_" + _ReportName);

            if (ListItems.Count == 0)
            {
                if (re != null)
                {
                    Dictionary<string, object> Vars = 
                        new Dictionary<string, object>();
                    re.GetVariables(Vars);
                    SaveReportFromTemplateFolder("F_", ReportName,
                        ListItems, Vars);
                }
                else
                    SaveReportFromTemplateFolder("F_", ReportName,
                        ListItems, _FilterForm != null ?
                        _FilterForm.FilterList : null);
            }
            comboBoxEdit1.Properties.Items.AddRange(ListItems);

            string CurrBrowseLayoutId = string.Empty;
            bool Tmp;
            DocDefault.GetDefaultLayout(_ReportName,
                out CurrBrowseLayoutId, out CurrPrintLayoutId, out Tmp);
            comboBoxEdit1.SelectedItem = CurrBrowseLayoutId;
            if (comboBoxEdit1.SelectedIndex < 0 && comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;
            else
                comboBoxEdit1_SelectedIndexChanged(null, null);
            Show();
            printControl1.Focus();
        }

        public frmFreeReport2()
        {
            InitializeComponent();
        }

        private bool LoadFilter = true;
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                BaseWinFramework.WinForm.AutoLockEntity.LockForm(re, this);

                try
                {
                    MemoryStream ms;
                    Dictionary<string, object> QueryFormFilter = 
                        new Dictionary<string, object>();
                    DocPrintBrowseLayout.GetFreeLayoutData("F_" + _ReportName,
                        comboBoxEdit1.Text, out ms, QueryFormFilter);
                    
                    if (_FilterForm != null)
                    {
                        try
                        {
                            if (re != null)
                            {
                                if (LoadFilter)
                                    re.SetPersistanceField(QueryFormFilter);
                                else
                                    LoadFilter = true;
                                re.GetVariables(QueryFormFilter);
                                re.DataChanged();
                            }
                            else
                            {
                                if (QueryFormFilter != null)
                                    _FilterForm.FilterList = QueryFormFilter;
                                QueryFormFilter = _FilterForm.FilterList;
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, 
                                "Error Baca Filter Data", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Exclamation);
                        }

                        if (re != null)
                        {
                            _DataSource = re.GetDataSource();
                            if (_DataSource == null)
                            {
                                string SqlQuery;
                                string SqlOrder;
                                List<FieldParam> ListParam = new List<FieldParam>();
                                re.GetDataSource(out SqlQuery, out SqlOrder,
                                    ListParam);
                                if (SqlQuery.Length > 0)
                                {
                                    if (SqlOrder.Length > 0)
                                        SqlQuery = string.Concat("SELECT * FROM (",
                                            SqlQuery, ") x ORDER BY ", SqlOrder);

                                    _DataSource = re.Dp.OpenDataTable(SqlQuery, ListParam);
                                }
                            }
                            re.GetVariables(QueryFormFilter);
                        }
                        else
                            _DataSource = null;

                        foreach (KeyValuePair<string, object> Filter in QueryFormFilter)
                        {
                            Type tp = Filter.Value.GetType();

                            if (Filter.Key != "DataSource")
                            {
                                if (Filter.Key == "DataSourceOrder" ||
                                    Filter.Key == "DataSourceParams") continue;
                                if (tp == typeof(string))
                                    _Evaluator.Variables.Add(Filter.Key, (string)Filter.Value);
                                else if (tp == typeof(int) ||
                                    tp == typeof(decimal) ||
                                    tp == typeof(Single) ||
                                    tp == typeof(double) ||
                                    tp.IsEnum)
                                    _Evaluator.Variables.Add(Filter.Key, Convert.ToDecimal(Filter.Value));
                                else if (tp == typeof(bool))
                                    _Evaluator.Variables.Add(Filter.Key, (bool)Filter.Value);
                                else if (tp == typeof(DateTime))
                                    _Evaluator.Variables.Add(Filter.Key, (DateTime)Filter.Value);
                                else
                                    _Evaluator.ObjValues.Add(Filter.Key, Filter.Value);
                            }
                            else if (tp == typeof(string))
                            {
                                string SqlQuery = ((string)Filter.Value).Trim();
                                object SqlOrder;
                                if (QueryFormFilter.TryGetValue("DataSourceOrder", out SqlOrder))
                                    SqlQuery = string.Concat("SELECT * FROM (",
                                        SqlQuery, ") x ORDER BY ", (string)SqlOrder);

                                if (SqlQuery.Length > 0)
                                {
                                    object p;
                                    QueryFormFilter.TryGetValue("DataSourceParams", out p);
                                    if (p != null)
                                    {
                                        List<FieldParam> ListParam = p as List<FieldParam>;
                                        if (ListParam != null)
                                            _DataSource = _EntityForm.DataPersistance
                                                .OpenDataTable(SqlQuery, ListParam);
                                        else
                                            _DataSource = _EntityForm.DataPersistance
                                            .OpenDataTable(SqlQuery, p as FieldParam[]);
                                    }
                                    else
                                        _DataSource = _EntityForm.DataPersistance
                                            .OpenDataTable(SqlQuery);
                                }
                            }
                            else
                                _DataSource = Filter.Value;
                        }
                    }
                    xReport Rpt = new xReport(_Evaluator);
                    if (_DataSource != null)
                    {
                        if (_DataSource.GetType().Name == "DataTable")
                            Rpt.DataSource = _DataSource;
                        else
                        {
                            IList ds = _DataSource as IList;
                            if (ds == null)
                            {
                                ds = new BindingList<object>();
                                ds.Add(_DataSource);
                            }
                            Rpt.DataSource = ds;
                        }
                    }
                    if (ms != null)
                    {
                        Rpt.LoadLayout(ms);
                        //Rpt.ScriptReferences = new string[] { GetType().Assembly.Location };
                        printControl1.PrintingSystem = Rpt.PrintingSystem;
                        Rpt.CreateDocument();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        "Error Baca Layout Laporan", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        protected override void RefreshReport()
        {
            comboBoxEdit1.Properties.Items.Clear();
            comboBoxEdit1.Properties.Items.AddRange(
                DocPrintBrowseLayout.GetListLayout("F_" + _ReportName));
            LoadFilter = false;
            if (comboBoxEdit1.SelectedIndex == -1)
                comboBoxEdit1.SelectedIndex = 0;
            else
                comboBoxEdit1_SelectedIndexChanged(null, null);
        }

        private void comboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1: //Refresh
                    RefreshReport();
                    break;
                case 2: //Layout Design
                    using (new WaitCursor())
                    {
                        if (rptl == null) rptl = new ReportLayout();
                        if (rptl.CurrentForm == null || rptl
                            .CurrentForm.IsDisposed)
                        {
                            if (_DataSource != null)
                            {
                                if (_DataSource.GetType().Name == 
                                    "DataTable")
                                {
                                    DataTable dt = (DataTable)_DataSource;
                                    if (dt.TableName.Length == 0)
                                        dt.TableName = _ReportName;
                                    rptl.ShowForm4(_ReportName, 
                                        new ReportDataTable(dt), _Evaluator);
                                }
                                else
                                    rptl.ShowForm4(_ReportName, 
                                        new _ReportEntity(_DataSource),
                                        _Evaluator);
                            }
                            else
                                rptl.ShowForm3(_ReportName, 
                                    _Evaluator);
                        }
                        else
                            rptl.BringToFront();
                    }
                    break;
                case 3: //Set Default
                    break;
            }
        }

        private void frmFreeReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            switch (e.KeyCode)
            {
                case Keys.R:    //Refresh
                    RefreshReport();
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        public void ShowForm2(EntityForm EntityForm, 
            string ReportName, string FreeFilter, 
            object TransStartDate, object TransEndDate, 
            object[] Parameters)
        {
            _ReportName = ReportName;
            _EntityForm = EntityForm;

            if (EntityForm.ModuleName.Length > 0 && BaseSecurity.CurrentLogin
                .CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(EntityForm.ModuleName);

                if (!ma.GetVariable<bool>(SecurityVarName.ReportDesignPrint, false))
                    comboBoxEdit1.Properties.Buttons[2].Enabled = false;
            }

            _Evaluator = BaseFactory.CreateInstance<Evaluator>();
            if (_ReportName.Contains("Laporan"))
                Text = _ReportName;
            else
                Text = "Laporan " + _ReportName;

            _FilterForm = BaseFactory.CreateInstance(
                _EntityForm.FilterFormType) as IFilterForm;
            if (_FilterForm == null)
            {
                XtraMessageBox.Show("Form Filter harus implement Interface IFilterForm !",
                    "Error Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            _ShowViewForm = _FilterForm as IShowView;
            if (_ShowViewForm == null)
            {
                XtraMessageBox.Show("Form Filter harus implement Interface IShowView !",
                    "Error Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            if (_EntityForm.FilterFormType == null || _FilterForm as ReportEntity != null)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            else
            {
                XtraForm Frm = _FilterForm as XtraForm;
                if (Frm != null)
                    InitFilterForm(Frm, xtraScrollableControl1, 
                        splitContainerControl1);
                else
                    QueryReportEntity(xtraScrollableControl1,
                        splitContainerControl1);
            }

            List<string> ListItems = DocPrintBrowseLayout
                .GetListLayout("F_" + _ReportName);

            if (ListItems.Count == 0)
            {
                if (re != null)
                {
                    Dictionary<string, object> Vars =
                        new Dictionary<string, object>();
                    re.GetVariables(Vars);
                    SaveReportFromTemplateFolder("F_", ReportName,
                        ListItems, Vars);
                }
                else
                    SaveReportFromTemplateFolder("F_", ReportName,
                        ListItems, _FilterForm != null ?
                        _FilterForm.FilterList : null);
            }
            comboBoxEdit1.Properties.Items.AddRange(ListItems);

            string CurrBrowseLayoutId = string.Empty;
            bool Tmp;
            DocDefault.GetDefaultLayout(_ReportName,
                out CurrBrowseLayoutId, out CurrPrintLayoutId, out Tmp);

            if (re != null)
                ((IShowView)re).ShowView(Parameters);
            else if (_ShowViewForm != null)
                _ShowViewForm.ShowView(Parameters);

            comboBoxEdit1.SelectedItem = CurrBrowseLayoutId;
            if (comboBoxEdit1.SelectedIndex < 0 && comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;
            else
                comboBoxEdit1_SelectedIndexChanged(null, null);
            Show();
            printControl1.Focus();
        }

        public void ShowForm3(string FreeFilter, 
            object TransStartDate, object TransEndDate, 
            object[] Parameters)
        {
            if (re != null)
                ((IShowView)re).ShowView(Parameters);
            else if (_ShowViewForm != null)
                _ShowViewForm.ShowView(Parameters);
            BringToFront();
            comboBoxEdit1_SelectedIndexChanged(null, null);
            printControl1.Focus();
        }
    }
}