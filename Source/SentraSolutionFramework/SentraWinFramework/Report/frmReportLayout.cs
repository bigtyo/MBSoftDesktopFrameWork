using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.IO;
using SentraUtility.Expression;
using System.ComponentModel.Design;
using SentraSolutionFramework.Entity;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraBars;
using SentraSolutionFramework;
using DevExpress.Data;
using System.Drawing.Printing;
using DevExpress.XtraEditors.Repository;
using System.Drawing.Design;
using DevExpress.XtraReports.UserDesigner.Native;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Control;

namespace SentraWinFramework.Report
{
    public partial class frmReportLayout : XtraForm
    {
        private const string strTanpaLayout = "(Tanpa Layout)";

        internal int OldPreviewIndex;
        private MemoryStream msTanpaLayout;
        private string RptName;
        private string LayoutHeader;
        public Evaluator _Evaluator;
        private bool SaveEval;
        private object _DataSource;
        private IDesignerHost _DesignerHost;

        // Dari Dokumen
        public void ShowForm(Form MdiParent, string ReportName, 
            BusinessEntity DataSource, Evaluator Evaluator)
        {
            SaveEval = false;
            try
            {
                xrDesignPanel1.SetCommandVisibility(ReportCommand.AddNewDataSource, CommandVisibility.None);

                _Evaluator = Evaluator ?? BaseFactory
                    .CreateInstance<Evaluator>();
                LayoutHeader = "D_";
                IDataDictionary ds = new ReportSingleEntity(DataSource);

                Text = "Desain Cetak " + ReportName;
                xReport rpt = new xReport(_Evaluator);
                rpt.PaperKind = PaperKind.A4;
                rpt.DataSource = ds;

                RptName = ReportName;
                NewDesign(rpt);
                msTanpaLayout = new MemoryStream();
                rpt.SaveLayout(msTanpaLayout);

                if (MdiParent != null) this.MdiParent = MdiParent;

                List<string> ListLayout = DocPrintBrowseLayout
                    .GetListLayout(LayoutHeader + RptName);
                ((RepositoryItemComboBox)barEditItem3.Edit)
                    .Items.AddRange(ListLayout);
                barEditItem3.EditValue = strTanpaLayout;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Baca Layout Laporan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Show();
            }
        }

        // Dari Laporan Daftar
        public void ShowForm2(Form MdiParent, string ReportName, 
            object DataSource, Evaluator Evaluator)
        {
            SaveEval = false;
            try
            {
                xrDesignPanel1.SetCommandVisibility(ReportCommand.AddNewDataSource, CommandVisibility.None);
                _Evaluator = Evaluator ?? BaseFactory.CreateInstance<Evaluator>();
                LayoutHeader = "L_";
                Text = "Desain Cetak " + ReportName;
                xReport rpt = new xReport(Evaluator);
                rpt.PaperKind = PaperKind.A4;
                rpt.DataSource = DataSource;
                RptName = ReportName;
                NewDesign(rpt);
                msTanpaLayout = new MemoryStream();
                rpt.SaveLayout(msTanpaLayout);
                if (MdiParent != null) this.MdiParent = MdiParent;

                List<string> ListLayout = DocPrintBrowseLayout
                    .GetListLayout(LayoutHeader + ReportName);
                ((RepositoryItemComboBox)barEditItem3.Edit)
                    .Items.AddRange(ListLayout);
                barEditItem3.EditValue = strTanpaLayout;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Baca Layout Laporan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Show();
            }
        }

        // Dari Laporan Bebas
        public void ShowForm3(Form MdiParent, string ReportName, 
            Evaluator Evaluator)
        {
            SaveEval = true;
            try
            {
                fieldListDockPanel1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                xrDesignPanel1.SetCommandVisibility(ReportCommand.AddNewDataSource, CommandVisibility.None);
                _Evaluator = Evaluator ?? BaseFactory.CreateInstance<Evaluator>();
                LayoutHeader = "F_";
                Text = "Desain Cetak " + ReportName;
                xReport rpt = new xReport(Evaluator);
                rpt.PaperKind = PaperKind.A4;
                RptName = ReportName;
                NewDesign(rpt);
                msTanpaLayout = new MemoryStream();
                rpt.SaveLayout(msTanpaLayout);
                if (MdiParent != null) this.MdiParent = MdiParent;

                List<string> ListLayout = DocPrintBrowseLayout
                    .GetListLayout(LayoutHeader + ReportName);
                ((RepositoryItemComboBox)barEditItem3.Edit)
                    .Items.AddRange(ListLayout);
                barEditItem3.EditValue = strTanpaLayout;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Baca Layout Laporan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Show();
            }
        }

