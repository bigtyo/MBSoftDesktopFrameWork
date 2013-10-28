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
using DevExpress.XtraEditors.Controls;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;

namespace SentraGL.Document
{
    public partial class DocPengeluaranPerintahBayar : DocumentForm
    {
        public DocPengeluaranPerintahBayar()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idAkunLookUpEdit,
                "NamaKas");
            ((PerintahBayar)Navigator.Entity).FormMode = 
                enFormPerintahBayar.PengeluaranUang;
            Navigator.onEntityAction += new SentraSolutionFramework.EntityAction(Navigator_onEntityAction);
            Navigator.onNewMode += new NewAction(Navigator_onNewMode);
        }

        void Navigator_onEntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode)
        {
            if (ActionMode == enEntityActionMode.AfterLoadFound)
                noPerintahBayarButtonEdit.Properties.Buttons[0].Enabled =
                    ((PerintahBayar)PerintahBayarBindingSource.DataSource)
                    .Status == enStatusPerintahBayar.BelumDibayar;
        }

        void Navigator_onNewMode(ref bool CallSetDefault)
        {
            noPerintahBayarButtonEdit.Properties.Buttons[0].Enabled = true;
        }

        private void noPerintahBayarButtonEdit_ButtonClick(
            object sender, ButtonPressedEventArgs e)
        {
            ((PerintahBayar)PerintahBayarBindingSource.DataSource)
                .PilihPerintahBayar();
        }
    }
}