using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraBars;
using SentraSecurity;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SentraSolutionFramework.Persistance;
using SentraWinFramework;
using DevExpress.XtraBars.Ribbon;
using DevExpress.Utils;
using SentraSolutionFramework;
using SentraWinSecurity.Properties;
using SentraSecurity.License;
using SentraWinSecurity.License;
using SentraSolutionFramework.Entity;
using System.Reflection;
using System.ComponentModel;

namespace SentraWinSecurity
{
    public delegate bool CheckValidation(int iParam);
    public delegate void ShowLogDocument(string DocumentId, 
        string ActionId, string DocumentNo, string Description);

    public static class BaseWinSecurity
    {
        private static bool CheckValidation(int iParam) { return true; }

        private static DataPersistance _DataPersistance;
        public static DataPersistance DataPersistance
        {
            get { return _DataPersistance ?? BaseFramework.DefaultDp; }
            set
            {
                _DataPersistance = value;
                BaseSecurity.CurrentLogin.Dp = value;
            }
        }

        public static CheckValidation TimeLicenseValidation = CheckValidation;
        public static CheckValidation TransactionLicenseValidation = CheckValidation;

        private static Dictionary<string, BarButtonItem> ListDocButton =
            new Dictionary<string, BarButtonItem>();
        private static Dictionary<string, BarButtonItem> ListDocBrowseButton =
            new Dictionary<string, BarButtonItem>();

        private static Dictionary<string, BarButtonItem> ListRptButton =
            new Dictionary<string, BarButtonItem>();
        private static Dictionary<string, BarButtonItem> ListActButton =
            new Dictionary<string, BarButtonItem>();

        /// <summary>
        /// List of button that enabled when Admin is Logged
        /// </summary>
        public static List<BarButtonItem> ListAdminButton =
            new List<BarButtonItem>();

        /// <summary>
        /// List of button that enabled when user is logged
        /// </summary>
        public static List<BarButtonItem> ListLoginButton =
            new List<BarButtonItem>();

