using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using SentraUtility;
using DevExpress.XtraGrid.Views.Grid;
using SentraWinFramework.Report;
using SentraSolutionFramework.Entity;
using System.Collections;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SentraWinFramework
{
    public partial class frmChooseEntity : XtraForm
    {
        private List<bool> Pilih;
        private IList RetVal;

        private bool IsOk;
        TableDef td;
        EntityColumnShow ecs;

        public frmChooseEntity()
        {
            InitializeComponent();
        }

        private static bool CompareEntity(TableDef td, BaseEntity Ent1, BaseEntity Ent2)
        {
            if (td.KeyFields.Count > 0)
            {
                foreach (FieldDef fld in td.KeyFields.Values)
                    if (!fld.GetValue(Ent1).Equals(fld.GetValue(Ent2)))
                        return false;
                return true;
            }
            else
            {
                foreach (FieldDef fld in td.NonKeyFields.Values)
                    switch (fld.DataType)
                    {
                        case DataType.Binary:
                        case DataType.Image:
                        case DataType.TimeStamp:
                            break;
                        default:
                            if (!fld.GetValue(Ent1).Equals(
                                fld.GetValue(Ent2)))
                                return false;
                            break;
                    }
                return true;
            }
        }

        #region ShowForm
        public IList ShowForm(IList ListSrc,
            string Caption, EntityColumnShow ecs)
        {
            if (ListSrc == null || ListSrc.Count == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data " + Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            td = MetaData.GetTableDef(ListSrc[0].GetType());
            if (ecs == null) ecs = new EntityColumnShow(string.Join(
                ",", td.GetListFieldNames(true).ToArray()));

            this.ecs = ecs;
            gridControl1.BeginUpdate();
            try
            {
                gridControl1.DataSource =
                    new ReportGrid(td.ClassType, ListSrc, ecs);
                string[] cols;
                bool FromECS;
                if (ecs.ColumnShow.Length > 0)
                {
                    cols = ecs.ColumnShow.Split(',');
                    FromECS = true;
                }
                else
                {
                    cols = td.GetListFieldNames(true).ToArray();
                    FromECS = false;
                }

                foreach (GridColumn gc in gridView1.Columns)
                    gc.VisibleIndex = -1;

                int i = 0;
                GridColumn FirstCol = null;
                foreach (string col in cols)
                {
                    int idx = FromECS ? col.IndexOf(" AS ", StringComparison.OrdinalIgnoreCase) : -1;
                    GridColumn gc;
                    if (idx < 0)
                        gc = gridView1.Columns[col.Trim()];
                    else
                    {
                        gc = gridView1.Columns[col.Substring(0, idx).Trim()];
                        gc.Caption = BaseUtility.SplitName(col.Substring(idx + 4).Trim());
                    }
                    if (i == 0) FirstCol = gc;
                    gc.VisibleIndex = i++;
                }
                
                gridView1.FocusedColumn = FirstCol;

                if (ecs.ListChild.Count > 0)
                {
                    gridControl1.ViewRegistered += new ViewOperationEventHandler(gridControl1_ViewRegistered);
                    int j = 0;
                    if (ecs.ListChild.Count == 1)
                        gridView1.OptionsDetail.ShowDetailTabs = false; 

                    foreach (ChildColumnShow cs in ecs.ListChild)
                    {
                        GridView gv = new GridView(gridControl1);
                        gv.OptionsDetail.EnableMasterViewMode = false;
                        gv.OptionsView.ColumnAutoWidth = false;
                        gv.OptionsView.EnableAppearanceEvenRow = true;
                        gv.OptionsView.EnableAppearanceOddRow = true;
                        gv.OptionsView.ShowGroupPanel = false;
                        gv.BestFitMaxRowCount = 15;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);
                        
                        if (cs.ColumnShow.Length == 0)
                        {
                            cols = td.ChildEntities[j].GetTableDef()
                                .GetListFieldNames(true).ToArray();
                            FromECS = false;
                        }
                        else
                        {
                            cols = cs.ColumnShow.Split(',');
                            FromECS = true;
                        }

                        i = 0;
                        foreach (string colName in cols)
                        {
                            int idx = FromECS ? colName.IndexOf(" AS ", 
                                StringComparison.OrdinalIgnoreCase) : -1;
                            GridColumn gc;
                            if (idx < 0)
                                gc = gv.Columns.AddField(colName.Trim());
                            else
                            {
                                gc = gv.Columns.AddField(colName.Substring(0, idx).Trim());
                                gc.Caption = BaseUtility.SplitName(colName.Substring(idx + 4).Trim());
                            }
                            gc.VisibleIndex = i++;
                            gc.OptionsColumn.ReadOnly = true;
                        }

                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(
                            td.ChildEntities[j++].GetChildType(), gv, true);
                    }
                }
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridView(td.ClassType,
                    gridView1, true);
            }
            catch
            {
                XtraMessageBox.Show("Nama Kolom tidak ditemukan dalam Entity",
                    "Error mengatur penampakan kolom",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { gridControl1.EndUpdate(); }

            Pilih = new List<bool>(ListSrc.Count);

            for (int i = 0; i < ListSrc.Count; i++)
                Pilih.Add(false);

            if (Caption.Length == 0)
                Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
            Text = Caption;
            gridView1.BeginDataUpdate();
            try
            {
                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;

                GridColumn gc = gridView1.Columns.Add();
                gc.FieldName = "xPilih";
                gc.UnboundType = UnboundColumnType.Boolean;
                gc.Caption = "Pilih";
                gc.AppearanceHeader.TextOptions
                    .HAlignment = HorzAlignment.Center;
                gc.OptionsFilter.AllowFilter = false;
                gc.VisibleIndex = 0;
                BaseWinFramework.WinForm.AutoFormat.AutoFormatGridControl(gridControl1, true);
                gridView1.BestFitColumns();
                gc.Width = 50;
                gridView1.FocusedColumn = gc;
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            RetVal = ListSrc;
            ShowDialog();
            return IsOk ? ListSrc : null;
        }

        public IList ShowForm(IList ListSrc, string Caption, 
            EntityColumnShow ecs, IList OldListSelect)
        {
            if (ListSrc == null || ListSrc.Count == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data " + Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            td = MetaData.GetTableDef(ListSrc[0].GetType());
            if (ecs == null) ecs = new EntityColumnShow(string.Join(
                ",", td.GetListFieldNames(true).ToArray()));

            this.ecs = ecs;
            gridControl1.BeginUpdate();
            try
            {
                gridControl1.DataSource =
                    new ReportGrid(td.ClassType, ListSrc, ecs);
                string[] cols;
                if (ecs.ColumnShow.Length > 0)
                    cols = ecs.ColumnShow.Split(',');
                else
                    cols = td.GetListFieldNames(true).ToArray();

                foreach (GridColumn gc in gridView1.Columns)
                    gc.VisibleIndex = -1;

                int i = 0;
                foreach (string col in cols)
                    gridView1.Columns[col.Trim()].VisibleIndex = i++;

                if (ecs.ListChild.Count > 0)
                {
                    gridControl1.ViewRegistered += new ViewOperationEventHandler(gridControl1_ViewRegistered);
                    int j = 0;
                    if (ecs.ListChild.Count == 1)
                        gridView1.OptionsDetail.ShowDetailTabs = false; 
                    foreach (ChildColumnShow cs in ecs.ListChild)
                    {
                        GridView gv = new GridView(gridControl1);
                        gv.OptionsDetail.EnableMasterViewMode = false;
                        gv.OptionsView.ColumnAutoWidth = false;
                        gv.OptionsView.EnableAppearanceEvenRow = true;
                        gv.OptionsView.EnableAppearanceOddRow = true;
                        gv.OptionsView.ShowGroupPanel = false;
                        gv.BestFitMaxRowCount = 15;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);
                        if (cs.ColumnShow.Length == 0)
                            cols = td.ChildEntities[j].GetTableDef()
                                .GetListFieldNames(true).ToArray();
                        else
                            cols = cs.ColumnShow.Split(',');
                        i = 0;
                        foreach (string colName in cols)
                        {
                            GridColumn gc = gv.Columns.AddField(
                                colName.Trim());
                            gc.VisibleIndex = i++;
                            gc.OptionsColumn.ReadOnly = true;
                        }

                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(
                            td.ChildEntities[j++].GetChildType(), gv, true);
                    }
                }
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridView(td.ClassType,
                    gridView1, true);
            }
            catch
            {
                XtraMessageBox.Show("Nama Kolom tidak ditemukan dalam Entity",
                    "Error mengatur penampakan kolom",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { gridControl1.EndUpdate(); }

            gridView1.BeginDataUpdate();
            try
            {
                Pilih = new List<bool>(ListSrc.Count);

                int SrcCnt = ListSrc.Count;
                for (int i = 0; i < SrcCnt; i++)
                    Pilih.Add(false);

                if (OldListSelect != null)
                {
                    int LastIndex = 0;
                    foreach (object obj in OldListSelect)
                        for (int i = LastIndex; i < SrcCnt; i++)
                            if (CompareEntity(td, (BaseEntity)obj,
                                (BaseEntity)ListSrc[i]))
                            {
                                LastIndex = i;
                                Pilih[i] = true;
                            }
                }
                if (Caption.Length == 0)
                    Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
                Text = Caption;

                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;

                GridColumn gc = gridView1.Columns.Add();
                gc.FieldName = "xPilih";
                gc.UnboundType = UnboundColumnType.Boolean;
                gc.Caption = "Pilih";
                gc.AppearanceHeader.TextOptions
                    .HAlignment = HorzAlignment.Center;
                gc.OptionsFilter.AllowFilter = false;
                gc.VisibleIndex = 0;
                BaseWinFramework.WinForm.AutoFormat.AutoFormatGridControl(gridControl1, true);
                gridView1.BestFitColumns();
                gc.Width = 50;
                gridView1.FocusedColumn = gc;
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            RetVal = ListSrc;
            ShowDialog();
            return IsOk ? ListSrc : null;
        }
        #endregion

        #region ShowForm Generic
        public IList<TEntity> ShowForm<TEntity>(IList<TEntity> ListSrc, 
            string Caption, EntityColumnShow ecs)
            where TEntity : BaseEntity
        {
            if (ListSrc == null || ListSrc.Count == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data " + typeof(TEntity).Name,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            td = MetaData.GetTableDef(typeof(TEntity));
            if (ecs == null) ecs = new EntityColumnShow(string.Join(
                ",", td.GetListFieldNames(true).ToArray()));

            this.ecs = ecs;
            gridControl1.BeginUpdate();
            try
            {
                gridControl1.DataSource =
                    new ReportGrid(td.ClassType, (IList)ListSrc, ecs);
                string[] cols;
                if (ecs.ColumnShow.Length > 0)
                    cols = ecs.ColumnShow.Split(',');
                else
                    cols = td.GetListFieldNames(true).ToArray();

                foreach (GridColumn gc in gridView1.Columns)
                    gc.VisibleIndex = -1;

                int i = 0;
                foreach (string col in cols)
                    gridView1.Columns[col.Trim()].VisibleIndex = i++;

                if (ecs.ListChild.Count > 0)
                {
                    gridControl1.ViewRegistered += new ViewOperationEventHandler(gridControl1_ViewRegistered);
                    int j = 0;
                    if (ecs.ListChild.Count == 1)
                        gridView1.OptionsDetail.ShowDetailTabs = false; 
                    foreach (ChildColumnShow cs in ecs.ListChild)
                    {
                        GridView gv = new GridView(gridControl1);
                        gv.OptionsDetail.EnableMasterViewMode = false;
                        gv.OptionsView.ColumnAutoWidth = false;
                        gv.OptionsView.EnableAppearanceEvenRow = true;
                        gv.OptionsView.EnableAppearanceOddRow = true;
                        gv.OptionsView.ShowGroupPanel = false;
                        gv.BestFitMaxRowCount = 15;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);
                        if (cs.ColumnShow.Length == 0)
                            cols = td.ChildEntities[j].GetTableDef()
                                .GetListFieldNames(true).ToArray();
                        else
                            cols = cs.ColumnShow.Split(',');
                        i = 0;
                        foreach (string colName in cols)
                        {
                            GridColumn gc = gv.Columns.AddField(
                                colName.Trim());
                            gc.VisibleIndex = i++;
                            gc.OptionsColumn.ReadOnly = true;
                        }

                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(
                            td.ChildEntities[j++].GetChildType(), gv, true);
                    }
                }
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridView(td.ClassType,
                    gridView1, true);
            }
            catch
            {
                XtraMessageBox.Show("Nama Kolom tidak ditemukan dalam Entity",
                    "Error mengatur penampakan kolom",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { gridControl1.EndUpdate(); }

            gridView1.BeginDataUpdate();
            try
            {
                Pilih = new List<bool>(ListSrc.Count);

                for (int i = 0; i < ListSrc.Count; i++)
                    Pilih.Add(false);

                if (Caption.Length == 0)
                    Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
                Text = Caption;

                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;

                GridColumn gc = gridView1.Columns.Add();
                gc.FieldName = "xPilih";
                gc.UnboundType = UnboundColumnType.Boolean;
                gc.Caption = "Pilih";
                gc.AppearanceHeader.TextOptions
                    .HAlignment = HorzAlignment.Center;
                gc.OptionsFilter.AllowFilter = false;
                gc.VisibleIndex = 0;
                BaseWinFramework.WinForm.AutoFormat.AutoFormatGridControl(gridControl1, true);
                gridView1.BestFitColumns();
                gc.Width = 50;
                gridView1.FocusedColumn = gc;
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            RetVal = (IList)ListSrc;
            ShowDialog();
            return IsOk ? ListSrc : null;
        }

        public IList<TEntity> ShowForm<TEntity>(IList<TEntity> ListSrc,
            string Caption, EntityColumnShow ecs, 
            IList<TEntity> OldListSelect)
            where TEntity : BaseEntity
        {
            if (ListSrc == null || ListSrc.Count == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data " + typeof(TEntity).Name,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            td = MetaData.GetTableDef(typeof(TEntity));
            if (ecs == null) ecs = new EntityColumnShow(string.Join(
                ",", td.GetListFieldNames(true).ToArray()));

            this.ecs = ecs;
            gridControl1.BeginUpdate();
            try
            {
                gridControl1.DataSource =
                    new ReportGrid(td.ClassType, (IList)ListSrc, ecs);
                string[] cols;
                if (ecs.ColumnShow.Length > 0)
                    cols = ecs.ColumnShow.Split(',');
                else
                    cols = td.GetListFieldNames(true).ToArray();

                foreach (GridColumn gc in gridView1.Columns)
                    gc.VisibleIndex = -1;

                int i = 0;
                foreach (string col in cols)
                    gridView1.Columns[col.Trim()].VisibleIndex = i++;

                if (ecs.ListChild.Count > 0)
                {
                    gridControl1.ViewRegistered += new ViewOperationEventHandler(gridControl1_ViewRegistered);
                    int j = 0;
                    if (ecs.ListChild.Count == 1)
                        gridView1.OptionsDetail.ShowDetailTabs = false; 
                    foreach (ChildColumnShow cs in ecs.ListChild)
                    {
                        GridView gv = new GridView(gridControl1);
                        gv.OptionsDetail.EnableMasterViewMode = false;
                        gv.OptionsView.ColumnAutoWidth = false;
                        gv.OptionsView.EnableAppearanceEvenRow = true;
                        gv.OptionsView.EnableAppearanceOddRow = true;
                        gv.OptionsView.ShowGroupPanel = false;
                        gv.BestFitMaxRowCount = 15;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);
                        if (cs.ColumnShow.Length == 0)
                            cols = td.ChildEntities[j].GetTableDef()
                                .GetListFieldNames(true).ToArray();
                        else
                            cols = cs.ColumnShow.Split(',');
                        i = 0;
                        foreach (string colName in cols)
                        {
                            GridColumn gc = gv.Columns.AddField(
                                colName.Trim());
                            gc.VisibleIndex = i++;
                            gc.OptionsColumn.ReadOnly = true;
                        }

                        BaseWinFramework.WinForm.AutoFormat
                            .AutoFormatReadOnlyGridView(
                            td.ChildEntities[j++].GetChildType(), gv, true);
                    }
                }
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridView(td.ClassType,
                    gridView1, true);
            }
            catch
            {
                XtraMessageBox.Show("Nama Kolom tidak ditemukan dalam Entity",
                    "Error mengatur penampakan kolom",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { gridControl1.EndUpdate(); }

            gridView1.BeginDataUpdate();
            try
            {
                int SrcCnt = ListSrc.Count;
                Pilih = new List<bool>(SrcCnt);

                for (int i = 0; i < SrcCnt; i++)
                    Pilih.Add(false);

                if (OldListSelect != null)
                {
                    int LastIndex = 0;
                    foreach (TEntity Ent in OldListSelect)
                        for (int i = LastIndex; i < SrcCnt; i++)
                            if (CompareEntity(td, Ent,
                                ListSrc[i]))
                            {
                                Pilih[i] = true;
                                LastIndex = i;
                            }
                }

                if (Caption.Length == 0)
                    Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
                Text = Caption;

                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;

                GridColumn gc = gridView1.Columns.Add();
                gc.FieldName = "xPilih";
                gc.UnboundType = UnboundColumnType.Boolean;
                gc.Caption = "Pilih";
                gc.AppearanceHeader.TextOptions
                    .HAlignment = HorzAlignment.Center;
                gc.OptionsFilter.AllowFilter = false;
                gc.VisibleIndex = 0;
                BaseWinFramework.WinForm.AutoFormat.AutoFormatGridControl(gridControl1, true);
                gridView1.BestFitColumns();
                gc.Width = 50;
                gridView1.FocusedColumn = gc;
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            RetVal = (IList)ListSrc;
            ShowDialog();
            return IsOk ? ListSrc : null;
        }
        #endregion

        void gridControl1_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            ((GridView)e.View).BestFitColumns();
        }

        private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = Pilih[e.ListSourceRowIndex];
            else
                Pilih[e.ListSourceRowIndex] = (bool)e.Value;
        }

        //Pilih
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            GridColumn colPilih = gridView1.Columns["xPilih"];
            gridView1.BeginDataUpdate();
            for (int i = 0; i < RetVal.Count; i++)
                if (!Pilih[i])
                {
                    Pilih.RemoveAt(i);
                    RetVal.RemoveAt(i--);
                }
            IsOk = true;
            gridView1.EndDataUpdate();
            Close();
        }

        //Tandai
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows();
            GridColumn colPilih = gridView1.Columns["xPilih"];
            foreach (int r in rows)
                gridView1.SetRowCellValue(r, colPilih, true);
        }

        //Bersihkan Tandai
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows();
            GridColumn colPilih = gridView1.Columns["xPilih"];
            foreach (int r in rows)
                gridView1.SetRowCellValue(r, colPilih, false);
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GridHitInfo hi = gridView1.CalcHitInfo(e.Location);
                if (hi.HitTest == GridHitTest.ColumnButton && hi.Column == null)
                {
                    gridView1.SelectAll();
                    return;
                }
            }
        }
    }
}