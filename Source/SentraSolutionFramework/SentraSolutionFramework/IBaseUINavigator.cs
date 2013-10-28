using System;
using System.Collections.Generic;
using System.Text;
using SentraSolutionFramework.Entity;
using SentraSolutionFramework.Persistance;
using System.Windows.Forms;
using SentraUtility.Expression;
using System.Collections;

namespace SentraSolutionFramework
{
    public delegate void LockForm(bool IsLocked);
    public delegate void NewAction(ref bool CallSetDefault);
    public delegate void BeforeAction(ref bool Cancel);
    public delegate void SelectPrintLayout(IList SelectedLayout);
    public delegate void DataSourceChanged(BusinessEntity Entity);
    public delegate void FormModeChanged(FormMode NewFormMode);

    public interface IBaseUINavigator
    {
        bool AllowAdd { get; set; }
        bool AllowBrowse { get; set; }
        bool AllowBrowseDesignPrint { get; set; }
        bool AllowBrowseExport { get; set; }
        bool AllowBrowsePrint { get; set; }
        bool AllowBrowseSaveLayout { get; set; }
        bool AllowDelete { get; set; }
        bool AllowDesignPrint { get; set; }
        bool AllowEdit { get; set; }
        bool AllowPrint { get; set; }
        void SetAutoFormMode();
        bool LastFormModeIsView { get; set; }
        Evaluator Evaluator { get; set; }
        event BeforeAction BeforeSaveDelete;
        event BeforeAction BeforeSaveNew;
        event BeforeAction BeforeSaveUpdate;
        BindingSource BindingSource { get; set; }
        string DataFilter { get; set; }
        event DataSourceChanged DataSourceChanged;
        bool EnableAutoFormat { get; set; }
        ParentEntity Entity { get; }
        string ExcludeFields { get; set; }
        void FindData(string Condition, params FieldParam[] Parameters);
        void FindKey(string Key);
        ParentEntity GetEntity();
        ParentEntity GetOriginal();
        bool MoveDataAfterDelete { get; set; }
        EntityNavigator Navigator { get; }
        event EntityAction onEntityAction;
        event DataMoving onDataMoving;
        event LockForm onLockForm;
        event NewAction onNewMode;
        event SelectPrintLayout onSelectPrintLayout;
        event FormModeChanged onFormModeChanged;
        bool AskSaveChangedForm { get; set; }
        void SetDefaultValue(string ExcludeFields);
        void SetDefaultValue();
        void SetNew();
        void SetSecurity(string ModuleName);
        bool Visible { get; set; }

        bool TryGetFocusedRowValue<TType>(string TableName,
            string FieldName, out TType Value);
    }
}
