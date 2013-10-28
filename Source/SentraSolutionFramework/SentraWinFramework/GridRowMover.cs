using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Diagnostics;

namespace SentraWinFramework
{
    [DebuggerNonUserCode]
    public partial class GridRowMover :
        XtraUserControl, IGridRowMover
    {
        public GridRowMover()
        {
            InitializeComponent();
        }

        private int LastValue = 500;
        private bool OnProcess = false;

        private GridControl _GridControl;
        public GridControl GridControl
        {
            get { return _GridControl; }
            set
            {
                _GridControl = value;
                if (DesignMode) return;
                if (value == null)
                {
                    XtraMessageBox.Show("GridControl pada GridRowMover masih null !",
                        "Error GridRowMover", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (ColumnView cv in _GridControl.ViewCollection)
                    if (cv.SourceView == null)
                    {
                        GridView gv = cv as GridView;
                        if (gv != null)
                            gv.ShowGridMenu += new GridMenuEventHandler(gv_ShowGridMenu);
                    }
            }
        }

        void  gv_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.HitInfo.InRowCell && e.HitInfo.RowHandle >=  0 &&
                hyperLinkEdit1.Enabled)
            {
                e.Menu.Items.Add(new DXMenuItem("&Hapus Baris",
                    HapusMenu_Click, Properties.Resources.Delete_Red));
            }
        }

        void HapusMenu_Click(object sender, EventArgs e)
        {
            hyperLinkEdit1_OpenLink(null, null);
        }

        void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            if (DesignMode || OnProcess) return;
            try
            {
                OnProcess = true;
                GridView gv = _GridControl.FocusedView as GridView;
                if (gv == null) return;

                int curValue = vScrollBar1.Value;
                BaseWinFramework.WinForm.Grid.MoveRowItem(gv,
                    curValue < LastValue);
                if (curValue > 900 || curValue < 100)
                {
                    vScrollBar1.Value = 500;
                    curValue = 500;
                }
                LastValue = curValue;
            }
            finally
            {
                OnProcess = false;
            }
        }

        private bool _ScrollVisible = true;
        [Category("Setting"), DefaultValue(true)]
        public bool ScrollVisible
        {
            get { return _ScrollVisible; }
            set
            {
                _ScrollVisible = value;
                vScrollBar1.Visible = value;
            }
        }

        private bool _DeleteRowVisible = true;
        [Category("Setting"), DefaultValue(true)]
        public bool DeleteRowVisible
        {
            get { return _DeleteRowVisible; }
            set
            {
                _DeleteRowVisible = value;
                hyperLinkEdit1.Visible = value;
            }
        }

        private bool _ScrollEnabled = true;
        [Category("Setting"), DefaultValue(true)]
        public bool ScrollEnabled
        {
            get { return _ScrollEnabled; }
            set
            {
                _ScrollEnabled = value;
                vScrollBar1.Enabled = value;
            }
        }

        private bool _DeleteRowEnabled = true;
        [Category("Setting"), DefaultValue(true)]
        public bool DeleteRowEnabled
        {
            get { return _DeleteRowEnabled; }
            set
            {
                _DeleteRowEnabled = value;
                hyperLinkEdit1.Enabled = value;
            }
        }

        private bool _DeleteConfirm = true;
        [Category("Setting"), DefaultValue(true)]
        public bool DeleteConfirm
        {
            get { return _DeleteConfirm; }
            set { _DeleteConfirm = value; }
        }

        private string _DeleteMessage = "Hapus Baris yang dipilih ?";
        [Category("Setting"), DefaultValue("Hapus Baris yang dipilih ?")]
        public string DeleteMessage
        {
            get { return _DeleteMessage; }
            set { _DeleteMessage = value; }
        }

        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            GridView gv = _GridControl.FocusedView as GridView;
            if (gv == null) return;

            if (gv.SelectedRowsCount > 0 && gv.GetSelectedRows()[0] >= 0)
            {
                if (_DeleteConfirm && XtraMessageBox.Show(_DeleteMessage,
                    "Konfirmasi Hapus Baris", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No) return;
                gv.DeleteSelectedRows();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode) return;
            ParentForm.KeyPreview = true;
            ParentForm.KeyDown += new KeyEventHandler(ParentForm_KeyDown);
        }

        void ParentForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
                if ((object.ReferenceEquals(ParentForm.ActiveControl, _GridControl) ||
                    _GridControl.FocusedView.ActiveEditor != null) && hyperLinkEdit1.Enabled)
                {
                    hyperLinkEdit1_OpenLink(null, null);
                    e.SuppressKeyPress = true;
                }
        }
    }
}
