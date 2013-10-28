using System;
using SentraSolutionFramework.Persistance;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using SentraSolutionFramework.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using SentraUtility;
using System.Windows.Forms;
using System.Collections;

namespace SentraSolutionFramework
{
    public enum enEntityActionMode
    {
        BeforeSetDefault,
        BeforeSaveNew,
        BeforeSaveUpdate,
        BeforeSaveDelete,
        BeforeSaveCancel,
        BeforeLoad,

        InitUI,
        EndUI,

        AfterSetDefault = 20,
        AfterSaveNew,
        AfterSaveUpdate,
        AfterSaveDelete,
        AfterSaveCancel,

        AfterLoadFound,
        AfterLoadNotFound,
        ErrorAfterSaveNew,
        ErrorAfterSaveUpdate,
        ErrorAfterSaveDelete
    }

    public enum xDialogResult
    {
        // Summary:
        //     Nothing is returned from the dialog box. This means that the modal dialog
        //     continues running.
        None = 0,
        //
        // Summary:
        //     The dialog box return value is OK (usually sent from a button labeled OK).
        OK = 1,
        //
        // Summary:
        //     The dialog box return value is Cancel (usually sent from a button labeled
        //     Cancel).
        Cancel = 2,
        //
        // Summary:
        //     The dialog box return value is Abort (usually sent from a button labeled
        //     Abort).
        Abort = 3,
        //
        // Summary:
        //     The dialog box return value is Retry (usually sent from a button labeled
        //     Retry).
        Retry = 4,
        //
        // Summary:
        //     The dialog box return value is Ignore (usually sent from a button labeled
        //     Ignore).
        Ignore = 5,
        //
        // Summary:
        //     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes = 6,
        //
        // Summary:
        //     The dialog box return value is No (usually sent from a button labeled No).
        No = 7,
    }

    public enum xMessageBoxButtons
    {
        // Summary:
        //     The message box contains an OK button.
        OK = 0,
        //
        // Summary:
        //     The message box contains OK and Cancel buttons.
        OKCancel = 1,
        //
        // Summary:
        //     The message box contains Abort, Retry, and Ignore buttons.
        AbortRetryIgnore = 2,
        //
        // Summary:
        //     The message box contains Yes, No, and Cancel buttons.
        YesNoCancel = 3,
        //
        // Summary:
        //     The message box contains Yes and No buttons.
        YesNo = 4,
        //
        // Summary:
        //     The message box contains Retry and Cancel buttons.
        RetryCancel = 5,
    }

    public delegate void EntityAction(BaseEntity ActionEntity, enEntityActionMode ActionMode);
    public delegate void BeforeSetAction(object Data, out bool Cancel);
    public delegate void ShowMessage(string Message, string Caption);

    public delegate List<object[]> ShowSelectTable(DataPersistance Dp,
        string ReturnFields, string SqlSelect, string Caption,
        string HideFields, params FieldParam[] Parameters);

    public delegate xDialogResult ShowDialogTable1(DataPersistance Dp,
        string SqlSelect, string Caption, string Message,
        xMessageBoxButtons Buttons, out bool IsDataExist,
        params FieldParam[] Parameters);

    public delegate xDialogResult ShowDialogTable2(DataPersistance Dp,
        string SqlSelect, string Caption, string Message,
        xMessageBoxButtons Buttons, out bool IsDataExist,
        Dictionary<string, string> FormatCols, params FieldParam[] Parameters);

    public delegate IList ChooseEntity(Type EntityType,
        IList ListSource, IList OldListSelect, DataPersistance Dp, 
        string Conditions, string OrderCondition,
        bool CallLoadRule, string Caption, EntityColumnShow ecs,
        params FieldParam[] Parameters);

    public delegate object ChooseSingleEntity(Type EntityType,
        IList ListSource, DataPersistance Dp, string Conditions, 
        string OrderCondition, bool CallLoadRule,
        string Caption, EntityColumnShow ecs,
        params FieldParam[] Parameters);

    public delegate bool ShowDialog(Type FormType, ParentEntity Entity);

    //[DebuggerNonUserCode]
    public static class BaseFramework
    {
        private static bool _EnableWriteLog;
        public static bool EnableWriteLog
        {
            get { return _EnableWriteLog; }
            set
            {
                _EnableWriteLog = value;
                if (DefaultDp != null)
                    DefaultDp.EnableWriteLog = value;
            }
        }

