using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SentraWinFramework;
using DevExpress.XtraEditors;
using SentraUtility.Expression;
using SentraGL;
using SentraSolutionFramework;
using SentraSolutionFramework.Entity;
using DevExpress.XtraTreeList.Nodes;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    public partial class DocProyek : DocumentForm
    {
        xTreeList Tree;

        public DocProyek()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idIndukLookUpEdit,
                "KodeInduk");
            Navigator.SetLookupEditDisplayMember(idIndukLookUpEdit1,
               "NamaInduk");
            Navigator.onFormModeChanged += new FormModeChanged(Navigator_onFormModeChanged);
            Tree = new xTreeList(BaseFramework.DefaultDp, Navigator, treeList1,
                "KodeProyek", string.Empty, "Posting");
        }

        void Navigator_onFormModeChanged(FormMode NewFormMode)
        {
            switch (NewFormMode)
            {
                case FormMode.FormAddNew:
                    groupControl1.Text = "Data Proyek Baru";
                    break;
                case FormMode.FormEdit:
                    groupControl1.Text = "Edit Data Proyek";
                    break;
                case FormMode.FormView:
                    groupControl1.Text = "Lihat Data Proyek";
                    break;
                case FormMode.FormError:
                    groupControl1.Text = "Data Proyek Error";
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
