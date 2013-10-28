using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmCetak : XtraForm
    {
        public string CurrentLayout;
        public List<string> SelectedLayout;
        private static bool UsePreview;

        public frmCetak()
        {
            InitializeComponent();
            CurrentLayout = string.Empty;
        }

        public static bool ShowForm(List<string> Pilihan,
            List<string> SelectedLayout, string Caption, ref bool UsePreview)
        {
            frmCetak frm = new frmCetak();
            if (SelectedLayout.Count == 0)
                SelectedLayout.Add(Pilihan[0]);
            foreach (string Data in Pilihan)
                frm.checkedListBoxControl1.Items.Add(Data, 
                    SelectedLayout.Contains(Data));
            frm.Text = Caption;
            frm.SelectedLayout = SelectedLayout;
            frm.radioGroup1.SelectedIndex = UsePreview ? 0 : 1;

            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UsePreview = frmCetak.UsePreview;
                return true;
            }
            else
                return false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Cetak
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //CurrentLayout = comboBoxEdit1.Text;
            UsePreview = radioGroup1.SelectedIndex == 0;
            SelectedLayout.Clear();
            foreach (CheckedListBoxItem ci in checkedListBoxControl1.CheckedItems)
                SelectedLayout.Add((string)ci.Value);
            Close();
        }
    }
}