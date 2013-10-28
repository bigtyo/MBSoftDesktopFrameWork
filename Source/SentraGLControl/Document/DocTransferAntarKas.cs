using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;

namespace SentraGL.Document
{
    public partial class DocTransferAntarKas : DocumentForm
    {
        public DocTransferAntarKas()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idKasAsalLookUpEdit, 
                "NoKasAsal");
            Navigator.SetLookupEditDisplayMember(idKasAsalLookUpEdit1,
                "NamaKasAsal");
            Navigator.SetLookupEditDisplayMember(idKasTujuanLookUpEdit,
                "NoKasTujuan");
            Navigator.SetLookupEditDisplayMember(idKasTujuanLookUpEdit1,
                "NamaKasTujuan");
        }
    }
}