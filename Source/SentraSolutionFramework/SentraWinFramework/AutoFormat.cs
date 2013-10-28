using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using DevExpress.XtraEditors;
using System.Reflection;

namespace SentraWinFramework
{
    [Designer(typeof(HostComponentDesigner))]
    public partial class AutoFormat : Component, ISupportInitialize
    {
        public AutoFormat()
        {
            InitializeComponent();
        }

        private Control _OwnerForm;
        [Browsable(false)]
        public Control OwnerForm
        {
            get { return _OwnerForm; }
            set { _OwnerForm = value; }
        }

        public AutoFormat(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit() { }
        void ISupportInitialize.EndInit()
        {
            if (!DesignMode)
                BaseWinFramework.WinForm.AutoFormat
                    .AutoFormatForm(_OwnerForm, false);
        }
        #endregion
    }

    internal class HostComponentDesigner : ComponentDesigner
    {
        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);

            if (ParentComponent is ContainerControl)
            {
                PropertyInfo Prop = Component.GetType()
                    .GetProperty("OwnerForm");
                if (Prop != null)
                    Prop.SetValue(Component, ParentComponent, null);
            }
        }
    }
}
