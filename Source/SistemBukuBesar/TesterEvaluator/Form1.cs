using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SentraUtility.Expression;

namespace TesterEvaluator
{
    public partial class Form1 : Form
    {
        Evaluator ev = new Evaluator();

        public Form1()
        {
            InitializeComponent();
            clsTest t = new clsTest();
            ev.ObjValues.Add(string.Empty, t);
            ev.ObjValues.Add("x", t);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> ListFn = ev.ObjValues.GetListFunction(string.Empty);
            
            listBox1.Items.Clear();
            foreach (string str in ListFn)
                listBox1.Items.Add(str);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ev.Parse(textBox1.Text).ToString());
        }
    }
}
