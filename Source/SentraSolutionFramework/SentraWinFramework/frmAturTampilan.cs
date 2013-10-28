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
    public partial class frmAturTampilan : XtraForm
    {
        private bool onInit;

        public frmAturTampilan()
        {
            InitializeComponent();
            onInit = true;
            radioGroup1.SelectedIndex = BaseWinFramework.Skin.SkinIndex;
            onInit = false;
        }

        public static void ShowForm(IWin32Window Owner)
        {
            new frmAturTampilan().Show(Owner);
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (onInit) return;

            using (new WaitCursor())
            {
                BaseWinFramework.Skin.SkinIndex = radioGroup1.SelectedIndex;
            }
        }

        private void frmAturTampilan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }
    }
}