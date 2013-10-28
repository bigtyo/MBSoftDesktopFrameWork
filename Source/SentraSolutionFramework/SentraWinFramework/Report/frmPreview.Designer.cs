using System.Windows.Forms;
namespace SentraWinFramework.Report
{
    partial class frmPreview
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
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.sbHal = new DevExpress.XtraBars.BarStaticItem();
            this.sbTotal = new DevExpress.XtraBars.BarStaticItem();
            this.sbZoom = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
            this.SuspendLayout();
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.sbHal);
            this.ribbonStatusBar1.ItemLinks.Add(this.sbTotal, true);
            this.ribbonStatusBar1.ItemLinks.Add(this.sbZoom, true);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 318);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(534, 24);
            // 
            // sbHal
            // 
            this.sbHal.Caption = "Halaman Ke: 1";
            this.sbHal.Id = 1;
            this.sbHal.Name = "sbHal";
            this.sbHal.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // sbTotal
            // 
            this.sbTotal.Caption = "Total Halaman: 1";
            this.sbTotal.Id = 2;
            this.sbTotal.Name = "sbTotal";
            this.sbTotal.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // sbZoom
            // 
            this.sbZoom.Caption = "Pembesaran: 100%";
            this.sbZoom.Id = 3;
            this.sbZoom.Name = "sbZoom";
            this.sbZoom.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonKeyTip = "";
            this.ribbonControl1.ApplicationIcon = null;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.sbHal,
            this.sbTotal,
            this.sbZoom});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(534, 0);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.Visible = false;
            // 
            // printControl1
            // 
            this.printControl1.Dock = DockStyle.Fill;
            this.printControl1.IsMetric = true;
            this.printControl1.Location = new System.Drawing.Point(0, 0);
            this.printControl1.Name = "printControl1";
            this.printControl1.Size = new System.Drawing.Size(534, 318);
            this.printControl1.TabIndex = 2;
            this.printControl1.SelectedPageChanged += new DevExpress.XtraPrinting.PageEventHandler(this.printControl1_SelectedPageChanged);
            this.printControl1.ZoomChanged += new System.EventHandler(this.printControl1_ZoomChanged);
            // 
            // frmPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 342);
            this.Controls.Add(this.printControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Name = "frmPreview";
            this.Text = "frmPreview";
            this.Activated += new System.EventHandler(this.frmPreview_Activated);
            this.FormClosing += new FormClosingEventHandler(this.frmPreview_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraPrinting.Preview.MultiplePagesControlContainer multiplePagesControlContainer1;
        private DevExpress.XtraPrinting.Preview.ColorPopupControlContainer colorPopupControlContainer1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraPrinting.Control.PrintControl printControl1;
        private DevExpress.XtraBars.BarStaticItem sbHal;
        private DevExpress.XtraBars.BarStaticItem sbTotal;
        private DevExpress.XtraBars.BarStaticItem sbZoom;
    }
}