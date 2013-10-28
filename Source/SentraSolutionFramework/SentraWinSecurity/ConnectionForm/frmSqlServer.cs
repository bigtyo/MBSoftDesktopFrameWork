using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Persistance;
using SentraUtility;
using SentraWinFramework;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using SentraSolutionFramework;

namespace SentraWinSecurity.ConnectionForm
{
    public partial class frmSqlServer : XtraForm
    {
        public string ConnectionString = string.Empty;
        public string FolderLocation = string.Empty;

        public DialogResult ShowForm(string ConnectionString)
        {
            using (new WaitCursor())
            {
                buttonEdit3.Location = comboBoxEdit1.Location;
                textEdit4.Location = comboBoxEdit1.Location;
                buttonEdit3.Size = comboBoxEdit1.Size;
                textEdit4.Size = comboBoxEdit1.Size;
                labelControl6.Location = checkEdit1.Location;

                if (ConnectionString.Length > 0)
                {
                    string TmpStr = BaseUtility
                        .GetValueFromConnectionString(
                        ConnectionString, "Integrated Security");

                    radioGroup1.SelectedIndex = string.Compare(TmpStr, "SSPI",
                        true) == 0 || string.Compare(TmpStr, "True", true) == 0 ?
                        0 : 1;
                    if (radioGroup1.SelectedIndex == 1)
                    {
                        textEdit2.Text = BaseUtility.GetValueFromConnectionString(
                            ConnectionString, "User ID");
                        textEdit3.Text = BaseUtility.GetValueFromConnectionString(
                            ConnectionString, "Password");
                    }
                    textEdit1.Text = BaseUtility.GetValueFromConnectionString(
                            ConnectionString, "Server");

                    List<string> ListConn = SqlServerPersistance
                         .GetListConnection(BaseFramework.ProductName,
                         textEdit1.Text, radioGroup1.SelectedIndex == 0,
                         textEdit2.Text, textEdit3.Text);
                    comboBoxEdit1.Properties.Items.Clear();
                    foreach (string Conn in ListConn)
                        comboBoxEdit1.Properties.Items.Add(Conn);

                    comboBoxEdit1.Text = BaseUtility.GetValueFromConnectionString(
                            ConnectionString, "Initial Catalog");
                }
            }
            return ShowDialog();
        }

        public frmSqlServer()
        {
            InitializeComponent();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0)
            {
                labelControl4.Enabled = false;
                labelControl5.Enabled = false;
                textEdit2.Enabled = false;
                textEdit3.Enabled = false;
            }
            else
            {
                labelControl4.Enabled = true;
                labelControl5.Enabled = true;
                textEdit2.Enabled = true;
                textEdit3.Enabled = true;
            }
        }

