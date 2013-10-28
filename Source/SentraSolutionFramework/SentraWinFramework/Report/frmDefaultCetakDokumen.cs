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
    internal sealed partial class frmDefaultCetakDokumen : XtraForm
    {
        //string DocId;
        //string BrowseLayoutId;
        //string PrintLayoutId;
        //bool DefaultPrintOnSave;

        public bool DefaultUsePrintPreview;
        public bool DefaultCetakKetikaSimpan;

        DocDefault dd = new DocDefault();

        public frmDefaultCetakDokumen()
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
            try
            {
                DocDefault.UpdateDefaultLayout(dd.DocId, dd.DefaultBrowseLayoutId, dd.DefaultPrintLayoutId,
                    checkEdit1.Checked, radioGroup1.SelectedIndex == 0);
                DefaultUsePrintPreview = radioGroup1.SelectedIndex == 0;
                DefaultCetakKetikaSimpan = checkEdit1.Checked;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Update Default Pencetakan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool ShowForm(string DocId)
        {
            dd.DocId = DocId;
            dd.LoadEntity(true);
            //this.DocId = DocId;
            //DocDefault.GetDefaultLayout(DocId, out BrowseLayoutId, out PrintLayoutId,
            //    out DefaultPrintOnSave, out DefaultUsePrintPreview);
            radioGroup1.SelectedIndex = dd.DefaultUsePrintPreview ? 0 : 1;
            DefaultUsePrintPreview = dd.DefaultUsePrintPreview;
            checkEdit1.Checked = dd.DefaultPrintOnSave;
            return ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}