        // Dari Laporan Bebas pake DataSource
        public void ShowForm4(Form MdiParent, string ReportName, 
            object DataSource, Evaluator Evaluator)
        {
            SaveEval = true;
            try
            {
                xrDesignPanel1.SetCommandVisibility(ReportCommand.AddNewDataSource, CommandVisibility.None);
                _Evaluator = Evaluator ?? BaseFactory.CreateInstance<Evaluator>();
                LayoutHeader = "F_";
                Text = "Desain Cetak " + ReportName;

                xReport rpt = new xReport(Evaluator);
                rpt.PaperKind = PaperKind.A4;
                rpt.DataSource = DataSource;
                RptName = ReportName;

                NewDesign(rpt);

                msTanpaLayout = new MemoryStream();
                rpt.SaveLayout(msTanpaLayout);

                if (MdiParent != null) this.MdiParent = MdiParent;

                List<string> ListLayout = DocPrintBrowseLayout
                    .GetListLayout(LayoutHeader + ReportName);
                ((RepositoryItemComboBox)barEditItem3.Edit)
                    .Items.AddRange(ListLayout);
                barEditItem3.EditValue = strTanpaLayout;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error Baca Layout Laporan",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Show();
            }
        }

        void rpt_DesignerLoaded(object sender, DesignerLoadedEventArgs e)
        {
            _DesignerHost = e.DesignerHost;
            if (_DataSource == null)
                _DataSource = xrDesignPanel1.Report.DataSource;
            IToolboxService ts = (IToolboxService)e.DesignerHost
                .GetService(typeof(IToolboxService));
            ts.AddToolboxItem(new ToolboxItem(typeof(xrFunction)));
            ts.AddToolboxItem(new ToolboxItem(typeof(xrPictureVar)));

            IMenuCommandService ms = (IMenuCommandService)e.DesignerHost
                .GetService(typeof(IMenuCommandService));

            MenuCommand mnCmd = ms.FindCommand(UICommands.OpenFile);
            if (mnCmd != null) ms.RemoveCommand(mnCmd);
            ms.AddCommand(new MenuCommand(new EventHandler(OnOpenFile),
                UICommands.OpenFile));
            
            mnCmd = ms.FindCommand(UICommands.Closing);
            if (mnCmd != null) ms.RemoveCommand(mnCmd);
            ms.AddCommand(new MenuCommand(new EventHandler(OnCloseFile),
                UICommands.Closing));

            mnCmd = ms.FindCommand(UICommands.NewReport);
            if (mnCmd != null) ms.RemoveCommand(mnCmd);
            ms.AddCommand(new MenuCommand(new EventHandler(OnNewReport),
                UICommands.NewReport));

       }

        private void OnOpenFile(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Membuka File Laporan";
            ofd.Filter = "File Laporan (*.repx)|*.repx|Semua File (*.*)|*.*";
            ofd.DefaultExt = "repx";
            ofd.FileOk += new CancelEventHandler(ofd_FileOk);
            ofd.ShowDialog();
        }

