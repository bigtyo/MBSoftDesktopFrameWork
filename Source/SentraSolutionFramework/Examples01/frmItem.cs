using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Persistance;
using Examples01.Entity;

namespace Examples01
{
    public partial class frmItem : XtraForm
    {
        public frmItem()
        {
            InitializeComponent();

            itemBindingSource.DataSource = new Item();
        }
   }
}