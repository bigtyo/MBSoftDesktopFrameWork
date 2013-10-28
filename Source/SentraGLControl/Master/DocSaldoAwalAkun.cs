using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using DevExpress.XtraGrid.Views.Base;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SentraGL.Master
{
    public partial class DocSaldoAwalAkun : DocumentForm
    {
        IUINavigator Navi;

        public DocSaldoAwalAkun()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(
                idAkunLookUpEdit, "NoAkun");
            Navigator.SetLookupEditDisplayMember(
                idAkunLookUpEdit1, "NamaAkun");
            Navi = Navigator;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((SaldoAwalAkun)saldoAwalAkunBindingSource
                .DataSource).ListSaldoAwal.Refresh();
        }

        FieldParam ParamAkun = new FieldParam("0", DataType.VarChar);
        private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            ParamAkun.Value = gridView1.GetFocusedRowCellValue(colIdAkun);
            if (ParamAkun.Value != null)
            {
                Navi.FindData("IdAkun=@0", ParamAkun);
                gridView1.Focus();
            }
        }
    }
}