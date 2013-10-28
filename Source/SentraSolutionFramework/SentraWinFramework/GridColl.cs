using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using SentraSolutionFramework.Entity;
using SentraUtility;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

namespace SentraWinFramework
{
    //[DebuggerNonUserCode]
    internal class GridColl
    {
        //[DebuggerNonUserCode]
        private class xCol
        {
            public RepositoryItemLookUpEditBase rle;
            public string DisplayMember = string.Empty;
            public FieldDef fldDisplay;
            public GetHandler GetSrcHandler;
            public xCol(GridColumn gcol,
                RepositoryItemLookUpEditBase rle, TableDef td, Type ParentType)
            {
                this.rle = rle;
                if (td != null)
                    fldDisplay = td[rle.DisplayMember] ?? td[gcol.FieldName];

                if (fldDisplay == null)
                    DisplayMember = rle.DisplayMember;

                if (ParentType != null)
                    GetSrcHandler = DynamicMethodCompiler.CreateGetHandler(
                        ParentType.GetMember(
                        ((BindingSource)rle.DataSource).DataMember)[0]);
            }
        }
        private Dictionary<int, xCol> ListCols;

        private TableDef td;

        public GridView gv;
        public IEntityCollection eColl;

        public void SetLookupEditDisplayMember(
            RepositoryItemLookUpEditBase le, string DisplayMember, bool AlwaysSet)
        {
            foreach (xCol xc in ListCols.Values)
                if (object.ReferenceEquals(xc.rle, le))
                {
                    if (AlwaysSet || xc.fldDisplay == null &&
                        DisplayMember.Length > 0)
                    {
                        if (td != null)
                            xc.fldDisplay = td[DisplayMember];
                        if (xc.fldDisplay == null)
                            xc.DisplayMember = DisplayMember;
                    }
                    break;
                }
        }

        public GridColl(GridView gv, IEntityCollection eColl,
            BaseEntity Entity)
        {
            ListCols = new Dictionary<int, xCol>();
            this.gv = gv;
            this.eColl = eColl;
            Type ChildType = null;
            if (gv.LevelName.Length > 0)
            {
                foreach (GridColumn gcol in gv.Columns)
                {
                    RepositoryItemLookUpEditBase rle = gcol.ColumnEdit
                        as RepositoryItemLookUpEditBase;
                    if (rle != null)
                    {
                        gcol.SortMode = ColumnSortMode.DisplayText;
                        gcol.FilterMode = ColumnFilterMode.DisplayText;
                        ListCols.Add(gcol.AbsoluteIndex,
                            new xCol(gcol, rle, null, null));
                    }
                }
                return;
            }
            else if (eColl == null)
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

            gv.RowUpdated += new RowObjectEventHandler(gv_RowUpdated);

            td = MetaData.GetTableDef(ChildType);
            Type tp = Entity.GetType();
            foreach (GridColumn gcol in gv.Columns)
            {
                RepositoryItemLookUpEditBase rle = gcol.ColumnEdit
                    as RepositoryItemLookUpEditBase;
                if (rle != null)
                {
                    gcol.SortMode = ColumnSortMode.DisplayText;
                    gcol.FilterMode = ColumnFilterMode.DisplayText;
                    ListCols.Add(gcol.AbsoluteIndex,
                        new xCol(gcol, rle, td, tp));
                }
            }
            gv.CustomColumnDisplayText += new CustomColumnDisplayTextEventHandler(gv_CustomColumnDisplayText);
            gv.CustomRowCellEditForEditing += new CustomRowCellEditEventHandler(gv_CustomRowCellEditForEditing);
        }

        void gv_RowUpdated(object sender, RowObjectEventArgs e)
        {
            if (e.RowHandle < 0) return;
            eColl.ClearError(e.RowHandle);
        }

        void gv_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            xCol xc;
            if (ListCols.TryGetValue(e.Column.AbsoluteIndex, out xc))
            {
                ((BindingSource)xc.rle.DataSource).DataMember = string.Empty;
                ((BindingSource)xc.rle.DataSource).DataSource = 
                    xc.GetSrcHandler(eColl.GetParent());
            }
        }

        void gv_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            UpdateDisplayText(eColl, e);
        }

        public void UpdateDisplayText(IEntityCollection eColl,
            CustomColumnDisplayTextEventArgs e)
        {
            xCol xc;
            if (ListCols.TryGetValue(e.Column.AbsoluteIndex, out xc))
            {
                if (e.ListSourceRowIndex < 0)
                {
                    if (e.RowHandle == GridControl.NewItemRowHandle)
                        e.DisplayText = string.Empty;
                    else if (e.DisplayText.Length == 0)
                        e.DisplayText = e.Value.ToString();
                }
                else if (xc.fldDisplay != null)
                    e.DisplayText = xc.fldDisplay.GetValue(
                        eColl.GetValue(e.ListSourceRowIndex)).ToString();
                else if (xc.DisplayMember.Length > 0)
                {
                    BaseEntity be = eColl.GetValue(e.ListSourceRowIndex);
                    //xc.fldDisplay = MetaData.GetTableDef(be.GetType())
                    //    .GetFieldDef(xc.DisplayMember);
                    //if (xc.fldDisplay == null)
                    //    xc.fldDisplay = MetaData.GetTableDef(be.GetType())
                    //        .GetFieldDef(e.Column.FieldName);
                    //if (xc.fldDisplay != null)
                    //    e.DisplayText = xc.fldDisplay.GetValue(be).ToString();
                    //else
                    //{
                        object Tmp = be[xc.DisplayMember];
                        if (Tmp == null)
                        {
                            if (e.Value != null)
                                e.DisplayText = e.Value.ToString();
                        }
                        else
                            e.DisplayText = Tmp.ToString();
                    //}
                }
                else if (e.Value != null)
                    e.DisplayText = e.Value.ToString();
            }
        }
    }
}
