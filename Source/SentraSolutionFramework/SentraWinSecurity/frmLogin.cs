using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraUtility;
using SentraSolutionFramework;
using SentraSecurity;
using SentraWinFramework;
using SentraSolutionFramework.Persistance;
using System.Diagnostics;
using System.IO;

namespace SentraWinSecurity
{
    public partial class frmLogin : XtraForm
    {
        private XmlConfig xmlc = BaseWinFramework.LocalConfig;

        public frmLogin()
        {
            InitializeComponent();

            //XmlConfig TmpConfig = new XmlConfig(
            //    BaseWinFramework.GetLocalUserDataPath() + "\\MSSSetting.dta");
            //if (TmpConfig.GetAllKeys("Connection").Length == 0)
            //    try
            //    {
            //        File.Delete(BaseWinFramework.GetLocalUserDataPath() + "\\MSSSetting.dta");
            //        File.Copy(BaseWinFramework.GetLocalDataPath() + "\\Setting.dta",
            //            BaseWinFramework.GetLocalUserDataPath() + "\\MSSSetting.dta");
            //    }
            //    catch { }

            //xmlc = new XmlConfig(BaseWinFramework
            //            .GetLocalUserDataPath() + "\\MSSSetting.dta");

            labelControl3.Visible = BaseUtility.IsDebugMode;
            checkEdit1.Visible = BaseUtility.IsDebugMode;
            checkEdit2.Visible = BaseUtility.IsDebugMode;
            checkEdit2.Checked = BaseFramework.AutoUpdateMetaData;
            checkEdit3.Visible = !BaseUtility.IsDebugMode && BaseFramework.ConnectEventServer;
            checkEdit4.Visible = !BaseUtility.IsDebugMode && BaseFramework.ConnectEventServer;

            comboBoxEdit1.Properties.Items.AddRange(Connection.GetListConnection());

            string Section = BaseFramework.ProductName.Replace(" ", "_") + "_Login";
            string TmpStr = xmlc.ReadString(
                Section, "ConnectionName", string.Empty);
            if (TmpStr.Length == 0)
                comboBoxEdit1.SelectedIndex = 0;
            else if (comboBoxEdit1.Properties.Items.Contains(TmpStr))
                comboBoxEdit1.Text = TmpStr;
            else
                comboBoxEdit1.SelectedIndex = 0;

            textEdit1.Text = xmlc.ReadString(
                Section, "UserName", "Admin");

            if (BaseSecurity.LoginWithRole)
            {
                TmpStr = xmlc.ReadString(
                    Section, "RoleName", string.Empty);
                if (TmpStr.Length == 0)
                {
                    if (comboBoxEdit2.Properties.Items.Count > 0)
                        comboBoxEdit2.SelectedIndex = 0;
                }
                else if (comboBoxEdit2.Properties.Items.Contains(TmpStr))
                    comboBoxEdit2.Text = TmpStr;
            }
            else
            {
                labelControl4.Visible = false;
                comboBoxEdit2.Visible = false;
                Height -= 40;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string EngineName, ConnectionString, FolderLocation;
                bool AutoCreateDb;

                BaseFramework.UseEventServer = checkEdit3.Checked;
                BaseFramework.ShowWarningWhenLogin = checkEdit4.Checked
                    && !BaseUtility.IsDebugMode;

                BaseFramework.AutoClearMetaDataVersion = checkEdit1.Checked;
                if (BaseUtility.IsDebugMode)
                    BaseFramework.AutoUpdateMetaData = checkEdit2.Checked;

                Connection.GetConnectionData(comboBoxEdit1.Text,
                    out EngineName, out ConnectionString,
                    out AutoCreateDb, out FolderLocation);

                BaseFramework.DefaultDp = BaseFramework
                    .DpEngine.CreateDataPersistance(
                    EngineName, ConnectionString, AutoCreateDb, FolderLocation);

                if (BaseSecurity.CurrentLogin.Login(
                   comboBoxEdit1.Text, comboBoxEdit2.Text, textEdit1.Text,
                   textEdit2.Text))
                {
                    string Section = BaseFramework.ProductName.Replace(" ", "_") + "_Login";

                    xmlc.WriteString(Section, "ConnectionName", comboBoxEdit1.Text);
                    xmlc.WriteString(Section, "UserName", textEdit1.Text);

                    UserLog.CheckAutoClearLog();

                    if (BaseSecurity.LoginWithRole)
                        xmlc.WriteString(Section, "RoleName", comboBoxEdit2.Text);
                    xmlc.Save();
                    if (BaseSecurity.CurrentLogin.CurrentRole.Length == 0 &&
                        !BaseUtility.IsDebugMode)
                        XtraMessageBox.Show("Anda Login sebagai Admin, dengan hak akses penuh terhadap semua modul yang ada.\n" +
                            "Buat User dan Peran Baru untuk membatasi hak akses user yang masuk ke aplikasi !",
                            "Pemberitahuan Login Admin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Nama User, Peran dan Password tidak sesuai !",
                        "Login tidak berhasil", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Simpan Koneksi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
            }
       }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!BaseSecurity.LoginWithRole) return;

            string EngineName, ConnectionString, FolderLocation;
            bool AutoCreateDb;

            try
            {
                using (new FormWaitCursor())
                {
                    Connection.GetConnectionData(comboBoxEdit1.Text,
                        out EngineName, out ConnectionString,
                        out AutoCreateDb, out FolderLocation);

                    BaseFramework.DefaultDp = BaseFramework
                        .DpEngine.CreateDataPersistance(
                        EngineName, ConnectionString, AutoCreateDb, FolderLocation);

                    DataTable dtRole = BaseSecurity.CurrentLogin.GetListRole(false);

                    comboBoxEdit2.Properties.Items.Clear();
                    comboBoxEdit2.Text = string.Empty;

                    foreach (DataRow dr in dtRole.Rows)
                        comboBoxEdit2.Properties.Items.Add(dr[0]);

                    if (comboBoxEdit2.Properties.Items.Count > 0)
                        comboBoxEdit2.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Buat Koneksi Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}