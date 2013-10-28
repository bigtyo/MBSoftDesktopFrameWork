using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using DevExpress.XtraEditors.DXErrorProvider;
using SentraUtility;
using System.Reflection;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;

namespace SentraWinFramework
{
    public partial class frmSingletonEntity : XtraForm, IUIEntity
    {
        private List<GridColl> ListGrid = new List<GridColl>();
        private Dictionary<string, int> DictGrid = new Dictionary<string, int>();

        [DebuggerNonUserCode]
        private class xLE
        {
            public LookUpEdit Le;
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

        private bool DialogMode = false;

        private ParentEntity pe;
        private DXErrorProvider ep;

        private DocumentForm frmc;

        public frmSingletonEntity()
        {
            InitializeComponent();
        }

        public static DialogResult ShowDialog<TControl>()
            where TControl : DocumentForm
        {
            return ShowDialog(typeof(TControl), string.Empty);
        }
        public static DialogResult ShowDialog<TControl>(
            ParentEntity Entity) where TControl : DocumentForm
        {
            return ShowDialog(typeof(TControl), string.Empty, Entity);
        }

        public static DialogResult 
            ShowDialog(Type ControlType, string ModuleName)
        {
            using (new WaitCursor(true))
            {
                if (ControlType.IsSubclassOf(typeof(DocumentForm)))
                {
                    ParentEntity orig;

                    frmSingletonEntity frm = new frmSingletonEntity();

                    Control FormCtrl = (Control)BaseFactory
                        .CreateInstance(ControlType);
                    BindingSource bnd = BaseWinFramework.FindMainBindingSource(
                        (Form)FormCtrl, typeof(ParentEntity));

                    orig = bnd.DataSource as ParentEntity;

                    if (orig == null)
                        orig = (ParentEntity)
                            BaseFactory.CreateInstance((Type)bnd.DataSource);
  
                    if (!orig.LoadEntity())
                        orig.SetDefaultValue(); 
                    
                    frm.pe = (ParentEntity)MetaData.CloneAll(orig);
                    frm.pe.SetOriginal(orig);

                    ((IRuleInitUI)frm.pe).InitUI();

                    DocumentForm frmc = (DocumentForm)FormCtrl;
                    frm.frmc = frmc;

                    bnd.DataSource = frm.pe;
                    IEntityControl df = FormCtrl as IEntityControl;
                    if (df != null)
                        df.InitNavigator(null);
                    ((IRuleInitUI)frm.pe).AfterInitNavigator(null);

                    BaseWinFramework.WinForm.AutoFormat
                        .AutoFormatForm(FormCtrl, false);
                    BaseWinFramework.WinForm.AutoLockEntity.LockForm(frm.pe,
                        MetaData.GetTableDef(frm.pe.GetType()), FormCtrl);

                    frm.ep = new DXErrorProvider(frm);
                    frm.ep.DataSource = bnd;

                    if (frmc != null)
                    {
                        frmc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        frmc.FormClosed += new FormClosedEventHandler(frmc_FormClosed);
                        frmc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        frmc.TopLevel = false;
                        frmc.Parent = frm;
                        frm.Width = frmc.Width + frm.Width -
                            frm.DisplayRectangle.Width;

                        if (frmc.ShowConfirmButton)
                            frm.Height = frmc.Height + 70;
                        else
                        {
                            frm.Height = frmc.Height + frm.Height -
                                frm.DisplayRectangle.Height;
                            frm.simpleButton1.Visible = false;
                            frm.simpleButton2.Visible = false;
                        }
                        frmc.Show();
                    }
                    else
                    {
                        frm.Width = FormCtrl.Width + frm.Width - frm.DisplayRectangle.Width;
                        frm.Height = FormCtrl.Height + 70;
                        frm.Controls.Add(FormCtrl);
                    }
                    if (frmc != null && frmc.Text.Length > 0)
                        frm.Text = frmc.Text;
                    else
                        frm.Text = BaseUtility.SplitName(ModuleName.Length == 0 ?
                            orig.GetType().Name : ModuleName);

                    ((Control)frmc).TabIndex = 0;
                    return frm.ShowForm(BaseWinFramework.MdiParent);
                }
                else
                {
                    XtraForm frm = (XtraForm)BaseFactory.CreateInstance(ControlType);
                    return frm.ShowDialog(BaseWinFramework.MdiParent);
                }
            }
        }

