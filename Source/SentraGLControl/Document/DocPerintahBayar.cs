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
    public partial class DocPerintahBayar : DocumentForm
    {
        public DocPerintahBayar()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idAkunLookUpEdit, 
                "NamaKas");
            ((PerintahBayar)Navigator.Entity).FormMode = 
                enFormPerintahBayar.PerintahBayar;
        }
    }
}