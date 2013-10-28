using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSecurity;
using SentraWinFramework;

namespace SentraWinSecurity
{
    public partial class frmSecurityForm : XtraForm, IModuleAccessForm
    {
        private ModuleAccess ma;

        public frmSecurityForm()
        {
            InitializeComponent();
        }

        #region IFormSettingModuleAccess Members

        bool IModuleAccessForm.ShowDialog(ModuleAccess ma,
            Dictionary<string, List<string>> ListKey, ref bool AllDocumentData)
        {
            labelControl2.Text = ma.ModuleName;
            this.ma = ma;
            checkEdit5.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentView, false);
            return ShowDialog(BaseWinFramework.MdiParent) == DialogResult.OK;
        }

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ma.SetVariable<bool>(SecurityVarName.DocumentView, 
                checkEdit5.Checked);
        }
    }
}