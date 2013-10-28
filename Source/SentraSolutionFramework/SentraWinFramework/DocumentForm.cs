using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework;
using SentraUtility;

namespace SentraWinFramework
{
    public partial class DocumentForm : XtraForm, IEntityControl
    {
        private bool _ShowConfirmButton = true;
        [Category("Behavior"), DefaultValue(true)]
        public bool ShowConfirmButton
        {
            get { return _ShowConfirmButton; }
            set { _ShowConfirmButton = value; }
        }

        private bool _ShowNavigator = true;
        [Category("Behavior"), DefaultValue(true)]
        public bool ShowNavigator
        {
            get { return _ShowNavigator; }
            set { _ShowNavigator = value; }
        }

        public DocumentForm()
        {
            InitializeComponent();
        }

        internal void CallOnFormClosing(FormClosingEventArgs e)
        {
            OnFormClosing(e);
        }

        protected virtual void InitNavigator(IUINavigator Navigator) { }

        protected internal virtual void EndDialog(DialogResult dr, Dictionary<string, object> Parameters) { }

        #region IEntityControl Members
        void IEntityControl.InitNavigator(IUINavigator Navigator)
        {
            BindingSource MainBs = BaseWinFramework.FindMainBindingSource(
                this, typeof(ParentEntity));
            if (MainBs == null)
                throw new ApplicationException("Main BindingSource not found !");

            if (MainBs.DataSource as ParentEntity == null)
            {
                Type EntityType = ((Type)MainBs.DataSource).UnderlyingSystemType;
                MainBs.DataSource = BaseFactory.CreateInstance(EntityType);
            }

            BaseWinFramework.WinForm.AutoFormat
                .AutoFormatForm(this, false);

            if (Navigator != null)
            {
                Navigator.BindingSource = MainBs;

                InitNavigator(Navigator);
                ((IRuleInitUI)MainBs.DataSource).AfterInitNavigator(Navigator);

                Navigator.SetAutoFormMode();
                if (!ShowNavigator)
                    Navigator.Visible = false;
            }
        }
        #endregion
    }
}