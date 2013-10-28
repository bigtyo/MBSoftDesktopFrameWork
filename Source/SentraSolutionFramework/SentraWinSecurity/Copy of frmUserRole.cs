using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSecurity;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework.Entity;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using SentraUtility;
using SentraSolutionFramework;

namespace SentraWinSecurity
{
    public partial class frmUserRole : XtraForm
    {
        [TypeDescriptionProvider(typeof(EntityTDProvider))]
        private class RoleSelection
        {
            public bool Select;
            public string RoleName;

            public RoleSelection(string RoleName)
            {
                Select = false;
                this.RoleName = RoleName;
            }
        }
        [TypeDescriptionProvider(typeof(EntityTDProvider))]
        private class UserSelection
        {
            public bool Select;
            public string UserName;

            public UserSelection(string UserName)
            {
                Select = false;
                this.UserName= UserName;
            }
        }

        private List<RoleSelection> ListRole = new List<RoleSelection>();
        private List<UserSelection> ListUser = new List<UserSelection>();

        private class clsModule
        {
            public ModuleAccess Ma;
            public TreeListNode Node;
            public bool AllDocumentData;

            public Dictionary<string,List<string>> ListKey =
                new Dictionary<string,List<string>>();

            public clsModule(string ModuleName, TreeListNode Node)
            {
                ModuleAccess GlobalMa = BaseSecurity.GetModuleAccess(ModuleName);
                Ma = new ModuleAccess(ModuleName,
                    GlobalMa.FolderName, GlobalMa.FormSettingType);
                Ma.ListDataField = GlobalMa.ListDataField;
                
                this.Node = Node;
            }
        }

        private Dictionary<string, clsModule> ListModule = 
            new Dictionary<string, clsModule>();
        
        private string CurrentUser;
        private string CurrentRole;

        public frmUserRole()
        {
            InitializeComponent();

            treeList1.BeginUnboundLoad();
            try
            {
                object[] NodeValue = new object[2];

                SortedList<string, TreeListNode> CacheFolder = new SortedList<string,TreeListNode>();

                #region Update Action Access Module
                foreach (ModuleAccess ma in BaseSecurity.ModuleAccessList.Values)
                {
                    TreeListNode ParentNode;
                    NodeValue[1] = string.Empty;
                    if (!CacheFolder.TryGetValue(ma.FolderName, out ParentNode))
                    {
                        string[] TmpStr = ma.FolderName.Split('\\');
                                 ICollection NodeColl = treeList1.Nodes;
                        bool IsFound;
                        for (int i = 0; i < TmpStr.Length; i++)
                        {
                            IsFound = false;
                            foreach (TreeListNode nd in NodeColl)
                                if (nd.GetValue(treeListColumn1).Equals(TmpStr[i]))
                                {
                                    NodeColl = nd.Nodes;
                                    ParentNode = nd;
                                    IsFound = true;
                                    break;
                                }
                            if (!IsFound)
                            {
                                for (int j = i; j < TmpStr.Length; j++)
                                {
                                    NodeValue[0] = TmpStr[j];
                                    ParentNode = treeList1.AppendNode(NodeValue, ParentNode);
                                }
                                CacheFolder.Add(ma.FolderName, ParentNode);
                                break;
                            }
                        }
                    }

                    NodeValue[0] = ma.ModuleName;
                    TreeListNode ndx = treeList1.AppendNode(NodeValue, 
                        ParentNode);
                    ListModule.Add(ma.ModuleName, new clsModule(ma.ModuleName, ndx));
                }
                #endregion
            }
            finally
            {
                treeList1.EndUnboundLoad();
            }

            DataTable dt = BaseSecurity.CurrentLogin.GetListUser();
            OnAddUser = true;
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string TmpStr = (string)dr[0];
                    listBoxControl1.Items.Add(TmpStr);
                    ListUser.Add(new UserSelection(TmpStr));
                }
            }
            finally
            {
                OnAddUser = false;
            }
            OnAddRole = true;
            try
            {
                dt = BaseSecurity.CurrentLogin.GetListRole(true);
                foreach (DataRow dr in dt.Rows)
                {
                    string TmpStr = (string)dr[0];
                    listBoxControl2.Items.Add(TmpStr);
                    ListRole.Add(new RoleSelection(TmpStr));
                }
            }
            finally
            {
                OnAddRole = false;
            }
            PeranGrid.DataSource = ListRole;
            UserGrid.DataSource = ListUser;

