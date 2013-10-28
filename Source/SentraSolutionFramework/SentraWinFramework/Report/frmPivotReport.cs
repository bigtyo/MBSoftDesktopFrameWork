using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraUtility;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using DevExpress.XtraGrid.Columns;
using SentraWinFramework.Report;
using System.IO;
using DevExpress.XtraEditors.Controls;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.UI;
using SentraSolutionFramework;
using SentraUtility.Expression;

namespace SentraWinFramework.Report
{
    internal sealed partial class frmPivotReport : XtraForm, ISkinForm
    {
        private frmReportLayout RptDesigner;
        private DataPersistance _Dp;
        private DataPersistance Dp
        {
            get { return _Dp ?? BaseFramework.DefaultDataPersistance; }
            set { _Dp = value; }
        }

        Type _EntityType;
        Type _FormType;
        string _ReportName;
        TableDef td;
        private MemoryStream DefaultLayout;

        private IFilterForm _FilterForm;
        private IShowView _ShowViewForm;

        private string CurrPrintLayoutId;
        private string DataFilter;
        internal Evaluator _Evaluator;

        public void ShowForm(EntityForm EntityForm, string ReportName)
        {
        }

        public void ShowForm(XtraForm MdiParent, Type FormType,
            Type EntityType, DataPersistance dp, 
            Type FilterFormType, string DataFilter)
        {
            _Evaluator = new Evaluator();
            Dp = dp;
            this.DataFilter = DataFilter;
            comboBoxEdit1.Properties.Items.Add("(Layout Default)");

            if (EntityType != null)
            {
                _FormType = FormType;
                _EntityType = EntityType;

                td = MetaData.GetTableDef(EntityType);
                Dp.ValidateTableDef(td);
                if (td.fldTransactionDate != null)
                {
                    dateEdit1.DateTime = DateTime.Today;
                    dateEdit2.DateTime = DateTime.Today;
                }
                else
                {
                    label3.Visible = false;
                    label4.Visible = false;
                    dateEdit1.Visible = false;
                    dateEdit2.Visible = false;
                }
                _ReportName = BaseUtility.SplitName(_EntityType.Name);
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                dateEdit1.Visible = false;
                dateEdit2.Visible = false;
                if (FilterFormType != null)
                {
                    _ReportName = FilterFormType.Name.Substring(0, 3).ToLower();
                    if (_ReportName == "frm" || _ReportName == "rpt")
                        _ReportName = BaseUtility.SplitName(
                            FilterFormType.Name.Substring(3));
                    else
                        _ReportName = BaseUtility.SplitName(FilterFormType.Name);
                }
                else
                    _ReportName = "Bebas";
            }
            Text = "Laporan " + _ReportName;

            comboBoxEdit1.Properties.Items.AddRange(
                DocBrowseLayout.GetListLayout(_ReportName));
            
            this.MdiParent = MdiParent;

            if (FilterFormType == null)
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            else
            {
                _FilterForm = BaseFactory.CreateInstance(FilterFormType) as IFilterForm;
                if (_FilterForm == null)
                {
                    XtraMessageBox.Show("Form Filter harus implement Interface IFilterForm !",
                        "Error Filter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                XtraForm Frm = _FilterForm as XtraForm;
                if (Frm != null)
                {
                    Frm.FormBorderStyle = FormBorderStyle.None;
                    Frm.TopLevel = false;
                    Frm.Parent = xtraScrollableControl1;
                    splitContainerControl1.SplitterPosition = Frm.Height + 5;
                    Frm.KeyPreview = true;
                    Frm.KeyDown += new KeyEventHandler(frmGridReport_KeyDown);
                    xtraScrollableControl1.BackColor = Frm.BackColor;
                    Frm.Show();
                }
                else
                    splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
            }
            barButtonItem8_ItemClick(null, null);

            DefaultLayout = new MemoryStream();
            pivotGridControl1.SaveLayoutToStream(DefaultLayout);

            string CurrBrowseLayoutId = string.Empty;
            bool Tmp;
            DocDefault.GetDefaultLayout(_ReportName,
                out CurrBrowseLayoutId, out CurrPrintLayoutId, out Tmp);
            Show();
            comboBoxEdit1.SelectedItem = CurrBrowseLayoutId;
            if (comboBoxEdit1.SelectedIndex < 0)
                comboBoxEdit1.SelectedIndex = 0;
            textEdit1.Focus();
        }

        public frmPivotReport()
        {
            InitializeComponent();
        }

        // Desain Laporan
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new WaitCursor())
            {
                if (RptDesigner == null || RptDesigner.IsDisposed)
                {
                    RptDesigner = new frmReportLayout();
                    RptDesigner.ShowForm2(MdiParent, _ReportName, pivotGridControl1.DataSource, _Evaluator);
                }
                else
                    RptDesigner.BringToFront();
            }
        }

