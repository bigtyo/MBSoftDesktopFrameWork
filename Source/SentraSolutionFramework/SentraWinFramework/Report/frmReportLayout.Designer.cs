using System.Windows.Forms;
namespace SentraWinFramework.Report
{
    partial class frmReportLayout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportLayout));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.xrDesignDockManager1 = new DevExpress.XtraReports.UserDesigner.XRDesignDockManager();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.fieldListDockPanel1 = new DevExpress.XtraReports.UserDesigner.FieldListDockPanel();
            this.fieldListDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.xrDesignPanel1 = new DevExpress.XtraReports.UserDesigner.XRDesignPanel();
            this.reportExplorerDockPanel1 = new DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel();
            this.reportExplorerDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.propertyGridDockPanel1 = new DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel();
            this.propertyGridDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            this.toolBoxDockPanel1 = new DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel();
            this.toolBoxDockPanel1_Container = new DevExpress.XtraReports.UserDesigner.DesignControlContainer();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignDockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.panelContainer2.SuspendLayout();
            this.fieldListDockPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel1)).BeginInit();
            this.reportExplorerDockPanel1.SuspendLayout();
            this.propertyGridDockPanel1.SuspendLayout();
            this.toolBoxDockPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.xrDesignDockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.barEditItem3,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4});
            this.barManager1.MaxItemId = 6;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox2});
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.barEditItem3, "", false, true, true, 215),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Border = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.barStaticItem1.Caption = "Current Layout : ";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "barEditItem3";
            this.barEditItem3.Edit = this.repositoryItemComboBox2;
            this.barEditItem3.Id = 1;
            this.barEditItem3.Name = "barEditItem3";
            this.barEditItem3.EditValueChanged += new System.EventHandler(this.barEditItem3_EditValueChanged);
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Items.AddRange(new object[] {
            "(Tanpa Layout)"});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Simpan";
            this.barButtonItem1.Glyph = global::SentraWinFramework.Properties.Resources.document_ok;
            this.barButtonItem1.Hint = "Simpan Layout ke Database";
            this.barButtonItem1.Id = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Simpan Baru";
            this.barButtonItem2.Glyph = global::SentraWinFramework.Properties.Resources.document_new;
            this.barButtonItem2.Hint = "Simpan ke Database sbg Layout Baru";
            this.barButtonItem2.Id = 3;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Hapus";
            this.barButtonItem3.Glyph = global::SentraWinFramework.Properties.Resources.document_delete;
            this.barButtonItem3.Hint = "Hapus Layout dari Database";
            this.barButtonItem3.Id = 4;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Fungsi";
            this.barButtonItem4.Glyph = global::SentraWinFramework.Properties.Resources.xrFunction1;
            this.barButtonItem4.Hint = "Tampilkan Daftar Fungsi";
            this.barButtonItem4.Id = 5;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // xrDesignDockManager1
            // 
            this.xrDesignDockManager1.Form = this;
            this.xrDesignDockManager1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("xrDesignDockManager1.ImageStream")));
            this.xrDesignDockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1,
            this.toolBoxDockPanel1});
            this.xrDesignDockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            this.xrDesignDockManager1.XRDesignPanel = this.xrDesignPanel1;
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.panelContainer2);
            this.panelContainer1.Controls.Add(this.propertyGridDockPanel1);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("cf7fbea8-281a-4041-8219-986636f95f73");
            this.panelContainer1.Location = new System.Drawing.Point(335, 26);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.Size = new System.Drawing.Size(192, 265);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // panelContainer2
            // 
            this.panelContainer2.ActiveChild = this.fieldListDockPanel1;
            this.panelContainer2.Controls.Add(this.reportExplorerDockPanel1);
            this.panelContainer2.Controls.Add(this.fieldListDockPanel1);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer2.ID = new System.Guid("c498a42e-a285-4ead-9bb3-ad2857db3b69");
            this.panelContainer2.Location = new System.Drawing.Point(0, 0);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.Size = new System.Drawing.Size(192, 124);
            this.panelContainer2.Tabbed = true;
            this.panelContainer2.Text = "panelContainer2";
            // 
            // fieldListDockPanel1
            // 
            this.fieldListDockPanel1.Controls.Add(this.fieldListDockPanel1_Container);
            this.fieldListDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.fieldListDockPanel1.ID = new System.Guid("faf69838-a93f-4114-83e8-d0d09cc5ce95");
            this.fieldListDockPanel1.ImageIndex = 0;
            this.fieldListDockPanel1.Location = new System.Drawing.Point(3, 25);
            this.fieldListDockPanel1.Name = "fieldListDockPanel1";
            this.fieldListDockPanel1.ShowNodeToolTips = false;
            this.fieldListDockPanel1.Size = new System.Drawing.Size(186, 73);
            this.fieldListDockPanel1.Text = "Field List";
            this.fieldListDockPanel1.XRDesignPanel = this.xrDesignPanel1;
            // 
            // fieldListDockPanel1_Container
            // 
            this.fieldListDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.fieldListDockPanel1_Container.Name = "fieldListDockPanel1_Container";
            this.fieldListDockPanel1_Container.Size = new System.Drawing.Size(186, 73);
            this.fieldListDockPanel1_Container.TabIndex = 0;
            // 
            // xrDesignPanel1
            // 
            this.xrDesignPanel1.Dock = DockStyle.Fill;
            this.xrDesignPanel1.Location = new System.Drawing.Point(0, 0);
            this.xrDesignPanel1.Name = "xrDesignPanel1";
            this.xrDesignPanel1.Padding = new Padding(1);
            this.xrDesignPanel1.Size = new System.Drawing.Size(519, 287);
            this.xrDesignPanel1.TabIndex = 9;
            this.xrDesignPanel1.DesignerHostLoaded += new DevExpress.XtraReports.UserDesigner.DesignerLoadedEventHandler(this.rpt_DesignerLoaded);
            this.xrDesignPanel1.SelectedTabIndexChanged += new System.EventHandler(this.xrDesignPanel1_SelectedTabIndexChanged);
            // 
            // reportExplorerDockPanel1
            // 
            this.reportExplorerDockPanel1.Controls.Add(this.reportExplorerDockPanel1_Container);
            this.reportExplorerDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.reportExplorerDockPanel1.ID = new System.Guid("fb3ec6cc-3b9b-4b9c-91cf-cff78c1edbf1");
            this.reportExplorerDockPanel1.ImageIndex = 2;
            this.reportExplorerDockPanel1.Location = new System.Drawing.Point(3, 25);
            this.reportExplorerDockPanel1.Name = "reportExplorerDockPanel1";
            this.reportExplorerDockPanel1.Size = new System.Drawing.Size(186, 73);
            this.reportExplorerDockPanel1.Text = "Report Explorer";
            this.reportExplorerDockPanel1.XRDesignPanel = this.xrDesignPanel1;
            // 
            // reportExplorerDockPanel1_Container
            // 
            this.reportExplorerDockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.reportExplorerDockPanel1_Container.Name = "reportExplorerDockPanel1_Container";
            this.reportExplorerDockPanel1_Container.Size = new System.Drawing.Size(186, 73);
            this.reportExplorerDockPanel1_Container.TabIndex = 0;
            // 
            // propertyGridDockPanel1
            // 
            this.propertyGridDockPanel1.Controls.Add(this.propertyGridDockPanel1_Container);
            this.propertyGridDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.propertyGridDockPanel1.ID = new System.Guid("b38d12c3-cd06-4dec-b93d-63a0088e495a");
            this.propertyGridDockPanel1.ImageIndex = 1;
            this.propertyGridDockPanel1.Location = new System.Drawing.Point(0, 124);
            this.propertyGridDockPanel1.Name = "propertyGridDockPanel1";
            this.propertyGridDockPanel1.Size = new System.Drawing.Size(192, 141);
            this.propertyGridDockPanel1.Text = "Property Grid";
            this.propertyGridDockPanel1.XRDesignPanel = this.xrDesignPanel1;
            // 
            // propertyGridDockPanel1_Container
            // 
            this.propertyGridDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.propertyGridDockPanel1_Container.Name = "propertyGridDockPanel1_Container";
            this.propertyGridDockPanel1_Container.Size = new System.Drawing.Size(186, 113);
            this.propertyGridDockPanel1_Container.TabIndex = 0;
            // 
            // toolBoxDockPanel1
            // 
            this.toolBoxDockPanel1.Controls.Add(this.toolBoxDockPanel1_Container);
            this.toolBoxDockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.toolBoxDockPanel1.ID = new System.Guid("161a5a1a-d9b9-4f06-9ac4-d0c3e507c54f");
            this.toolBoxDockPanel1.ImageIndex = 3;
            this.toolBoxDockPanel1.Location = new System.Drawing.Point(0, 26);
            this.toolBoxDockPanel1.Name = "toolBoxDockPanel1";
            this.toolBoxDockPanel1.Size = new System.Drawing.Size(126, 265);
            this.toolBoxDockPanel1.Text = "Tool Box";
            this.toolBoxDockPanel1.XRDesignPanel = this.xrDesignPanel1;
            // 
            // toolBoxDockPanel1_Container
            // 
            this.toolBoxDockPanel1_Container.Location = new System.Drawing.Point(3, 25);
            this.toolBoxDockPanel1_Container.Name = "toolBoxDockPanel1_Container";
            this.toolBoxDockPanel1_Container.Size = new System.Drawing.Size(120, 237);
            this.toolBoxDockPanel1_Container.TabIndex = 0;
            // 
            // frmReportLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 287);
            this.Controls.Add(this.xrDesignPanel1);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.toolBoxDockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmReportLayout";
            this.Text = "frmReportLayout";
            this.Activated += new System.EventHandler(this.frmReportLayout_Activated);
            this.FormClosing += new FormClosingEventHandler(this.frmReportLayout_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignDockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.panelContainer2.ResumeLayout(false);
            this.fieldListDockPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xrDesignPanel1)).EndInit();
            this.reportExplorerDockPanel1.ResumeLayout(false);
            this.propertyGridDockPanel1.ResumeLayout(false);
            this.toolBoxDockPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarEditItem barEditItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraReports.UserDesigner.XRDesignDockManager xrDesignDockManager1;
        private DevExpress.XtraReports.UserDesigner.XRDesignPanel xrDesignPanel1;
        private DevExpress.XtraReports.UserDesigner.ToolBoxDockPanel toolBoxDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer toolBoxDockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
        private DevExpress.XtraReports.UserDesigner.ReportExplorerDockPanel reportExplorerDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer reportExplorerDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.FieldListDockPanel fieldListDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer fieldListDockPanel1_Container;
        private DevExpress.XtraReports.UserDesigner.PropertyGridDockPanel propertyGridDockPanel1;
        private DevExpress.XtraReports.UserDesigner.DesignControlContainer propertyGridDockPanel1_Container;




    }
}