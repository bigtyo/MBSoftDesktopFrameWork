using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BxEventClient;
using SentraSecurity;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;

namespace ClientTest
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();

            BaseFramework.DefaultDp = new SqlServerPersistance("SV2003", "Barindo SDM", false, string.Empty, false, "sa", "Adm1n");
            BaseSecurity.CurrentLogin.Login("", "", "soewandi", "wd");
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Connect")
                eventClient1.ConnectServer(textBox1.Text, 
                    Convert.ToInt32(textBox2.Text), textBox3.Text, string.Empty);
            else
                eventClient1.DisconnectServer();
        }

        private void eventClient1_onConnected()
        {
            button1.Text = "Disconnect";
            label4.Text = "Terkoneksi";
            textBox3.ReadOnly = true;
        }

        private void eventClient1_onDisconnected()
        {
            button1.Text = "Connect";
            label4.Text = "Tidak Terkoneksi";
            textBox3.ReadOnly = false;
        }

        private void eventClient1_onConnectTimeOut()
        {
            label4.Text = "Tidak terhubung, server gagal dihubungi...";
        }

        private void eventClient1_onEventRaised(string EventName, object EventData)
        {
            listBox1.Items.Add(EventName);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            eventClient1.RegisterUserListener(textBox4.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            eventClient1.RaiseEvent(textBox4.Text, null);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            eventClient1.SendPopupMessage("Tes Pesan", textBox6.Text,
                (enMessageType)comboBox1.SelectedIndex, true,
                textBox5.Text.Split(','));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eventClient1.ShowWarningList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
