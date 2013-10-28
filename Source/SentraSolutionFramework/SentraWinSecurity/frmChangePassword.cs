using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSecurity;

namespace SentraWinSecurity
{
    public partial class frmChangePassword : XtraForm
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text != textEdit3.Text)
            {
                XtraMessageBox.Show("Password Baru tidak sama dengan Konfirmasi Password Baru !",
                    "Error Cek Password Baru",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
                return;
            }

            try
            {
                if (!BaseSecurity.CurrentLogin.ChangePassword(textEdit1.Text,
                    textEdit2.Text))
                    throw new ApplicationException("Password Lama tidak sesuai !");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Ubah Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                textEdit1.Focus();
            }
        }
    }
}