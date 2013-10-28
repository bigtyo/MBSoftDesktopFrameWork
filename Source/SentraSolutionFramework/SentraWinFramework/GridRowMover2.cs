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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Diagnostics;

namespace SentraWinFramework
{
    [DebuggerNonUserCode]
    public partial class GridRowMover2 : 
        XtraUserControl, IGridRowMover
    {
        public GridRowMover2()
        {
            InitializeComponent();
        }
        
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

        void gv_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            if (e.HitInfo.InRowCell && e.HitInfo.RowHandle >= 0 &&
                buttonEdit3.Enabled && buttonEdit3.Visible)
            {
                e.Menu.Items.Add(new DXMenuItem("&Hapus Baris",
                    HapusMenu_Click, Properties.Resources.Delete_Red));
            }
        }

        void HapusMenu_Click(object sender, EventArgs e)
        {
            buttonEdit3_ButtonClick(null, null);
        }

        private bool _ScrollVisible = true;
        [Category("Setting"), DefaultValue(true)]
        public bool ScrollVisible
        {
            get { return _ScrollVisible; }
            set
            {
                _ScrollVisible = value;
                buttonEdit1.Visible = value;
                buttonEdit2.Visible = value;
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
                buttonEdit3.Visible = value;
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
                buttonEdit1.Enabled = value;
                buttonEdit2.Enabled = value;
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
                buttonEdit3.Enabled = value;
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
                    _GridControl.FocusedView.ActiveEditor != null) && buttonEdit3.Enabled &&
                    buttonEdit3.Visible)
                {
                    buttonEdit3_ButtonClick(null, null);
                    e.SuppressKeyPress = true;
                }
        }

        private void buttonEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (OnProcess) return;
            try
            {
                OnProcess = true;
                GridView gv = _GridControl.FocusedView as GridView;
                if (gv != null)
                    BaseWinFramework.WinForm.Grid
                        .MoveRowItem(gv, true);
            }
            finally
            {
                OnProcess = false;
            }
        }

        private void buttonEdit2_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (OnProcess) return;
            try
            {
                OnProcess = true;
                GridView gv = _GridControl.FocusedView as GridView;
                if (gv != null)
                    BaseWinFramework.WinForm.Grid
                        .MoveRowItem(gv, false);
            }
            finally
            {
                OnProcess = false;
            }

        }

        private void buttonEdit3_ButtonClick(object sender, ButtonPressedEventArgs e)
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

        private void buttonEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
