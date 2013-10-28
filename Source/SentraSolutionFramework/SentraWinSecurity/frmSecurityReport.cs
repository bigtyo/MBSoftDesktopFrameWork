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
    public partial class frmSecurityReport : XtraForm, IModuleAccessForm
    {
        ModuleAccess ma;
        bool _OnLoad;

        public frmSecurityReport()
        {
            InitializeComponent();
        }

        #region IFormSettingModuleAccess Members

        bool IModuleAccessForm.ShowDialog(ModuleAccess ma, 
            Dictionary<string, List<string>> ListKey, ref bool AllDocumentData)
        {
            labelControl2.Text = string.Concat(ma.FolderName, "\\",
             ma.ModuleName);
            this.ma = ma;
            _OnLoad = true;
            checkEdit3.Checked = AllDocumentData;
            checkEdit1.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportView, false);

            checkEdit12.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportPrint, false);
            checkEdit11.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportDesignPrint, false);
            checkEdit10.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportSave, false);
            checkEdit4.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportLayoutSave, false);
            _OnLoad = false;
            return ShowDialog(BaseWinFramework.MdiParent) == DialogResult.OK;
         }

        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ma.SetVariable<bool>(SecurityVarName.ReportView, checkEdit1.Checked);

            ma.SetVariable<bool>(SecurityVarName.ReportPrint, checkEdit12.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportDesignPrint, checkEdit11.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportSave, checkEdit10.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportLayoutSave, checkEdit4.Checked);

        }

        private void checkEdit12_CheckedChanged(object sender, EventArgs e)
        {
            if (!_OnLoad && ((CheckEdit)sender).Checked)
                checkEdit1.Checked = true;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            _OnLoad = true;
            bool Checked = checkEdit2.Checked;

            checkEdit1.Checked = Checked;
            checkEdit12.Checked = Checked;
            checkEdit11.Checked = Checked;
            checkEdit10.Checked = Checked;
            checkEdit4.Checked = Checked;
            _OnLoad = false;
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBoxControl1.Enabled = !checkEdit3.Checked;
        }
    }
}