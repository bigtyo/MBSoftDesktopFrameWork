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
using SentraSolutionFramework.Persistance;
using DevExpress.XtraEditors.Controls;

namespace SentraWinSecurity
{
    public partial class frmSecurityDocument : XtraForm, IModuleAccessForm
    {
        Dictionary<string, List<string>> ListKey;
        ModuleAccess ma;
        bool _OnLoad;
        bool AllDocumentData;
        public frmSecurityDocument()
        {
            InitializeComponent();
        }

        private class Item
        {
            public bool Pilih = false;
            public string ItemName;

            public Item(bool Pilih, string ItemName)
            {
                this.Pilih = Pilih;
                this.ItemName = ItemName;
            }
        }

        private Dictionary<string, List<Item>> ListDataField =
            new Dictionary<string, List<Item>>();

        #region IFormSettingModuleAccess Members

        bool IModuleAccessForm.ShowDialog(ModuleAccess ma, 
            Dictionary<string, List<string>> ListKey, 
            ref bool AllDocumentData)
        {
            labelControl2.Text = string.Concat(ma.FolderName, "\\", 
                ma.ModuleName);
            this.ma = ma;
            _OnLoad = true;
            checkEdit5.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentView, false);
            checkEdit6.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportView, false);

            checkEdit1.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentNew, false);
            checkEdit2.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentEdit, false);
            checkEdit3.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentDelete, false);
            checkEdit7.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentPrint, false);
            checkEdit8.Checked = ma.GetVariable<bool>(
                SecurityVarName.DocumentDesignPrint, false);

            checkEdit12.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportPrint, false);
            checkEdit11.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportDesignPrint, false);
            checkEdit10.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportSave, false);
            checkEdit4.Checked = ma.GetVariable<bool>(
                SecurityVarName.ReportLayoutSave, false);
            _OnLoad = false;

            this.ListKey = ListKey;

            if (ma.ListDataField.Count == 0)
                xtraTabPage2.PageVisible = false;
            else
            {
                DataPersistance dp = BaseSecurity.CurrentLogin.Dp;
                checkEdit14.Checked = AllDocumentData;
                foreach (ModuleDataField mdf in ma.ListDataField)
                {
                    List<Item> ListItem = new List<Item>();
                    ListDataField.Add(mdf.DataFieldName, ListItem);
                    comboBoxEdit1.Properties.Items.Add(mdf.DataFieldName);

                    IDataReader rdr = dp.ExecuteReader(mdf.SqlQuery);
                    while (rdr.Read())
                    {
                        bool Pilih = false;
                        string DataKey = (string)rdr[0];
                        foreach (string key in ListKey[mdf.DataFieldName])
                            if (key == DataKey)
                            {
                                Pilih = true;
                                break;
                            }
                        ListItem.Add(new Item(Pilih, DataKey));
                    }
                    rdr.Close();
                }
                comboBoxEdit1.SelectedIndex = 0;
            }

            if (ShowDialog(BaseWinFramework.MdiParent) == DialogResult.OK)
            {
                AllDocumentData = this.AllDocumentData;
                return true;
            }
            else
                return false;
        }

        #endregion

        //Simpan
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ma.SetVariable<bool>(SecurityVarName.DocumentView, checkEdit5.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportView, checkEdit6.Checked);

            ma.SetVariable<bool>(SecurityVarName.DocumentNew, checkEdit1.Checked);
            ma.SetVariable<bool>(SecurityVarName.DocumentEdit, checkEdit2.Checked);
            ma.SetVariable<bool>(SecurityVarName.DocumentDelete, checkEdit3.Checked);
            ma.SetVariable<bool>(SecurityVarName.DocumentPrint, checkEdit7.Checked);
            ma.SetVariable<bool>(SecurityVarName.DocumentDesignPrint, checkEdit8.Checked);

            ma.SetVariable<bool>(SecurityVarName.ReportPrint, checkEdit12.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportDesignPrint, checkEdit11.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportSave, checkEdit10.Checked);
            ma.SetVariable<bool>(SecurityVarName.ReportLayoutSave, checkEdit4.Checked);

            string CurrentDataField = comboBoxEdit1.Text;
            AllDocumentData = checkEdit14.Checked;

            foreach (ModuleDataField mdf in ma.ListDataField)
            {
                ListKey[mdf.DataFieldName].Clear();
                if (mdf.DataFieldName == CurrentDataField)
                {
                    foreach (CheckedListBoxItem lbItem in checkedListBoxControl1.Items)
                        if (lbItem.CheckState == CheckState.Checked)
                            ListKey[mdf.DataFieldName].Add((string)lbItem.Value);
                }
                else
                {
                    List<Item> ListItem = ListDataField[mdf.DataFieldName];
                    foreach (Item Item in ListItem)
                        if (Item.Pilih) ListKey[mdf.DataFieldName].Add(Item.ItemName);
                }
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (!_OnLoad && ((CheckEdit)sender).Checked)
                checkEdit5.Checked = true;
        }

        private void checkEdit12_CheckedChanged(object sender, EventArgs e)
        {
            if (!_OnLoad && ((CheckEdit)sender).Checked)
                checkEdit6.Checked = true;
        }

        private void checkEdit9_CheckedChanged(object sender, EventArgs e)
        {
            bool Ischecked = checkEdit9.Checked;

            _OnLoad = true;
            checkEdit1.Checked = Ischecked;
            checkEdit2.Checked = Ischecked;
            checkEdit3.Checked = Ischecked;
            checkEdit5.Checked = Ischecked;
            checkEdit7.Checked = Ischecked;
            checkEdit8.Checked = Ischecked;
            _OnLoad = false;
        }

        private void checkEdit13_CheckedChanged(object sender, EventArgs e)
        {
            bool Ischecked = checkEdit13.Checked; 
            
            _OnLoad = true;
            checkEdit4.Checked = Ischecked;
            checkEdit6.Checked = Ischecked;
            checkEdit10.Checked = Ischecked;
            checkEdit11.Checked = Ischecked;
            checkEdit12.Checked = Ischecked;
            _OnLoad = false;
        }

        private List<Item> OldList;
        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (OldList != null)
            {
                int i = 0;
                foreach (CheckedListBoxItem lbItem in checkedListBoxControl1.Items)
                    OldList[i++].Pilih = lbItem.CheckState == CheckState.Checked;
            }
            
            OldList = ListDataField[comboBoxEdit1.Text];

            checkedListBoxControl1.Items.Clear();
            foreach (Item itm in OldList)
                checkedListBoxControl1.Items.Add(itm.ItemName, itm.Pilih);
        }

        private void checkEdit14_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxEdit1.Enabled = !checkEdit14.Checked;
            checkedListBoxControl1.Enabled = !checkEdit14.Checked;
        }
    }
}