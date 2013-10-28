using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraWinFramework;
using SentraGL;
using SentraGL.Master;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;

namespace SentraGL.Document
{
    public partial class frmJurnal : DocumentForm
    {
        public frmJurnal()
        {
            InitializeComponent();
        }

        public override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.onLockForm += new LockForm(Navigator_onLockForm);
        }

        void Navigator_onLockForm(bool IsLocked)
        {
            noDokSumberButtonEdit.Properties.Buttons[0].Enabled = IsLocked;
            noJurnalPembalikButtonEdit.Properties.Buttons[0].Enabled = IsLocked;
        }

        private void aturanJurnalLookUpEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                BaseWinFramework.SingleEntityForm
                    .ShowForm<frmAturanJurnal>();
        }

        private void jenisDokSumberLookUpEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                BaseWinFramework.SingleEntityForm
                    .ShowForm<frmJenisDokSumberJurnal>();
        }

        private void noDokSumberButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Jurnal Jr = (Jurnal)jurnalBindingSource.DataSource;
            if (!Jr.JurnalOtomatis) return;
            if (Jr.JenisDokSumber == "Jurnal Pembalik")
                BaseWinFramework.SingleEntityForm.ShowView(
                    BaseWinFramework.GetModuleName(GetType()),
                    "NoJurnal=" + Jr.DataPersistance
                    .FormatSqlValue(Jr.NoDokSumber, DataType.VarChar));
            else
                BaseWinFramework.SingleEntityForm.ShowViewWithKey(
                    Jr.JenisDokSumber, Jr.NoDokSumber);
        }

        private void noJurnalPembalikButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Jurnal Jurnal = ((Jurnal)jurnalBindingSource.DataSource);
            if (!Jurnal.BuatJurnalPembalik) return;

            BaseWinFramework.SingleEntityForm.ShowView<frmJurnal>(
                 string.Concat("NoJurnal=", Jurnal.FormatSqlValue(Jurnal.NoJurnalPembalik)));
        }
    }
}
