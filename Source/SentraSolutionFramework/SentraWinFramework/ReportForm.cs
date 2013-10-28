using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Reflection;
using SentraSolutionFramework.Entity;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;

namespace SentraWinFramework
{
    public partial class ReportForm : XtraForm, 
        IFilterForm, IGridSelected, IShowView, IInitGrid
    {
        private DXErrorProvider ep = new DXErrorProvider();

        public ReportForm()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (DesignMode || !Visible) return;
            BaseWinFramework.WinForm.AutoFormat
                .AutoFormatForm(this, false);
            base.OnVisibleChanged(e);
        }

        [Obsolete("Use ReportEntity instead")]
        protected virtual Dictionary<string, object> FilterList
        { get { return null; } set { } }

        protected virtual void GridSelected(object Data) { }

        [Obsolete("Use ReportEntity instead")]
        protected virtual void ShowView(params object[] Parameters) { }

        protected virtual void InitGrid(GridView GridView) { }

        internal bool HandleGridSelected()
        {
            return IsHandled("GridSelected");
        }

        private bool IsHandled(string MethodName)
        {
            return GetType().GetMethod(MethodName, 
                BindingFlags.Instance | 
                BindingFlags.NonPublic | 
                BindingFlags.DeclaredOnly) != null;
        }

        #region IFilterForm Members
        Dictionary<string, object> IFilterForm.FilterList
        {
            get
            {
                return FilterList;
            }
            set
            {
                FilterList = value;
            }
        }
        #endregion

        #region IGridSelected Members
        void IGridSelected.GridSelected(object Data)
        {
            GridSelected(Data);
        }
        #endregion

        #region IShowView Members
        void IShowView.ShowView(params object[] Parameters)
        {
            ShowView(Parameters);
        }
        #endregion

        #region IInitGrid Members
        void IInitGrid.InitGrid(GridView GridView)
        {
            InitGrid(GridView);
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            ep.ContainerControl = this;
            ep.DataSource = BaseWinFramework.FindMainBindingSource(this, typeof(ReportEntity));
            FindGridAndLookup(Controls);
        }

        public void UpdateErrorBinding()
        {
            if (ep != null)
                ep.UpdateBinding();
        }

        private void FindGridAndLookup(Control.ControlCollection Ctrls)
        {
            foreach (Control Ctrl in Ctrls)
            {
                LookUpEdit le = Ctrl as LookUpEdit;
                if (le != null)
                {
                    foreach (EditorButton b in le
                        .Properties.Buttons)
                        if (b.Kind == ButtonPredefines.Redo)
                        {
                            le.ButtonClick +=
                                new ButtonPressedEventHandler(
                                Le_ButtonClick);
                            break;
                        }
                }
                else if (Ctrl.Controls.Count > 0)
                    FindGridAndLookup(Ctrl.Controls);
            }
        }

        void Le_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Redo)
            {
                string DataMember = ((BindingSource)((LookUpEdit)sender).
                    Properties.DataSource).DataMember;
                object px = ((BindingSource)((LookUpEdit)sender).
                    Properties.DataSource).DataSource;
                if (DataMember.Length > 0)
                {
                    PropertyInfo pi = px.GetType().GetProperty(DataMember, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (pi != null)
                    {
                        IAutoUpdateList lst = (IAutoUpdateList)pi.GetValue(px, null);
                        lst.Refresh();
                    }
                    else
                    {

                        FieldInfo fi = px.GetType().GetField(DataMember, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (fi != null)
                        {
                            IAutoUpdateList lst = (IAutoUpdateList)fi.GetValue(px);
                            lst.Refresh();
                        }
                    }
                }
                else
                {
                    IAutoUpdateList lst = px as IAutoUpdateList;
                    if (lst != null) lst.Refresh();
                }
            }
        }
    }
}