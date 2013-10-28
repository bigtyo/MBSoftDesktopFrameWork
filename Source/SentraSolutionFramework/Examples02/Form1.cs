using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraEditors;
using Examples02.Entity;

namespace Examples02
{
    public partial class frmPelanggan : Form
    {
        EntityNavigator<Pelanggan> Plg = 
            new EntityNavigator<Pelanggan>();

        public frmPelanggan()
        {
            InitializeComponent();
            pelangganBindingSource.DataSource = Plg.Entity;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Plg.SetNew();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Plg.MoveFirst();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Plg.MovePrevious();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Plg.MoveNext();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Plg.MoveLast();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Plg.Delete();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {
                Plg.Save();
                if (Plg.SaveType == SaveType.SaveNew)
                    Plg.SetNew();
                else
                    XtraMessageBox.Show("Data Pelanggan sudah Tersimpan",
                        "Konfirmasi Simpan Pelanggan", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    "Error Simpan Pelanggan", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}