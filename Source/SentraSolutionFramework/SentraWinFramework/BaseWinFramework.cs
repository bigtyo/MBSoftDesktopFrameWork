using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.LookAndFeel;
using System.Windows.Forms;
using SentraWinFramework.Report;
using System.Reflection;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using SentraSolutionFramework.Entity;
using DevExpress.XtraEditors;
using SentraUtility;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using System.Data;
using System.Collections;
using SentraSolutionFramework.Persistance;
using SentraSolutionFramework;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data;
using DevExpress.XtraGrid.Localization;
using System.ComponentModel;
using DevExpress.XtraPivotGrid;
using SentraUtility.Expression;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Menu;
using System.Diagnostics;
using SentraSecurity;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using DevExpress.XtraBars.Ribbon;
using System.Threading;
using DevExpress.XtraBars;
using System.Drawing;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraEditors.Mask;
using System.IO;
using DevExpress.XtraGrid.Views.Base;

namespace SentraWinFramework
{
    public enum ReportType
    {
        Tabular,
        FreeLayout
    }
    public interface IAutoLockControl { }

    public delegate void DoAction();

    [DebuggerNonUserCode]
    public static class SecurityVarName
    {
        public static string DocumentView = "Lihat Dokumen";
        public static string ReportView = "Lihat Laporan";

        public static string DocumentNew = "Baru";
        public static string DocumentEdit = "Edit";
        public static string DocumentDelete = "Hapus";
        public static string DocumentPrint = "Cetak";
        public static string DocumentDesignPrint = "Desain Cetak";

        public static string ReportPrint = "Cetak Laporan";
        public static string ReportDesignPrint = "Desain Cetak Laporan";
        public static string ReportSave = "Simpan Laporan";
        public static string ReportLayoutSave = "Simpan Layout Laporan";
    }

    //[DebuggerNonUserCode]
    public static class BaseWinFramework
    {
        // utk keperluan internal ketika printdesign...
        internal static Evaluator Evaluator;

        internal static BindingSource FindMainBindingSource(Form Form, Type ParentType)
        {
            IFormBindingSource fbs = Form as IFormBindingSource;
            if (fbs != null)
                return fbs.GetMainBindingSource();

            FieldInfo fi = Form.GetType().GetField("components",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fi != null)
            {
                IContainer components = (IContainer)fi.GetValue(Form);
                if (components == null) return null;
                foreach (Component Cmp in components.Components)
                {
                    BindingSource bs = Cmp as BindingSource;
                    if (bs != null)
                    {
                        Type tp = bs.DataSource.GetType();
                        if (tp.Name == "RuntimeType")
                            tp = (Type)bs.DataSource;
                        if (tp.IsSubclassOf(ParentType))
                            return bs;
                    }
                }
            }
            return null;
        }

        public static string ReportFolder = 
            Application.StartupPath + "\\Report Template";

        public static XmlConfig LocalConfig;

        public static bool TouchScreenVersion;

        public static bool AutoRefreshReport = false;

        public static string GetLocalDataPath()
        {
            try
            {
                return string.Concat(Environment.GetFolderPath(
                    Environment.SpecialFolder.CommonApplicationData),
                    "\\", BaseFramework.CompanyName,
                    "\\", BaseFramework.ProductName);
            }
            catch
            {
                return string.Concat(
                    "C:\\SentraFramework Setting\\",
                    BaseFramework.CompanyName, 
                    BaseFramework.ProductName);
            }
        }

        public static string GetLocalUserDataPath()
        {
            try
            {
                return string.Concat(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData),
                    "\\", BaseFramework.CompanyName,
                    "\\", BaseFramework.ProductName);
            }
            catch
            {
                return string.Concat(
                    "C:\\SentraFramework Setting\\",
                    BaseFramework.CompanyName,
                    BaseFramework.ProductName);
            }
        }

        public static bool CloseAllMdiChildForms()
        {
            SingleEntityForm.ClearCache();
            bool Success = true;
            List<Form> ClosedForm = new List<Form>();
            foreach (Form frm in Application.OpenForms)
                if (frm.IsMdiChild)
                    ClosedForm.Add(frm);

            foreach (Form frm in ClosedForm)
            {
                int NumForm = Application.OpenForms.Count;
                frm.Close();
                if (NumForm == Application.OpenForms.Count)
                    Success = false;
            }
            return Success;
        }

        public static Type GetTypeFromDataSource(object DataSource)
        {
            string DataMember;
            BindingSource Bnd = DataSource as BindingSource;

            if (Bnd != null)
            {
                DataSource = Bnd.DataSource;
                DataMember = Bnd.DataMember;
                Bnd = DataSource as BindingSource;
                if (Bnd != null) DataSource = Bnd.DataSource;
            }
            else
                DataMember = string.Empty;

            if (DataSource == null) return null;

            if (DataMember.Length == 0)
            {
                Type RetType = DataSource as Type;

                if (RetType == null)
                {
                    IList List = DataSource as IList;
                    if (List != null && List.Count > 0)
                        RetType = List[0].GetType();
                    else
                        RetType = DataSource.GetType();
                }

                if (RetType.IsGenericType)
                    return RetType.GetGenericArguments()[0];
                else
                {
                    Bnd = DataSource as BindingSource;
                    if (Bnd != null)
                    {
                        RetType = Bnd.DataSource.GetType();
                        if (RetType.IsGenericType)
                            return RetType.GetGenericArguments()[0];
                        else
                        {
                            IList lst = Bnd.DataSource as IList;
                            if (lst != null && lst.Count > 0)
                                return lst[0].GetType();
                            else
                                return Bnd.DataSource.GetType();
                        }
                    }
                    else
                        return RetType;
                }
            }
            else
            {
                Type RetType = DataSource.GetType();

                if (RetType.IsGenericType)
                    return Utility.GetFieldOrPropType(
                            RetType.GetGenericArguments()[0], DataMember);
                else
                {
                    Type xRetType = Utility.GetFieldOrPropType(DataSource.GetType(),
                          DataMember);
                    if (xRetType != null)
                    {
                        if (xRetType == typeof(DataTable))
                            return RetType;
                        else
                            if (xRetType.IsGenericType)
                                return xRetType.GetGenericArguments()[0];
                            else
                                return xRetType;
                    }
                    else
                        return ((Type)DataSource);
                }
            }
        }

        /// <summary>
        /// Class untuk mengubah Skin Tampilan
        /// </summary>
        [DebuggerNonUserCode]
        public static class Skin
        {
            static Skin() { BaseWinFramework.Init(); }

            /// <summary>
            /// Daftar Skin yang bisa digunakan
            /// </summary>
            public static string[] SkinList = new string[] {
                "Caramel", "Money Twins",
                "Lilian", "The Asphalt World", "iMaginary", "Coffee",
                "Liquid Sky", "London Liquid Sky", "Glass Oceans", 
                "Stardust", "Black", "Blue",
                "Office 2007 Blue", "Office 2007 Black", 
                "Office 2007 Silver", "Office 2007 Green",
                "Office 2007 Pink", "Xmas 2008 Blue", "Valentine", 
                "McSkin", "Summer 2008", "Pumpkin", 
                "Dark Side" };

            private static Color[] ColorList = new Color[] {
                Color.LightCoral, Color.LightBlue,
                Color.LightBlue, Color.LightBlue, Color.LightBlue, Color.LightBlue, 
                Color.LightBlue, Color.LightBlue, Color.LightBlue, 
                Color.LightBlue, Color.LightYellow, Color.LightBlue, 
                Color.LightBlue, Color.LightYellow, 
                Color.LightSteelBlue, Color.LightGreen, 
                Color.LightPink, Color.LightBlue, Color.LightPink,
                Color.LightYellow, Color.LightYellow, Color.LightYellow,
                Color.LightYellow };

            internal static Color GetPageColor()
            {
                int i = 0;
                foreach (string SkinName in SkinList)
                {
                    if (SkinName == _SkinName)
                        return ColorList[i];
                    i++;
                }
                return Color.LightBlue;
            }

            /// <summary>
            /// Skin Aktif sesuai Index pada SkinList
            /// </summary>
            public static int SkinIndex
            {
                get
                {
                    for (int i = 0; i < SkinList.Length; i++)
                        if (SkinList[i] == _SkinName)
                            return i;
                    return 2;
                }
                set
                {
                    SkinName = SkinList[value];
                }
            }

            private static string _SkinName;

            /// <summary>
            /// Skin Aktif berdasarkan Nama
            /// </summary>
            public static string SkinName
            {
                get { return _SkinName; }
                set
                {
                    _SkinName = value;
                    UserLookAndFeel.Default.SetSkinStyle(_SkinName);

                    if (BaseWinFramework.mdiDesignRibbonController == null) return;
                    Color Clr = Color.LightBlue;

                    int i = 0;
                    foreach (string SkinName in SkinList)
                    {
                        if (SkinName == _SkinName)
                        {
                            Clr = ColorList[i];
                            break;
                        }
                        i++;
                    }
                    BaseWinFramework.mdiDesignRibbonPageCategory.Color = Clr;
                }
            }

            /// <summary>
            /// Menampilkan Jendela untuk mengubah Skin
            /// </summary>
            /// <param name="_MdiMain"></param>
            public static void ShowChangeSkin()
            {
                frmAturTampilan.ShowForm(_MdiParent);
            }
        }

        private static object DoWaitCursor(bool CallDoEvent)
        {
            if (CallDoEvent) Application.DoEvents();
            Cursor OldC = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            return OldC;
        }

        private static void DoRestoreCursor(object LastCursor)
        {
            Cursor.Current = (Cursor)LastCursor;
        }

        static BaseWinFramework()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            _IWaitCursor.DoWaitCursor = DoWaitCursor;
            _IWaitCursor.DoRestoreCursor = DoRestoreCursor;

            XmlConfig TmpConfig = new XmlConfig(
                GetLocalUserDataPath() + "\\MSSSetting.dta");
            if (TmpConfig.GetAllKeys("Connection").Length == 0)
                try
                {
                    File.Delete(GetLocalUserDataPath() + "\\MSSSetting.dta");
                    File.Copy(GetLocalDataPath() + "\\Setting.dta",
                        GetLocalUserDataPath() + "\\MSSSetting.dta");
                }
                catch { }

            LocalConfig = new XmlConfig(
                GetLocalUserDataPath() + "\\MSSSetting.dta");

            RegisterAllSkins();

