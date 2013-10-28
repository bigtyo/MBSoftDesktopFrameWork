using System.Windows.Forms;
namespace SentraWinFramework.Report
{
    partial class frmFreeReport
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.printControl1 = new DevExpress.XtraPrinting.Control.PrintControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.sbHal = new DevExpress.XtraBars.BarStaticItem();
            this.sbTotal = new DevExpress.XtraBars.BarStaticItem();
            this.sbZoom = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Left)
                        | AnchorStyles.Right)));
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 34);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.xtraScrollableControl1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.printControl1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(582, 308);
            this.splitContainerControl1.SplitterPosition = 53;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.xtraScrollableControl1.Appearance.Options.UseBackColor = true;
            this.xtraScrollableControl1.Dock = DockStyle.Fill;
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(578, 49);
            this.xtraScrollableControl1.TabIndex = 0;
            // 
            // printControl1
            // 
            this.printControl1.Dock = DockStyle.Fill;
            this.printControl1.IsMetric = false;
            this.printControl1.Location = new System.Drawing.Point(0, 0);
            this.printControl1.Name = "printControl1";
            this.printControl1.Size = new System.Drawing.Size(578, 245);
            this.printControl1.TabIndex = 0;
            this.printControl1.SelectedPageChanged += new DevExpress.XtraPrinting.PageEventHandler(this.printControl1_SelectedPageChanged);
            this.printControl1.ZoomChanged += new System.EventHandler(this.printControl1_ZoomChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(75, 13);
            this.labelControl1.TabIndex = 35;
            this.labelControl1.Text = "Layout Laporan";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(95, 6);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            toolTipTitleItem1.Text = "Refresh Laporan (Ctrl-R).";
            toolTipItem1.Appearance.Image = global::SentraWinFramework.Properties.Resources.document_refresh1;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = global::SentraWinFramework.Properties.Resources.document_refresh1;
            toolTipItem1.LeftIndent = 6;
            superToolTip2.Items.Add(toolTipTitleItem1);
            superToolTip2.Items.Add(toolTipItem1);
            toolTipTitleItem2.Text = "Edit Desain Laporan.";
            toolTipItem2.Appearance.Image = global::SentraWinFramework.Properties.Resources.edit1;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.Image = global::SentraWinFramework.Properties.Resources.edit1;
            toolTipItem2.LeftIndent = 6;
            superToolTip3.Items.Add(toolTipTitleItem2);
            superToolTip3.Items.Add(toolTipItem2);
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", superToolTip1),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, global::SentraWinFramework.Properties.Resources.document_refresh, new DevExpress.Utils.KeyShortcut((Keys.Control | Keys.R)), serializableAppearanceObject1, "", null, superToolTip2),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, global::SentraWinFramework.Properties.Resources.edit, superToolTip3)});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(435, 22);
            this.comboBoxEdit1.TabIndex = 1;
            this.comboBoxEdit1.ToolTip = "Pilih Layout Aktif";
            this.comboBoxEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.comboBoxEdit1_ButtonClick);
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.sbHal);
            this.ribbonStatusBar1.ItemLinks.Add(this.sbTotal, true);
            this.ribbonStatusBar1.ItemLinks.Add(this.sbZoom, true);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 343);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(582, 24);
            // 
            // sbHal
            // 
            this.sbHal.Caption = "barStaticItem1";
            this.sbHal.Id = 0;
            this.sbHal.Name = "sbHal";
            this.sbHal.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // sbTotal
            // 
            this.sbTotal.Caption = "barStaticItem1";
            this.sbTotal.Id = 1;
            this.sbTotal.Name = "sbTotal";
            this.sbTotal.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // sbZoom
            // 
            this.sbZoom.Caption = "barStaticItem2";
            this.sbZoom.Id = 2;
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
            this.ribbonControl1.MaxItemId = 3;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(582, 0);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.Visible = false;
            // 
            // frmFreeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 367);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.splitContainerControl1);
            this.KeyPreview = true;
            this.Name = "frmFreeReport";
            this.Text = "Lihat Laporan";
            this.Activated += new System.EventHandler(this.frmFreeReport_Activated);
            this.KeyDown += new KeyEventHandler(this.frmFreeReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
        private DevExpress.XtraPrinting.Control.PrintControl printControl1;
        private DevExpress.XtraPrinting.Preview.MultiplePagesControlContainer multiplePagesControlContainer1;
        private DevExpress.XtraPrinting.Preview.ColorPopupControlContainer colorPopupControlContainer1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarStaticItem sbHal;
        private DevExpress.XtraBars.BarStaticItem sbTotal;
        private DevExpress.XtraBars.BarStaticItem sbZoom;
    }
}