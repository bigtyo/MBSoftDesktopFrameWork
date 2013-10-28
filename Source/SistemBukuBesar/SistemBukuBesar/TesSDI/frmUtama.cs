using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraGL.Master;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;

namespace SistemBukuBesar.TesSDI
{
    public partial class frmUtama : XtraForm
    {
        public frmUtama()
        {
            InitializeComponent();

            BaseWinFramework.TouchScreenVersion = true;
            BaseFramework.DefaultDp = new SqlServerPersistance(".\\SqlExpress", "Buku Besar", false, string.Empty);
            BaseWinFramework.Init(null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            BaseWinFramework.SingleEntityForm.ShowForm<DocJenisPenerimaanKas>();
        }
    }
}