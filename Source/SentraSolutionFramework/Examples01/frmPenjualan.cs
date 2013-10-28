using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SentraSolutionFramework.Persistance;
using Examples01.Entity;
using SentraSolutionFramework;
using DevExpress.XtraEditors;

namespace Examples01
{
    public partial class frmPenjualan : XtraForm
    {
        public frmPenjualan()
        {
            InitializeComponent();

            Penjualan Penjualan = new Penjualan();

            penjualanBindingSource.DataSource = Penjualan;

            itemPenjualanBindingSource.DataSource = 
                Penjualan.ItemPenjualan;

            pelangganBindingSource.DataSource = 
                BaseFramework.DefaultDataPersistance
                .FastLoadEntities<Pelanggan>(
                "NoPelanggan,NamaPelanggan",
                "Aktif=true", "NoPelanggan");
            
            itemBindingSource.DataSource = 
                BaseFramework.DefaultDataPersistance
                .FastLoadEntities<Item>("KodeItem,NamaItem",
                "Aktif=true", "KodeItem");
        }
    }
}