using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BxEventClient;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraSecurity;
using SentraWinFramework;

namespace UserClient
{
    public partial class frmMain : XtraForm
    {
        public frmMain()
        {
            InitializeComponent();

            BaseWinFramework.Init(this);
            BaseFramework.AutoUpdateMetaData = false;
            BaseFramework.UseEventServer = true;
            BaseSecurity.LoginWithRole = false;

            eventClient1.onConnected += new EventAction(eventClient1_onConnected);
            eventClient1.onDisconnected += new EventAction(eventClient1_onDisconnected);
            eventClient1.onConnectTimeOut += new EventAction(eventClient1_onConnectTimeOut);
            UpdateStatus(false);
        }

        void eventClient1_onConnectTimeOut()
        {
            XtraMessageBox.Show("Server tidak dapat dihubungi...",
                "Error Menghubungi Server", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void eventClient1_onDisconnected()
        {
            UpdateStatus(false);
        }

        void eventClient1_onConnected()
        {
            UpdateStatus(true);
            //WindowState = FormWindowState.Minimized;
        }

        private void UpdateStatus(bool IsLogin)
        {
            if (IsLogin)
            {
                textEdit1.Enabled = false;
                textEdit2.Enabled = false;
                textEdit3.Enabled = false;
                notifyIcon1.Text = string.Concat(
                    "User ", textEdit1.Text, " login on ",
                    textEdit3.Text);
                simpleButton1.Enabled = true;
                simpleButton2.Text = "Logout";
            }
            else
            {
                eventClient1.DisconnectServer();
                textEdit1.Enabled = true;
                textEdit2.Enabled = true;
                textEdit3.Enabled = true;
                notifyIcon1.Text = "User Logout.";
                simpleButton1.Enabled = false;
                simpleButton2.Text = "Login";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            eventClient1.ShowWarningList();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            //if (WindowState == FormWindowState.Minimized)
            //    Visible = false;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (simpleButton2.Text == "Login")
                {
                    BaseFramework.DefaultDp = new SqlServerPersistance(textEdit3.Text,
                        "Barindo SDM", false, string.Empty, false,
                        "sa", "Adm1n");

                    if (textEdit1.Text.ToLower() == "evy")
                        BaseFramework.ProductName = "Barindo Penjualan";
                    else
                        BaseFramework.ProductName = "Sistem Pengadaan Dan Persediaan";

                    BaseSecurity.CurrentLogin.Login(textEdit3.Text, 
                        string.Empty, textEdit1.Text, textEdit2.Text);
                }
                else
                {
                    BaseSecurity.CurrentLogin.Logout();
                    UpdateStatus(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Login", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            eventClient1.TestEntityChanged("SPP/0903/0155", "SuratPermintaanPembelian");
        }
    }
}