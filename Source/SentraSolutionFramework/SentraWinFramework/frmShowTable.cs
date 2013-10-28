using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraGrid.Columns;
using System.Windows.Forms;
using SentraSolutionFramework;
using DevExpress.Utils;

namespace SentraWinFramework
{
    public partial class frmShowTable : DevExpress.XtraEditors.XtraForm
    {
        private DataPersistance _Dp;
        private SentraSolutionFramework.xMessageBoxButtons _Buttons;
        private SentraSolutionFramework.xDialogResult RetVal;

        public frmShowTable(DataPersistance Dp)
        {
            InitializeComponent();
            _Dp = Dp;
        }

        public xDialogResult ShowForm(string SqlSelect, string Caption, string Message, 
            xMessageBoxButtons Buttons, out bool IsDataExist,
            Dictionary<string, string> FormatCols, params FieldParam[] Parameters)
        {
            _Buttons = Buttons;
            gridControl1.DataSource = _Dp.OpenDataTable(SqlSelect, Parameters);
            IsDataExist = gridView1.RowCount > 0;

            switch (Buttons)
            {
                case SentraSolutionFramework.xMessageBoxButtons.AbortRetryIgnore:
                    simpleButton3.Text = "Batal";
                    simpleButton2.Text = "Ulangi";
                    simpleButton1.Text = "Abaikan";
                    RetVal = SentraSolutionFramework.xDialogResult.Ignore;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.OK:
                    simpleButton3.Visible = false;
                    simpleButton2.Visible = false;
                    simpleButton1.Text = "Ok";
                    RetVal = SentraSolutionFramework.xDialogResult.OK;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.OKCancel:
                    simpleButton3.Visible = false;
                    simpleButton2.Text = "Ok";
                    simpleButton1.Text = "Batal";
                    RetVal = SentraSolutionFramework.xDialogResult.Cancel;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.RetryCancel:
                    simpleButton3.Visible = false;
                    simpleButton2.Text = "Ulangi";
                    simpleButton1.Text = "Batal";
                    RetVal = SentraSolutionFramework.xDialogResult.Cancel;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNo:
                    simpleButton3.Visible = false;
                    simpleButton2.Text = "Ya";
                    simpleButton1.Text = "Tidak";
                    RetVal = SentraSolutionFramework.xDialogResult.No;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNoCancel:
                    simpleButton3.Text = "Ya";
                    simpleButton2.Text = "Tidak";
                    simpleButton1.Text = "Batal";
                    RetVal = SentraSolutionFramework.xDialogResult.Cancel;
                    break;
            }
            if (!IsDataExist) return RetVal;

            if (Caption.Length == 0) Caption = "Daftar Data";
            Text = Caption;
            labelControl1.Text = Message;

            gridView1.BeginDataUpdate();
            try
            {
                if (FormatCols != null)
                    foreach (KeyValuePair<string, string> FormatCol in FormatCols)
                    {
                        GridColumn gc = gridView1.Columns[FormatCol.Key];
                        if (gc != null)
                        {
                            gc.DisplayFormat.FormatType = FormatType.Custom;
                            gc.DisplayFormat.FormatString = FormatCol.Value;
                        }
                    }

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
            ShowDialog();
            return RetVal;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            switch (_Buttons)
            {
                case SentraSolutionFramework.xMessageBoxButtons.AbortRetryIgnore:
                    RetVal = SentraSolutionFramework.xDialogResult.Abort;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNoCancel:
                    RetVal = SentraSolutionFramework.xDialogResult.Yes;
                    break;
            }
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            switch (_Buttons)
            {
                case SentraSolutionFramework.xMessageBoxButtons.AbortRetryIgnore:
                    RetVal = SentraSolutionFramework.xDialogResult.Retry;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.OKCancel:
                    RetVal = SentraSolutionFramework.xDialogResult.OK;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.RetryCancel:
                    RetVal = SentraSolutionFramework.xDialogResult.Retry;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNo:
                    RetVal = SentraSolutionFramework.xDialogResult.Yes;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNoCancel:
                    RetVal = SentraSolutionFramework.xDialogResult.No;
                    break;
            }
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            switch (_Buttons)
            {
                case SentraSolutionFramework.xMessageBoxButtons.AbortRetryIgnore:
                    RetVal = SentraSolutionFramework.xDialogResult.Ignore;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.OK:
                    RetVal = SentraSolutionFramework.xDialogResult.OK;
                    break;
                case SentraSolutionFramework.xMessageBoxButtons.YesNo:
                    RetVal = SentraSolutionFramework.xDialogResult.No;
                    break;
                default:
                    RetVal = SentraSolutionFramework.xDialogResult.Cancel;
                    break;
            }
            Close();
        }
    }
}