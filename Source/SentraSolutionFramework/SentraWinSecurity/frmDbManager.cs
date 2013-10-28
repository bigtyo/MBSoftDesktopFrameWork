using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework;
using SentraSolutionFramework.Persistance;
using SentraWinSecurity.ConnectionForm;
using SentraSecurity;
using SentraWinSecurity.License;
using SentraWinFramework;
using SentraUtility;
using SentraSecurity.License;
using System.IO;

namespace SentraWinSecurity
{
    public partial class frmDbManager : XtraForm
    {
        DataPersistance CurrDp;

        public frmDbManager()
        {
            InitializeComponent();

            // Isi List Koneksi
            listBoxControl1.Items.AddRange(Connection.GetListConnection());

            // Isi List EngineName
            foreach (string EngineName in BaseFramework
                .DpEngine.DictEngine.Keys)
                comboBoxEdit1.Properties.Items.Add(EngineName);
        }

        private void ValidateReg()
        {
            if (CurrDp != null)
            {
                Registration rg = new Registration(CurrDp, comboBoxEdit1.Text);
                switch (rg.Limitation)
                {
                    case 0: //Demo
                        labelControl6.Text = "Latihan/ Demo (Max 200 Transaksi)";
                        break;
                    case 1: //Month Limit
                        if (rg.IsValidActivationCode())
                            labelControl6.Text = string.Concat("Operasional ",
                                rg.MonthLimitation.ToString(), " bulan");
                        else
                            labelControl6.Text = "Registrasi Error !";
                        break;
                    case 2: //Unlimited
                        if (rg.IsValidActivationCode())
                            labelControl6.Text = "Operasional sudah diregistrasi";
                        else
                            labelControl6.Text = "Registrasi Error !";
                        break;
                }
            }
            else
                labelControl6.Text = "Latihan/ Demo (Max 200 Transaksi)";
        }

        private void frmDbManager_Shown(object sender, EventArgs e)
        {
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;
            if (listBoxControl1.Items.Count > 0)
                listBoxControl1.SelectedIndex = 0;
            else
                listBoxControl1_SelectedIndexChanged(null, null);
        }