        #region Register Modules
        public static ModuleAccess RegisterDocumentModule(
            string ModuleName, string FolderName)
        {
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterReportModule(
            string ModuleName, string FolderName)
        {
            return BaseSecurity.RegisterModuleAccess<frmSecurityReport>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterReportModule(
            string ModuleName, string FolderName, BarButtonItem ButtonItem)
        {
            ListRptButton[ModuleName] = ButtonItem;
            return BaseSecurity.RegisterModuleAccess<frmSecurityReport>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterModule<TFormSettingModuleAccess>(
            string ModuleName, string FolderName, BarButtonItem ButtonItem)
            where TFormSettingModuleAccess : IModuleAccessForm
        {
            ListDocButton[ModuleName] = ButtonItem;
            return BaseSecurity.RegisterModuleAccess<TFormSettingModuleAccess>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterFormModule(
            string ModuleName, string FolderName)
        {
            return BaseSecurity.RegisterModuleAccess<frmSecurityForm>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterFormModule(
            string ModuleName, string FolderName, BarButtonItem ButtonItem)
        {
            ListActButton[ModuleName] = ButtonItem;
            return BaseSecurity.RegisterModuleAccess<frmSecurityForm>(
                ModuleName, FolderName);
        }
        #endregion

        public static void Init(Form MdiParent,
            DoAction DoLogin, DoAction DoLogout, bool AutoCloseChildForm)
        {
            Init(MdiParent, DoLogin, DoLogout, AutoCloseChildForm, null);
        }

        public static void Init(Form MdiParent, 
            DoAction DoLogin, DoAction DoLogout, bool AutoCloseChildForm,
            RibbonPageGroup SystemPageGroup)
        {
            BaseWinFramework.MdiParent = MdiParent;
            MdiParent.Shown += new EventHandler(
                delegate(object sender, EventArgs e)
                {
                    Logout(AutoCloseChildForm);
                    if (DoLogout != null) DoLogout();
                    if (ShowLogin() && DoLogin != null) DoLogin();
                });

            PopupMenu pop = BaseWinFramework.CreateAppMenu();
            if (pop.Ribbon != null)
            {
                if (pop.Ribbon.Toolbar.ItemLinks.Count == 0)
                    pop.Ribbon.ToolbarLocation = 
                        RibbonQuickAccessToolbarLocation.Hidden;

                #region Add Main PopupMenu
                BarButtonItem bi = new BarButtonItem(null, "&Login");
                bi.Hint = "Login ke Aplikasi";
                bi.Glyph = Resources.key1;
                bi.ItemClick += new ItemClickEventHandler(
                    delegate(object sender, ItemClickEventArgs e)
                    {
                        if (AutoCloseChildForm)
                        {
                            if (!BaseWinFramework.CloseAllMdiChildForms()) return;
                        }
                        if (ShowLogin())
                        {
                            if (DoLogin != null) DoLogin();
                        }
                    });
                pop.ItemLinks.Add(bi);

                bi = new BarButtonItem(null, "Log&out");
                bi.Hint = "Logout dari Aplikasi";
                bi.Glyph = Resources.key1_delete;
                bi.ItemClick += new ItemClickEventHandler(
                    delegate(object sender, ItemClickEventArgs e)
                    {
                        Logout(AutoCloseChildForm);
                        if (DoLogout != null) DoLogout();
                    });
                pop.ItemLinks.Add(bi);

                bi = new BarButtonItem(null, "&Ubah Password");
                bi.Glyph = Resources.keys;
                bi.ItemClick += new ItemClickEventHandler(
                    delegate(object sender, ItemClickEventArgs e)
                    {
                        ShowChangePassword();
                    });
                pop.ItemLinks.Add(bi, true);

                bi = new BarButtonItem(null, "&Seting Tampilan");
                bi.Glyph = Resources.monitor_brush;
                bi.ItemClick += new ItemClickEventHandler(
                    delegate(object sender, ItemClickEventArgs e)
                    {
                        BaseWinFramework.Skin.ShowChangeSkin();
                    });
                pop.ItemLinks.Add(bi);

                bi = new BarButtonItem(null, "&Keluar", -1,
                    new BarShortcut(Keys.Control | Keys.X));
                bi.Glyph = Resources.forbidden;
                bi.ItemClick += new ItemClickEventHandler(
                    delegate(object sender, ItemClickEventArgs e)
                    {
                        MdiParent.Close();
                    });
                pop.ItemLinks.Add(bi, true);
                #endregion

                pop.Ribbon.ApplicationButtonDropDownControl = pop;

                #region Update SystemPageGroup
                if (SystemPageGroup != null)
                {
                    bi = new BarButtonItem(null, "Manajemen Hak Akses");
                    bi.Glyph = Resources.users3_preferences;
                    bi.ItemClick += new ItemClickEventHandler(
                        delegate(object sender, ItemClickEventArgs e)
                        {
                            ShowUserManagement();
                        });
                    SystemPageGroup.ItemLinks.Add(bi);
                    ListAdminButton.Add(bi);

                    bi = new BarButtonItem(null, "Log Transaksi");
                    bi.Glyph = Resources.user1_time;
                    bi.ItemClick += new ItemClickEventHandler(
                        delegate(object sender, ItemClickEventArgs e)
                        {
                            ShowUserLog(null);
                        });
                    SystemPageGroup.ItemLinks.Add(bi);
                    ListAdminButton.Add(bi);

                    bi = new BarButtonItem(null, "Manajemen Database");
                    bi.Glyph = Resources.data_disk;
                    bi.ItemClick += new ItemClickEventHandler(
                        delegate(object sender, ItemClickEventArgs e)
                        {
                            ShowDbManager();
                        });
                    SystemPageGroup.ItemLinks.Add(bi);
                    ListAdminButton.Add(bi);
                }
                #endregion
            }
            InitStatusBar();
        }

        private static void InitStatusBar()
        {
            if (BaseWinFramework.mdiRibbonStatusBar != null)
            {
                RibbonStatusBar BarStatus = BaseWinFramework.mdiRibbonStatusBar;
                BarItem Item = new BarStaticItem();
                Item.Tag = "Connection";
                Item.Glyph = Resources.Koneksi;
                BarStatus.ItemLinks.Add(Item).BeginGroup = true;

                Item = new BarStaticItem();
                Item.Tag = "User";
                Item.Glyph = Resources.User;
                BarStatus.ItemLinks.Add(Item);

                Item = new BarStaticItem();
                Item.Tag = "Role";
                Item.Glyph = Resources.Peran;
                BarStatus.ItemLinks.Add(Item);
            }
            else if (BaseWinFramework.mdiBar != null)
            {
                Bar BarStatus = BaseWinFramework.mdiBar;

                BarItem Item = new BarStaticItem();
                Item.Tag = "Connection";
                BarStatus.ItemLinks.Add(Item).BeginGroup = true;

                Item = new BarStaticItem();
                Item.Tag = "User";
                BarStatus.ItemLinks.Add(Item);

                Item = new BarStaticItem();
                Item.Tag = "Role";
                BarStatus.ItemLinks.Add(Item);
            }
        }

        private static void RegisterPopupControl()
        {
            if (popCtrl != null) return;

            if (BaseWinFramework.MdiParent == null)
            {
                popCtrl = new PopupMenu();
                return;
            }

            popCtrl = BaseWinFramework.CreatePopupMenu();
            if (BaseWinFramework.mdiRibbonControl != null)
                popCtrl.Ribbon = BaseWinFramework.mdiRibbonControl;
            else
                popCtrl.Manager = BaseWinFramework.mdiBarManager;

            BarButtonItem bi = new BarButtonItem(popCtrl.Manager, "&Dokumen");
            bi.Glyph = Resources.document_out;
            bi.ItemClick += new ItemClickEventHandler(
                delegate(object sender, ItemClickEventArgs e)
                {
                    if (CurrentButtonModule.SingleDoc)
                        BaseWinFramework.SingleEntityForm
                            .ShowForm(CurrentButtonModule.ModuleName);
                    else
                        BaseWinFramework.SingleEntityForm
                            .ShowNewForm(CurrentButtonModule.ModuleName);
                });
            if (BaseWinFramework.mdiRibbonControl != null)
                popCtrl.ItemLinks.Add(bi);
            else
                popCtrl.LinksPersistInfo.Add(new LinkPersistInfo(bi));
            bi = new BarButtonItem(popCtrl.Manager, "D&aftar Dokumen");
            bi.Glyph = Resources.index;
            bi.ItemClick += new ItemClickEventHandler(delegate(object sender, ItemClickEventArgs e)
                {
                    BaseWinFramework.SingleEntityForm
                        .ShowTabular(CurrentButtonModule.ModuleName);
                });
            if (BaseWinFramework.mdiRibbonControl != null)
                popCtrl.ItemLinks.Add(bi);
            else
                popCtrl.LinksPersistInfo.Add(new LinkPersistInfo(bi));
            popCtrl.BeforePopup += new CancelEventHandler(popCtrl_BeforePopup);
        }

        static void popCtrl_BeforePopup(object sender, CancelEventArgs e)
        {
            BarButtonItem bt = ((BarButtonItemLink)((PopupMenu)sender).Activator).Item;
            CurrentButtonModule = bt.Tag as ButtonModule;

            if (CurrentButtonModule != null)
            {
                if (CurrentButtonModule.CanView)
                {
                    BarItem br = popCtrl.ItemLinks[0].Item;

                    br.Visibility = BarItemVisibility.Always;
                    br.Caption = bt.Caption;
                }
                else
                    popCtrl.ItemLinks[0].Item.Visibility = BarItemVisibility.Never;

                if (CurrentButtonModule.CanReport)
                {
                    BarItem br = popCtrl.ItemLinks[1].Item;

                    br.Visibility = BarItemVisibility.Always;
                    br.Caption = "Daftar " + bt.Caption;
                }
                else
                    popCtrl.ItemLinks[1].Item.Visibility = BarItemVisibility.Never;
            }
        }

        #region Register Modules with Form
        /// <summary>
        /// Register Dialog Form or Dialog Document Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterSingleDocumentModule<TForm>(
            string ModuleName, string FolderName) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<frmSecurityForm>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Dialog Form or Dialog Document Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <param name="ButtonItem"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterSingleDocumentModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            ListDocButton[ModuleName] = ButtonItem;

            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            ButtonItem.Tag = "S" + ModuleName;

            return BaseSecurity.RegisterModuleAccess<frmSecurityForm>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterDocumentModule<TForm>(
            string ModuleName, string FolderName) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterDocumentModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            ListDocButton[ModuleName] = ButtonItem;

            if ((ButtonItem.ButtonStyle == BarButtonStyle.DropDown ||
                ButtonItem.ActAsDropDown) && ButtonItem.DropDownControl == null)
            {
                RegisterPopupControl();
                ButtonItem.DropDownControl = popCtrl;
                ButtonItem.Tag = new ButtonModule(ModuleName, false, false, true);
            }
            else
                ButtonItem.Tag = "D" + ModuleName;

            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterMultiDocumentModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            ListDocButton[ModuleName] = ButtonItem;

            if ((ButtonItem.ButtonStyle == BarButtonStyle.DropDown ||
                ButtonItem.ActAsDropDown) && ButtonItem.DropDownControl == null)
            {
                RegisterPopupControl();
                ButtonItem.DropDownControl = popCtrl;
                ButtonItem.Tag = new ButtonModule(ModuleName, false, false, false);
            }
            else
                ButtonItem.Tag = "M" + ModuleName;

            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        public static ModuleAccess RegisterDocumentModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem, BarButtonItem ButtonBrowse) where TForm : Form
        {
            ModuleAccess ma = RegisterDocumentModule<TForm>(
                ModuleName, FolderName, ButtonItem);
            ListDocBrowseButton.Add(ModuleName, ButtonBrowse);
            ButtonBrowse.Tag = ModuleName;
            ButtonBrowse.ItemClick += new ItemClickEventHandler(ButtonBrowse_ItemClick);
            return ma;

        }

        static void ButtonBrowse_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseWinFramework.SingleEntityForm.ShowTabular(e.Item.Tag.ToString());
        }

        private class ButtonModule
        {
            public string ModuleName;
            public bool CanView;
            public bool CanReport;
            public bool SingleDoc;

            public ButtonModule(string ModuleName, bool CanView, 
                bool CanReport, bool SingleDoc)
            {
                this.ModuleName = ModuleName;
                this.CanView = CanView;
                this.CanReport = CanReport;
                this.SingleDoc = SingleDoc;
            }
        }
        private static ButtonModule CurrentButtonModule;

        private static void ButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            BarButtonItem bt = (BarButtonItem)e.Item;
            if (bt.ActAsDropDown) return;

            CurrentButtonModule = bt.Tag as ButtonModule;
            if (CurrentButtonModule != null)
            {
                if (CurrentButtonModule.CanView)
                {
                    if (CurrentButtonModule.SingleDoc)
                        BaseWinFramework.SingleEntityForm
                            .ShowForm(CurrentButtonModule.ModuleName);
                    else
                        BaseWinFramework.SingleEntityForm
                            .ShowNewForm(CurrentButtonModule.ModuleName);
                }
                else if (CurrentButtonModule.CanReport)
                    BaseWinFramework.SingleEntityForm
                        .ShowTabular(CurrentButtonModule.ModuleName);
            }
            else
            {
                string Tag = bt.Tag.ToString();
                switch (Tag.Substring(0, 1))
                {
                    case "D":
                        BaseWinFramework.SingleEntityForm
                            .ShowForm(Tag.Substring(1));
                        break;
                    case "M":
                        BaseWinFramework.SingleEntityForm
                            .ShowNewForm(Tag.Substring(1));
                        break;
                    case "T":
                        BaseWinFramework.SingleEntityForm
                            .ShowTabular(Tag.Substring(1));
                        break;
                    case "F":
                        BaseWinFramework.SingleEntityForm
                            .ShowFreeLayout(Tag.Substring(1));
                        break;
                    case "S":
                        Type FormType, FilterFormType;
                        if (BaseWinFramework.GetFormType(Tag.Substring(1), out FormType,
                            out FilterFormType))
                            frmSingletonEntity.ShowDialog(FormType, Tag.Substring(1));
                        break;
                }
            }
        }

        /// <summary>
        /// Register Document Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <typeparam name="TFilterForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterDocumentModule<TForm, TFilterForm>(
            string ModuleName, string FolderName) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), typeof(TFilterForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Document Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <typeparam name="TFilterForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <param name="ButtonItem"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterDocumentModule<TForm, TFilterForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), typeof(TFilterForm), ModuleName);
            ListDocButton[ModuleName] = ButtonItem;

            if ((ButtonItem.ButtonStyle == BarButtonStyle.DropDown ||
                ButtonItem.ActAsDropDown) && ButtonItem.DropDownControl == null)
            {
                RegisterPopupControl();
                ButtonItem.DropDownControl = popCtrl;
                ButtonItem.Tag = new ButtonModule(ModuleName, false, false, true);
            }
            else
                ButtonItem.Tag = "D" + ModuleName;

            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Report Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterReportModule<TForm>(
            string ModuleName, string FolderName) where TForm : class
        {
            BaseWinFramework.SetModuleName(null, typeof(TForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<frmSecurityReport>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Report Form
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <param name="ButtonItem"></param>
        /// <param name="ReportType"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterReportModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem, ReportType ReportType) where TForm : class
        {
            BaseWinFramework.SetModuleName(null, typeof(TForm), ModuleName);
            ListRptButton[ModuleName] = ButtonItem;
            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            if (ReportType == ReportType.Tabular)
                ButtonItem.Tag = "T" + ModuleName;
            else
                ButtonItem.Tag = "F" + ModuleName;
            return BaseSecurity.RegisterModuleAccess<frmSecurityReport>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Module with User Defined Seting Module
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <typeparam name="TFilterForm"></typeparam>
        /// <typeparam name="TFormSettingModuleAccess"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterModule<TForm, TFilterForm, 
            TFormSettingModuleAccess>(string ModuleName, string FolderName)
            where TForm : Form
            where TFormSettingModuleAccess : IModuleAccessForm
        {
            BaseWinFramework.SetModuleName(typeof(TForm), typeof(TFilterForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<TFormSettingModuleAccess>(
                ModuleName, FolderName);
        }
        
        /// <summary>
        /// Register Module with User Defined Seting Module
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <typeparam name="TFilterForm"></typeparam>
        /// <typeparam name="TFormSettingModuleAccess"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <param name="ButtonItem"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterModule<TForm, TFilterForm, TFormSettingModuleAccess>(
            string ModuleName, string FolderName, BarButtonItem ButtonItem)
            where TForm : Form
            where TFormSettingModuleAccess : IModuleAccessForm
        {
            BaseWinFramework.SetModuleName(typeof(TForm), typeof(TFilterForm), ModuleName);
            ListDocButton[ModuleName] = ButtonItem;

            if ((ButtonItem.ButtonStyle == BarButtonStyle.DropDown ||
                ButtonItem.ActAsDropDown) && ButtonItem.DropDownControl == null)
            {
                RegisterPopupControl();
                ButtonItem.DropDownControl = popCtrl;
                ButtonItem.Tag = new ButtonModule(ModuleName, false, false, true);
            }
            else
                ButtonItem.Tag = "D" + ModuleName;
            
            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            return BaseSecurity.RegisterModuleAccess<TFormSettingModuleAccess>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Module with Standard Seting Module
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterFormModule<TForm>(
            string ModuleName, string FolderName) where TForm : Form
        {
            BaseWinFramework.SetModuleName(null, typeof(TForm), ModuleName);
            return BaseSecurity.RegisterModuleAccess<frmSecurityForm>(
                ModuleName, FolderName);
        }

        /// <summary>
        /// Register Module with Standard Seting Module
        /// </summary>
        /// <typeparam name="TForm"></typeparam>
        /// <param name="ModuleName"></param>
        /// <param name="FolderName"></param>
        /// <param name="ButtonItem"></param>
        /// <returns></returns>
        public static ModuleAccess RegisterFormModule<TForm>(
            string ModuleName, string FolderName,
            BarButtonItem ButtonItem) where TForm : Form
        {
            BaseWinFramework.SetModuleName(typeof(TForm), ModuleName);
            ListActButton[ModuleName] = ButtonItem;

            ButtonItem.ItemClick += new ItemClickEventHandler(ButtonItem_ItemClick);
            ButtonItem.Tag = "D" + ModuleName;

            return BaseSecurity.RegisterModuleAccess<frmSecurityDocument>(
                ModuleName, FolderName);
        }
        #endregion

        public static void ShowChangePassword()
        {
            if (!BaseSecurity.CurrentLogin.IsLogged())
            {
                XtraMessageBox.Show("Login terlebih dahulu untuk dapat mengubah password !", "Error Ubah Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (BaseSecurity.CurrentLogin.CurrentRole.Length > 0)
            {
                if (new frmChangePassword().ShowDialog() == DialogResult.OK)
                    XtraMessageBox.Show("Password Telah Berubah !",
                        "Konfirmasi Ubah Password",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
                XtraMessageBox.Show("Login menggunakan User Admin Default tidak dapat mengubah Password !",
                    "Konfirmasi Ubah Password",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool CheckModuleAccess(string ModuleName, 
            string VarName, object ExpectedValue)
        {
            if (ModuleName.Length > 0 && 
                BaseSecurity.CurrentLogin.CurrentRole.Length > 0)
            {
                ModuleAccess ma = BaseSecurity.GetModuleAccess(ModuleName);
                object maValue;

                if (!ma.Variables.TryGetValue(VarName, out maValue) ||
                    maValue == null || !maValue.Equals(ExpectedValue))
                    return false;
            }
            return true;
        }

        public static void ShowDbManager()
        {
            using (new WaitCursor())
            {
                if (!BaseSecurity.CurrentLogin.IsUserAdmin)
                {
                    XtraMessageBox.Show("Hanya Administrator yang dapat Menampilkan Manajemen Database !", "Error Manajemen Database",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                new frmDbManager().ShowDialog(BaseWinFramework.MdiParent);
            }
        }

        private static PopupMenu popCtrl;

        public static bool ShowLogin()
        {
            using (new WaitCursor())
            {
                if (!Connection.IsConnectionExist())
                {
                    XtraMessageBox.Show("Koneksi tidak ditemukan, anda harus membuat koneksi ke database terlebih dahulu untuk melanjutkan !",
                        "Error Membaca Data Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    new frmDbManager().ShowDialog(BaseWinFramework.MdiParent);
                    return false;
                }
                bool Success = false;
                bool ErrorLicense = false;
                DataPersistance OrigDp = BaseFramework.DefaultDp;
                try
                {
                    BaseWinFramework.MdiParent.SuspendLayout();
                    try
                    {
                        Success = new frmLogin().ShowDialog(
                            BaseWinFramework.MdiParent) == DialogResult.OK;
                        if (Success)
                        {
                            #region Cek Registrasi
                            Registration Reg = new Registration(BaseFramework.DefaultDp.EngineName);
                            if (!Reg.IsValidActivationCode())
                            {
                                ErrorLicense = true;
                                throw new ApplicationException(
                                    "Kode Aktivasi tidak sesuai dengan komputer yang digunakan. Database tidak bisa digunakan !");
                            }
                            switch (Reg.Limitation)
                            {
                                case 0:
                                    if (TransactionLicenseValidation == null)
                                    {
                                        ErrorLicense = true;
                                        throw new ApplicationException(
                                            "TransactionLicenseValidation belum dihandle ! hubungi Pembuat Aplikasi");
                                    }
                                    else
                                        if (!TransactionLicenseValidation(200))
                                        {
                                            ErrorLicense = true;
                                            throw new ApplicationException(
                                                "Jumlah Transaksi sudah melebihi 200 transaksi. Perbarui Lisensi Anda !");
                                        }
                                    break;
                                case 1:
                                    if (TimeLicenseValidation == null)
                                    {
                                        ErrorLicense = true;
                                        throw new ApplicationException(
                                            "TimeLicenseValidation belum dihandle ! hubungi Pembuat Aplikasi");
                                    }
                                    else
                                        if (!TimeLicenseValidation(Reg.MonthLimitation))
                                        {
                                            ErrorLicense = true;
                                            throw new ApplicationException(string.Concat(
                                                "Lama Transaksi sudah melebihi ",
                                                Reg.MonthLimitation.ToString(),
                                                " bulan. Perbarui Lisensi Anda !"));
                                        }
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            BaseFramework.DefaultDp = OrigDp;
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message, "Error Login",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (ErrorLicense)
                        {
                            Logout(true);
                            new frmRegistration().ShowForm(BaseFramework.DefaultDp,
                                BaseFramework.DefaultDp.EngineName);
                        }
                        BaseFramework.DefaultDp = OrigDp;
                        return false;
                    }
                    finally
                    {
                        if (BaseWinFramework.mdiRibbonStatusBar != null)
                            UpdateStatus(BaseWinFramework.mdiRibbonStatusBar);
                        else
                            UpdateStatus(BaseWinFramework.mdiBar);
                    }
                    AutoUpdateList.ClearAllCache();

                    foreach (KeyValuePair<string, BarButtonItem> kv in ListDocButton)
                        if (kv.Value.DropDownControl == null)
                            kv.Value.Enabled = CheckModuleAccess(
                                kv.Key, SecurityVarName.DocumentView, true);
                        else
                        {
                            ButtonModule bm = kv.Value.Tag as ButtonModule;
                            if (bm != null)
                            {
                                bm.CanView = CheckModuleAccess(
                                    kv.Key, SecurityVarName.DocumentView, true);
                                bm.CanReport = CheckModuleAccess(
                                    kv.Key, SecurityVarName.ReportView, true);
                                kv.Value.Enabled = bm.CanView || bm.CanReport;
                            }
                        }

                    foreach (KeyValuePair<string, BarButtonItem> kv in ListDocBrowseButton)
                        kv.Value.Enabled = CheckModuleAccess(kv.Key,
                            SecurityVarName.ReportView, true);

                    foreach (KeyValuePair<string, BarButtonItem> kv in ListRptButton)
                        kv.Value.Enabled = CheckModuleAccess(
                            kv.Key, SecurityVarName.ReportView, true);
                    foreach (KeyValuePair<string, BarButtonItem> kv in ListActButton)
                        kv.Value.Enabled = CheckModuleAccess(
                            kv.Key, SecurityVarName.DocumentView, true);

                    bool IsAdmin = BaseSecurity.CurrentLogin.IsUserAdmin;
                    foreach (BarButtonItem bi in ListAdminButton)
                        bi.Enabled = IsAdmin;
                    foreach (BarButtonItem bi in ListLoginButton)
                        bi.Enabled = true;

                    if (popCtrl != null && popCtrl.Ribbon != null)
                    {
                        PopupMenu pm = popCtrl.Ribbon.ApplicationButtonDropDownControl as PopupMenu;
                        if (pm != null)
                        {
                            pm.ItemLinks[1].Item.Enabled = true;
                            pm.ItemLinks[2].Item.Enabled = true;
                        }
                    }
                    if (BaseWinFramework.mdiRibbonControl != null)
                    {
                        bool AllDisabled;
                        foreach (BarItem bi in BaseWinFramework.mdiRibbonControl.Items)
                        {
                            BarButtonItem bbi = bi as BarButtonItem;
                            if (bbi == null || bbi.DropDownControl as PopupMenu == null) continue;
                            if (bi.Tag as ButtonModule != null) continue;

                            AllDisabled = true;
                            foreach (BarItemLink bil in ((PopupMenu)bbi.DropDownControl).ItemLinks)
                                if (bil.Enabled)
                                {
                                    AllDisabled = false;
                                    break;
                                }
                            bi.Enabled = !AllDisabled;
                        }
                        foreach (RibbonPage rp in BaseWinFramework.mdiRibbonControl.Pages)
                        {
                            AllDisabled = true;
                            foreach (RibbonPageGroup rpg in rp.Groups)
                            {
                                bool PageDisabled = true;
                                foreach (BarButtonItemLink bil in rpg.ItemLinks)
                                    if (bil.Enabled && bil.Visible &&
                                        bil.Item.Visibility != BarItemVisibility.Never)
                                    {
                                        AllDisabled = false;
                                        PageDisabled = false;
                                        break;
                                    }
                                rpg.Visible = !PageDisabled;
                            }
                            rp.Visible = !AllDisabled;
                        }

                        AllDisabled = true;
                        foreach (BarButtonItemLink bil in
                            BaseWinFramework.mdiRibbonControl.Toolbar.ItemLinks)
                            if (bil.Enabled && bil.Visible &&
                                bil.Item.Visibility != BarItemVisibility.Never)
                                AllDisabled = false;
                        if (AllDisabled)
                            BaseWinFramework.mdiRibbonControl
                                .ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
                        else
                            BaseWinFramework.mdiRibbonControl
                                .ToolbarLocation = RibbonQuickAccessToolbarLocation.Default;
                    }
                }
                finally
                {
                    BaseWinFramework.MdiParent.ResumeLayout();
                    BaseWinFramework.SingleEntityForm.ClearCache();
                }

                return Success;
            }
        }

        public static void Logout(bool AutoCloseChildForm)
        {
            if (AutoCloseChildForm)
            {
                if (!BaseWinFramework.CloseAllMdiChildForms()) return;
            }
            try
            {
                BaseWinFramework.MdiParent.SuspendLayout();

                BaseSecurity.CurrentLogin.Logout(); 
                foreach (BarButtonItem btn in ListDocButton.Values)
                    btn.Enabled = false;
                foreach (BarButtonItem btn in ListDocBrowseButton.Values)
                    btn.Enabled = false;
                foreach (BarButtonItem btn in ListRptButton.Values)
                    btn.Enabled = false;
                foreach (BarButtonItem btn in ListActButton.Values)
                    btn.Enabled = false;
                foreach (BarButtonItem btn in ListAdminButton)
                    btn.Enabled = false;
                foreach (BarButtonItem btn in ListLoginButton)
                    btn.Enabled = false;

                if (popCtrl == null) RegisterPopupControl();
                if (popCtrl != null && popCtrl.Ribbon != null)
                {
                    PopupMenu pm = popCtrl.Ribbon.ApplicationButtonDropDownControl as PopupMenu;
                    if (pm != null)
                    {
                        pm.ItemLinks[1].Item.Enabled = false;
                        pm.ItemLinks[2].Item.Enabled = false;
                    }
                }

                if (BaseWinFramework.mdiRibbonStatusBar != null)
                    foreach (BarItem bi in BaseWinFramework.mdiRibbonControl.Items)
                    {
                        BarButtonItem bbi = bi as BarButtonItem;
                        if (bbi == null || bbi.DropDownControl as PopupMenu == null) continue;
                        bi.Enabled = false;
                    }

                if (BaseWinFramework.mdiRibbonStatusBar != null)
                    UpdateStatus(BaseWinFramework.mdiRibbonStatusBar);
                else
                    UpdateStatus(BaseWinFramework.mdiBar);
                BaseWinFramework.mdiRibbonControl
                    .ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
            }
            finally
            {
                BaseWinFramework.MdiParent.ResumeLayout();
            }
        }

        public static void UpdateStatus(Bar BarStatus)
        {
            if (BarStatus == null) return;
            if (BaseSecurity.CurrentLogin.CurrentUser.Length == 0)
                foreach (BarItemLink ItemLink in BarStatus.ItemLinks)
                    switch (ItemLink.Item.Tag.ToString())
                    {
                        case "Connection":
                            ItemLink.Caption = "Koneksi: (Logout)";
                            break;
                        case "User":
                            ItemLink.Caption = "User: (Logout)";
                            break;
                        case "Role":
                            ItemLink.Caption = "Peran: (Logout)";
                            break;
                    }
            else
                foreach (BarItemLink ItemLink in BarStatus.ItemLinks)
                    switch (ItemLink.Item.Tag.ToString())
                    {
                        case "Connection":
                            ItemLink.Caption = "Koneksi: " + BaseSecurity.CurrentLogin.CurrentConnection;
                            break;
                        case "User":
                            ItemLink.Caption = "User: " + BaseSecurity.CurrentLogin.CurrentUser;
                            break;
                        case "Role":
                            if (BaseSecurity.CurrentLogin.CurrentRole.Length == 0)
                                ItemLink.Caption = "Peran: -";
                            else
                                ItemLink.Caption = "Peran: " + BaseSecurity.CurrentLogin.CurrentRole;
                            break;
                    }
        }
        public static void UpdateStatus(RibbonStatusBar BarStatus)
        {
            if (BarStatus == null) return;
            if (BaseSecurity.CurrentLogin.CurrentUser.Length == 0)
                foreach (BarItemLink ItemLink in BarStatus.ItemLinks)
                {
                    if (ItemLink.Item.Tag != null)
                        switch (ItemLink.Item.Tag.ToString())
                        {
                            case "Connection":
                                ItemLink.Caption = "Koneksi: (Logout)";
                                break;
                            case "User":
                                ItemLink.Caption = "User: (Logout)";
                                break;
                            case "Role":
                                if (BaseSecurity.LoginWithRole)
                                    ItemLink.Caption = "Peran: (Logout)";
                                else
                                    ItemLink.Visible = false;
                                break;
                        }
                }
            else
                foreach (BarItemLink ItemLink in BarStatus.ItemLinks)
                    if (ItemLink.Item.Tag != null)
                        switch (ItemLink.Item.Tag.ToString())
                        {
                            case "Connection":
                                ItemLink.Caption = "Koneksi: " + 
                                    BaseSecurity.CurrentLogin.CurrentConnection;
                                break;
                            case "User":
                                ItemLink.Caption = "User: " + 
                                    BaseSecurity.CurrentLogin.CurrentUser;
                                break;
                            case "Role":
                                if (BaseSecurity.LoginWithRole)
                                {
                                    if (BaseSecurity.CurrentLogin.CurrentRole.Length == 0)
                                        ItemLink.Caption = "Peran: -";
                                    else
                                        ItemLink.Caption = "Peran: " +
                                            BaseSecurity.CurrentLogin.CurrentRole;
                                }
                                else
                                    ItemLink.Visible = false;
                                break;
                        }
        }

        public static void ShowUserManagement()
        {
            using (new WaitCursor())
            {
                if (!BaseSecurity.CurrentLogin.IsUserAdmin)
                {
                    XtraMessageBox.Show("Hanya Administrator yang dapat Mengatur User, Peran, dan Hak Akses !", "Error Manajemen User",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                BaseWinFramework.SingleForm.ShowForm<frmUserRole>();
            }
        }

        internal static ShowLogDocument _ShowDocProc;
        public static void ShowUserLog(ShowLogDocument ShowDocProc)
        {
            using (new WaitCursor())
            {
                if (!BaseSecurity.CurrentLogin.IsUserAdmin)
                {
                    XtraMessageBox.Show("Hanya Administrator yang dapat melihat Log Aktivitas User !", 
                        "Error Melihat Log Aktivitas User",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                _ShowDocProc = ShowDocProc;
                BaseWinFramework.SingleForm.ShowForm<frmUserLog>();
            }
        }
    }
}
