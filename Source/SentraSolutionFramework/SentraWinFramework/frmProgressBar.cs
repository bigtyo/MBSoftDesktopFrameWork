using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SentraWinFramework
{
    public partial class frmProgressBar : XtraForm
    {
        public frmProgressBar()
        {
            InitializeComponent();
        }

        public void SetText(string Caption1, string Caption2)
        {
            UIThread.BeginInvoke(this, delegate()
            {
                labelControl1.Text = Caption1;
                labelControl2.Text = Caption2;
            });
        }

        public void CloseForm()
        {
            UIThread.BeginInvoke(this, delegate()
            {
                Close();
            });
        }
    }
}