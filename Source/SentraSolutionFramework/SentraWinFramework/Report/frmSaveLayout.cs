using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmSaveLayout : XtraForm
    {
        public string strText;

        public frmSaveLayout()
        {
            InitializeComponent();
            strText = string.Empty;
        }

        //Simpan
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show("Nama Layout tidak boleh kosong !",
                    "Error Simpan Layout", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = System.Windows.Forms.DialogResult.None;
            }
            else
            {
                strText = textEdit1.Text.Trim();
                Close();
            }
        }

        //Batal
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}