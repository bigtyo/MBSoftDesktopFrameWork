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
using SentraSolutionFramework;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SentraSolutionFramework.Persistance;

namespace SentraWinFramework
{
    public partial class frmSelectTable : XtraForm
    {
        private List<bool> Pilih;
        private bool IsOk;
        private List<object[]> RetVal;
        private string[] ColNames;
        private DataPersistance _Dp;

        public frmSelectTable(DataPersistance Dp)
        {
            InitializeComponent();

            _Dp = Dp;
        }
        
        public List<object[]> ShowForm(string ReturnFields,
            string SqlSelect, string Caption, string HideFields,
            params FieldParam[] Parameters)
        {
            ColNames = ReturnFields.Split(',');
            gridControl1.DataSource = _Dp.OpenDataTable(SqlSelect, Parameters);
            int Cnt = gridView1.RowCount;
            if (Cnt == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new List<object[]>();
            }

            Pilih = new List<bool>(Cnt);
            for (int i = 0; i < Cnt; i++)
                Pilih.Add(false);
            if (Caption.Length == 0) Caption = "Pilih Data";
            Text = Caption;
            gridView1.BeginDataUpdate();
            try
            {
                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;
                if (HideFields.Length > 0)
                {
                    string[] ListHideFields = HideFields.Split(',');
                    foreach (string HideField in ListHideFields)
                        gridView1.Columns[HideField].Visible = false;
                }
                GridColumn gc = gridView1.Columns.Add();
                gc.FieldName = "xPilih";
                gc.UnboundType = UnboundColumnType.Boolean;
                gc.Caption = "Pilih";
                gc.AppearanceHeader.TextOptions
                    .HAlignment = HorzAlignment.Center;
                gc.OptionsFilter.AllowFilter = false;
                gc.VisibleIndex = 0;
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridControl(gridControl1, true);
                gridView1.BestFitColumns();
                gc.Width = 50;
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            ShowDialog();
            return IsOk ? RetVal : null;
        }

        //Pilih
        private void simpleButton3_Click(object sender, System.EventArgs e)
        {
            GridColumn colPilih = gridView1.Columns["xPilih"];
   
            RetVal = new List<object[]>();
            for (int i=0; i<gridView1.RowCount; i++) 
            {
                if ((bool)gridView1.GetRowCellValue(i, colPilih)) 
                {
                    object[] Data = new object[ColNames.Length];
                    for(int j = 0; j<ColNames.Length; j++) 
                        Data[j] = gridView1
                            .GetRowCellValue(i, ColNames[j]);
                    RetVal.Add(Data);
                }
            }
            IsOk = true;
            Close();
        }

        //Bersihkan Tanda
        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows();
            GridColumn colPilih = gridView1.Columns["xPilih"];
            foreach (int r in rows)
                gridView1.SetRowCellValue(r, colPilih, false);
        }

        // Tandai
        private void simpleButton1_Click(object sender, System.EventArgs e)
        {
            int[] rows = gridView1.GetSelectedRows();
            GridColumn colPilih = gridView1.Columns["xPilih"];
            foreach (int r in rows)
                gridView1.SetRowCellValue(r, colPilih, true);
        }

        private void gridView1_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = Pilih[e.ListSourceRowIndex];
            else
                Pilih[e.ListSourceRowIndex] = (bool)e.Value;
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