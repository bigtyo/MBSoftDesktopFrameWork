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
using DevExpress.XtraTreeList;
using SentraSolutionFramework.Entity;
using DevExpress.XtraTreeList.Nodes;
using SentraSolutionFramework.Persistance;

namespace SentraGL.Master
{
    public partial class DocDepartemen : DocumentForm
    {
        xTreeList Tree;

        public DocDepartemen()
        {
            InitializeComponent();
        }

        protected override void InitNavigator(IUINavigator Navigator)
        {
            Navigator.SetLookupEditDisplayMember(idIndukLookUpEdit,
                "KodeInduk");
            Navigator.SetLookupEditDisplayMember(idIndukLookUpEdit1,
               "NamaInduk");

            Tree = new xTreeList(BaseFramework.DefaultDp, Navigator, treeList1,
                "KodeDepartemen", string.Empty, "Posting");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Tree.Refresh();
        }

        private void treeList1_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if ((bool)e.Node.GetValue(colPosting))
            {
                if ((bool)e.Node.GetValue(colDepartemenProduksi))
                    e.NodeImageIndex = 3;
                else
                    e.NodeImageIndex = 2;
            }
            else
                e.NodeImageIndex = e.Node.Expanded ? 1 : 0;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ((Departemen)departemenBindingSource.DataSource).ShowAkun<DocTest>();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            MessageBox.Show("Hai");
        }
    }
}
