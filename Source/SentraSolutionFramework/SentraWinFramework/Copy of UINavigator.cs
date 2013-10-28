using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using DevExpress.XtraEditors.Controls;
using SentraUtility;
using SentraWinFramework.Report;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraReports.UI;
using System.IO;
using SentraUtility.Expression;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using SentraSecurity;
using System.Threading;
using SentraSolutionFramework;
using System.Collections;
using DevExpress.XtraEditors.DXErrorProvider;
using System.Reflection;
using DevExpress.XtraGrid.Repository;
using System.Diagnostics;
using SentraWinFramework.Properties;
using DevExpress.Utils.Menu;

namespace SentraWinFramework
{
    public partial class UINavigator : XtraUserControl, 
        INavigatableControl, IUINavigator
    {
        [DebuggerNonUserCode]
        private class GridColl
        {
            [DebuggerNonUserCode]
            private class xCol
            {
                public RepositoryItemLookUpEdit rle;
                public FieldDef fldDisplay;
                public GetHandler GetSrcHandler;
                public xCol(RepositoryItemLookUpEdit rle, TableDef td,
                    Type ParentType)
                {
                    this.rle = rle;
                    fldDisplay = td[rle.DisplayMember];
                    GetSrcHandler = DynamicMethodCompiler.CreateGetHandler(
                        ParentType.GetMember(
                        ((BindingSource)rle.DataSource).DataMember)[0]);
                }
            }
            private Dictionary<GridColumn, xCol> ListCols;
            //private RepositoryItemTextEdit rte;
            private TableDef td;

            public GridView gv;
            public IEntityCollection eColl;

            private bool EventHandled = false;

            public void SetLookupEditDisplayMember(
                RepositoryItemLookUpEdit le, string DisplayMember, bool AlwaysSet)
            {
                foreach (xCol xc in ListCols.Values)
                    if (object.ReferenceEquals(xc.rle, le))
                    {
                        if (AlwaysSet || xc.fldDisplay ==  null && DisplayMember.Length > 0)
                            xc.fldDisplay = td[DisplayMember];
                        break;
                    }
            }

            public GridColl(GridView gv, IEntityCollection eColl, BaseEntity Entity)
            {
                ListCols = new Dictionary<GridColumn, xCol>();
                this.gv = gv;
                this.eColl = eColl;
                gv.RowUpdated += new RowObjectEventHandler(gv_RowUpdated);
                Type ChildType = null;
                if (eColl == null)
                {
                    TableDef tdEntity = MetaData.GetTableDef(Entity.GetType());
                    string ChildName = ((BindingSource)gv.GridControl.DataSource).DataMember;

                    foreach (EntityCollDef ecd in tdEntity.ChildEntities)
                        if (ecd.FieldName == ChildName)
                        {
                            ChildType = ecd.GetChildType();
                            break;
                        }
                }
                else
                    ChildType = eColl.GetChildType();

                td = MetaData.GetTableDef(ChildType);
                Type tp = Entity.GetType();
                foreach (GridColumn gcol in gv.Columns)
                {
                    RepositoryItemLookUpEdit rle = gcol.ColumnEdit 
                        as RepositoryItemLookUpEdit;
                    if (rle != null)
                    {
                        ListCols.Add(gcol, new xCol(rle, td, tp));
                        //if (rte == null)
                        //{
                        //    rte = new RepositoryItemTextEdit();
                        //    rte.ReadOnly = true;
                        //    gv.GridControl.RepositoryItems.Add(rte);
                        //}
                        //gcol.ColumnEdit = rte;
                        if (!EventHandled)
                        {
                            gv.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gv_CustomColumnDisplayText);
                            gv.CustomRowCellEditForEditing += new CustomRowCellEditEventHandler(gv_CustomRowCellEditForEditing);
                            EventHandled = true;
                        }
                    }
                }
            }

            void gv_RowUpdated(object sender, RowObjectEventArgs e)
            {
                if (e.RowHandle < 0) return;
                eColl.ClearError(e.RowHandle);
            }

            void gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
            {
                xCol xc;
                if (ListCols.TryGetValue(e.Column, out xc))
                {
                    xc.rle.DataSource = xc.GetSrcHandler(eColl.GetParent());
                    e.RepositoryItem = xc.rle;
                }
            }

            void gv_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
            {
                xCol xc;
                if (ListCols.TryGetValue(e.Column, out xc))
                {
                    //if (xc.fldDisplay == null)
                    //    throw new ApplicationException(string.Concat("Field [",
                    //        e.Column.FieldName, 
                    //        "] dipergunakan sbg DisplayText tetapi tidak ada di tabel [", 
                    //        eColl.ChildName, "]"));
                    if (xc.fldDisplay != null)
                        e.DisplayText = e.ListSourceRowIndex < 0 ? string.Empty :
                            xc.fldDisplay.GetValue(
                            eColl.GetValue(e.ListSourceRowIndex)).ToString();
                    else
                        e.DisplayText = e.Value.ToString();
                }
            }
        }

