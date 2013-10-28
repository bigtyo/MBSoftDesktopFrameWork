using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraUtility.Expression;
using SentraGL;
using SentraSolutionFramework;

namespace SentraGL.Master
{
    public partial class DocMataUang : DocumentForm
    {
        public DocMataUang()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(
                idAkunLabaRugiSelisihKursLookUpEdit,
                "NoAkunLabaRugiSelisihKurs");
            Navigator.SetLookupEditDisplayMember(lookUpEdit1,
                "NamaAkunLabaRugiSelisihKurs");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((MataUang)mataUangBindingSource.DataSource).ShowPilihJurnal();
        }
    }
}
