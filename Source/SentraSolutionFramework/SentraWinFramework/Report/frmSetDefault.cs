using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmSetDefault : XtraForm
    {
        private string ReportName;
        private static bool UsePreview;

        public frmSetDefault()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Simpan
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            UsePreview = radioGroup1.SelectedIndex == 0;
            DocDefault.UpdateDefaultLayout(ReportName, comboBoxEdit1.Text,
                string.Empty, UsePreview);
        }

        public static void ShowForm(string ReportName, ref bool UsePrintPreview)
        {
            frmSetDefault frm = new frmSetDefault();
            frm.ReportName = ReportName;
            frm.Text = "Set Default Laporan " + ReportName;

            frm.comboBoxEdit1.Properties.Items.AddRange(
                DocBrowseLayout.GetListLayout(ReportName));

            frm.radioGroup1.SelectedIndex = UsePrintPreview ? 0 : 1;
            string DefBrowse;
            string DefPrint;
            bool Tmp;

            DocDefault.GetDefaultLayout(ReportName, out DefBrowse, 
                out DefPrint, out Tmp);
            
            try
            {
                frm.comboBoxEdit1.Text = DefBrowse;
            }
            catch { }

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UsePrintPreview = UsePreview;
        }
    }
}