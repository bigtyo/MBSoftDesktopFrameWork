using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using SentraSolutionFramework;
using DevExpress.XtraGrid.Columns;

namespace SentraWinFramework
{
    public partial class frmSelectTable2 : XtraForm
    {
        private bool IsOk;
        private object[] RetVal;
        private string[] ColNames;

        public frmSelectTable2()
        {
            InitializeComponent();
        }

        public object[] ShowForm(string ReturnFields, 
            string SqlSelect, string Caption)
        {
            ColNames = ReturnFields.Split(',');
            gridControl1.DataSource = BaseFramework
                .DefaultDataPersistance.OpenDataTable(SqlSelect);
            int Cnt = gridView1.RowCount;
            if (Cnt == 0)
            {
                XtraMessageBox.Show("Data Tidak Ditemukan !",
                    "Error Membaca Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new object[0];
            }

            if (Caption.Length == 0) Caption = "Data";
            Text = "Pilih " + Caption;
            gridView1.BeginDataUpdate();
            try
            {
                foreach (GridColumn gcol in gridView1.Columns)
                    gcol.OptionsColumn.ReadOnly = true;
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatReadOnlyGridControl(gridControl1, true);
                gridView1.BestFitColumns();
            }
            finally
            {
                gridView1.EndDataUpdate();
            }
            IsOk = false;
            ShowDialog();
            return IsOk ? RetVal : null;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }
    }
}