using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Entity;
using SentraUtility.Expression;
using SentraSolutionFramework;
using System.IO;

namespace SentraWinFramework.Report
{
    internal partial class frmReport : XtraForm
    {
        protected ReportLayout rptl;
        protected EntityForm _EntityForm;
        protected string _ReportName;

        protected IShowView _ShowViewForm;

        protected IReportEntity re;
        protected IFilterForm _FilterForm;
        protected Evaluator _Evaluator;

        protected object _DataSource;

        protected void InitFilterForm(Form Frm,
            XtraScrollableControl xtraScrollableControl1,
            SplitContainerControl splitContainerControl1)
        {
            Frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Frm.Height = Frm.ClientSize.Height;
            Frm.TopLevel = false;
            Frm.Parent = xtraScrollableControl1;
            Frm.Dock = DockStyle.Top;
            splitContainerControl1.SplitterPosition = Frm.Height + 7;

            //Find DataSource
            BindingSource bs = BaseWinFramework
                .FindMainBindingSource(Frm, typeof(ReportEntity));
            if (bs != null && bs.DataSource as ReportEntity == null)
            {
                Type EntityType = ((Type)bs.DataSource).UnderlyingSystemType;
                re = (ReportEntity)BaseFactory.CreateInstance(EntityType);
                _Evaluator = BaseFactory.CreateInstance<Evaluator>();
                TableDef td = MetaData.GetTableDef(EntityType);
                td.SetDefault((BaseEntity)re);

                re.InitUI();
                re.SetReportRefresh(ReportRefresh);
                re.SetFormChanged(OnFormChanged);
                bs.DataSource = re;
            }
            Frm.Show();
        }

        protected void QueryReportEntity(
            XtraScrollableControl xtraScrollableControl1,
            SplitContainerControl splitContainerControl1)
        {
            Control Ctrl = _FilterForm as Control;
            if (Ctrl != null)
            {
                splitContainerControl1.SplitterPosition = Ctrl.Height + 7;
                xtraScrollableControl1.Controls.Add(Ctrl);
                Ctrl.Dock = DockStyle.Fill;
            }
            else
            {
                splitContainerControl1.PanelVisibility = SplitPanelVisibility.Panel2;
                if (re != null)
                {
                    TableDef td = MetaData.GetTableDef(re.GetType());
                    td.SetDefault((BaseEntity)re);

                    re.InitUI();
                    re.SetReportRefresh(ReportRefresh);
                    re.SetFormChanged(OnFormChanged);
                }
            }
        }

        protected bool NeedRefresh = false;
        private void ReportRefresh()
        {
            if (BaseWinFramework.AutoRefreshReport)
                RefreshReport();
            else
                NeedRefresh = true;
        }

        protected virtual void RefreshReport() { }

        private void OnFormChanged()
        {
            //BaseWinFramework.WinForm.AutoFormat
            //    .AutoFormatForm((Control)_FilterForm, false);
            BaseWinFramework.WinForm.AutoLockEntity.LockForm(re, (Control)_FilterForm);
        }

        protected override void OnClosed(EventArgs e)
        {
            if (re != null) re.EndUI();
            if (_FilterForm != null)
                try
                {
                    if (_FilterForm != null)
                        ((Form)_FilterForm).Close();
                }
                catch { }
            base.OnClosed(e);
        }

        public static void SaveReportFromTemplateFolder(string RptHeader, string RptName,
            List<string> ListItems, Dictionary<string, object> FilterList)
        {
            if (!Directory.Exists(BaseWinFramework.ReportFolder)) return;
            string[] Files = Directory.GetFiles(
                BaseWinFramework.ReportFolder, string.Concat(
                RptHeader, RptName.Replace("/", "_"), "*.repx"));
            int LenRName = BaseWinFramework.ReportFolder
                .Length + RptName.Length + 2;
            foreach (string fl in Files)
            {
                string TmpName = fl.Substring(LenRName + 1);
                if (TmpName.Length == 5 || TmpName.Substring(0, 1) == "_")
                {
                    byte[] PrintLayout = File.ReadAllBytes(fl);
                    if (TmpName.Length == 5)
                        TmpName = RptName;
                    else
                        TmpName = TmpName.Substring(1, TmpName.Length - 6)
                            .Replace("_", "/");
                    if (PrintLayout != null)
                    {
                        DocPrintBrowseLayout.SaveNewFreeLayout(RptHeader + RptName,
                            TmpName, new MemoryStream(PrintLayout),
                            FilterList);
                        if (ListItems != null) ListItems.Add(TmpName);
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmReport
            // 
            this.ClientSize = new System.Drawing.Size(292, 267);
            this.Name = "frmReport";
            this.ResumeLayout(false);

        }
    }
}