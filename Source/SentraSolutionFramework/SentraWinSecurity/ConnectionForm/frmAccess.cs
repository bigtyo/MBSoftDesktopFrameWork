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
using System.IO;
using SentraSolutionFramework;

namespace SentraWinSecurity.ConnectionForm
{
    public partial class frmAccess : XtraForm
    {
        public string ConnectionString = string.Empty;
        public string FolderLocation = string.Empty;

        public DialogResult ShowForm(string ConnectionString)
        {
            return ShowDialog();
        }

        public frmAccess()
        {
            InitializeComponent();
            buttonEdit3.Location = buttonEdit1.Location;
            buttonEdit3.Size = buttonEdit1.Size;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            buttonEdit2.Enabled = checkEdit1.Checked;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.SuspendLayout();
            try
            {
                if (radioGroup1.SelectedIndex == 0)
                {
                    labelControl1.Visible = false;
                    textEdit1.Visible = false;
                    checkEdit1.Visible = false;
                    buttonEdit2.Visible = false;
                    buttonEdit1.Visible = true;
                    buttonEdit3.Visible = false;
                    labelControl2.Text = "Nama Database";
                }
                else
                {
                    labelControl1.Visible = true;
                    textEdit1.Visible = true;
                    checkEdit1.Visible = true;
                    buttonEdit2.Visible = true;
                    buttonEdit1.Visible = false;
                    buttonEdit3.Visible = true;
                    labelControl2.Text = "Folder Database";
                }
            }
            finally
            {
                base.ResumeLayout();
            }
        }

        // Find Database
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog fld = new OpenFileDialog();
            fld.DefaultExt = "mdb";
            fld.Title = "Pilih File Database";
            fld.Filter = "Access Database (*.mdb)|*.mdb";
            if (fld.ShowDialog(this) == DialogResult.OK)
                buttonEdit1.Text = fld.FileName;
        }

        // Find Template
        private void buttonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog fld = new OpenFileDialog();
            fld.DefaultExt = "tmd";
            fld.Title = "Pilih Template Database Baru";
            fld.Filter = "Template Access Database (*.tad)|*.tad";
            if (fld.ShowDialog(this) == DialogResult.OK)
                buttonEdit2.Text = fld.FileName;
        }

        // Find Folder
        private void buttonEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog fld = new FolderBrowserDialog();
            fld.Description = "Pilih Folder tempat menyimpan Database Baru ";
            if (fld.ShowDialog() == DialogResult.OK)
                buttonEdit3.Text = fld.SelectedPath;
        }

        // Simpan
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == 0) //Database sudah ada
            {
                #region Akses Database yang sudah ada
                ConnectionString = AccessPersistance.CreateConnectionString(
                    BaseUtility.GetFolderName(buttonEdit1.Text),
                    BaseUtility.GetFileNameExt(buttonEdit1.Text));
                try
                {
                    DataPersistance dp = new AccessPersistance(ConnectionString, false, string.Empty);

                    try
                    {
                        if (!dp.Find.IsExists<AppVariable>(string.Concat(
                            "ModuleName='System' AND VarName='AppName' AND VarValue='",
                            BaseFramework.ProductName, "'")))
                            throw new ApplicationException();
                        FolderLocation = BaseUtility.GetFolderName(buttonEdit1.Text);
                    }
                    catch
                    {
                        XtraMessageBox.Show(string.Concat(
                            "Database yang dipilih bukan Database ",
                            BaseFramework.ProductName, " !"),
                            "Error Buka File Database",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        ConnectionString = string.Empty;
                        DialogResult = DialogResult.None;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        "Error Simpan Koneksi Database",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ConnectionString = string.Empty;
                    DialogResult = DialogResult.None;
                }
                #endregion
            }
            else // Database Baru
            {
                if (checkEdit1.Checked && buttonEdit2.Text.Length > 0)
                {
                    string SourceFile = buttonEdit2.Text;
                    string tmpFile = textEdit1.Text;

                    int i = tmpFile.LastIndexOf(".");
                    if (i > 0) tmpFile = tmpFile.Substring(0, i);
                    string DestFile = string.Concat(
                        buttonEdit3.Text, "\\", tmpFile, ".mdb");

                    if (File.Exists(DestFile))
                        throw new ApplicationException(string.Concat(
                            "File Database Tujuan (", DestFile,
                            ") sudah ada !"));

                    if (!File.Exists(SourceFile))
                        throw new ApplicationException(string.Concat(
                            "Database Template (", SourceFile,
                            ") tidak ditemukan !"));

                    File.Copy(SourceFile, DestFile);

                    #region Akses Database yang sudah ada
                    ConnectionString = AccessPersistance.CreateConnectionString(
                        BaseUtility.GetFolderName(DestFile),
                        BaseUtility.GetFileNameExt(DestFile));
                    try
                    {
                        DataPersistance dp = new AccessPersistance(ConnectionString, false, string.Empty);

                        try
                        {
                            if (!dp.Find.IsExists(string.Concat("select VarName FROM _System_AppVariable WHERE ModuleName='System' AND VarName='AppName' AND VarValue='",
                                BaseFramework.ProductName, "'")))
                                throw new ApplicationException();
                            FolderLocation = BaseUtility.GetFolderName(DestFile);
                        }
                        catch
                        {
                            XtraMessageBox.Show(string.Concat(
                                "Database yang dipilih bukan Database ",
                                BaseFramework.ProductName, " !"),
                                "Error Buka File Database",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            File.Delete(DestFile);
                            ConnectionString = string.Empty;
                            DialogResult = DialogResult.None;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                            "Error Simpan Koneksi Database",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ConnectionString = string.Empty;
                        DialogResult = DialogResult.None;
                    }
                    #endregion
                }
                else // Tanpa Template
                    try
                    {
                        string tmpFile = textEdit1.Text.Trim();
                        if (tmpFile.Length == 0) throw new ApplicationException("Nama Database tidak boleh kosong !");
                        int i = tmpFile.LastIndexOf(".");
                        if (i > 0) tmpFile = tmpFile.Substring(0, i);

                        FolderLocation = buttonEdit3.Text;

                        ConnectionString = AccessPersistance.CreateConnectionString(
                            FolderLocation, tmpFile);

                        DataPersistance dp = new AccessPersistance(ConnectionString, true, FolderLocation);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message,
                            "Error Simpan Koneksi Database",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string tmpFile = textEdit1.Text;
                        int i = tmpFile.LastIndexOf(".");
                        if (i > 0) tmpFile = tmpFile.Substring(0, i);

                        File.Delete(string.Concat(FolderLocation,
                            "\\", tmpFile, ".mdb"));
                        ConnectionString = string.Empty;
                        FolderLocation = string.Empty;
                        DialogResult = DialogResult.None;
                    }
            }
        }
    }
}