        public static bool ConnectEventServer;
        public static bool UseEventServer;
        public static bool ShowWarningWhenLogin;

        //private static void LoadAllAssembly(Assembly asm)
        //{
        //    foreach (AssemblyName asn in asm.GetReferencedAssemblies())
        //        if (asn.GetPublicKeyToken().Length == 0)
        //            LoadAllAssembly(Assembly.Load(asn));
        //}

        private static void ShowMsg(string Message, string Caption)
        {
            MessageBox.Show(Message, Caption.Length > 0 ?
                Caption : Application.ProductName,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DateTime ServerDateOrDateTime = DateTime.MinValue.AddDays(1);

        internal static Dictionary<Assembly, DataPersistance> DictAsmDp =
            new Dictionary<Assembly, DataPersistance>();
        public static void RegisterDefaultDp(Assembly Asm, DataPersistance Dp)
        {
            DictAsmDp[Asm] = Dp;
        }

        public static bool AutoClearMetaDataVersion;
        public static bool AutoUpdateMetaData = true;

        static BaseFramework()
        {
            AutoDialog.ShowMessage = ShowMsg;
            try
            {
                //LoadAllAssembly(Assembly.GetEntryAssembly());

                //foreach (Assembly asm in AppDomain.CurrentDomain
                //    .GetAssemblies())
                //    if (!asm.GlobalAssemblyCache) 
                //        CheckServiceAndRels(asm);

                AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);

                //Kalau pada salah satu referencenya refer ke SentraSolutionFramework, Load it..
                string CurAsmName = Assembly.GetExecutingAssembly().GetName().Name;
                foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    if (!asm.GlobalAssemblyCache)
                        foreach (AssemblyName asn in asm.GetReferencedAssemblies())
                            if (asn.GetPublicKey() == null && asn.Name == CurAsmName)
                            {
                                CheckServiceAndRels(asm);
                                break;
                            }
            }
            catch { }
            onDefaultDPChanged += new EventHandler(BaseFramework_onDefaultDPChanged);
        }

        static void BaseFramework_onDefaultDPChanged(object sender, EventArgs e)
        {
            TransDate.Reload();
        }

        internal static void Init() { }

        [DebuggerNonUserCode]
        public static class TransDate
        {
            public static event BeforeSetAction onBeforeSetMinDate;
            public static event BeforeSetAction onBeforeSetStartDate;

            private static DateTime _MinDate;
            /// <summary>
            /// Minimum Transaction Date Allowed
            /// </summary>
            public static DateTime MinDate
            {
                get { return _MinDate; }
                set
                {
                    if (value < _StartDate) return;
                    if (onBeforeSetMinDate != null)
                    {
                        bool Cancel;
                        onBeforeSetMinDate(value, out Cancel);
                        if (Cancel) return;
                    }
                    _MinDate = value;
                    DefaultDp.SetVariable(
                        "System", "TransMinDate", value);
                }
            }

            private static DateTime _StartDate;
            /// <summary>
            /// Date of Computerization Begin
            /// </summary>
            public static DateTime StartDate
            {
                get { return _StartDate; }
                set
                {
                    if (StartDateMustBeStartMonth && value.Day != 1)
                        return;
                    if (onBeforeSetStartDate != null)
                    {
                        bool Cancel;
                        onBeforeSetStartDate(value, out Cancel);
                        if (Cancel) return;
                    }
                    _StartDate = value;
                    if (_StartDate > _MinDate)
                        MinDate = _StartDate;
                    DefaultDp.SetVariable(
                        "System", "TransStartDate", value);
                }
            }

            /// <summary>
            /// Reload TransStartDate & TransMinDate from Database
            /// </summary>
            public static void Reload()
            {
                if (BaseFramework.DefaultDp == null) return;

                _StartDate = BaseFramework.DefaultDp
                    .GetVariable<DateTime>("System", "TransStartDate",
                    BaseFramework.DefaultDp.GetDbDate().AddDays(1 - 
                    BaseFramework.DefaultDp.GetDbDate().Day));
                _MinDate = BaseFramework.DefaultDp
                    .GetVariable<DateTime>("System", "TransMinDate",
                    _StartDate);
            }

            public static void ReloadMinDate()
            {
                if (BaseFramework.DefaultDp == null) return;

                _MinDate = BaseFramework.DefaultDp
                    .GetVariable<DateTime>("System", "TransMinDate",
                    _StartDate);
            }

            public static bool CheckTransDate = true;
            public static bool ReloadMinDateBeforeSave = true;
            public static bool StartDateMustBeStartMonth = true;
        }

        private static Type TRegisterService = typeof(RegisterServiceAttribute);
        private static void CheckServiceAndRels(Assembly asm)
        {
            foreach (Type tp in asm.GetTypes())
            {
                if (!tp.IsClass || tp.IsAbstract) continue;
                if (tp.IsPublic)
                {
                    Type tpi = tp.GetInterface("I" + tp.Name, false);
                    if (tpi != null && tpi.IsPublic &&
                        tp.IsSubclassOf(typeof(BusinessEntity)))
                        BaseFactory.RegisterObjType(tpi, tp);
                    if (tp.IsDefined(TRegisterService, true))
                    {
                        BaseService.RegisterService(tp,
                            BaseFactory.CreateInstance(tp));
                    }
                }
                foreach (RelationAttribute rel in (RelationAttribute[])
                    tp.GetCustomAttributes(typeof(RelationAttribute), true))
                    try
                    {
                        MetaData.GetTableDef(rel._ParentType);
                    }
                    catch { }
            }
        }

        static void CurrentDomain_AssemblyLoad(object sender, 
            AssemblyLoadEventArgs args)
        {
            if (!args.LoadedAssembly.GlobalAssemblyCache)
                CheckServiceAndRels(args.LoadedAssembly);
        }

        public static event EntityAction onEntityAction;

        public static event EventHandler onDefaultDPChanged;

        private static DataPersistance _DefaultDp;
        public static DataPersistance DefaultDp
        {
            get
            {
                return GetDefaultDp(Assembly.GetCallingAssembly());
            }
            set
            {
                if (!object.ReferenceEquals(_DefaultDp, value))
                {
                    _DefaultDp = value;
                    if (onDefaultDPChanged != null)
                        onDefaultDPChanged(null, null);
                }
            }
        }

        public static DataPersistance GetDefaultDp()
        {
            return _DefaultDp;
        }
        public static DataPersistance GetDefaultDp(Assembly Asm)
        {
            DataPersistance retDp;
            if (DictAsmDp.TryGetValue(Asm, out retDp))
                return retDp;
            else
                return _DefaultDp;
        }

        [DebuggerNonUserCode]
        public static class DpEngine
        {
            static DpEngine()
            {
                BaseFramework.Init();
            }
            public static Dictionary<string, Type> DictEngine = 
                new Dictionary<string, Type>();
            public static void RegisterEngine<TEngine>() 
                where TEngine : DataPersistance, new()
            {
                string EngineName = DataPersistance.GetEngineName<TEngine>();

                if (!DictEngine.ContainsKey(EngineName))
                    DictEngine.Add(EngineName, typeof(TEngine));
            }
            public static DataPersistance CreateDataPersistance(
                string EngineName, string ConnectionString, 
                bool AutoCreateDb, string FolderLocation)
            {
                Type EType;
                if (DictEngine.TryGetValue(EngineName, out EType))
                {
                    return (DataPersistance)BaseFactory.CreateInstance(EType,
                        ConnectionString, AutoCreateDb, FolderLocation);
                }
                else
                    throw new ApplicationException("Engine Database tidak ditemukan !");
            }
        }

        /// <summary>
        /// For Internal Use Only
        /// </summary>
        [DebuggerNonUserCode]
        public static class AutoDialog
        {
            public static ShowMessage ShowMessage;
            public static ShowSelectTable ShowSelectTable;
            public static ShowDialogTable1 ShowDialogTable1;
            public static ShowDialogTable2 ShowDialogTable2;

            public static ShowDialog ShowDialog;

            public static ChooseEntity ChooseEntity;
            public static ChooseSingleEntity ChooseSingleEntity;
        }

        public static string ProductName = Application.ProductName;
        public static string CompanyName = Application.CompanyName;

        internal static void DoEntityAction(BaseEntity Entity, enEntityActionMode ActionMode)
        {
            if (onEntityAction != null) onEntityAction(Entity, ActionMode);
        }
    }
}
