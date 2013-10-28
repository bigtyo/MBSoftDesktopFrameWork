using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;

namespace Examples01
{
    public partial class frmStart : XtraForm
    {
        public frmStart()
        {
            InitializeComponent();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPenjualan Pnj = new frmPenjualan();
            Pnj.MdiParent = this;
            Pnj.Show();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPelanggan Plg = new frmPelanggan();
            Plg.MdiParent = this;
            Plg.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmItem Item = new frmItem();
            Item.MdiParent = this;
            Item.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.ShowChangeSkin(this);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.ShowReport(this, "Test");
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAkun frm = new frmAkun();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}