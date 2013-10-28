using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Connect")
                eventClient1.ConnectServer(textBox1.Text,
                    Convert.ToInt32(textBox3.Text));
            else
                eventClient1.Disconnect();
        }

        private void eventClient1_onConnected()
        {
            label3.Text = "Server Connected";
            button1.Text = "Stop";
        }

        private void eventClient1_onDisconnected()
        {
            label3.Text = "Server Disconnected";
            button1.Text = "Connect";
        }

        private void eventClient1_onConnectTimeOut()
        {
            label3.Text = "Server tidak dapat dihubungi...";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            eventClient1.RegisterEvent(textBox2.Text);
        }

        private void eventClient1_onEventRaised(string EventName, object EventData)
        {
            listBox1.Items.Add("Raised " + EventName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            eventClient1.RaiseEvent(textBox2.Text, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            eventClient1.UnregisterEvent(textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
