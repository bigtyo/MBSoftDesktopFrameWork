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
using SentraUtility;
using SentraSolutionFramework;
using System.Reflection;

namespace SentraWinFramework
{
    internal partial class frmDocument : XtraForm
    {
        public Control EntityCtrl;

        private Form PrevForm;

        public frmDocument(Type EntityControlType)
        {
            InitializeComponent();

            EntityCtrl = (Control)BaseFactory
                .CreateInstance(EntityControlType);

            XtraForm Frm = EntityCtrl as XtraForm;

            if (Frm != null)
            {
                Frm.Dock = DockStyle.Fill;
                Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                Frm.TopLevel = false;
                Frm.Parent = panelControl1;
                if (Frm.WindowState == FormWindowState.Maximized)
                {
                    Frm.WindowState = FormWindowState.Normal;
                    WindowState = FormWindowState.Maximized;
                }
                Frm.Show();
            }
            else
                panelControl1.Controls.Add(EntityCtrl);

            EntityCtrl.Dock = DockStyle.Fill;
            EntityCtrl.TabIndex = 0;

            ((IEntityControl)EntityCtrl).InitNavigator(uiNavigator1);

            Type EntityType = uiNavigator1.BindingSource.DataSource.GetType();
            if (!EntityType.IsSubclassOf(typeof(ParentEntity)))
            {
                EntityType = ((Type)uiNavigator1.BindingSource.DataSource)
                    .UnderlyingSystemType;
                object DataSource = BaseFactory.CreateInstance(EntityType);
                uiNavigator1.BindingSource.DataSource = DataSource;
            }

            if (Frm != null && Frm.Text.Length > 0)
                Text = Frm.Text;
            else
                Text = BaseWinFramework.GetModuleName(EntityControlType);
            if (Text.Length == 0 && EntityType != null)
                Text = BaseUtility.SplitName(EntityType.Name);

            if (BaseWinFramework.MdiParent == null && ActiveForm != null &&
                !ActiveForm.Modal)
            {
                PrevForm = ActiveForm;
                PrevForm.Hide();
            }
        }

        private void frmDocument_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (PrevForm != null && !PrevForm.IsDisposed)
                PrevForm.Show();
        }
    }
}