        static void frmc_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)((Form)sender).Parent).Close();
        }

        public static DialogResult 
            ShowDialog(Type ControlType, string ModuleName,
            ParentEntity Entity)
        {
            using (new WaitCursor(true))
            {
                if (ControlType.IsSubclassOf(typeof(DocumentForm)))
                {
                    ParentEntity orig;

                    frmSingletonEntity frm = new frmSingletonEntity();

                    Control FormCtrl = (Control)BaseFactory
                        .CreateInstance(ControlType);
                    BindingSource bnd = BaseWinFramework.FindMainBindingSource(
                        (Form)FormCtrl, typeof(ParentEntity));

                    orig = bnd.DataSource as ParentEntity;

                    if (orig == null)
                    {
                        if (Entity == null)
                        {
                            orig = (ParentEntity)
                                BaseFactory.CreateInstance((Type)bnd.DataSource);
                            if (!orig.LoadEntity())
                                orig.SetDefaultValue();
                            frm.pe = (ParentEntity)MetaData.CloneAll(orig);
                            frm.pe.SetOriginal(orig);
                        }
                        else
                        {
                            frm.pe = Entity;
                            MetaData.CloneToOriginal(Entity);
                            orig = Entity.GetOriginal();
                        }
                    }

                    frm.DialogMode = true;

                    ((IRuleInitUI)frm.pe).InitUI();

                    DocumentForm frmc = (DocumentForm)FormCtrl;
                    frm.frmc = frmc;

                    bnd.DataSource = frm.pe;
                    IEntityControl df = FormCtrl as IEntityControl;
                    if (df != null)
                        df.InitNavigator(null);
                    ((IRuleInitUI)frm.pe).AfterInitNavigator(null);

                    BaseWinFramework.WinForm.AutoFormat
                        .AutoFormatForm(FormCtrl, false);
                    //BaseWinFramework.WinForm.AutoLockEntity.LockForm(frm.pe,
                    //    MetaData.GetTableDef(frm.pe.GetType()), FormCtrl);

                    frm.ep = new DXErrorProvider(frm);
                    frm.ep.DataSource = bnd;

                    if (frmc != null)
                    {
                        frmc.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        frmc.FormClosed += new FormClosedEventHandler(frmc_FormClosed);
                        frmc.TopLevel = false;
                        frmc.Parent = frm;
                        frm.Width = frmc.Width + frm.Width -
                            frm.DisplayRectangle.Width;

                        if (frmc.ShowConfirmButton)
                            frm.Height = frmc.Height + 70;
                        else
                        {
                            frm.Height = frmc.Height + frm.Height -
                                frm.DisplayRectangle.Height;
                            frm.simpleButton1.Visible = false;
                            frm.simpleButton2.Visible = false;
                        }
                        frmc.Show();
                    }
                    else
                    {
                        frm.Width = FormCtrl.Width + frm.Width - frm.DisplayRectangle.Width;
                        frm.Height = FormCtrl.Height + 70;
                        frm.Controls.Add(FormCtrl);
                    }
                    if (frmc != null && frmc.Text.Length > 0)
                        frm.Text = frmc.Text;
                    else
                        frm.Text = BaseUtility.SplitName(ModuleName.Length == 0 ?
                            orig.GetType().Name : ModuleName);

                    ((Control)frmc).TabIndex = 0;
                    return frm.ShowForm(BaseWinFramework.MdiParent);
                }
                else
                {
                    XtraForm frm = (XtraForm)BaseFactory.CreateInstance(ControlType);
                    return frm.ShowDialog(BaseWinFramework.MdiParent);
                }
            }
        }

        private DialogResult ShowForm(Form MdiParent)
        {
            pe.UIEntity = this;
            pe.OnFormChanged += new OnAction(pe_OnFormChanged);
            pe.UIEntity = this;
            FindGridAndLookup(Controls);

            return ShowDialog(MdiParent);
        }

        void pe_OnFormChanged()
        {
            FormMode fm = pe.FormMode;
            BaseWinFramework.WinForm.AutoLockEntity.LockForm(pe,
                MetaData.GetTableDef(pe), this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (DialogMode) return;

            try
            {
                pe.Save();
                XtraMessageBox.Show(Text + " sudah Tersimpan !",
                    "Konfirmasi Simpan " + Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ep.UpdateBinding();
                pe.ClearError();
                XtraMessageBox.Show(ex.Message, "Error Simpan " +
                    Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
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

        private void FindGridAndLookup(Control.ControlCollection Ctrls)
        {
            foreach (Control Ctrl in Ctrls)
            {
                GridControl gc = Ctrl as GridControl;
                if (gc != null)
                {
                    BindingSource bs = gc.DataSource as BindingSource;
                    if (bs == null) continue;
                    IEntityCollection ec = null;

                    string FieldName = bs.DataMember.Length > 0 ?
                        bs.DataMember : gc.DataMember;

                    if (bs.DataSource.GetType() == typeof(BindingSource))
                        bs = (BindingSource)bs.DataSource;

                    if (FieldName.Length > 0)
                    {
                        Type t = bs.DataSource.GetType();
                        PropertyInfo pi = t.GetProperty(FieldName,
                            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (pi != null)
                            ec = pi.GetValue(bs.DataSource, null)
                                as IEntityCollection;
                        else
                        {
                            FieldInfo fi = t.GetField(FieldName,
                                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                            if (fi != null)
                                ec = fi.GetValue(bs.DataSource)
                                    as IEntityCollection;
                        }
                    }

                    if (ec == null) continue;

                    if (gc.ViewCollection.Count > 0)
                    {
                        gc.ViewRegistered += new ViewOperationEventHandler(gc_ViewRegistered);
                        gc.KeyDown += new KeyEventHandler(gc_KeyDown);
                    }
                    foreach (GridView gv in gc.ViewCollection)
                    {
                        if (gv.SourceView != null) continue;

                        //SetGridLookupEditDisplayMember(gv, null,
                        //    string.Empty, ec, false);

                        gv.OptionsBehavior.AllowIncrementalSearch = true;

                        foreach (GridColumn gcl in gv.Columns)
                        {
                            RepositoryItemLookUpEditBase rle = 
                                gcl.ColumnEdit as RepositoryItemLookUpEditBase;
                            if (rle != null)
                            {
                                SetGridLookupEditDisplayMember(gv,
                                    null, string.Empty,
                                    gv.LevelName.Length == 0 ? ec : null,
                                    false);
                                break;
                            }

                            //if (rle != null)
                            //    SetGridLookupEditDisplayMember(gv, rle,
                            //        gcl.FieldName,
                            //        gv.LevelName.Length == 0 ? ec : null,
                            //        false);
                        }
                    }
                    foreach (RepositoryItem ri in gc.RepositoryItems)
                    {
                        RepositoryItemLookUpEditBase rle = ri as
                            RepositoryItemLookUpEditBase;
                        if (rle != null)
                            foreach (EditorButton b in rle.Buttons)
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

                        foreach (EditorButton b in le
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

        void gc_KeyDown(object sender, KeyEventArgs e)
        {
            GridView gv = (GridView)((GridControl)sender).FocusedView;
            if (e.Shift && gv.IsShowDetailButtons)
            {
                if (e.KeyCode == Keys.Left)
                {
                    if (gv.GetMasterRowExpanded(gv.FocusedRowHandle))
                        gv.CollapseMasterRow(gv.FocusedRowHandle);
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (!gv.GetMasterRowExpanded(gv.FocusedRowHandle))
                        gv.ExpandMasterRow(gv.FocusedRowHandle);
                }
            }
        }

        void gc_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            ((GridView)e.View).RowUpdated += new RowObjectEventHandler(frmSingletonEntity_RowUpdated);
            ((GridView)e.View).CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(frmSingletonEntity_CustomColumnDisplayText);
            ((GridView)e.View).CustomRowCellEdit += new CustomRowCellEditEventHandler(frmSingletonEntity_CustomRowCellEdit);
        }

        [DebuggerNonUserCode]
        private class TempEditing
        {
            private MemberInfo mi;
            private object DataSource;

            public TempEditing(MemberInfo mi, object DataSource)
            {
                this.mi = mi;
                this.DataSource = DataSource;
            }

            public object ReQueryDataSource()
            {
                if (mi.MemberType == MemberTypes.Property)
                    return ((PropertyInfo)mi).GetValue(DataSource, null);
                else
                    return ((FieldInfo)mi).GetValue(DataSource);
            }
        }

        void frmSingletonEntity_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            RepositoryItemLookUpEditBase rle = 
                e.RepositoryItem as RepositoryItemLookUpEditBase;
            if (rle == null) return;

            BindingSource bs = (BindingSource)rle.DataSource;
            if (bs.DataMember.Length > 0)
            {
                Type EntityType = bs.DataSource.GetType();
                PropertyInfo pi = EntityType.GetProperty(bs.DataMember);
                if (pi != null)
                {
                    rle.Tag = new TempEditing(pi, bs.DataSource);
                    bs.DataSource = pi.GetValue(bs.DataSource, null);
                }
                else
                {
                    FieldInfo fi = EntityType.GetField(bs.DataMember);
                    if (fi != null)
                    {
                        rle.Tag = new TempEditing(fi, bs.DataSource);
                        bs.DataSource = fi.GetValue(bs.DataSource);
                    }
                }
                bs.DataMember = string.Empty;
            }
            else
            {
                TempEditing te = rle.Tag as TempEditing;
                if (te != null)
                {
                    bs.DataSource = te.ReQueryDataSource();
                }
            }
        }

        void frmSingletonEntity_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            RepositoryItemLookUpEditBase rle = 
                e.Column.ColumnEdit as RepositoryItemLookUpEditBase;
            if (rle == null) return;

            GridView gv = (GridView)sender;
            string TableName = gv.LevelName;
            IEntityCollection eColl = (IEntityCollection)gv.DataSource;

            GridColl gc = ListGrid[DictGrid[TableName]];
            gc.UpdateDisplayText(eColl, e);
        }

        void frmSingletonEntity_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (e.RowHandle < 0) return;
            ((BaseEntity)e.Row).ClearError();
        }

        public void SetGridLookupEditDisplayMember(GridView gv,
            RepositoryItemLookUpEditBase Le, string DisplayMember)
        {
            SetGridLookupEditDisplayMember(gv, Le, DisplayMember,
                null, true);
        }

        private void SetGridLookupEditDisplayMember(GridView gv,
            RepositoryItemLookUpEditBase Le, string DisplayMember,
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
                GridColl gc = new GridColl(gv, eColl, pe);
                if (Le != null)
                    gc.SetLookupEditDisplayMember(Le, DisplayMember, AlwaysSet);
                ListGrid.Add(gc);
                if (gv.LevelName.Length > 0)
                    DictGrid[gv.LevelName] = ListGrid.Count - 1;
                else if (eColl == null)
                    DictGrid[((BindingSource)gv.GridControl.DataSource).DataMember] =
                        ListGrid.Count - 1;
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
                ((IAutoUpdateList)((BindingSource)((LookUpEdit)sender)
                    .Properties.DataSource).DataSource).Refresh();
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
            }
            else if (AlwaysSet)
                FindLe.DisplayMember = DisplayMember;
        }

        #region IUIEntity Members
        bool IUIEntity.TryGetFocusedRowValue<TType>(string TableName,
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
                GridView gv = TableName.Length > 0 ?
                    ListGrid[DictGrid[TableName]].gv :
                    (GridView)((GridControl)
                    ((Form)ParentForm.ActiveControl).ActiveControl)
                    .FocusedView;

                if (typeof(TType).IsSubclassOf(typeof(BaseEntity)))
                {
                    object TmpVal = BaseFactory.CreateInstance<TType>();
                    Type tp = typeof(TType);

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
                    Value = (TType)TmpVal;
                    RetVal = !AllNull;
                }
                else
                {
                    object TmpVal = gv.GetFocusedRowCellValue(FieldName);
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
        #endregion
    }
}