        bool CancelClose = false;
        private void OnCloseFile(object sender, EventArgs e)
        {
            if (xrDesignPanel1.ReportState == ReportState.Changed)
            {
                switch (XtraMessageBox.Show("Layout telah berubah, simpan Perubahan ?",
                    "Konfirmasi Simpan Perubahan Layout", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        if (barEditItem3.EditValue.ToString() == strTanpaLayout)
                        {
                            XtraMessageBox.Show("(Tanpa Layout) tidak dapat disimpan !",
                                "Error Simpan Layout",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CancelClose = true;
                            return;
                        }

                        using (new WaitCursor())
                        {
                            try
                            {
                                MemoryStream mst = new MemoryStream();
                                xrDesignPanel1.Report.SaveLayout(mst);

                                if (SaveEval)
                                {
                                    Dictionary<string, object> Filter = new Dictionary<string, object>();

                                    if (_Evaluator != null)
                                        foreach (KeyValuePair<string, object> Var in _Evaluator.Variables.VarDictionary)
                                            Filter.Add(Var.Key, Var.Value);

                                    DocPrintBrowseLayout.SaveUpdateFreeLayout(LayoutHeader + RptName,
                                        barEditItem3.EditValue.ToString(), mst, Filter);
                                }
                                else
                                    DocPrintBrowseLayout.SaveUpdateLayout(LayoutHeader + RptName,
                                        barEditItem3.EditValue.ToString(), mst);
                                xrDesignPanel1.ReportState = ReportState.Opened;
                                xrDesignPanel1.FileName = string.Empty;
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show(ex.Message, "Error Update Layout",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CancelClose = true;
                                return;
                            }
                        }
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        CancelClose = true;
                        break;
                }
            }
        }

        void ofd_FileOk(object sender, CancelEventArgs e)
        {
            using (new WaitCursor())
            {
                OpenFileDialog ofd = sender as OpenFileDialog;

                try
                {
                    xReport Rpt = (xReport)xrDesignPanel1.Report;
                    Stream ms = ofd.OpenFile();
                    Rpt.LoadLayout(ms);
                    ms.Close();
                    Rpt.DataSource = xrDesignPanel1.Report.DataSource;
                    xrDesignPanel1.OpenReport(Rpt);
                    xrDesignPanel1.FileName = string.Empty;
                    xrDesignPanel1.Report.ScriptReferences = new string[] { GetType().Assembly.Location };
                    xrDesignPanel1.Report.DataSource = _DataSource;
                    fieldListDockPanel1.UpdateDataSource(_DesignerHost);
                    OriginalPrintControl = BaseWinFramework.mdiRibbonPrintController.PrintControl;
                    //xrDesignPanel1.Report.DataSource = null;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Membuka Laporan",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void NewDesign(xReport rpt)
        {
            xrDesignPanel1.FileName = string.Empty;
            rpt.Bands.Clear();
            rpt.Bands.Add(XtraReport.CreateBand(BandKind.Detail));
            xrDesignPanel1.OpenReport(rpt);
            xrDesignPanel1.Report.ScriptReferences = new string[] { GetType().Assembly.Location };
            xrDesignPanel1.ReportState = ReportState.Opened;
            OriginalPrintControl = BaseWinFramework.mdiRibbonPrintController.PrintControl;
        }

        public frmReportLayout()
        {
            InitializeComponent();

            BaseWinFramework.mdiRibbonPrintController.PrintControl = null;
            BaseWinFramework.mdiDesignRibbonController
                .XRDesignPanel = xrDesignPanel1;
        }

        private void frmReportLayout_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CancelClose)
            {
                CancelClose = false;
                e.Cancel = true;
            }
        }

        private void UpdateLayout(string NewLayout)
        {
            MemoryStream LayoutData;

            if (NewLayout == strTanpaLayout)
                LayoutData = msTanpaLayout;
            else
                DocPrintBrowseLayout.GetLayoutData(
                    LayoutHeader + RptName, NewLayout, out LayoutData);

            if (LayoutData == null) return;
            //xReport Rpt = new xReport(_Evaluator);
            xReport Rpt = (xReport)xrDesignPanel1.Report;
            Rpt.LoadLayout(LayoutData);
            Rpt.DataSource = _DataSource;
            xrDesignPanel1.Report.DataSource = _DataSource;
            fieldListDockPanel1.UpdateDataSource(_DesignerHost);
            //xrDesignPanel1.Report.DataSource = null;
            xrDesignPanel1.OpenReport(Rpt);
            xrDesignPanel1.FileName = string.Empty;
            xrDesignPanel1.Report.ScriptReferences = new string[] { GetType().Assembly.Location };
            OriginalPrintControl = BaseWinFramework.mdiRibbonPrintController.PrintControl;
        }

        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                UpdateLayout(barEditItem3.EditValue.ToString());
            }
        }

        private void xrDesignPanel1_SelectedTabIndexChanged(object sender, EventArgs e)
        {
            if (xrDesignPanel1.SelectedTabIndex == 1)
            {
                xrDesignPanel1.Report.DataSource = _DataSource;
                try
                {
                    fieldListDockPanel1.UpdateDataSource(_DesignerHost);
                }
                catch { }
                BaseWinFramework.Evaluator = _Evaluator;
            }
            OldPreviewIndex = xrDesignPanel1.SelectedTabIndex;
        }

        //Simpan Layout
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            string LayoutName = barEditItem3.EditValue.ToString();
            if (LayoutName == strTanpaLayout)
            {
                barButtonItem2_ItemClick(null, null);
                return;
            }

            if (XtraMessageBox.Show("Update Layout Cetak Laporan ?",
                "Konfirmasi Update Layout Cetak Laporan", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            using (new WaitCursor())
            {
                try
                {
                    MemoryStream mst = new MemoryStream();
                    xrDesignPanel1.Report.SaveLayout(mst);

                    if (SaveEval)
                    {
                        Dictionary<string, object> Filter = new Dictionary<string, object>();

                        if (_Evaluator != null)
                            foreach (KeyValuePair<string, object> Var in _Evaluator.Variables.VarDictionary)
                                Filter.Add(Var.Key, Var.Value);

                        DocPrintBrowseLayout.SaveUpdateFreeLayout(LayoutHeader + RptName,
                            LayoutName, mst, Filter);
                    }
                    else
                        DocPrintBrowseLayout.SaveUpdateLayout(LayoutHeader + RptName,
                            LayoutName, mst);
                    xrDesignPanel1.ReportState = ReportState.Opened;
                    xrDesignPanel1.FileName = string.Empty;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Update Layout",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        //Simpan Layout Baru
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmSaveLayout frm = new frmSaveLayout();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel) return;

            using (new WaitCursor())
            {
                MemoryStream mst = new MemoryStream();
                xrDesignPanel1.Report.SaveLayout(mst);

                try
                {
                    if (SaveEval)
                    {
                        Dictionary<string, object> Filter = new Dictionary<string, object>();

                        if (_Evaluator != null)
                            foreach (KeyValuePair<string, object> Var in _Evaluator.Variables.VarDictionary)
                                Filter.Add(Var.Key, Var.Value);

                        DocPrintBrowseLayout.SaveNewFreeLayout(LayoutHeader + RptName,
                            frm.strText, mst, Filter);
                    }
                    else
                        DocPrintBrowseLayout.SaveNewLayout(LayoutHeader + RptName,
                            frm.strText, mst);

                    ((RepositoryItemComboBox)barEditItem3.Edit).Items
                        .Add(frm.strText);

                    barEditItem3.EditValue = frm.strText;

                    xrDesignPanel1.ReportState = ReportState.Opened;
                    xrDesignPanel1.FileName = string.Empty;
                }
                finally { }
                //catch (Exception ex)
                //{
                //    XtraMessageBox.Show(ex.Message, "Error Menambah Layout",
                //        MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
        }

        //Hapus Layout
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            string LayoutName = barEditItem3.EditValue.ToString();
            if (LayoutName == strTanpaLayout)
            {
                XtraMessageBox.Show("(Tanpa Layout) tidak dapat dihapus !",
                    "Error Simpan Layout",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (XtraMessageBox.Show(string.Concat("Hapus Layout Laporan '", 
                LayoutName, "' ?"),
                "Konfirmasi Hapus Layout Laporan", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;

            using (new WaitCursor())
            {
                try
                {
                    DocPrintBrowseLayout.DeleteLayout(LayoutHeader + RptName, LayoutName);

                    RepositoryItemComboBox icb = (RepositoryItemComboBox)barEditItem3.Edit;
                    int Index = icb.Items.IndexOf(LayoutName);

                    icb.Items.RemoveAt(Index);
                    if (Index == icb.Items.Count) 
                        barEditItem3.EditValue = icb.Items[Index-1];
                    else
                        barEditItem3.EditValue = icb.Items[Index];

                    UpdateLayout(barEditItem3.EditValue.ToString());
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error Hapus Layout",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
         }

        // Field List Aktif
        //private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    xrDesignPanel1.Report.DataSource = _DataSource;
        //    fieldListDockPanel1.UpdateDataSource(_DesignerHost);
        //    xrDesignPanel1.Report.DataSource = null;
        //}

        //private void xrDesignPanel1_ComponentAdding(object sender, ComponentEventArgs e)
        //{
        //    xrDesignPanel1.Report.DataSource = null;
        //}

        //Daftar Fungsi
        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmFunctionList.ShowForm(_Evaluator);
        }

        //Baru
        private void OnNewReport(object sender, EventArgs e)
        {
            if (xrDesignPanel1.ReportState == ReportState.Changed &&
                XtraMessageBox.Show("Desain Laporan telah berubah, Batalkan Perubahan dan Buat Desain Baru ?",
                    "Konfirmasi Pembatalan Desain Laporan", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == System.Windows.Forms.DialogResult.No) return;
            NewDesign((xReport)xrDesignPanel1.Report);
            xrDesignPanel1.Report.DataSource = _DataSource;
            fieldListDockPanel1.UpdateDataSource(_DesignerHost);
            //xrDesignPanel1.Report.DataSource = null;
        }

        PrintControl OriginalPrintControl;
        private void frmReportLayout_Activated(object sender, EventArgs e)
        {
            BaseWinFramework.mdiRibbonPrintController.PrintControl =
                OriginalPrintControl;

            BaseWinFramework.mdiDesignRibbonController
                .XRDesignPanel = xrDesignPanel1;
        }
    }
}