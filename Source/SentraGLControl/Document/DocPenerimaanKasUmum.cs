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
    public partial class DocPenerimaanKasUmum : DocumentForm
    {
        public DocPenerimaanKasUmum()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idAkunLookUpEdit, 
                "NamaKas");
            Navigator.SetGridLookupEditDisplayMember(gridView1,
                repositoryItemLookUpEdit2, "KodeDepartemen");
            Navigator.SetGridLookupEditDisplayMember(gridView1,
                repositoryItemLookUpEdit3, "KodeProyek");
        }

        private void noDokSumberButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        }
    }
}