using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraEditors;
using Examples01.Entity;

namespace Examples01
{
    public partial class frmPelanggan : XtraForm
    {
        private EntityNavigator<Pelanggan> PlgNavigator = 
            new EntityNavigator<Pelanggan>();

        public frmPelanggan()
        {
            InitializeComponent();

            pelangganBindingSource.DataSource = PlgNavigator.Entity;
            PlgNavigator.onDataMoving += delegate
                (MoveType MovingType, bool IsError)
            {
                if (!IsError)
                {
                    labelControl1.Text = "Edit Pelanggan";
                    simpleButton7.Enabled = true;
                    simpleButton8.Visible = true;
                }
                else
                    simpleButton1_Click(null, null);

                namaPelangganTextEdit.Focus();
            };
            simpleButton1_Click(null, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PlgNavigator.SetNew();
            simpleButton7.Enabled = false;
            simpleButton8.Visible = false;
            labelControl1.Text = "Pelanggan Baru";
            namaPelangganTextEdit.Focus();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PlgNavigator.MoveFirst();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            PlgNavigator.MovePrevious();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            PlgNavigator.MoveNext();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            PlgNavigator.MoveLast();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                PlgNavigator.Save(true);
                if (PlgNavigator.SaveType == SaveType.SaveUpdate)
                    XtraMessageBox.Show("Data Pelanggan sudah tersimpan !",
                        "Sukses Simpan Pelanggan", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                    PlgNavigator.SetNew();
                namaPelangganTextEdit.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Simpan Pelanggan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (PlgNavigator.SaveType == SaveType.SaveNew) return;
            if (XtraMessageBox.Show("Hapus Data Pelanggan ?",
                "Konfirmasi Hapus Pelanggan", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;
            try
            {
                if (PlgNavigator.Delete() == 0)
                    XtraMessageBox.Show("Data tidak dapat ditemukan !",
                        "Error Hapus Data", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                else
                    PlgNavigator.MoveNext();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Hapus Pelanggan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!PlgNavigator.Reload())
                    XtraMessageBox.Show("Data tidak dapat ditemukan !",
                        "Error Reload Data", MessageBoxButtons.OK,
                         MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Baca Pelanggan",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}