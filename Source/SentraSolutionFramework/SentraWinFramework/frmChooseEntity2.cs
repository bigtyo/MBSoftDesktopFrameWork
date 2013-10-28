using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Entity;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using SentraUtility;
using DevExpress.XtraGrid.Views.Grid;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SentraWinFramework
{
    public partial class frmChooseEntity2 : XtraForm
    {
        private int Index;
        TableDef td;
        EntityColumnShow ecs;

        public frmChooseEntity2()
        {
            InitializeComponent();
        }

        public object ShowForm(IList ListSrc,
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
                        gv.OptionsBehavior.Editable = false;
                        gv.OptionsBehavior.AllowIncrementalSearch = true;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);

                        if (cs.ColumnShow.Length == 0 ||
                            cs.ColumnShow == "*")
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

            if (Caption.Length == 0)
                Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
            Text = Caption;

            Index = -1;
            ShowDialog();
            return Index < 0 ? null : ListSrc[Index];
        }

        public TEntity ShowForm<TEntity>(IList<TEntity> ListSrc, 
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

                gridView1.FocusedColumn = gridView1.Columns[cols[0]];

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
                        gv.OptionsBehavior.Editable = false;
                        gv.OptionsBehavior.AllowIncrementalSearch = true;

                        gridControl1.LevelTree.Nodes.Add(cs.ChildName, gv);
                        if (cs.ColumnShow.Length == 0 ||
                            cs.ColumnShow == "*")
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
                            //gc.OptionsColumn.ReadOnly = true;
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

            if (Caption.Length == 0)
                Caption = "Pilih " + BaseUtility.SplitName(td.ClassType.Name);
            Text = Caption;

            Index = -1;
            ShowDialog();
            return Index < 0 ? null : ListSrc[Index];
        }

        void gridControl1_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            ((GridView)e.View).BestFitColumns();
        }

        //Pilih
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Index = gridView1.GetDataSourceRowIndex(
                gridView1.FocusedRowHandle);
            Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (hi != null && hi.InRow)
            {
                Index = gridView1.GetDataSourceRowIndex(
                    gridView1.FocusedRowHandle);
                Close();
            }
        }

        GridHitInfo hi;
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                hi = gridView1.CalcHitInfo(e.Location);
            else
                hi = null;
        }
    }
}