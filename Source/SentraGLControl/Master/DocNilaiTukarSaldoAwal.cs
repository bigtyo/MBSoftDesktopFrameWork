using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;

namespace SentraGL.Master
{
    public partial class DocNilaiTukarSaldoAwal : DocumentForm
    {
        IUINavigator Navi;

        public DocNilaiTukarSaldoAwal()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navi = Navigator;
        }

        FieldParam ParamAkun = new FieldParam("0", DataType.VarChar);
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ParamAkun.Value = gridView1.GetFocusedRowCellValue(colKodeMataUang);
            if (ParamAkun.Value != null)
            {
                Navi.FindData("KodeMataUang=@0", ParamAkun);
                gridView1.Focus();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((NilaiTukarSaldoAwal)nilaiTukarSaldoAwalBindingSource
                .DataSource).ListNilaiTukarSaldoAwal.Refresh();
        }
    }
}