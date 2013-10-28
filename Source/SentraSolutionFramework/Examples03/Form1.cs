using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SentraSolutionFramework.Persistance;

namespace Examples03
{
    public partial class Form1 : Form
    {
        EntityNavigator<Pelangggan> PlgNavigator =
            new EntityNavigator<Pelangggan>();

        public Form1()
        {
            InitializeComponent();
            pelanggganBindingSource.DataSource = PlgNavigator.Entity;
            uiNavigator1.Init(PlgNavigator);
        }
    }
}