        private string FolderLocation = string.Empty;
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                Type EngineType = BaseFramework.DpEngine.DictEngine[comboBoxEdit1.Text];
                if (EngineType.Equals(typeof(SqlServerPersistance)))
                {
                    frmSqlServer frm = new frmSqlServer();
                    if (frm.ShowForm(buttonEdit1.Text) == DialogResult.OK)
                    {
                        buttonEdit1.Text = frm.ConnectionString;
                        FolderLocation = frm.FolderLocation;
                    }
                }
                else if (EngineType.Equals(typeof(AccessPersistance)))
                {
                    frmAccess frm = new frmAccess();
                    if (frm.ShowForm(buttonEdit1.Text) == DialogResult.OK)
                    {
                        buttonEdit1.Text = frm.ConnectionString;
                        FolderLocation = frm.FolderLocation;
                    }
                }
            }
            catch { }

        }

        //Simpan
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Length == 0)
            {
                XtraMessageBox.Show("Nama Koneksi tidak boleh kosong !",
                    "Error Simpan Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (XtraMessageBox.Show(string.Concat("Simpan ",
                groupControl1.Text, " ?"), "Konfirmasi Simpan Koneksi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No) return;

            try
            {
                if (groupControl1.Text == "Koneksi Baru")
                {
                    Connection.AddConnection(textEdit1.Text, comboBoxEdit1.Text,
                        buttonEdit1.Text, FolderLocation.Length > 0, FolderLocation);
                    listBoxControl1.Items.Add(textEdit1.Text);
                    listBoxControl1.SelectedIndex = listBoxControl1.Items.Count - 1;
                }
                else
                {
                    Connection.EditConnection(listBoxControl1.Text, textEdit1.Text,
                        comboBoxEdit1.Text, buttonEdit1.Text, FolderLocation.Length > 0, FolderLocation);
                    listBoxControl1.Items[listBoxControl1.SelectedIndex] = textEdit1.Text;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Simpan Koneksi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedIndex > -1)
            {
                using (new FormWaitCursor())
                {
                    string EngineName;
                    string ConnectionString;
                    bool TmpBool;
                    string TmpStr;
                    Connection.GetConnectionData(listBoxControl1.Text, out EngineName,
                        out ConnectionString, out TmpBool, out TmpStr);
                    textEdit1.Text = listBoxControl1.Text;
                    groupControl1.Text = "Edit Koneksi: " + textEdit1.Text;
                    comboBoxEdit1.Text = EngineName;
                    buttonEdit1.Text = ConnectionString;
                    simpleButton2.Enabled = true;
                    simpleButton4.Enabled = true;
                    simpleButton5.Enabled = true;
                    comboBoxEdit1.Enabled = false;
                    try
                    {
                        CurrDp = BaseFramework.DpEngine.CreateDataPersistance(
                            comboBoxEdit1.Text, ConnectionString, TmpBool, TmpStr);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Error Buat Koneksi",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                groupControl1.Text = "Koneksi Baru";
                textEdit1.Text = string.Empty;
                buttonEdit1.Text = string.Empty;
                comboBoxEdit1.SelectedIndex = 0;
                simpleButton2.Enabled = false;
                simpleButton4.Enabled = false;
                simpleButton5.Enabled = false;
                comboBoxEdit1.Enabled = true;
                CurrDp = null;
            }
            ValidateReg();
            textEdit1.Focus();
        }

        //Baru
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (listBoxControl1.SelectedIndex != -1)
                listBoxControl1.SelectedIndex = -1;
            else
                listBoxControl1_SelectedIndexChanged(null, null);
        }

        //Hapus
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(string.Concat("Hapus Koneksi ",
                listBoxControl1.Text, " ?"), "Konfirmasi Hapus Koneksi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;

            try
            {
                Connection.RemoveConnection(listBoxControl1.Text);
                int i = listBoxControl1.SelectedIndex;
                listBoxControl1.Items.RemoveAt(i);
                if (i < listBoxControl1.Items.Count)
                    listBoxControl1.SelectedIndex = i;
                else
                    listBoxControl1.SelectedIndex = i - 1;
            } 
            catch(Exception ex) 
            {
                XtraMessageBox.Show(ex.Message, "Error Hapus Koneksi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Register
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new frmRegistration().ShowForm(CurrDp, comboBoxEdit1.Text);
            ValidateReg();
        }

        //Backup
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string LastBackupFolder = CurrDp.GetVariable<string>("System", 
                "LastBackupFolder", string.Empty);
            fbd.SelectedPath = LastBackupFolder;
            fbd.Description = "Pilih Folder yang digunakan untuk membackup Database :";
            if (fbd.ShowDialog() != DialogResult.OK) return;
            CurrDp.SetVariable("System", "LastBackupFolder", fbd.SelectedPath);

            Type EngineType = BaseFramework.DpEngine.DictEngine[comboBoxEdit1.Text];
            if (EngineType.Equals(typeof(SqlServerPersistance)))
            {
                string FileName = string.Concat(fbd.SelectedPath, "\\",
                    ((SqlServerPersistance)CurrDp).DatabaseName,
                    "_", DateTime.Today.ToString("yyyy_MM_dd"), ".bak");

                if (File.Exists(FileName))
                {
                    XtraMessageBox.Show(string.Concat(
                        "File Tujuan Backup '", FileName,
                        "' sudah ada. Hapus File tersebut untuk melanjutkan !"),
                        "Error Backup Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                try
                {
                    using (new FormWaitCursor())
                    {
                        CurrDp.ExecuteNonQuery(string.Concat(
                            "BACKUP DATABASE [",
                            ((SqlServerPersistance)CurrDp).DatabaseName,
                            "] TO DISK = '", FileName, "'"));
                    }
                    XtraMessageBox.Show(string.Concat(
                        "Backup Database ",
                        ((SqlServerPersistance)CurrDp).DatabaseName, 
                        " telah sukses dilakukan di File '",  FileName,
                        "' !"), "Konfirmasi Backup Database",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Backup Database",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                XtraMessageBox.Show("Backup untuk database " + comboBoxEdit1.Text + "belum didukung !",
                    "Konfirmasi Backup Database", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
        }
    }
}