            DevExpress.Utils.ToolTipController ttc = DevExpress.Utils.ToolTipController.DefaultController;
            ttc.Rounded = true;
            ttc.AutoPopDelay = 50000;
            ttc.InitialDelay = 200;
            ttc.ShowBeak = true;
            try
            {
                Skin.SkinIndex = LocalConfig.ReadInt("Setting", "SkinIndex", 12);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error System",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            DevExpress.Skins.SkinManager.EnableFormSkins();
            GridLocalizer.Active = new Localizer.BahasaGridLocalizer();
            AutoDialog.InitAutoDialog();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            XtraMessageBox.Show(e.Exception.Message + " !", "Error Thread Exception",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Application.Exit();
        }

        private static string Caption1 = string.Empty; 
        private static string Caption2 = string.Empty;
        public static void ShowProgressBar(string Caption)
        {
            Caption1 = "Loading...";
            Caption2 = Caption;
            ShowSplash<frmProgressBar>();
        }
        public static void ShowProgressBar(string Caption1, string Caption2)
        {
            BaseWinFramework.Caption1 = Caption1;
            BaseWinFramework.Caption2 = Caption2;
            ShowSplash<frmProgressBar>();
        }
        public static void CloseProgressBar()
        {
            CloseSplash();
        }

        private static Form _frmSplash;
        private static Thread thrdSplash;

        public static void ShowSplash<TSplashForm>() 
            where TSplashForm : Form, new()
        {
            _frmSplash = new TSplashForm();
            thrdSplash = new Thread(new ThreadStart(ShowSplash));
            thrdSplash.SetApartmentState(ApartmentState.STA);
            thrdSplash.IsBackground = true;
            thrdSplash.Start();
        }

        public static void CloseSplash()
        {
            if (_frmSplash != null)
            {
                frmProgressBar frm = _frmSplash as frmProgressBar;
                if (frm != null)
                    frm.CloseForm();
                else
                    _frmSplash.Close();
                _frmSplash = null;
            }
        }

        private static void ShowSplash()
        {
            //Show in separate thread, need to re-register skin...
            RegisterAllSkins();
            UserLookAndFeel.Default.SetSkinStyle(Skin.SkinName);
            if (_frmSplash.GetType() == typeof(frmProgressBar))
                ((frmProgressBar)_frmSplash).SetText(Caption1, Caption2);
            _frmSplash.ShowDialog();
        }

        private static void RegisterAllSkins()
        {
            BonusSkins.Register();
            OfficeSkins.Register();
        }

        internal static Form _MdiParent;
        public static Form MdiParent
        {
            get { return _MdiParent; }
            set { Init(value); }
        }

        public static PopupMenu CreateAppMenu()
        {
            if (_MdiParent != null)
            {
                FieldInfo fi = _MdiParent.GetType()
                    .GetField("components", BindingFlags.Instance |
                    BindingFlags.NonPublic | BindingFlags.Public);

                IContainer components = (IContainer)fi.GetValue(_MdiParent);
                if (components == null)
                {
                    components = new Container();
                    fi.SetValue(_MdiParent, components);
                }
                PopupMenu pm = new ApplicationMenu(components);
                ((ApplicationMenu)pm).ShowRightPane = true;
                pm.Ribbon = mdiRibbonControl;
                return pm;
            }
            else 
                return new PopupMenu();
        }

        public static PopupMenu CreatePopupMenu()
        {
            if (_MdiParent != null)
            {
                FieldInfo fi = _MdiParent.GetType()
                    .GetField("components", BindingFlags.Instance |
                    BindingFlags.NonPublic | BindingFlags.Public);

                IContainer components = (IContainer)fi.GetValue(_MdiParent);
                if (components == null)
                {
                    components = new Container();
                    fi.SetValue(_MdiParent, components);
                }
                PopupMenu pm = new PopupMenu(components);

                pm.Ribbon = mdiRibbonControl;
                return pm;
            }
            else
                return new PopupMenu();
        }

        public static BarButtonItem AddButtonItem(string Caption,
            bool BeginGroup, PopupMenu ParentMenu,
            ItemClickEventHandler btnClick, Image Image)
        {
            BarButtonItem btn = new BarButtonItem(mdiBarManager, Caption);
            btn.Glyph = Image;
            if (btnClick != null)
                btn.ItemClick += new ItemClickEventHandler(btnClick);
            if (mdiRibbonControl != null)
                ParentMenu.ItemLinks.Add(btn, BeginGroup);
            else
                ParentMenu.LinksPersistInfo.Add(
                    new LinkPersistInfo(btn, BeginGroup));
            return btn;
        }

        static void MdiParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mdiRibbonControl != null)
                LocalConfig.WriteBool("Setting", 
                    "MinimizeRibbon", mdiRibbonControl.Minimized);
            LocalConfig.WriteInt("Setting", "SkinIndex", 
                BaseWinFramework.Skin.SkinIndex);
            LocalConfig.Save();
        }

        private static TFind GetObjectFromMdiParent<TFind>()
            where TFind : Control
        {
            Type FindType = typeof(TFind);
            foreach (Control Ctrl in _MdiParent.Controls)
                if (object.ReferenceEquals(Ctrl.GetType(), FindType))
                    return (TFind)Ctrl;
            return null;
        }

        public static RibbonControl mdiRibbonControl;
        public static RibbonStatusBar mdiRibbonStatusBar;
        public static BarManager mdiBarManager;
        public static Bar mdiBar;

        internal static PrintRibbonController mdiRibbonPrintController;
        internal static XRDesignRibbonController mdiDesignRibbonController;

        private static RibbonPageCategory mdiDesignRibbonPageCategory;

        public static void Init(Form MdiParent)
        {
            if (MdiParent == null || !MdiParent.IsMdiContainer) return;
            _MdiParent = MdiParent;

            MdiParent.FormClosed += new FormClosedEventHandler(MdiParent_FormClosed);
            MdiParent.Shown += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    CloseProgressBar();
                    MdiParent.Activate();
                });


            mdiRibbonControl = GetObjectFromMdiParent<RibbonControl>();
            if (mdiRibbonControl != null)
            {
                mdiRibbonControl.Minimized = LocalConfig.ReadBool("Setting",
                    "MinimizeRibbon", false);
                mdiRibbonStatusBar = GetObjectFromMdiParent<RibbonStatusBar>();

                Color PageColor = BaseWinFramework.Skin.GetPageColor();

                //Add Design Print
                mdiDesignRibbonController = new XRDesignRibbonController();
                mdiDesignRibbonController.Initialize(mdiRibbonControl, null);
                mdiDesignRibbonPageCategory =
                    new RibbonPageCategory("Desain Cetak",
                    PageColor, false);
                mdiRibbonControl.PageCategories.Add(mdiDesignRibbonPageCategory);
                mdiDesignRibbonPageCategory.Pages.AddRange(new RibbonPage[] {
                    mdiRibbonControl.Pages[
                    mdiRibbonControl.Pages.Count - 3],
                    mdiRibbonControl.Pages[
                    mdiRibbonControl.Pages.Count - 2],
                    mdiRibbonControl.Pages[
                    mdiRibbonControl.Pages.Count - 1] });
                Type tp = typeof(XRDesignRibbonController);
                FieldInfo fiPrc = tp.GetField("printRibbonController",
                    BindingFlags.NonPublic | BindingFlags.Instance);
                mdiRibbonPrintController = (PrintRibbonController)
                    fiPrc.GetValue(mdiDesignRibbonController);
                MdiParent.MdiChildActivate += new EventHandler(MdiParent_MdiChildActivate);
            }
            else
            {
                BarDockControl bdc = GetObjectFromMdiParent<BarDockControl>();
                if (bdc != null)
                {
                    mdiBarManager = bdc.Manager;
                    foreach(Bar br in mdiBarManager.Bars)
                        if (br.DockStyle == BarDockStyle.Bottom)
                        {
                            mdiBar = br;
                            break;
                        }
                }
            }
        }

        private static RibbonPage LastPage;

        static void MdiParent_MdiChildActivate(object sender, EventArgs e)
        {
            Form frm = _MdiParent.ActiveMdiChild;

            if (!object.ReferenceEquals(
                mdiRibbonControl.SelectedPage.Category,
                mdiDesignRibbonPageCategory))
                LastPage = mdiRibbonControl.SelectedPage;

            if (frm as frmPreview != null || frm as frmFreeReport != null)
            {
                mdiDesignRibbonPageCategory.Text = "Hasil Cetak";
                mdiDesignRibbonPageCategory.Visible = true;
                mdiDesignRibbonPageCategory.Pages[0].Visible = false;
                mdiDesignRibbonPageCategory.Pages[2].Visible = false;
                mdiRibbonControl.SelectedPage =
                    mdiDesignRibbonPageCategory.Pages[1];
            }
            else if (frm as frmReportLayout != null)
            {
                mdiDesignRibbonPageCategory.Text = "Desain Cetak";
                mdiDesignRibbonPageCategory.Pages[0].Visible = true;
                mdiDesignRibbonPageCategory.Pages[2].Visible = true;
                mdiDesignRibbonPageCategory.Visible = true;
                mdiRibbonControl.SelectedPage =
                    mdiDesignRibbonPageCategory.Pages[(frm as frmReportLayout).OldPreviewIndex];
            }
            else if (mdiDesignRibbonPageCategory.Visible)
            {
                //if (mdiRibbonControl.SelectedPage.Category.Name.Length > 0)
                //    mdiRibbonControl.SelectedPage = OldPage;
                try
                {
                    mdiRibbonControl.SelectedPage = LastPage;
                }
                catch { }
                mdiDesignRibbonPageCategory.Visible = false;
            }
        }

        internal static void Init() { }

        internal static bool CheckModuleAccessWithError(string ModuleName, 
            string VarName, object VarValue)
        {
            if (ModuleName.Length > 0 && BaseSecurity.CurrentLogin
                .CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(ModuleName);
                object maValue;

                if (!ma.Variables.TryGetValue(VarName, out maValue) ||
                    maValue == null || !maValue.Equals(VarValue))
                {
                    XtraMessageBox.Show(string.Concat(
                        "Anda tidak memiliki hak akses '", VarName, "' pada Modul [",
                        ModuleName, "] !"), "Hak Akses Modul",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        public static DialogResult ShowDocumentFormDialog<TControl>()
            where TControl : DocumentForm
        {
            return frmSingletonEntity.ShowDialog<TControl>();
        }

        public static DialogResult ShowDocumentFormDialog<TControl>(
            ParentEntity Entity) where TControl : DocumentForm
        {
            return frmSingletonEntity.ShowDialog<TControl>(Entity);
        }

        public static DialogResult ShowDocumentFormDialog(Type ControlType)
        {
            return frmSingletonEntity.ShowDialog(ControlType, string.Empty);
        }
        public static DialogResult ShowDocumentFormDialog(
            Type ControlType, ParentEntity Entity)
        {
            return frmSingletonEntity.ShowDialog(ControlType, string.Empty, Entity);
        }
    
        /// <summary>
        /// Menangani Form yang hanya punya 1 instance
        /// </summary>
        [DebuggerNonUserCode]
        public static class SingleForm
        {
            static SingleForm() { BaseWinFramework.Init(); }

            private static Dictionary<Type, Form> _SingleForm =
                new Dictionary<Type, Form>();

            public static DialogResult ShowDialog<TForm>()
                where TForm : Form, new()
            {
                string ModuleName = GetModuleName(typeof(TForm));

                if (!CheckModuleAccessWithError(ModuleName,
                    SecurityVarName.DocumentView, true))
                    return System.Windows.Forms.DialogResult.None;

                return BaseFactory.CreateInstance<TForm>().ShowDialog();
            }

            public static void ShowForm<TForm>()
                where TForm : Form, new()
            {
                string ModuleName = GetModuleName(typeof(TForm));

                if (!CheckModuleAccessWithError(ModuleName, 
                    SecurityVarName.DocumentView, true)) return;

                using (new WaitCursor())
                {
                    Form frm;

                    if (!_SingleForm.TryGetValue(typeof(TForm), out frm))
                    {
                        frm = BaseFactory.CreateInstance<TForm>();
                        frm.MdiParent = _MdiParent;
                        _SingleForm.Add(typeof(TForm), frm);
                        frm.Show();
                    }
                    else
                    {
                        if (frm.IsDisposed)
                        {
                            frm = BaseFactory.CreateInstance<TForm>();
                            frm.MdiParent = _MdiParent;
                            frm.Show();
                        }
                        else
                            frm.BringToFront();
                    }
                }
            }

            public static TForm GetForm<TForm>()
                where TForm : Form, new()
            {
                Form frm;

                if (!_SingleForm.TryGetValue(typeof(TForm), out frm))
                {
                    frm = BaseFactory.CreateInstance<TForm>();
                    frm.MdiParent = _MdiParent;
                }
                else
                {
                    if (frm.IsDisposed)
                    {
                        frm = BaseFactory.CreateInstance<TForm>();
                        frm.MdiParent = _MdiParent;
                    }
                }
                return (TForm)frm;
            }

            public static DialogResult ShowDialog(Type FormType)
            {
                string ModuleName = GetModuleName(FormType);

                if (!CheckModuleAccessWithError(ModuleName,
                    SecurityVarName.DocumentView, true))
                    return System.Windows.Forms.DialogResult.None;

                return ((Form)BaseFactory.CreateInstance(FormType)).ShowDialog();
            }
            public static void ShowForm(Type FormType)
            {
                string ModuleName = GetModuleName(FormType);

                if (!CheckModuleAccessWithError(ModuleName,
                    SecurityVarName.DocumentView, true)) return;

                using (new WaitCursor())
                {
                    Form frm;

                    if (!_SingleForm.TryGetValue(FormType, out frm))
                    {
                        frm = (Form)BaseFactory.CreateInstance(FormType);
                        frm.MdiParent = _MdiParent;
                        _SingleForm.Add(FormType, frm);
                        frm.Show();
                    }
                    else
                    {
                        if (frm.IsDisposed)
                        {
                            frm = (Form)BaseFactory.CreateInstance(FormType);
                            frm.MdiParent = _MdiParent;
                            frm.Show();
                        }
                        else
                            frm.BringToFront();
                    }
                }
            }
            public static Form GetForm(Type FormType)
            {
                Form frm;

                if (!_SingleForm.TryGetValue(FormType, out frm))
                {
                    frm = (Form)BaseFactory.CreateInstance(FormType);
                    frm.MdiParent = _MdiParent;
                }
                else
                {
                    if (frm.IsDisposed)
                    {
                        frm = (Form)BaseFactory.CreateInstance(FormType);
                        frm.MdiParent = _MdiParent;
                    }
                }
                return frm;
            }
        }

        private static Dictionary<Type, string> _ListFormModuleName =
            new Dictionary<Type, string>();

        [DebuggerNonUserCode]
        private class DocForm
        {
            public Type FormType;
            public Type FilterFormType;

            public DocForm(Type FormType, Type FilterFormType)
            {
                this.FormType = FormType;
                this.FilterFormType = FilterFormType;
            }
        }

        private static Dictionary<string, DocForm> _ListDocForm =
            new Dictionary<string, DocForm>();

        public static string GetModuleName(Type FormType)
        {
            string RetVal;

            if (_ListFormModuleName.TryGetValue(FormType, out RetVal))
                return RetVal;
            else
                return string.Empty;
        }
        public static void SetModuleName(Type FormType, string NewModuleName)
        {
            _ListFormModuleName[FormType] = NewModuleName;
            _ListDocForm[NewModuleName] = new DocForm(FormType, null);
        }
        public static void SetModuleName(Type FormType, Type FilterFormType, string NewModuleName)
        {
            if (FormType != null)
                _ListFormModuleName[FormType] = NewModuleName;
            else
                _ListFormModuleName[FilterFormType] = NewModuleName;

            _ListDocForm[NewModuleName] = new DocForm(FormType, FilterFormType);
        }

        public static bool GetFormType(string ModuleName, out Type FormType, out Type FilterFormType)
        {
            DocForm retDoc;

            if (_ListDocForm.TryGetValue(ModuleName, out retDoc))
            {
                FormType = retDoc.FormType;
                FilterFormType = retDoc.FilterFormType;
                return true;
            }
            else
            {
                FormType = null;
                FilterFormType = null;
                return false;
            }
        }

        /// <summary>
        /// Menangani Entity Form yang hanya memiliki 1 instance
        /// </summary>
        //[DebuggerNonUserCode]
        public static class SingleEntityForm
        {
            static SingleEntityForm() { BaseWinFramework.Init(); }

            public static void ClearCache()
            {
                foreach (EntityForm ef in _SingleForm.Values)
                    ef.DataFilter = string.Empty;
            }

            private static Dictionary<Type, EntityForm> _SingleForm =
                new Dictionary<Type, EntityForm>();

            public static EntityForm GetEntityForm(string ModuleName)
            {
                Type FormType;
                Type FilterFormType;

                if (!GetFormType(ModuleName, out FormType,
                    out FilterFormType)) return null;

                EntityForm ef;
                Type ModuleType = FormType ?? FilterFormType;
                if (!_SingleForm.TryGetValue(ModuleType, out ef))
                {
                    ef = new EntityForm(FormType, FilterFormType);
                    _SingleForm.Add(ModuleType, ef);
                }
                return ef;
            }

            public static EntityForm GetNewEntityForm(string ModuleName)
            {
                Type FormType;
                Type FilterFormType;

                if (!GetFormType(ModuleName, out FormType,
                    out FilterFormType)) return null;

                return new EntityForm(FormType, FilterFormType);
            }

            #region ShowForm<TForm>
            public static Form ShowForm<TForm>()
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowForm();
                }
            }
            public static Form ShowForm<TForm>(string DataFilter)
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowForm();
                }
            }
            #endregion

            #region ShowForm<TForm, TFilterForm>
            public static Form ShowForm<TForm, TFilterForm>()
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowForm();
                }
            }
            public static Form ShowForm<TForm, TFilterForm>(string DataFilter)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowForm();
                }
            }
            #endregion

            #region ShowForm(ModuleName)
            public static Form ShowForm(string ModuleName)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowForm();
                }
            }
            public static Form ShowForm(string ModuleName, 
                string DataFilter)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowForm();
                }
            }
            #endregion

            #region ShowNewForm(ModuleName)
            public static Form ShowNewForm(string ModuleName)
            {
                using (new WaitCursor())
                {
                    return GetNewEntityForm(ModuleName).ShowForm();
                }
            }
            public static Form ShowNewForm(string ModuleName,
                string DataFilter)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetNewEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowForm();
                }
            }
            #endregion

            #region ShowView<TForm>
            public static Form ShowView<TForm>(string Condition)
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowView(Condition);
                }
            }
            public static Form ShowForm<TForm>(string Condition, string DataFilter)
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowView(Condition);
                }
            }
            #endregion

            #region ShowView<TForm, TFilterForm>
            public static Form ShowView<TForm, TFilterForm>(string Condition)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowView(Condition);
                }
            }
            public static Form ShowView<TForm, TFilterForm>(string Condition, string DataFilter)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowView(Condition);
                }
            }
            #endregion

            #region ShowView(ModuleName)
            public static Form ShowView(string ModuleName, 
                string Condition, params FieldParam[] Parameters)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName)
                        .ShowView(Condition, Parameters);
                }
            }
            public static Form ShowView(string ModuleName, 
                string Condition, string DataFilter, 
                params FieldParam[] Parameters)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowView(Condition, Parameters);
                }
            }
            #endregion

            #region ShowViewWithKey(ModuleName)
            public static Form ShowViewWithKey(string ModuleName, string Key)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowViewWithKey(Key);
                }
            }
            public static Form ShowViewWithKey(string ModuleName, string Key, string DataFilter)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowViewWithKey(Key);
                }
            }
            #endregion

            public static EntityForm GetSingleEntityForm<TForm>()
                where TForm : new()
            {
                EntityForm frm;

                _SingleForm.TryGetValue(typeof(TForm), out frm);
                return frm;
            }

            #region ShowTabular<TForm, TFilterForm>
            public static Form ShowTabular<TForm, TFilterForm>()
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabular<TForm, TFilterForm>(string DataFilter)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabular<TForm, TFilterForm>(string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular(FreeFilter, 
                        TransStartDate, TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowTabular(ModuleName)
            public static Form ShowTabular(string ModuleName)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowTabular();
                }
            }
            public static Form ShowTabular(string ModuleName, string DataFilter)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabular(string ModuleName, string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowTabular(FreeFilter,
                        TransStartDate, TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowTabularEntity<TForm>
            public static Form ShowTabularEntity<TForm>()
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabularEntity<TForm>(string DataFilter)
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabularEntity<TForm>(string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
                where TForm : new()
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowTabular(FreeFilter, TransStartDate, 
                        TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowTabular<TFilterForm>
            public static Form ShowTabular<TFilterForm>()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null,  typeof(TFilterForm));
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabular<TFilterForm>(string DataFilter)
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null, typeof(TFilterForm), DataFilter);
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowTabular();
                }
            }
            public static Form ShowTabular<TFilterForm>(string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null, typeof(TFilterForm));
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowTabular(FreeFilter, TransStartDate, 
                        TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowFreeLayout<TForm, TFilterForm>
            public static Form ShowFreeLayout<TForm, TFilterForm>()
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout<TForm, TFilterForm>(string DataFilter)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>(DataFilter);
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout<TForm, TFilterForm>(string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
                where TForm : new()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TForm), out ef))
                    {
                        ef = new EntityForm<TForm, TFilterForm>();
                        _SingleForm.Add(typeof(TForm), ef);
                    }
                    return ef.ShowFreeLayout(FreeFilter, TransStartDate,
                        TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowFreeLayout(ModuleName)
            public static Form ShowFreeLayout(string ModuleName)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout(string ModuleName, string DataFilter)
            {
                using (new WaitCursor())
                {
                    EntityForm ef = GetEntityForm(ModuleName);
                    ef.DataFilter = DataFilter;
                    return ef.ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout(string ModuleName, string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
            {
                using (new WaitCursor())
                {
                    return GetEntityForm(ModuleName).ShowFreeLayout(FreeFilter, TransStartDate,
                        TransEndDate, Parameters);
                }
            }
            #endregion

            #region ShowFreeLayout<TFilterForm>
            public static Form ShowFreeLayout<TFilterForm>()
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null, typeof(TFilterForm));
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout<TFilterForm>(string DataFilter)
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null, typeof(TFilterForm), DataFilter);
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowFreeLayout();
                }
            }
            public static Form ShowFreeLayout<TFilterForm>(string FreeFilter,
                object TransStartDate, object TransEndDate,
                params object[] Parameters)
                where TFilterForm : IFilterForm
            {
                using (new WaitCursor())
                {
                    EntityForm ef;
                    if (!_SingleForm.TryGetValue(typeof(TFilterForm), out ef))
                    {
                        ef = new EntityForm(null, typeof(TFilterForm));
                        ef.FilterFormType = typeof(TFilterForm);
                        _SingleForm.Add(typeof(TFilterForm), ef);
                    }
                    return ef.ShowFreeLayout(FreeFilter, TransStartDate, 
                        TransEndDate, Parameters);
                }
            }
            #endregion
        }

        [DebuggerNonUserCode]
        public static class Utility
        {
            public static Type GetFieldOrPropType(Type EntityType, BindingMemberInfo bmi)
            {
                if (bmi.BindingPath.Length > 0)
                {
                    string[] Path = bmi.BindingPath.Split('.');
                    foreach (string strPath in Path)
                        EntityType = GetFieldOrPropType(EntityType, strPath);
                }
                return GetFieldOrPropType(EntityType, bmi.BindingField);
            }

            public static FieldDef GetBindingFieldDef(TableDef td, BindingMemberInfo bmi)
            {
                if (bmi.BindingPath.Length > 0)
                {
                    Type EntityType = td.ClassType;
                    string[] Path = bmi.BindingPath.Split('.');
                    foreach (string strPath in Path)
                        EntityType = GetFieldOrPropType(EntityType, strPath);
                    td = MetaData.GetTableDef(EntityType);
                }
                return td.GetFieldDef(bmi.BindingField);
            }

            public static ParentEntity GetEntity(ParentEntity Entity, BindingMemberInfo bmi)
            {
                object TmpEntity = Entity;
                if (bmi.BindingPath.Length > 0)
                {
                    string[] Path = bmi.BindingPath.Split('.');
                    foreach (string strPath in Path)
                    {
                        MemberInfo mi = GetFieldOrProp(TmpEntity, strPath);
                        if (mi == null) return null;

                        if (mi.MemberType == MemberTypes.Property)
                            TmpEntity = ((PropertyInfo)mi).GetValue(TmpEntity, null);
                        else
                            TmpEntity = ((FieldInfo)mi).GetValue(TmpEntity);
                    }
                }
                return (ParentEntity)TmpEntity;
            }

            public static Type GetFieldOrPropType(Type EntityType,
                string FieldOrPropName)
            {
                PropertyInfo pi = EntityType.GetProperty(FieldOrPropName);
                if (pi != null) return pi.PropertyType;

                FieldInfo fi = EntityType.GetField(FieldOrPropName);
                if (fi == null)
                    throw new ApplicationException(string.Concat(
                        FieldOrPropName, " tidak ada pada kelas ",
                        EntityType.ToString()));
                else
                    return fi.FieldType;
            }

            private static MemberInfo GetFieldOrProp(object Entity,
                string FieldOrPropName)
            {
                PropertyInfo pi = Entity.GetType().GetProperty(FieldOrPropName);
                if (pi != null) return pi;

                FieldInfo fi = Entity.GetType().GetField(FieldOrPropName);
                return fi;
            }
        }

        /// <summary>
        /// Class yang mengurusi Otomatisasi Lock, Format, dan Grid pada WinForm
        /// </summary>
        //[DebuggerNonUserCode]
        public static class WinForm
        {
            private static Type UINaviType;
            static WinForm()
            {
                BaseWinFramework.Init();
                UINaviType = typeof(UINavigator);
            }

           [DebuggerNonUserCode]
            public static class AutoLock
            {
                /// <summary>
                /// Melakukan Lock/ Unlock otomatis seluruh Bound-Control pada Form.
                /// Bound-Control yg ingin dilock manual, tambahkan 'SkipLock' pada Tag-nya.
                /// Unbound-Control yg ingin dilock otomatis, tambahkan 'AutoLock' pada Tag-nya.
                /// GridColumn yg ingin dilock manual, tambahkan 'SkipLock' pada Column-Tag-nya.
                /// </summary>
                /// <param name="Form"></param>
                /// <param name="Locked"></param>
                public static void LockForm(Control Parent, bool Locked)
                {
                    try
                    {
                        Parent.SuspendLayout();
                        foreach (Control Ctrl in Parent.Controls)
                        {
                            bool Success = false;
                            if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                            if (Ctrl.Tag == null || Ctrl.Tag != null &&
                                Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                                Ctrl.DataBindings.Count > 0 &&
                                Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                                Success = LockControl(Ctrl, Locked);
                            if (!Success && Ctrl.Controls.Count > 0)
                                _LockForm(Ctrl, Locked);
                        }
                    }
                    finally
                    {
                        Parent.ResumeLayout();
                    }
                }

                private static void _LockForm(Control Parent, bool Locked)
                {
                    foreach (Control Ctrl in Parent.Controls)
                    {
                        bool Success = false;
                        if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                        if (Ctrl.Tag == null || Ctrl.Tag != null &&
                            Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                            Ctrl.DataBindings.Count > 0 &&
                            Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                            Success = LockControl(Ctrl, Locked);
                        if (!Success && Ctrl.Controls.Count > 0)
                            _LockForm(Ctrl, Locked);
                    }
                }
                public static bool LockControl(Control Ctrl, bool Locked)
                {
                    GridControl gc = Ctrl as GridControl;
                    if (gc != null)
                    {
                        LockGridControl(gc, Locked);
                        return true;
                    }
                    if (Ctrl as IAutoLockControl != null)
                    {
                        Ctrl.Enabled = !Locked;
                        return true;
                    }
                    if (Ctrl as IGridRowMover != null)
                    {
                        Ctrl.Enabled = !Locked;
                        return true;
                    }
                    try
                    {
                        ButtonEdit be = Ctrl as ButtonEdit;
                        if (be != null) 
                        {
                            be.Properties.ReadOnly = Locked;
                            foreach (EditorButton btn in be.Properties.Buttons)
                                if(btn.Tag as string == null ||
                                    !btn.Tag.ToString().Contains("SkipLock"))
                                    btn.Enabled = !Locked;
                            return true;
                        }

                        ComboBoxEdit cmb = Ctrl as ComboBoxEdit;
                        if (cmb != null)
                        {
                            cmb.Properties.ReadOnly = Locked;
                            foreach (EditorButton btn in cmb.Properties.Buttons)
                                btn.Enabled = !Locked;
                            return true;
                        }

                        PropertyInfo prop = Ctrl.GetType()
                            .GetProperty("ReadOnly");
                        if (prop != null)
                        {
                            prop.SetValue(Ctrl, Locked, null);
                            return true;
                        }

                        PropertyInfo pinfo = Ctrl.GetType().GetProperty("Properties",
                            typeof(RepositoryItem));
                        if (pinfo != null)
                        {
                            RepositoryItem Props = pinfo.GetValue(Ctrl, null) as RepositoryItem;
                            if (Props != null)
                            {
                                Props.ReadOnly = Locked;
                                prop = Props.GetType().GetProperty("Buttons");
                                if (prop != null)
                                {
                                    foreach (EditorButton btn in
                                        (EditorButtonCollection)prop.GetValue(Props, null))
                                        btn.Enabled = !Locked;
                                    return true;
                                }
                            }
                        }
                        System.Windows.Forms.ComboBox cbb =
                            Ctrl as System.Windows.Forms.ComboBox;
                        if (cbb != null)
                            cbb.Enabled = !Locked;
                    }
                    catch { }
                    return false;
                }
                private static void LockGridControl(GridControl gc, bool Locked)
                {
                    foreach (GridView gv in gc.ViewCollection)
                    {
                        gv.OptionsBehavior.Editable = !Locked;

                        if (Locked)
                            gv.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                        else
                            gv.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;

                        int LastVisibleIndex = 0;
                        foreach (GridColumn Col in gv.Columns)
                            if (Col.Tag == null || Col.Tag.ToString()
                                .IndexOf("SkipLock") < 0)
                            {
                                Col.VisibleIndex = LastVisibleIndex++;
                                Col.OptionsColumn.ReadOnly = Locked;
                            }
                    }
                }
            }

            //[DebuggerNonUserCode]
            public static class AutoLockEntity
            {
                /// <summary>
                /// Melakukan Lock/ Unlock otomatis seluruh Bound-Control pada Form.
                /// Bound-Control yg ingin dilock manual, tambahkan 'SkipLock' pada Tag-nya.
                /// Unbound-Control yg ingin dilock otomatis, tambahkan 'AutoLock' pada Tag-nya.
                /// GridColumn yg ingin dilock manual, tambahkan 'SkipLock' pada Column-Tag-nya.
                /// </summary>
                /// <param name="Form"></param>
                /// <param name="Locked"></param>
                public static void LockForm(ParentEntity Entity, TableDef td, Control Parent)
                {
                    try
                    {
                        Parent.SuspendLayout();
                        foreach (Control Ctrl in Parent.Controls)
                        {
                            bool Success = false;
                            if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                            if (Ctrl.Tag == null || Ctrl.Tag != null &&
                                Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                                Ctrl.DataBindings.Count > 0 &&
                                Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                                Success = LockControl(Entity, td, Ctrl);
                            if (!Success && Ctrl.Controls.Count > 0)
                                _LockForm(Entity, td, Ctrl);
                        }
                    }
                    finally
                    {
                        Parent.ResumeLayout();
                    }
                }
                private static void _LockForm(ParentEntity Entity, TableDef td, Control Parent)
                {
                    foreach (Control Ctrl in Parent.Controls)
                    {
                        bool Success = false;
                        if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                        if (Ctrl.Tag == null || Ctrl.Tag != null &&
                            Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                            Ctrl.DataBindings.Count > 0 &&
                            Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                            Success = LockControl(Entity, td, Ctrl);
                        if (!Success && Ctrl.Controls.Count > 0)
                            _LockForm(Entity, td, Ctrl);
                    }
                }
                public static bool LockControl(ParentEntity Entity, TableDef td, Control Ctrl)
                {
                    GridControl gc = Ctrl as GridControl;
                    if (gc != null)
                    {
                        LockGridControl(Entity, td, gc);
                        return true;
                    }
                    if (Ctrl as IAutoLockControl != null)
                    {
                        switch (Entity.FormMode)
                        {
                            case FormMode.FormAddNew:
                            case FormMode.FormEdit:
                                Ctrl.Enabled = true;
                                break;
                            default:
                                Ctrl.Enabled = false;
                                break;
                        }
                        return true;
                    }
                    if (Ctrl as IGridRowMover != null)
                    {
                        BindingSource bs = (BindingSource)((IGridRowMover)Ctrl)
                            .GridControl.DataSource;
                        string ChildName;
                        if (bs.DataMember.Length > 0)
                            ChildName = bs.DataMember;
                        else
                        {
                            ChildName = ((IGridRowMover)Ctrl)
                                .GridControl.DataMember;
                            if (ChildName.Length == 0)
                                ChildName = ((IEntityCollection)bs.DataSource).ChildName;
                        }

                        switch (Entity.FormMode)
                        {
                            case FormMode.FormAddNew:
                            case FormMode.FormEdit:
                                Ctrl.Visible = Entity.IsChildVisible(ChildName);
                                Ctrl.Enabled = !Entity.IsChildReadOnly(ChildName);
                                if (Ctrl.Enabled)
                                {
                                    IGridRowMover grm = Ctrl as IGridRowMover;
                                    grm.ScrollEnabled = Entity.IsChildAllowMovingRow(ChildName);
                                    grm.DeleteRowEnabled = Entity.IsChildAllowDeleteRow(ChildName);
                                }
                                break;
                            default:
                                Ctrl.Visible = Entity.IsChildVisible(ChildName);
                                Ctrl.Enabled = false;
                                break;
                        }
                        return true;
                    }

                    try
                    {
                        bool doLock = false;
                        bool IsEditable = Entity.FormMode == FormMode.FormAddNew ||
                            Entity.FormMode == FormMode.FormEdit;

                        foreach (Binding bnd in Ctrl.DataBindings)
                            if (bnd.PropertyName == "EditValue" || 
                                bnd.PropertyName == "Text")
                            {
                                doLock = true;
                                FieldDef fld =  Utility.GetBindingFieldDef(td,
                                    bnd.BindingMemberInfo);
                                bool IsReadOnly = !IsEditable;
                                if (fld != null)
                                {
                                    bool IsVisibleBound = false;
                                    foreach (Binding bx in Ctrl.DataBindings)
                                        if (bx.PropertyName == "Visible")
                                        {
                                            IsVisibleBound = true;
                                            break;
                                        }
                                    if (!IsVisibleBound)
                                        Ctrl.Visible = !fld.IsFormHidden &&
                                            Entity.IsVisible(fld.FieldName);
                                    if (IsEditable)
                                    {
                                        IsReadOnly = fld.IsReadOnly ||
                                            Entity.IsReadOnly(fld.FieldName);
                                    }
                                }
                                else
                                {
                                    bool IsVisibleBound = false;
                                    foreach (Binding bx in Ctrl.DataBindings)
                                        if (bx.PropertyName == "Visible")
                                        {
                                            IsVisibleBound = true;
                                            break;
                                        }
                                    if (!IsVisibleBound)
                                        Ctrl.Visible = Utility.GetEntity(Entity, 
                                            bnd.BindingMemberInfo).IsVisible(
                                            bnd.BindingMemberInfo.BindingField);
                                    if (IsEditable)
                                    {
                                        string PropName = bnd.BindingMemberInfo
                                            .BindingField;
                                        PropertyInfo pi = Utility.GetEntity(Entity,
                                            bnd.BindingMemberInfo).GetType()
                                            .GetProperty(PropName);
                                        if (pi != null)
                                        {
                                            IsReadOnly = !pi.CanWrite ||
                                                Entity.IsReadOnly(PropName);
                                        }
                                        else
                                            IsReadOnly = Utility.GetEntity(Entity,
                                                bnd.BindingMemberInfo).IsReadOnly(PropName);
                                    }
                                }

                                System.Windows.Forms.ComboBox cbb =
                                    Ctrl as System.Windows.Forms.ComboBox;
                                if (cbb != null)
                                {
                                    cbb.Enabled = !IsReadOnly;
                                    return true;
                                }

                                PropertyInfo prop = Ctrl.GetType()
                                    .GetProperty("ReadOnly");
                                if (prop != null)
                                {
                                    prop.SetValue(Ctrl, IsReadOnly, null);
                                    return true;
                                }
                                PropertyInfo pinfo = Ctrl.GetType().GetProperty(
                                    "Properties", typeof(RepositoryItem));
                                if (pinfo != null)
                                {
                                    RepositoryItem Props = pinfo.GetValue(Ctrl, null) as RepositoryItem;
                                    if (Props != null)
                                    {
                                        Props.ReadOnly = IsReadOnly;
                                        prop = Props.GetType().GetProperty("Buttons");
                                        if (prop != null)
                                        {
                                            foreach (EditorButton btn in
                                                (EditorButtonCollection)prop.GetValue(Props, null))
                                                if (btn.Tag as string == null ||
                                                    !btn.Tag.ToString().Contains("SkipLock"))
                                                    btn.Enabled = !IsReadOnly;
                                            return true;
                                        }
                                    }
                                }
                                break;
                            }
                            else if (bnd.PropertyName == "Visible")
                                Ctrl.Visible = (bool)((BaseEntity)Entity)[
                                    bnd.BindingMemberInfo.BindingField];

                        if (!doLock)
                        {
                            ButtonEdit be = Ctrl as ButtonEdit;
                            if (be != null)
                            {
                                if (be.Tag as string == null ||
                                    !be.Tag.ToString().Contains("SkipLock"))
                                    be.Properties.ReadOnly = !IsEditable;
                                foreach (EditorButton btn in be.Properties.Buttons)
                                    if (btn.Tag as string == null ||
                                        !btn.Tag.ToString().Contains("SkipLock"))
                                        btn.Enabled = IsEditable;
                                return true;
                            }

                            ComboBoxEdit cmb = Ctrl as ComboBoxEdit;
                            if (cmb != null)
                            {
                                if (cmb.Tag as string == null ||
                                    !cmb.Tag.ToString().Contains("SkipLock"))
                                    cmb.Properties.ReadOnly = !IsEditable;
                                foreach (EditorButton btn in cmb.Properties.Buttons)
                                    if (btn.Tag as string == null ||
                                        !btn.Tag.ToString().Contains("SkipLock"))
                                        btn.Enabled = IsEditable;
                                return true;
                            }

                            if (Ctrl.Tag as string == null ||
                                !Ctrl.Tag.ToString().Contains("SkipLock"))
                            {
                                PropertyInfo prop = Ctrl.GetType()
                                    .GetProperty("ReadOnly");
                                if (prop != null)
                                {
                                    prop.SetValue(Ctrl, !IsEditable, null);
                                    return true;
                                }
                                if (Ctrl as GridControl != null)
                                {
                                    prop = Ctrl.GetType().GetProperty("Enabled");
                                    if (prop != null)
                                    {
                                        prop.SetValue(Ctrl, IsEditable, null);
                                        return !IsEditable || !Ctrl.HasChildren;
                                    }
                                }

                                PropertyInfo pinfo = Ctrl.GetType().GetProperty("Properties",
                                    typeof(RepositoryItem));
                                if (pinfo != null)
                                {
                                    RepositoryItem Props = pinfo.GetValue(Ctrl, null) as RepositoryItem;
                                    if (Props != null)
                                    {
                                        Props.ReadOnly = !IsEditable;
                                        prop = Props.GetType().GetProperty("Buttons");
                                        if (prop != null)
                                        {
                                            foreach (EditorButton btn in
                                                (EditorButtonCollection)prop.GetValue(Props, null))
                                                btn.Enabled = IsEditable;
                                            return true;
                                        }
                                    }
                                }

                                System.Windows.Forms.ComboBox cbb =
                                    Ctrl as System.Windows.Forms.ComboBox;

                                if (cbb != null)
                                {
                                    cbb.Enabled = IsEditable;
                                    return true;
                                }
                            }
                        }
                    }
                    catch { }
                    return false;
                }
                public static void LockGridControl(ParentEntity Entity, TableDef td, GridControl gc)
                {
                    gc.BeginUpdate();
                    try
                    {
                        GridView vw = (GridView)gc.MainView;

                        EntityCollDef gridEcd = null;
                        string FieldName = ((BindingSource)gc.DataSource).DataMember;
                        if (FieldName.Length > 0)
                        {
                            foreach (EntityCollDef ecd in td.ChildEntities)
                                if (ecd.FieldName == FieldName)
                                {
                                    gridEcd = ecd;
                                    break;
                                }
                        }
                        else
                        {
                            IEntityCollection iec = ((BindingSource)
                                gc.DataSource).DataSource as IEntityCollection;
                            if (iec == null)
                            {
                                if (gc.DataMember.Length > 0)
                                    FieldName = gc.DataMember;
                            }
                            else
                                FieldName = iec.ChildName;

                            if (FieldName.Length > 0)
                                foreach (EntityCollDef ecd in td.ChildEntities)
                                    if (ecd.FieldName == FieldName)
                                    {
                                        gridEcd = ecd;
                                        break;
                                    }
                        }
                        if (gridEcd == null) return;
                        bool IsEditable = Entity.FormMode == FormMode.FormAddNew ||
                            Entity.FormMode == FormMode.FormEdit;

                        bool IsVisibleBound = false;
                        foreach (Binding bx in gc.DataBindings)
                            if (bx.PropertyName == "Visible")
                            {
                                IsVisibleBound = true;
                                break;
                            }
                        if (!IsVisibleBound)
                            gc.Visible = Entity.IsChildVisible(FieldName);

                        if (IsEditable)
                            vw.OptionsBehavior.Editable = !Entity.IsChildReadOnly(FieldName);
                        else
                            vw.OptionsBehavior.Editable = false;

                        if (vw.Editable && Entity.IsChildAllowNew(FieldName))
                            vw.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                        else
                            vw.OptionsView.NewItemRowPosition = NewItemRowPosition.None;

                        TableDef childTd = gridEcd.GetTableDef();

                        int LastVisibleIndex = 0;
                        foreach (GridColumn Col in vw.Columns)
                            if (Col.Tag == null || Col.Tag.ToString().IndexOf("SkipLock") < 0)
                            {
                                FieldDef fld = childTd.GetFieldDef(Col.FieldName);
                                if (fld == null) continue;

                                if (fld.IsFormHidden || !Entity
                                    .IsChildColumnVisible(
                                    FieldName, Col.FieldName))
                                {
                                    Col.VisibleIndex = -1;
                                    continue;
                                }
                                if (!Col.Visible)
                                    Col.VisibleIndex = LastVisibleIndex;
                                LastVisibleIndex++;
                                if (IsEditable)
                                    Col.OptionsColumn.ReadOnly =
                                        Entity.IsChildColumnReadOnly(
                                        FieldName, Col.FieldName);
                                else
                                    Col.OptionsColumn.ReadOnly = true;
                            }
                    }
                    catch { }
                    finally
                    {
                        try
                        {
                            gc.EndUpdate();
                        }
                        catch { }
                    }
                }

                internal static void LockForm(IReportEntity Entity, Control Parent)
                {
                    try
                    {
                        Parent.SuspendLayout();
                        foreach (Control Ctrl in Parent.Controls)
                        {
                            bool Success = false;
                            if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                            if (Ctrl.Tag == null || Ctrl.Tag != null &&
                                Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                                Ctrl.DataBindings.Count > 0 &&
                                Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                                Success = LockControl(Entity, Ctrl);
                            if (!Success && Ctrl.Controls.Count > 0)
                                _LockForm(Entity, Ctrl);
                        }
                    }
                    finally
                    {
                        Parent.ResumeLayout();
                    }
                }
                private static void _LockForm(IReportEntity Entity, Control Parent)
                {
                    foreach (Control Ctrl in Parent.Controls)
                    {
                        bool Success = false;
                        if (object.ReferenceEquals(Ctrl.GetType(), UINaviType)) continue;
                        if (Ctrl.Tag == null || Ctrl.Tag != null &&
                            Ctrl.Tag.ToString().IndexOf("AutoLock") >= 0 ||
                            Ctrl.DataBindings.Count > 0 &&
                            Ctrl.Tag.ToString().IndexOf("SkipLock") < 0)
                            Success = LockControl(Entity, Ctrl);
                        if (!Success && Ctrl.Controls.Count > 0)
                            _LockForm(Entity, Ctrl);
                    }
                }
                private static bool LockControl(IReportEntity Entity, Control Ctrl)
                {
                    try
                    {
                        bool doLock = false;
                        bool IsEditable = true;

                        foreach (Binding bnd in Ctrl.DataBindings)
                            if (bnd.PropertyName == "EditValue" ||
                                bnd.PropertyName == "Text")
                            {
                                doLock = true;
                                bool IsReadOnly = false;

                                bool IsVisibleBound = false;
                                foreach (Binding bx in Ctrl.DataBindings)
                                    if (bx.PropertyName == "Visible")
                                    {
                                        IsVisibleBound = true;
                                        break;
                                    }
                                if (!IsVisibleBound)
                                    Ctrl.Visible = Entity.IsVisible(
                                        bnd.BindingMemberInfo.BindingField);
                                if (IsEditable)
                                {
                                    string PropName = bnd.BindingMemberInfo
                                        .BindingField;
                                    PropertyInfo pi = Entity.GetType()
                                        .GetProperty(PropName);
                                    if (pi != null)
                                    {
                                        IsReadOnly = !pi.CanWrite ||
                                            Entity.IsReadOnly(PropName);
                                    }
                                    else
                                        IsReadOnly = Entity.IsReadOnly(PropName);
                                }

                                System.Windows.Forms.ComboBox cbb =
                                    Ctrl as System.Windows.Forms.ComboBox;
                                if (cbb != null)
                                {
                                    cbb.Enabled = !IsReadOnly;
                                    return true;
                                }

                                PropertyInfo prop = Ctrl.GetType()
                                    .GetProperty("ReadOnly");
                                if (prop != null)
                                {
                                    prop.SetValue(Ctrl, IsReadOnly, null);
                                    return true;
                                }
                                PropertyInfo pinfo = Ctrl.GetType().GetProperty(
                                    "Properties", typeof(RepositoryItem));
                                if (pinfo != null)
                                {
                                    RepositoryItem Props = pinfo.GetValue(Ctrl, null) as RepositoryItem;
                                    if (Props != null)
                                    {
                                        Props.ReadOnly = IsReadOnly;
                                        prop = Props.GetType().GetProperty("Buttons");
                                        if (prop != null)
                                        {
                                            foreach (EditorButton btn in
                                                (EditorButtonCollection)prop.GetValue(Props, null))
                                                if (btn.Tag as string == null ||
                                                    !btn.Tag.ToString().Contains("SkipLock"))
                                                    btn.Enabled = !IsReadOnly;
                                            return true;
                                        }
                                    }
                                }
                                break;
                            }
                            else if (bnd.PropertyName == "Visible")
                                Ctrl.Visible = (bool)((BaseEntity)Entity)[
                                    bnd.BindingMemberInfo.BindingField];

                        if (!doLock)
                        {
                            ButtonEdit be = Ctrl as ButtonEdit;
                            if (be != null)
                            {
                                be.Properties.ReadOnly = !IsEditable;
                                foreach (EditorButton btn in be.Properties.Buttons)
                                    if (btn.Tag as string == null ||
                                        !btn.Tag.ToString().Contains("SkipLock"))
                                        btn.Enabled = IsEditable;
                                return true;
                            }

                            ComboBoxEdit cmb = Ctrl as ComboBoxEdit;
                            if (cmb != null)
                            {
                                cmb.Properties.ReadOnly = !IsEditable;
                                foreach (EditorButton btn in cmb.Properties.Buttons)
                                    btn.Enabled = IsEditable;
                                return true;
                            }

                            PropertyInfo prop = Ctrl.GetType()
                                .GetProperty("ReadOnly");
                            if (prop != null)
                            {
                                prop.SetValue(Ctrl, !IsEditable, null);
                                return true;
                            }
                            PropertyInfo pinfo = Ctrl.GetType().GetProperty("Properties",
                                typeof(RepositoryItem));
                            if (pinfo != null)
                            {
                                RepositoryItem Props = pinfo.GetValue(Ctrl, null) as RepositoryItem;
                                if (Props != null)
                                {
                                    Props.ReadOnly = !IsEditable;
                                    prop = Props.GetType().GetProperty("Buttons");
                                    if (prop != null)
                                    {
                                        foreach (EditorButton btn in
                                            (EditorButtonCollection)prop.GetValue(Props, null))
                                            btn.Enabled = IsEditable;
                                        return true;
                                    }
                                }
                            }
                            System.Windows.Forms.ComboBox cbb =
                                Ctrl as System.Windows.Forms.ComboBox;
                            if (cbb != null)
                            {
                                cbb.Enabled = IsEditable;
                                return true;
                            }
                        }
                    }
                    catch { }
                    return false;
                }
            }

            //[DebuggerNonUserCode]
            public static class AutoFormat
            {
                public static int GridBestFitMaxRowCount = 100;

                /// <summary>
                /// Melakukan AutoFormat pada seluruh kontrol pada form
                /// yang binding pada Persistance Entity
                /// </summary>
                /// <param name="Parent"></param>
                public static void AutoFormatForm(Control Parent, bool AutoResizeGridColumn)
                {
                    try
                    {
                        Parent.SuspendLayout();
                        foreach (Control Ctrl in Parent.Controls)
                        {
                            AutoFormatControl(Ctrl, AutoResizeGridColumn);
                            if (Ctrl.Controls.Count > 0)
                                _AutoFormatForm(Ctrl, AutoResizeGridColumn);
                        }
                    }
                    finally
                    {
                        Parent.ResumeLayout();
                    }
                }

                private static void _AutoFormatForm(Control Parent, bool AutoResizeGridColumn)
                {
                    foreach (Control Ctrl in Parent.Controls)
                    {
                        AutoFormatControl(Ctrl, AutoResizeGridColumn);
                        if (Ctrl.Controls.Count > 0)
                            _AutoFormatForm(Ctrl, AutoResizeGridColumn);
                    }
                }

                public static void AutoFormatControl(Control Ctrl, bool AutoResizeGridColumn)
                {
                    string strTag = Ctrl.Tag as string;
                    if (strTag != null && strTag.IndexOf("SkipFormat",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                        return;

                    LookUpEdit lue = Ctrl as LookUpEdit;
                    if (lue != null)
                    {
                        AutoFormatLookUpEditControl(lue);
                        return;
                    }
                    GridControl gc = Ctrl as GridControl;
                    if (gc != null)
                    {
                        BaseWinFramework.WinForm.AutoFormat.AutoFormatGridControl(gc, AutoResizeGridColumn);
                        return;
                    }
                    if (Ctrl.DataBindings.Count == 0) return;
                    Binding bnd = Ctrl.DataBindings[0];
                    if (bnd.DataSourceUpdateMode == DataSourceUpdateMode.OnPropertyChanged)
                        Ctrl.CausesValidation = false;
                    BindingSource bnds = bnd.DataSource as BindingSource;
                    if (bnds == null) return;
                    Type tpEntity;
                    if (bnds.DataSource.GetType().Name == "RuntimeType")
                        tpEntity = (Type)bnds.DataSource;
                    else
                        tpEntity = bnds.DataSource.GetType();

                    TableDef td = MetaData.GetTableDef(tpEntity);
                    if (td == null) return;
                    FieldDef fld = td.GetFieldDef(bnd.BindingMemberInfo.BindingField);

                    #region fld == null
                    if (fld == null)
                    {
                        Type tp = Utility.GetFieldOrPropType(
                            tpEntity, bnd.BindingMemberInfo);
                        if (tp == null)
                            throw new ApplicationException(string.Concat(
                                "Nama Field '", bnd.BindingMemberInfo.BindingField,
                                "' tidak ada dalam kelas '", td.ClassType, "'"));

                        if (tp == typeof(decimal))
                        {
                            if ((Ctrl as SpinEdit) != null ||
                                (Ctrl as CalcEdit) != null)
                            {
                                RepositoryItem r = (Ctrl as BaseEdit).Properties;
                                if (r.DisplayFormat.FormatString.Length == 0)
                                {
                                    r.DisplayFormat.FormatType = FormatType.Custom;
                                    r.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                }
                                if (r.EditFormat.FormatString.Length == 0)
                                {
                                    r.EditFormat.FormatType = FormatType.Custom;
                                    r.EditFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                }
                            }
                            return;
                        }
                        if (tp == typeof(int))
                        {
                            SpinEdit spe = Ctrl as SpinEdit;
                            if (spe != null ||
                                (Ctrl as CalcEdit) != null)
                            {
                                RepositoryItem r = (Ctrl as BaseEdit).Properties;
                                if (r.DisplayFormat.FormatString.Length == 0)
                                {
                                    r.DisplayFormat.FormatType = FormatType.Custom;
                                    r.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                }
                                if (r.EditFormat.FormatString.Length == 0)
                                {
                                    r.EditFormat.FormatType = FormatType.Custom;
                                    r.EditFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                }
                            }
                            if (spe != null)
                                spe.Properties.IsFloatValue = false;
                            return;
                        }
                        if (tp == typeof(DateTime))
                        {
                            DateEdit de = Ctrl as DateEdit;
                            if (de != null)
                            {
                                if (de.Properties.DisplayFormat.FormatString.Length < 2)
                                {
                                    de.Properties.DisplayFormat.Format = BaseWinFramework.ZeroDateFormatter;
                                    de.Properties.DisplayFormat.FormatType = FormatType.Custom;
                                    de.Properties.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;
                                    de.Properties.EditMask = BaseUtility.DefaultFormatDate;
                                }
                            }
                            return;
                        }
                        if (tp.IsEnum)
                        {
                            ComboBoxEdit cb = Ctrl as ComboBoxEdit;
                            if (cb != null)
                            {
                                cb.EditValueChanged += new EventHandler(cb_EditValueChanged);
                                cb.Tag = tp;
                                RepositoryItemComboBox props = cb.Properties;
                                props.DisplayFormat.FormatType = FormatType.Custom;
                                props.DisplayFormat.Format = DisplayEnumFormatter;
                                //props.EditFormat.FormatType = FormatType.Custom;
                                //props.EditFormat.Format = EditEnumFormatter;

                                props.Items.Clear();
                                props.Items.AddRange(EnumDef.GetEnumNames(tp));
                                props.TextEditStyle = TextEditStyles.DisableTextEditor;
                                return;
                            }
                            TextEdit tex = Ctrl as TextEdit;
                            if (tex != null)
                            {
                                tex.Tag = tp;

                                tex.Properties.DisplayFormat.FormatType = FormatType.Custom;
                                tex.Properties.DisplayFormat.Format = DisplayEnumFormatter;
                                tex.Properties.EditFormat.FormatType = FormatType.Custom;
                                tex.Properties.EditFormat.Format = DisplayEnumFormatter;
                                return;
                            }

                            System.Windows.Forms.ComboBox cbb =
                                Ctrl as System.Windows.Forms.ComboBox;
                            if (cbb != null)
                            {
                                foreach (Binding dataBnd in cbb.DataBindings)
                                    if (bnd.PropertyName == "EditValue")
                                    {
                                        dataBnd.FormatInfo = DisplayEnumFormatter;
                                        dataBnd.Parse += new ConvertEventHandler(AutoFormat_Parse);
                                        break;
                                    }

                                cbb.Items.Clear();
                                foreach (string Item in EnumDef.GetEnumNames(tp))
                                    cbb.Items.Add(Item);
                                if (cbb.DropDownStyle == ComboBoxStyle.DropDown)
                                    cbb.DropDownStyle = ComboBoxStyle.DropDownList;
                                return;
                            }
                        }
                        return;
                    }
                    #endregion

                    switch (fld.DataType)
                    {
                        case DataType.VarChar:
                        case DataType.Char:
                            TextEdit te = Ctrl as TextEdit;
                            if (te != null)
                            {
                                te.Properties.MaxLength = fld.Length;
                                if (te.Properties.ReadOnly)
                                    te.Tag = "SkipLock";
                            }
                            else
                            {
                                TextBox tb = Ctrl as TextBox;
                                if (tb != null)
                                {
                                    tb.MaxLength = fld.Length;
                                    if (tb.ReadOnly) tb.Tag = "SkipLock";
                                }
                            }
                            if (fld.IsEnum)
                            {
                                #region Enum Presentation
                                ComboBoxEdit cb = Ctrl as ComboBoxEdit;
                                if (cb != null)
                                {
                                    cb.EditValueChanged += new EventHandler(cb_EditValueChanged);

                                    cb.Tag = fld.FieldType;
                                    RepositoryItemComboBox props = cb.Properties;
                                    props.DisplayFormat.FormatType = FormatType.Custom;
                                    props.DisplayFormat.Format = DisplayEnumFormatter;
                                    //props.EditFormat.FormatType = FormatType.Custom;
                                    //props.EditFormat.Format = EditEnumFormatter;

                                    props.Items.Clear();
                                    props.Items.AddRange(fld.GetEnumNames());
                                    props.TextEditStyle = TextEditStyles.DisableTextEditor;
                                    return;
                                }

                                TextEdit tex = Ctrl as TextEdit;
                                if (tex != null)
                                {
                                    tex.EditValueChanged += new EventHandler(cb_EditValueChanged);
                                    tex.Tag = fld.FieldType;

                                    tex.Properties.DisplayFormat.FormatType = FormatType.Custom;
                                    tex.Properties.DisplayFormat.Format = DisplayEnumFormatter;
                                    tex.Properties.EditFormat.FormatType = FormatType.Custom;
                                    tex.Properties.EditFormat.Format = DisplayEnumFormatter;
                                    return;
                                }

                                System.Windows.Forms
                                    .ComboBox cbb = Ctrl as System
                                    .Windows.Forms.ComboBox;
                                if (cbb != null)
                                {
                                    foreach (Binding DataBnd in cbb.DataBindings)
                                        if (bnd.PropertyName == "Text")
                                        {
                                            DataBnd.FormatInfo = DisplayEnumFormatter;
                                            DataBnd.Parse += new ConvertEventHandler(AutoFormat_Parse);
                                            break;
                                        }
                                    cbb.MaxLength = fld.Length;
                                    cbb.Items.Clear();
                                    foreach (string Item in fld.GetEnumNames())
                                        cbb.Items.Add(Item);
                                    if (cbb.DropDownStyle == ComboBoxStyle.DropDown)
                                        cbb.DropDownStyle = ComboBoxStyle.DropDownList;
                                }
                                #endregion
                            }
                            break;
                        case DataType.Date:
                        case DataType.DateTime:
                        case DataType.TimeStamp:
                            DateEdit de = Ctrl as DateEdit;
                            if (de != null)
                            {
                                if (de.Properties.DisplayFormat.FormatString.Length < 2)
                                {
                                    de.Properties.DisplayFormat.Format = BaseWinFramework.ZeroDateFormatter;
                                    de.Properties.DisplayFormat.FormatType = FormatType.Custom;
                                    de.Properties.DisplayFormat.FormatString = fld.FormatString;
                                    de.Properties.EditMask = fld.FormatString;
                                }
                                if (de.Properties.ReadOnly) de.Tag = "SkipLock";
                            }
                            break;
                        case DataType.Time:
                            TimeEdit tie = Ctrl as TimeEdit;
                            if (tie != null)
                            {
                                if (tie.Properties.DisplayFormat.FormatString.Length == 0)
                                {
                                    tie.Properties.DisplayFormat.FormatType = FormatType.Custom;
                                    tie.Properties.DisplayFormat.FormatString = fld.FormatString;
                                    tie.Properties.EditMask = fld.FormatString;
                                }
                                if (tie.Properties.ReadOnly) tie.Tag = "SkipLock";
                            }
                            break;
                        case DataType.Decimal:
                            if ((Ctrl as SpinEdit) != null ||
                                (Ctrl as CalcEdit) != null)
                            {
                                RepositoryItem r = (Ctrl as BaseEdit).Properties;
                                if (r.DisplayFormat.FormatString.Length == 0)
                                {
                                    r.DisplayFormat.FormatType = FormatType.Custom;
                                    r.DisplayFormat.FormatString = fld.FormatString;
                                }
                                if (r.EditFormat.FormatString.Length == 0)
                                {
                                    r.EditFormat.FormatType = FormatType.Custom;
                                    r.EditFormat.FormatString = fld.FormatString;
                                }

                                if (r.ReadOnly) r.Tag = "SkipLock";
                            }
                            break;
                        case DataType.Integer:
                            SpinEdit spe = Ctrl as SpinEdit;
                            if (spe != null ||
                                (Ctrl as CalcEdit) != null)
                            {
                                RepositoryItem r = (Ctrl as BaseEdit).Properties;
                                if (r.DisplayFormat.FormatString.Length == 0)
                                {
                                    r.DisplayFormat.FormatType = FormatType.Custom;
                                    r.DisplayFormat.FormatString = fld.FormatString;
                                }
                                if (r.EditFormat.FormatString.Length == 0)
                                {
                                    r.EditFormat.FormatType = FormatType.Custom;
                                    r.EditFormat.FormatString = fld.FormatString;
                                }

                                if (r.ReadOnly) r.Tag = "SkipLock";
                            }
                            if (spe != null)
                                spe.Properties.IsFloatValue = false;
                            break;
                        case DataType.Boolean:
                            if ((Ctrl as System.Windows.Forms.Label) != null ||
                                (Ctrl as LabelControl) != null)
                            {
                                bnd.Format += new ConvertEventHandler(bnd_Format);
                                Ctrl.Tag = fld.FormatString;
                            }
                            break;
                    }
                }

                static void cb_EditValueChanged(object sender, EventArgs e)
                {
                    string TmpStr = ((ComboBoxEdit)sender).EditValue as string;
                    if (TmpStr == null) return;

                    ((ComboBoxEdit)sender).EditValue = EnumDef.GetEnumValue((Type)
                            ((Control)sender).Tag, TmpStr);
                }

                static void AutoFormat_Parse(object sender, ConvertEventArgs e)
                {
                    if (e.Value == null) return;
                    if (e.Value.GetType() == typeof(string))
                        e.Value = EnumDef.GetEnumValue(e.DesiredType, e.Value);
                }

                // untuk grid combo box
                static void cb2_ParseEditValue(object sender, ConvertEditValueEventArgs e)
                {
                    // string -> enum
                    if (e.Value == null) return;
                    if (e.Value.GetType() == typeof(string))
                    {
                        e.Value = EnumDef.GetEnumValue((Type)
                            ((RepositoryItemComboBox)((ComboBoxEdit)
                            sender).Tag).Tag, e.Value);
                        e.Handled = true;
                    }
                }

                private static void bnd_Format(object sender, ConvertEventArgs e)
                {
                    if (e.DesiredType == typeof(string))
                    {
                        string[] strBool = ((string)((Binding)sender).Control.Tag).Split(';');
                        e.Value = (bool)e.Value ? strBool[0] : strBool[1];
                    }
                }

                public static void AutoFormatLookUpEditControl(LookUpEdit lue)
                {
                    if (lue.Properties.PopupWidth < 350)
                        lue.Properties.PopupWidth = 350;
                    lue.Properties.NullText = string.Empty;
                    Type ObjType = GetTypeFromDataSource(lue.Properties.DataSource);
                    if (ObjType != null) AutoLookUpEditColumn(ObjType, lue);
                }

                private static void AutoLookUpEditColumn(Type EntityType, LookUpEdit lue)
                {
                    TableDef td = MetaData.GetTableDef(EntityType);
                    foreach (LookUpColumnInfo lc in lue.Properties.Columns)
                    {
                        if (lc.Caption.Length == 0) lc.Caption =
                            BaseUtility.SplitName(lc.FieldName);
                        else
                            lc.Caption = BaseUtility.SplitName(lc.Caption);

                        if (lc.FormatString.Length > 0) continue;
                        FieldDef fld = td.GetFieldDef(lc.FieldName);
                        lc.FormatType = FormatType.Custom;
                        if (fld != null)
                        {
                            lc.FormatString = fld.FormatString;
                            switch (fld.DataType)
                            {
                                case DataType.Integer:
                                case DataType.Decimal:
                                    lc.Alignment = HorzAlignment.Far;
                                    break;
                            }
                        }
                        else
                        {
                            Type tp = Utility.GetFieldOrPropType(
                                EntityType, lc.FieldName);
                            if (tp == typeof(decimal))
                            {
                                lc.Alignment = HorzAlignment.Far;
                                lc.FormatString = BaseUtility.DefaultFormatDecimal;
                            }
                            else if (tp == typeof(int))
                            {
                                lc.Alignment = HorzAlignment.Far;
                                lc.FormatString = BaseUtility.DefaultFormatInteger;
                            }
                            else if (tp == typeof(DateTime))
                                lc.FormatString = BaseUtility.DefaultFormatDate;
                        }
                    }
                }

                public static void AutoFormatGridControl(GridControl gc, bool AutoResizeColumn)
                {
                    foreach (GridView gv in gc.ViewCollection)
                    {
                        Type tp = GetTypeFromDataSource(gc.DataSource);
                        if (gc.DataMember.Length > 0)
                        {
                            tp = Utility.GetFieldOrPropType(tp, gc.DataMember);
                            if (tp.IsGenericType)
                                tp = tp.GetGenericArguments()[0];
                        }
                        if (gv.LevelName.Length > 0)
                        {
                            tp = Utility.GetFieldOrPropType(tp, gv.LevelName);
                            if (tp.IsGenericType)
                                tp = tp.GetGenericArguments()[0];
                        }
                        AutoFormatGridView(tp, gv, AutoResizeColumn);
                    }
                }
                public static void AutoFormatGridView(GridView GridView, bool AutoResizeColumn)
                {
                    GridView.OptionsNavigation.UseTabKey = false;

                    foreach (GridColumn gc in GridView.Columns)
                    {
                        gc.Caption = BaseUtility.SplitName(gc.Caption);

                        RepositoryItemLookUpEditBase rle = gc.ColumnEdit
                            as RepositoryItemLookUpEditBase;
                        if (rle != null)
                        {
                            rle.NullText = string.Empty;
                            foreach (EditorButton btn in rle.Buttons)
                                switch (btn.Kind)
                                {
                                    case ButtonPredefines.Plus:
                                        btn.Shortcut = new KeyShortcut(Keys.Control | Keys.T);
                                        break;
                                    case ButtonPredefines.Redo:
                                        btn.Shortcut = new KeyShortcut(Keys.Control | Keys.R);
                                        break;
                                }

                            RepositoryItemLookUpEdit rile = rle as RepositoryItemLookUpEdit;
                            if (rile != null)
                            {
                                foreach (LookUpColumnInfo c in rile.Columns)
                                    if (c.Caption.Length == 0)
                                        c.Caption = BaseUtility.SplitName(c.FieldName);
                            }
                            else
                            {
                                RepositoryItemGridLookUpEdit rgle = 
                                    rle as RepositoryItemGridLookUpEdit;
                                if (rgle != null)
                                    foreach (GridColumn ggc in rgle.View.Columns)
                                        if (ggc.Caption.Length == 0)
                                            ggc.Caption = BaseUtility.SplitName(ggc.FieldName);
                            }
                        }

                        Type tp = gc.ColumnType;

                        if (tp == typeof(bool))
                        {
                            gc.AppearanceHeader.TextOptions
                                .HAlignment = HorzAlignment.Center;
                        }
                        else if (tp == typeof(DateTime))
                        {
                            if (gc.DisplayFormat.FormatString.Length == 0)
                            {
                                gc.DisplayFormat.Format = ZeroDateFormatter;
                                gc.DisplayFormat.FormatType = FormatType.Custom;
                                gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;

                                if (gc.ColumnEdit == null)
                                {
                                    RepositoryItemDateEdit ride = new RepositoryItemDateEdit();
                                    ride.DisplayFormat.Format = ZeroDateFormatter;
                                    //ride.DisplayFormat.FormatType = FormatType.Custom;
                                    //ride.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;
                                    ride.EditFormat.Format = ZeroDateFormatter;
                                    //ride.EditFormat.FormatType = FormatType.Custom;
                                    //ride.EditFormat.FormatString = BaseUtility.DefaultFormatDate;
                                    ride.EditMask = BaseUtility.DefaultFormatDate;
                                    //ride.NullDate = new DateTime(1900, 1, 1);
                                    ride.ShowClear = false;
                                    gc.ColumnEdit = ride;
                                }
                            }
                        }
                        else if (tp == typeof(decimal) ||
                          tp == typeof(Single) ||
                          tp == typeof(double))
                        {
                            if (gc.DisplayFormat.FormatString.Length == 0)
                            {
                                gc.DisplayFormat.FormatType = FormatType.Custom;
                                gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                gc.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Far;

                                if (gc.ColumnEdit == null)
                                {
                                    RepositoryItemCalcEdit rice = new RepositoryItemCalcEdit();
                                    rice.DisplayFormat.FormatType = FormatType.Custom;
                                    rice.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                    rice.EditFormat.FormatType = FormatType.Custom;
                                    rice.EditFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                    rice.MouseUp += new MouseEventHandler(rice_MouseUp);
                                    rice.QueryCloseUp += new CancelEventHandler(rice_QueryCloseUp);
                                    rice.Tag = GridView;
                                    gc.ColumnEdit = rice;
                                }
                            }
                        }
                        else if (tp == typeof(int))
                        {
                            if (gc.DisplayFormat.FormatString.Length == 0)
                            {
                                gc.DisplayFormat.FormatType = FormatType.Custom;
                                gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                gc.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Far;

                                if (gc.ColumnEdit == null)
                                {
                                    RepositoryItemCalcEdit rice = new RepositoryItemCalcEdit();
                                    rice.DisplayFormat.FormatType = FormatType.Custom;
                                    rice.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                    rice.EditFormat.FormatType = FormatType.Custom;
                                    rice.EditFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                    rice.MouseUp += new MouseEventHandler(rice_MouseUp);
                                    rice.QueryCloseUp += new CancelEventHandler(rice_QueryCloseUp);
                                    rice.Tag = GridView;
                                    gc.ColumnEdit = rice;
                                }
                            }
                        }
                    }
                    if (AutoResizeColumn)
                    {
                        int OldMaxBestFit = GridView.BestFitMaxRowCount;
                        GridView.BestFitMaxRowCount = GridBestFitMaxRowCount;
                        GridView.BestFitColumns();
                        GridView.BestFitMaxRowCount = OldMaxBestFit;
                    }
                }
                public static void AutoFormatGridView(Type EntityType, GridView GridView, bool AutoResizeColumn)
                {
                    GridView.OptionsNavigation.UseTabKey = false;

                    TableDef td = MetaData.GetTableDef(EntityType);
                    if (GridView.OptionsBehavior.Editable)
                    {
                        GridView.ShowingEditor += new CancelEventHandler(GridView_ShowingEditor);
                        GridView.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridView_CellValueChanging);
                    }

                    foreach (GridColumn col in GridView.Columns)
                    {
                        col.Caption = BaseUtility.SplitName(col.Caption);
                        FieldDef fld = td.GetFieldDef(col.FieldName);

                        RepositoryItemLookUpEditBase rle = col.ColumnEdit
                            as RepositoryItemLookUpEditBase;
                        if (rle != null)
                        {
                            rle.NullText = string.Empty;
                            foreach (EditorButton btn in rle.Buttons)
                                switch (btn.Kind)
                                {
                                    case ButtonPredefines.Plus:
                                        btn.Shortcut = new KeyShortcut(Keys.Control | Keys.T);
                                        break;
                                    case ButtonPredefines.Redo:
                                        btn.Shortcut = new KeyShortcut(Keys.Control | Keys.R);
                                        break;
                                }
                            RepositoryItemLookUpEdit rile = rle as RepositoryItemLookUpEdit;
                            if (rile != null)
                            {
                                foreach (LookUpColumnInfo c in rile.Columns)
                                    if (c.Caption.Length == 0)
                                        c.Caption = BaseUtility.SplitName(c.FieldName);
                            }
                            else
                            {
                                RepositoryItemGridLookUpEdit rgle =
                                    rle as RepositoryItemGridLookUpEdit;
                                if (rgle != null)
                                    foreach (GridColumn ggc in rgle.View.Columns)
                                        if (ggc.Caption.Length == 0)
                                            ggc.Caption = BaseUtility.SplitName(ggc.FieldName);
                            }
                        }
                        #region Format Column
                        if (fld == null)
                        {
                            Type tp;
                            try
                            {
                                tp = Utility.GetFieldOrPropType(EntityType,
                                    col.FieldName);
                            }
                            catch
                            {
                                tp = col.ColumnType;
                            }
                            if (tp.IsEnum)
                            {
                                if (col.ColumnEdit == null)
                                {
                                    RepositoryItemComboBox cb =
                                         new RepositoryItemComboBox();
                                    cb.TextEditStyle = TextEditStyles.DisableTextEditor;
                                    foreach (string Item in EnumDef.GetEnumNames(tp))
                                        cb.Items.Add(Item);
                                    cb.Tag = tp;
                                    cb.ParseEditValue += new ConvertEditValueEventHandler(cb2_ParseEditValue);
                                    cb.EditFormat.FormatType = FormatType.Custom;
                                    cb.EditFormat.Format = DisplayEnumFormatter;
                                    cb.DisplayFormat.FormatType = FormatType.Custom;
                                    cb.DisplayFormat.Format = DisplayEnumFormatter;
                                    col.ColumnEdit = cb;
                                }
                            } else if (tp == typeof(bool))
                                col.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Center;
                            else if (tp == typeof(DateTime))
                            {
                                if (col.DisplayFormat.FormatString.Length == 0)
                                {
                                    col.DisplayFormat.Format = ZeroDateFormatter;
                                    col.DisplayFormat.FormatType = FormatType.Custom;
                                    col.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;

                                    if (col.ColumnEdit == null)
                                    {
                                        RepositoryItemDateEdit ride = new RepositoryItemDateEdit();
                                        ride.DisplayFormat.Format = ZeroDateFormatter;
                                        //ride.DisplayFormat.FormatType = FormatType.Custom;
                                        //ride.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;
                                        ride.EditFormat.Format = ZeroDateFormatter;
                                        //ride.EditFormat.FormatType = FormatType.Custom;
                                        //ride.EditFormat.FormatString = BaseUtility.DefaultFormatDate;
                                        ride.EditMask = BaseUtility.DefaultFormatDate;
                                        //ride.NullDate = new DateTime(1900, 1, 1);
                                        ride.ShowClear = false;
                                        col.ColumnEdit = ride;
                                    }
                                }
                            }
                            else if (tp == typeof(decimal) ||
                                tp == typeof(double) ||
                                tp == typeof(Single))
                            {
                                if (col.ColumnEdit == null)
                                {
                                    if (col.DisplayFormat.FormatString.Length == 0)
                                    {
                                        col.DisplayFormat.FormatType = FormatType.Custom;
                                        col.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                        col.AppearanceHeader.TextOptions
                                            .HAlignment = HorzAlignment.Far;

                                        if (col.ColumnEdit == null)
                                        {
                                            RepositoryItemCalcEdit rice = new RepositoryItemCalcEdit();
                                            rice.DisplayFormat.FormatType = FormatType.Custom;
                                            rice.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                            rice.EditFormat.FormatType = FormatType.Custom;
                                            rice.EditFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                                            rice.MouseUp += new MouseEventHandler(rice_MouseUp);
                                            rice.QueryCloseUp += new CancelEventHandler(rice_QueryCloseUp);
                                            rice.Tag = GridView;
                                            col.ColumnEdit = rice;
                                        }
                                    }
                                }
                            }
                            else if (tp == typeof(int))
                            {
                                if (col.ColumnEdit == null)
                                {
                                    if (col.DisplayFormat.FormatString.Length == 0)
                                    {
                                        col.DisplayFormat.FormatType = FormatType.Custom;
                                        col.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                        col.AppearanceHeader.TextOptions
                                            .HAlignment = HorzAlignment.Far;

                                        if (col.ColumnEdit == null)
                                        {
                                            RepositoryItemCalcEdit rice = new RepositoryItemCalcEdit();
                                            rice.DisplayFormat.FormatType = FormatType.Custom;
                                            rice.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                            rice.EditFormat.FormatType = FormatType.Custom;
                                            rice.EditFormat.FormatString = BaseUtility.DefaultFormatInteger;
                                            rice.MouseUp += new MouseEventHandler(rice_MouseUp);
                                            rice.QueryCloseUp += new CancelEventHandler(rice_QueryCloseUp);
                                            rice.Tag = GridView;
                                            col.ColumnEdit = rice;
                                        }
                                    }
                                }
                            }
                        }
                        else
                            switch (fld.DataType)
                            {
                                case DataType.VarChar:
                                case DataType.Char:
                                    if (col.ColumnEdit == null)
                                    {
                                        if (fld.FieldType.IsEnum)
                                        {
                                            RepositoryItemComboBox cb =
                                                 new RepositoryItemComboBox();
                                            cb.TextEditStyle = TextEditStyles.DisableTextEditor;
                                            foreach (string Item in EnumDef
                                                .GetEnumNames(fld.FieldType))
                                                cb.Items.Add(Item);
                                            cb.Tag = fld.FieldType;
                                            cb.ParseEditValue +=new ConvertEditValueEventHandler(cb2_ParseEditValue);
                                            cb.EditFormat.FormatType = FormatType.Custom;
                                            cb.EditFormat.Format = DisplayEnumFormatter;
                                            cb.DisplayFormat.FormatType = FormatType.Custom;
                                            cb.DisplayFormat.Format = DisplayEnumFormatter;
                                            col.ColumnEdit = cb;
                                        }
                                        else
                                        {
                                            RepositoryItemTextEdit rte = new RepositoryItemTextEdit();
                                            rte.MaxLength = fld.Length;
                                            col.ColumnEdit = rte;
                                        }
                                    }
                                    break;
                                case DataType.Boolean:
                                    col.AppearanceHeader.TextOptions
                                        .HAlignment = HorzAlignment.Center;
                                    break;
                                case DataType.Date:
                                case DataType.DateTime:
                                case DataType.TimeStamp:
                                    if (col.DisplayFormat.FormatString.Length == 0)
                                    {
                                        col.DisplayFormat.Format = ZeroDateFormatter;
                                        col.DisplayFormat.FormatType = FormatType.Custom;
                                        col.DisplayFormat.FormatString = fld.FormatString;

                                        if (col.ColumnEdit == null)
                                        {
                                            RepositoryItemDateEdit ride = new RepositoryItemDateEdit();
                                            ride.DisplayFormat.Format = ZeroDateFormatter;
                                            //ride.DisplayFormat.FormatType = FormatType.Custom;
                                            //ride.DisplayFormat.FormatString = fld.FormatString;
                                            ride.EditFormat.Format = ZeroDateFormatter;
                                            //ride.EditFormat.FormatType = FormatType.Custom;
                                            //ride.EditFormat.FormatString = fld.FormatString;
                                            ride.EditMask = fld.FormatString;
                                            //ride.NullDate = new DateTime(1900, 1, 1);
                                            ride.ShowClear = false;
                                            col.ColumnEdit = ride;
                                        }
                                    }
                                    break;
                                case DataType.Time:
                                    if (col.ColumnEdit == null)
                                    {
                                        if (col.DisplayFormat.FormatString.Length == 0)
                                        {
                                            col.DisplayFormat.FormatType = FormatType.Custom;
                                            col.DisplayFormat.FormatString = fld.FormatString;

                                            if (col.ColumnEdit == null)
                                            {
                                                RepositoryItemTimeEdit rite = new RepositoryItemTimeEdit();
                                                rite.EditMask = fld.FormatString;
                                                col.ColumnEdit = rite;
                                            }
                                        }
                                    }
                                    break;
                                case DataType.Decimal:
                                case DataType.Integer:
                                    if (col.ColumnEdit == null)
                                    {
                                        if (col.DisplayFormat.FormatString.Length == 0)
                                        {
                                            col.DisplayFormat.FormatType = FormatType.Custom;
                                            col.DisplayFormat.FormatString = fld.FormatString;
                                            col.AppearanceHeader.TextOptions
                                                .HAlignment = HorzAlignment.Far;

                                            if (col.ColumnEdit == null)
                                            {
                                                RepositoryItemCalcEdit rice = new RepositoryItemCalcEdit();
                                                rice.DisplayFormat.FormatType = FormatType.Custom;
                                                rice.DisplayFormat.FormatString = fld.FormatString;
                                                rice.EditFormat.FormatType = FormatType.Custom;
                                                rice.EditFormat.FormatString = fld.FormatString;
                                                rice.MouseUp += new MouseEventHandler(rice_MouseUp);
                                                rice.QueryCloseUp += new CancelEventHandler(rice_QueryCloseUp);
                                                rice.Tag = GridView;
                                                col.ColumnEdit = rice;
                                            }
                                        }
                                    }
                                    break;
                            }
                        #endregion
                    }
                    if (AutoResizeColumn)
                    {
                        int OldMaxBestFit = GridView.BestFitMaxRowCount;
                        GridView.BestFitMaxRowCount = GridBestFitMaxRowCount;
                        GridView.BestFitColumns();
                        GridView.BestFitMaxRowCount = OldMaxBestFit;
                    }
                }

                private static bool OnCheck;
                private static void GridView_CellValueChanging(object sender, CellValueChangedEventArgs e)
                {
                    CheckEdit ce = ((GridView)sender).ActiveEditor as CheckEdit;
                    if (ce == null || OnCheck) return;

                    OnCheck = true;
                    try
                    {
                        ce.EditValue = e.Value;
                        ((GridView)sender).PostEditor();
                    }
                    finally
                    {
                        OnCheck = false;
                    }
                }

                private static void GridView_ShowingEditor(object sender, CancelEventArgs e)
                {
                    GridView gv = (GridView)sender;

                    BusinessEntity be = gv.GetRow(gv.FocusedRowHandle) as BusinessEntity;
                    if (be != null && be.IsReadOnly(gv.FocusedColumn.FieldName))
                        e.Cancel = true;
                }

                static void rice_QueryCloseUp(object sender, CancelEventArgs e)
                {
                    decimal TmpValue = decimal.Parse(((CalcEdit)sender).Text);
                    GridView gv = (GridView)((RepositoryItemCalcEdit)
                        ((CalcEdit)sender).Tag).Tag;
                    ((GridView)((RepositoryItemCalcEdit)
                        ((CalcEdit)sender).Tag).Tag).CloseEditor();
                    gv.ShowEditor();
                    gv.EditingValue = TmpValue;
                    gv.CloseEditor();
                }

                private static void rice_MouseUp(object sender, MouseEventArgs e)
                {
                    ((CalcEdit)sender).SelectAll();
                }

                public static void AutoFormatReadOnlyGridControl(GridControl gc, bool AutoResizeColumn)
                {
                    foreach (GridView gv in gc.ViewCollection)
                        AutoFormatReadOnlyGridView(GetTypeFromDataSource(gc.DataSource),
                            gv, AutoResizeColumn);
                }
                public static void AutoFormatReadOnlyGridView(Type EntityType, GridView GridView, bool AutoResizeColumn)
                {
                    if (EntityType == null || EntityType == typeof(DataTable))
                    {
                        AutoFormatGridView(GridView, AutoResizeColumn);
                        return;
                    }
                    GridView.OptionsNavigation.UseTabKey = false;
                    TableDef td = MetaData.GetTableDef(EntityType);
                    RepositoryItemTextEdit rte = null;

                    foreach (GridColumn col in GridView.Columns)
                    {
                        col.Caption = BaseUtility.SplitName(col.Caption);

                        RepositoryItemLookUpEditBase rle = col.ColumnEdit
                            as RepositoryItemLookUpEditBase;
                        if (rle != null)
                        {
                            RepositoryItemLookUpEdit rile = rle as RepositoryItemLookUpEdit;
                            if (rile != null)
                            {
                                foreach (LookUpColumnInfo c in rile.Columns)
                                    if (c.Caption.Length == 0)
                                        c.Caption = BaseUtility.SplitName(c.FieldName);
                            }
                            else
                            {
                                RepositoryItemGridLookUpEdit rgle =
                                    rle as RepositoryItemGridLookUpEdit;
                                if (rgle != null)
                                    foreach (GridColumn ggc in rgle.View.Columns)
                                        if (ggc.Caption.Length == 0)
                                            ggc.Caption = BaseUtility.SplitName(ggc.FieldName);
                            }
                        }
                        FieldDef fld = td.GetFieldDef(col.FieldName);
                        if (fld != null)
                        {
                            if (fld.IsEnum)
                                col.DisplayFormat.Format = DisplayEnumFormatter;
                            else
                                switch (fld.DataType)
                                {
                                    case DataType.Boolean:
                                        col.AppearanceHeader.TextOptions
                                            .HAlignment = HorzAlignment.Center;
                                        continue;
                                    case DataType.VarChar:
                                    case DataType.Char:
                                        continue;
                                    case DataType.Date:
                                    case DataType.DateTime:
                                    case DataType.TimeStamp:
                                        col.DisplayFormat.Format = ZeroDateFormatter;
                                        rte = new RepositoryItemTextEdit();
                                        rte.EditFormat.Format = ZeroDateFormatter;
                                        break;
                                    case DataType.Decimal:
                                    case DataType.Integer:
                                        col.AppearanceHeader.TextOptions
                                            .HAlignment = HorzAlignment.Far;
                                        rte = new RepositoryItemTextEdit();
                                        break;
                                    case DataType.Image:
                                        RepositoryItemPictureEdit riie = new RepositoryItemPictureEdit();
                                        col.ColumnEdit = riie;
                                        riie.SizeMode = PictureSizeMode.Zoom;
                                        riie.NullText = " ";
                                        continue;
                                    default:
                                        rte = new RepositoryItemTextEdit();
                                        break;
                                }
                            col.DisplayFormat.FormatType = FormatType.Custom;
                            col.DisplayFormat.FormatString = fld.FormatString;
                            //rte.EditFormat.FormatType = FormatType.Custom;
                            //rte.EditFormat.FormatString = fld.FormatString;
                            col.ColumnEdit = rte;
                        }
                        else
                        {
                            Type tp = col.ColumnType;
                            if (tp == typeof(bool))
                            {
                                col.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Center;
                                continue;
                            }
                            col.DisplayFormat.FormatType = FormatType.Custom;

                            if (tp == typeof(decimal) ||
                                tp == typeof(double) ||
                                tp == typeof(Single))
                            {
                                col.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Far;
                                col.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                            }
                            else if (tp == typeof(int))
                            {
                                col.AppearanceHeader.TextOptions
                                    .HAlignment = HorzAlignment.Far;
                                col.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                            }
                            else if (tp == typeof(DateTime))
                            {
                                col.DisplayFormat.Format = ZeroDateFormatter;
                                col.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;
                            }
                            else
                                continue;
                            rte = new RepositoryItemTextEdit();
                            col.ColumnEdit = rte;
                            rte.EditFormat.FormatType = FormatType.Custom;
                            rte.EditFormat.FormatString = col.DisplayFormat.FormatString;
                        }
                    }
                    if (AutoResizeColumn)
                    {
                        int OldMaxBestFit = GridView.BestFitMaxRowCount;
                        GridView.BestFitMaxRowCount = GridBestFitMaxRowCount;
                        GridView.BestFitColumns();
                        GridView.BestFitMaxRowCount = OldMaxBestFit;
                    }
                }
                public static void AutoFormatReadOnlyGridView(GridView GridView, bool AutoResizeColumn)
                {
                    GridView.OptionsNavigation.UseTabKey = false;
                    foreach (GridColumn gc in GridView.Columns)
                    {
                        gc.Caption = BaseUtility.SplitName(gc.Caption);

                        RepositoryItemLookUpEditBase rle = gc.ColumnEdit
                            as RepositoryItemLookUpEditBase;
                        if (rle != null)
                        {
                            RepositoryItemLookUpEdit rile = rle as RepositoryItemLookUpEdit;
                            if (rile != null)
                            {
                                foreach (LookUpColumnInfo c in rile.Columns)
                                    if (c.Caption.Length == 0)
                                        c.Caption = BaseUtility.SplitName(c.FieldName);
                            }
                            else
                            {
                                RepositoryItemGridLookUpEdit rgle =
                                    rle as RepositoryItemGridLookUpEdit;
                                if (rgle != null)
                                    foreach (GridColumn ggc in rgle.View.Columns)
                                        if (ggc.Caption.Length == 0)
                                            ggc.Caption = BaseUtility.SplitName(ggc.FieldName);
                            }
                        }
                        Type tp = gc.ColumnType;
                        if (tp == typeof(bool))
                        {
                            gc.AppearanceHeader.TextOptions
                                .HAlignment = HorzAlignment.Center;
                            continue;
                        }
                        gc.DisplayFormat.FormatType = FormatType.Custom;
                        if (tp == typeof(decimal) ||
                            tp == typeof(double) ||
                            tp == typeof(Single))
                        {
                            gc.AppearanceHeader.TextOptions
                                .HAlignment = HorzAlignment.Far;
                            gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatDecimal;
                        }
                        else if (tp == typeof(int))
                        {
                            gc.AppearanceHeader.TextOptions
                                .HAlignment = HorzAlignment.Far;
                            gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatInteger;
                        }
                        else if (tp == typeof(DateTime))
                        {
                            gc.DisplayFormat.Format = ZeroDateFormatter;
                            gc.DisplayFormat.FormatString = BaseUtility.DefaultFormatDate;
                        }

                        RepositoryItemTextEdit rte = new RepositoryItemTextEdit();
                        gc.ColumnEdit = rte;
                        rte.EditFormat.FormatType = FormatType.Custom;
                        rte.EditFormat.FormatString = gc.DisplayFormat.FormatString;
                    }
                    if (AutoResizeColumn)
                    {
                        int OldMaxBestFit = GridView.BestFitMaxRowCount;
                        GridView.BestFitMaxRowCount = GridBestFitMaxRowCount;
                        GridView.BestFitColumns();
                        GridView.BestFitMaxRowCount = OldMaxBestFit;
                    }
                }
            }

            [DebuggerNonUserCode]
            public static class Grid
            {
                public static void SetGridCanSelectAll(GridView gv, bool Enable)
                {
                    if (Enable)
                        gv.MouseDown += new MouseEventHandler(gv_MouseDown);
                    else
                        gv.MouseDown -= new MouseEventHandler(gv_MouseDown);
                }

                public static void SetGridCanCopy(GridView gv, bool Enable)
                {
                    if (Enable)
                        gv.ShowGridMenu += new GridMenuEventHandler(gv_ShowGridMenu);
                    else
                        gv.ShowGridMenu -= new GridMenuEventHandler(gv_ShowGridMenu);
                }

                private static void gv_ShowGridMenu(object sender, GridMenuEventArgs e)
                {
                    e.Menu.Items.Add(new DXMenuItem("&Copy (Ctrl-C)",
                        CopyMenu_Click, Properties.Resources.copy1));
                }

                private static void CopyMenu_Click(object sender, EventArgs e)
                {
                    Application.DoEvents();
                    ((GridView)sender).CopyToClipboard();
                }

                private static void gv_MouseDown(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        GridHitInfo hi = ((GridView)sender).CalcHitInfo(e.Location);
                        if (hi.HitTest == GridHitTest.ColumnButton && hi.Column == null)
                        {
                            ((GridView)sender).SelectAll();
                            return;
                        }
                    }
                }

                public static void DeleteFindRow(GridView gridView, GridColumn Column, object Value)
                {
                    int i = 0;
                    while (i >= 0)
                    {
                        i = gridView.LocateByValue(i, Column, Value);
                        if (i >= 0) gridView.DeleteRow(i);
                    }
                }

                private static bool ExportTo(GridView gridView, IExportProvider provider)
                {
                    using (new WaitCursor())
                    {
                        try
                        {
                            BaseExportLink link = gridView.CreateExportLink(provider);
                            (link as GridViewExportLink).ExpandAll = false;
                            link.ExportTo(true);
                            provider.Dispose();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Simpan Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                private static bool ExportToPdf(GridView gridView, string FileName)
                {
                    using (new WaitCursor())
                    {
                        try
                        {
                            gridView.ExportToPdf(FileName);
                            return true;
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Simpan Data",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }

                private static void OpenFile(string fileName)
                {
                    if (XtraMessageBox.Show(
                        "Membuka file hasil ekspor ?", "File Hasil Ekspor",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            BaseUtility.OpenFileWithDefaultApp(fileName);
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, "Error Membuka File",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                private static string ShowSaveFileDialog(string title, string filter, string defaultName)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.Title = "Simpan ke " + title;
                    dlg.FileName = defaultName.Replace(".", string.Empty);
                    dlg.Filter = filter;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        return dlg.FileName;
                    else
                        return string.Empty;
                }

                public static void Grid2XLS(GridView gridView, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("Excel Document", "XLS Documents|*.xls", defaultName);
                    if (fileName.Length > 0)
                    {
                        if (ExportTo(gridView, new ExportXlsProvider(fileName)))
                            OpenFile(fileName);
                    }
                }
                public static void Grid2HTML(GridView gridView, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("HTML Document", "HTML Documents|*.html", defaultName);
                    if (fileName.Length > 0)
                    {
                        if (ExportTo(gridView, new ExportHtmlProvider(fileName)))
                            OpenFile(fileName);
                    }
                }
                public static void Grid2Text(GridView gridView, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("Text Document", "Text Documents|*.txt", defaultName);
                    if (fileName.Length > 0)
                    {
                        if (ExportTo(gridView, new ExportTxtProvider(fileName)))
                            OpenFile(fileName);
                    }
                }
                public static void Grid2XML(GridView gridView, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("XML Document", "XML Documents|*.xml", defaultName);
                    if (fileName.Length > 0)
                    {
                        if (ExportTo(gridView, new ExportXmlProvider(fileName)))
                            OpenFile(fileName);
                    }
                }
                public static void Grid2Pdf(GridView gridView, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("Pdf Document", "Pdf Documents|*.pdf", defaultName);
                    if (fileName.Length > 0)
                    {
                        if (ExportToPdf(gridView, fileName))
                            OpenFile(fileName);
                    }
                }

                public static void Pivot2XLS(PivotGridControl pv, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("Excel Document", "XLS Documents|*.xls", defaultName);
                    if (fileName.Length > 0)
                    {
                        pv.ExportToXls(fileName, true);
                        OpenFile(fileName);
                    }
                }
                public static void Pivot2HTML(PivotGridControl pv, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("HTML Document", "HTML Documents|*.html", defaultName);
                    if (fileName.Length > 0)
                    {
                        pv.ExportToHtml(fileName);
                        OpenFile(fileName);
                    }
                }
                public static void Pivot2Text(PivotGridControl pv, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("Text Document", "Text Documents|*.txt", defaultName);
                    if (fileName.Length > 0)
                    {
                        pv.ExportToText(fileName);
                        OpenFile(fileName);
                    }
                }
                public static void Pivot2Pdf(PivotGridControl pv, string fileName, string defaultName)
                {
                    if (fileName.Length == 0)
                        fileName = ShowSaveFileDialog("XML Document", "XML Documents|*.xml", defaultName);
                    if (fileName.Length > 0)
                    {
                        pv.ExportToPdf(fileName);
                        OpenFile(fileName);
                    }
                }

                public static void MoveRowItem(GridView gridView, bool isMoveUp)
                {
                    if (gridView.Editable == false) return;
                    int r = gridView.FocusedRowHandle;
                    if (r < 0) return;

                    gridView.CollapseAllDetails();

                    IList Data = gridView.DataSource as IList;
                    if (Data == null) return;

                    if (isMoveUp)
                    {
                        if (r == 0) return;
                    }
                    else if (r == Data.Count - 1) return;

                    IEntityCollection ec;
                    if (Data as BindingSource != null)
                    {
                        BindingSource bs = Data as BindingSource;
                        while (bs.DataSource as BindingSource != null)
                            bs = (BindingSource)bs.DataSource;
                        ec = (IEntityCollection)((IBusinessEntity)bs.DataSource)
                            .GetEntityCollection(((BindingSource)Data).DataMember);
                    }
                    else
                        ec = (IEntityCollection)Data;

                    IBusinessEntity be;

                    if (ec != null)
                        be = ec.GetParent() as IBusinessEntity;
                    else
                        be = null;

                    gridView.BeginDataUpdate();
                    try
                    {
                        if (ec != null) ec.OnRowMove = true;
                        object TmpVal = Data[r];

                        if (be != null)
                        {
                            bool Cancel = false;
                            be.BeforeRowChildMoved(ec.ChildName, r,
                                (BusinessEntity)TmpVal, ref Cancel);
                            if (Cancel) return;
                        }

                        Data.RemoveAt(r);

                        r = isMoveUp ? r - 1 : r + 1;

                        Data.Insert(r, TmpVal);
                        if (be != null)
                            be.AfterRowChildMoved(ec.ChildName, r,
                                (BusinessEntity)TmpVal);
                    }
                    finally
                    {
                        if (ec != null) ec.OnRowMove = false;
                        gridView.FocusedRowHandle = r;
                        gridView.ClearSelection();
                        gridView.SelectRow(r);
                        gridView.EndDataUpdate();
                    }
                }
            }
        }

        //[DebuggerNonUserCode]
        public static class AutoDialog
        {
            static AutoDialog() { BaseWinFramework.Init(); }

            public static List<object[]> ShowSelectTable(DataPersistance Dp,
                string ReturnFields, string SqlSelect, 
                string Caption, string HideFields,
                params FieldParam[] Parameters)
            {
                frmSelectTable frm = new frmSelectTable(Dp);
                return frm.ShowForm(ReturnFields, SqlSelect, 
                    Caption, HideFields, Parameters);
            }

            public static xDialogResult ShowDialogTable(DataPersistance Dp,
                string SqlSelect, string Caption, string Message,
                xMessageBoxButtons Buttons, out bool IsDataExist, 
                params FieldParam[] Parameters)
            {
                frmShowTable frm = new frmShowTable(Dp);
                return frm.ShowForm(SqlSelect, Caption, Message, Buttons,
                    out IsDataExist, null, Parameters);
            }

            public static xDialogResult ShowDialogTable(DataPersistance Dp,
                string SqlSelect, string Caption, string Message,
                xMessageBoxButtons Buttons, out bool IsDataExist, 
                Dictionary<string, string> FormatCols,
                params FieldParam[] Parameters)
            {
                frmShowTable frm = new frmShowTable(Dp);
                return frm.ShowForm(SqlSelect, Caption, Message, Buttons,
                    out IsDataExist, FormatCols, Parameters);
            }

            #region ChooseEntity
            public static IList ChooseEntity<TEntity>(
                IList<TEntity> ListSource, string Caption,
                EntityColumnShow ecs) where TEntity : BaseEntity, new()
            {
                return _ChooseEntity(typeof(TEntity), (IList)ListSource,
                    null, null, string.Empty, string.Empty, false,
                    Caption, ecs);
            }

            public static IList ChooseEntity<TEntity>(
                IList<TEntity> ListSource, string Caption,
                EntityColumnShow ecs, IList OldListSelect) 
                where TEntity : BaseEntity, new()
            {
                return _ChooseEntity(typeof(TEntity), (IList)ListSource,
                    OldListSelect, null, string.Empty, string.Empty, false,
                    Caption, ecs);
            }

            public static IList ChooseEntity<TEntity>(
                string Conditions, string OrderCondition,
                bool CallLoadRule, string Caption,
                EntityColumnShow ecs, DataPersistance Dp,
                params FieldParam[] Parameters)
                where TEntity : BaseEntity, new()
            {
                return _ChooseEntity(typeof(TEntity), null, null, Dp,
                    Conditions, OrderCondition, CallLoadRule,
                    Caption, ecs, Parameters);
            }

            public static IList ChooseEntity<TEntity>(
                string Conditions, string OrderCondition,
                bool CallLoadRule, string Caption,
                EntityColumnShow ecs, DataPersistance Dp, 
                IList OldListSelect,
                params FieldParam[] Parameters)
                where TEntity : BaseEntity, new()
            {
                return _ChooseEntity(typeof(TEntity), null, OldListSelect, Dp,
                    Conditions, OrderCondition, CallLoadRule,
                    Caption, ecs, Parameters);
            }
            #endregion

            #region ChooseSingleEntity
            public static TEntity ChooseSingleEntity<TEntity>(
                IList<TEntity> ListSource, string Caption, EntityColumnShow ecs)
                where TEntity : BaseEntity, new()
            {
                return (TEntity)new frmChooseEntity2()
                    .ShowForm(ListSource, Caption, ecs);
            }

            public static TEntity ChooseSingleEntity<TEntity>(
                string Conditions, string OrderCondition, string Caption,
                EntityColumnShow ecs, DataPersistance Dp, 
                params FieldParam[] Parameters)
                where TEntity : BaseEntity, new()
            {
                AutoUpdateBindingList<TEntity> ListSource;
                ListSource = Dp.LoadEntities<TEntity>(
                    Conditions, OrderCondition, true, true);

                return new frmChooseEntity2()
                    .ShowForm((IList<TEntity>)ListSource, Caption, ecs);
            }
            #endregion

            internal static void InitAutoDialog()
            {
                BaseFramework.AutoDialog.ShowMessage = _ShowMessage;
                BaseFramework.AutoDialog.ShowSelectTable = ShowSelectTable;
                BaseFramework.AutoDialog.ShowDialogTable1 = ShowDialogTable;
                BaseFramework.AutoDialog.ShowDialogTable2 = ShowDialogTable;
                BaseFramework.AutoDialog.ChooseEntity = _ChooseEntity;
                BaseFramework.AutoDialog.ChooseSingleEntity = _ChooseSingleEntity;
                BaseFramework.AutoDialog.ShowDialog = _ShowDialog;
            }

            private static bool 
                _ShowDialog(Type FormType, ParentEntity Entity)
            {
                return
                    BaseWinFramework.ShowDocumentFormDialog(FormType, Entity) == 
                    System.Windows.Forms.DialogResult.OK;
            }

            private static void _ShowMessage(string Message, string Caption)
            {
                XtraMessageBox.Show(Message, Caption.Length > 0 ?
                    Caption : Application.ProductName,
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }

            private static IList _ChooseEntity(Type EntityType,
                IList ListSource, IList OldListSelect, DataPersistance Dp,
                string Conditions, string OrderCondition,
                bool CallLoadRule, string Caption, EntityColumnShow ecs,
                params FieldParam[] Parameters)
            {
                if (ListSource != null)
                    return new frmChooseEntity()
                        .ShowForm(ListSource, Caption, ecs, OldListSelect);
                else
                    return new frmChooseEntity().ShowForm((IList)
                        Dp.ListLoadEntities(EntityType, null, 
                        Conditions, OrderCondition, CallLoadRule, 
                        Parameters), Caption, ecs, OldListSelect);
            }

            private static object _ChooseSingleEntity(Type EntityType,
                IList ListSource, DataPersistance Dp, string Conditions,
                string OrderCondition, bool CallLoadRule,
                string Caption, EntityColumnShow ecs,
                params FieldParam[] Parameters)
            {
                if (ListSource == null)
                    ListSource = (IList)Dp.ListLoadEntities(EntityType, null,
                        Conditions, OrderCondition, CallLoadRule,
                        Parameters);
                return new frmChooseEntity2()
                    .ShowForm(ListSource, Caption, ecs);
            }
        }

        /// <summary>
        /// Memformat otomatis Tgl 1/1/1900 menjadi string kosong
        /// </summary>
        public static ZeroDateFormatter ZeroDateFormatter = new ZeroDateFormatter();
        public static DisplayEnumFormatter DisplayEnumFormatter = new DisplayEnumFormatter();
    }

    [DebuggerNonUserCode]
    public class ZeroDateFormatter : IFormatProvider, ICustomFormatter
    {
        internal static Type TCustomFormatter = typeof(ICustomFormatter);

        public DateTime ZeroDate = new DateTime(1900, 1, 1);
        public string ZeroDateDisplay = string.Empty;

        #region IFormatProvider Members
        object IFormatProvider.GetFormat(Type formatType)
        {
            return object.ReferenceEquals(formatType,
                TCustomFormatter) ? this : null;
        }
        #endregion

        #region ICustomFormatter Members
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            DateTime dt = (DateTime)arg;
            return dt == ZeroDate ? ZeroDateDisplay : dt.ToString(format);
        }
        #endregion
    }

    [DebuggerNonUserCode]
    public class DisplayEnumFormatter : IFormatProvider, ICustomFormatter
    {
        internal static Type TCustomFormatter = typeof(ICustomFormatter);

        #region IFormatProvider Members
        object IFormatProvider.GetFormat(Type formatType)
        {
            return object.ReferenceEquals(formatType,
                TCustomFormatter) ? this : null;
        }
        #endregion

        #region ICustomFormatter Members
        string ICustomFormatter.Format(string format, object arg, IFormatProvider formatProvider)
        {
            try
            {
                return EnumDef.GetEnumName(arg.GetType(), arg);
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
