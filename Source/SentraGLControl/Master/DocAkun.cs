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
using SentraSolutionFramework.Persistance;
using DevExpress.XtraTreeList.Nodes;
using SentraSolutionFramework;

namespace SentraGL.Master
{
    public partial class DocAkun : DocumentForm
    {
        xTreeList Tree;

        public DocAkun()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(
                idIndukLookUpEdit, "NoInduk");
            Navigator.SetLookupEditDisplayMember(
                idIndukLookUpEdit1, "NamaInduk");
            Navigator.onFormModeChanged += new FormModeChanged(Navigator_onFormModeChanged);

            Tree = new xTreeList(BaseFramework.DefaultDp, Navigator, treeList1, 
                "NoAkun", string.Empty, "Posting");
       }

        void Navigator_onFormModeChanged(FormMode NewFormMode)
        {
            switch(NewFormMode) {
                case FormMode.FormAddNew:
                    groupControl1.Text = "Data Akun Baru";
                    break;
                case FormMode.FormEdit:
                    groupControl1.Text = "Edit Data Akun";
                    break;
                case FormMode.FormView:
                    groupControl1.Text = "Lihat Data Akun";
                    break;
                case FormMode.FormError:
                    groupControl1.Text = "Data Akun Error";
                    break;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Tree.Refresh();
        }

        private void treeList1_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e)
        {
            if ((bool)e.Node.GetValue(colPosting))
                e.NodeImageIndex = 2;
            else
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
        }
    }
}