            if (listBoxControl1.Items.Count == 0)
                simpleButton1_Click(null, null);
            else
            {
                listBoxControl1_SelectedIndexChanged(null, null);
            }

            if (listBoxControl2.Items.Count == 0)
                simpleButton4_Click(null, null);
            else
            {
                listBoxControl2_SelectedIndexChanged(null, null);
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                dateEdit1.Enabled = true;
                dateEdit2.Enabled = true;
                labelControl5.Enabled = true;
            }
            else
            {
                dateEdit1.Enabled = false;
                dateEdit2.Enabled = false;
                labelControl5.Enabled = false;
            }
        }

        private void checkEdit5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit5.Checked)
            {
                dateEdit4.Enabled = true;
                dateEdit3.Enabled = true;
                labelControl6.Enabled = true;
            }
            else
            {
                dateEdit4.Enabled = false;
                dateEdit3.Enabled = false;
                labelControl6.Enabled = false;
            }
        }

        // New User
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            groupControl1.Text = "User Baru :";
            textEdit1.Text = string.Empty;
            buttonEdit1.Text = string.Empty;
            checkEdit1.Checked = true;
            checkEdit2.Checked = true;
            checkEdit3.Checked = false;
            simpleButton2.Enabled = false;
            foreach (RoleSelection pr in ListRole)
                pr.Select = false;
            PeranGrid.RefreshDataSource();
            listBoxControl1.SelectedIndex = -1;
            CurrentUser = string.Empty;
            dateEdit1.DateTime = DateTime.Today;
            dateEdit2.DateTime = DateTime.Today;
            buttonEdit1.Properties.Buttons[0].Visible = false;
            buttonEdit1.Properties.ReadOnly = false;
            textEdit1.Focus();
        }

        //New Role
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            groupControl2.Text = "Peran Baru :";
            textEdit4.Text = string.Empty;
            checkEdit4.Checked = true;
            checkEdit5.Checked = false;
            simpleButton3.Enabled = false;
            foreach (UserSelection pu in ListUser)
                pu.Select = false;
            UserGrid.RefreshDataSource();
            listBoxControl2.SelectedIndex = -1;
            CurrentRole = string.Empty;
            dateEdit3.DateTime = DateTime.Today;
            dateEdit4.DateTime = DateTime.Today;
            foreach (clsModule mdl in ListModule.Values)
            {
                mdl.Ma.Variables.Clear();
                mdl.Node.SetValue(treeListColumn2, string.Empty);
            }

            textEdit4.Focus();
        }

        // Save User
        private bool OnAddUser = false;
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            textEdit1.Focus();
            try
            {
                using (EntityTransaction tr =
                    new EntityTransaction(BaseWinSecurity.DataPersistance))
                {
                    if (simpleButton2.Enabled == false) // User Baru
                    {
                        string UserName = textEdit1.Text;
                        BaseSecurity.CurrentLogin.Admin.User.Add(UserName,
                            buttonEdit1.Text, checkEdit1.Checked, checkEdit2.Checked,
                            checkEdit3.Checked, dateEdit1.DateTime, dateEdit2.DateTime);
                        
                        string SelRole = listBoxControl2.Text;
                        bool AddSelRole = false;

                        BaseSecurity.CurrentLogin.Admin.RoleUser.DeleteUser(UserName);
                        foreach (RoleSelection r in ListRole)
                            if (r.Select)
                            {
                                if (r.RoleName == SelRole)
                                    AddSelRole = true;
                                BaseSecurity.CurrentLogin.Admin.RoleUser.Add(
                                    r.RoleName, UserName);
                            }

                        UserSelection s = new UserSelection(UserName);
                        if (AddSelRole) s.Select = true;
                        ListUser.Add(s);
                        UserGrid.RefreshDataSource();
                        OnAddUser = true;
                        listBoxControl1.Items.Add(UserName);
                        OnAddUser = false;
                        simpleButton1_Click(null, null);
                    }
                    else // User Lama
                    {
                        string UserName = textEdit1.Text;
                        BaseSecurity.CurrentLogin.Admin.RoleUser.DeleteUser(CurrentUser);

                        BaseSecurity.CurrentLogin.Admin.User.Update(CurrentUser,
                            UserName, checkEdit1.Checked, checkEdit2.Checked,
                            checkEdit3.Checked, dateEdit1.DateTime, dateEdit2.DateTime);

                        string SelRole = listBoxControl2.Text;
                        bool UpdateSelRole = false;

                        foreach (RoleSelection r in ListRole)
                            if (r.Select)
                            {
                                if (r.RoleName == SelRole)
                                    UpdateSelRole = true;
                                BaseSecurity.CurrentLogin.Admin.RoleUser.Add(
                                    r.RoleName, UserName);
                            }

                        foreach (UserSelection s in ListUser)
                            if (s.UserName == CurrentUser)
                            {
                                s.Select = UpdateSelRole;
                                s.UserName = UserName;
                                break;
                            }
                        UserGrid.RefreshDataSource();

                        if (CurrentUser != UserName)
                            listBoxControl1.Items[listBoxControl1.SelectedIndex] = UserName;

                        CurrentUser = UserName;
                        textEdit1.Focus();
                    }
                    tr.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Simpan User",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Save Role
        private bool OnAddRole = false;
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (sender != null) textEdit4.Focus();
            try
            {
                using (EntityTransaction tr =
                    new EntityTransaction(BaseWinSecurity.DataPersistance))
                {
                    if (simpleButton3.Enabled == false) // Peran Baru
                    {
                        string RoleName = textEdit4.Text;
                        BaseSecurity.CurrentLogin.Admin.Role.Add(RoleName,
                            checkEdit4.Checked, checkEdit5.Checked,
                            dateEdit4.DateTime, dateEdit3.DateTime);

                        string SelUser = listBoxControl1.Text;
                        bool AddSelUser = false;

                        BaseSecurity.CurrentLogin.Admin.RoleUser.DeleteRole(RoleName);
                        foreach (UserSelection us in ListUser)
                            if (us.Select)
                            {
                                if (us.UserName == SelUser)
                                    AddSelUser = true;
                                BaseSecurity.CurrentLogin.Admin.RoleUser.Add(
                                    RoleName, us.UserName);
                            }
                        foreach (clsModule mdl in ListModule.Values)
                        {
                            BaseSecurity.CurrentLogin.Admin
                                .RoleModule.Add(RoleName,
                                mdl.Ma.ModuleName,
                                BaseUtility.Dictionary2String(
                                mdl.Ma.Variables), mdl.AllDocumentData);
                            foreach (ModuleDataField mdf in mdl.Ma.ListDataField)
                                BaseSecurity.CurrentLogin.Admin
                                    .RoleModule.UpdateDocumentVariable(
                                    RoleName, mdl.Ma.ModuleName,
                                    mdf.DataFieldName, mdf.DocumentName,
                                    mdl.ListKey[mdf.DataFieldName]);
                        }

                        RoleSelection rs = new RoleSelection(RoleName);
                        if (AddSelUser) rs.Select = true;
                        ListRole.Add(rs);
                        PeranGrid.RefreshDataSource();
                        OnAddRole = true;
                        try
                        {
                            listBoxControl2.Items.Add(RoleName);
                        }
                        finally
                        {
                            OnAddRole = false;
                        }
                        //Peran Baru...
                        if (sender != null) simpleButton4_Click(null, null);
                    }
                    else // Peran Lama
                    {
                        string RoleName = textEdit4.Text;
                        BaseSecurity.CurrentLogin.Admin.RoleUser.DeleteRole(CurrentRole);

                        BaseSecurity.CurrentLogin.Admin.Role.Update(CurrentRole,
                            RoleName, checkEdit4.Checked, checkEdit5.Checked,
                            dateEdit4.DateTime, dateEdit3.DateTime);

                        string SelUser = listBoxControl1.Text;
                        bool UpdateSelUser = false;

                        foreach (UserSelection us in ListUser)
                            if (us.Select)
                            {
                                if (us.UserName == SelUser)
                                    UpdateSelUser = true;
                                BaseSecurity.CurrentLogin.Admin.RoleUser.Add(
                                    RoleName, us.UserName);
                            }

                        foreach (RoleSelection rs in ListRole)
                            if (rs.RoleName == CurrentRole)
                            {
                                rs.Select = UpdateSelUser;
                                rs.RoleName = RoleName;
                                break;
                            }
                        foreach (clsModule mdl in ListModule.Values)
                        {
                            string newSecurityData = BaseUtility.Dictionary2String(
                                mdl.Ma.Variables);

                            BaseSecurity.CurrentLogin.Admin
                                .RoleModule.Update(RoleName,
                                mdl.Ma.ModuleName, newSecurityData, mdl.AllDocumentData);

                            foreach (ModuleDataField mdf in mdl.Ma.ListDataField)
                                BaseSecurity.CurrentLogin.Admin
                                    .RoleModule.UpdateDocumentVariable(
                                    RoleName, mdl.Ma.ModuleName,
                                    mdf.DataFieldName, mdf.DocumentName,
                                    mdl.ListKey[mdf.DataFieldName]);
                        }

                        PeranGrid.RefreshDataSource();

                        if (CurrentRole != RoleName)
                            listBoxControl2.Items[listBoxControl2.SelectedIndex] = RoleName;

                        CurrentRole = RoleName;
                        textEdit4.Focus();
                    }
                    tr.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Simpan Peran",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load User
        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnAddUser || listBoxControl1.SelectedIndex == -1) return;
            simpleButton2.Enabled = true;
            groupControl1.Text = "Edit User";
            User User = BaseSecurity.CurrentLogin.Admin.User
                .QueryUser(listBoxControl1.Text);
            if (User == null)
            {
                XtraMessageBox.Show(string.Concat("User ", listBoxControl1.Text,
                    " sudah dihapus dari database !"), "Error Baca User",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RemoveSelectedUser();
            }
            else
            {
                textEdit1.Text = User.UserName;
                checkEdit1.Checked = User.IsActive;
                checkEdit2.Checked = User.IsAdmin;
                checkEdit3.Checked = User.UseDateLimit;
                dateEdit1.DateTime = User.StartDate;
                dateEdit2.DateTime = User.EndDate;
                foreach (RoleSelection p in ListRole)
                    p.Select = User.ListRole.IndexOf(p.RoleName) >= 0;
                PeranGrid.RefreshDataSource();
                CurrentUser = User.UserName;
                buttonEdit1.Properties.ReadOnly = true;
                buttonEdit1.Properties.Buttons[0].Visible = true;
            }
        }

        private void RemoveSelectedUser()
        {
            if (listBoxControl1.SelectedIndex < 0) return;

            string UserName = listBoxControl1.Text;
            int idx = listBoxControl1.SelectedIndex;
            listBoxControl1.Items.RemoveAt(idx);

            for (int i = 0; i < ListUser.Count; i++)
                if (ListUser[i].UserName == UserName)
                {
                    ListUser.RemoveAt(i);
                    break;
                }

            int Cnt = listBoxControl1.Items.Count;
            if (idx < Cnt)
                listBoxControl1.SelectedIndex = idx;
            else if (Cnt > 0)
                listBoxControl1.SelectedIndex = Cnt - 1;
            else
                simpleButton1_Click(null, null);
        }
        private void RemoveSelectedRole()
        {
            if (listBoxControl2.SelectedIndex < 0) return;

            string RoleName = listBoxControl2.Text;
            int idx = listBoxControl2.SelectedIndex;
            listBoxControl2.Items.RemoveAt(idx);

            for (int i = 0; i < ListRole.Count; i++)
                if (ListRole[i].RoleName == RoleName)
                {
                    ListRole.RemoveAt(i);
                    break;
                }

            int Cnt = listBoxControl2.Items.Count;
            if (idx < Cnt)
                listBoxControl2.SelectedIndex = idx;
            else if (Cnt > 0)
                listBoxControl2.SelectedIndex = Cnt - 1;
            else
                simpleButton4_Click(null, null);
        }

        // Load Peran
        private void listBoxControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnAddRole || listBoxControl2.SelectedIndex == -1) return;

            simpleButton3.Enabled = true;
            groupControl2.Text = "Edit Peran";
            Role Role = BaseSecurity.CurrentLogin.Admin.Role
                .QueryRole(listBoxControl2.Text);
            if (Role == null)
            {
                XtraMessageBox.Show(string.Concat("Peran ", listBoxControl2.Text,
                    " sudah dihapus dari database !"), "Error Baca Peran",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RemoveSelectedRole();
            }
            else
            {
                textEdit4.Text = Role.RoleName;
                checkEdit4.Checked = Role.IsActive;
                checkEdit5.Checked = Role.UseDateLimit;
                dateEdit4.DateTime = Role.StartDate;
                dateEdit3.DateTime = Role.EndDate;
                foreach (UserSelection s in ListUser)
                    s.Select = Role.ListUser.IndexOf(s.UserName) >= 0;
                UserGrid.RefreshDataSource();
                CurrentRole = Role.RoleName;

                // Isi Variabel...
                IDataReader Rdr = BaseSecurity.CurrentLogin.Admin
                    .RoleModule.OpenDataReader(Role.RoleName);
                foreach (clsModule mdl in ListModule.Values)
                    mdl.Ma.Variables.Clear();

                try
                {
                    treeList1.BeginUpdate();
                    while (Rdr.Read())
                    {
                        string ModuleName = Rdr.GetString(0);
                        foreach (clsModule mdl in ListModule.Values)
                            if (mdl.Ma.ModuleName == ModuleName)
                            {
                                mdl.Ma.Variables = BaseUtility
                                    .String2Dictionary(Rdr.GetString(1));
                                mdl.Node.SetValue(treeListColumn2, mdl.Ma.ToString());
                                mdl.AllDocumentData = Rdr.GetBoolean(2);
                                break;
                            }
                    }
                }
                finally
                {
                    Rdr.Close();
                    treeList1.EndUpdate();
                }
                foreach (clsModule mdl in ListModule.Values)
                {
                    mdl.ListKey.Clear();
                    foreach (ModuleDataField mdf in mdl.Ma.ListDataField)
                    {
                        Rdr = BaseSecurity.CurrentLogin.Admin.RoleModule.GetListDocumentVariable(
                            Role.RoleName, mdl.Ma.ModuleName,
                            mdf.DataFieldName);
                        try
                        {
                            List<string> ListKey = new List<string>();
                            while (Rdr.Read())
                                ListKey.Add((string)Rdr[0]);
                            mdl.ListKey.Add(mdf.DataFieldName, ListKey);
                        }
                        finally
                        {
                            Rdr.Close();
                        }
                    }
                }
            }
        }
        
        // Delete User
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(string.Concat("Hapus User '", textEdit1.Text, "' ?"),
                "Konfirmasi Hapus User", MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question) == DialogResult.No) return;
            try
            {
                BaseSecurity.CurrentLogin.Admin.User.Delete(textEdit1.Text);
                RemoveSelectedUser();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Hapus User",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Delete Role
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(string.Concat("Hapus Peran '", textEdit4.Text, "' ?"),
                "Konfirmasi Hapus Peran", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;
            try
            {
                BaseSecurity.CurrentLogin.Admin.Role.Delete(textEdit4.Text);
                RemoveSelectedRole();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Hapus Peran",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUserRole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S: //Simpan
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                            simpleButton5_Click(null, null);
                        else
                            simpleButton6_Click(null, null);
                        break;
                    case Keys.F2: //Baru
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                            simpleButton1_Click(null, null);
                        else
                            simpleButton4_Click(null, null);
                        break;
                    case Keys.F8: //Hapus
                        if (xtraTabControl1.SelectedTabPageIndex == 0)
                        {
                            if (simpleButton2.Enabled)
                                simpleButton2_Click(null, null);
                        }
                        else
                        {
                            if (simpleButton3.Enabled)
                                simpleButton3_Click(null, null);
                        }
                        break;
                    default:
                        return;
                }
                e.SuppressKeyPress = true;
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            TreeListNode CurrentNode = treeList1.FocusedNode;
            clsModule mdl = ListModule[CurrentNode.GetDisplayText(
                treeListColumn1)];

            IModuleAccessForm ifs = (IModuleAccessForm)
                Activator.CreateInstance(mdl.Ma.FormSettingType);
            if (ifs.ShowDialog(mdl.Ma, mdl.ListKey, ref mdl.AllDocumentData))
            {
                CurrentNode.SetValue(treeListColumn2, mdl.Ma.ToString());
                if (simpleButton3.Enabled == false) // Peran Baru
                    simpleButton6_Click(null, null);
                else
                {
                    string newSecurityData = BaseUtility.Dictionary2String(
                        mdl.Ma.Variables);

                    BaseSecurity.CurrentLogin.Admin
                        .RoleModule.Update(textEdit4.Text,
                        mdl.Ma.ModuleName, newSecurityData, mdl.AllDocumentData);

                    foreach (ModuleDataField mdf in mdl.Ma.ListDataField)
                        BaseSecurity.CurrentLogin.Admin
                            .RoleModule.UpdateDocumentVariable(
                            textEdit4.Text, mdl.Ma.ModuleName,
                            mdf.DataFieldName, mdf.DocumentName,
                            mdl.ListKey[mdf.DataFieldName]);
                }
            }
        }

        private void treeList1_CustomNodeCellEdit(object sender, GetCustomNodeCellEditEventArgs e)
        {
            if (object.ReferenceEquals(e.Column, treeListColumn2) &&
                !e.Node.HasChildren)
                e.RepositoryItem = repositoryItemButtonEdit1;
        }

        private void treeList1_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            e.NodeImageIndex = e.Node.HasChildren ? 
                (e.Node.Expanded ? 0 : 1) : 2;
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren) 
                e.Cancel = true;
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (!treeList1.FocusedNode.HasChildren)
                repositoryItemButtonEdit1_ButtonClick(null, null);
        }

        //Reset Password
        private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (XtraMessageBox.Show(string.Concat(
                "Reset Password user: ", listBoxControl1.Text, " ?"),
                "Konfirmasi Reset Password", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No) return;
            try
            {
                BaseSecurity.CurrentLogin.Admin.User.ResetPassword(listBoxControl1.Text);
                XtraMessageBox.Show(string.Concat(
                    "Password user: ", listBoxControl1.Text, " telah direset !"),
                    "Reset Password", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,
                    "Error Reset Password", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
    }
}