using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraUtility.Expression;
using DevExpress.XtraEditors.Controls;

namespace SentraWinFramework.Report
{
    public partial class frmFunctionList : XtraForm
    {
        private Evaluator ev;

        public frmFunctionList()
        {
            InitializeComponent();
        }

        protected void ShowForm2(Evaluator ev)
        {
            ComboBoxItemCollection cols = comboBoxEdit1.Properties.Items;
            this.ev = ev;
            cols.Clear();

            if (ev.Variables.VarDictionary.Count > 0)
                cols.Add("[Variabel]");

            foreach (string str in ev.ObjValues.GetListObjectName())
                if (str.Length == 0)
                    cols.Add("[General]");
                else
                    cols.Add(str);

            if (cols.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;
            Show(BaseWinFramework._MdiParent);
        }

        private static frmFunctionList _frm;
        public static void ShowForm(Evaluator ev)
        {
            if (ev.ObjValues.Count == 0 && 
                ev.Variables.VarDictionary.Count == 0)
            {
                XtraMessageBox.Show("Laporan tidak memiliki Variabel/ Fungsi yang dapat ditampilkan !",
                    "Daftar Variabel/ Fungsi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_frm == null || _frm.IsDisposed)
                _frm = new frmFunctionList();
            _frm.ShowForm2(ev);
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            string strText = comboBoxEdit1.Text;
            if (strText == "[Variabel]")
                foreach (string strVar in ev.Variables.VarDictionary.Keys)
                    listBoxControl1.Items.Add(strVar);
            else
            {
                if (strText == "[General]") strText = string.Empty;
                List<string> ListFunction = ev.ObjValues.GetListFunction(strText);
                foreach (string FunctionId in ListFunction)
                    listBoxControl1.Items.Add(FunctionId);
            }
        }
    }
}