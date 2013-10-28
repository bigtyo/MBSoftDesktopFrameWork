namespace SentraWinFramework
{
    partial class GridRowMover3
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
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.ToolTipSeparatorItem toolTipSeparatorItem4 = new DevExpress.Utils.ToolTipSeparatorItem();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            this.buttonEdit3 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit2 = new DevExpress.XtraEditors.ButtonEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonEdit3
            // 
            this.buttonEdit3.Location = new System.Drawing.Point(0, 70);
            this.buttonEdit3.Name = "buttonEdit3";
            this.buttonEdit3.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit3.Properties.Appearance.Options.UseFont = true;
            this.buttonEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.buttonEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit3.Size = new System.Drawing.Size(28, 24);
            toolTipTitleItem3.Text = "Hapus Baris Item Data (Ctrl-D).";
            toolTipItem2.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_delete;
            toolTipItem2.Appearance.Options.UseImage = true;
            toolTipItem2.Image = global::SentraWinFramework.Properties.Resources.element_delete;
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Digunakan untuk menghapus Baris Item Data yang dipilih pada Grid.";
            toolTipTitleItem4.LeftIndent = 6;
            toolTipTitleItem4.Text = "Item Data yang akan dihapus harus dipilih terlebih dahulu dengan menekan panel in" +
                "dikator baris di sebelah kiri grid.";
            superToolTip2.Items.Add(toolTipTitleItem3);
            superToolTip2.Items.Add(toolTipItem2);
            superToolTip2.Items.Add(toolTipSeparatorItem2);
            superToolTip2.Items.Add(toolTipTitleItem4);
            this.buttonEdit3.SuperTip = superToolTip2;
            this.buttonEdit3.TabIndex = 5;
            this.buttonEdit3.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit3_ButtonClick);
            // 
            // buttonEdit2
            // 
            this.buttonEdit2.Location = new System.Drawing.Point(0, 30);
            this.buttonEdit2.Name = "buttonEdit2";
            this.buttonEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit2.Properties.Appearance.Options.UseFont = true;
            this.buttonEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.buttonEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit2.Size = new System.Drawing.Size(28, 24);
            toolTipTitleItem5.Text = "Pindah Baris Item Data ke Bawah.";
            toolTipItem3.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_down;
            toolTipItem3.Appearance.Options.UseImage = true;
            toolTipItem3.Image = global::SentraWinFramework.Properties.Resources.element_down;
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Digunakan untuk memindahkan Baris Item Data pada Grid ke bawah.";
            toolTipTitleItem6.LeftIndent = 6;
            toolTipTitleItem6.Text = "Pada satu saat hanya satu baris saja yang dapat dipindahkan.";
            superToolTip3.Items.Add(toolTipTitleItem5);
            superToolTip3.Items.Add(toolTipItem3);
            superToolTip3.Items.Add(toolTipSeparatorItem3);
            superToolTip3.Items.Add(toolTipTitleItem6);
            this.buttonEdit2.SuperTip = superToolTip3;
            this.buttonEdit2.TabIndex = 4;
            this.buttonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit2_ButtonClick);
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(0, 0);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEdit1.Properties.Appearance.Options.UseFont = true;
            this.buttonEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Up)});
            this.buttonEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.buttonEdit1.Size = new System.Drawing.Size(28, 24);
            toolTipTitleItem7.Text = "Pindah Baris Item Data ke Atas.";
            toolTipItem4.Appearance.Image = global::SentraWinFramework.Properties.Resources.element_up;
            toolTipItem4.Appearance.Options.UseImage = true;
            toolTipItem4.Image = global::SentraWinFramework.Properties.Resources.element_up;
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Digunakan untuk memindahkan Baris Item Data pada Grid ke Atas.";
            toolTipTitleItem8.LeftIndent = 6;
            toolTipTitleItem8.Text = "Pada satu saat hanya satu baris saja yang dapat dipindahkan.";
            superToolTip4.Items.Add(toolTipTitleItem7);
            superToolTip4.Items.Add(toolTipItem4);
            superToolTip4.Items.Add(toolTipSeparatorItem4);
            superToolTip4.Items.Add(toolTipTitleItem8);
            this.buttonEdit1.SuperTip = superToolTip4;
            this.buttonEdit1.TabIndex = 3;
            this.buttonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_ButtonClick);
            // 
            // GridRowMover3
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonEdit3);
            this.Controls.Add(this.buttonEdit2);
            this.Controls.Add(this.buttonEdit1);
            this.Name = "GridRowMover3";
            this.Size = new System.Drawing.Size(37, 95);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit buttonEdit3;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
    }
}
