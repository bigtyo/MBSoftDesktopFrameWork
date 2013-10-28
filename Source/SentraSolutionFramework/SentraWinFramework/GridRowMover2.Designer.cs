namespace SentraWinFramework
{
    partial class GridRowMover2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem1 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem2 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem3 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit2 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit3 = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(0, 0);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Up)});
            this.buttonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit1.Size = new System.Drawing.Size(25, 18);
            toolTipTitleItem1.Text = "Pindah Baris Item Data ke Atas.";
            toolTipItem1.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_up;
            toolTipItem1.Appearance.Options.UseImage = true;
            toolTipItem1.Image = global::SentraWinFramework.Properties.Resources.element_up;
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Digunakan untuk memindahkan Baris Item Data pada Grid ke Atas.";
            toolTipTitleItem2.LeftIndent = 6;
            toolTipTitleItem2.Text = "Pada satu saat hanya satu baris saja yang dapat dipindahkan.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.Items.Add(toolTipSeparatorItem1);
            superToolTip1.Items.Add(toolTipTitleItem2);
            this.buttonEdit1.SuperTip = superToolTip1;
            this.buttonEdit1.TabIndex = 2;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.Location = new System.Drawing.Point(0, 18);
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.buttonEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit2.Size = new System.Drawing.Size(25, 18);
            toolTipTitleItem3.Text = "Pindah Baris Item Data ke Bawah.";
            toolTipItem2.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_down;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.Image = global::SentraWinFramework.Properties.Resources.element_down;
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Digunakan untuk memindahkan Baris Item Data pada Grid ke bawah.";
            toolTipTitleItem4.LeftIndent = 6;
            toolTipTitleItem4.Text = "Pada satu saat hanya satu baris saja yang dapat dipindahkan.";
            superToolTip2.Items.Add(toolTipTitleItem3);
            superToolTip2.Items.Add(toolTipItem2);
            superToolTip2.Items.Add(toolTipSeparatorItem2);
            superToolTip2.Items.Add(toolTipTitleItem4);
            this.buttonEdit2.SuperTip = superToolTip2;
            this.buttonEdit2.TabIndex = 2;
            this.buttonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
            // 
            // buttonEdit3
            // 
            this.buttonEdit3.Location = new System.Drawing.Point(0, 41);
            this.buttonEdit3.Name = "buttonEdit3";
            this.buttonEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.buttonEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit3.Size = new System.Drawing.Size(25, 18);
            toolTipTitleItem5.Text = "Hapus Baris Item Data (Ctrl-D).";
            toolTipItem3.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_delete;
            toolTipItem3.Appearance.Options.UseImage = true;
            toolTipItem3.Image = global::SentraWinFramework.Properties.Resources.element_delete;
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Digunakan untuk menghapus Baris Item Data yang dipilih pada Grid.";
            toolTipTitleItem6.LeftIndent = 6;
            toolTipTitleItem6.Text = "Item Data yang akan dihapus harus dipilih terlebih dahulu dengan menekan panel in" +
                "dikator baris di sebelah kiri grid.";
            superToolTip3.Items.Add(toolTipTitleItem5);
            superToolTip3.Items.Add(toolTipItem3);
            superToolTip3.Items.Add(toolTipSeparatorItem3);
            superToolTip3.Items.Add(toolTipTitleItem6);
            this.buttonEdit3.SuperTip = superToolTip3;
            this.buttonEdit3.TabIndex = 2;
            this.buttonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit3_ButtonClick);
            this.buttonEdit3.EditValueChanged += new System.EventHandler(this.buttonEdit3_EditValueChanged);
            // 
            // GridRowMover2
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonEdit3);
            this.Controls.Add(this.buttonEdit2);
            this.Controls.Add(this.buttonEdit1);
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = true;
            this.Name = "GridRowMover2";
            this.Size = new System.Drawing.Size(28, 64);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit3;
    }
}
