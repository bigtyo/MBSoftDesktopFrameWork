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
using System.IO;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SentraSolutionFramework;
using SentraUtility.Expression;
using SentraSecurity;
using DevExpress.Utils;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmGridReportTC : frmReport, IBrowseForm
    {
        private MemoryStream DefaultLayout;
        private bool UseDefault;
        private List<string> DefaultSelectedPrintLayoutId = new List<string>();
        private bool DefaultUsePrintPreview;

        TableDef td;
        
        private string OldQuery;
        private string DateFormat = "dd MMM yyyy hh:mm:ss";

        public event SelectPrintLayout onSelectPrintLayout;

        private Form PrevForm;

        public void ShowForm(EntityForm EntityForm, string ReportName)
        {
            _EntityForm = EntityForm;
            _ReportName = ReportName;

            #region Check Security
            if (EntityForm.ModuleName.Length > 0 && 
                BaseSecurity.CurrentLogin.CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(EntityForm.ModuleName);
                if (!ma.GetVariable<bool>(SecurityVarName.ReportSave, false))
                {
                    barButtonItem3.Enabled = false;
                    barButtonItem4.Enabled = false;
                    barButtonItem5.Enabled = false;
                    barButtonItem6.Enabled = false;
                    barButtonItem10.Enabled = false;
                    barButtonItem12.Enabled = false;
                }
                if (!ma.GetVariable<bool>(SecurityVarName.ReportDesignPrint, false))
                {
                    barButtonItem9.Enabled = false;
                    barButtonItem11.Enabled = false;
                }
                if (!ma.GetVariable<bool>(SecurityVarName.ReportPrint, false))
                    barButtonItem7.Enabled = false;
                if (!ma.GetVariable<bool>(SecurityVarName.ReportLayoutSave, false))
                {
                    comboBoxEdit1.Properties.Buttons[1].Enabled = false;
                    comboBoxEdit1.Properties.Buttons[2].Enabled = false;
                    comboBoxEdit1.Properties.Buttons[3].Enabled = false;
                }
            }
            #endregion

            if (_Evaluator == null)
                _Evaluator = BaseFactory.CreateInstance<Evaluator>();
            comboBoxEdit1.Properties.Items.Add("(Layout Default)");

            #region Set DateEdit Visibility
            if (EntityForm.EntityType != null)
            {

                td = MetaData.GetTableDef(EntityForm.EntityType);
                EntityForm.DataPersistance.ValidateTableDef(td);
                if (EntityForm.fldTransactionDate != null)
                {
                    dateEdit1.DateTime = DateTime.Today;
                    dateEdit2.DateTime = DateTime.Today;
                }
                else
                {
                    label3.Visible = false;
                    label4.Visible = false;
                    checkEdit1.Visible = false;
                    dateEdit1.Visible = false;
                    dateEdit2.Visible = false;
                }
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                checkEdit1.Visible = false;
                dateEdit1.Visible = false;
                dateEdit2.Visible = false;
                barButtonItem1.Visibility = BarItemVisibility.Never;
                //barButtonItem2.Visibility = BarItemVisibility.Never;
            }
            #endregion

            if (EntityForm.EntityType == null)
                Text = _ReportName.IndexOf("Laporan",
                    StringComparison.OrdinalIgnoreCase) < 0 ?
                    "Laporan " + _ReportName : _ReportName;
            else if (_ReportName.Length > 0)
                Text = _ReportName.IndexOf("Daftar",
                    StringComparison.OrdinalIgnoreCase) < 0 ?
                    "Daftar " + _ReportName : _ReportName;
            else
                Text = "Daftar " + BaseUtility.SplitName(EntityForm.EntityType.Name);

            comboBoxEdit1.Properties.Items.AddRange(
                DocBrowseLayout.GetListLayout(_ReportName));

            if (EntityForm.FilterFormType == null)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            else
            {
                _FilterForm = BaseFactory.CreateInstance(
                    EntityForm.FilterFormType) as IFilterForm;
                if (_FilterForm == null)
                {
                    XtraMessageBox.Show("Form Filter harus implement Interface IFilterForm !",
                        "Error Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                if (_FilterForm as ReportEntity != null)
                {
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                    barButtonItem2.Visibility = BarItemVisibility.Never;
                }
                else
                {
                    XtraForm Frm = _FilterForm as XtraForm;
                    if (Frm != null)
                    {
                        Frm.KeyPreview = true;
                        Frm.KeyDown += new KeyEventHandler(frmGridReport_KeyDown);
                        InitFilterForm(Frm, xtraScrollableControl1,
                            splitContainerControl1);
                    }
                    else
                        QueryReportEntity(xtraScrollableControl1,
                            splitContainerControl1);
                    ReportForm rpf = _FilterForm as ReportForm;
                    if (rpf != null && !rpf.HandleGridSelected() ||
                        rpf == null && rpf as IGridSelected == null)
                        barButtonItem2.Visibility = BarItemVisibility.Never;
                }
            }

            _ShowViewForm = _FilterForm as IShowView;

            string CurrBrowseLayoutId;
            string CurrPrintLayoutId;

            DocDefault.GetDefaultLayout(_ReportName,
                out CurrBrowseLayoutId, out CurrPrintLayoutId, out DefaultUsePrintPreview);
            DefaultSelectedPrintLayoutId.AddRange(CurrPrintLayoutId.Split('|'));

            barButtonItem8_ItemClick(null, null);
            DefaultLayout = new MemoryStream();
            gridView1.SaveLayoutToStream(DefaultLayout);

            comboBoxEdit1.SelectedItem = CurrBrowseLayoutId;
            if (comboBoxEdit1.SelectedIndex < 0)
                comboBoxEdit1.SelectedIndex = 0;

            IInitGrid InitGrid = _FilterForm as IInitGrid;
            if (InitGrid != null) InitGrid.InitGrid(gridView1);

            gridView1.BestFitMaxRowCount = BaseWinFramework
                .WinForm.AutoFormat.GridBestFitMaxRowCount;
            gridView1.BestFitColumns();

            if (BaseWinFramework.MdiParent == null && ActiveForm != null &&
                !ActiveForm.Modal)
            {
                PrevForm = ActiveForm;
                PrevForm.Hide();
                WindowState = PrevForm.WindowState;
            }
            Show();
            textEdit1.Focus();
        }

        public frmGridReportTC()
        {
            InitializeComponent();
        }

        // Desain Laporan
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new WaitCursor())
            {
                if (rptl == null) rptl = new ReportLayout();
                if (rptl.CurrentForm == null || 
                    rptl.CurrentForm.IsDisposed)
                {
                    //Build Sort
                    string strSort = string.Empty;
                    foreach (GridColumnSortInfo col in gridView1.SortInfo)
                        if (col.SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
                            strSort = string.Concat(strSort, ",", col.Column.FieldName);
                        else
                            strSort = string.Concat(strSort, ",", col.Column.FieldName, " DESC");
                    if (strSort.Length > 0) strSort = strSort.Substring(1);

                    DataTable dt = gridControl1.DataSource as DataTable;

                    if (re != null) ((IBaseEntity)re).BeforePrint(_Evaluator);
                    if (dt != null)
                        rptl.ShowForm2(_ReportName,
                            new ReportDataTable(new DataView(dt,
                            gridView1.RowFilter, strSort, DataViewRowState.CurrentRows)),
                            _Evaluator);
                    else
                        rptl.ShowForm2(_ReportName,
                            new _ReportEntity(gridControl1.DataSource), _Evaluator);
                }
                else
                    rptl.BringToFront();
            }
        }

        #region Ekspor Laporan
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Grid2XLS(gridView1, string.Empty, _ReportName);
        }
        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Grid2Pdf(gridView1, string.Empty, _ReportName);
        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Grid2HTML(gridView1, string.Empty, _ReportName);
        }
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Grid2Text(gridView1, string.Empty, _ReportName);
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Grid2XML(gridView1, string.Empty, _ReportName);
        }
        #endregion

        List<ReportPreview> ListfrmPreview = new List<ReportPreview>();
        xReport Rpt;

        //Cetak
        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    using (new WaitCursor())
                    {
                        List<string> ListPrintLayout = DocPrintBrowseLayout.GetListLayout("L_" + _ReportName);

                        if (ListPrintLayout.Count == 0)
                        {
                            if (re != null)
                            {
                                Dictionary<string, object> Vars = new Dictionary<string, object>();
                                re.GetVariables(Vars);
                                SaveReportFromTemplateFolder("L_",
                                    _ReportName, ListPrintLayout, Vars);
                            }
                            else
                                SaveReportFromTemplateFolder("L_",
                                    _ReportName, ListPrintLayout,
                                    _FilterForm != null ?
                                    _FilterForm.FilterList : null);
                            if (ListPrintLayout.Count == 0)
                            {
                                XtraMessageBox.Show("Layout Cetak Laporan tidak ditemukan !",
                                    "Error Cetak Laporan",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        List<string> ListSelectedLayout = new List<string>();
                        if (onSelectPrintLayout != null)
                        {
                            onSelectPrintLayout(ListSelectedLayout);
                            UseDefault = false;
                        }
                        else
                        {
                            UseDefault = true;
                            foreach (string Layout in DefaultSelectedPrintLayoutId)
                                ListSelectedLayout.Add(Layout);
                        }

                        if (!frmCetak.ShowForm(ListPrintLayout, ListSelectedLayout,
                            "Cetak Dokumen", ref DefaultUsePrintPreview))
                            return;

                        int i = 0;
                        if (UseDefault) DefaultSelectedPrintLayoutId.Clear();
                        foreach (string Layout in ListSelectedLayout)
                        {
                            if (!ListPrintLayout.Contains(Layout)) continue;

                            MemoryStream LayoutData;
                            DocPrintBrowseLayout.GetLayoutData("L_" +
                                _ReportName, Layout, out LayoutData);
                            if (LayoutData == null) continue;

                            if (UseDefault)
                                DefaultSelectedPrintLayoutId.Add(Layout);
                            if (DefaultUsePrintPreview)
                            {
                                ReportPreview rp;
                                if (i < ListfrmPreview.Count)
                                {
                                    rp = ListfrmPreview[i];
                                }
                                else
                                {
                                    rp = new ReportPreview();
                                    ListfrmPreview.Add(rp);
                                }

                                Rpt = new xReport(_Evaluator);
                                Rpt.LoadLayout(LayoutData);

                                //Build Sort
                                string strSort = string.Empty;
                                foreach (GridColumnSortInfo col in
                                    gridView1.SortInfo)
                                    if (col.SortOrder == DevExpress.Data
                                        .ColumnSortOrder.Ascending)
                                        strSort = string.Concat(strSort, ",",
                                            col.Column.FieldName);
                                    else
                                        strSort = string.Concat(strSort, ",",
                                            col.Column.FieldName, " DESC");
                                if (strSort.Length > 0) strSort = strSort
                                    .Substring(1);

                                Rpt.DataSource = new DataView((DataTable)
                                    gridControl1.DataSource,
                                    gridView1.RowFilter, strSort,
                                    DataViewRowState.CurrentRows);
                                Rpt.ReportEntity = re;
                                rp.ShowForm(_ReportName, Rpt);
                                i++;
                            }
                            else
                            {
                                if (Rpt == null) Rpt = new xReport(_Evaluator);
                                Rpt.LoadLayout(LayoutData);
                                Rpt.DataSource = gridControl1.DataSource;

                                Rpt.CreateDocument();
                                Rpt.Print();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Cetak Laporan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Baru
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            _EntityForm.ShowNew();
        }

        //Lihat
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            if (td != null)
            {
                string Condition = string.Empty;
                FieldParam[] Params = new FieldParam[td.KeyFields.Count];
                int i = 0;
                foreach (FieldDef fld in td.KeyFields.Values)
                {
                    Condition = string.Concat(Condition, " AND ",
                        fld.FieldName, "=@", fld.FieldName);
                    Params[i++] = new FieldParam(fld,
                        gridView1.GetFocusedRowCellValue(
                    fld.FieldName));
                }

                _EntityForm.ShowView(Condition.Remove(0, 5), Params);
            }
            else
            {
                IGridSelected gs = _FilterForm as IGridSelected;
                if (gs != null)
                    gs.GridSelected(((DataView)gridView1.DataSource)
                        .Table.Rows[gridView1.FocusedRowHandle]);
            }
        }

        string OldCols = string.Empty;

        //Refresh
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            RefreshReport();
        }

        protected override void RefreshReport()
        {
            using (new WaitCursor(false))
            {
                Control LastFocus = ActiveControl;
                gridView1.Focus();
                if (LastFocus != null) LastFocus.Focus();

                BaseWinFramework.WinForm.AutoLockEntity.LockForm(re, this);

                string SqlQuery = string.Empty;
                string SqlOrder = string.Empty;
                bool WhereExist = false;
                string[] ListColHidden = null;

                if (dateEdit1.Visible && dateEdit1.Enabled)
                {
                    _Evaluator.Variables.Add("TglAwal", dateEdit1.DateTime);
                    _Evaluator.Variables.Add("TglAkhir", dateEdit2.DateTime);
                }

                if (_FilterForm == null)
                {
                    if (td != null)
                        SqlQuery = td.GetSqlSelect(_EntityForm.DataPersistance,
                            true, false);
                }

                List<FieldParam> ListParam = null;

                try
                {
                    Dictionary<string, object> QueryFormFilter;

                    if (re != null)
                    {
                        QueryFormFilter = new Dictionary<string, object>();

                        _DataSource = re.GetDataSource();
                        if (_DataSource == null)
                        {
                            ListParam = new List<FieldParam>();
                            re.GetDataSource(out SqlQuery, out SqlOrder, ListParam);
                            if (SqlQuery.Length == 0)
                                SqlQuery = td.GetSqlSelect(_EntityForm.DataPersistance, true, false);
                            else if (SqlQuery.IndexOf("SELECT ",
                                StringComparison.OrdinalIgnoreCase) < 0)
                            {
                                SqlQuery = string.Concat(td.GetSqlSelect(_EntityForm.DataPersistance, true, false),
                                    " WHERE ", SqlQuery);
                                WhereExist = true;
                            }
                            else
                                SqlQuery = string.Concat("SELECT * FROM (", SqlQuery,
                                    ") x");
                        }
                        re.GetVariables(QueryFormFilter);
                    }
                    else if (_FilterForm != null)
                        QueryFormFilter = _FilterForm.FilterList;
                    else
                        QueryFormFilter = null;

                    if (re != null && re.GetColumnHidden().Length > 0)
                        ListColHidden = re.GetColumnHidden().Split(',');

                    if (QueryFormFilter != null)
                    {
                        _DataSource = null;
                        foreach (KeyValuePair<string, object> Filter in
                            QueryFormFilter)
                        {
                            Type tp = Filter.Value.GetType();

                            if (Filter.Key != "DataSource")
                            {
                                if (Filter.Key == "DataSourceOrder" ||
                                    Filter.Key == "DataSourceParams") continue;
                                if (Filter.Key == "ColumnHidden")
                                    ListColHidden = ((string)Filter.Value).Split(',');
                                else if (tp == typeof(string))
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
                                else if (tp.IsClass)
                                    _Evaluator.ObjValues.Add(Filter.Key, Filter.Value);
                            }
                            else if (tp == typeof(string))
                            {
                                SqlQuery = ((string)Filter.Value).Trim();
                                if (SqlQuery.Length == 0)
                                    SqlQuery = td.GetSqlSelect(_EntityForm.DataPersistance, true, false);
                                else if (SqlQuery.IndexOf("SELECT ",
                                    StringComparison.OrdinalIgnoreCase) < 0)
                                {
                                    SqlQuery = string.Concat(td.GetSqlSelect(_EntityForm.DataPersistance, true, false),
                                        " WHERE ", SqlQuery);
                                    WhereExist = true;
                                }
                                else
                                    SqlQuery = string.Concat("SELECT * FROM (", SqlQuery,
                                        ") x");
                                object objOrder;
                                if (QueryFormFilter.TryGetValue("DataSourceOrder", out objOrder))
                                    SqlOrder = (string)objOrder;
                            }
                            else
                            {
                                _DataSource = Filter.Value;
                                try
                                {
                                    td = MetaData.GetTableDef((Type)_DataSource.GetType().GetGenericArguments()[0]);
                                }
                                catch
                                {
                                    td = null;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Filter",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_EntityForm.BrowseSql.Length > 0)
                {
                    SqlQuery = _EntityForm.BrowseSql;
                    WhereExist = _EntityForm.BrowseCondition.Length > 0;
                    if (WhereExist)
                        SqlQuery = string.Concat("SELECT * FROM (",
                            _EntityForm.BrowseSql, ") AS X WHERE ",
                            _EntityForm.BrowseCondition);
                    else
                        SqlQuery = string.Concat("SELECT * FROM (",
                            _EntityForm.BrowseSql, ") AS X");
                    SqlOrder = _EntityForm.BrowseOrder;
                }

                #region Bangun Sql Query utk mengisi DataSource
                if (td != null && _EntityForm.fldTransactionDate != null 
                    && checkEdit1.Checked)
                {
                    if (WhereExist)
                        SqlQuery = SqlQuery + " AND (";
                    else
                    {
                        SqlQuery = SqlQuery + " WHERE (";
                        WhereExist = true;
                    }
                    SqlQuery = string.Concat(SqlQuery,
                        _EntityForm.fldTransactionDate.FieldName, ">=",
                        _EntityForm.DataPersistance.FormatSqlValue(
                        dateEdit1.DateTime.Date, DataType.Date), " AND ",
                        _EntityForm.fldTransactionDate.FieldName, "<",
                        _EntityForm.DataPersistance.FormatSqlValue(
                        dateEdit2.DateTime.Date.AddDays(1), DataType.Date), ")");
                }
                #endregion

                if (textEdit1.Text.Length > 0)
                {
                    SqlQuery = string.Concat("SELECT * FROM (", SqlQuery,
                        ") X WHERE (", textEdit1.Text, ")");
                    WhereExist = true;
                }

                string DataFilter = _EntityForm.DataFilter.Length == 0 ?
                    string.Empty : string.Concat("(",
                    _EntityForm.DataFilter, ")");

                if (DataFilter.Length > 0)
                {
                    if (WhereExist)
                        SqlQuery = string.Concat(SqlQuery,
                            " AND ", DataFilter);
                    else
                    {
                        SqlQuery = string.Concat(SqlQuery,
                            " WHERE ", DataFilter);
                        WhereExist = true;
                    }
                }

                // Cek Error
                BaseEntity be = (BaseEntity)re;
                if (be != null && be.IsErrorExist())
                {
                    ReportForm rf = _FilterForm as ReportForm;
                    if (rf != null)
                        rf.UpdateErrorBinding();
                    XtraMessageBox.Show(be.GetErrorString(), "Error Baca Filter",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    be.ClearError();
                    return;
                }

                try
                {
                    gridControl1.BeginUpdate();
                    if (_DataSource == null)
                    {
                        #region Bila _DataSource == null
                        if (SqlQuery.Length == 0) return;

                        DataTable dt;

                        if (td != null)
                        {
                            #region Grid memiliki TableDef, ambil data dari Cache
                            AutoUpdateDataTable audt = gridControl1.DataSource as AutoUpdateDataTable;
                            if (audt != null)
                            {
                                if (OldQuery == SqlQuery)
                                {
                                    audt.Refresh();
                                    return;
                                }
                                audt.OnListRefresh -= new OnListRefresh(audt_OnListRefresh);
                                audt.Close();
                            }

                            audt = _EntityForm.DataPersistance
                                .OpenDataTable(td.ClassType,
                                SqlQuery, SqlOrder, true);

                            dt = audt;
                            barStaticItem2.Caption = audt
                                .LastQuery.ToString(DateFormat);
                            audt.OnListRefresh +=
                                new OnListRefresh(audt_OnListRefresh);
                            OldQuery = SqlQuery;
                            #endregion
                        }
                        else if (re != null)
                        {
                            #region Grid memliki ReportEntity
                            if (SqlOrder.Length > 0)
                                SqlQuery = string.Concat(SqlQuery,
                                    " ORDER BY ", SqlOrder);
                            dt = _EntityForm.DataPersistance
                                .OpenDataTable(SqlQuery, ListParam);
                            barStaticItem2.Caption = DateTime.Now
                                .ToString(DateFormat);
                            #endregion
                        }
                        else
                        {
                            #region Grid memiliki SqlQuery
                            if (SqlOrder.Length > 0)
                                SqlQuery = string.Concat(SqlQuery,
                                    " ORDER BY ", SqlOrder);

                            object p;
                            _FilterForm.FilterList.TryGetValue("DataSourceParams", out p);
                            if (p != null)
                            {
                                List<FieldParam> lp = p as List<FieldParam>;
                                if (lp != null)
                                    dt = _EntityForm.DataPersistance
                                        .OpenDataTable(SqlQuery, lp);
                                else
                                    dt = _EntityForm.DataPersistance
                                        .OpenDataTable(SqlQuery, p as FieldParam[]);
                            }
                            else
                                dt = _EntityForm.DataPersistance
                                    .OpenDataTable(SqlQuery);
                            barStaticItem2.Caption = DateTime.Now
                                .ToString(DateFormat);
                            #endregion
                        }

                        dt.TableName = _ReportName;

                        #region Cek apakah kolom grid berubah
                        string TmpCols = string.Empty;
                        foreach (DataColumn col in dt.Columns)
                            TmpCols += col.ColumnName;

                        if (TmpCols == OldCols)
                        {
                            gridControl1.DataSource = dt;
                            return;
                        }
                        OldCols = TmpCols;
                        gridView1.Columns.Clear();
                        gridControl1.DataSource = dt;
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region _DataSource ada isinya
                        barStaticItem2.Caption = DateTime.Now.ToString(DateFormat);
                        gridControl1.DataSource = _DataSource;
                        #endregion
                    }

                    #region set kolom visibe dan autoformat
                    if (td != null)
                    {
                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(td.ClassType,
                            gridView1, true);
                        if (_EntityForm.BrowseColumns.Length == 0)
                        {
                            foreach (FieldDef fld in td.KeyFields.Values)
                                if (fld.IsBrowseHidden && fld.IsPublic)
                                    gridView1.Columns[fld.FieldName].Visible = false;
                            foreach (FieldDef fld in td.NonKeyFields.Values)
                                if (fld.IsBrowseHidden && fld.IsPublic)
                                {
                                    GridColumn gc = gridView1.Columns.ColumnByFieldName(fld.FieldName);
                                    if (gc != null)
                                        gc.Visible = false;
                                }
                        }
                        else
                        {
                            string[] ColVisible = _EntityForm.BrowseColumns.Split(',');
                            for(int i=0; i<ColVisible.Length; i++)
                                ColVisible[i]=ColVisible[i].Trim();
                            int VisCtr = 0;
                            foreach (GridColumn gc in gridView1.Columns)
                            {
                                bool IsVisible = false;
                                foreach (string strCol in ColVisible)
                                    if (strCol == gc.FieldName)
                                    {
                                        IsVisible = true;
                                        break;
                                    }
                                gc.Visible = IsVisible;
                                if (IsVisible)
                                    gc.VisibleIndex = VisCtr++;
                            }
                        }
                    }
                    else
                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(gridView1, true);

                    if (ListColHidden != null && comboBoxEdit1.SelectedIndex <= 0)
                        foreach (string ColHidden in ListColHidden)
                        {
                            GridColumn gc = gridView1.Columns.ColumnByFieldName(ColHidden.Trim());
                            if (gc != null)
                                gc.Visible = false;
                        }
                    #endregion

                    if (_EntityForm.BrowseFormat.Count > 0)
                        foreach (KeyValuePair<string, string> kvp in _EntityForm.BrowseFormat)
                        {
                            GridColumn gc = gridView1.Columns.ColumnByFieldName(kvp.Key);
                            if (gc != null)
                            {
                                gc.DisplayFormat.FormatType = FormatType.Custom;
                                gc.DisplayFormat.FormatString = kvp.Value;
                            }
                        }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Membaca Filter",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    gridView1.BestFitMaxRowCount = BaseWinFramework
                        .WinForm.AutoFormat.GridBestFitMaxRowCount;
                    gridView1.BestFitColumns();
                    gridControl1.EndUpdate();
                }
            }
        }

        void audt_OnListRefresh()
        {
            barStaticItem2.Caption = ((AutoUpdateDataTable)gridControl1.DataSource).LastQuery.ToString(DateFormat);
        }

        private void dateEdit1_DateTimeChanged(object sender, EventArgs e)
        {
            if (gridControl1.DataSource != null)
                barButtonItem8_ItemClick(null, null);
        }

        private void frmGridReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            switch (e.KeyCode)
            {
                case Keys.R:    //Refresh
                    barButtonItem8_ItemClick(null, null);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.F2:   //Baru
                    if (barButtonItem1.Visibility == BarItemVisibility.Always)
                    {
                        barButtonItem1_ItemClick(null, null);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.F3:   //Lihat
                    if (barButtonItem2.Visibility == BarItemVisibility.Always)
                    {
                        barButtonItem2_ItemClick(null, null);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.P:    //Cetak
                    if (barButtonItem7.Enabled)
                    {
                        barButtonItem7_ItemClick(null, null);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.S:    //Simpan Layout
                    if (comboBoxEdit1.Properties.Buttons[1].Enabled)
                    {
                        LayoutAction(1);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.B:    //Layout Baru
                    if (comboBoxEdit1.Properties.Buttons[2].Enabled)
                    {
                        LayoutAction(2);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.H:    //Hapus Layout
                    if (comboBoxEdit1.Properties.Buttons[3].Enabled)
                    {
                        LayoutAction(3);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.C:    //Copy
                    if (barButtonItem10.Enabled)
                    {
                        barButtonItem10_ItemClick(null, null);
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        //Pindah layout, belum tentu refresh data
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    if (comboBoxEdit1.SelectedIndex == 0)
                    {
                        if (DefaultLayout != null)
                        {
                            DefaultLayout.Seek(0, SeekOrigin.Begin);
                            gridView1.RestoreLayoutFromStream(DefaultLayout);
                        }
                    }
                    else
                    {
                        MemoryStream ms;
                        string QueryFilter;
                        Dictionary<string, object> QueryFormFilter = 
                            new Dictionary<string, object>();
                        DocBrowseLayout.GetLayoutData(_ReportName,
                            comboBoxEdit1.Text, out ms, out QueryFilter,
                            QueryFormFilter);
                        bool NeedRefresh = false;
                        if (QueryFilter.Length > 0)
                        {
                            textEdit1.Text = QueryFilter;
                            NeedRefresh = true;
                        }
                        if (re != null)
                        {
                            re.SetPersistanceField(QueryFormFilter);
                            NeedRefresh = true;
                        }
                        else if (_FilterForm != null && QueryFormFilter != null)
                        {
                            _FilterForm.FilterList = QueryFormFilter;
                            NeedRefresh = true;
                        }
                        if (NeedRefresh) barButtonItem8_ItemClick(null, null);
                        if (ms != null)
                        {
                            gridView1.RestoreLayoutFromStream(ms);
                            ms.Close();
                        }
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

        private void comboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            LayoutAction(e.Button.Index);
        }

        private void LayoutAction(int ActionId)
        {
            using (new WaitCursor())
            {
                MemoryStream mst;
                Dictionary<string, object> DictFormFilter = null;
                int i = comboBoxEdit1.SelectedIndex;

                if (i == 0 && (ActionId == 1 || ActionId == 3))
                {
                    XtraMessageBox.Show("(Layout Default) tidak dapat disimpan/ dihapus !",
                        "Error Simpan Layout",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                switch (ActionId)
                {
                    case 1: //Simpan
                        if (XtraMessageBox.Show("Update Layout Laporan ?",
                            "Konfirmasi Update Layout Laporan", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) ==
                            System.Windows.Forms.DialogResult.No) return;
                        try
                        {
                            mst = new MemoryStream();
                            gridView1.SaveLayoutToStream(mst);
                            if (re != null)
                            {
                                DictFormFilter = new Dictionary<string, object>();
                                re.GetVariables(DictFormFilter);
                            }
                            else if (_FilterForm != null)
                                DictFormFilter = _FilterForm.FilterList;
                            DocBrowseLayout.SaveUpdateLayout(_ReportName,
                                comboBoxEdit1.Text, mst, textEdit1.Text, DictFormFilter);
                            mst.Close();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Update Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2: //Tambah
                        frmSaveLayout frm = new frmSaveLayout();
                        if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel) return;

                        try
                        {
                            mst = new MemoryStream();
                            gridView1.SaveLayoutToStream(mst);
                            if (re != null)
                            {
                                DictFormFilter = new Dictionary<string, object>();
                                re.GetVariables(DictFormFilter);
                            }
                            else if (_FilterForm != null)
                                DictFormFilter = _FilterForm.FilterList;
                            DocBrowseLayout.SaveNewLayout(_ReportName,
                                frm.strText, mst, textEdit1.Text, DictFormFilter);
                            mst.Close();
                            comboBoxEdit1.Properties.Items.Add(frm.strText);
                            comboBoxEdit1.SelectedIndex = comboBoxEdit1.Properties.Items.Count - 1;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Menambah Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 3: //Hapus
                        if (XtraMessageBox.Show("Hapus Layout Laporan ?",
                            "Konfirmasi Hapus Layout Laporan", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

                        try
                        {
                            DocBrowseLayout.DeleteLayout(_ReportName, comboBoxEdit1.Text);
                            int idx = comboBoxEdit1.SelectedIndex;
                            comboBoxEdit1.Properties.Items.RemoveAt(idx);
                            int Cnt = comboBoxEdit1.Properties.Items.Count;
                            if (idx < Cnt)
                                comboBoxEdit1.SelectedIndex = idx;
                            else
                                comboBoxEdit1.SelectedIndex = Cnt - 1;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Hapus Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
        }

        //Copy
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.DoEvents();
            gridView1.CopyToClipboard();
        }

        GridHitInfo hi;
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (hi.InRowCell)
                barButtonItem2_ItemClick(null, null);
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                hi = gridView1.CalcHitInfo(e.Location);
                if (hi.HitTest == GridHitTest.ColumnButton && hi.Column == null)
                {
                    gridView1.SelectAll();
                    return;
                }
            }
        }

        private void gridView1_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            //if (_EntityForm.ReportSecurity.AllowExport && e.HitInfo.InRowCell)
            if (e.HitInfo.InRowCell)
            {
                e.Menu.Items.Add(new DXMenuItem("&Copy (Ctrl-C)", 
                    CopyMenu_Click, Properties.Resources.copy1));
            }
        }

        private void CopyMenu_Click(object sender, EventArgs e)
        {
            barButtonItem10_ItemClick(null, null);
        }

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (barCheckItem1.Checked)
                gridView1.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            else
                gridView1.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded;
        }

        // SetDefault
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSetDefault.ShowForm(_ReportName, ref DefaultUsePrintPreview);
        }

        private void frmGridReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (td != null)
                {
                    AutoUpdateDataTable audt = (AutoUpdateDataTable)gridControl1.DataSource;
                    audt.OnListRefresh -= new OnListRefresh(audt_OnListRefresh);
                    audt.Close();
                }
                if (UseDefault)
                    DocDefault.UpdateSelectedPrintBrowseLayout(_ReportName,
                        string.Join("|", DefaultSelectedPrintLayoutId
                        .ToArray()));
            }
            catch { }
            if (PrevForm != null && !PrevForm.IsDisposed)
                PrevForm.Show();
        }

        public void ShowForm2(EntityForm EntityForm,
            string ReportName, string FreeFilter,
            object TransStartDate, object TransEndDate,
            object[] Parameters)
        {
            _EntityForm = EntityForm;
            _ReportName = ReportName;

            #region Check Security
            if (EntityForm.ModuleName.Length > 0 &&
                BaseSecurity.CurrentLogin.CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(EntityForm.ModuleName);
                if (!ma.GetVariable<bool>(SecurityVarName.ReportSave, false))
                {
                    barButtonItem3.Enabled = false;
                    barButtonItem4.Enabled = false;
                    barButtonItem5.Enabled = false;
                    barButtonItem6.Enabled = false;
                    barButtonItem10.Enabled = false;
                    barButtonItem12.Enabled = false;
                }
                if (!ma.GetVariable<bool>(SecurityVarName.ReportDesignPrint, false))
                {
                    barButtonItem9.Enabled = false;
                    barButtonItem11.Enabled = false;
                }
                if (!ma.GetVariable<bool>(SecurityVarName.ReportPrint, false))
                    barButtonItem7.Enabled = false;
                if (!ma.GetVariable<bool>(SecurityVarName.ReportLayoutSave, false))
                {
                    comboBoxEdit1.Properties.Buttons[1].Enabled = false;
                    comboBoxEdit1.Properties.Buttons[2].Enabled = false;
                    comboBoxEdit1.Properties.Buttons[3].Enabled = false;
                }
            }
            #endregion

            _Evaluator = BaseFactory.CreateInstance<Evaluator>();
            comboBoxEdit1.Properties.Items.Add("(Layout Default)");

            #region Set DateEdit Visibility
            bool DateVisible = false;
            if (EntityForm.EntityType != null)
            {

                td = MetaData.GetTableDef(EntityForm.EntityType);
                EntityForm.DataPersistance.ValidateTableDef(td);
                if (EntityForm.fldTransactionDate != null)
                {
                    dateEdit1.DateTime = DateTime.Today;
                    dateEdit2.DateTime = DateTime.Today;
                    DateVisible = true;
                }
                else
                {
                    label3.Visible = false;
                    label4.Visible = false;
                    checkEdit1.Visible = false;
                    dateEdit1.Visible = false;
                    dateEdit2.Visible = false;
                }
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                checkEdit1.Visible = false;
                dateEdit1.Visible = false;
                dateEdit2.Visible = false;
                barButtonItem1.Visibility = BarItemVisibility.Never;
                //barButtonItem2.Visibility = BarItemVisibility.Never;
            }
            #endregion

            if (EntityForm.EntityType == null)
                Text = _ReportName.IndexOf("Laporan",
                    StringComparison.OrdinalIgnoreCase) < 0 ?
                    "Laporan " + _ReportName : _ReportName;
            else if (_ReportName.Length > 0)
                Text = _ReportName.IndexOf("Daftar",
                    StringComparison.OrdinalIgnoreCase) < 0 ?
                    "Daftar " + _ReportName : _ReportName;
            else
                Text = "Daftar " + BaseUtility.SplitName(EntityForm.EntityType.Name);

            comboBoxEdit1.Properties.Items.AddRange(
                DocBrowseLayout.GetListLayout(_ReportName));

            _FilterForm = BaseFactory.CreateInstance(
                EntityForm.FilterFormType) as IFilterForm;
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

            if (EntityForm.FilterFormType == null)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            else if (_FilterForm as ReportEntity != null)
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                barButtonItem2.Visibility = BarItemVisibility.Never;
            }
            else
            {
                XtraForm Frm = _FilterForm as XtraForm;
                if (Frm != null)
                {
                    Frm.KeyPreview = true;
                    Frm.KeyDown += new KeyEventHandler(frmGridReport_KeyDown);
                    InitFilterForm(Frm, xtraScrollableControl1,
                        splitContainerControl1);
                }
                else
                    QueryReportEntity(xtraScrollableControl1,
                        splitContainerControl1);

                ReportForm rpf = _FilterForm as ReportForm;
                if (rpf != null && !rpf.HandleGridSelected() ||
                    rpf == null && rpf as IGridSelected == null)
                    barButtonItem2.Visibility = BarItemVisibility.Never;
            }

            if (DocPrintBrowseLayout
                .GetListLayout("L_" + ReportName).Count == 0)
            {
                if (re != null)
                {
                    Dictionary<string, object> Vars =
                        new Dictionary<string, object>();
                    re.GetVariables(Vars);
                    SaveReportFromTemplateFolder("L_",
                        ReportName, null, Vars);
                }
                else
                    SaveReportFromTemplateFolder("L_",
                        ReportName, null, _FilterForm != null ?
                        _FilterForm.FilterList : null);
            }

            string CurrBrowseLayoutId;
            string CurrPrintLayoutId;

            DocDefault.GetDefaultLayout(_ReportName,
                out CurrBrowseLayoutId, out CurrPrintLayoutId, out DefaultUsePrintPreview);
            DefaultSelectedPrintLayoutId.AddRange(CurrPrintLayoutId.Split('|'));

            textEdit1.Text = FreeFilter;
            if (DateVisible)
            {
                if (TransStartDate != null)
                    dateEdit1.DateTime = (DateTime)TransStartDate;
                if (TransEndDate != null)
                    dateEdit2.DateTime = (DateTime)TransEndDate;
            }
            if (re != null)
                ((IShowView)re).ShowView(Parameters);
            else if (_ShowViewForm != null)
                _ShowViewForm.ShowView(Parameters);

            barButtonItem8_ItemClick(null, null);
            DefaultLayout = new MemoryStream();
            gridView1.SaveLayoutToStream(DefaultLayout);

            comboBoxEdit1.SelectedItem = CurrBrowseLayoutId;
            if (comboBoxEdit1.SelectedIndex < 0)
                comboBoxEdit1.SelectedIndex = 0;

            IInitGrid InitGrid = _FilterForm as IInitGrid;
            if (InitGrid != null) InitGrid.InitGrid(gridView1);

            gridView1.BestFitMaxRowCount = BaseWinFramework
                .WinForm.AutoFormat.GridBestFitMaxRowCount;
            gridView1.BestFitColumns();

            if (BaseWinFramework.MdiParent == null && ActiveForm != null &&
                !ActiveForm.Modal)
            {
                PrevForm = ActiveForm;
                PrevForm.Hide();
                WindowState = PrevForm.WindowState;
            }

            Show();
            textEdit1.Focus();
        }

        public void ShowForm3(string FreeFilter,
            object TransStartDate, object TransEndDate,
            object[] Parameters)
        {
            textEdit1.Text = FreeFilter;
            if (dateEdit1.Visible)
            {
                if (TransStartDate != null)
                    dateEdit1.DateTime = (DateTime)TransStartDate;
                if (TransEndDate != null)
                    dateEdit2.DateTime = (DateTime)TransEndDate;
            }
            if (re != null)
                ((IShowView)re).ShowView(Parameters);
            else if (_ShowViewForm != null)
                _ShowViewForm.ShowView(Parameters);
            BringToFront();
            gridView1.BeginDataUpdate();
            try
            {
                barButtonItem8_ItemClick(null, null);
                comboBoxEdit1_SelectedIndexChanged(null, null);
                textEdit1.Focus();
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
        }

        private void dateEdit2_DateTimeChanged(object sender, EventArgs e)
        {
            if (gridControl1.DataSource != null)
                barButtonItem8_ItemClick(null, null);
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            bool IsChecked = checkEdit1.Checked;

            dateEdit1.Enabled = IsChecked;
            dateEdit2.Enabled = IsChecked;
            label3.Enabled = IsChecked;
            label4.Enabled = IsChecked;

            if (gridControl1.DataSource != null)
                barButtonItem8_ItemClick(null, null);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            checkEdit1.Checked = false;
            if (gridControl1.DataSource != null)
                barButtonItem8_ItemClick(null, null);
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Show Hide Filter Bebas
            if (label2.Visible)
            {
                label2.Visible = false;
                textEdit1.Visible = false;
                int Pos = splitContainerControl1.Top + splitContainerControl1.Height;
                splitContainerControl1.Top = 65;
                splitContainerControl1.Height = Pos - 65;
                barButtonItem13.Hint = "Tampilkan Filter Bebas";
            }
            else
            {
                label2.Visible = true;
                textEdit1.Visible = true;
                int Pos = splitContainerControl1.Top + splitContainerControl1.Height;
                splitContainerControl1.Top = 96;
                splitContainerControl1.Height = Pos - 96;
                barButtonItem13.Hint = "Sembunyikan Filter Bebas";
            }
        }
    }
}