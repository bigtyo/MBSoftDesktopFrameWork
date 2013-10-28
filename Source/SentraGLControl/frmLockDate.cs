using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework;

namespace SentraGL
{
    public partial class frmLockDate : XtraForm
    {
        public frmLockDate()
        {
            InitializeComponent();
            DateEdit1.DateTime = BaseFramework.TransDate.MinDate;
            DateEdit1.Properties.MinValue = BaseGL.SetingPerusahaan.TglMulaiSistemBaru;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                BaseFramework.TransDate.MinDate = DateEdit1.DateTime.Date;
                XtraMessageBox.Show("Penguncian Tgl Awal Transaksi Sukses !", "Penguncian Tgl Awal",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Penguncian Tgl Awal Transaksi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
        }
    }
}