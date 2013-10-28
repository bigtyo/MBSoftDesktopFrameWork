using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSecurity;

namespace SentraWinFramework
{
    public partial class frmCancelEntity : XtraForm
    {
        private string AlasanBatal = string.Empty;

        public frmCancelEntity()
        {
            InitializeComponent();
        }

        public static string ShowForm(IWin32Window Owner, string DocName)
        {
            frmCancelEntity frm = new frmCancelEntity();
            frm.groupControl1.Text = "Pembatalan " + DocName;
            frm.labelControl3.Text = "Dibatalkan oleh " + BaseSecurity.CurrentLogin.CurrentUser;
            frm.ShowDialog(Owner);
            return frm.AlasanBatal;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (memoEdit1.Text.Length == 0)
            {
                XtraMessageBox.Show("Alasan Batal harus diisi !", "Error Pembatalan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = System.Windows.Forms.DialogResult.None;
                return;
            }
            AlasanBatal = memoEdit1.Text;
        }
    }
}