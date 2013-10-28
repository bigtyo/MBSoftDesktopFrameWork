using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;

namespace SentraGL.Master
{
    public partial class DocJenisPengeluaranKas : DocumentForm
    {
        public DocJenisPengeluaranKas()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idAkunLookUpEdit, 
                "NoAkun");
            Navigator.SetLookupEditDisplayMember(lookUpEdit1, 
                "NamaAkun");
        }
    }
}
