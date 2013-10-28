using System.Windows.Forms;
namespace SentraWinFramework
{
    partial class frmDocument
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.uiNavigator1 = new SentraWinFramework.UINavigator();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Dock = DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(593, 274);
            this.panelControl1.TabIndex = 0;
            // 
            // uiNavigator1
            // 
            this.uiNavigator1.BindingSource = null;
            this.uiNavigator1.DataFilter = "";
            this.uiNavigator1.Dock = DockStyle.Bottom;
            this.uiNavigator1.Evaluator = null;
            this.uiNavigator1.ExcludeFields = "";
            this.uiNavigator1.LastFormModeIsView = true;
            this.uiNavigator1.Location = new System.Drawing.Point(0, 274);
            this.uiNavigator1.Name = "uiNavigator1";
            this.uiNavigator1.Size = new System.Drawing.Size(593, 72);
            this.uiNavigator1.TabIndex = 1;
            // 
            // frmDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 346);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.uiNavigator1);
            this.Name = "frmDocument";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "frmDocument";
            this.FormClosed += new FormClosedEventHandler(this.frmDocument_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SentraWinFramework.UINavigator uiNavigator1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}