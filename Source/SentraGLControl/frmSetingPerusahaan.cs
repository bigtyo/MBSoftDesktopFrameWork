using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Entity;

namespace SentraGL
{
    public partial class frmSetingPerusahaan : DocumentForm
    {
        public frmSetingPerusahaan()
        {
            InitializeComponent();
            setingPerusahaanBindingSource.DataSource =
                BaseGL.SetingPerusahaan;
        }
    }
}
