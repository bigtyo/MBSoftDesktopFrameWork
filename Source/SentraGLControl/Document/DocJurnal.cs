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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace SentraGL.Document
{
    public partial class DocJurnal : DocumentForm
    {
        public DocJurnal()
        {
            InitializeComponent();

        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.onLockForm += new LockForm(Navigator_onLockForm);
        }

        void Navigator_onLockForm(bool IsLocked)
        {
            noDokSumberButtonEdit.Properties.Buttons[0].Enabled = IsLocked;
            noJurnalPembalikButtonEdit.Properties.Buttons[0].Enabled = IsLocked;
        }

        private void aturanJurnalLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                BaseWinFramework.SingleEntityForm
                    .ShowForm<DocAturanJurnal>();
        }

        private void jenisDokSumberLookUpEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
                BaseWinFramework.SingleEntityForm
                    .ShowForm<DocJenisDokSumberJurnal>();
        }

        private void noDokSumberButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Jurnal Jr = (Jurnal)jurnalBindingSource.DataSource;
            if (!Jr.Internal) return;
            if (Jr.JenisDokSumber == "Jurnal Pembalik")
                BaseWinFramework.SingleEntityForm.ShowView(
                    BaseWinFramework.GetModuleName(GetType()),
                    "NoJurnal=" + Jr.FormatSqlValue(
                    Jr.NoDokSumber, DataType.VarChar));
            else
                BaseWinFramework.SingleEntityForm.ShowViewWithKey(
                    Jr.JenisDokSumber, Jr.NoDokSumber);
        }

        private void noJurnalPembalikButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Jurnal Jr = ((Jurnal)jurnalBindingSource.DataSource);
            if (!Jr.BuatJurnalPembalik) return;

            BaseWinFramework.SingleEntityForm.ShowView<DocJurnal>(
                 string.Concat("NoJurnal=", 
                 Jr.FormatSqlValue(Jr.NoJurnalPembalik)));
        }
    }
}
