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
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using DevExpress.XtraGrid.Views.Base;

namespace SentraGL.Document
{
    public partial class DocKursHarian : DocumentForm
    {
        DataPersistance Dp = BaseFramework.DefaultDp;
        IUINavigator Navi;

        public DocKursHarian()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navi = Navigator;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ((KursHarian)kursHarianBindingSource
                .DataSource).ListKursHarian.Refresh();
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (gridView1.GetRowHandle(0) < 0) return;
            
            Navi.FindData("TglKurs=@0 AND KodeMataUang=@1",
                new FieldParam("0", ((KursHarian)Navi.Entity).TglKurs),
                new FieldParam("1", gridView1.GetFocusedRowCellValue(
                    colKodeMataUang)));
            gridView1.Focus();
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            gridView1_MouseUp(null, null);
        }
    }
}