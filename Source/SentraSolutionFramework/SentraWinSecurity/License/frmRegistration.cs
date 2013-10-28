using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSecurity.License;
using SentraUtility;
using SentraSolutionFramework.Persistance;
using SentraWinFramework;

namespace SentraWinSecurity.License
{
    public partial class frmRegistration : XtraForm
    {
        Registration reg;

        public frmRegistration()
        {
            InitializeComponent();
        }

        public void ShowForm(DataPersistance dp, string EngineName)
        {
            reg = new Registration(dp, EngineName);
            labelControl12.Text = EngineName;
            radioGroup1.SelectedIndex = reg.Limitation;
            registrationBindingSource.DataSource = reg;
            modulesBindingSource.DataSource = reg.Modules;
            ShowDialog(BaseWinFramework.MdiParent);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                reg.Limitation = radioGroup1.SelectedIndex;
                if (reg.Save())
                {
                    XtraMessageBox.Show("Registrasi Sukses Dilakukan !",
                        "Konfirmasi Registrasi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Close();
                }
                else
                    XtraMessageBox.Show("Kode Aktivasi tidak sesuai !",
                        "Konfirmasi Registrasi", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Registrasi",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            spinEdit1.Enabled = (radioGroup1.SelectedIndex == 1) ;
        }

        int KeyCtr = 0;
        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt)
                switch (e.KeyCode)
                {
                    case Keys.A:
                        if (KeyCtr == 0)
                        {
                            KeyCtr++; 
                            e.Handled = true; 
                            e.SuppressKeyPress = true;
                        }
                        else KeyCtr = 0;
                        break;
                    case Keys.U:
                        if (KeyCtr == 1)
                        {
                            KeyCtr++;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else KeyCtr = 0;
                        break;
                    case Keys.T:
                        if (KeyCtr == 2)
                        {
                            KeyCtr++;
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        else KeyCtr = 0;
                        break;
                    case Keys.O:
                        if (KeyCtr == 3)
                        {
                            switch (radioGroup1.SelectedIndex)
                            {
                                case 0:
                                    textEdit4.Text = "DEMO/ LATIHAN";
                                    break;
                                case 1:
                                    textEdit4.Text =
                                        HardwareIdentification.Pack(
                                            string.Concat(
                                            labelControl10.Text,
                                            textEdit1.Text,
                                            textEdit2.Text,
                                            radioGroup1.SelectedIndex.ToString(),
                                            spinEdit1.Text,
                                            reg.EngineName));
                                    break;
                                case 2:
                                    textEdit4.Text =
                                        HardwareIdentification.Pack(
                                            string.Concat(
                                            labelControl10.Text,
                                            textEdit1.Text,
                                            textEdit2.Text,
                                            radioGroup1.SelectedIndex.ToString(),
                                            reg.EngineName));
                                    break;
                            }
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                        KeyCtr = 0;
                        break;
                    default:
                        KeyCtr = 0;
                        break;
                }
        }
    }
}