        #region Simpan Laporan
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Pivot2XLS(pivotGridControl1, string.Empty, _ReportName);
        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Pivot2HTML(pivotGridControl1, string.Empty, _ReportName);
        }
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Pivot2Text(pivotGridControl1, string.Empty, _ReportName);
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BaseWinFramework.WinForm.Grid.Pivot2Pdf(pivotGridControl1, string.Empty, _ReportName);
        }
        #endregion

        //Cetak
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    List<string> ListPrintLayout = DocPrintBrowseLayout.GetListLayout("L_" + _ReportName);

                    if (ListPrintLayout.Count == 0)
                    {
                        XtraMessageBox.Show("Layout Cetak Laporan tidak ditemukan !",
                            "Error Cetak Laporan",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string Layout;
                    if (ListPrintLayout.Count == 1)
                        Layout = ListPrintLayout[0];
                    else
                    {
                        Layout = frmCetak.ShowForm(ListPrintLayout, CurrPrintLayoutId, "Cetak Laporan");
                        if (Layout.Length == 0) return;
                    }
                    MemoryStream LayoutData;
                    DocPrintBrowseLayout.GetLayoutData("L_" + _ReportName, Layout, out LayoutData);

                    xReport Rpt = new xReport(_Evaluator);
                    Rpt.DataSource = pivotGridControl1.DataSource;
                    Rpt.LoadLayout(LayoutData);

                    new frmPreview().ShowForm(MdiParent, _ReportName, Rpt);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Cetak Laporan",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        //Refresh
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void dateEdit1_DateTimeChanged(object sender, EventArgs e)
        {
            if (pivotGridControl1.DataSource != null)
                barButtonItem8_ItemClick(null, null);
        }

        private void frmGridReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            switch (e.KeyCode)
            {
                case Keys.R:    //Refresh
                    barButtonItem8_ItemClick(null, null);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.P:    //Cetak
                    barButtonItem7_ItemClick(null, null);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.S:    //Simpan Layout
                    LayoutAction(1);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.B:    //Layout Baru
                    LayoutAction(2);
                    e.SuppressKeyPress = true;
                    break;
                case Keys.H:    //Hapus Layout
                    LayoutAction(3);
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                try
                {
                    if (comboBoxEdit1.SelectedIndex == 0)
                    {
                        DefaultLayout.Seek(0, SeekOrigin.Begin);
                        pivotGridControl1.RestoreLayoutFromStream(DefaultLayout);
                    }
                    else
                    {
                        MemoryStream ms;
                        string QueryFilter;
                        Dictionary<string, object> QueryFormFilter;
                        DocBrowseLayout.GetLayoutData(_ReportName,
                            comboBoxEdit1.Text, out ms, out QueryFilter,
                            out QueryFormFilter);
                        if (ms != null)
                            pivotGridControl1.RestoreLayoutFromStream(ms);
                        bool NeedRefresh = false;
                        if (QueryFilter.Length > 0)
                        {
                            textEdit1.Text = QueryFilter;
                            NeedRefresh = true;
                        }
                        if (_FilterForm != null && QueryFormFilter != null)
                        {
                            _FilterForm.FilterList = QueryFormFilter;
                            NeedRefresh = true;
                        }
                        if (NeedRefresh) barButtonItem8_ItemClick(null, null);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message,
                        "Error Baca Layout Laporan", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void comboBoxEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            LayoutAction(e.Button.Index);
        }

        private void LayoutAction(int ActionId)
        {
            using (new WaitCursor())
            {

                MemoryStream mst;
                Dictionary<string, object> DictFormFilter = null;
                int i = comboBoxEdit1.SelectedIndex;

                if (i == 0 && (ActionId == 1 || ActionId == 3))
                {
                    XtraMessageBox.Show("(Layout Default) tidak dapat disimpan/ dihapus !",
                        "Error Simpan Layout",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                switch (ActionId)
                {
                    case 1: //Simpan
                        if (XtraMessageBox.Show("Update Layout Laporan ?",
                            "Konfirmasi Update Layout Laporan", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No) return;

                        try
                        {
                            mst = new MemoryStream();
                            pivotGridControl1.SaveLayoutToStream(mst);
                            if (_FilterForm != null)
                                DictFormFilter = _FilterForm.FilterList;
                            DocBrowseLayout.SaveUpdateLayout(_ReportName,
                                comboBoxEdit1.Text, mst, textEdit1.Text, DictFormFilter);

                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Update Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2: //Tambah
                        frmSaveLayout frm = new frmSaveLayout();
                        if (frm.ShowDialog(this) == DialogResult.Cancel) return;

                        mst = new MemoryStream();
                        pivotGridControl1.SaveLayoutToStream(mst);
                        if (_FilterForm != null)
                            DictFormFilter = _FilterForm.FilterList;

                        try
                        {
                            DocBrowseLayout.SaveNewLayout(_ReportName,
                                frm.strText, mst, textEdit1.Text, DictFormFilter);
                            comboBoxEdit1.Properties.Items.Add(frm.strText);
                            comboBoxEdit1.SelectedIndex = comboBoxEdit1.Properties.Items.Count - 1;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Menambah Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 3: //Hapus
                        if (XtraMessageBox.Show("Hapus Layout Laporan ?",
                            "Konfirmasi Hapus Layout Laporan", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No) return;

                        try
                        {
                            DocBrowseLayout.DeleteLayout(_ReportName, comboBoxEdit1.Text);
                            int idx = comboBoxEdit1.SelectedIndex;
                            comboBoxEdit1.Properties.Items.RemoveAt(idx);
                            int Cnt = comboBoxEdit1.Properties.Items.Count;
                            if (idx < Cnt)
                                comboBoxEdit1.SelectedIndex = idx;
                            else
                                comboBoxEdit1.SelectedIndex = Cnt - 1;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Hapus Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
        }

        bool UsePrintPreview;

        // SetDefault
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSetDefault.ShowForm(_ReportName, ref UsePrintPreview);
        }

        #region ISkinFormChanged Members

        void ISkinForm.SkinChanged()
        {
            xtraScrollableControl1.BackColor = BackColor;
        }

        #endregion

        internal void ShowForm2(EntityForm EntityForm, 
            string ReportName, string FreeFilter, 
            object TransStartDate, object TransEndDate, 
            object[] Parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        internal void ShowForm3(string FreeFilter, 
            object TransStartDate, object TransEndDate, 
            object[] Parameters)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}