        private void comboBoxEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 1: //Refresh
                    using (new WaitCursor())
                    {
                        List<string> ListConn = SqlServerPersistance
                            .GetListConnection(BaseFramework.ProductName,
                            textEdit1.Text, radioGroup1.SelectedIndex == 0,
                            textEdit2.Text, textEdit3.Text);
                        comboBoxEdit1.Properties.Items.Clear();
                        foreach (string Conn in ListConn)
                            comboBoxEdit1.Properties.Items.Add(Conn);
                        break;
                    }
            }
        }

        private void CreateDatabase(string ServerName, string DatabaseName,
            bool Integrated, string UserName, string Password, string FolderLocation)
        {
            using (new WaitCursor())
            {
                string SqlCreate = string.Concat("CREATE DATABASE [",
                    DatabaseName, "] ON (NAME=[", DatabaseName,
                    "],FILENAME='", FolderLocation, @"\", DatabaseName,
                    ".mdf') LOG ON (NAME=[", DatabaseName,
                    "_Log],FILENAME='", FolderLocation, @"\", DatabaseName,
                    "_Log.ldf')");

                SqlConnection TmpCn = new SqlConnection(SqlServerPersistance.
                    CreateConnectionString(ServerName, "master",
                    Integrated, UserName, Password));
                TmpCn.Open();

                if (File.Exists(string.Concat(FolderLocation, @"\",
                    DatabaseName, ".mdf")))
                {
                    SqlCommand Cmd = new SqlCommand(SqlCreate +
                        " FOR ATTACH", TmpCn);
                    Cmd.ExecuteNonQuery();
                    TmpCn.Close();
                }
                else
                {
                    if (!Directory.Exists(FolderLocation))
                        Directory.CreateDirectory(FolderLocation);
                    SqlCommand Cmd = new SqlCommand(SqlCreate, TmpCn);
                    Cmd.ExecuteNonQuery();
                    TmpCn.Close();
                }
                int Ctr = 0;
                SqlConnection Cn;
            Ulang:
                try
                {
                    Cn = new SqlConnection(SqlServerPersistance
                        .CreateConnectionString(ServerName,
                        DatabaseName, Integrated, UserName, Password));
                    Cn.Open();
                }
                catch
                {
                    Ctr++;
                    Thread.Sleep(1000 * Ctr);
                    goto Ulang;  
                }
                Cn.Close();
            }
        }

        private bool RestoreDatabase(string ServerName, string DatabaseName,
            bool Integrated, string UserName, string Password, 
            string FolderLocation, string BackupFileName)
        {
            using (new WaitCursor())
            {
                DataPersistance dp = new SqlServerPersistance(ServerName,
                    "master", false, string.Empty, Integrated, UserName, Password);

                dp.ExecuteNonQuery(string.Concat(
                     "RESTORE VERIFYONLY FROM DISK='",
                     BackupFileName, "'"));

                string dbSourceFileName = string.Empty, 
                    dbSourceLogFileName = string.Empty,
                    dbDestFileName, dbDestLogFileName;
                string SqlCmd = string.Concat(
                    "RESTORE FILELISTONLY FROM DISK='",
                    BackupFileName, "'");

                IDataReader rdr = dp.ExecuteReader(SqlCmd);
                while (rdr.Read())
                    if ((string)rdr[2] == "D")
                        dbSourceFileName = (string)rdr[0];
                    else
                        dbSourceLogFileName = (string)rdr[0];
                rdr.Close();

                if (dbSourceFileName.Length == 0 ||
                    dbSourceLogFileName.Length == 0)
                    throw new ApplicationException("Database gagal dibaca !");

                dbDestFileName = string.Concat(FolderLocation,
                    "\\", DatabaseName, ".mdf");
                dbDestLogFileName = string.Concat(FolderLocation,
                    "\\", DatabaseName, "_log.ldf");

                SqlCmd = string.Concat(
                    "RESTORE DATABASE [", DatabaseName,
                    "] FROM DISK='", BackupFileName,
                    "' WITH FILE=1, MOVE '",
                    dbSourceFileName, "' TO '",
                    dbDestFileName, "', MOVE '",
                    dbSourceLogFileName, "' TO '",
                    dbDestLogFileName, "'");
                dp.ExecuteNonQuery(SqlCmd);

                string tmpStr = GetDbVar(dp, DatabaseName,
                    "System", "AppName");
                if (tmpStr != BaseFramework.ProductName)
                {
                    XtraMessageBox.Show("Database yang di attach bukan Database " + BaseFramework.ProductName,
                        "Error Attach Database", MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    dp.ExecuteNonQuery("EXEC sp_detach_db " +
                        dp.FormatSqlValue(
                        DatabaseName));
                    return false;
                }
                return true;
            }
        }

        //Simpan
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            switch (radioGroup2.SelectedIndex)
            {
                case 0: //Simpan
                    #region Simpan Koneksi Lama
                    using (new WaitCursor())
                    {
                        try
                        {
                            ConnectionString = SqlServerPersistance.CreateConnectionString(
                                textEdit1.Text, comboBoxEdit1.Text, radioGroup1.SelectedIndex == 0,
                                textEdit2.Text, textEdit3.Text);
                            DataPersistance dp = new SqlServerPersistance(ConnectionString, false, string.Empty);
                            string Data = (string)dp.Find.FirstValue(
                                "SELECT filename FROM master.dbo.sysdatabases WHERE name=@0",
                                string.Empty, string.Empty, new FieldParam("0", comboBoxEdit1.Text));
                            if (Data.Length > 0)
                                FolderLocation = BaseUtility.GetFolderName(Data);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                "Error Simpan Koneksi Database",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ConnectionString = string.Empty;
                            DialogResult = DialogResult.None;
                        }
                    }
                    #endregion
                    break;
                case 1: //Buat Baru dari Backup
                    #region Cek apakah server lokal
                    if (!CheckLocalServer())
                    {
                        XtraMessageBox.Show("Pembuatan database baru hanya dapat dilakukan di komputer server !",
                            "Error Pembuatan Database Baru", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Cek apakah database kosong
                    if (textEdit4.Text.Length == 0)
                    {
                        XtraMessageBox.Show("Nama Database tidak boleh kosong !",
                            "Error Pembuatan Database Baru",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Cek apakah Folder kosong
                    if (buttonEdit2.Text.Length == 0)
                    {
                        XtraMessageBox.Show("Nama Folder Penyimpanan Database tidak boleh kosong !",
                            "Error Pembuatan Database Baru", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion


                    #region Konfirmasi Buat Database Baru
                    if (XtraMessageBox.Show(string.Concat("Buat Database '",
                        textEdit4.Text, "' baru dari File Backup ?"), "Konfirmasi Buat Database Baru",
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    using (new WaitCursor())
                    {
                        try
                        {
                            if (!RestoreDatabase(textEdit1.Text, textEdit4.Text,
                                radioGroup1.SelectedIndex == 0,
                                textEdit2.Text, textEdit3.Text, buttonEdit2.Text,
                                buttonEdit1.Text)) return;

                            ConnectionString = SqlServerPersistance.CreateConnectionString(
                                textEdit1.Text, textEdit4.Text, radioGroup1.SelectedIndex == 0,
                                textEdit2.Text, textEdit3.Text);

                            FolderLocation = buttonEdit2.Text;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                "Error Buat Database Baru dari Backup",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DialogResult = DialogResult.None;
                        }
                    }
                    break;
                case 2: //Attach
                    #region #region Cek apakah server lokal
                    if (!CheckLocalServer())
                    {
                        XtraMessageBox.Show("Attach database hanya dapat dilakukan di komputer server !",
                            "Error Attach Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Konfirmasi Attach Database
                    if (XtraMessageBox.Show(string.Concat("Attach Database '",
                        buttonEdit3.Text, "' ?"), "Konfirmasi Attach Database",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    try
                    {
                        ConnectionString = SqlServerPersistance
                            .CreateConnectionString(
                            textEdit1.Text, BaseUtility.GetFileName(buttonEdit3.Text),
                            radioGroup1.SelectedIndex == 0,
                            textEdit2.Text, textEdit3.Text);

                        CreateDatabase(textEdit1.Text,
                            BaseUtility.GetFileName(buttonEdit3.Text),
                            radioGroup1.SelectedIndex == 0,
                            textEdit2.Text, textEdit3.Text,
                            BaseUtility.GetFolderName(buttonEdit3.Text));

                        SqlServerPersistance dp = new SqlServerPersistance(textEdit1.Text, "master",
                            false, string.Empty, radioGroup1.SelectedIndex == 0,
                            textEdit2.Text, textEdit3.Text);

                        string tmpStr = GetDbVar(dp, BaseUtility.GetFileName(buttonEdit3.Text),
                            "System", "AppName");
                        if (tmpStr != BaseFramework.ProductName)
                        {
                            XtraMessageBox.Show("Database yang di attach bukan Database " + BaseFramework.ProductName,
                                "Error Attach Database", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dp.ExecuteNonQuery("EXEC sp_detach_db " +
                                dp.FormatSqlValue(
                                BaseUtility.GetFileName(buttonEdit3.Text)));
                            ConnectionString = string.Empty;
                            DialogResult = DialogResult.None;
                            return;
                        }
                        FolderLocation = BaseUtility.GetFolderName(buttonEdit3.Text);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                            "Error Attach Database",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ConnectionString = string.Empty;
                        DialogResult = DialogResult.None;
                    }
                    break;
                case 3: //Buat Baru
                    #region Cek apakah server lokal
                    if (!CheckLocalServer())
                    {
                        XtraMessageBox.Show("Pembuatan database baru hanya dapat dilakukan di komputer server !",
                            "Error Pembuatan Database Baru", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Cek apakah database kosong
                    if (textEdit4.Text.Length == 0)
                    {
                        XtraMessageBox.Show("Nama Database tidak boleh kosong !",
                            "Error Pembuatan Database Baru", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Cek apakah Folder kosong
                    if (buttonEdit2.Text.Length == 0)
                    {
                        XtraMessageBox.Show("Nama Folder Penyimpanan Database tidak boleh kosong !",
                            "Error Pembuatan Database Baru", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    #region Konfirmasi Buat Database Baru
                    if (XtraMessageBox.Show(string.Concat("Buat Database '",
                        textEdit4.Text, "' baru ?"), "Konfirmasi Buat Database Baru",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        DialogResult = DialogResult.None;
                        return;
                    }
                    #endregion

                    using (new WaitCursor())
                    {
                        try
                        {
                            ConnectionString = SqlServerPersistance.CreateConnectionString(
                                textEdit1.Text, textEdit4.Text, radioGroup1.SelectedIndex == 0,
                                textEdit2.Text, textEdit3.Text);

                            FolderLocation = buttonEdit2.Text;
                            SqlServerPersistance dp;
                            if (checkEdit1.Checked &&
                                buttonEdit1.Text.Length > 0) // Bila ada Template
                            {
                                #region Copy Source To Dest
                                string SourceFile1 = buttonEdit1.Text;
                                string tmpFile = textEdit4.Text;
                                string DestFile1 = string.Concat(
                                    FolderLocation, "\\", tmpFile, ".mdf");
                                string SourceFile2 = SourceFile1.Substring(0,
                                    SourceFile1.Length - 4) + ".tml";
                                string DestFile2 = string.Concat(
                                    FolderLocation, "\\", tmpFile, "_Log.ldf");

                                if (File.Exists(DestFile1))
                                    throw new ApplicationException(string.Concat(
                                        "File Database Tujuan (", DestFile1,
                                        ") sudah ada !"));
                                if (File.Exists(DestFile2))
                                    throw new ApplicationException(string.Concat(
                                        "File Database Tujuan (", DestFile2,
                                        ") sudah ada !"));
                                if (!File.Exists(SourceFile1))
                                    throw new ApplicationException(string.Concat(
                                        "Database Template (", SourceFile1,
                                        ") tidak ditemukan !"));
                                if (!File.Exists(SourceFile2))
                                    throw new ApplicationException(string.Concat(
                                        "Database Log Template (", SourceFile2,
                                        ") tidak ditemukan !"));

                                File.Copy(SourceFile1, DestFile1);
                                File.Copy(SourceFile2, DestFile2);
                                #endregion

                                CreateDatabase(textEdit1.Text, tmpFile, radioGroup1.SelectedIndex == 0,
                                    textEdit2.Text, textEdit3.Text, FolderLocation);
                                dp = new SqlServerPersistance(textEdit1.Text, "master",
                                    false, string.Empty, radioGroup1.SelectedIndex == 0,
                                    textEdit2.Text, textEdit3.Text);
                                string AppName = GetDbVar(dp, textEdit4.Text, "System", "AppName");
                                if (!AppName.Equals(BaseFramework.ProductName))
                                {
                                    dp.ExecuteNonQuery("EXEC sp_detach_db " + dp.FormatSqlValue(tmpFile));
                                    File.Delete(DestFile1);
                                    File.Delete(DestFile2);
                                    throw new ApplicationException("Database template bukan template untuk Sistem " +
                                        BaseFramework.ProductName);
                                }
                            }
                            else
                            {
                                dp = new SqlServerPersistance(ConnectionString, true, FolderLocation);
                            }
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message,
                                "Error Pembuatan Database Baru",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ConnectionString = string.Empty;
                            FolderLocation = string.Empty;
                            DialogResult = DialogResult.None;
                        }
                    }
                    break;
            }
        }

        private string GetDbVar(DataPersistance dp, string dbName, 
            string ModuleName, string VarName)
        {
            try
            {
                return dp.Find.Value(string.Concat("SELECT VarValue FROM [", dbName, 
                    "].dbo._System_AppVariable WHERE ModuleName=@0 AND VarName=@1"), string.Empty,
                    new FieldParam("0", ModuleName),
                    new FieldParam("1", VarName)).ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        private bool CheckLocalServer()
        {
            string TmpServer = textEdit1.Text;
            int i = TmpServer.IndexOf('\\');
            if (i >= 0) TmpServer = TmpServer.Substring(0, i);

            if (TmpServer.Length == 0 || TmpServer.Equals(".") ||
                TmpServer.Equals("(local)", StringComparison.OrdinalIgnoreCase) ||
                TmpServer.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                TmpServer.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
                return true;

            XtraMessageBox.Show("Hanya komputer server yang dapat membuat database baru !",
                "Error Pembuatan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup2.SelectedIndex)
            {
                case 0: //Database yg ada
                    comboBoxEdit1.Visible = true;
                    buttonEdit3.Visible = false;
                    textEdit4.Visible = false;

                    checkEdit1.Visible = false;
                    labelControl7.Visible = false;
                    buttonEdit1.Visible = false;
                    buttonEdit2.Visible = false;
                    simpleButton1.Text = "Simpan";
                    labelControl2.Text = "Nama Database";
                    labelControl6.Visible = false;
                    break;
                case 1: //Baru dari Backup
                    comboBoxEdit1.Visible = false;
                    buttonEdit3.Visible = false;
                    textEdit4.Visible = true;

                    checkEdit1.Visible = false;
                    labelControl7.Visible = true;
                    buttonEdit1.Visible = true;
                    buttonEdit1.Enabled = true;
                    buttonEdit2.Visible = true;
                    simpleButton1.Text = "Buat Baru";
                    labelControl2.Text = "Nama Database";
                    labelControl6.Visible = true;
                    break;
                case 2: //Attach
                    comboBoxEdit1.Visible = false;
                    buttonEdit3.Visible = true;
                    textEdit4.Visible = false;

                    checkEdit1.Visible = false;
                    labelControl7.Visible = false;
                    buttonEdit1.Visible = false;
                    buttonEdit2.Visible = false;
                    simpleButton1.Text = "Attach Db";
                    labelControl2.Text = "Nama File Database";
                    labelControl6.Visible = false;
                    break;
                case 3: //Database Baru
                    comboBoxEdit1.Visible = false;
                    buttonEdit3.Visible = false;
                    textEdit4.Visible = true;

                    checkEdit1.Visible = true;
                    checkEdit1_CheckedChanged(null, null);
                    labelControl7.Visible = true;
                    buttonEdit1.Visible = true;
                    buttonEdit2.Visible = true;
                    simpleButton1.Text = "Buat Baru";
                    labelControl2.Text = "Nama Database";
                    labelControl6.Visible = false;
                    break;
            }
        }

        //Folder Database
        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!CheckLocalServer()) return;

            FolderBrowserDialog fld = new FolderBrowserDialog();
            fld.Description = "Pilih Folder tempat menyimpan Database Baru ";
            if (fld.ShowDialog() == DialogResult.OK)
                buttonEdit2.Text = fld.SelectedPath;
        }

        //Template/ Restore Database
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!CheckLocalServer()) return;

            OpenFileDialog fld = new OpenFileDialog();
            if (radioGroup2.SelectedIndex == 3)
            {
                fld.DefaultExt = "tmd";
                fld.Title = "Pilih Template Database Baru";
                fld.Filter = "Template Sql Server Database (*.tmd)|*.tmd";
            }
            else
            {
                fld.DefaultExt = "bak";
                fld.Title = "Pilih File Backup Database";
                fld.Filter = "Backup Sql Server Database (*.bak)|*.bak";
            }
            if (fld.ShowDialog(this) == DialogResult.OK)
                buttonEdit1.Text = fld.FileName;
        }

        //Attach ke File Database
        private void buttonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!CheckLocalServer()) return;

            OpenFileDialog fld = new OpenFileDialog();
            fld.DefaultExt = "mdf";
            fld.Title = "Pilih Database yang akan di Attach";
            fld.Filter = "Sql Server Database (*.mdf)|*.mdf";
            if (fld.ShowDialog(this) == DialogResult.OK)
                buttonEdit3.Text = fld.FileName;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            buttonEdit1.Enabled = checkEdit1.Checked;
        }
    }
}