        bool IBaseUINavigator.TryGetFocusedRowValue<TType>(string TableName,
            string FieldName, out TType Value)
        {
            if (DictGrid.Count == 0)
            {
                Value = default(TType);
                return false;
            }
            bool RetVal = false;
            try
            {
                if (typeof(TType).IsSubclassOf(typeof(BaseEntity)))
                {
                    object TmpVal = BaseFactory.CreateInstance<TType>();
                    //TableDef td = MetaData.GetTableDef(typeof(TType));
                    Type tp = typeof(TType);
                    GridView gv = ListGrid[DictGrid[TableName]].gv;
                    PropertyInfo pi;
                    FieldInfo fi;
                    bool AllNull = true;
                    foreach (GridColumn gc in gv.Columns)
                    {
                        object TmpRowVal = gv.GetFocusedRowCellValue(gc);
                        if (TmpRowVal == null) continue;
                        AllNull = false;

                        fi = tp.GetField("_" + gc.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (fi != null)
                        {
                            fi.SetValue(TmpVal, TmpRowVal);
                            continue;
                        }

                        pi = tp.GetProperty(gc.FieldName);
                        if (pi != null)
                        {
                            if (pi.CanWrite)
                                pi.SetValue(TmpVal, TmpRowVal, null);
                        }
                        else
                        {
                            fi = tp.GetField(gc.FieldName);
                            if (fi != null)
                                fi.SetValue(TmpVal, TmpRowVal);
                        }
                    }
                    //foreach (FieldDef fld in td.KeyFields.Values)
                    //{
                    //    object CellVal = gv.GetFocusedRowCellValue(fld.FieldName);
                    //    if (CellVal != null)
                    //        fld.SetValue((BaseEntity)TmpVal, CellVal);
                    //}
                    //foreach (FieldDef fld in td.NonKeyFields.Values)
                    //{
                    //    object CellVal = gv.GetFocusedRowCellValue(fld.FieldName);
                    //    if (CellVal != null)
                    //        fld.SetValue((BaseEntity)TmpVal, CellVal);
                    //}
                    Value = (TType)TmpVal;
                    RetVal = !AllNull;
                }
                else
                {
                    object TmpVal = ListGrid[DictGrid[TableName]].
                        gv.GetFocusedRowCellValue(FieldName);
                    RetVal = TmpVal != null;
                    Value = (TType)TmpVal;
                }
            }
            catch
            {
                Value = default(TType);
            }
            return RetVal;
        }

        private bool UseDefault;
        private List<string> DefaultSelectedPrintLayoutId = new List<string>();
        private bool DefaultUsePrintPreview;

        private EntityForm _EntityForm;
        public EntityForm EntityForm
        {
            set
            {
                _EntityForm = value;
                UpdateFilter();
            }
        }

        private Evaluator _Evaluator;
        public Evaluator Evaluator
        {
            get { return _Evaluator; }
            set { _Evaluator = value; }
        }

        public ParentEntity Entity { get { return _Navigator.Entity; } }

        public event BeforeAction BeforeSaveNew;
        public event BeforeAction BeforeSaveUpdate;
        public event BeforeAction BeforeSaveDelete;

        public event SelectPrintLayout onSelectPrintLayout;

        private EntityNavigator _Navigator;
        [Browsable(false)]
        public EntityNavigator Navigator { get { return _Navigator; } }
        private List<GridColl> ListGrid = new List<GridColl>();
        private Dictionary<string, int> DictGrid = new Dictionary<string, int>();

        [DebuggerNonUserCode]
        private class xLE
        {
            public LookUpEdit Le;
            public TextEdit Te;
            public string DisplayMember;

            public xLE(LookUpEdit Le, string DisplayMember)
            {
                if (Le.Properties.Buttons.Count == 3)
                {
                    EditorButton eb = Le.Properties.Buttons[0];
                    if (eb.Kind == ButtonPredefines.Combo)
                        eb.ToolTip = "F4 - Lihat Data";
                    eb = Le.Properties.Buttons[1];
                    if (eb.Kind == ButtonPredefines.Plus)
                    {
                        eb.ToolTip = "Ctrl+T - Tambah Data";
                        eb.Shortcut = new KeyShortcut(Keys.Control | Keys.T);
                    }
                    eb = Le.Properties.Buttons[2];
                    if (eb.Kind == ButtonPredefines.Redo)
                    {
                        eb.ToolTip = "Ctrl+R - Refresh Data";
                        eb.Shortcut = new KeyShortcut(Keys.Control | Keys.R);
                    }
                }
                this.Le = Le;
                this.DisplayMember = DisplayMember;
            }
        }

        private List<xLE> ListLE = new List<xLE>();
        private DXErrorProvider ep;

        private string BrowseLayoutId;
        private string RptName;

        public ParentEntity GetOriginal() { return _Navigator.Original; }
        public ParentEntity GetEntity()
        {
            if (_Navigator == null || _Navigator.Entity == null)
                throw new ApplicationException("DataSource pada UINavigator belum diisi !");
            return _Navigator.Entity;
        }

        public void FindData(string Condition, 
            params FieldParam[] Parameters)
        {
            if (_Navigator.FindCriteria(Condition, Parameters))
                SetEditMode();
        }

        public void FindKey(string Key)
        {
            if (_Navigator.FindKey(Key))
                SetEditMode();
        }

        public void SetGridAsNavigator(GridView gv)
        {
            gv.OptionsView.ShowDetailButtons = false;
            gv.FocusedRowChanged += new FocusedRowChangedEventHandler(gv_FocusedRowChanged);
        }

        void gv_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0 &&
                ((GridView)sender).GridControl.Focused)
            {
                ParentEntity pe = (ParentEntity)((BindingSource)
                    ((GridView)sender).DataSource).Current;
                string RetVal = string.Empty;
                DataPersistance Dp = pe.Dp;
                FieldParam[] Params = new FieldParam[MetaData.GetTableDef(pe)
                    .KeyFields.Count];
                int i = 0;
                foreach (FieldDef fld in MetaData.GetTableDef(pe)
                    .KeyFields.Values)
                {
                    RetVal = string.Concat(RetVal, " AND ",
                        fld.FieldName, "=@", fld.FieldName);
                    Params[i++] = new FieldParam(fld, pe[fld.FieldName]);
                }
                FindData(RetVal.Substring(5), Params);
            }
        }

        #region Navigator Events
        [Category("Setting")]
        public event LockForm onLockForm;
        [Category("Setting")]
        public event EntityAction onEntityAction;

        [Category("Setting")]
        public event NewAction onNewMode;
        [Category("Setting")]
        public event DataMoving onDataMoving;
        [Category("Setting")]
        public event FormModeChanged onFormModeChanged;
        #endregion

        private bool _AskSaveChangedForm = true;
        [Category("Setting"), DefaultValue(true)]
        public bool AskSaveChangedForm
        {
            get { return _AskSaveChangedForm; }
            set { _AskSaveChangedForm = value; }
        }

        private bool _MoveDataAfterDelete = true;
        [Category("Setting"), DefaultValue(true)]
        public bool MoveDataAfterDelete
        {
            get { return _MoveDataAfterDelete; }
            set { _MoveDataAfterDelete = value; }
        }

        private bool _EnableAutoFormat = true;
        [Category("Setting"), DefaultValue(true)]
        public bool EnableAutoFormat
        {
            get { return _EnableAutoFormat; }
            set { _EnableAutoFormat = value; }
        }

        private string _DataFilter = string.Empty;
        [Category("Setting")]
        public string DataFilter
        {
            get { return _DataFilter; }
            set
            {
                _DataFilter = value;
                UpdateFilter();
            }
        }

        private string _ExcludeFields = string.Empty;
        [Category("Setting")]
        public string ExcludeFields
        {
            get { return _ExcludeFields; }
            set
            {
                _ExcludeFields = value;
                if (!DesignMode && _Navigator != null) UpdateCombo();
            }
        }

        #region Allow Security
        private bool _AllowAdd = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowAdd
        {
            get { return _AllowAdd; }
            set { _AllowAdd = value; }
        }

        private bool _AllowEdit = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowEdit
        {
            get { return _AllowEdit; }
            set { _AllowEdit = value; }
        }

        private bool _AllowDelete = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowDelete
        {
            get { return _AllowDelete; }
            set { _AllowDelete = value; }
        }

        private bool _AllowBrowse = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowBrowse
        {
            get { return _AllowBrowse; }
            set
            {
                _AllowBrowse = value;
                simpleButton7.Enabled = value;
            }
        }

        private bool _AllowDesignPrint = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowDesignPrint
        {
            get { return _AllowDesignPrint; }
            set
            {
                _AllowDesignPrint = value;
                if (!_AllowDesignPrint)
                    //simpleButton6.ContextMenuStrip = null;
                    dropDownButton1.DropDownControl = null;
                else
                    //simpleButton6.ContextMenuStrip = contextMenuStrip1;
                    dropDownButton1.DropDownControl = BuildPrintmenu();
            }
        }

        private DXPopupMenu BuildPrintmenu()
        {
            DXPopupMenu mn = new DXPopupMenu();
            mn.Items.Add(new DXMenuItem("Edit Layout Cetak Dokumen", 
                EditPrintLayout, Resources.edit));
            mn.Items.Add(new DXMenuItem("Ubah Default Cetak...",
                UbahDefaultCetak, Resources.monitor_preferences));
            return mn;
        }

        ReportLayout rptl;
        private void EditPrintLayout(object sender, EventArgs ev)
        {
            using (new WaitCursor())
            {
                if (rptl == null) rptl = new ReportLayout();
                if (rptl.CurrentForm == null || rptl.CurrentForm.IsDisposed)
                    rptl.ShowForm(_Navigator.Original, _Evaluator);
                else
                    rptl.BringToFront();
            }
        }

        private void UbahDefaultCetak(object sender, EventArgs ev)
        {
            frmDefaultCetakDokumen frm = new frmDefaultCetakDokumen();
            if (frm.ShowForm(RptName))
            {
                DefaultUsePrintPreview = frm.DefaultUsePrintPreview;
                checkEdit2.Checked = frm.DefaultCetakKetikaSimpan;
            }
        }

        private bool _AllowPrint = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowPrint
        {
            get { return _AllowPrint; }
            set
            {
                _AllowPrint = value;
                //simpleButton6.Enabled = value;
                dropDownButton1.Enabled = value;
            }
        }

        private bool _AllowBrowseExport = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowBrowseExport
        {
            get { return _AllowBrowseExport; }
            set { _AllowBrowseExport = value; }
        }

        private bool _AllowBrowseDesignPrint = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowBrowseDesignPrint
        {
            get { return _AllowBrowseDesignPrint; }
            set { _AllowBrowseDesignPrint = value; }
        }

        private bool _AllowBrowsePrint = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowBrowsePrint
        {
            get { return _AllowBrowsePrint; }
            set { _AllowBrowsePrint = value; }
        }

        private bool _AllowBrowseSaveLayout = true;
        [Category("Security"), DefaultValue(true)]
        public bool AllowBrowseSaveLayout
        {
            get { return _AllowBrowseSaveLayout; }
            set { _AllowBrowseSaveLayout = value; }
        }
        #endregion

        private void UpdateCombo()
        {
            TableDef td = _Navigator.TableDef;

            string[] ArrExf = _ExcludeFields.Split(',');
            List<string> ListExf = new List<string>();
            foreach (string exf in ArrExf)
                ListExf.Add(exf.Trim());

            comboBoxEdit1.Properties.Items.Clear();
            int i;
            foreach (FieldDef fld in td.KeyFields.Values)
                if (fld.IsPublic && !fld.IsBrowseHidden)
                {
                    i = ListExf.IndexOf(fld.FieldName);
                    if (i < 0)
                        comboBoxEdit1.Properties.Items.Add(
                            BaseUtility.SplitName(fld.FieldName));
                    else
                        ListExf.RemoveAt(i);
                }
            foreach (FieldDef fld in td.NonKeyFields.Values)
                if (fld.IsPublic && !fld.IsBrowseHidden)
                {
                    i = ListExf.IndexOf(fld.FieldName);
                    if (i < 0)
                        switch (fld.DataType)
                        {
                            case DataType.Image:
                            case DataType.Binary:
                                break;
                            default:
                                comboBoxEdit1.Properties.Items.Add(
                                    BaseUtility.SplitName(fld.FieldName));
                                break;
                        }
                    else
                        ListExf.RemoveAt(i);
                }
            comboBoxEdit1.SelectedIndex = 0;
        }

        private void ParentForm_KeyDown(object sender, KeyEventArgs e)
        {
            bool Success = false;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (e.Alt)
                    {
                        e.SuppressKeyPress = true;
                        if (e.Control)
                            Success = _Navigator.MoveLast();
                        else
                            Success = _Navigator.MoveNext();
                        if (!Success)
                            XtraMessageBox.Show(
                                "Data Tidak Ditemukan !", "Error Navigasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            SetEditMode();
                    }
                    break;
                case Keys.Left:
                    if (e.Alt)
                    {
                        e.SuppressKeyPress = true;
                        if (e.Control)
                            Success = _Navigator.MoveFirst();
                        else
                            Success = _Navigator.MovePrevious();
                        if (!Success)
                            XtraMessageBox.Show(
                                "Data Tidak Ditemukan !", "Error Navigasi",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            SetEditMode();
                    }
                    break;
                case Keys.F2: // new form
                    if (e.Control && simpleButton1.Enabled)
                    {
                        e.SuppressKeyPress = true;
                        SetNew();
                    }
                    break;
                case Keys.F8:
                    if (e.Control && simpleButton3.Enabled)
                    {
                        simpleButton3_Click(null, null);
                        e.SuppressKeyPress = true;
                    }
                    break;
                case Keys.S:
                    if (e.Control && simpleButton4.Enabled)
                        using (new WaitCursor(false))
                        {
                            e.SuppressKeyPress = true;
                            Focus();
                            SaveData();
                        }
                    break;
                case Keys.Oemcomma:
                    if (e.Control)
                    {
                        try
                        {
                            e.SuppressKeyPress = true;
                            SetCurrentFieldValue();
                            if (_Navigator.FindFirst())
                                SetEditMode();
                        }
                        catch (Exception)
                        {
                            XtraMessageBox.Show("Data yang dicari tidak ketemu !",
                                "Error Pencarian Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
                case Keys.OemPeriod:
                    if (e.Control)
                    {
                        try
                        {
                            e.SuppressKeyPress = true;
                            SetCurrentFieldValue();
                            if (_Navigator.FindLast())
                                SetEditMode();
                        }
                        catch (Exception)
                        {
                            XtraMessageBox.Show("Data yang dicari tidak ketemu !",
                                "Error Pencarian Data", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
                default:
                    return;
            }
        }

        public UINavigator()
        {
            InitializeComponent();

            controlNavigator1.NavigatableControl = this;
            labelControl2.SuperTip = checkEdit2.SuperTip;
        }

        protected override void InitLayout()
        {
            if (DesignMode) Dock = DockStyle.Bottom;
            base.InitLayout();
        }

        private BindingSource _BindingSource;
        [Category("Data")]
        public BindingSource BindingSource
        {
            get { return _BindingSource; }
            set
            {
                _BindingSource = value;
                if (DesignMode || _BindingSource == null) return;
                if (_BindingSource.DataSource != null &&
                    _BindingSource.DataSource.GetType().Name != "RuntimeType")
                    _BindingSource_DataSourceChanged(null, null);
                _BindingSource.DataSourceChanged += 
                    new EventHandler(_BindingSource_DataSourceChanged);
            }
        }

        private void UpdateFilter()
        {
            if (_Navigator == null) return;
            string Filter = string.Empty;

            if (_EntityForm != null && _EntityForm.DataFilter.Length > 0)
                Filter = string.Concat(Filter, " AND (", 
                    _EntityForm.DataFilter, ")");
            if (_DataFilter.Length == 0)
            {
                if (Filter.Length > 0)
                    _Navigator.Filter = Filter.Substring(5);
            }
            else
                _Navigator.Filter = string.Concat("(", _DataFilter, ")",
                    Filter);
        }

        private void DoFormChanged()
        {
            FormMode fm = ((ParentEntity)_BindingSource.DataSource).FormMode;
            //Check ListLE
            foreach(xLE xl in ListLE)
                if (xl.Le.Properties.ReadOnly)
                {
                    xl.Le.Visible = false;
                    if (xl.Te == null)
                        CreateTextEdit(xl);
                    xl.Te.Visible = true;
                }
            BaseWinFramework.WinForm.AutoLockEntity.LockForm(_Navigator.Entity,
                MetaData.GetTableDef(_Navigator.Entity), ParentForm);
            if (onLockForm != null) onLockForm(_Navigator.Entity.FormMode == FormMode.FormView ||
                _Navigator.Entity.FormMode == FormMode.FormError);
        }

        public event DataSourceChanged DataSourceChanged;

        private bool AutoFormMode;
        void IBaseUINavigator.SetAutoFormMode()
        {
            UpdateCombo();
            UpdateFilter();
            if (_Navigator.Entity.AutoFormMode)
            {
                LastFormModeIsView = false;

                bool DefaultNull = true;
                foreach (FieldDef fld in MetaData.GetTableDef(
                    _Navigator.Entity.GetType())
                    .KeyFields.Values)
                    if (fld.DataTypeAtr.Default != null)
                    {
                        DefaultNull = false;
                        break;
                    }
                if (DefaultNull)
                    simpleButton1.Enabled = false;
                else
                    simpleButton1.Enabled = _Navigator.Entity.AllowAddNew;
                checkButton1.Checked = false;
                checkButton1.Enabled = false;
                AutoFormMode = true;
            }
        }

        void _BindingSource_DataSourceChanged(object sender, EventArgs e)
        {
            if (DesignMode) return;
            if (_BindingSource == null ||
                _BindingSource.DataSource == null)
                throw new ApplicationException("DataSource pada UINavigator belum diisi/ salah isi !");

            if (BindingSource.DataSource.GetType().Name == "RuntimeType")
                return;

            ParentEntity pe = BindingSource.DataSource as ParentEntity;

            if (pe == null)
                throw new ApplicationException("DataSource pada UINavigator belum diisi/ salah isi !");

            _Navigator = new EntityNavigator(this, pe, false);

            //_Navigator.onAfterLoad += new AfterLoad(_Navigator_onAfterLoad);

            pe.OnFormChanged += new OnAction(DoFormChanged);

            frmDocument doc = FindForm() as frmDocument;
            if (doc != null)
            {
                RptName = BaseWinFramework.GetModuleName(
                    doc.EntityCtrl.GetType());
            }
            else
                RptName = BaseWinFramework.GetModuleName(FindForm().GetType());
            if (RptName.Length == 0)
                RptName = BaseUtility.SplitName(pe.GetType().Name);

            _Navigator.ModuleName = RptName;

            _Navigator.onDataMoving += new DataMoving(_Navigator_onDataMoving);
            ParentForm.Load += new EventHandler(ParentForm_Load);
            if (!(ParentForm is frmDocument))
            {
                if (_EnableAutoFormat)
                    BaseWinFramework.WinForm.AutoFormat.AutoFormatForm(ParentForm, false);

                UpdateCombo();
                UpdateFilter();

                checkButton1.Enabled = _AllowEdit && _Navigator.Entity.AllowEdit 
                    && _Navigator.Entity.IsTransDateValid();
            }
            simpleButton7.Enabled = _AllowBrowse;
            if (!_AllowDesignPrint)
                dropDownButton1.DropDownControl = null;
            //dropDownButton1.ContextMenuStrip = null;
            //simpleButton6.ContextMenuStrip = null;`
            else
            {
                DXPopupMenu pm = BuildPrintmenu();
                pm.Items[0].Visible = _AllowDesignPrint;
                dropDownButton1.DropDownControl = pm;
                //dropDownButton1.ContextMenuStrip = contextMenuStrip1;
                //simpleButton6.ContextMenuStrip = contextMenuStrip1;
            }
            ParentForm.KeyPreview = true;
            ParentForm.KeyDown += new KeyEventHandler(ParentForm_KeyDown);
            ParentForm.Shown += new EventHandler(ParentForm_Shown);
            ParentForm.FormClosed += new FormClosedEventHandler(ParentForm_FormClosed);
            ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            bool DefaultPrintOnSave;
            string PrintLayoutId;
            DocDefault.GetDefaultLayout(RptName, out BrowseLayoutId,
                out PrintLayoutId, out DefaultPrintOnSave,
                out DefaultUsePrintPreview);

            DefaultSelectedPrintLayoutId.AddRange(PrintLayoutId.Split('|'));

            checkEdit2.Checked = DefaultPrintOnSave;

            UpdateFilter();

            pe.OnFormModeChanged += new OnAction(pe_OnFormModeChanged);
            pe.OnEntityAction += new EntityAction(pe_OnEntityAction);

            ((IRuleInitUI)pe).InitUI();
            if (DataSourceChanged != null) DataSourceChanged(pe);
        }

        private void CreateTextEdit(xLE xle)
        {
            LookUpEdit Le = xle.Le;
            TextEdit Te = new TextEdit();

            Te.Parent = Le.Parent;
            Te.Left = Le.Left;
            Te.Top = Le.Top;
            Te.Width = Le.Width;
            Te.Height = Le.Height;
            Te.TabIndex = Le.TabIndex;
            Te.Properties.ReadOnly = true;
            Te.Tag = "SkipLock";

            string DisplayMember = xle.DisplayMember;
            foreach(Binding bnd in Le.DataBindings)
                if (bnd.PropertyName == "EditValue")
                {
                    Te.DataBindings.Add("EditValue", ((BindingSource)bnd.DataSource)
                        .DataSource, DisplayMember);
                    break;
                }

            xle.Te = Te;
        }

        void pe_OnEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            switch (ActionMode)
            {
                case enEntityActionMode.BeforeLoad:
                    if (!BindingSource.IsBindingSuspended)
                    {
                        BindingSource.SuspendBinding();
                        if (onEntityAction != null) onEntityAction(ActionEntity, ActionMode);
                    }
                    break;
                case enEntityActionMode.AfterLoadFound:
                    if (BindingSource.IsBindingSuspended)
                    {
                        if (_LastFormModeIsView)
                        {
                            foreach (xLE xle in ListLE)
                            {
                                if (xle.Te == null)
                                    CreateTextEdit(xle);
                            }
                        }
                        if (onEntityAction != null) onEntityAction(ActionEntity, ActionMode);
                        BindingSource.ResumeBinding();
                    }
                    break;
                case enEntityActionMode.AfterLoadNotFound:
                    if (BindingSource.IsBindingSuspended)
                    {
                        if (onEntityAction != null) onEntityAction(ActionEntity, ActionMode);
                        BindingSource.ResumeBinding();
                    }
                    break;
                default:
                    if (onEntityAction != null) onEntityAction(ActionEntity, ActionMode);
                    break;
            }
        }

        void pe_OnFormModeChanged()
        {
            switch (_Navigator.Entity.FormMode)
            {
                case FormMode.FormAddNew:
                    SetNew();
                    break;
                case FormMode.FormEdit:
                case FormMode.FormView:
                    SetEditMode();
                    break;
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_AskSaveChangedForm) return;

            string strError = string.Empty;
            switch (_Navigator.Entity.FormMode)
            {
                case FormMode.FormAddNew:
                    if (_Navigator.Entity.IsDefaultEntity()) return;
                    if (BaseUtility.IsDebugMode)
                        strError = _Navigator.Entity
                            .GetStrIsNotDefaultEntity();
                    break;
                case FormMode.FormEdit:
                    if (_Navigator.Entity.Equals(
                        _Navigator.Entity.GetOriginal())) return;
                    if (BaseUtility.IsDebugMode)
                        strError = _Navigator.Entity
                            .GetStrNotEquals(_Navigator.Entity.GetOriginal());
                    break;
                default:
                    return;
            }
            DialogResult dr;

            if (BaseUtility.IsDebugMode)
                dr = XtraMessageBox.Show(string.Concat("Data '",
                    ParentForm.Text, "' Telah berubah (", strError, 
                    "), Simpan Perubahan ?"), "Konfirmasi Simpan Perubahan",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            else
                dr = XtraMessageBox.Show(string.Concat("Data '",
                    ParentForm.Text, "' Telah berubah, Simpan Perubahan ?"),
                    "Konfirmasi Simpan Perubahan",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (dr)
            {
                case DialogResult.Yes:
                    if (!SaveData()) e.Cancel = true;
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        void ParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (UseDefault)
                    DocDefault.UpdateSelectedLayout(RptName,
                        string.Join("|", DefaultSelectedPrintLayoutId
                        .ToArray()));
            }
            catch { }
            ((IRuleInitUI)_Navigator.Entity).EndUI();
        }

        void ParentForm_Load(object sender, EventArgs e)
        {
            FindGridAndLookup(ParentForm.Controls);

            ep = new DXErrorProvider(ParentForm);
            ep.DataSource = _BindingSource;
        }

        // Cari Data
        private void buttonEdit1_Properties_ButtonClick(object sender, 
            ButtonPressedEventArgs e)
        {
            try
            {
                SetCurrentFieldValue();

                bool IsFind;
                if (e.Button.Index == 0)
                    IsFind = _Navigator.FindFirst();
                else
                    IsFind = _Navigator.FindLast();
                if (IsFind) SetEditMode();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Data yang dicari tidak ketemu !",
                    "Error Pencarian Data", MessageBoxButtons.OK, 
                    MessageBoxIcon.Exclamation);
            }
        }

        #region INavigatableControl Members
        void INavigatableControl.AddNavigator(INavigatorOwner owner) { }
        void INavigatableControl.DoAction(NavigatorButtonType type)
        {
            using (new WaitCursor())
            {
                try
                {
                    bool Success;
                    switch (type)
                    {
                        case NavigatorButtonType.Next:
                            Success = _Navigator.MoveNext();
                            break;
                        case NavigatorButtonType.Prev:
                            Success = _Navigator.MovePrevious();
                            break;
                        case NavigatorButtonType.First:
                            Success = _Navigator.MoveFirst();
                            break;
                        default:
                            Success = _Navigator.MoveLast();
                            break;
                    }
                    if (!Success)
                        //SetEditMode();
                   // else
                        throw new ApplicationException("Data tidak ditemukan !");
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(
                        ex.Message, "Navigator Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    SetNew();
                }
            }
        }
        bool INavigatableControl.IsActionEnabled(NavigatorButtonType type) { return true; }
        int INavigatableControl.Position { get { return 0; } }
        int INavigatableControl.RecordCount { get { return 0; } }
        void INavigatableControl.RemoveNavigator(INavigatorOwner owner) { }
        #endregion

        // Baru
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (_AskSaveChangedForm)
            {
                string strError = string.Empty;
                switch (_Navigator.Entity.FormMode)
                {
                    case FormMode.FormAddNew:
                        if (_Navigator.Entity.IsDefaultEntity()) goto Bersihkan;
                        if (BaseUtility.IsDebugMode)
                            strError = _Navigator.Entity
                                .GetStrIsNotDefaultEntity();
                        break;
                    case FormMode.FormEdit:
                        if (_Navigator.Entity.Equals(
                            _Navigator.Entity.GetOriginal())) goto Bersihkan;
                        if (BaseUtility.IsDebugMode)
                            strError = _Navigator.Entity
                                .GetStrNotEquals(_Navigator.Entity.GetOriginal());
                        break;
                    default:
                        goto Bersihkan;
                }
                DialogResult dr;

                if (BaseUtility.IsDebugMode)
                    dr = XtraMessageBox.Show(string.Concat("Data '",
                        ParentForm.Text, "' Telah berubah (", strError,
                        "), Simpan Perubahan ?"), "Konfirmasi Simpan Perubahan",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                else
                    dr = XtraMessageBox.Show(string.Concat("Data '",
                        ParentForm.Text, "' Telah berubah, Simpan Perubahan ?"),
                        "Konfirmasi Simpan Perubahan",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (dr)
                {
                    case DialogResult.Yes:
                        if (!SaveData()) return;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
        Bersihkan:
            SetNew();
        }

        public void SetDefaultValue()
        {
            _Navigator.SetDefaultValue(string.Empty);
        }
        public void SetDefaultValue(string ExcludeFields)
        {
            _Navigator.SetDefaultValue(ExcludeFields);
        }

        private bool _LastFormModeIsView = true;
        public bool LastFormModeIsView
        {
            get { return _LastFormModeIsView; }
            set { _LastFormModeIsView = value; }
        }

        private bool OnProcessSetNew;
        public void SetNew()
        {
            if (OnProcessSetNew) return;
            OnProcessSetNew = true;
            try
            {
                bool CallSetDefault = true;

                ParentForm.ActiveControl = this;
                ParentForm.SelectNextControl(ParentForm, true, true, true, false);

                if (onNewMode != null) onNewMode(ref CallSetDefault);
                _Navigator.SetNew(CallSetDefault);

                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;
                buttonEdit1.Text = string.Empty;

                if (checkButton1.Checked == _LastFormModeIsView)
                    checkButton1_CheckedChanged(null, null);
                else
                    checkButton1.Checked = false;

                simpleButton4.Enabled = true;
                //simpleButton6.Enabled = false;
                dropDownButton1.Enabled = false;

                if (_AllowAdd && _Navigator.Entity.AllowAddNew)
                {
                    controlNavigator1.TextStringFormat = "Data Baru";
                    //checkButton1.ToolTip = "Data Baru";

                    foreach (GridColl gc in ListGrid)
                    {
                        GridView gv = gc.gv as GridView;
                        if (gv == null) continue;
                        if (object.ReferenceEquals(gv, gv
                            .GridControl.MainView))
                        {
                            gv.CollapseAllDetails();
                            gv.MoveFirst();
                            gv.FocusedColumn = gv.GetVisibleColumn(0);
                        }
                    }
                    if (onFormModeChanged != null)
                        onFormModeChanged(FormMode.FormAddNew);
                }
                else
                {
                    _Navigator.Entity.FormMode = FormMode.FormError;

                    controlNavigator1.TextStringFormat = "Data Kosong";
                    //checkButton1.ToolTip = "Data Kosong";

                    //BaseWinFramework.WinForm.AutoLockEntity.LockForm(
                    //    _Navigator.Entity, MetaData.GetTableDef(_Navigator.Entity),
                    //    ParentForm);
                    //if (onLockForm != null) onLockForm(true);

                    foreach (GridColl gridc in ListGrid)
                    {
                        GridView gv = gridc.gv as GridView;
                        if (gv == null) continue;

                        if (object.ReferenceEquals(gv, gv.GridControl.MainView))
                            gv.CollapseAllDetails();
                        //gridc.DetachLookupEdit();
                    }
                    simpleButton4.Enabled = false;
                    if (onFormModeChanged != null)
                        onFormModeChanged(FormMode.FormError);
                }
                if (Entity.FormMode == FormMode.FormAddNew) UpdateLookup(false);
            }
            finally
            {
                OnProcessSetNew = false;
            }
        }

        private bool OnProcessSetEdit;
        private void SetEditMode()
        {
            if (OnProcessSetEdit) return;
            OnProcessSetEdit = true;
            try
            {
                checkButton1.Enabled = _AllowEdit && !AutoFormMode &&
                    _Navigator.Entity.AllowEdit && _Navigator.Entity.IsTransDateValid() 
                    && !AutoFormMode;

                if (checkButton1.Checked == _LastFormModeIsView)
                    checkButton1_CheckedChanged(null, null);
                else
                    checkButton1.Checked = _LastFormModeIsView;

                simpleButton2.Enabled = true;
                buttonEdit1.Text = GetCurrentFieldValue();
                //simpleButton6.Enabled = _AllowPrint;
                dropDownButton1.Enabled = _AllowPrint;

                simpleButton3.Enabled = _AllowDelete && _Navigator.Entity.AllowDelete
                    && _Navigator.Entity.IsTransDateValid();
                if (_LastFormModeIsView)
                {
                    controlNavigator1.TextStringFormat = "Lihat Data";
                    //if (simpleButton3.Enabled)
                    //    checkButton1.ToolTip = "Data bisa dilihat/ dihapus";
                    //else
                    //    checkButton1.ToolTip = "Data hanya bisa dilihat";

                    _Navigator.Entity.FormMode = FormMode.FormView;
                    UpdateLookup(true);
                    if (onFormModeChanged != null)
                        onFormModeChanged(FormMode.FormView);
                }
                else
                {
                    controlNavigator1.TextStringFormat = "Edit Data";
                    //if (simpleButton3.Enabled)
                    //    checkButton1.ToolTip = "Data bisa diedit/ dihapus";
                    //else
                    //    checkButton1.ToolTip = "Data bisa diedit";

                    _Navigator.Entity.FormMode = FormMode.FormEdit;
                    UpdateLookup(false);
                    if (onFormModeChanged != null)
                        onFormModeChanged(FormMode.FormEdit);
                }
            }
            finally
            {
                OnProcessSetEdit = false;
            }
        }

        // Hapus Data
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                if (BeforeSaveDelete != null)
                {
                    bool Cancel = false;
                    BeforeSaveDelete(ref Cancel);
                    if (Cancel) return;
                }

                if (XtraMessageBox.Show(string.Concat("Hapus Data ",
                    this.ParentForm.Text, " ?"),
                    "Konfirmasi Hapus Data", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No) return;

                ParentEntity pe = _Navigator.Entity;

                try
                {
                    _Navigator.Delete(true);
                    //if (onAfterDelete != null) onAfterDelete();
                    if (_MoveDataAfterDelete && !_Navigator.MoveNext())
                        SetNew();
                    else
                        ParentForm.SelectNextControl(ParentForm,
                            true, true, true, false);

                    ep.UpdateBinding();
                    RefreshGridData();

                    //if (onValidateError != null) onValidateError();
                }
                catch (Exception ex)
                {
                    ep.UpdateBinding();
                    RefreshGridData();

                    //if (onValidateError != null) onValidateError();

                    string strError = pe.GetErrorString();
                    strError = strError.Length == 0 ||
                        ex.Message == pe.GetErrorString() ?
                        ex.Message : string.Concat(
                        strError, "\n", ex.Message);
                    XtraMessageBox.Show(strError,
                        "Gagal Menghapus Dokumen !", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    pe.ClearError();
                    ParentForm.SelectNextControl(ParentForm,
                        true, true, true, false);
                }

                //try
                //{
                //    _Navigator.Delete(true);
                //    if (onAfterDelete != null) onAfterDelete();
                //    if (_MoveDataAfterDelete && !_Navigator.MoveNext())
                //        SetNew();
                //    else
                //        ParentForm.SelectNextControl(ParentForm,
                //            true, true, true, false);
                //}
                //catch (Exception ex)
                //{
                //    XtraMessageBox.Show(ex.Message, "Error Hapus Data",
                //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
        }

        private bool SaveData()
        {
            using (new WaitCursor())
            {
                if (_Navigator.Entity.FormMode == FormMode.FormAddNew)
                {
                    //if (!_AllowAdd || !_Navigator.Entity.AllowAddNew)
                    //{
                    //    XtraMessageBox.Show("Anda tidak memiliki hak membuat Dokumen Baru !",
                    //        "Error Hak Akses", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return true;
                    //}
                    if (BeforeSaveNew != null)
                    {
                        bool Cancel = false;

                        BeforeSaveNew(ref Cancel);
                        if (Cancel) return true;
                    }
                }
                else
                {
                    //if (!_AllowEdit || !_Navigator.Entity.AllowEdit)
                    //{
                    //    XtraMessageBox.Show("Anda tidak memiliki hak mengedit Dokumen !",
                    //        "Error Hak Akses", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return true;
                    //}
                    if (BeforeSaveUpdate != null)
                    {
                        bool Cancel = false;

                        BeforeSaveUpdate(ref Cancel);
                        if (Cancel) return true;
                    }
                }
                try
                {
                    ParentEntity pe = _Navigator.Entity;

                    // Clear previous child error if exist
                    // Perlu dilakukan karena XtraGrid memiliki
                    // behaviour yg berbeda dlm penanganan error.
                    TableDef td = MetaData.GetTableDef(pe.GetType());
                    foreach (EntityCollDef ecd in td.ChildEntities)
                        ecd.GetCollValue(pe).ClearError();

                    try
                    {
                        _Navigator.Save(true);

                        ep.UpdateBinding();
                        RefreshGridData();

                        //if (onValidateError != null) onValidateError();
                    }
                    catch (Exception ex)
                    {
                        ep.UpdateBinding();
                        RefreshGridData();

                        //if (onValidateError != null) onValidateError();

                        string strError = pe.GetErrorString();
                        strError = strError.Length == 0 ||
                            ex.Message == pe.GetErrorString() ?
                            ex.Message : string.Concat(
                            strError, "\n", ex.Message);
                        XtraMessageBox.Show(strError,
                            "Gagal Penyimpanan Dokumen !", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        pe.ClearError();
                        ParentForm.SelectNextControl(ParentForm,
                            true, true, true, false);
                        return false;
                    }

                    if (_AllowPrint && checkEdit2.Checked)
                    {
                        try
                        {
                            dropDownButton1_Click(null, null);
                            //simpleButton6_Click(null, null);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Cetak Dokumen", 
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    if (_Navigator.Entity.FormMode == FormMode.FormAddNew)
                    {
                        ParentForm.SelectNextControl(ParentForm,
                            true, true, true, false);

                        //if (onAfterSaveNew != null) onAfterSaveNew();
                        bool CallSetDefault = true;
                        //if (!AutoFormMode)
                        //{
                        if (onNewMode != null) onNewMode(ref CallSetDefault);
                        _Navigator.SetNew(CallSetDefault);
                        //}
                        //else
                        //{
                        //    XtraMessageBox.Show("Data Sudah Tersimpan !",
                        //        "Konfirmasi Simpan", MessageBoxButtons.OK,
                        //        MessageBoxIcon.Information);
                        //    SetEditMode();
                        //    ParentForm.SelectNextControl(ParentForm,
                        //        true, true, true, false);
                        //}
                        foreach (GridColl gc in ListGrid)
                        {
                            gc.gv.MoveFirst();
                            gc.gv.FocusedColumn = gc.gv.GetVisibleColumn(0);
                        }
                    }
                    else
                    {
                        //if (onAfterSaveEdit != null) onAfterSaveEdit();
                        XtraMessageBox.Show("Data Sudah Tersimpan !",
                            "Konfirmasi Simpan", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        SetEditMode();
                        ParentForm.SelectNextControl(ParentForm,
                            true, true, true, false);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Simpan Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
        }

        private void RefreshGridData()
        {
            foreach (GridColl gc in ListGrid)
                if (object.ReferenceEquals(gc.gv, gc.gv
                    .GridControl.MainView))
                {
                    gc.gv.CollapseAllDetails();
                    gc.gv.RefreshData();
                }
        }

        // Simpan Data
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, 
            EventArgs e)
        {
            _Navigator.OrderField = comboBoxEdit1.Text.Replace(" ", string.Empty);
            if (_Navigator.Entity.FormMode == FormMode.FormEdit)
            {
                buttonEdit1.Text = GetCurrentFieldValue();
                buttonEdit1.Focus();
            }
        }

        private string GetCurrentFieldValue()
        {
            switch (_Navigator.OrderFieldDef.DataType)
            {
                case DataType.Char:
                case DataType.VarChar:
                    Type FieldType = _Navigator.CurrentFieldValue.GetType();
                    if (FieldType.IsEnum)
                        return EnumDef.GetEnumName(FieldType, 
                            _Navigator.CurrentFieldValue);
                    else
                        return (string)_Navigator.CurrentFieldValue;
                case DataType.Boolean:
                    return ((bool)_Navigator.CurrentFieldValue) ?
                        "Ya" : "Tidak";
                case DataType.Date:
                    return ((DateTime)_Navigator.CurrentFieldValue)
                        .ToString("dd MMM yyyy");
                case DataType.DateTime:
                    return ((DateTime)_Navigator.CurrentFieldValue)
                        .ToString("dd MMM yyyy hh:mm");
                case DataType.Time:
                    return ((DateTime)_Navigator.CurrentFieldValue)
                        .ToString("hh:mm");
                case DataType.TimeStamp:
                    return ((DateTime)_Navigator.CurrentFieldValue)
                        .ToString("dd MMM yyyy hh:mm");
                case DataType.Integer:
                    return ((int)_Navigator.CurrentFieldValue)
                        .ToString("#,##0");
                case DataType.Decimal:
                    return ((decimal)_Navigator.CurrentFieldValue)
                        .ToString("#,##0.##");
                default:
                    return _Navigator.CurrentFieldValue.ToString();
            }
        }

        private void SetCurrentFieldValue()
        {
            switch (_Navigator.OrderFieldDef.DataType)
            {
                case DataType.Char:
                case DataType.VarChar:
                    _Navigator.FindValue = buttonEdit1.Text + "%";
                    break;
                case DataType.Boolean:
                    switch (buttonEdit1.Text.ToLower())
                    {
                        case "ya":
                            _Navigator.FindValue = true;
                            break;
                        case "tidak":
                            _Navigator.FindValue = false;
                            break;
                        default:
                            throw new ApplicationException(
                                "Data hanya boleh berisi Ya/ Tidak");
                    }
                    break;
                case DataType.Date:
                case DataType.DateTime:
                case DataType.Time:
                case DataType.TimeStamp:
                    DateTime result;
                    if (DateTime.TryParse(buttonEdit1.Text, out result))
                        _Navigator.FindValue = result;
                    else
                        throw new ApplicationException(
                            "Data harus berisi Tanggal");
                    break; 
                case DataType.Decimal:
                    decimal decResult;
                    if (decimal.TryParse(buttonEdit1.Text, out decResult))
                        _Navigator.FindValue = decResult;
                    else
                        throw new ApplicationException(
                            "Data harus berisi Nilai Decimal");
                    break; 
                case DataType.Integer:
                    int intResult;
                    if (int.TryParse(buttonEdit1.Text, out intResult))
                        _Navigator.FindValue = intResult;
                    else
                        throw new ApplicationException(
                            "Data harus berisi Nilai Integer");
                    break;
            }
        }

        private void buttonEdit1_Enter(object sender, EventArgs e)
        {
            buttonEdit1.SelectAll();
        }

        // Reload Data
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                if (_Navigator.Entity.FormMode == FormMode.FormAddNew) return;
                if (!_Navigator.Reload())
                    SetNew();
                checkButton1_CheckedChanged(null, null);
                ParentForm.SelectNextControl(ParentForm, true, true, true, false);
            }
        }

        private void FindGridAndLookup(ControlCollection Ctrls)
        {
            foreach (Control Ctrl in Ctrls)
            {
                GridControl gc = Ctrl as GridControl;
                if (gc != null)
                {
                    BindingSource bs = gc.DataSource as BindingSource;
                    if (bs == null) continue;
                    IEntityCollection ec = null;

                    if (bs.DataMember.Length > 0)
                    {
                        Type t = bs.DataSource.GetType();
                        PropertyInfo pi = t.GetProperty(bs.DataMember,
                            TypeCompare2.AllFieldsBindAtr);
                        if (pi != null)
                            ec = pi.GetValue(bs.DataSource, null)
                                as IEntityCollection;
                        else
                        {
                            FieldInfo fi = t.GetField(bs.DataMember,
                                TypeCompare2.AllFieldsBindAtr);
                            if (fi != null)
                                ec = fi.GetValue(bs.DataSource)
                                    as IEntityCollection;
                        }
                    }

                    if (ec == null) continue;

                    foreach (GridView gv in gc.ViewCollection)
                    {
                        if (gv.SourceView != null || gv.LevelName.Length > 0) continue;

                        foreach (GridColumn gcl in gv.Columns)
                        {
                            RepositoryItemLookUpEdit rle = gcl.ColumnEdit as RepositoryItemLookUpEdit;
                            if (rle != null)
                                SetGridLookupEditDisplayMember(gv, rle,
                                    gcl.FieldName, ec, false);
                        }
                    }
                    foreach (RepositoryItem ri in gc.RepositoryItems)
                    {
                        RepositoryItemLookUpEdit rle = ri as 
                            RepositoryItemLookUpEdit;
                        if (rle != null)
                            foreach(EditorButton b in rle.Buttons)
                                if (b.Kind == ButtonPredefines.Redo)
                                {
                                    rle.ButtonClick +=
                                        new ButtonPressedEventHandler(
                                        rle_ButtonClick);
                                    break;
                                }
                    }
                }
                else
                {
                    LookUpEdit le = Ctrl as LookUpEdit;
                    if (le != null)
                    {
                        string tag = le.Tag as string;

                        if (tag != null && tag
                            .IndexOf("SkipChangeToText",
                            StringComparison.OrdinalIgnoreCase) >= 0)
                            continue;

                        SetLookupEditDisplayMember(le, 
                            le.DataBindings["EditValue"]
                            .BindingMemberInfo.BindingField, false);

                        foreach(EditorButton b in le
                            .Properties.Buttons)
                            if (b.Kind == ButtonPredefines.Redo)
                            {
                                le.ButtonClick += 
                                    new ButtonPressedEventHandler(
                                    Le_ButtonClick);
                                break;
                            }
                    }
                    else if (Ctrl.Controls.Count > 0)
                        FindGridAndLookup(Ctrl.Controls);
                }
            }
        }

        public void SetGridLookupEditDisplayMember(GridView gv, 
            RepositoryItemLookUpEdit Le, string DisplayMember)
        {
            SetGridLookupEditDisplayMember(gv, Le, DisplayMember, 
                null, true); 
        }

        private void SetGridLookupEditDisplayMember(GridView gv,
            RepositoryItemLookUpEdit Le, string DisplayMember,
            IEntityCollection eColl, bool AlwaysSet)
        {
            GridColl FindGc = null;

            foreach (GridColl gc in ListGrid)
                if (gc.gv == gv)
                {
                    FindGc = gc;
                    break;
                }
            if (FindGc == null)
            {
                GridColl gc = new GridColl(gv, eColl, _Navigator.Entity);
                if (Le != null)
                    gc.SetLookupEditDisplayMember(Le, DisplayMember, AlwaysSet);
                ListGrid.Add(gc);
                if (eColl == null)
                    DictGrid[((BindingSource)gv.GridControl.DataSource).DataMember] = ListGrid.Count - 1;
                else
                    DictGrid[eColl.ChildName] = ListGrid.Count - 1;
            }
            else if (Le != null)
                FindGc.SetLookupEditDisplayMember(Le, DisplayMember, AlwaysSet);
            if (FindGc != null && eColl != null)
                FindGc.eColl = eColl;
        }

        public void SetLookupEditDisplayMember(LookUpEdit Le, string DisplayMember)
        {
            SetLookupEditDisplayMember(Le, DisplayMember, true);
        }

        void rle_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Redo)
                ((IAutoUpdateList)((LookUpEdit)sender).Properties.DataSource).Refresh();
        }

        void Le_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Redo)
            {
                string DataMember = ((BindingSource)((LookUpEdit)sender).
                    Properties.DataSource).DataMember;
                object px = ((BindingSource)((LookUpEdit)sender).
                    Properties.DataSource).DataSource;
                if (DataMember.Length > 0)
                {
                    PropertyInfo pi = px.GetType().GetProperty(DataMember, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (pi != null)
                    {
                        IAutoUpdateList lst = (IAutoUpdateList)pi.GetValue(px, null);
                        lst.Refresh();
                    }
                    else
                    {

                        FieldInfo fi = px.GetType().GetField(DataMember, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (fi != null)
                        {
                            IAutoUpdateList lst = (IAutoUpdateList)fi.GetValue(px);
                            lst.Refresh();
                        }
                    }
                }
                else
                {
                    IAutoUpdateList lst = px as IAutoUpdateList;
                    if (lst != null) lst.Refresh();
                }
            }
        }

        private void SetLookupEditDisplayMember(LookUpEdit Le, string DisplayMember, bool AlwaysSet)
        {
            xLE FindLe = null;
            foreach (xLE xle in ListLE)
                if (xle.Le == Le)
                {
                    FindLe = xle;
                    break;
                }
            if (FindLe == null)
            {
                ListLE.Add(new xLE(Le, DisplayMember));
                Le.LocationChanged += new EventHandler(Le_LocationChanged);
            }
            else if (AlwaysSet)
                FindLe.DisplayMember = DisplayMember;
        }

        void Le_LocationChanged(object sender, EventArgs e)
        {
            foreach (xLE le in ListLE)
                if (object.ReferenceEquals(
                    sender, le.Le))
                {
                    if (le.Te != null)
                    {
                        le.Te.Left = le.Le.Left;
                        le.Te.Top = le.Le.Top;
                    }
                    return;
                }
        }

        private void UpdateLookup(bool ShowTextEdit)
        {
            BaseWinFramework.WinForm.AutoLockEntity.LockForm(
                _Navigator.Entity, MetaData.GetTableDef(_Navigator.Entity),
                ParentForm);
            FormMode frmMode = _Navigator.Entity.FormMode;
            if (onLockForm != null) onLockForm(
                frmMode == FormMode.FormError || frmMode == FormMode.FormView);

            if (ShowTextEdit)
                foreach (xLE xle in ListLE)
                {
                    //if (xle.Te == null)
                    //    CreateTextEdit(xle);
                    //xle.Te.Visible = true;
                    xle.Le.Visible = false;
                }
            else
                foreach (xLE xle in ListLE)
                {
                    if (!xle.Le.Properties.ReadOnly)
                    {
                        //xle.Le.Visible = true;
                        if (xle.Te != null) xle.Te.Visible = false;
                    }
                    else
                    {
                        xle.Le.Visible = false;
                        //if (xle.Te == null)
                        //    CreateTextEdit(xle);
                        //xle.Te.Visible = true;
                    }
                }
        }

        void ParentForm_Shown(object sender, EventArgs e)
        {
            try
            {
                ((Control)sender).SuspendLayout();
                if (_Navigator.Entity.FormMode == FormMode.FormError ||
                    _Navigator.Entity.FormMode == FormMode.FormAddNew)
                {
                    if (!_AllowAdd || !_Navigator.Entity.AllowAddNew)
                    {
                        simpleButton1.Enabled = false;
                        if (_Navigator.MoveFirst())
                        {
                            SetEditMode();
                            checkButton1_CheckedChanged(null, null);
                        }
                        else
                            SetNew();
                    }
                    else
                        SetNew();
                }

                #region Bug Fix utk Nested Form
                XtraForm Frm = FindForm(((Control)sender).Controls);
                if (Frm != null)
                    BugFixNestedForm(Frm);
                #endregion

                foreach (GridColl gc in ListGrid)
                {
                    gc.gv.MoveFirst();
                    gc.gv.FocusedColumn = gc.gv.GetVisibleColumn(0);
                }
            }
            finally
            {
                ((Control)sender).ResumeLayout();
            }
        }

        private void BugFixNestedForm(Control Frm)
        {
            foreach (Control Ctrl in Frm.Controls)
            {
                foreach (Binding bnd in Ctrl.DataBindings)
                    switch (bnd.PropertyName)
                    {
                        case "Visible":
                            Ctrl.Visible = true;
                            Ctrl.Visible = (bool)_Navigator
                                .Entity[bnd.BindingMemberInfo.BindingField];
                            break;
                        case "Enabled":
                            Ctrl.Enabled = true;
                            Ctrl.Enabled = (bool)_Navigator
                                .Entity[bnd.BindingMemberInfo.BindingField];
                            break;
                    }
                if (Ctrl.Controls.Count > 0)
                    BugFixNestedForm(Ctrl);
            }
        }

        private XtraForm FindForm(ControlCollection Controls)
        {
            foreach (Control Ctrl in Controls)
            {
                XtraForm frm = Ctrl as XtraForm;
                if (frm != null) return frm;
                frm = FindForm(Ctrl.Controls);
                if (frm != null) return frm;
            }
            return null;
        }

        void _Navigator_onDataMoving(MoveType MovingType, bool IsError)
        {
            if (onDataMoving != null)
                onDataMoving(MovingType, IsError);
        }

        //Browse
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    if (_EntityForm == null)
                        _EntityForm = new EntityForm(GetType());
                    _EntityForm.DoBrowseData();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Tampilkan Laporan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            checkEdit2.Checked = !checkEdit2.Checked;
        }

        List<ReportPreview> ListfrmPreview = new List<ReportPreview>();

        public void SetSecurity(string ModuleName)
        {
            SetSecurity(BaseSecurity.GetModuleAccess(ModuleName));
        }
        public void SetSecurity(ModuleAccess ma)
        {
            if (ma == null) return;
            if (BaseSecurity.CurrentLogin.CurrentRole.Length > 0)
            {
                _AllowAdd = ma.GetVariable<bool>(
                    SecurityVarName.DocumentNew, false);
                _AllowEdit = ma.GetVariable<bool>(
                    SecurityVarName.DocumentEdit, false);
                _AllowDelete = ma.GetVariable<bool>(
                    SecurityVarName.DocumentDelete, false);
                AllowBrowse = ma.GetVariable<bool>(
                    SecurityVarName.ReportView, false);
                AllowPrint = ma.GetVariable<bool>(
                    SecurityVarName.DocumentPrint, false);
                AllowDesignPrint = ma.GetVariable<bool>(
                    SecurityVarName.DocumentDesignPrint, false);

                _AllowBrowsePrint = ma.GetVariable<bool>(
                    SecurityVarName.ReportPrint, false);
                _AllowBrowseDesignPrint = ma.GetVariable<bool>(
                    SecurityVarName.ReportDesignPrint, false);
                _AllowBrowseExport = ma.GetVariable<bool>(
                    SecurityVarName.ReportSave, false);
                _AllowBrowseSaveLayout = ma.GetVariable<bool>(
                    SecurityVarName.ReportLayoutSave, false);
            }
        }

        [DebuggerNonUserCode]
        private class PrintThread
        {
            private UINavigator Parent;
            private ReportSingleEntity Entity;
            private MemoryStream LayoutData;

            public PrintThread(UINavigator ParentNavigator, 
                ReportSingleEntity Entity, MemoryStream LayoutData)
            {
                this.Parent = ParentNavigator;
                this.Entity = Entity;
                this.LayoutData = LayoutData;
            }

            public void PrintDocument()
            {
                xReport Rpt = new xReport(Parent._Evaluator);
                Rpt.LoadLayout(LayoutData);
                Rpt.DataSource = Entity;

                Rpt.CreateDocument();
                Rpt.Print();

                LayoutData.Close();
            }
        }

        // Lock/ Unlock
        private bool onChecked = false;
        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkButton1.Image = checkButton1.Checked ? 
                Resources.lock_ok : Resources.lock_open;

            if (_Navigator.Entity.FormMode == FormMode.FormError ||
                onChecked) return;
            try
            {
                ParentForm.SuspendLayout();
                onChecked = true;
                if (_Navigator.Entity.FormMode == FormMode.FormAddNew)
                {
                    if (checkButton1.Checked) checkButton1.Checked = false;
                    //BaseWinFramework.WinForm.AutoLockEntity.LockForm(
                    //    _Navigator.Entity, MetaData.GetTableDef(_Navigator.Entity),
                    //    ParentForm);
                    //if (_LastFormModeIsView)
                    //{
                    //    foreach (GridColl gridc in ListGrid)
                    //        gridc.AttachLookupEdit();

                    //    //if (onLockForm != null) onLockForm(false);
                    //}
                }
                else
                {
                    if (!_AllowEdit || !(_Navigator.Entity.AllowEdit 
                        && _Navigator.Entity.IsTransDateValid()))    // tidak boleh edit
                    {
                        if (!checkButton1.Checked)    // tidak  lock
                            checkButton1.Checked = true;  // lock
                    }
                    else if (AutoFormMode && checkButton1.Checked)
                        checkButton1.Checked = false;

                    _LastFormModeIsView = checkButton1.Checked;
                    if (_LastFormModeIsView)   // terakhir lock
                    {
                        //foreach (GridColl gridc in ListGrid)
                        //    gridc.DetachLookupEdit();

                        if (_Navigator.Entity.FormMode == FormMode.FormEdit)
                        {
                            foreach (GridColl gc in ListGrid)
                                if (object.ReferenceEquals(gc.gv,
                                    gc.gv.GridControl.MainView))
                                    gc.gv.CollapseAllDetails();
                            _Navigator.ReloadFromOriginal();
                        }
                        _Navigator.Entity.FormMode = FormMode.FormView;

                        controlNavigator1.TextStringFormat = "Lihat Data";
                        //if (simpleButton3.Enabled)
                        //    checkButton1.ToolTip = "Data bisa dilihat/ dihapus";
                        //else
                        //    checkButton1.ToolTip = "Data hanya bisa dilihat";

                        //BaseWinFramework.WinForm.AutoLockEntity
                        //    .LockForm(_Navigator.Entity,
                        //    MetaData.GetTableDef(_Navigator.Entity),
                        //    ParentForm);
                        UpdateLookup(true);
                        if (onFormModeChanged != null)
                            onFormModeChanged(FormMode.FormView);
                    }
                    else
                    {
                        foreach (GridColl gridc in ListGrid)
                        {
                            if (object.ReferenceEquals(gridc.gv,
                                gridc.gv.GridControl.MainView))
                                gridc.gv.CollapseAllDetails();
                            //gridc.AttachLookupEdit();
                        }

                        controlNavigator1.TextStringFormat = "Edit Data";
                        //if (simpleButton3.Enabled)
                        //    checkButton1.ToolTip = "Data bisa diedit/ dihapus";
                        //else
                        //    checkButton1.ToolTip = "Data bisa diedit";

                        _Navigator.Entity.FormMode = FormMode.FormEdit;

                        //BaseWinFramework.WinForm.AutoLockEntity
                        //    .LockForm(_Navigator.Entity,
                        //    MetaData.GetTableDef(_Navigator.Entity),
                        //    ParentForm);
                        UpdateLookup(false);
                        if (onFormModeChanged != null)
                            onFormModeChanged(FormMode.FormEdit);
                    }

                    simpleButton2.Enabled = true;
                    simpleButton3.Enabled = _AllowDelete && _Navigator.Entity.AllowDelete
                        && _Navigator.Entity.IsTransDateValid();
                    simpleButton4.Enabled = !_LastFormModeIsView;
                }
            }
            finally
            {
                DoFormChanged();
                ParentForm.ResumeLayout();
                onChecked = false;
            }
            ParentForm.SelectNextControl(ParentForm, true, true, true, false);
        }

        //Cetak
        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            if (_Navigator.Original == null) return;
            using (new WaitCursor())
            {
                List<string> ListPrintLayout = DocPrintBrowseLayout
                    .GetListLayout("D_" + RptName);

                if (ListPrintLayout.Count == 0)
                {
                    frmReport.SaveReportFromTemplateFolder("D_",
                        RptName, ListPrintLayout, null);

                    if (ListPrintLayout.Count == 0)
                    {
                        XtraMessageBox.Show(
                            "Layout Cetak Dokumen tidak ditemukan !",
                            "Error Cetak Dokumen",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                IList ListSelectedLayout = new List<string>();
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

                if (!frmCetak.ShowForm2(ListPrintLayout,
                    (List<string>)ListSelectedLayout,
                    "Cetak Dokumen", ref DefaultUsePrintPreview))
                    return;


                if (UseDefault) DefaultSelectedPrintLayoutId.Clear();

                ReportSingleEntity rse = new ReportSingleEntity(
                    sender == null ? _Navigator.Entity :
                    _Navigator.Original);
                if (DefaultUsePrintPreview)
                {
                    int i = 0;
                    foreach (string Layout in ListSelectedLayout)
                    {
                        if (!ListPrintLayout.Contains(Layout)) continue;

                        MemoryStream LayoutData;
                        DocPrintBrowseLayout.GetLayoutData("D_" + RptName, Layout, out LayoutData);
                        if (LayoutData != null)
                        {
                            if (UseDefault) DefaultSelectedPrintLayoutId.Add(Layout);
                            ReportPreview rp;
                            if (i < ListfrmPreview.Count)
                                rp = ListfrmPreview[i];
                            else
                            {
                                rp = new ReportPreview();
                                ListfrmPreview.Add(rp);
                            }
                            xReport Rpt = new xReport(_Evaluator);
                            Rpt.LoadLayout(LayoutData);
                            Rpt.DataSource = rse;

                            rp.ShowForm(RptName, Rpt);
                            i++;
                            LayoutData.Close();
                        }
                    }
                }
                else
                {
                    foreach (string Layout in ListSelectedLayout)
                    {
                        if (!ListPrintLayout.Contains(Layout)) continue;

                        MemoryStream LayoutData;
                        DocPrintBrowseLayout.GetLayoutData("D_" + RptName, Layout, out LayoutData);
                        if (LayoutData != null)
                        {
                            if (UseDefault) DefaultSelectedPrintLayoutId.Add(Layout);
                            PrintThread pt = new PrintThread(this, rse, LayoutData);
                            Thread td = new Thread(new ThreadStart(pt.PrintDocument));
                            td.Start();
                        }
                    }
                }
            }
        }
    }
}